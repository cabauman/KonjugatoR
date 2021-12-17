namespace KoreanConjugator;

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
}
