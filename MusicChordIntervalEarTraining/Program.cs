using Sampler;
using System;

namespace MusicChordIntervalEarTraining
{
    public class Program
    {
        #warning Show or hide answers here
        private static readonly bool isShowAnswerFirst = true;

        static void Main(string[] args)
        {
            const int midiChannel = 1;
            Random random = new Random();
            ProgressionBuilder progressionBuilder = new ProgressionBuilder(random, 2, false, false);
            InputParser inputParser = new InputParser(new IntervalParser(), new ChordTypeParser());

            ConfusionManager confusionManager = new ConfusionManager();

            // Plagal only
            confusionManager.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.PerfectFourth, ChordType.Major));

            // Perfect fifth or perfect fourth
            confusionManager.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.PerfectFifth, ChordType.Major),
                new ProgressionType(ChordType.Major, IntervalType.PerfectFourth, ChordType.Major));

            // Minor 2 or relative minor
            confusionManager.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.MajorSecond, ChordType.Minor),
                new ProgressionType(ChordType.Major, IntervalType.Sixth, ChordType.Minor));

            // Space, mixolydian b6 hindu or Major3 or phrygian dominant or Ionadimic
            confusionManager.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.FlatSixth, ChordType.Major),
                new ProgressionType(ChordType.Major, IntervalType.AugmentedFourthDiminishedFifthTritone, ChordType.Major),
                new ProgressionType(ChordType.Major, IntervalType.MajorThird, ChordType.Major),
                new ProgressionType(ChordType.Major, IntervalType.MinorSecond, ChordType.Major),
                new ProgressionType(ChordType.Major, IntervalType.AugmentedFourthDiminishedFifthTritone, ChordType.Minor));

            // Phrygian or Batman 1989 reversed phrygian dominant viib
            confusionManager.AddConfusion(new ProgressionType(ChordType.Minor, IntervalType.MinorSecond, ChordType.Major),
                new ProgressionType(ChordType.Minor, IntervalType.MajorSecond, ChordType.Major));

            // Batman 2008 or Batman 1989
            confusionManager.AddConfusion(new ProgressionType(ChordType.Minor, IntervalType.MajorSeventh, ChordType.Major),
                new ProgressionType(ChordType.Minor, IntervalType.MajorSecond, ChordType.Major));

            // Ionadimic or Relative minor space
            confusionManager.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.AugmentedFourthDiminishedFifthTritone, ChordType.Minor),
                new ProgressionType(ChordType.Major, IntervalType.MinorThird, ChordType.Minor));

            // Thacrimic or augmented minor to major or minor to parallel major or time reversed 'major to relative minor of space / petrushka' or minor third third minor third
            confusionManager.AddConfusion(new ProgressionType(ChordType.Minor, IntervalType.AugmentedFourthDiminishedFifthTritone, ChordType.Major),
                new ProgressionType(ChordType.Minor, IntervalType.MajorThird, ChordType.Major),
                new ProgressionType(ChordType.Minor, IntervalType.Sixth, ChordType.Major),
                new ProgressionType(ChordType.Minor, IntervalType.MinorThird, ChordType.Minor),
                new ProgressionType(ChordType.Minor, IntervalType.UnisonOctave, ChordType.Major));

            // Time reversed ultraphrygian minor, ultraphrygian minor or vader
            confusionManager.AddConfusion(new ProgressionType(ChordType.Minor, IntervalType.MajorSeventh, ChordType.Minor),
                new ProgressionType(ChordType.Minor, IntervalType.MinorSecond, ChordType.Minor),
                new ProgressionType(ChordType.Minor, IntervalType.FlatSixth, ChordType.Minor));

            // Midgar or evil danger
            confusionManager.AddConfusion(new ProgressionType(ChordType.Minor, IntervalType.Sixth, ChordType.Minor),
                new ProgressionType(ChordType.Minor, IntervalType.AugmentedFourthDiminishedFifthTritone, ChordType.Minor));

            // Deceptive cadence or aeolian iv or aeolian v or dorian IV
            confusionManager.AddConfusion(new ProgressionType(ChordType.Minor, IntervalType.FlatSeventh, ChordType.Major),
                new ProgressionType(ChordType.Minor, IntervalType.PerfectFourth, ChordType.Minor),
                new ProgressionType(ChordType.Minor, IntervalType.PerfectFifth, ChordType.Minor),
                new ProgressionType(ChordType.Minor, IntervalType.PerfectFourth, ChordType.Major));

            // Dorian VI or dorian ii or rock IIIb
            confusionManager.AddConfusion(new ProgressionType(ChordType.Minor, IntervalType.PerfectFourth, ChordType.Major),
                new ProgressionType(ChordType.Minor, IntervalType.MajorSecond, ChordType.Minor),
                new ProgressionType(ChordType.Major, IntervalType.MinorThird, ChordType.Major));

            // Mixolydian v or plagal
            confusionManager.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.PerfectFifth, ChordType.Minor),
                new ProgressionType(ChordType.Major, IntervalType.PerfectFourth, ChordType.Major));

            // Mixolydian b6 (hindu) or plagal minor or phrygian dominant viib
            confusionManager.AddConfusion(new ProgressionType(ChordType.Major, IntervalType.FlatSixth, ChordType.Major),
                new ProgressionType(ChordType.Major, IntervalType.PerfectFourth, ChordType.Minor),
                new ProgressionType(ChordType.Major, IntervalType.FlatSeventh, ChordType.Minor));

            Instrument instrument = new Piano();

            MusicPlayer musicPlayer = new MusicPlayer(random, instrument, midiChannel);

            while (true)
            {
                ProgressionType[] progressionTypes = confusionManager.GetProgressionTypes(0);
                #warning Activate, set or deactivate confusion manager here
                //Progression progression = progressionBuilder.BuildProgression();
                Progression progression = progressionBuilder.BuildProgression(progressionTypes);

                if (isShowAnswerFirst)
                {
                    Console.Clear();
                    Console.WriteLine(progression);
                }
                else
                {
                    Console.WriteLine("Intervals: 0, 2b, 2, 3m, 3M, 4, 4A, 5, 6b, 6, 7m, 7M");
                    Console.WriteLine("Chord types: M, m, M7, m7, 7, dim7, aug, hdim, dim");
                    Console.WriteLine("Type something like M 4 m (for minor plagal cadence), exit to quit");
                }

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
                else if (!isShowAnswerFirst)
                {
                    Console.WriteLine("It was");
                    Console.WriteLine(progression.ToString());

                    Console.WriteLine("Ready? press enter to continue");
                    Console.ReadLine();
                }

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
    }
}
