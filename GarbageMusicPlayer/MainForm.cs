using GarbageMusicPlayerClassLibrary;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GarbageMusicPlayer
{
    public partial class MainForm : Form
    {
        // Constructor
        public MainForm()
        {
            InitializeComponent();

            InitializeMusicPlayer();
            InitializePlayList();

            InitializeMainForm();
            InitializeCloseButton();
            InitializeAlbumArtAndLyrics();
            InitializeMusicTrackBar();
            InitializeTitleTextBox();
            InitializeMusicComment();
            InitializeListAndLyricsForm();

            AcceptParameter();
        }
        
        // Initailizer
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
            this.AutoScaleMode = AutoScaleMode.None;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackgroundImageLayout = ImageLayout.Center;
            this.DoubleBuffered = true;

            int unit = 8;
            this.Size = new Size(unit * 50, unit * 90);

            this.widthRatio = (double)this.Width / 450;
            this.heightRatio = (double)this.Height / 810;

            this.Text = "GMP";
            this.MusicPlayTimeCheckTimer.Stop();

            this.KeyPreview = true;
            this.mouseClicked = false;
        }

        private void InitializeCloseButton()
        {
            int width = 20;
            int height = 20;

            CloseButton.Location = new Point(this.Width - width, 0);
            CloseButton.Size = new Size(width, height);

            CloseButton.Click += FormCloseButtonClicked;
        }
        private void InitializeMusicComment()
        {
            MusicComment.Size = new Size(
                (int)(360 * this.widthRatio),
                (int)(90 * this.heightRatio)
            );
            MusicComment.Location = new Point(
                (int)(45 * this.widthRatio),
                (int)(45 * this.heightRatio)
            );

            MusicComment.MouseUp += ChildMouseUp;
            MusicComment.MouseMove += ChildMouseMove;
            MusicComment.MouseDown += ChildMouseDown;

            MusicComment.maxPt = 10.0f;
        }
        private void InitializeAlbumArtAndLyrics()
        {
            AlbumArtBox.Size = new Size(
                (int)(380 * this.widthRatio),
                (int)(380 * this.heightRatio)
            );
            AlbumArtBox.Location = new Point(
                (int)(35 * this.widthRatio),
                (int)(140 * this.heightRatio)
            );

            AlbumArtBox.Visible = true;
            AlbumArtBox.Parent = this;

            LyricsBox.Size = new Size(
                (int)(380 * this.widthRatio),
                (int)(380 * this.heightRatio)
            );
            LyricsBox.Location = new Point(
                (int)(35 * this.widthRatio),
                (int)(140 * this.heightRatio)
            );

            LyricsBox.Visible = false;

            SetDefaultBackgroundImage();
            SetDefaultAlbumArtImage();
        }
        private void InitializeMusicTrackBar()
        {
            musicTrackBar.Size = new Size(
                (int)(360 * this.widthRatio),
                (int)(30 * this.heightRatio)
            );
            musicTrackBar.Location = new Point(
                (int)(45 * this.widthRatio),
                (int)(560 * this.heightRatio)
            );

            ResetMusicTrackBar();
        }
        private void InitializeTitleTextBox()
        {
            TitleTextBox.Size = new Size(
                (int)(360 * this.widthRatio),
                (int)(35 * this.heightRatio)
            );
            TitleTextBox.Location = new Point(
                (int)(45 * this.widthRatio),
                (int)(610 * this.heightRatio)
            );

            TitleTextBox.MouseUp += ChildMouseUp;
            TitleTextBox.MouseMove += ChildMouseMove;
            TitleTextBox.MouseDown += ChildMouseDown;

            TitleTextBox.maxPt = 16.0f;
            TitleTextBox.Text = "TITLE";
        }
        private void InitializeListAndLyricsForm()
        {
            PlayListForm = new ControlForm
            {
                Location = new Point(
                    Location.X + this.Width,
                    this.Location.Y
                )
            };
            PlayListForm.ItemSelected += ItemSelectedInMusicPlayList;
            PlayListForm.ItemDeleted += ItemDeletedInMusicPlayList;
            PlayListForm.PlayButtonClicked += PlayButton_Click;

            PlayListForm.Show();

            Program.playList = new MusicList();
        }

        private void AcceptParameter()
        {
            if (Program.isParam)
            {
                string[] arr = Program.parameter.Split('\\');
                Program.playList.Add(new MusicInfo(arr[arr.Length - 1], Program.parameter));
                PlayListForm.UpdateList();
            }
        }

        // AlbumArt and Background Control
        private void DrawBackground(Bitmap origin)
        {
            Bitmap blurredImage = ImageController.ResizeBitmapFitSmaller(origin, this.Size);
            blurredImage = ImageController.CropBitmap(blurredImage, this.Size);
            blurredImage = ImageController.BoxBlur(blurredImage, 2);
            this.BackgroundImage = blurredImage;
        }
        private void DrawAlbumArt(Bitmap origin)
        {
            float aspect = (AlbumArtBox.Width - 80) / (float)AlbumArtBox.Width;

            Bitmap albumImage = ImageController.ResizeBitmapFitSmaller(origin, AlbumArtBox.Size);
            albumImage = ImageController.CropBitmap(albumImage, AlbumArtBox.Size);
            albumImage = ImageController.CropImageRoundEdge(albumImage);

            Bitmap pausedImage = ImageController.ResizeBitmap(albumImage, aspect);
            pausedImage = ImageController.Grayscale(pausedImage);

            this.AlbumArtBox.Image = albumImage;
            this.AlbumArtBox.StoppedImage = pausedImage;
        }

        private void DrawImages()
        {
            MusicInfo info = Program.playList.GetCurrentItem();
            if (info == null || info.AlbumImage == null)
            {
                SetDefaultAlbumArtImage();
                SetDefaultBackgroundImage();
                return;
            }

            DrawAlbumArt(info.AlbumImage);
            DrawBackground(info.AlbumImage);
        }
        private void SetLyrics()
        {
            MusicInfo info = Program.playList.GetCurrentItem();
            if(info == null)
            {
                this.LyricsBox.SetLyrics(null);
                return;
            }
            this.LyricsBox.SetLyrics(info.lyrics);
        }
        private void SetTitle()
        {
            MusicInfo info = Program.playList.GetCurrentItem();
            if(info == null)
            {
                TitleTextBox.Text = "TITLE";
                return;
            }
            TitleTextBox.Text = info.title;
        }
        private void SetComment()
        {
            MusicInfo info = Program.playList.GetCurrentItem();
            if(info == null)
            {
                MusicComment.Text = "";
                return;
            }
            MusicComment.Text = info.comment;
        }

        private void SetDefaultBackgroundImage()
        {
            if (DefaultBackgroundImage == null)
                DefaultBackgroundImage = new Bitmap(Properties.Resources.DefaultBackground1);

            this.BackgroundImage = DefaultBackgroundImage;
        }
        private void SetDefaultAlbumArtImage()
        {
            Size albumSize = AlbumArtBox.Size;

            if (DefaultAlbumImage == null)
                DefaultAlbumImage = new Bitmap(Properties.Resources.noAlbumImage);

            AlbumArtBox.Image = DefaultAlbumImage;
            AlbumArtBox.StoppedImage = ImageController.ResizeBitmap(DefaultAlbumImage, (albumSize.Width - 80) / (float)albumSize.Width);
        }

        // MusicTrackBar Control
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

        // Music Control
        private void MusicStop()
        {
            if (Program.musicPlayer.IsNull())
                return;

            AlbumArtBox.Stop();

            Program.musicPlayer.Stop();
            MusicPlayTimeCheckTimer.Stop();

            this.Invalidate();
        }
        private void MusicPause()
        {
            if (Program.musicPlayer.IsNull())
                return;

            AlbumArtBox.Stop();

            Program.musicPlayer.Pause();
            MusicPlayTimeCheckTimer.Stop();

            this.Invalidate();
        }
        private void MusicStart()
        {
            if (Program.musicPlayer.IsNull())
                return;

            AlbumArtBox.Start();

            Program.musicPlayer.Play();
            MusicPlayTimeCheckTimer.Start();

            this.Invalidate();
        }

        private void ChangeSelectedItem()
        {
            bool nowPlaying = Program.musicPlayer.IsPlay;
            MusicStop();

            MusicInfo musicItem = Program.playList.GetCurrentItem();

            if (musicItem == null)
            {
                MessageBox.Show("Music Not Selected");
                SetDefaultBackgroundImage();
                SetDefaultAlbumArtImage();
                return;
            }

            Program.musicPlayer.SetReader(musicItem);

            DrawImages();
            SetTitle();
            SetLyrics();
            SetComment();

            ResetMusicTrackBar();

            if(nowPlaying) MusicStart();
        }

        private void SetPrevMusic()
        {
            PlayListForm.MovePrev();
            ChangeSelectedItem();
        }
        private void SetNextMusic()
        {
            PlayListForm.MoveNext();
            ChangeSelectedItem();
        }
        private void SetSelectedMusic(int idx)
        {
            PlayListForm.MoveSelected(idx);
            ChangeSelectedItem();
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
        private void MusicPauseOrStart()
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

        // Event Handler
        private void FormCloseButtonClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CloseForm(object sender, FormClosingEventArgs e)
        {
            if (Program.musicPlayer != null)
                Program.musicPlayer.Stop();
            PlayListForm.Close();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            MusicPauseOrStart();
        }

        private void MusicEndCheck(object sender, EventArgs e)
        {
            if (Program.musicPlayer.IsEnd() && Program.playList.Count > 0)
            {
                SetNextMusic();
                MusicStart();
            }
        }
        private void MusicTrackBarScrolled(object sender, EventArgs e)
        {
            Program.musicPlayer.CurrentSecond = musicTrackBar.CurrentTickPosition;
            if(IsLyrics) this.LyricsBox.SyncedLyricsRefresh((int)Program.musicPlayer.CurrentSecond);
        }
        private void MusicPlayStateChecker(object sender, EventArgs e)
        {
            musicTrackBar.CurrentTickPosition = (int)Program.musicPlayer.CurrentSecond;
            if (IsLyrics) this.LyricsBox.SyncedLyricsRefresh((int)Program.musicPlayer.CurrentSecond);
        }

        private void ItemDeletedInMusicPlayList(object sender, EventArgs e)
        {
            ItemDeletedEventArgs es = (ItemDeletedEventArgs)e;
            if (es.IsDeleteAll)
            {
                MusicStop();

                DrawImages();
                SetTitle();
                SetLyrics();
                SetComment();
            }
            else if (es.IsSelectedMusicDeleted)
            {
                SetNextMusic();
            }
        }
        private void ItemSelectedInMusicPlayList(object sender, EventArgs e)
        {
            int idx = ((ItemSelectedEventArgs)e).idx;
            SetSelectedMusic(idx);
        }

        private void ToggleLyrics()
        {
            if(IsLyrics)
            {
                LyricsBox.Visible = false;
                AlbumArtBox.Visible = true;
                IsLyrics = false;
            }
            else
            {
                LyricsBox.Visible = true;
                AlbumArtBox.Visible = false;
                IsLyrics = true;
            }
            this.Invalidate();
        }

        // Overrided Event Handler
        protected override void OnMouseUp(MouseEventArgs e)
        {
            mouseClicked = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if(mouseClicked)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(
                    -mouseClickedLocation.X,
                    -mouseClickedLocation.Y
                );
                Location = mousePose;
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            mouseClicked = true;
            mouseClickedLocation = e.Location;
        }

        public void ChildMouseUp(object sender, MouseEventArgs e)
        {
            mouseClicked = false;
        }
        public void ChildMouseMove(object sender, MouseEventArgs e)
        {
            if (mouseClicked)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(
                    -mouseClickedLocation.X,
                    -mouseClickedLocation.Y
                );
                Location = mousePose;
            }
        }
        public void ChildMouseDown(object sender, MouseEventArgs e)
        {
            mouseClicked = true;
            
            Point loc = e.Location;
            loc.Offset(((Control)sender).Location);

            mouseClickedLocation = loc;
        }

        // Keyboard Handler
        protected override bool ProcessCmdKey(ref Message message, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Space:
                    MusicPauseOrStart();
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

                case Keys.R:
                    ToggleLyrics();
                    break;
            }
            return base.ProcessCmdKey(ref message, keyData);
        }

        private void ReceiveMessage(ref Message message)
        {
            Win32.CopyDataStruct st = (Win32.CopyDataStruct)Marshal.PtrToStructure(message.LParam, typeof(Win32.CopyDataStruct));
            string filePath = Marshal.PtrToStringUni(st.lpData);
            Program.playList.Add(new MusicInfo(filePath));
            PlayListForm.UpdateList();

            Win32.SetForegroundWindow(PlayListForm.Handle);
        }
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            switch (message.Msg)
            {
                case Win32.WM_COPYDATA:
                    ReceiveMessage(ref message);
                    break;
            }
        }

        private bool mouseClicked;
        private bool IsLyrics = false;

        private double widthRatio;
        private double heightRatio;

        private Point mouseClickedLocation;

        private Bitmap DefaultBackgroundImage;
        private Bitmap DefaultAlbumImage;

        //private Bitmap SelectedBackgroundImage;

        private ControlForm PlayListForm;
    }
}
