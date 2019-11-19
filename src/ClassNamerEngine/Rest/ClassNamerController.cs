using ClassNamerEngine.Puller;
using ClassNamerEngine.Rest.HeaderAttributes;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace ClassNamerEngine.Rest
{
    /// <summary>
    /// Default ClassNamer controller.
    /// </summary>
    public class ClassNamerController : Controller, IClassNamerController
    {
        /// <summary>
        /// Accept header name.
        /// </summary>
        public const string ACCEPT = "Accept";

        /// <summary>
        /// text/html content type.
        /// </summary>
        public const string TEXT_HTML = "text/html";

        private static readonly ILog log = LogManager.GetLogger(typeof(ClassNamerController));

        private readonly IRandomNamePuller randomNamePuller;
        private readonly IHtmlBuilder htmlBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassNamerController"/> class.
        /// </summary>
        /// <param name="randomNamePuller">Random name generator.</param>
        /// <param name="htmlBuilder">Html page builder.</param>
        public ClassNamerController(IRandomNamePuller randomNamePuller, IHtmlBuilder htmlBuilder)
        {
            this.randomNamePuller = randomNamePuller;
            this.htmlBuilder = htmlBuilder;
        }

        /// <summary>
        /// Method used for debugging as well as for health-check actions.
        /// </summary>
        /// <returns>Always 200.</returns>
        [HttpGet("test")]
        public StatusCodeResult Test()
        {
            return this.Ok();
        }

        /// <summary>
        /// Execute random name generator.
        /// </summary>
        /// <returns>Randomly generated class name.</returns>
        [HttpGet("RandomClassName")]
        [DefaultActionHeaderConstraint]
        [AddGitHubHeader]
        public ContentResult GetRandomClassName()
        {
            string randomClassName = this.randomNamePuller.GetRandomClassName();
            log.Info($"Returning plain text name: {randomClassName}");
            return Content(randomClassName, "text/plain");
        }

        /// <summary>
        /// Execute random name generator.
        /// </summary>
        /// <returns>Randomly generated class name.</returns>
        [HttpGet("RandomClassName")]
        [HtmlActionHeaderConstraint]
        [AddGitHubHeader]
        public ContentResult GetRandomClassNameHtml()
        {
            return Content(this.htmlBuilder.BuildHtml(this.randomNamePuller.GetRandomClassName()), TEXT_HTML);
        }
    }
}