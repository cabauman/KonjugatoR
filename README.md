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

* Remote Past e.g. 했었다
* Probable Past e.g. 했을 것이다
* Past e.g. 했다
* Present e.g. 한다
* Probable Future e.g. 할 것이다
* Intentional Future e.g. 하겠다

#### Speech Levels (Formality)

* Plain (FormalHigh, dictionary form) e.g. 하다
* Intimate (InformalLow) e.g. 해
* Polite (InformalHigh) e.g. 해요
* Deferential (FormalHigh) e.g. 합니다

#### Clause Types

* Statement (Declarative) e.g. 한다
* Question (Inquisitive) e.g. 하느냐?
* Confirmation (Confirmation) e.g. 하지?
* Suggestion (Propositive) e.g. 하자
* Command (Imperative) e.g. 해라

## Thanks to

[hangulsoup.com](https://hangulsoup.com/tools/conjugator.php) for inspiration regarding the UX design and including advanced speech levels and tenses.

[dbravender of dongsa.net](https://koreanverb.app/?search=%ED%95%98%EB%8B%A4) for making his conjugator [source code](https://github.com/dbravender/korean_conjugation) publicly available, including research of special cases.

[BenjaminTMilnes](https://github.com/BenjaminTMilnes) for making his DotNet KoreanRomanisation [source code](https://github.com/BenjaminTMilnes/KoreanRomanisation) publicly available. In particular, I like his implementations of KoreanLetter and KoreanSyllable.
