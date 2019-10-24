using System;
using System.Collections.Generic;
using System.Text;

namespace KoreanConjugator
{
    public class MutableVerbStem
    {
        public MutableVerbStem(string value, bool hasHiddenBadchim)
        {
            Value = value;
            HasHiddenBadchim = hasHiddenBadchim;
        }

        public string Value { get; }

        public bool HasHiddenBadchim { get; }
    }
}
