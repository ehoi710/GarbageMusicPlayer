using GarbageMusicPlayerClassLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GarbageMusicPlayer
{
    public partial class ControlForm : Form
    {
        // Event
        public event EventHandler ItemSelected;
        public event EventHandler ItemDeleted;
        public event EventHandler PlayButtonClicked;

        public ControlForm()
        {
            InitializeComponent();

            InitializeListForm();
            InitializePlayListView();
            InitializePlayButton();
        }

        // Initializer
        private void InitializeListForm()
        {
            int unit = 8;
            this.Size = new Size(unit * 40, unit * 90);

            this.widthRatio = (double)this.Width / 360;
            this.heightRatio = (double)this.Height / 810;

            this.Text = "";
        }
        private void InitializePlayListView()
        {
            PlayListView.Location = new Point(
                (int)(10 * widthRatio),
                (int)(290 * heightRatio)
            );
            PlayListView.Size = new Size(
                (int)(340 * widthRatio), 
                (int)(480 * heightRatio)
            );

            PlayListView.MouseDown += PlayListMouseDown;

            PlayListView.BeginUpdate();

            PlayListView.View = View.Details;

            PlayListView.GridLines = true;
            PlayListView.FullRowSelect = true;
            PlayListView.CheckBoxes = false;

            PlayListView.Columns.Add("Name", PlayListView.Width - 20);

            PlayListView.EndUpdate();

            UpdateList();
        }
        private void InitializePlayButton()
        {
            PlayButton.Size = new Size(
                (int)(120 * this.widthRatio),
                (int)(50 * this.widthRatio)
            );

            PlayButton.Location = new Point(
                (this.Width - PlayButton.Width) / 2,
                (int)(150 * this.heightRatio)
            );
        }

        // List view Control
        public void UpdateList()
        {
            PlayListView.BeginUpdate();

            PlayListView.Items.Clear();
            int idx = 0;
            foreach (MusicInfo item in Program.playList)
            {
                ListViewItem tmp = new ListViewItem
                {
                    Text = item.title,
                    Tag = idx
                };

                PlayListView.Items.Add(tmp);
                idx++;
            }

            PlayListView.EndUpdate();
        }

        public void MovePrev()
        {
            Program.playList.MovePrev();
        }
        public void MoveNext()
        {
            Program.playList.MoveNext();
        }
        public void MoveSelected(int idx)
        {
            Program.playList.SetCurrent(idx);
        }
        public void MoveRandom()
        {
            Program.playList.MoveRandom();
        }

        // Event Handler
        private void PlayListMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                if (PlayListView.SelectedIndices.Count > 0 && ItemSelected != null)
                {
                    int idx = PlayListView.SelectedIndices[0];
                    PlayListView.Enabled = false;
                    PlayListView.Enabled = true;
                    ItemSelected?.Invoke(this, new ItemSelectedEventArgs(idx));
                }
            }
            else
            {
                ListViewItem selectedItem = PlayListView.GetItemAt(e.X, e.Y);
                ContextMenu PlayListContextMenu = new ContextMenu();

                MenuItem deleteItem = new MenuItem("Delete Item");
                deleteItem.Click += (senders, es) =>
                {
                    int delIdx = (int)selectedItem.Tag;

                    ItemDeletedEventArgs ess = new ItemDeletedEventArgs
                    {
                        idx = delIdx,
                        IsDeleteAll = false,
                        IsSelectedMusicDeleted = (delIdx == Program.playList.GetCurrent())
                    };

                    Program.playList.RemoveAt(delIdx);
                    UpdateList();

                    GC.Collect();

                    ItemDeleted?.Invoke(this, ess);
                };

                MenuItem ClearAll = new MenuItem("Clear All");
                ClearAll.Click += (senders, es) =>
                {
                    foreach (MusicInfo mi in Program.playList)
                    {
                        mi.Dispose();
                    }

                    ItemDeletedEventArgs ess = new ItemDeletedEventArgs
                    {
                        idx = -1,
                        IsDeleteAll = true,
                        IsSelectedMusicDeleted = true
                    };

                    Program.playList.Clear();
                    UpdateList();

                    GC.Collect();

                    ItemDeleted?.Invoke(this, ess);
                };

                PlayListContextMenu.MenuItems.Add(deleteItem);
                PlayListContextMenu.MenuItems.Add(ClearAll);

                PlayListContextMenu.Show(PlayListView, e.Location);
            }
        }
        private void MusicPlayButtonClicked(object sender, EventArgs e)
        {
            if (PlayButton.Text == "Play")
            {
                PlayButton.Text = "Pause";
            }
            else
            {
                PlayButton.Text = "Play";
            }
            PlayButtonClicked.Invoke(this, new EventArgs());
        }

        // Overrided Event Handler
        protected override void OnMouseUp(MouseEventArgs e)
        {
            mouseClicked = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
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
        protected override void OnMouseDown(MouseEventArgs e)
        {
            mouseClicked = true;
            mouseClickedLocation = e.Location;
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            ;
        }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);
        }

        private bool mouseClicked = false;
        private Point mouseClickedLocation;

        private double widthRatio;
        private double heightRatio;
    }

    public class ItemDeletedEventArgs : EventArgs
    {
        public int idx;
        public bool IsSelectedMusicDeleted;
        public bool IsDeleteAll;
    }
    public class ItemSelectedEventArgs : EventArgs
    {
        public int idx;

        public ItemSelectedEventArgs(int idx)
        {
            this.idx = idx;
        }
    }
}
