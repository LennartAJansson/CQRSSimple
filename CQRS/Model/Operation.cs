namespace CQRS.Model
{
    using System;

    public class Operation
    {
        public Guid OperationId { get; set; }
        public DateTime Date { get; set; }
        public string Action { get; set; }
        public Guid WeatherForecastId { get; set; }
        public virtual WeatherForecast WeatherForecast { get; set; }
        public string Data { get; set; }
        public string After { get; set; }
    }
}
