namespace WeatherForecastsDb
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class WeatherForecastsDb
    {
        public static IServiceCollection AddWeatherForecastsDb(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<WeatherForecastsContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("WeatherForecastsDb")),
                    ServiceLifetime.Transient, ServiceLifetime.Transient);
    }
}