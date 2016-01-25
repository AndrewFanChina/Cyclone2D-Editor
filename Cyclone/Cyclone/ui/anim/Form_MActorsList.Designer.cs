namespace Cyclone.mod.anim
{
    partial class Form_MActorsList
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MActorsList));
            this.treeView_Animation = new System.Windows.Forms.TreeView();
            this.imageList_Animaion = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip_ItemFocused = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加单元ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加子单元ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.克隆单元ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_NoItemFocused = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加单元ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_ItemFocused.SuspendLayout();
            this.contextMenuStrip_NoItemFocused.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView_Animation
            // 
            this.treeView_Animation.AllowDrop = true;
            this.treeView_Animation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.treeView_Animation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView_Animation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Animation.ForeColor = System.Drawing.SystemColors.WindowText;
            this.treeView_Animation.HideSelection = false;
            this.treeView_Animation.ImageIndex = 0;
            this.treeView_Animation.ImageList = this.imageList_Animaion;
            this.treeView_Animation.Location = new System.Drawing.Point(0, 0);
            this.treeView_Animation.Name = "treeView_Animation";
            this.treeView_Animation.SelectedImageIndex = 0;
            this.treeView_Animation.Size = new System.Drawing.Size(198, 328);
            this.treeView_Animation.TabIndex = 3;
            this.treeView_Animation.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_Animation_DragDrop);
            this.treeView_Animation.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Animation_AfterSelect);
            this.treeView_Animation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_Animation_MouseDown);
            this.treeView_Animation.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_Animation_DragEnter);
            this.treeView_Animation.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Animation_NodeMouseClick);
            this.treeView_Animation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_Animation_KeyDown);
            this.treeView_Animation.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_Animation_ItemDrag);
            // 
            // imageList_Animaion
            // 
            this.imageList_Animaion.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Animaion.ImageStream")));
            this.imageList_Animaion.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Animaion.Images.SetKeyName(0, "movie_folder_16x16.png");
            this.imageList_Animaion.Images.SetKeyName(1, "movie_film_16x16.png");
            this.imageList_Animaion.Images.SetKeyName(2, "movie_movieClip_16x16.png");
            // 
            // contextMenuStrip_ItemFocused
            // 
            this.contextMenuStrip_ItemFocused.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加单元ToolStripMenuItem,
            this.添加子单元ToolStripMenuItem,
            this.克隆单元ToolStripMenuItem,
            this.上移ToolStripMenuItem,
            this.下移ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.重命名ToolStripMenuItem});
            this.contextMenuStrip_ItemFocused.Name = "contextMenuStrip_actor";
            this.contextMenuStrip_ItemFocused.ShowCheckMargin = true;
            this.contextMenuStrip_ItemFocused.ShowImageMargin = false;
            this.contextMenuStrip_ItemFocused.Size = new System.Drawing.Size(153, 180);
            // 
            // 添加单元ToolStripMenuItem
            // 
            this.添加单元ToolStripMenuItem.Name = "添加单元ToolStripMenuItem";
            this.添加单元ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加单元ToolStripMenuItem.Text = "添加平行单元";
            this.添加单元ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 添加子单元ToolStripMenuItem
            // 
            this.添加子单元ToolStripMenuItem.Name = "添加子单元ToolStripMenuItem";
            this.添加子单元ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加子单元ToolStripMenuItem.Text = "添加下属单元";
            this.添加子单元ToolStripMenuItem.Click += new System.EventHandler(this.添加子单元ToolStripMenuItem_Click);
            // 
            // 克隆单元ToolStripMenuItem
            // 
            this.克隆单元ToolStripMenuItem.Name = "克隆单元ToolStripMenuItem";
            this.克隆单元ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.克隆单元ToolStripMenuItem.Text = "克隆当前单元";
            this.克隆单元ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 上移ToolStripMenuItem
            // 
            this.上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
            this.上移ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.上移ToolStripMenuItem.Text = "上移(Ctrl+↑)";
            this.上移ToolStripMenuItem.Click += new System.EventHandler(this.上移ToolStripMenuItem_Click);
            // 
            // 下移ToolStripMenuItem
            // 
            this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
            this.下移ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.下移ToolStripMenuItem.Text = "下移(Ctrl+↓)";
            this.下移ToolStripMenuItem.Click += new System.EventHandler(this.下移ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除(Delete)";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 重命名ToolStripMenuItem
            // 
            this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
            this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.重命名ToolStripMenuItem.Text = "重命名单元";
            this.重命名ToolStripMenuItem.Click += new System.EventHandler(this.重命名ToolStripMenuItem_Click);
            // 
            // contextMenuStrip_NoItemFocused
            // 
            this.contextMenuStrip_NoItemFocused.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加单元ToolStripMenuItem2});
            this.contextMenuStrip_NoItemFocused.Name = "contextMenuStrip_actor";
            this.contextMenuStrip_NoItemFocused.ShowCheckMargin = true;
            this.contextMenuStrip_NoItemFocused.ShowImageMargin = false;
            this.contextMenuStrip_NoItemFocused.Size = new System.Drawing.Size(137, 26);
            // 
            // 添加单元ToolStripMenuItem2
            // 
            this.添加单元ToolStripMenuItem2.Name = "添加单元ToolStripMenuItem2";
            this.添加单元ToolStripMenuItem2.Size = new System.Drawing.Size(136, 22);
            this.添加单元ToolStripMenuItem2.Text = "添加新单元";
            this.添加单元ToolStripMenuItem2.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // Form_MActorsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 328);
            this.Controls.Add(this.treeView_Animation);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_MActorsList";
            this.Text = "影片剪辑";
            this.contextMenuStrip_ItemFocused.ResumeLayout(false);
            this.contextMenuStrip_NoItemFocused.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView treeView_Animation;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip_ItemFocused;
        private System.Windows.Forms.ToolStripMenuItem 添加单元ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 添加子单元ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 克隆单元ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList_Animaion;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip_NoItemFocused;
        private System.Windows.Forms.ToolStripMenuItem 添加单元ToolStripMenuItem2;
    }
}