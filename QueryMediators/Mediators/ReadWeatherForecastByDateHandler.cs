namespace QueryMediators
{
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts;
    using Contracts.Querys;

    using MediatR;

    public class ReadWeatherForecastByDateHandler : IRequestHandler<ReadWeatherForecastByDateQuery, WeatherForecastResponse>
    {
        public Task<WeatherForecastResponse> Handle(ReadWeatherForecastByDateQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}