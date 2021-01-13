using NAudio.Wave;
using System;

namespace GarbageMusicPlayerClassLibrary
{
    /// <summary>
    /// 음악이 종료되었을 때 func를 호출하는 MusicPlayer 클래스입니다.
    /// </summary>
    public class MusicPlayer
    {
        private bool _isPlay;
        private readonly IWavePlayer _wavePlayer;
        private AudioFileReader _reader;
        private TimeSpan _totalTime;

        private static readonly MusicPlayer instance = null;
        private MusicPlayer(EventHandler<StoppedEventArgs> func)
        {
            this._isPlay = false;
            this._wavePlayer = new WaveOutEvent();
            this._wavePlayer.PlaybackStopped += func;
            this._reader = null;
        }

        public static MusicPlayer GetInstance(EventHandler<StoppedEventArgs> func)
        {
            return instance ?? new MusicPlayer(func);
        }

        public bool IsNull()
        {
            return (_reader == null);
        }

        public void SetReader(string mp3Path)
        {
            _reader = new AudioFileReader(mp3Path);
            _wavePlayer.Init(_reader);
            _totalTime = _reader.TotalTime;
        }

        public void SetReader(MusicInfo music)
        {
            if(music == null)
            {
                _reader = null;
                return;
            }
            _reader = new AudioFileReader(music.path);
            _wavePlayer.Init(_reader);
            _totalTime = _reader.TotalTime;
        }

        public void SetVolume(float volume)
        {
            _wavePlayer.Volume = volume;
        }

        public double GetTotalSecond()
        {
            return _totalTime.TotalSeconds;
        }

        public void SetCurrentSecond(double sec)
        {
            _reader.CurrentTime = TimeSpan.FromSeconds(sec);
        }

        public double GetCurrentSecond()
        {
            return _reader.CurrentTime.TotalSeconds;
        }

        public void Play()
        {
            _isPlay = true;
            _wavePlayer.Play();
        }

        public void Play(TimeSpan time)
        {
            _isPlay = true;
            _wavePlayer.Play();
            _reader.CurrentTime = time;
        }

        public TimeSpan Pause()
        {
            _isPlay = false;
            _wavePlayer.Pause();
            return _reader.CurrentTime;
        }

        public void PlayToggle()
        {
            if(IsPlay())
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        public void Stop()
        {
            _isPlay = false;
            _wavePlayer.Stop();
        }

        public bool IsPlay()
        {
            return _isPlay;
        }

        public bool IsEnd()
        {
            if (_reader == null)
                return true;
            return (_reader.CurrentTime == _reader.TotalTime);
        }
    }
}
