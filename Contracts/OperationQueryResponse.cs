namespace Contracts
{
    public record OperationQueryResponse(Guid OperationId, bool Ready, DateTimeOffset Date, string Action, Guid WeatherForecastId, string RequestData, string Before, string After);
}