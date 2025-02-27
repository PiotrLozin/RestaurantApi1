using Microsoft.AspNetCore.Mvc;
using RestaurantApi.WeatherForecast.Commands;
using RestaurantApi.WeatherForecast.Queries;
using RestaurantApi.WeatherForecast.Services;

namespace RestaurantApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Weather> Get([FromQuery]WeatherForecastQuery weatherForecastQuery)
        {
            var result = _service.Get(weatherForecastQuery);
            return result;
        }

        [HttpPost("generate/{numberOfValues}")]
        public IActionResult Post([FromQuery]int numberOfValues, [FromBody] WeatherForecastCommand weatherForecastCommand)
        {
            if (numberOfValues <= 0)
            {
                return BadRequest("Number of values must be greater than 0");
            }

            if (weatherForecastCommand.MinTemperature > weatherForecastCommand.MaxTemperature)
            {
                return BadRequest("Min temperature must be less than max temperature");
            }

            var result = _service.Post(numberOfValues, weatherForecastCommand);
            return Ok(result);
        }
    }
}
