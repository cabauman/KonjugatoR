namespace KoreanConjugator;

/// <summary>
/// Represents a template where the suffix variant depends on whether or not the preceding text ends with a badchim.
/// </summary>
public readonly ref struct BadchimDependentSuffixTemplate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BadchimDependentSuffixTemplate"/> class.
    /// </summary>
    /// <param name="text">The template text.</param>
    /// <param name="wordClass">The word class(es).</param>
    /// <param name="badchimConnector">The badchim connector.</param>
    /// <param name="badchimlessConnector">The badchimless connector.</param>
    /// <param name="staticText">The portion of the template text that doesn't change.</param>
    public BadchimDependentSuffixTemplate(
        ReadOnlySpan<char> wordClass,
        ReadOnlySpan<char> badchimConnector,
        ReadOnlySpan<char> badchimlessConnector,
        ReadOnlySpan<char> staticText)
    {
        WordClass = wordClass;
        BadchimConnector = badchimConnector;
        BadchimlessConnector = badchimlessConnector;
        StaticText = staticText;
    }

    /// <summary>
    /// Gets the text of the word class(es) this suffix can be attached to.
    /// </summary>
    public ReadOnlySpan<char> WordClass { get; }

    /// <summary>
    /// Gets the text used when attaching this suffix to a word where the last syllable ends with a badchim.
    /// </summary>
    public ReadOnlySpan<char> BadchimConnector { get; }

    /// <summary>
    /// Gets the text used when attaching this suffix to a word where the last syllable doesn't end with a
    /// badchim, or if the badchim happens to be a 'ㄹ'.
    /// </summary>
    public ReadOnlySpan<char> BadchimlessConnector { get; }

    /// <summary>
    /// Gets the portion of the template text that doesn't change.
    /// </summary>
    public ReadOnlySpan<char> StaticText { get; }

    /// <inheritdoc/>
    public string ChooseSuffixVariant(string precedingText)
    {
        ReadOnlySpan<char> connector = "";
        if (BadchimConnector == string.Empty)
        {
            // Doesn't depend on a badchim
            // No modifications
        }
        else
        {
            // TODO: See about reducing this to one statement.
            if (HangulUtil.Final(precedingText[^1]) != 'ᆯ' && HangulUtil.HasFinal(precedingText[^1]))
            {
                // not == ㄹ
                // Choose badchim connector
                connector = BadchimConnector;
            }
            else
            {
                // Choose badchimless connector (it will be equal to string.Empty if none)
                connector = BadchimlessConnector;
            }
        }

        return string.Concat(connector, StaticText);
    }
}
