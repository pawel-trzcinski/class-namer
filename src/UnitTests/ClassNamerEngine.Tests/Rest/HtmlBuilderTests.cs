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

            Assert.That(html1Index, Is.Not.EqualTo(-1));
            Assert.That(html2Index, Is.Not.EqualTo(-1));
            Assert.That(body1Index, Is.Not.EqualTo(-1));
            Assert.That(body2Index, Is.Not.EqualTo(-1));

            Assert.That(html1Index < body1Index);
            Assert.That(body1Index < nameIndex);
            Assert.That(nameIndex < body2Index);
            Assert.That(body2Index < html2Index);
        }
    }
}