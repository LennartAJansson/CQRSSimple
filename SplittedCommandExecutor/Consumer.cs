namespace SplittedCommandExecutor
{
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    using AutoMapper;

    using Contracts.Commands;

    using Microsoft.Extensions.Logging;

    using Models;

    using NATS.Client;

    using WeatherForecastsDb;

    public class Consumer : IConsumer<MsgHandlerEventArgs>
    {
        private readonly ILogger<Consumer> logger;
        private readonly IMapper mapper;
        private readonly WeatherForecastsContext context;

        public Consumer(ILogger<Consumer> logger, IMapper mapper, WeatherForecastsContext context)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.context = context;
        }

        public Task Consume(MsgHandlerEventArgs args)
        {
            Msg msg = args.Message;

            CreateOperationCommand? command = JsonSerializer.Deserialize<CreateOperationCommand>(msg.Data);
            Operation operation = mapper.Map<Operation>(command);
            context.Operations.Add(operation);
            context.SaveChanges();

            //Create Operation of message
            switch (command.Action)
            {
                case "CreateWeatherForecastCommand":
                    CreateWeatherForecastCommand? request = JsonSerializer.Deserialize<CreateWeatherForecastCommand>(command.RequestData);
                    WeatherForecast forecast = mapper.Map<WeatherForecast>(request);
                    //TODO Take in consideration if the temperature is Celsius or Fahrenheit
                    context.WeatherForecasts.Add(forecast);
                    context.SaveChanges();
                    operation.After = JsonSerializer.Serialize(forecast);
                    msg.Ack();
                    break;
                case "UpdateWeatherForecastCommand":
                    UpdateWeatherForecastCommand? updateRequest = JsonSerializer.Deserialize<UpdateWeatherForecastCommand>(command.RequestData);
                    WeatherForecast? target = context.WeatherForecasts.Find(updateRequest.WeatherForecastId);
                    operation.Before = JsonSerializer.Serialize(target);
                    WeatherForecast source = mapper.Map<WeatherForecast>(updateRequest);
                    //TODO Take in consideration if the temperature is Celsius or Fahrenheit
                    context.WeatherForecasts.Update(target);
                    context.SaveChanges();
                    operation.After = JsonSerializer.Serialize(target);
                    msg.Ack();
                    break;
                case "DeleteWeatherForecastCommand":
                    DeleteWeatherForecastCommand? deleteRequest = JsonSerializer.Deserialize<DeleteWeatherForecastCommand>(command.RequestData);
                    WeatherForecast? deleteTarget = context.WeatherForecasts.Find(deleteRequest.WeatherForecastId);
                    operation.Before = JsonSerializer.Serialize(deleteTarget);
                    context.WeatherForecasts.Remove(deleteTarget);
                    context.SaveChanges();
                    msg.Ack();
                    break;
                default:
                    logger.LogWarning("Action {action} unknown", command.Action);
                    msg.Nak();
                    break;
            }

            operation.Ready = true;
            context.Operations.Update(operation);
            context.SaveChanges();

            logger.LogInformation("Received message {data} on subject {subject}, stream {stream}, seqno {seqno}.",
                            Encoding.UTF8.GetString(msg.Data), msg.Subject, msg.MetaData.Stream, msg.MetaData.StreamSequence);

            return Task.CompletedTask;
        }
    }
}
