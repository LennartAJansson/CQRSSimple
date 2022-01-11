namespace SeedData;
public record CreateWeatherForecastCommand(DateTimeOffset Date, decimal Temperature, string Summary, bool IsCelsius);
