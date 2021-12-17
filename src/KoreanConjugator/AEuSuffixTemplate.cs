namespace KoreanConjugator;

/// <summary>
/// Represents a suffix template that starts with the 아/어 grammatical principal.
/// </summary>
public class AEuSuffixTemplate : SuffixTemplate
{
    private readonly bool pastTense;

    /// <summary>
    /// Initializes a new instance of the <see cref="AEuSuffixTemplate"/> class.
    /// </summary>
    /// <param name="text">The template text.</param>
    /// <param name="staticText">The portion of the template text that doesn't change.</param>
    public AEuSuffixTemplate(string text, string staticText, bool pastTense)
        : base(text, staticText)
    {
        this.pastTense = pastTense;
    }

    /// <inheritdoc/>
    public override string ChooseSuffixVariant(string precedingText)
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
