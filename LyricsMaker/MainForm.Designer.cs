namespace LyricsMaker
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AlbumImageBox = new System.Windows.Forms.PictureBox();
            this.MusicLoadButton = new System.Windows.Forms.Button();
            this.LyricsSaveButton = new System.Windows.Forms.Button();
            this.LyricsBox = new System.Windows.Forms.ListView();
            this.MusicToggleButton = new System.Windows.Forms.Button();
            this.LineEdit = new System.Windows.Forms.TextBox();
            this.LeftRange = new GarbageMusicPlayerControlLibrary.DatetimeUpDown();
            this.unselectedColor = new GarbageMusicPlayerControlLibrary.ColorSelecter();
            this.selectedColor = new GarbageMusicPlayerControlLibrary.ColorSelecter();
            ((System.ComponentModel.ISupportInitialize)(this.AlbumImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AlbumImageBox
            // 
            this.AlbumImageBox.Location = new System.Drawing.Point(10, 10);
            this.AlbumImageBox.Name = "AlbumImageBox";
            this.AlbumImageBox.Size = new System.Drawing.Size(300, 300);
            this.AlbumImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AlbumImageBox.TabIndex = 0;
            this.AlbumImageBox.TabStop = false;
            // 
            // MusicLoadButton
            // 
            this.MusicLoadButton.Location = new System.Drawing.Point(10, 380);
            this.MusicLoadButton.Name = "MusicLoadButton";
            this.MusicLoadButton.Size = new System.Drawing.Size(145, 60);
            this.MusicLoadButton.TabIndex = 1;
            this.MusicLoadButton.Text = "Load";
            this.MusicLoadButton.UseVisualStyleBackColor = true;
            this.MusicLoadButton.Click += new System.EventHandler(this.FileOpen);
            // 
            // LyricsSaveButton
            // 
            this.LyricsSaveButton.Location = new System.Drawing.Point(165, 380);
            this.LyricsSaveButton.Name = "LyricsSaveButton";
            this.LyricsSaveButton.Size = new System.Drawing.Size(145, 60);
            this.LyricsSaveButton.TabIndex = 2;
            this.LyricsSaveButton.Text = "Save";
            this.LyricsSaveButton.UseVisualStyleBackColor = true;
            this.LyricsSaveButton.Click += new System.EventHandler(this.Save);
            // 
            // LyricsBox
            // 
            this.LyricsBox.HideSelection = false;
            this.LyricsBox.Location = new System.Drawing.Point(585, 92);
            this.LyricsBox.Name = "LyricsBox";
            this.LyricsBox.Size = new System.Drawing.Size(121, 97);
            this.LyricsBox.TabIndex = 4;
            this.LyricsBox.UseCompatibleStateImageBehavior = false;
            this.LyricsBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ItemSelected);
            // 
            // MusicToggleButton
            // 
            this.MusicToggleButton.Location = new System.Drawing.Point(10, 320);
            this.MusicToggleButton.Name = "MusicToggleButton";
            this.MusicToggleButton.Size = new System.Drawing.Size(300, 50);
            this.MusicToggleButton.TabIndex = 5;
            this.MusicToggleButton.Text = "Play";
            this.MusicToggleButton.UseVisualStyleBackColor = true;
            this.MusicToggleButton.Click += new System.EventHandler(this.MusicToggle);
            // 
            // LineEdit
            // 
            this.LineEdit.Location = new System.Drawing.Point(361, 92);
            this.LineEdit.Multiline = true;
            this.LineEdit.Name = "LineEdit";
            this.LineEdit.Size = new System.Drawing.Size(100, 25);
            this.LineEdit.TabIndex = 7;
            this.LineEdit.TextChanged += new System.EventHandler(this.LyricsChanged);
            // 
            // LeftRange
            // 
            this.LeftRange.Location = new System.Drawing.Point(445, 255);
            this.LeftRange.Name = "LeftRange";
            this.LeftRange.Size = new System.Drawing.Size(150, 150);
            this.LeftRange.TabIndex = 6;
            this.LeftRange.value = System.TimeSpan.Parse("00:00:00");
            // 
            // unselectedColor
            // 
            this.unselectedColor.Location = new System.Drawing.Point(325, 123);
            this.unselectedColor.Name = "unselectedColor";
            this.unselectedColor.Size = new System.Drawing.Size(160, 160);
            this.unselectedColor.TabIndex = 8;
            // 
            // selectedColor
            // 
            this.selectedColor.Location = new System.Drawing.Point(501, 123);
            this.selectedColor.Name = "selectedColor";
            this.selectedColor.Size = new System.Drawing.Size(160, 160);
            this.selectedColor.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.selectedColor);
            this.Controls.Add(this.unselectedColor);
            this.Controls.Add(this.LineEdit);
            this.Controls.Add(this.LeftRange);
            this.Controls.Add(this.MusicToggleButton);
            this.Controls.Add(this.LyricsBox);
            this.Controls.Add(this.LyricsSaveButton);
            this.Controls.Add(this.MusicLoadButton);
            this.Controls.Add(this.AlbumImageBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.AlbumImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox AlbumImageBox;
        private System.Windows.Forms.Button MusicLoadButton;
        private System.Windows.Forms.Button LyricsSaveButton;
        private System.Windows.Forms.ListView LyricsBox;
        private System.Windows.Forms.Button MusicToggleButton;
        private GarbageMusicPlayerControlLibrary.DatetimeUpDown LeftRange;
        private System.Windows.Forms.TextBox LineEdit;
        private GarbageMusicPlayerControlLibrary.ColorSelecter unselectedColor;
        private GarbageMusicPlayerControlLibrary.ColorSelecter selectedColor;
    }
}

