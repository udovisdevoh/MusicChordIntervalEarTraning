using System;

namespace MusicChordIntervalEarTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            QuestionBuilder questionBuilder = new QuestionBuilder(new Random(), 2, false, false);

            while (true)
            {
                Question question = questionBuilder.BuildQuestion();

                Console.WriteLine(question);

                Console.WriteLine("Intervals: 0, 2b, 2, 3m, 3M, 4, 4A, 5, 6b, 6, 7m, 7M");
                Console.WriteLine("Chord types: M, m, M7, m7, 7, dim7, aug, hdim, dim");
                Console.WriteLine("Type something like M 4 m (for minor plagal cadence), exit to quit");

                string input = Console.ReadLine();

                input = SanitizeInput(input);

                if (input.ToUpperInvariant() == "EXIT")
                {
                    break;
                }

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
            };
        }

        private static string SanitizeInput(string input)
        {
            input = input.Replace("-", " ");
            input = input.Replace("\t", " ");
            input = input.Replace("\r", " ");
            input = input.Replace("\n", " ");

            while (input.Contains("  "))
            {
                input = input.Replace("  ", " ");
            }

            return input;
        }
    }
}
