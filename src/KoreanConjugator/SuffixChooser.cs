using System;
using System.Text;

namespace KoreanConjugator;

/// <summary>
/// Represents a suffix template parser.
/// </summary>
public class SuffixChooser
{
    /// <summary>
    /// Returns a suffix template object by parsing the template text.
    /// </summary>
    /// <param name="precedingText">The text that precedes the suffix template.</param>
    /// <param name="templateText">The template text.</param>
    /// <returns>The suffix template object.</returns>
    public static ProcessedSuffix Choose(StringBuilder precedingText, string templateText)
    {
        if (templateText.Contains("(아/어)") || templateText.Contains("(았/었)"))
        {
            return ParseAEuSuffix(precedingText, templateText);
        }

        return ParseBadchimDependentSuffix(precedingText, templateText);
    }

    private static ProcessedSuffix ParseAEuSuffix(StringBuilder precedingText, string templateText)
    {
        AEuSuffixTemplate aEuTemplate = SuffixTemplateParser.ParseAEu(templateText);

        ReadOnlySpan<char> connector;
        if (precedingText[^1] == '하')
        {
            connector = aEuTemplate.IsPastTense ? "였" : "여";
        }
        else
        {
            connector = aEuTemplate.IsPastTense ? "었" : "어";

            int index = precedingText.Length - 1;
            while (index >= 0)
            {
                var medial = HangulUtil.Medial(precedingText[index]);
                if (medial != 'ᅳ')
                {
                    if (medial == 'ᅡ' || medial == 'ᅩ')
                    {
                        connector = aEuTemplate.IsPastTense ? "았" : "아";
                    }

                    break;
                }

                --index;
            }
        }

        return new ProcessedSuffix
        {
            TemplateText = templateText,
            Connector = connector,
            StaticText = aEuTemplate.StaticText
        };
    }

    private static ProcessedSuffix ParseBadchimDependentSuffix(StringBuilder precedingText, string templateText)
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

        return new ProcessedSuffix
        {
            TemplateText = templateText,
            Connector = connector,
            StaticText = suffixTemplate.StaticText
        };
    }
}
