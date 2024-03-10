namespace KoreanConjugator;

/// <summary>
/// Represents a utility that provides a list of suffix templates required for conjugation.
/// </summary>
public class ConjugationSuffixTemplateListProvider
{
    private static readonly Dictionary<(Tense, Formality, ClauseType), string> Map = new()
    {
        { (Tense.Past,      Formality.FormalHigh,       ClauseType.Declarative),      "(ㅂ/습)니다" },
        { (Tense.Past,      Formality.FormalHigh,       ClauseType.Imperative),       "" },
        { (Tense.Past,      Formality.FormalHigh,       ClauseType.Interrogative),    "(ㅂ/습)니까?" },
        { (Tense.Past,      Formality.FormalHigh,       ClauseType.Propositive),      "" },
        { (Tense.Past,      Formality.FormalLow,        ClauseType.Declarative),      "다" },
        { (Tense.Past,      Formality.FormalLow,        ClauseType.Imperative),       "" },
        { (Tense.Past,      Formality.FormalLow,        ClauseType.Interrogative),    "냐?" },
        { (Tense.Past,      Formality.FormalLow,        ClauseType.Propositive),      "" },
        { (Tense.Past,      Formality.InformalHigh,     ClauseType.Declarative),      "어요" },
        { (Tense.Past,      Formality.InformalHigh,     ClauseType.Imperative),       "" },
        { (Tense.Past,      Formality.InformalHigh,     ClauseType.Interrogative),    "어요?" },
        { (Tense.Past,      Formality.InformalHigh,     ClauseType.Propositive),      "" },
        { (Tense.Past,      Formality.InformalLow,      ClauseType.Declarative),      "어" },
        { (Tense.Past,      Formality.InformalLow,      ClauseType.Imperative),       "" },
        { (Tense.Past,      Formality.InformalLow,      ClauseType.Interrogative),    "어?" },
        { (Tense.Past,      Formality.InformalLow,      ClauseType.Propositive),      "" },
        { (Tense.Present,   Formality.FormalHigh,       ClauseType.Declarative),      "(ㅂ/습)니다" },
        { (Tense.Present,   Formality.FormalHigh,       ClauseType.Imperative),       "(ㅂ/습)시오" },
        { (Tense.Present,   Formality.FormalHigh,       ClauseType.Interrogative),    "(ㅂ/습)니까?" },
        { (Tense.Present,   Formality.FormalHigh,       ClauseType.Propositive),      "(ㅂ/읍)시다" },
        { (Tense.Present,   Formality.FormalLow,        ClauseType.Declarative),      "(ㄴ/는)다" },
        { (Tense.Present,   Formality.FormalLow,        ClauseType.Imperative),       "(아/어)라" },
        { (Tense.Present,   Formality.FormalLow,        ClauseType.Interrogative),    "나?" },
        { (Tense.Present,   Formality.FormalLow,        ClauseType.Propositive),      "자" },
        { (Tense.Present,   Formality.InformalHigh,     ClauseType.Declarative),      "(아/어)요" },
        { (Tense.Present,   Formality.InformalHigh,     ClauseType.Imperative),       "(아/어)요" },
        { (Tense.Present,   Formality.InformalHigh,     ClauseType.Interrogative),    "(아/어)요?" },
        { (Tense.Present,   Formality.InformalHigh,     ClauseType.Propositive),      "(아/어)요" },
        { (Tense.Present,   Formality.InformalLow,      ClauseType.Declarative),      "(아/어)" },
        { (Tense.Present,   Formality.InformalLow,      ClauseType.Imperative),       "(아/어)" },
        { (Tense.Present,   Formality.InformalLow,      ClauseType.Interrogative),    "(아/어)?" },
        { (Tense.Present,   Formality.InformalLow,      ClauseType.Propositive),      "(아/어)" },
        { (Tense.Future,    Formality.FormalHigh,       ClauseType.Declarative),      "(ㅂ/습)니다" },
        { (Tense.Future,    Formality.FormalHigh,       ClauseType.Imperative),       "" },
        { (Tense.Future,    Formality.FormalHigh,       ClauseType.Interrogative),    "(ㅂ/습)니까?" },
        { (Tense.Future,    Formality.FormalHigh,       ClauseType.Propositive),      "" },
        { (Tense.Future,    Formality.FormalLow,        ClauseType.Declarative),      "다" },
        { (Tense.Future,    Formality.FormalLow,        ClauseType.Imperative),       "" },
        { (Tense.Future,    Formality.FormalLow,        ClauseType.Interrogative),    "냐?" },
        { (Tense.Future,    Formality.FormalLow,        ClauseType.Propositive),      "" },
        { (Tense.Future,    Formality.InformalHigh,     ClauseType.Declarative),      "예요" },
        { (Tense.Future,    Formality.InformalHigh,     ClauseType.Imperative),       "" },
        { (Tense.Future,    Formality.InformalHigh,     ClauseType.Interrogative),    "예요?" },
        { (Tense.Future,    Formality.InformalHigh,     ClauseType.Propositive),      "" },
        { (Tense.Future,    Formality.InformalLow,      ClauseType.Declarative),      "야" },
        { (Tense.Future,    Formality.InformalLow,      ClauseType.Imperative),       "" },
        { (Tense.Future,    Formality.InformalLow,      ClauseType.Interrogative),    "야?" },
        { (Tense.Future,    Formality.InformalLow,      ClauseType.Propositive),      "" },
    };

    /// <summary>
    /// Returns a list of suffix template strings.
    /// </summary>
    /// <param name="verbStem">The verb stem.</param>
    /// <param name="conjugationParams">The conjugation parameters.</param>
    /// <returns>A list of suffix template strings.</returns>
    /// <exception cref="ArgumentException"><paramref name="verbStem"/> is null.</exception>
    public string[] GetSuffixTemplateStrings(string verbStem, ConjugationParams conjugationParams)
    {
        ArgumentException.ThrowIfNullOrEmpty(verbStem);

        var suffixTemplateStrings = ConvertParamsToSuffixes(conjugationParams);
        ApplyCopulaLogic(verbStem, conjugationParams, suffixTemplateStrings);

        return suffixTemplateStrings;
    }

    private static string[] ConvertParamsToSuffixes(ConjugationParams conjugationParams)
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

        var key = (conjugationParams.Tense, conjugationParams.Formality, conjugationParams.ClauseType);
        suffixes.Add(Map[key]);

        return suffixes.ToArray();
    }

    private static void ApplyCopulaLogic(string verbStem, ConjugationParams conjugationParams, string[] suffixTemplateStrings)
    {
        if (conjugationParams.Tense == Tense.Present &&
            !conjugationParams.Honorific &&
            HangulUtil.RegularIdaVerbs != null &&
            !HangulUtil.RegularIdaVerbs.Contains(verbStem) &&
            (verbStem.EndsWith('이') || verbStem.Equals("아니")))
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
