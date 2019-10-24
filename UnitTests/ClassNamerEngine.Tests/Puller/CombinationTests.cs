using ClassNamerEngine.Puller;
using NUnit.Framework;
using System;

namespace ClassNamerEngine.Tests.Puller
{
    [TestFixture]
    public class CombinationTests
    {
        [Test]
        public void NullPartsNotAllowed()
        {
            Assert.Throws<ArgumentNullException>(() => new Combination(null, 0));
        }

        [Test]
        public void EmptyPartsNotAllowed()
        {
            Assert.Throws<ArgumentNullException>(() => new Combination(new NamePart[0], 0));
        }

        [TestCase(-100)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(101)]
        [TestCase(200)]
        [TestCase(999)]
        public void WeightOutsideOfRangeNotAllowed(int weight)
        {
            Assert.Throws<ArgumentException>(() => new Combination(new NamePart[] { NamePart.Adjective }, weight));
        }

        [Test]
        public void WeightInRangeAllowed([Range(1, 100)]int weight)
        {
            Assert.DoesNotThrow(() => new Combination(new NamePart[] { NamePart.Adjective }, weight));
        }
    }
}