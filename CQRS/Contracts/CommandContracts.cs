namespace CQRS.Contracts
{
    using System;

    using MediatR;

    public interface ICommand<T> : IRequest<T> { }

    //ICommand
    public record CreateWeatherForecastRequest(DateTime Date, int Temperature, bool IsCelsius, string Summary) : ICommand<CreateWeatherForecastResponse>;
    public record UpdateWeatherForecastRequest(Guid Id, DateTime Date, int Temperature, string Summary) : ICommand<UpdateWeatherForecastResponse>;
    public record DeleteWeatherForecastRequest(Guid Id) : ICommand<DeleteWeatherForecastResponse>;

    //ICommand response
    public record CreateWeatherForecastResponse(Guid Id, DateTime Date, int Temperature, string Summary);
    public record UpdateWeatherForecastResponse(Guid Id, DateTime Date, int Temperature, string Summary);
    public record DeleteWeatherForecastResponse(Guid Id, DateTime Date, int Temperature, string Summary);
}
