namespace QueryMediators
{
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts;
    using Contracts.Querys;

    using MediatR;

    public class ReadWeatherForecastsHandler : IRequestHandler<ReadWeatherForecastsQuery, IEnumerable<WeatherForecastResponse>>
    {
        public Task<IEnumerable<WeatherForecastResponse>> Handle(ReadWeatherForecastsQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}