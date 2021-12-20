namespace CQRS.Contracts
{
    using System;

    using MediatR;

    public interface ICommand<T> : IRequest<T> { }

    //ICommand
    public record CreateWeatherForecastRequest(DateTime Date, int Temperature, bool IsCelsius, string Summary) : ICommand<CreateWeatherForecastResponse>;
    public record UpdateWeatherForecastRequest(Guid WeatherForecastId, DateTime Date, int Temperature, bool IsCelsius, string Summary) : ICommand<UpdateWeatherForecastResponse>;
    public record DeleteWeatherForecastRequest(Guid WeatherForecastId) : ICommand<DeleteWeatherForecastResponse>;

    //TODO Fix response types
    //ICommand response
    public record CreateWeatherForecastResponse(Guid WeatherForecastId);
    public record UpdateWeatherForecastResponse(Guid WeatherForecastId);
    public record DeleteWeatherForecastResponse(Guid WeatherForecastId);
}
