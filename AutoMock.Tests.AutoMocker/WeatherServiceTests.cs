namespace AutoMock.Tests.AutoMocker
{
    using System.Linq;
    using AutoFixture;
    using Core;
    using FluentAssertions;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.AutoMock;
    using NUnit.Framework;

    [TestFixture]
    public class WeatherServiceTests
    {
        private AutoMocker mocker;
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            this.mocker = new AutoMocker(MockBehavior.Strict);
            this.fixture = new Fixture();
        }

        [Test]
        public void TestShouldAutomaticallyInjectDependency()
        { 
            var mockWeatherRepo = mocker.GetMock<IWeatherRepository>();
                mockWeatherRepo.Setup(e => e.GetWeatherForecasts(It.IsAny<string>()))
                  .Returns(this.fixture.CreateMany<WeatherForecast>(3));
                mocker.GetMock<IOptions<TestSettings>>().Setup(e => e.Value).Returns(new TestSettings { Name = "name" });
            var sut = mocker.CreateInstance<WeatherService>();

            var actual = sut.GetWeatherForecasts();

            actual.Count().Should().Be(3);
            mockWeatherRepo.Verify(mock => mock.GetWeatherForecasts(It.IsAny<string>()), Times.Once);
        }
    }


}
