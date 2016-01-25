namespace Cyclone.mod.map
{
    partial class SmallDialog_NewTile_Physics
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
            this.panel_down = new System.Windows.Forms.Panel();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancle = new System.Windows.Forms.Button();
            this.panel_config = new System.Windows.Forms.Panel();
            this.panel_MapColor = new System.Windows.Forms.Panel();
            this.label_ColorInf = new System.Windows.Forms.Label();
            this.label_flagInf = new System.Windows.Forms.Label();
            this.numericUpDown_FlagInf = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.panel_down.SuspendLayout();
            this.panel_config.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FlagInf)).BeginInit();
            this.tableLayoutPanel_all.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorDialog_clipEditorBg
            // 
            this.colorDialog_clipEditorBg.FullOpen = true;
            // 
            // panel_down
            // 
            this.panel_down.Controls.Add(this.button_OK);
            this.panel_down.Controls.Add(this.button_Cancle);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_down.Location = new System.Drawing.Point(3, 108);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(270, 34);
            this.panel_down.TabIndex = 0;
            // 
            // button_OK
            // 
            this.button_OK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_OK.BackColor = System.Drawing.Color.Transparent;
            this.button_OK.ForeColor = System.Drawing.Color.Black;
            this.button_OK.Location = new System.Drawing.Point(55, 4);
            this.button_OK.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(54, 27);
            this.button_OK.TabIndex = 9;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancle
            // 
            this.button_Cancle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_Cancle.BackColor = System.Drawing.Color.Transparent;
            this.button_Cancle.ForeColor = System.Drawing.Color.Black;
            this.button_Cancle.Location = new System.Drawing.Point(162, 4);
            this.button_Cancle.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_Cancle.Name = "button_Cancle";
            this.button_Cancle.Size = new System.Drawing.Size(54, 27);
            this.button_Cancle.TabIndex = 8;
            this.button_Cancle.Text = "取消";
            this.button_Cancle.UseVisualStyleBackColor = false;
            this.button_Cancle.Click += new System.EventHandler(this.button_Cancle_Click);
            // 
            // panel_config
            // 
            this.panel_config.Controls.Add(this.panel_MapColor);
            this.panel_config.Controls.Add(this.label_ColorInf);
            this.panel_config.Controls.Add(this.label_flagInf);
            this.panel_config.Controls.Add(this.numericUpDown_FlagInf);
            this.panel_config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_config.Location = new System.Drawing.Point(3, 3);
            this.panel_config.Name = "panel_config";
            this.panel_config.Size = new System.Drawing.Size(270, 99);
            this.panel_config.TabIndex = 2;
            // 
            // panel_MapColor
            // 
            this.panel_MapColor.BackColor = System.Drawing.Color.Black;
            this.panel_MapColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_MapColor.Location = new System.Drawing.Point(137, 50);
            this.panel_MapColor.Name = "panel_MapColor";
            this.panel_MapColor.Size = new System.Drawing.Size(64, 25);
            this.panel_MapColor.TabIndex = 7;
            this.panel_MapColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MapColor_MouseDown);
            // 
            // label_ColorInf
            // 
            this.label_ColorInf.AutoSize = true;
            this.label_ColorInf.Location = new System.Drawing.Point(70, 58);
            this.label_ColorInf.Name = "label_ColorInf";
            this.label_ColorInf.Size = new System.Drawing.Size(65, 12);
            this.label_ColorInf.TabIndex = 6;
            this.label_ColorInf.Text = "标记颜色：";
            // 
            // label_flagInf
            // 
            this.label_flagInf.AutoSize = true;
            this.label_flagInf.Location = new System.Drawing.Point(70, 27);
            this.label_flagInf.Name = "label_flagInf";
            this.label_flagInf.Size = new System.Drawing.Size(65, 12);
            this.label_flagInf.TabIndex = 1;
            this.label_flagInf.Text = "标记编号：";
            // 
            // numericUpDown_FlagInf
            // 
            this.numericUpDown_FlagInf.Location = new System.Drawing.Point(137, 23);
            this.numericUpDown_FlagInf.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_FlagInf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_FlagInf.Name = "numericUpDown_FlagInf";
            this.numericUpDown_FlagInf.Size = new System.Drawing.Size(64, 21);
            this.numericUpDown_FlagInf.TabIndex = 0;
            this.numericUpDown_FlagInf.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tableLayoutPanel_all
            // 
            this.tableLayoutPanel_all.ColumnCount = 1;
            this.tableLayoutPanel_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.Controls.Add(this.panel_down, 0, 1);
            this.tableLayoutPanel_all.Controls.Add(this.panel_config, 0, 0);
            this.tableLayoutPanel_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_all.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel_all.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_all.Name = "tableLayoutPanel_all";
            this.tableLayoutPanel_all.RowCount = 2;
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(276, 145);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // SmallDialog_NewTile_Physics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(284, 153);
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmallDialog_NewTile_Physics";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建地图物理单元";
            this.panel_down.ResumeLayout(false);
            this.panel_config.ResumeLayout(false);
            this.panel_config.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FlagInf)).EndInit();
            this.tableLayoutPanel_all.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog_clipEditorBg;
        private System.Windows.Forms.Panel panel_down;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancle;
        private System.Windows.Forms.Panel panel_config;
        private System.Windows.Forms.Panel panel_MapColor;
        private System.Windows.Forms.Label label_ColorInf;
        private System.Windows.Forms.Label label_flagInf;
        private System.Windows.Forms.NumericUpDown numericUpDown_FlagInf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_all;
    }
}