namespace GarbageMusicPlayer
{
    partial class ControlForm
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
            this.PlayListView = new System.Windows.Forms.ListView();
            this.PlayButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PlayListView
            // 
            this.PlayListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlayListView.HideSelection = false;
            this.PlayListView.Location = new System.Drawing.Point(29, 77);
            this.PlayListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PlayListView.Name = "PlayListView";
            this.PlayListView.Size = new System.Drawing.Size(121, 128);
            this.PlayListView.TabIndex = 0;
            this.PlayListView.UseCompatibleStateImageBehavior = false;
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(431, 361);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(75, 23);
            this.PlayButton.TabIndex = 1;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.MusicPlayButtonClicked);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.PlayListView);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ControlForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView PlayListView;
        private System.Windows.Forms.Button PlayButton;
    }
}