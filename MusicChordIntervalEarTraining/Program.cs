using Sanford.Multimedia.Midi;
using System;

namespace MusicChordIntervalEarTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            const int midiChannel = 1;
            Random random = new Random();
            QuestionBuilder questionBuilder = new QuestionBuilder(random, 2, false, false);
            InputParser inputParser = new InputParser(new IntervalParser(), new ChordTypeParser());
            using (OutputDevice outputDevice = new OutputDevice(0))
            {
                //outputDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 1, 64, 64));

                MusicPlayer musicPlayer = new MusicPlayer(random, outputDevice, midiChannel);

                while (true)
                {
                    Progression progression = questionBuilder.BuildProgression();

                    Console.WriteLine(progression);

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


                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                }

            }
        }
    }
}
