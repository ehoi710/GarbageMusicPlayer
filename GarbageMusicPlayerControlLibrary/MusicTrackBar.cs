using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GarbageMusicPlayerControlLibrary
{
    public partial class MusicTrackBar: UserControl
    {
        private int CurrentTickPosition = 0;
        private int Min = 0;
        private int Max = 100;
        private bool thumbClicked = false;

        private readonly Point leftEnd;
        private readonly Point rightEnd;

        private Rectangle thumbRectangle = new Rectangle();

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

            thumbRectangle.Y = 5;
            thumbRectangle.Width = 10;
            thumbRectangle.Height = 10;
        }

        public int Value
        {
            get
            {
                return CurrentTickPosition;
            }
            set
            {
                if (Min <= value && value <= Max)
                    CurrentTickPosition = value;
                else if (value < Min)
                    CurrentTickPosition = Min;
                else if (Max < value)
                    CurrentTickPosition = Max;
                this.Invalidate();
            }
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
            g.FillEllipse(Brushes.White, thumbRectangle);
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
