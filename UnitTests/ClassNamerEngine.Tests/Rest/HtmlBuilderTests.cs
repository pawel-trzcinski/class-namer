using ClassNamerEngine.Rest;
using NUnit.Framework;
using System;

namespace ClassNamerEngine.Tests.Rest
{
    [TestFixture]
    public class HtmlBuilderTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        [TestCase("\r")]
        [TestCase("\n")]
        public void ClassNameCanNotBeEmpty(string className)
        {
            Assert.Throws<ArgumentNullException>(() => new HtmlBuilder().BuildHtml(className));
        }

        [Test]
        public void ClassNameIsContainedWithinHtmlBody()
        {
            string className = Guid.NewGuid().ToString();
            string html = new HtmlBuilder().BuildHtml(className);

            int html1Index = html.IndexOf("<html", StringComparison.OrdinalIgnoreCase);
            int html2Index = html.IndexOf("</html", StringComparison.OrdinalIgnoreCase);
            int body1Index = html.IndexOf("<body", StringComparison.OrdinalIgnoreCase);
            int body2Index = html.IndexOf("</body", StringComparison.OrdinalIgnoreCase);
            int nameIndex = html.IndexOf(className, StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(-1, html1Index);
            Assert.AreNotEqual(-1, html2Index);
            Assert.AreNotEqual(-1, body1Index);
            Assert.AreNotEqual(-1, body2Index);

            Assert.IsTrue(html1Index < body1Index);
            Assert.IsTrue(body1Index < nameIndex);
            Assert.IsTrue(nameIndex < body2Index);
            Assert.IsTrue(body2Index < html2Index);
        }
    }
}