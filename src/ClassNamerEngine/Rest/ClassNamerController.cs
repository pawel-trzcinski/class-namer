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
        public const string TextHtml = "text/html";

        /// <summary>
        /// text/plain content type.
        /// </summary>
        public const string TextPlain = "text/plain";

        private static readonly ILog Log = LogManager.GetLogger(typeof(ClassNamerController));

        private static readonly string RobotsContent = System.IO.File.ReadAllText("robots.txt");

        private readonly IRandomNamePuller _randomNamePuller;
        private readonly IHtmlBuilder _htmlBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassNamerController"/> class.
        /// </summary>
        /// <param name="randomNamePuller">Random name generator.</param>
        /// <param name="htmlBuilder">Html page builder.</param>
        public ClassNamerController(IRandomNamePuller randomNamePuller, IHtmlBuilder htmlBuilder)
        {
            _randomNamePuller = randomNamePuller;
            _htmlBuilder = htmlBuilder;
        }

        /// <summary>
        /// Method used for debugging as well as for health-check actions.
        /// </summary>
        /// <returns>Always 200.</returns>
        [HttpGet("test")]
        public StatusCodeResult Test()
        {
            return Ok();
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
        public ContentResult GetRandomClassName(string required)
        {
            string randomClassName = _randomNamePuller.GetRandomClassName(required);
            Log.Info($"Returning plain text name: {randomClassName}");
            return Content(randomClassName, TextPlain);
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
        public ContentResult GetRandomClassNameHtml(string required)
        {
            return Content(_htmlBuilder.BuildHtml(_randomNamePuller.GetRandomClassName(required)), TextHtml);
        }

        /// <summary>
        /// Standard robots file.
        /// </summary>
        /// <returns>Robots file content.</returns>
        [HttpGet("robots.txt")]
        public ContentResult Robots()
        {
            return Content(RobotsContent, TextPlain);
        }
    }
}