namespace GarbageMusicPlayer
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TitleTextBox = new System.Windows.Forms.Label();
            this.PlayButton = new System.Windows.Forms.Button();
            this.AlbumArtBox = new System.Windows.Forms.PictureBox();
            this.MusicPlayTimeCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.MusicComment = new System.Windows.Forms.Label();
            this.musicTrackBar = new GarbageMusicPlayerControlLibrary.MusicTrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.AlbumArtBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.BackColor = System.Drawing.Color.Transparent;
            this.TitleTextBox.Font = new System.Drawing.Font("Mplus 1p Light", 15F);
            this.TitleTextBox.ForeColor = System.Drawing.Color.White;
            this.TitleTextBox.Location = new System.Drawing.Point(45, 619);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(360, 30);
            this.TitleTextBox.TabIndex = 0;
            this.TitleTextBox.Text = "label1";
            this.TitleTextBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlayButton
            // 
            this.PlayButton.Font = new System.Drawing.Font("Mplus 1p Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayButton.Location = new System.Drawing.Point(160, 665);
            this.PlayButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(120, 50);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.TabStop = false;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // AlbumArtBox
            // 
            this.AlbumArtBox.BackColor = System.Drawing.Color.Transparent;
            this.AlbumArtBox.Image = global::GarbageMusicPlayer.Properties.Resources.noAlbumImage;
            this.AlbumArtBox.Location = new System.Drawing.Point(66, 192);
            this.AlbumArtBox.Margin = new System.Windows.Forms.Padding(6);
            this.AlbumArtBox.Name = "AlbumArtBox";
            this.AlbumArtBox.Size = new System.Drawing.Size(360, 360);
            this.AlbumArtBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.AlbumArtBox.TabIndex = 3;
            this.AlbumArtBox.TabStop = false;
            // 
            // MusicPlayTimeCheckTimer
            // 
            this.MusicPlayTimeCheckTimer.Tick += new System.EventHandler(this.MusicPlayStateChecker);
            // 
            // MusicComment
            // 
            this.MusicComment.BackColor = System.Drawing.Color.Transparent;
            this.MusicComment.Font = new System.Drawing.Font("나눔고딕코딩", 10F);
            this.MusicComment.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MusicComment.Location = new System.Drawing.Point(111, 50);
            this.MusicComment.Name = "MusicComment";
            this.MusicComment.Size = new System.Drawing.Size(360, 100);
            this.MusicComment.TabIndex = 8;
            this.MusicComment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // musicTrackBar
            // 
            this.musicTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.musicTrackBar.CurrentTickPosition = 0;
            this.musicTrackBar.Location = new System.Drawing.Point(51, 561);
            this.musicTrackBar.Max = 0;
            this.musicTrackBar.Name = "musicTrackBar";
            this.musicTrackBar.Size = new System.Drawing.Size(360, 30);
            this.musicTrackBar.TabIndex = 7;
            this.musicTrackBar.CurrentChangeEvent += new System.EventHandler(this.MusicTrackBarScrolled);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1200, 820);
            this.Controls.Add(this.TitleTextBox);
            this.Controls.Add(this.MusicComment);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.AlbumArtBox);
            this.Controls.Add(this.musicTrackBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GarbageMusicPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FomeClosing);
            ((System.ComponentModel.ISupportInitialize)(this.AlbumArtBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TitleTextBox;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.PictureBox AlbumArtBox;
        private GarbageMusicPlayerControlLibrary.MusicTrackBar musicTrackBar;
        private System.Windows.Forms.Timer MusicPlayTimeCheckTimer;
        private System.Windows.Forms.Label MusicComment;
    }
}

