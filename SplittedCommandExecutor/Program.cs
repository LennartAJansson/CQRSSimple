
using Mappers;

using Microsoft.EntityFrameworkCore;

using NATS.Client;
using NATS.Extensions.DependencyInjection;

using SplittedCommandExecutor;

using WeatherForecastsDb;

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
        });
        services.AddTransient<IConsumer<MsgHandlerEventArgs>, Consumer>();
        services.AddMappers();
        services.AddWeatherForecastsDb(context.Configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

UpdateDatabase(host.Services);

await host.RunAsync();

//Receives an Operation over the Event Source and executes that command against the db

static void UpdateDatabase(IServiceProvider provider)
{
    using (IServiceScope scope = provider.CreateScope())
    {
        WeatherForecastsContext context = scope.ServiceProvider.GetRequiredService<WeatherForecastsContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}