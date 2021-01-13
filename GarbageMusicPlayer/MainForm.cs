using GarbageMusicPlayerClassLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GarbageMusicPlayer
{
    public partial class MainForm : Form
    {
        public struct ComponentDefaultSize
        {
            public Control control;
            public Size size;
            public Point location;

            public ComponentDefaultSize(Control _control, Size _size, Point _location)
            {
                control = _control;
                size = new Size(_size.Width, _size.Height);
                location = new Point(_location.X, _location.Y);
            }
        }

        bool loadCompleted = false;

        Bitmap DefaultBackgroundImage;
        Bitmap DefaultAlbumImage;

        Size MainFormDefaultSize;
        List<ComponentDefaultSize> ComponentList;
        
        public MainForm()
        {
            InitializeComponent();

            InitializeComponentSize();
            InitializeMusicPlayer();

            Program.musicTree = new MusicTree(@"SOIM", Program.rootPath);
            Program.musicTree.Refresh();
            InitializeTreeView(MusicListTreeView, Program.musicTree);

            Program.playList = new MusicList();
            InitializeListView(PlayListView, Program.playList);

            InitializeVolumeBar(VolumeTrackBar, Program.musicPlayer);

            if (Program.parameter != null)
            {
                string[] arr = Program.parameter.Split('\\');
                Program.playList.Add(new MusicInfo(arr[arr.Length - 1], Program.parameter));
                RefreshListView(PlayListView, Program.playList);
            }

            KeyPreview = true;

            MusicPlayTimeCheckTimer.Stop();
            musicTrackBar.Parent = Background;
            TitleTextBox.Parent = Background;
            TitleTextBox.Text = "TITLE";
            AlbumArtBox.Parent = Background;
            MusicComment.Parent = Background;

            ResetAlbumArt();

            loadCompleted = true;
        }

        private void InitializeComponentSize()
        {
            this.Size = new Size(1200, 820);

            Background.Location = new Point(0, 10);
            Background.Size = new Size(450, 810);

            AlbumArtBox.Location = new Point(45, 175);
            AlbumArtBox.Size = new Size(360, 360);

            musicTrackBar.Location = new Point(45, 545);
            musicTrackBar.Size = new Size(360, 30);

            TitleTextBox.Location = new Point(45, 600);
            TitleTextBox.Size = new Size(360, 35);

            PlayButton.Location = new Point(170, 675);
            PlayButton.Size = new Size(120, 50);

            VolumeTrackBar.Location = new Point(465, 30);
            VolumeTrackBar.Size = new Size(180, 55);

            MusicListTreeView.Location = new Point(465, 300);
            MusicListTreeView.Size = new Size(360, 480);

            PlayListView.Location = new Point(830, 300);
            PlayListView.Size = new Size(360, 480);

            MusicComment.Location = new Point(45, 75);
            MusicComment.Size = new Size(360, 100);

            MainFormDefaultSize = new Size(this.Width, this.Height);
            ComponentList = new List<ComponentDefaultSize>();
            foreach (Control c in this.Controls)
            {
                ComponentList.Add(new ComponentDefaultSize(c, c.Size, c.Location));
            }
        }

        private void InitializeMusicPlayer()
        {
            Program.musicPlayer = MusicPlayer.GetInstance(MusicEndCheck);
        }

        public void InitializeTreeView(TreeView treeView, MusicTree musicTree)
        {
            treeView.BeginUpdate();

            if (musicTree != null)
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

        public void RefreshTreeView(TreeView treeView, TreeNode treeNode, MusicTree musicTree)
        {
            if (musicTree == null)
                return;

            treeView.BeginUpdate();

            treeNode.Nodes.Clear();
            AddTreeViewToMusicList(treeNode, musicTree);

            treeView.EndUpdate();
        }

        public void InitializeListView(ListView listView, MusicList playList)
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

        public void RefreshListView(ListView listView, MusicList playList)
        {
            listView.BeginUpdate();
            listView.Items.Clear();
            int idx = 0;
            foreach (MusicInfo item in playList)
            {
                ListViewItem tmp = new ListViewItem
                {
                    Text = item.title,
                    Tag = idx
                };

                listView.Items.Add(tmp);
                idx++;
            }
            listView.EndUpdate();
        }

        public void InitializeVolumeBar(TrackBar trackBar, MusicPlayer musicPlayer)
        {
            trackBar.Minimum = 0;
            trackBar.Maximum = 100;
            trackBar.Value = 50;
            musicPlayer.SetVolume(trackBar.Value / 100.0f);
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
                MusicInfo tmp = new MusicInfo((MusicInfo)e.Node.Tag);
                Program.playList.Add(tmp);
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
            foreach (MusicInfo item in musicTree.GetMusicList())
            {
                TreeNode tmp = new TreeNode
                {
                    Name = "file",
                    Text = item.title,
                    Tag = item
                };
                treeNode.Nodes.Add(tmp);
            }
        }
        
        public void DrawAlbumArt(MusicInfo info)
        {
            Background.SizeMode = PictureBoxSizeMode.CenterImage;

            Bitmap pictures = info.AlbumImage;
            AlbumArtBox.Image = ImageController.ResizeBitmap(pictures, AlbumArtBox.Width, AlbumArtBox.Height, ImageController.ImageResizeMode.Bigger);
            if(info.BlurredImage == null)
            {
                Bitmap bitmap = ImageController.ResizeBitmap(pictures, Background.Width, Background.Height, ImageController.ImageResizeMode.Smaller);
                bitmap = ImageController.CropBitmap(bitmap, Background.Width, Background.Height);
                info.BlurredImage = ImageController.BoxBlur(bitmap, 2);
            }
            
            Background.Image = info.BlurredImage;
        }

        public void ChangeSelectedItem(MusicInfo musicItem)
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

            TitleTextBox.Text = musicItem.title;

            MusicComment.Text = musicItem.comment;

            if (musicItem.AlbumImage != null)
                DrawAlbumArt(musicItem);
            else
                ResetAlbumArt();
        }

        public void ResetAlbumArt()
        {
            Background.SizeMode = PictureBoxSizeMode.StretchImage;

            if (DefaultBackgroundImage == null)
                DefaultBackgroundImage = new Bitmap(Properties.Resources.DefaultBackground1);
            if (DefaultAlbumImage == null)
                DefaultAlbumImage = new Bitmap(Properties.Resources.noAlbumImage);

            Background.Image = DefaultBackgroundImage;
            AlbumArtBox.Image = DefaultAlbumImage;
        }

        private void MusicPauseStart()
        {
            if (Program.musicPlayer.IsNull())
            {
                Program.playList.GetNext();
                if (Program.playList.Current == -1)
                {
                    MessageBox.Show("Music Not Selected");
                    return;
                }
                else
                {
                    ChangeSelectedItem(Program.playList.GetCurrent());
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
                    Program.playList.RemoveAt((int)tmp.Tag);
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

        private void ResizeComponant(Control componant, Size size, Point location)
        {
            componant.Size = size;
            componant.Location = location;
        }

        private int WidthScale(int width)
        {
            return (int)(width * (float)this.Width / MainFormDefaultSize.Width);
        }

        private int HeightScale(int Height)
        {
            return (int)(Height * (float)this.Height / MainFormDefaultSize.Height);
        }

        private void MainFormSizeChanged(object sender, EventArgs e)
        {
            if (loadCompleted == false)
                return;

            foreach(ComponentDefaultSize c in ComponentList)
            {
                if(c.control == Background)
                {
                    ResizeComponant(
                        c.control,
                        new Size(WidthScale(c.size.Width), this.Height - 10),
                        c.location
                    );
                }
                else
                {
                    ResizeComponant(
                        c.control,
                        new Size(WidthScale(c.size.Width), HeightScale(c.size.Height)),
                        new Point(WidthScale(c.location.X), HeightScale(c.location.Y))
                    );
                }
            }

            if (Program.playList != null && Program.playList.GetCurrent() != null)
                DrawAlbumArt(Program.playList.GetCurrent());
        }
    }
}
