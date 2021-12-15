namespace CQRS.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CQRS.Data;
    using CQRS.Exceptions;
    using CQRS.Model;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public interface IWeatherForecastsService
    {
        Task<WeatherForecast> Create(WeatherForecast forecast);
        Task<WeatherForecast> Read(int id);
        Task<IEnumerable<WeatherForecast>> Read();
        Task<WeatherForecast> Update(WeatherForecast forecast);
        Task<WeatherForecast> Delete(int id);
    }

    public class WeatherForecastsService : IWeatherForecastsService
    {
        private readonly ILogger<WeatherForecastsService> logger;
        private readonly WeatherForecastsContext context;

        public WeatherForecastsService(ILogger<WeatherForecastsService> logger, WeatherForecastsContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<WeatherForecast> Create(WeatherForecast forecast)
        {
            await context.AddAsync(forecast);

            if (await context.SaveChangesAsync() != 1)
            {
                logger.LogError("Couldn't save");
                throw new NoResultException();
            }

            return forecast;
        }

        public async Task<WeatherForecast> Read(int id)
        {
            WeatherForecast forecast = await context.FindAsync<WeatherForecast>(id);

            if (forecast == null)
            {
                logger.LogError("Couldn't find");
                throw new KeyNotFoundException();
            }

            return forecast;
        }

        public async Task<IEnumerable<WeatherForecast>> Read()
        {
            if (!await context.WeatherForecasts.AnyAsync())
            {
                logger.LogError("No records");
                throw new NoResultException();
            }

            return context.WeatherForecasts;
        }

        public async Task<WeatherForecast> Update(WeatherForecast forecast)
        {
            if (forecast.Id == default)
            {
                logger.LogError("Not an existing record");
                throw new DbUpdateException();
            }

            context.Update(forecast);

            await context.SaveChangesAsync();

            return forecast;
        }

        public async Task<WeatherForecast> Delete(int id)
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
    }
}
