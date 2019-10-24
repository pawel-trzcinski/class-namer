using ClassNamerEngine;

namespace ClassNamer
{
    /// <summary>
    /// Main program class.
    /// </summary>
    public static class Program
    {
#warning TODO - docker container with unit tests
#warning TODO - uzupełnić zbiory słów
        /// <summary>
        /// Application entry point.
        /// </summary>
        public static void Main()
        {
            Engine.InitializeContainer();
            Engine.StartHosting();
        }
    }
}