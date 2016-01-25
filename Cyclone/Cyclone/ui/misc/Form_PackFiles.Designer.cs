namespace Cyclone.mod.misc
{
    partial class Form_PackFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_PackFiles));
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_excute = new System.Windows.Forms.Button();
            this.panel_Center = new System.Windows.Forms.Panel();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.panel_Top = new System.Windows.Forms.Panel();
            this.button_openRuleFile = new System.Windows.Forms.Button();
            this.textBox_ruleFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown_packSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_PathSrc = new System.Windows.Forms.TextBox();
            this.button_openSrc = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel_all.SuspendLayout();
            this.panel_Bottom.SuspendLayout();
            this.panel_Center.SuspendLayout();
            this.panel_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_packSize)).BeginInit();
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
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(484, 330);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.Controls.Add(this.button_Close);
            this.panel_Bottom.Controls.Add(this.button_excute);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 282);
            this.panel_Bottom.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(484, 48);
            this.panel_Bottom.TabIndex = 2;
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(265, 8);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(52, 33);
            this.button_Close.TabIndex = 7;
            this.button_Close.Text = "关闭";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_excute
            // 
            this.button_excute.Location = new System.Drawing.Point(167, 8);
            this.button_excute.Name = "button_excute";
            this.button_excute.Size = new System.Drawing.Size(52, 33);
            this.button_excute.TabIndex = 6;
            this.button_excute.Text = "打包";
            this.button_excute.UseVisualStyleBackColor = true;
            this.button_excute.Click += new System.EventHandler(this.button_excute_Click);
            // 
            // panel_Center
            // 
            this.panel_Center.Controls.Add(this.richTextBox);
            this.panel_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Center.Location = new System.Drawing.Point(0, 82);
            this.panel_Center.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Center.Name = "panel_Center";
            this.panel_Center.Size = new System.Drawing.Size(484, 200);
            this.panel_Center.TabIndex = 1;
            // 
            // richTextBox
            // 
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(484, 200);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // panel_Top
            // 
            this.panel_Top.Controls.Add(this.button_openRuleFile);
            this.panel_Top.Controls.Add(this.textBox_ruleFile);
            this.panel_Top.Controls.Add(this.label4);
            this.panel_Top.Controls.Add(this.numericUpDown_packSize);
            this.panel_Top.Controls.Add(this.label3);
            this.panel_Top.Controls.Add(this.label2);
            this.panel_Top.Controls.Add(this.label1);
            this.panel_Top.Controls.Add(this.textBox_PathSrc);
            this.panel_Top.Controls.Add(this.button_openSrc);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(484, 82);
            this.panel_Top.TabIndex = 0;
            // 
            // button_openRuleFile
            // 
            this.button_openRuleFile.Location = new System.Drawing.Point(427, -1);
            this.button_openRuleFile.Name = "button_openRuleFile";
            this.button_openRuleFile.Size = new System.Drawing.Size(52, 26);
            this.button_openRuleFile.TabIndex = 9;
            this.button_openRuleFile.Text = "打开";
            this.button_openRuleFile.UseVisualStyleBackColor = true;
            this.button_openRuleFile.Click += new System.EventHandler(this.button_openRuleFile_Click);
            // 
            // textBox_ruleFile
            // 
            this.textBox_ruleFile.Location = new System.Drawing.Point(69, 4);
            this.textBox_ruleFile.Name = "textBox_ruleFile";
            this.textBox_ruleFile.Size = new System.Drawing.Size(354, 21);
            this.textBox_ruleFile.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "列表文件：";
            // 
            // numericUpDown_packSize
            // 
            this.numericUpDown_packSize.Location = new System.Drawing.Point(90, 56);
            this.numericUpDown_packSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDown_packSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_packSize.Name = "numericUpDown_packSize";
            this.numericUpDown_packSize.Size = new System.Drawing.Size(80, 21);
            this.numericUpDown_packSize.TabIndex = 6;
            this.numericUpDown_packSize.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "K";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "包裹上限大小：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "资源目录：";
            // 
            // textBox_PathSrc
            // 
            this.textBox_PathSrc.Location = new System.Drawing.Point(69, 30);
            this.textBox_PathSrc.Name = "textBox_PathSrc";
            this.textBox_PathSrc.Size = new System.Drawing.Size(354, 21);
            this.textBox_PathSrc.TabIndex = 1;
            // 
            // button_openSrc
            // 
            this.button_openSrc.Location = new System.Drawing.Point(427, 27);
            this.button_openSrc.Name = "button_openSrc";
            this.button_openSrc.Size = new System.Drawing.Size(52, 26);
            this.button_openSrc.TabIndex = 0;
            this.button_openSrc.Text = "打开";
            this.button_openSrc.UseVisualStyleBackColor = true;
            this.button_openSrc.Click += new System.EventHandler(this.button_openSrc_Click);
            // 
            // Form_PackFiles
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(492, 338);
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(389, 288);
            this.Name = "Form_PackFiles";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "资源打包工具";
            this.tableLayoutPanel_all.ResumeLayout(false);
            this.panel_Bottom.ResumeLayout(false);
            this.panel_Center.ResumeLayout(false);
            this.panel_Top.ResumeLayout(false);
            this.panel_Top.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_packSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_all;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Panel panel_Center;
        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Button button_openSrc;
        private System.Windows.Forms.TextBox textBox_PathSrc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Button button_excute;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_packSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ruleFile;
        private System.Windows.Forms.Button button_openRuleFile;
    }
}