namespace KoreanConjugator;

/// <summary>
/// Represents a suffix template that starts with the 아/어 grammatical principal.
/// </summary>
public readonly ref struct AEuSuffixTemplate
{
    private readonly bool pastTense;

    /// <summary>
    /// Initializes a new instance of the <see cref="AEuSuffixTemplate"/> class.
    /// </summary>
    /// <param name="text">The template text.</param>
    /// <param name="staticText">The portion of the template text that doesn't change.</param>
    /// <param name="pastTense">A value indicating whether the suffix should be past tense.</param>
    public AEuSuffixTemplate(ReadOnlySpan<char> staticText, bool pastTense)
    {
        StaticText = staticText;
        this.pastTense = pastTense;
    }

    /// <summary>
    /// Gets the portion of the template text that doesn't change.
    /// </summary>
    public ReadOnlySpan<char> StaticText { get; }

    public string ChooseSuffixVariant(string precedingText)
    {
        string connector;
        if (precedingText.Equals("하"))
        {
            connector = pastTense ? "였" : "여";
        }
        else
        {
            connector = pastTense ? "었" : "어";

            int index = precedingText.Length - 1;
            while (index >= 0)
            {
                var medial = HangulUtil.Medial(precedingText[index]);
                if (medial != 'ᅳ')
                {
                    if (medial == 'ᅡ' || medial == 'ᅩ')
                    {
                        connector = pastTense ? "았" : "아";
                    }

                    break;
                }

                --index;
            }
        }

        return string.Concat(connector, StaticText);
    }
}

public readonly ref struct Hello
{
    public ReadOnlySpan<char> Abc { get; }
    public string A { get; }
    public object B { get; init; }
}
