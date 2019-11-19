namespace ClassNamerEngine.Puller
{
    /// <summary>
    /// Name parts that random class name will consist of.
    /// </summary>
    public enum NamePart
    {
        /// <summary>
        /// Default null pattern implementation.
        /// </summary>
        None = 0,

        /// <summary>
        /// First verb in the name. It can give more meaning to <see cref="Description"/>.
        /// </summary>
        /// <example>
        /// <b>Custom</b>CompleteThreadInvoker
        /// </example>
        Adjective,

        /// <summary>
        /// Description of the <see cref="Subject"/>
        /// </summary>
        /// <example>
        /// Custom<b>Complete</b>ThreadInvoker
        /// </example>
        Description,

        /// <summary>
        /// Main subject of the class name
        /// </summary>
        /// <example>
        /// CustomComplete<b>Thread</b>Invoker
        /// </example>
        Subject,

        /// <summary>
        /// Role or main action of the <see cref="Subject"/>
        /// </summary>
        /// <example>
        /// CustomCompleteThread<b>Invoker</b>
        /// </example>
        Role
    }
}