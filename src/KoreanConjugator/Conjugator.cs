using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KoreanConjugator
{
    /// <summary>
    /// Represents a utility that can conjugate Korean verbs and adjectives from dictionary form.
    /// </summary>
    public class Conjugator : IConjugator
    {
        private static readonly Dictionary<Tuple<Tense, Formality, ClauseType>, string> Map =
            new Dictionary<Tuple<Tense, Formality, ClauseType>, string>
        {
            { Tuple.Create(Tense.Past,      Formality.FormalHigh,       ClauseType.Declarative),      "(ㅂ/습),니다" },
            { Tuple.Create(Tense.Past,      Formality.FormalHigh,       ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Past,      Formality.FormalHigh,       ClauseType.Interrogative),    "(ㅂ/습),니까?" },
            { Tuple.Create(Tense.Past,      Formality.FormalHigh,       ClauseType.Propositive),      "," },

            { Tuple.Create(Tense.Past,      Formality.FormalLow,        ClauseType.Declarative),      ",다" },
            { Tuple.Create(Tense.Past,      Formality.FormalLow,        ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Past,      Formality.FormalLow,        ClauseType.Interrogative),    ",냐?" },
            { Tuple.Create(Tense.Past,      Formality.FormalLow,        ClauseType.Propositive),      "," },

            { Tuple.Create(Tense.Past,      Formality.InformalHigh,     ClauseType.Declarative),      "어,요" },
            { Tuple.Create(Tense.Past,      Formality.InformalHigh,     ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Past,      Formality.InformalHigh,     ClauseType.Interrogative),    "어,요?" },
            { Tuple.Create(Tense.Past,      Formality.InformalHigh,     ClauseType.Propositive),      "," },

            { Tuple.Create(Tense.Past,      Formality.InformalLow,      ClauseType.Declarative),      "어," },
            { Tuple.Create(Tense.Past,      Formality.InformalLow,      ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Past,      Formality.InformalLow,      ClauseType.Interrogative),    "어,?" },
            { Tuple.Create(Tense.Past,      Formality.InformalLow,      ClauseType.Propositive),      "," },


            { Tuple.Create(Tense.Present,   Formality.FormalHigh,       ClauseType.Declarative),      "(ㅂ/습),니다" },
            { Tuple.Create(Tense.Present,   Formality.FormalHigh,       ClauseType.Imperative),       "(ㅂ/습),시오" },
            { Tuple.Create(Tense.Present,   Formality.FormalHigh,       ClauseType.Interrogative),    "(ㅂ/습),니까?" },
            { Tuple.Create(Tense.Present,   Formality.FormalHigh,       ClauseType.Propositive),      "(ㅂ/읍),시다" },

            { Tuple.Create(Tense.Present,   Formality.FormalLow,        ClauseType.Declarative),      "(ㄴ/는),다" },
            { Tuple.Create(Tense.Present,   Formality.FormalLow,        ClauseType.Imperative),       "(아/어),라" },
            { Tuple.Create(Tense.Present,   Formality.FormalLow,        ClauseType.Interrogative),    ",나?" },
            { Tuple.Create(Tense.Present,   Formality.FormalLow,        ClauseType.Propositive),      ",자" },

            { Tuple.Create(Tense.Present,   Formality.InformalHigh,     ClauseType.Declarative),      "(아/어),요" },
            { Tuple.Create(Tense.Present,   Formality.InformalHigh,     ClauseType.Imperative),       "(아/어),요" },
            { Tuple.Create(Tense.Present,   Formality.InformalHigh,     ClauseType.Interrogative),    "(아/어),요?" },
            { Tuple.Create(Tense.Present,   Formality.InformalHigh,     ClauseType.Propositive),      "(아/어),요" },

            { Tuple.Create(Tense.Present,   Formality.InformalLow,      ClauseType.Declarative),      "(아/어)," },
            { Tuple.Create(Tense.Present,   Formality.InformalLow,      ClauseType.Imperative),       "(아/어)," },
            { Tuple.Create(Tense.Present,   Formality.InformalLow,      ClauseType.Interrogative),    "(아/어),?" },
            { Tuple.Create(Tense.Present,   Formality.InformalLow,      ClauseType.Propositive),      "(아/어)," },


            { Tuple.Create(Tense.Future,    Formality.FormalHigh,       ClauseType.Declarative),      "(ㅂ/습),니다" },
            { Tuple.Create(Tense.Future,    Formality.FormalHigh,       ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Future,    Formality.FormalHigh,       ClauseType.Interrogative),    "(ㅂ/습),니까?" },
            { Tuple.Create(Tense.Future,    Formality.FormalHigh,       ClauseType.Propositive),      "," },

            { Tuple.Create(Tense.Future,    Formality.FormalLow,        ClauseType.Declarative),      ",다" },
            { Tuple.Create(Tense.Future,    Formality.FormalLow,        ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Future,    Formality.FormalLow,        ClauseType.Interrogative),    ",냐?" }, // TODO
            { Tuple.Create(Tense.Future,    Formality.FormalLow,        ClauseType.Propositive),      "," },

            { Tuple.Create(Tense.Future,    Formality.InformalHigh,     ClauseType.Declarative),      "예,요" },
            { Tuple.Create(Tense.Future,    Formality.InformalHigh,     ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Future,    Formality.InformalHigh,     ClauseType.Interrogative),    "예,요?" },
            { Tuple.Create(Tense.Future,    Formality.InformalHigh,     ClauseType.Propositive),      "," },

            { Tuple.Create(Tense.Future,    Formality.InformalLow,      ClauseType.Declarative),      "야," },
            { Tuple.Create(Tense.Future,    Formality.InformalLow,      ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Future,    Formality.InformalLow,      ClauseType.Interrogative),    "야,?" },
            { Tuple.Create(Tense.Future,    Formality.InformalLow,      ClauseType.Propositive),      "," },
        };

        private readonly ISuffixTemplateParser suffixTemplateParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="Conjugator"/> class.
        /// </summary>
        /// <param name="suffixTemplateParser">The implementation to use for parsing suffix templates.</param>
        public Conjugator(ISuffixTemplateParser suffixTemplateParser)
        {
            this.suffixTemplateParser = suffixTemplateParser ?? throw new ArgumentNullException(nameof(suffixTemplateParser));
        }

        /// <inheritdoc/>
        public ConjugationResult Conjugate(string verbStem, ConjugationParams conjugationParams)
        {
            if (string.IsNullOrEmpty(nameof(verbStem)))
                throw new ArgumentException(verbStem);

            if (conjugationParams.Honorific && HangulUtil.SpecialHonorificMap.TryGetValue(verbStem, out var honorificStem))
            {
                verbStem = honorificStem;
            }

            var suffixes = ConvertParamsToSuffixes(conjugationParams);
            ChooseSuffixesBasedOnPrecedingSyllable(verbStem, suffixes);
            (var modifiedVerbStem, var hasHiddenBadchim) = ApplyIrregularVerbRules(verbStem, suffixes.First().First());
            var conjugatedForm = MergeSyllablesFromLeftToRight(modifiedVerbStem, hasHiddenBadchim, suffixes);

            return new ConjugationResult(conjugatedForm, null);
        }

        /// <inheritdoc/>
        public ConjugationResult AttachSuffixToVerb(string verbStem, string suffixTemplateString)
        {
            if (string.IsNullOrEmpty(verbStem))
                throw new ArgumentException(nameof(verbStem));
            if (string.IsNullOrEmpty(suffixTemplateString))
                throw new ArgumentException(nameof(suffixTemplateString));

            string suffixString = GetSuffix(verbStem, suffixTemplateString);
            (var modifiedVerbStem, var hasHiddenBadchim) = ApplyIrregularVerbRules(verbStem, suffixString.First());
            var conjugatedForm = Attach(modifiedVerbStem, suffixString, hasHiddenBadchim);

            return new ConjugationResult(conjugatedForm, null);
        }

        public ConjugationResult AttachSuffixToNoun(string noun, string suffixTemplateString)
        {
            if (string.IsNullOrEmpty(noun))
                throw new ArgumentException(nameof(noun));
            if (string.IsNullOrEmpty(suffixTemplateString))
                throw new ArgumentException(nameof(suffixTemplateString));

            string suffixString = GetSuffix(noun, suffixTemplateString);
            var result = AttachToNoun(noun, suffixString);

            return new ConjugationResult(result, null);
        }

        private string[] ConvertParamsToSuffixes(ConjugationParams conjugationParams)
        {
            var suffixes = new List<string>();

            if (conjugationParams.Honorific)
            {
                suffixes.Add("(으)시");
            }

            switch (conjugationParams.Tense)
            {
                case Tense.Future:
                    suffixes.Add("(ㄹ/을) 거");
                    break;
                case Tense.Past:
                    suffixes.Add("(았/었)");
                    break;
                case Tense.Present:
                    break;
            }

            var key = Tuple.Create(conjugationParams.Tense, conjugationParams.Formality, conjugationParams.ClauseType);
            suffixes.AddRange(Map[key].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            return suffixes.ToArray();
        }

        private void ChooseSuffixesBasedOnPrecedingSyllable(string verbStem, string[] suffixTemplateStrings)
        {
            suffixTemplateStrings[0] = GetSuffix(verbStem, suffixTemplateStrings[0]);

            for (int i = 1; i < suffixTemplateStrings.Length; ++i)
            {
                var preceding = suffixTemplateStrings[i - 1];
                suffixTemplateStrings[i] = GetSuffix(preceding, suffixTemplateStrings[i]);
            }
        }

        private (string modifiedVerbStem, bool hasInvisibleBadchim) ApplyIrregularVerbRules(string verbStem, char firstSyllableOfFirstSuffix)
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
                            if ("돕곱".Any(x => x.Equals(lastSyllableOfVerbStem)) && "아어았었".Any(x => x.Equals(firstSyllableOfFirstSuffix)))
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
                            // drop ㄹ.
                            lastSyllableOfVerbStem = HangulUtil.DropFinal(lastSyllableOfVerbStem);
                            break;
                        case 'ᇂ':
                            // drop ㅎ.
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

            return (sb.ToString(), hasHiddenBadchim);
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
                    sb[sb.Length - 1] = result;
                    if (suffix.Length > 1)
                    {
                        sb.Append(suffix.Substring(1));
                    }
                }
                else
                {
                    sb.Append(suffix);
                }
            }

            return sb.ToString();
        }

        private string Attach(string text, string suffix, bool hasHiddenBadchim)
        {
            var sb = new StringBuilder();
            sb.Append(text);

            if (/*i == 1 && */hasHiddenBadchim)
            {
                // Attach.
                sb.Append(suffix);
            }
            else if (HangulUtil.HasFinal(text.Last()))
            {
                // Attach.
                sb.Append(suffix);
            }
            else
            {
                if (HangulUtil.IsModernCompatibilityLetter(suffix.First()))
                {
                    var composableFinal = HangulUtil.ToComposableFinal(suffix.First());
                    var final = HangulUtil.FinalToIndex(composableFinal);
                    int initial = HangulUtil.IndexOfInitial(text.Last());
                    int medial = HangulUtil.IndexOfMedial(text.Last());
                    char result = HangulUtil.Construct(initial, medial, final);
                    sb[sb.Length - 1] = result;
                    if (suffix.Length > 1)
                    {
                        sb.Append(suffix.Substring(1));
                    }
                }
                else
                {
                    if (HangulUtil.CanContract(text.Last(), suffix.First()))
                    {
                        // Apply vowel contraction.
                        char result = HangulUtil.Contract(text.Last(), suffix.First());
                        sb[sb.Length - 1] = result;
                        if (suffix.Length > 1)
                        {
                            sb.Append(suffix.Substring(1));
                        }
                    }
                    else
                    {
                        sb.Append(suffix);
                    }
                }
            }

            return string.Empty;
        }

        private string MergeSyllablesFromLeftToRight(string verbStem, bool hasHiddenBadchim, string[] suffixes)
        {
            var textBlocks = new string[suffixes.Length + 1];
            textBlocks[0] = verbStem;
            suffixes.CopyTo(textBlocks, 1);

            var sb = new StringBuilder();
            sb.Append(textBlocks[0]);

            for (int i = 1; i < textBlocks.Length; ++i)
            {
                var current = textBlocks[i];
                var preceding = textBlocks[i - 1];

                // If the last syllable of the verb stem has an invisible badchim.
                if (i == 1 && hasHiddenBadchim)
                {
                    // Attach.
                    sb.Append(current);
                }
                else if (HangulUtil.HasFinal(preceding.Last()))
                {
                    // Attach.
                    sb.Append(current);
                }
                else
                {
                    if (HangulUtil.IsModernCompatibilityLetter(current.First()))
                    {
                        var composableFinal = HangulUtil.ToComposableFinal(current.First());
                        var final = HangulUtil.FinalToIndex(composableFinal);
                        int initial = HangulUtil.IndexOfInitial(preceding.Last());
                        int medial = HangulUtil.IndexOfMedial(preceding.Last());
                        char result = HangulUtil.Construct(initial, medial, final);
                        sb[sb.Length - 1] = result;
                        if (current.Length > 1)
                        {
                            sb.Append(current.Substring(1));
                        }
                    }
                    else
                    {
                        if (HangulUtil.CanContract(preceding.Last(), current.First()))
                        {
                            // Apply vowel contraction.
                            char result = HangulUtil.Contract(preceding.Last(), current.First());
                            sb[sb.Length - 1] = result;
                            if (current.Length > 1)
                            {
                                sb.Append(current.Substring(1));
                            }
                        }
                        else
                        {
                            sb.Append(current);
                        }
                    }
                }
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
}
