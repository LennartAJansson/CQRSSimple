namespace SplittedCommandExecutor
{
    using System.Text;

    using NATS.Client;
    using NATS.Client.JetStream;

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
        private readonly IConnection connection;
        private readonly IJetStream jetStream;
        private readonly IAsyncSubscription subscription;

        public Worker(ILogger<Worker> logger, IConnection connection)
        {
            this.logger = logger;
            this.connection = connection;
            jetStream = connection.CreateJetStreamContext();
            subscription = connection.SubscribeAsync("foo");
            subscription.MessageHandler += DataReceived;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //jetStream.PublishAsync
            //jetStream.PullSubscribe
            //jetStream.PushSubscribeAsync

            subscription.Start();

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                connection.Publish("foo", "bar", Encoding.UTF8.GetBytes("help!"));

                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            connection.Drain();
            connection.Close();

            return base.StopAsync(cancellationToken);
        }

        private void DataReceived(object? sender, MsgHandlerEventArgs e)
        {
            Console.WriteLine(e.Message);
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            throw new NotImplementedException();
        }
    }
}