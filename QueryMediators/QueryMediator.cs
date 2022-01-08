namespace QueryMediators
{
    using System.Reflection;

    using Mappers;

    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    public static class QueryMediator
    {
        public static IServiceCollection AddQueryMediators(this IServiceCollection services) =>
            services.AddMediatR(Assembly.GetAssembly(typeof(QueryMediator))).AddMappers();
    }
}
