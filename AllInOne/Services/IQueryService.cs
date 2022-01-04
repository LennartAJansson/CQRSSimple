namespace AllInOne.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AllInOne.Model;

    public interface IQueryService
    {
        Task<WeatherForecast> ReadWeatherForecast(Guid id);
        Task<IEnumerable<WeatherForecast>> ReadWeatherForecasts();
        Task<IEnumerable<Operation>> ReadOperations();
    }
}
