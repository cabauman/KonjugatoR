using KoreanConjugator;
using Xunit;

namespace KoreanConjugator.Tests
{
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
            Assert.Equal(KoreanLetter.Giyeok, KoreanLetter.GetKoreanLetterFromInitialCharacterCode(0));
        }

        [Fact]
        public void GetKoreanLetterFromInitialCharacterCode1Test()
        {
            Assert.Equal(KoreanLetter.SsangGiyeok, KoreanLetter.GetKoreanLetterFromInitialCharacterCode(1));
        }

        [Fact]
        public void GetKoreanLetterFromInitialCharacterCode2Test()
        {
            Assert.Equal(KoreanLetter.Nieun, KoreanLetter.GetKoreanLetterFromInitialCharacterCode(2));
        }

        [Fact]
        public void GetKoreanLetterFromInitialCharacterCode18Test()
        {
            Assert.Equal(KoreanLetter.Hieut, KoreanLetter.GetKoreanLetterFromInitialCharacterCode(18));
        }

        #endregion

        #region GetKoreanLetterFromFinalCharacterCode() Tests

        [Fact]
        public void GetKoreanLetterFromFinalCharacterCode1Test()
        {
            Assert.Equal(KoreanLetter.GiyeokBatchim, KoreanLetter.GetKoreanLetterFromFinalCharacterCode(1));
        }

        [Fact]
        public void GetKoreanLetterFromFinalCharacterCode2Test()
        {
            Assert.Equal(KoreanLetter.SsangGiyeokBatchim, KoreanLetter.GetKoreanLetterFromFinalCharacterCode(2));
        }

        [Fact]
        public void GetKoreanLetterFromFinalCharacterCode3Test()
        {
            Assert.Equal(KoreanLetter.GiyeokShiotBatchim, KoreanLetter.GetKoreanLetterFromFinalCharacterCode(3));
        }

        [Fact]
        public void GetKoreanLetterFromFinalCharacterCode27Test()
        {
            Assert.Equal(KoreanLetter.HieutBatchim, KoreanLetter.GetKoreanLetterFromFinalCharacterCode(27));
        }

        #endregion
    }
}
