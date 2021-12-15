namespace CQRS.Contracts
{
    using System;
    using System.Collections.Generic;

    using MediatR;

    //These are the public contracts
    //They could be kept in a separate shared Assembly
    public record CreateWeatherForecastRequest(DateTime Date, int Temperature, bool IsCelsius, string Summary) : IRequest<CreateWeatherForecastResponse>;
    public record CreateWeatherForecastResponse(int Id, DateTime Date, int Temperature, string Summary);

    public record ReadWeatherForecastRequest(int Id) : IRequest<ReadWeatherForecastResponse>;
    public record ReadWeatherForecastResponse(int Id, DateTime Date, int Temperature, string Summary);

    public record ReadWeatherForecastsRequest() : IRequest<ReadWeatherForecastsResponse>;
    public record ReadWeatherForecastsResponse(IEnumerable<ReadWeatherForecastResponse> Responses);

    public record UpdateWeatherForecastRequest(int Id, DateTime Date, int Temperature, string Summary) : IRequest<UpdateWeatherForecastResponse>;
    public record UpdateWeatherForecastResponse(int Id, DateTime Date, int Temperature, string Summary);

    public record DeleteWeatherForecastRequest(int Id) : IRequest<DeleteWeatherForecastResponse>;
    public record DeleteWeatherForecastResponse(int Id, DateTime Date, int Temperature, string Summary);
}
