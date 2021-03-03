using Sampler;
using System;

namespace MusicChordIntervalEarTraining
{
    class Program
    {
        private static readonly bool isShowAnswerFirst = false;

        static void Main(string[] args)
        {
            const int midiChannel = 1;
            Random random = new Random();
            QuestionBuilder questionBuilder = new QuestionBuilder(random, 2, false, false);
            InputParser inputParser = new InputParser(new IntervalParser(), new ChordTypeParser());

            // Space, mixolydian b6 hindu or Major3
            ConfusionManager confusionList = new ConfusionManager();
            confusionList.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.FlatSixth, ChordType.Major),
                new ProgressionType(ChordType.Major, IntervalType.AugmentedFourthDiminishedFifthTritone, ChordType.Major),
                new ProgressionType(ChordType.Major, IntervalType.MajorThird, ChordType.Major));

            // Perfect fifth or perfect fourth
            confusionList.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.PerfectFifth, ChordType.Major),
                new ProgressionType(ChordType.Major, IntervalType.PerfectFourth, ChordType.Major));

            // Ionadimic or Relative minor space
            confusionList.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.AugmentedFourthDiminishedFifthTritone, ChordType.Minor),
                new ProgressionType(ChordType.Major, IntervalType.MinorThird, ChordType.Minor));

            // Thacrimic or augmented minor to major or minor to parallel major or time reversed 'major to relative minor of space / petrushka' or minor third third minor third
            confusionList.AddConfusion(new ProgressionType(ChordType.Minor, IntervalType.AugmentedFourthDiminishedFifthTritone, ChordType.Major),
                new ProgressionType(ChordType.Minor, IntervalType.MajorThird, ChordType.Major),
                new ProgressionType(ChordType.Minor, IntervalType.Sixth, ChordType.Major),
                new ProgressionType(ChordType.Minor, IntervalType.MinorThird, ChordType.Minor),
                new ProgressionType(ChordType.Minor, IntervalType.UnisonOctave, ChordType.Major));

            // Time reversed ultraphrygian minor or vader
            confusionList.AddConfusion(new ProgressionType(ChordType.Minor, IntervalType.MajorSeventh, ChordType.Minor),
                new ProgressionType(ChordType.Minor, IntervalType.FlatSixth, ChordType.Minor));

            // Midgar or evil danger
            confusionList.AddConfusion(new ProgressionType(ChordType.Minor, IntervalType.Sixth, ChordType.Minor),
                new ProgressionType(ChordType.Minor, IntervalType.AugmentedFourthDiminishedFifthTritone, ChordType.Minor));

            // Deceptive cadence or aeolian iv or aeolian v or dorian IV
            confusionList.AddConfusion(new ProgressionType(ChordType.Minor, IntervalType.FlatSeventh, ChordType.Major),
                new ProgressionType(ChordType.Minor, IntervalType.PerfectFourth, ChordType.Minor),
                new ProgressionType(ChordType.Minor, IntervalType.PerfectFifth, ChordType.Minor),
                new ProgressionType(ChordType.Minor, IntervalType.PerfectFourth, ChordType.Major));

            // Mixolydian v or plagal
            confusionList.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.PerfectFifth, ChordType.Minor),
                new ProgressionType(ChordType.Major, IntervalType.PerfectFourth, ChordType.Major));

            // Mixolydian b6 (hindu) or plagal minor or phrygian dominant viib
            confusionList.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.FlatSixth, ChordType.Major),
                new ProgressionType(ChordType.Major, IntervalType.PerfectFourth, ChordType.Minor),
                new ProgressionType(ChordType.Major, IntervalType.FlatSeventh, ChordType.Minor));

            Instrument harpsichord = new Harpsichord();

            MusicPlayer musicPlayer = new MusicPlayer(random, harpsichord, midiChannel);

            while (true)
            {
                Progression progression = questionBuilder.BuildProgression();

                if (isShowAnswerFirst)
                {
                    Console.WriteLine(progression);
                }

                Console.WriteLine("Intervals: 0, 2b, 2, 3m, 3M, 4, 4A, 5, 6b, 6, 7m, 7M");
                Console.WriteLine("Chord types: M, m, M7, m7, 7, dim7, aug, hdim, dim");
                Console.WriteLine("Type something like M 4 m (for minor plagal cadence), exit to quit");

                musicPlayer.Play(progression);

                string input = Console.ReadLine();

                musicPlayer.Stop();

                if (input.ToUpperInvariant().Contains("EXIT"))
                {
                    break;
                }

                if (!string.IsNullOrWhiteSpace(input))
                {
                    Progression parsedInput = inputParser.Parse(input, progression.PitchOffset);

                    Console.WriteLine("Parsed as " + parsedInput.ToString());

                    if (progression.ToString() == parsedInput.ToString())
                    {
                        Console.WriteLine("MATCH!");
                    }
                    else
                    {
                        Console.WriteLine("WRONG, it is " + progression.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("It was");
                    Console.WriteLine(progression.ToString());
                }

                Console.WriteLine("Ready? press enter to continue");
                Console.ReadLine();

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
    }
}
