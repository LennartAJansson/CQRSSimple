namespace AllInOne.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Common.Exceptions;

    using Contracts;
    using Contracts.Querys;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly ILogger<QueryController> logger;
        private readonly IMediator mediator;

        public QueryController(ILogger<QueryController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.logger.LogDebug("Creating controller for querys");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecastQueryResponse>>> ReadWeatherForecasts()
        {
            try
            {
                return Ok(await mediator.Send(new ReadWeatherForecastsQuery()));
            }
            catch (WeatherForecastNotFoundException ex)
            {
                return BadRequest($"No records available - {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Something screwed up - {ex.Message}");
            }
        }

        [HttpGet("{weatherForecastId}")]
        public async Task<ActionResult<WeatherForecastQueryResponse>> ReadWeatherForecastById(Guid weatherForecastId)
        {
            try
            {
                return Ok(await mediator.Send(new ReadWeatherForecastByIdQuery(weatherForecastId)));
            }
            catch (WeatherForecastNotFoundException ex)
            {
                return BadRequest($"Id not found - {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Something screwed up - {ex.Message}");
            }
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<WeatherForecastQueryResponse>> ReadWeatherForecastByDate(DateTimeOffset date)
        {
            try
            {
                return Ok(await mediator.Send(new ReadWeatherForecastByDateQuery(date)));
            }
            catch (WeatherForecastNotFoundException ex)
            {
                return BadRequest($"Date not found - {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Something screwed up - {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationQueryResponse>>> ReadOperations()
        {
            try
            {
                return Ok(await mediator.Send(new ReadOperationsQuery()));
            }
            catch (WeatherForecastNotFoundException ex)
            {
                return BadRequest($"No records available - {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Something screwed up - {ex.Message}");
            }
        }
    }
}
