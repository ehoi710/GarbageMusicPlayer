using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System;

namespace GarbageMusicPlayerControlLibrary
{
    public partial class FormatTextBox : UserControl
    {
        public new string Text
        {
            get
            {
                if (_text == null)
                    _text = "";
                return _text;
            }
            set
            {
                _text = value;
                this.Invalidate();
            }
        }
        public float maxPt = 12.0f;

        //public event MouseEventHandler MouseUp;

        public FormatTextBox()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.BackColor = Color.Transparent;
        }

        private void DrawText(Graphics graphics, Font font)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            font = new Font(font.FontFamily, maxPt);

            SizeF stringSize = graphics.MeasureString(this.Text, font);
            while (stringSize.Width >= this.Width)
            {
                font = new Font(font.Name, font.Size - 0.5f);
                stringSize = graphics.MeasureString(this.Text, font);
            }
            while (stringSize.Height >= this.Height)
            {
                font = new Font(font.Name, font.Size - 0.5f);
                stringSize = graphics.MeasureString(this.Text, font);
            }

            PointF textLocation = new PointF
            {
                X = (this.Width - stringSize.Width) / 2,
                Y = (this.Height - stringSize.Height) / 2
            };

            GraphicsPath path = new GraphicsPath();
            path.AddString(
                this.Text,
                font.FontFamily,
                (int)FontStyle.Regular,
                graphics.DpiY * font.Size / 72.0f,
                textLocation,
                new StringFormat()
            );
            graphics.DrawPath(new Pen(Color.FromArgb(0x7F, 0x00, 0x00, 0x00), 3f), path);
            graphics.FillPath(new SolidBrush(Color.White), path);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            DrawText(graphics, this.Font);
        }

        private string _text;
    }
}
