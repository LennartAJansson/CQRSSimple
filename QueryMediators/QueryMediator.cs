namespace QueryMediators
{
    using System.Reflection;

    using MediatR;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class QueryMediator
    {
        public static IServiceCollection AddQueryMediators(this IServiceCollection services, IConfiguration configuration) =>
            services.AddMediatR(Assembly.GetAssembly(typeof(QueryMediator)));
    }
}
