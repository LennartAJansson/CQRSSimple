namespace CQRS.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CQRS.Contracts;
    using CQRS.Exceptions;

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
            this.logger.LogDebug("Creating controller for queries");
        }

        [HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<OrderSummary>), (int)HttpStatusCode.OK)]
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
            catch (System.Exception)
            {
                return BadRequest("Something screwed up");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadWeatherForecastResponse>> ReadWeatherForecast(Guid id)
        {
            try
            {
                return Ok(await mediator.Send(new ReadWeatherForecastRequest(id)));
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Key not found");
            }
            catch (System.Exception)
            {
                return BadRequest("Something screwed up");
            }
        }

    }
}
