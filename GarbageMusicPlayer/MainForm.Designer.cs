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
            this.MusicListTreeView = new System.Windows.Forms.TreeView();
            this.PlayButton = new System.Windows.Forms.Button();
            this.AlbumArtBox = new System.Windows.Forms.PictureBox();
            this.PlayListView = new System.Windows.Forms.ListView();
            this.VolumeTrackBar = new System.Windows.Forms.TrackBar();
            this.Background = new System.Windows.Forms.PictureBox();
            this.MusicPlayTimeCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.musicTrackBar = new GarbageMusicPlayerControlLibrary.MusicTrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.AlbumArtBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Background)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.BackColor = System.Drawing.Color.Transparent;
            this.TitleTextBox.Font = new System.Drawing.Font("Mplus 1p Light", 15F);
            this.TitleTextBox.ForeColor = System.Drawing.Color.White;
            this.TitleTextBox.Location = new System.Drawing.Point(45, 593);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(360, 30);
            this.TitleTextBox.TabIndex = 0;
            this.TitleTextBox.Text = "label1";
            this.TitleTextBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MusicListTreeView
            // 
            this.MusicListTreeView.Location = new System.Drawing.Point(466, 307);
            this.MusicListTreeView.Name = "MusicListTreeView";
            this.MusicListTreeView.Size = new System.Drawing.Size(360, 480);
            this.MusicListTreeView.TabIndex = 1;
            this.MusicListTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.MusicListTreeView_AfterSelect);
            // 
            // PlayButton
            // 
            this.PlayButton.Font = new System.Drawing.Font("Mplus 1p Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayButton.Location = new System.Drawing.Point(170, 674);
            this.PlayButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(120, 50);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // AlbumArtBox
            // 
            this.AlbumArtBox.BackColor = System.Drawing.Color.Transparent;
            this.AlbumArtBox.Image = global::GarbageMusicPlayer.Properties.Resources.noAlbumImage;
            this.AlbumArtBox.Location = new System.Drawing.Point(45, 172);
            this.AlbumArtBox.Margin = new System.Windows.Forms.Padding(6);
            this.AlbumArtBox.Name = "AlbumArtBox";
            this.AlbumArtBox.Size = new System.Drawing.Size(360, 360);
            this.AlbumArtBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.AlbumArtBox.TabIndex = 3;
            this.AlbumArtBox.TabStop = false;
            // 
            // PlayListView
            // 
            this.PlayListView.HideSelection = false;
            this.PlayListView.Location = new System.Drawing.Point(849, 307);
            this.PlayListView.Name = "PlayListView";
            this.PlayListView.Size = new System.Drawing.Size(360, 480);
            this.PlayListView.TabIndex = 4;
            this.PlayListView.UseCompatibleStateImageBehavior = false;
            this.PlayListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlayListMouseDown);
            // 
            // VolumeTrackBar
            // 
            this.VolumeTrackBar.Location = new System.Drawing.Point(466, 31);
            this.VolumeTrackBar.Name = "VolumeTrackBar";
            this.VolumeTrackBar.Size = new System.Drawing.Size(180, 56);
            this.VolumeTrackBar.TabIndex = 5;
            this.VolumeTrackBar.Scroll += new System.EventHandler(this.VolumeBarScrolled);
            // 
            // Background
            // 
            this.Background.Image = global::GarbageMusicPlayer.Properties.Resources.DefaultBackground;
            this.Background.Location = new System.Drawing.Point(0, 10);
            this.Background.Name = "Background";
            this.Background.Size = new System.Drawing.Size(450, 810);
            this.Background.TabIndex = 6;
            this.Background.TabStop = false;
            // 
            // MusicPlayTimeCheckTimer
            // 
            this.MusicPlayTimeCheckTimer.Tick += new System.EventHandler(this.MusicPlayStateChecker);
            // 
            // musicTrackBar
            // 
            this.musicTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.musicTrackBar.Location = new System.Drawing.Point(45, 541);
            this.musicTrackBar.Name = "musicTrackBar";
            this.musicTrackBar.Size = new System.Drawing.Size(360, 30);
            this.musicTrackBar.TabIndex = 7;
            this.musicTrackBar.Value = 0;
            this.musicTrackBar.CurrentChangeEvent += new System.EventHandler(this.MusicTrackBarScrolled);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 820);
            this.Controls.Add(this.musicTrackBar);
            this.Controls.Add(this.VolumeTrackBar);
            this.Controls.Add(this.PlayListView);
            this.Controls.Add(this.AlbumArtBox);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.MusicListTreeView);
            this.Controls.Add(this.TitleTextBox);
            this.Controls.Add(this.Background);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Custom TrackBar Enabled";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FomeClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyboardDown);
            ((System.ComponentModel.ISupportInitialize)(this.AlbumArtBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Background)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleTextBox;
        private System.Windows.Forms.TreeView MusicListTreeView;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.PictureBox AlbumArtBox;
        private System.Windows.Forms.ListView PlayListView;
        private System.Windows.Forms.TrackBar VolumeTrackBar;
        private System.Windows.Forms.PictureBox Background;
        private GarbageMusicPlayerControlLibrary.MusicTrackBar musicTrackBar;
        private System.Windows.Forms.Timer MusicPlayTimeCheckTimer;
    }
}

