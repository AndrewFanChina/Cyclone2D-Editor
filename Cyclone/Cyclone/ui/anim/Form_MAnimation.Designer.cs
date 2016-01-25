using Cyclone.alg;
using DockingUI.WinFormsUI.Docking;
namespace Cyclone.mod.anim
{
    partial class Form_MAnimation
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
            DockingUI.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new DockingUI.WinFormsUI.Docking.DockPanelSkin();
            DockingUI.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new DockingUI.WinFormsUI.Docking.AutoHideStripSkin();
            DockingUI.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new DockingUI.WinFormsUI.Docking.DockPanelGradient();
            DockingUI.WinFormsUI.Docking.TabGradient tabGradient1 = new DockingUI.WinFormsUI.Docking.TabGradient();
            DockingUI.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new DockingUI.WinFormsUI.Docking.DockPaneStripSkin();
            DockingUI.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new DockingUI.WinFormsUI.Docking.DockPaneStripGradient();
            DockingUI.WinFormsUI.Docking.TabGradient tabGradient2 = new DockingUI.WinFormsUI.Docking.TabGradient();
            DockingUI.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new DockingUI.WinFormsUI.Docking.DockPanelGradient();
            DockingUI.WinFormsUI.Docking.TabGradient tabGradient3 = new DockingUI.WinFormsUI.Docking.TabGradient();
            DockingUI.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new DockingUI.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            DockingUI.WinFormsUI.Docking.TabGradient tabGradient4 = new DockingUI.WinFormsUI.Docking.TabGradient();
            DockingUI.WinFormsUI.Docking.TabGradient tabGradient5 = new DockingUI.WinFormsUI.Docking.TabGradient();
            DockingUI.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new DockingUI.WinFormsUI.Docking.DockPanelGradient();
            DockingUI.WinFormsUI.Docking.TabGradient tabGradient6 = new DockingUI.WinFormsUI.Docking.TabGradient();
            DockingUI.WinFormsUI.Docking.TabGradient tabGradient7 = new DockingUI.WinFormsUI.Docking.TabGradient();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MAnimation));
            this.panel_DockPanel = new DockingUI.WinFormsUI.Docking.DockPanel();
            this.menuStrip_movieAnim = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.撤销ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重做ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.影片角色列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.影片帧编辑区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.影片时间轴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.影片图库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.元件库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帧图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置面板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动画演示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复位所有面板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试面板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGL窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新所有贴图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.贴图插值渲染ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_movieAnim.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_DockPanel
            // 
            this.panel_DockPanel.ActiveAutoHideContent = null;
            this.panel_DockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_DockPanel.DockBackColor = System.Drawing.SystemColors.Control;
            this.panel_DockPanel.Location = new System.Drawing.Point(0, 25);
            this.panel_DockPanel.Name = "panel_DockPanel";
            this.panel_DockPanel.Size = new System.Drawing.Size(1362, 537);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("宋体", 9F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("宋体", 9F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.panel_DockPanel.Skin = dockPanelSkin1;
            this.panel_DockPanel.TabIndex = 5;
            // 
            // menuStrip_movieAnim
            // 
            this.menuStrip_movieAnim.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.视图ToolStripMenuItem,
            this.测试ToolStripMenuItem});
            this.menuStrip_movieAnim.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_movieAnim.Name = "menuStrip_movieAnim";
            this.menuStrip_movieAnim.Size = new System.Drawing.Size(1362, 25);
            this.menuStrip_movieAnim.TabIndex = 6;
            this.menuStrip_movieAnim.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.撤销ToolStripMenuItem,
            this.重做ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 撤销ToolStripMenuItem
            // 
            this.撤销ToolStripMenuItem.Enabled = false;
            this.撤销ToolStripMenuItem.Name = "撤销ToolStripMenuItem";
            this.撤销ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.撤销ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.撤销ToolStripMenuItem.Text = "撤销";
            this.撤销ToolStripMenuItem.Click += new System.EventHandler(this.button_Undo_Click);
            // 
            // 重做ToolStripMenuItem
            // 
            this.重做ToolStripMenuItem.Enabled = false;
            this.重做ToolStripMenuItem.Name = "重做ToolStripMenuItem";
            this.重做ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.重做ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.重做ToolStripMenuItem.Text = "重做";
            this.重做ToolStripMenuItem.Click += new System.EventHandler(this.button_Redo_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.影片角色列表ToolStripMenuItem,
            this.影片帧编辑区ToolStripMenuItem,
            this.影片时间轴ToolStripMenuItem,
            this.影片图库ToolStripMenuItem,
            this.元件库ToolStripMenuItem,
            this.帧图层ToolStripMenuItem,
            this.配置面板ToolStripMenuItem,
            this.动画演示ToolStripMenuItem,
            this.复位所有面板ToolStripMenuItem,
            this.openGL窗口ToolStripMenuItem});
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.视图ToolStripMenuItem.Text = "视图";
            // 
            // 影片角色列表ToolStripMenuItem
            // 
            this.影片角色列表ToolStripMenuItem.Name = "影片角色列表ToolStripMenuItem";
            this.影片角色列表ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.影片角色列表ToolStripMenuItem.Text = "角色列表";
            this.影片角色列表ToolStripMenuItem.Click += new System.EventHandler(this.影片角色列表ToolStripMenuItem_Click);
            // 
            // 影片帧编辑区ToolStripMenuItem
            // 
            this.影片帧编辑区ToolStripMenuItem.Name = "影片帧编辑区ToolStripMenuItem";
            this.影片帧编辑区ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.影片帧编辑区ToolStripMenuItem.Text = "帧编辑区";
            this.影片帧编辑区ToolStripMenuItem.Click += new System.EventHandler(this.影片帧编辑区ToolStripMenuItem_Click);
            // 
            // 影片时间轴ToolStripMenuItem
            // 
            this.影片时间轴ToolStripMenuItem.Name = "影片时间轴ToolStripMenuItem";
            this.影片时间轴ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.影片时间轴ToolStripMenuItem.Text = "时间轴";
            this.影片时间轴ToolStripMenuItem.Click += new System.EventHandler(this.影片时间轴ToolStripMenuItem_Click);
            // 
            // 影片图库ToolStripMenuItem
            // 
            this.影片图库ToolStripMenuItem.Name = "影片图库ToolStripMenuItem";
            this.影片图库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.影片图库ToolStripMenuItem.Text = "资源库";
            this.影片图库ToolStripMenuItem.Click += new System.EventHandler(this.影片图库ToolStripMenuItem_Click);
            // 
            // 元件库ToolStripMenuItem
            // 
            this.元件库ToolStripMenuItem.Name = "元件库ToolStripMenuItem";
            this.元件库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.元件库ToolStripMenuItem.Text = "元件库";
            this.元件库ToolStripMenuItem.Click += new System.EventHandler(this.元件库ToolStripMenuItem_Click);
            // 
            // 帧图层ToolStripMenuItem
            // 
            this.帧图层ToolStripMenuItem.Name = "帧图层ToolStripMenuItem";
            this.帧图层ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.帧图层ToolStripMenuItem.Text = "帧图层";
            this.帧图层ToolStripMenuItem.Click += new System.EventHandler(this.帧图层ToolStripMenuItem_Click);
            // 
            // 配置面板ToolStripMenuItem
            // 
            this.配置面板ToolStripMenuItem.Name = "配置面板ToolStripMenuItem";
            this.配置面板ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.配置面板ToolStripMenuItem.Text = "配置信息";
            this.配置面板ToolStripMenuItem.Click += new System.EventHandler(this.配置面板ToolStripMenuItem_Click);
            // 
            // 动画演示ToolStripMenuItem
            // 
            this.动画演示ToolStripMenuItem.Name = "动画演示ToolStripMenuItem";
            this.动画演示ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.动画演示ToolStripMenuItem.Text = "动画演示";
            this.动画演示ToolStripMenuItem.Click += new System.EventHandler(this.动画演示ToolStripMenuItem_Click);
            // 
            // 复位所有面板ToolStripMenuItem
            // 
            this.复位所有面板ToolStripMenuItem.Name = "复位所有面板ToolStripMenuItem";
            this.复位所有面板ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.复位所有面板ToolStripMenuItem.Text = "复位所有面板";
            this.复位所有面板ToolStripMenuItem.Click += new System.EventHandler(this.复位所有面板ToolStripMenuItem_Click);
            // 
            // 测试ToolStripMenuItem
            // 
            this.测试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.测试面板ToolStripMenuItem});
            this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
            this.测试ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.测试ToolStripMenuItem.Text = "测试";
            // 
            // 测试面板ToolStripMenuItem
            // 
            this.测试面板ToolStripMenuItem.Name = "测试面板ToolStripMenuItem";
            this.测试面板ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.测试面板ToolStripMenuItem.Text = "测试面板";
            this.测试面板ToolStripMenuItem.Click += new System.EventHandler(this.测试面板ToolStripMenuItem_Click);
            // 
            // openGL窗口ToolStripMenuItem
            // 
            this.openGL窗口ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新所有贴图ToolStripMenuItem,
            this.贴图插值渲染ToolStripMenuItem});
            this.openGL窗口ToolStripMenuItem.Name = "openGL窗口ToolStripMenuItem";
            this.openGL窗口ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openGL窗口ToolStripMenuItem.Text = "OpenGL窗口";
            // 
            // 刷新所有贴图ToolStripMenuItem
            // 
            this.刷新所有贴图ToolStripMenuItem.Name = "刷新所有贴图ToolStripMenuItem";
            this.刷新所有贴图ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.刷新所有贴图ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.刷新所有贴图ToolStripMenuItem.Text = "刷新所有贴图";
            this.刷新所有贴图ToolStripMenuItem.Click += new System.EventHandler(this.刷新所有贴图ToolStripMenuItem_Click);
            // 
            // 贴图插值渲染ToolStripMenuItem
            // 
            this.贴图插值渲染ToolStripMenuItem.Checked = true;
            this.贴图插值渲染ToolStripMenuItem.CheckOnClick = true;
            this.贴图插值渲染ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.贴图插值渲染ToolStripMenuItem.Name = "贴图插值渲染ToolStripMenuItem";
            this.贴图插值渲染ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.贴图插值渲染ToolStripMenuItem.Text = "贴图插值渲染";
            this.贴图插值渲染ToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.贴图插值渲染ToolStripMenuItem_CheckStateChanged);
            // 
            // Form_MAnimation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1362, 562);
            this.Controls.Add(this.panel_DockPanel);
            this.Controls.Add(this.menuStrip_movieAnim);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip_movieAnim;
            this.MinimumSize = new System.Drawing.Size(16, 600);
            this.Name = "Form_MAnimation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "影片动画编辑";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.Form_AnimationEditor_SizeChanged);
            this.Shown += new System.EventHandler(this.Form_MAnimation_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_AnimationEditor_FormClosing);
            this.menuStrip_movieAnim.ResumeLayout(false);
            this.menuStrip_movieAnim.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DockPanel panel_DockPanel;
        private System.Windows.Forms.MenuStrip menuStrip_movieAnim;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 撤销ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重做ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 影片角色列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 影片帧编辑区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 影片时间轴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置面板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 影片图库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 元件库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试面板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帧图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复位所有面板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 动画演示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openGL窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新所有贴图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 贴图插值渲染ToolStripMenuItem;
    }
}