using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace ClassNamerEngine.Rest
{
    /// <summary>
    /// Factory for creating scoped controller with use of <see cref="SimpleInjector"/>.
    /// </summary>
    public class ControllerFactory : IControllerFactory
    {
        private readonly Container container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerFactory"/> class.
        /// </summary>
        public ControllerFactory()
            : this(Engine.InjectionContainer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerFactory"/> class.
        /// </summary>
        /// <param name="container"><see cref="SimpleInjector"/> container.</param>
        public ControllerFactory(Container container)
        {
            this.container = container;
        }

        /// <inheritdoc/>
        public object CreateController(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Scope scope = ThreadScopedLifestyle.BeginScope(container);
            context.HttpContext.Features.Set<Scope>(scope);
            return scope.GetInstance<IClassNamerController>();
        }

        /// <inheritdoc/>
        public void ReleaseController(ControllerContext context, object controller)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.HttpContext.Features.Get<Scope>().Dispose();
        }
    }
}