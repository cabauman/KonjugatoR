using System.Text;

namespace KoreanConjugator;

/// <summary>
/// Represents a utility that can conjugate Korean verbs and adjectives from dictionary form.
/// </summary>
public class Conjugator : IConjugator
{
    private static readonly StringBuilder sb = new(8);
    private readonly string[] arr = new string[4];
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
    public string MemoryTest(string verbStem, ConjugationParams conjugationParams)
    {
        ArgumentException.ThrowIfNullOrEmpty(verbStem);

        if (conjugationParams.Honorific)
        {
            var honorificStem = HangulUtil.GetSpecialHonorificForm(verbStem);
            verbStem = honorificStem ?? verbStem;
        }

        var suffixTemplateStrings = templateListProvider.GetSuffixTemplateStrings(verbStem, conjugationParams, arr);
        // 11.72 KB
        // pass shared array into GetSuffixTemplateStrings: 0 KB
        var sanitizedVerbStem = ApplyVerbStemEdgeCaseLogic(verbStem, suffixTemplateStrings[0]);
        // 11.72 KB (+0 KB)
        // pass shared array into GetSuffixTemplateStrings: 0 KB (+0 KB)
        //long startMemory = GC.GetTotalMemory(false);
        var suffixes = GetSuffixes(sanitizedVerbStem, suffixTemplateStrings);
        // 92.19 (+80.47 KB)
        // pass shared array into GetSuffixTemplateStrings: 82.81 KB (+82.81 KB)
        // don't create new array in GetSuffixes: 77.34 KB (+77.34 KB)
        //long endMemory = GC.GetTotalMemory(false);
        //long allocatedMemory = endMemory - startMemory;
        //Console.WriteLine($"Memory allocated within Conjugate method: {allocatedMemory} bytes");
        //var mutableVerbStem = ApplyIrregularVerbRules(sanitizedVerbStem, suffixes[0][0]);
        // 108.59 KB (+16.4 KB)
        // shared StringBuilder: 98.44 KB (+6.25 KB)
        // MutableVerbStem to struct and avoid sb.ToString too early: 92.19 KB (+0 KB)
        //var conjugatedForm = MergeSyllablesFromLeftToRight(mutableVerbStem, suffixes);
        // 121.88 KB (+13.29 KB)
        // shared StringBuilder: 101.56 KB (+3.12 KB)
        // MutableVerbStem to struct and avoid sb.ToString too early: 92.19 KB (+0 KB)
        //var finalForm = ApplyConjugatedFormEdgeCaseLogic(conjugatedForm, conjugationParams.Honorific);
        // MutableVerbStem to struct and avoid sb.ToString too early: 95.31 KB (+3.12 KB)

        return "";
    }

    /// <inheritdoc/>
    public ConjugationResult Conjugate(string verbStem, ConjugationParams conjugationParams)
    {
        ArgumentException.ThrowIfNullOrEmpty(verbStem);

        if (verbStem.EndsWith('다'))
        {
            verbStem = verbStem[..^1];
        }

        if (conjugationParams.Honorific)
        {
            var honorificStem = HangulUtil.GetSpecialHonorificForm(verbStem);
            verbStem = honorificStem ?? verbStem;
        }

        var suffixTemplateStrings = templateListProvider.GetSuffixTemplateStrings(verbStem, conjugationParams, arr);
        var sanitizedVerbStem = ApplyVerbStemEdgeCaseLogic(verbStem, suffixTemplateStrings[0]);
        var suffixes = GetSuffixes(sanitizedVerbStem, suffixTemplateStrings);
        var mutableVerbStem = ApplyIrregularVerbRules(sanitizedVerbStem, suffixes[0][0]);
        var conjugatedForm = MergeSyllablesFromLeftToRight(mutableVerbStem, suffixes);
        var finalForm = ApplyConjugatedFormEdgeCaseLogic(conjugatedForm.Value, conjugationParams.Honorific);

        return new ConjugationResult(finalForm, []);
    }

    private string[] GetSuffixes(string verbStem, string[] suffixTemplateStrings)
    {
        var suffixes = suffixTemplateStrings;
        suffixes[0] = GetSuffix(verbStem, suffixTemplateStrings[0]);

        for (int i = 1; i < suffixTemplateStrings.Length; ++i)
        {
            if (string.IsNullOrEmpty(suffixTemplateStrings[i]))
            {
                break;
            }
            var preceding = suffixes[i - 1];
            suffixes[i] = GetSuffix(preceding, suffixTemplateStrings[i]);
        }

        return suffixes;
    }

    private static MutableVerbStem ApplyIrregularVerbRules(string verbStem, char firstSyllableOfFirstSuffix)
    {
        bool hasHiddenBadchim = false;
        //var sb = new StringBuilder(verbStem);
        sb.Clear();
        sb.Append(verbStem);
        char lastSyllableOfVerbStem = verbStem[^1];
        string valueToAppend = string.Empty;

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

        return new MutableVerbStem(sb, hasHiddenBadchim);
    }

    private string ApplyVerbStemEdgeCaseLogic(string verbStem, string suffixTemplateString)
    {
        if (verbStem is not "뵙" and not "푸")
        {
            return verbStem;
        }

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
        if (isHonorific && conjugatedForm[^2] == '셔' && conjugatedForm[^1] == '요')
        {
            //conjugatedForm = conjugatedForm.Replace("셔요", "세요");
            conjugatedForm[^2] = '세';
        }

        return conjugatedForm.ToString();
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

    private static MutableVerbStem MergeSyllablesFromLeftToRight(MutableVerbStem verbStem, string[] suffixes)
    {
        //var sb = new StringBuilder(verbStem.Value);
        //sb.Clear();
        //sb.Append(verbStem.Value);

        if (verbStem.HasHiddenBadchim)
        {
            verbStem.Value.Append(suffixes[0]);
        }
        else
        {
            Attach(verbStem.Value, suffixes[0]);
        }

        for (int i = 1; i < suffixes.Length; ++i)
        {
            var suffix = suffixes[i];
            if (string.IsNullOrEmpty(suffix))
            {
                break;
            }
            Attach(verbStem.Value, suffix);
        }

        return verbStem;
    }

    private string GetSuffix(string precedingText, string suffixTemplateString)
    {
        var suffixTemplate = suffixTemplateParser.Parse(suffixTemplateString);
        var suffixVariant = suffixTemplate.ChooseSuffixVariant(precedingText);

        return suffixVariant;
    }
}
