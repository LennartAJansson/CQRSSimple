namespace Models
{
    public class WeatherForecast
    {
        public Guid WeatherForecastId { get; set; }

        public DateTimeOffset Date { get; set; }

        public int Celsius { get; set; }

        public int Fahrenheit => 32 + (int)(Celsius / 0.5556);//Calculated fields will not be included in table

        public string? Summary { get; set; }

    }
}