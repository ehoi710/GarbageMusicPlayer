namespace GarbageMusicPlayerControlLibrary
{
    partial class ColorSelecter
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GreenTrackBar = new System.Windows.Forms.TrackBar();
            this.ColorViewer = new System.Windows.Forms.PictureBox();
            this.BlueTrackBar = new System.Windows.Forms.TrackBar();
            this.RedTrackBar = new System.Windows.Forms.TrackBar();
            this.ColorCodeBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.GreenTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // GreenTrackBar
            // 
            this.GreenTrackBar.AutoSize = false;
            this.GreenTrackBar.Location = new System.Drawing.Point(20, 100);
            this.GreenTrackBar.Name = "GreenTrackBar";
            this.GreenTrackBar.Size = new System.Drawing.Size(100, 30);
            this.GreenTrackBar.TabIndex = 0;
            this.GreenTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // ColorViewer
            // 
            this.ColorViewer.Location = new System.Drawing.Point(0, 0);
            this.ColorViewer.Name = "ColorViewer";
            this.ColorViewer.Size = new System.Drawing.Size(60, 60);
            this.ColorViewer.TabIndex = 1;
            this.ColorViewer.TabStop = false;
            // 
            // BlueTrackBar
            // 
            this.BlueTrackBar.AutoSize = false;
            this.BlueTrackBar.Location = new System.Drawing.Point(20, 130);
            this.BlueTrackBar.Name = "BlueTrackBar";
            this.BlueTrackBar.Size = new System.Drawing.Size(100, 30);
            this.BlueTrackBar.TabIndex = 2;
            this.BlueTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // RedTrackBar
            // 
            this.RedTrackBar.AutoSize = false;
            this.RedTrackBar.Location = new System.Drawing.Point(20, 70);
            this.RedTrackBar.Name = "RedTrackBar";
            this.RedTrackBar.Size = new System.Drawing.Size(100, 30);
            this.RedTrackBar.TabIndex = 3;
            this.RedTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // ColorCodeBox
            // 
            this.ColorCodeBox.Location = new System.Drawing.Point(20, 35);
            this.ColorCodeBox.Name = "ColorCodeBox";
            this.ColorCodeBox.Size = new System.Drawing.Size(100, 25);
            this.ColorCodeBox.TabIndex = 4;
            this.ColorCodeBox.TextChanged += new System.EventHandler(this.ValueChanged);
            // 
            // ColorSelecter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ColorCodeBox);
            this.Controls.Add(this.RedTrackBar);
            this.Controls.Add(this.BlueTrackBar);
            this.Controls.Add(this.ColorViewer);
            this.Controls.Add(this.GreenTrackBar);
            this.Name = "ColorSelecter";
            this.Size = new System.Drawing.Size(120, 160);
            ((System.ComponentModel.ISupportInitialize)(this.GreenTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar GreenTrackBar;
        private System.Windows.Forms.PictureBox ColorViewer;
        private System.Windows.Forms.TrackBar BlueTrackBar;
        private System.Windows.Forms.TrackBar RedTrackBar;
        private System.Windows.Forms.TextBox ColorCodeBox;
    }
}
