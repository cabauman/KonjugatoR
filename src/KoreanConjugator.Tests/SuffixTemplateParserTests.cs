using Xunit;

namespace KoreanConjugator.Tests;

public class SuffixTemplateParserTests
{
    [Fact]
    public void CustomTest()
    {
        Action action = () => SuffixTemplateParser2.ParseAEuTemplate("B/V + (ㄹ/을) 거야");
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void TestTest()
    {
        var istrue = HangulUtil.IsComposableLetter('ㄴ');
        var template = SuffixTemplateParser2.ParseAEuTemplate("(아/어)요");
        var result = template.ChooseSuffixVariant("가");
        //Assert.Equal("가요", result);
    }

    [Fact]
    public void Should_AssignBadchimlessConnector_When_MultipleConnectorsExist()
    {
        var template = SuffixTemplateParser.ParseBadchimDependentTemplate("A/V + (ㄹ/을) 거야");
        Assert.Equal('ㄹ', template.BadchimlessConnector[0]);
    }

    [Fact]
    public void Should_AssignBadchimConnector_When_MultipleConnectorsExist()
    {
        var template = SuffixTemplateParser.ParseBadchimDependentTemplate("A/V + (ㄹ/을) 거야");
        Assert.Equal('을', template.BadchimConnector[0]);
    }

    [Fact]
    public void Should_AssignBadchimConnector_When_OneConnectorExists()
    {
        var template = SuffixTemplateParser.ParseBadchimDependentTemplate("A/V + (으)면");
        Assert.Equal('으', template.BadchimConnector[0]);
    }

    [Fact]
    public void ShouldNot_AssignBadchimlessConnector_When_OneConnectorExists()
    {
        var template = SuffixTemplateParser.ParseBadchimDependentTemplate("A/V + (으)면");
        Assert.Equal(default, template.BadchimlessConnector[0]);
    }

    [Theory]
    [InlineData("V")]
    [InlineData("A")]
    [InlineData("A/V")]
    public void Should_AssignWordClass_When_WordClassIsValid(string wordClass)
    {
        var template = SuffixTemplateParser.ParseBadchimDependentTemplate($"{wordClass} + (으)면");
        Assert.True(wordClass == template.WordClass);
    }

    [Fact]
    public void NoMatch()
    {
        var template = SuffixTemplateParser.ParseAEuTemplate("B + (아/어)요");
    }

    [Fact]
    public void Should_ReturnAEuSuffixTemplate_When_ContainsAEu()
    {
        var template = SuffixTemplateParser.ParseAEuTemplate("A/V + (아/어)요");
        Assert.True("요".AsSpan().SequenceEqual(template.StaticText));
    }
}
