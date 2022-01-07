namespace Contracts.Querys
{
    public record ReadWeatherForecastByIdQuery(Guid WeatherForecastId) : IQuery<WeatherForecastQueryResponse>;
}