namespace CQRS.Model
{
    using System;

    public class WeatherForecast
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);//Calculated fields will not be included in table

        public string Summary { get; set; }
    }
}
