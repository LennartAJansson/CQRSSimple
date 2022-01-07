namespace Contracts.Commands
{
    //Only used for pushing requests on the async event stream. NATS contract
    public record CreateOperationCommand(Guid OperationId, DateTimeOffset Date, string Action, Guid WeatherForecastId, string RequestData);
}