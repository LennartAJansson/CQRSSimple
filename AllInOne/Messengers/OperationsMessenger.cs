namespace AllInOne.Messengers
{
    using System.Threading.Tasks;

    using AllInOne.Model;


    public class OperationsMessenger : IOperationsMessenger
    {
        public event NewOperationReceivedDelegate? NewOperationReceived;

        public async Task QueueOperation(Operation operation)
        {
            if (NewOperationReceived != null)
            {
                await NewOperationReceived.Invoke(operation);
            }
        }
    }
}
