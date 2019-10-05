using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KoreanConjugator
{
    /// <summary>
    /// Represents a utility that can conjugate Korean verbs, adjectives, and nouns from dictionary form.
    /// </summary>
    public class Conjugator
    {
        private static readonly Dictionary<Tuple<Tense, Formality, ClauseType>, string> Map =
            new Dictionary<Tuple<Tense, Formality, ClauseType>, string>
        {
            { Tuple.Create(Tense.Past,      Formality.FormalHigh,       ClauseType.Declarative),      "ㅂ/습,니다" },
            { Tuple.Create(Tense.Past,      Formality.FormalHigh,       ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Past,      Formality.FormalHigh,       ClauseType.Interrogative),    "ㅂ/습,니까?" },
            { Tuple.Create(Tense.Past,      Formality.FormalHigh,       ClauseType.Propositive),      "," },

            { Tuple.Create(Tense.Past,      Formality.FormalLow,        ClauseType.Declarative),      ",다" },
            { Tuple.Create(Tense.Past,      Formality.FormalLow,        ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Past,      Formality.FormalLow,        ClauseType.Interrogative),    "는,야?" },
            { Tuple.Create(Tense.Past,      Formality.FormalLow,        ClauseType.Propositive),      "," },

            { Tuple.Create(Tense.Past,      Formality.InformalHigh,     ClauseType.Declarative),      "어,요" },
            { Tuple.Create(Tense.Past,      Formality.InformalHigh,     ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Past,      Formality.InformalHigh,     ClauseType.Interrogative),    "어,요?" },
            { Tuple.Create(Tense.Past,      Formality.InformalHigh,     ClauseType.Propositive),      "," },

            { Tuple.Create(Tense.Past,      Formality.InformalLow,      ClauseType.Declarative),      "어," },
            { Tuple.Create(Tense.Past,      Formality.InformalLow,      ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Past,      Formality.InformalLow,      ClauseType.Interrogative),    "어,?" },
            { Tuple.Create(Tense.Past,      Formality.InformalLow,      ClauseType.Propositive),      "," },


            { Tuple.Create(Tense.Present,   Formality.FormalHigh,       ClauseType.Declarative),      "ㅂ/습,니다" },
            { Tuple.Create(Tense.Present,   Formality.FormalHigh,       ClauseType.Imperative),       "ㅂ/습,시오" },
            { Tuple.Create(Tense.Present,   Formality.FormalHigh,       ClauseType.Interrogative),    "ㅂ/습,니까?" },
            { Tuple.Create(Tense.Present,   Formality.FormalHigh,       ClauseType.Propositive),      "ㅂ/읍,시다" },

            { Tuple.Create(Tense.Present,   Formality.FormalLow,        ClauseType.Declarative),      "ㄴ/는,다" },
            { Tuple.Create(Tense.Present,   Formality.FormalLow,        ClauseType.Imperative),       "아/어,라" },
            { Tuple.Create(Tense.Present,   Formality.FormalLow,        ClauseType.Interrogative),    ",나?" },
            { Tuple.Create(Tense.Present,   Formality.FormalLow,        ClauseType.Propositive),      ",자" },

            { Tuple.Create(Tense.Present,   Formality.InformalHigh,     ClauseType.Declarative),      "아/어,요" },
            { Tuple.Create(Tense.Present,   Formality.InformalHigh,     ClauseType.Imperative),       "아/어,요" },
            { Tuple.Create(Tense.Present,   Formality.InformalHigh,     ClauseType.Interrogative),    "아/어,요?" },
            { Tuple.Create(Tense.Present,   Formality.InformalHigh,     ClauseType.Propositive),      "아/어,요" },

            { Tuple.Create(Tense.Present,   Formality.InformalLow,      ClauseType.Declarative),      "아/어," },
            { Tuple.Create(Tense.Present,   Formality.InformalLow,      ClauseType.Imperative),       "아/어," },
            { Tuple.Create(Tense.Present,   Formality.InformalLow,      ClauseType.Interrogative),    "아/어,?" },
            { Tuple.Create(Tense.Present,   Formality.InformalLow,      ClauseType.Propositive),      "아/어," },


            { Tuple.Create(Tense.Future,    Formality.FormalHigh,       ClauseType.Declarative),      "ㅂ/습,니다" },
            { Tuple.Create(Tense.Future,    Formality.FormalHigh,       ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Future,    Formality.FormalHigh,       ClauseType.Interrogative),    "ㅂ/습,니까?" },
            { Tuple.Create(Tense.Future,    Formality.FormalHigh,       ClauseType.Propositive),      "," },

            { Tuple.Create(Tense.Future,    Formality.FormalLow,        ClauseType.Declarative),      ",다" },
            { Tuple.Create(Tense.Future,    Formality.FormalLow,        ClauseType.Imperative),       "," },
            { Tuple.Create(Tense.Future,    Formality.FormalLow,        ClauseType.Interrogative),    "ㄴ,야?" }, // TODO
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
            this.suffixTemplateParser = suffixTemplateParser;
        }

        /// <summary>
        /// Transforms the given verb stem into a specific grammatical form specified by the parms.
        /// </summary>
        /// <param name="verbStem">The portion of the verb without the '다' syllable at the end.</param>
        /// <param name="conjugationParams">The params used to specify the conjugated form.</param>
        /// <returns>A conjugation result.</returns>
        public ConjugationResult Conjugate(string verbStem, ConjugationParams conjugationParams)
        {
            ConjugationResult result = new ConjugationResult();

            var suffixes = ConvertParamsToSuffixes(conjugationParams);
            ChooseSuffixesBasedOnPrecedingSyllable(verbStem, suffixes);
            var modifiedVerbStem = ApplyIrregularVerbRules(verbStem, suffixes.First().First());
            MergeSyllablesFromLeftToRight(modifiedVerbStem, suffixes);

            return result;
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
                    suffixes.Add("ㄹ/을 거");
                    break;
                case Tense.Past:
                    suffixes.Add("았/었");
                    break;
                case Tense.Present:
                    break;
            }

            var key = Tuple.Create(conjugationParams.Tense, conjugationParams.Formality, conjugationParams.ClauseType);
            suffixes.AddRange(Map[key].Split(','));

            return suffixes.ToArray();
        }

        private void ChooseSuffixesBasedOnPrecedingSyllable(string verbStem, string[] suffixes)
        {
            suffixes[0] = GetSuffixVariantForVerb(verbStem, suffixes[0]);

            for (int i = 1; i < suffixes.Length; ++i)
            {
                var preceding = suffixes[i - 1];
                suffixes[i] = GetCorrectSuffixVariant(preceding, suffixes[i]);
            }
        }

        private string GetSuffixVariantForVerb(string verbStem, string suffix)
        {
            if (!suffix.StartsWith("아/어"))
            {
                suffix = GetCorrectSuffixVariant(verbStem, suffix);
                return suffix;
            }

            string staticPortion = suffix.Remove(0, 3);
            string connector = "어";
            int index = verbStem.Length - 1;
            while (index >= 0)
            {
                var medial = HangulUtil.Medial(verbStem[index]);
                if (medial != 'ㅡ')
                {
                    if (medial == 'ㅏ' || medial == 'ㅗ')
                    {
                        connector = "아";
                    }

                    break;
                }

                --index;
            }

            return string.Concat(connector, staticPortion);
        }

        private string ApplyIrregularVerbRules(string verbStem, char firstSyllableOfFirstSuffix)
        {
            var sb = new StringBuilder(verbStem);
            char lastSyllableOfVerbStem = verbStem.Last();

            if (HangulUtil.IsIrregular(verbStem))
            {
                if ("아어았었".Any(value => value == firstSyllableOfFirstSuffix))
                {
                    switch (HangulUtil.Final(lastSyllableOfVerbStem))
                    {
                        case '르':
                            // add ㄹ to syllable preceding 르.
                            int indexOfCharToReplace = verbStem.Length - 2;
                            char newSyllable = HangulUtil.SetFinal(verbStem[indexOfCharToReplace], 'ᆯ');
                            sb[indexOfCharToReplace] = newSyllable;
                            break;
                        default:
                            break;
                    }
                }
                else if (HangulUtil.BeginsWithVowel(firstSyllableOfFirstSuffix))
                {
                    switch (HangulUtil.Final(lastSyllableOfVerbStem))
                    {
                        case 'ㅅ':
                            // Drop ㅅ.
                            lastSyllableOfVerbStem = HangulUtil.DropFinal(lastSyllableOfVerbStem);
                            break;
                        case 'ㄷ':
                            // ㄷ → ㄹ.
                            lastSyllableOfVerbStem = HangulUtil.SetFinal(lastSyllableOfVerbStem, 'ㄹ');
                            break;
                        case 'ㅂ':
                            // drop ㅂ, add 우/오.
                            lastSyllableOfVerbStem = HangulUtil.DropFinal(lastSyllableOfVerbStem);
                            sb.Append('우');
                            break;
                        case 'ㅎ':
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
                        case 'ㄹ':
                            // drop ㄹ.
                            lastSyllableOfVerbStem = HangulUtil.DropFinal(lastSyllableOfVerbStem);
                            break;
                        case 'ㅎ':
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
                        case 'ㄹ':
                            // drop ㄹ.
                            lastSyllableOfVerbStem = HangulUtil.DropFinal(lastSyllableOfVerbStem);
                            break;
                        default:
                            break;
                    }
                }
                else if (HangulUtil.IsLetter(firstSyllableOfFirstSuffix))
                {
                    if (firstSyllableOfFirstSuffix == 'ㄴ' || firstSyllableOfFirstSuffix == 'ㄹ' || firstSyllableOfFirstSuffix == 'ㅂ')
                    {
                        switch (HangulUtil.Final(lastSyllableOfVerbStem))
                        {
                            case 'ㄹ':
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
                            case 'ㄹ':
                                // ㄹ → ㄻ. Should probably move this to the MergeSyllablesFromLeftToRight step.
                                lastSyllableOfVerbStem = HangulUtil.SetFinal(lastSyllableOfVerbStem, 'ㄻ');
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            sb[verbStem.Length - 1] = lastSyllableOfVerbStem;

            return sb.ToString();
        }

        private string MergeSyllablesFromLeftToRight(string verbStem, string[] suffixes)
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

                if (HangulUtil.HasFinal(preceding.Last()))
                {
                    // Attach.
                    sb.Append(current);
                }
                else
                {
                    if (HangulUtil.Initial(current.First()).Equals(KoreanLetter.Ieung))
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
                        if (HangulUtil.IsLetter(current.First()))
                        {
                            char initial = HangulUtil.Initial(preceding.Last());
                            char medial = HangulUtil.Medial(preceding.Last());
                            char result = HangulUtil.Construct(initial, medial, current.First());
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

        private string GetCorrectSuffixVariant(string precedingText, string suffixString)
        {
            var suffixTemplate = suffixTemplateParser.Parse(suffixString);
            var suffixVariant = suffixTemplate.ChooseSuffixVariant(precedingText);

            return suffixVariant;
        }
    }
}
