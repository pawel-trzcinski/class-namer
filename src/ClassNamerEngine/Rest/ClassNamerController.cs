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

        /// <summary>
        /// text/plain content type.
        /// </summary>
        public const string TEXT_PLAIN = "text/plain";

        private static readonly ILog log = LogManager.GetLogger(typeof(ClassNamerController));

        private static readonly string robots_content = System.IO.File.ReadAllText("robots.txt");

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
        [HttpGet]
        [Route("")]
        [Route("RandomClassName")]
        [DefaultActionHeaderConstraint]
        [AddCorsHeader]
        [AddGitHubHeader]
        public ContentResult GetRandomClassName()
        {
            string randomClassName = this.randomNamePuller.GetRandomClassName();
            log.Info($"Returning plain text name: {randomClassName}");
            return Content(randomClassName, TEXT_PLAIN);
        }

        /// <summary>
        /// Execute random name generator.
        /// </summary>
        /// <returns>Randomly generated class name embodied in Html.</returns>
        [HttpGet]
        [Route("")]
        [Route("RandomClassName")]
        [HtmlActionHeaderConstraint]
        [AddCorsHeader]
        [AddGitHubHeader]
        public ContentResult GetRandomClassNameHtml()
        {
            return Content(this.htmlBuilder.BuildHtml(this.randomNamePuller.GetRandomClassName()), TEXT_HTML);
        }

        /// <summary>
        /// Standard robots file.
        /// </summary>
        /// <returns>Robots file content.</returns>
        [HttpGet("robots.txt")]
        public ContentResult Robots()
        {
            return Content(robots_content, TEXT_PLAIN);
        }
    }
}