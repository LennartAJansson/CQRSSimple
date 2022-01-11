namespace NATS.Extensions.DependencyInjection
{
    using System;
    using System.Collections.Generic;

    using global::NATS.Client;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNatsClient(this IServiceCollection services, Action<Options>? configureOptions = null, ServiceLifetime connectionServiceLifeTime = ServiceLifetime.Transient)
        {
            Options? defaultOptions = ConnectionFactory.GetDefaultOptions();
            configureOptions?.Invoke(defaultOptions);
            services.AddSingleton(defaultOptions);

            services.AddSingleton<ConnectionFactory>();
            services.AddSingleton<INatsClientConnectionFactory, NatsClientConnectionFactoryDecorator>();

            services.TryAdd(new ServiceDescriptor(typeof(IConnection), sp =>
            {
                Options? options = sp.GetRequiredService<Options>();
                INatsClientConnectionFactory? connectionFactory = sp.GetRequiredService<INatsClientConnectionFactory>();
                return connectionFactory.CreateConnection(options);
            }, connectionServiceLifeTime));

            services.TryAdd(new ServiceDescriptor(typeof(IEncodedConnection), sp =>
            {
                Options? options = sp.GetRequiredService<Options>();
                INatsClientConnectionFactory? connectionFactory = sp.GetRequiredService<INatsClientConnectionFactory>();
                return connectionFactory.CreateEncodedConnection(options);
            }, connectionServiceLifeTime));


            return services;
        }
    }
}