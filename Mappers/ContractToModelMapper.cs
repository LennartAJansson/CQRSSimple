namespace Mappers
{
    using AutoMapper;

    using Contracts.Commands;

    using Models;

    public class ContractToModelMapper : Profile
    {
        public ContractToModelMapper()
        {
            CreateMap<CreateOperationCommand, Operation>()
                .ForMember(dest => dest.OperationId, opt => opt.MapFrom(src => src.OperationId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Ready, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.WeatherForecastId, opt => opt.MapFrom(src => src.WeatherForecastId))
                .ForMember(dest => dest.RequestData, opt => opt.MapFrom(src => src.RequestData))
                .ForMember(dest => dest.Before, opt => opt.MapFrom(src => string.Empty))
                .ForMember(dest => dest.After, opt => opt.MapFrom(src => string.Empty));
            //CreateMap<CreateWeatherForecastCommand, WeatherForecast>();
            CreateMap<UpdateWeatherForecastCommand, WeatherForecast>()
                .ForMember(dest => dest.WeatherForecastId, opt => opt.MapFrom(src => src.WeatherForecastId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Celsius, opt => opt.MapFrom(src => src.IsCelsius ? src.Temperature : (src.Temperature - 32) * 0.5556m));
            //CreateMap<DeleteWeatherForecastCommand, WeatherForecast>();
        }
    }
}