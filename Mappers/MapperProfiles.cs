namespace Mappers
{

    using Microsoft.Extensions.DependencyInjection;

    public static class MapperProfiles
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile<ContractToModelMapper>(), typeof(ContractToModelMapper).Assembly)
                .AddAutoMapper(c => c.AddProfile<ModelToContractMapper>(), typeof(ModelToContractMapper).Assembly);

            return services;
        }
    }
}