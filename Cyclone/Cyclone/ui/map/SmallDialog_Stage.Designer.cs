namespace Cyclone.mod.map
{
    partial class SmallDialog_Stage
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
            this.button_Cancle = new System.Windows.Forms.Button();
            this.tableLayoutPanel_all = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_tileSize = new System.Windows.Forms.GroupBox();
            this.textBox_mapName = new System.Windows.Forms.TextBox();
            this.label_Name = new System.Windows.Forms.Label();
            this.groupBox_ImgMap = new System.Windows.Forms.GroupBox();
            this.TLP_ImgMap = new System.Windows.Forms.TableLayoutPanel();
            this.panel_ImgMap = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button_SelectNullImgMap = new System.Windows.Forms.Button();
            this.button_SelectAllImgMap = new System.Windows.Forms.Button();
            this.button_DelImgMap = new System.Windows.Forms.Button();
            this.button_AddImgMap = new System.Windows.Forms.Button();
            this.panel_down = new System.Windows.Forms.Panel();
            this.button_OK = new System.Windows.Forms.Button();
            this.colorDialog_clipEditorBg = new System.Windows.Forms.ColorDialog();
            this.button_SelectNull = new System.Windows.Forms.Button();
            this.button_SelectAll = new System.Windows.Forms.Button();
            this.button_DelMap = new System.Windows.Forms.Button();
            this.button_AddMap = new System.Windows.Forms.Button();
            this.tableLayoutPanel_all.SuspendLayout();
            this.groupBox_tileSize.SuspendLayout();
            this.groupBox_ImgMap.SuspendLayout();
            this.TLP_ImgMap.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel_down.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Cancle
            // 
            this.button_Cancle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_Cancle.BackColor = System.Drawing.Color.Transparent;
            this.button_Cancle.ForeColor = System.Drawing.Color.Black;
            this.button_Cancle.Location = new System.Drawing.Point(256, 4);
            this.button_Cancle.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_Cancle.Name = "button_Cancle";
            this.button_Cancle.Size = new System.Drawing.Size(54, 27);
            this.button_Cancle.TabIndex = 8;
            this.button_Cancle.Text = "取消";
            this.button_Cancle.UseVisualStyleBackColor = false;
            this.button_Cancle.Click += new System.EventHandler(this.button_Cancle_Click);
            // 
            // tableLayoutPanel_all
            // 
            this.tableLayoutPanel_all.ColumnCount = 1;
            this.tableLayoutPanel_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.Controls.Add(this.groupBox_tileSize, 0, 0);
            this.tableLayoutPanel_all.Controls.Add(this.groupBox_ImgMap, 0, 1);
            this.tableLayoutPanel_all.Controls.Add(this.panel_down, 0, 2);
            this.tableLayoutPanel_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_all.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel_all.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_all.Name = "tableLayoutPanel_all";
            this.tableLayoutPanel_all.RowCount = 3;
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_all.Size = new System.Drawing.Size(465, 292);
            this.tableLayoutPanel_all.TabIndex = 16;
            // 
            // groupBox_tileSize
            // 
            this.groupBox_tileSize.Controls.Add(this.textBox_mapName);
            this.groupBox_tileSize.Controls.Add(this.label_Name);
            this.groupBox_tileSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_tileSize.Location = new System.Drawing.Point(2, 2);
            this.groupBox_tileSize.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_tileSize.Name = "groupBox_tileSize";
            this.groupBox_tileSize.Size = new System.Drawing.Size(461, 70);
            this.groupBox_tileSize.TabIndex = 0;
            this.groupBox_tileSize.TabStop = false;
            this.groupBox_tileSize.Text = "场景配置";
            // 
            // textBox_mapName
            // 
            this.textBox_mapName.Location = new System.Drawing.Point(85, 17);
            this.textBox_mapName.Name = "textBox_mapName";
            this.textBox_mapName.Size = new System.Drawing.Size(146, 21);
            this.textBox_mapName.TabIndex = 3;
            this.textBox_mapName.Text = "新场景";
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(14, 21);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(65, 12);
            this.label_Name.TabIndex = 2;
            this.label_Name.Text = "场景名称：";
            // 
            // groupBox_ImgMap
            // 
            this.groupBox_ImgMap.Controls.Add(this.TLP_ImgMap);
            this.groupBox_ImgMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_ImgMap.Location = new System.Drawing.Point(2, 76);
            this.groupBox_ImgMap.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_ImgMap.Name = "groupBox_ImgMap";
            this.groupBox_ImgMap.Size = new System.Drawing.Size(461, 174);
            this.groupBox_ImgMap.TabIndex = 11;
            this.groupBox_ImgMap.TabStop = false;
            this.groupBox_ImgMap.Text = "图片映射";
            // 
            // TLP_ImgMap
            // 
            this.TLP_ImgMap.ColumnCount = 2;
            this.TLP_ImgMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.TLP_ImgMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_ImgMap.Controls.Add(this.panel_ImgMap, 1, 0);
            this.TLP_ImgMap.Controls.Add(this.panel3, 0, 0);
            this.TLP_ImgMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_ImgMap.Enabled = false;
            this.TLP_ImgMap.Location = new System.Drawing.Point(3, 17);
            this.TLP_ImgMap.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_ImgMap.Name = "TLP_ImgMap";
            this.TLP_ImgMap.RowCount = 1;
            this.TLP_ImgMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_ImgMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.TLP_ImgMap.Size = new System.Drawing.Size(455, 154);
            this.TLP_ImgMap.TabIndex = 1;
            // 
            // panel_ImgMap
            // 
            this.panel_ImgMap.AutoScroll = true;
            this.panel_ImgMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_ImgMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ImgMap.Location = new System.Drawing.Point(69, 0);
            this.panel_ImgMap.Margin = new System.Windows.Forms.Padding(0);
            this.panel_ImgMap.Name = "panel_ImgMap";
            this.panel_ImgMap.Size = new System.Drawing.Size(386, 154);
            this.panel_ImgMap.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_SelectNullImgMap);
            this.panel3.Controls.Add(this.button_SelectAllImgMap);
            this.panel3.Controls.Add(this.button_DelImgMap);
            this.panel3.Controls.Add(this.button_AddImgMap);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(63, 148);
            this.panel3.TabIndex = 0;
            // 
            // button_SelectNullImgMap
            // 
            this.button_SelectNullImgMap.Location = new System.Drawing.Point(2, 105);
            this.button_SelectNullImgMap.Name = "button_SelectNullImgMap";
            this.button_SelectNullImgMap.Size = new System.Drawing.Size(57, 24);
            this.button_SelectNullImgMap.TabIndex = 3;
            this.button_SelectNullImgMap.Text = "反选";
            this.button_SelectNullImgMap.UseVisualStyleBackColor = true;
            this.button_SelectNullImgMap.Click += new System.EventHandler(this.button_SelectNullImgMap_Click);
            // 
            // button_SelectAllImgMap
            // 
            this.button_SelectAllImgMap.Location = new System.Drawing.Point(3, 75);
            this.button_SelectAllImgMap.Name = "button_SelectAllImgMap";
            this.button_SelectAllImgMap.Size = new System.Drawing.Size(57, 24);
            this.button_SelectAllImgMap.TabIndex = 2;
            this.button_SelectAllImgMap.Text = "全选";
            this.button_SelectAllImgMap.UseVisualStyleBackColor = true;
            this.button_SelectAllImgMap.Click += new System.EventHandler(this.button_SelectAllImgMap_Click);
            // 
            // button_DelImgMap
            // 
            this.button_DelImgMap.Location = new System.Drawing.Point(2, 45);
            this.button_DelImgMap.Name = "button_DelImgMap";
            this.button_DelImgMap.Size = new System.Drawing.Size(57, 24);
            this.button_DelImgMap.TabIndex = 1;
            this.button_DelImgMap.Text = "删除";
            this.button_DelImgMap.UseVisualStyleBackColor = true;
            this.button_DelImgMap.Click += new System.EventHandler(this.button_DelImgMap_Click);
            // 
            // button_AddImgMap
            // 
            this.button_AddImgMap.Location = new System.Drawing.Point(2, 15);
            this.button_AddImgMap.Name = "button_AddImgMap";
            this.button_AddImgMap.Size = new System.Drawing.Size(57, 24);
            this.button_AddImgMap.TabIndex = 0;
            this.button_AddImgMap.Text = "添加";
            this.button_AddImgMap.UseVisualStyleBackColor = true;
            this.button_AddImgMap.Click += new System.EventHandler(this.button_AddImgMap_Click);
            // 
            // panel_down
            // 
            this.panel_down.Controls.Add(this.button_OK);
            this.panel_down.Controls.Add(this.button_Cancle);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_down.Location = new System.Drawing.Point(3, 255);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(459, 34);
            this.panel_down.TabIndex = 0;
            // 
            // button_OK
            // 
            this.button_OK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_OK.BackColor = System.Drawing.Color.Transparent;
            this.button_OK.ForeColor = System.Drawing.Color.Black;
            this.button_OK.Location = new System.Drawing.Point(149, 4);
            this.button_OK.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(54, 27);
            this.button_OK.TabIndex = 9;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // colorDialog_clipEditorBg
            // 
            this.colorDialog_clipEditorBg.FullOpen = true;
            // 
            // button_SelectNull
            // 
            this.button_SelectNull.Location = new System.Drawing.Point(293, 3);
            this.button_SelectNull.Name = "button_SelectNull";
            this.button_SelectNull.Size = new System.Drawing.Size(57, 24);
            this.button_SelectNull.TabIndex = 3;
            this.button_SelectNull.Text = "反选";
            this.button_SelectNull.UseVisualStyleBackColor = true;
            // 
            // button_SelectAll
            // 
            this.button_SelectAll.Location = new System.Drawing.Point(226, 3);
            this.button_SelectAll.Name = "button_SelectAll";
            this.button_SelectAll.Size = new System.Drawing.Size(57, 24);
            this.button_SelectAll.TabIndex = 2;
            this.button_SelectAll.Text = "全选";
            this.button_SelectAll.UseVisualStyleBackColor = true;
            // 
            // button_DelMap
            // 
            this.button_DelMap.Location = new System.Drawing.Point(159, 3);
            this.button_DelMap.Name = "button_DelMap";
            this.button_DelMap.Size = new System.Drawing.Size(57, 24);
            this.button_DelMap.TabIndex = 1;
            this.button_DelMap.Text = "删除";
            this.button_DelMap.UseVisualStyleBackColor = true;
            // 
            // button_AddMap
            // 
            this.button_AddMap.Location = new System.Drawing.Point(92, 3);
            this.button_AddMap.Name = "button_AddMap";
            this.button_AddMap.Size = new System.Drawing.Size(57, 24);
            this.button_AddMap.TabIndex = 0;
            this.button_AddMap.Text = "添加";
            this.button_AddMap.UseVisualStyleBackColor = true;
            // 
            // SmallDialog_Stage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(473, 300);
            this.Controls.Add(this.tableLayoutPanel_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmallDialog_Stage";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建场景";
            this.tableLayoutPanel_all.ResumeLayout(false);
            this.groupBox_tileSize.ResumeLayout(false);
            this.groupBox_tileSize.PerformLayout();
            this.groupBox_ImgMap.ResumeLayout(false);
            this.TLP_ImgMap.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel_down.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Cancle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_all;
        private System.Windows.Forms.Panel panel_down;
        private System.Windows.Forms.GroupBox groupBox_tileSize;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.TextBox textBox_mapName;
        private System.Windows.Forms.ColorDialog colorDialog_clipEditorBg;
        private System.Windows.Forms.Button button_SelectNull;
        private System.Windows.Forms.Button button_SelectAll;
        private System.Windows.Forms.Button button_DelMap;
        private System.Windows.Forms.Button button_AddMap;
        private System.Windows.Forms.GroupBox groupBox_ImgMap;
        private System.Windows.Forms.TableLayoutPanel TLP_ImgMap;
        private System.Windows.Forms.FlowLayoutPanel panel_ImgMap;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button_SelectNullImgMap;
        private System.Windows.Forms.Button button_SelectAllImgMap;
        private System.Windows.Forms.Button button_DelImgMap;
        private System.Windows.Forms.Button button_AddImgMap;

    }
}