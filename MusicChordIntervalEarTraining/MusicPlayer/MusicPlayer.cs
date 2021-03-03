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

        private const int defaultChordLength = 8;

        private const int noteLength = 200;

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

            Thread playerThread = new Thread(() =>
            {
                while (isPlaying)
                {
                    foreach (Chord chord in progression)
                    {
                        this.Play(chord, defaultChordLength);

                        if (!isPlaying)
                        {
                            break;
                        }
                    }

                    this.Play(progression.First(), defaultChordLength + 1);
                    this.Silence(defaultChordLength - 1);
                }

                lock (this.playerLock)
                {
                    this.isFinishedPlaying = true;
                }
            });
            playerThread.IsBackground = true;
            playerThread.Start();
        }

        private void Silence(int chordLength)
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

        private void Play(Chord chord, int chordLength)
        {
            int noteCount = 0;
            int[] notes = chord.GetNotes();
            Shuffle(notes);
            while (true)
            {
                foreach (int note in notes)
                {
                    if (random.Next(0, 2) == 0)
                    {
                        Play(24 + note);
                    }
                    else
                    {
                        if (random.Next(0, 2) == 0)
                        {
                            Play(24 + note + 12);
                        }
                        else
                        {
                            Play(24 + note + 24);
                        }
                    }

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
            #warning Implement
        }

        private void Play(int pitch)
        {
            this.instrument.Play(pitch, 127);
            Thread.Sleep(noteLength);
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
