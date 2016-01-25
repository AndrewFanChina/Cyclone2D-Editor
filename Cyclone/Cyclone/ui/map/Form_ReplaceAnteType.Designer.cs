namespace Cyclone.mod.map
{
    partial class Form_ReplaceAnteType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ReplaceAnteType));
            this.TLP_All = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.TLP_LR = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_Right = new System.Windows.Forms.GroupBox();
            this.TLP_replace = new System.Windows.Forms.TableLayoutPanel();
            this.listBox_Replace = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox_folder = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_Left = new System.Windows.Forms.GroupBox();
            this.listBox_Actors = new System.Windows.Forms.ListBox();
            this.TLP_All.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TLP_LR.SuspendLayout();
            this.groupBox_Right.SuspendLayout();
            this.TLP_replace.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox_Left.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLP_All
            // 
            this.TLP_All.ColumnCount = 1;
            this.TLP_All.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_All.Controls.Add(this.panel1, 0, 1);
            this.TLP_All.Controls.Add(this.TLP_LR, 0, 0);
            this.TLP_All.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_All.Location = new System.Drawing.Point(0, 0);
            this.TLP_All.Name = "TLP_All";
            this.TLP_All.RowCount = 2;
            this.TLP_All.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_All.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.TLP_All.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_All.Size = new System.Drawing.Size(601, 364);
            this.TLP_All.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 307);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 54);
            this.panel1.TabIndex = 5;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(311, 11);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 33);
            this.button_Cancel.TabIndex = 1;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(208, 11);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 33);
            this.button_OK.TabIndex = 0;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // TLP_LR
            // 
            this.TLP_LR.ColumnCount = 2;
            this.TLP_LR.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_LR.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_LR.Controls.Add(this.groupBox_Right, 1, 0);
            this.TLP_LR.Controls.Add(this.groupBox_Left, 0, 0);
            this.TLP_LR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_LR.Location = new System.Drawing.Point(3, 3);
            this.TLP_LR.Name = "TLP_LR";
            this.TLP_LR.RowCount = 1;
            this.TLP_LR.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_LR.Size = new System.Drawing.Size(595, 298);
            this.TLP_LR.TabIndex = 7;
            // 
            // groupBox_Right
            // 
            this.groupBox_Right.Controls.Add(this.TLP_replace);
            this.groupBox_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Right.Location = new System.Drawing.Point(300, 3);
            this.groupBox_Right.Name = "groupBox_Right";
            this.groupBox_Right.Size = new System.Drawing.Size(292, 292);
            this.groupBox_Right.TabIndex = 1;
            this.groupBox_Right.TabStop = false;
            this.groupBox_Right.Text = "替换成角色原型";
            // 
            // TLP_replace
            // 
            this.TLP_replace.ColumnCount = 1;
            this.TLP_replace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_replace.Controls.Add(this.listBox_Replace, 0, 1);
            this.TLP_replace.Controls.Add(this.panel2, 0, 0);
            this.TLP_replace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_replace.Location = new System.Drawing.Point(3, 17);
            this.TLP_replace.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_replace.Name = "TLP_replace";
            this.TLP_replace.RowCount = 2;
            this.TLP_replace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.TLP_replace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_replace.Size = new System.Drawing.Size(286, 272);
            this.TLP_replace.TabIndex = 0;
            // 
            // listBox_Replace
            // 
            this.listBox_Replace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Replace.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox_Replace.FormattingEnabled = true;
            this.listBox_Replace.ItemHeight = 12;
            this.listBox_Replace.Location = new System.Drawing.Point(3, 37);
            this.listBox_Replace.Name = "listBox_Replace";
            this.listBox_Replace.Size = new System.Drawing.Size(280, 232);
            this.listBox_Replace.TabIndex = 7;
            this.listBox_Replace.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_Replace_DrawItem);
            this.listBox_Replace.DoubleClick += new System.EventHandler(this.listBox_Replace_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comboBox_folder);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(286, 34);
            this.panel2.TabIndex = 6;
            // 
            // comboBox_folder
            // 
            this.comboBox_folder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_folder.FormattingEnabled = true;
            this.comboBox_folder.Location = new System.Drawing.Point(67, 7);
            this.comboBox_folder.Name = "comboBox_folder";
            this.comboBox_folder.Size = new System.Drawing.Size(207, 20);
            this.comboBox_folder.TabIndex = 1;
            this.comboBox_folder.SelectedIndexChanged += new System.EventHandler(this.comboBox_folder_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件夹：";
            // 
            // groupBox_Left
            // 
            this.groupBox_Left.Controls.Add(this.listBox_Actors);
            this.groupBox_Left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Left.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Left.Name = "groupBox_Left";
            this.groupBox_Left.Size = new System.Drawing.Size(291, 292);
            this.groupBox_Left.TabIndex = 0;
            this.groupBox_Left.TabStop = false;
            this.groupBox_Left.Text = "当前角色原型";
            // 
            // listBox_Actors
            // 
            this.listBox_Actors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Actors.FormattingEnabled = true;
            this.listBox_Actors.ItemHeight = 12;
            this.listBox_Actors.Location = new System.Drawing.Point(3, 17);
            this.listBox_Actors.Name = "listBox_Actors";
            this.listBox_Actors.Size = new System.Drawing.Size(285, 268);
            this.listBox_Actors.TabIndex = 0;
            this.listBox_Actors.SelectedIndexChanged += new System.EventHandler(this.listBox_Actors_SelectedIndexChanged);
            // 
            // Form_ReplaceAnteType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 364);
            this.Controls.Add(this.TLP_All);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_ReplaceAnteType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "替换角色原型";
            this.TLP_All.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.TLP_LR.ResumeLayout(false);
            this.groupBox_Right.ResumeLayout(false);
            this.TLP_replace.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox_Left.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLP_All;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.ComboBox comboBox_folder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel TLP_LR;
        private System.Windows.Forms.GroupBox groupBox_Left;
        private System.Windows.Forms.GroupBox groupBox_Right;
        private System.Windows.Forms.TableLayoutPanel TLP_replace;
        private System.Windows.Forms.ListBox listBox_Replace;
        private System.Windows.Forms.ListBox listBox_Actors;
    }
}