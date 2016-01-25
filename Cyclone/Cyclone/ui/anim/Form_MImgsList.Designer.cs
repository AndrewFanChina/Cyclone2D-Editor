namespace Cyclone.mod.anim
{
    partial class Form_MImgsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MImgsList));
            this.listBox_Images = new System.Windows.Forms.ListBox();
            this.toolStrip_imgList = new System.Windows.Forms.ToolStrip();
            this.TSB_add = new System.Windows.Forms.ToolStripButton();
            this.TSB_rename = new System.Windows.Forms.ToolStripButton();
            this.TSB_delete = new System.Windows.Forms.ToolStripButton();
            this.TSB_update = new System.Windows.Forms.ToolStripButton();
            this.TSB_moveUp = new System.Windows.Forms.ToolStripButton();
            this.TSB_moveDown = new System.Windows.Forms.ToolStripButton();
            this.TSB_moveTop = new System.Windows.Forms.ToolStripButton();
            this.TSB_moveBottom = new System.Windows.Forms.ToolStripButton();
            this.checkBox_forbbidOptimize = new System.Windows.Forms.ToolStripButton();
            this.TSB_link = new System.Windows.Forms.ToolStripButton();
            this.TSB_linkBreak = new System.Windows.Forms.ToolStripButton();
            this.TSB_linkTest = new System.Windows.Forms.ToolStripButton();
            this.TSB_sortUp = new System.Windows.Forms.ToolStripButton();
            this.TSB_sortDown = new System.Windows.Forms.ToolStripButton();
            this.TSB_checkUse = new System.Windows.Forms.ToolStripButton();
            this.TSB_Help = new System.Windows.Forms.ToolStripButton();
            this.panel_imgClipBg = new System.Windows.Forms.Panel();
            this.panel_clipEdit = new System.Windows.Forms.Panel();
            this.label_ClipUsedTime = new System.Windows.Forms.Label();
            this.textBox_ClipUsedTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox_Clip = new System.Windows.Forms.GroupBox();
            this.pictureBox_transferClip = new System.Windows.Forms.PictureBox();
            this.pictureBox_ClipCombine = new System.Windows.Forms.PictureBox();
            this.pictureBox_ClipUse = new System.Windows.Forms.PictureBox();
            this.numericUpDown_y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_h = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_w = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_x = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer_All = new System.Windows.Forms.SplitContainer();
            this.TLP_All = new System.Windows.Forms.TableLayoutPanel();
            this.panel_ClipProp = new System.Windows.Forms.Panel();
            this.toolStrip_imgList.SuspendLayout();
            this.panel_imgClipBg.SuspendLayout();
            this.groupBox_Clip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_transferClip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ClipCombine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ClipUse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_h)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_w)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_x)).BeginInit();
            this.splitContainer_All.Panel1.SuspendLayout();
            this.splitContainer_All.Panel2.SuspendLayout();
            this.splitContainer_All.SuspendLayout();
            this.TLP_All.SuspendLayout();
            this.panel_ClipProp.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox_Images
            // 
            this.listBox_Images.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox_Images.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Images.FormattingEnabled = true;
            this.listBox_Images.ItemHeight = 15;
            this.listBox_Images.Location = new System.Drawing.Point(0, 25);
            this.listBox_Images.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_Images.Name = "listBox_Images";
            this.listBox_Images.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_Images.Size = new System.Drawing.Size(451, 167);
            this.listBox_Images.TabIndex = 0;
            this.listBox_Images.TabStop = false;
            this.listBox_Images.SelectedIndexChanged += new System.EventHandler(this.listBoxImgList_SelectedIndexChanged);
            this.listBox_Images.DoubleClick += new System.EventHandler(this.listBox_ImageManager_DoubleClick);
            this.listBox_Images.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_ImageManager_KeyDown);
            // 
            // toolStrip_imgList
            // 
            this.toolStrip_imgList.AllowItemReorder = true;
            this.toolStrip_imgList.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip_imgList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip_imgList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSB_add,
            this.TSB_rename,
            this.TSB_delete,
            this.TSB_update,
            this.TSB_moveUp,
            this.TSB_moveDown,
            this.TSB_moveTop,
            this.TSB_moveBottom,
            this.checkBox_forbbidOptimize,
            this.TSB_link,
            this.TSB_linkBreak,
            this.TSB_linkTest,
            this.TSB_sortUp,
            this.TSB_sortDown,
            this.TSB_checkUse,
            this.TSB_Help});
            this.toolStrip_imgList.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_imgList.Name = "toolStrip_imgList";
            this.toolStrip_imgList.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip_imgList.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip_imgList.Size = new System.Drawing.Size(451, 25);
            this.toolStrip_imgList.Stretch = true;
            this.toolStrip_imgList.TabIndex = 12;
            // 
            // TSB_add
            // 
            this.TSB_add.AutoSize = false;
            this.TSB_add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_add.Image = global::Cyclone.Properties.Resources.imgOp_add;
            this.TSB_add.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_add.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_add.Name = "TSB_add";
            this.TSB_add.Size = new System.Drawing.Size(21, 20);
            this.TSB_add.Text = "导入图片";
            this.TSB_add.Click += new System.EventHandler(this.button_importImage_Click);
            // 
            // TSB_rename
            // 
            this.TSB_rename.AutoSize = false;
            this.TSB_rename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_rename.Image = global::Cyclone.Properties.Resources.imgOp_rename;
            this.TSB_rename.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_rename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_rename.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_rename.Name = "TSB_rename";
            this.TSB_rename.Size = new System.Drawing.Size(21, 20);
            this.TSB_rename.Text = "重新命名";
            this.TSB_rename.Click += new System.EventHandler(this.button_rename_Click);
            // 
            // TSB_delete
            // 
            this.TSB_delete.AutoSize = false;
            this.TSB_delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_delete.Image = global::Cyclone.Properties.Resources.imgOp_delete;
            this.TSB_delete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_delete.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_delete.Name = "TSB_delete";
            this.TSB_delete.Size = new System.Drawing.Size(21, 20);
            this.TSB_delete.Text = "删除图片";
            this.TSB_delete.Click += new System.EventHandler(this.button_delImage_Click);
            // 
            // TSB_update
            // 
            this.TSB_update.AutoSize = false;
            this.TSB_update.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_update.Image = global::Cyclone.Properties.Resources.imgOp_update;
            this.TSB_update.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_update.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_update.Name = "TSB_update";
            this.TSB_update.Size = new System.Drawing.Size(21, 20);
            this.TSB_update.Text = "更新图片";
            this.TSB_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // TSB_moveUp
            // 
            this.TSB_moveUp.AutoSize = false;
            this.TSB_moveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_moveUp.Image = global::Cyclone.Properties.Resources.imgOp_moveUp;
            this.TSB_moveUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_moveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_moveUp.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_moveUp.Name = "TSB_moveUp";
            this.TSB_moveUp.Size = new System.Drawing.Size(21, 20);
            this.TSB_moveUp.Text = "向上移动";
            this.TSB_moveUp.Click += new System.EventHandler(this.button_moveUp_Click);
            // 
            // TSB_moveDown
            // 
            this.TSB_moveDown.AutoSize = false;
            this.TSB_moveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_moveDown.Image = global::Cyclone.Properties.Resources.imgOp_moveDown;
            this.TSB_moveDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_moveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_moveDown.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_moveDown.Name = "TSB_moveDown";
            this.TSB_moveDown.Size = new System.Drawing.Size(21, 20);
            this.TSB_moveDown.Text = "向下移动";
            this.TSB_moveDown.Click += new System.EventHandler(this.button_moveDown_Click);
            // 
            // TSB_moveTop
            // 
            this.TSB_moveTop.AutoSize = false;
            this.TSB_moveTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_moveTop.Image = global::Cyclone.Properties.Resources.imgOp_moveTop;
            this.TSB_moveTop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_moveTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_moveTop.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_moveTop.Name = "TSB_moveTop";
            this.TSB_moveTop.Size = new System.Drawing.Size(21, 20);
            this.TSB_moveTop.Text = "移至顶端";
            this.TSB_moveTop.Click += new System.EventHandler(this.button_moveTop_Click);
            // 
            // TSB_moveBottom
            // 
            this.TSB_moveBottom.AutoSize = false;
            this.TSB_moveBottom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_moveBottom.Image = global::Cyclone.Properties.Resources.imgOp_moveBottom;
            this.TSB_moveBottom.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_moveBottom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_moveBottom.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_moveBottom.Name = "TSB_moveBottom";
            this.TSB_moveBottom.Size = new System.Drawing.Size(21, 20);
            this.TSB_moveBottom.Text = "移至底端";
            this.TSB_moveBottom.Click += new System.EventHandler(this.button_moveBottom_Click);
            // 
            // checkBox_forbbidOptimize
            // 
            this.checkBox_forbbidOptimize.AutoSize = false;
            this.checkBox_forbbidOptimize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.checkBox_forbbidOptimize.Image = global::Cyclone.Properties.Resources.imgOp_opt_forbbid;
            this.checkBox_forbbidOptimize.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.checkBox_forbbidOptimize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.checkBox_forbbidOptimize.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.checkBox_forbbidOptimize.Name = "checkBox_forbbidOptimize";
            this.checkBox_forbbidOptimize.Size = new System.Drawing.Size(21, 20);
            this.checkBox_forbbidOptimize.Text = "禁止优化";
            this.checkBox_forbbidOptimize.ToolTipText = "禁止优化";
            this.checkBox_forbbidOptimize.Click += new System.EventHandler(this.checkBox_forbbidOptimize_CheckedChanged);
            // 
            // TSB_link
            // 
            this.TSB_link.AutoSize = false;
            this.TSB_link.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_link.Image = global::Cyclone.Properties.Resources.imgOp_link;
            this.TSB_link.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_link.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_link.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_link.Name = "TSB_link";
            this.TSB_link.Size = new System.Drawing.Size(21, 20);
            this.TSB_link.Text = "加入优化链接";
            this.TSB_link.Click += new System.EventHandler(this.TSB_link_Click);
            // 
            // TSB_linkBreak
            // 
            this.TSB_linkBreak.AutoSize = false;
            this.TSB_linkBreak.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_linkBreak.Image = global::Cyclone.Properties.Resources.imgOp_linkBrek;
            this.TSB_linkBreak.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_linkBreak.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_linkBreak.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_linkBreak.Name = "TSB_linkBreak";
            this.TSB_linkBreak.Size = new System.Drawing.Size(21, 20);
            this.TSB_linkBreak.Text = "解除优化链接";
            this.TSB_linkBreak.Click += new System.EventHandler(this.TSB_linkBreak_Click);
            // 
            // TSB_linkTest
            // 
            this.TSB_linkTest.AutoSize = false;
            this.TSB_linkTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_linkTest.Image = global::Cyclone.Properties.Resources.imgOp_linkTest;
            this.TSB_linkTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_linkTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_linkTest.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_linkTest.Name = "TSB_linkTest";
            this.TSB_linkTest.Size = new System.Drawing.Size(21, 20);
            this.TSB_linkTest.Text = "预测优化链接";
            this.TSB_linkTest.Click += new System.EventHandler(this.TSB_linkTest_Click);
            // 
            // TSB_sortUp
            // 
            this.TSB_sortUp.AutoSize = false;
            this.TSB_sortUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_sortUp.Image = global::Cyclone.Properties.Resources.imgOp_sortUp;
            this.TSB_sortUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_sortUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_sortUp.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_sortUp.Name = "TSB_sortUp";
            this.TSB_sortUp.Size = new System.Drawing.Size(21, 20);
            this.TSB_sortUp.Text = "全部正序排列";
            this.TSB_sortUp.Click += new System.EventHandler(this.button_orderPositive_Click);
            // 
            // TSB_sortDown
            // 
            this.TSB_sortDown.AutoSize = false;
            this.TSB_sortDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_sortDown.Image = global::Cyclone.Properties.Resources.imgOp_sortDown;
            this.TSB_sortDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_sortDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_sortDown.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_sortDown.Name = "TSB_sortDown";
            this.TSB_sortDown.Size = new System.Drawing.Size(21, 20);
            this.TSB_sortDown.Text = "全部逆序排列";
            this.TSB_sortDown.Click += new System.EventHandler(this.button_orderBackwards_Click);
            // 
            // TSB_checkUse
            // 
            this.TSB_checkUse.AutoSize = false;
            this.TSB_checkUse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_checkUse.Image = global::Cyclone.Properties.Resources.imgOp_checkUse;
            this.TSB_checkUse.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_checkUse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_checkUse.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_checkUse.Name = "TSB_checkUse";
            this.TSB_checkUse.Size = new System.Drawing.Size(21, 20);
            this.TSB_checkUse.Text = "使用检查";
            this.TSB_checkUse.Click += new System.EventHandler(this.button_Check_Click);
            // 
            // TSB_Help
            // 
            this.TSB_Help.AutoSize = false;
            this.TSB_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Help.Image = global::Cyclone.Properties.Resources.help16;
            this.TSB_Help.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSB_Help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_Help.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TSB_Help.Name = "TSB_Help";
            this.TSB_Help.Size = new System.Drawing.Size(21, 20);
            this.TSB_Help.ToolTipText = resources.GetString("TSB_Help.ToolTipText");
            // 
            // panel_imgClipBg
            // 
            this.panel_imgClipBg.Controls.Add(this.panel_clipEdit);
            this.panel_imgClipBg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_imgClipBg.Location = new System.Drawing.Point(0, 0);
            this.panel_imgClipBg.Margin = new System.Windows.Forms.Padding(4);
            this.panel_imgClipBg.Name = "panel_imgClipBg";
            this.panel_imgClipBg.Size = new System.Drawing.Size(451, 335);
            this.panel_imgClipBg.TabIndex = 12;
            // 
            // panel_clipEdit
            // 
            this.panel_clipEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_clipEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_clipEdit.Location = new System.Drawing.Point(0, 0);
            this.panel_clipEdit.Margin = new System.Windows.Forms.Padding(0);
            this.panel_clipEdit.Name = "panel_clipEdit";
            this.panel_clipEdit.Size = new System.Drawing.Size(451, 335);
            this.panel_clipEdit.TabIndex = 11;
            this.panel_clipEdit.DoubleClick += new System.EventHandler(this.pictureBox_clipEdit_DoubleClick);
            this.panel_clipEdit.MouseLeave += new System.EventHandler(this.pictureBox_clipEdit_MouseLeave);
            this.panel_clipEdit.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_EditorBg_Paint);
            this.panel_clipEdit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_clipEdit_MouseMove);
            this.panel_clipEdit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_clipEdit_MouseDown);
            this.panel_clipEdit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_clipEdit_MouseUp);
            this.panel_clipEdit.MouseEnter += new System.EventHandler(this.pictureBox_clipEdit_MouseEnter);
            // 
            // label_ClipUsedTime
            // 
            this.label_ClipUsedTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ClipUsedTime.AutoSize = true;
            this.label_ClipUsedTime.BackColor = System.Drawing.Color.White;
            this.label_ClipUsedTime.Location = new System.Drawing.Point(229, 39);
            this.label_ClipUsedTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_ClipUsedTime.Name = "label_ClipUsedTime";
            this.label_ClipUsedTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_ClipUsedTime.Size = new System.Drawing.Size(39, 15);
            this.label_ClipUsedTime.TabIndex = 7;
            this.label_ClipUsedTime.Text = "    ";
            this.label_ClipUsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_ClipUsedTime
            // 
            this.textBox_ClipUsedTime.BackColor = System.Drawing.Color.White;
            this.textBox_ClipUsedTime.Enabled = false;
            this.textBox_ClipUsedTime.Location = new System.Drawing.Point(228, 34);
            this.textBox_ClipUsedTime.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_ClipUsedTime.Name = "textBox_ClipUsedTime";
            this.textBox_ClipUsedTime.Size = new System.Drawing.Size(40, 25);
            this.textBox_ClipUsedTime.TabIndex = 5;
            this.textBox_ClipUsedTime.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(224, 16);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "：使用";
            // 
            // groupBox_Clip
            // 
            this.groupBox_Clip.BackColor = System.Drawing.Color.Transparent;
            this.groupBox_Clip.Controls.Add(this.pictureBox_transferClip);
            this.groupBox_Clip.Controls.Add(this.pictureBox_ClipCombine);
            this.groupBox_Clip.Controls.Add(this.pictureBox_ClipUse);
            this.groupBox_Clip.Controls.Add(this.numericUpDown_y);
            this.groupBox_Clip.Controls.Add(this.numericUpDown_h);
            this.groupBox_Clip.Controls.Add(this.label_ClipUsedTime);
            this.groupBox_Clip.Controls.Add(this.numericUpDown_w);
            this.groupBox_Clip.Controls.Add(this.textBox_ClipUsedTime);
            this.groupBox_Clip.Controls.Add(this.numericUpDown_x);
            this.groupBox_Clip.Controls.Add(this.label2);
            this.groupBox_Clip.Controls.Add(this.label1);
            this.groupBox_Clip.Controls.Add(this.label7);
            this.groupBox_Clip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Clip.ForeColor = System.Drawing.Color.Black;
            this.groupBox_Clip.Location = new System.Drawing.Point(0, 0);
            this.groupBox_Clip.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox_Clip.Name = "groupBox_Clip";
            this.groupBox_Clip.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox_Clip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox_Clip.Size = new System.Drawing.Size(451, 68);
            this.groupBox_Clip.TabIndex = 0;
            this.groupBox_Clip.TabStop = false;
            this.groupBox_Clip.Text = "切片属性";
            // 
            // pictureBox_transferClip
            // 
            this.pictureBox_transferClip.BackgroundImage = global::Cyclone.Properties.Resources.transferClipOff;
            this.pictureBox_transferClip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_transferClip.InitialImage = null;
            this.pictureBox_transferClip.Location = new System.Drawing.Point(273, 34);
            this.pictureBox_transferClip.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_transferClip.Name = "pictureBox_transferClip";
            this.pictureBox_transferClip.Size = new System.Drawing.Size(27, 25);
            this.pictureBox_transferClip.TabIndex = 13;
            this.pictureBox_transferClip.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox_transferClip, "跨图转移切片模式(关)");
            this.pictureBox_transferClip.Click += new System.EventHandler(this.pictureBox_transferClip_Click);
            // 
            // pictureBox_ClipCombine
            // 
            this.pictureBox_ClipCombine.BackgroundImage = global::Cyclone.Properties.Resources.imgOp_combine;
            this.pictureBox_ClipCombine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_ClipCombine.InitialImage = null;
            this.pictureBox_ClipCombine.Location = new System.Drawing.Point(329, 34);
            this.pictureBox_ClipCombine.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_ClipCombine.Name = "pictureBox_ClipCombine";
            this.pictureBox_ClipCombine.Size = new System.Drawing.Size(27, 25);
            this.pictureBox_ClipCombine.TabIndex = 12;
            this.pictureBox_ClipCombine.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox_ClipCombine, "切片重复合并");
            this.pictureBox_ClipCombine.Click += new System.EventHandler(this.pictureBox_ClipCombine_Click);
            this.pictureBox_ClipCombine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ClipCombine_MouseDown);
            this.pictureBox_ClipCombine.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ClipCombine_MouseUp);
            // 
            // pictureBox_ClipUse
            // 
            this.pictureBox_ClipUse.BackgroundImage = global::Cyclone.Properties.Resources.imgOp_checkUse;
            this.pictureBox_ClipUse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_ClipUse.InitialImage = null;
            this.pictureBox_ClipUse.Location = new System.Drawing.Point(301, 34);
            this.pictureBox_ClipUse.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_ClipUse.Name = "pictureBox_ClipUse";
            this.pictureBox_ClipUse.Size = new System.Drawing.Size(27, 25);
            this.pictureBox_ClipUse.TabIndex = 11;
            this.pictureBox_ClipUse.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox_ClipUse, "切片使用检查");
            this.pictureBox_ClipUse.Click += new System.EventHandler(this.pictureBox_ClipUse_Click);
            this.pictureBox_ClipUse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ClipUse_MouseDown);
            this.pictureBox_ClipUse.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ClipUse_MouseUp);
            // 
            // numericUpDown_y
            // 
            this.numericUpDown_y.Location = new System.Drawing.Point(61, 34);
            this.numericUpDown_y.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDown_y.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numericUpDown_y.Name = "numericUpDown_y";
            this.numericUpDown_y.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDown_y.Size = new System.Drawing.Size(53, 25);
            this.numericUpDown_y.TabIndex = 5;
            this.numericUpDown_y.TabStop = false;
            this.numericUpDown_y.ValueChanged += new System.EventHandler(this.numericUpDown_y_ValueChanged);
            // 
            // numericUpDown_h
            // 
            this.numericUpDown_h.Location = new System.Drawing.Point(172, 34);
            this.numericUpDown_h.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDown_h.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numericUpDown_h.Name = "numericUpDown_h";
            this.numericUpDown_h.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDown_h.Size = new System.Drawing.Size(53, 25);
            this.numericUpDown_h.TabIndex = 7;
            this.numericUpDown_h.TabStop = false;
            this.numericUpDown_h.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_h.ValueChanged += new System.EventHandler(this.numericUpDown_h_ValueChanged);
            // 
            // numericUpDown_w
            // 
            this.numericUpDown_w.Location = new System.Drawing.Point(117, 34);
            this.numericUpDown_w.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDown_w.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numericUpDown_w.Name = "numericUpDown_w";
            this.numericUpDown_w.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDown_w.Size = new System.Drawing.Size(53, 25);
            this.numericUpDown_w.TabIndex = 6;
            this.numericUpDown_w.TabStop = false;
            this.numericUpDown_w.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_w.ValueChanged += new System.EventHandler(this.numericUpDown_w_ValueChanged);
            // 
            // numericUpDown_x
            // 
            this.numericUpDown_x.Location = new System.Drawing.Point(7, 34);
            this.numericUpDown_x.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDown_x.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numericUpDown_x.Name = "numericUpDown_x";
            this.numericUpDown_x.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDown_x.Size = new System.Drawing.Size(53, 25);
            this.numericUpDown_x.TabIndex = 4;
            this.numericUpDown_x.TabStop = false;
            this.numericUpDown_x.ValueChanged += new System.EventHandler(this.numericUpDown_x_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "：宽高";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "：坐标";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 2000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // splitContainer_All
            // 
            this.splitContainer_All.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_All.Location = new System.Drawing.Point(4, 4);
            this.splitContainer_All.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer_All.Name = "splitContainer_All";
            this.splitContainer_All.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_All.Panel1
            // 
            this.splitContainer_All.Panel1.Controls.Add(this.listBox_Images);
            this.splitContainer_All.Panel1.Controls.Add(this.toolStrip_imgList);
            // 
            // splitContainer_All.Panel2
            // 
            this.splitContainer_All.Panel2.Controls.Add(this.panel_imgClipBg);
            this.splitContainer_All.Size = new System.Drawing.Size(451, 535);
            this.splitContainer_All.SplitterDistance = 195;
            this.splitContainer_All.SplitterWidth = 5;
            this.splitContainer_All.TabIndex = 102;
            // 
            // TLP_All
            // 
            this.TLP_All.ColumnCount = 1;
            this.TLP_All.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_All.Controls.Add(this.splitContainer_All, 0, 0);
            this.TLP_All.Controls.Add(this.panel_ClipProp, 0, 1);
            this.TLP_All.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_All.Location = new System.Drawing.Point(0, 0);
            this.TLP_All.Margin = new System.Windows.Forms.Padding(4);
            this.TLP_All.Name = "TLP_All";
            this.TLP_All.RowCount = 2;
            this.TLP_All.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_All.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.TLP_All.Size = new System.Drawing.Size(459, 619);
            this.TLP_All.TabIndex = 103;
            // 
            // panel_ClipProp
            // 
            this.panel_ClipProp.Controls.Add(this.groupBox_Clip);
            this.panel_ClipProp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ClipProp.Location = new System.Drawing.Point(4, 547);
            this.panel_ClipProp.Margin = new System.Windows.Forms.Padding(4);
            this.panel_ClipProp.Name = "panel_ClipProp";
            this.panel_ClipProp.Size = new System.Drawing.Size(451, 68);
            this.panel_ClipProp.TabIndex = 103;
            // 
            // Form_MImgsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 619);
            this.Controls.Add(this.TLP_All);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_MImgsList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "贴图素材库";
            this.Shown += new System.EventHandler(this.Form_MImgsList_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form_MImgsList_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_MImgsList_KeyDown);
            this.toolStrip_imgList.ResumeLayout(false);
            this.toolStrip_imgList.PerformLayout();
            this.panel_imgClipBg.ResumeLayout(false);
            this.groupBox_Clip.ResumeLayout(false);
            this.groupBox_Clip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_transferClip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ClipCombine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ClipUse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_h)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_w)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_x)).EndInit();
            this.splitContainer_All.Panel1.ResumeLayout(false);
            this.splitContainer_All.Panel1.PerformLayout();
            this.splitContainer_All.Panel2.ResumeLayout(false);
            this.splitContainer_All.ResumeLayout(false);
            this.TLP_All.ResumeLayout(false);
            this.panel_ClipProp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox listBox_Images;
        private System.Windows.Forms.ToolStrip toolStrip_imgList;
        private System.Windows.Forms.ToolStripButton TSB_add;
        private System.Windows.Forms.ToolStripButton TSB_update;
        private System.Windows.Forms.ToolStripButton TSB_rename;
        private System.Windows.Forms.ToolStripButton TSB_delete;
        private System.Windows.Forms.ToolStripButton TSB_moveUp;
        private System.Windows.Forms.ToolStripButton TSB_moveDown;
        private System.Windows.Forms.ToolStripButton TSB_moveTop;
        private System.Windows.Forms.ToolStripButton TSB_moveBottom;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panel_clipEdit;
        private System.Windows.Forms.GroupBox groupBox_Clip;
        private System.Windows.Forms.Label label_ClipUsedTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_ClipUsedTime;
        private System.Windows.Forms.NumericUpDown numericUpDown_h;
        private System.Windows.Forms.NumericUpDown numericUpDown_w;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_y;
        private System.Windows.Forms.NumericUpDown numericUpDown_x;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_imgClipBg;
        private System.Windows.Forms.SplitContainer splitContainer_All;
        private System.Windows.Forms.TableLayoutPanel TLP_All;
        private System.Windows.Forms.Panel panel_ClipProp;
        private System.Windows.Forms.ToolStripButton TSB_sortUp;
        private System.Windows.Forms.ToolStripButton TSB_sortDown;
        private System.Windows.Forms.ToolStripButton checkBox_forbbidOptimize;
        private System.Windows.Forms.ToolStripButton TSB_checkUse;
        private System.Windows.Forms.ToolStripButton TSB_linkBreak;
        private System.Windows.Forms.ToolStripButton TSB_linkTest;
        private System.Windows.Forms.ToolStripButton TSB_Help;
        private System.Windows.Forms.ToolStripButton TSB_link;
        private System.Windows.Forms.PictureBox pictureBox_ClipUse;
        private System.Windows.Forms.PictureBox pictureBox_ClipCombine;
        private System.Windows.Forms.PictureBox pictureBox_transferClip;
    }
}