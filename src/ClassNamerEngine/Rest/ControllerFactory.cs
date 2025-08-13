using System;
using log4net;
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
        private static readonly ILog Log = LogManager.GetLogger(typeof(ControllerFactory));

        private readonly Container _container;

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
            _container = container;
        }

        /// <inheritdoc/>
        public object CreateController(ControllerContext context)
        {
            Log.Debug("Creating controller");

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Scope scope = ThreadScopedLifestyle.BeginScope(_container);

            Log.Debug("Seting Scope feature");
            context.HttpContext.Features.Set<Scope>(scope);

            Log.Debug("Getting controller from incection container");
            return scope.GetInstance<IClassNamerController>();
        }

        /// <inheritdoc/>
        public void ReleaseController(ControllerContext context, object controller)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Log.Debug("Disposing of Scope feature");
            context.HttpContext.Features.Get<Scope>().Dispose();
        }
    }
}