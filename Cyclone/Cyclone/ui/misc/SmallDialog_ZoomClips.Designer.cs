namespace Cyclone.mod.misc
{
    partial class ZoomClipsDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_1S = new System.Windows.Forms.Label();
            this.trackBar_BG = new System.Windows.Forms.TrackBar();
            this.label_1 = new System.Windows.Forms.Label();
            this.label_0S = new System.Windows.Forms.Label();
            this.trackBar_PHY = new System.Windows.Forms.TrackBar();
            this.label_0 = new System.Windows.Forms.Label();
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel_down.SuspendLayout();
            this.panel_config.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_BG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_PHY)).BeginInit();
            this.tableLayoutPanel_all.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_down
            // 
            this.panel_down.Controls.Add(this.checkBox1);
            this.panel_down.Controls.Add(this.button_OK);
            this.panel_down.Controls.Add(this.button_Cancle);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_down.Location = new System.Drawing.Point(3, 110);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(281, 34);
            this.panel_down.TabIndex = 0;
            // 
            // button_OK
            // 
            this.button_OK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_OK.BackColor = System.Drawing.Color.Transparent;
            this.button_OK.ForeColor = System.Drawing.Color.Black;
            this.button_OK.Location = new System.Drawing.Point(121, 4);
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
            this.button_Cancle.Location = new System.Drawing.Point(195, 4);
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
            this.panel_config.Controls.Add(this.panel1);
            this.panel_config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_config.Location = new System.Drawing.Point(3, 3);
            this.panel_config.Name = "panel_config";
            this.panel_config.Size = new System.Drawing.Size(281, 101);
            this.panel_config.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_1S);
            this.panel1.Controls.Add(this.trackBar_BG);
            this.panel1.Controls.Add(this.label_1);
            this.panel1.Controls.Add(this.label_0S);
            this.panel1.Controls.Add(this.trackBar_PHY);
            this.panel1.Controls.Add(this.label_0);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(281, 101);
            this.panel1.TabIndex = 7;
            // 
            // label_1S
            // 
            this.label_1S.AutoSize = true;
            this.label_1S.Location = new System.Drawing.Point(236, 55);
            this.label_1S.Name = "label_1S";
            this.label_1S.Size = new System.Drawing.Size(29, 12);
            this.label_1S.TabIndex = 6;
            this.label_1S.Text = "100%";
            // 
            // trackBar_BG
            // 
            this.trackBar_BG.AutoSize = false;
            this.trackBar_BG.Location = new System.Drawing.Point(11, 74);
            this.trackBar_BG.Maximum = 1000;
            this.trackBar_BG.Name = "trackBar_BG";
            this.trackBar_BG.Size = new System.Drawing.Size(262, 18);
            this.trackBar_BG.TabIndex = 5;
            this.trackBar_BG.TickFrequency = 100;
            this.trackBar_BG.Value = 100;
            this.trackBar_BG.ValueChanged += new System.EventHandler(this.trackBar_BG_ValueChanged);
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Location = new System.Drawing.Point(9, 55);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(77, 12);
            this.label_1.TabIndex = 4;
            this.label_1.Text = "缩放百分比：";
            // 
            // label_0S
            // 
            this.label_0S.AutoSize = true;
            this.label_0S.Location = new System.Drawing.Point(236, 9);
            this.label_0S.Name = "label_0S";
            this.label_0S.Size = new System.Drawing.Size(29, 12);
            this.label_0S.TabIndex = 3;
            this.label_0S.Text = "100%";
            // 
            // trackBar_PHY
            // 
            this.trackBar_PHY.AutoSize = false;
            this.trackBar_PHY.Location = new System.Drawing.Point(8, 27);
            this.trackBar_PHY.Maximum = 1000;
            this.trackBar_PHY.Name = "trackBar_PHY";
            this.trackBar_PHY.Size = new System.Drawing.Size(262, 18);
            this.trackBar_PHY.TabIndex = 2;
            this.trackBar_PHY.TickFrequency = 100;
            this.trackBar_PHY.Value = 100;
            this.trackBar_PHY.ValueChanged += new System.EventHandler(this.trackBar_PHY_ValueChanged);
            // 
            // label_0
            // 
            this.label_0.AutoSize = true;
            this.label_0.Location = new System.Drawing.Point(9, 9);
            this.label_0.Name = "label_0";
            this.label_0.Size = new System.Drawing.Size(77, 12);
            this.label_0.TabIndex = 1;
            this.label_0.Text = "平移百分比：";
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
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(287, 147);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(8, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "应用所有切片";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ZoomClipsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(295, 155);
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZoomClipsDialog";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "平移和缩放切片";
            this.panel_down.ResumeLayout(false);
            this.panel_down.PerformLayout();
            this.panel_config.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_BG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_PHY)).EndInit();
            this.tableLayoutPanel_all.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog_clipEditorBg;
        private System.Windows.Forms.Panel panel_down;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Panel panel_config;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_all;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.Label label_0;
        private System.Windows.Forms.Button button_Cancle;
        private System.Windows.Forms.Label label_1S;
        private System.Windows.Forms.TrackBar trackBar_BG;
        private System.Windows.Forms.Label label_0S;
        private System.Windows.Forms.TrackBar trackBar_PHY;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}