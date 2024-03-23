namespace KoreanConjugator;

/// <summary>
/// Represents a suffix template that starts with the 아/어 grammatical principal.
/// </summary>
public readonly ref struct AEuSuffixTemplate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AEuSuffixTemplate"/> class.
    /// </summary>
    /// <param name="templateText">The template text.</param>
    /// <param name="staticText">The portion of the template text that doesn't change.</param>
    /// <param name="isPastTense">A value indicating whether the suffix should be past tense.</param>
    public AEuSuffixTemplate(string templateText, string staticText, bool isPastTense)
    {
        TemplateText = templateText;
        StaticText = staticText;
        IsPastTense = isPastTense;
    }

    public string TemplateText { get; init; }
    public string StaticText { get; init; }
    public bool IsPastTense { get; init; }
}
