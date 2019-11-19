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
            Assert.IsTrue(attribute.Accept(null));
        }

        [Test]
        public void HasOrder100()
        {
            DefaultActionHeaderConstraintAttribute attribute = new DefaultActionHeaderConstraintAttribute();
            Assert.AreEqual(100, attribute.Order);
        }
    }
}