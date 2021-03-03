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
                    string modalName = this.GetModalName(chord.ChordType, this.intervals[chordIndex - 1], chords[chordIndex - 1].ChordType);
                    if (!string.IsNullOrWhiteSpace(modalName))
                    {
                        stringBuilder.AppendLine(modalName);
                    }
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

        private string GetModalName(ChordType chordType1, IntervalType intervalType, ChordType chordType2)
        {
            if (IsMajor(chordType1) && IsMajor(chordType2))
            { 
                if (intervalType == IntervalType.UnisonOctave)
                {
                    return "Major unison";
                }
            }
            else if (IsMinor(chordType1) && IsMinor(chordType2))
            {
                if (intervalType == IntervalType.UnisonOctave)
                {
                    return "Minor unison";
                }
            }

            return string.Empty;
        }

        private bool IsMajor(ChordType chordType)
        {
            return chordType == ChordType.Major || chordType == ChordType.Major7 || chordType == ChordType.Dominant7;
        }

        private bool IsMinor(ChordType chordType)
        {
            return chordType == ChordType.Minor || chordType == ChordType.Minor7;
        }
    }
}
