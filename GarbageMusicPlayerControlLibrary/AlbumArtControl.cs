using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GarbageMusicPlayerControlLibrary
{
    public partial class AlbumArtControl : UserControl
    {
        public Image Image { get; set; }
        public Image StoppedImage { get; set; } 

        public AlbumArtControl()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.Image = null;
            this.StoppedImage = null;
        }

        public void ReDraw()
        {
            this.Invalidate();
        }

        public void Stop()
        {
            IsPlay = false;
            this.Invalidate();
        }
        public void Start()
        {
            IsPlay = true;
            this.Invalidate();
        }

        private void DrawImage(Graphics graphics, Image img)
        {
            if (img == null)
                return;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Point location = new Point()
            {
                X = (this.Width - img.Width) / 2,
                Y = (this.Height - img.Height) / 2
            };

            graphics.DrawImage(img, location);
        }
        private void DrawAlbumArt(Graphics graphics)
        {
            //graphics.Clear(Color.Transparent);
            if (IsPlay)
            {
                DrawImage(graphics, this.Image);
            }
            else
            {
                DrawImage(graphics, this.StoppedImage);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            DrawAlbumArt(graphics);
        }

        private bool IsPlay = false;
    }
}
