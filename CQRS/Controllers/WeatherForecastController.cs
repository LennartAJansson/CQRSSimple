namespace CQRS.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CQRS.Contracts;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateWeatherForecastResponse>> Create(CreateWeatherForecastRequest request) => Ok(await mediator.Send(request));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadWeatherForecastsResponse>>> Read() => Ok(await mediator.Send(new ReadWeatherForecastsRequest()));

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
        public async Task<ActionResult<UpdateWeatherForecastResponse>> Update(UpdateWeatherForecastRequest request) => Ok(await mediator.Send(request));

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteWeatherForecastResponse>> Delete(int id)
        {
            try
            {
                return Ok(await mediator.Send(new DeleteWeatherForecastRequest(id)));
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
