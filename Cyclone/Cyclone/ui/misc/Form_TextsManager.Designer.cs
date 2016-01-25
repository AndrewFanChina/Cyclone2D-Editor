namespace Cyclone.mod.misc
{
    partial class Form_TextsManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_TextsManager));
            this.tableLayoutPanel_text = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Bootons = new System.Windows.Forms.Panel();
            this.label_LineNumber = new System.Windows.Forms.Label();
            this.label_UsedTime = new System.Windows.Forms.Label();
            this.button_clearSpilth = new System.Windows.Forms.Button();
            this.button_Inseart = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.button_moveBottom = new System.Windows.Forms.Button();
            this.button_moveTop = new System.Windows.Forms.Button();
            this.button_moveDown = new System.Windows.Forms.Button();
            this.button_moveUp = new System.Windows.Forms.Button();
            this.button_Export = new System.Windows.Forms.Button();
            this.button_Import = new System.Windows.Forms.Button();
            this.button_check = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.listBox_Texts = new System.Windows.Forms.ListBox();
            this.contextMenuStrip_Fun = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加文字ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入文本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除字符ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.向上移动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.向下移动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移至顶端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移至低端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel_text.SuspendLayout();
            this.panel_Bootons.SuspendLayout();
            this.contextMenuStrip_Fun.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_text
            // 
            this.tableLayoutPanel_text.ColumnCount = 1;
            this.tableLayoutPanel_text.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_text.Controls.Add(this.panel_Bootons, 0, 1);
            this.tableLayoutPanel_text.Controls.Add(this.listBox_Texts, 0, 0);
            this.tableLayoutPanel_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_text.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_text.Name = "tableLayoutPanel_text";
            this.tableLayoutPanel_text.RowCount = 2;
            this.tableLayoutPanel_text.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_text.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel_text.Size = new System.Drawing.Size(748, 435);
            this.tableLayoutPanel_text.TabIndex = 1;
            // 
            // panel_Bootons
            // 
            this.panel_Bootons.Controls.Add(this.label_LineNumber);
            this.panel_Bootons.Controls.Add(this.label_UsedTime);
            this.panel_Bootons.Controls.Add(this.button_clearSpilth);
            this.panel_Bootons.Controls.Add(this.button_Inseart);
            this.panel_Bootons.Controls.Add(this.button_add);
            this.panel_Bootons.Controls.Add(this.button_moveBottom);
            this.panel_Bootons.Controls.Add(this.button_moveTop);
            this.panel_Bootons.Controls.Add(this.button_moveDown);
            this.panel_Bootons.Controls.Add(this.button_moveUp);
            this.panel_Bootons.Controls.Add(this.button_Export);
            this.panel_Bootons.Controls.Add(this.button_Import);
            this.panel_Bootons.Controls.Add(this.button_check);
            this.panel_Bootons.Controls.Add(this.button_Cancel);
            this.panel_Bootons.Controls.Add(this.button_delete);
            this.panel_Bootons.Controls.Add(this.button_OK);
            this.panel_Bootons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Bootons.Location = new System.Drawing.Point(2, 376);
            this.panel_Bootons.Margin = new System.Windows.Forms.Padding(2);
            this.panel_Bootons.Name = "panel_Bootons";
            this.panel_Bootons.Size = new System.Drawing.Size(744, 57);
            this.panel_Bootons.TabIndex = 1;
            // 
            // label_LineNumber
            // 
            this.label_LineNumber.AutoSize = true;
            this.label_LineNumber.Location = new System.Drawing.Point(446, 33);
            this.label_LineNumber.Name = "label_LineNumber";
            this.label_LineNumber.Size = new System.Drawing.Size(65, 12);
            this.label_LineNumber.TabIndex = 22;
            this.label_LineNumber.Text = "当前行号：";
            // 
            // label_UsedTime
            // 
            this.label_UsedTime.AutoSize = true;
            this.label_UsedTime.Location = new System.Drawing.Point(446, 11);
            this.label_UsedTime.Name = "label_UsedTime";
            this.label_UsedTime.Size = new System.Drawing.Size(65, 12);
            this.label_UsedTime.TabIndex = 21;
            this.label_UsedTime.Text = "使用次数：";
            // 
            // button_clearSpilth
            // 
            this.button_clearSpilth.BackColor = System.Drawing.Color.Transparent;
            this.button_clearSpilth.Location = new System.Drawing.Point(368, 1);
            this.button_clearSpilth.Name = "button_clearSpilth";
            this.button_clearSpilth.Size = new System.Drawing.Size(72, 53);
            this.button_clearSpilth.TabIndex = 20;
            this.button_clearSpilth.Text = "清除冗余";
            this.button_clearSpilth.UseVisualStyleBackColor = false;
            this.button_clearSpilth.Click += new System.EventHandler(this.button_clearSpilth_Click);
            // 
            // button_Inseart
            // 
            this.button_Inseart.BackColor = System.Drawing.Color.Transparent;
            this.button_Inseart.Location = new System.Drawing.Point(80, 1);
            this.button_Inseart.Name = "button_Inseart";
            this.button_Inseart.Size = new System.Drawing.Size(72, 27);
            this.button_Inseart.TabIndex = 19;
            this.button_Inseart.Text = "插入";
            this.button_Inseart.UseVisualStyleBackColor = false;
            this.button_Inseart.Click += new System.EventHandler(this.button_Inseart_Click);
            // 
            // button_add
            // 
            this.button_add.BackColor = System.Drawing.Color.Transparent;
            this.button_add.Location = new System.Drawing.Point(8, 1);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(72, 27);
            this.button_add.TabIndex = 18;
            this.button_add.Text = "添加";
            this.button_add.UseVisualStyleBackColor = false;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_moveBottom
            // 
            this.button_moveBottom.BackColor = System.Drawing.Color.Transparent;
            this.button_moveBottom.Location = new System.Drawing.Point(224, 27);
            this.button_moveBottom.Name = "button_moveBottom";
            this.button_moveBottom.Size = new System.Drawing.Size(72, 27);
            this.button_moveBottom.TabIndex = 17;
            this.button_moveBottom.Text = "置底";
            this.button_moveBottom.UseVisualStyleBackColor = false;
            this.button_moveBottom.Click += new System.EventHandler(this.button_moveBottom_Click);
            // 
            // button_moveTop
            // 
            this.button_moveTop.BackColor = System.Drawing.Color.Transparent;
            this.button_moveTop.Location = new System.Drawing.Point(152, 27);
            this.button_moveTop.Name = "button_moveTop";
            this.button_moveTop.Size = new System.Drawing.Size(72, 27);
            this.button_moveTop.TabIndex = 16;
            this.button_moveTop.Text = "置顶";
            this.button_moveTop.UseVisualStyleBackColor = false;
            this.button_moveTop.Click += new System.EventHandler(this.button_moveTop_Click);
            // 
            // button_moveDown
            // 
            this.button_moveDown.BackColor = System.Drawing.Color.Transparent;
            this.button_moveDown.Location = new System.Drawing.Point(80, 27);
            this.button_moveDown.Name = "button_moveDown";
            this.button_moveDown.Size = new System.Drawing.Size(72, 27);
            this.button_moveDown.TabIndex = 15;
            this.button_moveDown.Text = "向下";
            this.button_moveDown.UseVisualStyleBackColor = false;
            this.button_moveDown.Click += new System.EventHandler(this.button_moveDown_Click);
            // 
            // button_moveUp
            // 
            this.button_moveUp.BackColor = System.Drawing.Color.Transparent;
            this.button_moveUp.Location = new System.Drawing.Point(8, 27);
            this.button_moveUp.Name = "button_moveUp";
            this.button_moveUp.Size = new System.Drawing.Size(72, 27);
            this.button_moveUp.TabIndex = 14;
            this.button_moveUp.Text = "向上";
            this.button_moveUp.UseVisualStyleBackColor = false;
            this.button_moveUp.Click += new System.EventHandler(this.button_moveUp_Click);
            // 
            // button_Export
            // 
            this.button_Export.BackColor = System.Drawing.Color.Transparent;
            this.button_Export.Location = new System.Drawing.Point(296, 1);
            this.button_Export.Name = "button_Export";
            this.button_Export.Size = new System.Drawing.Size(72, 27);
            this.button_Export.TabIndex = 11;
            this.button_Export.Text = "导出";
            this.button_Export.UseVisualStyleBackColor = false;
            this.button_Export.Click += new System.EventHandler(this.button_Export_Click);
            // 
            // button_Import
            // 
            this.button_Import.BackColor = System.Drawing.Color.Transparent;
            this.button_Import.Location = new System.Drawing.Point(224, 1);
            this.button_Import.Name = "button_Import";
            this.button_Import.Size = new System.Drawing.Size(72, 27);
            this.button_Import.TabIndex = 10;
            this.button_Import.Text = "导入";
            this.button_Import.UseVisualStyleBackColor = false;
            this.button_Import.Click += new System.EventHandler(this.button_Import_Click);
            // 
            // button_check
            // 
            this.button_check.BackColor = System.Drawing.Color.Transparent;
            this.button_check.Location = new System.Drawing.Point(296, 27);
            this.button_check.Name = "button_check";
            this.button_check.Size = new System.Drawing.Size(72, 27);
            this.button_check.TabIndex = 9;
            this.button_check.Text = "使用检查";
            this.button_check.UseVisualStyleBackColor = false;
            this.button_check.Click += new System.EventHandler(this.button_Check_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_Cancel.BackColor = System.Drawing.Color.Transparent;
            this.button_Cancel.Location = new System.Drawing.Point(581, 15);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(72, 27);
            this.button_Cancel.TabIndex = 8;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = false;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_delete
            // 
            this.button_delete.BackColor = System.Drawing.Color.Transparent;
            this.button_delete.Location = new System.Drawing.Point(152, 1);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(72, 27);
            this.button_delete.TabIndex = 7;
            this.button_delete.Text = "删除";
            this.button_delete.UseVisualStyleBackColor = false;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_OK
            // 
            this.button_OK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_OK.BackColor = System.Drawing.Color.Transparent;
            this.button_OK.Location = new System.Drawing.Point(662, 15);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(72, 27);
            this.button_OK.TabIndex = 6;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // listBox_Texts
            // 
            this.listBox_Texts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Texts.FormattingEnabled = true;
            this.listBox_Texts.ItemHeight = 12;
            this.listBox_Texts.Location = new System.Drawing.Point(3, 3);
            this.listBox_Texts.Name = "listBox_Texts";
            this.listBox_Texts.Size = new System.Drawing.Size(742, 364);
            this.listBox_Texts.TabIndex = 2;
            this.listBox_Texts.Tag = "";
            this.listBox_Texts.SelectedIndexChanged += new System.EventHandler(this.listBox_Texts_SelectedIndexChanged);
            this.listBox_Texts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_Texts_MouseDown);
            this.listBox_Texts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBox_Edit_KeyPress);
            this.listBox_Texts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_Text_KeyDown);
            // 
            // contextMenuStrip_Fun
            // 
            this.contextMenuStrip_Fun.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加文字ToolStripMenuItem,
            this.插入文本ToolStripMenuItem,
            this.删除字符ToolStripMenuItem,
            this.向上移动ToolStripMenuItem,
            this.向下移动ToolStripMenuItem,
            this.移至顶端ToolStripMenuItem,
            this.移至低端ToolStripMenuItem});
            this.contextMenuStrip_Fun.Name = "contextMenuStrip1";
            this.contextMenuStrip_Fun.Size = new System.Drawing.Size(168, 158);
            // 
            // 添加文字ToolStripMenuItem
            // 
            this.添加文字ToolStripMenuItem.Name = "添加文字ToolStripMenuItem";
            this.添加文字ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.添加文字ToolStripMenuItem.Text = "添加文本";
            this.添加文字ToolStripMenuItem.Click += new System.EventHandler(this.添加文字ToolStripMenuItem_Click);
            // 
            // 插入文本ToolStripMenuItem
            // 
            this.插入文本ToolStripMenuItem.Name = "插入文本ToolStripMenuItem";
            this.插入文本ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.插入文本ToolStripMenuItem.Text = "插入文本";
            this.插入文本ToolStripMenuItem.Click += new System.EventHandler(this.插入文本ToolStripMenuItem_Click);
            // 
            // 删除字符ToolStripMenuItem
            // 
            this.删除字符ToolStripMenuItem.Name = "删除字符ToolStripMenuItem";
            this.删除字符ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.删除字符ToolStripMenuItem.Text = "删除文本";
            this.删除字符ToolStripMenuItem.Click += new System.EventHandler(this.删除字符ToolStripMenuItem_Click);
            // 
            // 向上移动ToolStripMenuItem
            // 
            this.向上移动ToolStripMenuItem.Name = "向上移动ToolStripMenuItem";
            this.向上移动ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.向上移动ToolStripMenuItem.Text = "向上移动(Ctrl+↑)";
            this.向上移动ToolStripMenuItem.Click += new System.EventHandler(this.向上移动ToolStripMenuItem_Click);
            // 
            // 向下移动ToolStripMenuItem
            // 
            this.向下移动ToolStripMenuItem.Name = "向下移动ToolStripMenuItem";
            this.向下移动ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.向下移动ToolStripMenuItem.Text = "向下移动(Ctrl+↓)";
            this.向下移动ToolStripMenuItem.Click += new System.EventHandler(this.向下移动ToolStripMenuItem_Click);
            // 
            // 移至顶端ToolStripMenuItem
            // 
            this.移至顶端ToolStripMenuItem.Name = "移至顶端ToolStripMenuItem";
            this.移至顶端ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.移至顶端ToolStripMenuItem.Text = "移至顶端";
            this.移至顶端ToolStripMenuItem.Click += new System.EventHandler(this.移至顶端ToolStripMenuItem_Click);
            // 
            // 移至低端ToolStripMenuItem
            // 
            this.移至低端ToolStripMenuItem.Name = "移至低端ToolStripMenuItem";
            this.移至低端ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.移至低端ToolStripMenuItem.Text = "移至底端";
            this.移至低端ToolStripMenuItem.Click += new System.EventHandler(this.移至低端ToolStripMenuItem_Click);
            // 
            // Form_TextsManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(754, 441);
            this.Controls.Add(this.tableLayoutPanel_text);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_TextsManager";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文字编辑器";
            this.tableLayoutPanel_text.ResumeLayout(false);
            this.panel_Bootons.ResumeLayout(false);
            this.panel_Bootons.PerformLayout();
            this.contextMenuStrip_Fun.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_text;
        private System.Windows.Forms.Panel panel_Bootons;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_check;
        private System.Windows.Forms.Button button_Export;
        private System.Windows.Forms.Button button_Import;
        private System.Windows.Forms.Button button_moveBottom;
        private System.Windows.Forms.Button button_moveTop;
        private System.Windows.Forms.Button button_moveDown;
        private System.Windows.Forms.Button button_moveUp;
        private System.Windows.Forms.ListBox listBox_Texts;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Fun;
        private System.Windows.Forms.ToolStripMenuItem 添加文字ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除字符ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 向上移动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 向下移动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移至顶端ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移至低端ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入文本ToolStripMenuItem;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_Inseart;
        private System.Windows.Forms.Button button_clearSpilth;
        private System.Windows.Forms.Label label_UsedTime;
        private System.Windows.Forms.Label label_LineNumber;

    }
}