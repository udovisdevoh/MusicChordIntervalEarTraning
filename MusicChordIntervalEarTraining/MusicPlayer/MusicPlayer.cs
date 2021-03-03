using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace MusicChordIntervalEarTraining
{
    public class MusicPlayer
    {
        private object playerLock = new object();

        private bool isPlaying = false;

        private Random random;

        public MusicPlayer(Random random)
        {
            this.random = random;
        }

        public void Play(Progression progression)
        {
            this.Stop();

            Thread playerThread = new Thread(() =>
            {
                lock (this.playerLock)
                {
                    this.isPlaying = true;
                }

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
            });
            playerThread.IsBackground = true;
            playerThread.Start();
        }

        private void Play(Chord chord)
        {
            foreach (int note in chord)
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
                    break;
                }
            }
        }

        private void Play(int pitch)
        {
            ChannelMessage noteOn = this.BuildChannelMessage(pitch, true);
            Thread.Sleep(100);
            ChannelMessage noteOn = this.BuildChannelMessage(pitch, true);
        }

        public void Stop()
        {
            lock (this.playerLock)
            {
                this.isPlaying = false;
            }
        }
    }
}
