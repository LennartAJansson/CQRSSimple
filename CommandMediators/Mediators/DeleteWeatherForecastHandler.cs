namespace CommandMediators.Mediators
{
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts;
    using Contracts.Commands;

    using MediatR;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.NATS;
    using Microsoft.Extensions.Options;

    using NATS.Client;
    using NATS.Client.JetStream;

    using NATSExamples;

    public class DeleteWeatherForecastHandler : IRequestHandler<DeleteWeatherForecastCommand, WeatherForecastCommandResponse>
    {
        private readonly ILogger<DeleteWeatherForecastHandler> logger;
        private readonly NatsProducer natsProducer;
        private readonly IConnection connection;

        public DeleteWeatherForecastHandler(ILogger<DeleteWeatherForecastHandler> logger, IOptions<NatsProducer> options, IConnection connection)
        {
            this.logger = logger;
            natsProducer = options.Value;
            this.connection = connection;
        }

        public async Task<WeatherForecastCommandResponse> Handle(DeleteWeatherForecastCommand request, CancellationToken cancellationToken)
        {
            JsUtils.CreateStreamOrUpdateSubjects(connection, natsProducer.Stream, natsProducer.Subject);
            IJetStream jetStream = connection.CreateJetStreamContext();

            CreateOperationCommand? operation = new(Guid.NewGuid(), DateTimeOffset.Now, request.GetType().Name,
                request.WeatherForecastId, JsonSerializer.Serialize(request));

            byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(operation));
            Msg msg = new Msg(natsProducer.Subject, null, null, data);
            PublishAck pa = await jetStream.PublishAsync(msg);

            logger.LogInformation("Published message {data} on subject {subject}, stream {stream}, seqno {seqno}.",
                                    Encoding.UTF8.GetString(data), natsProducer.Subject, pa.Stream, pa.Seq);

            return new WeatherForecastCommandResponse(operation.WeatherForecastId, request.Date, request.Temperature, request.Summary, request.IsCelsius);

        }
    }
}
