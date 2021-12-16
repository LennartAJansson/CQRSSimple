namespace CQRS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CQRS.Model;

    public interface IQueryService
    {
        Task<WeatherForecast> Read(Guid id);
        Task<IEnumerable<WeatherForecast>> Read();
    }
}
