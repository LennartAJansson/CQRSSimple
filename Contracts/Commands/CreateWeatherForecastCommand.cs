namespace Contracts.Commands
{
    public record CreateWeatherForecastCommand(DateTimeOffset Date, decimal Temperature, string Summary) : ICommand<WeatherForecastResponse>;
}