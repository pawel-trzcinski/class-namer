namespace ClassNamerEngine.Puller
{
    /// <summary>
    /// Interface for random name generator.
    /// </summary>
    public interface IRandomNamePuller
    {
        /// <summary>
        /// Generate random class name.
        /// </summary>
        /// <returns>Randomly generated name consisting of 1 to 4 parts.</returns>
        string GetRandomClassName();
    }
}
