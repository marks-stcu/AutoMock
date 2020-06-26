namespace AutoMock.Tests.AutoMocker
{
    using System.Linq;
    using AutoFixture;
    using Core;
    using FluentAssertions;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Moq.AutoMock;
    using NUnit.Framework;

    public class WeatherRepositoryTests
    {
        private Fixture fixture;
        private AutoMocker autoMock;
        private StubbedSession session;
        private DefaultHttpContext httpContext;

        [SetUp]
        public void SetUp()
        {
            this.fixture = new Fixture();
            this.autoMock = new AutoMocker(MockBehavior.Strict);
            this.session = new StubbedSession();
            this.httpContext = new DefaultHttpContext { Session = this.session };
        }

        [Test]
        public void TestShouldAutomaticallyInjectDependency()
        {
            autoMock.GetMock<IHttpContextAccessor>().SetupProperty(e => e.HttpContext, httpContext);
            session.SetString("RimNo", "Test");
            var sut = autoMock.CreateInstance<WeatherRepository>();

            var result = sut.GetWeatherForecasts(fixture.Create<string>());

            result.Should().NotBeNullOrEmpty();
            result.All(e => e.Name == "Test").Should().BeTrue();

        }
        
    }
}
