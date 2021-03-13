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

        private Instrument instrument;

        private int currentPlayCount = 0;

        public MusicPlayer(Random random, Instrument instrument)
        {
            this.random = random;
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
            {*/
                chordLength = 8;
                noteLength = 200 - 25 + random.Next(0, 50);
            /*}
            else
            {
                chordLength = 12;
                noteLength = 150 - 19 + random.Next(0, 38);
            }*/



            int octaveOffset = (random.Next(0, 2) + 2) * 12;

            int rythmSeed = random.Next();

            Thread playerThread = new Thread(() =>
            {
                while (isPlaying)
                {
                    for (int loopIndex = 0; loopIndex < 1; ++loopIndex)
                    {
                        foreach (Chord chord in progression)
                        {
                            int[] notes = chord.GetNotes();
                            Shuffle(notes, rythmSeed);

                            this.Play(chordLength, noteLength, octaveOffset, rythmSeed, notes);

                            if (!isPlaying)
                            {
                                break;
                            }
                        }
                    }

                    int[] lastNotes = progression.First().GetNotes();
                    Shuffle(lastNotes, rythmSeed);

                    this.Play(chordLength + 1, noteLength, octaveOffset, rythmSeed, lastNotes);
                    this.Silence(chordLength - 2, noteLength);

                    ++this.currentPlayCount;

                    this.Silence(1, noteLength);
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

        private void Play(int chordLength, int noteLength, int octaveOffset, int rythmSeed, int[] notes)
        {
            Random rythmRandom = new Random(rythmSeed);

            int noteCount = 0;
            while (true)
            {
                foreach (int note in notes)
                {
                    if (noteCount == 0 || noteCount % 2 == 0 || random.Next(0, 2) != 0)
                    {
                        int parallelNoteCount = this.random.Next(0, 2) + 1;
                        for (int noteIndex = 0; noteIndex < parallelNoteCount; ++noteIndex)
                        {
                            if (rythmRandom.Next(0, 2) == 0)
                            {
                                Play(note + octaveOffset, noteLength);
                            }
                            else
                            {
                                if (rythmRandom.Next(0, 2) == 0)
                                {
                                    Play(note + 12 + octaveOffset, noteLength);
                                }
                                else
                                {
                                    Play(note + 24 + octaveOffset, noteLength);
                                }
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

        private void Shuffle(int[] notes, int rythmSeed)
        {
            Random shuffleRandom = new Random(rythmSeed);

            for (int index = 0; index < notes.Length; ++index)
            {
                int noteAtIndex = notes[index];
                int randomIndex = shuffleRandom.Next(0, notes.Length);
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
                this.currentPlayCount = 0;
                this.isPlaying = false;
            }

            while (!isFinishedPlaying)
            {
                Thread.Sleep(10);
            }
        }

        public void WaitUntilFinishedRepeat(int repeatCount)
        {
            while (this.currentPlayCount < repeatCount)
            {
                Thread.Sleep(5);
            }
            Stop();
        }
    }
}
