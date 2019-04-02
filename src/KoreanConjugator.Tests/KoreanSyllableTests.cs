using Xunit;

namespace KoreanConjugator.Tests
{
    public class KoreanSyllableTests
    {
        [Fact]
        public void CreateViaIndicesTest()
        {
            var syllable = new KoreanSyllable(0, 0, 18);

            Assert.True(syllable.Equals('값'));
            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.BieupShiotBatchim, syllable.Final);
        }

        #region Initial Character Codes Tests

        [Fact]
        public void CharacterCodesGaTest()
        {
            var syllable = new KoreanSyllable('가');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesNaTest()
        {
            var syllable = new KoreanSyllable('나');

            Assert.Equal(KoreanLetter.Nieun, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesDaTest()
        {
            var syllable = new KoreanSyllable('다');

            Assert.Equal(KoreanLetter.Digeut, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesMaTest()
        {
            var syllable = new KoreanSyllable('마');

            Assert.Equal(KoreanLetter.Mieum, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesBaTest()
        {
            var syllable = new KoreanSyllable('바');

            Assert.Equal(KoreanLetter.Bieup, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesSaTest()
        {
            var syllable = new KoreanSyllable('사');

            Assert.Equal(KoreanLetter.Shiot, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesATest()
        {
            var syllable = new KoreanSyllable('아');

            Assert.Equal(KoreanLetter.Ieung, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesJaTest()
        {
            var syllable = new KoreanSyllable('자');

            Assert.Equal(KoreanLetter.Jieut, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesHaTest()
        {
            var syllable = new KoreanSyllable('하');

            Assert.Equal(KoreanLetter.Hieut, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        #endregion

        #region Medial Character Codes Tests

        [Fact]
        public void CharacterCodesGeoTest()
        {
            var syllable = new KoreanSyllable('거');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.Eo, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGoTest()
        {
            var syllable = new KoreanSyllable('고');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.O, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGuTest()
        {
            var syllable = new KoreanSyllable('구');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.U, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGeuTest()
        {
            var syllable = new KoreanSyllable('그');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.Eu, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGiTest()
        {
            var syllable = new KoreanSyllable('기');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.I, syllable.Medial);
            Assert.Equal(KoreanLetter.None, syllable.Final);
        }

        #endregion

        #region Final Character Codes Tests

        [Fact]
        public void CharacterCodesGakTest()
        {
            var syllable = new KoreanSyllable('각');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.GiyeokBatchim, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGanTest()
        {
            var syllable = new KoreanSyllable('간');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.NieunBatchim, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGat1Test()
        {
            var syllable = new KoreanSyllable('갇');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.DigeutBatchim, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGamTest()
        {
            var syllable = new KoreanSyllable('감');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.MieumBatchim, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGapTest()
        {
            var syllable = new KoreanSyllable('갑');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.BieupBatchim, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGat2Test()
        {
            var syllable = new KoreanSyllable('갓');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.ShiotBatchim, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGangTest()
        {
            var syllable = new KoreanSyllable('강');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.IeungBatchim, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGat3Test()
        {
            var syllable = new KoreanSyllable('갖');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.JieutBatchim, syllable.Final);
        }

        [Fact]
        public void CharacterCodesGat4Test()
        {
            var syllable = new KoreanSyllable('갛');

            Assert.Equal(KoreanLetter.Giyeok, syllable.Initial);
            Assert.Equal(KoreanLetter.A, syllable.Medial);
            Assert.Equal(KoreanLetter.HieutBatchim, syllable.Final);
        }

        #endregion
    }
}
