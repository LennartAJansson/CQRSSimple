namespace AllInOne.Services
{
    using System;
    using System.Threading.Tasks;

    using AllInOne.Model;

    public interface ICommandService
    {
        Task<WeatherForecast> CreateWeatherForecast(WeatherForecast forecast);
        Task<WeatherForecast> UpdateWeatherForecast(WeatherForecast forecast);
        Task<WeatherForecast> DeleteWeatherForecast(Guid id);
        Task<Operation> CreateOperation(Operation operation);
    }
}
