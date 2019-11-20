using ClassNamerEngine;
using log4net;

namespace ClassNamer
{
    /// <summary>
    /// Main program class.
    /// </summary>
    public static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// Application entry point.
        /// </summary>
        public static void Main()
        {
            log.Info("Initializing injection container");
            Engine.InitializeContainer();

            log.Info("Starting engine hosting");
            Engine.StartHosting();
        }
    }
}