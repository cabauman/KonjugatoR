using Microsoft.Extensions.ObjectPool;
using System.Text;
using System.Buffers;

namespace KoreanConjugator;

/// <summary>
/// Represents a utility that can conjugate Korean verbs and adjectives from dictionary form.
/// </summary>
public class Conjugator : IConjugator
{
    private static readonly StringBuilder sb = new(8);
    private static readonly ObjectPool<StringBuilder> sbPool;
    private readonly ISuffixTemplateParser suffixTemplateParser;
    private readonly ConjugationSuffixTemplateListProvider templateListProvider = new();
    private string[] arr = new string[4];

    static Conjugator()
    {
        var objectPoolProvider = new DefaultObjectPoolProvider();
        sbPool = objectPoolProvider.CreateStringBuilderPool();
    }

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
    public ConjugationResult Conjugate(string verbStem, in ConjugationParams conjugationParams)
    {
        if (string.IsNullOrEmpty(nameof(verbStem)))
        {
            throw new ArgumentException(verbStem);
        }

        if (conjugationParams.Honorific)
        {
            var honorificStem = HangulUtil.GetSpecialHonorificForm(verbStem);
            verbStem = honorificStem ?? verbStem;
        }

        templateListProvider.GetSuffixTemplateStrings(verbStem, conjugationParams, arr);
        var conjugatedForm = GetSuffixes(verbStem, arr);
        var finalForm = ApplyConjugatedFormEdgeCaseLogic(conjugatedForm, conjugationParams.Honorific);

        return new ConjugationResult(finalForm, Array.Empty<string>());
    }

    public string Conjugate2(string verbStem, in ConjugationParams conjugationParams)
    {
        if (string.IsNullOrEmpty(nameof(verbStem)))
        {
            throw new ArgumentException(verbStem);
        }

        if (conjugationParams.Honorific)
        {
            var honorificStem = HangulUtil.GetSpecialHonorificForm(verbStem);
            verbStem = honorificStem ?? verbStem;
        }

        //var arr = ArrayPool<string>.Shared.Rent(4);
        templateListProvider.GetSuffixTemplateStrings(verbStem, conjugationParams, arr);
        // TODO: ApplyVerbStemEdgeCaseLogic calls GetSuffix which is an identical call to what happens in GetSuffixes.
        var sanitizedVerbStem = ApplyVerbStemEdgeCaseLogic(verbStem, arr[0]);
        //var suffixes = GetSuffixes(sanitizedVerbStem, arr);
        //var mutableVerbStem = ApplyIrregularVerbRules(sanitizedVerbStem, suffixes[0][0]);
        //var conjugatedForm = MergeSyllablesFromLeftToRight(mutableVerbStem, suffixes);
        //var finalForm = ApplyConjugatedFormEdgeCaseLogic(conjugatedForm, conjugationParams.Honorific);
        //ArrayPool<string>.Shared.Return(arr);

        return sanitizedVerbStem;
    }

    private StringBuilder GetSuffixes(string verbStem, string[] suffixTemplateStrings)
    {
        var firstSuffix = SuffixTemplateParser2.Parse(suffixTemplateStrings[0], verbStem);
        var sanitizedVerbStem = ApplyVerbStemEdgeCaseLogic(verbStem, arr[0]);
        var firstSyllableOfFirstSuffix = firstSuffix.ChooseSuffixVariant(verbStem);
        var mutableVerbStem = ApplyIrregularVerbRules(sanitizedVerbStem, firstSyllableOfFirstSuffix);

        if (mutableVerbStem.HasHiddenBadchim)
        {
            sb.Append(firstSuffix.ChooseSuffixVariant(verbStem));
            sb.Append(firstSuffix.StaticText);
        }
        else
        {
            Span<char> span = stackalloc char[firstSuffix.StaticText.Length + 1];
            var suffix = firstSuffix.ChooseSuffixVariant(verbStem, span);
            Attach(sb, suffix);
        }

        var preceding = firstSuffix;
        for (int i = 1; i < suffixTemplateStrings.Length; ++i)
        {
            if (suffixTemplateStrings[i] is null)
            {
                break;
            }
            //var preceding = suffixTemplateStrings[i - 1];
            //var suffix = GetSuffix(preceding, suffixTemplateStrings[i]);
            var suffixTemplate = SuffixTemplateParser2.Parse(suffixTemplateStrings[i], preceding);
            var suffix = suffixTemplate.ChooseSuffixVariant(verbStem);
            Attach(sb, suffix);
            //previous =
        }

        return sb;
    }

    public static MutableVerbStem ApplyIrregularVerbRules(string verbStem, char firstSyllableOfFirstSuffix)
    {
        bool hasHiddenBadchim = false;
        //var sb = sbPool.Get();
        sb.Clear();
        sb.Append(verbStem);
        char lastSyllableOfVerbStem = verbStem[^1];
        string valueToAppend = string.Empty;

        //BadchimDependentSuffixTemplate suffix1 = default;
        //suffix1.ChooseSuffixVariant("");

        if (HangulUtil.IsIrregular(verbStem))
        {
            if ('르'.Equals(lastSyllableOfVerbStem))
            {
                if ("아어았었".Contains(firstSyllableOfFirstSuffix))
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
                        if ((verbStem.Equals("묻잡") || "돕곱".Contains(lastSyllableOfVerbStem)) &&
                            "아어았었".Contains(firstSyllableOfFirstSuffix))
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
                        if ("아어았었".Contains(firstSyllableOfFirstSuffix))
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
            else if (HangulUtil.IsModernCompatibilityLetter(firstSyllableOfFirstSuffix))
            {
                if (firstSyllableOfFirstSuffix is 'ㄴ' or 'ㄹ' or 'ㅂ')
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
                else if (firstSyllableOfFirstSuffix == 'ㅁ')
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

        var stem = new MutableVerbStem(sb, hasHiddenBadchim);
        //sbPool.Return(sb);
        return stem;
    }

    public string ApplyVerbStemEdgeCaseLogic(string verbStem, string suffixTemplateString)
    {
        var suffix = GetSuffix(verbStem, suffixTemplateString);
        if (verbStem.Equals("뵙") && HangulUtil.Initial(suffix[0]).Equals('ᄋ'))
        {
            verbStem = "뵈";
        }
        else if (verbStem.Equals("푸") && HangulUtil.Initial(suffix[0]).Equals('ᄋ'))
        {
            verbStem = "퍼";
        }

        return verbStem;
    }

    private static string ApplyConjugatedFormEdgeCaseLogic(StringBuilder conjugatedForm, bool isHonorific)
    {
        //var sb = sbPool.Get();
        conjugatedForm.Replace("셔요", "세요");
        //if (isHonorific && conjugatedForm.Contains("셔요"))
        //{
        //    conjugatedForm = conjugatedForm.Replace("셔요", "세요");
        //}

        //sbPool.Return(sb);
        var result = conjugatedForm.ToString();
        sbPool.Return(conjugatedForm);
        return result;
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
            if (HangulUtil.IsModernCompatibilityLetter(suffix[0]))
            {
                var composableFinal = HangulUtil.ToComposableFinal(suffix[0]);
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
                if (HangulUtil.CanContract(lastSyllableOfSb, suffix[0]))
                {
                    // Apply vowel contraction.
                    char result = HangulUtil.Contract(lastSyllableOfSb, suffix[0]);
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

    // TODO: Move to SuffixTemplate???
    private static void Attach(StringBuilder sb, Span<char> suffix)
    {
        var lastSyllableOfSb = sb[^1];

        if (HangulUtil.HasFinal(lastSyllableOfSb))
        {
            sb.Append(suffix);
        }
        else
        {
            if (HangulUtil.IsModernCompatibilityLetter(suffix[0]))
            {
                var composableFinal = HangulUtil.ToComposableFinal(suffix[0]);
                var final = HangulUtil.FinalToIndex(composableFinal);
                int initial = HangulUtil.IndexOfInitial(lastSyllableOfSb);
                int medial = HangulUtil.IndexOfMedial(lastSyllableOfSb);
                char result = HangulUtil.Construct(initial, medial, final);
                sb[^1] = result;
                if (suffix.Length > 1)
                {
                    sb.Append(suffix[1..]);
                }
            }
            else
            {
                if (HangulUtil.CanContract(lastSyllableOfSb, suffix.ChooseSuffixVariant(preceding)))
                {
                    // Apply vowel contraction.
                    char result = HangulUtil.Contract(lastSyllableOfSb, suffix.ChooseSuffixVariant(preceding));
                    sb[^1] = result;
                    sb.Append(suffix[1..]);
                }
                else
                {
                    sb.Append(suffix);
                }
            }
        }
    }

    //public static StringBuilder MergeSyllablesFromLeftToRight(MutableVerbStem verbStem, SuffixTemplate[] suffixes)
    //{
    //    //var sb = sbPool.Get();
    //    var sb = verbStem.Value;
    //    //var stem = verbStem.Value.ToString();
    //    //sb.Clear();
    //    //sb.Append(stem);

    //    if (verbStem.HasHiddenBadchim)
    //    {
    //        sb.Append(suffixes[0].ChooseSuffixVariant(""));
    //        sb.Append(suffixes[0].StaticText);
    //    }
    //    else
    //    {
    //        Attach(sb, suffixes[0]);
    //    }

    //    for (int i = 1; i < suffixes.Length; ++i)
    //    {
    //        //if (suffixes[i] is null)
    //        //{
    //        //    break;
    //        //}
    //        var suffix = suffixes[i];
    //        Attach(sb, suffix);
    //    }

    //    //var result = sb.ToString();
    //    //sbPool.Return(sb);
    //    return sb;
    //}

    public static StringBuilder MergeSyllablesFromLeftToRight(MutableVerbStem verbStem, string[] suffixes)
    {
        //var sb = sbPool.Get();
        var sb = verbStem.Value;
        //var stem = verbStem.Value.ToString();
        //sb.Clear();
        //sb.Append(stem);

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
            if (suffixes[i] is null)
            {
                break;
            }
            var suffix = suffixes[i];
            Attach(sb, suffix);
        }

        //var result = sb.ToString();
        //sbPool.Return(sb);
        return sb;
    }

    public string GetSuffix(string precedingText, string suffixTemplateString)
    {
        return suffixTemplateParser.Parse(suffixTemplateString, precedingText);
    }
}
