using System.IO;
using log4net;
using Newtonsoft.Json.Linq;

namespace ClassNamerEngine.Configuration
{
    /// <summary>
    /// Default implementation of <see cref="IConfigurationReader"/>.
    /// Reads implementation from appsettings.json that is in the current working folder.
    /// </summary>
    public class ConfigurationReader : IConfigurationReader
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConfigurationReader));

        private static readonly object LockObject = new object();
        private volatile ClassNamerConfiguration _classNamerConfiguration;

        private readonly string _settingsFile;

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
            _settingsFile = settingsFile;
        }

        /// <inheritdoc/>
        public ClassNamerConfiguration ReadConfiguration()
        {
            if (_classNamerConfiguration != null)
            {
                return _classNamerConfiguration;
            }

            lock (LockObject)
            {
                if (_classNamerConfiguration != null)
                {
                    return _classNamerConfiguration;
                }

                Log.Info($"Reading configuration from {_settingsFile}");

                string jsonText = File.ReadAllText(_settingsFile);
                JObject settings = JObject.Parse(jsonText);

                Log.Debug($"Configuration from file: {settings.ToString()}");

                _classNamerConfiguration = settings[nameof(ClassNamerConfiguration)].ToObject<ClassNamerConfiguration>();
            }

            return _classNamerConfiguration;
        }
    }
}