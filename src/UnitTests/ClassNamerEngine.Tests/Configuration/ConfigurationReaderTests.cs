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

            Assert.IsNotNull(configuration);
            Assert.IsNotNull(configuration.Combinations);
            Assert.AreEqual(5, configuration.Combinations.Count);
            Assert.IsNotNull(configuration.WordsSet);
            Assert.AreEqual(4, configuration.WordsSet.Count);
        }
    }
}