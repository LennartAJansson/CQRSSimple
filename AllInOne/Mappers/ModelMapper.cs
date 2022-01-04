namespace AllInOne.Mappers
{
    using AllInOne.Contracts;
    using AllInOne.Model;

    using AutoMapper;

    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            // https://docs.automapper.org/en/stable/Configuration.html

            CreateMap<ReadWeatherForecastResponse, WeatherForecast>()
                .ForMember(dest => dest.WeatherForecastId, opt => opt.MapFrom(src => src.WeatherForecastId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.TemperatureC, opt => opt.MapFrom(src => src.TemperatureC))
                .ForMember(dest => dest.TemperatureF, opt => opt.MapFrom(src => src.TemperatureF))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary));

            CreateMap<WeatherForecast, ReadWeatherForecastResponse>()
                .ForCtorParam("Date", opt => opt.MapFrom(src => src.Date))
                .ForCtorParam("TemperatureC", opt => opt.MapFrom(src => src.TemperatureC))
                .ForCtorParam("TemperatureF", opt => opt.MapFrom(src => src.TemperatureF))
                .ForCtorParam("Summary", opt => opt.MapFrom(src => src.Summary));

            CreateMap<ReadOperationResponse, Operation>()
                .ForMember(dest => dest.OperationId, opt => opt.MapFrom(src => src.OperationId))
                .ForMember(dest => dest.Ready, opt => opt.MapFrom(src => src.Ready))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.WeatherForecastId, opt => opt.MapFrom(src => src.WeatherForecastId))
                .ForMember(dest => dest.RequestData, opt => opt.MapFrom(src => src.RequestData))
                .ForMember(dest => dest.Before, opt => opt.MapFrom(src => src.Before))
                .ForMember(dest => dest.After, opt => opt.MapFrom(src => src.After));

            CreateMap<Operation, ReadOperationResponse>()
                .ForCtorParam("OperationId", opt => opt.MapFrom(src => src.OperationId))
                .ForCtorParam("Ready", opt => opt.MapFrom(src => src.Ready))
                .ForCtorParam("Date", opt => opt.MapFrom(src => src.Date))
                .ForCtorParam("Action", opt => opt.MapFrom(src => src.Action))
                .ForCtorParam("WeatherForecastId", opt => opt.MapFrom(src => src.WeatherForecastId))
                .ForCtorParam("RequestData", opt => opt.MapFrom(src => src.RequestData))
                .ForCtorParam("Before", opt => opt.MapFrom(src => src.Before))
                .ForCtorParam("After", opt => opt.MapFrom(src => src.After));

        }
    }
}
