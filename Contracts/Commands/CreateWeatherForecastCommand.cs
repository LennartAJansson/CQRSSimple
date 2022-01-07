namespace Contracts.Commands
{
    public record CreateWeatherForecastCommand(DateTimeOffset Date, decimal Temperature, string Summary, bool IsCelsius) : ICommand<WeatherForecastCommandResponse>;
}