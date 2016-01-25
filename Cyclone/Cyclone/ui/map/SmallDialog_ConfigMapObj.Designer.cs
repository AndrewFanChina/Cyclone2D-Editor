using Cyclone.Cyclone.ui.exctl;
namespace Cyclone.mod.map
{
    partial class SmallDialog_ConfigMapObj
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
            this.textBox_ATName = new System.Windows.Forms.TextBox();
            this.label_AT_Name = new System.Windows.Forms.Label();
            this.checkBox_active = new System.Windows.Forms.CheckBox();
            this.numericUpDown_FrameID = new System.Windows.Forms.NumericUpDown();
            this.label_FrameID = new System.Windows.Forms.Label();
            this.comboBox_ActionID = new System.Windows.Forms.ComboBox();
            this.label＿ActionID = new System.Windows.Forms.Label();
            this.numericUpDown_ID = new System.Windows.Forms.NumericUpDown();
            this.label_NPC_ID = new System.Windows.Forms.Label();
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_Self = new System.Windows.Forms.GroupBox();
            this.TLP_slefDefine = new System.Windows.Forms.TableLayoutPanel();
            this.panel_SelfRight = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_SelfLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox_Common = new System.Windows.Forms.GroupBox();
            this.panel_propCommon = new System.Windows.Forms.Panel();
            this.contextMenuStrip_SelfDefine = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加单元ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FrameID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ID)).BeginInit();
            this.tableLayoutPanel_all.SuspendLayout();
            this.groupBox_Self.SuspendLayout();
            this.TLP_slefDefine.SuspendLayout();
            this.groupBox_Common.SuspendLayout();
            this.panel_propCommon.SuspendLayout();
            this.contextMenuStrip_SelfDefine.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_ATName
            // 
            this.textBox_ATName.Location = new System.Drawing.Point(47, 5);
            this.textBox_ATName.Name = "textBox_ATName";
            this.textBox_ATName.ReadOnly = true;
            this.textBox_ATName.Size = new System.Drawing.Size(117, 21);
            this.textBox_ATName.TabIndex = 17;
            // 
            // label_AT_Name
            // 
            this.label_AT_Name.AutoSize = true;
            this.label_AT_Name.Location = new System.Drawing.Point(3, 9);
            this.label_AT_Name.Name = "label_AT_Name";
            this.label_AT_Name.Size = new System.Drawing.Size(53, 12);
            this.label_AT_Name.TabIndex = 16;
            this.label_AT_Name.Text = "原型ID：";
            // 
            // checkBox_active
            // 
            this.checkBox_active.AutoSize = true;
            this.checkBox_active.Checked = true;
            this.checkBox_active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_active.Location = new System.Drawing.Point(3, 57);
            this.checkBox_active.Name = "checkBox_active";
            this.checkBox_active.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox_active.Size = new System.Drawing.Size(48, 16);
            this.checkBox_active.TabIndex = 15;
            this.checkBox_active.Text = "可见";
            this.checkBox_active.UseVisualStyleBackColor = true;
            this.checkBox_active.CheckedChanged += new System.EventHandler(this.checkBox_active_CheckedChanged);
            // 
            // numericUpDown_FrameID
            // 
            this.numericUpDown_FrameID.Location = new System.Drawing.Point(217, 29);
            this.numericUpDown_FrameID.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDown_FrameID.Name = "numericUpDown_FrameID";
            this.numericUpDown_FrameID.Size = new System.Drawing.Size(96, 21);
            this.numericUpDown_FrameID.TabIndex = 13;
            this.numericUpDown_FrameID.ValueChanged += new System.EventHandler(this.numericUpDown_FrameID_ValueChanged);
            // 
            // label_FrameID
            // 
            this.label_FrameID.AutoSize = true;
            this.label_FrameID.Location = new System.Drawing.Point(171, 33);
            this.label_FrameID.Name = "label_FrameID";
            this.label_FrameID.Size = new System.Drawing.Size(53, 12);
            this.label_FrameID.TabIndex = 12;
            this.label_FrameID.Text = "时间ID：";
            // 
            // comboBox_ActionID
            // 
            this.comboBox_ActionID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ActionID.FormattingEnabled = true;
            this.comboBox_ActionID.Location = new System.Drawing.Point(47, 29);
            this.comboBox_ActionID.Name = "comboBox_ActionID";
            this.comboBox_ActionID.Size = new System.Drawing.Size(117, 20);
            this.comboBox_ActionID.TabIndex = 11;
            this.comboBox_ActionID.SelectedIndexChanged += new System.EventHandler(this.comboBox_ActionID_SelectedIndexChanged);
            // 
            // label＿ActionID
            // 
            this.label＿ActionID.AutoSize = true;
            this.label＿ActionID.Location = new System.Drawing.Point(3, 33);
            this.label＿ActionID.Name = "label＿ActionID";
            this.label＿ActionID.Size = new System.Drawing.Size(53, 12);
            this.label＿ActionID.TabIndex = 10;
            this.label＿ActionID.Text = "动作ID：";
            // 
            // numericUpDown_ID
            // 
            this.numericUpDown_ID.Location = new System.Drawing.Point(217, 5);
            this.numericUpDown_ID.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numericUpDown_ID.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numericUpDown_ID.Name = "numericUpDown_ID";
            this.numericUpDown_ID.Size = new System.Drawing.Size(96, 21);
            this.numericUpDown_ID.TabIndex = 9;
            this.numericUpDown_ID.ValueChanged += new System.EventHandler(this.numericUpDown_ID_ValueChanged);
            // 
            // label_NPC_ID
            // 
            this.label_NPC_ID.AutoSize = true;
            this.label_NPC_ID.Location = new System.Drawing.Point(171, 9);
            this.label_NPC_ID.Name = "label_NPC_ID";
            this.label_NPC_ID.Size = new System.Drawing.Size(53, 12);
            this.label_NPC_ID.TabIndex = 1;
            this.label_NPC_ID.Text = "NPC ID：";
            // 
            // tableLayoutPanel_all
            // 
            this.tableLayoutPanel_all.ColumnCount = 1;
            this.tableLayoutPanel_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.Controls.Add(this.groupBox_Self, 0, 1);
            this.tableLayoutPanel_all.Controls.Add(this.groupBox_Common, 0, 0);
            this.tableLayoutPanel_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_all.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel_all.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_all.Name = "tableLayoutPanel_all";
            this.tableLayoutPanel_all.RowCount = 2;
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 168F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(335, 280);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // groupBox_Self
            // 
            this.groupBox_Self.Controls.Add(this.TLP_slefDefine);
            this.groupBox_Self.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Self.Location = new System.Drawing.Point(3, 115);
            this.groupBox_Self.Name = "groupBox_Self";
            this.groupBox_Self.Size = new System.Drawing.Size(329, 162);
            this.groupBox_Self.TabIndex = 19;
            this.groupBox_Self.TabStop = false;
            this.groupBox_Self.Text = "自定义参数";
            // 
            // TLP_slefDefine
            // 
            this.TLP_slefDefine.ColumnCount = 2;
            this.TLP_slefDefine.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_slefDefine.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_slefDefine.Controls.Add(this.panel_SelfRight, 1, 0);
            this.TLP_slefDefine.Controls.Add(this.panel_SelfLeft, 0, 0);
            this.TLP_slefDefine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_slefDefine.Location = new System.Drawing.Point(3, 17);
            this.TLP_slefDefine.Name = "TLP_slefDefine";
            this.TLP_slefDefine.RowCount = 1;
            this.TLP_slefDefine.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_slefDefine.Size = new System.Drawing.Size(323, 142);
            this.TLP_slefDefine.TabIndex = 0;
            // 
            // panel_SelfRight
            // 
            this.panel_SelfRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_SelfRight.Location = new System.Drawing.Point(164, 3);
            this.panel_SelfRight.Name = "panel_SelfRight";
            this.panel_SelfRight.Size = new System.Drawing.Size(156, 136);
            this.panel_SelfRight.TabIndex = 1;
            this.panel_SelfRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_SelfRight_MouseDown);
            // 
            // panel_SelfLeft
            // 
            this.panel_SelfLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_SelfLeft.Location = new System.Drawing.Point(3, 3);
            this.panel_SelfLeft.Name = "panel_SelfLeft";
            this.panel_SelfLeft.Size = new System.Drawing.Size(155, 136);
            this.panel_SelfLeft.TabIndex = 0;
            this.panel_SelfLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_SelfRight_MouseDown);
            // 
            // groupBox_Common
            // 
            this.groupBox_Common.Controls.Add(this.panel_propCommon);
            this.groupBox_Common.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Common.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Common.Name = "groupBox_Common";
            this.groupBox_Common.Size = new System.Drawing.Size(329, 106);
            this.groupBox_Common.TabIndex = 18;
            this.groupBox_Common.TabStop = false;
            this.groupBox_Common.Text = "通用的参数";
            // 
            // panel_propCommon
            // 
            this.panel_propCommon.Controls.Add(this.numericUpDown_ID);
            this.panel_propCommon.Controls.Add(this.textBox_ATName);
            this.panel_propCommon.Controls.Add(this.label_NPC_ID);
            this.panel_propCommon.Controls.Add(this.label_AT_Name);
            this.panel_propCommon.Controls.Add(this.checkBox_active);
            this.panel_propCommon.Controls.Add(this.comboBox_ActionID);
            this.panel_propCommon.Controls.Add(this.numericUpDown_FrameID);
            this.panel_propCommon.Controls.Add(this.label_FrameID);
            this.panel_propCommon.Controls.Add(this.label＿ActionID);
            this.panel_propCommon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_propCommon.Location = new System.Drawing.Point(3, 17);
            this.panel_propCommon.Name = "panel_propCommon";
            this.panel_propCommon.Size = new System.Drawing.Size(323, 86);
            this.panel_propCommon.TabIndex = 0;
            // 
            // contextMenuStrip_SelfDefine
            // 
            this.contextMenuStrip_SelfDefine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加单元ToolStripMenuItem,
            this.上移ToolStripMenuItem,
            this.下移ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip_SelfDefine.Name = "contextMenuStrip_actor";
            this.contextMenuStrip_SelfDefine.ShowCheckMargin = true;
            this.contextMenuStrip_SelfDefine.ShowImageMargin = false;
            this.contextMenuStrip_SelfDefine.Size = new System.Drawing.Size(153, 114);
            // 
            // 添加单元ToolStripMenuItem
            // 
            this.添加单元ToolStripMenuItem.Name = "添加单元ToolStripMenuItem";
            this.添加单元ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加单元ToolStripMenuItem.Text = "添加参数单元";
            this.添加单元ToolStripMenuItem.Click += new System.EventHandler(this.添加单元ToolStripMenuItem_Click);
            // 
            // 上移ToolStripMenuItem
            // 
            this.上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
            this.上移ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.上移ToolStripMenuItem.Text = "上移参数单元";
            this.上移ToolStripMenuItem.Click += new System.EventHandler(this.上移ToolStripMenuItem_Click);
            // 
            // 下移ToolStripMenuItem
            // 
            this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
            this.下移ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.下移ToolStripMenuItem.Text = "下移参数单元";
            this.下移ToolStripMenuItem.Click += new System.EventHandler(this.下移ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除参数单元";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // SmallDialog_ConfigMapObj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(343, 288);
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmallDialog_ConfigMapObj";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "对象参数设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SmallDialog_MapEventObjConfig_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FrameID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ID)).EndInit();
            this.tableLayoutPanel_all.ResumeLayout(false);
            this.groupBox_Self.ResumeLayout(false);
            this.TLP_slefDefine.ResumeLayout(false);
            this.groupBox_Common.ResumeLayout(false);
            this.panel_propCommon.ResumeLayout(false);
            this.panel_propCommon.PerformLayout();
            this.contextMenuStrip_SelfDefine.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_NPC_ID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_all;
        private System.Windows.Forms.NumericUpDown numericUpDown_ID;
        private System.Windows.Forms.Label label＿ActionID;
        private System.Windows.Forms.ComboBox comboBox_ActionID;
        private System.Windows.Forms.Label label_FrameID;
        private System.Windows.Forms.NumericUpDown numericUpDown_FrameID;
        private System.Windows.Forms.CheckBox checkBox_active;
        private System.Windows.Forms.TextBox textBox_ATName;
        private System.Windows.Forms.Label label_AT_Name;
        private System.Windows.Forms.GroupBox groupBox_Common;
        private System.Windows.Forms.Panel panel_propCommon;
        private System.Windows.Forms.GroupBox groupBox_Self;
        private System.Windows.Forms.TableLayoutPanel TLP_slefDefine;
        private System.Windows.Forms.FlowLayoutPanel panel_SelfRight;
        private System.Windows.Forms.FlowLayoutPanel panel_SelfLeft;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip_SelfDefine;
        private System.Windows.Forms.ToolStripMenuItem 添加单元ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
    }
}