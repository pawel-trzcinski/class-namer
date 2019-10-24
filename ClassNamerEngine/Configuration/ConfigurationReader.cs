using System.IO;
using Newtonsoft.Json.Linq;

namespace ClassNamerEngine.Configuration
{
    /// <summary>
    /// Default implementation of <see cref="IConfigurationReader"/>.
    /// Reads implementation from appsettings.json that is in the current working folder.
    /// </summary>
    public class ConfigurationReader : IConfigurationReader
    {
        private static readonly object lockObject = new object();
        private volatile ClassNamerConfiguration classNamerConfiguration;

        private readonly string settingsFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationReader"/> class.
        /// </summary>
        public ConfigurationReader()
            : this("./appsettings.json")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationReader"/> class.
        /// </summary>
        /// <param name="settingsFile">File to read configuration from.</param>
        protected ConfigurationReader(string settingsFile)
        {
            this.settingsFile = settingsFile;
        }

        /// <inheritdoc/>
        public ClassNamerConfiguration ReadConfiguration()
        {
            if (this.classNamerConfiguration != null)
            {
                return this.classNamerConfiguration;
            }

            lock (lockObject)
            {
                if (this.classNamerConfiguration != null)
                {
                    return this.classNamerConfiguration;
                }

                string jsonText = File.ReadAllText(settingsFile);
                JObject settings = JObject.Parse(jsonText);

                this.classNamerConfiguration = settings[nameof(ClassNamerConfiguration)].ToObject<ClassNamerConfiguration>();
            }

            return this.classNamerConfiguration;
        }
    }
}