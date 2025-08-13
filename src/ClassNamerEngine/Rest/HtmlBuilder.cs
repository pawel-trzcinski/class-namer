using System;
using System.IO;
using System.Text;
using log4net;

namespace ClassNamerEngine.Rest
{
    /// <summary>
    /// Default implementation of <see cref="IHtmlBuilder"/>.
    /// </summary>
    public class HtmlBuilder : IHtmlBuilder
    {
        private const string Placeholder = "[PLACEHOLDER]";
        private static readonly int PlaceholderLength = Placeholder.Length;

        private static readonly ILog Log = LogManager.GetLogger(typeof(HtmlBuilder));

        private static readonly object LockObject = new object();
        private volatile bool _htmlFileRead;

        private string _htmlPrefix;
        private string _htmlSuffix;
        private int _htmlPartsLength;

        /// <inheritdoc/>
        public string BuildHtml(string className)
        {
            if (String.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentNullException(nameof(className));
            }

            Log.Debug($"Building html for {className}");

            Initialize();

            StringBuilder sb = new StringBuilder(_htmlPartsLength + className.Length);
            sb.Append(_htmlPrefix);
            sb.Append(className);
            sb.Append(_htmlSuffix);
            return sb.ToString();
        }

        private void Initialize()
        {
            if (_htmlFileRead)
            {
                return;
            }

            lock (LockObject)
            {
                if (_htmlFileRead)
                {
                    return;
                }

                Log.Debug("Initializing HtmlBuilder");

                string defaultFileText = File.ReadAllText(Path.Combine(".", "Data", "Default.html"));

                int indexOfPlaceholder = defaultFileText.IndexOf(Placeholder, StringComparison.OrdinalIgnoreCase);

                _htmlPrefix = defaultFileText.Substring(0, indexOfPlaceholder);
                _htmlSuffix = defaultFileText.Substring(indexOfPlaceholder + PlaceholderLength);
                _htmlPartsLength = _htmlPrefix.Length + _htmlSuffix.Length;

                _htmlFileRead = true;
            }
        }
    }
}