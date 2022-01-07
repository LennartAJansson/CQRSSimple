﻿namespace Contracts.Commands
{
    public record DeleteWeatherForecastCommand(Guid WeatherForecastId, DateTimeOffset Date, decimal Temperature, string Summary, bool IsCelsius) : ICommand<WeatherForecastCommandResponse>;
}