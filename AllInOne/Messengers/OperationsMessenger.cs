namespace AllInOne.Messengers
{
    using System.Threading.Tasks;

    using AllInOne.Model;


    public class OperationsMessenger : IOperationsMessenger
    {
        public event NewOperationReceivedDelegate? NewOperationReceived;

        public Task? QueueOperation(object sender, Operation operation) => NewOperationReceived?.Invoke(sender, operation);
    }
}
