using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.mod;
using Cyclone.alg;
using System.Collections;
using System.Threading;
using System.IO;
using DockingUI.WinFormsUI.Docking;
using Cyclone.mod.anim;
using System.Drawing.Drawing2D;
using OpenTK.Graphics.OpenGL;
using Cyclone.alg.math;
using Cyclone.alg.opengl;
using Cyclone.alg.util;
using Cyclone.alg.win32;

namespace Cyclone.mod.anim
{
    public partial class Form_MFrameEdit : DockContent
    {
        Form_MAnimation form_MA;
        Cursor cursor_unitSelect = null;
        Cursor cursor_unitMove = null;
        Cursor handCursor = null;
        Cursor[] transCursors = new Cursor[10];
        private bool noScrollEvent = false;
        private int MouseLeftDownMoved = -1;//这个值用来记录左键按下后是否移动过，-1空状态，0，左键按下，1左键按下后移动。
        public Form_MFrameEdit(Form_MAnimation form_MAT)
        {
            this.form_MA = form_MAT;
            InitializeComponent();
            //滚动条
            hScrollBar_FrameEdit.Maximum = MAX_OFFSET * 2 + 9;
            vScrollBar_FrameEdit.Maximum = MAX_OFFSET * 2 + 9;
            //鼠标资源
            cursor_unitSelect = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_unitSelect.ico"));
            cursor_unitMove = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_unitMove.ico"));
            handCursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.hand.ico"));
            transCursors[0] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_rotate.ico"));
            transCursors[1] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_scale0.ico"));
            transCursors[2] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_scale1.ico"));
            transCursors[3] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_scale2.ico"));
            transCursors[4] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_scale3.ico"));
            transCursors[5] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_coner0.ico"));
            transCursors[6] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_coner1.ico"));
            transCursors[7] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_coner2.ico"));
            transCursors[8] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_coner3.ico"));
            //transCursors[9] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_sherk0.ico"));
            //transCursors[10] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_sherk1.ico"));
            //transCursors[11] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_sherk2.ico"));
            //transCursors[12] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_trans_sherk3.ico"));
            transCursors[9] = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursor_unitMoveCenter.ico"));

            glView.MouseWheel += new MouseEventHandler(glView_MouseWheel);
            glView.Cursor = cursor_unitSelect;
        }
        protected override string GetPersistString()
        {
            return "Form_MFrameEdit";
        }
        //帧编辑部分========================================================================================================
        private TextureImage imgEditBgBuffer = null;//窗口背景缓存
        private int imgEditCenterPixelX = 0;//中心点像素坐标X
        private int imgEditCenterPixelY = 0; //中心点像素坐标
        private short MAX_OFFSET = 1024;//中心点像素坐标与编辑器中心最大偏移，决定了整个世界的大小
        private int imgEditCenterPixelX_Pre = 0;//中心点像素坐标X备份
        private int imgEditCenterPixelY_Pre = 0; //中心点像素坐标备份
        private int imgEditBfWidth = 0;//窗口缓存宽度
        private int imgEditBfHeight = 0; //窗口缓存高度
        public int zoomLevel = 1;//当前缩放级别
        public const int zoomLevelMin = 1;//缩放最大级别
        public const int zoomLevelMax = 16;//缩放最大级别
        public List<MFrameUnit> currentUnits = new List<MFrameUnit>();//当前焦点切片集合
        private List<PointF> currentUnitsPositions = new List<PointF>();//当前切片坐标
        private List<MFrameUnit> bakClipElemnts = new List<MFrameUnit>();//拷贝切片集的备份
        //private Graphics gPictureBox = null;//编辑区的绘制句柄
        public Operation_Transform TransformNew = null;//变形对象
        public Operation_Transform TransformOld = null;//前一变形对象
        //删除帧元素
        public void deleteCurrentUnits()
        {
            if (form_MA.form_MTimeLine!=null)
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                for (int i = 0; i < currentUnits.Count; i++)
                {
                    if (currentUnits[i] != null)
                    {
                        Object parent = currentUnits[i].GetParent();
                        if (parent is MFrame)
                        {
                            ((MFrame)parent).Remove(currentUnits[i]);
                        }
                    }
                }
                form_MA.form_MTimeLine.updateTLFrameRegion();
                UpdateRegion_EditFrame();
                form_MA.form_MFrameLevel.UpdateRegion_FrameLevel();
                form_MA.historyManager.AddHistory(HistoryType.Action);
            }

        }
        //寻找当前焦点集合的中心
        private static float[] centerPoint = new float[4];
        private float[] findClipsCenter()
        {
            if (currentUnits == null || currentUnits.Count == 0)
            {
                return null;
            }
            //寻找所有切片的左上角和右下角
            float xSum = 0;
            float ySum = 0;
            for (int i = 0; i < currentUnits.Count; i++)
            {
                xSum+= ((MFrameUnit_Bitmap)currentUnits[i]).posX;
                ySum+= ((MFrameUnit_Bitmap)currentUnits[i]).posY;
            }
            centerPoint[0] = xSum;
            centerPoint[1] = ySum;
            if (currentUnits.Count > 0)
            {
                xSum /= currentUnits.Count;
                ySum /= currentUnits.Count;
            }
            centerPoint[0] = xSum;
            centerPoint[1] = ySum;
            return centerPoint;
        }
        //按照ID排序当前选中切片
        private void orderCurrentClips()
        {
            for (int i = 0; i < currentUnits.Count; i++)
            {
                MFrameUnit actionClip = currentUnits[i];
                int index = actionClip.GetID();
                for (int j = 0; j < i; j++)
                {
                    MFrameUnit actionClipJ = currentUnits[j];
                    int indexJ = actionClipJ.GetID();
                    if (index < indexJ)
                    {
                        MFrameUnit obj = currentUnits[i];
                        currentUnits.RemoveAt(i);
                        if (j >= currentUnits.Count)
                        {
                            currentUnits.Add(obj);
                        }
                        else
                        {
                            currentUnits.Insert(j, obj);
                        }
                        break;
                    }
                }
            }
            form_MA.form_MFrameLevel.updateLevelEye();
            form_MA.form_MFrameLevel.updateLevelLock();
        }
        //向上层(里)移动切片
        public void moveUpCurrentClip()
        {
            if (form_MA.form_MTimeLine == null || form_MA.form_MTimeLine.focusFrame == null ||currentUnits.Count == 0)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Action);
            orderCurrentClips();
            int indexFirst = getFirstIndexInSelectedUnits();
            indexFirst--;
            if (indexFirst < 0)
            {
                indexFirst = 0;
            }
            for (int i = 0; i < currentUnits.Count; i++)
            {
                MFrameUnit actionClip = currentUnits[i];
                form_MA.form_MTimeLine.focusFrame.Remove(actionClip);
            }
            for (int i = 0; i < currentUnits.Count; i++)
            {
                MFrameUnit actionClip = currentUnits[i];
                form_MA.form_MTimeLine.focusFrame.Insert(actionClip,indexFirst + i);
            }
            form_MA.historyManager.AddHistory(HistoryType.Action);
            UpdateRegion_EditFrame();
            form_MA.form_MFrameLevel.showUnitInView(indexFirst);
            form_MA.form_MFrameLevel.UpdateRegion_FrameLevel();
            form_MA.form_MFrameLevel.updateLevelEye();
            form_MA.form_MFrameLevel.updateLevelLock();
        }
        //获取当前选中切片集合的最里层单元ID
        public int getFirstIndexInSelectedUnits()
        {
            if (currentUnits.Count == 0)
            {
                return -1;
            }
            int indexFirst = int.MaxValue;
            for (int i = 0; i < currentUnits.Count; i++)
            {
                MFrameUnit actionClip = currentUnits[i];
                int index = actionClip.GetID();
                if (indexFirst > index)
                {
                    indexFirst = index;
                }
            }
            return indexFirst;
        }
        //向下层(外)移动切片
        public void moveDownCurrentClip()
        {
            if (form_MA.form_MTimeLine == null || form_MA.form_MTimeLine.focusFrame == null || currentUnits.Count == 0)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Action);
            orderCurrentClips();
            int indexLast = getLastIndexInSelectedUnits();
            indexLast++;
            indexLast -= currentUnits.Count - 1;
            for (int i = 0; i < currentUnits.Count; i++)
            {
                MFrameUnit actionClip = currentUnits[i];
                form_MA.form_MTimeLine.focusFrame.Remove(actionClip);
            }
            if (indexLast > form_MA.form_MTimeLine.focusFrame.Count())
            {
                indexLast = form_MA.form_MTimeLine.focusFrame.Count();
            }
            for (int i = 0; i < currentUnits.Count; i++)
            {
                MFrameUnit actionClip = currentUnits[i];
                if (indexLast + i >= form_MA.form_MTimeLine.focusFrame.Count())
                {
                    form_MA.form_MTimeLine.focusFrame.Add(actionClip);
                }
                else
                {
                    form_MA.form_MTimeLine.focusFrame.Insert(actionClip,indexLast + i);
                }
            }
            form_MA.historyManager.AddHistory(HistoryType.Action);
            UpdateRegion_EditFrame();
            form_MA.form_MFrameLevel.showUnitInView(indexLast + currentUnits.Count - 1);
            form_MA.form_MFrameLevel.UpdateRegion_FrameLevel();
            form_MA.form_MFrameLevel.updateLevelEye();
            form_MA.form_MFrameLevel.updateLevelLock();
        }
        //获取当前选中切片集合的最外层单元ID
        public int getLastIndexInSelectedUnits()
        {
            if (currentUnits.Count == 0)
            {
                return -1;
            }
            int indexLast = 0;
            for (int i = 0; i < currentUnits.Count; i++)
            {
                MFrameUnit actionClip = currentUnits[i];
                int index = actionClip.GetID();
                if (indexLast < index)
                {
                    indexLast = index;
                }
            }
            return indexLast;
        }
        //将切片增加到为焦点切片集合
        public void addFocusClips(MFrameUnit clipElement, bool focusItsCenter)
        {
            addFocusClips(clipElement, focusItsCenter, false);
        }
        public void addFocusClips(MFrameUnit clipElement, bool focusItsCenter, bool allowDel)
        {
            if (clipElement == null)
            {
                return;
            }
            if (!currentUnits.Contains(clipElement))
            {
                currentUnits.Add(clipElement);
            }
            else if (allowDel)
            {
                currentUnits.Remove(clipElement);
            }
            if (focusItsCenter)
            {
                imgEditCenterPixelX = (int)clipElement.posX;
                imgEditCenterPixelY = (int)clipElement.posY;
            }
            rememberCurrentUnitsCoor();
            form_MA.form_MFrameLevel.updateLevelEye();
            form_MA.form_MFrameLevel.updateLevelLock();
            form_MA.showInfor("共选中了" + currentUnits.Count + "个单元");

        }
        //释放焦点切片
        public void releaseFocusClips()
        {
            currentUnits.Clear();
            rememberCurrentUnitsCoor();
            releaseTransform();
            form_MA.form_MConfig.checkUnitProperty();
        }
        public void releaseTransform()
        {
            TransformNew = null;
        }
        //整体移动当前焦点切片集
        private void moveCurrentClips(int x, int y)
        {
            if (currentUnits == null)
            {
                return;
            }
            checkTransitFrame();
            MFrameUnit_Bitmap clipElement = null;
            for (int i = 0; i < currentUnits.Count; i++)
            {
                if (currentUnits[i] != null)
                {
                    clipElement = (MFrameUnit_Bitmap)currentUnits[i];
                    clipElement.posX += (short)x;
                    clipElement.posY += (short)y;
                }
            }
            if (TransformNew != null)
            {
                TransformNew.posX = TransformOld.posX + (short)x;
                TransformNew.posY = TransformOld.posY + (short)y;
            }
            rememberCurrentUnitsCoor();
            form_MA.historyManager.AddHistory(HistoryType.Action);
            form_MA.form_MConfig.checkUnitProperty();
        }
        //记下当前所有切片的坐标
        public void rememberCurrentUnitsCoor()
        {
            rememberCurrentUnitsCoor(true);
        }
        private void rememberCurrentUnitsCoor(bool updateTransform)
        {
            currentUnitsPositions.Clear();
            if (currentUnits.Count == 0)
            {
                return;
            }
            MFrameUnit clip = null;
            for (int i = 0; i < currentUnits.Count; i++)
            {
                if (currentUnits[i] != null)
                {
                    clip = currentUnits[i];
                    currentUnitsPositions.Add(new PointF(clip.posX, clip.posY));
                }
                else
                {
                    Console.WriteLine("error");
                }
            }
            if (updateTransform)
            {
                if (TransformNew != null)
                {
                    TransformOld = (Operation_Transform)(TransformNew.Clone());
                }
            }
        }
        //刷新编辑区显示
        public void UpdateRegion_EditAndFrameLevel()
        {
            UpdateRegion_EditFrame();
            if (form_MA.form_MFrameLevel!=null)
            {
                form_MA.form_MFrameLevel.UpdateRegion_FrameLevel();
            }

        }
        private Matrix4 matrixDraw = new Matrix4();
        public void UpdateRegion_EditFrame()
        {
            if (Form_MAnimation.inResetPanels)
            {
                return;
            }
            if (!loaded || this.IsHidden || this.Width <= 0 || this.Height <= 0)
            {
                return;
            }
            if (!glView.MakeCurrent())
            {
                return;
            }
            //获取屏幕尺寸
            imgEditBfWidth = glView.Width;
            imgEditBfHeight = glView.Height;
            GLGraphics.resetClip(imgEditBfWidth, imgEditBfHeight);
            //用白色清空屏幕
            GL.ClearColor(Color.White);
            //清除颜色和深度为缓冲
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //重置模型视图矩阵
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            //用白色清空屏幕
            GL.ClearColor(Color.White);
            //附加属性
            //调整滚动条的调节范围（等同当前的背景图片可以动尺寸）
            setHScroolBarValue(MAX_OFFSET * 2 + 9, (MAX_OFFSET - imgEditCenterPixelX));
            setVScroolBarValue(MAX_OFFSET * 2 + 9, (MAX_OFFSET - imgEditCenterPixelY));

            //窗口缓冲尺寸
            if (imgEditBfWidth <= 0 || imgEditBfHeight <= 0)
            {
                return;
            }
            //映射坐标系
            matrixDraw.identity();
            matrixDraw.preScale(1.0f, -1.0f, 1.0f);
            matrixDraw.postTranslate(0, imgEditBfHeight, 0.0f);
            float[] data = matrixDraw.getValue();
            GL.MultMatrix(data);
            //创建网格
            int w_clip = 8;
            int h_clip = 8;
            if (imgEditBgBuffer == null || imgEditBgBuffer.OrgWidth != imgEditBfWidth + w_clip * 4
                || imgEditBgBuffer.OrgHeight != imgEditBfHeight + h_clip * 4)
            {
                Bitmap imgTemp = new Bitmap(imgEditBfWidth + w_clip * 4, imgEditBfHeight + h_clip * 4);
                int x = -w_clip;
                int y = -h_clip;
                bool bColor = true;
                Graphics gBgBuffer = Graphics.FromImage(imgTemp);
                GraphicsUtil.fillRect(gBgBuffer, 0, 0, imgTemp.Width, imgTemp.Height, Consts.color_white);
                while (x <= imgTemp.Width)
                {
                    bColor = !bColor;
                    int yT = y + (bColor ? h_clip : 0);
                    while (yT <= imgTemp.Height)
                    {
                        GraphicsUtil.fillRect(gBgBuffer, x, yT, w_clip, h_clip, Consts.colorGray);
                        yT += h_clip * 2;
                    }
                    x += w_clip;
                }
                imgEditBgBuffer = ConstTextureImgs.createImage(imgTemp);
                gBgBuffer.Dispose();
                gBgBuffer = null;
                imgTemp.Dispose();
                imgTemp = null;
            }
            //绘制背景
            if (Consts.showBgGrid)
            {
                int left = imgEditCenterPixelX * zoomLevel + imgEditBfWidth / 2;
                int top = -imgEditCenterPixelY * zoomLevel + imgEditBfHeight / 2;
                left += -(left / (w_clip * 2)) * (w_clip * 2);
                top += -(top / (h_clip * 2)) * (h_clip * 2);
                if (left > 0)
                {
                    left -= (w_clip * 2);
                }
                if (top > 0)
                {
                    top -= (h_clip * 2);
                }
                GLGraphics.drawTextureImage(imgEditBgBuffer, new RectangleF(-left, -top, imgEditBfWidth, imgEditBfHeight), 0, 0);
            }
            else
            {
                GLGraphics.setRGBColor(Consts.colorAnimBG);
                GLGraphics.fillRect(0, 0, imgEditBfWidth, imgEditBfHeight);
            }
            //绘制所有动画
            if (form_MA.form_MTimeLine != null)
            {
                //绘制洋葱皮效果
                if (Form_MTimeLine.OnionSkin_Enable)
                {
                    int bfCount = 1;
                    int ffCount = 1;
                    switch (Form_MTimeLine.OnionSkin_Mode)
                    {
                        case Form_MTimeLine.OnionSkinMode_FB:
                            bfCount = Form_MTimeLine.OnionSkin_Frame;
                            ffCount = Form_MTimeLine.OnionSkin_Frame;
                            break;
                        case Form_MTimeLine.OnionSkinMode_Back:
                            bfCount = Form_MTimeLine.OnionSkin_Frame;
                            ffCount = 0;
                            break;
                        case Form_MTimeLine.OnionSkinMode_Front:
                            bfCount = 0;
                            ffCount = Form_MTimeLine.OnionSkin_Frame;
                            break;
                    }

                    for (int i = -bfCount; i <= ffCount; i++)
                    {
                        if (i == 0)
                        {
                            continue;
                        }
                        List<MFrame> frameEditListX = form_MA.form_MTimeLine.getSameTimeFrames_Visible(Form_MTimeLine.timePosition + i);
                        foreach (MFrame frameEdit in frameEditListX)
                        {
                            if (frameEdit != null)
                            {
                                float alpha = (float)(Math.Pow(0.7, Math.Abs(i)));
                                frameEdit.glDisplay(getUICenterX(), getUICenterY(), zoomLevel, null, false, alpha, Form_MTimeLine.timePosition + i);
                            }
                        }
                    }

                }
                //绘制当前帧
                List<MFrame> frameEditList = form_MA.form_MTimeLine.getSameTimeFrames_Visible();
                foreach (MFrame frameEdit in frameEditList)
                {
                    if (frameEdit != null)
                    {
                        frameEdit.glDisplay(getUICenterX(), getUICenterY(), zoomLevel, currentUnits, TransformNew == null, Form_MTimeLine.timePosition);
                    }
                }
            }
            //边框
            if (Consts.showScreenFrame)
            {
                //世界边缘
                int screenW = Consts.screenWidth;
                int screenH = Consts.screenHeight;
                GLGraphics.setARGBColor(Consts.colorGrid, 0x88);
                GLGraphics.drawRect(
                    (imgEditCenterPixelX - screenW / 2) * zoomLevel + imgEditBfWidth / 2,
                    (imgEditCenterPixelY - screenH / 2) * zoomLevel + imgEditBfHeight / 2,
                    (screenW) * zoomLevel,
                    (screenH) * zoomLevel,false);
                screenW = MAX_OFFSET * 2;
                screenH = MAX_OFFSET * 2;
                //屏幕边框
                GLGraphics.setARGBColor(Consts.colorGrid1, 0x88);
                GLGraphics.drawRect(
                    (imgEditCenterPixelX - screenW / 2) * zoomLevel + imgEditBfWidth / 2,
                    (imgEditCenterPixelY - screenH / 2) * zoomLevel + imgEditBfHeight / 2,
                    (screenW) * zoomLevel,
                    (screenH) * zoomLevel, false);
            }
            if (Consts.showMFrameEdit_Axis)
            {
                GLGraphics.setARGBColor(Consts.colorMFrameEdit_Axis, 0x88);
                GLGraphics.drawLine(
                    imgEditCenterPixelX * zoomLevel + imgEditBfWidth / 2 - (MAX_OFFSET * zoomLevel),
                    imgEditCenterPixelY * zoomLevel + imgEditBfHeight / 2,
                    imgEditCenterPixelX * zoomLevel + imgEditBfWidth / 2 + (MAX_OFFSET * zoomLevel),
                    imgEditCenterPixelY * zoomLevel + imgEditBfHeight / 2,false);
                GLGraphics.drawLine(
                    imgEditCenterPixelX * zoomLevel + imgEditBfWidth / 2,
                    imgEditCenterPixelY * zoomLevel + imgEditBfHeight / 2 - (MAX_OFFSET * zoomLevel),
                    imgEditCenterPixelX * zoomLevel + imgEditBfWidth / 2,
                    imgEditCenterPixelY * zoomLevel + imgEditBfHeight / 2 + (MAX_OFFSET * zoomLevel), false);
            }

            //绘制变形框
            if (TransformNew != null)
            {
                TransformNew.glDisplay(getUICenterX(), getUICenterY(), zoomLevel, Consts.colorAnimSelect);
            }
            //绘制选择框
            if (clipEditMode == EMODE_SELECT_RECT)
            {
                GLGraphics.setRGBColor(Consts.colorAnimSelect);
                GLGraphics.drawLine(pointCursorOld.X, pointCursorOld.Y, pointCursorNew.X, pointCursorOld.Y,false);
                GLGraphics.drawLine(pointCursorOld.X, pointCursorOld.Y, pointCursorOld.X, pointCursorNew.Y, false);
                GLGraphics.drawLine(pointCursorNew.X, pointCursorOld.Y, pointCursorNew.X, pointCursorNew.Y, false);
                GLGraphics.drawLine(pointCursorOld.X, pointCursorNew.Y, pointCursorNew.X, pointCursorNew.Y, false);
            }
            glView.SwapBuffers();
        }
        //设置垂直滚动条数值
        private void setVScroolBarValue(int maxValue, int value)
        {
            noScrollEvent = true;
            if (maxValue > 0 && vScrollBar_FrameEdit.Maximum != maxValue)
            {
                vScrollBar_FrameEdit.Maximum = maxValue;
            }
            if (value >= 0 && value <= vScrollBar_FrameEdit.Maximum - 9 && vScrollBar_FrameEdit.Value != value)
            {
                vScrollBar_FrameEdit.Value = value;
            }
            noScrollEvent = false;
        }
        //设置水平滚动条数值
        private void setHScroolBarValue(int maxValue, int value)
        {
            noScrollEvent = true;
            if (maxValue > 0 && hScrollBar_FrameEdit.Maximum != maxValue)
            {
                hScrollBar_FrameEdit.Maximum = maxValue;
            }
            if (value >= 0 && value <= hScrollBar_FrameEdit.Maximum - 9 && hScrollBar_FrameEdit.Value != value)
            {
                hScrollBar_FrameEdit.Value = value;
            }
            noScrollEvent = false;
        }

        //在编辑区是否点击到某个切片
        private MFrameUnit focousEditClip(int X, int Y)
        {
            MFrameUnit clipTemp = null;
            if (form_MA.form_MTimeLine != null)
            {
                List<MFrame> frameEditList = form_MA.form_MTimeLine.getSameTimeFrames(true,false);
                foreach (MFrame frameEdit in frameEditList)
                {
                    for (int i = frameEdit.Count()-1; i >= 0; i--)
                    {
                        MFrameUnit unit = frameEdit[i];
                        if (unit.isVisible && !unit.isLocked && unit.hitRegion(getUICenterX(), getUICenterY(), zoomLevel, X, Y, Form_MTimeLine.timePosition))
                        {
                            clipTemp = unit;
                            break;
                        }
                    }
                }
            }

            return clipTemp;
        }

        //(帧编辑)拷贝切片集合
        public void copyActionClips()
        {
            bakClipElemnts.Clear();
            if (currentUnits.Count > 0)
            {
                for (int i = 0; i < currentUnits.Count; i++)
                {
                    MFrameUnit newActionClip = currentUnits[i].CloneAtTime();
                    bakClipElemnts.Add(newActionClip);
                }
            }
        }
        //(帧编辑)粘贴动作切片集合
        public void pasteActionClips()
        {
            MFrame frame = form_MA.form_MTimeLine.getEditFrame();
            if (frame == null || bakClipElemnts.Count == 0)
            {
                return;
            }
            //备份检查
            bool checkFailed = false;
            for (int i = 0; i < bakClipElemnts.Count; i++)
            {
                MFrameUnit aciontClip = bakClipElemnts[i];
                if (aciontClip is MFrameUnit_Bitmap)
                {
                    MFrameUnit_Bitmap unitBitmap = (MFrameUnit_Bitmap)aciontClip;
                    if (unitBitmap.clipElement.GetID() < 0)
                    {
                        checkFailed = true;
                        break;
                    }
                }
                else if (aciontClip is MFrameUnit_MC)
                {
                    //...go on
                }
            }
            if (checkFailed)
            {
                return;
            }
            //计算插入
            int insertID = -1;
            bool insert = false;
            if (currentUnits.Count > 0)
            {
                insert = true;
                for (int i = 0; i < currentUnits.Count; i++)
                {
                    MFrameUnit clip = currentUnits[i];
                    if (clip.GetID() > insertID)
                    {
                        insertID = clip.GetID();
                    }
                }
            }
            if (bakClipElemnts.Count > 0)
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                //执行插入
                List<MFrameUnit> v = new List<MFrameUnit>();
                for (int i = 0; i < bakClipElemnts.Count; i++)
                {
                    MFrameUnit aciontClip = bakClipElemnts[i];
                    MFrameUnit newActionClip = null;
                    newActionClip = aciontClip.Clone(frame);
                    if (insert)
                    {
                        frame.Insert(newActionClip, insertID);
                        insertID++;
                    }
                    else
                    {
                        frame.Add(newActionClip);
                    }
                    v.Add(newActionClip);
                }
                //增加历史记录
                form_MA.historyManager.AddHistory(HistoryType.Action);
                //重新选中
                releaseFocusClips();
                for (int i = 0; i < v.Count; i++)
                {
                    currentUnits.Add(v[i]);
                }
                //刷新界面
                UpdateRegion_EditFrame();
                form_MA.form_MFrameLevel.UpdateRegion_FrameLevel();
                rememberCurrentUnitsCoor();
                form_MA.form_MConfig.checkUnitProperty();
                form_MA.form_MTimeLine.updateTLFrameRegion(true);
            }
        }
        //清除剪贴板，避免在撤销时造成错误
        public void ClearClipboard()
        {
            bakClipElemnts.Clear();
        }
        //编辑区鼠标移动
        public byte clipEditMode = 0;//当前切片编辑模式(只针对左键)
        public const byte EMODE_NONE = 0;//无移动
        public const byte EMODE_CANSELECT = EMODE_NONE + 1;//可以点击
        public const byte EMODE_DRAG = EMODE_CANSELECT + 1;//整体拖动
        public const byte EMODE_SELECT_RECT = EMODE_DRAG + 1;//框选
        public const byte EMODE_TRANS_Rotate = EMODE_SELECT_RECT + 1;//变形_旋转
        public const byte EMODE_TRANS_ScaleX = EMODE_TRANS_Rotate + 1;//变形_边缘水平缩放
        public const byte EMODE_TRANS_ScaleY = EMODE_TRANS_ScaleX + 1;//变形_边缘垂直缩放
        public const byte EMODE_TRANS_ScaleLB2RT = EMODE_TRANS_ScaleY + 1;//变形_边缘左下角到右上角缩放
        public const byte EMODE_TRANS_ScaleLT2RB = EMODE_TRANS_ScaleLB2RT + 1;//变形_边缘左上角到右下角缩放
        public const byte EMODE_TRANS_CornerX = EMODE_TRANS_ScaleLT2RB + 1;//变形_拐角水平缩放
        public const byte EMODE_TRANS_CornerY = EMODE_TRANS_CornerX + 1;//变形_拐角垂直缩放
        public const byte EMODE_TRANS_CornerLB2RT = EMODE_TRANS_CornerY + 1;//变形_拐角左下角到右上角缩放
        public const byte EMODE_TRANS_CornerLT2RB = EMODE_TRANS_CornerLB2RT + 1;//变形_拐角左上角到右下角缩放
        //public const byte EMODE_TRANS_SherkX = EMODE_TRANS_CornerLT2RB + 1;//斜切_拐角水平斜切
        //public const byte EMODE_TRANS_SherkY = EMODE_TRANS_SherkX + 1;//斜切_拐角垂直斜切
        //public const byte EMODE_TRANS_SherkLB2RT = EMODE_TRANS_SherkY + 1;//斜切_拐角左下角到右上角斜切
        //public const byte EMODE_TRANS_SherkLT2RB = EMODE_TRANS_SherkLB2RT + 1;//斜切_拐角左上角到右下角斜切
        public const byte EMODE_TRANS_MoveAnchor = EMODE_TRANS_CornerLT2RB + 1;//移动锚点
        public Point pointCursorOld = new Point();//旧鼠标坐标
        public Point pointCursorNew = new Point();//新鼠标坐标
        //缩放处理
        //增加缩放
        public void zoomIn()
        {
            zoom(1);
        }
        public void zoom(int level)
        {
            int tLevel = zoomLevel;
            zoomLevel += level;
            if (zoomLevel > zoomLevelMax)
            {
                zoomLevel = zoomLevelMax;
            }
            if (zoomLevel < zoomLevelMin)
            {
                zoomLevel = zoomLevelMin;
            }
            if (zoomLevel != tLevel)
            {
                UpdateRegion_EditFrame();
            }
        }
        //减小缩放
        public void zoomOut()
        {
            zoom(-1);
        }
        //重置缩放
        public void zoomReset()
        {
            if (zoomLevel != zoomLevelMin)
            {
                zoomLevel = zoomLevelMin;
                UpdateRegion_EditFrame();
            }
        }
        //绘制标尺
        int rulerW = 24;
        private void drawRuler(Graphics g)
        {
            int gap = 20 / zoomLevel;
            if (gap < 1)
            {
                gap = 1;
            }
            int i = 0;
            int y = (imgEditBfHeight / 2) % zoomLevel + zoomLevel;
            while (y < rulerW)
            {
                y += zoomLevel;
            }
            int x = (imgEditBfWidth / 2) % zoomLevel + zoomLevel;
            while (x < rulerW)
            {
                x += zoomLevel;
            }
            GraphicsUtil.fillRect(g, 0, 0, rulerW, imgEditBfHeight, Consts.colorWhite);
            GraphicsUtil.fillRect(g, 0, 0, imgEditBfWidth, rulerW, Consts.colorWhite);
            GraphicsUtil.drawLine(g, rulerW, rulerW, rulerW, imgEditBfHeight, Consts.colorBlack, 1);
            GraphicsUtil.drawLine(g, rulerW, rulerW, imgEditBfWidth, rulerW, Consts.colorBlack, 1);
            while (y < imgEditBfHeight)
            {
                if (i % gap == 0)
                {
                    GraphicsUtil.drawLine(g, 0, y, rulerW, y, Consts.colorBlack, 1);
                    GraphicsUtil.drawString(g, 0, y, i + "", Consts.fontSmall, Consts.colorBlack, Consts.LEFT | Consts.TOP);
                }
                y += zoomLevel;
                i++;
            }

            i = 0;
            while (x < imgEditBfWidth)
            {
                if (i % gap == 0)
                {
                    GraphicsUtil.drawLine(g, x, 0, x, rulerW, Consts.colorBlack, 1);
                    GraphicsUtil.drawString(g, x, 0, i + "", Consts.fontSmall, Consts.colorBlack, Consts.LEFT | Consts.TOP);
                }
                x += zoomLevel;
                i++;
            }
        }
        //(事件响应)编辑区图片框---------------------------------------------------------------
        //编辑区滚轮缩放
        private void glView_MouseWheel(object sender, MouseEventArgs e)
        {
            zoom(-e.Delta / 120);
        }
        //编辑区按键事件
        private void glView_KeyDown(object sender, KeyEventArgs e)
        {
            if (!glView.Cursor.Equals(Cursors.Hand))
            {
                switch (e.KeyValue)
                {
                    case (int)Keys.Up:
                        this.moveCurrentClips(0, -1);
                        UpdateRegion_EditFrame();
                        break;
                    case (int)Keys.Down:
                        this.moveCurrentClips(0, 1);
                        UpdateRegion_EditFrame();
                        break;
                    case (int)Keys.Left:
                        this.moveCurrentClips(-1, 0);
                        UpdateRegion_EditFrame();
                        break;
                    case (int)Keys.Right:
                        this.moveCurrentClips(1, 0);
                        UpdateRegion_EditFrame();
                        break;
                    case (int)Keys.Delete:
                        deleteCurrentUnits();
                        UpdateRegion_EditAndFrameLevel();
                        break;
                    case (int)Keys.C:
                        if (e.Control)
                        {
                            this.copyActionClips();
                        }
                        break;
                    case (int)Keys.V:
                        if (e.Control)
                        {
                            this.pasteActionClips();
                            UpdateRegion_EditAndFrameLevel();
                        }
                        break;
                    case (int)Keys.T:
                        if (e.Control)
                        {
                            ///变形操作
                            if (currentUnits.Count > 0)
                            {
                                generateTransform();
                                rememberCurrentUnitsCoor();
                                rememberUnitsTrans();
                                form_MA.form_MConfig.checkUnitProperty();
                                UpdateRegion_EditFrame();
                            }
                        }
                        break;
                    case (int)Keys.A:
                        if (e.Control)
                        {
                            selectAll(true);
                            form_MA.historyManager.ReadyHistory(HistoryType.Action);
                            rememberCurrentUnitsCoor();
                            UpdateRegion_EditFrame();
                            form_MA.form_MFrameLevel.UpdateRegion_FrameLevel();
                            form_MA.form_MConfig.checkUnitProperty();
                            if (checkAllInSameFrame())
                            {
                                form_MA.form_MTimeLine.updateTLFrameRegion(false);
                            }
                        }
                        break;
                    case (int)Keys.Space:
                        glView.Cursor = handCursor;
                        break;
                }


            }
            else
            {
                bool needRefresh = true;
                switch (e.KeyValue)
                {
                    case (int)Keys.Up:
                        this.imgEditCenterPixelY--;
                        break;
                    case (int)Keys.Down:
                        this.imgEditCenterPixelY++;
                        break;
                    case (int)Keys.Left:
                        this.imgEditCenterPixelX--;
                        break;
                    case (int)Keys.Right:
                        this.imgEditCenterPixelX++;
                        break;
                    default:
                        needRefresh = false;
                        break;
                }
                if (needRefresh)
                {
                    adjustCenterPixel();
                    UpdateRegion_EditFrame();
                }

            }

        }
        //生成变形对象
        public void generateTransform()
        {
            if (currentUnits.Count == 1)
            {
                TransformNew = currentUnits[0].getOpreTransForm();
            }
            else
            {
                List<RectangleF> rects = new List<RectangleF>();
                for (int i = 0; i < currentUnits.Count; i++)
                {
                    rects.Add(currentUnits[i].getTransformBox());
                }
                RectangleF rectAll = MathUtil.getRectsBox(rects);
                TransformNew = new Operation_Transform();
                TransformNew.posX = rectAll.X + rectAll.Width / 2;
                TransformNew.posY = rectAll.Y + rectAll.Height / 2;
                TransformNew.size.Width = rectAll.Width;
                TransformNew.size.Height = rectAll.Height;
                TransformOld = (Operation_Transform)(TransformNew.Clone());
            }
        }


        //编辑区鼠标按下事件
        private void glView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseLeftDownMoved = 0;
            }
            if (!glView.Focused)
            {
                glView.Focus();
            }
            pointCursorNew = new Point(e.X, e.Y);
            if (Consts.moveAlignPixel)
            {
                pointCursorNew.X -= pointCursorNew.X % zoomLevel;
                pointCursorNew.Y -= pointCursorNew.Y % zoomLevel;
            }
            pointCursorOld = pointCursorNew;

            imgEditCenterPixelX_Pre = imgEditCenterPixelX;
            imgEditCenterPixelY_Pre = imgEditCenterPixelY;
            if (e.Button == MouseButtons.Left)
            {
                if (!glView.Cursor.Equals(handCursor))//左键功能
                {
                    if (currentUnits.Count == 0)//未选中集合
                    {
                        MFrameUnit clip = focousEditClip(pointCursorOld.X, pointCursorOld.Y);
                        if (clip != null)
                        {
                            addFocusClips(clip, false);
                            clipEditMode = EMODE_DRAG;
                        }
                        else
                        {
                            releaseFocusClips();
                            clipEditMode = EMODE_SELECT_RECT;
                        }
                        UpdateRegion_EditFrame();
                    }
                    else//已经选中集合
                    {
                        if (TransformNew != null && clipEditMode >= EMODE_TRANS_Rotate)
                        {
                            rememberUnitsTrans();
                        }
                        else
                        {
                            MFrameUnit unitT = focousEditClip(pointCursorOld.X, pointCursorOld.Y);
                            if (unitT == null)
                            {
                                if (ModifierKeys != Keys.Control && ModifierKeys != Keys.Shift)
                                {
                                    releaseFocusClips();
                                    clipEditMode = EMODE_SELECT_RECT;
                                }

                            }
                            else if (!currentUnits.Contains(unitT))
                            {
                                if ((TransformNew == null && ModifierKeys != Keys.Control && ModifierKeys != Keys.Shift) || TransformNew != null)
                                {
                                    releaseFocusClips();
                                }
                                addFocusClips(unitT, false);
                                clipEditMode = EMODE_DRAG;
                            }
                            else
                            {
                                if (TransformNew == null)
                                {
                                    if (ModifierKeys == Keys.Control)
                                    {
                                        currentUnits.Remove(unitT);
                                        form_MA.form_MFrameLevel.updateLevelEye();
                                        form_MA.form_MFrameLevel.updateLevelLock();
                                    }
                                }
                                clipEditMode = EMODE_DRAG;
                            }
                        }
                        rememberCurrentUnitsCoor();
                        UpdateRegion_EditFrame();
                    }
                    form_MA.form_MConfig.checkUnitProperty();
                    if (checkAllInSameFrame())
                    {
                        form_MA.form_MTimeLine.updateTLFrameRegion(false);
                        form_MA.form_MTimeLine.updateTLNaviRegion();
                    }
                    form_MA.form_MFrameLevel.UpdateRegion_FrameLevel();
                    form_MA.historyManager.ReadyHistory(HistoryType.Action);
                }
                else
                {
 
                }

            }
        }
        //编辑区鼠标弹起事件
        private void glView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (!glView.Cursor.Equals(handCursor))//当前处于编辑切片状态
                {
                    int lastClipMode = clipEditMode;
                    clipEditMode = EMODE_NONE;
                    if (lastClipMode == EMODE_DRAG || MathUtil.inRegionClose(lastClipMode, EMODE_TRANS_Rotate, EMODE_TRANS_MoveAnchor))
                    {
                        if (MouseLeftDownMoved > 0 && currentUnits.Count > 0)
                        {
                            form_MA.historyManager.AddHistory(HistoryType.Action);
                        }
                    }
                    if (lastClipMode == EMODE_DRAG)
                    {
                        rememberCurrentUnitsCoor();
                    }
                    else if (lastClipMode == EMODE_SELECT_RECT)
                    {
                        int cursorx = Math.Min(pointCursorOld.X, pointCursorNew.X);
                        int cursory = Math.Min(pointCursorOld.Y, pointCursorNew.Y);
                        int cursorw = Math.Abs(pointCursorOld.X - pointCursorNew.X);
                        int cursorh = Math.Abs(pointCursorOld.Y - pointCursorNew.Y);
                        //int clipx, clipy, clipw, cliph;
                        MFrameUnit unit = null;
                        this.releaseFocusClips();
                        if (form_MA.form_MTimeLine != null)
                        {
                            List<MFrame> frameEditList = form_MA.form_MTimeLine.getSameTimeFrames(true,false);
                            foreach (MFrame frameEdit in frameEditList)
                            {
                                for (int i = 0; i < frameEdit.Count(); i++)
                                {
                                    unit = frameEdit[i];
                                    if (unit.isVisible && !unit.isLocked && unit.hitRegion(getUICenterX(), getUICenterY(), zoomLevel, new Rectangle(cursorx, cursory, cursorw, cursorh), Form_MTimeLine.timePosition))
                                    {
                                        currentUnits.Add(unit);
                                    }

                                }
                            }
                            form_MA.showInfor("共选中了" + currentUnits.Count + "个单元");
                        }
                        form_MA.historyManager.ReadyHistory(HistoryType.Action);
                        rememberCurrentUnitsCoor();
                        this.UpdateRegion_EditFrame();
                        if (checkAllInSameFrame())
                        {
                            form_MA.form_MTimeLine.updateTLFrameRegion(false);
                        }
                        form_MA.form_MFrameLevel.updateLevelEye();
                        form_MA.form_MFrameLevel.updateLevelLock();
                        form_MA.form_MFrameLevel.UpdateRegion_FrameLevel();
                        form_MA.form_MConfig.checkUnitProperty();
                    }
                }
            }
            pointCursorOld = new Point(e.X, glView.Height - e.Y);
            MouseLeftDownMoved = -1;
        }
        //编辑区鼠标移入事件
        private void glView_MouseEnter(object sender, EventArgs e)
        {
            if (!form_MA.Equals(Form.ActiveForm))
            {
                return;
            }
            glView.Focus();
            glView.Cursor = cursor_unitSelect;
            Form_MAnimation.transCmdKey = true;
        }
        //编辑区鼠标移动事件
        private void glView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mPoint = new Point(e.X, e.Y);
            pointCursorNew = mPoint;
            //Console.WriteLine("glView_MouseMove:" + pointCursorNew.X + "," + pointCursorNew.Y);
            if (!glView.Cursor.Equals(handCursor))//当前处于编辑切片状态
            {
                if (Consts.moveAlignPixel)
                {
                    pointCursorNew.X -= pointCursorNew.X % zoomLevel;
                    pointCursorNew.Y -= pointCursorNew.Y % zoomLevel;
                }
                if (e.Button.Equals(MouseButtons.Left))//正在编辑中
                {
                    if (Math.Abs(pointCursorNew.X - pointCursorOld.X) >= zoomLevel || Math.Abs(pointCursorNew.Y - pointCursorOld.Y) >= zoomLevel)
                    {
                        MouseLeftDownMoved = 1;
                        //查看是否是中间帧操作
                        checkTransitFrame();
                        //执行操作
                        switch (clipEditMode)
                        {
                            case EMODE_DRAG:
                                Transform_MoveAllAtNotTrans(pointCursorNew.X - pointCursorOld.X, pointCursorNew.Y - pointCursorOld.Y);
                                form_MA.form_MConfig.checkUnitProperty();
                                break;
                            case EMODE_TRANS_Rotate:
                                Transform_RotateAll(pointCursorOld, pointCursorNew);
                                break;
                            case EMODE_TRANS_ScaleX:
                            case EMODE_TRANS_ScaleY:
                            case EMODE_TRANS_ScaleLB2RT:
                            case EMODE_TRANS_ScaleLT2RB:
                                Transform_ScaleBorder(pointCursorOld, pointCursorNew);
                                break;
                            case EMODE_TRANS_CornerX:
                            case EMODE_TRANS_CornerY:
                            case EMODE_TRANS_CornerLB2RT:
                            case EMODE_TRANS_CornerLT2RB:
                                Transform_ScaleCorner(pointCursorOld, pointCursorNew);
                                break;
                            //case EMODE_TRANS_SherkX:
                            //case EMODE_TRANS_SherkY:
                            //case EMODE_TRANS_SherkLB2RT:
                            //case EMODE_TRANS_SherkLT2RB:
                            //    Transform_ShearBorder(pointCursorOld, pointCursorNew);
                            //    break;
                            case EMODE_TRANS_MoveAnchor:
                                Transform_MoveAnchor(pointCursorOld, pointCursorNew);
                                break;
                        }
                        UpdateRegion_EditAndFrameLevel();
                    }
                }
                //查找可编辑状态
                else
                {
                    clipEditMode = EMODE_NONE;
                    if (form_MA.form_MTimeLine != null)
                    {
                        List<MFrame> frameEditList = form_MA.form_MTimeLine.getSameTimeFrames(true,false);
                        foreach (MFrame frameEdit in frameEditList)
                        {
                            //寻找变形方式
                            if (TransformNew != null)
                            {
                                PointF pCursor = mPoint;
                                PointF[] pList = TransformNew.pBorderCorner;
                                int len = pList.Length;
                                PointF pCenter = new PointF((pList[0].X + pList[2].X) / 2, (pList[0].Y + pList[2].Y) / 2);
                                //移动锚点
                                if (clipEditMode == EMODE_NONE)
                                {
                                    PointF position = TransformNew.getPosition(getUICenterX(), getUICenterY(), zoomLevel);
                                    if (MathUtil.getDistance_2Points(position, pCursor) < 11.0f)
                                    {
                                        clipEditMode = EMODE_TRANS_MoveAnchor;
                                    }
                                }
                                //四角缩放或者旋转
                                if (clipEditMode == EMODE_NONE)
                                {
                                    for (int i = 0; i < len; i++)
                                    {
                                        int iPre = (i - 1 + len) % len;
                                        int iNxt = (i + 1) % len;
                                        if (MathUtil.pointClose(pList[i], mPoint, 10.0f))
                                        {
                                            if (MathUtil.inRayAngle(pCursor, pList[i], pList[iPre], pList[iNxt]))//缩放
                                            {
                                                transBoderPID_0 = i;
                                                transBoderPID_1 = iNxt;
                                                float anle0 = MathUtil.getPointAngle(pList[i], pList[iPre]);
                                                float anle1 = MathUtil.getPointAngle(pList[i], pList[iNxt]);
                                                float anle = MathUtil.gapMiddleDegree(anle0, anle1);
                                                if (MathUtil.inRegionClose(anle, (float)(Math.PI * 0 / 8), (float)(Math.PI * 1 / 8)) ||
                                                    MathUtil.inRegionClose(anle, (float)(Math.PI * 15 / 8), (float)(Math.PI * 16 / 8)) ||
                                                    MathUtil.inRegionClose(anle, (float)(Math.PI * 7 / 8), (float)(Math.PI * 9 / 8)))
                                                {
                                                    clipEditMode = EMODE_TRANS_CornerX;
                                                }
                                                else if (MathUtil.inRegionClose(anle, (float)(Math.PI * 1 / 8), (float)(Math.PI * 3 / 8)) ||
                                                    MathUtil.inRegionClose(anle, (float)(Math.PI * 9 / 8), (float)(Math.PI * 11 / 8)))
                                                {
                                                    clipEditMode = EMODE_TRANS_CornerLB2RT;
                                                }
                                                else if (MathUtil.inRegionClose(anle, (float)(Math.PI * 3 / 8), (float)(Math.PI * 5 / 8)) ||
                                                    MathUtil.inRegionClose(anle, (float)(Math.PI * 11 / 8), (float)(Math.PI * 13 / 8)))
                                                {
                                                    clipEditMode = EMODE_TRANS_CornerY;
                                                }
                                                else if (MathUtil.inRegionClose(anle, (float)(Math.PI * 5 / 8), (float)(Math.PI * 7 / 8)) ||
                                                    MathUtil.inRegionClose(anle, (float)(Math.PI * 13 / 8), (float)(Math.PI * 15 / 8)))
                                                {
                                                    clipEditMode = EMODE_TRANS_CornerLT2RB;
                                                }
                                            }
                                            else//旋转
                                            {
                                                clipEditMode = EMODE_TRANS_Rotate;
                                            }
                                            break;
                                        }
                                    }
                                }
                                //中点缩放
                                if (clipEditMode == EMODE_NONE)
                                {
                                    pList = TransformNew.pBorderCenter;
                                    len = pList.Length;
                                    for (int i = 0; i < len; i++)
                                    {
                                        int iPre = (i - 1 + len) % len;
                                        int iNxt = (i + 1) % len;
                                        if (MathUtil.pointClose(pList[i], pCursor, 10.0f))
                                        {
                                            transBoderPID_0 = i;
                                            transBoderPID_1 = iNxt;
                                            float anle = MathUtil.getPointAngle(pCenter, pList[i]);
                                            if (MathUtil.inRegionClose(anle, (float)(Math.PI * 0 / 8), (float)(Math.PI * 1 / 8)) ||
                                                MathUtil.inRegionClose(anle, (float)(Math.PI * 15 / 8), (float)(Math.PI * 16 / 8)) ||
                                                MathUtil.inRegionClose(anle, (float)(Math.PI * 7 / 8), (float)(Math.PI * 9 / 8)))
                                            {
                                                clipEditMode = EMODE_TRANS_ScaleX;
                                            }
                                            else if (MathUtil.inRegionClose(anle, (float)(Math.PI * 1 / 8), (float)(Math.PI * 3 / 8)) ||
                                                MathUtil.inRegionClose(anle, (float)(Math.PI * 9 / 8), (float)(Math.PI * 11 / 8)))
                                            {
                                                clipEditMode = EMODE_TRANS_ScaleLB2RT;
                                            }
                                            else if (MathUtil.inRegionClose(anle, (float)(Math.PI * 3 / 8), (float)(Math.PI * 5 / 8)) ||
                                                MathUtil.inRegionClose(anle, (float)(Math.PI * 11 / 8), (float)(Math.PI * 13 / 8)))
                                            {
                                                clipEditMode = EMODE_TRANS_ScaleY;
                                            }
                                            else if (MathUtil.inRegionClose(anle, (float)(Math.PI * 5 / 8), (float)(Math.PI * 7 / 8)) ||
                                                MathUtil.inRegionClose(anle, (float)(Math.PI * 13 / 8), (float)(Math.PI * 15 / 8)))
                                            {
                                                clipEditMode = EMODE_TRANS_ScaleLT2RB;
                                            }
                                            break;
                                        }
                                    }
                                }
                                //四边斜切变形
                                //if (clipEditMode == EMODE_NONE)
                                //{
                                    //pList = TransformNew.pBorderCorner;
                                    //len = pList.Length;
                                    //for (int i = 0; i < len; i++)
                                    //{
                                    //    int iPre = (i - 1 + len) % len;
                                    //    int iNxt = (i + 1) % len;
                                    //    if (MathUtil.getDistance_PointToSegment(pCursor, pList[i], pList[iNxt]) < 6.0f)
                                    //    {
                                    //        transBoderPID_0 = i;
                                    //        transBoderPID_1 = iNxt;
                                    //        float di = MathUtil.getDistance_PointToSegment(pCursor, pList[i], pList[iNxt]);
                                    //        float anle = MathUtil.getPointAngle(pList[i], pList[iNxt]);
                                    //        if (MathUtil.inRegionClose(anle, (float)(Math.PI * 0 / 8), (float)(Math.PI * 1 / 8)) ||
                                    //            MathUtil.inRegionClose(anle, (float)(Math.PI * 15 / 8), (float)(Math.PI * 16 / 8)) ||
                                    //            MathUtil.inRegionClose(anle, (float)(Math.PI * 7 / 8), (float)(Math.PI * 9 / 8)))
                                    //        {
                                    //            clipEditMode = EMODE_TRANS_SherkX;
                                    //        }
                                    //        else if (MathUtil.inRegionClose(anle, (float)(Math.PI * 1 / 8), (float)(Math.PI * 3 / 8)) ||
                                    //            MathUtil.inRegionClose(anle, (float)(Math.PI * 9 / 8), (float)(Math.PI * 11 / 8)))
                                    //        {
                                    //            clipEditMode = EMODE_TRANS_SherkLB2RT;
                                    //        }
                                    //        else if (MathUtil.inRegionClose(anle, (float)(Math.PI * 3 / 8), (float)(Math.PI * 5 / 8)) ||
                                    //            MathUtil.inRegionClose(anle, (float)(Math.PI * 11 / 8), (float)(Math.PI * 13 / 8)))
                                    //        {
                                    //            clipEditMode = EMODE_TRANS_SherkY;
                                    //        }
                                    //        else if (MathUtil.inRegionClose(anle, (float)(Math.PI * 5 / 8), (float)(Math.PI * 7 / 8)) ||
                                    //            MathUtil.inRegionClose(anle, (float)(Math.PI * 13 / 8), (float)(Math.PI * 15 / 8)))
                                    //        {
                                    //            clipEditMode = EMODE_TRANS_SherkLT2RB;
                                    //        }
                                    //        break;
                                    //    }
                                    //}
                                //}
                            }
                            //寻找可选对象
                            if (clipEditMode == EMODE_NONE)
                            {
                                MFrameUnit clipElement = null;
                                for (int i = 0; i < frameEdit.Count(); i++)
                                {
                                    if (frameEdit[i] != null)
                                    {
                                        clipElement = frameEdit[i];
                                        if (clipElement.hitRegion(getUICenterX(), getUICenterY(), zoomLevel, mPoint.X, mPoint.Y, Form_MTimeLine.timePosition))
                                        {
                                            clipEditMode = EMODE_CANSELECT;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    int centerX = getUICenterX();
                    int centerY = getUICenterY();
                    if (currentUnits.Count == 0)
                    {
                        form_MA.showInfor("鼠标偏移坐标:[" + ((pointCursorNew.X - centerX) / zoomLevel) + "," + ((pointCursorNew.Y - centerY) / zoomLevel) + "]，" +
                                  "鼠标屏幕坐标:[" + ((pointCursorNew.X - centerX) / zoomLevel + Consts.screenWidth / 2) + "," + ((pointCursorNew.Y - centerY) / zoomLevel + Consts.screenHeight / 2) + "]");
                    }

                }
                //设置鼠标状态
                if (clipEditMode == EMODE_CANSELECT)
                {
                    glView.Cursor = cursor_unitMove;
                }
                else if (clipEditMode == EMODE_NONE)
                {
                    glView.Cursor = cursor_unitSelect;
                }
                else if (clipEditMode >= EMODE_TRANS_Rotate)
                {
                    glView.Cursor = transCursors[clipEditMode - EMODE_TRANS_Rotate];
                }

            }
            else //当前处于拖动窗口状态，按下左键拖动窗口
            {
                if (e.Button.Equals(MouseButtons.Left))//按下左键拖动窗口
                {
                    //Console.WriteLine("moving window:" + pointCursorNew.X + "," + pointCursorNew.Y);
                    imgEditCenterPixelX = imgEditCenterPixelX_Pre + (pointCursorNew.X - pointCursorOld.X) / zoomLevel;
                    imgEditCenterPixelY = imgEditCenterPixelY_Pre + (pointCursorNew.Y - pointCursorOld.Y) / zoomLevel;
                    adjustCenterPixel();
                    UpdateRegion_EditFrame();
                }

            }
        }
        //检测选择单元是否位于过渡帧上，如果有，则将相应的过渡帧转换为关键帧
        public void checkTransitFrame()
        {
            List<MFrame> copyCreatedFrames = new List<MFrame>();
            int timePos = Form_MTimeLine.timePosition;
            int motionFrameCount = 0;
            for (int i = 0; i < currentUnits.Count; i++)
            {
                MFrameUnit unitI = currentUnits[i];
                MFrame frameI = unitI.parent;
                bool needReplace = false;
                if (copyCreatedFrames.Contains(frameI))//已经创建过关键帧
                {
                    needReplace = true;
                }
                else if (frameI.hasMotion && frameI.timeBegin < timePos && frameI.timeBegin + frameI.timeLast > timePos)
                {
                    //插入关键帧
                    MTimeLine timeLine = frameI.GetParent();
                    timeLine.insertKeyFrame(timePos);
                    copyCreatedFrames.Add(frameI);
                    motionFrameCount++;
                    needReplace = true;
                }
                if (needReplace)
                {
                    //获取新帧
                    MFrame newFrame = frameI.GetNext();
                    //替换帧选择项目
                    currentUnits[i] = newFrame[currentUnits[i].GetID()];
                }
            }
            if (motionFrameCount > 0)
            {
                form_MA.form_MTimeLine.reseFocusFrame();
                form_MA.form_MTimeLine.clearFrameFocus();
                form_MA.form_MTimeLine.updateTLFrameRegion();
                form_MA.form_MTimeLine.updateTLRulerRegion();
                this.rememberCurrentUnitsCoor();
                this.rememberUnitsTrans();
            }
        }
        //编辑区 水平滚动条
        private void hScrollBar_FrameEdit_ValueChanged(object sender, EventArgs e)
        {
            if (noScrollEvent)
            {
                return;
            }
            imgEditCenterPixelX = MAX_OFFSET - hScrollBar_FrameEdit.Value;
            UpdateRegion_EditFrame();
        }
        //编辑区 垂直滚动条
        private void vScrollBar_FrameEdit_ValueChanged(object sender, EventArgs e)
        {
            if (noScrollEvent)
            {
                return;
            }
            imgEditCenterPixelY = MAX_OFFSET - vScrollBar_FrameEdit.Value;
            UpdateRegion_EditFrame();
        }
        private void Form_AnimationEditor_SizeChanged(object sender, EventArgs e)
        {
            if (Form_MAnimation.inResetPanels)
            {
                return;
            }
            UpdateRegion_EditAndFrameLevel();
        }

        //增加导入切片(当点在当前编辑区内时)
        public void addClips(Point editPointSceen, List<MClipElement> baseClipElments)
        {
            Point editPointClient = glView.PointToClient(editPointSceen);
            editPointClient.Y = editPointClient.Y;//glView.Height - 
            if (editPointClient.X >= 0 && editPointClient.Y >= 0 && editPointClient.X < glView.Width && editPointClient.Y < glView.Height)
            {
                MFrame frameEdit=form_MA.form_MTimeLine.getEditFrame();
                if (frameEdit!=null)
                {
                    if (baseClipElments.Count >0)
                    {
                        form_MA.historyManager.ReadyHistory(HistoryType.Action);
                        int xMin = baseClipElments[0].clipRect.X;
                        int xMax = baseClipElments[0].clipRect.X + baseClipElments[0].clipRect.Width;
                        int yMin = baseClipElments[0].clipRect.Y;
                        int yMax = baseClipElments[0].clipRect.Y + baseClipElments[0].clipRect.Height;
                        for (int i = 1; i < baseClipElments.Count;i++ )
                        {
                            MClipElement clip=baseClipElments[i];
                            if (xMin > clip.clipRect.X)
                            {
                                xMin = clip.clipRect.X;
                            }
                            if (xMax < clip.clipRect.X + clip.clipRect.Width)
                            {
                                xMax = clip.clipRect.X + clip.clipRect.Width;
                            }
                            if (yMin > clip.clipRect.Y)
                            {
                                yMin = clip.clipRect.Y;
                            }
                            if (yMax < clip.clipRect.Y + clip.clipRect.Height)
                            {
                                yMax = clip.clipRect.Y + clip.clipRect.Height;
                            }
                        }
                        int widthAll = xMax - xMin;
                        int heightAll = yMax - yMin;
                        int xAll = (xMax + xMin )/ 2;
                        int yAll = (yMax + yMin) / 2;
                        foreach (MClipElement bClip in baseClipElments)
                        {
                            MFrameUnit_Bitmap fUnit = new MFrameUnit_Bitmap();
                            fUnit.SetParent(frameEdit);
                            int offX = (bClip.clipRect.X + bClip.clipRect.Width / 2 - xAll) + (editPointClient.X - (getUICenterX())) / zoomLevel;
                            int offY = -((bClip.clipRect.Y + bClip.clipRect.Height / 2) - yAll) + (editPointClient.Y - (getUICenterY())) / zoomLevel;
                            fUnit.posX = offX;
                            fUnit.posY = offY;
                            fUnit.clipElement = bClip;
                            frameEdit.Add(fUnit);
                        }
                        form_MA.historyManager.AddHistory(HistoryType.Action);
                        this.UpdateRegion_EditAndFrameLevel();
                        form_MA.form_MTimeLine.updateTLFrameRegion();
                    }
                }
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys e)
        {
            if ((e == Keys.Up || e == Keys.Down || e == Keys.Left || e == Keys.Right)
                && (glView.Focused))
            {
                NativeMethods.SendMessage(glView.Handle, msg.Msg, (uint)msg.WParam, (uint)msg.LParam);
                msg.Result = (IntPtr)1;
                return true;
            }
            base.ProcessCmdKey(ref msg, e);
            return false;
        }
        //校正画布中心
        private void adjustCenterPixel()
        {
            if (imgEditCenterPixelX < -MAX_OFFSET)
            {
                imgEditCenterPixelX = -MAX_OFFSET;
            }
            if (imgEditCenterPixelX > MAX_OFFSET)
            {
                imgEditCenterPixelX = MAX_OFFSET;
            }
            if (imgEditCenterPixelY < -MAX_OFFSET)
            {
                imgEditCenterPixelY = -MAX_OFFSET;
            }
            if (imgEditCenterPixelY > MAX_OFFSET)
            {
                imgEditCenterPixelY = MAX_OFFSET;
            }
        }

        private void Form_MFrameEdit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!glView.Cursor.Equals(cursor_unitSelect))
                {
                    glView.Cursor = cursor_unitSelect;
                }
            }
        }

        private void glView_MouseLeave(object sender, EventArgs e)
        {
            Form_MAnimation.transCmdKey = false;
        }
        //帧单元操作--->基于记录的坐标，整体移动当前焦点切片集(并且自动移动到合适的位置)
        private void Transform_MoveAllAtNotTrans(float xOffset, float yOffset)
        {
            if (currentUnits == null || currentUnits.Count == 0 || (xOffset == 0 && yOffset == 0))
            {
                return;
            }
            PointF position;
            float xT, yT;
            float xMax = xOffset / zoomLevel;
            float yMax = yOffset / zoomLevel;
            float temp;
            for (int i = 0; i < currentUnits.Count; i++)
            {
                if (currentUnits[i] != null)
                {
                    position = currentUnitsPositions[i];
                    xT = position.X + xOffset / zoomLevel;
                    yT = position.Y + yOffset / zoomLevel;
                    if (xT < -MAX_OFFSET)
                    {
                        temp = -MAX_OFFSET - position.X;
                        if (Math.Abs(temp) <= Math.Abs(xMax))
                        {
                            xMax = temp;
                        }
                    }
                    if (xT > MAX_OFFSET)
                    {
                        temp = MAX_OFFSET - position.X;
                        if (Math.Abs(temp) <= Math.Abs(xMax))
                        {
                            xMax = temp;
                        }
                    }
                    if (yT < -MAX_OFFSET)
                    {
                        temp = -MAX_OFFSET - position.Y;
                        if (Math.Abs(temp) <= Math.Abs(yMax))
                        {
                            yMax = temp;
                        }
                    }
                    if (yT > MAX_OFFSET)
                    {
                        temp = MAX_OFFSET - position.Y;
                        if (Math.Abs(temp) <= Math.Abs(yMax))
                        {
                            yMax = temp;
                        }
                    }
                }
            }
            MFrameUnit_Bitmap clipElement;
            for (int i = 0; i < currentUnits.Count; i++)
            {
                if (currentUnits[i] != null)
                {
                    position = currentUnitsPositions[i];
                    clipElement = (MFrameUnit_Bitmap)currentUnits[i];
                    clipElement.posX = position.X + xMax;
                    clipElement.posY = position.Y + yMax;
                }
            }
            if (TransformNew != null)
            {
                TransformNew.posX = TransformOld.posX + xMax;
                TransformNew.posY = TransformOld.posY + yMax;
            }
        }
        //帧单元操作--->基于记录的帧单元集合，旋转当前切片(最后需要校正每个帧单元位置...?)
        static Matrix mtrixTemp0 = new Matrix();
        private void Transform_RotateAll(Point pOld, Point pNew)
        {
            float degreeChanged = MathUtil.getAngle_3Points(pOld, pNew, TransformNew.getCenter(getUICenterX(), getUICenterY(), zoomLevel));
            Transform_RotateAll(degreeChanged);
            form_MA.form_MConfig.checkUnitProperty();
        }
        private void Transform_RotateAll(float degreeChanged)
        {
            TransformNew.rotateDegree = TransformOld.rotateDegree + degreeChanged;
            if (currentUnits.Count == 1)
            {
                currentUnitsTransNew[0].rotateDegree = currentUnitsTransOld[0].rotateDegree + degreeChanged;
            }
            else
            {
                Vector2 vPos = new Vector2();
                for (int i = 0; i < currentUnitsTransNew.Count; i++)
                {
                    currentUnitsTransNew[i].rotateDegree = currentUnitsTransOld[i].rotateDegree + degreeChanged;
                    PointF transformPos = TransformNew.getPosition(getUICenterX(), getUICenterY(), zoomLevel);
                    vPos.setValue(transformPos, currentUnitsTransOld[i].getPosition(getUICenterX(), getUICenterY(), zoomLevel));
                    mtrixTemp0.Reset();
                    mtrixTemp0.Rotate(degreeChanged);
                    PointF[] positionNewVector = new PointF[1];
                    positionNewVector[0] = new PointF(vPos.x, vPos.y);
                    mtrixTemp0.TransformPoints(positionNewVector);
                    PointF positionDest = new PointF(positionNewVector[0].X + transformPos.X, positionNewVector[0].Y + transformPos.Y);
                    currentUnitsTransNew[i].setPosition(positionDest, getUICenterX(), getUICenterY(), zoomLevel);
                }
            }
            applyUnitsTrans();
            rememberCurrentUnitsCoor(false);
            UpdateRegion_EditAndFrameLevel();
        }
        //帧单元操作--->基于记录的帧单元集合，选中边缘中心后，拖动以沿着边缘径向缩放当前帧单元(bug)
        private int transBoderPID_0, transBoderPID_1;
        private void Transform_ScaleBorder(Point pOld, Point pNew)
        {
            PointF pTransBorder0 = TransformOld.pBorderCorner[transBoderPID_0];//边端点0
            PointF pTransBorder1 = TransformOld.pBorderCorner[transBoderPID_1];//边端点1
            PointF pTransBC = new PointF((pTransBorder0.X + pTransBorder1.X) / 2, (pTransBorder0.Y + pTransBorder1.Y) / 2);//边中点
            PointF pNewReal = new PointF(pNew.X - (pOld.X - pTransBC.X), pNew.Y - (pOld.Y - pTransBC.Y));//实际新位置
            PointF pTCenter = TransformOld.getCenter(getUICenterX(), getUICenterY(), zoomLevel);//原变形对象中心
            float disOld = MathUtil.getDistance_PointToLine(pTCenter, pTransBorder0, pTransBorder1);//原先距离
            float disNew = MathUtil.getDistance_PointToLine(pNewReal, pTransBorder0, pTransBorder1);//新的距离
            if (MathUtil.segmentCossLine(pTCenter, pNewReal, pTransBorder0, pTransBorder1))
            {
                disNew = disOld + disNew;
            }
            else
            {
                disNew = disOld - disNew;
            }
            //if (Math.Abs(disNew - disOld) > 5)
            //{
            //    Console.WriteLine("disNew:" + disNew);
            //    Console.WriteLine("disOld:" + disOld);
            //}
            float scaleNew = disNew / disOld;
            Transform_ScaleBorder(scaleNew, transBoderPID_0 % 2 == 0);
            form_MA.form_MConfig.checkUnitProperty();
        }

        private void Transform_ScaleBorder(float scaleNew,bool xOry)
        {
            float scaleChanged = scaleNew - 1.0f;
            //Console.WriteLine("scaleChanged:" + scaleChanged);
            if (xOry)
            {
                TransformNew.scaleX = TransformOld.scaleX * scaleNew;
                if (currentUnits.Count == 1)
                {
                    currentUnitsTransNew[0].scaleX = currentUnitsTransOld[0].scaleX * scaleNew;
                }
                else
                {
                    //根据任意方向进行的缩放重新计算所有参数S
                    for (int i = 0; i < currentUnitsTransNew.Count; i++)
                    {
                        //位置变化
                        float angleGap = TransformOld.rotateDegree;
                        PointF pTP = TransformOld.getPosition();
                        PointF pMP = currentUnitsTransOld[i].getPosition();
                        PointF v = new PointF(pMP.X - pTP.X, pMP.Y - pTP.Y);//从变形对象位置到模块位置的向量
                        PointF pTC = TransformOld.getCenter();//getUICenterX(), getUICenterY(), zoomLevel
                        PointF pTX = new PointF(
                            TransformOld.pBorderCenter[2].X - pTC.X,
                            TransformOld.pBorderCenter[2].Y - pTC.Y);//变形对象+X轴
                        PointF vScale = MathUtil.getPointProject(v, pTX);//v到变形对象+X轴投影(变形矢量)
                        v.X += vScale.X * scaleChanged;
                        v.Y += vScale.Y * scaleChanged;
                        currentUnitsTransNew[i].posX = v.X + pTP.X;
                        currentUnitsTransNew[i].posY = v.Y + pTP.Y;
                        //缩放变化
                        float pTXLen = (float)Math.Sqrt(pTX.X * pTX.X + pTX.Y * pTX.Y);
                        PointF pScaleTX = new PointF(pTX.X / pTXLen, pTX.Y / pTXLen);//变形对象+X轴单位向量
                        PointF pMX = new PointF(
                            currentUnitsTransOld[i].pBorderCenter[2].X - currentUnitsTransOld[i].posX,
                            currentUnitsTransOld[i].pBorderCenter[2].Y - currentUnitsTransOld[i].posY);//模块对象+X轴
                        PointF pMY = new PointF(
                            currentUnitsTransOld[i].pBorderCenter[3].X - currentUnitsTransOld[i].posX,
                            currentUnitsTransOld[i].pBorderCenter[3].Y - currentUnitsTransOld[i].posY);//模块对象+Y轴
                        float sizeScaleX = scaleChanged * Math.Abs(MathUtil.getPointProjectSize(pScaleTX, pMX));
                        float sizeScaleY = scaleChanged * Math.Abs(MathUtil.getPointProjectSize(pScaleTX, pMY));
                        currentUnitsTransNew[i].scaleX = currentUnitsTransOld[i].scaleX * (1 + sizeScaleX);
                        currentUnitsTransNew[i].scaleY = currentUnitsTransOld[i].scaleY * (1 + sizeScaleY);
                    }
                }
            }
            else
            {
                TransformNew.scaleY = TransformOld.scaleY * scaleNew;
                if (currentUnits.Count == 1)
                {
                    currentUnitsTransNew[0].scaleY = currentUnitsTransOld[0].scaleY * scaleNew;
                }
                else
                {
                    //根据任意方向进行的缩放重新计算所有参数S
                    for (int i = 0; i < currentUnitsTransNew.Count; i++)
                    {
                        //位置变化
                        float angleGap = TransformOld.rotateDegree;
                        PointF pTP = TransformOld.getPosition();
                        PointF pMP = currentUnitsTransOld[i].getPosition();
                        PointF v = new PointF(pMP.X - pTP.X, pMP.Y - pTP.Y);//从变形对象位置到模块位置的向量
                        PointF pTC = TransformOld.getCenter();//getUICenterX(), getUICenterY(), zoomLevel
                        PointF pTY = new PointF(
                            TransformOld.pBorderCenter[1].X - pTC.X,
                            TransformOld.pBorderCenter[1].Y - pTC.Y);//变形对象+Y轴
                        PointF vScale = MathUtil.getPointProject(v, pTY);//v到变形对象+Y轴投影(变形矢量)
                        v.X += vScale.X * scaleChanged;
                        v.Y += vScale.Y * scaleChanged;
                        currentUnitsTransNew[i].posX = v.X + pTP.X;
                        currentUnitsTransNew[i].posY = v.Y + pTP.Y;
                        //缩放变化
                        float pTXLen = (float)Math.Sqrt(pTY.X * pTY.X + pTY.Y * pTY.Y);
                        PointF pScaleTX = new PointF(pTY.X / pTXLen, pTY.Y / pTXLen);//变形对象+Y轴单位向量
                        PointF pMX = new PointF(
                            currentUnitsTransOld[i].pBorderCenter[2].X - currentUnitsTransOld[i].posX,
                            currentUnitsTransOld[i].pBorderCenter[2].Y - currentUnitsTransOld[i].posY);//模块对象+X轴
                        PointF pMY = new PointF(
                            currentUnitsTransOld[i].pBorderCenter[3].X - currentUnitsTransOld[i].posX,
                            currentUnitsTransOld[i].pBorderCenter[3].Y - currentUnitsTransOld[i].posY);//模块对象+Y轴
                        float sizeScaleX = scaleChanged * Math.Abs(MathUtil.getPointProjectSize(pScaleTX, pMX));
                        float sizeScaleY = scaleChanged * Math.Abs(MathUtil.getPointProjectSize(pScaleTX, pMY));
                        currentUnitsTransNew[i].scaleX = currentUnitsTransOld[i].scaleX * (1 + sizeScaleX);
                        currentUnitsTransNew[i].scaleY = currentUnitsTransOld[i].scaleY * (1 + sizeScaleY);
                    }
                }
            }
            applyUnitsTrans();
            this.UpdateRegion_EditAndFrameLevel();
        }
        //帧单元操作--->基于记录的帧单元集合，选中对角顶点后，拖动以缩放当前帧单元(最后需要校正每个帧单元位置...?)
        private void Transform_ScaleCorner(Point pOld, Point pNew)
        {
            PointF pTransCorner = TransformOld.pBorderCorner[transBoderPID_0];//对角顶点
            PointF pNewReal = new PointF(pNew.X - (pOld.X - pTransCorner.X), pNew.Y - (pOld.Y - pTransCorner.Y));//实际新位置
            PointF pTCenter = TransformOld.getCenter(getUICenterX(), getUICenterY(), zoomLevel);//原变形对象中心
            float disOld = MathUtil.getDistance_2Points(pTCenter, pTransCorner);//原先对角线长度
            float disNew = MathUtil.getProjection_2Vector(pOld, pNew, pTCenter, pTransCorner);//长度变化
            disNew = disOld + disNew;
            float scaleNew = disNew / disOld;
            float scaleChanged = scaleNew - 1.0f;
            TransformNew.scaleX = TransformOld.scaleX * scaleNew;
            TransformNew.scaleY = TransformOld.scaleY * scaleNew;
            if (currentUnits.Count == 1)
            {
                currentUnitsTransNew[0].scaleX = currentUnitsTransOld[0].scaleX * scaleNew;
                currentUnitsTransNew[0].scaleY = currentUnitsTransOld[0].scaleY * scaleNew;
            }
            else
            {
                //根据任意方向进行的缩放重新计算所有参数S
                for (int i = 0; i < currentUnitsTransNew.Count; i++)
                {
                    //位置变化
                    float angleGap = TransformOld.rotateDegree;
                    PointF pTP = TransformOld.getPosition();
                    PointF pMP = currentUnitsTransOld[i].getPosition();
                    PointF v = new PointF(pMP.X - pTP.X, pMP.Y - pTP.Y);//从变形对象位置到模块位置的向量
                    v.X *= scaleNew;
                    v.Y *= scaleNew;
                    currentUnitsTransNew[i].posX = v.X + pTP.X;
                    currentUnitsTransNew[i].posY = v.Y + pTP.Y;
                    //缩放变化
                    currentUnitsTransNew[i].scaleX = currentUnitsTransOld[i].scaleX * (1 + scaleChanged);
                    currentUnitsTransNew[i].scaleY = currentUnitsTransOld[i].scaleY * (1 + scaleChanged);
                }
            }
            applyUnitsTrans();
            this.UpdateRegion_EditAndFrameLevel();
            form_MA.form_MConfig.checkUnitProperty();
        }
        //帧单元操作--->基于记录的帧单元集合，选中边缘后，平行拖动以斜切当前帧单元(最后需要校正每个帧单元位置...?)
        //private void Transform_ShearBorder(Point pOld, Point pNew)
        //{
        //    PointF pTransBorder0 = TransformOld.pBorderCorner[transBoderPID_0];//边端点0
        //    PointF pTransBorder1 = TransformOld.pBorderCorner[transBoderPID_1];//边端点1
        //    float shear = MathUtil.getProjection_2Vector(pOld, pNew, pTransBorder0, pTransBorder1);
        //    float rate = 1;
        //    if (TransformOld.scaleX != 0)
        //    {
        //        rate *= MathUtil.getCode(TransformOld.scaleX);
        //    }
        //    if (TransformOld.scaleY != 0)
        //    {
        //        rate *= MathUtil.getCode(TransformOld.scaleY);
        //    }
        //    if (transBoderPID_0 % 2 == 1)
        //    {
        //        shear = shear / (TransformOld.size.Height / 2);
        //        shear *= rate;
        //        TransformNew.shearX = TransformOld.shearX + shear;
        //        if (currentUnits.Count == 1)
        //        {
        //            currentUnitsTransNew[0].shearX = currentUnitsTransOld[0].shearX + shear;
        //        }
        //    }
        //    else
        //    {
        //        shear = -shear / (TransformOld.size.Width / 2);
        //        shear *= rate;
        //        TransformNew.shearY = TransformOld.shearY + shear;
        //        if (currentUnits.Count == 1)
        //        {
        //            currentUnitsTransNew[0].shearY = currentUnitsTransOld[0].shearY + shear;
        //        }
        //    }
        //    applyUnitsTrans();
        //    this.updateBox_EditRegion();
        //    form_MA.form_MConfig.checkUnitProperty(currentUnits);
        //}
        //帧单元操作--->移动帧单元的锚点位置
        private void Transform_MoveAnchor(Point pOld, Point pNew)
        {
            PointF positionOld = TransformOld.getPosition(getUICenterX(), getUICenterY(), zoomLevel);
            PointF positionNew = new PointF(positionOld.X + pNew.X - pOld.X, positionOld.Y + pNew.Y - pOld.Y);
            //移动位置
            TransformNew.setPosition(positionNew,getUICenterX(), getUICenterY(), zoomLevel);
            //计算新的锚点
            PointF achorChanged = new PointF(pNew.X - pOld.X, pNew.Y - pOld.Y);
            achorChanged = TransformOld.getInverseVector(achorChanged,zoomLevel);
            PointF achorOld = new PointF(TransformOld.size.Width * TransformOld.anchorX, TransformOld.size.Height * TransformOld.anchorY);
            achorOld.X += achorChanged.X;
            achorOld.Y += achorChanged.Y;
            TransformNew.anchorX = achorOld.X / TransformOld.size.Width;
            TransformNew.anchorY = achorOld.Y / TransformOld.size.Height;
            if (currentUnits.Count == 1)
            {
                currentUnitsTransNew[0].setValue(TransformNew);
            }
            applyUnitsTrans();
            this.UpdateRegion_EditAndFrameLevel();
            form_MA.form_MConfig.checkUnitProperty();
        }
        //记录帧单元变形集合(帧单元集合拷贝变形信息->变形对象集合)
        List<Operation_Transform> currentUnitsTransOld = new List<Operation_Transform>();
        List<Operation_Transform> currentUnitsTransNew = new List<Operation_Transform>();
        private void rememberUnitsTrans()
        {
            currentUnitsTransOld.Clear();
            foreach (MFrameUnit unit in currentUnits)
            {
                currentUnitsTransOld.Add(unit.getOpreTransForm());
            }
            currentUnitsTransNew.Clear();
            foreach (MFrameUnit unit in currentUnits)
            {
                currentUnitsTransNew.Add(unit.getOpreTransForm());
            }
            foreach (Operation_Transform trans in currentUnitsTransOld)
            {
                trans.genAssistPoints(0, 0, 1);
            }
            if (TransformOld != null)
            {
                TransformOld.genAssistPoints(0, 0, 1);
            }
        }
        //应用帧单元变形集合(变形对象集合拷贝变形信息->帧单元集合)
        private void applyUnitsTrans()
        {
            for (int i = 0; i < currentUnitsTransNew.Count; i++)
            {
                Operation_Transform trans = currentUnitsTransNew[i];
                currentUnits[i].applyOpreTransForm(trans);
            }
        }
        //获取UI中心在当前缩放比例下的位置
        public int getUICenterX()
        {
            return imgEditBfWidth / 2 + imgEditCenterPixelX * zoomLevel;
        }
        public int getUICenterY()
        {
            return imgEditBfHeight / 2 + imgEditCenterPixelY * zoomLevel;
        }
        //更新单一元素的变形对象
        public void checkSingleTransform()
        {
            if (TransformNew != null)
            {
                if (currentUnits.Count == 1)
                {
                    TransformNew = currentUnits[0].getOpreTransForm();
                }
            }
        }
        //释放所有变形信息
        public void resetTransform()
        {
            if (currentUnits.Count == 0)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Action);
            form_MA.form_MFrameEdit.checkTransitFrame();
            foreach (MFrameUnit unit in currentUnits)
            {
                unit.resetTransform();
            }
            form_MA.historyManager.AddHistory(HistoryType.Action);
            if (currentUnits.Count == 1)
            {
                form_MA.form_MConfig.checkUnitProperty();
            }
            if (TransformNew != null)
            {
                generateTransform();
                rememberCurrentUnitsCoor();
                rememberUnitsTrans();
                form_MA.form_MConfig.checkUnitProperty();
            }
            UpdateRegion_EditAndFrameLevel();
        }
        //全选当前帧
        public void selectAll(bool cossLevel)
        {
            this.releaseFocusClips();
            if (form_MA.form_MTimeLine != null)
            {
                List<MFrame> frames = form_MA.form_MTimeLine.getSameTimeFrames(true,false);
                if (!cossLevel)
                {
                    frames.Clear();
                    MFrame frame = form_MA.form_MTimeLine.getEditFrame();
                    if (frame != null)
                    {
                        frames.Add(frame);
                    }
                }
                foreach (MFrame frameEdit in frames)
                {
                    if (frameEdit != null)
                    {
                        for (int i = 0; i < frameEdit.Count(); i++)
                        {
                            currentUnits.Add(frameEdit[i]);
                            form_MA.form_MFrameLevel.updateLevelEye();
                            form_MA.form_MFrameLevel.updateLevelLock();

                        }
                    }
                }
            }
            form_MA.showInfor("共选中了" + currentUnits.Count + "个单元");
        }
        //选中当前帧的某一个单元
        public void selectUnit(int id)
        {
            this.releaseFocusClips();
            if (form_MA.form_MTimeLine != null)
            {
                MFrame frameEdit = form_MA.form_MTimeLine.getEditFrame();
                if (frameEdit != null &&id>=0&& frameEdit.Count() > id)
                {
                    currentUnits.Add(frameEdit[id]);
                    form_MA.form_MFrameLevel.updateLevelEye();
                    form_MA.form_MFrameLevel.updateLevelLock();
                }
            }
            form_MA.showInfor("共选中了" + currentUnits.Count + "个单元");
        }
        //当时间轴位置改变时，检查当前是否需要更新补间动画的属性
        public void checkTransitProperty()
        {
            if (currentUnits.Count != 1)
            {
                return;
            }
            MFrame frame = currentUnits[0].parent;
            if (!frame.IsNormalMotion())
            {
                return;
            }
            form_MA.form_MConfig.checkUnitProperty();
        }
        //检查当前选中的切块是否全部位于同一时间轴的同一帧，如果是，则进行图层和帧切换，返回是否已经切换过图层和帧
        public bool checkAllInSameFrame()
        {
            if(currentUnits==null||currentUnits.Count==0)
            {
                return false;
            }
            bool allowSet = true;
            MFrame frame=null;
            foreach (MFrameUnit unit in currentUnits)
            {
                MFrame frameI = (MFrame)(unit.GetParent());
                if (frame == null)
                {
                    frame = frameI;
                }
                else
                {
                    if (!frameI.Equals(frame))
                    {
                        allowSet = false;
                        break;
                    }
                }
            }
            if (allowSet)
            {
                MTimeLine timeLine = (MTimeLine)(frame.GetParent());
                form_MA.form_MTimeLine.setFocusFrame(timeLine.GetID(), frame.timeBegin);
            }
            return allowSet;
        }
        //############################### 操作面板 ##################################
        public enum LayOuts
        {
            Align_Left,
            Align_HCenter,
            Align_Right,
            Align_Top,
            Align_VCenter,
            Align_Bottom,
            Align_SetH,
            Align_SetV,
            Scatter_V,
            Scatter_H,
            Match_V,
            Match_H,
            Mirror_H,
            Mirror_V,
            Mirror_TL,
            Mirror_TR,
            AnchorSet,
        }
        public void LayoutOperations(LayOuts layout, float param0, float param1, bool toCanvas)
        {
            if (currentUnits.Count == 0)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Action);
            checkTransitFrame();
            List<RectangleF> boxs = new List<RectangleF>();
            for (int i = 0; i < currentUnits.Count; i++)
            {
                boxs.Add(currentUnits[i].getTransformBox());
            }
            RectangleF rectSurround = MathUtil.getRectsBox(boxs);
            //对齐操作
            if (layout >= LayOuts.Align_Left && layout <= LayOuts.Align_Bottom)
            {
                for (int i = 0; i < currentUnits.Count; i++)
                {
                    switch (layout)
                    {
                        case LayOuts.Align_Left:
                            if (toCanvas)
                            {
                                currentUnits[i].posX += -boxs[i].X;
                            }
                            else
                            {
                                currentUnits[i].posX += rectSurround.X - boxs[i].X;
                            }
                            break;
                        case LayOuts.Align_HCenter:
                            if (toCanvas)
                            {
                                currentUnits[i].posX += -(boxs[i].X + boxs[i].Width / 2);
                            }
                            else
                            {
                                currentUnits[i].posX += rectSurround.X + rectSurround.Width / 2 - (boxs[i].X + boxs[i].Width / 2);
                            }
                            break;
                        case LayOuts.Align_Right:
                            if (toCanvas)
                            {
                                currentUnits[i].posX += -(boxs[i].X + boxs[i].Width);
                            }
                            else
                            {
                                currentUnits[i].posX += rectSurround.X + rectSurround.Width - (boxs[i].X + boxs[i].Width);
                            }
                            break;
                        case LayOuts.Align_Bottom:
                            if (toCanvas)
                            {
                                currentUnits[i].posY += -boxs[i].Y;
                            }
                            else
                            {
                                currentUnits[i].posY += rectSurround.Y - boxs[i].Y;
                            }
                            break;
                        case LayOuts.Align_VCenter:
                            if (toCanvas)
                            {
                                currentUnits[i].posY += -(boxs[i].Y + boxs[i].Height / 2);
                            }
                            else
                            {
                                currentUnits[i].posY += rectSurround.Y + rectSurround.Height / 2 - (boxs[i].Y + boxs[i].Height / 2);
                            }
                            break;
                        case LayOuts.Align_Top:
                            if (toCanvas)
                            {
                                currentUnits[i].posY += -(boxs[i].Y + boxs[i].Height);
                            }
                            else
                            {
                                currentUnits[i].posY += rectSurround.Y + rectSurround.Height - (boxs[i].Y + boxs[i].Height);
                            }
                            break;
                    }
                }
            }
            //整体排列
            if (layout >= LayOuts.Align_SetH && layout <= LayOuts.Align_SetV)
            {
                if (layout == LayOuts.Align_SetH)
                {
                    float x = -rectSurround.Width / 2 - rectSurround.X;
                    for (int i = 0; i < boxs.Count; i++)
                    {
                        currentUnits[i].posX += x;
                    }
                }
                else
                {
                    float y = -rectSurround.Height / 2 - rectSurround.Y;
                    for (int i = 0; i < boxs.Count; i++)
                    {
                        currentUnits[i].posY += y;
                    }
                }

            }
            //均匀分布
            if (layout >= LayOuts.Scatter_V && layout <= LayOuts.Scatter_H && boxs.Count > 1)
            {
                if (layout == LayOuts.Scatter_V)
                {
                    if (toCanvas)
                    {
                        rectSurround.Y = -rectSurround.Height / 2;
                    }
                    //获得顺序
                    List<int> order = MathUtil.getRectanglesOrder(boxs, MathUtil.BoderType.BoderTop);
                    //搜集全部高度
                    float hAll = 0;
                    for (int i = 0; i < boxs.Count; i++)
                    {
                        hAll += boxs[i].Height;
                    }
                    float gap = (rectSurround.Height - hAll) / (boxs.Count - 1);

                    float y = rectSurround.Y;
                    //将目标改动到相应的位置
                    for (int i = 0; i < order.Count; i++)
                    {
                        int id = order[i];
                        currentUnits[id].posY += y - boxs[id].Y;
                        y += boxs[id].Height + gap;
                    }
                }
                else
                {
                    if (toCanvas)
                    {
                        rectSurround.X = -rectSurround.Width / 2;
                    }
                    //获得顺序
                    List<int> order = MathUtil.getRectanglesOrder(boxs, MathUtil.BoderType.BoderLeft);
                    //搜集全部高度
                    float wAll = 0;
                    for (int i = 0; i < boxs.Count; i++)
                    {
                        wAll += boxs[i].Width;
                    }
                    float gap = (rectSurround.Width - wAll) / (boxs.Count - 1);

                    float x = rectSurround.X;
                    //将目标改动到相应的位置
                    for (int i = 0; i < order.Count; i++)
                    {
                        int id = order[i];
                        currentUnits[id].posX += x - boxs[id].X;
                        x += boxs[id].Width + gap;
                    }
                }
            }
            //匹配大小
            if (layout >= LayOuts.Match_V && layout <= LayOuts.Match_H && boxs.Count > 1)
            {
                if (layout == LayOuts.Match_V)
                {
                    float ySize = Math.Abs(currentUnits[0].getSize().Height * currentUnits[0].scaleY);
                    for (int i = 1; i < currentUnits.Count; i++)
                    {
                        currentUnits[i].scaleY = (ySize / currentUnits[i].getSize().Height) * (currentUnits[i].scaleY >= 0 ? 1 : -1);
                    }
                }
                else
                {
                    float xSize = Math.Abs(currentUnits[0].getSize().Width * currentUnits[0].scaleX);
                    for (int i = 1; i < currentUnits.Count; i++)
                    {
                        currentUnits[i].scaleX = (xSize / currentUnits[i].getSize().Width) * (currentUnits[i].scaleX >= 0 ? 1 : -1);
                    }
                }

            }
            //镜像
            if (layout >= LayOuts.Mirror_H && layout <= LayOuts.Mirror_V)
            {
                if (layout == LayOuts.Mirror_H)
                {
                    float xCenter = rectSurround.X + rectSurround.Width / 2;
                    if (toCanvas)
                    {
                        xCenter = 0;
                    }
                    for (int i = 0; i < currentUnits.Count; i++)
                    {
                        currentUnits[i].posX += (xCenter - (boxs[i].X + boxs[i].Width / 2)) * 2;
                        currentUnits[i].rotateDegree = MathUtil.standirdDegree(180 - currentUnits[i].rotateDegree);
                        currentUnits[i].scaleY *= -1;
                    }
                }
                else
                {
                    float yCenter = rectSurround.Y + rectSurround.Height / 2;
                    if (toCanvas)
                    {
                        yCenter = 0;
                    }
                    for (int i = 0; i < currentUnits.Count; i++)
                    {
                        currentUnits[i].posY += (yCenter - (boxs[i].Y + boxs[i].Height / 2)) * 2;
                        currentUnits[i].rotateDegree = MathUtil.standirdDegree(- currentUnits[i].rotateDegree);
                        currentUnits[i].scaleY *= -1;
                    }
                }
            }
            //旋转
            if (layout >= LayOuts.Mirror_TL && layout <= LayOuts.Mirror_TR)
            {
                PointF pCenter = new PointF(rectSurround.X + rectSurround.Width / 2, rectSurround.Y + rectSurround.Height / 2);
                if (toCanvas)
                {
                    pCenter = new PointF(0, 0);
                }
                Matrix m = new Matrix();
                if (layout == LayOuts.Mirror_TR)
                {
                    m.Rotate(90);
                    PointF[] pVector = new PointF[1];
                    for (int i = 0; i < currentUnits.Count; i++)
                    {
                        pVector[0] = new PointF((boxs[i].X + boxs[i].Width / 2) - pCenter.X, (boxs[i].Y + boxs[i].Height / 2) - pCenter.Y);
                        m.TransformPoints(pVector);
                        PointF destPoint = new PointF(pCenter.X + pVector[0].X, pCenter.Y + pVector[0].Y);
                        currentUnits[i].posX += destPoint.X - (boxs[i].X + boxs[i].Width / 2);
                        currentUnits[i].posY += destPoint.Y - (boxs[i].Y + boxs[i].Height / 2);
                        currentUnits[i].rotateDegree =  MathUtil.standirdDegree(currentUnits[i].rotateDegree + 90);
                    }
                }
                else
                {
                    m.Rotate(-90);
                    PointF[] pVector = new PointF[1];
                    for (int i = 0; i < currentUnits.Count; i++)
                    {
                        pVector[0] = new PointF((boxs[i].X + boxs[i].Width / 2) - pCenter.X, (boxs[i].Y + boxs[i].Height / 2) - pCenter.Y);
                        m.TransformPoints(pVector);
                        PointF destPoint = new PointF(pCenter.X + pVector[0].X, pCenter.Y + pVector[0].Y);
                        currentUnits[i].posX += destPoint.X - (boxs[i].X + boxs[i].Width / 2);
                        currentUnits[i].posY += destPoint.Y - (boxs[i].Y + boxs[i].Height / 2);
                        currentUnits[i].rotateDegree = MathUtil.standirdDegree(currentUnits[i].rotateDegree - 90);
                    }
                }

            }
            //设置锚点
            if (layout == LayOuts.AnchorSet)
            {
                for (int i = 0; i < currentUnits.Count; i++)
                {
                    currentUnits[i].anchorX = param0;
                    currentUnits[i].anchorY = param1;
                }

            }
            form_MA.historyManager.AddHistory(HistoryType.Action);
            if (TransformNew != null)
            {
                generateTransform();
            }
            rememberCurrentUnitsCoor();
            rememberUnitsTrans();
            UpdateRegion_EditFrame();
            form_MA.form_MConfig.checkUnitProperty();
        }


        private void glView_DoubleClick(object sender, EventArgs e)
        {
            if (currentUnits.Count == 1)
            {
                if (form_MA.form_MFrameLevel.showUnitInView(currentUnits[0].GetID()))
                {
                    form_MA.form_MFrameLevel.UpdateRegion_FrameLevel();
                }
                if (currentUnits[0] is MFrameUnit_Bitmap)
                {
                    form_MA.form_MImgsList.setFocusClip(((MFrameUnit_Bitmap)currentUnits[0]).clipElement, true);
                }
            }
        }
        public bool loaded = false;
        private void Form_MFrameEdit_Load(object sender, EventArgs e)
        {
            glView.MakeCurrent();
            GLWorld.InitContext();
            GLWorld.SetupViewport(glView);
            this.loaded = true;
        }
        private void glView_Resize(object sender, EventArgs e)
        {
            if (loaded)
            {
                glView.MakeCurrent();
                GLWorld.SetupViewport(glView);
                UpdateRegion_EditFrame();
            }
        }
        private void Form_MFrameEdit_RegionChanged(object sender, EventArgs e)
        {
            if (glContextRereated >= 0)
            {
                glContextRereated = -1;
                //Console.WriteLine("Form_MFrameEdit_RegionChanged");
                if (!Form_MAnimation.inResetPanels)
                {
                    form_MA.form_MImgsList.mImgsManager.rebindTextures();
                    ConstTextureImgs.rebindTextures();
                }
                UpdateRegion_EditFrame();
            }
        }
        private int glContextRereated = -1;
        private void Form_MFrameEdit_ParentChanged(object Sender, EventArgs e)
        {
            if (loaded && Parent != null)
            {
                glContextRereated = 0;
            }
            //Console.WriteLine("Form_MFrameEdit_ParentChanged:" + this.Parent);
        }

        private void glView_Paint(object sender, PaintEventArgs e)
        {
            this.UpdateRegion_EditFrame();
        }



    }
   

}