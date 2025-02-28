using Microsoft.AspNetCore.Mvc;
using RestaurantApi.WeatherForecast.Commands;
using RestaurantApi.WeatherForecast.Queries;

namespace RestaurantApi.WeatherForecast.Services
{
    public interface IWeatherForecastService
    {
        public IEnumerable<Weather> Get(int count, int minTemperature, int maxTemperature);

        public IActionResult Post(int numberOfValues, WeatherForecastRequest weatherForecastCommand);
    }
}
