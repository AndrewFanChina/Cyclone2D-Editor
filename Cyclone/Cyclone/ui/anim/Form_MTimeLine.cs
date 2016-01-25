using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DockingUI.WinFormsUI.Docking;
using Cyclone.alg;
using Cyclone.mod;
using Cyclone.mod.anim;
using Cyclone.mod.util;
using Cyclone.alg.math;
using Cyclone.alg.util;

namespace Cyclone.mod.anim
{
    public partial class Form_MTimeLine : DockContent
    {
        public MTimeLineHoder currentTimeLineHoder = null;
        public MTimeLine currentTimeLine= null;
        public MFrame focusFrame = null;       //当前帧(是存在的帧)
        public static int timePosition = 0;    //时间轴位置
        public static int focusX1 = -1, focusY1 = -1, focusX2 = -1, focusY2 = -1;//多选时焦点行列
        public static int focusState = -1;//-1处于未选状态,0处于单选状态,1处于多选状态
        public MFrameType focusFrameType = MFrameType.TYPE_NUL;   //焦点帧的帧类型
        public static Cursor handCursor;       //拖动光标
        public bool inDraging = false;      //是否正在拖动
        public int dragDestX, dragDestY, dragSrcX, dragSrcY;       //拖动目标
        public static bool OnionSkin_Enable = false;//是否使用洋葱皮
        public const int OnionSkinMode_FB = 0;//洋葱皮模式_0[左右各透视1帧]
        public const int OnionSkinMode_Back = 1;//洋葱皮模式_1[左透视2帧]
        public const int OnionSkinMode_Front = 2;//洋葱皮模式_2[右透视2帧]
        public static int OnionSkin_Mode = OnionSkinMode_FB;//洋葱皮模式
        public static int OnionSkin_Frame = 1;//洋葱皮透视帧数
        public MFrame bakCopyedKeyFrame = null;   //拷贝的关键帧

        Form_MAnimation form_MA;
        public Form_MTimeLine(Form_MAnimation form_MAT)
        {
            form_MA = form_MAT;
            handCursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursors_move.ico"));
            InitializeComponent();
        }
        protected override string GetPersistString()
        {
            return "Form_MTimeLine";
        }
        public void setHolder(MTimeLineHoder mTimeLineHoderT)
        {
            currentTimeLineHoder = mTimeLineHoderT;
            currentTimeLine = null;
            focusFrame = null;
            if (currentTimeLineHoder != null && timePosition >= currentTimeLineHoder.getMaxFrameLen())
            {
                timePosition = currentTimeLineHoder.getMaxFrameLen() - 1;
            }
            if (timePosition<0)
            {
                timePosition = 0;
            }
            clearFrameFocus();
            form_MA.form_MFrameEdit.releaseFocusClips();
        }
        private void Form_MTimeLine_Shown(object sender, EventArgs e)
        {
            SPContainer_ALL.SplitterDistance = 180;
        }

        private void panel_levels_Paint(object sender, PaintEventArgs e)
        {
            updateTLNaviRegion();
        }
        //设置当前时间轴
        public void setCurrentTimeLine(int id)
        {
            if (currentTimeLineHoder == null)
            {
                return;
            }
            currentTimeLine = currentTimeLineHoder[id];
        }
        //刷新时间轴导航区显示++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Image TLN_PanelBuffer = null;
        int TLN_PanelWidth, TLN_PanelHeight;
        public static int TimeLine_rowH = 20;//时间轴行高
        public int TLN_hideH = 0;         //被隐藏的高度
        public int TimeLine_rowBegin, TimeLine_rowEnd;//显示的起始和结束行
        public void updateTLNaviRegion()
        {
            //窗口缓冲尺寸
            TLN_PanelWidth = panel_levels.Width;
            TLN_PanelHeight = panel_levels.Height;
            //调整缓冲------------------------------------------------------

            //刷新窗口缓存大小
            if (TLN_PanelBuffer == null || TLN_PanelBuffer.Width != TLN_PanelWidth || TLN_PanelBuffer.Height != TLN_PanelHeight)
            {
                TLN_PanelBuffer = new Bitmap(TLN_PanelWidth, TLN_PanelHeight);
            }
            //绘制----------------------------------------------------------
            Graphics gBuffer = Graphics.FromImage(TLN_PanelBuffer);
            //窗口背景
            GraphicsUtil.fillRect(gBuffer, 0, 0, TLN_PanelWidth, TLN_PanelHeight, 0xc0c0c0);
            if (currentTimeLineHoder != null)
            {
                int id = -1;
                if (currentTimeLine != null)
                {
                    id = currentTimeLine.GetID();
                }
                //计算被隐藏的高度
                float scrollPercent = (vScrollBar_level.Value - vScrollBar_level.Minimum) * 1.0f / (vScrollBar_level.Maximum - vScrollBar_level.Minimum);
                TLN_hideH = (int)((TimeLine_rowH * currentTimeLineHoder.Count() - (TLN_PanelHeight - TimeLine_rowH)) * scrollPercent);//预留一个空行高
                TLN_hideH -= TLN_hideH % TimeLine_rowH;            //约束到行高
                if (TLN_hideH < 0)
                {
                    TLN_hideH = 0;
                }
                //计算可以显示的行数
                TimeLine_rowBegin = TLN_hideH / TimeLine_rowH;
                TimeLine_rowEnd = TimeLine_rowBegin + (TLN_PanelHeight + TimeLine_rowH - 1) / TimeLine_rowH;
                if (TimeLine_rowEnd >= currentTimeLineHoder.Count())
                {
                    TimeLine_rowEnd = currentTimeLineHoder.Count() - 1;
                }
                currentTimeLineHoder.displayNaviRegion(gBuffer, TLN_PanelWidth, TLN_PanelHeight, TimeLine_rowBegin, TimeLine_rowEnd,TimeLine_rowH, id);
            }
            //绘制到屏幕
            Graphics g = panel_levels.CreateGraphics();
            if (TLN_PanelBuffer != null)
            {
                GraphicsUtil.drawImage(g, TLN_PanelBuffer, 0, 0, 0, 0, TLN_PanelBuffer.Width, TLN_PanelBuffer.Height);
            }
            //清除临时资源
            gBuffer.Dispose();
            gBuffer = null;
            g.Dispose();
            g = null;
        }
        //刷新时间轴帧区域显示++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Image TLF_PanelBuffer = null;
        Image TLF_PanelScreenBuffer = null;
        int TLF_PanelWidth, TLF_PanelHeight;
        public static int TimeLine_columnWidth = 8;  //时间轴背景帧宽度
        public int TLF_BgFrameCount_m = 100;//时间轴背景帧预留个数
        public int TLF_hideW = 0;                //被隐藏的宽度
        public int TimeLine_columnBegin, TimeLine_columnEnd;   //显示的起始和结束帧
        public void updateTLFrameRegion()
        {
            updateTLFrameRegion(true);
        }
        public void updateTLFrameRegion(bool updateBuffer)
        {
            Graphics g = panel_timeLine.CreateGraphics();
            //窗口缓冲尺寸
            TLF_PanelWidth = panel_timeLine.Width;
            TLF_PanelHeight = panel_timeLine.Height;
            if (updateBuffer)
            {
                //调整缓冲------------------------------------------------------

                //刷新窗口缓存大小
                if (TLF_PanelBuffer == null || TLF_PanelBuffer.Width != TLF_PanelWidth || TLF_PanelBuffer.Height != TLF_PanelHeight)
                {
                    TLF_PanelBuffer = new Bitmap(TLF_PanelWidth, TLF_PanelHeight);
                }
                //绘制----------------------------------------------------------
                Graphics gBuffer = Graphics.FromImage(TLF_PanelBuffer);
                //窗口背景
                GraphicsUtil.fillRect(gBuffer, 0, 0, TLF_PanelWidth, TLF_PanelHeight, 0xc0c0c0);
                if (currentTimeLineHoder != null)
                {
                    int id = -1;
                    //重新计算显示帧数
                    int frameCount = (TLF_PanelWidth + TimeLine_columnWidth - 1) / TimeLine_columnWidth;
                    int maxFrameLen = currentTimeLineHoder.getMaxFrameLen();
                    if (maxFrameLen + TLF_BgFrameCount_m > frameCount)
                    {
                        frameCount = maxFrameLen + TLF_BgFrameCount_m;
                    }
                    //计算被隐藏的宽度
                    float scrollPercent = (hScrollBar_frames.Value - hScrollBar_frames.Minimum) * 1.0f / (hScrollBar_frames.Maximum - hScrollBar_frames.Minimum - 9);
                    TLF_hideW = (int)((TimeLine_columnWidth * frameCount - TLF_PanelWidth) * scrollPercent);
                    TLF_hideW -= TLF_hideW % TimeLine_columnWidth;                                          //约束到帧宽
                    if (TLF_hideW < 0)
                    {
                        TLF_hideW = 0;
                    }
                    //计算可以显示的帧数
                    TimeLine_columnBegin = TLF_hideW / TimeLine_columnWidth;
                    TimeLine_columnEnd = TimeLine_columnBegin + (TLF_PanelWidth + TimeLine_columnWidth - 1) / TimeLine_columnWidth;
                    if (TimeLine_columnEnd >= frameCount)
                    {
                        TimeLine_columnEnd = frameCount - 1;
                    }
                    currentTimeLineHoder.displayFrameRegion(gBuffer, TLF_PanelWidth, TimeLine_rowBegin, TimeLine_rowEnd, TimeLine_rowH, TimeLine_columnBegin, TimeLine_columnEnd, TimeLine_columnWidth, id);
                    //if (focusRow >= 0 && focusColumn >= 0)
                    //{
                    //    GraphicsUtil.fillRect(gBuffer, (focusColumn-TimeLine_columnBegin) * TimeLine_columnWidth, (focusRow-TimeLine_rowBegin) * TimeLine_rowH, TimeLine_columnWidth, TimeLine_rowH, 0x3399ff, 0x55);
                    //}
                }
                //清除临时资源
                gBuffer.Dispose();
                gBuffer = null;
            }
            //刷新窗口缓存大小
            if (TLF_PanelScreenBuffer == null || TLF_PanelScreenBuffer.Width != TLF_PanelWidth || TLF_PanelScreenBuffer.Height != TLF_PanelHeight)
            {
                TLF_PanelScreenBuffer = new Bitmap(TLF_PanelWidth, TLF_PanelHeight);
            }
            Graphics gSceenBuffer = Graphics.FromImage(TLF_PanelScreenBuffer);
            //绘制缓冲内容
            if (TLF_PanelBuffer != null)
            {
                GraphicsUtil.drawImage(gSceenBuffer, TLF_PanelBuffer, 0, 0, 0, 0, TLF_PanelBuffer.Width, TLF_PanelBuffer.Height);
            }
            //绘制选择区域
            if (focusState >= 0)
            {
                GraphicsUtil.fillRect(gSceenBuffer, (Math.Min(focusX1, focusX2) - TimeLine_columnBegin) * TimeLine_columnWidth, (Math.Min(focusY1, focusY2) - TimeLine_rowBegin) * TimeLine_rowH, TimeLine_columnWidth * (Math.Abs(focusX1 - focusX2) + 1), TimeLine_rowH * (Math.Abs(focusY1 - focusY2) + 1), 0x3399FF, 0x88);
            }
            //绘制拖动区域
            if (inDraging)
            {
                GraphicsUtil.fillRect(gSceenBuffer, (dragDestX - TimeLine_columnBegin) * TimeLine_columnWidth, (dragDestY - TimeLine_rowBegin) * TimeLine_rowH, TimeLine_columnWidth, TimeLine_rowH, 0xFF9933, 0x88);
            }
            //绘制时间轴位置
            GraphicsUtil.fillRect(gSceenBuffer, (timePosition - TimeLine_columnBegin) * TimeLine_columnWidth + TimeLine_columnWidth / 2 - 1, 0, 1, TLF_PanelHeight, 0xCC0000);
            
            //绘制到屏幕
            if (TLF_PanelBuffer != null)
            {
                GraphicsUtil.drawImage(g, TLF_PanelScreenBuffer, 0, 0, 0, 0, TLF_PanelBuffer.Width, TLF_PanelBuffer.Height);
            }
            g.Dispose();
            g = null;
        }
        //刷新时间轴帧标尺显示++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Image TLR_PanelBuffer = null;
        int TLR_PanelWidth, TLR_PanelHeight;
        //显示时间轴导航部分
        private static Font font = new Font("宋体", 9);
        public void updateTLRulerRegion()
        {
            updateTLRulerRegion(true);
        }
        public void updateTLRulerRegion(bool updateBuffer)
        {
            Graphics g = panel_timeLineRuler.CreateGraphics();
            if (updateBuffer)
            {
                //窗口缓冲尺寸
                TLR_PanelWidth = panel_timeLineRuler.Width;
                TLR_PanelHeight = panel_timeLineRuler.Height;
                //调整缓冲------------------------------------------------------

                //刷新窗口缓存大小
                if (TLR_PanelBuffer == null || TLR_PanelBuffer.Width != TLR_PanelWidth || TLR_PanelBuffer.Height != TLR_PanelHeight)
                {
                    TLR_PanelBuffer = new Bitmap(TLR_PanelWidth, TLR_PanelHeight);
                }
                Graphics gBuffer = Graphics.FromImage(TLR_PanelBuffer);
                //绘制----------------------------------------------------------
                Image imgTopBg = global::Cyclone.Properties.Resources.timeLine_topBg;
                Image imgRuler = global::Cyclone.Properties.Resources.timeLineFrame;
                int xT = 0;
                //先绘制背景
                while (xT < TLR_PanelWidth)
                {
                    GraphicsUtil.drawClip(gBuffer, imgTopBg, xT, 0, 0, 0, imgTopBg.Width, imgTopBg.Height, 0);
                    xT += imgTopBg.Width;
                }
                //绘制标尺刻度
                int bgFC = imgRuler.Width / TimeLine_columnWidth;
                xT = 0;
                while (xT < TLR_PanelWidth)
                {
                    GraphicsUtil.drawClip(gBuffer, imgRuler, xT, TLR_PanelHeight - 5, 0, 62, imgRuler.Width, 3, 0);
                    xT += imgRuler.Width;
                }
                xT = 0;
                while (xT < TLR_PanelWidth)
                {
                    GraphicsUtil.drawClip(gBuffer, imgRuler, xT, 0, 0, 61, imgRuler.Width, 4, 0);
                    xT += imgRuler.Width;
                }
                //绘制数值
                if (currentTimeLineHoder != null)
                {
                    xT = -(TimeLine_columnBegin % 5 + 1) * TimeLine_columnWidth;
                    int number = TimeLine_columnBegin - (TimeLine_columnBegin % 5);
                    while (xT < TLR_PanelWidth)
                    {
                        GraphicsUtil.drawString(gBuffer, xT, TLR_PanelHeight - 5, "" + (number), font, 0x0, Consts.LEFT | Consts.BOTTOM);
                        xT += 5 * TimeLine_columnWidth;
                        number += 5;
                    }
                }
                //清除临时资源
                gBuffer.Dispose();
                gBuffer = null;
            }
            //绘制到屏幕
            if (TLR_PanelBuffer != null)
            {
                GraphicsUtil.drawImage(g, TLR_PanelBuffer, 0, 0, 0, 0, TLR_PanelBuffer.Width, TLR_PanelBuffer.Height);
            }
            //绘制时间轴位置
            Image imgRulerPos = global::Cyclone.Properties.Resources.rulerPos;
            if (imgRulerPos != null)
            {
                GraphicsUtil.drawImage(g, imgRulerPos, (timePosition - TimeLine_columnBegin) * TimeLine_columnWidth-1, 0, 0, 0, imgRulerPos.Width, imgRulerPos.Height);
            }
            g.Dispose();
            g = null;
        }
        private void panel_levels_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                添加单元ToolStripMenuItem.Enabled = currentTimeLineHoder!=null;
                克隆单元ToolStripMenuItem.Enabled = currentTimeLine != null;
                上移ToolStripMenuItem.Enabled = currentTimeLine != null;
                下移ToolStripMenuItem.Enabled = currentTimeLine != null;
                删除ToolStripMenuItem.Enabled = currentTimeLine != null;
                重命名ToolStripMenuItem.Enabled = currentTimeLine != null;
                contextMenuStrip_navi.Show((Control)(sender), e.Location);
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (currentTimeLineHoder != null)
                {
                    int selectRow = e.Y / TimeLine_rowH + TimeLine_rowBegin;
                    if (selectRow <= TimeLine_rowEnd)
                    {
                        if (e.X > panel_levels.Width - 48)
                        {
                            if (currentTimeLineHoder[selectRow] != null)
                            {
                                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                                if (e.X > panel_levels.Width - 24)
                                {
                                    currentTimeLineHoder[selectRow].isLocked = !currentTimeLineHoder[selectRow].isLocked;
                                }
                                else
                                {
                                    currentTimeLineHoder[selectRow].isVisible = !currentTimeLineHoder[selectRow].isVisible;
                                }
                                form_MA.historyManager.AddHistory(HistoryType.Action);
                                updateTLNaviRegion();
                                form_MA.form_MFrameEdit.releaseFocusClips();
                                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
                            }
                        }
                        else
                        {
                            setCurrentTimeLine(selectRow);
                            updateTLNaviRegion();
                            if (clearFrameFocus())
                            {
                                updateTLFrameRegion();
                            }
                        }
                    }
                }
            }
        }


        private void vScrollBar_level_Scroll(object sender, ScrollEventArgs e)
        {
            this.updateTLNaviRegion();
            this.updateTLFrameRegion();
        }

        private void panel_levels_MouseEnter(object sender, EventArgs e)
        {
            if (!form_MA.Equals(Form.ActiveForm))
            {
                return;
            }
            ((Control)sender).Focus();
        }

        private void panel_levels_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (currentTimeLineHoder != null && currentTimeLine!=null)
            {
                int id = currentTimeLine.GetID();
                if (e.Control)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        上移ToolStripMenuItem_Click(null, null);
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        下移ToolStripMenuItem_Click(null, null);
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        删除ToolStripMenuItem_Click(null, null);
                    }
                    else
                    {
                        if (e.KeyCode == Keys.Up)
                        {
                            id--;
                            if (id < 0)
                            {
                                id = currentTimeLineHoder.Count() - 1;
                            }
                        }
                        else if (e.KeyCode == Keys.Down)
                        {
                            id++;
                            if (id >= currentTimeLineHoder.Count())
                            {
                                id = 0;
                            }
                        }
                        this.setCurrentTimeLine(id);
                        scrollRightPosV(id);
                    }
                }

            }
        }
        //纵向滚动到合适的位置
        private void scrollRightPosV(int id)
        {
            clearFrameFocus();
            if (id < TimeLine_rowBegin || id >= TimeLine_rowBegin + TLN_PanelHeight / TimeLine_rowH)
            {
                int hideHTemp;
                if (id < TimeLine_rowBegin)
                {
                    hideHTemp = id * TimeLine_rowH;
                }
                else
                {
                    hideHTemp = (id - TLN_PanelHeight / TimeLine_rowH + 2) * TimeLine_rowH;
                }
                //计算被隐藏的高度
                float scrollPercent = hideHTemp * 1.0f / (TimeLine_rowH * currentTimeLineHoder.Count() - (TLN_PanelHeight - TimeLine_rowH));//预留一个空行高
                if (scrollPercent < 0)
                {
                    scrollPercent = 0;
                }
                if (scrollPercent > 1)
                {
                    scrollPercent = 1;
                }
                int newValue = (int)((vScrollBar_level.Maximum - vScrollBar_level.Minimum) * scrollPercent + vScrollBar_level.Minimum);
                if (newValue != vScrollBar_level.Value)
                {
                    vScrollBar_level.Value = newValue;
                }
                else
                {
                    updateTLNaviRegion();
                    updateTLFrameRegion();
                }
            }
            else
            {
                updateTLNaviRegion();
                updateTLFrameRegion();
            }
        }

        private void 添加单元ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTimeLineHoder != null)
            {
                String name = "图层" + currentTimeLineHoder.Count();
                SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("新建图层", name);
                txtDialog.ShowDialog();
                name = txtDialog.getValue();
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                MTimeLine element = new MTimeLine(currentTimeLineHoder);
                element.name = name;
                currentTimeLineHoder.Add(element);
                form_MA.historyManager.AddHistory(HistoryType.Action);
                this.setCurrentTimeLine(element.GetID());
                scrollRightPosV(element.GetID());
            }
        }
        private void 克隆单元ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTimeLineHoder != null && currentTimeLine!=null)
            {
                String name = "图层" + currentTimeLineHoder.Count();
                SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("克隆图层", name);
                txtDialog.ShowDialog();
                name = txtDialog.getValue();
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                MTimeLine element = currentTimeLine.Clone();
                element.name = name;
                currentTimeLineHoder.Add(element);
                form_MA.historyManager.AddHistory(HistoryType.Action);
                this.setCurrentTimeLine(element.GetID());
                scrollRightPosV(element.GetID());
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }

        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTimeLineHoder != null && currentTimeLine != null)
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                currentTimeLineHoder.MoveUpElement(currentTimeLine.GetID());
                form_MA.historyManager.AddHistory(HistoryType.Action);
                scrollRightPosV(currentTimeLine.GetID());
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }
        private void vScrollBar_level_ValueChanged(object sender, EventArgs e)
        {
            this.updateTLNaviRegion();
            this.updateTLFrameRegion();
        }

        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTimeLineHoder != null && currentTimeLine != null)
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                currentTimeLineHoder.MoveDownElement(currentTimeLine.GetID());
                form_MA.historyManager.AddHistory(HistoryType.Action);
                scrollRightPosV(currentTimeLine.GetID());
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTimeLineHoder != null && currentTimeLine != null)
            {
                if (!MessageBox.Show("确定删除图层“" + currentTimeLine .name+ "”？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.Yes))
                {
                    return;
                }
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                int index = currentTimeLine.GetID();
                currentTimeLineHoder.RemoveAt(index);
                if (index >= currentTimeLineHoder.Count())
                {
                    index--;
                }
                form_MA.historyManager.AddHistory(HistoryType.Action);
                setCurrentTimeLine(index);
                scrollRightPosV(index);
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTimeLineHoder != null && currentTimeLine != null)
            {
                String name = currentTimeLine.name;
                SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("重命名图层", name);
                txtDialog.ShowDialog();
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                currentTimeLine.name = txtDialog.getValue();
                form_MA.historyManager.AddHistory(HistoryType.Action);
                this.updateTLNaviRegion();
            }
        }

        private void panel_timeLine_Paint(object sender, PaintEventArgs e)
        {
            updateTLFrameRegion();
        }

        private void hScrollBar_frames_ValueChanged(object sender, EventArgs e)
        {
            updateTLFrameRegion();
            updateTLRulerRegion();
        }

        private void panel_timeLine_MouseDown(object sender, MouseEventArgs e)
        {
            int ex = e.X;
            int ey = e.Y;
            ex = MathUtil.limitNumber(ex, 0, ((Control)sender).Width);
            ey = MathUtil.limitNumber(ey, 0, ((Control)sender).Height);
            int yT = TimeLine_rowBegin + ey / TimeLine_rowH;
            int xT = TimeLine_columnBegin + ex / TimeLine_columnWidth;
            bool needUpdate_FrameRegion = false;
            bool needUpdate_NaviRegion = false;
            bool needUpdate_EditBox = false;
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                //释放多选焦点
                if (focusState != -1)
                {
                    if (!((e.Button == MouseButtons.Right) && (MathUtil.inRegionClose(xT, focusX1, focusX2) && MathUtil.inRegionClose(yT, focusY1, focusY2))))
                    {
                        focusState = -1;
                        needUpdate_FrameRegion = true;
                    }
                }
            }
            //设置多选焦点起点
            if ((e.Button == MouseButtons.Left) || (!MathUtil.inRegionClose(xT, focusX1, focusX2) || (!MathUtil.inRegionClose(yT, focusY1, focusY2))))
            {
                focusY1 = yT;
                focusX1 = xT;
                focusY2 = yT;
                focusX2 = xT;
                focusState = -1;
                if (ey < (TimeLine_rowEnd - TimeLine_rowBegin + 1) * TimeLine_rowH)
                {
                    focusState = 0;
                    needUpdate_FrameRegion = true;
                    needUpdate_NaviRegion = setFocusFrameByMouse(ey / TimeLine_rowH, ex / TimeLine_columnWidth);
                    if (currentTimeLine != null && focusFrame!=null)
                    {
                        form_MA.form_MFrameEdit.selectAll(false);
                        needUpdate_EditBox = true;
                    }
                }
                if (setTimeLinePosByX(ex))
                {
                    needUpdate_EditBox = true;
                    needUpdate_FrameRegion = true;
                }
            }
            //更新显示
            if (needUpdate_FrameRegion)
            {
                updateTLFrameRegion(false);
            }
            if (needUpdate_NaviRegion)
            {
                updateTLNaviRegion();
            }
            if (needUpdate_EditBox)
            {
                updateTLRulerRegion(false);
                form_MA.form_MFrameEdit.rememberCurrentUnitsCoor();
                form_MA.form_MFrameEdit.UpdateRegion_EditFrame();
                form_MA.form_MFrameLevel.UpdateRegion_FrameLevel();
                form_MA.form_MConfig.checkUnitProperty();
            }
            if (e.Button == MouseButtons.Right)
            {
                if (ey < (TimeLine_rowEnd - TimeLine_rowBegin + 1) * TimeLine_rowH)
                {
                    bool selectOneFrame = (focusX1 == focusX2 && focusY1 == focusY2 && focusX1 >= 0 && focusY1 >= 0);
                    插入帧间隔ToolStripMenuItem.Enabled = currentTimeLine != null;
                    插入关键帧ToolStripMenuItem.Enabled = selectOneFrame;
                    清除帧内容ToolStripMenuItem.Enabled = selectOneFrame && focusFrame!=null && focusFrame.Count() > 0;
                    删除当前帧ToolStripMenuItem.Enabled = currentTimeLine != null;
                    创建补间动画ToolStripMenuItem.Enabled = selectOneFrame && focusFrame != null && focusFrameType != MFrameType.TYPE_NUL && !focusFrame.hasMotion;
                    删除补间动画ToolStripMenuItem.Enabled = selectOneFrame && focusFrame != null && focusFrameType != MFrameType.TYPE_NUL && focusFrame.hasMotion;
                    复制关键帧ToolStripMenuItem.Enabled = selectOneFrame && focusFrame != null && focusFrameType == MFrameType.TYPE_KEY;
                    粘贴关键帧ToolStripMenuItem.Enabled = bakCopyedKeyFrame != null;
                    contextMenuStrip_frame.Show((Control)(sender), e.Location);
                }
            }
        }
        //复制剪贴板
        public void CopyClipboard()
        {
            if (currentTimeLineHoder == null || currentTimeLine == null || focusFrame==null)
            {
                return;
            }
            bool selectOneFrame = (focusX1 == focusX2 && focusY1 == focusY2 && focusX1 >= 0 && focusY1 >= 0);
            if (!selectOneFrame)
            {
                return;
            }
            bakCopyedKeyFrame = focusFrame.Clone();
        }
        //粘贴剪贴板
        public void PasteClipboard()
        {
            if (currentTimeLineHoder == null || currentTimeLine == null)
            {
                return;
            }
            bool selectOneFrame = (focusX1 == focusX2 && focusY1 == focusY2 && focusX1 >= 0 && focusY1 >= 0);
            if (!selectOneFrame)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Action);
            if (focusFrameType == MFrameType.TYPE_NUL)
            {
                currentTimeLine.insertKeyFrame(focusX1);
            }
            MFrameType frameType = currentTimeLine.getFrameTypeByX(focusX1);
            if (frameType == MFrameType.TYPE_MID)
            {
                currentTimeLine.insertKeyFrame(focusX1);
            }
            MFrame newKeyFrame = currentTimeLine.getFrameByX(focusX1);
            newKeyFrame.replaceContent(bakCopyedKeyFrame);
            form_MA.historyManager.AddHistory(HistoryType.Action);
            form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            this.updateTLFrameRegion();
            this.reseFocusFrame();
        }
        //清除剪贴板，避免在撤销时造成错误
        public void ClearClipboard()
        {
            bakCopyedKeyFrame = null;
        }

        private void panel_timeLine_MouseMove(object sender, MouseEventArgs e)
        {
            int ex = e.X;
            int ey = e.Y;
            ex = MathUtil.limitNumber(ex, 0, ((Control)sender).Width);
            ey = MathUtil.limitNumber(ey, 0, ((Control)sender).Height);
            //设置移动帧起始步骤
            if (!inDraging && (e.Button == MouseButtons.Left) && (ModifierKeys == Keys.Control))
            {
                if (currentTimeLine != null && focusFrame != null && focusFrameType == MFrameType.TYPE_KEY)
                {
                    panel_timeLine.Cursor = handCursor;
                    dragSrcX = TimeLine_columnBegin + ex / TimeLine_columnWidth;
                    dragSrcY = TimeLine_rowBegin + ey / TimeLine_rowH;
                    inDraging = true;
                }
            }
            if (inDraging)
            {
                dragDestX = TimeLine_columnBegin + ex / TimeLine_columnWidth;
                dragDestX = MathUtil.limitNumber(dragDestX, 0, short.MaxValue);
                dragDestY = TimeLine_rowBegin + ey / TimeLine_rowH;
                dragDestY = MathUtil.limitNumber(dragDestY, 0, currentTimeLineHoder.Count() - 1);
                updateTLFrameRegion(false);
            }
            //多选处理
            if (!inDraging && e.Button == MouseButtons.Left && focusState >= 0)
            {
                focusY2 = TimeLine_rowBegin + ey / TimeLine_rowH;
                focusX2 = TimeLine_columnBegin + ex / TimeLine_columnWidth;
                if (focusY2 > TimeLine_rowEnd)
                {
                    focusY2 = TimeLine_rowEnd;
                }
                if (focusState==0 &&(focusY2 != focusY1 || focusX2 != focusX1))
                {
                    focusState = 1;
                }
            }
            //更新时间轴位置
            if (!inDraging && e.Button == MouseButtons.Left)
            {
                if (setTimeLinePosByX(ex))
                {
                    updateTLRulerRegion(false);
                    form_MA.form_MFrameEdit.checkTransitProperty();
                    form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
                }
                updateTLFrameRegion(false);
            }
        }
        private void panel_timeLine_MouseUp(object sender, MouseEventArgs e)
        {
            if (inDraging)
            {
                if (focusFrame != null && currentTimeLine != null)
                {
                    form_MA.historyManager.ReadyHistory(HistoryType.Action);
                    //先克隆内容
                    MFrame copyFrame = focusFrame.Clone();
                    //先从原时间轴删除并补齐到前导关键帧
                    int id = focusFrame.GetID();
                    MFrame fronFrame = currentTimeLine[id - 1];
                    if (fronFrame != null)
                    {
                        fronFrame.timeLast += focusFrame.timeLast;
                        currentTimeLine.RemoveAt(id);
                    }
                    else
                    {
                        focusFrame.Clear();
                    }
                    //判断目标类型
                    MTimeLine destTimeLine = currentTimeLineHoder[dragDestY];
                    MFrame destFrame = destTimeLine.getFrameByX(dragDestX);
                    MFrameType destType = destTimeLine.getFrameTypeByX(dragDestX);
                    if (destFrame == null || destType == MFrameType.TYPE_MID)
                    {
                        destTimeLine.insertKeyFrame(dragDestX);
                        destFrame = destTimeLine.getFrameByX(dragDestX);
                        destFrame.replaceContent(copyFrame);
                    }
                    else if (destType == MFrameType.TYPE_KEY)
                    {
                        destFrame = destTimeLine.getFrameByX(dragDestX);
                        destFrame.replaceContent(copyFrame);
                    }
                    form_MA.historyManager.AddHistory(HistoryType.Action);
                }
                //复位状态
                inDraging = false;
                panel_timeLine.Cursor = Cursors.Default;
                if (setTimeLinePosByX(e.X))
                {
                    updateTLRulerRegion(false);
                    form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
                }
                this.clearFrameFocus();
                this.updateTLFrameRegion(true);


            }
        }
        private void panel_timeLineRuler_MouseDown(object sender, MouseEventArgs e)
        {
            int ex = e.X;
            if (e.Button == MouseButtons.Left)
            {
                if (setTimeLinePosByX(ex))
                {
                    updateTLFrameRegion(false);
                    updateTLRulerRegion(false);
                    form_MA.form_MFrameEdit.checkTransitProperty();
                    form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
                }
            }
        }

        private void panel_timeLineRuler_MouseMove(object sender, MouseEventArgs e)
        {
            int ex = e.X;
            if (e.Button == MouseButtons.Left)
            {
                if (setTimeLinePosByX(ex))
                {
                    updateTLFrameRegion(false);
                    updateTLRulerRegion(false);
                    form_MA.form_MFrameEdit.checkTransitProperty();
                    form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
                }
            }
        }
        //清除焦点帧
        public bool clearFrameFocus()
        {
            bool changed = focusState>=0;
            focusState = -1;
            focusX1 = -1;
            focusY1 = -1;
            focusX2 = -1;
            focusY2 = -1;
            return changed;
        }
        //设置当前点击的帧,返回是否时间轴被改变
        public bool setFocusFrameByMouse(int row, int column)
        {
            int focusRow = TimeLine_rowBegin + row;
            int focusColumn = TimeLine_columnBegin + column;
            return setFocusFrame(focusRow, focusColumn);
        }
        public bool setFocusFrame(int focusRow, int focusColumn)
        {
            focusX1 = focusX2 = focusColumn;
            focusY1 = focusY2 = focusRow;
            focusState = 1;
            bool tineLineChanged = false;
            if (currentTimeLineHoder != null)
            {
                //激活对应的行
                if (currentTimeLine == null || (currentTimeLine.GetID() != focusRow && focusRow < currentTimeLineHoder.Count()))
                {
                    setCurrentTimeLine(focusRow);
                    tineLineChanged = true;
                }
                //获得当前点击帧的帧类型
                if (currentTimeLine != null)
                {
                    focusFrame = currentTimeLine.getFrameByX(focusColumn);
                    focusFrameType = currentTimeLine.getFrameTypeByX(focusColumn);
                }
            }
            return tineLineChanged;
        }
        //重置当前帧为当前时间轴上的当前时间点
        public void reseFocusFrame()
        {
            if (currentTimeLine != null)
            {
                setFocusFrame(currentTimeLine.GetID(), timePosition);
            }
        }
        //设置时间轴X位置，返回时间轴X位置是否改变
        private bool setTimeLinePosByX(int ex)
        {
            int timeLinePosOld = timePosition;
            if (ex >= 0)
            {
                timePosition = TimeLine_columnBegin + ex / TimeLine_columnWidth;
            }
            else
            {
                timePosition = TimeLine_columnBegin;
            }
            int max = 0;
            if (currentTimeLineHoder != null)
            {
                max = currentTimeLineHoder.getMaxFrameLen() - 1;
            }
            timePosition = MathUtil.limitNumber(timePosition, 0, max);
            bool res = timeLinePosOld != timePosition;
            if (res)
            {
                form_MA.form_MFrameEdit.releaseTransform();
            }
            return res;
        }
        //设置时间轴X位置，返回时间轴X位置是否改变
        public bool setTimeLinePos(int posNew)
        {
            int timeLinePosOld = timePosition;
            int max = 0;
            if (currentTimeLineHoder != null)
            {
                max = currentTimeLineHoder.getMaxFrameLen() - 1;
            }
            timePosition = MathUtil.limitNumber(posNew, 0, max);
            bool res = timeLinePosOld != timePosition;
            if (res)
            {
                form_MA.form_MFrameEdit.releaseTransform();
            }
            return res;
        }
        private void 插入帧间隔ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTimeLineHoder != null && currentTimeLine != null)
            {
                int yMin=Math.Min(focusY1,focusY2);
                int yMax=Math.Max(focusY1,focusY2);
                int xMin=Math.Min(focusX1,focusX2);
                int xMax=Math.Max(focusX1,focusX2);
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                currentTimeLineHoder.insertFrameDelay(yMin, xMin, yMax - yMin + 1, xMax - xMin + 1);
                form_MA.historyManager.AddHistory(HistoryType.Action);
                this.clearFrameFocus();
                this.updateTLFrameRegion();
                this.updateTLRulerRegion();
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }

        private void 插入关键帧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (focusX1 == focusX2 && focusY1 == focusY2 && focusX1 >= 0 && focusY1 >= 0 && currentTimeLineHoder != null && currentTimeLine != null)
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                currentTimeLineHoder.insertKeyFrame(focusY1, focusX1);
                form_MA.historyManager.AddHistory(HistoryType.Action);
                this.clearFrameFocus();
                this.updateTLFrameRegion();
                this.updateTLRulerRegion();
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }
        private void 删除当前帧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTimeLineHoder != null && currentTimeLine != null)
            {
                int yMin = Math.Min(focusY1, focusY2);
                int yMax = Math.Max(focusY1, focusY2);
                int xMin = Math.Min(focusX1, focusX2);
                int xMax = Math.Max(focusX1, focusX2);
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                if (currentTimeLineHoder.removeFrameDelay(yMin, xMin, yMax - yMin + 1, xMax - xMin + 1))
                {
                    form_MA.historyManager.AddHistory(HistoryType.Action);
                }
                this.clearFrameFocus();
                setTimeLinePosByX(xMin * TimeLine_columnWidth);
                form_MA.form_MFrameEdit.checkTransitProperty();
                this.reseFocusFrame();
                this.updateTLFrameRegion();
                this.updateTLRulerRegion();
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }
        private void 清除帧内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (focusX1 == focusX2 && focusY1 == focusY2 && focusX1 >= 0 && focusY1 >= 0 && currentTimeLineHoder != null && currentTimeLine != null)
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                MFrame frame = currentTimeLine.getFrameByX(focusX1);
                if (frame != null)
                {
                    frame.Clear();
                }
                form_MA.historyManager.AddHistory(HistoryType.Action);
                this.clearFrameFocus();
                this.updateTLFrameRegion();
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }
        private void 创建补间动画ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (focusX1 == focusX2 && focusY1 == focusY2 && focusX1 >= 0 && focusY1 >= 0 && currentTimeLineHoder != null && currentTimeLine != null)
            {
                MFrame frame = currentTimeLine.getFrameByX(focusX1);
                if (frame != null)
                {
                    form_MA.historyManager.ReadyHistory(HistoryType.Action);
                    frame.hasMotion = true;
                    form_MA.historyManager.AddHistory(HistoryType.Action);
                }
                this.clearFrameFocus();
                this.updateTLFrameRegion();
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }

        private void 删除补间动画ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (focusX1 == focusX2 && focusY1 == focusY2 && focusX1 >= 0 && focusY1 >= 0 && currentTimeLineHoder != null && currentTimeLine != null)
            {
                MFrame frame = currentTimeLine.getFrameByX(focusX1);
                if (frame != null)
                {
                    form_MA.historyManager.ReadyHistory(HistoryType.Action);
                    frame.hasMotion = false;
                    form_MA.historyManager.AddHistory(HistoryType.Action);
                }
                this.clearFrameFocus();
                this.updateTLFrameRegion();
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }

        private void panel_timeLineRuler_Paint(object sender, PaintEventArgs e)
        {
            updateTLRulerRegion();
        }

        private void panel_timeLine_MouseLeave(object sender, EventArgs e)
        {
            if (inDraging)
            {
                inDraging = false;
                panel_timeLine.Cursor = Cursors.Default;
                this.updateTLFrameRegion(false);
            }
        }
        //获取编辑帧
        public MFrame getEditFrame()
        {
            if (currentTimeLineHoder == null || currentTimeLine == null || currentTimeLine.isLocked ||!currentTimeLine.isVisible)
            {
                return null;
            }
            if (focusFrame != null)
            {
                return focusFrame;
            }
            return currentTimeLine.getFrameByX(timePosition);
        }
        //获取指定时间点上的可见帧列表(即当前时间点的所有层上的帧集合，如果帧不可见，则不返回)
        private List<MFrame> sameTimeFrameList = new List<MFrame>();
        public List<MFrame> getSameTimeFrames_Visible()
        {
            sameTimeFrameList.Clear();
            if (currentTimeLineHoder == null)
            {
                return sameTimeFrameList;
            }
            for(int i=0;i<currentTimeLineHoder.Count();i++)
            {
                MTimeLine timeLine = currentTimeLineHoder[i];
                if (timeLine.isVisible)
                {
                    MFrame frame = timeLine.getFrameByX(timePosition);
                    if (frame != null)
                    {
                        sameTimeFrameList.Add(frame);
                    }
                }
            }
            return sameTimeFrameList;
        }
        public List<MFrame> getSameTimeFrames(bool visible,bool locked)
        {
            sameTimeFrameList.Clear();
            if (currentTimeLineHoder == null)
            {
                return sameTimeFrameList;
            }
            for (int i = 0; i < currentTimeLineHoder.Count(); i++)
            {
                MTimeLine timeLine = currentTimeLineHoder[i];
                if (timeLine.isVisible == visible && timeLine.isLocked == locked)
                {
                    MFrame frame = timeLine.getFrameByX(timePosition);
                    if (frame != null)
                    {
                        sameTimeFrameList.Add(frame);
                    }
                }
            }
            return sameTimeFrameList;
        }
        //获取指定时间点上的可见帧列表(即指定时间点的所有层上的帧集合，如果帧不可见，则不返回)
        private List<MFrame> sameTimeFrameListX = new List<MFrame>();
        public List<MFrame> getSameTimeFrames_Visible(int posX)
        {
            sameTimeFrameListX.Clear();
            if (currentTimeLineHoder == null)
            {
                return sameTimeFrameList;
            }
            for (int i = 0; i < currentTimeLineHoder.Count(); i++)
            {
                MTimeLine timeLine = currentTimeLineHoder[i];
                if (timeLine.isVisible)
                {
                    MFrame frame = timeLine.getFrameByX(posX);
                    if (frame != null)
                    {
                        sameTimeFrameListX.Add(frame);
                    }
                }

            }
            return sameTimeFrameListX;
        }


        private void pictureBox_Com_MouseDown(object sender, MouseEventArgs e)
        {
            Point pLocal = ((PictureBox)(sender)).Location;
            pLocal.Y += 1;
            pLocal.X += 1;
            ((PictureBox)(sender)).Location = pLocal;
        }

        private void pictureBox_Com_MouseUp(object sender, MouseEventArgs e)
        {
            Point pLocal = ((PictureBox)(sender)).Location;
            pLocal.Y -= 1;
            pLocal.X -= 1;
            ((PictureBox)(sender)).Location = pLocal;
        }

        private void pictureBox_Add_Click(object sender, EventArgs e)
        {
            添加单元ToolStripMenuItem_Click(null, null);
        }

        private void pictureBox_Del_Click(object sender, EventArgs e)
        {
            删除ToolStripMenuItem_Click(null, null);
        }

        private void pictureBox_OnionSkin_MouseDown(object sender, MouseEventArgs e)
        {
            OnionSkin_Enable = !OnionSkin_Enable;
            if (OnionSkin_Enable)
            {
                pictureBox_OnionSkin.Image = global::Cyclone.Properties.Resources.OnionSkin_Enable;
            }
            else
            {
                pictureBox_OnionSkin.Image = global::Cyclone.Properties.Resources.OnionSkin_Diable;
            }
            pictureBox_OnionSkin.Refresh();
            form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
        }

        private void pictureBox_OnionSkinMode_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_OnionSkinMode.Image = global::Cyclone.Properties.Resources.OnionSkin_ModeE;
            CMS_OnionSkin.Show(pictureBox_OnionSkinMode,new Point(0,pictureBox_OnionSkinMode.Height-1));
        }

        private void CMS_OnionSkin_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            pictureBox_OnionSkinMode.Image = global::Cyclone.Properties.Resources.OnionSkin_Mode;
        }

        private void setOSMode(int mode)
        {
            OnionSkin_Mode = mode;
            TSMI_0.Checked = OnionSkin_Mode == OnionSkinMode_FB;
            TSMI_1.Checked = OnionSkin_Mode == OnionSkinMode_Back;
            TSMI_2.Checked = OnionSkin_Mode == OnionSkinMode_Front;
            form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
        }
        private void setOSFrame(int frame)
        {
            OnionSkin_Frame = frame;
            透视1帧ToolStripMenuItem.Checked = OnionSkin_Frame == 1;
            透视2帧ToolStripMenuItem.Checked = OnionSkin_Frame == 2;
            form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
        }
        private void TSMI_0_Click(object sender, EventArgs e)
        {
            setOSMode(OnionSkinMode_FB);
        }


        private void TSMI_1_Click(object sender, EventArgs e)
        {
            setOSMode(OnionSkinMode_Back);
        }

        private void TSMI_2_Click(object sender, EventArgs e)
        {
            setOSMode(OnionSkinMode_Front);
        }

        private void 透视1帧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setOSFrame(1);
        }

        private void 透视2帧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setOSFrame(2);
        }

        private void pictureBox_eye_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentTimeLineHoder != null)
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                //检查是否有关闭的层
                if (currentTimeLineHoder.isSomeOneNotVisible())
                {
                    currentTimeLineHoder.makeAllVisible(true);
                }
                else
                {
                    currentTimeLineHoder.makeAllVisible(false);
                }
                form_MA.historyManager.AddHistory(HistoryType.Action);
                updateTLNaviRegion();
                form_MA.form_MFrameEdit.releaseFocusClips();
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }

        private void pictureBox_lock_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentTimeLineHoder != null)
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                //检查是否有关闭的层
                if (currentTimeLineHoder.isSomeOneLocked())
                {
                    currentTimeLineHoder.makeAllLocked(false);
                }
                else
                {
                    currentTimeLineHoder.makeAllLocked(true);
                }
                form_MA.historyManager.AddHistory(HistoryType.Action);
                updateTLNaviRegion();
                form_MA.form_MFrameEdit.releaseFocusClips();
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }

        private void 复制关键帧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyClipboard();
        }

        private void 粘贴关键帧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PasteClipboard();
        }





    }
}