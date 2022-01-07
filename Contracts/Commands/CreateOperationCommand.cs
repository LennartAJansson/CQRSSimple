namespace Contracts.Commands
{
    //TODO Complete the properties for CreateOperationCommand
    public record CreateOperationCommand(Guid OperationId, bool Ready, DateTimeOffset Date, string Action, Guid WeatherForecastId, string RequestData, string Before, string After) : ICommand<OperationResponse>;
}