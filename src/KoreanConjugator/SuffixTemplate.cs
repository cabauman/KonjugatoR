namespace KoreanConjugator
{
    /// <summary>
    /// Represents a template where the resulting suffix depends on a grammatical principle and the preceding text.
    /// </summary>
    public abstract class SuffixTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuffixTemplate"/> class.
        /// </summary>
        /// <param name="text">The template text.</param>
        public SuffixTemplate(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Gets the template text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the portion of the template text that doesn't change.
        /// </summary>
        public string StaticText { get; }

        /// <summary>
        /// Gets the suffix text based off of the template and preceding text.
        /// </summary>
        /// <param name="precedingText">The text that the suffix will be attached to.</param>
        /// <returns>The suffix text.</returns>
        public abstract string ChooseSuffixVariant(string precedingText);
    }
}
