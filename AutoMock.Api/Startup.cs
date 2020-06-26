using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoMock.Api
{
    using Core;
    using STCU.Middleware.Services;
    using STCU.SerilogConfiguration.Web;

    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Env { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TestSettings>(Configuration.GetSection(nameof(TestSettings)));
            services.AddControllers();
            services.AddTransient<IWeatherService, WeatherService>()
                    .AddTransient<IWeatherRepository, WeatherRepository>();
            services.AddSerilog("AutoMock", Env.EnvironmentName, Configuration);
            services.AddSession(options => options.Cookie =
                                               CookieSettingsService.CreateCookieBuilder(
                                                   $"Session.AutoMock",
                                                   GetApplicationVirtualPath()
                                               ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private string GetApplicationVirtualPath() =>
            Configuration.GetValue<string>("CookieSettings:ApplicationVirtualPath");
    }
}
