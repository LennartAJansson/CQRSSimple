namespace AllInOne.Messengers
{
    using System.Threading.Tasks;

    using AllInOne.Model;

    public delegate Task NewOperationReceivedDelegate(Operation operation);

    public interface IOperationsMessenger
    {
        event NewOperationReceivedDelegate? NewOperationReceived;
        public Task QueueOperation(Operation operation);
    }
}