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

    public class ReadWeatherForecastByDateHandler : IRequestHandler<ReadWeatherForecastByDateQuery, WeatherForecastQueryResponse>
    {
        private readonly ILogger<ReadWeatherForecastByDateHandler> logger;
        private readonly IMapper mapper;
        private readonly ConnectionStrings connectionStrings;

        public ReadWeatherForecastByDateHandler(ILogger<ReadWeatherForecastByDateHandler> logger,
            IOptions<ConnectionStrings> options, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
            connectionStrings = options.Value;
        }

        public async Task<WeatherForecastQueryResponse> Handle(ReadWeatherForecastByDateQuery request, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(connectionStrings.WeatherForecastsDb))
            {
                connection.Open();

                IEnumerable<WeatherForecast> forecasts = await connection.QueryAsync<WeatherForecast>(
                    @$"SELECT * FROM WeatherForecasts WHERE Date='{request.Date}'"
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