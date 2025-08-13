using ClassNamerEngine.Configuration;
using ClassNamerEngine.Puller;
using NUnit.Framework;
using System;
using System.Linq;
using AutoFixture;
using Moq;

namespace ClassNamerEngine.Tests.Puller
{
    [TestFixture]
    public class RandomNamePullerTests
    {
        private static readonly string[] PossibleWords = new string[]
        {
            "Custom",

            "Complete",
            "Completea",
            "Completeb",
            "Qc",
            "Qd",
            "Qe",
            "Qf",
            "Qg",
            "Qh",
            "Qi",
            "Qj",
            "Qk",
            "Ql",

           "Thread",
            "Threada",

            "Invoker",
            "Invokera",
            "Invokerb"
        }.OrderByDescending(p => p.Length).ToArray();

        private class TestConfigurationReader : ConfigurationReader
        {
            public TestConfigurationReader(string fileName)
                : base(fileName)
            {
            }
        }

        [Test]
        [Combinatorial]
        [Repeat(50)]
        public void CreatesRandomClassName([Range(1, 10)] int singleInstanceExecCount, [Values(null, "", " ")]string emptyRequired)
        {
            RandomNamePuller puller = new RandomNamePuller(new ConfigurationReader());

            for (int i = 0; i < singleInstanceExecCount; i++)
            {
                TestOneName(puller.GetRandomClassName(emptyRequired));
            }
        }

        private static void TestOneName(string name)
        {
            Console.WriteLine($"Testing '{name}'");

            Assert.That(name, Is.Not.Null);
            Assert.That(name, Is.Not.Empty);

            string workingName = name;
            while (!String.IsNullOrEmpty(workingName))
            {
                bool wordRemoved = false;
                for (int i = 0; i < PossibleWords.Length; i++)
                {
                    int index = workingName.IndexOf(PossibleWords[i]);

                    if (index == -1)
                    {
                        // next word check
                        continue;
                    }

                    // remove word from name
                    workingName = workingName.Remove(index, PossibleWords[i].Length);
                    wordRemoved = true;
                }

                if (!wordRemoved && !String.IsNullOrEmpty(workingName))
                {
                    Assert.Fail($"Unknown words left in name '{name}': '{workingName}'");
                }
            }
        }

        [Test]
        [Repeat(50)]
        public void RequiredWordIsInserted()
        {
            // Arrange
            var fixture = new Fixture();
            string required = fixture.Create<string>();
            var puller = new RandomNamePuller(new ConfigurationReader());

            // Act
            string name = puller.GetRandomClassName(required);
            
            // Assert
            Assert.That(name, Contains.Substring(required));
        }
        
        [TestCase("./appsettings.CombinationTest1.json", "AdjectivexxxDescriptionxxxSubjectxxxRolexxx")]
        [TestCase("./appsettings.CombinationTest2.json", "RolexxxDescriptionxxxSubjectxxxAdjectivexxx")]
        [TestCase("./appsettings.CombinationTest3.json", "Adjectivexxx")]
        [TestCase("./appsettings.CombinationTest4.json", "Descriptionxxx")]
        [TestCase("./appsettings.CombinationTest5.json", "Subjectxxx")]
        [TestCase("./appsettings.CombinationTest6.json", "Rolexxx")]
        [TestCase("./appsettings.CombinationTest7.json", "AdjectivexxxRolexxx")]
        [TestCase("./appsettings.CombinationTest8.json", "DescriptionxxxSubjectxxxRolexxx")]
        public void CombinationsAreCorrect(string configurationFile, string expectedClassName)
        {
            Assert.That(new RandomNamePuller(new TestConfigurationReader(configurationFile)).GetRandomClassName(null), Is.EqualTo(expectedClassName));
        }

        [TestCase("./appsettings.DuplicateConsecutivesTest1.json", "ThreadWordoneWordtwo")]
        [TestCase("./appsettings.DuplicateConsecutivesTest2.json", "WordoneDogWordtwo")]
        [TestCase("./appsettings.DuplicateConsecutivesTest3.json", "WordoneWordtwoBug")]
        [TestCase("./appsettings.DuplicateConsecutivesTest4.json", "WordBugWordBug")]
        public void DuplicateConsecutivesRemoved(string configurationFile, string expectedClassName)
        {
            Assert.That(new RandomNamePuller(new TestConfigurationReader(configurationFile)).GetRandomClassName(null), Is.EqualTo(expectedClassName));
        }
    }
}