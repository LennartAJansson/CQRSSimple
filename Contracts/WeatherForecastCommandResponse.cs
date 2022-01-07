namespace Contracts
{
    public record WeatherForecastCommandResponse(Guid WeatherForecastId, DateTimeOffset Date, decimal Temperature, string Summary, bool IsCelsius);
}