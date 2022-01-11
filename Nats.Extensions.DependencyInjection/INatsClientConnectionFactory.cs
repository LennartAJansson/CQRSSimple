namespace NATS.Extensions.DependencyInjection
{
    using global::NATS.Client;

    public interface INatsClientConnectionFactory
    {
        IConnection CreateConnection(Action<Options>? configureOptions = null);

        IConnection CreateConnection(Options options);

        IEncodedConnection CreateEncodedConnection(Action<Options>? configureOptions = null);

        IEncodedConnection CreateEncodedConnection(Options options);
    }
}