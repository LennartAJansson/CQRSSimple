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
        DateTime StopDate = new DateTime(2021, 12, 31, 23, 1, 0);
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        while (StartDate < StopDate)
        {
            decimal temp = GetTemp(StartDate);
            CreateWeatherForecastCommand request = new CreateWeatherForecastCommand(StartDate, temp, GetSummaries(temp), true);
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
        { "Freezing", "Cold", "Frosty", "Chilly", "Cool", "Mild", "Warm", "Hot", "Sweltering" };

    private static string GetSummaries(decimal temp) =>
        temp switch
        {
            < -10 => Summaries[0],//Freezing
            < 0 => Summaries[1],//Cold
            < 10 => Summaries[2],//Frosty
            < 15 => Summaries[3],//Chilly
            < 20 => Summaries[4],//Cool
            < 25 => Summaries[5],//Mild
            < 30 => Summaries[6],//Warm
            < 35 => Summaries[7],//Hot
            _ => Summaries[8]//Sweltering
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

    private static decimal GetTemp(DateTime date) =>
        GetTimeBand(date) switch
        {
            TimeBand.Day => date.Month switch
            {
                < 2 or > 11 => new Random().NextDecimal(-10, 0),
                < 3 or > 10 => new Random().NextDecimal(-5, 5),
                < 5 or > 8 => new Random().NextDecimal(5, 15),
                _ => new Random().NextDecimal(15, 35)
            },
            TimeBand.Hase => date.Month switch
            {
                < 2 or > 11 => new Random().NextDecimal(-15, -5),
                < 3 or > 10 => new Random().NextDecimal(-7, 3),
                < 5 or > 8 => new Random().NextDecimal(-3, 13),
                _ => new Random().NextDecimal(13, 33)
            },
            _ => date.Month switch
            {
                < 2 or > 11 => new Random().NextDecimal(-20, -10),
                < 3 or > 10 => new Random().NextDecimal(-10, 0),
                < 5 or > 8 => new Random().NextDecimal(-5, 5),
                _ => new Random().NextDecimal(5, 20)
            }
        };
}
public static class ExtensionMethods
{
    public static decimal NextDecimal(this Random rng)
    {
        double RandH, RandL;
        do
        {
            RandH = rng.NextDouble();
            RandL = rng.NextDouble();
        } while ((RandH > 0.99999999999999d) || (RandL > 0.99999999999999d));
        return (decimal)RandH + (decimal)RandL / 1E14m;
    }
    public static decimal NextDecimal(this Random rng, decimal minValue, decimal maxValue) => rng.NextDecimal() * (maxValue - minValue) + minValue;
}
