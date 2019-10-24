using System;
using System.IO;
using System.Text;

namespace ClassNamerEngine.Rest
{
    /// <summary>
    /// Default implementation of <see cref="IHtmlBuilder"/>.
    /// </summary>
    public class HtmlBuilder : IHtmlBuilder
    {
        private const string PLACEHOLDER = "[PLACEHOLDER]";
        private static readonly int placeholderLength = PLACEHOLDER.Length;

        private static readonly object lockObject = new object();
        private volatile bool htmlFileRead;

        private string htmlPrefix;
        private string htmlSuffix;
        private int htmlPartsLength;

        /// <inheritdoc/>
        public string BuildHtml(string className)
        {
            if (String.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentNullException(nameof(className));
            }

            Initialize();

            StringBuilder sb = new StringBuilder(htmlPartsLength + className.Length);
            sb.Append(htmlPrefix);
            sb.Append(className);
            sb.Append(htmlSuffix);
            return sb.ToString();
        }

        private void Initialize()
        {
            if (this.htmlFileRead)
            {
                return;
            }

            lock (lockObject)
            {
                if (this.htmlFileRead)
                {
                    return;
                }

                string defaultFileText = File.ReadAllText(Path.Combine(".", "Data", "Default.html"));

                int indexOfPlaceholder = defaultFileText.IndexOf(PLACEHOLDER, StringComparison.OrdinalIgnoreCase);

                this.htmlPrefix = defaultFileText.Substring(0, indexOfPlaceholder);
                this.htmlSuffix = defaultFileText.Substring(indexOfPlaceholder + placeholderLength);
                this.htmlPartsLength = this.htmlPrefix.Length + this.htmlSuffix.Length;

                htmlFileRead = true;
            }
        }
    }
}