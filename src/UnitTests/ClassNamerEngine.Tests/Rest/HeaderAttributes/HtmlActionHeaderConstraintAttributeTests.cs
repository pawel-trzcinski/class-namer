using ClassNamerEngine.Rest.HeaderAttributes;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using NUnit.Framework;
using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using ClassNamerEngine.Rest;
using Microsoft.Extensions.Primitives;

namespace ClassNamerEngine.Tests.Rest.HeaderAttributes
{
    [TestFixture]
    public class HtmlActionHeaderConstraintAttributeTests
    {
        [Test]
        public void DoNotAllowNullContext()
        {
            HtmlActionHeaderConstraintAttribute attribute = new HtmlActionHeaderConstraintAttribute();
            Assert.Throws<ArgumentNullException>(() => attribute.Accept(null));
        }


        [TestCase("Content-Type")]
        [TestCase("Allow")]
        [TestCase("Status")]
        public void NeedsAcceptHeader(string headerName)
        {
            ActionConstraintContext context = new ActionConstraintContext()
            {
                RouteContext = new RouteContext(new DefaultHttpContext())
            };

            IHeaderDictionary headers = context.RouteContext.HttpContext.Request.Headers;
            headers.Add(headerName, new StringValues(ClassNamerController.TEXT_HTML));

            HtmlActionHeaderConstraintAttribute attribute = new HtmlActionHeaderConstraintAttribute();

            Assert.IsFalse(attribute.Accept(context));
        }

        [TestCase(ClassNamerController.TEXT_HTML)]
        [TestCase("Allow" + ClassNamerController.TEXT_HTML)]
        [TestCase(ClassNamerController.TEXT_HTML + "Status")]
        [TestCase("Whatever" + ClassNamerController.TEXT_HTML + "Status")]
        public void AcceptContainsHtml(string headerValue)
        {
            ActionConstraintContext context = new ActionConstraintContext()
            {
                RouteContext = new RouteContext(new DefaultHttpContext())
            };

            IHeaderDictionary headers = context.RouteContext.HttpContext.Request.Headers;
            headers.Add(ClassNamerController.ACCEPT, new StringValues(headerValue));

            HtmlActionHeaderConstraintAttribute attribute = new HtmlActionHeaderConstraintAttribute();

            Assert.IsTrue(attribute.Accept(context));
        }

        [Test]
        public void HasOrder0()
        {
            HtmlActionHeaderConstraintAttribute attribute = new HtmlActionHeaderConstraintAttribute();
            Assert.AreEqual(0, attribute.Order);
        }
    }
}