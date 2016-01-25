namespace Cyclone.mod.util
{
    partial class SmallDialog_ShowList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmallDialog_ShowList));
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.listBox_content = new System.Windows.Forms.ListBox();
            this.panel_tools = new System.Windows.Forms.Panel();
            this.button_del = new System.Windows.Forms.Button();
            this.button_goto = new System.Windows.Forms.Button();
            this.tableLayoutPanel_all.SuspendLayout();
            this.panel_tools.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_all
            // 
            this.tableLayoutPanel_all.ColumnCount = 1;
            this.tableLayoutPanel_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.Controls.Add(this.listBox_content, 0, 0);
            this.tableLayoutPanel_all.Controls.Add(this.panel_tools, 0, 1);
            this.tableLayoutPanel_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_all.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel_all.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_all.Name = "tableLayoutPanel_all";
            this.tableLayoutPanel_all.RowCount = 2;
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(612, 390);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // listBox_content
            // 
            this.listBox_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_content.FormattingEnabled = true;
            this.listBox_content.ItemHeight = 12;
            this.listBox_content.Location = new System.Drawing.Point(3, 3);
            this.listBox_content.Name = "listBox_content";
            this.listBox_content.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_content.Size = new System.Drawing.Size(606, 340);
            this.listBox_content.TabIndex = 0;
            this.listBox_content.DoubleClick += new System.EventHandler(this.listBox_content_DoubleClick);
            this.listBox_content.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_content_KeyDown);
            // 
            // panel_tools
            // 
            this.panel_tools.Controls.Add(this.button_del);
            this.panel_tools.Controls.Add(this.button_goto);
            this.panel_tools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_tools.Location = new System.Drawing.Point(0, 354);
            this.panel_tools.Margin = new System.Windows.Forms.Padding(0);
            this.panel_tools.Name = "panel_tools";
            this.panel_tools.Size = new System.Drawing.Size(612, 36);
            this.panel_tools.TabIndex = 1;
            // 
            // button_del
            // 
            this.button_del.Location = new System.Drawing.Point(79, 4);
            this.button_del.Name = "button_del";
            this.button_del.Size = new System.Drawing.Size(75, 28);
            this.button_del.TabIndex = 3;
            this.button_del.Text = "É¾³ý";
            this.button_del.UseVisualStyleBackColor = true;
            this.button_del.Click += new System.EventHandler(this.button_del_Click);
            // 
            // button_goto
            // 
            this.button_goto.Location = new System.Drawing.Point(3, 4);
            this.button_goto.Name = "button_goto";
            this.button_goto.Size = new System.Drawing.Size(75, 28);
            this.button_goto.TabIndex = 2;
            this.button_goto.Text = "×ªµ½";
            this.button_goto.UseVisualStyleBackColor = true;
            this.button_goto.Click += new System.EventHandler(this.button_goto_Click);
            // 
            // SmallDialog_ShowList
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(620, 398);
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(481, 328);
            this.Name = "SmallDialog_ShowList";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "½Å±¾ËÑË÷Æ÷";
            this.tableLayoutPanel_all.ResumeLayout(false);
            this.panel_tools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_all;
        private System.Windows.Forms.ListBox listBox_content;
        private System.Windows.Forms.Panel panel_tools;
        private System.Windows.Forms.Button button_del;
        private System.Windows.Forms.Button button_goto;
    }
}