namespace AllInOne.Contracts
{
    using System;

    using MediatR;

    public interface ICommand<T> : IRequest<T> { }

    //ICommand
    public record CreateWeatherForecastRequest(DateTime Date, int Temperature, string Summary, bool IsCelsius) : ICommand<WeatherForecastIdResponse>;
    public record UpdateWeatherForecastRequest(Guid WeatherForecastId, DateTime Date, int Temperature, string Summary, bool IsCelsius) : ICommand<WeatherForecastIdResponse>;
    public record DeleteWeatherForecastRequest(Guid WeatherForecastId) : ICommand<WeatherForecastIdResponse>;

    //TODO Fix response types
    //ICommand response
    public record WeatherForecastIdResponse(Guid WeatherForecastId);
}
