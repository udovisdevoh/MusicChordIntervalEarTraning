using System;

namespace MusicChordIntervalEarTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            QuestionBuilder questionBuilder = new QuestionBuilder(new Random(), 2, false, false);
            InputParser inputParser = new InputParser(new IntervalParser(), new ChordTypeParser());

            while (true)
            {
                Progression question = questionBuilder.BuildQuestion();

                Console.WriteLine(question);

                Console.WriteLine("Intervals: 0, 2b, 2, 3m, 3M, 4, 4A, 5, 6b, 6, 7m, 7M");
                Console.WriteLine("Chord types: M, m, M7, m7, 7, dim7, aug, hdim, dim");
                Console.WriteLine("Type something like M 4 m (for minor plagal cadence), exit to quit");

                string input = Console.ReadLine();

                if (input.ToUpperInvariant().Contains("EXIT"))
                {
                    break;
                }

                Progression parsedInput = inputParser.Parse(input, question.PitchOffset);

                Console.WriteLine("Parsed as " + parsedInput.ToString());

                if (question.ToString() == parsedInput.ToString())
                {
                    Console.WriteLine("MATCH!");
                }
                else
                {
                    Console.WriteLine("WRONG, it is " + question.ToString());
                }


                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
            };
        }
    }
}
