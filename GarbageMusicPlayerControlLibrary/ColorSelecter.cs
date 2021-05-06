using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GarbageMusicPlayerControlLibrary
{
    public partial class ColorSelecter : UserControl
    {
        public Color SelectedColor { get; private set; }
        public int Argb 
        { 
            get
            {
                return SelectedColor.ToArgb();
            }
        }
        public byte A
        {
            get
            {
                return (byte)(this.Argb >> 8 * 3);
            }
        }
        public byte R
        {
            get
            {
                return (byte)(this.Argb >> 8 * 2);
            }
        }
        public byte G
        {
            get
            {
                return (byte)(this.Argb >> 8 * 1);
            }
        }
        public byte B
        {
            get
            {
                return (byte)(this.Argb >> 8 * 0);
            }
        }

        [Description("Color Changed"), Category("")]
        public event EventHandler ColorChanged;

        public ColorSelecter()
        {
            InitializeComponent();

            this.SelectedColor = Color.White;

            InitializeColorViewer();
            InitializeColorTrackBar();
            DrawColor();

            colorChanging = false;
        }

        private void InitializeColorViewer()
        {
            background = new Bitmap(this.ColorViewer.Width, this.ColorViewer.Height);
            for(int y = 0; y < this.ColorViewer.Height; y++)
            {
                for(int x = 0; x < this.ColorViewer.Width; x++)
                {
                    background.SetPixel(x, y, ((x / 10) % 2 == 0) ^ ((y / 10) % 2 == 0) ? Color.White : Color.Gray);
                }
            }

            this.ColorViewer.BackgroundImage = background;
        }
        private void InitializeColorTrackBar()
        {
            this.RedTrackBar.Minimum = 0;
            this.RedTrackBar.Maximum = 255;
            this.RedTrackBar.Value = 0;

            this.GreenTrackBar.Minimum = 0;
            this.GreenTrackBar.Maximum = 255;
            this.GreenTrackBar.Value = 0;

            this.BlueTrackBar.Minimum = 0;
            this.BlueTrackBar.Maximum = 255;
            this.BlueTrackBar.Value = 0;

            this.RedTrackBar.ValueChanged += ColorTrackBarChanged;
            this.GreenTrackBar.ValueChanged += ColorTrackBarChanged;
            this.BlueTrackBar.ValueChanged += ColorTrackBarChanged;
        }

        private void DrawColor()
        {
            colorChanging = true;
            Bitmap col = new Bitmap(this.ColorViewer.Width, this.ColorViewer.Height);
            for (int y = 0; y < this.ColorViewer.Height; y++)
            {
                for (int x = 0; x < this.ColorViewer.Width; x++)
                {
                    col.SetPixel(x, y, SelectedColor);
                }
            }
            this.ColorViewer.Image = col;
            this.ColorCodeBox.Text = this.Argb.ToString("X");

            this.RedTrackBar.Value = this.R;
            this.GreenTrackBar.Value = this.G;
            this.BlueTrackBar.Value = this.B;

            colorChanging = false;
        }

        private void ColorTrackBarChanged(object sender, EventArgs e)
        {
            if (colorChanging) return;

            int Argb = 0;
            Argb += (0xFF << 8 * 3);
            Argb += (RedTrackBar.Value << 8 * 2);
            Argb += (GreenTrackBar.Value << 8 * 1);
            Argb += (BlueTrackBar.Value);

            this.SelectedColor = Color.FromArgb(Argb);

            DrawColor();

            ColorChanged(this, new EventArgs());
        }

        public void SetColor(int Argb)
        {
            int rgb = (int)(0xFF000000 | Argb);
            this.SelectedColor = Color.FromArgb(rgb);

            DrawColor();

            ColorChanged(this, new EventArgs());
        }

        public void SetColor(Color color)
        {
            this.SelectedColor = color;

            DrawColor();
            ColorChanged(this, new EventArgs());
        }

        public int GetArgb()
        {
            int result = 0;

            result += A << 8 * 3;
            result += R << 8 * 2;
            result += G * 0x00000100;
            result += B * 0x00000001;

            return result;
        }

        private Bitmap background;

        private void ValueChanged(object sender, EventArgs e)
        {
            if (colorChanging) return;

            if (this.ColorCodeBox.Text == null) return;
            if (this.ColorCodeBox.Text.Length != 8) return;

            int result = 0;
            for(int i = 0; i < 8; i++)
            {
                result *= 0x10;

                if ('A' <= ColorCodeBox.Text[i] && ColorCodeBox.Text[i] <= 'F') result += ColorCodeBox.Text[i] - 'A' + 10;
                else result += ColorCodeBox.Text[i] - '0';
            }

            SetColor(result);
        }

        private bool colorChanging = true;
    }
}
