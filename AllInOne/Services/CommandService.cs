namespace AllInOne.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AllInOne.Data;
    using AllInOne.Exceptions;
    using AllInOne.Model;

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
            if (context != null && context.WeatherForecasts != null && forecast != null)
            {
                await context.WeatherForecasts.AddAsync(forecast);

                if (await context.SaveChangesAsync() != 1)
                {
                    logger.LogError("Couldn't save forecast");
                    throw new NoResultException();
                }

                return forecast;
            }

            logger.LogError("Not able to create forecast");
            throw new DbUpdateException();
        }

        public async Task<WeatherForecast> UpdateWeatherForecast(WeatherForecast forecast)
        {
            if (context != null && context.WeatherForecasts != null && forecast != null)
            {
                if (forecast.WeatherForecastId == default)
                {
                    logger.LogError("Not an existing record");
                    throw new DbUpdateException();
                }

                WeatherForecast? entry = await context.WeatherForecasts.FindAsync(forecast.WeatherForecastId);

                if (entry == null)
                {
                    logger.LogError("Couldn't find forecast");
                    throw new KeyNotFoundException();
                }

                context.Entry(entry).CurrentValues.SetValues(forecast);

                await context.SaveChangesAsync();

                return forecast;
            }

            logger.LogError("Not able to update forecast");
            throw new DbUpdateException();
        }

        public async Task<WeatherForecast> DeleteWeatherForecast(Guid id)
        {
            if (context != null && context.WeatherForecasts != null && id != default)
            {
                WeatherForecast? forecast = await context.FindAsync<WeatherForecast>(id);

                if (forecast == null)
                {
                    logger.LogError("Couldn't find forecast");
                    throw new KeyNotFoundException();
                }

                context.Remove(forecast);

                await context.SaveChangesAsync();

                return forecast;
            }

            logger.LogError("Not able to delete forecast");
            throw new DbUpdateException();
        }

        public async Task<Operation> CreateOperation(Operation operation)
        {
            if (context != null && context.Operations != null)
            {
                await context.AddAsync(operation);

                if (await context.SaveChangesAsync() != 1)
                {
                    logger.LogError("Couldn't save operation");
                    throw new DbUpdateException();
                }

                return operation;
            }

            logger.LogError("Not able to create operation");
            throw new DbUpdateException();
        }
    }
}
