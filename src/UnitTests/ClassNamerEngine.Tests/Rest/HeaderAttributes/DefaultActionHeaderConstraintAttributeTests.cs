using ClassNamerEngine.Rest.HeaderAttributes;
using NUnit.Framework;

namespace ClassNamerEngine.Tests.Rest.HeaderAttributes
{
    [TestFixture]
    public class DefaultActionHeaderConstraintAttributeTests
    {
        [Test]
        public void ItReturnsTrue()
        {
            DefaultActionHeaderConstraintAttribute attribute = new DefaultActionHeaderConstraintAttribute();
            Assert.That(attribute.Accept(null));
        }

        [Test]
        public void HasOrder100()
        {
            DefaultActionHeaderConstraintAttribute attribute = new DefaultActionHeaderConstraintAttribute();
            Assert.That(attribute.Order, Is.EqualTo(100));
        }
    }
}