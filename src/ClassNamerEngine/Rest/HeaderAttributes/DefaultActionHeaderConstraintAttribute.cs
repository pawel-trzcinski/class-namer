using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ClassNamerEngine.Rest.HeaderAttributes
{
    /// <summary>
    /// Action constraint that checks nothing.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DefaultActionHeaderConstraintAttribute : Attribute, IActionConstraint
    {
        /// <inheritdoc />
        public int Order => 100;

        /// <inheritdoc />
        public bool Accept(ActionConstraintContext context)
        {
            return true;
        }
    }
}