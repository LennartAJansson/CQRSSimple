namespace Contracts.Commands
{
    public record DeleteWeatherForecastCommand(DateTimeOffset Date, decimal Temperature, string Summary) : ICommand<WeatherForecastResponse>;
}