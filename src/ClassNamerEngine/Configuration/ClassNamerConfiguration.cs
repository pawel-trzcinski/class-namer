using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using ClassNamerEngine.Puller;
using Newtonsoft.Json;

namespace ClassNamerEngine.Configuration
{
    /// <summary>
    /// Main configuration class for all ClassNamer bussiness logic.
    /// </summary>
    public class ClassNamerConfiguration
    {
        private static readonly char[] splitCharacters = new char[] { ' ', '\t', '\r', '\n', ',', '.', ';', '-', ':' };

        private static readonly string lowerAlphabet = "qwertyuioplkjhgfdsazxcvbnm".ToLowerInvariant();
        private static readonly string upperAlphabet = lowerAlphabet.ToUpperInvariant();
        private static readonly HashSet<char> allowedCharacters = new HashSet<char>(lowerAlphabet + upperAlphabet);

        /// <summary>
        /// Gets collection of possible <see cref="NamePart"/> combinations along with weight associated to it.
        /// </summary>
        public IReadOnlyCollection<Combination> Combinations { get; }

        /// <summary>
        /// Gets dictionary of available words to pull from. The words are normalized (first letter is big, the rest are small).
        /// </summary>
        public IReadOnlyDictionary<NamePart, IReadOnlyCollection<string>> WordsSet { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassNamerConfiguration"/> class.
        /// </summary>
        /// <param name="combinations">Collection of possible <see cref="NamePart"/> combinations along with weight associated to it.</param>
        /// <param name="wordsSet">Dictionary of available words to pull from.</param>
        [JsonConstructor]
        public ClassNamerConfiguration(Combination[] combinations, Dictionary<NamePart, string[]> wordsSet)
        {
            this.Combinations = new ReadOnlyCollection<Combination>(combinations);
            this.WordsSet = wordsSet.ToDictionary
                (
                    k => k.Key,
                    v => (IReadOnlyCollection<string>)new ReadOnlyCollection<string>(v.Value.SelectMany(SplitIntoWords).Select(NormalizeWord).ToArray())
                );

            ValidateConfiguration();
        }

        private static string[] SplitIntoWords(string word)
        {
            return word.Split(splitCharacters).Where(p => !String.IsNullOrWhiteSpace(p)).Select(p => p.Trim()).ToArray();
        }

        private static string NormalizeWord(string word)
        {
            return word[0].ToString(CultureInfo.InvariantCulture).ToUpperInvariant() + word.Substring(1).ToLowerInvariant();
        }

        private void ValidateConfiguration()
        {
            foreach (string word in this.WordsSet.Values.SelectMany(p => p))
            {
                foreach (char wordCharacter in word)
                {
                    if (!allowedCharacters.Contains(wordCharacter))
                    {
                        throw new ArgumentException($"Invalid character '{wordCharacter}' in word {word}");
                    }
                }
            }
        }
    }
}