namespace AllInOne.Messengers
{
    using System.Threading.Tasks;

    using AllInOne.Model;

    public delegate Task NewOperationReceivedDelegate(object sender, Operation operation);

    public interface IOperationsMessenger
    {
        event NewOperationReceivedDelegate? NewOperationReceived;
        public Task? QueueOperation(object sender, Operation operation);
    }
}