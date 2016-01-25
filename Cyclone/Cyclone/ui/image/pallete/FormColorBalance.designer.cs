namespace Cyclone.mod.image.pallete
{
    partial class FormColorBalance
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBar_2 = new System.Windows.Forms.TrackBar();
            this.trackBar_3 = new System.Windows.Forms.TrackBar();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelTextR = new System.Windows.Forms.Label();
            this.labelTextG = new System.Windows.Forms.Label();
            this.labelTextB = new System.Windows.Forms.Label();
            this.labelR = new System.Windows.Forms.Label();
            this.labelG = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_reset = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.checkBox_maintainBright = new System.Windows.Forms.CheckBox();
            this.radioButton_Light = new System.Windows.Forms.RadioButton();
            this.radioButton_middle = new System.Windows.Forms.RadioButton();
            this.radioButton_dark = new System.Windows.Forms.RadioButton();
            this.trackBar_1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_3)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar_2
            // 
            this.trackBar_2.LargeChange = 10;
            this.trackBar_2.Location = new System.Drawing.Point(5, 78);
            this.trackBar_2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar_2.Maximum = 100;
            this.trackBar_2.Minimum = -100;
            this.trackBar_2.Name = "trackBar_2";
            this.trackBar_2.Size = new System.Drawing.Size(353, 56);
            this.trackBar_2.TabIndex = 1;
            this.trackBar_2.TickFrequency = 10;
            this.trackBar_2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_2.ValueChanged += new System.EventHandler(this.trackBarG_ValueChanged);
            // 
            // trackBar_3
            // 
            this.trackBar_3.LargeChange = 10;
            this.trackBar_3.Location = new System.Drawing.Point(5, 138);
            this.trackBar_3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar_3.Maximum = 100;
            this.trackBar_3.Minimum = -100;
            this.trackBar_3.Name = "trackBar_3";
            this.trackBar_3.Size = new System.Drawing.Size(353, 56);
            this.trackBar_3.TabIndex = 2;
            this.trackBar_3.TickFrequency = 10;
            this.trackBar_3.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_3.ValueChanged += new System.EventHandler(this.trackBarB_ValueChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(476, 122);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(80, 32);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelTextR
            // 
            this.labelTextR.AutoSize = true;
            this.labelTextR.Location = new System.Drawing.Point(17, 10);
            this.labelTextR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTextR.Name = "labelTextR";
            this.labelTextR.Size = new System.Drawing.Size(37, 15);
            this.labelTextR.TabIndex = 5;
            this.labelTextR.Text = "青色";
            // 
            // labelTextG
            // 
            this.labelTextG.AutoSize = true;
            this.labelTextG.Location = new System.Drawing.Point(17, 67);
            this.labelTextG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTextG.Name = "labelTextG";
            this.labelTextG.Size = new System.Drawing.Size(37, 15);
            this.labelTextG.TabIndex = 6;
            this.labelTextG.Text = "洋红";
            // 
            // labelTextB
            // 
            this.labelTextB.AutoSize = true;
            this.labelTextB.Location = new System.Drawing.Point(17, 128);
            this.labelTextB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTextB.Name = "labelTextB";
            this.labelTextB.Size = new System.Drawing.Size(37, 15);
            this.labelTextB.TabIndex = 7;
            this.labelTextB.Text = "黄色";
            // 
            // labelR
            // 
            this.labelR.AutoSize = true;
            this.labelR.Location = new System.Drawing.Point(366, 10);
            this.labelR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelR.Name = "labelR";
            this.labelR.Size = new System.Drawing.Size(15, 15);
            this.labelR.TabIndex = 8;
            this.labelR.Text = "0";
            // 
            // labelG
            // 
            this.labelG.AutoSize = true;
            this.labelG.Location = new System.Drawing.Point(366, 78);
            this.labelG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelG.Name = "labelG";
            this.labelG.Size = new System.Drawing.Size(15, 15);
            this.labelG.TabIndex = 9;
            this.labelG.Text = "0";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(366, 138);
            this.labelB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(15, 15);
            this.labelB.TabIndex = 10;
            this.labelB.Text = "0";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelTextR);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelTextG);
            this.panel1.Controls.Add(this.labelTextB);
            this.panel1.Controls.Add(this.labelB);
            this.panel1.Controls.Add(this.labelG);
            this.panel1.Controls.Add(this.trackBar_2);
            this.panel1.Controls.Add(this.labelR);
            this.panel1.Controls.Add(this.trackBar_1);
            this.panel1.Controls.Add(this.trackBar_3);
            this.panel1.Location = new System.Drawing.Point(63, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 213);
            this.panel1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "蓝色";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "绿色";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(311, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "红色";
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(476, 171);
            this.button_reset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(80, 32);
            this.button_reset.TabIndex = 12;
            this.button_reset.Text = "复位";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.checkBox_maintainBright);
            this.panel10.Controls.Add(this.radioButton_Light);
            this.panel10.Controls.Add(this.radioButton_middle);
            this.panel10.Controls.Add(this.radioButton_dark);
            this.panel10.Location = new System.Drawing.Point(63, 253);
            this.panel10.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(405, 75);
            this.panel10.TabIndex = 7;
            // 
            // checkBox_maintainBright
            // 
            this.checkBox_maintainBright.AutoSize = true;
            this.checkBox_maintainBright.Checked = true;
            this.checkBox_maintainBright.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_maintainBright.Location = new System.Drawing.Point(31, 40);
            this.checkBox_maintainBright.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox_maintainBright.Name = "checkBox_maintainBright";
            this.checkBox_maintainBright.Size = new System.Drawing.Size(89, 19);
            this.checkBox_maintainBright.TabIndex = 3;
            this.checkBox_maintainBright.Text = "保持亮度";
            this.checkBox_maintainBright.UseVisualStyleBackColor = true;
            this.checkBox_maintainBright.CheckedChanged += new System.EventHandler(this.checkBox_maintainBright_CheckedChanged);
            // 
            // radioButton_Light
            // 
            this.radioButton_Light.AutoSize = true;
            this.radioButton_Light.Location = new System.Drawing.Point(315, 15);
            this.radioButton_Light.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton_Light.Name = "radioButton_Light";
            this.radioButton_Light.Size = new System.Drawing.Size(58, 19);
            this.radioButton_Light.TabIndex = 2;
            this.radioButton_Light.Text = "高光";
            this.radioButton_Light.UseVisualStyleBackColor = true;
            this.radioButton_Light.CheckedChanged += new System.EventHandler(this.radioButton_Light_CheckedChanged);
            // 
            // radioButton_middle
            // 
            this.radioButton_middle.AutoSize = true;
            this.radioButton_middle.Checked = true;
            this.radioButton_middle.Location = new System.Drawing.Point(164, 15);
            this.radioButton_middle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton_middle.Name = "radioButton_middle";
            this.radioButton_middle.Size = new System.Drawing.Size(73, 19);
            this.radioButton_middle.TabIndex = 1;
            this.radioButton_middle.TabStop = true;
            this.radioButton_middle.Text = "中间调";
            this.radioButton_middle.UseVisualStyleBackColor = true;
            this.radioButton_middle.CheckedChanged += new System.EventHandler(this.radioButton_middle_CheckedChanged);
            // 
            // radioButton_dark
            // 
            this.radioButton_dark.AutoSize = true;
            this.radioButton_dark.Location = new System.Drawing.Point(29, 15);
            this.radioButton_dark.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton_dark.Name = "radioButton_dark";
            this.radioButton_dark.Size = new System.Drawing.Size(58, 19);
            this.radioButton_dark.TabIndex = 0;
            this.radioButton_dark.Text = "暗调";
            this.radioButton_dark.UseVisualStyleBackColor = true;
            this.radioButton_dark.CheckedChanged += new System.EventHandler(this.radioButton_dark_CheckedChanged);
            // 
            // trackBar_1
            // 
            this.trackBar_1.LargeChange = 10;
            this.trackBar_1.Location = new System.Drawing.Point(5, 20);
            this.trackBar_1.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar_1.Maximum = 100;
            this.trackBar_1.Minimum = -100;
            this.trackBar_1.Name = "trackBar_1";
            this.trackBar_1.Size = new System.Drawing.Size(353, 56);
            this.trackBar_1.TabIndex = 0;
            this.trackBar_1.TickFrequency = 10;
            this.trackBar_1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_1.ValueChanged += new System.EventHandler(this.trackBarR_ValueChanged);
            // 
            // FormColorBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 365);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.button_reset);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormColorBalance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "色彩平衡";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRate_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar_2;
        private System.Windows.Forms.TrackBar trackBar_3;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelTextR;
        private System.Windows.Forms.Label labelTextG;
        private System.Windows.Forms.Label labelTextB;
        private System.Windows.Forms.Label labelR;
        private System.Windows.Forms.Label labelG;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.RadioButton radioButton_Light;
        private System.Windows.Forms.RadioButton radioButton_middle;
        private System.Windows.Forms.RadioButton radioButton_dark;
        private System.Windows.Forms.CheckBox checkBox_maintainBright;
        private System.Windows.Forms.TrackBar trackBar_1;
    }
}