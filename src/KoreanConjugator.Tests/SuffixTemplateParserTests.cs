using Xunit;

namespace KoreanConjugator.Tests
{
    public class SuffixTemplateParserTests
    {
        [Fact]
        public void Should_AssignBadchimlessConnector_When_MultipleConnectorsExist()
        {
            var sut = new SuffixTemplateParser();
            var template = (BadchimDependentSuffixTemplate)sut.Parse("V/A + (ㄹ/을) 거야");
            Assert.Equal("ㄹ", template.BadchimlessConnector);
        }

        [Fact]
        public void Should_AssignBadchimConnector_When_MultipleConnectorsExist()
        {
            var sut = new SuffixTemplateParser();
            var template = (BadchimDependentSuffixTemplate)sut.Parse("V/A + (ㄹ/을) 거야");
            Assert.Equal("을", template.BadchimConnector);
        }

        [Fact]
        public void Should_AssignBadchimConnector_When_OneConnectorExists()
        {
            var sut = new SuffixTemplateParser();
            var template = (BadchimDependentSuffixTemplate)sut.Parse("V/A + (으)면");
            Assert.Equal("으", template.BadchimConnector);
        }

        [Fact]
        public void ShouldNot_AssignBadchimlessConnector_When_OneConnectorExists()
        {
            var sut = new SuffixTemplateParser();
            var template = (BadchimDependentSuffixTemplate)sut.Parse("V/A + (으)면");
            Assert.Equal(string.Empty, template.BadchimlessConnector);
        }

        [Theory]
        [InlineData("V")]
        [InlineData("A")]
        [InlineData("A/V")]
        public void Should_AssignWordClass_When_WordClassIsValid(string wordClass)
        {
            var sut = new SuffixTemplateParser();
            var template = (BadchimDependentSuffixTemplate)sut.Parse($"{wordClass} + (으)면");
            Assert.Equal(wordClass, template.WordClass);
        }
    }
}
