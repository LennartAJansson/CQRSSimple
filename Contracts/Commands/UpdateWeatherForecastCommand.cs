namespace Contracts.Commands
{
    public record UpdateWeatherForecastCommand(DateTimeOffset Date, decimal Temperature, string Summary) : ICommand<WeatherForecastResponse>;
}