namespace KoreanConjugator;

/// <summary>
/// Represents the result of a conjugation.
/// </summary>
public readonly struct ConjugationResult
{
    public ConjugationResult(string value, IReadOnlyList<string> steps)
    {
        Value = value;
        Steps = steps;
        // TODO: Use this value or get rid of it.
        Type = null;
    }

    /// <summary>
    /// Gets the resulting conjugated form.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets the list of steps that the conjugator took to get the resulting value.
    /// </summary>
    public IReadOnlyList<string> Steps { get; }

    /// <summary>
    /// Gets whether the verb or adjective is regular or irregular.
    /// </summary>
    public string? Type { get; }
}
