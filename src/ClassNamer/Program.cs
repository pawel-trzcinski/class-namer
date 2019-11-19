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

#warning TODO - niech Madzia powie jak ten html zrobić, żeby google dobrze złapało
#warning TODO - kupic ClassNamer.pl albo jakies inne panstwo
#warning TODO - znalezc hosting darmowy i wieczny

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