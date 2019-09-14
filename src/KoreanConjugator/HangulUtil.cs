using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KoreanConjugator
{

    public static class HangulUtil
    {
        private const int FirstKoreanSyllableCharacterCode = 44032;
        private const int LastKoreanSyllableCharacterCode = 55203;

        private const int NumberOfInitials = 19;
        private const int NumberOfMedials = 21;
        private const int NumberOfFinals = 28;

        private const int FirstKoreanLetterCharacterCode = 4352;
        private const int LastKoreanLetterCharacterCode = 4607;

        private const int FirstCommonInitialCharacterCode = 4352;
        private const int LastCommonInitialCharacterCode = 4370;

        private const int FirstCommonMedialCharacterCode = 4449;
        private const int LastCommonMedialCharacterCode = 4469;

        private const int FirstCommonFinalCharacterCode = 4520;
        private const int LastCommonFinalCharacterCode = 4546;

        public static char Initial(char syllable)
        {
            var initialOffset = (syllable - FirstKoreanSyllableCharacterCode) / (NumberOfMedials * NumberOfFinals);

            return (char)(FirstCommonInitialCharacterCode + initialOffset);
        }

        public static char Medial(char syllable)
        {
            var medialOffset = ((syllable - FirstKoreanSyllableCharacterCode) % (NumberOfMedials * NumberOfFinals)) / NumberOfFinals;

            return (char)(FirstCommonMedialCharacterCode + medialOffset);
        }

        public static char Final(char syllable)
        {
            var finalOffset = ((syllable - FirstKoreanSyllableCharacterCode) % (NumberOfMedials * NumberOfFinals)) % NumberOfFinals;

            return (char)(FirstCommonFinalCharacterCode + finalOffset);
        }

        public static bool HasFinal(char syllable)
        {
            var finalOffset = ((syllable - FirstKoreanSyllableCharacterCode) % (NumberOfMedials * NumberOfFinals)) % NumberOfFinals;

            return finalOffset > 0;
        }

        public static bool BeginsWithVowel(char syllable)
        {
            return Initial(syllable) == 'ᄋ';
        }

        public static char DropFinal(char syllable)
        {
            return SetFinal(syllable, '\0');
        }

        public static char SetFinal(char syllable, char final)
        {
            var initial = Initial(syllable);
            var medial = Medial(syllable);
            var newSyllable = Construct(initial, medial, final);
            return newSyllable;
        }

        public static bool BeginsWithNieun(char syllable)
        {
            return Initial(syllable) == 'ᄂ';
        }

        public static bool BeginsWithSieut(char syllable)
        {
            return Initial(syllable) == 'ᄉ';
        }

        public static bool BeginsWith(char syllable, char initial, char medial)
        {
            return IsSyllable(syllable) && Initial(syllable) == initial && Medial(syllable) == medial;
        }

        public static char Construct(int initial, int medial, int final)
        {
            if (initial < 0 || initial >= NumberOfInitials)
            {
                throw new ArgumentOutOfRangeException(nameof(initial), $"The initial character code must be between 0 and {NumberOfInitials - 1}");
            }

            if (medial < 0 || medial >= NumberOfMedials)
            {
                throw new ArgumentOutOfRangeException(nameof(medial), $"The medial character code must be between 0 and {NumberOfMedials - 1}");
            }

            if (final < 0 || final >= NumberOfFinals)
            {
                throw new ArgumentOutOfRangeException(nameof(final), $"The final character code must be between 0 and {NumberOfFinals - 1}");
            }

            int characterCode = (initial * NumberOfMedials * NumberOfFinals) + (medial * NumberOfFinals) + final + FirstKoreanSyllableCharacterCode;

            return (char)characterCode;
        }

        public static Dictionary<Tuple<char, char>, char> VowelContractionMap = new Dictionary<Tuple<char, char>, char>
        {
            { Tuple.Create('ㅏ', 'ㅏ'), 'ㅏ' },
            { Tuple.Create('ㅓ', 'ㅓ'), 'ㅓ' },
            { Tuple.Create('ㅐ', 'ㅓ'), 'ㅐ' },
            { Tuple.Create('ㅏ', 'ㅣ'), 'ㅐ' },
            { Tuple.Create('ㅓ', 'ㅣ'), 'ㅐ' },
            { Tuple.Create('ㅑ', 'ㅣ'), 'ㅒ' },
            { Tuple.Create('ㅔ', 'ㅓ'), 'ㅔ' },
            { Tuple.Create('ㅗ', 'ㅏ'), 'ㅘ' },
            { Tuple.Create('ㅜ', 'ㅓ'), 'ㅝ' },
            { Tuple.Create('ㅚ', 'ㅓ'), 'ㅙ' },
            { Tuple.Create('ㅡ', 'ㅏ'), 'ㅏ' },
            { Tuple.Create('ㅡ', 'ㅓ'), 'ㅓ' },
            { Tuple.Create('ㅏ', 'ㅡ'), 'ㅏ' },
            { Tuple.Create('ㅓ', 'ㅡ'), 'ㅓ' },
            { Tuple.Create('ㅣ', 'ㅓ'), 'ㅕ' },
            { Tuple.Create('시', 'ㅓ'), '세' },
            { Tuple.Create('하', 'ㅕ'), '해' }
        };

        public static char Contract(char syllable1, char syllable2)
        {
            var medial2 = Medial(syllable2);

            if (!VowelContractionMap.TryGetValue(Tuple.Create(syllable1, medial2), out char medial))
            {
                var medial1 = Medial(syllable1);
                medial = VowelContractionMap[Tuple.Create(medial1, medial2)];
            }

            return Construct(Initial(syllable1), medial, Final(syllable2));
        }

        internal static bool IsLetter(char character)
        {
            throw new NotImplementedException();
        }

        internal static bool IsSyllable(char character)
        {
            return character >= FirstKoreanSyllableCharacterCode && character <= LastKoreanSyllableCharacterCode;
        }

        public static char[] Irregulars = { 'ㅅ', 'ㄷ', 'ㅂ', 'ㅡ', '르', 'ㄹ', 'ㅎ' };

        public static HashSet<string> IrregularExceptions = new HashSet<string>();
    }
}
