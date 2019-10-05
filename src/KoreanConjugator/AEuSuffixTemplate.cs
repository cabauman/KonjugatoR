using System.Linq;

namespace KoreanConjugator
{
    /// <summary>
    /// Represents a suffix template that starts with the 아/어 grammatical principal.
    /// </summary>
    public class AEuSuffixTemplate : SuffixTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AEuSuffixTemplate"/> class.
        /// </summary>
        /// <param name="text">The template text.</param>
        public AEuSuffixTemplate(string text)
            : base(text)
        {
        }

        /// <inheritdoc/>
        public override string ChooseSuffixVariant(string precedingText)
        {
            // TODO: Need to handle 았/었 too.
            string connector = "어";
            if (precedingText.Last() == 'ᅡ' ||
                precedingText.Last() == 'ᅩ')
            {
                connector = "아";
            }

            return string.Concat(connector, StaticText);
        }
    }
}
