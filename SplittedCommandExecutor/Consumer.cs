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
            if (command != null)
            {
                Operation operation = mapper.Map<Operation>(command);

                if (context.Operations != null && context.WeatherForecasts != null)
                {
                    context.Operations.Add(operation);
                    context.SaveChanges();

                    switch (command.Action)
                    {
                        case "CreateWeatherForecastCommand":
                            CreateWeatherForecastCommand? createRequest = JsonSerializer.Deserialize<CreateWeatherForecastCommand>(command.RequestData);
                            if (createRequest != null)
                            {
                                WeatherForecast createTarget = mapper.Map<WeatherForecast>(createRequest);
                                createTarget.WeatherForecastId = operation.WeatherForecastId;
                                if (createTarget != null)
                                {
                                    operation.Before = string.Empty;

                                    context.WeatherForecasts.Add(createTarget);
                                    context.SaveChanges();

                                    operation.After = JsonSerializer.Serialize(createTarget);

                                    msg.Ack();
                                }
                            }
                            break;
                        case "UpdateWeatherForecastCommand":
                            UpdateWeatherForecastCommand? updateRequest = JsonSerializer.Deserialize<UpdateWeatherForecastCommand>(command.RequestData);
                            if (updateRequest != null)
                            {
                                WeatherForecast? updateTarget = context.WeatherForecasts.Find(updateRequest.WeatherForecastId);
                                if (updateTarget != null)
                                {
                                    operation.Before = JsonSerializer.Serialize(updateTarget);

                                    WeatherForecast updateSource = mapper.Map<WeatherForecast>(updateRequest);
                                    updateTarget.CopyFrom(updateSource);

                                    context.WeatherForecasts.Update(updateTarget);
                                    context.SaveChanges();

                                    operation.After = JsonSerializer.Serialize(updateTarget);

                                    msg.Ack();
                                }
                            }
                            break;
                        case "DeleteWeatherForecastCommand":
                            DeleteWeatherForecastCommand? deleteRequest = JsonSerializer.Deserialize<DeleteWeatherForecastCommand>(command.RequestData);
                            if (deleteRequest != null)
                            {
                                WeatherForecast? deleteTarget = context.WeatherForecasts.Find(deleteRequest.WeatherForecastId);
                                if (deleteTarget != null)
                                {
                                    operation.Before = JsonSerializer.Serialize(deleteTarget);

                                    context.WeatherForecasts.Remove(deleteTarget);
                                    context.SaveChanges();

                                    operation.After = string.Empty;

                                    msg.Ack();
                                }
                            }
                            break;
                        default:
                            logger.LogWarning("Action {action} unknown", command.Action);

                            msg.Nak();
                            break;
                    }

                    operation.Ready = true;
                    context.Operations.Update(operation);
                    context.SaveChanges();
                }
            }

            logger.LogInformation("Received message {data} on subject {subject}, stream {stream}, seqno {seqno}.",
                                Encoding.UTF8.GetString(msg.Data), msg.Subject, msg.MetaData.Stream, msg.MetaData.StreamSequence);

            return Task.CompletedTask;
        }
    }
}
