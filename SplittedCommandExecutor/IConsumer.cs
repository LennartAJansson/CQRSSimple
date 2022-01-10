namespace SplittedCommandExecutor
{
    using System.Threading.Tasks;

    public interface IConsumer<in TMessage> : IConsumer
        where TMessage : class
    {
        Task Consume(TMessage args);
    }

    public interface IConsumer
    {
    }
}
