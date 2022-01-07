namespace QueryMediators
{
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts;
    using Contracts.Querys;

    using MediatR;

    using Microsoft.Extensions.Logging;

    public class ReadWeatherForecastsHandler : IRequestHandler<ReadWeatherForecastsQuery, IEnumerable<WeatherForecastQueryResponse>>
    {
        private readonly ILogger<ReadWeatherForecastsHandler> logger;

        public ReadWeatherForecastsHandler(ILogger<ReadWeatherForecastsHandler> logger) => this.logger = logger;
        public Task<IEnumerable<WeatherForecastQueryResponse>> Handle(ReadWeatherForecastsQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}