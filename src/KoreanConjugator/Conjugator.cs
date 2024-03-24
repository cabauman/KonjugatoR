using System.Text;

namespace KoreanConjugator;

/// <summary>
/// Represents a utility that can conjugate Korean verbs and adjectives from dictionary form.
/// </summary>
public class Conjugator : IConjugator
{
    private readonly string[] arr = new string[4];
    private readonly StringBuilder sb = new StringBuilder(1);
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
        //var sanitizedVerbStem = ApplyVerbStemEdgeCaseLogic(verbStem, suffixTemplateStrings[0]);
        // 11.72 KB (+0 KB)
        // pass shared array into GetSuffixTemplateStrings: 0 KB (+0 KB)
        //long startMemory = GC.GetTotalMemory(false);
        //var suffixes = GetSuffixes(sanitizedVerbStem, suffixTemplateStrings);
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
        //var finalForm = ApplyConjugatedFormEdgeCaseLogic(conjugatedForm.Value, conjugationParams.Honorific);
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

        sb.Clear();
        sb.Append(verbStem);
        var mutableVerbStem = new MutableVerbStem
        {
            Stem = verbStem,
            Builder = sb,
        };

        var suffixTemplateStrings = templateListProvider.GetSuffixTemplateStrings(verbStem, conjugationParams, arr);
        mutableVerbStem = ApplyVerbStemEdgeCaseLogic(mutableVerbStem, suffixTemplateStrings[0]);
        var firstSuffix = GetSuffix(mutableVerbStem.Builder, suffixTemplateStrings[0]);
        mutableVerbStem = ApplyIrregularVerbRules(mutableVerbStem, firstSuffix.FirstChar);
        var conjugatedForm = MergeSyllablesFromLeftToRight(mutableVerbStem, suffixTemplateStrings, firstSuffix);
        var finalForm = ApplyConjugatedFormEdgeCaseLogic(conjugatedForm.Builder, conjugationParams.Honorific);

        return new ConjugationResult(finalForm, []);
    }

    private static MutableVerbStem ApplyIrregularVerbRules(MutableVerbStem verbStem, char firstSyllableOfFirstSuffix)
    {
        bool hasHiddenBadchim = false;
        char lastSyllableOfVerbStem = verbStem.Stem[^1];
        string valueToAppend = string.Empty;

        if (HangulUtil.IsIrregular(verbStem.Stem))
        {
            if ('르'.Equals(lastSyllableOfVerbStem))
            {
                if ("아어았었".Contains(firstSyllableOfFirstSuffix))
                {
                    // add ㄹ to syllable preceding 르.
                    int indexOfCharToReplace = verbStem.Stem.Length - 2;
                    char newSyllable = HangulUtil.SetFinal(verbStem.Stem[indexOfCharToReplace], 'ᆯ');
                    verbStem.Builder[indexOfCharToReplace] = newSyllable;
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
                        if ((verbStem.Stem.Equals("묻잡") || "돕곱".Contains(lastSyllableOfVerbStem)) &&
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

        verbStem.Builder[^1] = lastSyllableOfVerbStem;
        verbStem.Builder.Append(valueToAppend);
        verbStem.HasHiddenBadchim = hasHiddenBadchim;

        return verbStem;
    }

    private MutableVerbStem ApplyVerbStemEdgeCaseLogic(MutableVerbStem verbStem, string suffixTemplateString)
    {
        if (!verbStem.Stem.Equals("뵙") && !verbStem.Stem.Equals("푸"))
        {
            return verbStem;
        }

        var suffix = GetSuffix(verbStem.Builder, suffixTemplateString);
        if (verbStem.Stem.Equals("뵙") && HangulUtil.Initial(suffix.FirstChar).Equals('ᄋ'))
        {
            verbStem.Builder[0] = '뵈';
            verbStem.Stem = "뵈";
        }
        else if (verbStem.Stem.Equals("푸") && HangulUtil.Initial(suffix.FirstChar).Equals('ᄋ'))
        {
            verbStem.Builder[0] = '퍼';
            verbStem.Stem = "퍼";
        }

        return verbStem;
    }

    private static string ApplyConjugatedFormEdgeCaseLogic(StringBuilder conjugatedForm, bool isHonorific)
    {
        if (!isHonorific)
        {
            return conjugatedForm.ToString();
        }

        // Replace 셔요 with 세요.
        for (int i = 0; i < conjugatedForm.Length - 1; i++)
        {
            if (conjugatedForm[i] == '셔' && conjugatedForm[i + 1] == '요')
            {
                conjugatedForm[i] = '세';
                break;
            }
        }

        return conjugatedForm.ToString();
    }

    private static void Attach(StringBuilder sb, ProcessedSuffix suffix)
    {
        var lastSyllableOfSb = sb[^1];

        if (HangulUtil.HasFinal(lastSyllableOfSb))
        {
            sb.Append(suffix);
        }
        else
        {
            if (HangulUtil.IsModernCompatibilityLetter(suffix.FirstChar))
            {
                var composableFinal = HangulUtil.ToComposableFinal(suffix.FirstChar);
                var final = HangulUtil.FinalToIndex(composableFinal);
                int initial = HangulUtil.IndexOfInitial(lastSyllableOfSb);
                int medial = HangulUtil.IndexOfMedial(lastSyllableOfSb);
                char result = HangulUtil.Construct(initial, medial, final);
                sb[^1] = result;
                foreach (var c in suffix.Chars.Skip(1))
                {
                    sb.Append(c);
                }
            }
            else
            {
                if (HangulUtil.CanContract(lastSyllableOfSb, suffix.FirstChar))
                {
                    // Apply vowel contraction.
                    char result = HangulUtil.Contract(lastSyllableOfSb, suffix.FirstChar);
                    sb[^1] = result;
                    foreach (var c in suffix.Chars.Skip(1))
                    {
                        sb.Append(c);
                    }
                }
                else
                {
                    sb.Append(suffix);
                }
            }
        }
    }

    private static MutableVerbStem MergeSyllablesFromLeftToRight(MutableVerbStem verbStem, Span<string> suffixes, ProcessedSuffix firstSuffix)
    {
        if (verbStem.HasHiddenBadchim)
        {
            verbStem.Builder.Append(firstSuffix);
        }
        else
        {
            Attach(verbStem.Builder, firstSuffix);
        }

        for (int i = 1; i < suffixes.Length; ++i)
        {
            var suffix = GetSuffix(verbStem.Builder, suffixes[i]);
            Attach(verbStem.Builder, suffix);
        }

        return verbStem;
    }

    private static ProcessedSuffix GetSuffix(StringBuilder precedingText, string suffixTemplateString)
    {
        return SuffixChooser.Choose(precedingText, suffixTemplateString);
    }
}
