namespace AutoMock.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Serilog;

    public interface IWeatherRepository
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts(string name);
    }
    public class WeatherRepository : IWeatherRepository
    {
        private readonly ILogger logger;
        private readonly ISession session;
        public WeatherRepository(IHttpContextAccessor httpContextAccessor, ILogger logger = null)
        {
            this.logger = logger ?? Log.Logger;
            this.session = httpContextAccessor.HttpContext.Session;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<WeatherForecast> GetWeatherForecasts(string name)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                             {
                                 Name = session.GetString("RimNo") ?? name,
                                 Date = DateTime.Now.AddDays(index),
                                 TemperatureC = rng.Next(-20, 55),
                                 Summary = Summaries[rng.Next(Summaries.Length)]
                             })
                             .ToArray();
        }
    }
}
