namespace SeedData;
public record CreateWeatherForecastRequest(DateTime Date, int Temperature, bool IsCelsius, string Summary);
