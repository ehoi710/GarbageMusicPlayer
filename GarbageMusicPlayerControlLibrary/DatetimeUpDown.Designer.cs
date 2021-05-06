namespace GarbageMusicPlayerControlLibrary
{
    partial class DatetimeUpDown
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
            this.UpButton = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.DataLabel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // UpButton
            // 
            this.UpButton.Font = new System.Drawing.Font("Gulim", 5F);
            this.UpButton.Location = new System.Drawing.Point(80, 17);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(17, 26);
            this.UpButton.TabIndex = 1;
            this.UpButton.Text = "▲";
            this.UpButton.UseVisualStyleBackColor = true;
            this.UpButton.Click += new System.EventHandler(this.Up);
            // 
            // DownButton
            // 
            this.DownButton.Font = new System.Drawing.Font("Gulim", 5F);
            this.DownButton.Location = new System.Drawing.Point(86, 79);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(33, 25);
            this.DownButton.TabIndex = 2;
            this.DownButton.Text = "▼";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.Click += new System.EventHandler(this.Down);
            // 
            // DataLabel
            // 
            this.DataLabel.Font = new System.Drawing.Font("Gulim", 10F);
            this.DataLabel.Location = new System.Drawing.Point(27, 83);
            this.DataLabel.Multiline = true;
            this.DataLabel.Name = "DataLabel";
            this.DataLabel.Size = new System.Drawing.Size(29, 31);
            this.DataLabel.TabIndex = 3;
            this.DataLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataLabel.TextChanged += new System.EventHandler(this.valueChanged);
            // 
            // DatetimeUpDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DataLabel);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.UpButton);
            this.Name = "DatetimeUpDown";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.TextBox DataLabel;
    }
}
