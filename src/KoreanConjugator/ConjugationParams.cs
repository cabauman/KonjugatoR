namespace KoreanConjugator
{
    /// <summary>
    /// Represents a set of params used to specify the desired conjugation form.
    /// </summary>
    public readonly struct ConjugationParams
    {
        /// <summary>
        /// Gets the world class.
        /// </summary>
        public WordClass WordClass { get; }

        /// <summary>
        /// Gets a value indicating whether the conjugation should use honorific form.
        /// </summary>
        public bool Honorific { get; }

        /// <summary>
        /// Gets the tense.
        /// </summary>
        public Tense Tense { get; }

        /// <summary>
        /// Gets the formality.
        /// </summary>
        public Formality Formality { get; }

        /// <summary>
        /// Gets the clause type.
        /// </summary>
        public ClauseType ClauseType { get; }
    }
}
