namespace CQRS.Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(settings => configuration.GetSection("ConnectionStrings").Bind(settings));

            return services;
        }
    }
}
