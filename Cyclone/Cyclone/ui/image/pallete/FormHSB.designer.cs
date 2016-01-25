namespace Cyclone.mod.image.pallete
{
    partial class FormHSB
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
            this.trackBar_Hue = new System.Windows.Forms.TrackBar();
            this.trackBar_Saturation = new System.Windows.Forms.TrackBar();
            this.trackBar_Brightness = new System.Windows.Forms.TrackBar();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelTextR = new System.Windows.Forms.Label();
            this.labelTextG = new System.Windows.Forms.Label();
            this.labelTextB = new System.Windows.Forms.Label();
            this.labelR = new System.Windows.Forms.Label();
            this.labelG = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_reset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Hue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Saturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Brightness)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar_Hue
            // 
            this.trackBar_Hue.LargeChange = 10;
            this.trackBar_Hue.Location = new System.Drawing.Point(5, 14);
            this.trackBar_Hue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar_Hue.Maximum = 180;
            this.trackBar_Hue.Minimum = -180;
            this.trackBar_Hue.Name = "trackBar_Hue";
            this.trackBar_Hue.Size = new System.Drawing.Size(341, 56);
            this.trackBar_Hue.TabIndex = 0;
            this.trackBar_Hue.TickFrequency = 10;
            this.trackBar_Hue.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_Hue.ValueChanged += new System.EventHandler(this.trackBarR_ValueChanged);
            // 
            // trackBar_Saturation
            // 
            this.trackBar_Saturation.LargeChange = 10;
            this.trackBar_Saturation.Location = new System.Drawing.Point(5, 72);
            this.trackBar_Saturation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar_Saturation.Maximum = 100;
            this.trackBar_Saturation.Minimum = -100;
            this.trackBar_Saturation.Name = "trackBar_Saturation";
            this.trackBar_Saturation.Size = new System.Drawing.Size(341, 56);
            this.trackBar_Saturation.TabIndex = 1;
            this.trackBar_Saturation.TickFrequency = 10;
            this.trackBar_Saturation.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_Saturation.ValueChanged += new System.EventHandler(this.trackBarG_ValueChanged);
            // 
            // trackBar_Brightness
            // 
            this.trackBar_Brightness.LargeChange = 10;
            this.trackBar_Brightness.Location = new System.Drawing.Point(5, 132);
            this.trackBar_Brightness.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar_Brightness.Maximum = 100;
            this.trackBar_Brightness.Minimum = -100;
            this.trackBar_Brightness.Name = "trackBar_Brightness";
            this.trackBar_Brightness.Size = new System.Drawing.Size(341, 56);
            this.trackBar_Brightness.TabIndex = 2;
            this.trackBar_Brightness.TickFrequency = 10;
            this.trackBar_Brightness.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_Brightness.ValueChanged += new System.EventHandler(this.trackBarB_ValueChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(258, 254);
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
            this.labelTextR.Location = new System.Drawing.Point(17, 4);
            this.labelTextR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTextR.Name = "labelTextR";
            this.labelTextR.Size = new System.Drawing.Size(37, 15);
            this.labelTextR.TabIndex = 5;
            this.labelTextR.Text = "色相";
            // 
            // labelTextG
            // 
            this.labelTextG.AutoSize = true;
            this.labelTextG.Location = new System.Drawing.Point(17, 61);
            this.labelTextG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTextG.Name = "labelTextG";
            this.labelTextG.Size = new System.Drawing.Size(52, 15);
            this.labelTextG.TabIndex = 6;
            this.labelTextG.Text = "饱和度";
            // 
            // labelTextB
            // 
            this.labelTextB.AutoSize = true;
            this.labelTextB.Location = new System.Drawing.Point(17, 122);
            this.labelTextB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTextB.Name = "labelTextB";
            this.labelTextB.Size = new System.Drawing.Size(37, 15);
            this.labelTextB.TabIndex = 7;
            this.labelTextB.Text = "明度";
            // 
            // labelR
            // 
            this.labelR.AutoSize = true;
            this.labelR.Location = new System.Drawing.Point(364, 30);
            this.labelR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelR.Name = "labelR";
            this.labelR.Size = new System.Drawing.Size(15, 15);
            this.labelR.TabIndex = 8;
            this.labelR.Text = "0";
            // 
            // labelG
            // 
            this.labelG.AutoSize = true;
            this.labelG.Location = new System.Drawing.Point(364, 91);
            this.labelG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelG.Name = "labelG";
            this.labelG.Size = new System.Drawing.Size(27, 15);
            this.labelG.TabIndex = 9;
            this.labelG.Text = "0%";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(364, 151);
            this.labelB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(27, 15);
            this.labelB.TabIndex = 10;
            this.labelB.Text = "0%";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelTextR);
            this.panel1.Controls.Add(this.labelTextG);
            this.panel1.Controls.Add(this.labelTextB);
            this.panel1.Controls.Add(this.labelB);
            this.panel1.Controls.Add(this.labelG);
            this.panel1.Controls.Add(this.trackBar_Saturation);
            this.panel1.Controls.Add(this.labelR);
            this.panel1.Controls.Add(this.trackBar_Hue);
            this.panel1.Controls.Add(this.trackBar_Brightness);
            this.panel1.Location = new System.Drawing.Point(32, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 194);
            this.panel1.TabIndex = 7;
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(131, 254);
            this.button_reset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(80, 32);
            this.button_reset.TabIndex = 12;
            this.button_reset.Text = "复位";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // FormHSB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 318);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_reset);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHSB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "色相/饱和度";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRate_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Hue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Saturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Brightness)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar_Saturation;
        private System.Windows.Forms.TrackBar trackBar_Brightness;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelTextR;
        private System.Windows.Forms.Label labelTextG;
        private System.Windows.Forms.Label labelTextB;
        private System.Windows.Forms.Label labelR;
        private System.Windows.Forms.Label labelG;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar trackBar_Hue;
        private System.Windows.Forms.Button button_reset;
    }
}