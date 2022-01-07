namespace Contracts.Querys
{
    public record ReadWeatherForecastsQuery() : IQuery<IEnumerable<WeatherForecastResponse>>;
}