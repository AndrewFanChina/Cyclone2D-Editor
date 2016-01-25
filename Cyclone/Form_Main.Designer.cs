using System.Drawing;
namespace Cyclone
{
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_文件_0 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Combine = new System.Windows.Forms.ToolStripMenuItem();
            this.导出数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator文件_1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.撤销ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重做ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动画和地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.影片动画编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图图片管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图层透明度调整ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.场景缩放ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_P100 = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_P200 = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_P400 = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_P800 = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_P50 = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_P25 = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_P12dot5 = new System.Windows.Forms.ToolStripMenuItem();
            this.物理标记ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图形IDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.融合地形层上图形元素编号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.顶层显示物理层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图角色初始帧ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图角色NPC编号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图角色锚点坐标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.脚本编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.脚本编辑器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变量与函数容器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.其它功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文本管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图片处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.调色板编辑器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按比例缩放ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图片格式转换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.压缩混淆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.小工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件批量打包ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.生成地图位图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成脚本文本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip_Main = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Welcome = new System.Windows.Forms.ToolStripStatusLabel();
            this.label_showFunction = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSPB_load = new System.Windows.Forms.ToolStripProgressBar();
            this.tableLayoutPanel_Main = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_Center = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_Center_L = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_Maps = new System.Windows.Forms.GroupBox();
            this.TLP_mapList = new System.Windows.Forms.TableLayoutPanel();
            this.listBox_stage = new System.Windows.Forms.ListBox();
            this.listBox_Maps = new System.Windows.Forms.ListBox();
            this.tabControl_Center_L_T = new System.Windows.Forms.TabControl();
            this.tabPage_physic = new System.Windows.Forms.TabPage();
            this.TLP_Physics = new System.Windows.Forms.TableLayoutPanel();
            this.TLP_Physics_TOP = new System.Windows.Forms.TableLayoutPanel();
            this.panel_PhysicsBG = new System.Windows.Forms.Panel();
            this.pictureBox_Physics = new System.Windows.Forms.PictureBox();
            this.vScrollBar_Physics = new System.Windows.Forms.VScrollBar();
            this.panel_Physics_Tool = new System.Windows.Forms.Panel();
            this.button_config = new System.Windows.Forms.Button();
            this.button_AutoGen = new System.Windows.Forms.Button();
            this.numericUpDown_autoGen_Phy = new System.Windows.Forms.NumericUpDown();
            this.button_del_Phy = new System.Windows.Forms.Button();
            this.button_add_Phy = new System.Windows.Forms.Button();
            this.tabPage_Gfx = new System.Windows.Forms.TabPage();
            this.TLP_Gfx = new System.Windows.Forms.TableLayoutPanel();
            this.TLP_Gfx_Top = new System.Windows.Forms.TableLayoutPanel();
            this.panel_ground = new System.Windows.Forms.Panel();
            this.pictureBox_Gfx = new System.Windows.Forms.PictureBox();
            this.vScrollBar_Gfx = new System.Windows.Forms.VScrollBar();
            this.panel_gfx_tool = new System.Windows.Forms.Panel();
            this.label_TileGfxUsedTime = new System.Windows.Forms.Label();
            this.label_TileGfxID = new System.Windows.Forms.Label();
            this.button_del_Gfx = new System.Windows.Forms.Button();
            this.panel_Flag = new System.Windows.Forms.Panel();
            this.button_Copy_Gfx = new System.Windows.Forms.Button();
            this.button_Right_Gfx = new System.Windows.Forms.Button();
            this.button_addOne_Gfx = new System.Windows.Forms.Button();
            this.button_Left_Gfx = new System.Windows.Forms.Button();
            this.button_Down_Gfx = new System.Windows.Forms.Button();
            this.button_Up_Gfx = new System.Windows.Forms.Button();
            this.button_TileGfx_TransN = new System.Windows.Forms.Button();
            this.button_TileGfx_TransP = new System.Windows.Forms.Button();
            this.button_TileGfx_FlipV = new System.Windows.Forms.Button();
            this.button_TileGfx_FlipH = new System.Windows.Forms.Button();
            this.panel_GfXGroup = new System.Windows.Forms.Panel();
            this.button_NameFolder = new System.Windows.Forms.Button();
            this.button_CheckContainer = new System.Windows.Forms.Button();
            this.button_ClearSpilth = new System.Windows.Forms.Button();
            this.comboBox_GfxType = new System.Windows.Forms.ComboBox();
            this.button_DelGfxFolder = new System.Windows.Forms.Button();
            this.button_AddGfxFolder = new System.Windows.Forms.Button();
            this.button_addMore_Grx = new System.Windows.Forms.Button();
            this.tabPage_AT = new System.Windows.Forms.TabPage();
            this.TLP_AT = new System.Windows.Forms.TableLayoutPanel();
            this.panel_ATpack = new System.Windows.Forms.Panel();
            this.button_ClearATSpilth = new System.Windows.Forms.Button();
            this.button_ATPack_Rename = new System.Windows.Forms.Button();
            this.button_checkAT = new System.Windows.Forms.Button();
            this.comboBox_ATFolders = new System.Windows.Forms.ComboBox();
            this.button_obj_import = new System.Windows.Forms.Button();
            this.button_ATPack_Del = new System.Windows.Forms.Button();
            this.button_ATPack_Add = new System.Windows.Forms.Button();
            this.TLP_AT_top = new System.Windows.Forms.TableLayoutPanel();
            this.panel_AT = new System.Windows.Forms.Panel();
            this.pictureBox_AT = new System.Windows.Forms.PictureBox();
            this.vScrollBar_AT = new System.Windows.Forms.VScrollBar();
            this.panel_obj_tool = new System.Windows.Forms.Panel();
            this.button_GenIDs = new System.Windows.Forms.Button();
            this.button_cloneAnteType = new System.Windows.Forms.Button();
            this.button_configAT = new System.Windows.Forms.Button();
            this.lable_AT = new System.Windows.Forms.Label();
            this.button_Obj_del = new System.Windows.Forms.Button();
            this.button_obj_refresh = new System.Windows.Forms.Button();
            this.tabPage_Scripts = new System.Windows.Forms.TabPage();
            this.SC_BG = new System.Windows.Forms.SplitContainer();
            this.groupBox_lofter = new System.Windows.Forms.GroupBox();
            this.listBox_Carrier = new System.Windows.Forms.ListBox();
            this.groupBox_threads = new System.Windows.Forms.GroupBox();
            this.listBox_Files = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel_Canvas = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Canvas = new System.Windows.Forms.Panel();
            this.pictureBox_Canvas = new System.Windows.Forms.PictureBox();
            this.vScrollBar_Canvas = new System.Windows.Forms.VScrollBar();
            this.hScrollBar_Canvas = new System.Windows.Forms.HScrollBar();
            this.toolStrip_Operate = new System.Windows.Forms.ToolStrip();
            this.TSB_open = new System.Windows.Forms.ToolStripButton();
            this.TSB_save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_levelPhysic = new System.Windows.Forms.ToolStripButton();
            this.TSB_level_Ground = new System.Windows.Forms.ToolStripButton();
            this.TSB_level_Surface = new System.Windows.Forms.ToolStripButton();
            this.TSB_level_Tile_Obj = new System.Windows.Forms.ToolStripButton();
            this.TSB_level_Obj_Mask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_level_Object = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_levelActors_Eye = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_pencil = new System.Windows.Forms.ToolStripButton();
            this.TSB_rectSelect = new System.Windows.Forms.ToolStripButton();
            this.TSB_pointSelect = new System.Windows.Forms.ToolStripButton();
            this.TSB_Straw = new System.Windows.Forms.ToolStripButton();
            this.TSB_FillIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_autoTile = new System.Windows.Forms.ToolStripButton();
            this.TSB_fillCorner = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_Copy = new System.Windows.Forms.ToolStripButton();
            this.TSB_Paste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_Undo = new System.Windows.Forms.ToolStripButton();
            this.TSB_Redo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_findAT_Pre = new System.Windows.Forms.ToolStripButton();
            this.TSB_findAT_Next = new System.Windows.Forms.ToolStripButton();
            this.TSB_Replace = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_Refresh = new System.Windows.Forms.ToolStripButton();
            this.TSB_ErrorCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.contextMenuStrip_listItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.克隆toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置双击条目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_AnteType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_MoveAnteType = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_listItemkss = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kss_新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑kss脚本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_RefreshKss = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新载体列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Main.SuspendLayout();
            this.statusStrip_Main.SuspendLayout();
            this.tableLayoutPanel_Main.SuspendLayout();
            this.tableLayoutPanel_Center.SuspendLayout();
            this.tableLayoutPanel_Center_L.SuspendLayout();
            this.groupBox_Maps.SuspendLayout();
            this.TLP_mapList.SuspendLayout();
            this.tabControl_Center_L_T.SuspendLayout();
            this.tabPage_physic.SuspendLayout();
            this.TLP_Physics.SuspendLayout();
            this.TLP_Physics_TOP.SuspendLayout();
            this.panel_PhysicsBG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Physics)).BeginInit();
            this.panel_Physics_Tool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_autoGen_Phy)).BeginInit();
            this.tabPage_Gfx.SuspendLayout();
            this.TLP_Gfx.SuspendLayout();
            this.TLP_Gfx_Top.SuspendLayout();
            this.panel_ground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Gfx)).BeginInit();
            this.panel_gfx_tool.SuspendLayout();
            this.panel_GfXGroup.SuspendLayout();
            this.tabPage_AT.SuspendLayout();
            this.TLP_AT.SuspendLayout();
            this.panel_ATpack.SuspendLayout();
            this.TLP_AT_top.SuspendLayout();
            this.panel_AT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AT)).BeginInit();
            this.panel_obj_tool.SuspendLayout();
            this.tabPage_Scripts.SuspendLayout();
            this.SC_BG.Panel1.SuspendLayout();
            this.SC_BG.Panel2.SuspendLayout();
            this.SC_BG.SuspendLayout();
            this.groupBox_lofter.SuspendLayout();
            this.groupBox_threads.SuspendLayout();
            this.tableLayoutPanel_Canvas.SuspendLayout();
            this.panel_Canvas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Canvas)).BeginInit();
            this.toolStrip_Operate.SuspendLayout();
            this.contextMenuStrip_listItem.SuspendLayout();
            this.contextMenuStrip_AnteType.SuspendLayout();
            this.contextMenuStrip_listItemkss.SuspendLayout();
            this.CMS_RefreshKss.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.menuStrip_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.动画和地图ToolStripMenuItem,
            this.脚本编辑ToolStripMenuItem,
            this.其它功能ToolStripMenuItem,
            this.图片处理ToolStripMenuItem,
            this.小工具ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip_Main.Size = new System.Drawing.Size(1267, 31);
            this.menuStrip_Main.TabIndex = 1;
            this.menuStrip_Main.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开工程ToolStripMenuItem,
            this.保存工程ToolStripMenuItem,
            this.toolStripSeparator_文件_0,
            this.ToolStripMenuItem_Combine,
            this.导出数据ToolStripMenuItem,
            this.toolStripSeparator文件_1,
            this.退出ToolStripMenuItem1});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 27);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开工程ToolStripMenuItem
            // 
            this.打开工程ToolStripMenuItem.Name = "打开工程ToolStripMenuItem";
            this.打开工程ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.打开工程ToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.打开工程ToolStripMenuItem.Text = "打开工程";
            this.打开工程ToolStripMenuItem.Click += new System.EventHandler(this.打开工程ToolStripMenuItem_Click);
            // 
            // 保存工程ToolStripMenuItem
            // 
            this.保存工程ToolStripMenuItem.Name = "保存工程ToolStripMenuItem";
            this.保存工程ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存工程ToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.保存工程ToolStripMenuItem.Text = "保存工程";
            this.保存工程ToolStripMenuItem.Click += new System.EventHandler(this.保存工程ToolStripMenuItem_Click);
            // 
            // toolStripSeparator_文件_0
            // 
            this.toolStripSeparator_文件_0.Name = "toolStripSeparator_文件_0";
            this.toolStripSeparator_文件_0.Size = new System.Drawing.Size(193, 6);
            // 
            // ToolStripMenuItem_Combine
            // 
            this.ToolStripMenuItem_Combine.Name = "ToolStripMenuItem_Combine";
            this.ToolStripMenuItem_Combine.Size = new System.Drawing.Size(196, 24);
            this.ToolStripMenuItem_Combine.Text = "合并场景和动画";
            this.ToolStripMenuItem_Combine.Click += new System.EventHandler(this.ToolStripMenuItem_Combine_Click);
            // 
            // 导出数据ToolStripMenuItem
            // 
            this.导出数据ToolStripMenuItem.Name = "导出数据ToolStripMenuItem";
            this.导出数据ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.导出数据ToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.导出数据ToolStripMenuItem.Text = "导出数据";
            this.导出数据ToolStripMenuItem.Click += new System.EventHandler(this.导出数据ToolStripMenuItem_Click);
            // 
            // toolStripSeparator文件_1
            // 
            this.toolStripSeparator文件_1.Name = "toolStripSeparator文件_1";
            this.toolStripSeparator文件_1.Size = new System.Drawing.Size(193, 6);
            // 
            // 退出ToolStripMenuItem1
            // 
            this.退出ToolStripMenuItem1.Name = "退出ToolStripMenuItem1";
            this.退出ToolStripMenuItem1.Size = new System.Drawing.Size(196, 24);
            this.退出ToolStripMenuItem1.Text = "退出";
            this.退出ToolStripMenuItem1.Click += new System.EventHandler(this.退出ToolStripMenuItem1_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.撤销ToolStripMenuItem,
            this.重做ToolStripMenuItem,
            this.toolStripSeparator9,
            this.刷新ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(51, 27);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 撤销ToolStripMenuItem
            // 
            this.撤销ToolStripMenuItem.Name = "撤销ToolStripMenuItem";
            this.撤销ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.撤销ToolStripMenuItem.Size = new System.Drawing.Size(194, 24);
            this.撤销ToolStripMenuItem.Text = "撤销";
            this.撤销ToolStripMenuItem.Click += new System.EventHandler(this.撤销ToolStripMenuItem_Click);
            // 
            // 重做ToolStripMenuItem
            // 
            this.重做ToolStripMenuItem.Name = "重做ToolStripMenuItem";
            this.重做ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.重做ToolStripMenuItem.Size = new System.Drawing.Size(194, 24);
            this.重做ToolStripMenuItem.Text = "重做";
            this.重做ToolStripMenuItem.Click += new System.EventHandler(this.重做ToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(191, 6);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(194, 24);
            this.刷新ToolStripMenuItem.Text = "刷新脚本载体";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // 动画和地图ToolStripMenuItem
            // 
            this.动画和地图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.影片动画编辑ToolStripMenuItem,
            this.地图图片管理ToolStripMenuItem,
            this.toolStripSeparator13,
            this.配置ToolStripMenuItem,
            this.显示ToolStripMenuItem});
            this.动画和地图ToolStripMenuItem.Name = "动画和地图ToolStripMenuItem";
            this.动画和地图ToolStripMenuItem.Size = new System.Drawing.Size(51, 27);
            this.动画和地图ToolStripMenuItem.Text = "视图";
            // 
            // 影片动画编辑ToolStripMenuItem
            // 
            this.影片动画编辑ToolStripMenuItem.Name = "影片动画编辑ToolStripMenuItem";
            this.影片动画编辑ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.影片动画编辑ToolStripMenuItem.Size = new System.Drawing.Size(229, 24);
            this.影片动画编辑ToolStripMenuItem.Text = "影片动画编辑";
            this.影片动画编辑ToolStripMenuItem.Click += new System.EventHandler(this.影片动画编辑ToolStripMenuItem_Click);
            // 
            // 地图图片管理ToolStripMenuItem
            // 
            this.地图图片管理ToolStripMenuItem.Name = "地图图片管理ToolStripMenuItem";
            this.地图图片管理ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.地图图片管理ToolStripMenuItem.Size = new System.Drawing.Size(229, 24);
            this.地图图片管理ToolStripMenuItem.Text = "地图图片管理";
            this.地图图片管理ToolStripMenuItem.Click += new System.EventHandler(this.地图图片管理ToolStripMenuItem_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(226, 6);
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图层透明度调整ToolStripMenuItem});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(229, 24);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // 图层透明度调整ToolStripMenuItem
            // 
            this.图层透明度调整ToolStripMenuItem.Name = "图层透明度调整ToolStripMenuItem";
            this.图层透明度调整ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.图层透明度调整ToolStripMenuItem.Text = "图层透明度调整";
            this.图层透明度调整ToolStripMenuItem.Click += new System.EventHandler(this.图层透明度调整ToolStripMenuItem_Click);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.场景缩放ToolStripMenuItem,
            this.物理标记ToolStripMenuItem,
            this.图形IDToolStripMenuItem,
            this.融合地形层上图形元素编号ToolStripMenuItem,
            this.顶层显示物理层ToolStripMenuItem,
            this.地图角色初始帧ToolStripMenuItem,
            this.地图角色NPC编号ToolStripMenuItem,
            this.地图角色锚点坐标ToolStripMenuItem});
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(229, 24);
            this.显示ToolStripMenuItem.Text = "显示";
            // 
            // 场景缩放ToolStripMenuItem
            // 
            this.场景缩放ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_P100,
            this.TSMI_P200,
            this.TSMI_P400,
            this.TSMI_P800,
            this.TSMI_P50,
            this.TSMI_P25,
            this.TSMI_P12dot5});
            this.场景缩放ToolStripMenuItem.Name = "场景缩放ToolStripMenuItem";
            this.场景缩放ToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.场景缩放ToolStripMenuItem.Text = "场景缩放";
            // 
            // TSMI_P100
            // 
            this.TSMI_P100.Checked = true;
            this.TSMI_P100.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSMI_P100.Name = "TSMI_P100";
            this.TSMI_P100.Size = new System.Drawing.Size(122, 24);
            this.TSMI_P100.Text = "100%";
            this.TSMI_P100.Click += new System.EventHandler(this.TSMI_P100_Click);
            // 
            // TSMI_P200
            // 
            this.TSMI_P200.Name = "TSMI_P200";
            this.TSMI_P200.Size = new System.Drawing.Size(122, 24);
            this.TSMI_P200.Text = "200%";
            this.TSMI_P200.Click += new System.EventHandler(this.TSMI_P200_Click);
            // 
            // TSMI_P400
            // 
            this.TSMI_P400.Name = "TSMI_P400";
            this.TSMI_P400.Size = new System.Drawing.Size(122, 24);
            this.TSMI_P400.Text = "400%";
            this.TSMI_P400.Click += new System.EventHandler(this.TSMI_P400_Click);
            // 
            // TSMI_P800
            // 
            this.TSMI_P800.Name = "TSMI_P800";
            this.TSMI_P800.Size = new System.Drawing.Size(122, 24);
            this.TSMI_P800.Text = "800%";
            this.TSMI_P800.Click += new System.EventHandler(this.TSMI_P800_Click);
            // 
            // TSMI_P50
            // 
            this.TSMI_P50.Name = "TSMI_P50";
            this.TSMI_P50.Size = new System.Drawing.Size(122, 24);
            this.TSMI_P50.Text = "50%";
            this.TSMI_P50.Click += new System.EventHandler(this.TSMI_P50_Click);
            // 
            // TSMI_P25
            // 
            this.TSMI_P25.Name = "TSMI_P25";
            this.TSMI_P25.Size = new System.Drawing.Size(122, 24);
            this.TSMI_P25.Text = "25%";
            this.TSMI_P25.Click += new System.EventHandler(this.TSMI_P25_Click);
            // 
            // TSMI_P12dot5
            // 
            this.TSMI_P12dot5.Name = "TSMI_P12dot5";
            this.TSMI_P12dot5.Size = new System.Drawing.Size(122, 24);
            this.TSMI_P12dot5.Text = "12.5%";
            this.TSMI_P12dot5.Click += new System.EventHandler(this.TSMI_P12dot5_Click);
            // 
            // 物理标记ToolStripMenuItem
            // 
            this.物理标记ToolStripMenuItem.Name = "物理标记ToolStripMenuItem";
            this.物理标记ToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.物理标记ToolStripMenuItem.Text = "地图中物理元素编号";
            this.物理标记ToolStripMenuItem.Click += new System.EventHandler(this.物理标记ToolStripMenuItem_Click);
            // 
            // 图形IDToolStripMenuItem
            // 
            this.图形IDToolStripMenuItem.Name = "图形IDToolStripMenuItem";
            this.图形IDToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.图形IDToolStripMenuItem.Text = "底层地形层上图形元素编号";
            this.图形IDToolStripMenuItem.Click += new System.EventHandler(this.图形IDToolStripMenuItem_Click);
            // 
            // 融合地形层上图形元素编号ToolStripMenuItem
            // 
            this.融合地形层上图形元素编号ToolStripMenuItem.Name = "融合地形层上图形元素编号ToolStripMenuItem";
            this.融合地形层上图形元素编号ToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.融合地形层上图形元素编号ToolStripMenuItem.Text = "融合地形层上图形元素编号";
            this.融合地形层上图形元素编号ToolStripMenuItem.Click += new System.EventHandler(this.融合地形层上图形元素编号ToolStripMenuItem_Click);
            // 
            // 顶层显示物理层ToolStripMenuItem
            // 
            this.顶层显示物理层ToolStripMenuItem.Name = "顶层显示物理层ToolStripMenuItem";
            this.顶层显示物理层ToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.顶层显示物理层ToolStripMenuItem.Text = "置顶显示物理层";
            this.顶层显示物理层ToolStripMenuItem.Click += new System.EventHandler(this.顶层显示物理层ToolStripMenuItem_Click);
            // 
            // 地图角色初始帧ToolStripMenuItem
            // 
            this.地图角色初始帧ToolStripMenuItem.Name = "地图角色初始帧ToolStripMenuItem";
            this.地图角色初始帧ToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.地图角色初始帧ToolStripMenuItem.Text = "地图角色帧编号";
            this.地图角色初始帧ToolStripMenuItem.Click += new System.EventHandler(this.地图角色初始帧ToolStripMenuItem_Click);
            // 
            // 地图角色NPC编号ToolStripMenuItem
            // 
            this.地图角色NPC编号ToolStripMenuItem.Name = "地图角色NPC编号ToolStripMenuItem";
            this.地图角色NPC编号ToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.地图角色NPC编号ToolStripMenuItem.Text = "地图角色NPC编号";
            this.地图角色NPC编号ToolStripMenuItem.Click += new System.EventHandler(this.地图角色NPC编号ToolStripMenuItem_Click);
            // 
            // 地图角色锚点坐标ToolStripMenuItem
            // 
            this.地图角色锚点坐标ToolStripMenuItem.Name = "地图角色锚点坐标ToolStripMenuItem";
            this.地图角色锚点坐标ToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.地图角色锚点坐标ToolStripMenuItem.Text = "地图角色锚点坐标";
            this.地图角色锚点坐标ToolStripMenuItem.Click += new System.EventHandler(this.地图角色锚点坐标ToolStripMenuItem_Click);
            // 
            // 脚本编辑ToolStripMenuItem
            // 
            this.脚本编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.脚本编辑器ToolStripMenuItem,
            this.变量与函数容器ToolStripMenuItem});
            this.脚本编辑ToolStripMenuItem.Name = "脚本编辑ToolStripMenuItem";
            this.脚本编辑ToolStripMenuItem.Size = new System.Drawing.Size(96, 27);
            this.脚本编辑ToolStripMenuItem.Text = "脚本与数值";
            // 
            // 脚本编辑器ToolStripMenuItem
            // 
            this.脚本编辑器ToolStripMenuItem.Name = "脚本编辑器ToolStripMenuItem";
            this.脚本编辑器ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.脚本编辑器ToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.脚本编辑器ToolStripMenuItem.Text = "属性与数值编辑";
            this.脚本编辑器ToolStripMenuItem.Click += new System.EventHandler(this.脚本编辑器ToolStripMenuItem_Click);
            // 
            // 变量与函数容器ToolStripMenuItem
            // 
            this.变量与函数容器ToolStripMenuItem.Name = "变量与函数容器ToolStripMenuItem";
            this.变量与函数容器ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.变量与函数容器ToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.变量与函数容器ToolStripMenuItem.Text = "变量与函数容器";
            this.变量与函数容器ToolStripMenuItem.Click += new System.EventHandler(this.变量容器ToolStripMenuItem_Click);
            // 
            // 其它功能ToolStripMenuItem
            // 
            this.其它功能ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UIToolStripMenuItem,
            this.文本管理ToolStripMenuItem});
            this.其它功能ToolStripMenuItem.Name = "其它功能ToolStripMenuItem";
            this.其它功能ToolStripMenuItem.Size = new System.Drawing.Size(81, 27);
            this.其它功能ToolStripMenuItem.Text = "其它功能";
            // 
            // UIToolStripMenuItem
            // 
            this.UIToolStripMenuItem.Name = "UIToolStripMenuItem";
            this.UIToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.UIToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.UIToolStripMenuItem.Text = "界面编辑";
            this.UIToolStripMenuItem.Click += new System.EventHandler(this.UIToolStripMenuItem_Click);
            // 
            // 文本管理ToolStripMenuItem
            // 
            this.文本管理ToolStripMenuItem.Name = "文本管理ToolStripMenuItem";
            this.文本管理ToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.文本管理ToolStripMenuItem.Text = "文本管理";
            this.文本管理ToolStripMenuItem.Click += new System.EventHandler(this.文字编辑ToolStripMenuItem_Click);
            // 
            // 图片处理ToolStripMenuItem
            // 
            this.图片处理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.调色板编辑器ToolStripMenuItem,
            this.按比例缩放ToolStripMenuItem,
            this.图片格式转换ToolStripMenuItem,
            this.压缩混淆ToolStripMenuItem});
            this.图片处理ToolStripMenuItem.Name = "图片处理ToolStripMenuItem";
            this.图片处理ToolStripMenuItem.Size = new System.Drawing.Size(81, 27);
            this.图片处理ToolStripMenuItem.Text = "图片处理";
            // 
            // 调色板编辑器ToolStripMenuItem
            // 
            this.调色板编辑器ToolStripMenuItem.Name = "调色板编辑器ToolStripMenuItem";
            this.调色板编辑器ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.调色板编辑器ToolStripMenuItem.Text = "调色板编辑";
            this.调色板编辑器ToolStripMenuItem.Click += new System.EventHandler(this.调色板编辑器ToolStripMenuItem_Click);
            // 
            // 按比例缩放ToolStripMenuItem
            // 
            this.按比例缩放ToolStripMenuItem.Name = "按比例缩放ToolStripMenuItem";
            this.按比例缩放ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.按比例缩放ToolStripMenuItem.Text = "按比例缩放";
            this.按比例缩放ToolStripMenuItem.Click += new System.EventHandler(this.按比例缩放ToolStripMenuItem_Click);
            // 
            // 图片格式转换ToolStripMenuItem
            // 
            this.图片格式转换ToolStripMenuItem.Name = "图片格式转换ToolStripMenuItem";
            this.图片格式转换ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.图片格式转换ToolStripMenuItem.Text = "格式转换";
            this.图片格式转换ToolStripMenuItem.Click += new System.EventHandler(this.图片格式转换ToolStripMenuItem1_Click);
            // 
            // 压缩混淆ToolStripMenuItem
            // 
            this.压缩混淆ToolStripMenuItem.Name = "压缩混淆ToolStripMenuItem";
            this.压缩混淆ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.压缩混淆ToolStripMenuItem.Text = "压缩混淆";
            this.压缩混淆ToolStripMenuItem.Click += new System.EventHandler(this.压缩混淆ToolStripMenuItem_Click);
            // 
            // 小工具ToolStripMenuItem
            // 
            this.小工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件批量打包ToolStripMenuItem,
            this.toolStripSeparator8,
            this.生成地图位图ToolStripMenuItem,
            this.生成脚本文本ToolStripMenuItem,
            this.toolStripSeparator11});
            this.小工具ToolStripMenuItem.Name = "小工具ToolStripMenuItem";
            this.小工具ToolStripMenuItem.Size = new System.Drawing.Size(66, 27);
            this.小工具ToolStripMenuItem.Text = "小工具";
            // 
            // 文件批量打包ToolStripMenuItem
            // 
            this.文件批量打包ToolStripMenuItem.Name = "文件批量打包ToolStripMenuItem";
            this.文件批量打包ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.文件批量打包ToolStripMenuItem.Text = "文件批量打包";
            this.文件批量打包ToolStripMenuItem.Click += new System.EventHandler(this.文件批量打包ToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(165, 6);
            // 
            // 生成地图位图ToolStripMenuItem
            // 
            this.生成地图位图ToolStripMenuItem.Name = "生成地图位图ToolStripMenuItem";
            this.生成地图位图ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.生成地图位图ToolStripMenuItem.Text = "生成关卡位图";
            this.生成地图位图ToolStripMenuItem.Click += new System.EventHandler(this.生成地图位图ToolStripMenuItem_Click);
            // 
            // 生成脚本文本ToolStripMenuItem
            // 
            this.生成脚本文本ToolStripMenuItem.Name = "生成脚本文本ToolStripMenuItem";
            this.生成脚本文本ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.生成脚本文本ToolStripMenuItem.Text = "生成脚本文本";
            this.生成脚本文本ToolStripMenuItem.Click += new System.EventHandler(this.生成脚本文本ToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(165, 6);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.版本信息ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(51, 27);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 版本信息ToolStripMenuItem
            // 
            this.版本信息ToolStripMenuItem.Name = "版本信息ToolStripMenuItem";
            this.版本信息ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.版本信息ToolStripMenuItem.Text = "版本信息";
            this.版本信息ToolStripMenuItem.Click += new System.EventHandler(this.版本信息ToolStripMenuItem_Click);
            // 
            // statusStrip_Main
            // 
            this.statusStrip_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.statusStrip_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Welcome,
            this.label_showFunction,
            this.TSPB_load});
            this.statusStrip_Main.Location = new System.Drawing.Point(0, 717);
            this.statusStrip_Main.Name = "statusStrip_Main";
            this.statusStrip_Main.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip_Main.Size = new System.Drawing.Size(1267, 28);
            this.statusStrip_Main.TabIndex = 3;
            this.statusStrip_Main.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Welcome
            // 
            this.toolStripStatusLabel_Welcome.Name = "toolStripStatusLabel_Welcome";
            this.toolStripStatusLabel_Welcome.Size = new System.Drawing.Size(54, 23);
            this.toolStripStatusLabel_Welcome.Text = "提示：";
            // 
            // label_showFunction
            // 
            this.label_showFunction.Name = "label_showFunction";
            this.label_showFunction.Size = new System.Drawing.Size(84, 23);
            this.label_showFunction.Text = "功能指示区";
            // 
            // TSPB_load
            // 
            this.TSPB_load.Name = "TSPB_load";
            this.TSPB_load.Size = new System.Drawing.Size(267, 22);
            // 
            // tableLayoutPanel_Main
            // 
            this.tableLayoutPanel_Main.ColumnCount = 1;
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Main.Controls.Add(this.statusStrip_Main, 0, 3);
            this.tableLayoutPanel_Main.Controls.Add(this.tableLayoutPanel_Center, 0, 2);
            this.tableLayoutPanel_Main.Controls.Add(this.menuStrip_Main, 0, 0);
            this.tableLayoutPanel_Main.Controls.Add(this.toolStrip_Operate, 0, 1);
            this.tableLayoutPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Main.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_Main.Name = "tableLayoutPanel_Main";
            this.tableLayoutPanel_Main.RowCount = 4;
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel_Main.Size = new System.Drawing.Size(1267, 745);
            this.tableLayoutPanel_Main.TabIndex = 4;
            // 
            // tableLayoutPanel_Center
            // 
            this.tableLayoutPanel_Center.ColumnCount = 2;
            this.tableLayoutPanel_Center.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 396F));
            this.tableLayoutPanel_Center.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Center.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel_Center.Controls.Add(this.tableLayoutPanel_Center_L, 0, 0);
            this.tableLayoutPanel_Center.Controls.Add(this.tableLayoutPanel_Canvas, 1, 0);
            this.tableLayoutPanel_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Center.Location = new System.Drawing.Point(3, 64);
            this.tableLayoutPanel_Center.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel_Center.Name = "tableLayoutPanel_Center";
            this.tableLayoutPanel_Center.RowCount = 1;
            this.tableLayoutPanel_Center.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Center.Size = new System.Drawing.Size(1261, 651);
            this.tableLayoutPanel_Center.TabIndex = 4;
            // 
            // tableLayoutPanel_Center_L
            // 
            this.tableLayoutPanel_Center_L.ColumnCount = 1;
            this.tableLayoutPanel_Center_L.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Center_L.Controls.Add(this.groupBox_Maps, 0, 1);
            this.tableLayoutPanel_Center_L.Controls.Add(this.tabControl_Center_L_T, 0, 0);
            this.tableLayoutPanel_Center_L.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Center_L.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel_Center_L.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel_Center_L.Name = "tableLayoutPanel_Center_L";
            this.tableLayoutPanel_Center_L.RowCount = 2;
            this.tableLayoutPanel_Center_L.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel_Center_L.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel_Center_L.Size = new System.Drawing.Size(388, 643);
            this.tableLayoutPanel_Center_L.TabIndex = 2;
            // 
            // groupBox_Maps
            // 
            this.groupBox_Maps.Controls.Add(this.TLP_mapList);
            this.groupBox_Maps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Maps.ForeColor = System.Drawing.Color.Black;
            this.groupBox_Maps.Location = new System.Drawing.Point(0, 450);
            this.groupBox_Maps.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox_Maps.Name = "groupBox_Maps";
            this.groupBox_Maps.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox_Maps.Size = new System.Drawing.Size(388, 193);
            this.groupBox_Maps.TabIndex = 0;
            this.groupBox_Maps.TabStop = false;
            this.groupBox_Maps.Text = "地图和场景列表";
            // 
            // TLP_mapList
            // 
            this.TLP_mapList.ColumnCount = 2;
            this.TLP_mapList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_mapList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_mapList.Controls.Add(this.listBox_stage, 1, 0);
            this.TLP_mapList.Controls.Add(this.listBox_Maps, 0, 0);
            this.TLP_mapList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_mapList.Location = new System.Drawing.Point(8, 26);
            this.TLP_mapList.Margin = new System.Windows.Forms.Padding(4);
            this.TLP_mapList.Name = "TLP_mapList";
            this.TLP_mapList.RowCount = 1;
            this.TLP_mapList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_mapList.Size = new System.Drawing.Size(372, 159);
            this.TLP_mapList.TabIndex = 0;
            // 
            // listBox_stage
            // 
            this.listBox_stage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(119)))));
            this.listBox_stage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_stage.ForeColor = System.Drawing.Color.Maroon;
            this.listBox_stage.FormattingEnabled = true;
            this.listBox_stage.ItemHeight = 15;
            this.listBox_stage.Location = new System.Drawing.Point(186, 0);
            this.listBox_stage.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_stage.Name = "listBox_stage";
            this.listBox_stage.Size = new System.Drawing.Size(186, 154);
            this.listBox_stage.TabIndex = 1;
            this.listBox_stage.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox_stage_MouseDoubleClick);
            this.listBox_stage.SelectedIndexChanged += new System.EventHandler(this.listBox_stage_SelectedIndexChanged);
            this.listBox_stage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_stage_MouseDown);
            this.listBox_stage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_stage_KeyDown);
            // 
            // listBox_Maps
            // 
            this.listBox_Maps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(119)))));
            this.listBox_Maps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Maps.ForeColor = System.Drawing.Color.Maroon;
            this.listBox_Maps.FormattingEnabled = true;
            this.listBox_Maps.ItemHeight = 15;
            this.listBox_Maps.Location = new System.Drawing.Point(0, 0);
            this.listBox_Maps.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_Maps.Name = "listBox_Maps";
            this.listBox_Maps.Size = new System.Drawing.Size(186, 154);
            this.listBox_Maps.TabIndex = 0;
            this.listBox_Maps.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox_Maps_MouseDoubleClick);
            this.listBox_Maps.SelectedIndexChanged += new System.EventHandler(this.listBox_Maps_SelectedIndexChanged);
            this.listBox_Maps.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_Maps_MouseDown);
            this.listBox_Maps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_Maps_KeyDown);
            // 
            // tabControl_Center_L_T
            // 
            this.tabControl_Center_L_T.Controls.Add(this.tabPage_physic);
            this.tabControl_Center_L_T.Controls.Add(this.tabPage_Gfx);
            this.tabControl_Center_L_T.Controls.Add(this.tabPage_AT);
            this.tabControl_Center_L_T.Controls.Add(this.tabPage_Scripts);
            this.tabControl_Center_L_T.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Center_L_T.HotTrack = true;
            this.tabControl_Center_L_T.ItemSize = new System.Drawing.Size(66, 18);
            this.tabControl_Center_L_T.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Center_L_T.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl_Center_L_T.Name = "tabControl_Center_L_T";
            this.tabControl_Center_L_T.Padding = new System.Drawing.Point(11, 3);
            this.tabControl_Center_L_T.SelectedIndex = 0;
            this.tabControl_Center_L_T.Size = new System.Drawing.Size(388, 450);
            this.tabControl_Center_L_T.TabIndex = 0;
            this.tabControl_Center_L_T.SelectedIndexChanged += new System.EventHandler(this.tabControl_Center_L_T_SelectedIndexChanged);
            // 
            // tabPage_physic
            // 
            this.tabPage_physic.BackColor = System.Drawing.Color.Transparent;
            this.tabPage_physic.Controls.Add(this.TLP_Physics);
            this.tabPage_physic.Location = new System.Drawing.Point(4, 22);
            this.tabPage_physic.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage_physic.Name = "tabPage_physic";
            this.tabPage_physic.Size = new System.Drawing.Size(380, 424);
            this.tabPage_physic.TabIndex = 5;
            this.tabPage_physic.Text = "物理元素";
            this.tabPage_physic.UseVisualStyleBackColor = true;
            // 
            // TLP_Physics
            // 
            this.TLP_Physics.ColumnCount = 1;
            this.TLP_Physics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Physics.Controls.Add(this.TLP_Physics_TOP, 0, 0);
            this.TLP_Physics.Controls.Add(this.panel_Physics_Tool, 0, 1);
            this.TLP_Physics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Physics.Location = new System.Drawing.Point(0, 0);
            this.TLP_Physics.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_Physics.Name = "TLP_Physics";
            this.TLP_Physics.RowCount = 2;
            this.TLP_Physics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Physics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.TLP_Physics.Size = new System.Drawing.Size(380, 424);
            this.TLP_Physics.TabIndex = 1;
            // 
            // TLP_Physics_TOP
            // 
            this.TLP_Physics_TOP.ColumnCount = 2;
            this.TLP_Physics_TOP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Physics_TOP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.TLP_Physics_TOP.Controls.Add(this.panel_PhysicsBG, 0, 0);
            this.TLP_Physics_TOP.Controls.Add(this.vScrollBar_Physics, 1, 0);
            this.TLP_Physics_TOP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Physics_TOP.Location = new System.Drawing.Point(0, 0);
            this.TLP_Physics_TOP.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_Physics_TOP.Name = "TLP_Physics_TOP";
            this.TLP_Physics_TOP.RowCount = 1;
            this.TLP_Physics_TOP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Physics_TOP.Size = new System.Drawing.Size(380, 380);
            this.TLP_Physics_TOP.TabIndex = 1;
            // 
            // panel_PhysicsBG
            // 
            this.panel_PhysicsBG.BackColor = System.Drawing.Color.LightGray;
            this.panel_PhysicsBG.Controls.Add(this.pictureBox_Physics);
            this.panel_PhysicsBG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_PhysicsBG.Location = new System.Drawing.Point(0, 0);
            this.panel_PhysicsBG.Margin = new System.Windows.Forms.Padding(0);
            this.panel_PhysicsBG.Name = "panel_PhysicsBG";
            this.panel_PhysicsBG.Padding = new System.Windows.Forms.Padding(1);
            this.panel_PhysicsBG.Size = new System.Drawing.Size(357, 380);
            this.panel_PhysicsBG.TabIndex = 0;
            // 
            // pictureBox_Physics
            // 
            this.pictureBox_Physics.BackColor = System.Drawing.Color.White;
            this.pictureBox_Physics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Physics.Location = new System.Drawing.Point(1, 1);
            this.pictureBox_Physics.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Physics.Name = "pictureBox_Physics";
            this.pictureBox_Physics.Size = new System.Drawing.Size(355, 378);
            this.pictureBox_Physics.TabIndex = 0;
            this.pictureBox_Physics.TabStop = false;
            this.pictureBox_Physics.DoubleClick += new System.EventHandler(this.pictureBox_Physics_DoubleClick);
            this.pictureBox_Physics.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBox_Physics_PreviewKeyDown);
            this.pictureBox_Physics.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Physics_MouseDown);
            this.pictureBox_Physics.MouseEnter += new System.EventHandler(this.pictureBox_Physics_MouseEnter);
            // 
            // vScrollBar_Physics
            // 
            this.vScrollBar_Physics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar_Physics.Location = new System.Drawing.Point(357, 0);
            this.vScrollBar_Physics.Name = "vScrollBar_Physics";
            this.vScrollBar_Physics.Size = new System.Drawing.Size(23, 380);
            this.vScrollBar_Physics.TabIndex = 1;
            this.vScrollBar_Physics.ValueChanged += new System.EventHandler(this.vScrollBar_Physics_ValueChanged);
            // 
            // panel_Physics_Tool
            // 
            this.panel_Physics_Tool.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Physics_Tool.Controls.Add(this.button_config);
            this.panel_Physics_Tool.Controls.Add(this.button_AutoGen);
            this.panel_Physics_Tool.Controls.Add(this.numericUpDown_autoGen_Phy);
            this.panel_Physics_Tool.Controls.Add(this.button_del_Phy);
            this.panel_Physics_Tool.Controls.Add(this.button_add_Phy);
            this.panel_Physics_Tool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Physics_Tool.Location = new System.Drawing.Point(0, 380);
            this.panel_Physics_Tool.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Physics_Tool.Name = "panel_Physics_Tool";
            this.panel_Physics_Tool.Size = new System.Drawing.Size(380, 44);
            this.panel_Physics_Tool.TabIndex = 2;
            // 
            // button_config
            // 
            this.button_config.BackgroundImage = global::Cyclone.Properties.Resources.edit_C;
            this.button_config.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_config.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_config.Location = new System.Drawing.Point(45, 4);
            this.button_config.Margin = new System.Windows.Forms.Padding(4);
            this.button_config.Name = "button_config";
            this.button_config.Size = new System.Drawing.Size(37, 35);
            this.button_config.TabIndex = 5;
            this.button_config.UseVisualStyleBackColor = true;
            this.button_config.MouseLeave += new System.EventHandler(this.button_config_MouseLeave);
            this.button_config.Click += new System.EventHandler(this.button_config_Click);
            this.button_config.MouseHover += new System.EventHandler(this.button_config_MouseHover);
            // 
            // button_AutoGen
            // 
            this.button_AutoGen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_AutoGen.BackgroundImage = global::Cyclone.Properties.Resources.magic;
            this.button_AutoGen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_AutoGen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_AutoGen.Location = new System.Drawing.Point(98, 4);
            this.button_AutoGen.Margin = new System.Windows.Forms.Padding(4);
            this.button_AutoGen.Name = "button_AutoGen";
            this.button_AutoGen.Size = new System.Drawing.Size(37, 35);
            this.button_AutoGen.TabIndex = 4;
            this.button_AutoGen.UseVisualStyleBackColor = true;
            this.button_AutoGen.MouseLeave += new System.EventHandler(this.button_AutoGen_MouseLeave);
            this.button_AutoGen.Click += new System.EventHandler(this.button_AutoGen_Click);
            this.button_AutoGen.MouseEnter += new System.EventHandler(this.button_AutoGen_MouseEnter);
            // 
            // numericUpDown_autoGen_Phy
            // 
            this.numericUpDown_autoGen_Phy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_autoGen_Phy.Font = new System.Drawing.Font("宋体", 10F);
            this.numericUpDown_autoGen_Phy.Location = new System.Drawing.Point(143, 6);
            this.numericUpDown_autoGen_Phy.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown_autoGen_Phy.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numericUpDown_autoGen_Phy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_autoGen_Phy.Name = "numericUpDown_autoGen_Phy";
            this.numericUpDown_autoGen_Phy.Size = new System.Drawing.Size(67, 27);
            this.numericUpDown_autoGen_Phy.TabIndex = 3;
            this.numericUpDown_autoGen_Phy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button_del_Phy
            // 
            this.button_del_Phy.BackgroundImage = global::Cyclone.Properties.Resources.delete_C;
            this.button_del_Phy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_del_Phy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_del_Phy.Location = new System.Drawing.Point(321, 4);
            this.button_del_Phy.Margin = new System.Windows.Forms.Padding(4);
            this.button_del_Phy.Name = "button_del_Phy";
            this.button_del_Phy.Size = new System.Drawing.Size(37, 35);
            this.button_del_Phy.TabIndex = 2;
            this.button_del_Phy.UseVisualStyleBackColor = true;
            this.button_del_Phy.MouseLeave += new System.EventHandler(this.button_del_Phy_MouseLeave);
            this.button_del_Phy.Click += new System.EventHandler(this.button_del_Phy_Click);
            this.button_del_Phy.MouseHover += new System.EventHandler(this.button_del_Phy_MouseHover);
            // 
            // button_add_Phy
            // 
            this.button_add_Phy.BackgroundImage = global::Cyclone.Properties.Resources.addItem_C;
            this.button_add_Phy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_add_Phy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_add_Phy.Location = new System.Drawing.Point(5, 4);
            this.button_add_Phy.Margin = new System.Windows.Forms.Padding(4);
            this.button_add_Phy.Name = "button_add_Phy";
            this.button_add_Phy.Size = new System.Drawing.Size(37, 35);
            this.button_add_Phy.TabIndex = 1;
            this.button_add_Phy.UseVisualStyleBackColor = true;
            this.button_add_Phy.MouseLeave += new System.EventHandler(this.button_add_Phy_MouseLeave);
            this.button_add_Phy.Click += new System.EventHandler(this.button_add_Phy_Click);
            this.button_add_Phy.MouseHover += new System.EventHandler(this.button_add_Phy_MouseHover);
            // 
            // tabPage_Gfx
            // 
            this.tabPage_Gfx.BackColor = System.Drawing.Color.Transparent;
            this.tabPage_Gfx.Controls.Add(this.TLP_Gfx);
            this.tabPage_Gfx.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Gfx.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage_Gfx.Name = "tabPage_Gfx";
            this.tabPage_Gfx.Size = new System.Drawing.Size(380, 424);
            this.tabPage_Gfx.TabIndex = 0;
            this.tabPage_Gfx.Text = "图形元素";
            this.tabPage_Gfx.UseVisualStyleBackColor = true;
            this.tabPage_Gfx.Enter += new System.EventHandler(this.tabPage_Gfx_Enter);
            // 
            // TLP_Gfx
            // 
            this.TLP_Gfx.ColumnCount = 1;
            this.TLP_Gfx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Gfx.Controls.Add(this.TLP_Gfx_Top, 0, 1);
            this.TLP_Gfx.Controls.Add(this.panel_gfx_tool, 0, 2);
            this.TLP_Gfx.Controls.Add(this.panel_GfXGroup, 0, 0);
            this.TLP_Gfx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Gfx.Location = new System.Drawing.Point(0, 0);
            this.TLP_Gfx.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_Gfx.Name = "TLP_Gfx";
            this.TLP_Gfx.RowCount = 3;
            this.TLP_Gfx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.TLP_Gfx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Gfx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.TLP_Gfx.Size = new System.Drawing.Size(380, 424);
            this.TLP_Gfx.TabIndex = 0;
            // 
            // TLP_Gfx_Top
            // 
            this.TLP_Gfx_Top.ColumnCount = 2;
            this.TLP_Gfx_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Gfx_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.TLP_Gfx_Top.Controls.Add(this.panel_ground, 0, 0);
            this.TLP_Gfx_Top.Controls.Add(this.vScrollBar_Gfx, 1, 0);
            this.TLP_Gfx_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Gfx_Top.Location = new System.Drawing.Point(0, 44);
            this.TLP_Gfx_Top.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_Gfx_Top.Name = "TLP_Gfx_Top";
            this.TLP_Gfx_Top.RowCount = 1;
            this.TLP_Gfx_Top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Gfx_Top.Size = new System.Drawing.Size(380, 336);
            this.TLP_Gfx_Top.TabIndex = 1;
            // 
            // panel_ground
            // 
            this.panel_ground.BackColor = System.Drawing.Color.LightGray;
            this.panel_ground.Controls.Add(this.pictureBox_Gfx);
            this.panel_ground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ground.Location = new System.Drawing.Point(0, 0);
            this.panel_ground.Margin = new System.Windows.Forms.Padding(0);
            this.panel_ground.Name = "panel_ground";
            this.panel_ground.Padding = new System.Windows.Forms.Padding(1);
            this.panel_ground.Size = new System.Drawing.Size(357, 336);
            this.panel_ground.TabIndex = 0;
            // 
            // pictureBox_Gfx
            // 
            this.pictureBox_Gfx.BackColor = System.Drawing.Color.White;
            this.pictureBox_Gfx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Gfx.Location = new System.Drawing.Point(1, 1);
            this.pictureBox_Gfx.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Gfx.Name = "pictureBox_Gfx";
            this.pictureBox_Gfx.Size = new System.Drawing.Size(355, 334);
            this.pictureBox_Gfx.TabIndex = 0;
            this.pictureBox_Gfx.TabStop = false;
            this.pictureBox_Gfx.DoubleClick += new System.EventHandler(this.pictureBox_Gfx_DoubleClick);
            this.pictureBox_Gfx.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBox_Gfx_PreviewKeyDown);
            this.pictureBox_Gfx.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Gfx_MouseMove);
            this.pictureBox_Gfx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Gfx_MouseDown);
            this.pictureBox_Gfx.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Gfx_MouseUp);
            this.pictureBox_Gfx.MouseEnter += new System.EventHandler(this.pictureBox_Gfx_MouseEnter);
            // 
            // vScrollBar_Gfx
            // 
            this.vScrollBar_Gfx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar_Gfx.Location = new System.Drawing.Point(357, 0);
            this.vScrollBar_Gfx.Name = "vScrollBar_Gfx";
            this.vScrollBar_Gfx.Size = new System.Drawing.Size(23, 336);
            this.vScrollBar_Gfx.TabIndex = 1;
            this.vScrollBar_Gfx.ValueChanged += new System.EventHandler(this.vScrollBar_Gfx_ValueChanged);
            // 
            // panel_gfx_tool
            // 
            this.panel_gfx_tool.BackColor = System.Drawing.SystemColors.Control;
            this.panel_gfx_tool.Controls.Add(this.label_TileGfxUsedTime);
            this.panel_gfx_tool.Controls.Add(this.label_TileGfxID);
            this.panel_gfx_tool.Controls.Add(this.button_del_Gfx);
            this.panel_gfx_tool.Controls.Add(this.panel_Flag);
            this.panel_gfx_tool.Controls.Add(this.button_Copy_Gfx);
            this.panel_gfx_tool.Controls.Add(this.button_Right_Gfx);
            this.panel_gfx_tool.Controls.Add(this.button_addOne_Gfx);
            this.panel_gfx_tool.Controls.Add(this.button_Left_Gfx);
            this.panel_gfx_tool.Controls.Add(this.button_Down_Gfx);
            this.panel_gfx_tool.Controls.Add(this.button_Up_Gfx);
            this.panel_gfx_tool.Controls.Add(this.button_TileGfx_TransN);
            this.panel_gfx_tool.Controls.Add(this.button_TileGfx_TransP);
            this.panel_gfx_tool.Controls.Add(this.button_TileGfx_FlipV);
            this.panel_gfx_tool.Controls.Add(this.button_TileGfx_FlipH);
            this.panel_gfx_tool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_gfx_tool.Location = new System.Drawing.Point(0, 380);
            this.panel_gfx_tool.Margin = new System.Windows.Forms.Padding(0);
            this.panel_gfx_tool.Name = "panel_gfx_tool";
            this.panel_gfx_tool.Size = new System.Drawing.Size(380, 44);
            this.panel_gfx_tool.TabIndex = 2;
            // 
            // label_TileGfxUsedTime
            // 
            this.label_TileGfxUsedTime.AutoSize = true;
            this.label_TileGfxUsedTime.Location = new System.Drawing.Point(165, 24);
            this.label_TileGfxUsedTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_TileGfxUsedTime.Name = "label_TileGfxUsedTime";
            this.label_TileGfxUsedTime.Size = new System.Drawing.Size(75, 15);
            this.label_TileGfxUsedTime.TabIndex = 41;
            this.label_TileGfxUsedTime.Text = "使用次数:";
            // 
            // label_TileGfxID
            // 
            this.label_TileGfxID.AutoSize = true;
            this.label_TileGfxID.Location = new System.Drawing.Point(165, 6);
            this.label_TileGfxID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_TileGfxID.Name = "label_TileGfxID";
            this.label_TileGfxID.Size = new System.Drawing.Size(75, 15);
            this.label_TileGfxID.TabIndex = 40;
            this.label_TileGfxID.Text = "单元编号:";
            // 
            // button_del_Gfx
            // 
            this.button_del_Gfx.BackgroundImage = global::Cyclone.Properties.Resources.delete_C;
            this.button_del_Gfx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_del_Gfx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_del_Gfx.Location = new System.Drawing.Point(321, 4);
            this.button_del_Gfx.Margin = new System.Windows.Forms.Padding(4);
            this.button_del_Gfx.Name = "button_del_Gfx";
            this.button_del_Gfx.Size = new System.Drawing.Size(37, 35);
            this.button_del_Gfx.TabIndex = 6;
            this.button_del_Gfx.UseVisualStyleBackColor = true;
            this.button_del_Gfx.MouseLeave += new System.EventHandler(this.button_del_Gfx_MouseLeave);
            this.button_del_Gfx.Click += new System.EventHandler(this.button_del_Gfx_Click);
            this.button_del_Gfx.MouseHover += new System.EventHandler(this.button_del_Gfx_MouseHover);
            // 
            // panel_Flag
            // 
            this.panel_Flag.BackColor = System.Drawing.Color.Transparent;
            this.panel_Flag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel_Flag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Flag.Location = new System.Drawing.Point(283, 4);
            this.panel_Flag.Margin = new System.Windows.Forms.Padding(4);
            this.panel_Flag.Name = "panel_Flag";
            this.panel_Flag.Size = new System.Drawing.Size(37, 34);
            this.panel_Flag.TabIndex = 39;
            this.panel_Flag.MouseLeave += new System.EventHandler(this.panel_Flag_MouseLeave);
            this.panel_Flag.MouseEnter += new System.EventHandler(this.panel_Flag_MouseEnter);
            // 
            // button_Copy_Gfx
            // 
            this.button_Copy_Gfx.BackgroundImage = global::Cyclone.Properties.Resources.copy_C;
            this.button_Copy_Gfx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_Copy_Gfx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Copy_Gfx.Location = new System.Drawing.Point(44, 4);
            this.button_Copy_Gfx.Margin = new System.Windows.Forms.Padding(4);
            this.button_Copy_Gfx.Name = "button_Copy_Gfx";
            this.button_Copy_Gfx.Size = new System.Drawing.Size(37, 35);
            this.button_Copy_Gfx.TabIndex = 34;
            this.button_Copy_Gfx.Tag = "克隆当前图形元素";
            this.button_Copy_Gfx.UseVisualStyleBackColor = true;
            this.button_Copy_Gfx.MouseLeave += new System.EventHandler(this.button_Copy_Gfx_MouseLeave);
            this.button_Copy_Gfx.Click += new System.EventHandler(this.button_Copy_Gfx_Click);
            this.button_Copy_Gfx.MouseHover += new System.EventHandler(this.button_Copy_Gfx_MouseHover);
            // 
            // button_Right_Gfx
            // 
            this.button_Right_Gfx.BackgroundImage = global::Cyclone.Properties.Resources.right_C;
            this.button_Right_Gfx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_Right_Gfx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Right_Gfx.Location = new System.Drawing.Point(103, 21);
            this.button_Right_Gfx.Margin = new System.Windows.Forms.Padding(0);
            this.button_Right_Gfx.Name = "button_Right_Gfx";
            this.button_Right_Gfx.Size = new System.Drawing.Size(19, 18);
            this.button_Right_Gfx.TabIndex = 38;
            this.button_Right_Gfx.UseVisualStyleBackColor = true;
            this.button_Right_Gfx.MouseLeave += new System.EventHandler(this.button_Right_Gfx_MouseLeave);
            this.button_Right_Gfx.Click += new System.EventHandler(this.button_Right_Gfx_Click);
            this.button_Right_Gfx.MouseHover += new System.EventHandler(this.button_Right_Gfx_MouseHover);
            // 
            // button_addOne_Gfx
            // 
            this.button_addOne_Gfx.BackgroundImage = global::Cyclone.Properties.Resources.importMapElement;
            this.button_addOne_Gfx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_addOne_Gfx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_addOne_Gfx.Location = new System.Drawing.Point(5, 4);
            this.button_addOne_Gfx.Margin = new System.Windows.Forms.Padding(4);
            this.button_addOne_Gfx.Name = "button_addOne_Gfx";
            this.button_addOne_Gfx.Size = new System.Drawing.Size(37, 35);
            this.button_addOne_Gfx.TabIndex = 2;
            this.button_addOne_Gfx.UseVisualStyleBackColor = true;
            this.button_addOne_Gfx.MouseLeave += new System.EventHandler(this.button_addOne_Gfx_MouseLeave);
            this.button_addOne_Gfx.Click += new System.EventHandler(this.button_addOne_Gfx_Click);
            this.button_addOne_Gfx.MouseHover += new System.EventHandler(this.button_addOne_Gfx_MouseHover);
            // 
            // button_Left_Gfx
            // 
            this.button_Left_Gfx.BackgroundImage = global::Cyclone.Properties.Resources.left_C;
            this.button_Left_Gfx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_Left_Gfx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Left_Gfx.Location = new System.Drawing.Point(103, 4);
            this.button_Left_Gfx.Margin = new System.Windows.Forms.Padding(0);
            this.button_Left_Gfx.Name = "button_Left_Gfx";
            this.button_Left_Gfx.Size = new System.Drawing.Size(19, 18);
            this.button_Left_Gfx.TabIndex = 37;
            this.button_Left_Gfx.UseVisualStyleBackColor = true;
            this.button_Left_Gfx.MouseLeave += new System.EventHandler(this.button_Left_Gfx_MouseLeave);
            this.button_Left_Gfx.Click += new System.EventHandler(this.button_Left_Gfx_Click);
            this.button_Left_Gfx.MouseHover += new System.EventHandler(this.button_Left_Gfx_MouseHover);
            // 
            // button_Down_Gfx
            // 
            this.button_Down_Gfx.BackgroundImage = global::Cyclone.Properties.Resources.down_C;
            this.button_Down_Gfx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_Down_Gfx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Down_Gfx.Location = new System.Drawing.Point(84, 21);
            this.button_Down_Gfx.Margin = new System.Windows.Forms.Padding(0);
            this.button_Down_Gfx.Name = "button_Down_Gfx";
            this.button_Down_Gfx.Size = new System.Drawing.Size(19, 18);
            this.button_Down_Gfx.TabIndex = 36;
            this.button_Down_Gfx.UseVisualStyleBackColor = true;
            this.button_Down_Gfx.MouseLeave += new System.EventHandler(this.button_Down_Gfx_MouseLeave);
            this.button_Down_Gfx.Click += new System.EventHandler(this.button_Down_Gfx_Click);
            this.button_Down_Gfx.MouseHover += new System.EventHandler(this.button_Down_Gfx_MouseHover);
            // 
            // button_Up_Gfx
            // 
            this.button_Up_Gfx.BackgroundImage = global::Cyclone.Properties.Resources.Up_C;
            this.button_Up_Gfx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_Up_Gfx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Up_Gfx.Location = new System.Drawing.Point(84, 4);
            this.button_Up_Gfx.Margin = new System.Windows.Forms.Padding(0);
            this.button_Up_Gfx.Name = "button_Up_Gfx";
            this.button_Up_Gfx.Size = new System.Drawing.Size(19, 18);
            this.button_Up_Gfx.TabIndex = 35;
            this.button_Up_Gfx.UseVisualStyleBackColor = true;
            this.button_Up_Gfx.MouseLeave += new System.EventHandler(this.button_Up_Gfx_MouseLeave);
            this.button_Up_Gfx.Click += new System.EventHandler(this.button_Up_Gfx_Click);
            this.button_Up_Gfx.MouseHover += new System.EventHandler(this.button_Up_Gfx_MouseHover);
            // 
            // button_TileGfx_TransN
            // 
            this.button_TileGfx_TransN.BackgroundImage = global::Cyclone.Properties.Resources.rotateN_C;
            this.button_TileGfx_TransN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_TileGfx_TransN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_TileGfx_TransN.Location = new System.Drawing.Point(141, 21);
            this.button_TileGfx_TransN.Margin = new System.Windows.Forms.Padding(0);
            this.button_TileGfx_TransN.Name = "button_TileGfx_TransN";
            this.button_TileGfx_TransN.Size = new System.Drawing.Size(19, 18);
            this.button_TileGfx_TransN.TabIndex = 33;
            this.button_TileGfx_TransN.UseVisualStyleBackColor = true;
            this.button_TileGfx_TransN.MouseLeave += new System.EventHandler(this.button_Right_Gfx_MouseLeave);
            this.button_TileGfx_TransN.Click += new System.EventHandler(this.button_TileGfx_TransN_Click);
            this.button_TileGfx_TransN.MouseHover += new System.EventHandler(this.button_TileGfx_TransN_MouseHover);
            // 
            // button_TileGfx_TransP
            // 
            this.button_TileGfx_TransP.BackgroundImage = global::Cyclone.Properties.Resources.rotateP_C;
            this.button_TileGfx_TransP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_TileGfx_TransP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_TileGfx_TransP.Location = new System.Drawing.Point(123, 21);
            this.button_TileGfx_TransP.Margin = new System.Windows.Forms.Padding(0);
            this.button_TileGfx_TransP.Name = "button_TileGfx_TransP";
            this.button_TileGfx_TransP.Size = new System.Drawing.Size(19, 18);
            this.button_TileGfx_TransP.TabIndex = 32;
            this.button_TileGfx_TransP.UseVisualStyleBackColor = true;
            this.button_TileGfx_TransP.MouseLeave += new System.EventHandler(this.button_Right_Gfx_MouseLeave);
            this.button_TileGfx_TransP.Click += new System.EventHandler(this.button_TileGfx_TransP_Click);
            this.button_TileGfx_TransP.MouseHover += new System.EventHandler(this.button_TileGfx_TransP_MouseHover);
            // 
            // button_TileGfx_FlipV
            // 
            this.button_TileGfx_FlipV.BackgroundImage = global::Cyclone.Properties.Resources.flipY_C;
            this.button_TileGfx_FlipV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_TileGfx_FlipV.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_TileGfx_FlipV.Location = new System.Drawing.Point(141, 4);
            this.button_TileGfx_FlipV.Margin = new System.Windows.Forms.Padding(0);
            this.button_TileGfx_FlipV.Name = "button_TileGfx_FlipV";
            this.button_TileGfx_FlipV.Size = new System.Drawing.Size(19, 18);
            this.button_TileGfx_FlipV.TabIndex = 31;
            this.button_TileGfx_FlipV.UseVisualStyleBackColor = true;
            this.button_TileGfx_FlipV.MouseLeave += new System.EventHandler(this.button_Right_Gfx_MouseLeave);
            this.button_TileGfx_FlipV.Click += new System.EventHandler(this.button_TileGfx_FlipV_Click);
            this.button_TileGfx_FlipV.MouseHover += new System.EventHandler(this.button_TileGfx_FlipV_MouseHover);
            // 
            // button_TileGfx_FlipH
            // 
            this.button_TileGfx_FlipH.BackgroundImage = global::Cyclone.Properties.Resources.flipX_C;
            this.button_TileGfx_FlipH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_TileGfx_FlipH.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_TileGfx_FlipH.Location = new System.Drawing.Point(123, 4);
            this.button_TileGfx_FlipH.Margin = new System.Windows.Forms.Padding(0);
            this.button_TileGfx_FlipH.Name = "button_TileGfx_FlipH";
            this.button_TileGfx_FlipH.Size = new System.Drawing.Size(19, 18);
            this.button_TileGfx_FlipH.TabIndex = 30;
            this.button_TileGfx_FlipH.UseVisualStyleBackColor = true;
            this.button_TileGfx_FlipH.MouseLeave += new System.EventHandler(this.button_Right_Gfx_MouseLeave);
            this.button_TileGfx_FlipH.Click += new System.EventHandler(this.button_TileGfx_FlipH_Click);
            this.button_TileGfx_FlipH.MouseHover += new System.EventHandler(this.button_TileGfx_FlipH_MouseHover);
            // 
            // panel_GfXGroup
            // 
            this.panel_GfXGroup.BackColor = System.Drawing.SystemColors.Control;
            this.panel_GfXGroup.Controls.Add(this.button_NameFolder);
            this.panel_GfXGroup.Controls.Add(this.button_CheckContainer);
            this.panel_GfXGroup.Controls.Add(this.button_ClearSpilth);
            this.panel_GfXGroup.Controls.Add(this.comboBox_GfxType);
            this.panel_GfXGroup.Controls.Add(this.button_DelGfxFolder);
            this.panel_GfXGroup.Controls.Add(this.button_AddGfxFolder);
            this.panel_GfXGroup.Controls.Add(this.button_addMore_Grx);
            this.panel_GfXGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_GfXGroup.Location = new System.Drawing.Point(0, 0);
            this.panel_GfXGroup.Margin = new System.Windows.Forms.Padding(0);
            this.panel_GfXGroup.Name = "panel_GfXGroup";
            this.panel_GfXGroup.Size = new System.Drawing.Size(380, 44);
            this.panel_GfXGroup.TabIndex = 3;
            // 
            // button_NameFolder
            // 
            this.button_NameFolder.BackgroundImage = global::Cyclone.Properties.Resources.nameFolder;
            this.button_NameFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_NameFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_NameFolder.Location = new System.Drawing.Point(83, 4);
            this.button_NameFolder.Margin = new System.Windows.Forms.Padding(4);
            this.button_NameFolder.Name = "button_NameFolder";
            this.button_NameFolder.Size = new System.Drawing.Size(37, 35);
            this.button_NameFolder.TabIndex = 42;
            this.button_NameFolder.UseVisualStyleBackColor = true;
            this.button_NameFolder.MouseLeave += new System.EventHandler(this.button_NameFolder_MouseLeave);
            this.button_NameFolder.Click += new System.EventHandler(this.button_NameFolder_Click);
            this.button_NameFolder.MouseHover += new System.EventHandler(this.button_NameFolder_MouseHover);
            // 
            // button_CheckContainer
            // 
            this.button_CheckContainer.BackgroundImage = global::Cyclone.Properties.Resources.information;
            this.button_CheckContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_CheckContainer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_CheckContainer.Location = new System.Drawing.Point(321, 4);
            this.button_CheckContainer.Margin = new System.Windows.Forms.Padding(4);
            this.button_CheckContainer.Name = "button_CheckContainer";
            this.button_CheckContainer.Size = new System.Drawing.Size(37, 35);
            this.button_CheckContainer.TabIndex = 41;
            this.button_CheckContainer.Tag = "图形容器使用统计与错误检查";
            this.button_CheckContainer.UseVisualStyleBackColor = true;
            this.button_CheckContainer.MouseLeave += new System.EventHandler(this.button_CheckContainer_MouseLeave);
            this.button_CheckContainer.Click += new System.EventHandler(this.button_CheckContainer_Click);
            this.button_CheckContainer.MouseHover += new System.EventHandler(this.button_CheckContainer_MouseHover);
            // 
            // button_ClearSpilth
            // 
            this.button_ClearSpilth.BackgroundImage = global::Cyclone.Properties.Resources.spilth;
            this.button_ClearSpilth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_ClearSpilth.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_ClearSpilth.Location = new System.Drawing.Point(283, 4);
            this.button_ClearSpilth.Margin = new System.Windows.Forms.Padding(4);
            this.button_ClearSpilth.Name = "button_ClearSpilth";
            this.button_ClearSpilth.Size = new System.Drawing.Size(37, 35);
            this.button_ClearSpilth.TabIndex = 40;
            this.button_ClearSpilth.Tag = "清除多余的图形元素";
            this.button_ClearSpilth.UseVisualStyleBackColor = true;
            this.button_ClearSpilth.MouseLeave += new System.EventHandler(this.button_ClearSpilth_MouseLeave);
            this.button_ClearSpilth.Click += new System.EventHandler(this.button_ClearSpilth_Click);
            this.button_ClearSpilth.MouseHover += new System.EventHandler(this.button_ClearSpilth_MouseHover);
            // 
            // comboBox_GfxType
            // 
            this.comboBox_GfxType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(241)))));
            this.comboBox_GfxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_GfxType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_GfxType.FormattingEnabled = true;
            this.comboBox_GfxType.Location = new System.Drawing.Point(124, 10);
            this.comboBox_GfxType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_GfxType.Name = "comboBox_GfxType";
            this.comboBox_GfxType.Size = new System.Drawing.Size(117, 23);
            this.comboBox_GfxType.TabIndex = 4;
            this.comboBox_GfxType.SelectedIndexChanged += new System.EventHandler(this.comboBox_GfxType_SelectedIndexChanged);
            // 
            // button_DelGfxFolder
            // 
            this.button_DelGfxFolder.BackgroundImage = global::Cyclone.Properties.Resources.delFolder;
            this.button_DelGfxFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_DelGfxFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_DelGfxFolder.Location = new System.Drawing.Point(44, 4);
            this.button_DelGfxFolder.Margin = new System.Windows.Forms.Padding(4);
            this.button_DelGfxFolder.Name = "button_DelGfxFolder";
            this.button_DelGfxFolder.Size = new System.Drawing.Size(37, 35);
            this.button_DelGfxFolder.TabIndex = 3;
            this.button_DelGfxFolder.UseVisualStyleBackColor = true;
            this.button_DelGfxFolder.MouseLeave += new System.EventHandler(this.button_DelGfxFolder_MouseLeave);
            this.button_DelGfxFolder.Click += new System.EventHandler(this.button_DelGfxFolder_Click);
            this.button_DelGfxFolder.MouseHover += new System.EventHandler(this.button_DelGfxFolder_MouseHover);
            // 
            // button_AddGfxFolder
            // 
            this.button_AddGfxFolder.BackgroundImage = global::Cyclone.Properties.Resources.addFolder;
            this.button_AddGfxFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_AddGfxFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_AddGfxFolder.Location = new System.Drawing.Point(5, 4);
            this.button_AddGfxFolder.Margin = new System.Windows.Forms.Padding(4);
            this.button_AddGfxFolder.Name = "button_AddGfxFolder";
            this.button_AddGfxFolder.Size = new System.Drawing.Size(37, 35);
            this.button_AddGfxFolder.TabIndex = 2;
            this.button_AddGfxFolder.UseVisualStyleBackColor = true;
            this.button_AddGfxFolder.MouseLeave += new System.EventHandler(this.button_AddGfxFolder_MouseLeave);
            this.button_AddGfxFolder.Click += new System.EventHandler(this.button_AddGfxFolder_Click);
            this.button_AddGfxFolder.MouseHover += new System.EventHandler(this.button_AddGfxFolder_MouseHover);
            // 
            // button_addMore_Grx
            // 
            this.button_addMore_Grx.BackgroundImage = global::Cyclone.Properties.Resources.importMapElements;
            this.button_addMore_Grx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_addMore_Grx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_addMore_Grx.Location = new System.Drawing.Point(244, 4);
            this.button_addMore_Grx.Margin = new System.Windows.Forms.Padding(4);
            this.button_addMore_Grx.Name = "button_addMore_Grx";
            this.button_addMore_Grx.Size = new System.Drawing.Size(37, 35);
            this.button_addMore_Grx.TabIndex = 1;
            this.button_addMore_Grx.UseVisualStyleBackColor = true;
            this.button_addMore_Grx.MouseLeave += new System.EventHandler(this.button_addMore_Grx_MouseLeave);
            this.button_addMore_Grx.Click += new System.EventHandler(this.button_importElement_Click);
            this.button_addMore_Grx.MouseHover += new System.EventHandler(this.button_addMore_Grx_MouseHover);
            // 
            // tabPage_AT
            // 
            this.tabPage_AT.BackColor = System.Drawing.Color.Transparent;
            this.tabPage_AT.Controls.Add(this.TLP_AT);
            this.tabPage_AT.Location = new System.Drawing.Point(4, 22);
            this.tabPage_AT.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage_AT.Name = "tabPage_AT";
            this.tabPage_AT.Size = new System.Drawing.Size(380, 424);
            this.tabPage_AT.TabIndex = 3;
            this.tabPage_AT.Text = "角色原型";
            this.tabPage_AT.UseVisualStyleBackColor = true;
            // 
            // TLP_AT
            // 
            this.TLP_AT.ColumnCount = 1;
            this.TLP_AT.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_AT.Controls.Add(this.panel_ATpack, 0, 0);
            this.TLP_AT.Controls.Add(this.TLP_AT_top, 0, 1);
            this.TLP_AT.Controls.Add(this.panel_obj_tool, 0, 2);
            this.TLP_AT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_AT.Location = new System.Drawing.Point(0, 0);
            this.TLP_AT.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_AT.Name = "TLP_AT";
            this.TLP_AT.RowCount = 3;
            this.TLP_AT.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.TLP_AT.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_AT.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.TLP_AT.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TLP_AT.Size = new System.Drawing.Size(380, 424);
            this.TLP_AT.TabIndex = 1;
            // 
            // panel_ATpack
            // 
            this.panel_ATpack.BackColor = System.Drawing.SystemColors.Control;
            this.panel_ATpack.Controls.Add(this.button_ClearATSpilth);
            this.panel_ATpack.Controls.Add(this.button_ATPack_Rename);
            this.panel_ATpack.Controls.Add(this.button_checkAT);
            this.panel_ATpack.Controls.Add(this.comboBox_ATFolders);
            this.panel_ATpack.Controls.Add(this.button_obj_import);
            this.panel_ATpack.Controls.Add(this.button_ATPack_Del);
            this.panel_ATpack.Controls.Add(this.button_ATPack_Add);
            this.panel_ATpack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ATpack.Location = new System.Drawing.Point(0, 0);
            this.panel_ATpack.Margin = new System.Windows.Forms.Padding(0);
            this.panel_ATpack.Name = "panel_ATpack";
            this.panel_ATpack.Size = new System.Drawing.Size(380, 44);
            this.panel_ATpack.TabIndex = 4;
            // 
            // button_ClearATSpilth
            // 
            this.button_ClearATSpilth.BackgroundImage = global::Cyclone.Properties.Resources.spilth;
            this.button_ClearATSpilth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_ClearATSpilth.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_ClearATSpilth.Location = new System.Drawing.Point(283, 4);
            this.button_ClearATSpilth.Margin = new System.Windows.Forms.Padding(4);
            this.button_ClearATSpilth.Name = "button_ClearATSpilth";
            this.button_ClearATSpilth.Size = new System.Drawing.Size(37, 35);
            this.button_ClearATSpilth.TabIndex = 41;
            this.button_ClearATSpilth.Tag = "清除多余的图形元素";
            this.button_ClearATSpilth.UseVisualStyleBackColor = true;
            this.button_ClearATSpilth.MouseLeave += new System.EventHandler(this.button_ClearATSpilth_MouseLeave);
            this.button_ClearATSpilth.Click += new System.EventHandler(this.button_ClearATSpilth_Click);
            this.button_ClearATSpilth.MouseHover += new System.EventHandler(this.button_ClearATSpilth_MouseHover);
            // 
            // button_ATPack_Rename
            // 
            this.button_ATPack_Rename.BackgroundImage = global::Cyclone.Properties.Resources.nameFolder;
            this.button_ATPack_Rename.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_ATPack_Rename.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_ATPack_Rename.Location = new System.Drawing.Point(83, 4);
            this.button_ATPack_Rename.Margin = new System.Windows.Forms.Padding(4);
            this.button_ATPack_Rename.Name = "button_ATPack_Rename";
            this.button_ATPack_Rename.Size = new System.Drawing.Size(37, 35);
            this.button_ATPack_Rename.TabIndex = 42;
            this.button_ATPack_Rename.UseVisualStyleBackColor = true;
            this.button_ATPack_Rename.Click += new System.EventHandler(this.button_ATPack_Rename_Click);
            // 
            // button_checkAT
            // 
            this.button_checkAT.BackgroundImage = global::Cyclone.Properties.Resources.information;
            this.button_checkAT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_checkAT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_checkAT.Location = new System.Drawing.Point(321, 4);
            this.button_checkAT.Margin = new System.Windows.Forms.Padding(4);
            this.button_checkAT.Name = "button_checkAT";
            this.button_checkAT.Size = new System.Drawing.Size(37, 35);
            this.button_checkAT.TabIndex = 41;
            this.button_checkAT.Tag = "图形容器使用统计与错误检查";
            this.button_checkAT.UseVisualStyleBackColor = true;
            // 
            // comboBox_ATFolders
            // 
            this.comboBox_ATFolders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(241)))));
            this.comboBox_ATFolders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ATFolders.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_ATFolders.FormattingEnabled = true;
            this.comboBox_ATFolders.Location = new System.Drawing.Point(124, 10);
            this.comboBox_ATFolders.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_ATFolders.Name = "comboBox_ATFolders";
            this.comboBox_ATFolders.Size = new System.Drawing.Size(117, 23);
            this.comboBox_ATFolders.TabIndex = 4;
            this.comboBox_ATFolders.SelectedIndexChanged += new System.EventHandler(this.comboBox_ATFolders_SelectedIndexChanged);
            // 
            // button_obj_import
            // 
            this.button_obj_import.BackgroundImage = global::Cyclone.Properties.Resources.anim_import;
            this.button_obj_import.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_obj_import.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_obj_import.Location = new System.Drawing.Point(244, 4);
            this.button_obj_import.Margin = new System.Windows.Forms.Padding(4);
            this.button_obj_import.Name = "button_obj_import";
            this.button_obj_import.Size = new System.Drawing.Size(37, 35);
            this.button_obj_import.TabIndex = 1;
            this.button_obj_import.UseVisualStyleBackColor = true;
            this.button_obj_import.MouseLeave += new System.EventHandler(this.button_obj_import_MouseLeave);
            this.button_obj_import.Click += new System.EventHandler(this.button_AT_import_Click);
            this.button_obj_import.MouseHover += new System.EventHandler(this.button_obj_import_MouseHover);
            // 
            // button_ATPack_Del
            // 
            this.button_ATPack_Del.BackgroundImage = global::Cyclone.Properties.Resources.delFolder;
            this.button_ATPack_Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_ATPack_Del.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_ATPack_Del.Location = new System.Drawing.Point(44, 4);
            this.button_ATPack_Del.Margin = new System.Windows.Forms.Padding(4);
            this.button_ATPack_Del.Name = "button_ATPack_Del";
            this.button_ATPack_Del.Size = new System.Drawing.Size(37, 35);
            this.button_ATPack_Del.TabIndex = 3;
            this.button_ATPack_Del.UseVisualStyleBackColor = true;
            this.button_ATPack_Del.Click += new System.EventHandler(this.button_ATPack_Del_Click);
            // 
            // button_ATPack_Add
            // 
            this.button_ATPack_Add.BackgroundImage = global::Cyclone.Properties.Resources.addFolder;
            this.button_ATPack_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_ATPack_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_ATPack_Add.Location = new System.Drawing.Point(5, 4);
            this.button_ATPack_Add.Margin = new System.Windows.Forms.Padding(4);
            this.button_ATPack_Add.Name = "button_ATPack_Add";
            this.button_ATPack_Add.Size = new System.Drawing.Size(37, 35);
            this.button_ATPack_Add.TabIndex = 2;
            this.button_ATPack_Add.UseVisualStyleBackColor = true;
            this.button_ATPack_Add.Click += new System.EventHandler(this.button_ATPack_Add_Click);
            // 
            // TLP_AT_top
            // 
            this.TLP_AT_top.ColumnCount = 2;
            this.TLP_AT_top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_AT_top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.TLP_AT_top.Controls.Add(this.panel_AT, 0, 0);
            this.TLP_AT_top.Controls.Add(this.vScrollBar_AT, 1, 0);
            this.TLP_AT_top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_AT_top.Location = new System.Drawing.Point(0, 44);
            this.TLP_AT_top.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_AT_top.Name = "TLP_AT_top";
            this.TLP_AT_top.RowCount = 1;
            this.TLP_AT_top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_AT_top.Size = new System.Drawing.Size(380, 336);
            this.TLP_AT_top.TabIndex = 1;
            // 
            // panel_AT
            // 
            this.panel_AT.BackColor = System.Drawing.Color.LightGray;
            this.panel_AT.Controls.Add(this.pictureBox_AT);
            this.panel_AT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_AT.Location = new System.Drawing.Point(0, 0);
            this.panel_AT.Margin = new System.Windows.Forms.Padding(0);
            this.panel_AT.Name = "panel_AT";
            this.panel_AT.Padding = new System.Windows.Forms.Padding(1);
            this.panel_AT.Size = new System.Drawing.Size(357, 336);
            this.panel_AT.TabIndex = 0;
            // 
            // pictureBox_AT
            // 
            this.pictureBox_AT.BackColor = System.Drawing.Color.White;
            this.pictureBox_AT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_AT.Location = new System.Drawing.Point(1, 1);
            this.pictureBox_AT.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_AT.Name = "pictureBox_AT";
            this.pictureBox_AT.Size = new System.Drawing.Size(355, 334);
            this.pictureBox_AT.TabIndex = 0;
            this.pictureBox_AT.TabStop = false;
            this.pictureBox_AT.DoubleClick += new System.EventHandler(this.pictureBox_AT_DoubleClick);
            this.pictureBox_AT.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBox_AT_PreviewKeyDown);
            this.pictureBox_AT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_AT_MouseDown);
            this.pictureBox_AT.MouseEnter += new System.EventHandler(this.pictureBox_AT_MouseEnter);
            // 
            // vScrollBar_AT
            // 
            this.vScrollBar_AT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar_AT.Location = new System.Drawing.Point(357, 0);
            this.vScrollBar_AT.Maximum = 5000;
            this.vScrollBar_AT.Name = "vScrollBar_AT";
            this.vScrollBar_AT.Size = new System.Drawing.Size(23, 336);
            this.vScrollBar_AT.SmallChange = 10;
            this.vScrollBar_AT.TabIndex = 1;
            this.vScrollBar_AT.ValueChanged += new System.EventHandler(this.vScrollBar_AT_ValueChanged);
            // 
            // panel_obj_tool
            // 
            this.panel_obj_tool.BackColor = System.Drawing.SystemColors.Control;
            this.panel_obj_tool.Controls.Add(this.button_GenIDs);
            this.panel_obj_tool.Controls.Add(this.button_cloneAnteType);
            this.panel_obj_tool.Controls.Add(this.button_configAT);
            this.panel_obj_tool.Controls.Add(this.lable_AT);
            this.panel_obj_tool.Controls.Add(this.button_Obj_del);
            this.panel_obj_tool.Controls.Add(this.button_obj_refresh);
            this.panel_obj_tool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_obj_tool.Location = new System.Drawing.Point(0, 380);
            this.panel_obj_tool.Margin = new System.Windows.Forms.Padding(0);
            this.panel_obj_tool.Name = "panel_obj_tool";
            this.panel_obj_tool.Size = new System.Drawing.Size(380, 44);
            this.panel_obj_tool.TabIndex = 2;
            // 
            // button_GenIDs
            // 
            this.button_GenIDs.BackgroundImage = global::Cyclone.Properties.Resources.es;
            this.button_GenIDs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_GenIDs.Location = new System.Drawing.Point(283, 4);
            this.button_GenIDs.Margin = new System.Windows.Forms.Padding(4);
            this.button_GenIDs.Name = "button_GenIDs";
            this.button_GenIDs.Size = new System.Drawing.Size(37, 35);
            this.button_GenIDs.TabIndex = 36;
            this.button_GenIDs.Tag = "克隆当前角色原型";
            this.button_GenIDs.UseVisualStyleBackColor = true;
            this.button_GenIDs.MouseLeave += new System.EventHandler(this.button_GenIDs_MouseLeave);
            this.button_GenIDs.Click += new System.EventHandler(this.button_GenIDs_Click);
            this.button_GenIDs.MouseHover += new System.EventHandler(this.button_GenIDs_MouseHover);
            // 
            // button_cloneAnteType
            // 
            this.button_cloneAnteType.BackgroundImage = global::Cyclone.Properties.Resources.copy_C;
            this.button_cloneAnteType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_cloneAnteType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_cloneAnteType.Location = new System.Drawing.Point(44, 4);
            this.button_cloneAnteType.Margin = new System.Windows.Forms.Padding(4);
            this.button_cloneAnteType.Name = "button_cloneAnteType";
            this.button_cloneAnteType.Size = new System.Drawing.Size(37, 35);
            this.button_cloneAnteType.TabIndex = 35;
            this.button_cloneAnteType.Tag = "克隆当前角色原型";
            this.button_cloneAnteType.UseVisualStyleBackColor = true;
            this.button_cloneAnteType.MouseLeave += new System.EventHandler(this.button_cloneAnteType_MouseLeave);
            this.button_cloneAnteType.Click += new System.EventHandler(this.button_cloneAnteType_Click);
            this.button_cloneAnteType.MouseHover += new System.EventHandler(this.button_cloneAnteType_MouseHover);
            // 
            // button_configAT
            // 
            this.button_configAT.BackgroundImage = global::Cyclone.Properties.Resources.edit_C;
            this.button_configAT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_configAT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_configAT.Location = new System.Drawing.Point(5, 4);
            this.button_configAT.Margin = new System.Windows.Forms.Padding(4);
            this.button_configAT.Name = "button_configAT";
            this.button_configAT.Size = new System.Drawing.Size(37, 35);
            this.button_configAT.TabIndex = 6;
            this.button_configAT.UseVisualStyleBackColor = true;
            this.button_configAT.MouseLeave += new System.EventHandler(this.button_configAT_MouseLeave);
            this.button_configAT.Click += new System.EventHandler(this.button_configAT_Click);
            this.button_configAT.MouseHover += new System.EventHandler(this.button_configAT_MouseHover);
            // 
            // lable_AT
            // 
            this.lable_AT.AutoSize = true;
            this.lable_AT.Location = new System.Drawing.Point(125, 14);
            this.lable_AT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lable_AT.Name = "lable_AT";
            this.lable_AT.Size = new System.Drawing.Size(45, 15);
            this.lable_AT.TabIndex = 5;
            this.lable_AT.Text = "角色:";
            // 
            // button_Obj_del
            // 
            this.button_Obj_del.BackgroundImage = global::Cyclone.Properties.Resources.delete_C;
            this.button_Obj_del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_Obj_del.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Obj_del.Location = new System.Drawing.Point(321, 4);
            this.button_Obj_del.Margin = new System.Windows.Forms.Padding(4);
            this.button_Obj_del.Name = "button_Obj_del";
            this.button_Obj_del.Size = new System.Drawing.Size(37, 35);
            this.button_Obj_del.TabIndex = 4;
            this.button_Obj_del.UseVisualStyleBackColor = true;
            this.button_Obj_del.MouseLeave += new System.EventHandler(this.button_Obj_del_MouseLeave);
            this.button_Obj_del.Click += new System.EventHandler(this.button_Obj_del_Click);
            this.button_Obj_del.MouseHover += new System.EventHandler(this.button_Obj_Del_MouseHover);
            // 
            // button_obj_refresh
            // 
            this.button_obj_refresh.BackgroundImage = global::Cyclone.Properties.Resources.anim_refresh;
            this.button_obj_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_obj_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_obj_refresh.Location = new System.Drawing.Point(83, 4);
            this.button_obj_refresh.Margin = new System.Windows.Forms.Padding(4);
            this.button_obj_refresh.Name = "button_obj_refresh";
            this.button_obj_refresh.Size = new System.Drawing.Size(37, 35);
            this.button_obj_refresh.TabIndex = 2;
            this.button_obj_refresh.UseVisualStyleBackColor = true;
            this.button_obj_refresh.MouseLeave += new System.EventHandler(this.button_obj_refresh_MouseLeave);
            this.button_obj_refresh.Click += new System.EventHandler(this.button_AT_refresh_Click);
            this.button_obj_refresh.MouseHover += new System.EventHandler(this.button_obj_refresh_MouseHover);
            // 
            // tabPage_Scripts
            // 
            this.tabPage_Scripts.BackColor = System.Drawing.Color.Transparent;
            this.tabPage_Scripts.Controls.Add(this.SC_BG);
            this.tabPage_Scripts.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Scripts.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage_Scripts.Name = "tabPage_Scripts";
            this.tabPage_Scripts.Size = new System.Drawing.Size(380, 424);
            this.tabPage_Scripts.TabIndex = 6;
            this.tabPage_Scripts.Text = "脚本设置";
            this.tabPage_Scripts.UseVisualStyleBackColor = true;
            this.tabPage_Scripts.Enter += new System.EventHandler(this.tabPage_Scripts_Enter);
            // 
            // SC_BG
            // 
            this.SC_BG.BackColor = System.Drawing.SystemColors.Control;
            this.SC_BG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SC_BG.Location = new System.Drawing.Point(0, 0);
            this.SC_BG.Margin = new System.Windows.Forms.Padding(4);
            this.SC_BG.Name = "SC_BG";
            // 
            // SC_BG.Panel1
            // 
            this.SC_BG.Panel1.Controls.Add(this.groupBox_lofter);
            // 
            // SC_BG.Panel2
            // 
            this.SC_BG.Panel2.Controls.Add(this.groupBox_threads);
            this.SC_BG.Size = new System.Drawing.Size(380, 424);
            this.SC_BG.SplitterDistance = 183;
            this.SC_BG.SplitterWidth = 11;
            this.SC_BG.TabIndex = 1;
            // 
            // groupBox_lofter
            // 
            this.groupBox_lofter.Controls.Add(this.listBox_Carrier);
            this.groupBox_lofter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_lofter.Location = new System.Drawing.Point(0, 0);
            this.groupBox_lofter.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_lofter.Name = "groupBox_lofter";
            this.groupBox_lofter.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_lofter.Size = new System.Drawing.Size(183, 424);
            this.groupBox_lofter.TabIndex = 1;
            this.groupBox_lofter.TabStop = false;
            this.groupBox_lofter.Text = "载体列表";
            // 
            // listBox_Carrier
            // 
            this.listBox_Carrier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Carrier.FormattingEnabled = true;
            this.listBox_Carrier.ItemHeight = 15;
            this.listBox_Carrier.Location = new System.Drawing.Point(4, 22);
            this.listBox_Carrier.Margin = new System.Windows.Forms.Padding(4);
            this.listBox_Carrier.Name = "listBox_Carrier";
            this.listBox_Carrier.Size = new System.Drawing.Size(175, 394);
            this.listBox_Carrier.TabIndex = 0;
            this.listBox_Carrier.SelectedIndexChanged += new System.EventHandler(this.kss_listBox_Carrier_SelectedIndexChanged);
            this.listBox_Carrier.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_Carrier_MouseDown);
            // 
            // groupBox_threads
            // 
            this.groupBox_threads.Controls.Add(this.listBox_Files);
            this.groupBox_threads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_threads.Location = new System.Drawing.Point(0, 0);
            this.groupBox_threads.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_threads.Name = "groupBox_threads";
            this.groupBox_threads.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_threads.Size = new System.Drawing.Size(186, 424);
            this.groupBox_threads.TabIndex = 0;
            this.groupBox_threads.TabStop = false;
            this.groupBox_threads.Text = "线程列表";
            // 
            // listBox_Files
            // 
            this.listBox_Files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Files.FormattingEnabled = true;
            this.listBox_Files.ItemHeight = 15;
            this.listBox_Files.Location = new System.Drawing.Point(4, 22);
            this.listBox_Files.Margin = new System.Windows.Forms.Padding(4);
            this.listBox_Files.Name = "listBox_Files";
            this.listBox_Files.Size = new System.Drawing.Size(178, 394);
            this.listBox_Files.TabIndex = 1;
            this.listBox_Files.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.kss_listBox_Files_MouseDoubleClick);
            this.listBox_Files.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kss_listBox_Files_MouseDown);
            this.listBox_Files.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kss_listBox_Files_KeyDown);
            // 
            // tableLayoutPanel_Canvas
            // 
            this.tableLayoutPanel_Canvas.ColumnCount = 2;
            this.tableLayoutPanel_Canvas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Canvas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel_Canvas.Controls.Add(this.panel_Canvas, 0, 0);
            this.tableLayoutPanel_Canvas.Controls.Add(this.vScrollBar_Canvas, 1, 0);
            this.tableLayoutPanel_Canvas.Controls.Add(this.hScrollBar_Canvas, 0, 1);
            this.tableLayoutPanel_Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Canvas.Location = new System.Drawing.Point(400, 4);
            this.tableLayoutPanel_Canvas.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel_Canvas.Name = "tableLayoutPanel_Canvas";
            this.tableLayoutPanel_Canvas.RowCount = 2;
            this.tableLayoutPanel_Canvas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Canvas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel_Canvas.Size = new System.Drawing.Size(857, 643);
            this.tableLayoutPanel_Canvas.TabIndex = 4;
            // 
            // panel_Canvas
            // 
            this.panel_Canvas.AutoScroll = true;
            this.panel_Canvas.BackColor = System.Drawing.Color.Silver;
            this.panel_Canvas.Controls.Add(this.pictureBox_Canvas);
            this.panel_Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Canvas.Location = new System.Drawing.Point(0, 0);
            this.panel_Canvas.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Canvas.Name = "panel_Canvas";
            this.panel_Canvas.Size = new System.Drawing.Size(834, 622);
            this.panel_Canvas.TabIndex = 3;
            // 
            // pictureBox_Canvas
            // 
            this.pictureBox_Canvas.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Canvas.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Canvas.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_Canvas.Name = "pictureBox_Canvas";
            this.pictureBox_Canvas.Size = new System.Drawing.Size(834, 622);
            this.pictureBox_Canvas.TabIndex = 0;
            this.pictureBox_Canvas.TabStop = false;
            this.pictureBox_Canvas.MouseLeave += new System.EventHandler(this.pictureBox_Canvas_MouseLeave);
            this.pictureBox_Canvas.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBox_Canvas_PreviewKeyDown);
            this.pictureBox_Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Canvas_MouseMove);
            this.pictureBox_Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Canvas_MouseDown);
            this.pictureBox_Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Canvas_MouseUp);
            this.pictureBox_Canvas.MouseEnter += new System.EventHandler(this.pictureBox_Canvas_MouseEnter);
            // 
            // vScrollBar_Canvas
            // 
            this.vScrollBar_Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar_Canvas.Location = new System.Drawing.Point(834, 0);
            this.vScrollBar_Canvas.Name = "vScrollBar_Canvas";
            this.vScrollBar_Canvas.Size = new System.Drawing.Size(23, 622);
            this.vScrollBar_Canvas.TabIndex = 4;
            this.vScrollBar_Canvas.ValueChanged += new System.EventHandler(this.vScrollBar_Canvas_ValueChanged);
            // 
            // hScrollBar_Canvas
            // 
            this.hScrollBar_Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hScrollBar_Canvas.Location = new System.Drawing.Point(0, 622);
            this.hScrollBar_Canvas.Name = "hScrollBar_Canvas";
            this.hScrollBar_Canvas.Size = new System.Drawing.Size(834, 21);
            this.hScrollBar_Canvas.TabIndex = 5;
            this.hScrollBar_Canvas.ValueChanged += new System.EventHandler(this.hScrollBar_Canvas_ValueChanged);
            // 
            // toolStrip_Operate
            // 
            this.toolStrip_Operate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.toolStrip_Operate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip_Operate.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip_Operate.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip_Operate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSB_open,
            this.TSB_save,
            this.toolStripSeparator1,
            this.TSB_levelPhysic,
            this.TSB_level_Ground,
            this.TSB_level_Surface,
            this.TSB_level_Tile_Obj,
            this.TSB_level_Obj_Mask,
            this.toolStripSeparator12,
            this.TSB_level_Object,
            this.toolStripSeparator6,
            this.TSB_levelActors_Eye,
            this.toolStripSeparator2,
            this.TSB_pencil,
            this.TSB_rectSelect,
            this.TSB_pointSelect,
            this.TSB_Straw,
            this.TSB_FillIn,
            this.toolStripSeparator4,
            this.TSB_autoTile,
            this.TSB_fillCorner,
            this.toolStripSeparator3,
            this.TSB_Copy,
            this.TSB_Paste,
            this.toolStripSeparator5,
            this.TSB_Undo,
            this.TSB_Redo,
            this.toolStripSeparator10,
            this.TSB_findAT_Pre,
            this.TSB_findAT_Next,
            this.TSB_Replace,
            this.toolStripSeparator7,
            this.TSB_Refresh,
            this.TSB_ErrorCheck,
            this.toolStripTextBox1,
            this.toolStripLabel1});
            this.toolStrip_Operate.Location = new System.Drawing.Point(0, 31);
            this.toolStrip_Operate.Name = "toolStrip_Operate";
            this.toolStrip_Operate.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.toolStrip_Operate.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip_Operate.Size = new System.Drawing.Size(1267, 31);
            this.toolStrip_Operate.TabIndex = 0;
            this.toolStrip_Operate.Text = "toolStrip_operate";
            // 
            // TSB_open
            // 
            this.TSB_open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_open.Image = global::Cyclone.Properties.Resources.openHS;
            this.TSB_open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_open.Name = "TSB_open";
            this.TSB_open.Size = new System.Drawing.Size(23, 28);
            this.TSB_open.Text = "打开工程";
            this.TSB_open.Click += new System.EventHandler(this.TSB_open_Click);
            // 
            // TSB_save
            // 
            this.TSB_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_save.Image = global::Cyclone.Properties.Resources.saveHS;
            this.TSB_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_save.Name = "TSB_save";
            this.TSB_save.Size = new System.Drawing.Size(23, 28);
            this.TSB_save.Text = "保存工程";
            this.TSB_save.Click += new System.EventHandler(this.TSB_save_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // TSB_levelPhysic
            // 
            this.TSB_levelPhysic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.TSB_levelPhysic.Checked = true;
            this.TSB_levelPhysic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSB_levelPhysic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_levelPhysic.Image = global::Cyclone.Properties.Resources.level1;
            this.TSB_levelPhysic.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_levelPhysic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_levelPhysic.Name = "TSB_levelPhysic";
            this.TSB_levelPhysic.Size = new System.Drawing.Size(23, 28);
            this.TSB_levelPhysic.Text = "物理标记层";
            this.TSB_levelPhysic.Click += new System.EventHandler(this.TSB_levelPhysic_Click);
            // 
            // TSB_level_Ground
            // 
            this.TSB_level_Ground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.TSB_level_Ground.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_level_Ground.Image = global::Cyclone.Properties.Resources.level2;
            this.TSB_level_Ground.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_level_Ground.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_level_Ground.Name = "TSB_level_Ground";
            this.TSB_level_Ground.Size = new System.Drawing.Size(23, 28);
            this.TSB_level_Ground.Text = "底层地形层";
            this.TSB_level_Ground.Click += new System.EventHandler(this.TSB_levelGround_Click);
            // 
            // TSB_level_Surface
            // 
            this.TSB_level_Surface.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.TSB_level_Surface.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_level_Surface.Image = global::Cyclone.Properties.Resources.level3;
            this.TSB_level_Surface.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_level_Surface.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_level_Surface.Name = "TSB_level_Surface";
            this.TSB_level_Surface.Size = new System.Drawing.Size(23, 28);
            this.TSB_level_Surface.Text = "融合地形层";
            this.TSB_level_Surface.Click += new System.EventHandler(this.TSB_levelSurface_Click);
            // 
            // TSB_level_Tile_Obj
            // 
            this.TSB_level_Tile_Obj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.TSB_level_Tile_Obj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_level_Tile_Obj.Image = global::Cyclone.Properties.Resources.level4;
            this.TSB_level_Tile_Obj.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_level_Tile_Obj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_level_Tile_Obj.Name = "TSB_level_Tile_Obj";
            this.TSB_level_Tile_Obj.Size = new System.Drawing.Size(23, 28);
            this.TSB_level_Tile_Obj.Text = "对象地形层";
            this.TSB_level_Tile_Obj.Click += new System.EventHandler(this.TSB_levelObjectMask_Click);
            // 
            // TSB_level_Obj_Mask
            // 
            this.TSB_level_Obj_Mask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_level_Obj_Mask.Image = global::Cyclone.Properties.Resources.level5;
            this.TSB_level_Obj_Mask.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_level_Obj_Mask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_level_Obj_Mask.Name = "TSB_level_Obj_Mask";
            this.TSB_level_Obj_Mask.Size = new System.Drawing.Size(23, 28);
            this.TSB_level_Obj_Mask.Text = "无关对象层";
            this.TSB_level_Obj_Mask.Click += new System.EventHandler(this.TSB_levelGroundObject_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 31);
            // 
            // TSB_level_Object
            // 
            this.TSB_level_Object.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.TSB_level_Object.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_level_Object.Image = global::Cyclone.Properties.Resources.level6;
            this.TSB_level_Object.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_level_Object.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_level_Object.Name = "TSB_level_Object";
            this.TSB_level_Object.Size = new System.Drawing.Size(23, 28);
            this.TSB_level_Object.Text = "角色事件层";
            this.TSB_level_Object.Click += new System.EventHandler(this.TSB_levelActor_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // TSB_levelActors_Eye
            // 
            this.TSB_levelActors_Eye.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_levelActors_Eye.Image = global::Cyclone.Properties.Resources.eyeOn;
            this.TSB_levelActors_Eye.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_levelActors_Eye.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_levelActors_Eye.Name = "TSB_levelActors_Eye";
            this.TSB_levelActors_Eye.Size = new System.Drawing.Size(23, 28);
            this.TSB_levelActors_Eye.Text = "显示单层或全部";
            this.TSB_levelActors_Eye.Click += new System.EventHandler(this.TSB_levelActors_Eye_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // TSB_pencil
            // 
            this.TSB_pencil.Checked = true;
            this.TSB_pencil.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TSB_pencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_pencil.Image = global::Cyclone.Properties.Resources.pencil;
            this.TSB_pencil.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_pencil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_pencil.Name = "TSB_pencil";
            this.TSB_pencil.Size = new System.Drawing.Size(23, 28);
            this.TSB_pencil.Text = "铅笔(B)";
            this.TSB_pencil.Click += new System.EventHandler(this.TSB_pencil_Click);
            // 
            // TSB_rectSelect
            // 
            this.TSB_rectSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_rectSelect.Image = global::Cyclone.Properties.Resources.select;
            this.TSB_rectSelect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_rectSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_rectSelect.Name = "TSB_rectSelect";
            this.TSB_rectSelect.Size = new System.Drawing.Size(23, 28);
            this.TSB_rectSelect.Text = "框选(M)";
            this.TSB_rectSelect.Click += new System.EventHandler(this.TSB_rectSelect_Click);
            // 
            // TSB_pointSelect
            // 
            this.TSB_pointSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_pointSelect.Image = global::Cyclone.Properties.Resources.eraser;
            this.TSB_pointSelect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_pointSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_pointSelect.Name = "TSB_pointSelect";
            this.TSB_pointSelect.Size = new System.Drawing.Size(23, 28);
            this.TSB_pointSelect.Text = "橡皮(E)";
            this.TSB_pointSelect.Click += new System.EventHandler(this.TSB_erase_Click);
            // 
            // TSB_Straw
            // 
            this.TSB_Straw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Straw.Image = global::Cyclone.Properties.Resources.straw;
            this.TSB_Straw.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_Straw.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_Straw.Name = "TSB_Straw";
            this.TSB_Straw.Size = new System.Drawing.Size(23, 28);
            this.TSB_Straw.Text = "吸管(I)";
            this.TSB_Straw.Click += new System.EventHandler(this.TSB_Straw_Click);
            // 
            // TSB_FillIn
            // 
            this.TSB_FillIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_FillIn.Image = global::Cyclone.Properties.Resources.fillIn;
            this.TSB_FillIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_FillIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_FillIn.Name = "TSB_FillIn";
            this.TSB_FillIn.Size = new System.Drawing.Size(23, 28);
            this.TSB_FillIn.Text = "填充";
            this.TSB_FillIn.Click += new System.EventHandler(this.TSB_FillIn_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // TSB_autoTile
            // 
            this.TSB_autoTile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_autoTile.Image = global::Cyclone.Properties.Resources.autoTile;
            this.TSB_autoTile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_autoTile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_autoTile.Name = "TSB_autoTile";
            this.TSB_autoTile.Size = new System.Drawing.Size(23, 28);
            this.TSB_autoTile.Text = "启用自动地形";
            this.TSB_autoTile.Click += new System.EventHandler(this.TSB_autoTile_Click);
            // 
            // TSB_fillCorner
            // 
            this.TSB_fillCorner.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_fillCorner.Image = global::Cyclone.Properties.Resources.fillCorner;
            this.TSB_fillCorner.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_fillCorner.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_fillCorner.Name = "TSB_fillCorner";
            this.TSB_fillCorner.Size = new System.Drawing.Size(23, 28);
            this.TSB_fillCorner.Text = "斜角自动补齐(F)";
            this.TSB_fillCorner.Click += new System.EventHandler(this.TSB_fillCorner_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // TSB_Copy
            // 
            this.TSB_Copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Copy.Image = global::Cyclone.Properties.Resources.CopyHS;
            this.TSB_Copy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_Copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_Copy.Name = "TSB_Copy";
            this.TSB_Copy.Size = new System.Drawing.Size(23, 28);
            this.TSB_Copy.Text = "复制";
            this.TSB_Copy.Click += new System.EventHandler(this.TSB_Copy_Click);
            // 
            // TSB_Paste
            // 
            this.TSB_Paste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Paste.Image = global::Cyclone.Properties.Resources.PasteHS;
            this.TSB_Paste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_Paste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_Paste.Name = "TSB_Paste";
            this.TSB_Paste.Size = new System.Drawing.Size(23, 28);
            this.TSB_Paste.Text = "粘贴";
            this.TSB_Paste.Click += new System.EventHandler(this.TSB_Paste_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // TSB_Undo
            // 
            this.TSB_Undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Undo.Enabled = false;
            this.TSB_Undo.Image = global::Cyclone.Properties.Resources.undo;
            this.TSB_Undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_Undo.Name = "TSB_Undo";
            this.TSB_Undo.Size = new System.Drawing.Size(23, 28);
            this.TSB_Undo.Text = "撤销";
            this.TSB_Undo.Click += new System.EventHandler(this.TSB_Undo_Click);
            // 
            // TSB_Redo
            // 
            this.TSB_Redo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Redo.Enabled = false;
            this.TSB_Redo.Image = global::Cyclone.Properties.Resources.redo;
            this.TSB_Redo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_Redo.Name = "TSB_Redo";
            this.TSB_Redo.Size = new System.Drawing.Size(23, 28);
            this.TSB_Redo.Text = "重做";
            this.TSB_Redo.Click += new System.EventHandler(this.TSB_Redo_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 31);
            // 
            // TSB_findAT_Pre
            // 
            this.TSB_findAT_Pre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_findAT_Pre.Image = global::Cyclone.Properties.Resources.FindPreHS;
            this.TSB_findAT_Pre.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_findAT_Pre.Name = "TSB_findAT_Pre";
            this.TSB_findAT_Pre.Size = new System.Drawing.Size(23, 28);
            this.TSB_findAT_Pre.Text = "查找前一单元";
            // 
            // TSB_findAT_Next
            // 
            this.TSB_findAT_Next.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_findAT_Next.Image = global::Cyclone.Properties.Resources.FindNextHS;
            this.TSB_findAT_Next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_findAT_Next.Name = "TSB_findAT_Next";
            this.TSB_findAT_Next.Size = new System.Drawing.Size(23, 28);
            this.TSB_findAT_Next.Text = "查找下一单元";
            // 
            // TSB_Replace
            // 
            this.TSB_Replace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Replace.Image = global::Cyclone.Properties.Resources.replaceAT;
            this.TSB_Replace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_Replace.Name = "TSB_Replace";
            this.TSB_Replace.Size = new System.Drawing.Size(23, 28);
            this.TSB_Replace.Text = "替换所有同类单元";
            this.TSB_Replace.Click += new System.EventHandler(this.TSB_Replace_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
            // 
            // TSB_Refresh
            // 
            this.TSB_Refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Refresh.Image = global::Cyclone.Properties.Resources.refreshScreen;
            this.TSB_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_Refresh.Name = "TSB_Refresh";
            this.TSB_Refresh.Size = new System.Drawing.Size(23, 28);
            this.TSB_Refresh.Text = "刷新对象层";
            this.TSB_Refresh.Click += new System.EventHandler(this.TSB_Refresh_Click);
            // 
            // TSB_ErrorCheck
            // 
            this.TSB_ErrorCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_ErrorCheck.Image = global::Cyclone.Properties.Resources.bug;
            this.TSB_ErrorCheck.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_ErrorCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_ErrorCheck.Name = "TSB_ErrorCheck";
            this.TSB_ErrorCheck.Size = new System.Drawing.Size(23, 28);
            this.TSB_ErrorCheck.Text = "使用统计与错误检查";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.toolStripTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(133, 31);
            this.toolStripTextBox1.Text = "2013.3.14";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.IsLink = true;
            this.toolStripLabel1.LinkColor = System.Drawing.Color.Green;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(161, 28);
            this.toolStripLabel1.Text = "访问官网：Cyclone2d";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // contextMenuStrip_listItem
            // 
            this.contextMenuStrip_listItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.克隆toolStripMenuItem,
            this.上移ToolStripMenuItem,
            this.下移ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.设置双击条目ToolStripMenuItem});
            this.contextMenuStrip_listItem.Name = "contextMenuStrip_actor";
            this.contextMenuStrip_listItem.ShowCheckMargin = true;
            this.contextMenuStrip_listItem.ShowImageMargin = false;
            this.contextMenuStrip_listItem.Size = new System.Drawing.Size(179, 148);
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.新建ToolStripMenuItem.Text = "新建(双击空白)";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 克隆toolStripMenuItem
            // 
            this.克隆toolStripMenuItem.Name = "克隆toolStripMenuItem";
            this.克隆toolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.克隆toolStripMenuItem.Text = "克隆";
            this.克隆toolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 上移ToolStripMenuItem
            // 
            this.上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
            this.上移ToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.上移ToolStripMenuItem.Text = "上移(Ctrl+↑)";
            this.上移ToolStripMenuItem.Click += new System.EventHandler(this.上移ToolStripMenuItem_Click);
            // 
            // 下移ToolStripMenuItem
            // 
            this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
            this.下移ToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.下移ToolStripMenuItem.Text = "下移(Ctrl+↓)";
            this.下移ToolStripMenuItem.Click += new System.EventHandler(this.下移ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.删除ToolStripMenuItem.Text = "删除(Delete)";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 设置双击条目ToolStripMenuItem
            // 
            this.设置双击条目ToolStripMenuItem.Name = "设置双击条目ToolStripMenuItem";
            this.设置双击条目ToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.设置双击条目ToolStripMenuItem.Text = "设置(双击条目)";
            this.设置双击条目ToolStripMenuItem.Click += new System.EventHandler(this.设置双击条目ToolStripMenuItem_Click);
            // 
            // contextMenuStrip_AnteType
            // 
            this.contextMenuStrip_AnteType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_MoveAnteType});
            this.contextMenuStrip_AnteType.Name = "contextMenuStrip_AnteType";
            this.contextMenuStrip_AnteType.Size = new System.Drawing.Size(169, 28);
            // 
            // ToolStripMenuItem_MoveAnteType
            // 
            this.ToolStripMenuItem_MoveAnteType.Name = "ToolStripMenuItem_MoveAnteType";
            this.ToolStripMenuItem_MoveAnteType.Size = new System.Drawing.Size(168, 24);
            this.ToolStripMenuItem_MoveAnteType.Text = "移动到文件夹";
            // 
            // contextMenuStrip_listItemkss
            // 
            this.contextMenuStrip_listItemkss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kss_新建ToolStripMenuItem,
            this.编辑kss脚本ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.contextMenuStrip_listItemkss.Name = "contextMenuStrip_actor";
            this.contextMenuStrip_listItemkss.ShowCheckMargin = true;
            this.contextMenuStrip_listItemkss.ShowImageMargin = false;
            this.contextMenuStrip_listItemkss.Size = new System.Drawing.Size(209, 148);
            // 
            // kss_新建ToolStripMenuItem
            // 
            this.kss_新建ToolStripMenuItem.Name = "kss_新建ToolStripMenuItem";
            this.kss_新建ToolStripMenuItem.Size = new System.Drawing.Size(208, 24);
            this.kss_新建ToolStripMenuItem.Text = "添加脚本(双击空白)";
            this.kss_新建ToolStripMenuItem.Click += new System.EventHandler(this.kss_新建ToolStripMenuItem_Click);
            // 
            // 编辑kss脚本ToolStripMenuItem
            // 
            this.编辑kss脚本ToolStripMenuItem.Name = "编辑kss脚本ToolStripMenuItem";
            this.编辑kss脚本ToolStripMenuItem.Size = new System.Drawing.Size(208, 24);
            this.编辑kss脚本ToolStripMenuItem.Text = "编辑脚本(双击条目)";
            this.编辑kss脚本ToolStripMenuItem.Click += new System.EventHandler(this.kss_编辑kss脚本ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(208, 24);
            this.toolStripMenuItem2.Text = "重设k脚本";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.kss_设置双击条目ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(208, 24);
            this.toolStripMenuItem3.Text = "上移(Ctrl+↑)";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.kss_上移ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(208, 24);
            this.toolStripMenuItem4.Text = "下移(Ctrl+↓)";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.kss_下移ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(208, 24);
            this.toolStripMenuItem5.Text = "删除(Delete)";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.kss_删除ToolStripMenuItem_Click);
            // 
            // CMS_RefreshKss
            // 
            this.CMS_RefreshKss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新载体列表ToolStripMenuItem});
            this.CMS_RefreshKss.Name = "CMS_RefreshKss";
            this.CMS_RefreshKss.Size = new System.Drawing.Size(166, 28);
            // 
            // 刷新载体列表ToolStripMenuItem
            // 
            this.刷新载体列表ToolStripMenuItem.Name = "刷新载体列表ToolStripMenuItem";
            this.刷新载体列表ToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.刷新载体列表ToolStripMenuItem.Text = "刷新载体(F5)";
            this.刷新载体列表ToolStripMenuItem.Click += new System.EventHandler(this.刷新载体列表ToolStripMenuItem_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(1267, 745);
            this.Controls.Add(this.tableLayoutPanel_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_Main;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1258, 729);
            this.Name = "Form_Main";
            this.Text = "Cyclone2D";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.SizeChanged += new System.EventHandler(this.Form_Main_SizeChanged);
            this.Shown += new System.EventHandler(this.Form_Main_Shown);
            this.Activated += new System.EventHandler(this.Form_Main_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            this.statusStrip_Main.ResumeLayout(false);
            this.statusStrip_Main.PerformLayout();
            this.tableLayoutPanel_Main.ResumeLayout(false);
            this.tableLayoutPanel_Main.PerformLayout();
            this.tableLayoutPanel_Center.ResumeLayout(false);
            this.tableLayoutPanel_Center_L.ResumeLayout(false);
            this.groupBox_Maps.ResumeLayout(false);
            this.TLP_mapList.ResumeLayout(false);
            this.tabControl_Center_L_T.ResumeLayout(false);
            this.tabPage_physic.ResumeLayout(false);
            this.TLP_Physics.ResumeLayout(false);
            this.TLP_Physics_TOP.ResumeLayout(false);
            this.panel_PhysicsBG.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Physics)).EndInit();
            this.panel_Physics_Tool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_autoGen_Phy)).EndInit();
            this.tabPage_Gfx.ResumeLayout(false);
            this.TLP_Gfx.ResumeLayout(false);
            this.TLP_Gfx_Top.ResumeLayout(false);
            this.panel_ground.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Gfx)).EndInit();
            this.panel_gfx_tool.ResumeLayout(false);
            this.panel_gfx_tool.PerformLayout();
            this.panel_GfXGroup.ResumeLayout(false);
            this.tabPage_AT.ResumeLayout(false);
            this.TLP_AT.ResumeLayout(false);
            this.panel_ATpack.ResumeLayout(false);
            this.TLP_AT_top.ResumeLayout(false);
            this.panel_AT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AT)).EndInit();
            this.panel_obj_tool.ResumeLayout(false);
            this.panel_obj_tool.PerformLayout();
            this.tabPage_Scripts.ResumeLayout(false);
            this.SC_BG.Panel1.ResumeLayout(false);
            this.SC_BG.Panel2.ResumeLayout(false);
            this.SC_BG.ResumeLayout(false);
            this.groupBox_lofter.ResumeLayout(false);
            this.groupBox_threads.ResumeLayout(false);
            this.tableLayoutPanel_Canvas.ResumeLayout(false);
            this.panel_Canvas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Canvas)).EndInit();
            this.toolStrip_Operate.ResumeLayout(false);
            this.toolStrip_Operate.PerformLayout();
            this.contextMenuStrip_listItem.ResumeLayout(false);
            this.contextMenuStrip_AnteType.ResumeLayout(false);
            this.contextMenuStrip_listItemkss.ResumeLayout(false);
            this.CMS_RefreshKss.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开工程ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip_Main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Main;
        private System.Windows.Forms.ToolStripMenuItem 保存工程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_文件_0;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator文件_1;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 小工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版本信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton TSB_levelPhysic;
        private System.Windows.Forms.ToolStrip toolStrip_Operate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_listItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 克隆toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Center;
        private System.Windows.Forms.GroupBox groupBox_Maps;
        private System.Windows.Forms.ListBox listBox_Maps;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Center_L;
        private System.Windows.Forms.Panel panel_Canvas;
        private System.Windows.Forms.TabControl tabControl_Center_L_T;
        private System.Windows.Forms.TabPage tabPage_Gfx;
        private System.Windows.Forms.TableLayoutPanel TLP_Gfx;
        private System.Windows.Forms.TableLayoutPanel TLP_Gfx_Top;
        private System.Windows.Forms.Panel panel_ground;
        private System.Windows.Forms.PictureBox pictureBox_Gfx;
        private System.Windows.Forms.VScrollBar vScrollBar_Gfx;
        private System.Windows.Forms.Panel panel_gfx_tool;
        private System.Windows.Forms.TabPage tabPage_AT;
        private System.Windows.Forms.ToolStripButton TSB_level_Ground;
        private System.Windows.Forms.ToolStripButton TSB_level_Surface;
        private System.Windows.Forms.ToolStripButton TSB_level_Object;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton TSB_open;
        private System.Windows.Forms.ToolStripButton TSB_save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton TSB_levelActors_Eye;
        private System.Windows.Forms.Button button_addMore_Grx;
        private System.Windows.Forms.Button button_addOne_Gfx;
        private System.Windows.Forms.TabPage tabPage_physic;
        private System.Windows.Forms.ToolStripMenuItem 设置双击条目ToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Canvas;
        private System.Windows.Forms.VScrollBar vScrollBar_Canvas;
        private System.Windows.Forms.HScrollBar hScrollBar_Canvas;
        private System.Windows.Forms.TableLayoutPanel TLP_Physics;
        private System.Windows.Forms.TableLayoutPanel TLP_Physics_TOP;
        private System.Windows.Forms.Panel panel_PhysicsBG;
        private System.Windows.Forms.PictureBox pictureBox_Physics;
        private System.Windows.Forms.VScrollBar vScrollBar_Physics;
        private System.Windows.Forms.Panel panel_Physics_Tool;
        private System.Windows.Forms.Button button_del_Phy;
        private System.Windows.Forms.Button button_add_Phy;
        private System.Windows.Forms.NumericUpDown numericUpDown_autoGen_Phy;
        private System.Windows.Forms.Button button_AutoGen;
        private System.Windows.Forms.Button button_config;
        private System.Windows.Forms.ToolStripButton TSB_FillIn;
        private System.Windows.Forms.ToolStripButton TSB_rectSelect;
        private System.Windows.Forms.ToolStripButton TSB_pointSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton TSB_pencil;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 物理标记ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton TSB_Copy;
        private System.Windows.Forms.ToolStripButton TSB_Paste;
        private System.Windows.Forms.Button button_del_Gfx;
        private System.Windows.Forms.Button button_TileGfx_TransN;
        private System.Windows.Forms.Button button_TileGfx_TransP;
        private System.Windows.Forms.Button button_TileGfx_FlipV;
        private System.Windows.Forms.Button button_TileGfx_FlipH;
        private System.Windows.Forms.ToolStripStatusLabel label_showFunction;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Welcome;
        private System.Windows.Forms.ToolStripButton TSB_level_Obj_Mask;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图层透明度调整ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 顶层显示物理层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 场景缩放ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMI_P100;
        private System.Windows.Forms.ToolStripMenuItem TSMI_P200;
        private System.Windows.Forms.ToolStripMenuItem TSMI_P400;
        private System.Windows.Forms.ToolStripMenuItem TSMI_P800;
        private System.Windows.Forms.ToolStripMenuItem TSMI_P50;
        private System.Windows.Forms.ToolStripMenuItem TSMI_P25;
        private System.Windows.Forms.ToolStripMenuItem TSMI_P12dot5;
        private System.Windows.Forms.ToolStripMenuItem 图形IDToolStripMenuItem;
        private System.Windows.Forms.Button button_Copy_Gfx;
        private System.Windows.Forms.Button button_Right_Gfx;
        private System.Windows.Forms.Button button_Left_Gfx;
        private System.Windows.Forms.Button button_Down_Gfx;
        private System.Windows.Forms.Button button_Up_Gfx;
        private System.Windows.Forms.TableLayoutPanel TLP_AT;
        private System.Windows.Forms.TableLayoutPanel TLP_AT_top;
        private System.Windows.Forms.Panel panel_AT;
        private System.Windows.Forms.PictureBox pictureBox_AT;
        private System.Windows.Forms.VScrollBar vScrollBar_AT;
        private System.Windows.Forms.Panel panel_obj_tool;
        private System.Windows.Forms.Button button_obj_import;
        private System.Windows.Forms.Panel panel_Flag;
        private System.Windows.Forms.Button button_obj_refresh;
        private System.Windows.Forms.Button button_Obj_del;
        private System.Windows.Forms.ToolStripButton TSB_Undo;
        private System.Windows.Forms.ToolStripButton TSB_Redo;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Combine;
        private System.Windows.Forms.PictureBox pictureBox_Canvas;
        private System.Windows.Forms.ToolStripButton TSB_Refresh;
        private System.Windows.Forms.ToolStripButton TSB_level_Tile_Obj;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.Button button_ClearSpilth;
        private System.Windows.Forms.Button button_CheckContainer;
        private System.Windows.Forms.ToolStripButton TSB_ErrorCheck;
        private System.Windows.Forms.Panel panel_GfXGroup;
        private System.Windows.Forms.Button button_AddGfxFolder;
        private System.Windows.Forms.Button button_DelGfxFolder;
        private System.Windows.Forms.ComboBox comboBox_GfxType;
        private System.Windows.Forms.Label label_TileGfxID;
        private System.Windows.Forms.Label label_TileGfxUsedTime;
        private System.Windows.Forms.Button button_NameFolder;
        private System.Windows.Forms.Button button_ClearATSpilth;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem 变量与函数容器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 脚本编辑器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton TSB_Straw;
        private System.Windows.Forms.Panel panel_ATpack;
        private System.Windows.Forms.Button button_checkAT;
        private System.Windows.Forms.ComboBox comboBox_ATFolders;
        private System.Windows.Forms.Button button_ATPack_Rename;
        private System.Windows.Forms.Button button_ATPack_Del;
        private System.Windows.Forms.Button button_ATPack_Add;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_AnteType;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_MoveAnteType;
        private System.Windows.Forms.Label lable_AT;
        private System.Windows.Forms.Button button_configAT;
        private System.Windows.Forms.ToolStripMenuItem 地图角色初始帧ToolStripMenuItem;
        private System.Windows.Forms.Button button_cloneAnteType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton TSB_findAT_Pre;
        private System.Windows.Forms.ToolStripButton TSB_Replace;
        private System.Windows.Forms.ToolStripButton TSB_findAT_Next;
        private System.Windows.Forms.Button button_GenIDs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem 生成地图位图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成脚本文本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar TSPB_load;
        private System.Windows.Forms.ToolStripMenuItem 文件批量打包ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 调色板编辑器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图片格式转换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 压缩混淆ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按比例缩放ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.TableLayoutPanel TLP_mapList;
        public System.Windows.Forms.ListBox listBox_stage;
        private System.Windows.Forms.ToolStripMenuItem 地图角色NPC编号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图角色锚点坐标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem UIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文本管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton TSB_autoTile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 融合地形层上图形元素编号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton TSB_fillCorner;
        private System.Windows.Forms.ToolStripMenuItem 动画和地图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 脚本编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 其它功能ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图片处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 影片动画编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图图片管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 撤销ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重做ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.TabPage tabPage_Scripts;
        private System.Windows.Forms.SplitContainer SC_BG;
        private System.Windows.Forms.GroupBox groupBox_lofter;
        private System.Windows.Forms.ListBox listBox_Carrier;
        private System.Windows.Forms.GroupBox groupBox_threads;
        private System.Windows.Forms.ListBox listBox_Files;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_listItemkss;
        private System.Windows.Forms.ToolStripMenuItem kss_新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑kss脚本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ContextMenuStrip CMS_RefreshKss;
        private System.Windows.Forms.ToolStripMenuItem 刷新载体列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}

