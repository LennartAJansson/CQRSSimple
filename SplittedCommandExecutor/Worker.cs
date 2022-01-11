namespace SplittedCommandExecutor
{
    using Microsoft.Extensions.Options;

    using NATS.Client;
    using NATS.Client.JetStream;
    using NATS.Extensions.DependencyInjection;

    using NATSExamples;

    public class Worker : BackgroundService
    {
        //https://github.com/nats-io/nats.net
        //https://github.com/nats-io/nats.net#the-jetstream-context
        //TODO Check serialization of Msg

        //The * wildcard matches any token, at any level of the subject
        //foo.*.bar equals foo.abc.bar, foo.123.bar but NOT only foo.bar
        //The > wildcard matches any length of the tail of a subject, and can only be the last token
        //foo.> equals foo.abc, foo.bar etc but NOT only foo

        private readonly ILogger<Worker> logger;
        private readonly NatsConsumer natsConsumer;
        private readonly IConnection connection;
        private readonly IServiceScopeFactory scopeFactory;
        private IJetStream? jetStream = null;
        private IJetStreamPushAsyncSubscription? subscription = null;

        public Worker(ILogger<Worker> logger, IOptions<NatsConsumer> options, IConnection connection, IServiceScopeFactory scopeFactory)
        {
            this.logger = logger;
            this.connection = connection;
            this.scopeFactory = scopeFactory;
            natsConsumer = options.Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            if (!JsUtils.StreamExists(connection, natsConsumer.Stream))
            {
                JsUtils.CreateStreamWhenDoesNotExist(connection, natsConsumer.Stream, natsConsumer.Subject);
            }

            ConsumerConfiguration cc = ConsumerConfiguration.Builder()
                                    .WithDurable(natsConsumer.Consumer)
                                    .WithDeliverSubject(natsConsumer.DeliverySubject)
                                    .Build();

            connection.CreateJetStreamManagementContext()
                .AddOrUpdateConsumer(natsConsumer.Stream, cc);

            jetStream = connection.CreateJetStreamContext();

            PushSubscribeOptions so = PushSubscribeOptions.BindTo(natsConsumer.Stream, natsConsumer.Consumer);
            subscription = jetStream.PushSubscribeAsync(natsConsumer.Subject, MessageArrived, true, so);

            return base.StartAsync(cancellationToken);
        }


        private void MessageArrived(object? sender, MsgHandlerEventArgs args) =>
            Task.Factory.StartNew(async () =>
            {
                using (IServiceScope? scope = scopeFactory.CreateScope())
                {
                    await scope.ServiceProvider.GetRequiredService<IConsumer<MsgHandlerEventArgs>>().Consume(args);
                }
            });

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(10000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            if (subscription != null)
            {
                //subscription.Drain();
                //subscription.Unsubscribe();
                //subscription.Dispose();
            }

            return base.StopAsync(cancellationToken);
        }
    }
}