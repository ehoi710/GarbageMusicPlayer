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

        private int _currentTickPosition;
        public int CurrentTickPosition
        {
            get
            {
                return _currentTickPosition;
            }
            set
            {
                if (Min <= value && value <= Max)
                    _currentTickPosition = value;
                else if (value < Min)
                    _currentTickPosition = Min;
                else if (Max < value)
                    _currentTickPosition = Max;

                this.Invalidate();
            }
        }

        [Description("Current Change Event"), Category("")]
        public event EventHandler CurrentChangeEvent;

        // Constructor
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

            currentCoord = new Point(leftEnd.X, leftEnd.Y);

            Min = 0;
            Max = 1;
            CurrentTickPosition = 0;
        }

        private int CurrentXCoordinate()
        {
            return leftEnd.X + (rightEnd.X - leftEnd.X) * CurrentTickPosition / Max;
        }

        // Event Handler
        protected override void OnPaint(PaintEventArgs e)
        {
            currentCoord.X = CurrentXCoordinate();
            thumbRectangle.X = currentCoord.X - 5;
            thumbRectangle.Y = currentCoord.Y - 5;

            Graphics g = e.Graphics;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.DrawLine(new Pen(Color.Black, 2.1f), leftEnd, rightEnd);
            g.DrawLine(new Pen(Color.Cyan, 2), leftEnd, currentCoord);
            g.DrawLine(new Pen(Color.White, 2), currentCoord, rightEnd);
            
            g.FillEllipse(Brushes.Black, thumbRectangle);
            g.FillEllipse(
                Brushes.White, 
                new RectangleF(
                    new PointF(thumbRectangle.X + 0.1f, thumbRectangle.Y + 0.1f),
                    new SizeF(thumbRectangle.Width - 0.2f, thumbRectangle.Height - 0.2f)
                )
            );
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (thumbRectangle.Contains(e.Location))
            {
                thumbClicked = true;
                return;
            }
            int changeX = e.X;
            if (changeX < leftEnd.X)
                changeX = leftEnd.X;
            if (changeX > rightEnd.X)
                changeX = rightEnd.X;

            CurrentTickPosition = (changeX - leftEnd.X) * Max / (rightEnd.X - leftEnd.X);
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
        protected override void OnSizeChanged(EventArgs e)
        {
            leftEnd = new Point(5, 10);
            rightEnd = new Point(this.Width - 5, 10);
            base.OnSizeChanged(e);
        }

        private bool thumbClicked = false;

        private Point leftEnd;
        private Point rightEnd;

        private Rectangle thumbRectangle;
        private Point currentCoord;
    }
}
