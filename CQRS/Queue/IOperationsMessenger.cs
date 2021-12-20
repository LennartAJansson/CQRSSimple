namespace CQRS.Queue
{
    using System.Threading.Tasks;

    using CQRS.Model;

    public delegate Task NewOperationArrivedDelegate(object sender, Operation operation);

    public interface IOperationsMessenger
    {
        event NewOperationArrivedDelegate NewOperationArrived;
        public Task SendOperation(object sender, Operation operation);
    }
}