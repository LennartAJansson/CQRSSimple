namespace AllInOne.Mediators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AllInOne.Contracts;

    using AllInOne.Model;
    using AllInOne.Services;

    using AutoMapper;

    using MediatR;

    using MethodTimer;

    using Microsoft.Extensions.Logging;

    public class QueryMediator : IRequestHandler<ReadWeatherForecastRequest, ReadWeatherForecastResponse>,
        IRequestHandler<ReadWeatherForecastsRequest, IEnumerable<ReadWeatherForecastResponse>>,
        IRequestHandler<ReadOperationsRequest, IEnumerable<ReadOperationResponse>>

    {
        private readonly ILogger<QueryMediator> logger;
        private readonly IQueryService service;
        private readonly IMapper mapper;

        public QueryMediator(ILogger<QueryMediator> logger, IQueryService service, IMapper mapper)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
        }

        [Time]
        public async Task<ReadWeatherForecastResponse> Handle(ReadWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for ReadWeatherForecastRequest");

            WeatherForecast w = await service.ReadWeatherForecast(request.WeatherForecastId);
            return mapper.Map<ReadWeatherForecastResponse>(w);
        }

        [Time]
        public async Task<IEnumerable<ReadWeatherForecastResponse>> Handle(ReadWeatherForecastsRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for ReadWeatherForecastsRequest");

            IEnumerable<WeatherForecast> forecasts = await service.ReadWeatherForecasts();

            return forecasts.Select(w => mapper.Map<ReadWeatherForecastResponse>(w));
        }

        [Time]
        public async Task<IEnumerable<ReadOperationResponse>> Handle(ReadOperationsRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Handle for ReadOperationsRequest");

            IEnumerable<Operation> operations = await service.ReadOperations();

            return operations.Select(o => mapper.Map<ReadOperationResponse>(o));
        }
    }
}
