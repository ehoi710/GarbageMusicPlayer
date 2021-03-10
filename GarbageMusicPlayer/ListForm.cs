using GarbageMusicPlayerClassLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GarbageMusicPlayer
{
    public partial class ListForm : Form
    {
        public ListForm()
        {
            InitializeComponent();

            InitializeListForm();
            InitializePlayListView();
        }

        // Initializer
        private void InitializeListForm()
        {
            this.Size = new Size(400, 820);

            this.Text = "";
        }
        private void InitializePlayListView()
        {
            PlayListView.Location = new Point(15, 290);
            PlayListView.Size = new Size(360, 480);

            PlayListView.MouseDown += PlayListMouseDown;

            PlayListView.BeginUpdate();

            PlayListView.View = View.Details;

            PlayListView.GridLines = true;
            PlayListView.FullRowSelect = true;
            PlayListView.CheckBoxes = false;

            PlayListView.Columns.Add("Name", 360);

            PlayListView.EndUpdate();

            RefreshListView(Program.playList);
        }

        // List view Control
        public void RefreshListView(MusicList playList)
        {
            ListView listView = PlayListView;

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

        // Event Handler
        private void PlayListMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                ListViewItem tmp = PlayListView.GetItemAt(e.X, e.Y);
                ContextMenu PlayListContextMenu = new ContextMenu();

                MenuItem deleteItem = new MenuItem
                {
                    Text = "Delete Item"
                };

                deleteItem.Click += (senders, es) =>
                {
                    int delIdx = (int)tmp.Tag;
                    MusicInfo delInfo = Program.playList[delIdx];
                    delInfo.Dispose();
                    Program.playList.RemoveAt(delIdx);
                    RefreshListView(Program.playList);

                    GC.Collect();
                };

                MenuItem ClearAll = new MenuItem
                {
                    Text = "Clear All"
                };

                ClearAll.Click += (senders, es) =>
                {
                    foreach (MusicInfo mi in Program.playList)
                    {
                        mi.Dispose();
                    }
                    Program.playList.Clear();
                    RefreshListView(Program.playList);

                    GC.Collect();
                };

                PlayListContextMenu.MenuItems.Add(deleteItem);
                PlayListContextMenu.MenuItems.Add(ClearAll);

                PlayListContextMenu.Show(PlayListView, new Point(e.X, e.Y));
            }
        }
    }
}
