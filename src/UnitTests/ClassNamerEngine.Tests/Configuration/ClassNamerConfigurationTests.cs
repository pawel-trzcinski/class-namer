using ClassNamerEngine.Configuration;
using NUnit.Framework;
using ClassNamerEngine.Puller;
using System.Linq;

namespace ClassNamerEngine.Tests.Configuration
{
    [TestFixture]
    public class ClassNamerConfigurationTests
    {
        [Test]
        public void WordsFromFileAreCorrect()
        {
            ClassNamerConfiguration configuration = new ConfigurationReader().ReadConfiguration();

            Assert.AreEqual(1, configuration.WordsSet[NamePart.Adjective].Count);
            Assert.IsTrue(configuration.WordsSet[NamePart.Adjective].Any(p => p.Equals("Custom")));
            Assert.AreEqual(2, configuration.WordsSet[NamePart.Subject].Count);
            Assert.IsTrue(configuration.WordsSet[NamePart.Subject].Any(p => p.Equals("Threada")));
            Assert.AreEqual(3, configuration.WordsSet[NamePart.Role].Count);
            Assert.IsTrue(configuration.WordsSet[NamePart.Role].Any(p => p.Equals("Invokerb")));

            Assert.AreEqual(13, configuration.WordsSet[NamePart.Description].Count);
            Assert.Contains("Complete", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Completea", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Completeb", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Qc", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Qd", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Qe", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Qf", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Qg", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Qh", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Qi", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Qj", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Qk", configuration.WordsSet[NamePart.Description].ToArray());
            Assert.Contains("Ql", configuration.WordsSet[NamePart.Description].ToArray());
        }
    }
}