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
            headers.Add(headerName, new StringValues(ClassNamerController.TextHtml));

            HtmlActionHeaderConstraintAttribute attribute = new HtmlActionHeaderConstraintAttribute();

            Assert.That(!attribute.Accept(context));
        }

        [TestCase(ClassNamerController.TextHtml)]
        [TestCase("Allow" + ClassNamerController.TextHtml)]
        [TestCase(ClassNamerController.TextHtml + "Status")]
        [TestCase("Whatever" + ClassNamerController.TextHtml + "Status")]
        public void AcceptContainsHtml(string headerValue)
        {
            ActionConstraintContext context = new ActionConstraintContext()
            {
                RouteContext = new RouteContext(new DefaultHttpContext())
            };

            IHeaderDictionary headers = context.RouteContext.HttpContext.Request.Headers;
            headers.Add(ClassNamerController.ACCEPT, new StringValues(headerValue));

            HtmlActionHeaderConstraintAttribute attribute = new HtmlActionHeaderConstraintAttribute();

            Assert.That(attribute.Accept(context));
        }

        [Test]
        public void HasOrder0()
        {
            HtmlActionHeaderConstraintAttribute attribute = new HtmlActionHeaderConstraintAttribute();
            Assert.That(attribute.Order, Is.Zero);
        }
    }
}