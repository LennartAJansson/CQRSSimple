namespace CQRS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CQRS.Data;
    using CQRS.Exceptions;
    using CQRS.Model;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class CommandService : ICommandService
    {
        private readonly ILogger<CommandService> logger;
        private readonly WeatherForecastsContext context;

        public CommandService(ILogger<CommandService> logger, WeatherForecastsContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<WeatherForecast> CreateWeatherForecast(WeatherForecast forecast)
        {
            await context.AddAsync(forecast);

            if (await context.SaveChangesAsync() != 1)
            {
                logger.LogError("Couldn't save");
                throw new NoResultException();
            }

            return forecast;
        }

        public async Task<WeatherForecast> UpdateWeatherForecast(WeatherForecast forecast)
        {
            if (forecast.WeatherForecastId == default)
            {
                logger.LogError("Not an existing record");
                throw new DbUpdateException();
            }

            WeatherForecast entry = await context.WeatherForecasts.FindAsync(forecast.WeatherForecastId);

            context.Entry(entry).CurrentValues.SetValues(forecast);

            await context.SaveChangesAsync();

            return forecast;
        }

        public async Task<WeatherForecast> DeleteWeatherForecast(Guid id)
        {
            WeatherForecast forecast = await context.FindAsync<WeatherForecast>(id);

            if (forecast == null)
            {
                logger.LogError("Couldn't find");
                throw new KeyNotFoundException();
            }

            context.Remove(forecast);
            await context.SaveChangesAsync();

            return forecast;
        }

        public async Task<Operation> CreateOperation(Operation operation)
        {
            await context.AddAsync(operation);

            if (await context.SaveChangesAsync() != 1)
            {
                logger.LogError("Couldn't save");
                throw new NoResultException();
            }

            return operation;
        }
    }
}
