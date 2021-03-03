using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicChordIntervalEarTraining
{
    public class Progression : IEnumerable<Chord>
    {
        #region Members
        private List<Chord> chords;

        private List<IntervalType> intervals;
        #endregion

        #region Constructors
        public Progression()
        {
            this.chords = new List<Chord>();
            this.intervals = new List<IntervalType>();
        }
        #endregion

        #region Properties
        public int PitchOffset
        {
            get { return (int)this.chords[0].NoteType; }
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

            if (this.chords.Count > 1)
            {
                Chord previousChord = this.chords[this.chords.Count - 2];
                IntervalType interval = this.GetIntervalType(previousChord, chord);
                this.intervals.Add(interval);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int chordIndex = 0;
            foreach (Chord chord in chords)
            {
                stringBuilder.AppendLine(chord.ToString());

                if (chordIndex > 0)
                {
                    stringBuilder.AppendLine(this.intervals[chordIndex - 1].ToString());
                }

                ++chordIndex;
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
