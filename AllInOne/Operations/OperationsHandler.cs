namespace AllInOne.Operations
{
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using AllInOne.Contracts;
    using AllInOne.Messengers;
    using AllInOne.Model;
    using AllInOne.Services;

    using MethodTimer;

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
            this.operationsMessenger.NewOperationReceived += OperationReceived;
            this.command = command;
            this.query = query;
        }

        private async Task OperationReceived(Operation operation)
        {
            if (operation != null && operation.Action != null && operation.RequestData != null)
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

                _ = await command.CreateOperation(operation);
            }
        }

        [Time]
        private async Task CreateWeatherForecast(Operation operation)
        {
            if (operation != null && operation.RequestData != null)
            {
                CreateWeatherForecastRequest? request = JsonSerializer.Deserialize<CreateWeatherForecastRequest>(operation.RequestData);

                if (request != null)
                {
                    WeatherForecast forecast = new WeatherForecast
                    {
                        WeatherForecastId = operation.WeatherForecastId,
                        Date = request.Date,
                        Summary = request.Summary
                    };

                    forecast.TemperatureC = request.IsCelsius ? request.Temperature : (int)((request.Temperature - 32) * 0.5556);
                    operation.Before = "";

                    forecast = await command.CreateWeatherForecast(forecast);

                    operation.After = JsonSerializer.Serialize(forecast);
                    operation.Ready = true;
                }
            }
        }

        [Time]
        private async Task UpdateWeatherForecast(Operation operation)
        {
            if (operation != null && operation.RequestData != null)
            {
                UpdateWeatherForecastRequest? request = JsonSerializer.Deserialize<UpdateWeatherForecastRequest>(operation.RequestData);

                if (request != null)
                {
                    WeatherForecast? forecast = await query.ReadWeatherForecast(request.WeatherForecastId);
                    operation.Before = JsonSerializer.Serialize(forecast);

                    forecast.Date = request.Date;
                    forecast.Summary = request.Summary;
                    forecast.TemperatureC = request.IsCelsius ? request.Temperature : (int)((request.Temperature - 32) * 0.5556);

                    forecast = await command.UpdateWeatherForecast(forecast);

                    operation.After = JsonSerializer.Serialize(forecast);
                    operation.Ready = true;
                }
            }
        }

        [Time]
        private async Task DeleteWeatherForecast(Operation operation)
        {
            if (operation != null && operation.RequestData != null)
            {
                DeleteWeatherForecastRequest? request = JsonSerializer.Deserialize<DeleteWeatherForecastRequest>(operation.RequestData);

                if (request != null)
                {
                    operation.Before = JsonSerializer.Serialize(await query.ReadWeatherForecast(request.WeatherForecastId));

                    _ = await command.DeleteWeatherForecast(request.WeatherForecastId);

                    operation.After = JsonSerializer.Serialize("");
                    operation.Ready = true;
                }
            }
        }

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
