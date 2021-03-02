using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicChordIntervalEarTraining
{
    public class IntervalParser
    {
        public IntervalType Parse(string token)
        {
            IntervalType intervalType;
            if (!this.TryParse(token, out intervalType))
            {
                return IntervalType.Unknown;
            }
            return intervalType;
        }

        private bool TryParse(string token, out IntervalType intervalType)
        {
            intervalType = IntervalType.Unknown;

            if (token.Contains("0") || token.Contains("8"))
            {
                intervalType = IntervalType.UnisonOctave;
                return true;
            }
            else if (token.Contains("2"))
            {
                if (this.IsMinor(token))
                {
                    intervalType = IntervalType.MinorSecond;
                    return true;
                }
                else
                {
                    intervalType = IntervalType.MajorSecond;
                    return true;
                }
            }
            else if (token.Contains("3"))
            {
                if (this.IsMinor(token))
                {
                    intervalType = IntervalType.MinorThird;
                    return true;
                }
                else
                {
                    intervalType = IntervalType.MajorThird;
                    return true;
                }
            }
            else if (token.Contains("4") || token.ToUpperInvariant().Contains("FOUR"))
            {
                if (this.IsAugmented(token))
                {
                    intervalType = IntervalType.AugmentedFourthDiminishedFifthTritone;
                    return true;
                }
                else
                {
                    intervalType = IntervalType.PerfectFourth;
                    return true;
                }
            }
            else if (token.ToUpperInvariant().Contains("TRIT"))
            {
                intervalType = IntervalType.AugmentedFourthDiminishedFifthTritone;
                return true;
            }
            else if (token.Contains("5") || token.ToUpperInvariant().Contains("FI"))
            {
                if (this.IsAugmented(token))
                {
                    intervalType = IntervalType.FlatSixth;
                    return true;
                }
                else if (this.IsDiminished(token))
                {
                    intervalType = IntervalType.AugmentedFourthDiminishedFifthTritone;
                    return true;
                }
                else
                {
                    intervalType = IntervalType.PerfectFifth;
                    return true;
                }
            }
            else if (token.Contains("7") || token.ToUpperInvariant().Contains("SEV"))
            {
                if (this.IsMinor(token))
                {
                    intervalType = IntervalType.FlatSeventh;
                    return true;
                }
                else
                {
                    intervalType = IntervalType.MajorSeventh;
                    return true;
                }
            }
            else if (token.Contains("6") || token.ToUpperInvariant().Contains("SIX"))
            {
                if (this.IsMinor(token))
                {
                    intervalType = IntervalType.FlatSixth;
                    return true;
                }
                else
                {
                    intervalType = IntervalType.Sixth;
                    return true;
                }
            }

            return false;
        }

        private bool IsAugmented(string token)
        {
            return token.ToUpperInvariant().Contains("A");
        }

        private bool IsDiminished(string token)
        {
            return token.ToUpperInvariant().Contains("D");
        }

        private bool IsMinor(string token)
        {
            if (token.ToUpperInvariant().Contains("MIN"))
            {
                return true;
            }
            else if (token.ToUpperInvariant().Contains("B"))
            {
                return true;
            }
            else if (token.ToUpperInvariant().Contains("FLAT"))
            {
                return true;
            }
            else if (token.Contains("m") && !IsMajor(token))
            {
                return true;
            }
            return false;
        }

        private bool IsMajor(string token)
        {
            if (token.ToUpperInvariant().Contains("MAJ"))
            {
                return true;
            }
            else if (token.Contains("M"))
            {
                return true;
            }
            return false;
        }
    }
}
