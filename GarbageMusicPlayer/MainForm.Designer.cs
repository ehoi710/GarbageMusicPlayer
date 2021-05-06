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
            this.MusicPlayTimeCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.CloseButton = new System.Windows.Forms.Button();
            this.MusicComment = new GarbageMusicPlayerControlLibrary.FormatTextBox();
            this.TitleTextBox = new GarbageMusicPlayerControlLibrary.FormatTextBox();
            this.AlbumArtBox = new GarbageMusicPlayerControlLibrary.AlbumArtControl();
            this.musicTrackBar = new GarbageMusicPlayerControlLibrary.MusicTrackBar();
            this.LyricsBox = new GarbageMusicPlayerControlLibrary.LyricsViewer();
            this.SuspendLayout();
            // 
            // MusicPlayTimeCheckTimer
            // 
            this.MusicPlayTimeCheckTimer.Tick += new System.EventHandler(this.MusicPlayStateChecker);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Transparent;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(762, 108);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(25, 28);
            this.CloseButton.TabIndex = 12;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = false;
            // 
            // MusicComment
            // 
            this.MusicComment.BackColor = System.Drawing.Color.Transparent;
            this.MusicComment.Font = new System.Drawing.Font("Mplus 1p Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MusicComment.Location = new System.Drawing.Point(577, 165);
            this.MusicComment.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MusicComment.Name = "MusicComment";
            this.MusicComment.Size = new System.Drawing.Size(150, 200);
            this.MusicComment.TabIndex = 11;
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.BackColor = System.Drawing.Color.Transparent;
            this.TitleTextBox.Font = new System.Drawing.Font("Mplus 1p Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleTextBox.Location = new System.Drawing.Point(947, 1552);
            this.TitleTextBox.Margin = new System.Windows.Forms.Padding(6, 9, 6, 9);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(281, 466);
            this.TitleTextBox.TabIndex = 10;
            // 
            // AlbumArtBox
            // 
            this.AlbumArtBox.BackColor = System.Drawing.Color.Transparent;
            this.AlbumArtBox.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlbumArtBox.Image = global::GarbageMusicPlayer.Properties.Resources.noAlbumImage;
            this.AlbumArtBox.Location = new System.Drawing.Point(66, 44);
            this.AlbumArtBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AlbumArtBox.Name = "AlbumArtBox";
            this.AlbumArtBox.Size = new System.Drawing.Size(150, 200);
            this.AlbumArtBox.StoppedImage = null;
            this.AlbumArtBox.TabIndex = 9;
            // 
            // musicTrackBar
            // 
            this.musicTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.musicTrackBar.CurrentTickPosition = 0;
            this.musicTrackBar.Location = new System.Drawing.Point(51, 748);
            this.musicTrackBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.musicTrackBar.Max = 1;
            this.musicTrackBar.Min = 0;
            this.musicTrackBar.Name = "musicTrackBar";
            this.musicTrackBar.Size = new System.Drawing.Size(360, 40);
            this.musicTrackBar.TabIndex = 7;
            this.musicTrackBar.CurrentChangeEvent += new System.EventHandler(this.MusicTrackBarScrolled);
            // 
            // LyricsBox
            // 
            this.LyricsBox.BackColor = System.Drawing.Color.Transparent;
            this.LyricsBox.Location = new System.Drawing.Point(463, 212);
            this.LyricsBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LyricsBox.Name = "LyricsBox";
            this.LyricsBox.Size = new System.Drawing.Size(150, 150);
            this.LyricsBox.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1200, 1055);
            this.Controls.Add(this.LyricsBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.MusicComment);
            this.Controls.Add(this.TitleTextBox);
            this.Controls.Add(this.AlbumArtBox);
            this.Controls.Add(this.musicTrackBar);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GarbageMusicPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseForm);
            this.ResumeLayout(false);

        }

        #endregion
        private GarbageMusicPlayerControlLibrary.MusicTrackBar musicTrackBar;
        private System.Windows.Forms.Timer MusicPlayTimeCheckTimer;
        private GarbageMusicPlayerControlLibrary.AlbumArtControl AlbumArtBox;
        private GarbageMusicPlayerControlLibrary.FormatTextBox TitleTextBox;
        private GarbageMusicPlayerControlLibrary.FormatTextBox MusicComment;
        private System.Windows.Forms.Button CloseButton;
        private GarbageMusicPlayerControlLibrary.LyricsViewer LyricsBox;
    }
}

