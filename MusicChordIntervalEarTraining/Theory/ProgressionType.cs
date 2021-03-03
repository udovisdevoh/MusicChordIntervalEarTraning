using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicChordIntervalEarTraining
{
    public class ProgressionType
    {
        private ChordType chord1Type;
        
        private IntervalType intervalType;
        
        private ChordType chord2Type;

        public ProgressionType(ChordType chord1Type, IntervalType intervalType, ChordType chord2Type)
        {
            this.chord1Type = chord1Type;
            this.intervalType = intervalType;
            this.chord2Type = chord2Type;
        }
    }
}
