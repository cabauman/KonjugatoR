using System.Text;

namespace KoreanConjugator;

/// <summary>
/// Represents a mutable verb stem.
/// </summary>
public ref struct MutableVerbStem
{
    /// <summary>
    /// Gets the core verb stem.
    /// </summary>
    public string Stem { get; set; }

    /// <summary>
    /// Gets the string builder used to build the conjugated form.
    /// </summary>
    public StringBuilder Builder { get; init; }

    /// <summary>
    /// Gets a value indicating whether the verb stem has a hidden badchim.
    /// </summary>
    public bool HasHiddenBadchim { get; set; }
}
