# KoreanConjugator

[![codecov](https://codecov.io/gh/cabauman/KoreanConjugator/branch/master/graph/badge.svg?token=5ZG48AUZID)](https://codecov.io/gh/cabauman/KoreanConjugator)
[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)

## Flow
1. Line up components.
2. Choose between options based on vowel and badchim of preceding syllable.
3. Apply vowel contractions or/and irregular verb mutations.
4. Merge syllables from left to right.

*Example: 듣다*
1. Line up components (stem + honorific marker + tense  + formality + clause type).
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

#### Common Suffixes

* ~아/어
* ~ㄴ/은 (descriptive verb to adjective OR action verb to past tense adjective)
* ~ㄴ/는 (action verb to plain form)
* ~ㅂ/습
* ~ㄹ/을
* ~ㅂ/읍
* ~ㅁ/음
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

## Irregulars

### Overview (for 아/어 suffix)

* ㅂ-Irregular	Drop final consonant ㅂ, add hangul 우** (e.g. 춥 -> 추우)
* ㅅ-Irregular	Drop final consonant ㅅ (e.g. 낫 -> 나)
* ㄷ-Irregular	Replace final consonant ㄷ with ㄹ (e.g. 듣 -> 들)
* ㅎ-Irregular	Drop final consonant ㅎ (e.g. 그렇	그러)
* ㄹ-Irregular	Drop final consonant ㄹ (e.g. 달 -> 다); The conjugation follows the same pattern as the stems with no 받침 e.g. 팔면, 살면; not 으면. Whenever the ㄹ 받침 is followed by ㄴ,ㅂ,ㅅ, the ㄹ disappears.
* 으-Irregular	When the verb stem ends with the 모음 “으”, the “으” is dropped whenever the verb tense ending begins with a 모음 and 어 / 아 added in it's place. e.g. 쓰다 → 써요 (ㅆ+어요)
* 르-Irregular	Add ㄹ as an ending consonant to the second to last hangul (e.g. 고르 -> 골르)

### Detailed Explanations

#### ㅅ
If the last letter of a word stem ends in ㅅ (for example: 짓다 = to build), the ㅅ gets removed when adding a vowel. Notice that this only happens when adding a vowel. When conjugating to the plain form, consonant, this irregular does not apply.

*Exceptions: 웃다, 벗다, 씻다*

#### ㄷ
If the last letter of a word stem ends in ㄷ (for example: 걷다 = to walk), the ㄷ gets changed to ㄹ when adding a vowel. This is only done with verbs.

*Exceptions: 걷다 (to tuck), 받다, 묻다, 닫다*

#### ㅂ
If the last letter of a word stem ends in ㅂ (쉽다 = easy), the ㅂ changes to 우 when adding a vowel. 우 then gets added to the next syllable in the conjugated word. This is mostly done with adjectives. Many verbs end with ㅂ but this rule is rarely applied to verbs Some of the few verbs where this rule applies are: 줍다 (to pick up), 눕다 (to lie down). In the words “돕다” (to help) and “곱다” (an uncommon way to say “beautiful”) ㅂ changes to 오 instead of 우. Note that in most irregulars, the word changes differently if the last vowel in the stem is ㅗ OR ㅏ. However, in the ㅂ irregular, except for 돕다 and 곱다, all applicable words are changed by adding 우.

*Exceptions: 좁다, 잡다, 넓다*

#### ㅡ
If the final vowel in a stem is ㅡ (for example: 잠그다 = to lock), when adding ~아/어, you can not determine whether you need to add ~어 or ~아 to the stem by looking at ㅡ. Instead, you must look at the vowel in the second last syllable. For example, in the word “잠그다”, the second last syllable in the stem is “잠”, and the vowel here is ㅏ. Therefore, as usual, we add ~아  to 잠그. In cases like this where a word ends in “ㅡ” (that is, there is no final consonant after “ㅡ”) and is followed by ~아/어 (or any of its derivatives), the ~아/어~ the “ㅡ” is eliminated and the addition of ~아/어~ merges to the stem. Some stems only have one syllable. For example, the stem of 크다 is just 크. In this case, we know that we need to use the ㅡ irregular, but there is no previous syllable to draw on to determine what should be added to the stem. In these cases, ~어 is added to the stem. An irregular to this already irregular rule is “만들다 (to make).” Even though the second last syllable in the stem has the vowel “ㅏ”, ~어~ is added instead of ~아~.

*With 듣다, both ㅡ and ㄷ irregulars are used*

*With 들다 both ㅡ and ㄹ irregulars are used*

#### 르
If the final syllable in a stem is 르 (마르다), it is conjugated differently when adding ~아/어. This irregular only applies when adding ~아/어(or any of its derivatives) to a stem and not when adding any other grammatical principles that starts with a vowel or consonant. When adding ~아/어 to these words, an additional ㄹ is created and placed in the syllable preceding 르 as the last consonant. The 르 also gets changed to either 러 or 라 (depending on if you are adding 어 or 아). This is done to both verbs and adjectives.

*The only exception is 따르다 = to follow/to pour (따라)*

#### ㄹ
If the final letter of a stem is ㄹ AND you add any of the following ~ㄴ/은, ~ㄴ/는, ~ㅂ/습, ~ㄹ/을: The first option (~ㄴ/ ~ㅂ / ~ㄹ ) should be used. In addition, the ㄹ is removed from the stem and the ~ㄴ / ~ㅂ / ~ㄹ is add directly to the stem.

#### ㅎ
If the final syllable in a stem is ㅎ (그렇다), 

## Thanks to

[hangulsoup.com](https://hangulsoup.com/tools/conjugator.php) for inspiration regarding the UX design and including advanced speech levels and tenses.

[dbravender of dongsa.net](https://koreanverb.app/?search=%ED%95%98%EB%8B%A4) for making his conjugator [source code](https://github.com/dbravender/korean_conjugation) publicly available, including research of special cases.

[BenjaminTMilnes](https://github.com/BenjaminTMilnes) for making his DotNet KoreanRomanisation [source code](https://github.com/BenjaminTMilnes/KoreanRomanisation) publicly available. In particular, I like his implementations of KoreanLetter and KoreanSyllable.
