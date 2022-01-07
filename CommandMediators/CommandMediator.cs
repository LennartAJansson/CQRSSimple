namespace CommandMediators
{
    using System.Reflection;

    using MediatR;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.NATS;

    public static class CommandMediator
    {
        public static IServiceCollection AddCommandMediators(this IServiceCollection services, IConfiguration configuration) =>
            services.AddNatsClient(options => { })
                .AddMediatR(Assembly.GetAssembly(typeof(CommandMediator)));
    }
}