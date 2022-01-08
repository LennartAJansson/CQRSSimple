namespace QueryMediators
{
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Contracts;
    using Contracts.Querys;

    using Dapper;

    using MediatR;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Models;

    public class ReadWeatherForecastByIdHandler : IRequestHandler<ReadWeatherForecastByIdQuery, WeatherForecastQueryResponse>
    {
        private readonly ILogger<ReadWeatherForecastByIdHandler> logger;
        private readonly IMapper mapper;
        private readonly ConnectionStrings connectionStrings;

        public ReadWeatherForecastByIdHandler(ILogger<ReadWeatherForecastByIdHandler> logger,
            IOptions<ConnectionStrings> options, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
            connectionStrings = options.Value;
        }

        public async Task<WeatherForecastQueryResponse> Handle(ReadWeatherForecastByIdQuery request, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(connectionStrings.WeatherForecastsDb))
            {
                connection.Open();

                IEnumerable<WeatherForecast> forecasts = await connection.QueryAsync<WeatherForecast>(
                    @$"SELECT * FROM WeatherForecasts WHERE WeatherForecastId='{request.WeatherForecastId}'"
                );

                WeatherForecast? forecast = forecasts.FirstOrDefault();

                if (forecast == null)
                {
                    logger.LogError("Couldn't find");
                    throw new KeyNotFoundException();
                }

                return mapper.Map<WeatherForecastQueryResponse>(forecast);
            }
        }
    }
}