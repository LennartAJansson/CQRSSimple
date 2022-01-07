namespace QueryMediators
{
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts;
    using Contracts.Querys;

    using MediatR;

    public class ReadWeatherForecastByIdHandler : IRequestHandler<ReadWeatherForecastByIdQuery, WeatherForecastResponse>
    {
        public Task<WeatherForecastResponse> Handle(ReadWeatherForecastByIdQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}