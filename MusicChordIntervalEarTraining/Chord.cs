using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicChordIntervalEarTraining
{
    public class Chord
    {
        #region Members
        private ChordType chordType;

        private NoteType noteType;
        #endregion

        #region Constructors
        public Chord(ChordType chordType, NoteType noteType)
        {
            this.chordType = chordType;
            this.noteType = noteType;
        }
        #endregion

        #region Properties
        public ChordType ChordType
        {
            get { return this.chordType; }
        }

        public NoteType NoteType
        {
            get { return this.noteType; }
        }
        #endregion

        public override string ToString()
        {
            return $"{noteType} {chordType}";
        }
    }
}
