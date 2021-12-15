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
        IRequestHandler<ReadWeatherForecastsRequest, ReadWeatherForecastsResponse>

    {
        private readonly ILogger<WeatherForecastQueryMediator> logger;
        private readonly IWeatherForecastsService service;

        public WeatherForecastQueryMediator(ILogger<WeatherForecastQueryMediator> logger, IWeatherForecastsService service)
        {
            this.logger = logger;
            this.service = service;
        }

        public async Task<ReadWeatherForecastResponse> Handle(ReadWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for ReadWeatherForecastRequest");

            //Get from database
            WeatherForecast w = await service.Read(request.Id);

            return new ReadWeatherForecastResponse(w.Id, w.Date, w.TemperatureC, w.Summary);
        }

        public async Task<ReadWeatherForecastsResponse> Handle(ReadWeatherForecastsRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for ReadWeatherForecastsRequest");

            //Get from database
            IEnumerable<WeatherForecast> forecasts = await service.Read();

            return new ReadWeatherForecastsResponse(forecasts.Select(w => new ReadWeatherForecastResponse(w.Id, w.Date, w.TemperatureC, w.Summary)));
        }

    }
}
