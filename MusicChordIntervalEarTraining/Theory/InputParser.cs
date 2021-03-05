using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicChordIntervalEarTraining
{
    public class InputParser
    {
        private IntervalParser intervalParser;

        private ChordTypeParser chordTypeParser;

        public InputParser(IntervalParser intervalParser, ChordTypeParser chordTypeParser)
        {
            this.intervalParser = intervalParser;
            this.chordTypeParser = chordTypeParser;
        }

        public Progression Parse(string input, int pitchOffset)
        {
            input = this.Sanitize(input);

            string[] tokens = input.Split(" ");

            bool isExpectedChord = true;

            Progression progression = new Progression();

            foreach (string token in tokens)
            {
                if (isExpectedChord)
                {
                    ChordType chordType = this.chordTypeParser.Parse(token);
                    Chord chord = new Chord(chordType, (NoteType)pitchOffset);
                    progression.AddChord(chord);
                }
                else
                {
                    IntervalType interval = this.intervalParser.Parse(token);
                    pitchOffset += (int)interval;

                    while (pitchOffset >= 12)
                    {
                        pitchOffset -= 12;
                    }
                    while (pitchOffset < 0)
                    {
                        pitchOffset += 12;
                    }
                }

                isExpectedChord = !isExpectedChord;
            }

            return progression;
        }

        internal Progression Parse(string input, object pitchOffset)
        {
            throw new NotImplementedException();
        }

        private string Sanitize(string input)
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
