namespace CQRS.Operations
{
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using CQRS.Contracts;
    using CQRS.Model;
    using CQRS.Queue;
    using CQRS.Services;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class OperationsHandler : BackgroundService
    {
        private readonly ILogger<OperationsHandler> logger;
        private readonly IOperationsMessenger operationsMessenger;
        private readonly ICommandService command;
        private readonly IQueryService query;

        public OperationsHandler(ILogger<OperationsHandler> logger, IOperationsMessenger operationsMessenger, ICommandService command, IQueryService query)
        {
            this.logger = logger;
            this.operationsMessenger = operationsMessenger;
            this.operationsMessenger.NewOperationArrived += OperationArrived;
            this.command = command;
            this.query = query;
        }

        private async Task OperationArrived(object sender, Operation operation)
        {
            switch (operation.Action)
            {
                case "CreateWeatherForecastRequest":
                    await CreateWeatherForecast(operation);
                    break;
                case "UpdateWeatherForecastRequest":
                    await UpdateWeatherForecast(operation);
                    break;
                case "DeleteWeatherForecastRequest":
                    await DeleteWeatherForecast(operation);
                    break;
                default:
                    logger.LogError("Failed to handle operation {id}", operation.OperationId);
                    break;
            }

            operation = await command.CreateOperation(operation);
        }

        private async Task CreateWeatherForecast(Operation operation)
        {
            CreateWeatherForecastRequest request = JsonSerializer.Deserialize<CreateWeatherForecastRequest>(operation.RequestData);
            WeatherForecast forecast = new WeatherForecast
            {
                WeatherForecastId = operation.WeatherForecastId,
                Date = request.Date,
                Summary = request.Summary
            };

            if (!request.IsCelsius)
            {
                forecast.TemperatureC = (int)((request.Temperature - 32) * 0.5556);
            }
            else
            {
                forecast.TemperatureC = request.Temperature;
            }

            operation.Before = "";
            forecast = await command.CreateWeatherForecast(forecast);
            operation.After = JsonSerializer.Serialize(forecast);
            operation.Ready = true;
        }
        private async Task UpdateWeatherForecast(Operation operation)
        {
            UpdateWeatherForecastRequest request = JsonSerializer.Deserialize<UpdateWeatherForecastRequest>(operation.RequestData);
            WeatherForecast forecast = await query.Read(request.WeatherForecastId);
            operation.Before = JsonSerializer.Serialize(forecast);

            forecast.Date = request.Date;
            forecast.Summary = request.Summary;
            forecast.TemperatureC = request.IsCelsius ? request.Temperature : (int)((request.Temperature - 32) * 0.5556);

            forecast = await command.UpdateWeatherForecast(forecast);
            operation.After = JsonSerializer.Serialize(forecast);
            operation.Ready = true;
        }

        private async Task DeleteWeatherForecast(Operation operation)
        {
            DeleteWeatherForecastRequest request = JsonSerializer.Deserialize<DeleteWeatherForecastRequest>(operation.RequestData);

            operation.Before = JsonSerializer.Serialize(await query.Read(request.WeatherForecastId));
            WeatherForecast forecast = await command.DeleteWeatherForecast(request.WeatherForecastId);
            operation.After = JsonSerializer.Serialize("");
            operation.Ready = true;
        }
        //JsonSerializerOptions deserializeOptions = new JsonSerializerOptions();
        //deserializeOptions.Converters.Add(new WeatherForecastJsonConverter());
        //string after = JsonSerializer.Serialize<WeatherForecast>(forecast, deserializeOptions);

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StartAsync");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("ExecuteAsync");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StopAsync");

            return base.StopAsync(cancellationToken);
        }
    }
}
