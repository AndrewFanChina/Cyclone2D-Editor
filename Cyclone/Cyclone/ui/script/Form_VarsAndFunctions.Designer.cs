namespace Cyclone.ui_script
{
    partial class Form_VarsAndFunctions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_VarsAndFunctions));
            this.TLP_All = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Bootons = new System.Windows.Forms.Panel();
            this.button_genHead = new System.Windows.Forms.Button();
            this.label_lineNumber = new System.Windows.Forms.Label();
            this.button_order = new System.Windows.Forms.Button();
            this.button_moveDown = new System.Windows.Forms.Button();
            this.button_moveUp = new System.Windows.Forms.Button();
            this.button_Check = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.tabControl_Lists = new System.Windows.Forms.TabControl();
            this.tabPage_int = new System.Windows.Forms.TabPage();
            this.listBox_VarInt = new System.Windows.Forms.ListBox();
            this.tabPage_String = new System.Windows.Forms.TabPage();
            this.listBox_VarString = new System.Windows.Forms.ListBox();
            this.tabPage＿Trigger = new System.Windows.Forms.TabPage();
            this.listBox_Trigger = new System.Windows.Forms.ListBox();
            this.tabPage_Context = new System.Windows.Forms.TabPage();
            this.listBox_Condition = new System.Windows.Forms.ListBox();
            this.tabPage_Excution = new System.Windows.Forms.TabPage();
            this.listBox_Execution = new System.Windows.Forms.ListBox();
            this.TLP_All.SuspendLayout();
            this.panel_Bootons.SuspendLayout();
            this.tabControl_Lists.SuspendLayout();
            this.tabPage_int.SuspendLayout();
            this.tabPage_String.SuspendLayout();
            this.tabPage＿Trigger.SuspendLayout();
            this.tabPage_Context.SuspendLayout();
            this.tabPage_Excution.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLP_All
            // 
            this.TLP_All.ColumnCount = 1;
            this.TLP_All.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_All.Controls.Add(this.panel_Bootons, 0, 1);
            this.TLP_All.Controls.Add(this.tabControl_Lists, 0, 0);
            this.TLP_All.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_All.Location = new System.Drawing.Point(0, 0);
            this.TLP_All.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_All.Name = "TLP_All";
            this.TLP_All.RowCount = 2;
            this.TLP_All.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_All.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.TLP_All.Size = new System.Drawing.Size(754, 523);
            this.TLP_All.TabIndex = 1;
            // 
            // panel_Bootons
            // 
            this.panel_Bootons.Controls.Add(this.button_genHead);
            this.panel_Bootons.Controls.Add(this.label_lineNumber);
            this.panel_Bootons.Controls.Add(this.button_order);
            this.panel_Bootons.Controls.Add(this.button_moveDown);
            this.panel_Bootons.Controls.Add(this.button_moveUp);
            this.panel_Bootons.Controls.Add(this.button_Check);
            this.panel_Bootons.Controls.Add(this.button_Close);
            this.panel_Bootons.Controls.Add(this.button_Delete);
            this.panel_Bootons.Controls.Add(this.button_Add);
            this.panel_Bootons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Bootons.Location = new System.Drawing.Point(2, 480);
            this.panel_Bootons.Margin = new System.Windows.Forms.Padding(2);
            this.panel_Bootons.Name = "panel_Bootons";
            this.panel_Bootons.Size = new System.Drawing.Size(750, 41);
            this.panel_Bootons.TabIndex = 1;
            // 
            // button_genHead
            // 
            this.button_genHead.Location = new System.Drawing.Point(276, 6);
            this.button_genHead.Name = "button_genHead";
            this.button_genHead.Size = new System.Drawing.Size(73, 28);
            this.button_genHead.TabIndex = 28;
            this.button_genHead.Text = "生成头文件";
            this.button_genHead.UseVisualStyleBackColor = true;
            this.button_genHead.Click += new System.EventHandler(this.button_genHead_Click);
            // 
            // label_lineNumber
            // 
            this.label_lineNumber.AutoSize = true;
            this.label_lineNumber.Location = new System.Drawing.Point(187, 14);
            this.label_lineNumber.Name = "label_lineNumber";
            this.label_lineNumber.Size = new System.Drawing.Size(53, 12);
            this.label_lineNumber.TabIndex = 27;
            this.label_lineNumber.Text = "当前行号";
            // 
            // button_order
            // 
            this.button_order.BackgroundImage = global::Cyclone.Properties.Resources.moveDown;
            this.button_order.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_order.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_order.Location = new System.Drawing.Point(98, 6);
            this.button_order.Margin = new System.Windows.Forms.Padding(0);
            this.button_order.Name = "button_order";
            this.button_order.Size = new System.Drawing.Size(28, 28);
            this.button_order.TabIndex = 26;
            this.button_order.UseVisualStyleBackColor = true;
            this.button_order.Click += new System.EventHandler(this.button_order_Click);
            // 
            // button_moveDown
            // 
            this.button_moveDown.BackgroundImage = global::Cyclone.Properties.Resources.down;
            this.button_moveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_moveDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_moveDown.Location = new System.Drawing.Point(69, 6);
            this.button_moveDown.Margin = new System.Windows.Forms.Padding(0);
            this.button_moveDown.Name = "button_moveDown";
            this.button_moveDown.Size = new System.Drawing.Size(28, 28);
            this.button_moveDown.TabIndex = 25;
            this.button_moveDown.UseVisualStyleBackColor = true;
            this.button_moveDown.Click += new System.EventHandler(this.button_moveDown_Click);
            // 
            // button_moveUp
            // 
            this.button_moveUp.BackgroundImage = global::Cyclone.Properties.Resources.Up;
            this.button_moveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_moveUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_moveUp.Location = new System.Drawing.Point(40, 6);
            this.button_moveUp.Margin = new System.Windows.Forms.Padding(0);
            this.button_moveUp.Name = "button_moveUp";
            this.button_moveUp.Size = new System.Drawing.Size(28, 28);
            this.button_moveUp.TabIndex = 24;
            this.button_moveUp.UseVisualStyleBackColor = true;
            this.button_moveUp.Click += new System.EventHandler(this.button_moveUp_Click);
            // 
            // button_Check
            // 
            this.button_Check.BackgroundImage = global::Cyclone.Properties.Resources.infor;
            this.button_Check.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_Check.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Check.Location = new System.Drawing.Point(127, 6);
            this.button_Check.Margin = new System.Windows.Forms.Padding(0);
            this.button_Check.Name = "button_Check";
            this.button_Check.Size = new System.Drawing.Size(28, 28);
            this.button_Check.TabIndex = 23;
            this.button_Check.UseVisualStyleBackColor = true;
            this.button_Check.Click += new System.EventHandler(this.button_Check_Click);
            // 
            // button_Close
            // 
            this.button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Close.Location = new System.Drawing.Point(674, 6);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(52, 28);
            this.button_Close.TabIndex = 22;
            this.button_Close.Text = "关闭";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.BackgroundImage = global::Cyclone.Properties.Resources.delete;
            this.button_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Delete.Location = new System.Drawing.Point(156, 6);
            this.button_Delete.Margin = new System.Windows.Forms.Padding(0);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(28, 28);
            this.button_Delete.TabIndex = 21;
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_ActionFrame_Del_Click);
            // 
            // button_Add
            // 
            this.button_Add.BackgroundImage = global::Cyclone.Properties.Resources.addItem;
            this.button_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Add.Location = new System.Drawing.Point(11, 6);
            this.button_Add.Margin = new System.Windows.Forms.Padding(0);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(28, 28);
            this.button_Add.TabIndex = 20;
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_AddElement_Click);
            // 
            // tabControl_Lists
            // 
            this.tabControl_Lists.Controls.Add(this.tabPage_int);
            this.tabControl_Lists.Controls.Add(this.tabPage_String);
            this.tabControl_Lists.Controls.Add(this.tabPage＿Trigger);
            this.tabControl_Lists.Controls.Add(this.tabPage_Context);
            this.tabControl_Lists.Controls.Add(this.tabPage_Excution);
            this.tabControl_Lists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Lists.Location = new System.Drawing.Point(3, 3);
            this.tabControl_Lists.Name = "tabControl_Lists";
            this.tabControl_Lists.Padding = new System.Drawing.Point(20, 3);
            this.tabControl_Lists.SelectedIndex = 0;
            this.tabControl_Lists.Size = new System.Drawing.Size(748, 472);
            this.tabControl_Lists.TabIndex = 2;
            // 
            // tabPage_int
            // 
            this.tabPage_int.Controls.Add(this.listBox_VarInt);
            this.tabPage_int.Location = new System.Drawing.Point(4, 22);
            this.tabPage_int.Name = "tabPage_int";
            this.tabPage_int.Size = new System.Drawing.Size(740, 446);
            this.tabPage_int.TabIndex = 0;
            this.tabPage_int.Text = "整型变量";
            this.tabPage_int.UseVisualStyleBackColor = true;
            // 
            // listBox_VarInt
            // 
            this.listBox_VarInt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_VarInt.FormattingEnabled = true;
            this.listBox_VarInt.ItemHeight = 12;
            this.listBox_VarInt.Location = new System.Drawing.Point(0, 0);
            this.listBox_VarInt.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_VarInt.Name = "listBox_VarInt";
            this.listBox_VarInt.Size = new System.Drawing.Size(740, 436);
            this.listBox_VarInt.TabIndex = 0;
            this.listBox_VarInt.SelectedIndexChanged += new System.EventHandler(this.listBox_common_SelectedIndexChanged);
            this.listBox_VarInt.DoubleClick += new System.EventHandler(this.listBox_VarInt_DoubleClick);
            this.listBox_VarInt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_VarInt_KeyDown);
            // 
            // tabPage_String
            // 
            this.tabPage_String.Controls.Add(this.listBox_VarString);
            this.tabPage_String.Location = new System.Drawing.Point(4, 22);
            this.tabPage_String.Name = "tabPage_String";
            this.tabPage_String.Size = new System.Drawing.Size(740, 446);
            this.tabPage_String.TabIndex = 1;
            this.tabPage_String.Text = "字符变量";
            this.tabPage_String.UseVisualStyleBackColor = true;
            // 
            // listBox_VarString
            // 
            this.listBox_VarString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_VarString.FormattingEnabled = true;
            this.listBox_VarString.ItemHeight = 12;
            this.listBox_VarString.Location = new System.Drawing.Point(0, 0);
            this.listBox_VarString.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_VarString.Name = "listBox_VarString";
            this.listBox_VarString.Size = new System.Drawing.Size(740, 436);
            this.listBox_VarString.TabIndex = 1;
            this.listBox_VarString.SelectedIndexChanged += new System.EventHandler(this.listBox_common_SelectedIndexChanged);
            this.listBox_VarString.DoubleClick += new System.EventHandler(this.listBox_VarString_DoubleClick);
            this.listBox_VarString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_VarString_KeyDown);
            // 
            // tabPage＿Trigger
            // 
            this.tabPage＿Trigger.Controls.Add(this.listBox_Trigger);
            this.tabPage＿Trigger.Location = new System.Drawing.Point(4, 22);
            this.tabPage＿Trigger.Name = "tabPage＿Trigger";
            this.tabPage＿Trigger.Size = new System.Drawing.Size(740, 446);
            this.tabPage＿Trigger.TabIndex = 2;
            this.tabPage＿Trigger.Text = "触发函数";
            this.tabPage＿Trigger.UseVisualStyleBackColor = true;
            // 
            // listBox_Trigger
            // 
            this.listBox_Trigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Trigger.FormattingEnabled = true;
            this.listBox_Trigger.ItemHeight = 12;
            this.listBox_Trigger.Location = new System.Drawing.Point(0, 0);
            this.listBox_Trigger.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_Trigger.Name = "listBox_Trigger";
            this.listBox_Trigger.Size = new System.Drawing.Size(740, 436);
            this.listBox_Trigger.TabIndex = 2;
            this.listBox_Trigger.SelectedIndexChanged += new System.EventHandler(this.listBox_common_SelectedIndexChanged);
            this.listBox_Trigger.DoubleClick += new System.EventHandler(this.listBox_Trigger_DoubleClick);
            this.listBox_Trigger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_Trigger_KeyDown);
            // 
            // tabPage_Context
            // 
            this.tabPage_Context.Controls.Add(this.listBox_Condition);
            this.tabPage_Context.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Context.Name = "tabPage_Context";
            this.tabPage_Context.Size = new System.Drawing.Size(740, 446);
            this.tabPage_Context.TabIndex = 3;
            this.tabPage_Context.Text = "环境函数";
            this.tabPage_Context.UseVisualStyleBackColor = true;
            // 
            // listBox_Condition
            // 
            this.listBox_Condition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Condition.FormattingEnabled = true;
            this.listBox_Condition.ItemHeight = 12;
            this.listBox_Condition.Location = new System.Drawing.Point(0, 0);
            this.listBox_Condition.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_Condition.Name = "listBox_Condition";
            this.listBox_Condition.Size = new System.Drawing.Size(740, 436);
            this.listBox_Condition.TabIndex = 3;
            this.listBox_Condition.SelectedIndexChanged += new System.EventHandler(this.listBox_common_SelectedIndexChanged);
            this.listBox_Condition.DoubleClick += new System.EventHandler(this.listBox_Condition_DoubleClick);
            this.listBox_Condition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_Condition_KeyDown);
            // 
            // tabPage_Excution
            // 
            this.tabPage_Excution.Controls.Add(this.listBox_Execution);
            this.tabPage_Excution.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Excution.Name = "tabPage_Excution";
            this.tabPage_Excution.Size = new System.Drawing.Size(740, 446);
            this.tabPage_Excution.TabIndex = 4;
            this.tabPage_Excution.Text = "执行函数";
            this.tabPage_Excution.UseVisualStyleBackColor = true;
            // 
            // listBox_Execution
            // 
            this.listBox_Execution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Execution.FormattingEnabled = true;
            this.listBox_Execution.ItemHeight = 12;
            this.listBox_Execution.Location = new System.Drawing.Point(0, 0);
            this.listBox_Execution.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_Execution.Name = "listBox_Execution";
            this.listBox_Execution.Size = new System.Drawing.Size(740, 436);
            this.listBox_Execution.TabIndex = 4;
            this.listBox_Execution.SelectedIndexChanged += new System.EventHandler(this.listBox_common_SelectedIndexChanged);
            this.listBox_Execution.DoubleClick += new System.EventHandler(this.listBox_Execution_DoubleClick);
            this.listBox_Execution.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_Execution_KeyDown);
            // 
            // Form_VarsAndFunctions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(754, 523);
            this.Controls.Add(this.TLP_All);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_VarsAndFunctions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变量与函数容器";
            this.Activated += new System.EventHandler(this.Form_VarsAndFunctions_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_VarsAndFunctions_FormClosing);
            this.TLP_All.ResumeLayout(false);
            this.panel_Bootons.ResumeLayout(false);
            this.panel_Bootons.PerformLayout();
            this.tabControl_Lists.ResumeLayout(false);
            this.tabPage_int.ResumeLayout(false);
            this.tabPage_String.ResumeLayout(false);
            this.tabPage＿Trigger.ResumeLayout(false);
            this.tabPage_Context.ResumeLayout(false);
            this.tabPage_Excution.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLP_All;
        private System.Windows.Forms.Panel panel_Bootons;
        private System.Windows.Forms.TabControl tabControl_Lists;
        private System.Windows.Forms.TabPage tabPage_int;
        private System.Windows.Forms.TabPage tabPage_String;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.ListBox listBox_VarInt;
        private System.Windows.Forms.ListBox listBox_VarString;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.TabPage tabPage＿Trigger;
        private System.Windows.Forms.ListBox listBox_Trigger;
        private System.Windows.Forms.TabPage tabPage_Context;
        private System.Windows.Forms.ListBox listBox_Condition;
        private System.Windows.Forms.TabPage tabPage_Excution;
        private System.Windows.Forms.ListBox listBox_Execution;
        private System.Windows.Forms.Button button_Check;
        private System.Windows.Forms.Button button_moveDown;
        private System.Windows.Forms.Button button_moveUp;
        private System.Windows.Forms.Button button_order;
        private System.Windows.Forms.Label label_lineNumber;
        private System.Windows.Forms.Button button_genHead;

    }
}