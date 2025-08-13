using ClassNamerEngine.Configuration;
using NUnit.Framework;

namespace ClassNamerEngine.Tests.Configuration
{
    [TestFixture]
    public class ConfigurationReaderTests
    {
        [Test]
        public void ConfigrationHasValuesAsInFile()
        {
            ClassNamerConfiguration configuration = new ConfigurationReader().ReadConfiguration();

            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.Combinations, Is.Not.Null);
            Assert.That(configuration.Combinations.Count, Is.EqualTo(5));
            Assert.That(configuration.WordsSet, Is.Not.Null);
            Assert.That(configuration.WordsSet.Count, Is.EqualTo(4));
        }
    }
}