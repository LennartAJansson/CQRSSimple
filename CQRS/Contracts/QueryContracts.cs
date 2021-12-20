namespace CQRS.Contracts
{
    using System;
    using System.Collections.Generic;

    using MediatR;

    public interface IQuery<T> : IRequest<T> { }

    //IQuery
    public record ReadWeatherForecastRequest(Guid WeatherForecastId) : IQuery<ReadWeatherForecastResponse>;
    public record ReadWeatherForecastsRequest() : IQuery<IEnumerable<ReadWeatherForecastResponse>>;

    //IQuery responses
    public record ReadWeatherForecastResponse(Guid WeatherForecastId, DateTime Date, int TemperatureC, int TemperatureF, string Summary);
}
