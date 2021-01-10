using GarbageMusicPlayerClassLibrary;
using System;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MetroFramework;

namespace GarbageMusicPlayer
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        bool isFormExtend = true;

        public MainForm()
        {
            InitializeComponent();
            InitializeMainData();
        }

        private void InitializeMainData()
        {
            Program.musicPlayer = MusicPlayer.GetInstance(MusicEndCheck);

            Program.musicTree = new MusicTree(@"SOIM", Program.rootPath); Program.musicTree.Refresh();
            InitializeTreeView(MusicListTreeView, Program.musicTree);

            Program.playList = new MusicCurrentList();
            InitializeListView(PlayListView, Program.playList);

            InitializeVolumeBar(VolumeTrackBar, Program.musicPlayer);

            if (Program.parameter != null)
            {
                string[] arr = Program.parameter.Split('\\');
                Program.playList.Insert(new MusicItem(arr[arr.Length - 1], Program.parameter));
                RefreshListView(PlayListView, Program.playList);
            }

            KeyPreview = true;

            MusicPlayTimeCheckTimer.Stop();
            musicTrackBar.Parent = Background;
            TitleTextBox.Parent = Background;
            TitleTextBox.Text = "TITLE";
            AlbumArtBox.Parent = Background;
            ResetAlbumArt();
        }

        private void MusicListTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name.Equals("dir"))
            {
                MusicTree list = Program.musicTree.GetSubListByPath((String)e.Node.Tag);
                if ((e.Node.Nodes.Count != 0) && (list.isLoaded))
                {
                    if (e.Node.IsExpanded) e.Node.Collapse();
                    else e.Node.Expand();
                }
                else
                {
                    list.Refresh();
                    RefreshTreeView(MusicListTreeView, e.Node, list);
                    e.Node.Expand();
                }
            }
            else
            {
                MusicItem tmp = new MusicItem((MusicItem)e.Node.Tag);
                Program.playList.Insert(tmp);
                RefreshListView(PlayListView, Program.playList);
            }
            MusicListTreeView.SelectedNode = null;
        }

        public void AddTreeViewToMusicList(TreeNode treeNode, MusicTree musicTree)
        {
            foreach (MusicTree list in musicTree.GetSubList())
            {
                TreeNode tmp = new TreeNode
                {
                    Name = "dir",
                    Text = list.dirName,
                    Tag = list.path
                };

                treeNode.Nodes.Add(tmp);
                AddTreeViewToMusicList(treeNode.Nodes[treeNode.Nodes.Count - 1], list);
            }
            foreach (MusicItem item in musicTree.GetMusicList())
            {
                TreeNode tmp = new TreeNode
                {
                    Name = "file",
                    Text = item.name,
                    Tag = item
                };
                treeNode.Nodes.Add(tmp);
            }
        }

        public void RefreshTreeView(TreeView treeView, TreeNode treeNode, MusicTree musicTree)
        {
            if (musicTree == null)
                return;

            treeView.BeginUpdate();

            treeNode.Nodes.Clear();
            AddTreeViewToMusicList(treeNode, musicTree);

            treeView.EndUpdate();
        }

        public void InitializeTreeView(TreeView treeView, MusicTree musicTree)
        {
            treeView.BeginUpdate();

            if(musicTree != null)
            {
                TreeNode tmp = new TreeNode
                {
                    Name = "dir",
                    Text = musicTree.dirName,
                    Tag = musicTree.path
                };

                treeView.Nodes.Add(tmp);
            }
            treeView.EndUpdate();

            RefreshTreeView(treeView, treeView.Nodes[0], musicTree);
        }

        public void RefreshListView(ListView listView, MusicCurrentList playList)
        {
            listView.BeginUpdate();
            listView.Items.Clear();
            int idx = 0;
            foreach (MusicItem item in playList)
            {
                ListViewItem tmp = new ListViewItem
                {
                    Text = item.name,
                    Tag = idx
                };

                listView.Items.Add(tmp);
                idx++;
            }
            listView.EndUpdate();
        }

        public void InitializeListView(ListView listView, MusicCurrentList playList)
        {
            listView.BeginUpdate();

            listView.View = View.Details;
            
            listView.GridLines = true;
            listView.FullRowSelect = true;
            listView.CheckBoxes = false;

            listView.Columns.Add("Name", 360);

            listView.EndUpdate();

            RefreshListView(listView, playList);
        }

        public void InitializeVolumeBar(TrackBar trackBar, MusicPlayer musicPlayer)
        {
            trackBar.Minimum = 0;
            trackBar.Maximum = 100;
            trackBar.Value = 50;
            musicPlayer.SetVolume(trackBar.Value / 100.0f);
        }

        public Bitmap ResizeBitmap(Bitmap bitmap, int width, int height, bool invers = false)
        {
            Size size;
            if(!invers && ((bitmap.Width * height > bitmap.Height * width)) || (invers && (bitmap.Width * height < bitmap.Height * width)))
            {
                size = new Size(width, (int)(bitmap.Height * ((float)width / bitmap.Width)));
            }
            else if(!invers && ((bitmap.Width * height < bitmap.Height * width)) || (invers && (bitmap.Width * height > bitmap.Height * width)))
            {
                size = new Size((int)(bitmap.Width * ((float)height / bitmap.Height)), height);
            }
            else
            {
                size = new Size(width, height);
            }
            return new Bitmap(bitmap, size);
        }

        public Bitmap CropBitmap(Bitmap bitmap, int width, int height)
        {
            Rectangle cropArea = new Rectangle((bitmap.Width - width) / 2, (bitmap.Height - height) / 2, width, height);
            Bitmap CroppedBitmap = new Bitmap(width, height);

            using(Graphics g = Graphics.FromImage(CroppedBitmap))
            {
                g.DrawImage(bitmap, -cropArea.X, -cropArea.Y);
                return CroppedBitmap;
            }
        }
        private int Max(int a, int b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        private int Min(int a, int b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        private Bitmap BoxBlurH(Bitmap image)
        {
            Bitmap blurred = new Bitmap(image);
            int r = 2;
            int w = (r + r + 1);

            for (int y = 0; y < blurred.Height; y++)
            {
                int R = 0;
                int G = 0;
                int B = 0;

                for (int xx = -r; xx <= r; xx++)
                {
                    int ix = Min(blurred.Width - 1, Max(0, xx));
                    Color pix = image.GetPixel(ix, y);

                    R += pix.R;
                    G += pix.G;
                    B += pix.B;
                }

                for (int x = 1; x < blurred.Width; x++)
                {
                    int minx = Min(blurred.Width - 1, Max(0, x - r - 1));
                    int maxx = Min(blurred.Width - 1, Max(0, x + r));

                    Color d = image.GetPixel(minx, y);
                    Color a = image.GetPixel(maxx, y);

                    R = R - d.R + a.R;
                    G = G - d.G + a.G;
                    B = B - d.B + a.B;

                    blurred.SetPixel(x, y, Color.FromArgb(R / w, G / w, B / w));
                }
            }

            return blurred;
        }

        private Bitmap BoxBlurT(Bitmap image)
        {
            Bitmap blurred = new Bitmap(image);
            int r = 2;
            int w = (r + r + 1);

            for (int x = 0; x < blurred.Width; x++)
            {
                int R = 0;
                int G = 0;
                int B = 0;

                for (int yy = -r; yy <= r; yy++)
                {
                    int iy = Min(blurred.Width - 1, Max(0, yy));
                    Color pix = image.GetPixel(x, iy);

                    R += pix.R;
                    G += pix.G;
                    B += pix.B;
                }

                for (int y = 1; y < blurred.Height; y++)
                {
                    int miny = Min(blurred.Height - 1, Max(0, y - r - 1));
                    int maxy = Min(blurred.Height - 1, Max(0, y + r));

                    Color d = image.GetPixel(x, miny);
                    Color a = image.GetPixel(x, maxy);

                    R = R - d.R + a.R;
                    G = G - d.G + a.G;
                    B = B - d.B + a.B;

                    blurred.SetPixel(x, y, Color.FromArgb(R / w, G / w, B / w));
                }
            }

            return blurred;
        }

        public Bitmap Blur(Bitmap image)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            Bitmap blurred = BoxBlurT(BoxBlurH(image));
            //sw.Stop();
            //MessageBox.Show(sw.ElapsedMilliseconds.ToString() + "ms");
            return blurred;
        }

        public void DrawAlbumArt(TagLib.IPicture pictures)
        {
            MemoryStream ms = new MemoryStream(pictures.Data.Data);
            Bitmap bitmap = new Bitmap(Image.FromStream(ms));
            AlbumArtBox.Image = ResizeBitmap(bitmap, AlbumArtBox.Width, AlbumArtBox.Height);
            Background.Image = Blur(CropBitmap(ResizeBitmap(bitmap, Background.Width, Background.Height, true), Background.Width, Background.Height));
        }

        public void ChangeSelectedItem(MusicItem musicItem)
        {
            musicTrackBar.Value = 0;
            MusicPlayTimeCheckTimer.Stop();
            PlayButton.Text = "Play";

            Program.musicPlayer.SetReader(musicItem);

            if (musicItem == null)
            {
                TitleTextBox.Text = "Music not Selected";
                ResetAlbumArt();
                return;
            }

            if (musicItem.file.Tag.Title != null)
                TitleTextBox.Text = musicItem.file.Tag.Title;
            else
                TitleTextBox.Text = (musicItem).name;

            if (musicItem.file.Tag.Pictures.Length != 0)
                DrawAlbumArt(musicItem.file.Tag.Pictures[0]);
            else
                ResetAlbumArt();
        }

        public void ResetAlbumArt()
        {
            Background.Image = new Bitmap(Properties.Resources.DefaultBackground1);
            AlbumArtBox.Image = new Bitmap(Properties.Resources.noAlbumImage);
        }

        private void MusicPauseStart()
        {
            if (Program.musicPlayer.IsNull())
            {
                if (Program.playList.Current == null)
                {
                    MessageBox.Show("Music Not Selected");
                    return;
                }
                else
                {
                    ChangeSelectedItem(Program.playList.Current);
                }
            }

            if(Program.musicPlayer.IsPlay())
            {
                Program.musicPlayer.Pause();
                PlayButton.Text = "Play";
                MusicPlayTimeCheckTimer.Stop();
            }
            else
            {
                Program.musicPlayer.Play();
                PlayButton.Text = "Pause";
                MusicPlayTimeCheckTimer.Start();
            }
        }

        private void GetNextMusic()
        {
            Program.musicPlayer.Stop();
            ChangeSelectedItem(Program.playList.GetNext());
        }

        public void MusicEndCheck(object sender, EventArgs e)
        {
            if (Program.musicPlayer.IsEnd())
            {
                GetNextMusic();
                Program.musicPlayer.Play();
                PlayButton.Text = "Stop";
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            MusicPauseStart();
            PlayButton.Enabled = false;
            PlayButton.Enabled = true;
        }

        private void FomeClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.musicPlayer != null)
                Program.musicPlayer.Stop();
        }

        private void PlayListMouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button.Equals(MouseButtons.Right))
            {
                ListViewItem tmp = PlayListView.GetItemAt(e.X, e.Y);
                ContextMenu PlayListContextMenu = new ContextMenu();

                MenuItem deleteItem = new MenuItem
                {
                    Text = "Delete Item"
                };

                deleteItem.Click += (senders, es) =>
                {
                    Program.playList.Delete((int)tmp.Tag);
                    RefreshListView(PlayListView, Program.playList);
                };

                MenuItem ClearAll = new MenuItem
                {
                    Text = "Clear All"
                };

                ClearAll.Click += (senders, es) =>
                {
                    Program.playList.Clear();
                    RefreshListView(PlayListView, Program.playList);
                };

                PlayListContextMenu.MenuItems.Add(deleteItem);
                PlayListContextMenu.MenuItems.Add(ClearAll);

                PlayListContextMenu.Show(PlayListView, new Point(e.X, e.Y));
            }
        }

        private void VolumeBarScrolled(object sender, EventArgs e)
        {
            Program.musicPlayer.SetVolume(VolumeTrackBar.Value / 100.0f);
        }

        private void KeyboardDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Space:
                    MusicPauseStart();
                    break;

                case Keys.A:
                    Program.musicPlayer.Stop();
                    ChangeSelectedItem(Program.playList.GetPrev());
                    break;

                case Keys.D:
                    GetNextMusic();
                    break;

                case Keys.E:
                    if (isFormExtend)
                        this.Width = Background.Width;
                    else
                        this.Width = 1076;
                    isFormExtend = !isFormExtend;
                    break;
            }
        }

        private void MusicTrackBarScrolled(object sender, EventArgs e)
        {
            Program.musicPlayer.SetCurrentSecond(musicTrackBar.Value * Program.musicPlayer.GetTotalSecond() / 100);
        }

        private void MusicPlayStateChecker(object sender, EventArgs e)
        {
            musicTrackBar.Value = (int)(Program.musicPlayer.GetCurrentSecond() * 100 / Program.musicPlayer.GetTotalSecond());
        }
    }
}
