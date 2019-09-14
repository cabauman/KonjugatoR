using System;
using System.Collections.Generic;
using System.Linq;

namespace KoreanConjugator
{
    public abstract class SuffixTemplate
    {
        public SuffixTemplate(string text)
        {
            Template = text;
        }

        public string Template { get; }

        public char[] Syllables { get; protected set; }

        public string StaticPortion { get; }

        public abstract string ChooseSuffixVariant(string precedingText);

        public void AssignSyllables(string connector)
        {
        }
    }

    public class AEuSuffixTemplate : SuffixTemplate
    {
        public AEuSuffixTemplate(string text)
            : base(text)
        {
        }

        public override string ChooseSuffixVariant(string precedingText)
        {
            // TODO: Need to handle 았/었 too.
            string connector = "어";
            if (precedingText.Last() == 'ᅡ' ||
                precedingText.Last() == 'ᅩ')
            {
                connector = "아";
            }

            return string.Concat(connector, StaticPortion);
        }
    }

    public class DefaultSuffixTemplate : SuffixTemplate
    {
        public DefaultSuffixTemplate(string text)
            : base(text)
        {
        }

        public string BadchimConnector { get; }

        public string BadchimlessConnector { get; }

        public override string ChooseSuffixVariant(string precedingText)
        {
            string connector = string.Empty;
            if (BadchimConnector == null) // doesn't depend on badchim
            {
                // No modifications
            }
            else
            {
                if (HangulUtil.HasFinal(precedingText.Last())) // not == ㄹ
                {
                    // Choose badchim connector
                    connector = BadchimConnector;
                }
                else
                {
                    // Choose badchimless connector (it will be equal to string.Empty if none)
                    connector = BadchimlessConnector;
                }
            }

            return string.Concat(connector, StaticPortion);
        }
    }
}
