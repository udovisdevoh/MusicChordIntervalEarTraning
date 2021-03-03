using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicChordIntervalEarTraining
{
    public class ProgressionType
    {
        #region
        private ChordType chord1Type;
        
        private IntervalType intervalType;
        
        private ChordType chord2Type;
        #endregion

        #region Properties
        public ChordType Chord1Type
        {
            get { return this.chord1Type; }
        }

        public ChordType Chord2Type
        {
            get { return this.chord2Type; }
        }

        public IntervalType IntervalType
        {
            get { return this.intervalType; }
        }
        #endregion

        #region Constructors
        public ProgressionType(ChordType chord1Type, IntervalType intervalType, ChordType chord2Type)
        {
            this.chord1Type = chord1Type;
            this.intervalType = intervalType;
            this.chord2Type = chord2Type;
        }
        #endregion
    }
}
