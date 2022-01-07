namespace Contracts
{
    public record WeatherForecastResponse(Guid WeatherForecastId, DateTimeOffset Date, decimal Celsius, decimal Fahrenheit, string Summary);
}