using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.mod;
using Cyclone.alg;
using DockingUI.WinFormsUI.Docking;
using Cyclone.mod.anim;
using Cyclone.alg.math;
using Cyclone.alg.util;

namespace Cyclone.mod.anim
{
    public partial class Form_MFrameLevel : DockContent
    {
        Form_MAnimation form_MA;
        public Form_MFrameLevel(Form_MAnimation form_MAT)
        {
            InitializeComponent();
            this.form_MA = form_MAT;
            pictureBox_clipsLevel.MouseWheel += new MouseEventHandler(pictureBox_clipsLevel_MouseWheel);
        }
        protected override string GetPersistString()
        {
            return "Form_MFrameLevel";
        }
        private void pictureBox_clipsLevel_MouseDown(object sender, MouseEventArgs e)
        {
            if (!form_MA.Equals(Form.ActiveForm))
            {
                return;
            }
            pictureBox_clipsLevel.Focus();
            if (e.Button.Equals(MouseButtons.Left))//左键选择切片
            {
                if (form_MA.form_MTimeLine != null && form_MA.form_MTimeLine.focusFrame != null&&
                    !form_MA.form_MTimeLine.focusFrame.GetParent().isLocked)
                {
                   
                    clickClipsLevel(e.Y);
                }
            }
        }
        public float zoomLevel_ClipsLevel = 1;//切片层当前缩放级别
        public const float zoomLevelMin = 0.25f;//缩放最大级别
        public const float zoomLevelMax = 4.0f;//缩放最大级别
        //(切片层)滚轮事件
        private void pictureBox_clipsLevel_MouseWheel(object sender, MouseEventArgs e)
        {
            int value = e.Delta / 120;
            if (ModifierKeys == Keys.Control)
            {
                zoom(zoomLevel_ClipsLevel - value * 0.25f);
                Form_MAnimation.noScrollEvent = true;
                trackBar_zoomLevel.Value = (int)(zoomLevel_ClipsLevel/ 0.25f);
                Form_MAnimation.noScrollEvent = false;
            }
            else
            {
                int destValue = this.vScrollBar_ClipsLevel.Value - value * 5;
                if (destValue < vScrollBar_ClipsLevel.Minimum)
                {
                    destValue = vScrollBar_ClipsLevel.Minimum;
                }
                else if (destValue > vScrollBar_ClipsLevel.Maximum-9)
                {
                    destValue = vScrollBar_ClipsLevel.Maximum - 9;
                }
                if (destValue != vScrollBar_ClipsLevel.Value)
                {
                    vScrollBar_ClipsLevel.Value = destValue;
                }
            }


        }
        //切换到新的缩放比例
        private void zoom(float value)
        {
            float zoomLevel_ClipsLevelPre = zoomLevel_ClipsLevel;
            zoomLevel_ClipsLevel = value;
            if (zoomLevel_ClipsLevel > zoomLevelMax)
            {
                zoomLevel_ClipsLevel = zoomLevelMax;
            }
            if (zoomLevel_ClipsLevel < zoomLevelMin)
            {
                zoomLevel_ClipsLevel = zoomLevelMin;
            }
            if (zoomLevel_ClipsLevel != zoomLevel_ClipsLevelPre)
            {
                this.UpdateRegion_FrameLevel();
                label_zoomLevel.Text = "显示：" + (zoomLevel_ClipsLevel * 100) + "%";
            }
        }
        //切片层部分========================================================================================================
        private Image imgClipsLevelBuf = null;//切片层显示区域缓冲
        //(切片层图片框)刷新切片层图片框显示区域
        public void UpdateRegion_FrameLevel()
        {
            if (pictureBox_clipsLevel.Height <= 0 || pictureBox_clipsLevel.Width <= 0)
            {
                return;
            }
            //调整缓冲
            if (imgClipsLevelBuf == null || imgClipsLevelBuf.Width != pictureBox_clipsLevel.Width || imgClipsLevelBuf.Height != pictureBox_clipsLevel.Height)
            {
                imgClipsLevelBuf = new Bitmap(pictureBox_clipsLevel.Width, pictureBox_clipsLevel.Height);
            }
            //计算最大切片尺寸以调整滚动条
            //尺寸,同容器框大小
            int containerW = imgClipsLevelBuf.Width;
            int containerH = imgClipsLevelBuf.Height;
            //绘制到缓冲
            Graphics gBuf = Graphics.FromImage(imgClipsLevelBuf);
            gBuf.Clear(Color.Transparent);
            if (form_MA!=null &&form_MA.form_MTimeLine!=null && form_MA.form_MTimeLine.focusFrame != null)
            {
                //扫描切片，计算缓冲大小
                float maxWidth = 0;
                for (int i = 0; i < form_MA.form_MTimeLine.focusFrame.Count();i++ )//横向扫描最大切片宽度
                {
                    MFrameUnit clip = form_MA.form_MTimeLine.focusFrame[i];
                    float wI = clip.getTransformSize(zoomLevel_ClipsLevel).Width;
                    if (maxWidth < wI)
                    {
                        maxWidth = wI;
                    }
                }
                float maxHeight = 0;
                for (int i = 0; i < form_MA.form_MTimeLine.focusFrame.Count(); i++)//纵向扫描最大切片高度
                {
                    MFrameUnit clip = form_MA.form_MTimeLine.focusFrame[i];
                    maxHeight += clip.getLastTransformSize().Height + form_MA.LEVEL_GAP;
                }
                //调整滚动条
                int maxHeightS=(int)maxHeight;
                int scrollH = maxHeightS > containerH ? maxHeightS - containerH : 0;
                int x = 0;
                float y = (vScrollBar_ClipsLevel.Maximum - 9) == 0 ? 0 : vScrollBar_ClipsLevel.Value * scrollH / (vScrollBar_ClipsLevel.Maximum - 9);
                y = -y;
                //绘制所有切块
                for (int i = 0; i < form_MA.form_MTimeLine.focusFrame.Count(); i++)
                {
                    MFrameUnit clip = form_MA.form_MTimeLine.focusFrame[i];
                    SizeF clipBox = clip.getLastTransformSize();
                    clip.GdiDisplay(gBuf, x + clipBox.Width / 2 + form_MA.LEVEL_GAP, y + clipBox.Height / 2 + form_MA.LEVEL_GAP, zoomLevel_ClipsLevel, Form_MTimeLine.timePosition);
                    if (clip.isLocked)
                    {
                        GraphicsUtil.fillRect(gBuf, x + form_MA.LEVEL_GAP, y + form_MA.LEVEL_GAP, clipBox.Width, clipBox.Height, 0x00FF00, 0x44);
                    }
                    if (!clip.isVisible)
                    {
                        GraphicsUtil.fillRect(gBuf, x + form_MA.LEVEL_GAP, y + form_MA.LEVEL_GAP, clipBox.Width, clipBox.Height, Consts.colorRed, 0x44);
                        if (form_MA.form_MFrameEdit.currentUnits.Contains(clip))
                        {
                            GraphicsUtil.drawRect(gBuf, x + form_MA.LEVEL_GAP, y + form_MA.LEVEL_GAP, clipBox.Width, clipBox.Height, Consts.colorBlue);
                        }
                    }
                    else if (form_MA.form_MFrameEdit.currentUnits.Contains(clip))
                    {
                        GraphicsUtil.fillRect(gBuf, x + form_MA.LEVEL_GAP, y + form_MA.LEVEL_GAP, clipBox.Width, clipBox.Height, Consts.colorBlue, 0x44);
                        GraphicsUtil.drawRect(gBuf, x + form_MA.LEVEL_GAP, y + form_MA.LEVEL_GAP, clipBox.Width, clipBox.Height, Consts.colorBlue);
                    }
                    else
                    {
                        GraphicsUtil.drawRect(gBuf, x + form_MA.LEVEL_GAP, y + form_MA.LEVEL_GAP, clipBox.Width, clipBox.Height, Consts.colorDarkGray);
                    }
                    y += clipBox.Height + form_MA.LEVEL_GAP;
                }
            }
            gBuf.Dispose();
            //绘制到屏幕
            if (pictureBox_clipsLevel.Image == null || !pictureBox_clipsLevel.Image.Equals(imgClipsLevelBuf))
            {
                pictureBox_clipsLevel.Image = imgClipsLevelBuf;
            }
            else
            {
                pictureBox_clipsLevel.Refresh();
            }
        }
        //(切片层图片框)向上移动切片焦点
        public void focusUp_ClipsLevel()
        {
            if (form_MA.form_MTimeLine == null || form_MA.form_MTimeLine.focusFrame == null || form_MA.form_MFrameEdit.currentUnits.Count == 0)
            {
                return;
            }
            int id = 0;
            if (form_MA.form_MFrameEdit.currentUnits.Count == 1)
            {
                id = form_MA.form_MTimeLine.focusFrame.GetSonID(form_MA.form_MFrameEdit.currentUnits[0]);
            }
            else
            {
                id = form_MA.form_MFrameEdit.getFirstIndexInSelectedUnits();
            }
            if (id <= 0)
            {
                return;
            }
            form_MA.form_MFrameEdit.selectUnit(id-1);
            showUnitInView(id - 1);
            form_MA.form_MFrameEdit.UpdateRegion_EditFrame();
            UpdateRegion_FrameLevel();
        }
        //(切片层图片框)向下移动切片焦点
        public void focusDown_ClipsLevel()
        {
            if (form_MA.form_MTimeLine == null || form_MA.form_MTimeLine.focusFrame == null || form_MA.form_MFrameEdit.currentUnits.Count == 0)
            {
                return;
            }
            int id = 0;
            if (form_MA.form_MFrameEdit.currentUnits.Count == 1)
            {
                id = form_MA.form_MTimeLine.focusFrame.GetSonID(form_MA.form_MFrameEdit.currentUnits[0]);
            }
            else
            {
                id = form_MA.form_MFrameEdit.getLastIndexInSelectedUnits();
            }
            if (id < 0 || id >= form_MA.form_MTimeLine.focusFrame.Count() - 1)
            {
                return;
            }
            form_MA.form_MFrameEdit.selectUnit(id + 1);
            showUnitInView(id + 1);
            form_MA.form_MFrameEdit.UpdateRegion_EditFrame();
            UpdateRegion_FrameLevel();
        }
        //调整滚动条，让切片层中指定的单元完整显示
        public bool showUnitInView(int id)
        {
            if (form_MA.form_MTimeLine==null || form_MA.form_MTimeLine.focusFrame == null || id < 0 || id >= form_MA.form_MTimeLine.focusFrame.Count())
            {
                return false;
            }
            float maxHeight = 0;
            float unitY = 0;
            float unitH = 0;
            for (int i = 0; i < form_MA.form_MTimeLine.focusFrame.Count(); i++)
            {
                MFrameUnit clip = form_MA.form_MTimeLine.focusFrame[i];
                SizeF sizeI = clip.getTransformSize(zoomLevel_ClipsLevel);
                if (id == i)
                {
                    unitY = maxHeight;
                    unitH = sizeI.Height;
                }
                maxHeight += sizeI.Height + form_MA.LEVEL_GAP;
            }
            int maxHeightS = (int)maxHeight;
            int containerH = pictureBox_clipsLevel.Height;
            if (maxHeightS <= containerH || (vScrollBar_ClipsLevel.Maximum - 9) == 0)
            {
                return false;
            }
            int scrollH = maxHeightS - containerH;
            float y =  vScrollBar_ClipsLevel.Value * scrollH / (vScrollBar_ClipsLevel.Maximum - 9);
            int needChange = -1;
            if (unitY < y)
            {
                y = unitY;
                needChange = 0;
            }
            if (unitY + unitH > y + containerH)
            {
                y = unitY + unitH + form_MA.LEVEL_GAP - containerH;
                needChange = 1;
            }
            if (needChange<0)
            {
                return false;
            }
            if (y < 0)
            {
                y = 0;
            }
            Form_MAnimation.noScrollEvent = true;
            int newValue = (int)((y * (vScrollBar_ClipsLevel.Maximum - 9) + (scrollH - 1) * needChange) / scrollH);
            newValue = MathUtil.limitNumber(newValue, vScrollBar_ClipsLevel.Minimum, vScrollBar_ClipsLevel.Maximum - 9);
            vScrollBar_ClipsLevel.Value = newValue;
            Form_MAnimation.noScrollEvent = false;
            return true;
        }
        //(切片层图片框)设置切片焦点
        public void clickClipsLevel(int cursorY)
        {
            if (form_MA.form_MTimeLine == null || form_MA.form_MTimeLine.focusFrame == null)
            {
                return;
            }
            float maxHeight = 0;
            for (int i = 0; i < form_MA.form_MTimeLine.focusFrame.Count(); i++)//纵向扫描所有切片高度
            {
                MFrameUnit clip = form_MA.form_MTimeLine.focusFrame[i];
                maxHeight += clip.getTransformSize(zoomLevel_ClipsLevel).Height + form_MA.LEVEL_GAP;
            }
            //maxHeight *= zoomLevel_ClipsLevel;
            int containerH = imgClipsLevelBuf.Height;
            int maxHeightS = (int)maxHeight;
            int scrollH = maxHeightS > containerH ? maxHeightS - containerH : 0;
            cursorY += (vScrollBar_ClipsLevel.Maximum - 9) == 0 ? 0 : vScrollBar_ClipsLevel.Value * scrollH / (vScrollBar_ClipsLevel.Maximum - 9);

            maxHeight = 0;
            MFrameUnit focusedClipclip = null;
            for (int i = 0; i < form_MA.form_MTimeLine.focusFrame.Count(); i++)//纵向扫描所有切片高度
            {
                MFrameUnit clip = form_MA.form_MTimeLine.focusFrame[i];
                SizeF clipBox = clip.getLastTransformSize();
                if (MathUtil.inRegionCloseLeft(cursorY, maxHeight, maxHeight + (clipBox.Height + form_MA.LEVEL_GAP)))
                {
                    focusedClipclip = clip;
                    break;
                }
                maxHeight += (clipBox.Height + form_MA.LEVEL_GAP);
            }
            if (focusedClipclip != null)
            {
                if (ModifierKeys == Keys.Shift)
                {
                    if (form_MA.form_MFrameEdit.currentUnits.Count == 1)
                    {
                        MFrameUnit startClip = form_MA.form_MFrameEdit.currentUnits[0];
                        int startID = startClip.GetID();
                        int endID = focusedClipclip.GetID();
                        form_MA.form_MFrameEdit.releaseFocusClips();
                        for (int i = Math.Min(startID, endID); i <= Math.Max(startID, endID); i++)
                        {
                            form_MA.form_MFrameEdit.addFocusClips(form_MA.form_MTimeLine.focusFrame[i], false);
                        }
                    }
                    else
                    {
                        form_MA.form_MFrameEdit.releaseFocusClips();
                        form_MA.form_MFrameEdit.addFocusClips(focusedClipclip, false);
                    }
                }
                else if (ModifierKeys == Keys.Control)
                {
                    form_MA.form_MFrameEdit.addFocusClips(focusedClipclip, false, true);
                }
                else
                {
                    form_MA.form_MFrameEdit.releaseFocusClips();
                    form_MA.form_MFrameEdit.addFocusClips(focusedClipclip, false);
                }
                form_MA.form_MFrameEdit.UpdateRegion_EditFrame();
                UpdateRegion_FrameLevel();
                form_MA.form_MConfig.checkUnitProperty();
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Action);

        }

        private void pictureBox_clipsLevel_MouseEnter(object sender, EventArgs e)
        {
            if (!form_MA.Equals(Form.ActiveForm))
            {
                return;
            }
            pictureBox_clipsLevel.Focus();
            Form_MAnimation.transCmdKey = true;
        }

        private void pictureBox_clipsLevel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (pictureBox_clipsLevel.Focused)
            {
                if (form_MA.form_MTimeLine.focusFrame != null)
                {
                    switch (e.KeyValue)
                    {
                        case (int)Keys.Up:
                            if (!e.Control)
                            {
                                focusUp_ClipsLevel();
                            }
                            else
                            {
                                form_MA.form_MFrameEdit.moveUpCurrentClip();
                            }
                            break;
                        case (int)Keys.Down:
                            if (!e.Control)
                            {
                                focusDown_ClipsLevel();
                            }
                            else
                            {
                                form_MA.form_MFrameEdit.moveDownCurrentClip();
                            }
                            break;
                        case (int)Keys.Delete:
                            form_MA.form_MFrameEdit.deleteCurrentUnits();
                            break;
                        case (int)Keys.C:
                            if (e.Control)
                            {
                                form_MA.form_MFrameEdit.copyActionClips();
                            }
                            break;
                        case (int)Keys.V:
                            if (e.Control)
                            {
                                form_MA.form_MFrameEdit.pasteActionClips();
                            }
                            break;
                    }
                }
            }
        }

        private void vScrollBar_ClipsLevel_ValueChanged(object sender, EventArgs e)
        {
            if (Form_MAnimation.noScrollEvent)
            {
                return;
            }
            this.UpdateRegion_FrameLevel();
        }

        private void button_visible_Click(object sender, EventArgs e)
        {
            if (form_MA.form_MFrameEdit.currentUnits.Count == 0)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Action);
            bool makeVisible = false;
            for (int i = 0; i < form_MA.form_MFrameEdit.currentUnits.Count; i++)
            {
                MFrameUnit clipElement = form_MA.form_MFrameEdit.currentUnits[i];
                if (clipElement.isVisible)
                {
                    makeVisible = true;
                    break;
                }
            }
            makeVisible = !makeVisible;
            for (int i = 0; i < form_MA.form_MFrameEdit.currentUnits.Count; i++)
            {
                MFrameUnit clipElement = form_MA.form_MFrameEdit.currentUnits[i];
                clipElement.isVisible = makeVisible;
            }
            form_MA.historyManager.AddHistory(HistoryType.Action);
            updateLevelEye();
            updateLevelLock();
            form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
        }

        private void button_lock_Click(object sender, EventArgs e)
        {
            if (form_MA.form_MFrameEdit.currentUnits.Count == 0)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Action);
            bool makeLock = false;
            for (int i = 0; i < form_MA.form_MFrameEdit.currentUnits.Count; i++)
            {
                MFrameUnit clipElement = form_MA.form_MFrameEdit.currentUnits[i];
                if (clipElement.isLocked)
                {
                    makeLock = true;
                    break;
                }
            }
            makeLock = !makeLock;
            for (int i = 0; i < form_MA.form_MFrameEdit.currentUnits.Count; i++)
            {
                MFrameUnit clipElement = form_MA.form_MFrameEdit.currentUnits[i];
                clipElement.isLocked = makeLock;
            }
            form_MA.historyManager.AddHistory(HistoryType.Action);
            updateLevelLock();
            form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
        }
        //更新层可见状态
        public void updateLevelEye()
        {
            if (form_MA.form_MFrameEdit.currentUnits.Count == 0)
            {
                return;
            }
            bool makeVisible = false;
            for (int i = 0; i < form_MA.form_MFrameEdit.currentUnits.Count; i++)
            {
                MFrameUnit clipElement = form_MA.form_MFrameEdit.currentUnits[i];
                if (clipElement.isVisible)
                {
                    makeVisible = true;
                    break;
                }
            }
            button_visible.BackgroundImage = makeVisible ? global::Cyclone.Properties.Resources.level_eye_on : global::Cyclone.Properties.Resources.level_eye_off;
        }
        //更新层锁定状态
        public void updateLevelLock()
        {
            if (form_MA.form_MFrameEdit.currentUnits.Count == 0)
            {
                return;
            }
            bool makeLock = false;
            for (int i = 0; i < form_MA.form_MFrameEdit.currentUnits.Count; i++)
            {
                MFrameUnit clipElement = form_MA.form_MFrameEdit.currentUnits[i];
                if (clipElement.isLocked)
                {
                    makeLock = true;
                    break;
                }
            }
            button_lock.BackgroundImage = makeLock ? global::Cyclone.Properties.Resources.level_lock_locked : global::Cyclone.Properties.Resources.level_lock_unclocked;
        }

        private void pictureBox_clipsLevel_MouseLeave(object sender, EventArgs e)
        {
            Form_MAnimation.transCmdKey = false;
        }

        private void trackBar_zoomLevel_ValueChanged(object sender, EventArgs e)
        {
            if (Form_MAnimation.noScrollEvent)
            {
                return;
            }
            zoom(0.25f*((int)(trackBar_zoomLevel.Value)));
        }

        private void button_changeLevelUp_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.moveUpCurrentClip();
        }

        private void button_changeLevelDown_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.moveDownCurrentClip();
        }
    }
}