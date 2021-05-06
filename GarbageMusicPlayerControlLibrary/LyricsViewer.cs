using System.Drawing;
using System.Windows.Forms;
using GarbageMusicPlayerClassLibrary;
using System.Drawing.Drawing2D;

namespace GarbageMusicPlayerControlLibrary
{
    public partial class LyricsViewer : UserControl
    {
        public LyricsContainer Lyrics { get; private set; }
        public int syncedLine;

        public void Reset()
        {
            this.clickedPosition = 0;
            this.LyricsPosition = 0;
            this.LyricsHeight = 1;
        }
        public void ReDraw()
        {
            this.Invalidate();
        }

        public void SetLyrics(string str)
        {
            this.Lyrics = new LyricsContainer(str);
            this.syncedLine = -1;
        }

        public void SyncedLyricsRefresh(int currentSec)
        {
            if (this.Lyrics == null) return;
            if (this.Lyrics.isSync == false) return;

            this.syncedLine = -1;
            while (
                this.Lyrics.data.Count > this.syncedLine + 1 &&
                currentSec >= this.Lyrics.data[this.syncedLine + 1].time.TotalSeconds
                )
            {
                this.syncedLine++;
            }
            this.Invalidate();
        }

        private void UpdateLyricsPosition(int dy)
        {
            this.LyricsPosition += dy;

            if (this.LyricsPosition < this.Height - this.LyricsHeight)
                this.LyricsPosition = this.Height - this.LyricsHeight;
            if (this.LyricsPosition > 0)
                this.LyricsPosition = 0;
        }

        private void DrawLyrics(Graphics graphics)
        {
            if (Lyrics == null || Lyrics.data == null)
            {
                return;
            }

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int stringPos = LyricsPosition;

            float fontSize = 10.0f;
            Font font = new Font(this.Font.Name, fontSize);

            graphics.FillRectangle(
                new SolidBrush(Color.FromArgb(0x1F7F7F7F)),
                new Rectangle(new Point(0, 0), this.Size)
            );

            this.LyricsHeight = 0;
            SizeF[] sizeArray = new SizeF[this.Lyrics.data.Count];
            for (int i = 0; i < this.Lyrics.data.Count; i++)
            {
                sizeArray[i] = graphics.MeasureString(this.Lyrics.data[i].str, font);
                this.LyricsHeight += (int)sizeArray[i].Height;
            }

            for (int i = 0; i < this.Lyrics.data.Count; i++)
            {
                SizeF LineSize = sizeArray[i];
                int lyricsX = (int)(this.Width - LineSize.Width) / 2;
                int LineHeight = (int)LineSize.Height;

                if (stringPos + LineHeight < 0)
                {
                    stringPos += LineHeight;
                    continue;
                }
                if (stringPos > this.Height)
                {
                    break;
                }

                GraphicsPath stringPath = new GraphicsPath();
                stringPath.AddString(
                    this.Lyrics.data[i].str,
                    this.Font.FontFamily,
                    (int)FontStyle.Regular,
                    graphics.DpiY * fontSize / 72,
                    new Point(lyricsX, stringPos),
                    new StringFormat()
                );
                stringPos += LineHeight;

                graphics.DrawPath(new Pen(Color.Black, 2f), stringPath);
                if (this.Lyrics.isSync)
                {
                    if (this.syncedLine == i)
                        graphics.FillPath(new SolidBrush(this.Lyrics.data[i].selectedColor), stringPath);
                    else
                        graphics.FillPath(new SolidBrush(this.Lyrics.data[i].unselectedColor), stringPath);
                }
                else
                {
                    graphics.FillPath(new SolidBrush(Color.White), stringPath);
                }
            }
        }

        public LyricsViewer()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.Lyrics = null;
            Reset();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            DrawLyrics(graphics);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.IsMouseClick)
            {
                int dy = e.Y - clickedPosition;
                this.clickedPosition = e.Y;

                this.UpdateLyricsPosition(dy);
                this.Invalidate();
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.IsMouseClick = true;
            this.clickedPosition = e.Y;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.IsMouseClick = false;
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.UpdateLyricsPosition(e.Delta);
            this.Invalidate();
        }

        private int clickedPosition;
        private int LyricsPosition;
        private int LyricsHeight;

        private bool IsMouseClick;
    }
}
