using System.Text;

namespace KoreanConjugator;

/// <summary>
/// Represents a utility that can conjugate Korean verbs and adjectives from dictionary form.
/// </summary>
public class Conjugator : IConjugator
{
    private readonly ISuffixTemplateParser suffixTemplateParser;
    private readonly ConjugationSuffixTemplateListProvider templateListProvider = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Conjugator"/> class.
    /// </summary>
    /// <param name="suffixTemplateParser">The implementation to use for parsing suffix templates.</param>
    public Conjugator(ISuffixTemplateParser suffixTemplateParser)
    {
        ArgumentNullException.ThrowIfNull(suffixTemplateParser);
        this.suffixTemplateParser = suffixTemplateParser;
    }

    /// <inheritdoc/>
    public ConjugationResult Conjugate(string verbStem, ConjugationParams conjugationParams)
    {
        ArgumentException.ThrowIfNullOrEmpty(verbStem);

        if (conjugationParams.Honorific)
        {
            var honorificStem = HangulUtil.GetSpecialHonorificForm(verbStem);
            verbStem = honorificStem ?? verbStem;
        }

        var suffixTemplateStrings = templateListProvider.GetSuffixTemplateStrings(verbStem, conjugationParams);
        var sanitizedVerbStem = ApplyVerbStemEdgeCaseLogic(verbStem, suffixTemplateStrings.First());
        var suffixes = GetSuffixes(sanitizedVerbStem, suffixTemplateStrings);
        var mutableVerbStem = ApplyIrregularVerbRules(sanitizedVerbStem, suffixes.First().First());
        var conjugatedForm = MergeSyllablesFromLeftToRight(mutableVerbStem, suffixes);
        var finalForm = ApplyConjugatedFormEdgeCaseLogic(conjugatedForm, conjugationParams.Honorific);

        return new ConjugationResult(finalForm, Array.Empty<string>());
    }

    private string[] GetSuffixes(string verbStem, string[] suffixTemplateStrings)
    {
        var suffixes = new string[suffixTemplateStrings.Length];
        suffixes[0] = GetSuffix(verbStem, suffixTemplateStrings[0]);

        for (int i = 1; i < suffixTemplateStrings.Length; ++i)
        {
            var preceding = suffixes[i - 1];
            suffixes[i] = GetSuffix(preceding, suffixTemplateStrings[i]);
        }

        return suffixes;
    }

    private static MutableVerbStem ApplyIrregularVerbRules(string verbStem, char firstSyllableOfFirstSuffix)
    {
        bool hasHiddenBadchim = false;
        var sb = new StringBuilder(verbStem);
        char lastSyllableOfVerbStem = verbStem.Last();
        string valueToAppend = string.Empty;

        if (HangulUtil.IsIrregular(verbStem))
        {
            if ('르'.Equals(lastSyllableOfVerbStem))
            {
                if ("아어았었".Any(value => value == firstSyllableOfFirstSuffix))
                {
                    // add ㄹ to syllable preceding 르.
                    int indexOfCharToReplace = verbStem.Length - 2;
                    char newSyllable = HangulUtil.SetFinal(verbStem[indexOfCharToReplace], 'ᆯ');
                    sb[indexOfCharToReplace] = newSyllable;
                }
            }
            else if (HangulUtil.BeginsWithVowel(firstSyllableOfFirstSuffix))
            {
                switch (HangulUtil.Final(lastSyllableOfVerbStem))
                {
                    case 'ᆺ':
                        // Drop ㅅ.
                        lastSyllableOfVerbStem = HangulUtil.DropFinal(lastSyllableOfVerbStem);
                        hasHiddenBadchim = true;
                        break;
                    case 'ᆮ':
                        // ㄷ → ㄹ.
                        lastSyllableOfVerbStem = HangulUtil.SetFinal(lastSyllableOfVerbStem, 'ᆯ');
                        break;
                    case 'ᆸ':
                        // drop ㅂ, add 우/오.
                        if ((verbStem.Equals("묻잡") || "돕곱".Any(x => x.Equals(lastSyllableOfVerbStem))) &&
                            "아어았었".Any(x => x.Equals(firstSyllableOfFirstSuffix)))
                        {
                            valueToAppend = "오";
                        }
                        else
                        {
                            valueToAppend = "우";
                        }

                        lastSyllableOfVerbStem = HangulUtil.DropFinal(lastSyllableOfVerbStem);
                        break;
                    case 'ᇂ':
                        // drop ㅎ.
                        lastSyllableOfVerbStem = HangulUtil.DropFinal(lastSyllableOfVerbStem);
                        if ("아어았었".Any(value => value == firstSyllableOfFirstSuffix))
                        {
                            lastSyllableOfVerbStem = HangulUtil.Contract(lastSyllableOfVerbStem, '이');
                        }

                        break;
                    default:
                        break;
                }
            }
            else if (HangulUtil.BeginsWithNieun(firstSyllableOfFirstSuffix))
            {
                switch (HangulUtil.Final(lastSyllableOfVerbStem))
                {
                    case 'ᆯ':
                    case 'ᇂ':
                        // drop ᆯ/ᇂ
                        lastSyllableOfVerbStem = HangulUtil.DropFinal(lastSyllableOfVerbStem);
                        break;
                    default:
                        break;
                }
            }
            else if (HangulUtil.BeginsWithSieut(firstSyllableOfFirstSuffix))
            {
                switch (HangulUtil.Final(lastSyllableOfVerbStem))
                {
                    case 'ᆯ':
                        // drop ㄹ.
                        lastSyllableOfVerbStem = HangulUtil.DropFinal(lastSyllableOfVerbStem);
                        break;
                    default:
                        break;
                }
            }
            else if (HangulUtil.IsComposableLetter(firstSyllableOfFirstSuffix))
            {
                if (firstSyllableOfFirstSuffix == 'ㄴ' || firstSyllableOfFirstSuffix == 'ㄹ' || firstSyllableOfFirstSuffix == 'ㅂ')
                {
                    switch (HangulUtil.Final(lastSyllableOfVerbStem))
                    {
                        case 'ᆯ':
                            // drop ㄹ.
                            lastSyllableOfVerbStem = HangulUtil.DropFinal(lastSyllableOfVerbStem);
                            break;
                        default:
                            break;
                    }
                }
                else if (firstSyllableOfFirstSuffix == 'ᄆ')
                {
                    switch (HangulUtil.Final(lastSyllableOfVerbStem))
                    {
                        case 'ᆯ':
                            // ㄹ → ㄻ. Should probably move this to the MergeSyllablesFromLeftToRight step.
                            lastSyllableOfVerbStem = HangulUtil.SetFinal(lastSyllableOfVerbStem, 'ᆱ');
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        sb[verbStem.Length - 1] = lastSyllableOfVerbStem;
        sb.Append(valueToAppend);

        return new MutableVerbStem(sb.ToString(), hasHiddenBadchim);
    }

    private string ApplyVerbStemEdgeCaseLogic(string verbStem, string suffixTemplateString)
    {
        var suffix = GetSuffix(verbStem, suffixTemplateString);
        if (verbStem.Equals("뵙") && HangulUtil.Initial(suffix.First()).Equals('ᄋ'))
        {
            verbStem = "뵈";
        }
        else if (verbStem.Equals("푸") && HangulUtil.Initial(suffix.First()).Equals('ᄋ'))
        {
            verbStem = "퍼";
        }

        return verbStem;
    }

    private static string ApplyConjugatedFormEdgeCaseLogic(string conjugatedForm, bool isHonorific)
    {
        if (isHonorific && conjugatedForm.Contains("셔요"))
        {
            conjugatedForm = conjugatedForm.Replace("셔요", "세요");
        }

        return conjugatedForm;
    }

    private static void Attach(StringBuilder sb, string suffix)
    {
        var lastSyllableOfSb = sb[^1];

        if (HangulUtil.HasFinal(lastSyllableOfSb))
        {
            sb.Append(suffix);
        }
        else
        {
            if (HangulUtil.IsModernCompatibilityLetter(suffix.First()))
            {
                var composableFinal = HangulUtil.ToComposableFinal(suffix.First());
                var final = HangulUtil.FinalToIndex(composableFinal);
                int initial = HangulUtil.IndexOfInitial(lastSyllableOfSb);
                int medial = HangulUtil.IndexOfMedial(lastSyllableOfSb);
                char result = HangulUtil.Construct(initial, medial, final);
                sb[^1] = result;
                if (suffix.Length > 1)
                {
                    sb.Append(suffix.AsSpan(1));
                }
            }
            else
            {
                if (HangulUtil.CanContract(lastSyllableOfSb, suffix.First()))
                {
                    // Apply vowel contraction.
                    char result = HangulUtil.Contract(lastSyllableOfSb, suffix.First());
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
        }
    }

    private static string MergeSyllablesFromLeftToRight(MutableVerbStem verbStem, string[] suffixes)
    {
        var sb = new StringBuilder(verbStem.Value);

        if (verbStem.HasHiddenBadchim)
        {
            sb.Append(suffixes[0]);
        }
        else
        {
            Attach(sb, suffixes[0]);
        }

        for (int i = 1; i < suffixes.Length; ++i)
        {
            var suffix = suffixes[i];
            Attach(sb, suffix);
        }

        return sb.ToString();
    }

    private string GetSuffix(string precedingText, string suffixTemplateString)
    {
        var suffixTemplate = suffixTemplateParser.Parse(suffixTemplateString);
        var suffixVariant = suffixTemplate.ChooseSuffixVariant(precedingText);

        return suffixVariant;
    }
}
