using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Primitives;

namespace ClassNamerEngine.Rest.HeaderAttributes
{
    /// <summary>
    /// Action constraint that checks if header has Accept:text/html value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HtmlActionHeaderConstraintAttribute : Attribute, IActionConstraint
    {
        /// <inheritdoc />
        public int Order => 0;

        /// <inheritdoc />
        public bool Accept(ActionConstraintContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.RouteContext.HttpContext.Request.Headers.TryGetValue(ClassNamerController.ACCEPT, out StringValues values) && values.Any(v => (v ?? String.Empty).IndexOf(ClassNamerController.TextHtml, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}