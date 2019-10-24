using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KoreanConjugator
{
    /// <summary>
    /// Represents a utility for performing common operations on Korean letters and syllables.
    /// </summary>
    public static class HangulUtil
    {
        private const int FirstKoreanSyllableCharacterCode = 44032;
        private const int LastKoreanSyllableCharacterCode = 55203;

        private const int NumberOfInitials = 19;
        private const int NumberOfMedials = 21;
        private const int NumberOfFinals = 28;

        private const int FirstModernCompatibilityLetterCharacterCode = 12593;
        private const int LastModernCompatibilityLetterCharacterCode = 12643;

        private const int FirstModernCompatibilityConsonantCharacterCode = 12593;
        private const int LastModernCompatibilityConsonantCharacterCode = 12622;

        private const int FirstModernCompatibilityVowelCharacterCode = 12623;
        private const int LastModernCompatibilityVowelCharacterCode = 12643;

        private const int FirstKoreanLetterCharacterCode = 4352;
        private const int LastKoreanLetterCharacterCode = 4607;

        private const int FirstModernInitialCharacterCode = 4352;
        private const int LastModernInitialCharacterCode = 4370;

        private const int FirstModernMedialCharacterCode = 4449;
        private const int LastModernMedialCharacterCode = 4469;

        private const int FirstModernFinalCharacterCode = 4520;
        private const int LastModernFinalCharacterCode = 4546;

        private static readonly string CompatibilityInitials = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
        private static readonly string Initials = "ᄀᄁᄂᄃᄄᄅᄆᄇᄈᄉᄊᄋᄌᄍᄎᄏᄐᄑᄒ";
        private static readonly string CompatibilityMedials = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
        private static readonly string Medials = "ᅡᅢᅣᅤᅥᅦᅧᅨᅩᅪᅫᅬᅭᅮᅯᅰᅱᅲᅳᅴᅵ";
        private static readonly string CompatibilityFinals = "\0ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
        private static readonly string Finals = "\0ᆨᆩᆪᆫᆬᆭᆮᆯᆰᆱᆲᆳᆴᆵᆶᆷᆸᆹᆺᆻᆼᆽᆾᆿᇀᇁᇂ";

        private static readonly string ModernCompatibilityLetters = CompatibilityInitials + CompatibilityMedials + CompatibilityFinals;
        private static readonly string ModernComposableLetters = Initials + Medials + Finals;

        private static readonly Dictionary<char, char> CompatibilityToComposableFinalMap = CompatibilityFinals
            .Zip(Finals, (k, v) => new { k, v })
            .ToDictionary(x => x.k, x => x.v);

        private static readonly char[] Irregulars = { 'ᆺ', 'ᆮ', 'ᆸ', 'ᅳ', '르', 'ᆯ', 'ᇂ' };

        private static readonly HashSet<string> IrregularExceptions = new HashSet<string>
        {
            "따르",
            "웃",
            "벗",
            "씻",
            //"걷", Another meaning of 걷다 is to tuck, which is an exception.
            "받",
            "묻",
            "닫",
            "좁",
            "잡",
        };

        private static HashSet<string> BieupRegularsThatLookIrregular { get; set; }

        private static HashSet<string> SieutRegularsThatLookIrregular { get; set; }

        private static HashSet<string> HieutRegularsThatLookIrregular { get; set; }

        private static HashSet<string> DigeutRegularsThatLookIrregular { get; set; }

        private static HashSet<string> LeuRegularsThatLookIrregular { get; set; }

        private static readonly Dictionary<Tuple<char, char>, char> VowelContractionMap = new Dictionary<Tuple<char, char>, char>
        {
            { Tuple.Create('ᅡ', 'ᅡ'), 'ᅡ' },
            { Tuple.Create('ᅥ', 'ᅥ'), 'ᅥ' },
            { Tuple.Create('ᅢ', 'ᅥ'), 'ᅢ' },
            { Tuple.Create('ᅢ', 'ᅡ'), 'ᅢ' },
            { Tuple.Create('ᅡ', 'ᅵ'), 'ᅢ' },
            { Tuple.Create('ᅥ', 'ᅵ'), 'ᅢ' },
            { Tuple.Create('ᅣ', 'ᅵ'), 'ᅤ' },
            { Tuple.Create('ᅤ', 'ᅥ'), 'ᅤ' },
            { Tuple.Create('ᅤ', 'ᅡ'), 'ᅤ' },
            { Tuple.Create('ᅦ', 'ᅥ'), 'ᅦ' },
            { Tuple.Create('ᅩ', 'ᅡ'), 'ᅪ' },
            { Tuple.Create('ᅮ', 'ᅥ'), 'ᅯ' },
            { Tuple.Create('ᅮ', 'ᅳ'), 'ᅮ' },
            { Tuple.Create('ᅬ', 'ᅥ'), 'ᅫ' },
            { Tuple.Create('ᅳ', 'ᅡ'), 'ᅡ' },
            { Tuple.Create('ᅳ', 'ᅥ'), 'ᅥ' },
            { Tuple.Create('ᅡ', 'ᅳ'), 'ᅡ' },
            { Tuple.Create('ᅥ', 'ᅳ'), 'ᅥ' },
            { Tuple.Create('ᅣ', 'ᅳ'), 'ᅣ' },
            { Tuple.Create('ᅵ', 'ᅥ'), 'ᅧ' },
            { Tuple.Create('ᅡ', 'ᅧ'), 'ᅢ' },
        };

        public static readonly Dictionary<string, string> SpecialHonorificMap = new Dictionary<string, string>
        {
            { "먹", "들" },        // 잡수시다; 드시다
            { "있", "계" },        // 계시다
            { "자", "주무" },      // 주무시다
            { "말하", "말씀하" },  // 말씀하시다
            { "주", "드리" },      // Not 드리시다; when the receiver deserves hight respect

            { "에게", "께" },
            { "한테", "께" },
            { "말", "말씀" },
            { "보", "뵈" },       // or slightly even more formal 뵙.
        };

        public static void LoadRegularsThatLookIrregular()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Data\RegularsThatLookIrregular.txt");
            string[] lines = File.ReadAllLines(path);
            string text = File.ReadAllText(path);
            string[] words = text.Split(',');
            var exceptions = new HashSet<string>(words);

            for (int i = 0; i < lines.Length; ++i)
            {
                //int indexOfLastSyllableOfFirstWord = lines[i].IndexOf('l') - 1;
                //char lastSyllableOfFirstWord = lines[i][indexOfLastSyllableOfFirstWord];
                //char final = Final(lastSyllableOfFirstWord);
                char firstCharOfLine = lines[i][0];

                switch (firstCharOfLine)
                {
                    case 'ㅂ':
                        lines[i] = lines[i].Remove(0, 2);
                        BieupRegularsThatLookIrregular = new HashSet<string>(lines[i].Split(','));
                        break;
                    case 'ㅅ':
                        lines[i] = lines[i].Remove(0, 2);
                        SieutRegularsThatLookIrregular = new HashSet<string>(lines[i].Split(','));
                        break;
                    case 'ㄷ':
                        lines[i] = lines[i].Remove(0, 2);
                        DigeutRegularsThatLookIrregular = new HashSet<string>(lines[i].Split(','));
                        break;
                    case 'ㅎ':
                        lines[i] = lines[i].Remove(0, 2);
                        HieutRegularsThatLookIrregular = new HashSet<string>(lines[i].Split(','));
                        break;
                    case '르':
                        lines[i] = lines[i].Remove(0, 2);
                        LeuRegularsThatLookIrregular = new HashSet<string>(lines[i].Split(','));
                        break;
                }

                if (lines[i][0].Equals('ㅂ'))
                {
                    lines[i] = lines[i].Remove(0, 2);
                }
            }
        }

        /// <summary>
        /// Gets the index of the Korean letter in the "Initial" position.
        /// </summary>
        /// <param name="syllable">The Korean syllable.</param>
        /// <returns>The index of the Korean letter in the "Initial" position.</returns>
        public static int IndexOfInitial(char syllable)
        {
            return (syllable - FirstKoreanSyllableCharacterCode) / (NumberOfMedials * NumberOfFinals);
        }

        /// <summary>
        /// Gets the Korean letter in the "Initial" position.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <returns>The Korean letter in the "Initial" position.</returns>
        public static char Initial(char syllable)
        {
            var initialOffset = IndexOfInitial(syllable);

            return (char)(FirstModernInitialCharacterCode + initialOffset);
        }

        /// <summary>
        /// Gets the index of the Korean letter in the "Medial" position.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <returns>The index of the Korean letter in the "Medial" position.</returns>
        public static int IndexOfMedial(char syllable)
        {
            return ((syllable - FirstKoreanSyllableCharacterCode) % (NumberOfMedials * NumberOfFinals)) / NumberOfFinals;
        }

        /// <summary>
        /// Gets the Korean letter in the "Medial" position.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <returns>The Korean letter in the "Medial" position.</returns>
        public static char Medial(char syllable)
        {
            var medialOffset = IndexOfMedial(syllable);

            return (char)(FirstModernMedialCharacterCode + medialOffset);
        }

        /// <summary>
        /// Gets the index of the Korean letter in the "Final" position.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <returns>The index of the Korean letter in the "Final" position.</returns>
        public static int IndexOfFinal(char syllable)
        {
            return (syllable - FirstKoreanSyllableCharacterCode) % NumberOfFinals;
        }

        /// <summary>
        /// Gets the Korean letter in the "Final" position.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <returns>The Korean letter in the "Final" position.</returns>
        public static char Final(char syllable)
        {
            var finalOffset = IndexOfFinal(syllable);

            return (char)(FirstModernFinalCharacterCode + finalOffset - 1);
        }

        /// <summary>
        /// Gets a value indicating whether the syllable contains a Korean letter in the "Final" position.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <returns><c>true</c> if the syllable has a Final; otherwise, <c>false</c>.</returns>
        public static bool HasFinal(char syllable)
        {
            var finalOffset = ((syllable - FirstKoreanSyllableCharacterCode) % (NumberOfMedials * NumberOfFinals)) % NumberOfFinals;

            return finalOffset > 0;
        }

        /// <summary>
        /// Gets a value indicating whether the syllable begins with a vowel.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <returns><c>true</c> if the syllable begins with a vowel; otherwise, <c>false</c>.</returns>
        public static bool BeginsWithVowel(char syllable)
        {
            return Initial(syllable) == 'ᄋ';
        }

        /// <summary>
        /// Removes the Korean letter in the "Final" position of the syllable.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <returns>The resulting Korean syllable.</returns>
        public static char DropFinal(char syllable)
        {
            return SetFinal(syllable, '\0');
        }

        /// <summary>
        /// Sets the Korean letter in the "Final" position of the syllable.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <param name="final">The Korean letter to place in the "Final" postion.</param>
        /// <returns>The resulting Korean syllable.</returns>
        public static char SetFinal(char syllable, char final)
        {
            var initial = Initial(syllable);
            var medial = Medial(syllable);
            var newSyllable = Construct(initial, medial, final);
            return newSyllable;
        }

        /// <summary>
        /// Gets a value indicating whether the syllable begins with 'ㄴ'.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <returns><c>true</c> if the syllable begins with 'ㄴ'; otherwise, <c>false</c>.</returns>
        public static bool BeginsWithNieun(char syllable)
        {
            return Initial(syllable) == 'ᄂ';
        }

        /// <summary>
        /// Gets a value indicating whether the syllable begins with 'ㅅ'.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <returns><c>true</c> if the syllable begins with 'ㅅ'; otherwise, <c>false</c>.</returns>
        public static bool BeginsWithSieut(char syllable)
        {
            return Initial(syllable) == 'ᄉ';
        }

        /// <summary>
        /// Gets a value indicating whether the syllable begins with 'ㅅ'.
        /// </summary>
        /// <param name="syllable">A Korean syllable.</param>
        /// <param name="initial">A Korean letter that belongs in the "Initial" position of a syllable.</param>
        /// <param name="medial">A Korean letter that belongs in the "Medial" position of a syllable.</param>
        /// <returns><c>true</c> if the syllable begins with 'ㅅ'; otherwise, <c>false</c>.</returns>
        public static bool BeginsWith(char syllable, char initial, char medial)
        {
            return IsSyllable(syllable) && Initial(syllable) == initial && Medial(syllable) == medial;
        }

        /// <summary>
        /// Constructs a Korean syllable.
        /// </summary>
        /// <param name="initial">The index of the initial jamo character.</param>
        /// <param name="medial">The index of the medial jamo character.</param>
        /// <param name="final">The index of the final jamo character.</param>
        /// <returns>The constructed Korean syllable.</returns>
        public static char Construct(int initial, int medial, int final)
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

            int characterCode = (initial * NumberOfMedials * NumberOfFinals) + (medial * NumberOfFinals) + final + FirstKoreanSyllableCharacterCode;

            return (char)characterCode;
        }

        public static char Construct(char initial, char medial, char final)
        {
            int indexOfFinal = final.Equals('\0') ? 0 : final - FirstModernFinalCharacterCode + 1;

            return Construct(
                initial - FirstModernInitialCharacterCode,
                medial - FirstModernMedialCharacterCode,
                indexOfFinal);
        }

        public static bool CanContract(char character1, char character2)
        {
            var medial1 = Medial(character1);
            var medial2 = Medial(character2);
            var key1 = Tuple.Create(medial1, medial2);
            var key2 = Tuple.Create(character1, medial2);

            return !IsModernCompatibilityLetter(character1) &&
                KoreanLetter.Ieung.Equals(Initial(character2)) &&
                (VowelContractionMap.ContainsKey(key1) || VowelContractionMap.ContainsKey(key2));
        }

        /// <summary>
        /// Contracts two syllables into one.
        /// </summary>
        /// <param name="syllable1">The first syllable.</param>
        /// <param name="syllable2">The second syllable.</param>
        /// <returns>The contracted Korean syllable.</returns>
        public static char Contract(char syllable1, char syllable2)
        {
            var medial1 = Medial(syllable1);
            var medial2 = Medial(syllable2);
            if (!VowelContractionMap.TryGetValue(Tuple.Create(medial1, medial2), out char medial))
            {
                throw new Exception($"Tried to contract {syllable1} and {syllable2} but" +
                    "couldn't find a valid contraction.");
            }

            return Construct(Initial(syllable1), medial, Final(syllable2));
        }

        /// <summary>
        /// Gets a value indicating whether the verb stem is irregular.
        /// </summary>
        /// <param name="verbStem">A verb without '다' at the end.</param>
        /// <returns><c>true</c> if the verb stem is irregular; otherwise, <c>false</c>.</returns>
        public static bool IsIrregular(string verbStem)
        {
            if (string.IsNullOrEmpty(verbStem))
            {
                throw new ArgumentException(nameof(verbStem));
            }

            bool hasIrregularForm =
                Irregulars.Contains(Final(verbStem.Last())) ||
                Irregulars.Contains(Medial(verbStem.Last())) ||
                Irregulars.Contains(verbStem.Last());

            return
                IsSyllable(verbStem.Last()) &&
                hasIrregularForm &&
                !IrregularExceptions.Contains(verbStem);
        }

        /// <summary>
        /// Gets a value indicating whether the character is a composable letter.
        /// </summary>
        /// <param name="character">The character to check.</param>
        /// <returns><c>true</c> if the character is a composable letter; otherwise, <c>false</c>.</returns>
        public static bool IsComposableLetter(char character)
        {
            return character >= FirstKoreanLetterCharacterCode && character <= LastKoreanLetterCharacterCode;
        }

        /// <summary>
        /// Gets a value indicating whether the character is a modern compatibility letter.
        /// </summary>
        /// <param name="character">The character to check.</param>
        /// <returns><c>true</c> if the character is a modern compatibility letter; otherwise, <c>false</c>.</returns>
        public static bool IsModernCompatibilityLetter(char character)
        {
            return character >= FirstModernCompatibilityLetterCharacterCode && character <= LastModernCompatibilityLetterCharacterCode;
        }

        /// <summary>
        /// Gets a value indicating whether the character is a Korean syllable.
        /// </summary>
        /// <param name="character">The character to check.</param>
        /// <returns><c>true</c> if the character is a Korean syllable; otherwise, <c>false</c>.</returns>
        public static bool IsSyllable(char character)
        {
            return character >= FirstKoreanSyllableCharacterCode && character <= LastKoreanSyllableCharacterCode;
        }

        /// <summary>
        /// Gets the composable version of a compatibility letter.
        /// </summary>
        /// <param name="compatibilityLetter">A compatibility letter.</param>
        /// <returns>A composable Korean letter that belongs in the "Final" position of a syllable.</returns>
        public static char ToComposableFinal(char compatibilityLetter)
        {
            if (!CompatibilityToComposableFinalMap.TryGetValue(compatibilityLetter, out var result))
            {
                throw new ArgumentException($"[{compatibilityLetter}] is not a compatibility letter.");
            }

            return result;
        }

        /// <summary>
        /// Gets the index of the "Final".
        /// </summary>
        /// <param name="final">A Korean letter that belongs in the "Final" posoiton of a syllable.</param>
        /// <returns>The index of the "Final".</returns>
        public static int FinalToIndex(char final)
        {
            return (final + 1) - FirstModernFinalCharacterCode;
        }
    }
}
