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
    }
}
