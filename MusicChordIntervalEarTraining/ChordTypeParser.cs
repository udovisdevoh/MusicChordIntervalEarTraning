using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicChordIntervalEarTraining
{
    public class ChordTypeParser
    {
        public ChordType Parse(string token)
        {
            ChordType chordType;
            if (!this.TryParse(token, out chordType))
            {
                if (!this.TryParse(token.ToLowerInvariant(), out chordType))
                {
                    if (!this.TryParse(token.ToUpperInvariant(), out chordType))
                    {
                        return ChordType.Unknown;
                    }
                }
            }
            return chordType;
        }

        private bool TryParse(string token, out ChordType chordType)
        {
            token = token.Trim();
            chordType = ChordType.Unknown;

            if (token == "M")
            {
                chordType = ChordType.Major;
                return true;
            }
            else if (token == "M7")
            {
                chordType = ChordType.Major7;
                return true;
            }
            else if (token == "7M")
            {
                chordType = ChordType.Major7;
                return true;
            }
            else if (token == "m")
            {
                chordType = ChordType.Minor;
                return true;
            }
            else if (token == "m7")
            {
                chordType = ChordType.Minor7;
                return true;
            }
            else if (token == "7m")
            {
                chordType = ChordType.Minor7;
                return true;
            }
            else if (token == "7")
            {
                chordType = ChordType.Dominant7;
                return true;
            }
            else if (token.ToUpperInvariant() == "AUG")
            {
                chordType = ChordType.Augmented;
                return true;
            }
            else if (token.ToUpperInvariant().Contains("DIM"))
            {
                if (token.ToUpperInvariant().Contains("H"))
                {
                    chordType = ChordType.HalfDiminished7;
                    return true;
                }
                else if (token.Contains("7"))
                {
                    chordType = ChordType.Diminished7;
                    return true;
                }
                else
                {
                    chordType = ChordType.DiminishedTriad;
                    return true;
                }
            }
            else if (token.ToUpperInvariant().Contains("DOM") || token.ToUpperInvariant().Contains("SEVEN"))
            {
                chordType = ChordType.Dominant7;
                return true;
            }
            else if (token.ToUpperInvariant().Contains("MAJ"))
            {
                if (token.Contains("7"))
                {
                    chordType = ChordType.Major7;
                    return true;
                }
                else
                {
                    chordType = ChordType.Major;
                    return true;
                }
            }
            else if (token.ToUpperInvariant().Contains("MIN"))
            {
                if (token.Contains("7"))
                {
                    chordType = ChordType.Minor7;
                    return true;
                }
                else
                {
                    chordType = ChordType.Minor;
                    return true;
                }
            }

            return false;
        }
    }
}
