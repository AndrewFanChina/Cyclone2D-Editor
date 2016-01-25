namespace Cyclone.mod.anim
{
    partial class Form_MFrameLevel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MFrameLevel));
            this.tableLayoutPanel_FrameEdit_R = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.vScrollBar_ClipsLevel = new System.Windows.Forms.VScrollBar();
            this.panel_clipsLevel = new System.Windows.Forms.Panel();
            this.pictureBox_clipsLevel = new System.Windows.Forms.PictureBox();
            this.panel_levelTools = new System.Windows.Forms.Panel();
            this.button_changeLevelDown = new System.Windows.Forms.Button();
            this.button_changeLevelUp = new System.Windows.Forms.Button();
            this.trackBar_zoomLevel = new System.Windows.Forms.TrackBar();
            this.button_lock = new System.Windows.Forms.Button();
            this.button_visible = new System.Windows.Forms.Button();
            this.label_zoomLevel = new System.Windows.Forms.Label();
            this.toolTip_level = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel_FrameEdit_R.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.panel_clipsLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_clipsLevel)).BeginInit();
            this.panel_levelTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_zoomLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_FrameEdit_R
            // 
            this.tableLayoutPanel_FrameEdit_R.ColumnCount = 1;
            this.tableLayoutPanel_FrameEdit_R.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_FrameEdit_R.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel_FrameEdit_R.Controls.Add(this.panel_levelTools, 0, 0);
            this.tableLayoutPanel_FrameEdit_R.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_FrameEdit_R.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_FrameEdit_R.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_FrameEdit_R.Name = "tableLayoutPanel_FrameEdit_R";
            this.tableLayoutPanel_FrameEdit_R.RowCount = 2;
            this.tableLayoutPanel_FrameEdit_R.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel_FrameEdit_R.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_FrameEdit_R.Size = new System.Drawing.Size(352, 389);
            this.tableLayoutPanel_FrameEdit_R.TabIndex = 19;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel8.Controls.Add(this.vScrollBar_ClipsLevel, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.panel_clipsLevel, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 26);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(352, 363);
            this.tableLayoutPanel8.TabIndex = 32;
            // 
            // vScrollBar_ClipsLevel
            // 
            this.vScrollBar_ClipsLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar_ClipsLevel.Location = new System.Drawing.Point(335, 0);
            this.vScrollBar_ClipsLevel.Name = "vScrollBar_ClipsLevel";
            this.vScrollBar_ClipsLevel.Size = new System.Drawing.Size(17, 363);
            this.vScrollBar_ClipsLevel.TabIndex = 3;
            this.vScrollBar_ClipsLevel.ValueChanged += new System.EventHandler(this.vScrollBar_ClipsLevel_ValueChanged);
            // 
            // panel_clipsLevel
            // 
            this.panel_clipsLevel.Controls.Add(this.pictureBox_clipsLevel);
            this.panel_clipsLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_clipsLevel.Location = new System.Drawing.Point(2, 0);
            this.panel_clipsLevel.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.panel_clipsLevel.Name = "panel_clipsLevel";
            this.panel_clipsLevel.Size = new System.Drawing.Size(333, 363);
            this.panel_clipsLevel.TabIndex = 28;
            // 
            // pictureBox_clipsLevel
            // 
            this.pictureBox_clipsLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.pictureBox_clipsLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_clipsLevel.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_clipsLevel.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_clipsLevel.Name = "pictureBox_clipsLevel";
            this.pictureBox_clipsLevel.Size = new System.Drawing.Size(333, 363);
            this.pictureBox_clipsLevel.TabIndex = 4;
            this.pictureBox_clipsLevel.TabStop = false;
            this.pictureBox_clipsLevel.MouseLeave += new System.EventHandler(this.pictureBox_clipsLevel_MouseLeave);
            this.pictureBox_clipsLevel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBox_clipsLevel_PreviewKeyDown);
            this.pictureBox_clipsLevel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_clipsLevel_MouseDown);
            this.pictureBox_clipsLevel.MouseEnter += new System.EventHandler(this.pictureBox_clipsLevel_MouseEnter);
            // 
            // panel_levelTools
            // 
            this.panel_levelTools.Controls.Add(this.button_changeLevelDown);
            this.panel_levelTools.Controls.Add(this.button_changeLevelUp);
            this.panel_levelTools.Controls.Add(this.trackBar_zoomLevel);
            this.panel_levelTools.Controls.Add(this.button_lock);
            this.panel_levelTools.Controls.Add(this.button_visible);
            this.panel_levelTools.Controls.Add(this.label_zoomLevel);
            this.panel_levelTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_levelTools.Location = new System.Drawing.Point(0, 0);
            this.panel_levelTools.Margin = new System.Windows.Forms.Padding(0);
            this.panel_levelTools.Name = "panel_levelTools";
            this.panel_levelTools.Size = new System.Drawing.Size(352, 26);
            this.panel_levelTools.TabIndex = 33;
            // 
            // button_changeLevelDown
            // 
            this.button_changeLevelDown.BackgroundImage = global::Cyclone.Properties.Resources.level_changeLevelDown;
            this.button_changeLevelDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_changeLevelDown.Location = new System.Drawing.Point(73, 1);
            this.button_changeLevelDown.Name = "button_changeLevelDown";
            this.button_changeLevelDown.Size = new System.Drawing.Size(24, 24);
            this.button_changeLevelDown.TabIndex = 5;
            this.toolTip_level.SetToolTip(this.button_changeLevelDown, "向外层移动单元");
            this.button_changeLevelDown.UseVisualStyleBackColor = true;
            this.button_changeLevelDown.Click += new System.EventHandler(this.button_changeLevelDown_Click);
            // 
            // button_changeLevelUp
            // 
            this.button_changeLevelUp.BackgroundImage = global::Cyclone.Properties.Resources.level_changeLevelUp;
            this.button_changeLevelUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_changeLevelUp.Location = new System.Drawing.Point(49, 1);
            this.button_changeLevelUp.Name = "button_changeLevelUp";
            this.button_changeLevelUp.Size = new System.Drawing.Size(24, 24);
            this.button_changeLevelUp.TabIndex = 4;
            this.toolTip_level.SetToolTip(this.button_changeLevelUp, "向里层移动单元");
            this.button_changeLevelUp.UseVisualStyleBackColor = true;
            this.button_changeLevelUp.Click += new System.EventHandler(this.button_changeLevelUp_Click);
            // 
            // trackBar_zoomLevel
            // 
            this.trackBar_zoomLevel.AutoSize = false;
            this.trackBar_zoomLevel.Location = new System.Drawing.Point(164, 5);
            this.trackBar_zoomLevel.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar_zoomLevel.Maximum = 16;
            this.trackBar_zoomLevel.Minimum = 1;
            this.trackBar_zoomLevel.Name = "trackBar_zoomLevel";
            this.trackBar_zoomLevel.Size = new System.Drawing.Size(98, 16);
            this.trackBar_zoomLevel.TabIndex = 2;
            this.trackBar_zoomLevel.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip_level.SetToolTip(this.trackBar_zoomLevel, "图层面板显示比率(可在面板内使用Ctrl+上下箭头快捷控制)");
            this.trackBar_zoomLevel.Value = 4;
            this.trackBar_zoomLevel.ValueChanged += new System.EventHandler(this.trackBar_zoomLevel_ValueChanged);
            // 
            // button_lock
            // 
            this.button_lock.BackgroundImage = global::Cyclone.Properties.Resources.level_lock_unclocked;
            this.button_lock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_lock.Location = new System.Drawing.Point(25, 1);
            this.button_lock.Name = "button_lock";
            this.button_lock.Size = new System.Drawing.Size(24, 24);
            this.button_lock.TabIndex = 1;
            this.toolTip_level.SetToolTip(this.button_lock, "锁定/解锁 单元");
            this.button_lock.UseVisualStyleBackColor = true;
            this.button_lock.Click += new System.EventHandler(this.button_lock_Click);
            // 
            // button_visible
            // 
            this.button_visible.BackgroundImage = global::Cyclone.Properties.Resources.level_eye_on;
            this.button_visible.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_visible.Location = new System.Drawing.Point(1, 1);
            this.button_visible.Name = "button_visible";
            this.button_visible.Size = new System.Drawing.Size(24, 24);
            this.button_visible.TabIndex = 0;
            this.toolTip_level.SetToolTip(this.button_visible, "隐藏/显示 单元");
            this.button_visible.UseVisualStyleBackColor = true;
            this.button_visible.Click += new System.EventHandler(this.button_visible_Click);
            // 
            // label_zoomLevel
            // 
            this.label_zoomLevel.AutoSize = true;
            this.label_zoomLevel.Location = new System.Drawing.Point(101, 7);
            this.label_zoomLevel.Name = "label_zoomLevel";
            this.label_zoomLevel.Size = new System.Drawing.Size(65, 12);
            this.label_zoomLevel.TabIndex = 3;
            this.label_zoomLevel.Text = "显示：100%";
            // 
            // Form_MFrameLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 389);
            this.Controls.Add(this.tableLayoutPanel_FrameEdit_R);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_MFrameLevel";
            this.Text = "帧图层";
            this.tableLayoutPanel_FrameEdit_R.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.panel_clipsLevel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_clipsLevel)).EndInit();
            this.panel_levelTools.ResumeLayout(false);
            this.panel_levelTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_zoomLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_FrameEdit_R;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.VScrollBar vScrollBar_ClipsLevel;
        private System.Windows.Forms.Panel panel_clipsLevel;
        public System.Windows.Forms.PictureBox pictureBox_clipsLevel;
        private System.Windows.Forms.Panel panel_levelTools;
        private System.Windows.Forms.Button button_lock;
        private System.Windows.Forms.Button button_visible;
        private System.Windows.Forms.TrackBar trackBar_zoomLevel;
        private System.Windows.Forms.Label label_zoomLevel;
        private System.Windows.Forms.Button button_changeLevelUp;
        private System.Windows.Forms.Button button_changeLevelDown;
        private System.Windows.Forms.ToolTip toolTip_level;
    }
}