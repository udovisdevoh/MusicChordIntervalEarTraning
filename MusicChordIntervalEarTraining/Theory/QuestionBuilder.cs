using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicChordIntervalEarTraining
{
    public class QuestionBuilder
    {
        #region Members
        private Random random;

        private int chordCount;

        private bool isAllowSeventh;

        private bool isAllowAugmentedAndDiminished;
        #endregion

        #region Constructors
        public QuestionBuilder(Random random, int chordCount, bool isAllowSeventh, bool isAllowAugmentedAndDiminished)
        {
            this.random = random;
            this.chordCount = chordCount;
            this.isAllowSeventh = isAllowSeventh;
            this.isAllowAugmentedAndDiminished = isAllowAugmentedAndDiminished;
        }
        #endregion

        public Progression BuildProgression()
        {
            Progression question = new Progression();

            for (int currentCount = 0; currentCount < this.chordCount; ++currentCount)
            {
                Chord chord = this.BuildRandomChord();
                question.AddChord(chord);
            }

            return question;
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