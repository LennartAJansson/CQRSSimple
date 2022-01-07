namespace Contracts.Querys
{
    public record ReadWeatherForecastByDateQuery(DateTimeOffset Date) : IQuery<WeatherForecastResponse>;
}