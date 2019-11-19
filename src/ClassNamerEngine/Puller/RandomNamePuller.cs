using System;
using System.Collections.Generic;
using System.Linq;
using ClassNamerEngine.Configuration;
using log4net;

namespace ClassNamerEngine.Puller
{
    /// <summary>
    /// Default implementation of <see cref="IRandomNamePuller"/>.
    /// </summary>
    public class RandomNamePuller : IRandomNamePuller
    {
        private readonly Combination[] pullingArray;
        private readonly int pullingRange;
        private readonly Random pullingRandom = new Random();

        private readonly Dictionary<NamePart, string[]> wordsSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomNamePuller"/> class.
        /// </summary>
        /// <param name="configurationReader"><see cref="IConfigurationReader"/> instance.</param>
        public RandomNamePuller(IConfigurationReader configurationReader)
        {
            if (configurationReader == null)
            {
                throw new ArgumentNullException(nameof(configurationReader));
            }

            ClassNamerConfiguration configuration = configurationReader.ReadConfiguration();

            this.pullingArray = GeneratePullingArray(configuration);
            this.pullingRange = this.pullingArray.Length;

            this.wordsSet = configuration.WordsSet.ToDictionary(k => k.Key, v => v.Value.ToArray());
        }

        /// <summary>
        /// Generate random class name consisting of 1 to 4 parts. The parts are described in <see cref="NamePart"/>.
        /// </summary>
        /// <returns>Randomly generated class name.</returns>
        public string GetRandomClassName()
        {
            Combination combination = pullingArray[this.pullingRandom.Next(pullingRange)];

            List<string> result = new List<string>(combination.Parts.Count);
            foreach (NamePart namePart in combination.Parts)
            {
                if (!wordsSet.TryGetValue(namePart, out string[] words))
                {
                    throw new InvalidOperationException($"Unable to get words set for NamePart {namePart}");
                }

                result.Add(words[pullingRandom.Next(words.Length)]);
            }

            return String.Concat(RemoveConsecutiveDuplicates(result));
        }

        private static List<string> RemoveConsecutiveDuplicates(List<string> pulledWords)
        {
            List<string> result = new List<string>(pulledWords.Count);

            foreach (string pulledWord in pulledWords)
            {
                if (String.Equals(pulledWord, result.LastOrDefault(), StringComparison.Ordinal))
                {
                    continue;
                }

                result.Add(pulledWord);
            }

            return result;
        }

        private static Combination[] GeneratePullingArray(ClassNamerConfiguration classNamerConfiguration)
        {
            List<Combination> result = new List<Combination>(classNamerConfiguration.Combinations.Sum(c => c.Weight));
            foreach (Combination combination in classNamerConfiguration.Combinations)
            {
                for (int i = 0; i < combination.Weight; i++)
                {
                    result.Add(combination);
                }
            }

            return result.ToArray();
        }
    }
}