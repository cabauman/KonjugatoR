namespace KoreanConjugator;

/// <summary>
/// Represents a suffix template parser.
/// </summary>
public class SuffixTemplateParser : ISuffixTemplateParser
{
    /// <inheritdoc/>
    public ProcessedSuffix Parse(string templateText)
    {
        return default;
    }

    /// <summary>
    /// Returns a suffix template object by parsing the template text.
    /// </summary>
    /// <param name="templateText">The template text.</param>
    /// <returns>The suffix template object.</returns>
    public static AEuSuffixTemplate ParseAEu(string templateText)
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

        return new AEuSuffixTemplate
        {
            TemplateText = templateText,
            StaticText = staticText,
            IsPastTense = badchimlessConnector[0] == '았'
        };
    }

    /// <summary>
    /// Returns a suffix template object by parsing the template text.
    /// </summary>
    /// <param name="templateText">The template text.</param>
    /// <returns>The suffix template object.</returns>
    public static BadchimDependentSuffixTemplate ParseBadchimDependent(string templateText)
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

        return new BadchimDependentSuffixTemplate
        {
            TemplateText = templateText,
            WordClass = wordClass,
            BadchimConnector = badchimConnector,
            BadchimlessConnector = badchimlessConnector,
            StaticText = staticText,
        };
    }

    private static bool TryParseWordClass(ReadOnlySpan<char> s, out int length, out int nextStartIndex)
    {
        // MemoryExtensions.Equals(s[..6], "A/V + ", StringComparison.OrdinalIgnoreCase)
        if (s.Length >= 6 && s[..6] is "A/V + ")
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
}
