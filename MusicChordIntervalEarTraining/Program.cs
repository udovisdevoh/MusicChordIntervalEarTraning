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

                Console.ReadLine();
            };
        }
    }
}
