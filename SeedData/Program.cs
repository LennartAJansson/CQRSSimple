using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) => services
        .AddHttpClient<IWorker, Worker>(client => client
            .BaseAddress = new Uri("https://localhost:5001")))
    .Build();

await host.StartAsync();

await host.Services.GetRequiredService<IWorker>().Seed();

await host.StopAsync();
