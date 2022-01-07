namespace Receive
{
    using System.Text;

    using Microsoft.Extensions.Options;

    using NATS.Client;
    using NATS.Client.JetStream;

    using NATSExamples;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly NatsConsumer natsConsumer;
        private readonly IConnection connection;
        private IJetStream? jetStream = null;
        private IJetStreamPushAsyncSubscription? subscription = null;

        public Worker(ILogger<Worker> logger, IOptions<NatsConsumer> options, IConnection connection)
        {
            this.logger = logger;
            this.connection = connection;
            natsConsumer = options.Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            JsUtils.CreateStreamWhenDoesNotExist(connection, natsConsumer.Stream, natsConsumer.Subject);

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

        private void MessageArrived(object? sender, MsgHandlerEventArgs args)
        {
            Msg msg = args.Message;
            msg.Ack();

            logger.LogInformation("Received message {data} on subject {subject}, stream {stream}, seqno {seqno}.",
                            Encoding.UTF8.GetString(msg.Data), natsConsumer.Subject, msg.MetaData.Stream, msg.MetaData.StreamSequence);
        }

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