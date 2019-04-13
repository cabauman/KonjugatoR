# KoreanConjugator

## Flow
1. Line up components.
2. Choose between options based on vowel and badchim of preceding syllable.
3. Apply vowel contractions or/and irregular verb mutations.
4. Merge syllables from left to right.

*Example: 듣다*
1. Line up components (stem + honorific marker + tense  + fomality + clause type).
> 듣    + (으)시            + ㄹ/을 거 + ㅂ/습     + 니다

2. Choose between options based on vowel and badchim of preceding syllable.
    * (으)시 selection for 듣: 으시 (has non-ㄹ padchim)
    * ㄹ/을 selection for 시: ㄹ 거 (has no padchim or padchim = ㄹ)
    * ㅂ/습 selection for 거: ㅂ   (has no padchim or padchim = ㄹ)

> 듣 으 시 ㄹ 거 ㅂ 니다

3. Apply vowel contractions or/and irregular verb mutations.
    * Irregular verb ㄷ -> ㄹ conversion preceding vowel: 듣 + 으 -> 들으

> 들 으 시 ㄹ 거 ㅂ 니다

4. Merge syllables from left to right.
    * Merge syllable without badchim with stand-alone consonant: 시 + ㄹ -> 실
    * Merge syllable without badchim with stand-alone consonant: 거 + ㅂ -> 겁

> 들으실 겁니다

## Terminology

#### Tenses

* Remote Past (Past Perfect) e.g. 했었다
* Probable Past e.g. 했을 것이다
* Past e.g. 했다
* Present e.g. 한다
* Probable Future e.g. 할 것이다
* Intentional Future e.g. 하겠다
* Past Future e.g. 했겠다

#### Speech Levels (Formality)

* Plain (FormalLow, dictionary form) e.g. 하다
* Intimate (InformalLow) e.g. 해
* Polite (InformalHigh) e.g. 해요
* Deferential (FormalHigh) e.g. 합니다

#### Clause Types

* Statement (Declarative) e.g. 한다
* Question (Inquisitive) e.g. 하느냐?
* Confirmation (Confirmation) e.g. 하지?
* Suggestion (Propositive) e.g. 하자
* Command (Imperative) e.g. 해라

#### Basics

* Jamo (자모): letters in the Korean alphabet.
* Consonant (자음)
* Vowel (모음)
* Modern Jamo excludes those that have never been used or have fallen out of use.
* Compatibility jamo can be thought of as the "academic" version of jamo. When displayed in isolation they are centered and rendered nicely, in contrast to those that are used in the construction of syllables which appear more skewed and have an offset.

#### Syllable Components

* Initial/Lead (초성): a consonant, which is placed in the first of three possible positions.
* Medial (중성): a vowel, which is placed in the second of three possible positions.
* Final (받침/종성): a consonant (or consonant cluster) which is placed in the third of three possible positions.

#### Verb/adjective irregulars for 아/어 suffix

* ㅂ-Irregular	Drop final consonant ㅂ, add hangul 우** (e.g. 춥 -> 추우)
* ㅅ-Irregular	Drop final consonant ㅅ (e.g. 낫 -> 나)
* ㄷ-Irregular	Replace final consonant ㄷ with ㄹ (e.g. 듣 -> 들)
* ㅎ-Irregular	Drop final consonant ㅎ (e.g. 그렇	그러)
* ㄹ-Irregular	Drop final consonant ㄹ (e.g. 달 -> 다); The conjugation follows the same pattern as the stems with no 받침 e.g. 팔면, 살면; not 으면. Whenever the ㄹ 받침 is followed by ㄴ,ㅂ,ㅅ, the ㄹ disappears.
* 으-Irregular	When the verb stem ends with the 모음 “으”, the “으” is dropped whenever the verb tense ending begins with a 모음 and 어 / 아 added in it's place. e.g. 쓰다 → 써요 (ㅆ+어요)
* 르-Irregular	Add ㄹ as an ending consonant to the second to last hangul (e.g. 고르 -> 골르)

#### Common Suffixes

* ~아/어
* ~ㄴ/은
* ~ㄴ/는
* ~ㅂ/습
* ~ㄹ/을
* (으)

#### Formulas

19 initials (ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ)

21 medials (ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ)

28 finals  (\0ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ)

syllable character code = ((initial * 588) + (medial * 28) + final) + 44032

index of initial = (syllable - 44032) / 588

index of medial = (syllable - 44032) / 28 % 21

index of final = (syllable - 44032) % 28

|  | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | A | B | C | D | E | F |
|--------|--------|--------|--------|--------|----|----|----|----|----|----|----|----|----|----|----|----|
| U+110x | ᄀ | ᄁ | ᄂ | ᄃ | ᄄ | ᄅ | ᄆ | ᄇ | ᄈ | ᄉ | ᄊ | ᄋ | ᄌ | ᄍ | ᄎ | ᄏ |
| U+111x | ᄐ | ᄑ | ᄒ | ᄓ | ᄔ | ᄕ | ᄖ | ᄗ | ᄘ | ᄙ | ᄚ | ᄛ | ᄜ | ᄝ | ᄞ | ᄟ |
| U+112x | ᄠ | ᄡ | ᄢ | ᄣ | ᄤ | ᄥ | ᄦ | ᄧ | ᄨ | ᄩ | ᄪ | ᄫ | ᄬ | ᄭ | ᄮ | ᄯ |
| U+113x | ᄰ | ᄱ | ᄲ | ᄳ | ᄴ | ᄵ | ᄶ | ᄷ | ᄸ | ᄹ | ᄺ | ᄻ | ᄼ | ᄽ | ᄾ | ᄿ |
| U+114x | ᅀ | ᅁ | ᅂ | ᅃ | ᅄ | ᅅ | ᅆ | ᅇ | ᅈ | ᅉ | ᅊ | ᅋ | ᅌ | ᅍ | ᅎ | ᅏ |
| U+115x | ᅐ | ᅑ | ᅒ | ᅓ | ᅔ | ᅕ | ᅖ | ᅗ | ᅘ | ᅙ | ᅚ | ᅛ | ᅜ | ᅝ | ᅞ | ᅟ |
| U+116x | ᅠ | ᅡ | ᅢ | ᅣ | ᅤ | ᅥ | ᅦ | ᅧ | ᅨ | ᅩ | ᅪ | ᅫ | ᅬ | ᅭ | ᅮ | ᅯ |
| U+117x | ᅰ | ᅱ | ᅲ | ᅳ | ᅴ | ᅵ | ᅶ | ᅷ | ᅸ | ᅹ | ᅺ | ᅻ | ᅼ | ᅽ | ᅾ | ᅿ |
| U+118x | ᆀ | ᆁ | ᆂ | ᆃ | ᆄ | ᆅ | ᆆ | ᆇ | ᆈ | ᆉ | ᆊ | ᆋ | ᆌ | ᆍ | ᆎ | ᆏ |
| U+119x | ᆐ | ᆑ | ᆒ | ᆓ | ᆔ | ᆕ | ᆖ | ᆗ | ᆘ | ᆙ | ᆚ | ᆛ | ᆜ | ᆝ | ᆞ | ᆟ |
| U+11Ax | ᆠ | ᆡ | ᆢ | ᆣ | ᆤ | ᆥ | ᆦ | ᆧ | ᆨ | ᆩ | ᆪ | ᆫ | ᆬ | ᆭ | ᆮ | ᆯ |
| U+11Bx | ᆰ | ᆱ | ᆲ | ᆳ | ᆴ | ᆵ | ᆶ | ᆷ | ᆸ | ᆹ | ᆺ | ᆻ | ᆼ | ᆽ | ᆾ | ᆿ |
| U+11Cx | ᇀ | ᇁ | ᇂ | ᇃ | ᇄ | ᇅ | ᇆ | ᇇ | ᇈ | ᇉ | ᇊ | ᇋ | ᇌ | ᇍ | ᇎ | ᇏ |
| U+11Dx | ᇐ | ᇑ | ᇒ | ᇓ | ᇔ | ᇕ | ᇖ | ᇗ | ᇘ | ᇙ | ᇚ | ᇛ | ᇜ | ᇝ | ᇞ | ᇟ |
| U+11Ex | ᇠ | ᇡ | ᇢ | ᇣ | ᇤ | ᇥ | ᇦ | ᇧ | ᇨ | ᇩ | ᇪ | ᇫ | ᇬ | ᇭ | ᇮ | ᇯ |
| U+11Fx | ᇰ | ᇱ | ᇲ | ᇳ | ᇴ | ᇵ | ᇶ | ᇷ | ᇸ | ᇹ | ᇺ | ᇻ | ᇼ | ᇽ | ᇾ | ᇿ |

## Thanks to

[hangulsoup.com](https://hangulsoup.com/tools/conjugator.php) for inspiration regarding the UX design and including advanced speech levels and tenses.

[dbravender of dongsa.net](https://koreanverb.app/?search=%ED%95%98%EB%8B%A4) for making his conjugator [source code](https://github.com/dbravender/korean_conjugation) publicly available, including research of special cases.

[BenjaminTMilnes](https://github.com/BenjaminTMilnes) for making his DotNet KoreanRomanisation [source code](https://github.com/BenjaminTMilnes/KoreanRomanisation) publicly available. In particular, I like his implementations of KoreanLetter and KoreanSyllable.
