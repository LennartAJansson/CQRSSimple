namespace CQRS.Queue
{
    using System.Threading.Tasks;

    using CQRS.Model;


    public class OperationsMessenger : IOperationsMessenger
    {
        public event NewOperationArrivedDelegate NewOperationArrived;

        public Task SendOperation(object sender, Operation operation) => NewOperationArrived?.Invoke(sender, operation);
    }
}
