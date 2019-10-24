using ClassNamerEngine.Rest;
using NUnit.Framework;
using SimpleInjector;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace ClassNamerEngine.Tests.Rest
{
    [TestFixture]
    public class ControllerFactoryTests
    {
        private class TestController : ControllerFactory, IClassNamerController
        {
            public void Dispose()
            {
            }
        }


        [Test]
        public void CreateDoesNotAllowNullContext()
        {
            Container container = new Container();
            ControllerFactory factory = new ControllerFactory(container);

            Assert.Throws<ArgumentNullException>(() => factory.CreateController(null));
        }


        [Test]
        public void ReleaseDoesNotAllowNullContext()
        {
            Container container = new Container();
            ControllerFactory factory = new ControllerFactory(container);

            Assert.Throws<ArgumentNullException>(() => factory.ReleaseController(null, null));
        }

        [Test]
        public void FactoryCreatesControllerAndReleasesIt()
        {
            Container container = new Container();
            container.Options.DefaultScopedLifestyle = ScopedLifestyle.Flowing;

            container.Register<IClassNamerController, TestController>(Lifestyle.Scoped);
            ControllerFactory factory = new ControllerFactory(container);
            ControllerContext context = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };
            bool scopeEnded = false;

            Assert.AreEqual(typeof(TestController).FullName, factory.CreateController(context).GetType().FullName);

            Scope scope = context.HttpContext.Features.Get<Scope>();
            Assert.IsNotNull(scope);
            scope.WhenScopeEnds(() => scopeEnded = true);

            factory.ReleaseController(context, null);
            Assert.IsTrue(scopeEnded);
        }
    }
}