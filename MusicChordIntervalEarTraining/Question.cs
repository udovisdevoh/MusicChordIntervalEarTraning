using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicChordIntervalEarTraining
{
    public class Question : IEnumerable<Chord>
    {
        #region Members
        private List<Chord> chords;
        #endregion

        #region Constructors
        public Question()
        {
            this.chords = new List<Chord>();
        }
        #endregion

        public IEnumerator<Chord> GetEnumerator()
        {
            return chords.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return chords.GetEnumerator();
        }

        public void AddChord(Chord chord)
        {
            this.chords.Add(chord);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Chord previousChord = null;
            foreach (Chord chord in chords)
            {
                stringBuilder.AppendLine(chord.ToString());

                if (previousChord != null)
                {
                    IntervalType intervalType = this.GetIntervalType(previousChord, chord);

                    stringBuilder.AppendLine(intervalType.ToString());
                }

                previousChord = chord;
            }

            return stringBuilder.ToString();
        }

        private IntervalType GetIntervalType(Chord chord1, Chord chord2)
        {
            int distance = (int)chord2.NoteType - (int)chord1.NoteType;

            while (distance < 0)
            {
                distance += 12;
            }

            while (distance > 12)
            {
                distance -= 12;
            }

            return (IntervalType)distance;
        }
    }
}
