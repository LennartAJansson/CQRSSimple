namespace CommandMediators
{
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts;
    using Contracts.Commands;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Models;

    using NATS.Client;
    using NATS.Client.JetStream;

    public class UpdateWeatherForecastHandler : IRequestHandler<UpdateWeatherForecastCommand, WeatherForecastResponse>
    {
        private readonly ILogger<UpdateWeatherForecastHandler> logger;
        private readonly IConnection connection;
        private readonly IJetStream jetStream;

        public UpdateWeatherForecastHandler(ILogger<UpdateWeatherForecastHandler> logger, IConnection connection)
        {
            this.logger = logger;
            this.connection = connection;
            jetStream = connection.CreateJetStreamContext();
        }

        public Task<WeatherForecastResponse> Handle(UpdateWeatherForecastCommand request, CancellationToken cancellationToken)
        {
            //Data should be an Operation
            Operation operation = new Operation();
            string json = JsonSerializer.Serialize(operation);
            Msg msg = new Msg
            {
                Data = Encoding.UTF8.GetBytes(json),
                Subject = "Subject to send to",
                Header = new MsgHeader(),
                Reply = "Reply subject (if any)"
            };

            connection.Publish(msg);

            //Check the msg afterwards
            //msg.IsJetStream
            //msg.LastAck
            //msg.MetaData
            //msg.Status

            throw new NotImplementedException();
        }
    }
}
