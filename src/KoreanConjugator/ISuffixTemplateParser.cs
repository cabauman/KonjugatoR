namespace KoreanConjugator;

/// <summary>
/// A contract for parsing a suffix template.
/// </summary>
public interface ISuffixTemplateParser
{
    /// <summary>
    /// Parses the suffix template text into a suffix template object.
    /// </summary>
    /// <param name="templateText">The template text.</param>
    /// <returns>A suffix template object.</returns>
    ProcessedSuffix Parse(string templateText);
}
