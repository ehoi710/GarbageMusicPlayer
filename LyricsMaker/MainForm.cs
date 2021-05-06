using GarbageMusicPlayerClassLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LyricsMaker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            InitializeMusicPlayer();

            InitializeMainForm();

            InitializeAlbumImage();
            InitializeButtons();
            InitializeLyricsBox();
            InitializeRangeBoxes();
            InitializeTextBox();
            InitializeColorSelecter();
        }

        private void InitializeMusicPlayer()
        {
            this.player = new MusicPlayer();
        }

        private void InitializeMainForm()
        {
            this.ClientSize = new Size(900, 450);
        }

        private void InitializeAlbumImage()
        {
            this.AlbumImageBox.Location = new Point(10, 10);
            this.AlbumImageBox.Size = new Size(300, 300);

            ResetAlbumImageBox();
        }
        private void InitializeButtons()
        {
            this.MusicToggleButton.Location = new Point(10, 320);
            this.MusicToggleButton.Size = new Size(300, 50);
            this.MusicToggleButton.Text = "Play";

            this.MusicLoadButton.Location = new Point(10, 380);
            this.MusicLoadButton.Size = new Size(145, 60);
            this.MusicLoadButton.Text = "Load";

            this.LyricsSaveButton.Location = new Point(165, 380);
            this.LyricsSaveButton.Size = new Size(145, 60);
            this.LyricsSaveButton.Text = "Save";
        }
        private void InitializeLyricsBox()
        {
            this.LyricsBox.Location = new Point(590, 10);
            this.LyricsBox.Size = new Size(300, 430);

            this.LyricsBox.BeginUpdate();

            this.LyricsBox.View = View.Details;
            this.LyricsBox.GridLines = true;
            this.LyricsBox.FullRowSelect = true;

            this.LyricsBox.Columns.Add("Time", 60);
            this.LyricsBox.Columns.Add("Text", this.LyricsBox.Width - 85);

            this.LyricsBox.EndUpdate();
        }
        private void InitializeRangeBoxes()
        {
            this.LeftRange.Location = new Point(320, 40);
            this.LeftRange.Size = new Size(100, 20);

            this.LeftRange.ValueChangeEvent += DateValueChanged;
        }
        private void InitializeTextBox()
        {
            this.LineEdit.Location = new Point(320, 70);
            this.LineEdit.Size = new Size(260, 25);
        }
        private void InitializeColorSelecter()
        {
            this.unselectedColor.Location = new Point(320, 200);
            this.unselectedColor.Size = new Size(120, 160);
            this.unselectedColor.ColorChanged += (sender, es) =>
            {
                UpdateLyrics();
            };

            this.selectedColor.Location = new Point(460, 200);
            this.selectedColor.Size = new Size(120, 160);
            this.selectedColor.ColorChanged += (sender, es) =>
            {
                UpdateLyrics();
            };
        }

        private void ResetAlbumImageBox()
        {
            this.AlbumImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            if (DefaultAlbumImage == null)
                DefaultAlbumImage = new Bitmap(Properties.Resources.noAlbumImage);

            SetAlbumImage(DefaultAlbumImage);
        }

        private void UpdateLyrics()
        {
            LyricsContainer.Data backup = this.Lyrics.data[selectedIdx];
            try
            {
                this.Lyrics.data[selectedIdx] = LyricsContainer.DataFromString(
                    "[" + this.LeftRange.value.ToString() + "] " + 
                    this.unselectedColor.SelectedColor.ToArgb().ToString("X") + " " + 
                    this.selectedColor.SelectedColor.ToArgb().ToString("X") + " " + 
                    LineEdit.Text);
            }
            catch
            {
                this.Lyrics.data[selectedIdx] = backup;
            }
            UpdateLyricsBox();
        }
        private void UpdateLyricsBox()
        {
            this.LyricsBox.BeginUpdate();

            this.LyricsBox.Items.Clear();
            int idx = 0;
            if (this.Lyrics == null) return;
            foreach (LyricsContainer.Data line in this.Lyrics.data)
            {
                ListViewItem tmp = new ListViewItem(new string[] { line.time.ToString(), line.str })
                {
                    Tag = idx
                };
                this.LyricsBox.Items.Add(tmp);
                idx++;
            }

            this.LyricsBox.EndUpdate();
        }

        private void LoadLine()
        {
            LyricsContainer.Data line = this.Lyrics.data[selectedIdx];

            this.LineEdit.Enabled = false;

            this.LeftRange.SetValue(line.time);
            this.unselectedColor.SetColor(line.unselectedColor);
            this.selectedColor.SetColor(line.selectedColor);

            string str = line.str;
            this.LineEdit.Text = str;
            this.LineEdit.Enabled = true;

            player.CurrentSecond = this.LeftRange.value.TotalSeconds;
        }

        private void SetMusic(MusicInfo music)
        {
            this.player.SetReader(music);
            this.Text = music.title;
        }
        private void SetAlbumImage(Bitmap albumImage)
        {
            if (albumImage == null) ResetAlbumImageBox();
            else this.AlbumImageBox.Image = ImageController.ResizeBitmapFitSmaller(albumImage, this.AlbumImageBox.Size);
        }
        private void SetLyrics(string lyrics)
        {
            this.Lyrics = new LyricsContainer(lyrics)
            {
                isSync = true
            };
            UpdateLyricsBox();
        }

        private void FileOpen(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "MP3 Files(*.mp3)|*mp3"
            };
            openFileDialog.ShowDialog();

            if(openFileDialog.FileName.Length > 0)
            {
                this.FilePath = openFileDialog.FileName;
                this.musicInfo = new MusicInfo(FilePath);

                SetAlbumImage(this.musicInfo.AlbumImage);
                SetLyrics(this.musicInfo.lyrics);
                SetMusic(this.musicInfo);
            }
        }
        private void Save(object sender, EventArgs e)
        {
            if (this.Lyrics == null) MessageBox.Show("Lyrics is not loaded!");

            player.Dispose();

            TagLib.File file = TagLib.File.Create(musicInfo.path);
            file.Tag.Lyrics = this.Lyrics.ToString();
            file.Save();

            InitializeMusicPlayer();
            player.SetReader(musicInfo);
        }

        private void MusicToggle(object sender, EventArgs e)
        {
            if(player == null || player.IsNull())
            {
                return;
            }

            if(player.IsPlay)
            {
                this.MusicToggleButton.Text = "Play";
                player.Pause();
                player.CurrentSecond = this.LeftRange.value.TotalSeconds;
            }
            else
            {
                this.MusicToggleButton.Text = "Stop";
                player.Play();
            }
        }

        private void DateValueChanged(object sender, EventArgs e)
        {
            if(this.selectedIdx != -1) UpdateLyrics();
            player.CurrentSecond = this.LeftRange.value.TotalSeconds;
        }
        private void LyricsChanged(object sender, EventArgs e)
        {
            UpdateLyrics();
        }

        private void ItemSelected(object sender, MouseEventArgs e)
        {
            if(e.Button.Equals(MouseButtons.Left))
            {
                if (this.LyricsBox.SelectedIndices.Count <= 0) return;

                this.selectedIdx = this.LyricsBox.SelectedIndices[0];
                LoadLine();
            }
            else
            {
                ContextMenu LyricsBoxContextMenu = new ContextMenu();

                MenuItem addItem = new MenuItem("Add new Item");
                addItem.Click += (senders, es) =>
                {
                    ListViewItem selectedItem = LyricsBox.GetItemAt(e.X, e.Y);
                    int addIdx = (int)selectedItem.Tag;
                    LyricsContainer.Data data = new LyricsContainer.Data
                    {
                        time = TimeSpan.FromSeconds(0),
                        selectedColor = Color.White,
                        unselectedColor = Color.Gray,
                        str = ""
                    };

                    Lyrics.data.Insert(addIdx + 1, data);
                    UpdateLyricsBox();

                    this.LyricsBox.Enabled = false;
                    this.LyricsBox.Enabled = true;
                };

                MenuItem deleteItem = new MenuItem("Delete Item");
                deleteItem.Click += (senders, es) =>
                {
                    ListViewItem selectedItem = LyricsBox.GetItemAt(e.X, e.Y);
                    int delIdx = (int)selectedItem.Tag;
                    Lyrics.data.RemoveAt(delIdx);
                    UpdateLyricsBox();

                    GC.Collect();

                    this.LyricsBox.Enabled = false;
                    this.LyricsBox.Enabled = true;

                    this.selectedIdx = -1;
                };

                LyricsBoxContextMenu.MenuItems.Add(addItem);
                LyricsBoxContextMenu.MenuItems.Add(deleteItem);

                LyricsBoxContextMenu.Show(LyricsBox, e.Location);
            }
        }

        private string FilePath = null;
        private MusicPlayer player = null;

        private Bitmap DefaultAlbumImage = null;
        private MusicInfo musicInfo = null;
        private LyricsContainer Lyrics;

        private int selectedIdx = -1;
    }
}
