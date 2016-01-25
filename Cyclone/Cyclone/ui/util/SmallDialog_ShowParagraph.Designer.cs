namespace Cyclone.mod.util
{
    partial class SmallDialog_ShowParagraph
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
            this.colorDialog_clipEditorBg = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox_Content = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel_all.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_all
            // 
            this.tableLayoutPanel_all.ColumnCount = 1;
            this.tableLayoutPanel_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.Controls.Add(this.richTextBox_Content, 0, 0);
            this.tableLayoutPanel_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_all.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel_all.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_all.Name = "tableLayoutPanel_all";
            this.tableLayoutPanel_all.RowCount = 1;
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(612, 294);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // richTextBox_Content
            // 
            this.richTextBox_Content.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Content.Location = new System.Drawing.Point(3, 3);
            this.richTextBox_Content.Name = "richTextBox_Content";
            this.richTextBox_Content.ReadOnly = true;
            this.richTextBox_Content.Size = new System.Drawing.Size(606, 288);
            this.richTextBox_Content.TabIndex = 2;
            this.richTextBox_Content.Text = "";
            // 
            // SmallDialog_ShowString
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(620, 302);
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(481, 328);
            this.Name = "SmallDialog_ShowString";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "–≈œ¢œ‘ æ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SmallDialog_ShowString_FormClosing);
            this.tableLayoutPanel_all.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog_clipEditorBg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_all;
        private System.Windows.Forms.RichTextBox richTextBox_Content;
    }
}