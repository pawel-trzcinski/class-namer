using ClassNamerEngine;
using log4net;

namespace ClassNamer
{
    /// <summary>
    /// Main program class.
    /// </summary>
    public static class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// Application entry point.
        /// </summary>
        public static void Main()
        {
            Log.Info("Initializing injection container");
            Engine.InitializeContainer();

            Log.Info("Starting engine hosting");
            Engine.StartHosting();
        }
    }
}