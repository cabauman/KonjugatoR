namespace KoreanConjugator;

/// <summary>
/// Represents a suffix template that starts with the 아/어 grammatical principal.
/// </summary>
public readonly ref struct AEuSuffixTemplate
{
    /// <summary>
    /// Gets the template text.
    /// </summary>
    public string TemplateText { get; init; }

    /// <summary>
    /// Gets the portion of the template text that doesn't change.
    /// </summary>
    public ReadOnlySpan<char> StaticText { get; init; }

    /// <summary>
    /// Gets a value indicating whether the suffix should be past tense.
    /// </summary>
    public bool IsPastTense { get; init; }
}
