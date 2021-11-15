namespace CQRS.Data
{
    using CQRS.Model;

    using Microsoft.EntityFrameworkCore;

    public class WeatherForecastsContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public WeatherForecastsContext(DbContextOptions<WeatherForecastsContext> options)
            : base(options)
        { }
    }
}
