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

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ILogger<CommandController> logger;
        private readonly IMediator mediator;

        public CommandController(ILogger<CommandController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.logger.LogDebug("Creating controller for commands");
        }

        [HttpPost]
        public async Task<ActionResult<CreateWeatherForecastResponse>> CreateWeatherForecast(CreateWeatherForecastRequest request)
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

        [HttpPut]
        public async Task<ActionResult<UpdateWeatherForecastResponse>> UpdateWeatherForecast(UpdateWeatherForecastRequest request)
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
        public async Task<ActionResult<DeleteWeatherForecastResponse>> DeleteWeatherForecast(int id)
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
