namespace SplittedApi.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Common.Exceptions;

    using Contracts;
    using Contracts.Commands;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<WeatherForecastResponse>> CreateWeatherForecast(CreateWeatherForecastCommand request)
        {
            try
            {
                return Ok(await mediator.Send(request));

            }
            catch (NoWeatherForecastException ex)
            {
                return BadRequest($"Record NOT saved - {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Something screwed up - {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<WeatherForecastResponse>> UpdateWeatherForecast(UpdateWeatherForecastCommand request)
        {
            try
            {
                return Ok(await mediator.Send(request));
            }
            catch (NoWeatherForecastException ex)
            {
                return BadRequest($"Trying to update non-existing record - {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Something screwed up - {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<WeatherForecastResponse>> DeleteWeatherForecast(DeleteWeatherForecastCommand request)
        {
            try
            {
                return Ok(await mediator.Send(request));
            }
            catch (NoWeatherForecastException ex)
            {
                return BadRequest($"Trying to delete non-existing record - {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Something screwed up - {ex.Message}");
            }
        }
    }
}
