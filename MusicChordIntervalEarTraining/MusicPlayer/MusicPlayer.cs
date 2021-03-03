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

            Thread playerThread = new Thread(() =>
            {
                while (isPlaying)
                {
                    foreach (Chord chord in progression)
                    {
                        this.Play(chord);

                        if (!isPlaying)
                        {
                            break;
                        }
                    }

                    this.Play(progression.Last());
                }

                lock (this.playerLock)
                {
                    this.isFinishedPlaying = true;
                }
            });
            playerThread.IsBackground = true;
            playerThread.Start();
        }

        private void Play(Chord chord)
        {
            int noteCount = 0;
            int[] notes = this.GetNotes(chord);
            while (true)
            {
                Shuffle(notes);
                foreach (int note in notes)
                {
                    if (random.Next(0, 2) == 0)
                    {
                        Play(64 + note);
                    }
                    else
                    {
                        Play(64 + note + 12);
                    }

                    if (!isPlaying)
                    {
                        return;
                    }
                    ++noteCount;

                    if (noteCount >= 16)
                    {
                        return;
                    }
                }
            }
        }

        private int[] GetNotes(Chord chord)
        {
            #warning Implement
            return new int[] { 1, 2, 3 };
        }

        private void Shuffle(int[] notes)
        {
            #warning Implement
        }

        private void Play(int pitch)
        {
            this.instrument.Play(pitch, 127);
            Thread.Sleep(100);
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
