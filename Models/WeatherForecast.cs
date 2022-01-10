namespace Models
{
    public class WeatherForecast
    {
        public Guid WeatherForecastId { get; set; }

        public DateTimeOffset Date { get; set; }

        public decimal Celsius { get; set; }

        public decimal Fahrenheit => 32 + Celsius / 0.5556m;//Calculated fields will not be included in table

        public string? Summary { get; set; }

    }
}