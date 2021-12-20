namespace CQRS.Mediators
{
    using System;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using CQRS.Contracts;
    using CQRS.Model;
    using CQRS.Queue;

    using MediatR;

    using Microsoft.Extensions.Logging;

    public class CommandMediator : IRequestHandler<CreateWeatherForecastRequest, CreateWeatherForecastResponse>,
        IRequestHandler<UpdateWeatherForecastRequest, UpdateWeatherForecastResponse>,
        IRequestHandler<DeleteWeatherForecastRequest, DeleteWeatherForecastResponse>
    {
        private readonly ILogger<CommandMediator> logger;
        private readonly IOperationsMessenger operationsMessenger;

        public CommandMediator(ILogger<CommandMediator> logger, IOperationsMessenger operationsMessenger)
        {
            this.logger = logger;
            this.operationsMessenger = operationsMessenger;
        }

        public async Task<CreateWeatherForecastResponse> Handle(CreateWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for CreateWeatherForecastRequest");

            Operation operation = new()
            {
                OperationId = Guid.NewGuid(),
                Action = request.GetType().Name,
                Date = DateTime.Now,
                RequestData = JsonSerializer.Serialize(request),
                WeatherForecastId = Guid.NewGuid(),
                Before = "",
                After = ""
            };

            await operationsMessenger.SendOperation(this, operation);

            return new CreateWeatherForecastResponse(operation.WeatherForecastId);
        }

        public async Task<UpdateWeatherForecastResponse> Handle(UpdateWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for UpdateWeatherForecastRequest");

            Operation operation = new()
            {
                OperationId = default,
                Action = request.GetType().Name,
                Date = DateTime.Now,
                RequestData = JsonSerializer.Serialize(request),
                WeatherForecastId = request.WeatherForecastId,
                Before = "",
                After = ""
            };

            await operationsMessenger.SendOperation(this, operation);

            return new UpdateWeatherForecastResponse(request.WeatherForecastId);
        }

        public async Task<DeleteWeatherForecastResponse> Handle(DeleteWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for DeleteWeatherForecastRequest");

            Operation operation = new()
            {
                OperationId = default,
                Action = request.GetType().Name,
                Date = DateTime.Now,
                RequestData = JsonSerializer.Serialize(request),
                WeatherForecastId = request.WeatherForecastId,
                Before = "",
                After = ""
            };

            await operationsMessenger.SendOperation(this, operation);

            return new DeleteWeatherForecastResponse(request.WeatherForecastId);
        }
    }
}
