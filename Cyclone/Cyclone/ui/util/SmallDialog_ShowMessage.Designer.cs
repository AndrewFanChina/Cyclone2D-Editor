namespace Cyclone.mod.util
{
    partial class SmallDialog_ShowMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmallDialog_ShowMessage));
            this.colorDialog_clipEditorBg = new System.Windows.Forms.ColorDialog();
            this.richTextBox_Content = new System.Windows.Forms.RichTextBox();
            this.groupBox_content = new System.Windows.Forms.GroupBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.TLP_0 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_content.SuspendLayout();
            this.TLP_0.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox_Content
            // 
            this.richTextBox_Content.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Content.Location = new System.Drawing.Point(3, 17);
            this.richTextBox_Content.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox_Content.Name = "richTextBox_Content";
            this.richTextBox_Content.ReadOnly = true;
            this.richTextBox_Content.Size = new System.Drawing.Size(221, 68);
            this.richTextBox_Content.TabIndex = 0;
            this.richTextBox_Content.Text = "";
            // 
            // groupBox_content
            // 
            this.groupBox_content.Controls.Add(this.richTextBox_Content);
            this.groupBox_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_content.Location = new System.Drawing.Point(3, 3);
            this.groupBox_content.Name = "groupBox_content";
            this.groupBox_content.Size = new System.Drawing.Size(227, 88);
            this.groupBox_content.TabIndex = 1;
            this.groupBox_content.TabStop = false;
            // 
            // button_ok
            // 
            this.button_ok.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ok.Location = new System.Drawing.Point(3, 97);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(227, 26);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "确定";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // TLP_0
            // 
            this.TLP_0.ColumnCount = 1;
            this.TLP_0.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_0.Controls.Add(this.groupBox_content, 0, 0);
            this.TLP_0.Controls.Add(this.button_ok, 0, 1);
            this.TLP_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_0.Location = new System.Drawing.Point(4, 4);
            this.TLP_0.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_0.Name = "TLP_0";
            this.TLP_0.RowCount = 2;
            this.TLP_0.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_0.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.TLP_0.Size = new System.Drawing.Size(233, 126);
            this.TLP_0.TabIndex = 3;
            // 
            // SmallDialog_ShowMessage
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(241, 134);
            this.Controls.Add(this.TLP_0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SmallDialog_ShowMessage";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标题";
            this.groupBox_content.ResumeLayout(false);
            this.TLP_0.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog_clipEditorBg;
        private System.Windows.Forms.RichTextBox richTextBox_Content;
        private System.Windows.Forms.GroupBox groupBox_content;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.TableLayoutPanel TLP_0;
    }
}