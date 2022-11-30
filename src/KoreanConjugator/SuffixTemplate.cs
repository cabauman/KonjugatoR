using System;
using System.Linq;

namespace KoreanConjugator;

/// <summary>
/// Represents a template where the resulting suffix depends on a grammatical principle and the preceding text.
/// </summary>
public readonly struct SuffixTemplate
{
    private readonly bool pastTense;

    /// <summary>
    /// Initializes a new instance of the <see cref="SuffixTemplate"/> class.
    /// </summary>
    /// <param name="text">The template text.</param>
    /// <param name="staticText">The portion of the template text that doesn't change.</param>
    public SuffixTemplate(string text, ReadOnlySpan<char> staticText)
    {
        Text = text;
        StaticText = staticText;
    }

    /// <summary>
    /// Gets the template text.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Gets the text of the word class(es) this suffix can be attached to.
    /// </summary>
    public ReadOnlySpan<char> WordClass { get; }

    /// <summary>
    /// Gets the text used when attaching this suffix to a word where the last syllable ends with a badchim.
    /// </summary>
    public ReadOnlySpan<char> FirstSyllableOption1 { get; }

    /// <summary>
    /// Gets the text used when attaching this suffix to a word where the last syllable doesn't end with a
    /// badchim, or if the badchim happens to be a 'ㄹ'.
    /// </summary>
    public ReadOnlySpan<char> FirstSyllableOption2 { get; }

    /// <summary>
    /// Gets the portion of the template text that doesn't change.
    /// </summary>
    public ReadOnlyMemory<char> StaticText { get; }

    /// <summary>
    /// Gets the suffix text based off of the template and preceding text.
    /// </summary>
    /// <param name="precedingText">The text that the suffix will be attached to.</param>
    /// <returns>The suffix text.</returns>
    public char ChooseSuffixVariant(string precedingText)
    {
        if (FirstSyllableOption1[0] is '아' or '았')
        {
            return ChooseAEuSuffixVariant(precedingText);
        }
        else
        {
            return ChooseBadchimDependentSuffixVariant(precedingText);
        }
    }

    private char ChooseAEuSuffixVariant(string precedingText)
    {
        char connector;
        if (precedingText.Equals("하"))
        {
            connector = pastTense ? '였' : '여';
        }
        else
        {
            connector = pastTense ? '었' : '어';

            int index = precedingText.Length - 1;
            while (index >= 0)
            {
                var medial = HangulUtil.Medial(precedingText[index]);
                if (medial != 'ᅳ')
                {
                    if (medial == 'ᅡ' || medial == 'ᅩ')
                    {
                        connector = pastTense ? '았' : '아';
                    }

                    break;
                }

                --index;
            }
        }

        return connector;
    }

    private char ChooseBadchimDependentSuffixVariant(string precedingText)
    {
        var badchimlessConnector = FirstSyllableOption1;
        var badchimConnector = FirstSyllableOption2;

        char connector = default;
        if (badchimConnector == string.Empty)
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
                connector = badchimConnector[0];
            }
            else
            {
                // Choose badchimless connector (it will be equal to string.Empty if none)
                connector = badchimlessConnector[0];
            }
        }

        return connector;
    }
}
