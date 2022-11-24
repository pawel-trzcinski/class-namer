using ClassNamerEngine;
using log4net;

namespace ClassNamer
{
	// TODO: 
	// It would be good to have some optional pre-conditions (e.g. a desired purpose of the class like "factory" or "service" or a minimum number of words that we want to use)
	// Language selector (e.g. when using Java names are going to be 10x longer)
	// Input field with a "required" keyword that we want to include in the generator
	// Choosing your desired seniority level (e.g. on intern level you would get mixed PascalCase, snake_case etc.)
	// Obscenity level? If we want some additional swear words
	
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