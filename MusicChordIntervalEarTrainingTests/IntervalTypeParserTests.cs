using MusicChordIntervalEarTraining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MusicChordIntervalEarTrainingTests
{
    public class IntervalTypeParserTests
    {
        [Theory]
        [InlineData("0", IntervalType.UnisonOctave)]
        [InlineData("8", IntervalType.UnisonOctave)]
        [InlineData("2b", IntervalType.MinorSecond)]
        [InlineData("b2", IntervalType.MinorSecond)]
        [InlineData("2m", IntervalType.MinorSecond)]
        [InlineData("m2", IntervalType.MinorSecond)]
        [InlineData("2MIN", IntervalType.MinorSecond)]
        [InlineData("MIN2", IntervalType.MinorSecond)]
        [InlineData("2", IntervalType.MajorSecond)]
        [InlineData("2M", IntervalType.MajorSecond)]
        [InlineData("M2", IntervalType.MajorSecond)]
        [InlineData("maJ2", IntervalType.MajorSecond)]
        [InlineData("2maJ", IntervalType.MajorSecond)]
        [InlineData("2maJor", IntervalType.MajorSecond)]
        [InlineData("3m", IntervalType.MinorThird)]
        [InlineData("3MiN", IntervalType.MinorThird)]
        [InlineData("3MInOR", IntervalType.MinorThird)]
        [InlineData("m3", IntervalType.MinorThird)]
        [InlineData("MiN3", IntervalType.MinorThird)]
        [InlineData("MInOR3", IntervalType.MinorThird)]
        [InlineData("3", IntervalType.MajorThird)]
        [InlineData("3M", IntervalType.MajorThird)]
        [InlineData("3mAj", IntervalType.MajorThird)]
        [InlineData("3maJor", IntervalType.MajorThird)]
        [InlineData("M3", IntervalType.MajorThird)]
        [InlineData("mAj3", IntervalType.MajorThird)]
        [InlineData("maJor3", IntervalType.MajorThird)]
        [InlineData("4", IntervalType.PerfectFourth)]
        [InlineData("4tH", IntervalType.PerfectFourth)]
        [InlineData("fouRth", IntervalType.PerfectFourth)]
        [InlineData("4A", IntervalType.AugmentedFourthDiminishedFifthTritone)]
        [InlineData("4a", IntervalType.AugmentedFourthDiminishedFifthTritone)]
        [InlineData("A4", IntervalType.AugmentedFourthDiminishedFifthTritone)]
        [InlineData("a4", IntervalType.AugmentedFourthDiminishedFifthTritone)]
        [InlineData("5diM", IntervalType.AugmentedFourthDiminishedFifthTritone)]
        [InlineData("diM5", IntervalType.AugmentedFourthDiminishedFifthTritone)]
        [InlineData("tritOne", IntervalType.AugmentedFourthDiminishedFifthTritone)]
        [InlineData("5", IntervalType.PerfectFifth)]
        [InlineData("5tH", IntervalType.PerfectFifth)]
        [InlineData("fiFth", IntervalType.PerfectFifth)]
        [InlineData("6b", IntervalType.FlatSixth)]
        [InlineData("b6", IntervalType.FlatSixth)]
        [InlineData("6B", IntervalType.FlatSixth)]
        [InlineData("B6", IntervalType.FlatSixth)]
        [InlineData("B6th", IntervalType.FlatSixth)]
        [InlineData("b6th", IntervalType.FlatSixth)]
        [InlineData("6flAt", IntervalType.FlatSixth)]
        [InlineData("6thflAt", IntervalType.FlatSixth)]
        [InlineData("flAt6", IntervalType.FlatSixth)]
        [InlineData("flAt6th", IntervalType.FlatSixth)]
        [InlineData("flAtsix", IntervalType.FlatSixth)]
        [InlineData("flAtsixtH", IntervalType.FlatSixth)]
        [InlineData("sixflAt", IntervalType.FlatSixth)]
        [InlineData("sixtHflAt", IntervalType.FlatSixth)]
        [InlineData("5aUg", IntervalType.FlatSixth)]
        [InlineData("aUg5", IntervalType.FlatSixth)]
        [InlineData("5a", IntervalType.FlatSixth)]
        [InlineData("a5", IntervalType.FlatSixth)]
        [InlineData("6", IntervalType.Sixth)]
        [InlineData("6tH", IntervalType.Sixth)]
        [InlineData("six", IntervalType.Sixth)]
        [InlineData("sixTh", IntervalType.Sixth)]
        [InlineData("7b", IntervalType.FlatSeventh)]
        [InlineData("b7", IntervalType.FlatSeventh)]
        [InlineData("m7", IntervalType.FlatSeventh)]
        [InlineData("7m", IntervalType.FlatSeventh)]
        [InlineData("7mIn", IntervalType.FlatSeventh)]
        [InlineData("7mInor", IntervalType.FlatSeventh)]
        [InlineData("mIn7", IntervalType.FlatSeventh)]
        [InlineData("mInor7", IntervalType.FlatSeventh)]
        [InlineData("b7th", IntervalType.FlatSeventh)]
        [InlineData("flaT7", IntervalType.FlatSeventh)]
        [InlineData("flaT7th", IntervalType.FlatSeventh)]
        [InlineData("7flaT", IntervalType.FlatSeventh)]
        [InlineData("7thM", IntervalType.MajorSeventh)]
        [InlineData("M7", IntervalType.MajorSeventh)]
        [InlineData("maj7", IntervalType.MajorSeventh)]
        [InlineData("maJor7", IntervalType.MajorSeventh)]
        [InlineData("7M", IntervalType.MajorSeventh)]
        [InlineData("7maj", IntervalType.MajorSeventh)]
        [InlineData("7maJor", IntervalType.MajorSeventh)]
        [InlineData("M7th", IntervalType.MajorSeventh)]
        public void Test(string token, IntervalType expectedIntervalType)
        {
            // Arrange
            IntervalParser intervalParser = new IntervalParser();

            // Act
            IntervalType actualIntervalType = intervalParser.Parse(token);

            // Assert
            Assert.Equal(expectedIntervalType, actualIntervalType);
        }
    }
}
