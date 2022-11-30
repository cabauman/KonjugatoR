namespace KoreanConjugator;

/// <summary>
/// Represents a utility that provides a list of suffix templates required for conjugation.
/// </summary>
public class ConjugationSuffixTemplateListProvider
{
    private static readonly Dictionary<(Tense, Formality, ClauseType), string[]> Map = new()
    {
        { (Tense.Past,      Formality.FormalHigh,       ClauseType.Declarative),      new string[] { "(ㅂ/습)","니다" } },
        { (Tense.Past,      Formality.FormalHigh,       ClauseType.Imperative),       Array.Empty<string>() },
        { (Tense.Past,      Formality.FormalHigh,       ClauseType.Interrogative),    new string[] { "(ㅂ/습)니까?" } },
        { (Tense.Past,      Formality.FormalHigh,       ClauseType.Propositive),      Array.Empty<string>() },
        { (Tense.Past,      Formality.FormalLow,        ClauseType.Declarative),      new string[] { "다" } },
        { (Tense.Past,      Formality.FormalLow,        ClauseType.Imperative),       Array.Empty<string>() },
        { (Tense.Past,      Formality.FormalLow,        ClauseType.Interrogative),    new string[] { "냐?" } },
        { (Tense.Past,      Formality.FormalLow,        ClauseType.Propositive),      Array.Empty<string>() },
        { (Tense.Past,      Formality.InformalHigh,     ClauseType.Declarative),      new string[] { "어요" } },
        { (Tense.Past,      Formality.InformalHigh,     ClauseType.Imperative),       Array.Empty<string>() },
        { (Tense.Past,      Formality.InformalHigh,     ClauseType.Interrogative),    new string[] { "어요?" } },
        { (Tense.Past,      Formality.InformalHigh,     ClauseType.Propositive),      Array.Empty<string>() },
        { (Tense.Past,      Formality.InformalLow,      ClauseType.Declarative),      new string[] { "어" } },
        { (Tense.Past,      Formality.InformalLow,      ClauseType.Imperative),       Array.Empty<string>() },
        { (Tense.Past,      Formality.InformalLow,      ClauseType.Interrogative),    new string[] { "어?" } },
        { (Tense.Past,      Formality.InformalLow,      ClauseType.Propositive),      Array.Empty<string>() },
        { (Tense.Present,   Formality.FormalHigh,       ClauseType.Declarative),      new string[] { "(ㅂ/습)니다" } },
        { (Tense.Present,   Formality.FormalHigh,       ClauseType.Imperative),       new string[] { "(ㅂ/습)시오" } },
        { (Tense.Present,   Formality.FormalHigh,       ClauseType.Interrogative),    new string[] { "(ㅂ/습)니까?" } },
        { (Tense.Present,   Formality.FormalHigh,       ClauseType.Propositive),      new string[] { "(ㅂ/읍)시다" } },
        { (Tense.Present,   Formality.FormalLow,        ClauseType.Declarative),      new string[] { "(ㄴ/는)다" } },
        { (Tense.Present,   Formality.FormalLow,        ClauseType.Imperative),       new string[] { "(아/어)라" } },
        { (Tense.Present,   Formality.FormalLow,        ClauseType.Interrogative),    new string[] { "나?" } },
        { (Tense.Present,   Formality.FormalLow,        ClauseType.Propositive),      new string[] { "자" } },
        { (Tense.Present,   Formality.InformalHigh,     ClauseType.Declarative),      new string[] { "(아/어)요" } },
        { (Tense.Present,   Formality.InformalHigh,     ClauseType.Imperative),       new string[] { "(아/어)요" } },
        { (Tense.Present,   Formality.InformalHigh,     ClauseType.Interrogative),    new string[] { "(아/어)요?" } },
        { (Tense.Present,   Formality.InformalHigh,     ClauseType.Propositive),      new string[] { "(아/어)요" } },
        { (Tense.Present,   Formality.InformalLow,      ClauseType.Declarative),      new string[] { "(아/어)" } },
        { (Tense.Present,   Formality.InformalLow,      ClauseType.Imperative),       new string[] { "(아/어)" } },
        { (Tense.Present,   Formality.InformalLow,      ClauseType.Interrogative),    new string[] { "(아/어)?" } },
        { (Tense.Present,   Formality.InformalLow,      ClauseType.Propositive),      new string[] { "(아/어)" } },
        { (Tense.Future,    Formality.FormalHigh,       ClauseType.Declarative),      new string[] { "(ㅂ/습)니다" } },
        { (Tense.Future,    Formality.FormalHigh,       ClauseType.Imperative),       Array.Empty<string>() },
        { (Tense.Future,    Formality.FormalHigh,       ClauseType.Interrogative),    new string[] { "(ㅂ/습)니까?" } },
        { (Tense.Future,    Formality.FormalHigh,       ClauseType.Propositive),      Array.Empty<string>() },
        { (Tense.Future,    Formality.FormalLow,        ClauseType.Declarative),      new string[] { "다" } },
        { (Tense.Future,    Formality.FormalLow,        ClauseType.Imperative),       Array.Empty<string>() },
        { (Tense.Future,    Formality.FormalLow,        ClauseType.Interrogative),    new string[] { "냐?" } },
        { (Tense.Future,    Formality.FormalLow,        ClauseType.Propositive),      Array.Empty<string>() },
        { (Tense.Future,    Formality.InformalHigh,     ClauseType.Declarative),      new string[] { "예요" } },
        { (Tense.Future,    Formality.InformalHigh,     ClauseType.Imperative),       Array.Empty<string>() },
        { (Tense.Future,    Formality.InformalHigh,     ClauseType.Interrogative),    new string[] { "예요?" } },
        { (Tense.Future,    Formality.InformalHigh,     ClauseType.Propositive),      Array.Empty<string>() },
        { (Tense.Future,    Formality.InformalLow,      ClauseType.Declarative),      new string[] { "야" } },
        { (Tense.Future,    Formality.InformalLow,      ClauseType.Imperative),       Array.Empty<string>() },
        { (Tense.Future,    Formality.InformalLow,      ClauseType.Interrogative),    new string[] { "야?" } },
        { (Tense.Future,    Formality.InformalLow,      ClauseType.Propositive),      Array.Empty<string>() },
    };

    private static readonly Dictionary<(Tense, Formality, ClauseType), string> Map2 = new()
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
    public void GetSuffixTemplateStrings(string verbStem, in ConjugationParams conjugationParams, string[] arr)
    {
        if (string.IsNullOrEmpty(verbStem))
        {
            throw new ArgumentException("null or empty", nameof(verbStem));
        }

        ConvertParamsToSuffixes(conjugationParams, arr);
        ApplyCopulaLogic(verbStem, conjugationParams, arr);
    }

    private static void ConvertParamsToSuffixes(in ConjugationParams conjugationParams, string[] result)
    {
        //var count = 0;
        //if (conjugationParams.Honorific)
        //{
        //    count++;
        //}
        //if (conjugationParams.Tense is Tense.Future or Tense.Past)
        //{
        //    count++;
        //}
        //var key = (conjugationParams.Tense, conjugationParams.Formality, conjugationParams.ClauseType);
        //var end = Map[key];
        //if (end.Length > 0)
        //{
        //    count++;
        //}
        //var result = new string[count];

        //int i = 0;
        //if (conjugationParams.Honorific)
        //{
        //    result[i++] = "(으)시";
        //}
        //switch (conjugationParams.Tense)
        //{
        //    case Tense.Future:
        //        result[i++] = "(ㄹ/을) 거";
        //        break;
        //    case Tense.Past:
        //        result[i++] = "(았/었)";
        //        break;
        //    case Tense.Present:
        //        break;
        //}
        //if (end.Length > 0)
        //{
        //    result[i] = end;
        //}

        int i = 0;
        if (conjugationParams.Honorific)
        {
            result[i++] = "(으)시";
        }
        switch (conjugationParams.Tense)
        {
            case Tense.Future:
                result[i++] = "(ㄹ/을) 거";
                break;
            case Tense.Past:
                result[i++] = "(았/었)";
                break;
            case Tense.Present:
                break;
        }
        var key = (conjugationParams.Tense, conjugationParams.Formality, conjugationParams.ClauseType);
        //var end = Map[key];
        //if (end.Length > 0)
        //{
        //    result[i++] = end[0];
        //}
        //if (end.Length > 1)
        //{
        //    result[i] = end[1];
        //}

        var end = Map2[key];
        if (end.Length > 0)
        {
            result[i] = end;
        }
    }

    private static void ApplyCopulaLogic(string verbStem, in ConjugationParams conjugationParams, string[] suffixTemplateStrings)
    {
        if (conjugationParams.Tense == Tense.Present &&
            !conjugationParams.Honorific &&
            HangulUtil.RegularIdaVerbs != null &&
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
