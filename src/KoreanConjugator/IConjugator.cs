namespace KoreanConjugator
{
    /// <summary>
    /// A contract for conjugating Korean verbs and adjectives.
    /// </summary>
    public interface IConjugator
    {
        /// <summary>
        /// Transforms the given verb stem into a specific grammatical form specified by the parms.
        /// </summary>
        /// <param name="verbStem">The portion of the verb without the '다' syllable at the end.</param>
        /// <param name="conjugationParams">The params used to specify the conjugated form.</param>
        /// <returns>A conjugation result.</returns>
        ConjugationResult Conjugate(string verbStem, ConjugationParams conjugationParams);

        /// <summary>
        /// Attaches a suffix to a verb stem.
        /// </summary>
        /// <param name="verbStem">A verb stem.</param>
        /// <param name="suffixTemplateString">A suffix template string.</param>
        /// <returns>A conjugation result.</returns>
        ConjugationResult AttachSuffixToVerb(string verbStem, string suffixTemplateString);

        /// <summary>
        /// Attaches a suffix to a noun.
        /// </summary>
        /// <param name="noun">A noun.</param>
        /// <param name="suffixTemplateString">The suffix template string.</param>
        /// <returns>A conjugation result.</returns>
        ConjugationResult AttachSuffixToNoun(string noun, string suffixTemplateString);
    }
}