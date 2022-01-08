namespace QueryMediators
{
    using System.Reflection;

    using Mappers;

    using MediatR;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class QueryMediator
    {
        public static IServiceCollection AddQueryMediators(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<ConnectionStrings>(options => configuration.GetSection("ConnectionStrings").Bind(options))
                .AddMediatR(Assembly.GetAssembly(typeof(QueryMediator)))
                .AddMappers();
    }
}
