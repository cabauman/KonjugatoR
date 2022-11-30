using System.Text;

namespace KoreanConjugator;

/// <summary>
/// Represents a utility that can attaches suffixes to Korean text.
/// </summary>
public abstract class SuffixAttacher
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SuffixAttacher"/> class.
    /// </summary>
    public SuffixAttacher()
    {
    }

    /// <summary>
    /// Attaches a suffix to a verb stem.
    /// </summary>
    /// <param name="verbStem">A verb stem.</param>
    /// <param name="suffixTemplateString">A suffix template string.</param>
    /// <returns>A conjugation result.</returns>
    public ConjugationResult AttachSuffixToVerb(string verbStem, string suffixTemplateString)
    {
        ArgumentException.ThrowIfNullOrEmpty(verbStem);
        ArgumentException.ThrowIfNullOrEmpty(suffixTemplateString);

        // verbStem = ApplyVerbStemEdgeCaseLogic(verbStem, suffixTemplateString);
        // var suffixString = GetSuffix(verbStem, suffixTemplateString);
        // var mutableVerbStem = ApplyIrregularVerbRules(verbStem, suffixString.First());
        // var conjugatedForm = Attach(mutableVerbStem, suffixString);
        // return new ConjugationResult(conjugatedForm, null);
        throw new NotImplementedException();
    }

    /// <summary>
    /// Attaches a suffix to a noun.
    /// </summary>
    /// <param name="noun">A noun.</param>
    /// <param name="suffixTemplateString">The suffix template string.</param>
    /// <returns>A conjugation result.</returns>
    public ConjugationResult AttachSuffixToNoun(string noun, string suffixTemplateString)
    {
        ArgumentException.ThrowIfNullOrEmpty(noun);
        ArgumentException.ThrowIfNullOrEmpty(suffixTemplateString);

        // string suffixString = GetSuffix(noun, suffixTemplateString);
        // var result = AttachToNoun(noun, suffixString);
        // return new ConjugationResult(result, null);
        throw new NotImplementedException();
    }

    private string AttachToNoun(string text, string suffix)
    {
        var sb = new StringBuilder();
        sb.Append(text);

        if (HangulUtil.HasFinal(text.Last()))
        {
            // Attach.
            sb.Append(suffix);
        }
        else
        {
            if (HangulUtil.CanContract(text.Last(), suffix.First()))
            {
                // Apply vowel contraction.
                char result = HangulUtil.Contract(text.Last(), suffix.First());
                sb[^1] = result;
                if (suffix.Length > 1)
                {
                    sb.Append(suffix.AsSpan(1));
                }
            }
            else
            {
                sb.Append(suffix);
            }
        }

        return sb.ToString();
    }

    private string Attach(MutableVerbStem verbStem, string suffix)
    {
        // var sb = new StringBuilder(verbStem.Value);
        // if (verbStem.HasHiddenBadchim)
        // {
        //     sb.Append(suffix);
        // }
        // else
        // {
        //     Attach(sb, suffix);
        // }
        // return sb.ToString();
        throw new NotImplementedException();
    }
}
