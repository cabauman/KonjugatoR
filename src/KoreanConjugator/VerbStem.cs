using System.Text;

namespace KoreanConjugator;

/// <summary>
/// Represents a mutable verb stem.
/// </summary>
public struct VerbStem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MutableVerbStem"/> class.
    /// </summary>
    /// <param name="value">The verb stem.</param>
    public VerbStem(string verbStem, StringBuilder value)
    {
        Value= verbStem;
        Sb = value;
    }

    /// <summary>
    /// Gets the verb stem.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets the verb stem.
    /// </summary>
    public StringBuilder Sb { get; }

    /// <summary>
    /// Gets a value indicating whether the verb stem has a hidden badchim.
    /// </summary>
    public bool HasHiddenBadchim { get; private set; }

    public void Conjugate(string[] suffixTemplateStrings)
    {
        var firstSuffix = SuffixTemplateParser2.Parse(suffixTemplateStrings[0]);
        var firstSyllableOfFirstSuffix = firstSuffix.ChooseSuffixVariant(Value);
        ApplyVerbStemEdgeCaseLogic(firstSuffix);
        ApplyIrregularVerbRules(' ');
        MergeSyllablesFromLeftToRight(suffixTemplateStrings);
        ApplyConjugatedFormEdgeCaseLogic(true);
    }

    private void ApplyVerbStemEdgeCaseLogic(SuffixTemplate suffix)
    {
        var firstSyllable = suffix.ChooseSuffixVariant(Value);
        if (Sb.Equals("뵙") && HangulUtil.Initial(firstSyllable).Equals('ᄋ'))
        {
            Sb[0] = '뵈';
        }
        else if (Sb.Equals("푸") && HangulUtil.Initial(firstSyllable).Equals('ᄋ'))
        {
            Sb[0] = '퍼';
        }
    }

    private void ApplyIrregularVerbRules(char firstSyllableOfFirstSuffix)
    {
        bool hasHiddenBadchim = false;
        char lastSyllableOfVerbStem = Sb[^1];
        string valueToAppend = string.Empty;

        if (HangulUtil.IsIrregular(Value))
        {
            if ('르'.Equals(lastSyllableOfVerbStem))
            {
                if ("아어았었".Contains(firstSyllableOfFirstSuffix))
                {
                    // add ㄹ to syllable preceding 르.
                    int indexOfCharToReplace = Sb.Length - 2;
                    char newSyllable = HangulUtil.SetFinal(Sb[indexOfCharToReplace], 'ᆯ');
                    Sb[indexOfCharToReplace] = newSyllable;
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
                        if ((Sb.Equals("묻잡") || "돕곱".Contains(lastSyllableOfVerbStem)) &&
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

        Sb[^1] = lastSyllableOfVerbStem;
        Sb.Append(valueToAppend);
        HasHiddenBadchim = hasHiddenBadchim;
    }

    private void MergeSyllablesFromLeftToRight(string[] suffixTemplateStrings)
    {
        for (int i = 1; i < suffixTemplateStrings.Length; ++i)
        {
            if (suffixTemplateStrings[i] is null)
            {
                break;
            }

            var suffixTemplate = SuffixTemplateParser2.Parse(suffixTemplateStrings[i]);
            ChooseSuffixVariant(suffixTemplate);
        }
    }

    private void ChooseSuffixVariant(SuffixTemplate suffixTemplate)
    {
        if (suffixTemplate.FirstSyllableOption1.Length == 1 && suffixTemplate.FirstSyllableOption1[0] is '아' or '았')
        {
            ChooseAEuSuffixVariant(suffixTemplate);
        }
        else
        {
            ChooseBadchimDependentSuffixVariant(suffixTemplate);
        }
    }

    private void ChooseAEuSuffixVariant(SuffixTemplate suffixTemplate)
    {
        char connector;
        if (Sb.Equals("하"))
        {
            connector = suffixTemplate.pastTense ? '였' : '여';
        }
        else
        {
            connector = suffixTemplate.pastTense ? '었' : '어';

            int index = Sb.Length - 1;
            while (index >= 0)
            {
                var medial = HangulUtil.Medial(Sb[index]);
                if (medial != 'ᅳ')
                {
                    if (medial == 'ᅡ' || medial == 'ᅩ')
                    {
                        connector = suffixTemplate.pastTense ? '았' : '아';
                    }

                    break;
                }

                --index;
            }
        }

        Sb.Append(connector);
        Sb.Append(suffixTemplate.StaticText);
    }

    private void ChooseBadchimDependentSuffixVariant(SuffixTemplate suffixTemplate)
    {
        var badchimlessConnector = suffixTemplate.FirstSyllableOption1;
        var badchimConnector = suffixTemplate.FirstSyllableOption2;

        if (badchimlessConnector.Length == 0 && badchimConnector.Length == 0)
        {
            Sb.Append(suffixTemplate.StaticText);
            return;
        }

        char connector = default;
        if (HangulUtil.Final(Sb[^1]) is not 'ᆯ' and not '\0')
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

        Attach(connector);
        Sb.Append(suffixTemplate.StaticText);
    }

    private void Attach(char suffix)
    {
        if (suffix == '\0')
        {
            return;
        }

        var lastSyllableOfSb = Sb[^1];

        if (HangulUtil.HasFinal(lastSyllableOfSb))
        {
            if (HangulUtil.IsModernCompatibilityLetter(suffix))
            {
                throw new InvalidOperationException();
            }

            Sb.Append(suffix);
        }
        else
        {
            if (HangulUtil.IsModernCompatibilityLetter(suffix))
            {
                var composableFinal = HangulUtil.ToComposableFinal(suffix);
                var final = HangulUtil.FinalToIndex(composableFinal);
                int initial = HangulUtil.IndexOfInitial(lastSyllableOfSb);
                int medial = HangulUtil.IndexOfMedial(lastSyllableOfSb);
                char result = HangulUtil.Construct(initial, medial, final);
                Sb[^1] = result;
            }
            else
            {
                if (HangulUtil.CanContract(lastSyllableOfSb, suffix))
                {
                    // Apply vowel contraction.
                    char result = HangulUtil.Contract(lastSyllableOfSb, suffix);
                    Sb[^1] = result;
                }
                else
                {
                    Sb.Append(suffix);
                }
            }
        }
    }

    private void ApplyConjugatedFormEdgeCaseLogic(bool isHonorific)
    {
        Sb.Replace("셔요", "세요");
        //if (isHonorific && Value.Contains("셔요"))
        //{
        //    Value = Value.Replace("셔요", "세요");
        //}

        var result = Sb.ToString();
    }
}
