namespace CQRS.Contracts
{
    using System;

    using MediatR;

    public interface ICommand<T> : IRequest<T> { }

    //ICommand
    public record CreateWeatherForecastRequest(DateTime Date, int Temperature, bool IsCelsius, string Summary) : ICommand<CreateWeatherForecastResponse>;
    public record UpdateWeatherForecastRequest(int Id, DateTime Date, int Temperature, string Summary) : ICommand<UpdateWeatherForecastResponse>;
    public record DeleteWeatherForecastRequest(int Id) : ICommand<DeleteWeatherForecastResponse>;

    //ICommand response
    public record CreateWeatherForecastResponse(int Id, DateTime Date, int Temperature, string Summary);
    public record UpdateWeatherForecastResponse(int Id, DateTime Date, int Temperature, string Summary);
    public record DeleteWeatherForecastResponse(int Id, DateTime Date, int Temperature, string Summary);
}
