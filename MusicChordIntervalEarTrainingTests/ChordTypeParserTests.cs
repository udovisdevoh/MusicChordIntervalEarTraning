using MusicChordIntervalEarTraining;
using System;
using Xunit;

namespace MusicChordIntervalEarTrainingTests
{
    public class ChordTypeParserTests
    {
        [Theory]
        [InlineData("AuG", ChordType.Augmented)]
        [InlineData("dIm", ChordType.DiminishedTriad)]
        [InlineData("dIm7", ChordType.Diminished7)]
        [InlineData("7dIm", ChordType.Diminished7)]
        [InlineData("hdIm7", ChordType.HalfDiminished7)]
        [InlineData("7hdIm", ChordType.HalfDiminished7)]
        [InlineData("hdIm", ChordType.HalfDiminished7)]
        [InlineData("M", ChordType.Major)]
        [InlineData("maJ", ChordType.Major)]
        [InlineData("maJoR", ChordType.Major)]
        [InlineData("m", ChordType.Minor)]
        [InlineData("MiN", ChordType.Minor)]
        [InlineData("MINoR", ChordType.Minor)]
        [InlineData("M7", ChordType.Major7)]
        [InlineData("maJ7", ChordType.Major7)]
        [InlineData("maJoR7", ChordType.Major7)]
        [InlineData("7M", ChordType.Major7)]
        [InlineData("7maJ", ChordType.Major7)]
        [InlineData("7maJoR", ChordType.Major7)]
        [InlineData("m7", ChordType.Minor7)]
        [InlineData("MiN7", ChordType.Minor7)]
        [InlineData("MINoR7", ChordType.Minor7)]
        [InlineData("7m", ChordType.Minor7)]
        [InlineData("7MiN", ChordType.Minor7)]
        [InlineData("7MINoR", ChordType.Minor7)]
        [InlineData("7", ChordType.Dominant7)]
        [InlineData("seven", ChordType.Dominant7)]
        [InlineData("dOm", ChordType.Dominant7)]
        [InlineData("dOminant", ChordType.Dominant7)]
        [InlineData("7dOm", ChordType.Dominant7)]
        [InlineData("7dOminant", ChordType.Dominant7)]
        [InlineData("dOm7", ChordType.Dominant7)]
        [InlineData("dOminant7", ChordType.Dominant7)]
        public void Test1(string token, ChordType expectedChordType)
        {
            // Arrange
            ChordTypeParser chordTypeParser = new ChordTypeParser();

            // Act
            ChordType actualChordType = chordTypeParser.Parse(token);

            // Assert
            Assert.Equal(expectedChordType, actualChordType);
        }
    }
}