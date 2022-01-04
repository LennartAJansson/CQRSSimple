namespace AllInOne.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AllInOne.Configuration;
    using AllInOne.Exceptions;
    using AllInOne.Model;

    using Dapper;

    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class QueryService : IQueryService
    {
        //TODO https://github.com/DapperLib/Dapper
        //TODO https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/cqrs-microservice-reads
        // Tim Corey, negative thoughts on EF Core: https://www.youtube.com/watch?v=qkJ9keBmQWo

        private readonly ILogger<QueryService> logger;
        private readonly ConnectionStrings connectionStrings;

        public QueryService(ILogger<QueryService> logger, IOptions<ConnectionStrings> options)
        {
            this.logger = logger;
            connectionStrings = options.Value;
        }

        public async Task<WeatherForecast> ReadWeatherForecast(Guid weatherForecastId)
        {

            using (SqlConnection connection = new SqlConnection(connectionStrings.WeatherForecastsDb))
            {
                connection.Open();

                IEnumerable<WeatherForecast> forecasts = await connection.QueryAsync<WeatherForecast>(
                    @$"SELECT * FROM WeatherForecasts WHERE WeatherForecastId='{weatherForecastId}'"
                );
                WeatherForecast? forecast = forecasts.FirstOrDefault();

                if (forecast == null)
                {
                    logger.LogError("Couldn't find");
                    throw new KeyNotFoundException();
                }

                return forecast;
            }
        }

        public async Task<IEnumerable<WeatherForecast>> ReadWeatherForecasts()
        {
            using (SqlConnection connection = new SqlConnection(connectionStrings.WeatherForecastsDb))
            {
                connection.Open();

                IEnumerable<WeatherForecast> forecasts = await connection.QueryAsync<WeatherForecast>(
                    @"SELECT * FROM WeatherForecasts ORDER BY Date"
                );

                if (!forecasts.Any())
                {
                    logger.LogError("No records");
                    throw new NoResultException();
                }

                return forecasts;
            }
        }

        public async Task<IEnumerable<Operation>> ReadOperations()
        {
            using (SqlConnection connection = new SqlConnection(connectionStrings.WeatherForecastsDb))
            {
                connection.Open();

                IEnumerable<Operation> operations = await connection.QueryAsync<Operation>(
                    @"SELECT * FROM Operations ORDER BY Date"
                );

                if (!operations.Any())
                {
                    logger.LogError("No records");
                    throw new NoResultException();
                }

                return operations;
            }

        }
    }
}
