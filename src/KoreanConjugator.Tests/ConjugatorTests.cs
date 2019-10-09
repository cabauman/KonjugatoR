using Xunit;

namespace KoreanConjugator.Tests
{
    public class ConjugatorTests
    {
        [Theory]
        [InlineData("먹", "먹어")]
        [InlineData("가", "가")]
        [InlineData("읽", "읽어")]
        public void Should_ConjugateToPresentInformalLowDeclarative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Declarative,
                Formality = Formality.InformalLow,
                Honorific = false,
                Tense = Tense.Present,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹을 거야")]
        [InlineData("가", "갈 거야")]
        [InlineData("읽", "읽을 거야")]
        public void Should_ConjugateToFutureInformalLowDeclarative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Declarative,
                Formality = Formality.InformalLow,
                Honorific = false,
                Tense = Tense.Future,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹었어")]
        [InlineData("가", "갔어")]
        [InlineData("읽", "읽었어")]
        public void Should_ConjugateToPastInformalLowDeclarative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Declarative,
                Formality = Formality.InformalLow,
                Honorific = false,
                Tense = Tense.Past,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹어요")]
        [InlineData("가", "가요")]
        [InlineData("읽", "읽어요")]
        public void Should_ConjugateToPresentInformalHighDeclarative(string stem, string expected)
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

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹을 거예요")]
        [InlineData("가", "갈 거예요")]
        [InlineData("읽", "읽을 거예요")]
        public void Should_ConjugateToFutureInformalHighDeclarative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Declarative,
                Formality = Formality.InformalHigh,
                Honorific = false,
                Tense = Tense.Future,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹었어요")]
        [InlineData("가", "갔어요")]
        [InlineData("읽", "읽었어요")]
        public void Should_ConjugateToPastInformalHighDeclarative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Declarative,
                Formality = Formality.InformalHigh,
                Honorific = false,
                Tense = Tense.Past,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹는다")]
        [InlineData("가", "간다")]
        [InlineData("읽", "읽는다")]
        public void Should_ConjugateToPresentFormalLowDeclarative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Declarative,
                Formality = Formality.FormalLow,
                Honorific = false,
                Tense = Tense.Present,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹을 거다")]
        [InlineData("가", "갈 거다")]
        [InlineData("읽", "읽을 거다")]
        public void Should_ConjugateToFutureFormalLowDeclarative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Declarative,
                Formality = Formality.FormalLow,
                Honorific = false,
                Tense = Tense.Future,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹었다")]
        [InlineData("가", "갔다")]
        [InlineData("읽", "읽었다")]
        public void Should_ConjugateToPastFormalLowDeclarative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Declarative,
                Formality = Formality.FormalLow,
                Honorific = false,
                Tense = Tense.Past,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹습니다")]
        [InlineData("가", "갑니다")]
        [InlineData("읽", "읽습니다")]
        public void Should_ConjugateToPresentFormalHighDeclarative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Declarative,
                Formality = Formality.FormalHigh,
                Honorific = false,
                Tense = Tense.Present,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹을 겁니다")]
        [InlineData("가", "갈 겁니다")]
        [InlineData("읽", "읽을 겁니다")]
        public void Should_ConjugateToFutureFormalHighDeclarative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Declarative,
                Formality = Formality.FormalHigh,
                Honorific = false,
                Tense = Tense.Future,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹었습니다")]
        [InlineData("가", "갔습니다")]
        [InlineData("읽", "읽었습니다")]
        public void Should_ConjugateToPastFormalHighDeclarative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Declarative,
                Formality = Formality.FormalHigh,
                Honorific = false,
                Tense = Tense.Past,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        // ------------------------------------------------------------

        [Theory]
        [InlineData("먹", "먹어?")]
        [InlineData("가", "가?")]
        [InlineData("읽", "읽어?")]
        public void Should_ConjugateToPresentInformalLowInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.InformalLow,
                Honorific = false,
                Tense = Tense.Present,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹을 거야?")]
        [InlineData("가", "갈 거야?")]
        [InlineData("읽", "읽을 거야?")]
        public void Should_ConjugateToFutureInformalLowInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.InformalLow,
                Honorific = false,
                Tense = Tense.Future,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹었어?")]
        [InlineData("가", "갔어?")]
        [InlineData("읽", "읽었어?")]
        public void Should_ConjugateToPastInformalLowInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.InformalLow,
                Honorific = false,
                Tense = Tense.Past,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }
        [Theory]
        [InlineData("먹", "먹어요?")]
        [InlineData("가", "가요?")]
        [InlineData("읽", "읽어요?")]
        public void Should_ConjugateToPresentInformalHighInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.InformalHigh,
                Honorific = false,
                Tense = Tense.Present,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹을 거예요?")]
        [InlineData("가", "갈 거예요?")]
        [InlineData("읽", "읽을 거예요?")]
        public void Should_ConjugateToFutureInformalHighInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.InformalHigh,
                Honorific = false,
                Tense = Tense.Future,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹었어요?")]
        [InlineData("가", "갔어요?")]
        [InlineData("읽", "읽었어요?")]
        public void Should_ConjugateToPastInformalHighInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.InformalHigh,
                Honorific = false,
                Tense = Tense.Past,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹나?")]
        [InlineData("가", "가나?")]
        [InlineData("읽", "읽나?")]
        public void Should_ConjugateToPresentFormalLowInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.FormalLow,
                Honorific = false,
                Tense = Tense.Present,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹을 건야?")]
        [InlineData("가", "갈 건야?")]
        [InlineData("읽", "읽을 건야?")]
        public void Should_ConjugateToFutureFormalLowInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.FormalLow,
                Honorific = false,
                Tense = Tense.Future,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹었는야?")]
        [InlineData("가", "갔는야?")]
        [InlineData("읽", "읽었는야?")]
        public void Should_ConjugateToPastFormalLowInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.FormalLow,
                Honorific = false,
                Tense = Tense.Past,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹습니까?")]
        [InlineData("가", "갑니까?")]
        [InlineData("읽", "읽습니까?")]
        public void Should_ConjugateToPresentFormalHighInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.FormalHigh,
                Honorific = false,
                Tense = Tense.Present,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹을 겁니까?")]
        [InlineData("가", "갈 겁니까?")]
        [InlineData("읽", "읽을 겁니까?")]
        public void Should_ConjugateToFutureFormalHighInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.FormalHigh,
                Honorific = false,
                Tense = Tense.Future,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("먹", "먹었습니까?")]
        [InlineData("가", "갔습니까?")]
        [InlineData("읽", "읽었습니까?")]
        public void Should_ConjugateToPastFormalHighInterrogative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Interrogative,
                Formality = Formality.FormalHigh,
                Honorific = false,
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
        [InlineData("먹", "먹어요")]
        [InlineData("가", "가요")]
        [InlineData("읽", "읽어요")]
        public void Should_ConjugateToPresentInformalHighImperative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Imperative,
                Formality = Formality.InformalHigh,
                Honorific = false,
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
        [InlineData("먹", "먹습시오")]
        [InlineData("가", "갑시오")]
        [InlineData("읽", "읽습시오")]
        public void Should_ConjugateToPresentFormalHighImperative(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Imperative,
                Formality = Formality.FormalHigh,
                Honorific = false,
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
        [InlineData("먹", "먹어요")]
        [InlineData("가", "가요")]
        [InlineData("읽", "읽어요")]
        public void Should_ConjugateToPresentInformalHighPropositive(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Propositive,
                Formality = Formality.InformalHigh,
                Honorific = false,
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
        [InlineData("먹", "먹읍시다")]
        [InlineData("가", "갑시다")]
        [InlineData("읽", "읽읍시다")]
        public void Should_ConjugateToPresentFormalHighPropositive(string stem, string expected)
        {
            var sut = new Conjugator(new SuffixTemplateParser());

            var conjugationParams = new ConjugationParams()
            {
                ClauseType = ClauseType.Propositive,
                Formality = Formality.FormalHigh,
                Honorific = false,
                Tense = Tense.Present,
                WordClass = WordClass.Verb,
            };

            var result = sut.Conjugate(stem, conjugationParams);
            Assert.Equal(expected, result.Value);
        }
    }
}
