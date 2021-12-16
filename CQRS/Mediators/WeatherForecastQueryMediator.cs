namespace CQRS.Mediators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using CQRS.Contracts;
    using CQRS.Model;
    using CQRS.Services;

    using MediatR;

    using Microsoft.Extensions.Logging;

    public class WeatherForecastQueryMediator : IRequestHandler<ReadWeatherForecastRequest, ReadWeatherForecastResponse>,
        IRequestHandler<ReadWeatherForecastsRequest, IEnumerable<ReadWeatherForecastResponse>>

    {
        private readonly ILogger<WeatherForecastQueryMediator> logger;
        private readonly IQueryService service;

        public WeatherForecastQueryMediator(ILogger<WeatherForecastQueryMediator> logger, IQueryService service)
        {
            this.logger = logger;
            this.service = service;
        }

        public async Task<ReadWeatherForecastResponse> Handle(ReadWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for ReadWeatherForecastRequest");

            WeatherForecast w = await service.Read(request.Id);

            return new ReadWeatherForecastResponse(w.WeatherForecastId, w.Date, w.TemperatureC, w.TemperatureF, w.Summary);
        }

        public async Task<IEnumerable<ReadWeatherForecastResponse>> Handle(ReadWeatherForecastsRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for ReadWeatherForecastsRequest");

            IEnumerable<WeatherForecast> forecasts = await service.Read();

            return forecasts.Select(w => new ReadWeatherForecastResponse(w.WeatherForecastId, w.Date, w.TemperatureC, w.TemperatureF, w.Summary));
        }
    }
}
