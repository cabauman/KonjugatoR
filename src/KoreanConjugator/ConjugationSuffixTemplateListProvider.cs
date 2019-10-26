using System;
using System.Collections.Generic;

namespace KoreanConjugator
{
    /// <summary>
    /// Represents a utility that provides a list of suffix templates required for conjugation.
    /// </summary>
    public class ConjugationSuffixTemplateListProvider
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

        public string[] Get(string verbStem, ConjugationParams conjugationParams)
        {
            if (string.IsNullOrEmpty(nameof(verbStem)))
                throw new ArgumentException(verbStem);

            var suffixTemplateStrings = ConvertParamsToSuffixes(conjugationParams);
            ApplyCopulaLogic(verbStem, conjugationParams, suffixTemplateStrings);

            return suffixTemplateStrings;
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

        private void ApplyCopulaLogic(string verbStem, ConjugationParams conjugationParams, string[] suffixTemplateStrings)
        {
            if (conjugationParams.Tense == Tense.Present &&
                !conjugationParams.Honorific &&
                !HangulUtil.RegularIdaVerbs.Contains(verbStem) &&
                (verbStem.EndsWith("이") || verbStem.Equals("아니")))
            {
                if (conjugationParams.Formality == Formality.InformalLow)
                {
                    suffixTemplateStrings[0] = "야";
                }
                else if (conjugationParams.Formality == Formality.InformalHigh)
                {
                    suffixTemplateStrings[0] = "에";
                }
            }
        }
    }
}
