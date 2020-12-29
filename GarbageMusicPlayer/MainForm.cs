using GarbageMusicPlayerClassLibrary;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MetroFramework;

namespace GarbageMusicPlayer
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeMainData();
        }

        private void InitializeMainData()
        {
            Program.musicTree = new MusicTree(@"SOIM", Program.rootPath); Program.musicTree.Refresh();
            Program.musicPlayer = MusicPlayer.GetInstance(MusicEndCheck);
            Program.playList = new MusicCurrentList();

            InitializeTreeView(MusicListTreeView, Program.musicTree);
            InitializeListView(PlayListView, Program.playList);
            InitializeVolumeBar(VolumeTrackBar, Program.musicPlayer);

            KeyPreview = true;

            MusicPlayTimeCheckTimer.Stop();
            musicTrackBar.Parent = Background;
            TitleTextBox.Parent = Background;
            TitleTextBox.Text = "TITLE";
            AlbumArtBox.Parent = Background;
            ResetAlbumArt();

            if (Program.parameter != null)
            {
                string[] arr = Program.parameter.Split('\\');
                Program.playList.Insert(new MusicItem(arr[arr.Length - 1], Program.parameter));
                RefreshListView(PlayListView, Program.playList);
            }
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
                Program.playList.Insert((MusicItem)e.Node.Tag);
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
            treeView.BeginUpdate();

            treeNode.Nodes.Clear();
            AddTreeViewToMusicList(treeNode, musicTree);

            treeView.EndUpdate();
        }

        public void InitializeTreeView(TreeView treeView, MusicTree musicTree)
        {
            treeView.BeginUpdate();

            TreeNode tmp = new TreeNode
            {
                Name = "dir",
                Text = musicTree.dirName,
                Tag = musicTree.path
            };

            treeView.Nodes.Add(tmp);
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

        private Bitmap Blur(Bitmap image)
        {
            Bitmap blurred = new Bitmap(image);
            for(int y = 1; y < image.Height - 2; y++)
            {
                int avgR = 0;
                int avgG = 0;
                int avgB = 0;
                
                for (int yy = -1; yy < 2; yy++)
                {
                    for (int xx = -1; xx < 2; xx++)
                    {
                        Color c = image.GetPixel(1 + xx, y + yy);
                        avgR += c.R;
                        avgG += c.G;
                        avgB += c.B;
                    }
                }

                for (int x = 1; x < image.Width - 2; x++)
                {
                    if(x != 1)
                    {
                        for(int yy = -1; yy < 2; yy++)
                        {
                            Color c = image.GetPixel(x - 2, y + yy);
                            avgR -= c.R;
                            avgG -= c.G;
                            avgB -= c.B;

                            Color a = image.GetPixel(x + 1, y + yy);
                            avgR += a.R;
                            avgG += a.G;
                            avgB += a.B;
                        }
                    }
                    blurred.SetPixel(x, y, Color.FromArgb(avgR / 9, avgG / 9, avgB / 9));
                }
            }
            return blurred;
        }

        public Bitmap ResizeBitmap(Bitmap bitmap, int width, int height, bool invers = false)
        {
            Size size;
            if(!invers && ((bitmap.Width * height > bitmap.Height * width)) || (invers && (bitmap.Width * height < bitmap.Height * width)))
            {
                size = new Size(width, (int)(height * ((float)bitmap.Height / bitmap.Width)));
            }
            else if(!invers && ((bitmap.Width * height < bitmap.Height * width)) || (invers && (bitmap.Width * height > bitmap.Height * width)))
            {
                size = new Size((int)(width * ((float)bitmap.Width / bitmap.Height)), height);
            }
            else
            {
                size = new Size(width, height);
            }
            return new Bitmap(bitmap, size);
        }

        public Bitmap CropBitmap(Bitmap bitmap, int width, int height)
        {
            Bitmap CroppedBitmap = new Bitmap(bitmap);
            CroppedBitmap = CroppedBitmap.Clone(
                new Rectangle((bitmap.Width - width) / 2, (bitmap.Height - height), width, height),
                System.Drawing.Imaging.PixelFormat.DontCare
            );
            return CroppedBitmap;
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
                ChangeSelectedItem(Program.playList.Current);
            }
            if (Program.musicPlayer.IsNull())
            {
                MessageBox.Show("Music Not Selected");
                return;
            }
            PlayButton.Text = Program.musicPlayer.PlayToggle();
            if (MusicPlayTimeCheckTimer.Enabled)
                MusicPlayTimeCheckTimer.Stop();
            else
                MusicPlayTimeCheckTimer.Start();
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
