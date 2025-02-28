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
            var result = _service.Get(
                weatherForecastQuery.CountOfElements,
                weatherForecastQuery.MinTemperature,
                weatherForecastQuery.MaxTemperature
                );

            return result;
        }

        [HttpPost("generate")]
        public IActionResult Post([FromQuery]int numberOfValues, [FromBody] WeatherForecastRequest weatherForecastRequest)
        {
            if (numberOfValues <= 0)
            {
                return BadRequest("Number of values must be greater than 0");
            }

            if (weatherForecastRequest.MinTemperature > weatherForecastRequest.MaxTemperature)
            {
                return BadRequest("Min temperature must be less than max temperature");
            }

            var result = _service.Get(
                numberOfValues,
                weatherForecastRequest.MinTemperature,
                weatherForecastRequest.MaxTemperature);
            //var result = _service.Post(numberOfValues, weatherForecastRequest);
            return Ok(result);
        }
    }
}
