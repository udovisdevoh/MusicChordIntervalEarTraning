using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sampler;

namespace MusicChordIntervalEarTraining
{
    public class MusicPlayer
    {
        private object playerLock = new object();

        private bool isPlaying = false;

        private bool isFinishedPlaying = true;

        private Random random;

        private int midiChannel;

        private Instrument instrument;

        public MusicPlayer(Random random, Instrument instrument, int midiChannel)
        {
            this.random = random;
            this.midiChannel = midiChannel;
            this.instrument = instrument;
        }

        public void Play(Progression progression)
        {
            this.Stop();

            lock (this.playerLock)
            {
                this.isPlaying = true;
                this.isFinishedPlaying = false;
            }

            int chordLength;
            int noteLength;
            /*if (random.Next(0, 2) == 0)
            {
                chordLength = 8;
                noteLength = 200 - 25 + random.Next(0, 50);
            }
            else
            {
                chordLength = 12;
                noteLength = 150 - 19 + random.Next(0, 38);
            }*/

            chordLength = 8;
            noteLength = 200;

            Thread playerThread = new Thread(() =>
            {
                while (isPlaying)
                {
                    foreach (Chord chord in progression)
                    {
                        this.Play(chord, chordLength, noteLength);

                        if (!isPlaying)
                        {
                            break;
                        }
                    }

                    this.Play(progression.First(), chordLength + 1, noteLength);
                    this.Silence(chordLength - 1, noteLength);
                }

                lock (this.playerLock)
                {
                    this.isFinishedPlaying = true;
                }
            });
            playerThread.IsBackground = true;
            playerThread.Start();
        }

        private void Silence(int chordLength, int noteLength)
        {
            for (int index = 0; index < chordLength; ++index)
            {
                Thread.Sleep(noteLength);
                if (!isPlaying)
                {
                    return;
                }
            }
        }

        private void Play(Chord chord, int chordLength, int noteLength)
        {
            int noteCount = 0;
            int[] notes = chord.GetNotes();
            Shuffle(notes);
            while (true)
            {
                foreach (int note in notes)
                {
                    int parallelNoteCount = this.random.Next(0, 2) + 1;
                    for (int noteIndex = 0; noteIndex < parallelNoteCount; ++noteIndex)
                    {
                        if (random.Next(0, 2) == 0)
                        {
                            Play(24 + note, noteLength);
                        }
                        else
                        {
                            if (random.Next(0, 2) == 0)
                            {
                                Play(24 + note + 12, noteLength);
                            }
                            else
                            {
                                Play(24 + note + 24, noteLength);
                            }
                        }
                    }
                    Thread.Sleep(noteLength);

                    if (!isPlaying)
                    {
                        return;
                    }
                    ++noteCount;

                    if (noteCount >= chordLength)
                    {
                        return;
                    }
                }
            }
        }

        private void Shuffle(int[] notes)
        {
            for (int index = 0; index < notes.Length; ++index)
            {
                int noteAtIndex = notes[index];
                int randomIndex = random.Next(0, notes.Length);
                int noteAtRandomIndex = notes[randomIndex];

                notes[index] = noteAtRandomIndex;
                notes[randomIndex] = noteAtIndex;
            }
        }

        private void Play(int pitch, int noteLength)
        {
            this.instrument.Play(pitch, 127);
        }

        public void Stop()
        {
            lock (this.playerLock)
            {
                this.isPlaying = false;
            }

            while (!isFinishedPlaying)
            {
                Thread.Sleep(10);
            }
        }
    }
}
