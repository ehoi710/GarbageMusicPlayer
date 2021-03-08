using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GarbageMusicPlayerControlLibrary
{
    public partial class MusicTrackBar: UserControl
    {
        public int Min { get; set; }
        public int Max { get; set; }

        private int _CurrentTickPosition;
        public int CurrentTickPosition
        {
            get
            {
                return _CurrentTickPosition;
            }

            set
            {
                if (Min <= value && value <= Max)
                    _CurrentTickPosition = value;
                else if (value < Min)
                    _CurrentTickPosition = Min;
                else if (Max < value)
                    _CurrentTickPosition = Max;

                this.Invalidate();
            }
        }

        private bool thumbClicked = false;

        private readonly Point leftEnd;
        private readonly Point rightEnd;

        private Rectangle thumbRectangle;

        [Description("Current Change Event"), Category("")]
        public event EventHandler CurrentChangeEvent;

        public MusicTrackBar()
        {
            InitializeComponent();
            
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            
            leftEnd = new Point(5, 10);
            rightEnd = new Point(this.Width - 5, 10);

            thumbRectangle = new Rectangle(
                new Point(0, 5), 
                new Size(10, 10)
            );

            Min = 0;
            Max = 1;
        }

        private int CurrentXCoordinate()
        {
            return (CurrentTickPosition * (this.Width - 10) / Max);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            thumbRectangle.X = CurrentXCoordinate();

            Graphics g = e.Graphics;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.DrawLine(new Pen(Color.Black, 2.1f), leftEnd, rightEnd);
            g.DrawLine(new Pen(Color.White, 2), leftEnd, rightEnd);

            g.FillEllipse(Brushes.Black, thumbRectangle);
            g.FillEllipse(Brushes.White, new RectangleF(
                new PointF(thumbRectangle.X + 0.1f, thumbRectangle.Y + 0.1f),
                new SizeF(thumbRectangle.Width - 0.2f, thumbRectangle.Height - 0.2f)
                ));
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (thumbRectangle.Contains(e.Location))
            {
                thumbClicked = true;
                return;
            }
            int changeX = e.X;
            if (changeX < 5)
                changeX = 5;
            if (changeX > this.Width - 5)
                changeX = this.Width - 5;

            changeX -= 5;
            CurrentTickPosition = (changeX * Max / (this.Width - 10));
            Invoke(CurrentChangeEvent);
            this.Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            thumbClicked = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if(thumbClicked)
            {
                if (CurrentTickPosition < Max && e.X > CurrentXCoordinate())
                {
                    CurrentTickPosition++;
                }
                else if (CurrentTickPosition > 0 && e.X < CurrentXCoordinate())
                {
                    CurrentTickPosition--;
                }
                Invoke(CurrentChangeEvent);
                this.Invalidate();
            }
        }
    }
}
