namespace CQRS.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CQRS.Contracts;
    using CQRS.Exceptions;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> logger;
        private readonly IMediator mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.logger.LogDebug("Creating controller for request");
        }

        [HttpPost]
        public async Task<ActionResult<CreateWeatherForecastResponse>> Create(CreateWeatherForecastRequest request)
        {
            try
            {
                return Ok(await mediator.Send(request));

            }
            catch (NoResultException)
            {
                return BadRequest("Record NOT saved");
            }
            catch (System.Exception)
            {
                return BadRequest("Something screwed up");
            }
        }

        [HttpGet]
        public async Task<ActionResult<ReadWeatherForecastsResponse>> Read()
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
        public async Task<ActionResult<ReadWeatherForecastResponse>> Read(int id)
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

        [HttpPut]
        public async Task<ActionResult<UpdateWeatherForecastResponse>> Update(UpdateWeatherForecastRequest request)
        {
            try
            {
                return Ok(await mediator.Send(request));
            }
            catch (DbUpdateException)
            {
                return BadRequest("Trying to update non-existing record");
            }
            catch (System.Exception)
            {
                return BadRequest("Something screwed up");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteWeatherForecastResponse>> Delete(int id)
        {
            try
            {
                return Ok(await mediator.Send(new DeleteWeatherForecastRequest(id)));
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
