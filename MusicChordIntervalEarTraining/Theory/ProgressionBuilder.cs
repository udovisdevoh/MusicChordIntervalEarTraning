using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicChordIntervalEarTraining
{
    public class ProgressionBuilder
    {
        #region Members
        private Random random;

        private int chordCount;

        private bool isAllowSeventh;

        private bool isAllowAugmentedAndDiminished;
        #endregion

        #region Constructors
        public ProgressionBuilder(Random random, int chordCount, bool isAllowSeventh, bool isAllowAugmentedAndDiminished)
        {
            this.random = random;
            this.chordCount = chordCount;
            this.isAllowSeventh = isAllowSeventh;
            this.isAllowAugmentedAndDiminished = isAllowAugmentedAndDiminished;
        }
        #endregion

        public Progression BuildProgression()
        {
            Progression progression = new Progression();

            for (int currentCount = 0; currentCount < this.chordCount; ++currentCount)
            {
                Chord chord = this.BuildRandomChord();
                progression.AddChord(chord);
            }

            return progression;
        }

        public Progression BuildProgression(ProgressionType[] progressionTypes)
        {
            int progressionTypeIndex = random.Next(0, progressionTypes.Length);
            ProgressionType progressionType = progressionTypes[progressionTypeIndex];

            Progression progression = new Progression();

            int noteType1 = random.Next(0, 12);
            int noteType2 = noteType1 + (int)progressionType.IntervalType;

            while (noteType2 > 12)
            {
                noteType2 -= 12;
            }

            while (noteType2 < 0)
            {
                noteType2 += 12;
            }

            progression.AddChord(new Chord(progressionType.Chord1Type, (NoteType)noteType1));
            progression.AddChord(new Chord(progressionType.Chord2Type, (NoteType)noteType2));

            return progression;
        }

        private Chord BuildRandomChord()
        {
            ChordType chordType = this.BuildRandomChordType();
            NoteType noteType = this.BuildNoteType();

            return new Chord(chordType, noteType);
        }

        private ChordType BuildRandomChordType()
        {
            if (this.isAllowAugmentedAndDiminished)
            {
                return (ChordType)random.Next(0, 9);
            }
            else if (this.isAllowSeventh)
            {
                return (ChordType)random.Next(0, 5);
            }
            else
            {
                return (ChordType)random.Next(0, 2);
            }
        }

        private NoteType BuildNoteType()
        {
            return (NoteType)random.Next(0, 11);
        }
    }
}