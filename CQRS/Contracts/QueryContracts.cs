namespace CQRS.Contracts
{
    using System;
    using System.Collections.Generic;

    using MediatR;

    public interface IQuery<T> : IRequest<T> { }

    //IQuery
    public record ReadWeatherForecastRequest(Guid Id) : IQuery<ReadWeatherForecastResponse>;
    public record ReadWeatherForecastsRequest() : IQuery<IEnumerable<ReadWeatherForecastResponse>>;

    //IQuery responses
    public record ReadWeatherForecastResponse(Guid Id, DateTime Date, int Celsius, int Fahrenheit, string Summary);
}
