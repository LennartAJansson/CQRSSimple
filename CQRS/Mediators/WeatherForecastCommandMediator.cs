namespace CQRS.Mediators
{
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
        private readonly IWeatherForecastsService service;

        public WeatherForecastCommandMediator(ILogger<WeatherForecastCommandMediator> logger, IWeatherForecastsService service)
        {
            this.logger = logger;
            this.service = service;
        }
        public async Task<CreateWeatherForecastResponse> Handle(CreateWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for CreateWeatherForecastRequest");

            int temp = request.Temperature;
            WeatherForecast f;
            if (!request.IsCelsius)
            {
                f = new() { Date = request.Date, TemperatureC = (int)((request.Temperature - 32) * 0.5556), Summary = request.Summary };
            }
            else
            {
                f = new() { Date = request.Date, TemperatureC = request.Temperature, Summary = request.Summary };
            }

            //Store in database
            f = await service.Create(f);

            return new CreateWeatherForecastResponse(f.Id, f.Date, f.TemperatureC, f.Summary);
        }

        public async Task<UpdateWeatherForecastResponse> Handle(UpdateWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for UpdateWeatherForecastRequest");

            WeatherForecast f = new() { Id = request.Id, Date = request.Date, TemperatureC = request.Temperature, Summary = request.Summary };

            //Update in database
            f = await service.Update(f);

            return new UpdateWeatherForecastResponse(f.Id, f.Date, f.TemperatureC, f.Summary);
        }

        public async Task<DeleteWeatherForecastResponse> Handle(DeleteWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for DeleteWeatherForecastRequest");

            //Delete in database
            WeatherForecast w = await service.Delete(request.Id);

            return new DeleteWeatherForecastResponse(w.Id, w.Date, w.TemperatureC, w.Summary);
        }
    }
}
