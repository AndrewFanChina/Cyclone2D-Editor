namespace Cyclone.mod.misc
{
    partial class SmallDialog_ExportConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmallDialog_ExportConfig));
            this.button_OK = new System.Windows.Forms.Button();
            this.groupBox_anim = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_left = new System.Windows.Forms.TableLayoutPanel();
            this.panel_LT = new System.Windows.Forms.Panel();
            this.checkBox_ActionOffset = new System.Windows.Forms.CheckBox();
            this.comboBox_ActionOffset = new System.Windows.Forms.ComboBox();
            this.checkBox_SplitAnimation = new System.Windows.Forms.CheckBox();
            this.comboBox_animFormat = new System.Windows.Forms.ComboBox();
            this.label_Format = new System.Windows.Forms.Label();
            this.checkBox_confuseImg = new System.Windows.Forms.CheckBox();
            this.richTextBox_export = new System.Windows.Forms.RichTextBox();
            this.groupBox_Result = new System.Windows.Forms.GroupBox();
            this.TLP_showResult = new System.Windows.Forms.TableLayoutPanel();
            this.panel_ShowString = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar_Export = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_up = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_map = new System.Windows.Forms.GroupBox();
            this.TLP_Map = new System.Windows.Forms.TableLayoutPanel();
            this.panel_RT = new System.Windows.Forms.Panel();
            this.checkBox_CompileScrpts = new System.Windows.Forms.CheckBox();
            this.TLP_Center = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_Common = new System.Windows.Forms.GroupBox();
            this.TLP_commonSet = new System.Windows.Forms.TableLayoutPanel();
            this.panel_CommonSet = new System.Windows.Forms.Panel();
            this.panel_down = new System.Windows.Forms.Panel();
            this.button_Cancle = new System.Windows.Forms.Button();
            this.groupBox_anim.SuspendLayout();
            this.tableLayoutPanel_left.SuspendLayout();
            this.panel_LT.SuspendLayout();
            this.groupBox_Result.SuspendLayout();
            this.TLP_showResult.SuspendLayout();
            this.panel_ShowString.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel_all.SuspendLayout();
            this.tableLayoutPanel_up.SuspendLayout();
            this.groupBox_map.SuspendLayout();
            this.TLP_Map.SuspendLayout();
            this.panel_RT.SuspendLayout();
            this.TLP_Center.SuspendLayout();
            this.groupBox_Common.SuspendLayout();
            this.TLP_commonSet.SuspendLayout();
            this.panel_CommonSet.SuspendLayout();
            this.panel_down.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_OK
            // 
            this.button_OK.BackColor = System.Drawing.Color.Transparent;
            this.button_OK.ForeColor = System.Drawing.Color.Black;
            this.button_OK.Location = new System.Drawing.Point(15, 19);
            this.button_OK.Margin = new System.Windows.Forms.Padding(4, 4, 40, 4);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(91, 59);
            this.button_OK.TabIndex = 9;
            this.button_OK.Text = "导出";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // groupBox_anim
            // 
            this.groupBox_anim.Controls.Add(this.tableLayoutPanel_left);
            this.groupBox_anim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_anim.Location = new System.Drawing.Point(4, 4);
            this.groupBox_anim.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_anim.Name = "groupBox_anim";
            this.groupBox_anim.Padding = new System.Windows.Forms.Padding(11, 4, 11, 10);
            this.groupBox_anim.Size = new System.Drawing.Size(412, 136);
            this.groupBox_anim.TabIndex = 13;
            this.groupBox_anim.TabStop = false;
            this.groupBox_anim.Text = "动画导出设置";
            // 
            // tableLayoutPanel_left
            // 
            this.tableLayoutPanel_left.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel_left.ColumnCount = 1;
            this.tableLayoutPanel_left.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_left.Controls.Add(this.panel_LT, 0, 0);
            this.tableLayoutPanel_left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_left.Location = new System.Drawing.Point(11, 22);
            this.tableLayoutPanel_left.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_left.Name = "tableLayoutPanel_left";
            this.tableLayoutPanel_left.RowCount = 1;
            this.tableLayoutPanel_left.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_left.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel_left.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel_left.Size = new System.Drawing.Size(390, 104);
            this.tableLayoutPanel_left.TabIndex = 18;
            // 
            // panel_LT
            // 
            this.panel_LT.Controls.Add(this.checkBox_ActionOffset);
            this.panel_LT.Controls.Add(this.comboBox_ActionOffset);
            this.panel_LT.Controls.Add(this.checkBox_SplitAnimation);
            this.panel_LT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_LT.Location = new System.Drawing.Point(6, 6);
            this.panel_LT.Margin = new System.Windows.Forms.Padding(4);
            this.panel_LT.Name = "panel_LT";
            this.panel_LT.Size = new System.Drawing.Size(378, 92);
            this.panel_LT.TabIndex = 1;
            // 
            // checkBox_ActionOffset
            // 
            this.checkBox_ActionOffset.AutoSize = true;
            this.checkBox_ActionOffset.Location = new System.Drawing.Point(11, 11);
            this.checkBox_ActionOffset.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_ActionOffset.Name = "checkBox_ActionOffset";
            this.checkBox_ActionOffset.Size = new System.Drawing.Size(149, 19);
            this.checkBox_ActionOffset.TabIndex = 16;
            this.checkBox_ActionOffset.Text = "导出动作位移数据";
            this.checkBox_ActionOffset.UseVisualStyleBackColor = true;
            this.checkBox_ActionOffset.CheckedChanged += new System.EventHandler(this.checkBox_ActionOffset_CheckedChanged);
            // 
            // comboBox_ActionOffset
            // 
            this.comboBox_ActionOffset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ActionOffset.FormattingEnabled = true;
            this.comboBox_ActionOffset.Items.AddRange(new object[] {
            "X偏移",
            "XY偏移",
            "XYZ偏移"});
            this.comboBox_ActionOffset.Location = new System.Drawing.Point(173, 9);
            this.comboBox_ActionOffset.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_ActionOffset.Name = "comboBox_ActionOffset";
            this.comboBox_ActionOffset.Size = new System.Drawing.Size(87, 23);
            this.comboBox_ActionOffset.TabIndex = 15;
            this.comboBox_ActionOffset.SelectedIndexChanged += new System.EventHandler(this.comboBox_ActionOffset_SelectedIndexChanged);
            // 
            // checkBox_SplitAnimation
            // 
            this.checkBox_SplitAnimation.AutoSize = true;
            this.checkBox_SplitAnimation.Location = new System.Drawing.Point(11, 38);
            this.checkBox_SplitAnimation.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_SplitAnimation.Name = "checkBox_SplitAnimation";
            this.checkBox_SplitAnimation.Size = new System.Drawing.Size(149, 19);
            this.checkBox_SplitAnimation.TabIndex = 20;
            this.checkBox_SplitAnimation.Text = "分立存储动画数据";
            this.checkBox_SplitAnimation.UseVisualStyleBackColor = true;
            this.checkBox_SplitAnimation.CheckedChanged += new System.EventHandler(this.checkBox_SplitAnimation_CheckedChanged);
            // 
            // comboBox_animFormat
            // 
            this.comboBox_animFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_animFormat.FormattingEnabled = true;
            this.comboBox_animFormat.Items.AddRange(new object[] {
            "png",
            "bmp",
            "gif"});
            this.comboBox_animFormat.Location = new System.Drawing.Point(235, 8);
            this.comboBox_animFormat.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_animFormat.Name = "comboBox_animFormat";
            this.comboBox_animFormat.Size = new System.Drawing.Size(68, 23);
            this.comboBox_animFormat.TabIndex = 14;
            this.comboBox_animFormat.SelectedIndexChanged += new System.EventHandler(this.comboBox_animFormat_SelectedIndexChanged);
            // 
            // label_Format
            // 
            this.label_Format.AutoSize = true;
            this.label_Format.BackColor = System.Drawing.Color.Transparent;
            this.label_Format.Location = new System.Drawing.Point(117, 13);
            this.label_Format.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Format.Name = "label_Format";
            this.label_Format.Size = new System.Drawing.Size(112, 15);
            this.label_Format.TabIndex = 13;
            this.label_Format.Text = "指定图片格式：";
            // 
            // checkBox_confuseImg
            // 
            this.checkBox_confuseImg.AutoSize = true;
            this.checkBox_confuseImg.Location = new System.Drawing.Point(11, 12);
            this.checkBox_confuseImg.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_confuseImg.Name = "checkBox_confuseImg";
            this.checkBox_confuseImg.Size = new System.Drawing.Size(89, 19);
            this.checkBox_confuseImg.TabIndex = 15;
            this.checkBox_confuseImg.Text = "混淆图片";
            this.checkBox_confuseImg.UseVisualStyleBackColor = true;
            this.checkBox_confuseImg.CheckedChanged += new System.EventHandler(this.checkBox_confuseImg_CheckedChanged);
            // 
            // richTextBox_export
            // 
            this.richTextBox_export.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox_export.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_export.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_export.Location = new System.Drawing.Point(1, 1);
            this.richTextBox_export.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox_export.Name = "richTextBox_export";
            this.richTextBox_export.ReadOnly = true;
            this.richTextBox_export.Size = new System.Drawing.Size(805, 134);
            this.richTextBox_export.TabIndex = 14;
            this.richTextBox_export.Text = "";
            // 
            // groupBox_Result
            // 
            this.groupBox_Result.Controls.Add(this.TLP_showResult);
            this.groupBox_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Result.Location = new System.Drawing.Point(4, 246);
            this.groupBox_Result.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_Result.Name = "groupBox_Result";
            this.groupBox_Result.Padding = new System.Windows.Forms.Padding(9, 4, 9, 10);
            this.groupBox_Result.Size = new System.Drawing.Size(833, 206);
            this.groupBox_Result.TabIndex = 15;
            this.groupBox_Result.TabStop = false;
            this.groupBox_Result.Text = "显示导出结果";
            // 
            // TLP_showResult
            // 
            this.TLP_showResult.ColumnCount = 1;
            this.TLP_showResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_showResult.Controls.Add(this.panel_ShowString, 0, 0);
            this.TLP_showResult.Controls.Add(this.panel1, 0, 1);
            this.TLP_showResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_showResult.Location = new System.Drawing.Point(9, 22);
            this.TLP_showResult.Margin = new System.Windows.Forms.Padding(4);
            this.TLP_showResult.Name = "TLP_showResult";
            this.TLP_showResult.RowCount = 2;
            this.TLP_showResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_showResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TLP_showResult.Size = new System.Drawing.Size(815, 174);
            this.TLP_showResult.TabIndex = 0;
            // 
            // panel_ShowString
            // 
            this.panel_ShowString.AutoScroll = true;
            this.panel_ShowString.BackColor = System.Drawing.Color.DimGray;
            this.panel_ShowString.Controls.Add(this.richTextBox_export);
            this.panel_ShowString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ShowString.Location = new System.Drawing.Point(4, 4);
            this.panel_ShowString.Margin = new System.Windows.Forms.Padding(4);
            this.panel_ShowString.Name = "panel_ShowString";
            this.panel_ShowString.Padding = new System.Windows.Forms.Padding(1);
            this.panel_ShowString.Size = new System.Drawing.Size(807, 136);
            this.panel_ShowString.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar_Export);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 148);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(807, 22);
            this.panel1.TabIndex = 16;
            // 
            // progressBar_Export
            // 
            this.progressBar_Export.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar_Export.Location = new System.Drawing.Point(0, 0);
            this.progressBar_Export.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar_Export.Name = "progressBar_Export";
            this.progressBar_Export.Size = new System.Drawing.Size(807, 22);
            this.progressBar_Export.TabIndex = 12;
            // 
            // tableLayoutPanel_all
            // 
            this.tableLayoutPanel_all.ColumnCount = 1;
            this.tableLayoutPanel_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.Controls.Add(this.groupBox_Result, 0, 2);
            this.tableLayoutPanel_all.Controls.Add(this.tableLayoutPanel_up, 0, 0);
            this.tableLayoutPanel_all.Controls.Add(this.TLP_Center, 0, 1);
            this.tableLayoutPanel_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_all.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel_all.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_all.Name = "tableLayoutPanel_all";
            this.tableLayoutPanel_all.RowCount = 3;
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(841, 456);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // tableLayoutPanel_up
            // 
            this.tableLayoutPanel_up.ColumnCount = 2;
            this.tableLayoutPanel_up.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_up.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_up.Controls.Add(this.groupBox_map, 0, 0);
            this.tableLayoutPanel_up.Controls.Add(this.groupBox_anim, 0, 0);
            this.tableLayoutPanel_up.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_up.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_up.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_up.Name = "tableLayoutPanel_up";
            this.tableLayoutPanel_up.RowCount = 1;
            this.tableLayoutPanel_up.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_up.Size = new System.Drawing.Size(841, 144);
            this.tableLayoutPanel_up.TabIndex = 1;
            // 
            // groupBox_map
            // 
            this.groupBox_map.Controls.Add(this.TLP_Map);
            this.groupBox_map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_map.Location = new System.Drawing.Point(424, 4);
            this.groupBox_map.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_map.Name = "groupBox_map";
            this.groupBox_map.Padding = new System.Windows.Forms.Padding(11, 4, 11, 10);
            this.groupBox_map.Size = new System.Drawing.Size(413, 136);
            this.groupBox_map.TabIndex = 14;
            this.groupBox_map.TabStop = false;
            this.groupBox_map.Text = "地图导出设置";
            // 
            // TLP_Map
            // 
            this.TLP_Map.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.TLP_Map.ColumnCount = 1;
            this.TLP_Map.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Map.Controls.Add(this.panel_RT, 0, 0);
            this.TLP_Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Map.Location = new System.Drawing.Point(11, 22);
            this.TLP_Map.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_Map.Name = "TLP_Map";
            this.TLP_Map.RowCount = 1;
            this.TLP_Map.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Map.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.TLP_Map.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.TLP_Map.Size = new System.Drawing.Size(391, 104);
            this.TLP_Map.TabIndex = 18;
            // 
            // panel_RT
            // 
            this.panel_RT.Controls.Add(this.checkBox_CompileScrpts);
            this.panel_RT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_RT.Location = new System.Drawing.Point(6, 6);
            this.panel_RT.Margin = new System.Windows.Forms.Padding(4);
            this.panel_RT.Name = "panel_RT";
            this.panel_RT.Size = new System.Drawing.Size(379, 92);
            this.panel_RT.TabIndex = 1;
            // 
            // checkBox_CompileScrpts
            // 
            this.checkBox_CompileScrpts.AutoSize = true;
            this.checkBox_CompileScrpts.Location = new System.Drawing.Point(11, 11);
            this.checkBox_CompileScrpts.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_CompileScrpts.Name = "checkBox_CompileScrpts";
            this.checkBox_CompileScrpts.Size = new System.Drawing.Size(149, 19);
            this.checkBox_CompileScrpts.TabIndex = 20;
            this.checkBox_CompileScrpts.Text = "重新编译场景脚本";
            this.checkBox_CompileScrpts.UseVisualStyleBackColor = true;
            this.checkBox_CompileScrpts.CheckedChanged += new System.EventHandler(this.checkBox_CompileScrpts_CheckedChanged);
            // 
            // TLP_Center
            // 
            this.TLP_Center.ColumnCount = 2;
            this.TLP_Center.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.49207F));
            this.TLP_Center.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.50794F));
            this.TLP_Center.Controls.Add(this.groupBox_Common, 0, 0);
            this.TLP_Center.Controls.Add(this.panel_down, 1, 0);
            this.TLP_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Center.Location = new System.Drawing.Point(0, 144);
            this.TLP_Center.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_Center.Name = "TLP_Center";
            this.TLP_Center.RowCount = 1;
            this.TLP_Center.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Center.Size = new System.Drawing.Size(841, 98);
            this.TLP_Center.TabIndex = 10;
            // 
            // groupBox_Common
            // 
            this.groupBox_Common.Controls.Add(this.TLP_commonSet);
            this.groupBox_Common.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Common.Location = new System.Drawing.Point(4, 4);
            this.groupBox_Common.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_Common.Name = "groupBox_Common";
            this.groupBox_Common.Padding = new System.Windows.Forms.Padding(11, 4, 11, 10);
            this.groupBox_Common.Size = new System.Drawing.Size(610, 90);
            this.groupBox_Common.TabIndex = 16;
            this.groupBox_Common.TabStop = false;
            this.groupBox_Common.Text = "公共导出设置";
            // 
            // TLP_commonSet
            // 
            this.TLP_commonSet.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.TLP_commonSet.ColumnCount = 1;
            this.TLP_commonSet.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_commonSet.Controls.Add(this.panel_CommonSet, 0, 0);
            this.TLP_commonSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_commonSet.Location = new System.Drawing.Point(11, 22);
            this.TLP_commonSet.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_commonSet.Name = "TLP_commonSet";
            this.TLP_commonSet.RowCount = 1;
            this.TLP_commonSet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_commonSet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.TLP_commonSet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.TLP_commonSet.Size = new System.Drawing.Size(588, 58);
            this.TLP_commonSet.TabIndex = 18;
            // 
            // panel_CommonSet
            // 
            this.panel_CommonSet.Controls.Add(this.comboBox_animFormat);
            this.panel_CommonSet.Controls.Add(this.checkBox_confuseImg);
            this.panel_CommonSet.Controls.Add(this.label_Format);
            this.panel_CommonSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_CommonSet.Location = new System.Drawing.Point(6, 6);
            this.panel_CommonSet.Margin = new System.Windows.Forms.Padding(4);
            this.panel_CommonSet.Name = "panel_CommonSet";
            this.panel_CommonSet.Size = new System.Drawing.Size(576, 46);
            this.panel_CommonSet.TabIndex = 1;
            // 
            // panel_down
            // 
            this.panel_down.Controls.Add(this.button_OK);
            this.panel_down.Controls.Add(this.button_Cancle);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_down.Location = new System.Drawing.Point(622, 4);
            this.panel_down.Margin = new System.Windows.Forms.Padding(4);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(215, 90);
            this.panel_down.TabIndex = 0;
            // 
            // button_Cancle
            // 
            this.button_Cancle.BackColor = System.Drawing.Color.Transparent;
            this.button_Cancle.ForeColor = System.Drawing.Color.Black;
            this.button_Cancle.Location = new System.Drawing.Point(109, 19);
            this.button_Cancle.Margin = new System.Windows.Forms.Padding(4, 4, 40, 4);
            this.button_Cancle.Name = "button_Cancle";
            this.button_Cancle.Size = new System.Drawing.Size(91, 59);
            this.button_Cancle.TabIndex = 8;
            this.button_Cancle.Text = "关闭";
            this.button_Cancle.UseVisualStyleBackColor = false;
            this.button_Cancle.Click += new System.EventHandler(this.button_Cancle_Click);
            // 
            // SmallDialog_ExportConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(851, 466);
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmallDialog_ExportConfig";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "导出Cyclone工程";
            this.groupBox_anim.ResumeLayout(false);
            this.tableLayoutPanel_left.ResumeLayout(false);
            this.panel_LT.ResumeLayout(false);
            this.panel_LT.PerformLayout();
            this.groupBox_Result.ResumeLayout(false);
            this.TLP_showResult.ResumeLayout(false);
            this.panel_ShowString.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel_all.ResumeLayout(false);
            this.tableLayoutPanel_up.ResumeLayout(false);
            this.groupBox_map.ResumeLayout(false);
            this.TLP_Map.ResumeLayout(false);
            this.panel_RT.ResumeLayout(false);
            this.panel_RT.PerformLayout();
            this.TLP_Center.ResumeLayout(false);
            this.groupBox_Common.ResumeLayout(false);
            this.TLP_commonSet.ResumeLayout(false);
            this.panel_CommonSet.ResumeLayout(false);
            this.panel_CommonSet.PerformLayout();
            this.panel_down.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.GroupBox groupBox_anim;
        private System.Windows.Forms.RichTextBox richTextBox_export;
        private System.Windows.Forms.GroupBox groupBox_Result;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_all;
        private System.Windows.Forms.Panel panel_down;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_up;
        private System.Windows.Forms.Panel panel_ShowString;
        private System.Windows.Forms.Label label_Format;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_left;
        private System.Windows.Forms.Panel panel_LT;
        private System.Windows.Forms.CheckBox checkBox_confuseImg;
        private System.Windows.Forms.GroupBox groupBox_map;
        private System.Windows.Forms.TableLayoutPanel TLP_Map;
        private System.Windows.Forms.Panel panel_RT;
        private System.Windows.Forms.ComboBox comboBox_animFormat;
        private System.Windows.Forms.ProgressBar progressBar_Export;
        private System.Windows.Forms.TableLayoutPanel TLP_showResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Cancle;
        private System.Windows.Forms.CheckBox checkBox_SplitAnimation;
        private System.Windows.Forms.ComboBox comboBox_ActionOffset;
        private System.Windows.Forms.CheckBox checkBox_ActionOffset;
        private System.Windows.Forms.GroupBox groupBox_Common;
        private System.Windows.Forms.TableLayoutPanel TLP_commonSet;
        private System.Windows.Forms.Panel panel_CommonSet;
        private System.Windows.Forms.TableLayoutPanel TLP_Center;
        private System.Windows.Forms.CheckBox checkBox_CompileScrpts;
    }
}