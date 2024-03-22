using Xunit;

namespace KoreanConjugator.Tests;

public class ConjugatorTests
{
    [Theory]
    [InlineData("먹", true, "드셔")]
    [InlineData("먹", false, "먹어")]
    [InlineData("가", true, "가셔")]
    [InlineData("가", false, "가")]
    [InlineData("읽", true, "읽으셔")]
    [InlineData("읽", false, "읽어")]
    [InlineData("듣", true, "들으셔")]
    [InlineData("듣", false, "들어")]
    [InlineData("짓", true, "지으셔")]
    [InlineData("짓", false, "지어")]
    [InlineData("살", true, "사셔")]
    [InlineData("살", false, "살아")]
    [InlineData("쉽", true, "쉬우셔")]
    [InlineData("쉽", false, "쉬워")]
    [InlineData("돕", true, "도우셔")]
    [InlineData("돕", false, "도와")]
    [InlineData("잠그", true, "잠그셔")]
    [InlineData("잠그", false, "잠가")]
    [InlineData("모르", true, "모르셔")]
    [InlineData("모르", false, "몰라")]
    [InlineData("푸", true, "푸셔")]
    [InlineData("푸", false, "퍼")]
    [InlineData("뵙", true, "뵈셔")]
    [InlineData("뵙", false, "봬")]
    [InlineData("기다리", true, "기다리셔")]
    [InlineData("기다리", false, "기다려")]
    [InlineData("하", true, "하셔")]
    [InlineData("하", false, "해")]
    [InlineData("노랗", true, "노라셔")]
    [InlineData("노랗", false, "노래")]
    [InlineData("하얗", true, "하야셔")]
    [InlineData("하얗", false, "하얘")]
    [InlineData("이", true, "이셔")]
    [InlineData("이", false, "이야")]
    [InlineData("아니", true, "아니셔")]
    [InlineData("아니", false, "아니야")]
    [InlineData("놓", true, "놓으셔")]
    [InlineData("놓", false, "놓아")]
    [InlineData("끼울이", true, "끼울이셔")]
    [InlineData("끼울이", false, "끼울여")]
    [InlineData("댄이", false, "댄이야")]
    [InlineData("댄이", true, "댄이셔")]
    [InlineData("폭력적이", false, "폭력적이야")]
    [InlineData("폭력적이", true, "폭력적이셔")]
    [InlineData("모이", false, "모여")]
    [InlineData("모이", true, "모이셔")]
    [InlineData("예쁘", false, "예뻐")]
    [InlineData("따르", false, "따라")]
    [InlineData("치르", false, "치러")]
    [InlineData("들르", false, "들러")]
    public void Should_ConjugateToPresentInformalLowDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.InformalLow,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드실 거야")]
    [InlineData("먹", false, "먹을 거야")]
    [InlineData("가", true, "가실 거야")]
    [InlineData("가", false, "갈 거야")]
    [InlineData("읽", true, "읽으실 거야")]
    [InlineData("읽", false, "읽을 거야")]
    public void Should_ConjugateToFutureInformalLowDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.InformalLow,
            Honorific = honorific,
            Tense = Tense.Future,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드셨어")]
    [InlineData("먹", false, "먹었어")]
    [InlineData("가", true, "가셨어")]
    [InlineData("가", false, "갔어")]
    [InlineData("읽", true, "읽으셨어")]
    [InlineData("읽", false, "읽었어")]
    [InlineData("기다리", true, "기다리셨어")]
    [InlineData("기다리", false, "기다렸어")]
    public void Should_ConjugateToPastInformalLowDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.InformalLow,
            Honorific = honorific,
            Tense = Tense.Past,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드세요")]
    [InlineData("먹", false, "먹어요")]
    [InlineData("가", true, "가세요")]
    [InlineData("가", false, "가요")]
    [InlineData("읽", true, "읽으세요")]
    [InlineData("읽", false, "읽어요")]
    public void Should_ConjugateToPresentInformalHighDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.InformalHigh,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드실 거예요")]
    [InlineData("먹", false, "먹을 거예요")]
    [InlineData("가", true, "가실 거예요")]
    [InlineData("가", false, "갈 거예요")]
    [InlineData("읽", true, "읽으실 거예요")]
    [InlineData("읽", false, "읽을 거예요")]
    public void Should_ConjugateToFutureInformalHighDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.InformalHigh,
            Honorific = honorific,
            Tense = Tense.Future,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드셨어요")]
    [InlineData("먹", false, "먹었어요")]
    [InlineData("가", true, "가셨어요")]
    [InlineData("가", false, "갔어요")]
    [InlineData("읽", true, "읽으셨어요")]
    [InlineData("읽", false, "읽었어요")]
    public void Should_ConjugateToPastInformalHighDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.InformalHigh,
            Honorific = honorific,
            Tense = Tense.Past,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드신다")]
    [InlineData("먹", false, "먹는다")]
    [InlineData("가", true, "가신다")]
    [InlineData("가", false, "간다")]
    [InlineData("읽", true, "읽으신다")]
    [InlineData("읽", false, "읽는다")]
    public void Should_ConjugateToPresentFormalLowDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.FormalLow,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드실 거다")]
    [InlineData("먹", false, "먹을 거다")]
    [InlineData("가", true, "가실 거다")]
    [InlineData("가", false, "갈 거다")]
    [InlineData("읽", true, "읽으실 거다")]
    [InlineData("읽", false, "읽을 거다")]
    public void Should_ConjugateToFutureFormalLowDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.FormalLow,
            Honorific = honorific,
            Tense = Tense.Future,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드셨다")]
    [InlineData("먹", false, "먹었다")]
    [InlineData("가", true, "가셨다")]
    [InlineData("가", false, "갔다")]
    [InlineData("읽", true, "읽으셨다")]
    [InlineData("읽", false, "읽었다")]
    public void Should_ConjugateToPastFormalLowDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.FormalLow,
            Honorific = honorific,
            Tense = Tense.Past,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드십니다")]
    [InlineData("먹", false, "먹습니다")]
    [InlineData("가", true, "가십니다")]
    [InlineData("가", false, "갑니다")]
    [InlineData("읽", true, "읽으십니다")]
    [InlineData("읽", false, "읽습니다")]
    public void Should_ConjugateToPresentFormalHighDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.FormalHigh,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드실 겁니다")]
    [InlineData("먹", false, "먹을 겁니다")]
    [InlineData("가", true, "가실 겁니다")]
    [InlineData("가", false, "갈 겁니다")]
    [InlineData("읽", true, "읽으실 겁니다")]
    [InlineData("읽", false, "읽을 겁니다")]
    public void Should_ConjugateToFutureFormalHighDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.FormalHigh,
            Honorific = honorific,
            Tense = Tense.Future,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드셨습니다")]
    [InlineData("먹", false, "먹었습니다")]
    [InlineData("가", true, "가셨습니다")]
    [InlineData("가", false, "갔습니다")]
    [InlineData("읽", true, "읽으셨습니다")]
    [InlineData("읽", false, "읽었습니다")]
    public void Should_ConjugateToPastFormalHighDeclarative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.FormalHigh,
            Honorific = honorific,
            Tense = Tense.Past,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    // ------------------------------------------------------------

    [Theory]
    [InlineData("먹", true, "드셔?")]
    [InlineData("먹", false, "먹어?")]
    [InlineData("가", true, "가셔?")]
    [InlineData("가", false, "가?")]
    [InlineData("읽", true, "읽으셔?")]
    [InlineData("읽", false, "읽어?")]
    public void Should_ConjugateToPresentInformalLowInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.InformalLow,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드실 거야?")]
    [InlineData("먹", false, "먹을 거야?")]
    [InlineData("가", true, "가실 거야?")]
    [InlineData("가", false, "갈 거야?")]
    [InlineData("읽", true, "읽으실 거야?")]
    [InlineData("읽", false, "읽을 거야?")]
    public void Should_ConjugateToFutureInformalLowInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.InformalLow,
            Honorific = honorific,
            Tense = Tense.Future,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드셨어?")]
    [InlineData("먹", false, "먹었어?")]
    [InlineData("가", true, "가셨어?")]
    [InlineData("가", false, "갔어?")]
    [InlineData("읽", true, "읽으셨어?")]
    [InlineData("읽", false, "읽었어?")]
    public void Should_ConjugateToPastInformalLowInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.InformalLow,
            Honorific = honorific,
            Tense = Tense.Past,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드세요?")]
    [InlineData("먹", false, "먹어요?")]
    [InlineData("가", true, "가세요?")]
    [InlineData("가", false, "가요?")]
    [InlineData("읽", true, "읽으세요?")]
    [InlineData("읽", false, "읽어요?")]
    public void Should_ConjugateToPresentInformalHighInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.InformalHigh,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드실 거예요?")]
    [InlineData("먹", false, "먹을 거예요?")]
    [InlineData("가", true, "가실 거예요?")]
    [InlineData("가", false, "갈 거예요?")]
    [InlineData("읽", true, "읽으실 거예요?")]
    [InlineData("읽", false, "읽을 거예요?")]
    [InlineData("살", true, "사실 거예요?")]
    [InlineData("살", false, "살 거예요?")]
    public void Should_ConjugateToFutureInformalHighInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.InformalHigh,
            Honorific = honorific,
            Tense = Tense.Future,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드셨어요?")]
    [InlineData("먹", false, "먹었어요?")]
    [InlineData("가", true, "가셨어요?")]
    [InlineData("가", false, "갔어요?")]
    [InlineData("읽", true, "읽으셨어요?")]
    [InlineData("읽", false, "읽었어요?")]
    public void Should_ConjugateToPastInformalHighInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.InformalHigh,
            Honorific = honorific,
            Tense = Tense.Past,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드시나?")]
    [InlineData("먹", false, "먹나?")]
    [InlineData("가", true, "가시나?")]
    [InlineData("가", false, "가나?")]
    [InlineData("읽", true, "읽으시나?")]
    [InlineData("읽", false, "읽나?")]
    public void Should_ConjugateToPresentFormalLowInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.FormalLow,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드실 거냐?")]
    [InlineData("먹", false, "먹을 거냐?")]
    [InlineData("가", true, "가실 거냐?")]
    [InlineData("가", false, "갈 거냐?")]
    [InlineData("읽", true, "읽으실 거냐?")]
    [InlineData("읽", false, "읽을 거냐?")]
    public void Should_ConjugateToFutureFormalLowInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.FormalLow,
            Honorific = honorific,
            Tense = Tense.Future,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드셨냐?")]
    [InlineData("먹", false, "먹었냐?")]
    [InlineData("가", true, "가셨냐?")]
    [InlineData("가", false, "갔냐?")]
    [InlineData("읽", true, "읽으셨냐?")]
    [InlineData("읽", false, "읽었냐?")]
    public void Should_ConjugateToPastFormalLowInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.FormalLow,
            Honorific = honorific,
            Tense = Tense.Past,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드십니까?")]
    [InlineData("먹", false, "먹습니까?")]
    [InlineData("가", true, "가십니까?")]
    [InlineData("가", false, "갑니까?")]
    [InlineData("읽", true, "읽으십니까?")]
    [InlineData("읽", false, "읽습니까?")]
    public void Should_ConjugateToPresentFormalHighInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.FormalHigh,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드실 겁니까?")]
    [InlineData("먹", false, "먹을 겁니까?")]
    [InlineData("가", true, "가실 겁니까?")]
    [InlineData("가", false, "갈 겁니까?")]
    [InlineData("읽", true, "읽으실 겁니까?")]
    [InlineData("읽", false, "읽을 겁니까?")]
    public void Should_ConjugateToFutureFormalHighInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.FormalHigh,
            Honorific = honorific,
            Tense = Tense.Future,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드셨습니까?")]
    [InlineData("먹", false, "먹었습니까?")]
    [InlineData("가", true, "가셨습니까?")]
    [InlineData("가", false, "갔습니까?")]
    [InlineData("읽", true, "읽으셨습니까?")]
    [InlineData("읽", false, "읽었습니까?")]
    public void Should_ConjugateToPastFormalHighInterrogative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Interrogative,
            Formality = Formality.FormalHigh,
            Honorific = honorific,
            Tense = Tense.Past,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    // ------------------------------------------------------------

    [Theory]
    [InlineData("먹", "먹어")]
    [InlineData("가", "가")]
    [InlineData("읽", "읽어")]
    public void Should_ConjugateToPresentInformalLowImperative(string stem, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Imperative,
            Formality = Formality.InformalLow,
            Honorific = false,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드세요")]
    [InlineData("먹", false, "먹어요")]
    [InlineData("가", true, "가세요")]
    [InlineData("가", false, "가요")]
    [InlineData("읽", true, "읽으세요")]
    [InlineData("읽", false, "읽어요")]
    public void Should_ConjugateToPresentInformalHighImperative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Imperative,
            Formality = Formality.InformalHigh,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", "먹어라")]
    [InlineData("가", "가라")]
    [InlineData("읽", "읽어라")]
    public void Should_ConjugateToPresentFormalLowImperative(string stem, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Imperative,
            Formality = Formality.FormalLow,
            Honorific = false,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }


    [Theory]
    [InlineData("먹", true, "드십시오")]
    [InlineData("먹", false, "먹으십시오")]
    [InlineData("가", true, "가십시오")]
    [InlineData("가", false, "가십시오")]
    [InlineData("읽", true, "읽으십시오")]
    [InlineData("읽", false, "읽으십시오")]
    public void Should_ConjugateToPresentFormalHighImperative(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Imperative,
            Formality = Formality.FormalHigh,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    // ------------------------------------------------------------

    [Theory]
    [InlineData("먹", "먹어")]
    [InlineData("가", "가")]
    [InlineData("읽", "읽어")]
    public void Should_ConjugateToPresentInformalLowPropositive(string stem, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Propositive,
            Formality = Formality.InformalLow,
            Honorific = false,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", true, "드세요")]
    [InlineData("먹", false, "먹어요")]
    [InlineData("가", true, "가세요")]
    [InlineData("가", false, "가요")]
    [InlineData("읽", true, "읽으세요")]
    [InlineData("읽", false, "읽어요")]
    public void Should_ConjugateToPresentInformalHighPropositive(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Propositive,
            Formality = Formality.InformalHigh,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData("먹", "먹자")]
    [InlineData("가", "가자")]
    [InlineData("읽", "읽자")]
    public void Should_ConjugateToPresentFormalLowPropositive(string stem, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Propositive,
            Formality = Formality.FormalLow,
            Honorific = false,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }


    [Theory]
    [InlineData("먹", true, "드십시다")]
    [InlineData("먹", false, "먹읍시다")]
    [InlineData("가", true, "가십시다")]
    [InlineData("가", false, "갑시다")]
    [InlineData("읽", true, "읽으십시다")]
    [InlineData("읽", false, "읽읍시다")]
    public void Should_ConjugateToPresentFormalHighPropositive(string stem, bool honorific, string expected)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Propositive,
            Formality = Formality.FormalHigh,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal(expected, result.Value);
    }

    // ------------------------------------------------------------

    [Theory]
    [InlineData("읽", false)]
    public void Should_ConjugateCorrectlyWhenCalledMoreThanOnce(string stem, bool honorific)
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.FormalLow,
            Honorific = honorific,
            Tense = Tense.Past,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal("읽었다", result.Value);

        conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.FormalLow,
            Honorific = honorific,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };
        result = sut.Conjugate(stem, conjugationParams);
        Assert.Equal("읽는다", result.Value);
    }

    [Fact]
    public void Should_ConjugateCorrectlyWhenVerbHasDaSuffix()
    {
        var sut = new Conjugator(new SuffixTemplateParser());

        var conjugationParams = new ConjugationParams()
        {
            ClauseType = ClauseType.Declarative,
            Formality = Formality.InformalHigh,
            Honorific = false,
            Tense = Tense.Present,
            WordClass = WordClass.Verb,
        };

        var result = sut.Conjugate("먹다", conjugationParams);
        Assert.Equal("먹어요", result.Value);
    }
}
