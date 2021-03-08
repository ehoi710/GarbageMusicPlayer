namespace GarbageMusicPlayer
{
    partial class ListForm
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
            this.SuspendLayout();
            // 
            // PlayListView
            // 
            this.PlayListView.HideSelection = false;
            this.PlayListView.Location = new System.Drawing.Point(29, 58);
            this.PlayListView.Name = "PlayListView";
            this.PlayListView.Size = new System.Drawing.Size(121, 97);
            this.PlayListView.TabIndex = 0;
            this.PlayListView.UseCompatibleStateImageBehavior = false;
            // 
            // ListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PlayListView);
            this.Name = "ListForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView PlayListView;
    }
}