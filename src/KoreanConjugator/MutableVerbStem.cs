namespace KoreanConjugator;

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
