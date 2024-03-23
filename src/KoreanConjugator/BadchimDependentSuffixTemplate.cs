namespace KoreanConjugator;

/// <summary>
/// Represents a template where the suffix variant depends on whether or not the preceding text ends with a badchim.
/// </summary>
public readonly ref struct BadchimDependentSuffixTemplate
{
    /// <summary>
    /// Gets the template text.
    /// </summary>
    public string TemplateText { get; init; }

    /// <summary>
    /// Gets the text of the word class(es) this suffix can be attached to.
    /// </summary>
    public ReadOnlySpan<char> WordClass { get; init; }

    /// <summary>
    /// Gets the portion of the template text that doesn't change.
    /// </summary>
    public ReadOnlySpan<char> StaticText { get; init; }

    /// <summary>
    /// Gets the text used when attaching this suffix to a word where the last syllable ends with a badchim.
    /// </summary>
    public ReadOnlySpan<char> BadchimConnector { get; init; }

    /// <summary>
    /// Gets the text used when attaching this suffix to a word where the last syllable doesn't end with a
    /// badchim, or if the badchim happens to be a 'ㄹ'.
    /// </summary>
    public ReadOnlySpan<char> BadchimlessConnector { get; init; }
}
