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

            string firstModalName = this.GetModalName(chords[0].ChordType, this.intervals[0], chords[1].ChordType);
            if (!string.IsNullOrWhiteSpace(firstModalName))
            {
                stringBuilder.AppendLine(firstModalName);
            }

            foreach (Chord chord in chords)
            {
                stringBuilder.AppendLine(chord.ToString());

                if (chordIndex > 0)
                {
                    stringBuilder.AppendLine(this.intervals[chordIndex - 1].ToString());
                    if (chordIndex > 1)
                    {
                        string modalName = this.GetModalName(chords[chordIndex - 1].ChordType, this.intervals[chordIndex - 1], chord.ChordType);
                        if (!string.IsNullOrWhiteSpace(modalName))
                        {
                            stringBuilder.AppendLine(modalName);
                        }
                    }
                }

                ++chordIndex;
            }

            stringBuilder.AppendLine(chords.First().ToString());

            return stringBuilder.ToString();
        }

        private IntervalType GetIntervalType(Chord chord1, Chord chord2)
        {
            int distance = (int)chord2.NoteType - (int)chord1.NoteType;

            while (distance < 0)
            {
                distance += 12;
            }

            while (distance >= 12)
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
                else if (intervalType == IntervalType.MinorSecond)
                {
                    return "Phrygian dominant";
                }
                else if (intervalType == IntervalType.MajorSecond)
                {
                    return "Lydian";
                }
                else if (intervalType == IntervalType.MinorThird)
                {
                    return "Blues / Rock IIIb";
                }
                else if (intervalType == IntervalType.MajorThird)
                {
                    return "Creep / Santeria";
                }
                else if (intervalType == IntervalType.PerfectFourth)
                {
                    return "Plagal";
                }
                else if (intervalType == IntervalType.AugmentedFourthDiminishedFifthTritone)
                {
                    return "Petruskha / space";
                }
                else if (intervalType == IntervalType.PerfectFifth)
                {
                    return "Perfect cadence";
                }
                else if (intervalType == IntervalType.FlatSixth)
                {
                    return "Mixolydian b6 (Hindu) / Mario / Blues / Rock VIb";
                }
                else if (intervalType == IntervalType.Sixth)
                {
                    return "Picardy 3rd";
                }
                else if (intervalType == IntervalType.FlatSeventh)
                {
                    return "Mixolydian b7";
                }
                else if (intervalType == IntervalType.MajorSeventh)
                {
                    return "Time reversed phrygian dominant";
                }
            }
            else if (IsMinor(chordType1) && IsMinor(chordType2))
            {
                if (intervalType == IntervalType.UnisonOctave)
                {
                    return "Minor unison";
                }
                else if (intervalType == IntervalType.MinorSecond)
                {
                    return "Ultraphrygian minor";
                }
                else if (intervalType == IntervalType.MajorSecond)
                {
                    return "Dorian minor ii";
                }
                else if (intervalType == IntervalType.MinorThird)
                {
                    return "Minor minor third minor";
                }
                else if (intervalType == IntervalType.MajorThird)
                {
                    return "Time reversed vader";
                }
                else if (intervalType == IntervalType.PerfectFourth)
                {
                    return "Aeolian iv";
                }
                else if (intervalType == IntervalType.AugmentedFourthDiminishedFifthTritone)
                {
                    return "Evil danger";
                }
                else if (intervalType == IntervalType.PerfectFifth)
                {
                    return "Aeolian v";
                }
                else if (intervalType == IntervalType.FlatSixth)
                {
                    return "Vader";
                }
                else if (intervalType == IntervalType.Sixth)
                {
                    return "Midgar";
                }
                else if (intervalType == IntervalType.FlatSeventh)
                {
                    return "Phrygian viib";
                }
                else if (intervalType == IntervalType.MajorSeventh)
                {
                    return "Time reversed 'ultraphrygian minor'";
                }
            }
            else if (IsMajor(chordType1) && IsMinor(chordType2))
            {
                if (intervalType == IntervalType.UnisonOctave)
                {
                    return "Major to parallel minor";
                }
                else if (intervalType == IntervalType.MinorSecond)
                {
                    return "Ultraphrygian major";
                }
                else if (intervalType == IntervalType.MajorSecond)
                {
                    return "Minor ii";
                }
                else if (intervalType == IntervalType.MinorThird)
                {
                    return "Major to relative minor of space / petrushka chord";
                }
                else if (intervalType == IntervalType.MajorThird)
                {
                    return "A day in the life";
                }
                else if (intervalType == IntervalType.PerfectFourth)
                {
                    return "Minor plagal";
                }
                else if (intervalType == IntervalType.AugmentedFourthDiminishedFifthTritone)
                {
                    return "Ionadimic";
                }
                else if (intervalType == IntervalType.PerfectFifth)
                {
                    return "Mixolydian v";
                }
                else if (intervalType == IntervalType.FlatSixth)
                {
                    return "Augmented (major to minor)";
                }
                else if (intervalType == IntervalType.Sixth)
                {
                    return "Relative minor (sixth)";
                }
                else if (intervalType == IntervalType.FlatSeventh)
                {
                    return "Phrygian dominant viib";
                }
                else if (intervalType == IntervalType.MajorSeventh)
                {
                    return "Lydian MAJ7th minor 'promenade sur mars'";
                }
            }
            else if (IsMinor(chordType1) && IsMajor(chordType2))
            {
                if (intervalType == IntervalType.UnisonOctave)
                {
                    return "Minor to parallel major";
                }
                else if (intervalType == IntervalType.MinorSecond)
                {
                    return "Phrygian";
                }
                else if (intervalType == IntervalType.MajorSecond)
                {
                    return "Batman 1989 (time reverse phrygian dominant viib)";
                }
                else if (intervalType == IntervalType.MinorThird)
                {
                    return "Minor to relative major";
                }
                else if (intervalType == IntervalType.MajorThird)
                {
                    return "Augmented (minor to major)";
                }
                else if (intervalType == IntervalType.PerfectFourth)
                {
                    return "Dorian IV";
                }
                else if (intervalType == IntervalType.AugmentedFourthDiminishedFifthTritone)
                {
                    return "Thacrimic";
                }
                else if (intervalType == IntervalType.PerfectFifth)
                {
                    return "Harmonic minor perfect cadence";
                }
                else if (intervalType == IntervalType.FlatSixth)
                {
                    return "Epic b6";
                }
                else if (intervalType == IntervalType.Sixth)
                {
                    return "Time reversed 'major to relative minor of space / petrushka chord'";
                }
                else if (intervalType == IntervalType.FlatSeventh)
                {
                    return "Deceptive cadence";
                }
                else if (intervalType == IntervalType.MajorSeventh)
                {
                    return "Batman 2008";
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
