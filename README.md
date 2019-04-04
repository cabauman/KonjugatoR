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

#### Syllable Components

* Initial/Lead (초성)
* Medial (중성)
* Final (받침/종성): the final consonant (or consonant cluster) at the end of a Korean syllable.

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


## Thanks to

[hangulsoup.com](https://hangulsoup.com/tools/conjugator.php) for inspiration regarding the UX design and including advanced speech levels and tenses.

[dbravender of dongsa.net](https://koreanverb.app/?search=%ED%95%98%EB%8B%A4) for making his conjugator [source code](https://github.com/dbravender/korean_conjugation) publicly available, including research of special cases.

[BenjaminTMilnes](https://github.com/BenjaminTMilnes) for making his DotNet KoreanRomanisation [source code](https://github.com/BenjaminTMilnes/KoreanRomanisation) publicly available. In particular, I like his implementations of KoreanLetter and KoreanSyllable.
