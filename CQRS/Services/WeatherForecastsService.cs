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
            await context.SaveChangesAsync();

            return forecast;
        }

        public async Task<WeatherForecast> Read(int id)
        {
            WeatherForecast forecast = await context.FindAsync<WeatherForecast>(id);

            if (forecast == null)
            {
                throw new KeyNotFoundException();
            }

            return forecast;
        }

        public async Task<IEnumerable<WeatherForecast>> Read()
        {
            if (await context.WeatherForecasts.CountAsync() == 0)
            {
                throw new NoResultException();
            }

            return context.WeatherForecasts;
        }

        public async Task<WeatherForecast> Update(WeatherForecast forecast)
        {
            context.Update(forecast);

            await context.SaveChangesAsync();

            return forecast;
        }

        public async Task<WeatherForecast> Delete(int id)
        {
            WeatherForecast forecast = await context.FindAsync<WeatherForecast>(id);

            if (forecast == null)
            {
                throw new KeyNotFoundException();
            }

            context.Remove(forecast);
            await context.SaveChangesAsync();

            return forecast;
        }
    }
}
