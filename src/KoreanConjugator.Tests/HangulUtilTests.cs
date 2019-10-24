using Xunit;

namespace KoreanConjugator.Tests
{
    public class HangulUtilTests
    {
        [Theory]
        [InlineData(0, 0, 0, '가')]
        public void Should_Construct(int initialIndex, int medialIndex, int finalIndex, char expected)
        {
            var syllable = HangulUtil.Construct(initialIndex, medialIndex, finalIndex);
            Assert.Equal(expected, syllable);
        }

        [Theory]
        [InlineData('오', '아', '와')]
        [InlineData('우', '어', '워')]
        [InlineData('해', '었', '했')]
        [InlineData('가', '았', '갔')]
        [InlineData('시', '어', '셔')]
        [InlineData('하', '여', '해')]
        public void Should_Contract(char syllable1, char syllable2, char expected)
        {
            var syllable = HangulUtil.Contract(syllable1, syllable2);
            Assert.Equal(expected, syllable);
        }

        [Theory]
        [InlineData('들', 'ᆯ')]
        public void Should_ReturnFinal(char syllable, char expected)
        {
            var final = HangulUtil.Final(syllable);
            Assert.Equal(expected, final);
        }
    }
}
