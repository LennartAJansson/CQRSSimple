
using Microsoft.Extensions.NATS;

using SplittedCommandExecutor;

using WeatherForecastsDb;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddNatsClient(options => { });
        services.AddWeatherForecastsDb(context.Configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

//Receives an Operation over the Event Source and executes that command against the db