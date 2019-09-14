using System;
using System.Globalization;

namespace KoreanConjugator
{
    /// <summary>
    /// Represents a single letter of Korean - a jamo.
    /// </summary>
    public readonly struct KoreanLetter : IEquatable<KoreanLetter>, IEquatable<char>, IEquatable<int>, IComparable<KoreanLetter>, IComparable<char>, IComparable<int>, IFormattable
    {
        #region Instantiation Shortcuts

        public static readonly KoreanLetter None = new KoreanLetter(-1);

        public static readonly KoreanLetter Giyeok = new KoreanLetter('ᄀ');
        public static readonly KoreanLetter SsangGiyeok = new KoreanLetter('ᄁ');
        public static readonly KoreanLetter Nieun = new KoreanLetter('ᄂ');
        public static readonly KoreanLetter Digeut = new KoreanLetter('ᄃ');
        public static readonly KoreanLetter SsangDigeut = new KoreanLetter('ᄄ');
        public static readonly KoreanLetter Rieul = new KoreanLetter('ᄅ');
        public static readonly KoreanLetter Mieum = new KoreanLetter('ᄆ');
        public static readonly KoreanLetter Bieup = new KoreanLetter('ᄇ');
        public static readonly KoreanLetter SsangBieup = new KoreanLetter('ᄈ');
        public static readonly KoreanLetter Shiot = new KoreanLetter('ᄉ');
        public static readonly KoreanLetter SsangShiot = new KoreanLetter('ᄊ');
        public static readonly KoreanLetter Ieung = new KoreanLetter('ᄋ');
        public static readonly KoreanLetter Jieut = new KoreanLetter('ᄌ');
        public static readonly KoreanLetter SsangJieut = new KoreanLetter('ᄍ');
        public static readonly KoreanLetter Chieut = new KoreanLetter('ᄎ');
        public static readonly KoreanLetter Kieuk = new KoreanLetter('ᄏ');
        public static readonly KoreanLetter Tieut = new KoreanLetter('ᄐ');
        public static readonly KoreanLetter Pieup = new KoreanLetter('ᄑ');
        public static readonly KoreanLetter Hieut = new KoreanLetter('ᄒ');

        public static readonly KoreanLetter A = new KoreanLetter('ᅡ');
        public static readonly KoreanLetter Ae = new KoreanLetter('ᅢ');
        public static readonly KoreanLetter Ya = new KoreanLetter('ᅣ');
        public static readonly KoreanLetter Yae = new KoreanLetter('ᅤ');
        public static readonly KoreanLetter Eo = new KoreanLetter('ᅥ');
        public static readonly KoreanLetter E = new KoreanLetter('ᅦ');
        public static readonly KoreanLetter Yeo = new KoreanLetter('ᅧ');
        public static readonly KoreanLetter Ye = new KoreanLetter('ᅨ');
        public static readonly KoreanLetter O = new KoreanLetter('ᅩ');
        public static readonly KoreanLetter Wa = new KoreanLetter('ᅪ');
        public static readonly KoreanLetter Wae = new KoreanLetter('ᅫ');
        public static readonly KoreanLetter Oe = new KoreanLetter('ᅬ');
        public static readonly KoreanLetter Yo = new KoreanLetter('ᅭ');
        public static readonly KoreanLetter U = new KoreanLetter('ᅮ');
        public static readonly KoreanLetter Wo = new KoreanLetter('ᅯ');
        public static readonly KoreanLetter We = new KoreanLetter('ᅰ');
        public static readonly KoreanLetter Wi = new KoreanLetter('ᅱ');
        public static readonly KoreanLetter Yu = new KoreanLetter('ᅲ');
        public static readonly KoreanLetter Eu = new KoreanLetter('ᅳ');
        public static readonly KoreanLetter Ui = new KoreanLetter('ᅴ');
        public static readonly KoreanLetter I = new KoreanLetter('ᅵ');

        public static readonly KoreanLetter GiyeokBatchim = new KoreanLetter('ᆨ');
        public static readonly KoreanLetter SsangGiyeokBatchim = new KoreanLetter('ᆩ');
        public static readonly KoreanLetter GiyeokShiotBatchim = new KoreanLetter('ᆪ');
        public static readonly KoreanLetter NieunBatchim = new KoreanLetter('ᆫ');
        public static readonly KoreanLetter NieunJieutBatchim = new KoreanLetter('ᆬ');
        public static readonly KoreanLetter NieunHieutBatchim = new KoreanLetter('ᆭ');
        public static readonly KoreanLetter DigeutBatchim = new KoreanLetter('ᆮ');
        public static readonly KoreanLetter RieulBatchim = new KoreanLetter('ᆯ');
        public static readonly KoreanLetter RieulGiyeokBatchim = new KoreanLetter('ᆰ');
        public static readonly KoreanLetter RieulMieumBatchim = new KoreanLetter('ᆱ');
        public static readonly KoreanLetter RieulBieupBatchim = new KoreanLetter('ᆲ');
        public static readonly KoreanLetter RieulShiotBatchim = new KoreanLetter('ᆳ');
        public static readonly KoreanLetter RieulTieutBatchim = new KoreanLetter('ᆴ');
        public static readonly KoreanLetter RieulPieupBatchim = new KoreanLetter('ᆵ');
        public static readonly KoreanLetter RieulHieutBatchim = new KoreanLetter('ᆶ');
        public static readonly KoreanLetter MieumBatchim = new KoreanLetter('ᆷ');
        public static readonly KoreanLetter BieupBatchim = new KoreanLetter('ᆸ');
        public static readonly KoreanLetter BieupShiotBatchim = new KoreanLetter('ᆹ');
        public static readonly KoreanLetter ShiotBatchim = new KoreanLetter('ᆺ');
        public static readonly KoreanLetter SsangShiotBatchim = new KoreanLetter('ᆻ');
        public static readonly KoreanLetter IeungBatchim = new KoreanLetter('ᆼ');
        public static readonly KoreanLetter JieutBatchim = new KoreanLetter('ᆽ');
        public static readonly KoreanLetter ChieutBatchim = new KoreanLetter('ᆾ');
        public static readonly KoreanLetter KieukBatchim = new KoreanLetter('ᆿ');
        public static readonly KoreanLetter TieutBatchim = new KoreanLetter('ᇀ');
        public static readonly KoreanLetter PieupBatchim = new KoreanLetter('ᇁ');
        public static readonly KoreanLetter HieutBatchim = new KoreanLetter('ᇂ');

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

        public static bool operator ==(KoreanLetter left, KoreanLetter right) => Equals(left, right);

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

        public bool Equals(KoreanLetter other) => CharacterCode == other.CharacterCode;

        public bool Equals(char c) => CharacterCode == c;

        public bool Equals(int i) => CharacterCode == i;

        public override bool Equals(object obj) => obj is KoreanLetter koreanLetter && Equals(koreanLetter);

        public override int GetHashCode() => CharacterCode.GetHashCode();

        public int CompareTo(KoreanLetter other) => CharacterCode.CompareTo(other.CharacterCode);

        public int CompareTo(char c) => CharacterCode.CompareTo(c);

        public int CompareTo(int i) => CharacterCode.CompareTo(i);

        #endregion

        #region String Formatting

        public override string ToString()
        {
            return ToString("H");
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            format = format.Replace("H", ((char)CharacterCode).ToString());

            return format;
        }

        #endregion
    }
}
