namespace CQRS.Services
{
    using System;
    using System.Threading.Tasks;

    using CQRS.Model;

    public interface ICommandService
    {
        Task<WeatherForecast> Create(WeatherForecast forecast);
        Task<WeatherForecast> Update(WeatherForecast forecast);
        Task<WeatherForecast> Delete(Guid id);
    }
}
