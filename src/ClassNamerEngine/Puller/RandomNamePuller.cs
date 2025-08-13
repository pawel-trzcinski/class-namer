using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClassNamerEngine.Configuration;

namespace ClassNamerEngine.Puller
{
    /// <summary>
    /// Default implementation of <see cref="IRandomNamePuller"/>.
    /// </summary>
    public class RandomNamePuller : IRandomNamePuller
    {
        private readonly Combination[] _pullingArray;
        private readonly int _pullingRange;
        private readonly Random _pullingRandom = new Random();

        private readonly Dictionary<NamePart, string[]> _wordsSet;
        
        private static readonly Regex _rxWhitespace = new Regex(@"\s+");

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

            _pullingArray = GeneratePullingArray(configuration);
            _pullingRange = _pullingArray.Length;

            _wordsSet = configuration.WordsSet.ToDictionary(k => k.Key, v => v.Value.ToArray());
        }

        /// <summary>
        /// Generate random class name consisting of 1 to 4 parts. The parts are described in <see cref="NamePart"/>.
        /// </summary>
        /// <returns>Randomly generated class name.</returns>
        public string GetRandomClassName(string required)
        {
            Combination combination = _pullingArray[_pullingRandom.Next(_pullingRange)];

            List<string> result = new List<string>(combination.Parts.Count);
            foreach (NamePart namePart in combination.Parts)
            {
                if (!_wordsSet.TryGetValue(namePart, out string[] words))
                {
                    throw new InvalidOperationException($"Unable to get words set for NamePart {namePart}");
                }

                result.Add(words[_pullingRandom.Next(words.Length)]);
            }

            if (!String.IsNullOrWhiteSpace(required))
            {
                InsertWordIntoRandomPlace(required, result);
            }
            
            return String.Concat(RemoveConsecutiveDuplicates(result));
        }

        private static void InsertWordIntoRandomPlace(string word, List<string> words)
        {
            // Sanitize Word - remove all whitespace chars
            word = _rxWhitespace.Replace(word, String.Empty);
            
            // Insert anywhere
            words.Insert(Random.Shared.Next(0, words.Count + 1), word);
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