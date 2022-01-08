namespace CommandMediators
{
    using System.Reflection;

    using Mappers;

    using MediatR;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.NATS;

    public static class CommandMediator
    {
        public static IServiceCollection AddCommandMediators(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NatsProducer>(options => configuration.GetSection("NATS").Bind(options));
            NatsProducer natsProducer = configuration.GetSection("NATS").Get<NatsProducer>();
            services.AddNatsClient(options =>
            {
                options.Servers = natsProducer.Servers;
                options.Url = natsProducer.Url;
                options.Verbose = true;
            });
            services.AddMediatR(Assembly.GetAssembly(typeof(CommandMediator)));
            services.AddMappers();
            return services;
        }
    }
}