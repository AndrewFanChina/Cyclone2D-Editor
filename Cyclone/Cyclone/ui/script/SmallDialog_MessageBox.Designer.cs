namespace Cyclone.ui_script
{
    partial class SmallDialog_MessageBox
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
            this.button_B1 = new System.Windows.Forms.Button();
            this.button_B2 = new System.Windows.Forms.Button();
            this.button_B3 = new System.Windows.Forms.Button();
            this.text_Warning = new System.Windows.Forms.RichTextBox();
            this.button_B4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_B1
            // 
            this.button_B1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_B1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_B1.ForeColor = System.Drawing.Color.White;
            this.button_B1.Location = new System.Drawing.Point(11, 101);
            this.button_B1.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_B1.Name = "button_B1";
            this.button_B1.Size = new System.Drawing.Size(60, 30);
            this.button_B1.TabIndex = 11;
            this.button_B1.Text = "决定1";
            this.button_B1.UseVisualStyleBackColor = false;
            this.button_B1.Click += new System.EventHandler(this.button_BL_Click);
            // 
            // button_B2
            // 
            this.button_B2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_B2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_B2.ForeColor = System.Drawing.Color.White;
            this.button_B2.Location = new System.Drawing.Point(72, 101);
            this.button_B2.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_B2.Name = "button_B2";
            this.button_B2.Size = new System.Drawing.Size(60, 30);
            this.button_B2.TabIndex = 12;
            this.button_B2.Text = "决定2";
            this.button_B2.UseVisualStyleBackColor = false;
            this.button_B2.Click += new System.EventHandler(this.button_BC_Click);
            // 
            // button_B3
            // 
            this.button_B3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_B3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_B3.ForeColor = System.Drawing.Color.White;
            this.button_B3.Location = new System.Drawing.Point(133, 101);
            this.button_B3.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_B3.Name = "button_B3";
            this.button_B3.Size = new System.Drawing.Size(60, 30);
            this.button_B3.TabIndex = 13;
            this.button_B3.Text = "决定3";
            this.button_B3.UseVisualStyleBackColor = false;
            this.button_B3.Click += new System.EventHandler(this.button_BR_Click);
            // 
            // text_Warning
            // 
            this.text_Warning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.text_Warning.BackColor = System.Drawing.Color.LightGray;
            this.text_Warning.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_Warning.Location = new System.Drawing.Point(12, 17);
            this.text_Warning.Name = "text_Warning";
            this.text_Warning.ReadOnly = true;
            this.text_Warning.Size = new System.Drawing.Size(241, 74);
            this.text_Warning.TabIndex = 15;
            this.text_Warning.Text = "";
            // 
            // button_B4
            // 
            this.button_B4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_B4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_B4.ForeColor = System.Drawing.Color.White;
            this.button_B4.Location = new System.Drawing.Point(194, 101);
            this.button_B4.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_B4.Name = "button_B4";
            this.button_B4.Size = new System.Drawing.Size(60, 30);
            this.button_B4.TabIndex = 16;
            this.button_B4.Text = "决定4";
            this.button_B4.UseVisualStyleBackColor = false;
            this.button_B4.Click += new System.EventHandler(this.button4_Click);
            // 
            // SmallDialog_MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(265, 148);
            this.ControlBox = false;
            this.Controls.Add(this.button_B4);
            this.Controls.Add(this.text_Warning);
            this.Controls.Add(this.button_B3);
            this.Controls.Add(this.button_B2);
            this.Controls.Add(this.button_B1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmallDialog_MessageBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "提示：";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_B1;
        private System.Windows.Forms.Button button_B2;
        private System.Windows.Forms.Button button_B3;
        private System.Windows.Forms.RichTextBox text_Warning;
        private System.Windows.Forms.Button button_B4;


    }
}