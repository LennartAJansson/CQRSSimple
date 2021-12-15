namespace CQRS.Contracts
{
    using System;
    using System.Collections.Generic;

    using MediatR;

    public interface IQuery<T> : IRequest<T> { }

    //IQuery
    public record ReadWeatherForecastRequest(int Id) : IQuery<ReadWeatherForecastResponse>;
    public record ReadWeatherForecastsRequest() : IQuery<ReadWeatherForecastsResponse>;

    //IQuery responses
    public record ReadWeatherForecastResponse(int Id, DateTime Date, int Temperature, string Summary);
    public record ReadWeatherForecastsResponse(IEnumerable<ReadWeatherForecastResponse> Responses);
}
