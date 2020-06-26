namespace AutoMock.Tests.AutofacTest
{
    using System.Linq;
    using AutoFixture;
    using Core;
    using FluentAssertions;
    using Autofac.Extras.Moq;
    using Microsoft.AspNetCore.Http;
    using NUnit.Framework;

    [TestFixture]
    public class WeatherRepositoryTests
    {
        private Fixture fixture;
        private AutoMock autoMock;
        private HttpContext httpContext;
        private ISession session;

        [SetUp]
        public void Setup()
        {
            this.fixture = new Fixture();
            this.autoMock = AutoMock.GetStrict();
            this.session = new StubbedSession();
            this.httpContext = new DefaultHttpContext {Session = this.session};
        }


        [Test]
        public void TestShouldAutomaticallyInjectDependency()
        {

            autoMock.Mock<IHttpContextAccessor>().SetupProperty(e => e.HttpContext, httpContext);
            session.SetString("RimNo", "Test");
            var sut = autoMock.Create<WeatherRepository>();
            var result = sut.GetWeatherForecasts(fixture.Create<string>());
            result.Should().NotBeNullOrEmpty();
            result.All(e => e.Name == "Test").Should().BeTrue();
        }


    }
}
