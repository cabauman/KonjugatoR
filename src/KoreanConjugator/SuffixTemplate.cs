using System;
using System.Linq;
using System.Text;

namespace KoreanConjugator;

/// <summary>
/// Represents a template where the resulting suffix depends on a grammatical principle and the preceding text.
/// </summary>
public readonly ref struct SuffixTemplate
{
    public readonly bool pastTense;

    /// <summary>
    /// Initializes a new instance of the <see cref="SuffixTemplate"/> class.
    /// </summary>
    /// <param name="text">The template text.</param>
    /// <param name="staticText">The portion of the template text that doesn't change.</param>
    public SuffixTemplate(
        string text,
        ReadOnlySpan<char> wordClass,
        ReadOnlySpan<char> firstSyllableOption1,
        ReadOnlySpan<char> firstSyllableOption2,
        ReadOnlySpan<char> staticText)
    {
        Text = text;
        WordClass = wordClass;
        FirstSyllableOption1 = firstSyllableOption1;
        FirstSyllableOption2 = firstSyllableOption2;
        StaticText = staticText;
        pastTense = firstSyllableOption1.SequenceEqual("았");
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
    public ReadOnlySpan<char> StaticText { get; }

    /// <summary>
    /// Gets the suffix text based off of the template and preceding text.
    /// </summary>
    /// <param name="precedingText">The text that the suffix will be attached to.</param>
    /// <returns>The suffix text.</returns>
    public void ChooseSuffixVariant(string precedingText, Span<char> buffer)
    {
        if (FirstSyllableOption1.Length == 1 && FirstSyllableOption1[0] is '아' or '았')
        {
            ChooseAEuSuffixVariant(precedingText);
        }
        else
        {
            ChooseBadchimDependentSuffixVariant(precedingText, buffer);
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

    private void ChooseBadchimDependentSuffixVariant(string precedingText, Span<char> buffer)
    {
        var badchimlessConnector = FirstSyllableOption1;
        var badchimConnector = FirstSyllableOption2;

        if (badchimlessConnector.Length == 0 && badchimConnector.Length == 0)
        {
            return StaticText;
        }

        char connector = default;
        // TODO: See about reducing this to one statement.
        if (HangulUtil.Final(precedingText[^1]) is not 'ᆯ' and not '\0')
        {
            // not == ㄹ
            // Choose badchim connector
            connector = badchimConnector[0];
        }
        else
        {
            if (badchimlessConnector.Length == 1)
            {
                // Choose badchimless connector (it will be equal to string.Empty if none)
                connector = badchimlessConnector[0];
            }
        }

        var sb = new StringBuilder();
        //Span<char> x = stackalloc char[StaticText.Length + 1];
        //x[0] = connector;
        //StaticText.CopyTo(x[1..]);
        //sb.Append(x);

        //sb.Append(connector);
        //sb.Append(StaticText);

        buffer[0] = connector;
        StaticText.CopyTo(buffer[1..]);
    }
}
