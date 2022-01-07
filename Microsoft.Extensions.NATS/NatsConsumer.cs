namespace Microsoft.Extensions.NATS
{
    public class NatsConsumer
    {
        public string[]? Servers { get; set; }
        public string? Url { get; set; }
        public string? Stream { get; set; }
        public string? Subject { get; set; }
        public string? Consumer { get; set; }
        public string? DeliverySubject { get; set; }
    }
}