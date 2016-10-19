namespace WallpaperBoi
{
    partial class WallpaperForm
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
            this.wallpaper = new System.Windows.Forms.PictureBox();
            this.broken = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wallpaper)).BeginInit();
            this.SuspendLayout();
            // 
            // wallpaper
            // 
            this.wallpaper.BackColor = System.Drawing.Color.Transparent;
            this.wallpaper.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.wallpaper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wallpaper.Location = new System.Drawing.Point(0, 0);
            this.wallpaper.Name = "wallpaper";
            this.wallpaper.Size = new System.Drawing.Size(799, 592);
            this.wallpaper.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.wallpaper.TabIndex = 0;
            this.wallpaper.TabStop = false;
            // 
            // broken
            // 
            this.broken.BackColor = System.Drawing.Color.Transparent;
            this.broken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.broken.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.broken.Location = new System.Drawing.Point(0, 0);
            this.broken.Name = "broken";
            this.broken.Size = new System.Drawing.Size(799, 592);
            this.broken.TabIndex = 1;
            this.broken.Text = "Gratz Jason, \r\nyou broke it.";
            this.broken.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.broken.Visible = false;
            // 
            // WallpaperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 592);
            this.Controls.Add(this.broken);
            this.Controls.Add(this.wallpaper);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WallpaperForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WallpaperForm";
            this.Load += new System.EventHandler(this.WallpaperForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wallpaper)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox wallpaper;
        private System.Windows.Forms.Label broken;
    }
}