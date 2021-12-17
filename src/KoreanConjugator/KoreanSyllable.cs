using System.Globalization;

namespace KoreanConjugator;

/// <summary>
/// Represents a syllable in Korean.
/// </summary>
public readonly struct KoreanSyllable : IEquatable<KoreanSyllable>, IEquatable<char>, IEquatable<int>, IComparable<KoreanSyllable>, IComparable<char>, IComparable<int>, IFormattable
{
    #region Character Code Constants

    /// <summary>
    /// 가.
    /// </summary>
    private const int FirstKoreanSyllableCharacterCode = 44032;

    /// <summary>
    /// 힣.
    /// </summary>
    private const int LastKoreanSyllableCharacterCode = 55203;

    /// <summary>
    /// ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ.
    /// </summary>
    private const int NumberOfInitials = 19;

    /// <summary>
    /// ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ.
    /// </summary>
    private const int NumberOfMedials = 21;

    /// <summary>
    /// \0ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ.
    /// </summary>
    private const int NumberOfFinals = 28;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="KoreanSyllable"/> struct.
    /// </summary>
    /// <param name="characterCode">The character code.</param>
    public KoreanSyllable(int characterCode)
    {
        if (!IsAKoreanSyllable(characterCode))
        {
            var message = $"Korean syllables have character codes between {FirstKoreanSyllableCharacterCode} and {LastKoreanSyllableCharacterCode}.";

            throw new ArgumentOutOfRangeException(nameof(characterCode), message);
        }

        CharacterCode = characterCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KoreanSyllable"/> struct.
    /// </summary>
    /// <param name="initial">The index of the initial jamo character.</param>
    /// <param name="medial">The index of the medial jamo character.</param>
    public KoreanSyllable(int initial, int medial)
        : this(initial, medial, 0)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KoreanSyllable"/> struct.
    /// </summary>
    /// <param name="initial">The index of the initial jamo character.</param>
    /// <param name="medial">The index of the medial jamo character.</param>
    /// <param name="final">The index of the final jamo character.</param>
    public KoreanSyllable(int initial, int medial, int final)
    {
        if (initial < 0 || initial >= NumberOfInitials)
        {
            throw new ArgumentOutOfRangeException(nameof(initial), $"The index of the initial character must be between 0 and {NumberOfInitials - 1}");
        }

        if (medial < 0 || medial >= NumberOfMedials)
        {
            throw new ArgumentOutOfRangeException(nameof(medial), $"The index of the medial character must be between 0 and {NumberOfMedials - 1}");
        }

        if (final < 0 || final >= NumberOfFinals)
        {
            throw new ArgumentOutOfRangeException(nameof(final), $"The index of the final character must be between 0 and {NumberOfFinals - 1}");
        }

        CharacterCode = (initial * NumberOfMedials * NumberOfFinals) + (medial * NumberOfFinals) + final + FirstKoreanSyllableCharacterCode;
    }

    #endregion

    #region Core Properties

    /// <summary>
    /// Gets the character code.
    /// </summary>
    public int CharacterCode { get; }

    /// <summary>
    /// Gets the initial.
    /// </summary>
    /// <remarks>This is a consonant, which is placed in the first of three possible positions.</remarks>
    public KoreanLetter Initial
    {
        get
        {
            var initialIndex = (CharacterCode - FirstKoreanSyllableCharacterCode) / (NumberOfMedials * NumberOfFinals);

            return KoreanLetter.GetKoreanLetterFromInitialIndex(initialIndex);
        }
    }

    /// <summary>
    /// Gets the medial.
    /// </summary>
    /// <remarks>This is a vowel, which is placed in the second of three possible positions.</remarks>
    public KoreanLetter Medial
    {
        get
        {
            var medialIndex = ((CharacterCode - FirstKoreanSyllableCharacterCode) % (NumberOfMedials * NumberOfFinals)) / NumberOfFinals;

            return KoreanLetter.GetKoreanLetterFromMedialIndex(medialIndex);
        }
    }

    /// <summary>
    /// Gets the final.
    /// </summary>
    /// <remarks>This is a consonant, which is placed in the third of three possible positions.</remarks>
    public KoreanLetter Final
    {
        get
        {
            var finalIndex = ((CharacterCode - FirstKoreanSyllableCharacterCode) % (NumberOfMedials * NumberOfFinals)) % NumberOfFinals;

            return KoreanLetter.GetKoreanLetterFromFinalIndex(finalIndex);
        }
    }

    /// <summary>
    /// Gets a value indicating whether this syllable contains a final.
    /// </summary>
    public bool HasFinal
    {
        get
        {
            var finalIndex = ((CharacterCode - FirstKoreanSyllableCharacterCode) % (NumberOfMedials * NumberOfFinals)) % NumberOfFinals;

            return finalIndex > 0;
        }
    }

    #endregion

    #region Operator Overloads

    /// <summary>
    /// Indicates whether the two specified <see cref="KoreanSyllable" /> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The seocond object to compare.</param>
    /// <returns><c>true</c> if the two <see cref="KoreanSyllable" /> objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(KoreanSyllable left, KoreanSyllable right) => Equals(left, right);

    /// <summary>
    /// Indicates whether the two specified <see cref="KoreanSyllable" /> objects are unequal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The seocond object to compare.</param>
    /// <returns><c>true</c> if the two <see cref="KoreanSyllable" /> objects are unequal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(KoreanSyllable left, KoreanSyllable right) => !Equals(left, right);

    #endregion

    /// <summary>
    /// Gets a value indicating whether the character code corresponds to a Korean syllable.
    /// </summary>
    /// <param name="characterCode">The character code.</param>
    /// <returns><c>true</c> if the character code corresponds to a Korean syllable; otherwise, <c>false</c>.</returns>
    public static bool IsAKoreanSyllable(int characterCode)
    {
        return characterCode >= FirstKoreanSyllableCharacterCode && characterCode <= LastKoreanSyllableCharacterCode;
    }

    #region Comparisons

    /// <inheritdoc/>
    public bool Equals(KoreanSyllable other) => CharacterCode == other.CharacterCode;

    /// <inheritdoc/>
    public bool Equals(char c) => CharacterCode == c;

    /// <inheritdoc/>
    public bool Equals(int i) => CharacterCode == i;

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is KoreanSyllable koreanSyllable && Equals(koreanSyllable);

    /// <inheritdoc/>
    public override int GetHashCode() => CharacterCode.GetHashCode();

    /// <inheritdoc/>
    public int CompareTo(KoreanSyllable other) => CharacterCode.CompareTo(other.CharacterCode);

    /// <inheritdoc/>
    public int CompareTo(char c) => CharacterCode.CompareTo(c);

    /// <inheritdoc/>
    public int CompareTo(int i) => CharacterCode.CompareTo(i);

    #endregion

    #region String Formatting

    /// <inheritdoc/>
    public override string ToString()
    {
        // TODO: Document what S means.
        return ToString("S", CultureInfo.CurrentCulture);
    }

    /// <inheritdoc/>
    public string ToString(string format, IFormatProvider formatProvider)
    {
        format = format.Replace("S", ((char)CharacterCode).ToString());

        return format;
    }

    #endregion
}
