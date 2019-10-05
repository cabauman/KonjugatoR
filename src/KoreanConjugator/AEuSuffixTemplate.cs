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
        /// <param name="staticText">The portion of the template text that doesn't change.</param>
        public AEuSuffixTemplate(string text, string staticText)
            : base(text, staticText)
        {
        }

        /// <inheritdoc/>
        public override string ChooseSuffixVariant(string precedingText)
        {
            // TODO: Need to handle 았/었 too.
            string connector = "어";
            int index = precedingText.Length - 1;
            while (index >= 0)
            {
                var medial = HangulUtil.Medial(precedingText[index]);
                if (medial != 'ㅡ')
                {
                    if (medial == 'ㅏ' || medial == 'ㅗ')
                    {
                        connector = "아";
                    }

                    break;
                }

                --index;
            }

            return string.Concat(connector, StaticText);
        }
    }
}
