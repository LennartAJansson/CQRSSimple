namespace AllInOne.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AllInOne.Contracts;
    using AllInOne.Exceptions;

    using MediatR;

    using MethodTimer;

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
            this.logger.LogDebug("Creating controller for queries");
        }

        [HttpGet]
        [Time]
        public async Task<ActionResult<IEnumerable<ReadWeatherForecastResponse>>> ReadWeatherForecasts()
        {
            try
            {
                return Ok(await mediator.Send(new ReadWeatherForecastsRequest()));
            }
            catch (NoResultException)
            {
                return BadRequest("No records available");
            }
            catch (Exception)
            {
                return BadRequest("Something screwed up");
            }
        }

        [HttpGet("{weatherForecastId}")]
        [Time]
        public async Task<ActionResult<ReadWeatherForecastResponse>> ReadWeatherForecast(Guid weatherForecastId)
        {
            try
            {
                return Ok(await mediator.Send(new ReadWeatherForecastRequest(weatherForecastId)));
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Key not found");
            }
            catch (Exception)
            {
                return BadRequest("Something screwed up");
            }
        }

        [HttpGet]
        [Time]
        public async Task<ActionResult<IEnumerable<ReadOperationResponse>>> ReadOperations()
        {
            try
            {
                return Ok(await mediator.Send(new ReadOperationsRequest()));
            }
            catch (NoResultException)
            {
                return BadRequest("No records available");
            }
            catch (Exception)
            {
                return BadRequest("Something screwed up");
            }
        }
    }
}
