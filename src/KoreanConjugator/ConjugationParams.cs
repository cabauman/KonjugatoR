namespace KoreanConjugator
{
    public struct ConjugationParams
    {
        public WordClass WordClass { get; }

        public bool Honorific { get; }

        public Tense Tense { get; }

        public Formality Formality { get; }

        public ClauseType ClauseType { get; }
    }
}
