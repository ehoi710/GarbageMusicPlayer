using GarbageMusicPlayerClassLibrary;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GarbageMusicPlayer
{
    public partial class MainForm : Form
    {
        Bitmap DefaultBackgroundImage;
        Bitmap DefaultAlbumImage;

        ListForm PlayListForm;

        public MainForm()
        {
            InitializeComponent();

            InitializeMusicPlayer();
            InitializePlayList();

            InitializeMainForm();
            InitializeAlbumArt();
            InitializeMusicTrackBar();
            InitializeTitleTextBox();
            InitializePlayButton();
            InitializeMusicComment();
            InitializeListForm();

            AcceptParameter();
        }

        private void InitializeMusicPlayer()
        {
            //Initialize music player with stopped Event Handler MusicEndCheck
            Program.musicPlayer = new MusicPlayer();
            Program.musicPlayer.SetStoppedEventHandler(MusicEndCheck);
        }
        private void InitializePlayList()
        {
            Program.playList = new MusicList();
        }
        private void InitializeMainForm()
        {
            this.Size = new Size(450, 820);

            this.Text = "GMP";
            MusicPlayTimeCheckTimer.Stop();

            KeyPreview = true;
        }
        private void InitializeAlbumArt()
        {
            AlbumArtBox.Location = new Point(45, 155);
            AlbumArtBox.Size = new Size(360, 360);

            ResetAlbumArt();
        }
        private void InitializeMusicTrackBar()
        {
            musicTrackBar.Location = new Point(45, 525);
            musicTrackBar.Size = new Size(360, 30);

            ResetMusicTrackBar();
        }
        private void InitializeTitleTextBox()
        {
            TitleTextBox.Location = new Point(45, 580);
            TitleTextBox.Size = new Size(360, 35);

            TitleTextBox.Text = "TITLE";
        }
        private void InitializePlayButton()
        {
            PlayButton.Location = new Point(170, 655);
            PlayButton.Size = new Size(120, 50);
        }
        private void InitializeMusicComment()
        {
            MusicComment.Location = new Point(45, 55);
            MusicComment.Size = new Size(360, 100);
        }
        private void InitializeListForm()
        {
            PlayListForm = new ListForm
            {
                Location = new Point(Location.X + this.Width, this.Location.Y)
            };

            PlayListForm.Show();

            Program.playList = new MusicList();
        }

        private void AcceptParameter()
        {
            if (Program.isParam)
            {
                string[] arr = Program.parameter.Split('\\');
                Program.playList.Add(new MusicInfo(arr[arr.Length - 1], Program.parameter));
                PlayListForm.RefreshListView(Program.playList);
            }
        }

        private void DrawAlbumArt(MusicInfo info)
        {
            Size albumSize = AlbumArtBox.Size;
            Size backgroundSize = this.Size;

            if (info == null || info.AlbumImage == null)
            {
                ResetAlbumArt();
                return;
            }

            this.BackgroundImageLayout = ImageLayout.Center;

            Bitmap pictures = info.AlbumImage;
            pictures = ImageController.ResizeBitmapFitSmaller(pictures, albumSize);

            Bitmap blurred_pictures = info.BlurredImage;
            blurred_pictures = ImageController.ResizeBitmapFitSmaller(blurred_pictures, backgroundSize);
            blurred_pictures = ImageController.CropBitmap(blurred_pictures, backgroundSize);

            blurred_pictures = ImageController.DecreseValue(blurred_pictures, this.TitleTextBox.Location, this.TitleTextBox.Size);

            this.AlbumArtBox.Image = pictures;
            this.BackgroundImage = blurred_pictures;
        }
        private void ResetAlbumArt()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            SetDefaultBackgroundImage();
            SetDefaultAlbumArtImage();
        }

        private void ResetMusicTrackBar()
        {
            int max;
            if (Program.musicPlayer.IsNull())
            {
                max = 1;
            }
            else
            {
                max = (int)Program.musicPlayer.TotalSecond;
            }
            musicTrackBar.Max = max;
            musicTrackBar.CurrentTickPosition = 0;
        }

        private void MusicStop()
        {
            if (Program.musicPlayer.IsNull())
                return;

            Program.musicPlayer.Stop();
            PlayButton.Text = "Play";
            MusicPlayTimeCheckTimer.Stop();
        }
        private void MusicPause()
        {
            if (Program.musicPlayer.IsNull())
                return;

            Program.musicPlayer.Pause();
            PlayButton.Text = "Play";
            MusicPlayTimeCheckTimer.Stop();
        }
        private void MusicStart()
        {
            if (Program.musicPlayer.IsNull())
                return;

            Program.musicPlayer.Play();
            PlayButton.Text = "Pause";
            MusicPlayTimeCheckTimer.Start();
        }

        void SetDefaultBackgroundImage()
        {
            if (DefaultBackgroundImage == null)
                DefaultBackgroundImage = new Bitmap(Properties.Resources.DefaultBackground1);

            this.BackgroundImage = DefaultBackgroundImage;
        }
        void SetDefaultAlbumArtImage()
        {
            if (DefaultAlbumImage == null)
                DefaultAlbumImage = new Bitmap(Properties.Resources.noAlbumImage);

            AlbumArtBox.Image = DefaultAlbumImage;
        }

        private void ChangeSelectedItem(MusicInfo musicItem)
        {
            MusicStop();

            if (musicItem == null)
            {
                MessageBox.Show("Music Not Selected");

                ResetAlbumArt();
                return;
            }

            Program.musicPlayer.SetReader(musicItem);

            TitleTextBox.Text = musicItem.title;
            MusicComment.Text = musicItem.comment;

            DrawAlbumArt(musicItem);

            ResetMusicTrackBar();
        }

        private void SetPrevMusic()
        {
            MusicStop();
            Program.playList.MovePrev();
            ChangeSelectedItem(Program.playList.GetCurrentItem());
        }
        private void SetNextMusic()
        {
            MusicStop();
            Program.playList.MoveNext();
            ChangeSelectedItem(Program.playList.GetCurrentItem());
        }

        private void ToggleMusicPlayState()
        {
            if (Program.musicPlayer.IsPlay)
            {
                MusicPause();
            }
            else
            {
                MusicStart();
            }
        }
        private void MusicPauseOrStartEvent()
        {
            if (Program.musicPlayer.IsNull())
            {
                SetNextMusic();
                MusicStart();
            }
            else
            {
                ToggleMusicPlayState();
            }
        }

        private void MoveMusicTimeAsSecond(double second)
        {
            musicTrackBar.CurrentTickPosition += (int)(musicTrackBar.Max / Program.musicPlayer.TotalSecond * second);
            Program.musicPlayer.CurrentSecond = musicTrackBar.CurrentTickPosition;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Space:
                    MusicPauseOrStartEvent();
                    break;

                case Keys.Left:
                    MoveMusicTimeAsSecond(-5);
                    break;

                case Keys.Right:
                    MoveMusicTimeAsSecond(5);
                    break;

                case Keys.A:
                    SetPrevMusic();
                    break;

                case Keys.D:
                    SetNextMusic();
                    break;

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void PlayButton_Click(object sender, EventArgs e)
        {
            MusicPauseOrStartEvent();
            PlayButton.Enabled = false;
            PlayButton.Enabled = true;
        }

        private void MusicEndCheck(object sender, EventArgs e)
        {
            if (Program.musicPlayer.IsEnd())
            {
                SetNextMusic();
                MusicStart();
            }
        }
        private void FomeClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.musicPlayer != null)
                Program.musicPlayer.Stop();
        }
        private void MusicTrackBarScrolled(object sender, EventArgs e)
        {
            Program.musicPlayer.CurrentSecond = musicTrackBar.CurrentTickPosition;
        }
        private void MusicPlayStateChecker(object sender, EventArgs e)
        {
            musicTrackBar.CurrentTickPosition = (int)Program.musicPlayer.CurrentSecond;
        }

        protected override void WndProc(ref Message m)
        {
            switch(m.Msg)
            {
                case Win32.WM_COPYDATA:
                    Win32.CopyDataStruct st = (Win32.CopyDataStruct)Marshal.PtrToStructure(m.LParam, typeof(Win32.CopyDataStruct));
                    string strData = Marshal.PtrToStringUni(st.lpData);
                    Program.playList.Add(new MusicInfo(strData));
                    PlayListForm.RefreshListView(Program.playList);

                    Win32.SetForegroundWindow(PlayListForm.Handle);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
