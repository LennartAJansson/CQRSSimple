namespace QueryMediators
{
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts;
    using Contracts.Querys;

    using MediatR;

    using Microsoft.Extensions.Logging;

    public class ReadWeatherForecastByIdHandler : IRequestHandler<ReadWeatherForecastByIdQuery, WeatherForecastQueryResponse>
    {
        private readonly ILogger<ReadWeatherForecastByIdHandler> logger;

        public ReadWeatherForecastByIdHandler(ILogger<ReadWeatherForecastByIdHandler> logger) => this.logger = logger;
        public Task<WeatherForecastQueryResponse> Handle(ReadWeatherForecastByIdQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}