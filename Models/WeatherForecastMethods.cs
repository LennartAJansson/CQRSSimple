namespace Models
{
    public partial class WeatherForecast
    {
        public void CopyFrom(WeatherForecast source)
        {
            Celsius = source.Celsius;
            Summary = source.Summary;
        }
    }
}