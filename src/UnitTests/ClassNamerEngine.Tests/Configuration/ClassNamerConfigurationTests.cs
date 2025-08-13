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

            Assert.That(configuration.WordsSet[NamePart.Adjective].Count, Is.EqualTo(1));
            Assert.That(configuration.WordsSet[NamePart.Adjective].Any(p => p.Equals("Custom")));
            Assert.That(configuration.WordsSet[NamePart.Subject].Count, Is.EqualTo(2));
            Assert.That(configuration.WordsSet[NamePart.Subject].Any(p => p.Equals("Threada")));
            Assert.That(configuration.WordsSet[NamePart.Role].Count, Is.EqualTo(3));
            Assert.That(configuration.WordsSet[NamePart.Role].Any(p => p.Equals("Invokerb")));

            Assert.That(configuration.WordsSet[NamePart.Description].Count, Is.EqualTo(13));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Complete"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Completea"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Completeb"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Qc"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Qd"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Qe"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Qf"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Qg"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Qh"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Qi"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Qj"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Qk"));
            Assert.That(configuration.WordsSet[NamePart.Description].ToArray(), Contains.Item("Ql"));
        }
    }
}