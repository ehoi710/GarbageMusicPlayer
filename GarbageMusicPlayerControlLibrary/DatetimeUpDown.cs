using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GarbageMusicPlayerControlLibrary
{
    public partial class DatetimeUpDown : UserControl
    {
        public TimeSpan value { get; set; }

        [Description("Value Changed Event"), Category("")]
        public event EventHandler ValueChangeEvent;

        public DatetimeUpDown()
        {
            InitializeComponent();

            this.value = TimeSpan.FromSeconds(0);

            SetText();
            this.Invalidate();
        }

        public void SetValue(TimeSpan time)
        {
            this.value = time;
            SetText();
        }

        private void SetText()
        {
            this.DataLabel.Text = this.value.ToString();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.DataLabel.Location = new Point(0, 0);
            this.DataLabel.Size = new Size(this.Width - BUTTON_WIDTH, this.Height);

            this.UpButton.Location = new Point(this.Width - BUTTON_WIDTH, 0);
            this.UpButton.Size = new Size(BUTTON_WIDTH, this.Height / 2);
            
            this.DownButton.Location = new Point(this.Width - BUTTON_WIDTH, this.Height / 2);
            this.DownButton.Size = new Size(BUTTON_WIDTH, this.Height / 2);
        }

        private const int BUTTON_WIDTH = 20;

        private void Up(object sender, EventArgs e)
        {
            this.value += TimeSpan.FromSeconds(1);
            SetText();

            ValueChangeEvent(this, new EventArgs());
        }

        private void Down(object sender, EventArgs e)
        {
            if (this.value == TimeSpan.FromSeconds(0)) return;
            this.value -= TimeSpan.FromSeconds(1);
            SetText();

            ValueChangeEvent(this, new EventArgs());
        }

        private void valueChanged(object sender, EventArgs e)
        {
            try
            {
                this.value = TimeSpan.Parse(DataLabel.Text);
                ValueChangeEvent(this, new EventArgs());
            }
            catch
            {

            }
        }
    }
}
