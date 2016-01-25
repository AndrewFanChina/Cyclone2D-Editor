namespace Cyclone.mod.imgage
{
    partial class Form_Image_Compress
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
            this.colorDialog_clipEditorBg = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.checkBox_Compress = new System.Windows.Forms.CheckBox();
            this.checkBox_Confuse = new System.Windows.Forms.CheckBox();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_excute = new System.Windows.Forms.Button();
            this.panel_Center = new System.Windows.Forms.Panel();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.panel_Top = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_PathDest = new System.Windows.Forms.TextBox();
            this.button_openDest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_PathSrc = new System.Windows.Forms.TextBox();
            this.button_openSrc = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel_all.SuspendLayout();
            this.panel_Bottom.SuspendLayout();
            this.panel_Center.SuspendLayout();
            this.panel_Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_all
            // 
            this.tableLayoutPanel_all.ColumnCount = 1;
            this.tableLayoutPanel_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.Controls.Add(this.panel_Bottom, 0, 2);
            this.tableLayoutPanel_all.Controls.Add(this.panel_Center, 0, 1);
            this.tableLayoutPanel_all.Controls.Add(this.panel_Top, 0, 0);
            this.tableLayoutPanel_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_all.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel_all.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_all.Name = "tableLayoutPanel_all";
            this.tableLayoutPanel_all.RowCount = 3;
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(497, 330);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.Controls.Add(this.checkBox_Compress);
            this.panel_Bottom.Controls.Add(this.checkBox_Confuse);
            this.panel_Bottom.Controls.Add(this.button_Close);
            this.panel_Bottom.Controls.Add(this.button_excute);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 289);
            this.panel_Bottom.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(497, 41);
            this.panel_Bottom.TabIndex = 2;
            // 
            // checkBox_Compress
            // 
            this.checkBox_Compress.AutoSize = true;
            this.checkBox_Compress.Checked = true;
            this.checkBox_Compress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Compress.Location = new System.Drawing.Point(69, 18);
            this.checkBox_Compress.Name = "checkBox_Compress";
            this.checkBox_Compress.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Compress.TabIndex = 9;
            this.checkBox_Compress.Text = "压缩";
            this.checkBox_Compress.UseVisualStyleBackColor = true;
            // 
            // checkBox_Confuse
            // 
            this.checkBox_Confuse.AutoSize = true;
            this.checkBox_Confuse.Checked = true;
            this.checkBox_Confuse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Confuse.Location = new System.Drawing.Point(6, 18);
            this.checkBox_Confuse.Name = "checkBox_Confuse";
            this.checkBox_Confuse.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Confuse.TabIndex = 8;
            this.checkBox_Confuse.Text = "混淆";
            this.checkBox_Confuse.UseVisualStyleBackColor = true;
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(432, 4);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(52, 33);
            this.button_Close.TabIndex = 7;
            this.button_Close.Text = "关闭";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_excute
            // 
            this.button_excute.Location = new System.Drawing.Point(374, 4);
            this.button_excute.Name = "button_excute";
            this.button_excute.Size = new System.Drawing.Size(52, 33);
            this.button_excute.TabIndex = 6;
            this.button_excute.Text = "压缩";
            this.button_excute.UseVisualStyleBackColor = true;
            this.button_excute.Click += new System.EventHandler(this.button_excute_Click);
            // 
            // panel_Center
            // 
            this.panel_Center.Controls.Add(this.richTextBox);
            this.panel_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Center.Location = new System.Drawing.Point(0, 100);
            this.panel_Center.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Center.Name = "panel_Center";
            this.panel_Center.Size = new System.Drawing.Size(497, 189);
            this.panel_Center.TabIndex = 1;
            // 
            // richTextBox
            // 
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(497, 189);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // panel_Top
            // 
            this.panel_Top.Controls.Add(this.label2);
            this.panel_Top.Controls.Add(this.textBox_PathDest);
            this.panel_Top.Controls.Add(this.button_openDest);
            this.panel_Top.Controls.Add(this.label1);
            this.panel_Top.Controls.Add(this.textBox_PathSrc);
            this.panel_Top.Controls.Add(this.button_openSrc);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(497, 100);
            this.panel_Top.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "目标目录：";
            // 
            // textBox_PathDest
            // 
            this.textBox_PathDest.Location = new System.Drawing.Point(69, 56);
            this.textBox_PathDest.Name = "textBox_PathDest";
            this.textBox_PathDest.Size = new System.Drawing.Size(357, 21);
            this.textBox_PathDest.TabIndex = 4;
            // 
            // button_openDest
            // 
            this.button_openDest.Location = new System.Drawing.Point(432, 50);
            this.button_openDest.Name = "button_openDest";
            this.button_openDest.Size = new System.Drawing.Size(52, 33);
            this.button_openDest.TabIndex = 3;
            this.button_openDest.Text = "打开";
            this.button_openDest.UseVisualStyleBackColor = true;
            this.button_openDest.Click += new System.EventHandler(this.button_openDest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "源图目录：";
            // 
            // textBox_PathSrc
            // 
            this.textBox_PathSrc.Location = new System.Drawing.Point(69, 20);
            this.textBox_PathSrc.Name = "textBox_PathSrc";
            this.textBox_PathSrc.Size = new System.Drawing.Size(357, 21);
            this.textBox_PathSrc.TabIndex = 1;
            // 
            // button_openSrc
            // 
            this.button_openSrc.Location = new System.Drawing.Point(432, 14);
            this.button_openSrc.Name = "button_openSrc";
            this.button_openSrc.Size = new System.Drawing.Size(52, 33);
            this.button_openSrc.TabIndex = 0;
            this.button_openSrc.Text = "打开";
            this.button_openSrc.UseVisualStyleBackColor = true;
            this.button_openSrc.Click += new System.EventHandler(this.button_openSrc_Click);
            // 
            // Form_Image_Compress
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(505, 338);
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(389, 288);
            this.Name = "Form_Image_Compress";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "图片压缩工具";
            this.tableLayoutPanel_all.ResumeLayout(false);
            this.panel_Bottom.ResumeLayout(false);
            this.panel_Bottom.PerformLayout();
            this.panel_Center.ResumeLayout(false);
            this.panel_Top.ResumeLayout(false);
            this.panel_Top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog_clipEditorBg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_all;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Panel panel_Center;
        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Button button_openSrc;
        private System.Windows.Forms.TextBox textBox_PathSrc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_PathDest;
        private System.Windows.Forms.Button button_openDest;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Button button_excute;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.CheckBox checkBox_Confuse;
        private System.Windows.Forms.CheckBox checkBox_Compress;
    }
}