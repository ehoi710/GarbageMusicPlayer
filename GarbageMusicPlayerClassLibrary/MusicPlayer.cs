using NAudio.Wave;
using System;

namespace GarbageMusicPlayerClassLibrary
{
    /// <summary>
    /// 음악이 종료되었을 때 func를 호출하는 MusicPlayer 클래스입니다.
    /// </summary>
    public class MusicPlayer
    {
        public bool IsPlay { get; set; }

        public float Volume
        {
            get
            {
                if (IsNull()) return 0.0f;
                return wavePlayer.Volume;
            }
            set
            {
                if (IsNull()) return;
                wavePlayer.Volume = value;
            }
        }

        public double CurrentSecond
        {
            get {
                if (IsNull()) return 0.0f;
                return reader.CurrentTime.TotalSeconds;
            }
            set {
                if (IsNull()) return;
                reader.CurrentTime = TimeSpan.FromSeconds(value); 
            }
        }
        public double TotalSecond
        {
            get {
                if (IsNull()) return 0.0f;
                return totalTime.TotalSeconds; 
            }
        }
        public double PausedTime { get; set; }

        public MusicPlayer()
        {
            Reset();
            wavePlayer = new WaveOutEvent();
        }
        ~MusicPlayer()
        {
            this.Dispose();
        }

        public void Reset()
        {
            IsPlay = false;
            Volume = 0.2f;

            reader = null;
        }

        public void SetStoppedEventHandler(EventHandler<StoppedEventArgs> stoppedEventArgs)
        {
            wavePlayer.PlaybackStopped += stoppedEventArgs;
        }
        public void SetReader(MusicInfo music)
        {
            if(music == null)
            {
                reader = null;
                return;
            }

            reader = new AudioFileReader(music.path);
            wavePlayer.Init(reader);
            totalTime = reader.TotalTime;
        }

        public void Play()
        {
            if (IsNull()) return;

            IsPlay = true;
            wavePlayer.Play();
        }
        public void Play(TimeSpan time)
        {
            if (IsNull()) return;

            IsPlay = true;
            reader.CurrentTime = time;
            wavePlayer.Play();
        }
        public TimeSpan Pause()
        {
            if (IsNull()) return TimeSpan.FromSeconds(0.0f);

            IsPlay = false;
            PausedTime = reader.CurrentTime.TotalSeconds;

            wavePlayer.Pause();
            return reader.CurrentTime;
        }
        public void Stop()
        {
            wavePlayer.Stop();
            Reset();
        }

        public bool IsNull()
        {
            return (reader == null);
        }
        public bool IsEnd()
        {
            if (IsNull()) return true;
            return (reader.CurrentTime == reader.TotalTime);
        }

        public void Dispose()
        {
            this.reader.Dispose();
            this.wavePlayer.Dispose();
        }

        private readonly IWavePlayer wavePlayer;

        private AudioFileReader reader;

        private TimeSpan totalTime;
    }
}
