using System;
using System.Text.RegularExpressions;

namespace KoreanConjugator;

/// <summary>
/// Represents a suffix template parser.
/// </summary>
public class SuffixTemplateParser2
{
    public static SuffixTemplate Parse(string templateText)
    {
        if ((templateText.Contains("(아/어)") || templateText.Contains("(았/었)")))
        {
            //var template = ParseAEuTemplate(templateText);
            //return template.ChooseSuffixVariant(precedingText);
            return ParseAEuTemplate(templateText);
        }
        else
        {
            //var template = ParseBadchimDependentTemplate(templateText);
            // template.ChooseSuffixVariant(precedingText);
            return ParseAEuTemplate(templateText);
        }
    }

    public static void Parse2(Span<char> span)
    {

    }

    public static SuffixTemplate ParseAEuTemplate(string templateText)
    {
        var s = templateText.AsSpan();
        ReadOnlySpan<char> wordClass = default;
        ReadOnlySpan<char> badchimConnector = default;
        ReadOnlySpan<char> badchimlessConnector = default;
        ReadOnlySpan<char> staticText = default;

        if (TryParseWordClass(s, out var length, out var nextStartIndex))
        {
            wordClass = s[..length];
        }
        if (TryParseDynamicText(s, nextStartIndex, out var badchimlessConnectorIndex, out var badchimConnectorIndex, out nextStartIndex))
        {
            if (badchimlessConnectorIndex > 0)
            {
                badchimlessConnector = s.Slice(badchimlessConnectorIndex, 1);
            }
            badchimConnector = s.Slice(badchimConnectorIndex, 1);
        }
        if (nextStartIndex < s.Length)
        {
            // TODO: Make sure remaining text is valid (Korean syllables).
            staticText = s[nextStartIndex..];
        }

        //return new AEuSuffixTemplate(staticText, badchimlessConnector[0] == '았');
        return new SuffixTemplate(templateText, wordClass, badchimlessConnector, badchimConnector, staticText);
    }

    private static bool TryParseWordClass(ReadOnlySpan<char> s, out int length, out int nextStartIndex)
    {
        if (s.Length >= 6 && s[..6] == "A/V + ")
        {
            length = 3;
            nextStartIndex = 6;
            return true;
        }
        if (s.Length >= 4 && s[..4] is "A + " or "V + ")
        {
            length = 1;
            nextStartIndex = 4;
            return true;
        }
        length = 0;
        nextStartIndex = 0;
        return false;
    }

    private static bool TryParseDynamicText(
        ReadOnlySpan<char> s,
        int start,
        out int badchimlessConnectorIndex,
        out int badchimConnectorIndex,
        out int nextStartIndex)
    {
        var index = nextStartIndex = start;
        badchimlessConnectorIndex = -1;
        badchimConnectorIndex = -1;

        if (s[index] != '(')
        {
            return false;
        }
        index += 1;
        if (!HangulUtil.IsSyllable(s[index]) && !HangulUtil.IsModernCompatibilityLetter(s[index]))
        {
            // TODO: Be more strict (ㅂ|ㄹ|ㄴ|ㅁ|가-힣)
            return false;
        }
        index += 1;
        if (s[index] == ')')
        {
            nextStartIndex = index + 1;
            badchimConnectorIndex = index - 1;
            return true;
        }
        if (s[index] != '/')
        {
            return false;
        }
        index += 1;
        if (!HangulUtil.IsSyllable(s[index]))
        {
            return false;
        }
        index += 1;
        if (s[index] != ')')
        {
            return false;
        }

        // 012345
        // (A/B)
        nextStartIndex = index + 1;
        badchimConnectorIndex = index - 1;
        badchimlessConnectorIndex = index - 3;
        return true;
    }

    public static BadchimDependentSuffixTemplate ParseBadchimDependentTemplate(string templateText)
    {
        return new BadchimDependentSuffixTemplate();
    }
}
