
namespace AutoMock.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Core;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService weatherService;


        public WeatherForecastController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return this.weatherService.GetWeatherForecasts();
        }
    }
}
