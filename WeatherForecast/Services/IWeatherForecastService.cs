using Microsoft.AspNetCore.Mvc;
using RestaurantApi.WeatherForecast.Commands;
using RestaurantApi.WeatherForecast.Queries;

namespace RestaurantApi.WeatherForecast.Services
{
    public interface IWeatherForecastService
    {
        public IEnumerable<Weather> Get(WeatherForecastQuery weatherForecastQuery);

        public IActionResult Post(int numberOfValues, WeatherForecastCommand weatherForecastCommand);
    }
}
