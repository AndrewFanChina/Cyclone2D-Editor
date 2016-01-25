namespace Cyclone.mod.prop
{
    partial class SmallDialog_PropertyValue
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_value = new System.Windows.Forms.NumericUpDown();
            this.comboBox_value = new System.Windows.Forms.ComboBox();
            this.button_clearPropID = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_value)).BeginInit();
            this.SuspendLayout();
            // 
            // button_closeImageManager
            // 
            this.button_closeImageManager.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_closeImageManager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_closeImageManager.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_closeImageManager.ForeColor = System.Drawing.Color.White;
            this.button_closeImageManager.Location = new System.Drawing.Point(185, 186);
            this.button_closeImageManager.Margin = new System.Windows.Forms.Padding(4, 4, 40, 4);
            this.button_closeImageManager.Name = "button_closeImageManager";
            this.button_closeImageManager.Size = new System.Drawing.Size(88, 38);
            this.button_closeImageManager.TabIndex = 11;
            this.button_closeImageManager.Text = "确定";
            this.button_closeImageManager.UseVisualStyleBackColor = false;
            this.button_closeImageManager.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // textBox_value
            // 
            this.textBox_value.Font = new System.Drawing.Font("宋体", 9F);
            this.textBox_value.Location = new System.Drawing.Point(93, 22);
            this.textBox_value.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(345, 25);
            this.textBox_value.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "字符型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "整数型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 122);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "索引型：";
            // 
            // numericUpDown_value
            // 
            this.numericUpDown_value.Location = new System.Drawing.Point(93, 70);
            this.numericUpDown_value.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDown_value.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDown_value.Minimum = new decimal(new int[] {
            65535,
            0,
            0,
            -2147483648});
            this.numericUpDown_value.Name = "numericUpDown_value";
            this.numericUpDown_value.Size = new System.Drawing.Size(347, 25);
            this.numericUpDown_value.TabIndex = 16;
            // 
            // comboBox_value
            // 
            this.comboBox_value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_value.FormattingEnabled = true;
            this.comboBox_value.Location = new System.Drawing.Point(93, 118);
            this.comboBox_value.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_value.MaxDropDownItems = 30;
            this.comboBox_value.Name = "comboBox_value";
            this.comboBox_value.Size = new System.Drawing.Size(309, 23);
            this.comboBox_value.TabIndex = 17;
            // 
            // button_clearPropID
            // 
            this.button_clearPropID.Location = new System.Drawing.Point(411, 116);
            this.button_clearPropID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_clearPropID.Name = "button_clearPropID";
            this.button_clearPropID.Size = new System.Drawing.Size(29, 28);
            this.button_clearPropID.TabIndex = 18;
            this.button_clearPropID.Text = "×";
            this.button_clearPropID.UseVisualStyleBackColor = true;
            this.button_clearPropID.Click += new System.EventHandler(this.button_clearPropID_Click);
            // 
            // SmallDialog_PropertyValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(459, 245);
            this.ControlBox = false;
            this.Controls.Add(this.button_clearPropID);
            this.Controls.Add(this.comboBox_value);
            this.Controls.Add(this.numericUpDown_value);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_value);
            this.Controls.Add(this.button_closeImageManager);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmallDialog_PropertyValue";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配置属性值";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_TextDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_closeImageManager;
        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_value;
        private System.Windows.Forms.ComboBox comboBox_value;
        private System.Windows.Forms.Button button_clearPropID;


    }
}