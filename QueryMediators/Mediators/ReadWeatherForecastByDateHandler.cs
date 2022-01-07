namespace QueryMediators
{
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts;
    using Contracts.Querys;

    using MediatR;

    using Microsoft.Extensions.Logging;

    public class ReadWeatherForecastByDateHandler : IRequestHandler<ReadWeatherForecastByDateQuery, WeatherForecastQueryResponse>
    {
        private readonly ILogger<ReadWeatherForecastByDateHandler> logger;

        public ReadWeatherForecastByDateHandler(ILogger<ReadWeatherForecastByDateHandler> logger) => this.logger = logger;
        public Task<WeatherForecastQueryResponse> Handle(ReadWeatherForecastByDateQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}