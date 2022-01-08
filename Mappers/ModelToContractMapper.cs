namespace Mappers
{
    using AutoMapper;

    using Contracts;

    using Models;

    public class ModelToContractMapper : Profile
    {
        public ModelToContractMapper()
        {
            CreateMap<Operation, OperationQueryResponse>()
                .ForCtorParam("OperationId", opt => opt.MapFrom(src => src.OperationId))
                .ForCtorParam("Ready", opt => opt.MapFrom(src => src.Ready))
                .ForCtorParam("Date", opt => opt.MapFrom(src => src.Date))
                .ForCtorParam("Action", opt => opt.MapFrom(src => src.Action))
                .ForCtorParam("WeatherForecastId", opt => opt.MapFrom(src => src.WeatherForecastId))
                .ForCtorParam("RequestData", opt => opt.MapFrom(src => src.RequestData))
                .ForCtorParam("Before", opt => opt.MapFrom(src => src.Before))
                .ForCtorParam("After", opt => opt.MapFrom(src => src.After));
            CreateMap<WeatherForecast, WeatherForecastQueryResponse>()
                .ForCtorParam("WeatherForecastId", opt => opt.MapFrom(src => src.WeatherForecastId))
                .ForCtorParam("Date", opt => opt.MapFrom(src => src.Date))
                .ForCtorParam("Celsius", opt => opt.MapFrom(src => src.Celsius))
                .ForCtorParam("Fahrenheit", opt => opt.MapFrom(src => src.Fahrenheit))
                .ForCtorParam("Summary", opt => opt.MapFrom(src => src.Summary));
        }
    }
}