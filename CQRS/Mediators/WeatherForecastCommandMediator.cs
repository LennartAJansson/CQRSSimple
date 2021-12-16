namespace CQRS.Mediators
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CQRS.Contracts;
    using CQRS.Model;
    using CQRS.Services;

    using MediatR;

    using Microsoft.Extensions.Logging;

    public class WeatherForecastCommandMediator : IRequestHandler<CreateWeatherForecastRequest, CreateWeatherForecastResponse>,
        IRequestHandler<UpdateWeatherForecastRequest, UpdateWeatherForecastResponse>,
        IRequestHandler<DeleteWeatherForecastRequest, DeleteWeatherForecastResponse>
    {
        private readonly ILogger<WeatherForecastCommandMediator> logger;
        private readonly ICommandService command;
        private readonly IQueryService query;

        public WeatherForecastCommandMediator(ILogger<WeatherForecastCommandMediator> logger, ICommandService command, IQueryService query)
        {
            this.logger = logger;
            this.command = command;
            this.query = query;
        }
        public async Task<CreateWeatherForecastResponse> Handle(CreateWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for CreateWeatherForecastRequest");

            WeatherForecast forecast = null;

            if (!request.IsCelsius)
            {
                forecast = new() { WeatherForecastId = default, Date = request.Date, TemperatureC = (int)((request.Temperature - 32) * 0.5556), Summary = request.Summary };
            }
            else
            {
                forecast = new() { WeatherForecastId = default, Date = request.Date, TemperatureC = request.Temperature, Summary = request.Summary };
            }

            //Store in database
            forecast = await command.CreateWeatherForecast(forecast);
            string after = System.Text.Json.JsonSerializer.Serialize(forecast);

            Operation operation = new()
            {
                OperationId = default,
                Action = "CreateWeatherForecast",
                Date = DateTime.Now,
                Data = System.Text.Json.JsonSerializer.Serialize(request),
                WeatherForecastId = forecast.WeatherForecastId,
                After = after
            };

            operation = await command.CreateOperation(operation);

            return new CreateWeatherForecastResponse(forecast.WeatherForecastId, forecast.Date, forecast.TemperatureC, forecast.Summary);
        }

        public async Task<UpdateWeatherForecastResponse> Handle(UpdateWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for UpdateWeatherForecastRequest");

            WeatherForecast forecast = new() { WeatherForecastId = request.Id, Date = request.Date, TemperatureC = request.Temperature, Summary = request.Summary };

            //Update in database
            forecast = await command.UpdateWeatherForecast(forecast);
            string after = System.Text.Json.JsonSerializer.Serialize(forecast);

            Operation operation = new()
            {
                OperationId = default,
                Action = "UpdateWeatherForecast",
                Date = DateTime.Now,
                Data = System.Text.Json.JsonSerializer.Serialize(request),
                WeatherForecastId = forecast.WeatherForecastId,
                After = after
            };

            operation = await command.CreateOperation(operation);

            return new UpdateWeatherForecastResponse(forecast.WeatherForecastId, forecast.Date, forecast.TemperatureC, forecast.Summary);
        }

        public async Task<DeleteWeatherForecastResponse> Handle(DeleteWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for DeleteWeatherForecastRequest");

            //Delete in database
            WeatherForecast forecast = await command.DeleteWeatherForecast(request.Id);
            string after = "";

            Operation operation = new()
            {
                OperationId = default,
                Action = "UpdateWeatherForecast",
                Date = DateTime.Now,
                Data = System.Text.Json.JsonSerializer.Serialize(request),
                WeatherForecastId = forecast.WeatherForecastId,
                After = after
            };

            operation = await command.CreateOperation(operation);

            return new DeleteWeatherForecastResponse(forecast.WeatherForecastId, forecast.Date, forecast.TemperatureC, forecast.Summary);
        }
    }
}
