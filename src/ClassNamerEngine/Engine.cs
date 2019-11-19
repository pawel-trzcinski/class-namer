using ClassNamerEngine.Configuration;
using ClassNamerEngine.Puller;
using ClassNamerEngine.Rest;
using log4net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SimpleInjector;

namespace ClassNamerEngine
{
    /// <summary>
    /// Main engine of the app. It contains all the bad, non-injectable stuff.
    /// </summary>
    public static class Engine
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Engine));

        private static IWebHost webHost;

        /// <summary>
        /// Gets main <see cref="SimpleInjector"/> container.
        /// </summary>
        public static Container InjectionContainer { get; } = new Container();

        /// <summary>
        /// Initialize <see cref="SimpleInjector"/>'s container. Should be executed at the beginning of the application.
        /// </summary>
        public static void InitializeContainer()
        {
            InjectionContainer.Options.DefaultScopedLifestyle = ScopedLifestyle.Flowing;

            InjectionContainer.RegisterSingleton<IConfigurationReader, ConfigurationReader>();
            InjectionContainer.RegisterSingleton<IRandomNamePuller, RandomNamePuller>();
            InjectionContainer.RegisterSingleton<IHtmlBuilder, HtmlBuilder>();

            InjectionContainer.Register<IClassNamerController, ClassNamerController>(Lifestyle.Scoped);

            log.Debug("Container verification attempt");
            InjectionContainer.Verify();
        }

        /// <summary>
        /// Start REST service hosting.
        /// </summary>
        public static void StartHosting()
        {
            webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            webHost.Run();
        }

        /// <summary>
        /// Stops REST service hosting.
        /// </summary>
        public static void StopHosting()
        {
            log.Info("Hosting stoppingt");
            webHost.StopAsync().Wait();
        }
    }
}