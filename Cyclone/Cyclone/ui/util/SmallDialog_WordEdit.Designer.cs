namespace Cyclone.mod.util
{
    partial class SmallDialog_WordEdit
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
            this.button_closeImageManager = new System.Windows.Forms.Button();
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.text_warning = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button_closeImageManager
            // 
            this.button_closeImageManager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button_closeImageManager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_closeImageManager.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_closeImageManager.ForeColor = System.Drawing.Color.White;
            this.button_closeImageManager.Location = new System.Drawing.Point(73, 79);
            this.button_closeImageManager.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_closeImageManager.Name = "button_closeImageManager";
            this.button_closeImageManager.Size = new System.Drawing.Size(95, 30);
            this.button_closeImageManager.TabIndex = 11;
            this.button_closeImageManager.Text = "确定";
            this.button_closeImageManager.UseVisualStyleBackColor = false;
            this.button_closeImageManager.Click += new System.EventHandler(this.button_closeImageManager_Click);
            // 
            // textBox_value
            // 
            this.textBox_value.Font = new System.Drawing.Font("宋体", 9F);
            this.textBox_value.Location = new System.Drawing.Point(32, 51);
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(176, 21);
            this.textBox_value.TabIndex = 12;
            // 
            // text_warning
            // 
            this.text_warning.BackColor = System.Drawing.SystemColors.Control;
            this.text_warning.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_warning.Location = new System.Drawing.Point(12, 9);
            this.text_warning.Name = "text_warning";
            this.text_warning.ReadOnly = true;
            this.text_warning.Size = new System.Drawing.Size(216, 35);
            this.text_warning.TabIndex = 14;
            this.text_warning.Text = "";
            // 
            // SmallDialog_WordEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(240, 130);
            this.Controls.Add(this.text_warning);
            this.Controls.Add(this.textBox_value);
            this.Controls.Add(this.button_closeImageManager);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmallDialog_WordEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "重命名";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SmallDialog_WordEdit_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_TextDialog_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_closeImageManager;
        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.RichTextBox text_warning;


    }
}