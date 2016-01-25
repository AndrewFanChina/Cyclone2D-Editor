namespace Cyclone.mod.misc
{
    partial class SmallDialog_History
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
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.panel_down = new System.Windows.Forms.Panel();
            this.checkBox＿Refresh = new System.Windows.Forms.CheckBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.listBox_History = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel_all.SuspendLayout();
            this.panel_down.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_all
            // 
            this.tableLayoutPanel_all.ColumnCount = 1;
            this.tableLayoutPanel_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.Controls.Add(this.panel_down, 0, 1);
            this.tableLayoutPanel_all.Controls.Add(this.listBox_History, 0, 0);
            this.tableLayoutPanel_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_all.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel_all.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_all.Name = "tableLayoutPanel_all";
            this.tableLayoutPanel_all.RowCount = 2;
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(276, 196);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // panel_down
            // 
            this.panel_down.Controls.Add(this.checkBox＿Refresh);
            this.panel_down.Controls.Add(this.button_OK);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_down.Location = new System.Drawing.Point(3, 157);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(270, 36);
            this.panel_down.TabIndex = 1;
            // 
            // checkBox＿Refresh
            // 
            this.checkBox＿Refresh.AutoSize = true;
            this.checkBox＿Refresh.Location = new System.Drawing.Point(13, 11);
            this.checkBox＿Refresh.Name = "checkBox＿Refresh";
            this.checkBox＿Refresh.Size = new System.Drawing.Size(48, 16);
            this.checkBox＿Refresh.TabIndex = 10;
            this.checkBox＿Refresh.Text = "更新";
            this.checkBox＿Refresh.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_OK.BackColor = System.Drawing.Color.Transparent;
            this.button_OK.ForeColor = System.Drawing.Color.Black;
            this.button_OK.Location = new System.Drawing.Point(174, 5);
            this.button_OK.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(54, 27);
            this.button_OK.TabIndex = 9;
            this.button_OK.Text = "隐藏";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // listBox_History
            // 
            this.listBox_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_History.FormattingEnabled = true;
            this.listBox_History.ItemHeight = 12;
            this.listBox_History.Location = new System.Drawing.Point(3, 3);
            this.listBox_History.Name = "listBox_History";
            this.listBox_History.Size = new System.Drawing.Size(270, 148);
            this.listBox_History.TabIndex = 0;
            this.listBox_History.SelectedIndexChanged += new System.EventHandler(this.listBox_History_SelectedIndexChanged);
            // 
            // SmallDialog_History
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(284, 204);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmallDialog_History";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "历史记录面板";
            this.tableLayoutPanel_all.ResumeLayout(false);
            this.panel_down.ResumeLayout(false);
            this.panel_down.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_all;
        private System.Windows.Forms.ListBox listBox_History;
        private System.Windows.Forms.Panel panel_down;
        private System.Windows.Forms.Button button_OK;
        public System.Windows.Forms.CheckBox checkBox＿Refresh;
    }
}