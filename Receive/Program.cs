using Microsoft.Extensions.NATS;

using Receive;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<NatsConsumer>(options => context.Configuration.GetSection("NATS").Bind(options));
        NatsConsumer natsConsumer = context.Configuration.GetSection("NATS").Get<NatsConsumer>();
        services.AddNatsClient(options =>
        {
            options.Servers = natsConsumer.Servers;
            options.Url = natsConsumer.Url;
            options.Verbose = true;
        })
            .AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
