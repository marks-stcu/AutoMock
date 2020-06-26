namespace AutoMock.Core
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Options;

    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts();

    }
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository weatherRepository;
        private readonly TestSettings testSettings;

        public WeatherService(IWeatherRepository weatherRepository, IOptions<TestSettings> testOptions)
        {
            this.weatherRepository = weatherRepository;
            this.testSettings = testOptions.Value;
        }

        public IEnumerable<WeatherForecast> GetWeatherForecasts()
        {
            return this.weatherRepository.GetWeatherForecasts(testSettings.Name);
        }
    }
}
