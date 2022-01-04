namespace AllInOne.Mediators
{
    using System;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using AllInOne.Contracts;
    using AllInOne.Messengers;
    using AllInOne.Model;

    using MediatR;

    using MethodTimer;

    using Microsoft.Extensions.Logging;

    public class CommandMediator : IRequestHandler<CreateWeatherForecastRequest, WeatherForecastIdResponse>,
        IRequestHandler<UpdateWeatherForecastRequest, WeatherForecastIdResponse>,
        IRequestHandler<DeleteWeatherForecastRequest, WeatherForecastIdResponse>
    {
        private readonly ILogger<CommandMediator> logger;
        private readonly IOperationsMessenger operationsMessenger;

        public CommandMediator(ILogger<CommandMediator> logger, IOperationsMessenger operationsMessenger)
        {
            this.logger = logger;
            this.operationsMessenger = operationsMessenger;
        }

        [Time]
        public async Task<WeatherForecastIdResponse> Handle(CreateWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            if (operationsMessenger != null && request != null)
            {
                Operation? operation = new()
                {
                    OperationId = Guid.NewGuid(),
                    Action = request.GetType().Name,
                    Date = DateTime.Now,
                    RequestData = JsonSerializer.Serialize(request),
                    WeatherForecastId = Guid.NewGuid(),
                    Before = "",
                    After = ""
                };

                if (operation != null)
                {
                    await operationsMessenger.QueueOperation(operation);

                    return new WeatherForecastIdResponse(operation.WeatherForecastId);
                }
            }

            logger.LogError("Not able to handle command");
            throw new OperationCanceledException();
        }

        [Time]
        public async Task<WeatherForecastIdResponse> Handle(UpdateWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            if (operationsMessenger != null && request != null)
            {
                Operation? operation = new()
                {
                    OperationId = default,
                    Action = request.GetType().Name,
                    Date = DateTime.Now,
                    RequestData = JsonSerializer.Serialize(request),
                    WeatherForecastId = request.WeatherForecastId,
                    Before = "",
                    After = ""
                };

                if (this != null && operation != null)
                {
                    await operationsMessenger.QueueOperation(operation);

                    return new WeatherForecastIdResponse(request.WeatherForecastId);
                }
            }

            logger.LogError("Not able to handle command");
            throw new OperationCanceledException();
        }

        [Time]
        public async Task<WeatherForecastIdResponse> Handle(DeleteWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            if (operationsMessenger != null && request != null)
            {
                Operation? operation = new()
                {
                    OperationId = default,
                    Action = request.GetType().Name,
                    Date = DateTime.Now,
                    RequestData = JsonSerializer.Serialize(request),
                    WeatherForecastId = request.WeatherForecastId,
                    Before = "",
                    After = ""
                };

                if (operation != null && operation.Action != null && operation.After != null && operation.Before != null)
                {
                    await operationsMessenger.QueueOperation(operation);

                    return new WeatherForecastIdResponse(request.WeatherForecastId);
                }
            }

            logger.LogError("Not able to handle command");
            throw new OperationCanceledException();
        }
    }
}
