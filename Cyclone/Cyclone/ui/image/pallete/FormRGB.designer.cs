namespace Cyclone.mod.image.pallete
{
    partial class FormRGB
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.trackBarR = new System.Windows.Forms.TrackBar();
            this.trackBarG = new System.Windows.Forms.TrackBar();
            this.trackBarB = new System.Windows.Forms.TrackBar();
            this.pictureBoxColor = new System.Windows.Forms.PictureBox();
            this.labelTextR = new System.Windows.Forms.Label();
            this.labelTextG = new System.Windows.Forms.Label();
            this.labelTextB = new System.Windows.Forms.Label();
            this.labelR = new System.Windows.Forms.Label();
            this.labelG = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColor)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(109, 159);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(48, 28);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(167, 159);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(48, 28);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // trackBarR
            // 
            this.trackBarR.LargeChange = 16;
            this.trackBarR.Location = new System.Drawing.Point(12, 12);
            this.trackBarR.Maximum = 255;
            this.trackBarR.Name = "trackBarR";
            this.trackBarR.Size = new System.Drawing.Size(178, 45);
            this.trackBarR.TabIndex = 2;
            this.trackBarR.TickFrequency = 16;
            this.trackBarR.ValueChanged += new System.EventHandler(this.trackBarR_ValueChanged);
            // 
            // trackBarG
            // 
            this.trackBarG.LargeChange = 16;
            this.trackBarG.Location = new System.Drawing.Point(12, 63);
            this.trackBarG.Maximum = 255;
            this.trackBarG.Name = "trackBarG";
            this.trackBarG.Size = new System.Drawing.Size(178, 45);
            this.trackBarG.TabIndex = 3;
            this.trackBarG.TickFrequency = 16;
            this.trackBarG.ValueChanged += new System.EventHandler(this.trackBarG_ValueChanged);
            // 
            // trackBarB
            // 
            this.trackBarB.LargeChange = 16;
            this.trackBarB.Location = new System.Drawing.Point(12, 114);
            this.trackBarB.Maximum = 255;
            this.trackBarB.Name = "trackBarB";
            this.trackBarB.Size = new System.Drawing.Size(178, 45);
            this.trackBarB.TabIndex = 4;
            this.trackBarB.TickFrequency = 16;
            this.trackBarB.ValueChanged += new System.EventHandler(this.trackBarB_ValueChanged);
            // 
            // pictureBoxColor
            // 
            this.pictureBoxColor.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxColor.Location = new System.Drawing.Point(35, 160);
            this.pictureBoxColor.Name = "pictureBoxColor";
            this.pictureBoxColor.Size = new System.Drawing.Size(64, 26);
            this.pictureBoxColor.TabIndex = 5;
            this.pictureBoxColor.TabStop = false;
            this.pictureBoxColor.DoubleClick += new System.EventHandler(this.pictureBoxColor_DoubleClick);
            // 
            // labelTextR
            // 
            this.labelTextR.AutoSize = true;
            this.labelTextR.Location = new System.Drawing.Point(198, 18);
            this.labelTextR.Name = "labelTextR";
            this.labelTextR.Size = new System.Drawing.Size(17, 12);
            this.labelTextR.TabIndex = 6;
            this.labelTextR.Text = "红";
            // 
            // labelTextG
            // 
            this.labelTextG.AutoSize = true;
            this.labelTextG.Location = new System.Drawing.Point(198, 69);
            this.labelTextG.Name = "labelTextG";
            this.labelTextG.Size = new System.Drawing.Size(17, 12);
            this.labelTextG.TabIndex = 7;
            this.labelTextG.Text = "绿";
            // 
            // labelTextB
            // 
            this.labelTextB.AutoSize = true;
            this.labelTextB.Location = new System.Drawing.Point(198, 120);
            this.labelTextB.Name = "labelTextB";
            this.labelTextB.Size = new System.Drawing.Size(17, 12);
            this.labelTextB.TabIndex = 8;
            this.labelTextB.Text = "蓝";
            // 
            // labelR
            // 
            this.labelR.AutoSize = true;
            this.labelR.Location = new System.Drawing.Point(218, 18);
            this.labelR.Name = "labelR";
            this.labelR.Size = new System.Drawing.Size(11, 12);
            this.labelR.TabIndex = 9;
            this.labelR.Text = "0";
            // 
            // labelG
            // 
            this.labelG.AutoSize = true;
            this.labelG.Location = new System.Drawing.Point(218, 69);
            this.labelG.Name = "labelG";
            this.labelG.Size = new System.Drawing.Size(11, 12);
            this.labelG.TabIndex = 10;
            this.labelG.Text = "0";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(218, 120);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(11, 12);
            this.labelB.TabIndex = 11;
            this.labelB.Text = "0";
            // 
            // FormRGB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 203);
            this.ControlBox = false;
            this.Controls.Add(this.labelB);
            this.Controls.Add(this.labelG);
            this.Controls.Add(this.labelR);
            this.Controls.Add(this.labelTextB);
            this.Controls.Add(this.labelTextG);
            this.Controls.Add(this.labelTextR);
            this.Controls.Add(this.pictureBoxColor);
            this.Controls.Add(this.trackBarB);
            this.Controls.Add(this.trackBarG);
            this.Controls.Add(this.trackBarR);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormRGB";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RGB修改";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TrackBar trackBarR;
        private System.Windows.Forms.TrackBar trackBarG;
        private System.Windows.Forms.TrackBar trackBarB;
        private System.Windows.Forms.PictureBox pictureBoxColor;
        private System.Windows.Forms.Label labelTextR;
        private System.Windows.Forms.Label labelTextG;
        private System.Windows.Forms.Label labelTextB;
        private System.Windows.Forms.Label labelR;
        private System.Windows.Forms.Label labelG;
        private System.Windows.Forms.Label labelB;
    }
}