
namespace AutoMock.Tests.AutofacTest
{
    using NUnit.Framework;
    using System.Linq;
    using Autofac.Extras.Moq;
    using AutoFixture;
    using Core;
    using FluentAssertions;
    using Microsoft.Extensions.Options;
    using Moq;

    public class WeatherServiceTests
    {
        protected AutoMock AutoMock;
        protected Fixture Fixture;

        [SetUp]
        public void Setup()
        {
            this.AutoMock = AutoMock.GetStrict();
            this.Fixture = new Fixture();
        }

        [TearDown]
        public void TearDown()
        {
            this.AutoMock.Dispose();
        }

        [Test]
        public void TestShouldAutomaticallyInjectDependency()
        {
            AutoMock.Mock<IWeatherRepository>().Setup(e => e.GetWeatherForecasts(It.IsAny<string>()))
                    .Returns(this.Fixture.CreateMany<WeatherForecast>(3));
            AutoMock.Mock<IOptions<TestSettings>>().Setup(e => e.Value).Returns(new TestSettings { Name = "name" });
            //Unlike other mock injections, this object does not respond to setup changes after being instantiated. Must be called last.
            var sut = AutoMock.Create<WeatherService>();

            var result = sut.GetWeatherForecasts();

            result.Count().Should().Be(3);
        }
    }
}