using System.Collections.Generic;

namespace KoreanConjugator
{
    /// <summary>
    /// Represents the result of a conjugation.
    /// </summary>
    public readonly struct ConjugationResult
    {
        internal ConjugationResult(string value, List<string> steps)
        {
            Value = value;
            Steps = steps;
        }

        /// <summary>
        /// Gets the resulting conjugated form.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets the list of steps that the conjugator took to get the resulting value.
        /// </summary>
        public IList<string> Steps { get; }
    }
}
