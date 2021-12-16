namespace CQRS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CQRS.Configuration;
    using CQRS.Exceptions;
    using CQRS.Model;

    using Dapper;

    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class QueryService : IQueryService
    {
        //TODO https://github.com/DapperLib/Dapper
        //TODO https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/cqrs-microservice-reads

        private readonly ILogger<QueryService> logger;
        private readonly ConnectionStrings connectionStrings;

        public QueryService(ILogger<QueryService> logger, IOptions<ConnectionStrings> options)
        {
            this.logger = logger;
            connectionStrings = options.Value;
        }

        public async Task<WeatherForecast> Read(Guid id)
        {

            using (SqlConnection connection = new SqlConnection(connectionStrings.WeatherForecastsDb))
            {
                connection.Open();

                IEnumerable<WeatherForecast> forecasts = await connection.QueryAsync<WeatherForecast>(
                    @$"SELECT *
                        FROM WeatherForecasts
                        WHERE Id='{id}'"
                );
                WeatherForecast forecast = forecasts.FirstOrDefault();

                if (forecast == null)
                {
                    logger.LogError("Couldn't find");
                    throw new KeyNotFoundException();
                }

                return forecast;
            }
        }

        public async Task<IEnumerable<WeatherForecast>> Read()
        {
            using (SqlConnection connection = new SqlConnection(connectionStrings.WeatherForecastsDb))
            {
                connection.Open();

                IEnumerable<WeatherForecast> forecasts = await connection.QueryAsync<WeatherForecast>(
                    @"SELECT *
                        FROM WeatherForecasts"
                );

                if (!forecasts.Any())
                {
                    logger.LogError("No records");
                    throw new NoResultException();
                }

                return forecasts;
            }

        }
    }
}
