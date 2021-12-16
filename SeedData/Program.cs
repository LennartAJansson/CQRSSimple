using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient<IWorker, Worker>(ConfigureHttpClientOptions);
    })
    .Build();

await host.StartAsync();

await host.Services.GetRequiredService<IWorker>().Seed();

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
        string str = "{\"Date\":\"2021-12-16T14:12:00\",\"Temperature\":2,\"IsCelsius\":true,\"Summary\":\"Clear\"}";
        HttpResponseMessage? response = await client.PostAsync("/api/command/createweatherforecast", new StringContent(str, Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }
}