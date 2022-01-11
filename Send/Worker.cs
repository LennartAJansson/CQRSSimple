namespace Send
{
    using System.Text;

    using Microsoft.Extensions.Options;

    using NATS.Client;
    using NATS.Client.JetStream;
    using NATS.Extensions.DependencyInjection;

    using NATSExamples;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly NatsProducer natsProducer;
        private readonly IConnection connection;
        private IJetStream? jetStream = null;

        public Worker(ILogger<Worker> logger, IOptions<NatsProducer> options, IConnection connection)
        {
            this.logger = logger;
            this.connection = connection;
            natsProducer = options.Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            JsUtils.CreateStreamOrUpdateSubjects(connection, natsProducer.Stream, natsProducer.Subject);

            jetStream = connection.CreateJetStreamContext();

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int x = 1;

            while (!stoppingToken.IsCancellationRequested)
            {
                byte[] data = Encoding.UTF8.GetBytes($"Message number - {x++}");
                Msg msg = new Msg(natsProducer.Subject, null, null, data);

                if (jetStream != null)
                {
                    PublishAck pa = await jetStream.PublishAsync(msg);

                    logger.LogInformation("Published message {data} on subject {subject}, stream {stream}, seqno {seqno}.",
                                            Encoding.UTF8.GetString(data), natsProducer.Subject, pa.Stream, pa.Seq);
                }
                else
                {
                    logger.LogInformation("JetStream not available");
                }

                await Task.Delay(5000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken) => base.StopAsync(cancellationToken);
    }

}