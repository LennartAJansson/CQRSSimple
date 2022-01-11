namespace NATS.Extensions.DependencyInjection
{
    using global::NATS.Client;

    internal class NatsClientConnectionFactoryDecorator : INatsClientConnectionFactory
    {
        private readonly ConnectionFactory connectionFactory;

        public NatsClientConnectionFactoryDecorator(ConnectionFactory connectionFactory) =>
            this.connectionFactory = connectionFactory;

        public IConnection CreateConnection(Action<Options>? configureOptions = null)
        {
            Options? options = ConnectionFactory.GetDefaultOptions();
            configureOptions?.Invoke(options);
            return CreateConnection(options);
        }

        public IConnection CreateConnection(Options options) =>
            connectionFactory.CreateConnection(options);

        public IEncodedConnection CreateEncodedConnection(Action<Options>? configureOptions = null)
        {
            Options? options = ConnectionFactory.GetDefaultOptions();
            configureOptions?.Invoke(options);
            return CreateEncodedConnection(options);
        }

        public IEncodedConnection CreateEncodedConnection(Options options) =>
            connectionFactory.CreateEncodedConnection(options);
    }
}