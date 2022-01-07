namespace WeatherForecastsDb
{

    using Microsoft.EntityFrameworkCore;

    using Models;

    public class WeatherForecastsContext : DbContext
    {
        public DbSet<WeatherForecast>? WeatherForecasts { get; set; }
        public DbSet<Operation>? Operations { get; set; }

        public WeatherForecastsContext(DbContextOptions<WeatherForecastsContext> options)
            : base(options)
        { }
    }
}