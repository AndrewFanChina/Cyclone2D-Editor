namespace Cyclone.mod.util
{
    partial class SmallDialog_ShowPicture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmallDialog_ShowPicture));
            this.panel_ImgBG = new System.Windows.Forms.Panel();
            this.pictureBox_Img = new System.Windows.Forms.PictureBox();
            this.panel_introduction = new System.Windows.Forms.Panel();
            this.richTextBox_Content = new System.Windows.Forms.RichTextBox();
            this.splitContainer_ALL = new System.Windows.Forms.SplitContainer();
            this.panel_ImgBG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Img)).BeginInit();
            this.panel_introduction.SuspendLayout();
            this.splitContainer_ALL.Panel1.SuspendLayout();
            this.splitContainer_ALL.Panel2.SuspendLayout();
            this.splitContainer_ALL.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ImgBG
            // 
            this.panel_ImgBG.AutoScroll = true;
            this.panel_ImgBG.Controls.Add(this.pictureBox_Img);
            this.panel_ImgBG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ImgBG.Location = new System.Drawing.Point(0, 0);
            this.panel_ImgBG.Name = "panel_ImgBG";
            this.panel_ImgBG.Padding = new System.Windows.Forms.Padding(3);
            this.panel_ImgBG.Size = new System.Drawing.Size(776, 422);
            this.panel_ImgBG.TabIndex = 1;
            // 
            // pictureBox_Img
            // 
            this.pictureBox_Img.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox_Img.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Img.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Img.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Img.Name = "pictureBox_Img";
            this.pictureBox_Img.Size = new System.Drawing.Size(773, 419);
            this.pictureBox_Img.TabIndex = 0;
            this.pictureBox_Img.TabStop = false;
            // 
            // panel_introduction
            // 
            this.panel_introduction.Controls.Add(this.richTextBox_Content);
            this.panel_introduction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_introduction.Location = new System.Drawing.Point(0, 0);
            this.panel_introduction.Name = "panel_introduction";
            this.panel_introduction.Size = new System.Drawing.Size(776, 128);
            this.panel_introduction.TabIndex = 2;
            // 
            // richTextBox_Content
            // 
            this.richTextBox_Content.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Content.Location = new System.Drawing.Point(0, 0);
            this.richTextBox_Content.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox_Content.Name = "richTextBox_Content";
            this.richTextBox_Content.ReadOnly = true;
            this.richTextBox_Content.Size = new System.Drawing.Size(776, 128);
            this.richTextBox_Content.TabIndex = 0;
            this.richTextBox_Content.Text = "";
            // 
            // splitContainer_ALL
            // 
            this.splitContainer_ALL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_ALL.Location = new System.Drawing.Point(4, 4);
            this.splitContainer_ALL.Name = "splitContainer_ALL";
            this.splitContainer_ALL.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_ALL.Panel1
            // 
            this.splitContainer_ALL.Panel1.Controls.Add(this.panel_ImgBG);
            // 
            // splitContainer_ALL.Panel2
            // 
            this.splitContainer_ALL.Panel2.Controls.Add(this.panel_introduction);
            this.splitContainer_ALL.Size = new System.Drawing.Size(776, 554);
            this.splitContainer_ALL.SplitterDistance = 422;
            this.splitContainer_ALL.TabIndex = 4;
            // 
            // SmallDialog_ShowPicture
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.splitContainer_ALL);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SmallDialog_ShowPicture";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel_ImgBG.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Img)).EndInit();
            this.panel_introduction.ResumeLayout(false);
            this.splitContainer_ALL.Panel1.ResumeLayout(false);
            this.splitContainer_ALL.Panel2.ResumeLayout(false);
            this.splitContainer_ALL.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_Content;
        private System.Windows.Forms.PictureBox pictureBox_Img;
        private System.Windows.Forms.Panel panel_ImgBG;
        private System.Windows.Forms.Panel panel_introduction;
        private System.Windows.Forms.SplitContainer splitContainer_ALL;
    }
}