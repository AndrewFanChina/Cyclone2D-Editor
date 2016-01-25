using OpenTK.Graphics;
namespace Cyclone.mod.anim
{
    partial class Form_MAnimPlay
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MAnimPlay));
            this.tableLayoutPanel_PreviewBox = new System.Windows.Forms.TableLayoutPanel();
            this.panel_PrevieWBox = new System.Windows.Forms.Panel();
            this.glView = new OpenTK.GLControl();
            this.tableLayoutPanel_config = new System.Windows.Forms.TableLayoutPanel();
            this.panel_PreviewBoxTools = new System.Windows.Forms.Panel();
            this.label_playMode = new System.Windows.Forms.Label();
            this.comboBox_PlayMode = new System.Windows.Forms.ComboBox();
            this.label_fps = new System.Windows.Forms.Label();
            this.button_zoomOut = new System.Windows.Forms.Button();
            this.button_ZoomIn = new System.Windows.Forms.Button();
            this.button_Play = new System.Windows.Forms.Button();
            this.trackBar_PreviewBox = new System.Windows.Forms.TrackBar();
            this.panel_Player = new System.Windows.Forms.Panel();
            this.timer_play = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel_PreviewBox.SuspendLayout();
            this.panel_PrevieWBox.SuspendLayout();
            this.tableLayoutPanel_config.SuspendLayout();
            this.panel_PreviewBoxTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_PreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_PreviewBox
            // 
            this.tableLayoutPanel_PreviewBox.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel_PreviewBox.ColumnCount = 1;
            this.tableLayoutPanel_PreviewBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_PreviewBox.Controls.Add(this.panel_PrevieWBox, 0, 0);
            this.tableLayoutPanel_PreviewBox.Controls.Add(this.tableLayoutPanel_config, 0, 1);
            this.tableLayoutPanel_PreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_PreviewBox.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_PreviewBox.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel_PreviewBox.Name = "tableLayoutPanel_PreviewBox";
            this.tableLayoutPanel_PreviewBox.RowCount = 2;
            this.tableLayoutPanel_PreviewBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_PreviewBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel_PreviewBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_PreviewBox.Size = new System.Drawing.Size(592, 466);
            this.tableLayoutPanel_PreviewBox.TabIndex = 3;
            // 
            // panel_PrevieWBox
            // 
            this.panel_PrevieWBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_PrevieWBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.panel_PrevieWBox.Controls.Add(this.glView);
            this.panel_PrevieWBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_PrevieWBox.Location = new System.Drawing.Point(4, 4);
            this.panel_PrevieWBox.Margin = new System.Windows.Forms.Padding(2);
            this.panel_PrevieWBox.Name = "panel_PrevieWBox";
            this.panel_PrevieWBox.Size = new System.Drawing.Size(584, 407);
            this.panel_PrevieWBox.TabIndex = 0;
            // 
            // glView
            // 
            this.glView.BackColor = System.Drawing.Color.Black;
            this.glView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glView.Location = new System.Drawing.Point(0, 0);
            this.glView.Name = "glView";
            this.glView.Size = new System.Drawing.Size(584, 407);
            this.glView.TabIndex = 1;
            this.glView.VSync = false;
            this.glView.MouseLeave += new System.EventHandler(this.glView_MouseLeave);
            this.glView.Paint += new System.Windows.Forms.PaintEventHandler(this.glView_Paint);
            this.glView.Resize += new System.EventHandler(this.glView_Resize);
            this.glView.MouseEnter += new System.EventHandler(this.glView_MouseEnter);
            // 
            // tableLayoutPanel_config
            // 
            this.tableLayoutPanel_config.ColumnCount = 2;
            this.tableLayoutPanel_config.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 483F));
            this.tableLayoutPanel_config.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_config.Controls.Add(this.panel_PreviewBoxTools, 0, 0);
            this.tableLayoutPanel_config.Controls.Add(this.panel_Player, 1, 0);
            this.tableLayoutPanel_config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_config.Location = new System.Drawing.Point(5, 418);
            this.tableLayoutPanel_config.Name = "tableLayoutPanel_config";
            this.tableLayoutPanel_config.RowCount = 1;
            this.tableLayoutPanel_config.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_config.Size = new System.Drawing.Size(582, 43);
            this.tableLayoutPanel_config.TabIndex = 2;
            // 
            // panel_PreviewBoxTools
            // 
            this.panel_PreviewBoxTools.Controls.Add(this.label_playMode);
            this.panel_PreviewBoxTools.Controls.Add(this.comboBox_PlayMode);
            this.panel_PreviewBoxTools.Controls.Add(this.label_fps);
            this.panel_PreviewBoxTools.Controls.Add(this.button_zoomOut);
            this.panel_PreviewBoxTools.Controls.Add(this.button_ZoomIn);
            this.panel_PreviewBoxTools.Controls.Add(this.button_Play);
            this.panel_PreviewBoxTools.Controls.Add(this.trackBar_PreviewBox);
            this.panel_PreviewBoxTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_PreviewBoxTools.Location = new System.Drawing.Point(0, 0);
            this.panel_PreviewBoxTools.Margin = new System.Windows.Forms.Padding(0);
            this.panel_PreviewBoxTools.Name = "panel_PreviewBoxTools";
            this.panel_PreviewBoxTools.Size = new System.Drawing.Size(483, 43);
            this.panel_PreviewBoxTools.TabIndex = 1;
            // 
            // label_playMode
            // 
            this.label_playMode.AutoSize = true;
            this.label_playMode.Location = new System.Drawing.Point(187, 3);
            this.label_playMode.Name = "label_playMode";
            this.label_playMode.Size = new System.Drawing.Size(65, 12);
            this.label_playMode.TabIndex = 1;
            this.label_playMode.Text = "播放模式：";
            // 
            // comboBox_PlayMode
            // 
            this.comboBox_PlayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PlayMode.FormattingEnabled = true;
            this.comboBox_PlayMode.Items.AddRange(new object[] {
            "保持循环",
            "鼠标移入"});
            this.comboBox_PlayMode.Location = new System.Drawing.Point(187, 21);
            this.comboBox_PlayMode.Name = "comboBox_PlayMode";
            this.comboBox_PlayMode.Size = new System.Drawing.Size(75, 20);
            this.comboBox_PlayMode.TabIndex = 0;
            // 
            // label_fps
            // 
            this.label_fps.AutoSize = true;
            this.label_fps.Location = new System.Drawing.Point(127, 2);
            this.label_fps.Name = "label_fps";
            this.label_fps.Size = new System.Drawing.Size(41, 12);
            this.label_fps.TabIndex = 9;
            this.label_fps.Text = "FPS:38";
            // 
            // button_zoomOut
            // 
            this.button_zoomOut.BackgroundImage = global::Cyclone.Properties.Resources.zoom_out_PV;
            this.button_zoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_zoomOut.FlatAppearance.BorderSize = 0;
            this.button_zoomOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Tomato;
            this.button_zoomOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.button_zoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_zoomOut.Location = new System.Drawing.Point(77, 5);
            this.button_zoomOut.Name = "button_zoomOut";
            this.button_zoomOut.Size = new System.Drawing.Size(32, 32);
            this.button_zoomOut.TabIndex = 8;
            this.button_zoomOut.UseVisualStyleBackColor = true;
            this.button_zoomOut.Click += new System.EventHandler(this.button_ZoomOut_Click);
            // 
            // button_ZoomIn
            // 
            this.button_ZoomIn.BackgroundImage = global::Cyclone.Properties.Resources.zoom_in_PV;
            this.button_ZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_ZoomIn.FlatAppearance.BorderSize = 0;
            this.button_ZoomIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Tomato;
            this.button_ZoomIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.button_ZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ZoomIn.Location = new System.Drawing.Point(41, 5);
            this.button_ZoomIn.Name = "button_ZoomIn";
            this.button_ZoomIn.Size = new System.Drawing.Size(32, 32);
            this.button_ZoomIn.TabIndex = 7;
            this.button_ZoomIn.UseVisualStyleBackColor = true;
            this.button_ZoomIn.Click += new System.EventHandler(this.button_ZoomIn_Click);
            // 
            // button_Play
            // 
            this.button_Play.BackColor = System.Drawing.Color.Transparent;
            this.button_Play.BackgroundImage = global::Cyclone.Properties.Resources.pause;
            this.button_Play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_Play.FlatAppearance.BorderSize = 0;
            this.button_Play.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Tomato;
            this.button_Play.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.button_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Play.ForeColor = System.Drawing.Color.Transparent;
            this.button_Play.Location = new System.Drawing.Point(5, 5);
            this.button_Play.Name = "button_Play";
            this.button_Play.Size = new System.Drawing.Size(32, 32);
            this.button_Play.TabIndex = 0;
            this.button_Play.UseVisualStyleBackColor = false;
            this.button_Play.Click += new System.EventHandler(this.button_Play_PreviewBox_Click);
            // 
            // trackBar_PreviewBox
            // 
            this.trackBar_PreviewBox.AutoSize = false;
            this.trackBar_PreviewBox.Location = new System.Drawing.Point(105, 12);
            this.trackBar_PreviewBox.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar_PreviewBox.Maximum = 60;
            this.trackBar_PreviewBox.Minimum = 16;
            this.trackBar_PreviewBox.Name = "trackBar_PreviewBox";
            this.trackBar_PreviewBox.Size = new System.Drawing.Size(89, 29);
            this.trackBar_PreviewBox.TabIndex = 2;
            this.trackBar_PreviewBox.TickFrequency = 4;
            this.trackBar_PreviewBox.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_PreviewBox.Value = 38;
            this.trackBar_PreviewBox.ValueChanged += new System.EventHandler(this.trackBar_PreviewBox_ValueChanged);
            // 
            // panel_Player
            // 
            this.panel_Player.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Player.Location = new System.Drawing.Point(483, 0);
            this.panel_Player.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Player.Name = "panel_Player";
            this.panel_Player.Size = new System.Drawing.Size(99, 43);
            this.panel_Player.TabIndex = 2;
            // 
            // timer_play
            // 
            this.timer_play.Interval = 26;
            this.timer_play.Tick += new System.EventHandler(this.timer_play_Tick);
            // 
            // Form_MAnimPlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.tableLayoutPanel_PreviewBox);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_MAnimPlay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "动画演示";
            this.Load += new System.EventHandler(this.Form_GLViewParent_Load);
            this.RegionChanged += new System.EventHandler(this.Form_GLViewParent_RegionChanged);
            this.ParentChanged += new System.EventHandler(this.Form_GLViewParent_ParentChanged);
            this.Shown += new System.EventHandler(this.Form_MAnimPW_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_DisplayAnimation_FormClosed);
            this.tableLayoutPanel_PreviewBox.ResumeLayout(false);
            this.panel_PrevieWBox.ResumeLayout(false);
            this.tableLayoutPanel_config.ResumeLayout(false);
            this.panel_PreviewBoxTools.ResumeLayout(false);
            this.panel_PreviewBoxTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_PreviewBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_PreviewBox;
        private System.Windows.Forms.Panel panel_PreviewBoxTools;
        private System.Windows.Forms.Button button_Play;
        private System.Windows.Forms.TrackBar trackBar_PreviewBox;
        private System.Windows.Forms.Button button_zoomOut;
        private System.Windows.Forms.Button button_ZoomIn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_config;
        private System.Windows.Forms.Panel panel_Player;
        private System.Windows.Forms.Label label_fps;
        private System.Windows.Forms.Label label_playMode;
        private System.Windows.Forms.ComboBox comboBox_PlayMode;
        private System.Windows.Forms.Panel panel_PrevieWBox;
        private OpenTK.GLControl glView;
        private System.Windows.Forms.Timer timer_play;
    }
}