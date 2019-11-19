namespace ClassNamerEngine.Rest
{
    /// <summary>
    /// Builds html page.
    /// </summary>
    public interface IHtmlBuilder
    {
        /// <summary>
        /// Build html page for specific class name.
        /// </summary>
        /// <param name="className">Class name to display.</param>
        /// <returns>Full html page.</returns>
        string BuildHtml(string className);
    }
}
