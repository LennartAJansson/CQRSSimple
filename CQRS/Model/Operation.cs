namespace CQRS.Model
{
    using System;

    public class Operation
    {
        public Guid OperationId { get; set; }
        public bool Ready { get; set; }
        public DateTime Date { get; set; }
        public string Action { get; set; }
        public Guid WeatherForecastId { get; set; }
        public string RequestData { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
    }
}
