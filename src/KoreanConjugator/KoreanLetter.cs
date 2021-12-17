using System.Globalization;

namespace KoreanConjugator;

/// <summary>
/// Represents a single letter of Korean - a jamo.
/// </summary>
public readonly struct KoreanLetter : IEquatable<KoreanLetter>, IEquatable<char>, IEquatable<int>, IComparable<KoreanLetter>, IComparable<char>, IComparable<int>, IFormattable
{
    #region Instantiation Shortcuts

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static readonly KoreanLetter None = new (-1);
    public static readonly KoreanLetter Giyeok = new ('ᄀ');
    public static readonly KoreanLetter SsangGiyeok = new ('ᄁ');
    public static readonly KoreanLetter Nieun = new ('ᄂ');
    public static readonly KoreanLetter Digeut = new ('ᄃ');
    public static readonly KoreanLetter SsangDigeut = new ('ᄄ');
    public static readonly KoreanLetter Rieul = new ('ᄅ');
    public static readonly KoreanLetter Mieum = new ('ᄆ');
    public static readonly KoreanLetter Bieup = new ('ᄇ');
    public static readonly KoreanLetter SsangBieup = new ('ᄈ');
    public static readonly KoreanLetter Shiot = new ('ᄉ');
    public static readonly KoreanLetter SsangShiot = new ('ᄊ');
    public static readonly KoreanLetter Ieung = new ('ᄋ');
    public static readonly KoreanLetter Jieut = new ('ᄌ');
    public static readonly KoreanLetter SsangJieut = new ('ᄍ');
    public static readonly KoreanLetter Chieut = new ('ᄎ');
    public static readonly KoreanLetter Kieuk = new ('ᄏ');
    public static readonly KoreanLetter Tieut = new ('ᄐ');
    public static readonly KoreanLetter Pieup = new ('ᄑ');
    public static readonly KoreanLetter Hieut = new ('ᄒ');

    public static readonly KoreanLetter A = new ('ᅡ');
    public static readonly KoreanLetter Ae = new ('ᅢ');
    public static readonly KoreanLetter Ya = new ('ᅣ');
    public static readonly KoreanLetter Yae = new ('ᅤ');
    public static readonly KoreanLetter Eo = new ('ᅥ');
    public static readonly KoreanLetter E = new ('ᅦ');
    public static readonly KoreanLetter Yeo = new ('ᅧ');
    public static readonly KoreanLetter Ye = new ('ᅨ');
    public static readonly KoreanLetter O = new ('ᅩ');
    public static readonly KoreanLetter Wa = new ('ᅪ');
    public static readonly KoreanLetter Wae = new ('ᅫ');
    public static readonly KoreanLetter Oe = new ('ᅬ');
    public static readonly KoreanLetter Yo = new ('ᅭ');
    public static readonly KoreanLetter U = new ('ᅮ');
    public static readonly KoreanLetter Wo = new ('ᅯ');
    public static readonly KoreanLetter We = new ('ᅰ');
    public static readonly KoreanLetter Wi = new ('ᅱ');
    public static readonly KoreanLetter Yu = new ('ᅲ');
    public static readonly KoreanLetter Eu = new ('ᅳ');
    public static readonly KoreanLetter Ui = new ('ᅴ');
    public static readonly KoreanLetter I = new ('ᅵ');

    public static readonly KoreanLetter GiyeokBatchim = new ('ᆨ');
    public static readonly KoreanLetter SsangGiyeokBatchim = new ('ᆩ');
    public static readonly KoreanLetter GiyeokShiotBatchim = new ('ᆪ');
    public static readonly KoreanLetter NieunBatchim = new ('ᆫ');
    public static readonly KoreanLetter NieunJieutBatchim = new ('ᆬ');
    public static readonly KoreanLetter NieunHieutBatchim = new ('ᆭ');
    public static readonly KoreanLetter DigeutBatchim = new ('ᆮ');
    public static readonly KoreanLetter RieulBatchim = new ('ᆯ');
    public static readonly KoreanLetter RieulGiyeokBatchim = new ('ᆰ');
    public static readonly KoreanLetter RieulMieumBatchim = new ('ᆱ');
    public static readonly KoreanLetter RieulBieupBatchim = new ('ᆲ');
    public static readonly KoreanLetter RieulShiotBatchim = new ('ᆳ');
    public static readonly KoreanLetter RieulTieutBatchim = new ('ᆴ');
    public static readonly KoreanLetter RieulPieupBatchim = new ('ᆵ');
    public static readonly KoreanLetter RieulHieutBatchim = new ('ᆶ');
    public static readonly KoreanLetter MieumBatchim = new ('ᆷ');
    public static readonly KoreanLetter BieupBatchim = new ('ᆸ');
    public static readonly KoreanLetter BieupShiotBatchim = new ('ᆹ');
    public static readonly KoreanLetter ShiotBatchim = new ('ᆺ');
    public static readonly KoreanLetter SsangShiotBatchim = new ('ᆻ');
    public static readonly KoreanLetter IeungBatchim = new ('ᆼ');
    public static readonly KoreanLetter JieutBatchim = new ('ᆽ');
    public static readonly KoreanLetter ChieutBatchim = new ('ᆾ');
    public static readonly KoreanLetter KieukBatchim = new ('ᆿ');
    public static readonly KoreanLetter TieutBatchim = new ('ᇀ');
    public static readonly KoreanLetter PieupBatchim = new ('ᇁ');
    public static readonly KoreanLetter HieutBatchim = new ('ᇂ');
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    #endregion

    #region Character Code Constants

    private const int FirstKoreanLetterCharacterCode = 4352;
    private const int LastKoreanLetterCharacterCode = 4607;

    private const int FirstModernInitialCharacterCode = 4352;
    private const int LastModernInitialCharacterCode = 4370;

    private const int FirstModernMedialCharacterCode = 4449;
    private const int LastModernMedialCharacterCode = 4469;

    private const int FirstModernFinalCharacterCode = 4520;
    private const int LastModernFinalCharacterCode = 4546;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="KoreanLetter"/> struct.
    /// </summary>
    /// <param name="characterCode">The character code.</param>
    public KoreanLetter(int characterCode)
    {
        if (!IsAKoreanLetter(characterCode) && characterCode != -1)
        {
            var message = $"Korean letters have character codes between {FirstKoreanLetterCharacterCode} and {LastKoreanLetterCharacterCode}.";

            throw new ArgumentOutOfRangeException(nameof(characterCode), message);
        }

        CharacterCode = characterCode;
    }

    #endregion

    #region Core Properties

    /// <summary>
    /// Gets the character code.
    /// </summary>
    public int CharacterCode { get; }

    #endregion

    #region Operator Overloads

    /// <summary>
    /// Indicates whether the two specified <see cref="KoreanLetter" /> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The seocond object to compare.</param>
    /// <returns><c>true</c> if the two <see cref="KoreanLetter" /> objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(KoreanLetter left, KoreanLetter right) => Equals(left, right);

    /// <summary>
    /// Indicates whether the two specified <see cref="KoreanLetter" /> objects are unequal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The seocond object to compare.</param>
    /// <returns><c>true</c> if the two <see cref="KoreanLetter" /> objects are unequal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(KoreanLetter left, KoreanLetter right) => !Equals(left, right);

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets the letter corresponding to the initial index.
    /// </summary>
    /// <param name="initialIndex">The index of the initial.</param>
    /// <returns>The corresponding letter.</returns>
    public static KoreanLetter GetKoreanLetterFromInitialIndex(int initialIndex)
    {
        if (!IsAModernInitial(FirstModernInitialCharacterCode + initialIndex))
        {
            var message = $"Korean initials are numbered between 0 and {LastModernInitialCharacterCode - FirstModernInitialCharacterCode}";

            throw new ArgumentOutOfRangeException(nameof(initialIndex), message);
        }

        return new KoreanLetter(FirstModernInitialCharacterCode + initialIndex);
    }

    /// <summary>
    /// Gets the letter corresponding to the medial index.
    /// </summary>
    /// <param name="medialIndex">The index of the medial.</param>
    /// <returns>The corresponding letter.</returns>
    public static KoreanLetter GetKoreanLetterFromMedialIndex(int medialIndex)
    {
        if (!IsAModernMedial(FirstModernMedialCharacterCode + medialIndex))
        {
            var message = $"Korean medials are numbered between 0 and {LastModernMedialCharacterCode - FirstModernMedialCharacterCode}";

            throw new ArgumentOutOfRangeException(nameof(medialIndex), message);
        }

        return new KoreanLetter(FirstModernMedialCharacterCode + medialIndex);
    }

    /// <summary>
    /// Gets the letter corresponding to the final index.
    /// </summary>
    /// <param name="finalIndex">The index of the final.</param>
    /// <returns>The corresponding Korean letter.</returns>
    public static KoreanLetter GetKoreanLetterFromFinalIndex(int finalIndex)
    {
        if (finalIndex == 0)
        {
            return new KoreanLetter(-1);
        }

        if (!IsAModernFinal(FirstModernFinalCharacterCode + finalIndex - 1))
        {
            var message = $"Korean finals are numbered between 1 and {LastModernFinalCharacterCode - FirstModernFinalCharacterCode + 1}";

            throw new ArgumentOutOfRangeException(nameof(finalIndex), message);
        }

        return new KoreanLetter(FirstModernFinalCharacterCode + finalIndex - 1);
    }

    /// <summary>
    /// Gets a value indicating whether the character code corresponds to a Korean letter.
    /// </summary>
    /// <param name="characterCode">The character code.</param>
    /// <returns><c>true</c> if the character code corresponds to a Korean letter; otherwise, <c>false</c>.</returns>
    public static bool IsAKoreanLetter(int characterCode)
    {
        return characterCode >= FirstKoreanLetterCharacterCode && characterCode <= LastKoreanLetterCharacterCode;
    }

    /// <summary>
    /// Gets a value indicating whether the character code corresponds to a common initial.
    /// </summary>
    /// <param name="characterCode">The character code.</param>
    /// <returns><c>true</c> if the character code corresponds to a common initial; otherwise, <c>false</c>.</returns>
    public static bool IsAModernInitial(int characterCode)
    {
        return characterCode >= FirstModernInitialCharacterCode && characterCode <= LastModernInitialCharacterCode;
    }

    /// <summary>
    /// Gets a value indicating whether the character code corresponds to a common medial.
    /// </summary>
    /// <param name="characterCode">The character code.</param>
    /// <returns><c>true</c> if the character code corresponds to a common medial; otherwise, <c>false</c>.</returns>
    public static bool IsAModernMedial(int characterCode)
    {
        return characterCode >= FirstModernMedialCharacterCode && characterCode <= LastModernMedialCharacterCode;
    }

    /// <summary>
    /// Gets a value indicating whether the character code corresponds to a common final.
    /// </summary>
    /// <param name="characterCode">The character code.</param>
    /// <returns><c>true</c> if the character code corresponds to a common final; otherwise, <c>false</c>.</returns>
    public static bool IsAModernFinal(int characterCode)
    {
        return characterCode >= FirstModernFinalCharacterCode && characterCode <= LastModernFinalCharacterCode;
    }

    #endregion

    #region Comparisons

    /// <inheritdoc/>
    public bool Equals(KoreanLetter other) => CharacterCode == other.CharacterCode;

    /// <inheritdoc/>
    public bool Equals(char c) => CharacterCode == c;

    /// <inheritdoc/>
    public bool Equals(int i) => CharacterCode == i;

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is KoreanLetter koreanLetter && Equals(koreanLetter);

    /// <inheritdoc/>
    public override int GetHashCode() => CharacterCode.GetHashCode();

    /// <inheritdoc/>
    public int CompareTo(KoreanLetter other) => CharacterCode.CompareTo(other.CharacterCode);

    /// <inheritdoc/>
    public int CompareTo(char c) => CharacterCode.CompareTo(c);

    /// <inheritdoc/>
    public int CompareTo(int i) => CharacterCode.CompareTo(i);

    #endregion

    #region String Formatting

    /// <inheritdoc/>
    public override string ToString()
    {
        // TODO: Document what H means.
        return ToString("H", CultureInfo.CurrentCulture);
    }

    /// <inheritdoc/>
    public string ToString(string format, IFormatProvider formatProvider)
    {
        format = format.Replace("H", ((char)CharacterCode).ToString());

        return format;
    }

    #endregion
}
