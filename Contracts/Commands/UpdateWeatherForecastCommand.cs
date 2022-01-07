namespace Contracts.Commands
{
    public record UpdateWeatherForecastCommand(Guid WeatherForecastId, DateTimeOffset Date, decimal Temperature, string Summary, bool IsCelsius) : ICommand<WeatherForecastCommandResponse>;
}