using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ClassNamerEngine.Puller
{
    /// <summary>
    /// Template for class name generation.
    /// </summary>
    public class Combination
    {
        /// <summary>
        /// Gets <see cref="NamePart"/> elements of wihch class name will consist of.
        /// </summary>
        public IReadOnlyCollection<NamePart> Parts { get; }

        /// <summary>
        /// Gets number greater or equal to 0 and lesser or equal to 100 that is used to calculate <see cref="Combination"/> chance of being created.
        /// </summary>
        public int Weight { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Combination"/> class.
        /// </summary>
        /// <param name="parts"><see cref="NamePart"/> elements of wihch class name will consist of.</param>
        /// <param name="weight">Number greater or equal to 1 and lesser or equal to 100 that is used to calculate <see cref="Combination"/> chance of being created.</param>
        [JsonConstructor]
        public Combination(NamePart[] parts, int weight)
        {
            if (parts == null || parts.Length == 0)
            {
                throw new ArgumentNullException(nameof(parts));
            }

            if (weight < 1 || weight > 100)
            {
                throw new ArgumentException($"{weight} must be greater or equal to 1 and lesser or equal to 100", nameof(weight));
            }

            Parts = new ReadOnlyCollection<NamePart>(parts);
            Weight = weight;
        }
    }
}