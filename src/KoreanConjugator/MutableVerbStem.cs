using System.Text;

namespace KoreanConjugator;

/// <summary>
/// Represents a mutable verb stem.
/// </summary>
public readonly struct MutableVerbStem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MutableVerbStem"/> class.
    /// </summary>
    /// <param name="value">The verb stem.</param>
    /// <param name="hasHiddenBadchim">A value indicating whether the verb stem has a hidden badchim.</param>
    public MutableVerbStem(StringBuilder value, bool hasHiddenBadchim)
    {
        Value = value;
        HasHiddenBadchim = hasHiddenBadchim;
    }

    /// <summary>
    /// Gets the verb stem.
    /// </summary>
    public StringBuilder Value { get; }

    /// <summary>
    /// Gets a value indicating whether the verb stem has a hidden badchim.
    /// </summary>
    public bool HasHiddenBadchim { get; }
}
