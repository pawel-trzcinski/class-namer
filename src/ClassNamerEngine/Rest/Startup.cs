using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace ClassNamerEngine.Rest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "It's AspNetCore reflection stuff")]
    public class Startup
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Startup));

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Debug("Startup.ConfigureServices");

            services.AddMvc();
            services.AddSingleton<IControllerFactory, ControllerFactory>();
            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app)
        {
            Log.Debug("Startup.Configure");

            app.UseMvc();
        }
    }
}
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member