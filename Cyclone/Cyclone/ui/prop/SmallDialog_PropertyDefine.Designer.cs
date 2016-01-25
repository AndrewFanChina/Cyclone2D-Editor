namespace Cyclone.mod.prop
{
    partial class SmallDialog_PropertyDefine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmallDialog_PropertyDefine));
            this.panel_down = new System.Windows.Forms.Panel();
            this.checkBox_refreshAll = new System.Windows.Forms.CheckBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancle = new System.Windows.Forms.Button();
            this.panel_config = new System.Windows.Forms.Panel();
            this.button_clearPropID = new System.Windows.Forms.Button();
            this.comboBox_Prop = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Value = new System.Windows.Forms.ComboBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_Value = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
            this.panel_init = new System.Windows.Forms.Panel();
            this.button_clearPropIDDef = new System.Windows.Forms.Button();
            this.numericUpDown_def = new System.Windows.Forms.NumericUpDown();
            this.comboBox_constDef = new System.Windows.Forms.ComboBox();
            this.comboBox_PropDef = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_def = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel_down.SuspendLayout();
            this.panel_config.SuspendLayout();
            this.tableLayoutPanel_all.SuspendLayout();
            this.TLP_Main.SuspendLayout();
            this.panel_init.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_def)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_down
            // 
            this.panel_down.Controls.Add(this.checkBox_refreshAll);
            this.panel_down.Controls.Add(this.button_OK);
            this.panel_down.Controls.Add(this.button_Cancle);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_down.Location = new System.Drawing.Point(3, 235);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(476, 42);
            this.panel_down.TabIndex = 0;
            // 
            // checkBox_refreshAll
            // 
            this.checkBox_refreshAll.AutoSize = true;
            this.checkBox_refreshAll.Location = new System.Drawing.Point(7, 13);
            this.checkBox_refreshAll.Name = "checkBox_refreshAll";
            this.checkBox_refreshAll.Size = new System.Drawing.Size(120, 16);
            this.checkBox_refreshAll.TabIndex = 10;
            this.checkBox_refreshAll.Text = "全部刷新为默认值";
            this.checkBox_refreshAll.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_OK.BackColor = System.Drawing.Color.Transparent;
            this.button_OK.ForeColor = System.Drawing.Color.Black;
            this.button_OK.Location = new System.Drawing.Point(156, 8);
            this.button_OK.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(54, 27);
            this.button_OK.TabIndex = 9;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancle
            // 
            this.button_Cancle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_Cancle.BackColor = System.Drawing.Color.Transparent;
            this.button_Cancle.ForeColor = System.Drawing.Color.Black;
            this.button_Cancle.Location = new System.Drawing.Point(267, 8);
            this.button_Cancle.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_Cancle.Name = "button_Cancle";
            this.button_Cancle.Size = new System.Drawing.Size(54, 27);
            this.button_Cancle.TabIndex = 8;
            this.button_Cancle.Text = "取消";
            this.button_Cancle.UseVisualStyleBackColor = false;
            this.button_Cancle.Click += new System.EventHandler(this.button_Cancle_Click);
            // 
            // panel_config
            // 
            this.panel_config.Controls.Add(this.button_clearPropID);
            this.panel_config.Controls.Add(this.comboBox_Prop);
            this.panel_config.Controls.Add(this.label1);
            this.panel_config.Controls.Add(this.comboBox_Value);
            this.panel_config.Controls.Add(this.textBox_name);
            this.panel_config.Controls.Add(this.label_Value);
            this.panel_config.Controls.Add(this.label_name);
            this.panel_config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_config.Location = new System.Drawing.Point(5, 5);
            this.panel_config.Name = "panel_config";
            this.panel_config.Size = new System.Drawing.Size(466, 75);
            this.panel_config.TabIndex = 2;
            // 
            // button_clearPropID
            // 
            this.button_clearPropID.Location = new System.Drawing.Point(412, 43);
            this.button_clearPropID.Name = "button_clearPropID";
            this.button_clearPropID.Size = new System.Drawing.Size(22, 22);
            this.button_clearPropID.TabIndex = 19;
            this.button_clearPropID.Text = "×";
            this.button_clearPropID.UseVisualStyleBackColor = true;
            this.button_clearPropID.Click += new System.EventHandler(this.button_clearPropID_Click);
            // 
            // comboBox_Prop
            // 
            this.comboBox_Prop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Prop.FormattingEnabled = true;
            this.comboBox_Prop.Location = new System.Drawing.Point(90, 44);
            this.comboBox_Prop.Name = "comboBox_Prop";
            this.comboBox_Prop.Size = new System.Drawing.Size(319, 20);
            this.comboBox_Prop.TabIndex = 10;
            this.comboBox_Prop.SelectedIndexChanged += new System.EventHandler(this.comboBox_Prop_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "属性分类：";
            // 
            // comboBox_Value
            // 
            this.comboBox_Value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Value.FormattingEnabled = true;
            this.comboBox_Value.Items.AddRange(new object[] {
            "数值型",
            "字符型",
            "属性ID",
            "常量ID"});
            this.comboBox_Value.Location = new System.Drawing.Point(298, 17);
            this.comboBox_Value.Name = "comboBox_Value";
            this.comboBox_Value.Size = new System.Drawing.Size(135, 20);
            this.comboBox_Value.TabIndex = 8;
            this.comboBox_Value.SelectedIndexChanged += new System.EventHandler(this.comboBox_Value_SelectedIndexChanged);
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(90, 16);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(144, 21);
            this.textBox_name.TabIndex = 7;
            // 
            // label_Value
            // 
            this.label_Value.AutoSize = true;
            this.label_Value.Location = new System.Drawing.Point(236, 21);
            this.label_Value.Name = "label_Value";
            this.label_Value.Size = new System.Drawing.Size(65, 12);
            this.label_Value.TabIndex = 6;
            this.label_Value.Text = "单元类型：";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(32, 20);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(65, 12);
            this.label_name.TabIndex = 1;
            this.label_name.Text = "单元名称：";
            // 
            // tableLayoutPanel_all
            // 
            this.tableLayoutPanel_all.ColumnCount = 1;
            this.tableLayoutPanel_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.Controls.Add(this.panel_down, 0, 1);
            this.tableLayoutPanel_all.Controls.Add(this.TLP_Main, 0, 0);
            this.tableLayoutPanel_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_all.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel_all.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_all.Name = "tableLayoutPanel_all";
            this.tableLayoutPanel_all.RowCount = 2;
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(482, 280);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // TLP_Main
            // 
            this.TLP_Main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.TLP_Main.ColumnCount = 1;
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Main.Controls.Add(this.panel_init, 0, 1);
            this.TLP_Main.Controls.Add(this.panel_config, 0, 0);
            this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Main.Location = new System.Drawing.Point(3, 3);
            this.TLP_Main.Name = "TLP_Main";
            this.TLP_Main.RowCount = 2;
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.92946F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.07054F));
            this.TLP_Main.Size = new System.Drawing.Size(476, 226);
            this.TLP_Main.TabIndex = 1;
            // 
            // panel_init
            // 
            this.panel_init.Controls.Add(this.button_clearPropIDDef);
            this.panel_init.Controls.Add(this.numericUpDown_def);
            this.panel_init.Controls.Add(this.comboBox_constDef);
            this.panel_init.Controls.Add(this.comboBox_PropDef);
            this.panel_init.Controls.Add(this.label3);
            this.panel_init.Controls.Add(this.label2);
            this.panel_init.Controls.Add(this.label5);
            this.panel_init.Controls.Add(this.textBox_def);
            this.panel_init.Controls.Add(this.label4);
            this.panel_init.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_init.Location = new System.Drawing.Point(5, 88);
            this.panel_init.Name = "panel_init";
            this.panel_init.Size = new System.Drawing.Size(466, 133);
            this.panel_init.TabIndex = 3;
            // 
            // button_clearPropIDDef
            // 
            this.button_clearPropIDDef.Location = new System.Drawing.Point(412, 69);
            this.button_clearPropIDDef.Name = "button_clearPropIDDef";
            this.button_clearPropIDDef.Size = new System.Drawing.Size(22, 22);
            this.button_clearPropIDDef.TabIndex = 33;
            this.button_clearPropIDDef.Text = "×";
            this.button_clearPropIDDef.UseVisualStyleBackColor = true;
            this.button_clearPropIDDef.Click += new System.EventHandler(this.button_clearPropIDDef_Click);
            // 
            // numericUpDown_def
            // 
            this.numericUpDown_def.Location = new System.Drawing.Point(118, 17);
            this.numericUpDown_def.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDown_def.Minimum = new decimal(new int[] {
            65535,
            0,
            0,
            -2147483648});
            this.numericUpDown_def.Name = "numericUpDown_def";
            this.numericUpDown_def.Size = new System.Drawing.Size(316, 21);
            this.numericUpDown_def.TabIndex = 32;
            // 
            // comboBox_constDef
            // 
            this.comboBox_constDef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_constDef.FormattingEnabled = true;
            this.comboBox_constDef.Location = new System.Drawing.Point(118, 95);
            this.comboBox_constDef.Name = "comboBox_constDef";
            this.comboBox_constDef.Size = new System.Drawing.Size(316, 20);
            this.comboBox_constDef.TabIndex = 31;
            // 
            // comboBox_PropDef
            // 
            this.comboBox_PropDef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PropDef.FormattingEnabled = true;
            this.comboBox_PropDef.Location = new System.Drawing.Point(118, 70);
            this.comboBox_PropDef.Name = "comboBox_PropDef";
            this.comboBox_PropDef.Size = new System.Drawing.Size(291, 20);
            this.comboBox_PropDef.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "常量ID默认值：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "属性ID默认值：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "数值型默认值：";
            // 
            // textBox_def
            // 
            this.textBox_def.Location = new System.Drawing.Point(118, 44);
            this.textBox_def.Name = "textBox_def";
            this.textBox_def.Size = new System.Drawing.Size(316, 21);
            this.textBox_def.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "字符型默认值：";
            // 
            // SmallDialog_PropertyDefine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(490, 288);
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmallDialog_PropertyDefine";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建属性单元";
            this.panel_down.ResumeLayout(false);
            this.panel_down.PerformLayout();
            this.panel_config.ResumeLayout(false);
            this.panel_config.PerformLayout();
            this.tableLayoutPanel_all.ResumeLayout(false);
            this.TLP_Main.ResumeLayout(false);
            this.panel_init.ResumeLayout(false);
            this.panel_init.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_def)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_down;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancle;
        private System.Windows.Forms.Panel panel_config;
        private System.Windows.Forms.Label label_Value;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_all;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.ComboBox comboBox_Value;
        private System.Windows.Forms.ComboBox comboBox_Prop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_clearPropID;
        private System.Windows.Forms.TableLayoutPanel TLP_Main;
        private System.Windows.Forms.Panel panel_init;
        private System.Windows.Forms.TextBox textBox_def;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_constDef;
        private System.Windows.Forms.ComboBox comboBox_PropDef;
        private System.Windows.Forms.NumericUpDown numericUpDown_def;
        private System.Windows.Forms.Button button_clearPropIDDef;
        private System.Windows.Forms.CheckBox checkBox_refreshAll;
    }
}