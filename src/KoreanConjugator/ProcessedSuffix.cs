using System.Text;

namespace KoreanConjugator;

/// <summary>
/// Represents a template where the resulting suffix depends on a grammatical principle and the preceding text.
/// </summary>
public readonly ref struct ProcessedSuffix
{
    /// <summary>
    /// Gets the template text.
    /// </summary>
    public string TemplateText { get; init; }

    /// <summary>
    /// Gets the portion of the template text that doesn't change.
    /// </summary>
    public ReadOnlySpan<char> StaticText { get; init; }

    /// <summary>
    /// Gets the connector.
    /// </summary>
    public ReadOnlySpan<char> Connector { get; init; }

    /// <summary>
    /// Gets the first character of the connector if the connector is not empty; otherwise, the first character of the static text.
    /// </summary>
    public char FirstChar => Connector.Length > 0 ? Connector[0] : StaticText[0];

    internal DualSpanEnumerator Chars => new(Connector, StaticText);
}

internal static class ProcessedSuffixExtensions
{
    public static StringBuilder Append(this StringBuilder sb, ProcessedSuffix processedSuffix)
    {
        sb.Append(processedSuffix.Connector);
        sb.Append(processedSuffix.StaticText);
        return sb;
    }

    public static DualSpanEnumerator Skip(this DualSpanEnumerator @this, int count)
    {
        for (var i = 0; i < count; i++)
        {
            @this.MoveNext();
        }
        return @this;
    }
}

internal ref struct DualSpanEnumerator
{
    private readonly ReadOnlySpan<char> _span1;
    private readonly ReadOnlySpan<char> _span2;
    private int _index;

    public DualSpanEnumerator(ReadOnlySpan<char> span1, ReadOnlySpan<char> span2)
    {
        _span1 = span1;
        _span2 = span2;
    }

    public bool MoveNext()
    {
        if (_index < _span1.Length)
        {
            Current = _span1[_index];
            _index++;
            return true;
        }

        var index = _index - _span1.Length;
        if (index < _span2.Length)
        {
            Current = _span2[index];
            _index++;
            return true;
        }

        return false;
    }

    public char Current { get; private set; }

    public readonly DualSpanEnumerator GetEnumerator() => this;
}
