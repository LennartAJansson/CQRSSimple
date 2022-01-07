namespace Contracts
{
    public record WeatherForecastQueryResponse(Guid WeatherForecastId, DateTimeOffset Date, decimal Celsius, decimal Fahrenheit, string Summary);
}