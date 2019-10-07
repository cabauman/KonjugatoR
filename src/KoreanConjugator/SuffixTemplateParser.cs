using System;
using System.Text.RegularExpressions;

namespace KoreanConjugator
{
    /// <summary>
    /// Represents a suffix template parser.
    /// </summary>
    public class SuffixTemplateParser : ISuffixTemplateParser
    {
        /// <inheritdoc/>
        public SuffixTemplate Parse(string templateText)
        {
            // V/A + [ㄹ/을] 거

            string wordClass = "(?<WordClass>A/V|A|V)";
            string optionalWordClass = $"(?:{wordClass} \\+ )?";
            string noBadchim = "(?<NoBadchim>[ㅂ|ㄹ|ㄴ|ㅁ|가-힣])";
            string badchim = "(?<Badchim>[가-힣])";
            string optionalNoBadchim = $"(?:{noBadchim}/)?";
            string badchimNoBadchim = $"\\({optionalNoBadchim}{badchim}\\)";
            string optionalBadchimNoBadchim = $"(?:{badchimNoBadchim})?";
            string staticTextGroup = "(?<StaticText>.*)";
            string pattern = $"{optionalWordClass}{optionalBadchimNoBadchim}{staticTextGroup}";

            var regex = new Regex(pattern);
            var match = regex.Match(templateText);

            if (!match.Success)
            {
                throw new ArgumentException("Suffix doesn't match the template format.");
            }

            var wordClassResult = match.Groups["WordClass"].Value;
            var badchimlessConnector = match.Groups["NoBadchim"].Value;
            var badchimConnector = match.Groups["Badchim"].Value;
            var staticText = match.Groups["StaticText"].Value;

            if (badchimlessConnector.Equals("아") && badchimConnector.Equals("어"))
            {
                return new AEuSuffixTemplate(templateText, staticText);
            }

            GroupCollection collection = match.Groups;

            // Note that group 0 is always the whole match
            for (int i = 1; i < collection.Count; i++)
            {
                Group group = collection[i];
                string name = regex.GroupNameFromNumber(i);
                Console.WriteLine("{0}: {1} {2}", name, group.Success, group.Value);
            }

            var template = new BadchimDependentSuffixTemplate(templateText, wordClassResult, badchimConnector, badchimlessConnector, staticText);

            return template;
        }
    }
}
