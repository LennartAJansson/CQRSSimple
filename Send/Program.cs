using Microsoft.Extensions.NATS;

using Send;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<NatsProducer>(options => context.Configuration.GetSection("NATS").Bind(options));
        NatsProducer natsProducer = context.Configuration.GetSection("NATS").Get<NatsProducer>();
        services.AddNatsClient(options =>
        {
            options.Servers = natsProducer.Servers;
            options.Url = natsProducer.Url;
            options.Verbose = true;
        })
            .AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
