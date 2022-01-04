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
        [Time]
        public async Task<ActionResult<WeatherForecastIdResponse>> CreateWeatherForecast(CreateWeatherForecastRequest request)
        {
            try
            {
                return Ok(await mediator.Send(request));

            }
            catch (NoResultException)
            {
                return BadRequest("Record NOT saved");
            }
            catch (Exception)
            {
                return BadRequest("Something screwed up");
            }
        }

        [HttpPut]
        [Time]
        public async Task<ActionResult<WeatherForecastIdResponse>> UpdateWeatherForecast(UpdateWeatherForecastRequest request)
        {
            try
            {
                return Ok(await mediator.Send(request));
            }
            catch (DbUpdateException)
            {
                return BadRequest("Trying to update non-existing record");
            }
            catch (Exception)
            {
                return BadRequest("Something screwed up");
            }
        }

        [HttpDelete]
        [Time]
        public async Task<ActionResult<WeatherForecastIdResponse>> DeleteWeatherForecast(DeleteWeatherForecastRequest request)
        {
            try
            {
                return Ok(await mediator.Send(request));
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
    }
}
