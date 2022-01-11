namespace SeedData;
public record CreateWeatherForecastResponse(Guid WeatherForecastId, DateTimeOffset Date, decimal Temperature, string Summary, bool IsCelsius);