namespace Models
{
    public class Operation
    {
        public Guid OperationId { get; set; }
        public bool Ready { get; set; }
        public DateTimeOffset Date { get; set; }
        public string? Action { get; set; }
        public Guid WeatherForecastId { get; set; }
        public string? RequestData { get; set; }
        public string? Before { get; set; }
        public string? After { get; set; }
    }
}