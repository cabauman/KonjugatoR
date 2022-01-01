using System.Text.RegularExpressions;

namespace KoreanConjugator;

/// <summary>
/// Represents a suffix template parser.
/// </summary>
public class SuffixTemplateParser : ISuffixTemplateParser
{
    private static readonly Regex aEuaRegex = CreateAEuRegex();
    private static readonly Regex badchimDependentRegex = CreateBadchimDependentRegex();

    /// <inheritdoc/>
    public SuffixTemplate Parse(string templateText)
    {
        // V/A + [ㄹ/을] 거
        if (templateText.Contains("(아/어)") || templateText.Contains("(았/었)"))
        {
            return ParseAEuTemplate(templateText);
        }

        var match = badchimDependentRegex.Match(templateText);
        if (!match.Success)
        {
            throw new ArgumentException("Suffix doesn't match the template format.");
        }

        var wordClassResult = match.Groups["WordClass"].Value;
        var badchimlessConnector = match.Groups["NoBadchim"].Value;
        var badchimConnector = match.Groups["Badchim"].Value;
        var staticText = match.Groups["StaticText"].Value;

        var template = new BadchimDependentSuffixTemplate(templateText, wordClassResult, badchimConnector, badchimlessConnector, staticText);

        return template;
    }

    private static SuffixTemplate ParseAEuTemplate(string templateText)
    {
        var match = aEuaRegex.Match(templateText);
        if (!match.Success)
        {
            throw new ArgumentException("Suffix doesn't match the template format.");
        }

        // TODO: Either use or get rid of these assinments.
        var wordClassResult = match.Groups["WordClass"].Value;
        var aResult = match.Groups["AGroup"].Value;
        var euResult = match.Groups["EuGroup"].Value;
        var staticTextResult = match.Groups["StaticText"].Value;

        var pastTense = aResult.Equals("았");
        return new AEuSuffixTemplate(templateText, staticTextResult, pastTense);
    }

    private static Regex CreateAEuRegex()
    {
        string wordClassGroup = "(?<WordClass>A/V|A|V)";
        string optionalWordClassGroup = $"(?:{wordClassGroup} \\+ )?";
        string staticTextGroup = "(?<StaticText>.*)";
        string aGroup = "(?<AGroup>[아|았])";
        string euGroup = "(?<EuGroup>[어|었])";
        string aEuGroup = $"\\({aGroup}/{euGroup}\\)";
        string pattern = $"{optionalWordClassGroup}{aEuGroup}{staticTextGroup}";

        return new(pattern, RegexOptions.Compiled);
    }

    private static Regex CreateBadchimDependentRegex()
    {
        string wordClassGroup = "(?<WordClass>A/V|A|V)";
        string optionalWordClassGroup = $"(?:{wordClassGroup} \\+ )?";
        string staticTextGroup = "(?<StaticText>.*)";
        string noBadchim = "(?<NoBadchim>[ㅂ|ㄹ|ㄴ|ㅁ|가-힣])";
        string badchim = "(?<Badchim>[가-힣])";
        string optionalNoBadchim = $"(?:{noBadchim}/)?";
        string badchimNoBadchim = $"\\({optionalNoBadchim}{badchim}\\)";
        string optionalBadchimNoBadchim = $"(?:{badchimNoBadchim})?";
        string pattern = $"{optionalWordClassGroup}{optionalBadchimNoBadchim}{staticTextGroup}";

        return new(pattern, RegexOptions.Compiled);
    }
}
