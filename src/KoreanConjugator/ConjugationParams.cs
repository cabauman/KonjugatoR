namespace KoreanConjugator;

/// <summary>
/// Represents a set of params used to specify the desired conjugation form.
/// </summary>
public readonly struct ConjugationParams
{
    /// <summary>
    /// Gets or sets the world class.
    /// </summary>
    public WordClass WordClass { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the conjugation should use honorific form.
    /// </summary>
    public bool Honorific { get; init; }

    /// <summary>
    /// Gets or sets the tense.
    /// </summary>
    public Tense Tense { get; init; }

    /// <summary>
    /// Gets or sets the formality.
    /// </summary>
    public Formality Formality { get; init; }

    /// <summary>
    /// Gets or sets the clause type.
    /// </summary>
    public ClauseType ClauseType { get; init; }
}
