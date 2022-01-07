namespace SeedData;

using System.Text;
using System.Text.Json;

internal class Worker : IWorker
{
    private readonly HttpClient client;

    public Worker(HttpClient client) => this.client = client;

    public async Task Seed()
    {
        List<Guid> ids = new List<Guid>();

        DateTime StartDate = new DateTime(2021, 1, 1, 0, 0, 0);
        DateTime StopDate = new DateTime(2021, 1, 31, 23, 1, 0);
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        while (StartDate < StopDate)
        {
            int temp = GetTemp(StartDate);
            CreateWeatherForecastRequest request = new CreateWeatherForecastRequest(StartDate, temp, true, GetSummaries(temp));
            Console.WriteLine(request);

            HttpResponseMessage? response = await client.PostAsync("/api/command/createweatherforecast",
                new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            string responseMessage = await response.Content.ReadAsStringAsync();
            CreateWeatherForecastResponse? responseObject = JsonSerializer.Deserialize<CreateWeatherForecastResponse>(responseMessage, options);
            if (responseObject != null)
            {
                Console.WriteLine(responseObject.WeatherForecastId);
                ids.Add(responseObject.WeatherForecastId);
            }

            StartDate = StartDate.AddHours(1);
        }
    }

    private static readonly string[] Summaries =
        { "Freezing", "Cold", "Mild", "Warm", "Hot" };

    private static string GetSummaries(int temp) =>
        temp switch
        {
            < 0 => Summaries[0],
            < 5 => Summaries[1],
            < 15 => Summaries[2],
            < 25 => Summaries[3],
            _ => Summaries[4]
        };
    private enum TimeBand
    {
        Night,
        Hase,
        Day
    }

    private static TimeBand GetTimeBand(DateTime timeOfDay) =>

        timeOfDay.Hour switch
        {
            > 20 => TimeBand.Night,
            > 18 => TimeBand.Hase,
            < 6 => TimeBand.Night,
            < 8 => TimeBand.Hase,
            _ => TimeBand.Day
        };

    private static int GetTemp(DateTime date) =>
        GetTimeBand(date) switch
        {
            TimeBand.Day => date.Month switch
            {
                < 3 or > 10 => new Random().Next(-5, 5),
                < 5 or > 8 => new Random().Next(5, 15),
                _ => new Random().Next(15, 35)
            },
            TimeBand.Hase => date.Month switch
            {
                < 3 or > 10 => new Random().Next(-7, 3),
                < 5 or > 8 => new Random().Next(-3, 13),
                _ => new Random().Next(13, 33)
            },
            _ => date.Month switch
            {
                < 3 or > 10 => new Random().Next(-10, 0),
                < 5 or > 8 => new Random().Next(-5, 5),
                _ => new Random().Next(5, 20)
            }
        };
}
