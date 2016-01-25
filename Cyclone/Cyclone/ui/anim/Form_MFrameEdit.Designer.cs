using Cyclone.alg;
using OpenTK.Graphics;
namespace Cyclone.mod.anim
{
    partial class Form_MFrameEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MFrameEdit));
            this.TLP_all = new System.Windows.Forms.TableLayoutPanel();
            this.TLP_bottom = new System.Windows.Forms.TableLayoutPanel();
            this.hScrollBar_FrameEdit = new System.Windows.Forms.HScrollBar();
            this.vScrollBar_FrameEdit = new System.Windows.Forms.VScrollBar();
            this.panel_EditBG = new System.Windows.Forms.Panel();
            this.glView = new OpenTK.GLControl();
            this.panel_RightBottom = new System.Windows.Forms.Panel();
            this.TLP_all.SuspendLayout();
            this.TLP_bottom.SuspendLayout();
            this.panel_EditBG.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLP_all
            // 
            this.TLP_all.ColumnCount = 2;
            this.TLP_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_all.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_all.Controls.Add(this.TLP_bottom, 0, 1);
            this.TLP_all.Controls.Add(this.vScrollBar_FrameEdit, 1, 0);
            this.TLP_all.Controls.Add(this.panel_EditBG, 0, 0);
            this.TLP_all.Controls.Add(this.panel_RightBottom, 1, 1);
            this.TLP_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_all.Location = new System.Drawing.Point(0, 0);
            this.TLP_all.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_all.Name = "TLP_all";
            this.TLP_all.RowCount = 2;
            this.TLP_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_all.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_all.Size = new System.Drawing.Size(544, 361);
            this.TLP_all.TabIndex = 19;
            // 
            // TLP_bottom
            // 
            this.TLP_bottom.ColumnCount = 1;
            this.TLP_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_bottom.Controls.Add(this.hScrollBar_FrameEdit, 0, 0);
            this.TLP_bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_bottom.Location = new System.Drawing.Point(3, 341);
            this.TLP_bottom.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TLP_bottom.Name = "TLP_bottom";
            this.TLP_bottom.RowCount = 1;
            this.TLP_bottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_bottom.Size = new System.Drawing.Size(518, 20);
            this.TLP_bottom.TabIndex = 8;
            // 
            // hScrollBar_FrameEdit
            // 
            this.hScrollBar_FrameEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hScrollBar_FrameEdit.Location = new System.Drawing.Point(0, 0);
            this.hScrollBar_FrameEdit.Name = "hScrollBar_FrameEdit";
            this.hScrollBar_FrameEdit.Size = new System.Drawing.Size(518, 20);
            this.hScrollBar_FrameEdit.TabIndex = 1;
            this.hScrollBar_FrameEdit.ValueChanged += new System.EventHandler(this.hScrollBar_FrameEdit_ValueChanged);
            // 
            // vScrollBar_FrameEdit
            // 
            this.vScrollBar_FrameEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar_FrameEdit.Location = new System.Drawing.Point(524, 0);
            this.vScrollBar_FrameEdit.Name = "vScrollBar_FrameEdit";
            this.vScrollBar_FrameEdit.Size = new System.Drawing.Size(20, 341);
            this.vScrollBar_FrameEdit.TabIndex = 2;
            this.vScrollBar_FrameEdit.ValueChanged += new System.EventHandler(this.vScrollBar_FrameEdit_ValueChanged);
            // 
            // panel_EditBG
            // 
            this.panel_EditBG.Controls.Add(this.glView);
            this.panel_EditBG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_EditBG.Location = new System.Drawing.Point(0, 0);
            this.panel_EditBG.Margin = new System.Windows.Forms.Padding(0);
            this.panel_EditBG.Name = "panel_EditBG";
            this.panel_EditBG.Size = new System.Drawing.Size(524, 341);
            this.panel_EditBG.TabIndex = 9;
            // 
            // glView
            // 
            this.glView.BackColor = System.Drawing.Color.Black;
            this.glView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glView.Location = new System.Drawing.Point(0, 0);
            this.glView.Name = "glView";
            this.glView.Size = new System.Drawing.Size(524, 341);
            this.glView.TabIndex = 0;
            this.glView.VSync = true;
            this.glView.DoubleClick += new System.EventHandler(this.glView_DoubleClick);
            this.glView.MouseLeave += new System.EventHandler(this.glView_MouseLeave);
            this.glView.Paint += new System.Windows.Forms.PaintEventHandler(this.glView_Paint);
            this.glView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glView_MouseMove);
            this.glView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glView_MouseDown);
            this.glView.Resize += new System.EventHandler(this.glView_Resize);
            this.glView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glView_MouseUp);
            this.glView.MouseEnter += new System.EventHandler(this.glView_MouseEnter);
            this.glView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glView_KeyDown);
            // 
            // panel_RightBottom
            // 
            this.panel_RightBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_RightBottom.Location = new System.Drawing.Point(524, 341);
            this.panel_RightBottom.Margin = new System.Windows.Forms.Padding(0);
            this.panel_RightBottom.Name = "panel_RightBottom";
            this.panel_RightBottom.Size = new System.Drawing.Size(20, 20);
            this.panel_RightBottom.TabIndex = 10;
            // 
            // Form_MFrameEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(544, 361);
            this.Controls.Add(this.TLP_all);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(60, 38);
            this.Name = "Form_MFrameEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "影片动画编辑";
            this.Load += new System.EventHandler(this.Form_MFrameEdit_Load);
            this.RegionChanged += new System.EventHandler(this.Form_MFrameEdit_RegionChanged);
            this.SizeChanged += new System.EventHandler(this.Form_AnimationEditor_SizeChanged);
            this.ParentChanged += new System.EventHandler(this.Form_MFrameEdit_ParentChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form_MFrameEdit_KeyUp);
            this.TLP_all.ResumeLayout(false);
            this.TLP_bottom.ResumeLayout(false);
            this.panel_EditBG.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLP_all;
        private System.Windows.Forms.HScrollBar hScrollBar_FrameEdit;
        private System.Windows.Forms.VScrollBar vScrollBar_FrameEdit;
        private System.Windows.Forms.TableLayoutPanel TLP_bottom;
        private System.Windows.Forms.Panel panel_EditBG;
        private System.Windows.Forms.Panel panel_RightBottom;
        private OpenTK.GLControl glView;
    }
}