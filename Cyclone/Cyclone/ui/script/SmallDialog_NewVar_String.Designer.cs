namespace Cyclone.ui_script
{
    partial class SmallDialog_NewVar_String
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
            this.panel_down = new System.Windows.Forms.Panel();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancle = new System.Windows.Forms.Button();
            this.panel_config = new System.Windows.Forms.Panel();
            this.textBox_Value = new System.Windows.Forms.TextBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_Value = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.panel_down.SuspendLayout();
            this.panel_config.SuspendLayout();
            this.tableLayoutPanel_all.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_down
            // 
            this.panel_down.Controls.Add(this.button_OK);
            this.panel_down.Controls.Add(this.button_Cancle);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_down.Location = new System.Drawing.Point(3, 108);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(317, 34);
            this.panel_down.TabIndex = 0;
            // 
            // button_OK
            // 
            this.button_OK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_OK.BackColor = System.Drawing.Color.Transparent;
            this.button_OK.ForeColor = System.Drawing.Color.Black;
            this.button_OK.Location = new System.Drawing.Point(78, 4);
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
            this.button_Cancle.Location = new System.Drawing.Point(185, 4);
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
            this.panel_config.Controls.Add(this.textBox_Value);
            this.panel_config.Controls.Add(this.textBox_name);
            this.panel_config.Controls.Add(this.label_Value);
            this.panel_config.Controls.Add(this.label_name);
            this.panel_config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_config.Location = new System.Drawing.Point(3, 3);
            this.panel_config.Name = "panel_config";
            this.panel_config.Size = new System.Drawing.Size(317, 99);
            this.panel_config.TabIndex = 2;
            // 
            // textBox_Value
            // 
            this.textBox_Value.Location = new System.Drawing.Point(77, 57);
            this.textBox_Value.Name = "textBox_Value";
            this.textBox_Value.Size = new System.Drawing.Size(215, 21);
            this.textBox_Value.TabIndex = 8;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(77, 21);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(215, 21);
            this.textBox_name.TabIndex = 7;
            // 
            // label_Value
            // 
            this.label_Value.AutoSize = true;
            this.label_Value.Location = new System.Drawing.Point(24, 61);
            this.label_Value.Name = "label_Value";
            this.label_Value.Size = new System.Drawing.Size(41, 12);
            this.label_Value.TabIndex = 6;
            this.label_Value.Text = "数值：";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(24, 25);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(41, 12);
            this.label_name.TabIndex = 1;
            this.label_name.Text = "名称：";
            // 
            // tableLayoutPanel_all
            // 
            this.tableLayoutPanel_all.ColumnCount = 1;
            this.tableLayoutPanel_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.Controls.Add(this.panel_down, 0, 1);
            this.tableLayoutPanel_all.Controls.Add(this.panel_config, 0, 0);
            this.tableLayoutPanel_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_all.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel_all.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_all.Name = "tableLayoutPanel_all";
            this.tableLayoutPanel_all.RowCount = 2;
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(323, 145);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // SmallDialog_NewVar_String
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(331, 153);
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmallDialog_NewVar_String";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建字符变量单元";
            this.panel_down.ResumeLayout(false);
            this.panel_config.ResumeLayout(false);
            this.panel_config.PerformLayout();
            this.tableLayoutPanel_all.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox textBox_Value;
    }
}