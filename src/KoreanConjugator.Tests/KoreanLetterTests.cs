using KoreanConjugator;
using Xunit;

namespace KoreanConjugator.Tests;

public class KoreanLetterTests
{
    #region IsAKoreanLetter() Tests

    [Fact]
    public void Is0AKoreanLetterTest()
    {
        Assert.False(KoreanLetter.IsAKoreanLetter(0));
    }

    [Fact]
    public void Is1AKoreanLetterTest()
    {
        Assert.False(KoreanLetter.IsAKoreanLetter(1));
    }

    [Fact]
    public void Is100AKoreanLetterTest()
    {
        Assert.False(KoreanLetter.IsAKoreanLetter(100));
    }

    [Fact]
    public void Is45000AKoreanLetterTest()
    {
        Assert.False(KoreanLetter.IsAKoreanLetter(45000));
    }

    #endregion

    #region GetKoreanLetterFromInitialCharacterCode() Tests

    [Fact]
    public void GetKoreanLetterFromInitialCharacterCode0Test()
    {
        Assert.Equal(KoreanLetter.Giyeok, KoreanLetter.GetKoreanLetterFromInitialIndex(0));
    }

    [Fact]
    public void GetKoreanLetterFromInitialCharacterCode1Test()
    {
        Assert.Equal(KoreanLetter.SsangGiyeok, KoreanLetter.GetKoreanLetterFromInitialIndex(1));
    }

    [Fact]
    public void GetKoreanLetterFromInitialCharacterCode2Test()
    {
        Assert.Equal(KoreanLetter.Nieun, KoreanLetter.GetKoreanLetterFromInitialIndex(2));
    }

    [Fact]
    public void GetKoreanLetterFromInitialCharacterCode18Test()
    {
        Assert.Equal(KoreanLetter.Hieut, KoreanLetter.GetKoreanLetterFromInitialIndex(18));
    }

    #endregion

    #region GetKoreanLetterFromFinalCharacterCode() Tests

    [Fact]
    public void GetKoreanLetterFromFinalCharacterCode1Test()
    {
        Assert.Equal(KoreanLetter.GiyeokBatchim, KoreanLetter.GetKoreanLetterFromFinalIndex(1));
    }

    [Fact]
    public void GetKoreanLetterFromFinalCharacterCode2Test()
    {
        Assert.Equal(KoreanLetter.SsangGiyeokBatchim, KoreanLetter.GetKoreanLetterFromFinalIndex(2));
    }

    [Fact]
    public void GetKoreanLetterFromFinalCharacterCode3Test()
    {
        Assert.Equal(KoreanLetter.GiyeokShiotBatchim, KoreanLetter.GetKoreanLetterFromFinalIndex(3));
    }

    [Fact]
    public void GetKoreanLetterFromFinalCharacterCode27Test()
    {
        Assert.Equal(KoreanLetter.HieutBatchim, KoreanLetter.GetKoreanLetterFromFinalIndex(27));
    }

    #endregion
}
