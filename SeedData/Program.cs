using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient<IWorker, Worker>(ConfigureHttpClientOptions);
    })
    .Build();

await host.StartAsync();


await host.StopAsync();

void ConfigureHttpClientOptions(HttpClient client) => client.BaseAddress = new Uri("https://localhost:5001");

internal interface IWorker
{
    Task Seed();
}

internal class Worker : IWorker
{
    private readonly HttpClient client;

    public Worker(HttpClient client) => this.client = client;

    public async Task Seed()
    {
        string str = "";
        HttpResponseMessage? response = await client.PostAsync("api/command/createweatherforecast", new StringContent(str));
        response.EnsureSuccessStatusCode();
    }
}