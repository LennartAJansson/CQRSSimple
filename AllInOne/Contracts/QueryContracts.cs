namespace AllInOne.Contracts
{
    using System;
    using System.Collections.Generic;

    using MediatR;

    public interface IQuery<T> : IRequest<T> { }

    //IQuery
    public record ReadWeatherForecastRequest(Guid WeatherForecastId) : IQuery<ReadWeatherForecastResponse>;
    public record ReadWeatherForecastsRequest() : IQuery<IEnumerable<ReadWeatherForecastResponse>>;
    public record ReadOperationsRequest() : IQuery<IEnumerable<ReadOperationResponse>>;

    //IQuery responses
    public record ReadWeatherForecastResponse(Guid WeatherForecastId, DateTime Date, int TemperatureC, int TemperatureF, string Summary);
    public record ReadOperationResponse(Guid OperationId, bool Ready, DateTime Date, string Action, Guid WeatherForecastId, string RequestData, string Before, string After);
}
