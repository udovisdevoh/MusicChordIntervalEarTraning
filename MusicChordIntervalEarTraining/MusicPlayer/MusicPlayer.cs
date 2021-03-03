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

        private bool isFinishedPlaying = true;

        private Random random;

        private int midiChannel;

        private OutputDevice outputDevice;

        public MusicPlayer(Random random, OutputDevice outputDevice, int midiChannel)
        {
            this.random = random;
            this.midiChannel = midiChannel;
            this.outputDevice = outputDevice;
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
            ChannelMessage noteOn = this.BuildChannelMessage(pitch, ChannelCommand.NoteOn);
            this.outputDevice.Send(noteOn);
            Thread.Sleep(100);
            ChannelMessage noteOff = this.BuildChannelMessage(pitch, ChannelCommand.NoteOff);
            this.outputDevice.Send(noteOff);
            Thread.Sleep(10);
        }

        private ChannelMessage BuildChannelMessage(int pitch, ChannelCommand command)
        {
            ChannelMessage channelMessage = new ChannelMessage(command, midiChannel, pitch, 127);
            return channelMessage;
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
