namespace Contracts
{
    public record OperationResponse(Guid OperationId, bool Ready, DateTimeOffset Date, string Action, Guid WeatherForecastId, string RequestData, string Before, string After);
}