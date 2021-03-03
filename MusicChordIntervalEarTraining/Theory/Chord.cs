using System;
using System.Collections;
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

        private int[] notes;
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

        public int[] GetNotes()
        {
            if (this.notes == null)
            {
                List<int> noteList = new List<int>();

                if (chordType == ChordType.Augmented)
                {
                    noteList.Add((int)NoteType.C + (int)noteType);
                    noteList.Add((int)NoteType.E + (int)noteType);
                    noteList.Add((int)NoteType.GSharp + (int)noteType);
                }
                else if (chordType == ChordType.Diminished7)
                {
                    noteList.Add((int)NoteType.C + (int)noteType);
                    noteList.Add((int)NoteType.DSharp + (int)noteType);
                    noteList.Add((int)NoteType.FSharp + (int)noteType);
                    noteList.Add((int)NoteType.A + (int)noteType);
                }
                else if (chordType == ChordType.DiminishedTriad)
                {
                    noteList.Add((int)NoteType.C + (int)noteType);
                    noteList.Add((int)NoteType.DSharp + (int)noteType);
                    noteList.Add((int)NoteType.FSharp + (int)noteType);
                }
                else if (chordType == ChordType.Dominant7)
                {
                    noteList.Add((int)NoteType.C + (int)noteType);
                    noteList.Add((int)NoteType.E + (int)noteType);
                    noteList.Add((int)NoteType.G + (int)noteType);
                    noteList.Add((int)NoteType.ASharp + (int)noteType);
                }
                else if (chordType == ChordType.HalfDiminished7)
                {
                    noteList.Add((int)NoteType.C + (int)noteType);
                    noteList.Add((int)NoteType.DSharp + (int)noteType);
                    noteList.Add((int)NoteType.FSharp + (int)noteType);
                    noteList.Add((int)NoteType.ASharp + (int)noteType);
                }
                else if (chordType == ChordType.Major)
                {
                    noteList.Add((int)NoteType.C + (int)noteType);
                    noteList.Add((int)NoteType.E + (int)noteType);
                    noteList.Add((int)NoteType.G + (int)noteType);
                }
                else if (chordType == ChordType.Major7)
                {
                    noteList.Add((int)NoteType.C + (int)noteType);
                    noteList.Add((int)NoteType.E + (int)noteType);
                    noteList.Add((int)NoteType.G + (int)noteType);
                    noteList.Add((int)NoteType.B + (int)noteType);
                }
                else if (chordType == ChordType.Minor)
                {
                    noteList.Add((int)NoteType.C + (int)noteType);
                    noteList.Add((int)NoteType.DSharp + (int)noteType);
                    noteList.Add((int)NoteType.G + (int)noteType);
                }
                else if (chordType == ChordType.Minor7)
                {
                    noteList.Add((int)NoteType.C + (int)noteType);
                    noteList.Add((int)NoteType.DSharp + (int)noteType);
                    noteList.Add((int)NoteType.G + (int)noteType);
                    noteList.Add((int)NoteType.ASharp + (int)noteType);
                }

                this.notes = noteList.ToArray();
            }

            return this.notes;
        }
    }
}
