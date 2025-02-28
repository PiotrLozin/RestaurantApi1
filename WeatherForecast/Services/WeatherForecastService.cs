using Microsoft.AspNetCore.Mvc;
using RestaurantApi.WeatherForecast.Commands;
using RestaurantApi.WeatherForecast.Queries;

namespace RestaurantApi.WeatherForecast.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<Weather> Get(int count, int minTemperature, int maxTemperature)
        {
            return Enumerable.Range(1, count).Select(index => new Weather
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(minTemperature, maxTemperature),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public IActionResult Post(int numberOfValues, WeatherForecastRequest weatherForecastCommand)
        {
            throw new NotImplementedException();
        }
    }
}
