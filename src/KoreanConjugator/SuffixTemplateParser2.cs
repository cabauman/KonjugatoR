using System;

namespace KoreanConjugator;

/// <summary>
/// Represents a suffix template parser.
/// </summary>
public class SuffixTemplateParser2
{
    public static string ParseAEu(string precedingText, string templateText)
    {
        AEuSuffixTemplate suffixTemplate = SuffixTemplateParser.ParseAEu(templateText);

        string connector;
        if (precedingText.Equals("하"))
        {
            connector = suffixTemplate.IsPastTense ? "였" : "여";
        }
        else
        {
            connector = suffixTemplate.IsPastTense ? "었" : "어";

            int index = precedingText.Length - 1;
            while (index >= 0)
            {
                var medial = HangulUtil.Medial(precedingText[index]);
                if (medial != 'ᅳ')
                {
                    if (medial == 'ᅡ' || medial == 'ᅩ')
                    {
                        connector = suffixTemplate.IsPastTense ? "았" : "아";
                    }

                    break;
                }

                --index;
            }
        }

        return string.Concat(connector, suffixTemplate.StaticText);
    }

    public static string ParseBadchimDependent(string precedingText, string templateText)
    {
        BadchimDependentSuffixTemplate suffixTemplate = SuffixTemplateParser.ParseBadchimDependent(templateText);

        ReadOnlySpan<char> connector = string.Empty;
        if (suffixTemplate.BadchimConnector == string.Empty)
        {
            // Doesn't depend on a badchim. No modifications.
        }
        else
        {
            if (HangulUtil.Final(precedingText[^1]) is not 'ᆯ' and not '\0')
            {
                // Choose badchim connector
                connector = suffixTemplate.BadchimConnector;
            }
            else
            {
                // Choose badchimless connector (it will be equal to string.Empty if none)
                connector = suffixTemplate.BadchimlessConnector;
            }
        }

        return string.Concat(connector, suffixTemplate.StaticText);
    }
}
