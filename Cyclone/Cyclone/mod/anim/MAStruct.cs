using System;
using Cyclone.alg;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using Cyclone.mod.anim;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Cyclone.alg.type;
using Cyclone.alg.math;
using Cyclone.alg.opengl;
using Cyclone.alg.util;
using Cyclone.mod.animimg;
using System.Xml;
using Cyclone.Cyclone.alg.util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Cyclone.mod.anim
{
    /// <summary>
    /// 角色管理器，拥有一个存放所有角色的列表，并管理所有角色级别的增减，访问等操作
    /// </summary>
    public class MActorsManager : MNode<MActorFolder>
    {
        public int nameID = 0;
        private static int nameIDCount = 0;
        public MActorsManager()
        {
        }
        public MActorsManager(Form_MActorsList parentT)
        {
            parent = parentT;
            nameID = nameIDCount;
            nameIDCount++;
        }
        public MActorsManager Clone()
        {
            return Clone((Form_MActorsList)parent);
        }
        public MActorsManager Clone(Form_MActorsList parent)
        {
            MActorsManager newInstance = new MActorsManager(parent);
            newInstance.parent = parent;
            newInstance.name = name;
            newInstance.allowUpdateUI = allowUpdateUI;
            newInstance.ui = ui;
            foreach (MActorFolder son in m_sonList)
            {
                newInstance.m_sonList.Add((MActorFolder)son.Clone(newInstance));
            }
            return newInstance;
        }
        public MActor getActorBySumID(int sumID)
        {
            for (int i = 0; i < this.Count(); i++)
            {
                MActorFolder folder = this[i];
                if (sumID - folder.Count() < 0)
                {
                    return folder[sumID];
                }
                else
                {
                    sumID -= folder.Count();
                }
            }
            return null;
        }
        //统计所有角色的数量
        public int getActorsCount()
        {
            int count = 0;
            for (int i = 0; i < this.Count(); i++)
            {
                MActorFolder folder = this[i];
                count += folder.Count();
            }
            return count;
        }
        public MActorFolder getFolderByName(String folderName)
        {
            for (int i = 0; i < this.Count(); i++)
            {
                MActorFolder folder = this[i];
                if (folder.name.Equals(folderName))
                {
                    return folder;
                }
            }
            return null;
        }
        public MActor getActorByName(String folderName, String actorName)
        {
            for (int i = 0; i < this.Count(); i++)
            {
                MActorFolder folder = this[i];
                if (folder.name.Equals(folderName))
                {
                    for (int j = 0; j < folder.Count(); j++)
                    {
                        MActor actor = folder[j];
                        if (actor.name.Equals(actorName))
                        {
                            return actor;
                        }
                    }
                }
            }
            return null;
        }
        internal void combine(MActorsManager srcActorsManager, ArrayList actorsID)
        {
            for (int i = 0; i < actorsID.Count; i++)
            {
                int id = (int)actorsID[i];
                MActor srcActor = srcActorsManager.getActorBySumID(id);
                MActor destActor = this.getActorByName(((MActorFolder)srcActor.GetParent()).name, srcActor.name);
                //替换相同名字的单元
                if (destActor != null)
                {
                    MActorFolder destFolder = (MActorFolder)destActor.GetParent();
                    destFolder[destActor.GetID()] = srcActor;
                }
                else
                {
                    //转移到相同的文件夹
                    MActorFolder destFolder = this.getFolderByName(((MActorFolder)srcActor.GetParent()).name);
                    if (destFolder == null)
                    {
                        destFolder = new MActorFolder(this);
                        destFolder.name = ((MActorFolder)srcActor.GetParent()).name;
                        this.Add(destFolder);
                    }
                    destFolder.Add(srcActor);
                }
            }
        }
        public int[][] spitStreamInfor;
        public override void ExportObject(System.IO.Stream s)
        {
            int actorsCount = getActorsCount();
            UserDoc.ArrayTxts_Head.Add("//Generated " + actorsCount + " sprites");
            UserDoc.ArrayTxts_Head.Add(" ");
            UserDoc.ArrayTxts_Java.Add("//Generated " + actorsCount + " sprites");
            UserDoc.ArrayTxts_Java.Add(" ");
            //输出角色分组信息，如果集中存储的话，同时创建分立流信息
            short len = (short)this.Count();
            IOUtil.writeShort(s, len);
            if (!Consts.exp_splitAnimation)
            {
                spitStreamInfor = new int[len][];
            }
            for (short i = 0; i < len; i++)
            {
                MActorFolder elem = m_sonList[i];
                short elemCount=(short)elem.Count();
                IOUtil.writeShort(s, elemCount);
                if (!Consts.exp_splitAnimation)
                {
                    spitStreamInfor[i] = new int[elemCount];
                }
            }
            //输出角色动画数据(无论是否分立，动画数据都不会存储在s)
            Stream sOut = null;
            if (!Consts.exp_splitAnimation)//集中存储，需要同时记录存储位置
            {
                String fileName = Consts.exportC2DBinFolder + Consts.exportFileName + "_sds.bin";
                sOut = File.Open(fileName, FileMode.Create); 
            }
            for (short i = 0; i < len; i++)
            {
                MActorFolder elem = m_sonList[i];
                UserDoc.ArrayTxts_Head.Add("#define SpriteFolder_" + elem.name + " " + i);
                UserDoc.ArrayTxts_Java.Add("public static final short SpriteFolder_" + elem.name + " = " + i+";");
                elem.ExportObject(sOut);
            }
            UserDoc.ArrayTxts_Head.Add(" ");
            UserDoc.ArrayTxts_Java.Add(" ");
            //关闭集中存储流
            if (sOut != null)
            {
                sOut.Close();
                sOut.Dispose();
            }
            //如果集中存储的话，同时输出分立流信息到s
            if (!Consts.exp_splitAnimation)
            {
                for (short i = 0; i < spitStreamInfor.Length; i++)
                {
                    for (short j = 0; j < spitStreamInfor[i].Length; j++)
                    {
                        IOUtil.writeInt(s, spitStreamInfor[i][j]);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 角色文件夹
    /// </summary>
    public class MActorFolder : MNode<MActor>
    {
        public MActorFolder()
        {
        }
        public MActorFolder(MActorsManager parenT)
        {
            parent = parenT;
        }
        public MActorFolder Clone()
        {
            return Clone((MActorsManager)parent);
        }
        public MActorFolder Clone(MActorsManager parent)
        {
            MActorFolder newInstance = new MActorFolder(parent);
            newInstance.parent = parent;
            newInstance.name = name;
            newInstance.allowUpdateUI = allowUpdateUI;
            newInstance.ui = ui;
            foreach (MActor son in m_sonList)
            {
                newInstance.m_sonList.Add((MActor)son.Clone(newInstance));
            }
            return newInstance;
        }
        public override void ExportObject(System.IO.Stream s)
        {
            short len = (short)m_sonList.Count;
            //IOUtil.writeShort(s, len);//此信息已经包含在父类角色分组信息
            for (short i = 0; i < len; i++)
            {
                MActor elem = m_sonList[i];
                UserDoc.ArrayTxts_Head.Add("#define Sprite_" + this.name + "_" + elem.name + " " + i);
                UserDoc.ArrayTxts_Java.Add("public static final short Sprite_" + this.name + "_" + elem.name + " = " + i+";");
                elem.ExportObject(s);
            }
        }
    }
    /// <summary>
    /// 角色单元
    /// </summary>
    public class MActor : MNode<MAction>
    {
        public MActor()
        {
        }
        public MActor(MActorFolder parenT)
        {
            parent = parenT;
        }
        public MActor Clone()
        {
            return Clone((MActorFolder)parent);
        }
        public int getMaxNumOfTimeLine()
        {
            int max = 0;
            for (int i = 0; i < this.Count(); i++)
            {
                int numTimeLine = this[i].Count();
                if (numTimeLine > max)
                {
                    max = numTimeLine;
                }
            }
            return max;
        }
        public MActor Clone(MActorFolder parent)
        {
            MActor newInstance = new MActor(parent);
            newInstance.parent = parent;
            newInstance.name = name;
            newInstance.allowUpdateUI = allowUpdateUI;
            newInstance.ui = ui;
            foreach (MAction son in m_sonList)
            {
                newInstance.m_sonList.Add((MAction)son.Clone(newInstance));
            }
            return newInstance;
        }
        public Rect getBox(int actionID, int timePos)
        {
            if (this[actionID] == null)
            {
                return null;
            }
            if (this[actionID].getMaxFrameLen() < timePos || timePos<0)
            {
                return null;
            }
            return this[actionID].getBox(timePos);
        }
        public Rect getBoxGDI(int actionID, int timePos)
        {
            Rect rect = getBox(actionID, timePos);
            return rect;
        }
        //带图片映射的绘制(中心对齐)
        public Boolean display(Graphics g, int x, int y, int actionID, int frameID, float zoomLevel, Rect limitRect, float alpha, ObjectVector imgMapList)
        {
            MAction action = this[actionID];
            if (action == null)
            {
                return false;
            }
            if (frameID < 0 || frameID >= action.getMaxFrameLen())
            {
                return false;
            }
            for(int i=0;i<action.Count();i++)
            {
                MTimeLine timeLine = action[i];
                MFrame frame = timeLine.getFrameByX(frameID);
                if (frame != null)
                {
                    frame.display(g, x, y , zoomLevel, limitRect, alpha, frameID, imgMapList);
                }
            }
            return true;
        }
        //获取一系列用到的图片
        public List<MImgElement> getUsedImages()
        {
            List<MImgElement> imgsList = new List<MImgElement>();
            MActor actor = this;
            for (int i1 = 0; i1 < actor.Count(); i1++)
            {
                for (int i2 = 0; i2 < actor[i1].Count(); i2++)
                {
                    for (int i3 = 0; i3 < actor[i1][i2].Count(); i3++)
                    {
                        for (int i4 = 0; i4 < actor[i1][i2][i3].Count(); i4++)
                        {
                            MFrameUnit unit = actor[i1][i2][i3][i4];
                            if (unit is MFrameUnit_Bitmap)
                            {
                                MFrameUnit_Bitmap unitBitmap = (MFrameUnit_Bitmap)unit;
                                if (unitBitmap.clipElement != null && unitBitmap.clipElement.imageElement != null)
                                {
                                    if (!imgsList.Contains(unitBitmap.clipElement.imageElement))
                                    {
                                        imgsList.Add(unitBitmap.clipElement.imageElement);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return imgsList;
        }
        //导出资源
        public override void ExportObject(System.IO.Stream s)
        {
            String subName = "_sd_" + ((MActorFolder)parent).GetID() + "_" + this.GetID();
            if (Consts.exp_splitAnimation)//分立存储
            {
                String fileName = Consts.exportC2DBinFolder + Consts.exportFileName + subName+".bin";
                s = File.Open(fileName, FileMode.Create);
            }
            else//集中存储
            {
                MActorsManager actorsManager = (MActorsManager)(((MActorFolder)parent).GetParent());
                actorsManager.spitStreamInfor[((MActorFolder)parent).GetID()][GetID()] = (int)(s.Position);
            }
            //输出内容-------------------------------
            //输出图片引用列表
            List<MImgElement> imgsList = getUsedImages();
            short len = (short)imgsList.Count;
            IOUtil.writeShort(s, len);
            for (short i = 0; i < len; i++)
            {
                IOUtil.writeShort(s, (short)imgsList[i].GetID());
            }
            //输出动作列表
            len = (short)m_sonList.Count;
            IOUtil.writeShort(s, len);
            for (short i = 0; i < len; i++)
            {
                MAction elem = m_sonList[i];
                elem.ExportObject(s);
            }
            //分立存储情况下，关闭分立流
            if (Consts.exp_splitAnimation)
            {
                s.Close();
                s.Dispose();
            }
        }

    }
    /// <summary>
    /// 时间轴拥有者（可以是一个动作单元，也可以是任意一种库元件）
    /// </summary>
    public class MTimeLineHoder : MNode<MTimeLine>
    {
        public MTimeLineHoder()
        {
        }
        public MTimeLineHoder(MActor parenT)
        {
            parent = parenT;
        }
        public virtual MTimeLineHoder Clone()
        {
            throw new Exception("can't use this method");
        }

        public void displayNaviRegion(Graphics gBuffer, int bufferWidth, int bufferHeight, int showBegin, int showEnd,int LevelH, int selcetedID)
        {
            //遍历显示每行
            for (int j=showBegin;j<=showEnd;j++)
            {
                MTimeLine line = this[j];
                line.displayItem_Navi(gBuffer, 0, (j - showBegin) * LevelH, bufferWidth, LevelH, selcetedID);
            }
        }
        public void displayFrameRegion(Graphics gBuffer, int bufferWidth, int rowBegin, int rowEnd,int rowHeight, int columnBegin, int columnEnd, int columnW, int selcetedID)
        {
            //遍历显示每行
            for (int j = rowBegin; j <= rowEnd; j++)
            {
                MTimeLine line = this[j];
                line.displayItem_Frame(gBuffer, 0, (j - rowBegin) * rowHeight, bufferWidth, rowHeight, columnBegin, columnEnd, columnW, selcetedID);
            }
        }
        //获取最大的帧长度
        public int getMaxFrameLen()
        {
            int len = 0;
            foreach (MTimeLine son in m_sonList)
            {
                int lenI = son.getTimeLineLen();
                if (lenI > len)
                {
                    len = lenI;
                }
            }
            return len;
        }
        //获取某个帧的变形后包围框
        public Rect getBox(int timePos)
        {
            Rect rect = null;
            List<RectangleF> rects = new List<RectangleF>();
            for (int i = 0; i < this.Count(); i++)
            {
                MTimeLine timeLine = this[i];
                MFrame frame = timeLine.getFrameByX(timePos);
                if (frame != null)
                {
                    RectangleF rectI = frame.getBox(timePos);
                    rects.Add(rectI);
                }
            }
            RectangleF all = MathUtil.getRectsBox(rects);
            if (all.Width > 0 && all.Height > 0)
            {
                rect = new Rect((int)all.X, (int)all.Y, (int)all.Width, (int)all.Height);
            }
            return rect;
        }
        //插入关键帧
        public void insertKeyFrame(int row, int column)
        {
            MTimeLine timeLine = this[row];
            if (timeLine == null)
            {
                return;
            }
            timeLine.insertKeyFrame(column);
        }
        //插入帧间隔
        public void insertFrameDelay(int row ,int column)
        {
            MTimeLine timeLine = this[row];
            if (timeLine == null)
            {
                return;
            }
            timeLine.insertFrameDelay(column);
        }
        //插入帧多行多列间隔
        public void insertFrameDelay(int rowBegin, int columnNegin,int rowCount,int columnCount)
        {
            for (int i = rowBegin; i < rowBegin + rowCount; i++)
            {
                MTimeLine timeLine = this[i];
                if (timeLine == null)
                {
                    return;
                }
                timeLine.insertFrameDelay(columnNegin, columnCount);
            }
        }
        //删除多行多列帧间隔
        public bool removeFrameDelay(int rowBegin, int columnNegin, int rowCount, int columnCount)
        {
            bool hasChanged = false;
            for (int i = rowBegin; i < rowBegin + rowCount; i++)
            {
                MTimeLine timeLine = this[i];
                if (timeLine == null)
                {
                    break;
                }
                if (timeLine.removeFrameDelay(columnNegin, columnCount, rowCount > 1 || columnCount > 1))
                {
                    hasChanged = true;
                }
            }
            return hasChanged;
        }
        //检查是否有显示关闭的层
        public bool isSomeOneNotVisible()
        {
            foreach (MTimeLine son in m_sonList)
            {
                if (!son.isVisible)
                {
                    return true;
                }
            }
            return false;
        }
        //设置全部层的显示属性
        public void makeAllVisible(bool visible)
        {
            foreach (MTimeLine son in m_sonList)
            {
                son.isVisible = visible;
            }
        }
        //检查是否有锁定的层
        public bool isSomeOneLocked()
        {
            foreach (MTimeLine son in m_sonList)
            {
                if (son.isLocked)
                {
                    return true;
                }
            }
            return false;
        }
        //设置全部层的锁定属性
        public void makeAllLocked(bool locked)
        {
            foreach (MTimeLine son in m_sonList)
            {
                son.isLocked = locked;
            }
        }
    }
    /// <summary>
    /// 动作单元
    /// </summary>
    public class MAction : MTimeLineHoder
    {
        public MAction()
        {
        }
        public MAction(MActor parenT)
        {
            parent = parenT;
        }
        public override MTimeLineHoder Clone()
        {
            return Clone((MActor)parent);
        }
        public MTimeLineHoder Clone(MActor parent)
        {
            MAction newInstance = new MAction(parent);
            newInstance.parent = parent;
            newInstance.name = name;
            newInstance.allowUpdateUI = allowUpdateUI;
            newInstance.ui = ui;
            foreach (MTimeLine son in m_sonList)
            {
                newInstance.m_sonList.Add((MTimeLine)son.Clone(newInstance));
            }
            return newInstance;
        }
    }
    ///// <summary>
    ///// 时间轴文件夹单元
    ///// </summary>
    //public class MTimeLineFolder : MNode<MTimeLine>
    //{
    //    public MTimeLineFolder(MTimeLineHoder parenT)
    //    {
    //        parent = parenT;
    //    }
    //    public MTimeLineFolder Clone()
    //    {
    //        MTimeLineFolder newInstance = new MTimeLineFolder((MTimeLineHoder)parent);
    //        newInstance.parent = parent;
    //        newInstance.name = name;
    //        newInstance.allowUpdateUI = allowUpdateUI;
    //        newInstance.ui = ui;
    //        foreach (MTimeLine son in sonList)
    //        {
    //            newInstance.sonList.Add((MTimeLine)son.Clone());
    //        }
    //        return newInstance;
    //    }
    //}
    /// <summary>
    /// 时间轴单元
    /// </summary>
    public class MTimeLine : MNode<MFrame>
    {
        public bool isVisible = true;
        public bool isLocked = false;
        public MTimeLine()
        {
        }
        public MTimeLine(MTimeLineHoder parenT)
        {
            parent = parenT;
        }
        public MTimeLine Clone()
        {
            return Clone((MTimeLineHoder)parent);
        }
        public MTimeLine Clone(MTimeLineHoder parent)
        {
            MTimeLine newInstance = new MTimeLine(parent);
            newInstance.parent = parent;
            newInstance.name = name;
            newInstance.allowUpdateUI = allowUpdateUI;
            newInstance.isVisible = isVisible;
            newInstance.isLocked = isLocked;
            newInstance.ui = ui;
            foreach (MFrame son in m_sonList)
            {
                newInstance.Add((MFrame)son.Clone(newInstance));
            }
            return newInstance;
        }
        //获得指定列位置的帧
        public MFrame getFrameByX(int xPos)
        {
            MFrame f = null;
            for (int i = 0; i < Count(); i++)
            {
                MFrame fI = this[i];
                if (fI.timeBegin <= xPos && fI.timeBegin + fI.timeLast > xPos)
                {
                    f = fI;
                    break;
                }
            }
            return f;
        }
        //获得指定列位置的帧类型
        public MFrameType getFrameTypeByX(int xPos)
        {
            MFrameType type = MFrameType.TYPE_NUL;
            for (int i = 0; i < Count(); i++)
            {
                MFrame fI = this[i];
                if (fI.timeBegin == xPos)
                {
                    type = fI.frameType;
                    break;
                }
                else if (fI.timeBegin < xPos && fI.timeBegin + fI.timeLast > xPos)
                {
                    type = MFrameType.TYPE_MID;
                    break;
                }
            }
            return type;
        }
        //获得指定列位置的前端帧
        public MFrame getFrontFrameByX(int xPos)
        {
            MFrame f = null;
            for (int i = 0; i < Count(); i++)
            {
                MFrame fI = this[i];
                if (fI.timeBegin > xPos)
                {
                    break;
                }
                else
                {
                    f = fI;
                }
            }
            return f;
        }
        //显示时间轴导航部分
        private static Font font = new Font("宋体", 9);
        public void displayItem_Navi(Graphics gBuffer, int x, int y, int bufferWidth, int rowHeight, int selcetedID)
        {
            GraphicsUtil.fillRect(gBuffer, x, y, bufferWidth, rowHeight, 0xededed);
            Image img_Level = global::Cyclone.Properties.Resources.timeLine_Level;
            Image img_LevelFlag = global::Cyclone.Properties.Resources.timeLine_LevelFlag;
            Image img_Eye = global::Cyclone.Properties.Resources.movie_eye_16x16;
            Image img_EyeClose = global::Cyclone.Properties.Resources.movie_cross_16x16;
            Image img_Lock = global::Cyclone.Properties.Resources.movie_lock_16x16;
            if (selcetedID==this.GetID())
            {
                GraphicsUtil.fillRect(gBuffer, x, y, bufferWidth, rowHeight - 1, 0x3399ff);
                GraphicsUtil.drawString(gBuffer, x + 35, y + rowHeight - 1, name, font, 0xFFFFFF, Consts.BOTTOM | Consts.LEFT);
            }
            else
            {
                GraphicsUtil.fillRect(gBuffer, x, y, bufferWidth, rowHeight - 1, 0xffffff);
                GraphicsUtil.drawString(gBuffer, x + 35, y + rowHeight - 1, name, font, 0x646464, Consts.BOTTOM | Consts.LEFT);
            }
            GraphicsUtil.drawClip(gBuffer, img_Level, x + 20, y + (rowHeight - img_Level.Height) / 2 + 1, 0, 0, img_Level.Width, img_Level.Height, 0);
            Image imgEyeFlag = isVisible ? img_LevelFlag : img_EyeClose;
            Image imgLockFlag = isLocked ? img_Lock : img_LevelFlag;
            GraphicsUtil.drawClip(gBuffer, imgEyeFlag, bufferWidth - 36 - imgEyeFlag.Width / 2, y + (rowHeight - imgEyeFlag.Height) / 2 + 1, 0, 0, imgEyeFlag.Width, imgEyeFlag.Height, 0);
            int offY = isLocked ? -1 : 1;
            GraphicsUtil.drawClip(gBuffer, imgLockFlag, bufferWidth - 12 - imgLockFlag.Width / 2, y + (rowHeight - imgLockFlag.Height) / 2 + offY, 0, 0, imgLockFlag.Width, imgLockFlag.Height, 0);
        }
        //显示时间轴上的帧
        public void displayItem_Frame(Graphics gBuffer, int x, int y, int bufferWidth, int rowHeight, int columnBegin, int columnEnd, int columnWidth, int selcetedID)
        {
            GraphicsUtil.fillRect(gBuffer, x, y, bufferWidth, rowHeight, 0xa0a0a0);
            Image img = global::Cyclone.Properties.Resources.timeLineFrameBg;
            //先绘制背景
            int bgFC = img.Width / columnWidth;
            int xT = -(columnBegin % 5) * columnWidth;
            while (x+xT < bufferWidth)
            {
                GraphicsUtil.drawClip(gBuffer, img, x+xT, y, 0, 0, img.Width, img.Height, 0);
                xT += img.Width;
            }
            //绘制关键帧和帧间隔
            Image imgKeyFrame = global::Cyclone.Properties.Resources.timelinekeyframe;
            Image imgFrameDelay = global::Cyclone.Properties.Resources.timeLineFrame;
            MFrame nextFrame = getFrontFrameByX(columnBegin);
            if (nextFrame == null)
            {
                return;
            }
            xT = x + (nextFrame.timeBegin - columnBegin) * columnWidth;
            while (nextFrame != null)
            {
                int _x=0;
                int _y = 0;
                if (nextFrame.hasMotion)
                {
                    if (nextFrame.Count() > 0)
                    {
                        _x = 6 * columnWidth;
                    }
                    else
                    {
                        _x = 9 * columnWidth;
                    }
                    _y = 2*rowHeight;
                }
                else if (nextFrame.Count()==0)
                {
                    _x = 3 * columnWidth;
                    _y = rowHeight;
                }
                //绘制起始间隔
                int offset = 0;
                if (nextFrame.timeLast == 1)
                {
                    offset = columnWidth;
                }
                GraphicsUtil.drawClip(gBuffer, imgKeyFrame, xT, y - 1, _x + offset, 0, columnWidth, rowHeight+1, 0);
                xT += columnWidth;
                //绘制中间和结尾间隔
                if (nextFrame.timeLast > 1)
                {
                    if (nextFrame.timeLast > 2)
                    {
                        int xBegin = xT;
                        //绘制中间间隔
                        int midWidth = (nextFrame.timeLast - 2) * columnWidth;
                        while (midWidth > imgFrameDelay.Width)
                        {
                            GraphicsUtil.drawClip(gBuffer, imgFrameDelay, xT, y - 1, 0, _y, imgFrameDelay.Width, rowHeight + 1, 0);
                            midWidth -= imgFrameDelay.Width;
                            xT += imgFrameDelay.Width;
                        }
                        if (midWidth > 0)
                        {
                            GraphicsUtil.drawClip(gBuffer, imgFrameDelay, xT, y - 1, 0, _y, midWidth, rowHeight + 1, 0);
                            xT += midWidth;
                        }
                        //绘制过渡线
                        if (nextFrame.hasMotion)
                        {
                            int yCenter=y - 1+15;
                            if (nextFrame.IsNormalMotion())
                            {
                                GraphicsUtil.drawLine(gBuffer, xBegin, yCenter, xT, yCenter, 0, 1);
                                GraphicsUtil.drawLine(gBuffer, xT - 4, yCenter - 2, xT - 1, yCenter, 0, 1);
                                GraphicsUtil.drawLine(gBuffer, xT - 4, yCenter + 2, xT - 1, yCenter, 0, 1);
                            }
                            else
                            {
                                GraphicsUtil.drawDashLine(gBuffer, xBegin, yCenter, xT, yCenter, 0, 1);
                            }
                        }
                    }

                    //绘制结束间隔
                    GraphicsUtil.drawClip(gBuffer, imgKeyFrame, xT, y - 1, _x + columnWidth * 2, 0, columnWidth, rowHeight + 1, 0);
                    xT += columnWidth;
                }
                nextFrame=this[nextFrame.GetID()+1];
            }


        }
        //获取时间轴长度(即全部持续的间隔数)
        public int getTimeLineLen()
        {
            if (Count() > 0)
            {
                return m_sonList[Count() - 1].timeBegin + m_sonList[Count() - 1].timeLast;
            }
            return 0;
        }
        //插入帧间隔(补齐到列)
        public void insertFrameDelay(int column)
        {
            MFrame fontFrame = getFrontFrameByX(column);
            MFrameType frameType = getFrameTypeByX(column);
            if (fontFrame == null)
            {
                MFrame frame = new MFrame(this);
                frame.timeBegin = 0;
                frame.timeLast = column + 1;
                this.Add(frame);
            }
            else
            {
                if (frameType == MFrameType.TYPE_NUL)
                {
                    fontFrame.timeLast = column - fontFrame.timeBegin + 1;
                }
                else if (frameType == MFrameType.TYPE_KEY || frameType == MFrameType.TYPE_MID)
                {
                    addDelay(fontFrame.GetID(), 1);
                }
            }

        }
        //插入帧间隔(根据个数)
        public void insertFrameDelay(int column,int count)
        {
            MFrame fontFrame = getFrontFrameByX(column + count -1);
            MFrameType frameType = getFrameTypeByX(column + count - 1);
            if (fontFrame == null)
            {
                MFrame frame = new MFrame(this);
                frame.timeBegin = 0;
                frame.timeLast = column + count;
                this.Add(frame);
            }
            else
            {
                if (frameType == MFrameType.TYPE_NUL)
                {
                    fontFrame.timeLast = column + count - fontFrame.timeBegin;
                }
                else if (frameType == MFrameType.TYPE_KEY || frameType == MFrameType.TYPE_MID)
                {
                    addDelay(fontFrame.GetID(), count);
                }
            }

        }
        //插入关键帧
        public void insertKeyFrame(int column)
        {
            MFrame fontFrame = getFrontFrameByX(column);
            MFrameType frameType = getFrameTypeByX(column);
            if (fontFrame == null)
            {
                MFrame frame = new MFrame(this);
                frame.timeBegin = 0;
                frame.timeLast = column + 1;
                this.Add(frame);
            }
            else
            {
                if (frameType == MFrameType.TYPE_NUL)
                {
                    //调整前端帧的长度
                    fontFrame.timeLast = column - fontFrame.timeBegin;
                    //增加新的关键帧
                    MFrame frame = fontFrame.Clone(this);
                    frame.timeBegin = column;
                    frame.timeLast = 1;
                    this.Add(frame);
                }
                else if (frameType == MFrameType.TYPE_KEY || frameType == MFrameType.TYPE_MID)
                {
                    if (frameType == MFrameType.TYPE_KEY)//在关键帧上插入关键帧
                    {
                        //插入新的关键帧
                        MFrame frame = fontFrame.Clone(this);
                        frame.timeLast = 1;
                        this.Insert(frame, fontFrame.GetID());
                        //从之前的前端帧开始，将所有帧推后
                        addOffX(fontFrame.GetID(), 1);
                    }
                    else if (frameType == MFrameType.TYPE_MID)//在中间帧上插入关键帧
                    {
                        MFrame endFrame = fontFrame.GetNext();
                        ValueTransform transform=new ValueTransform();
                        bool needSetTransform = false;
                        if (fontFrame.hasMotion && endFrame != null && endFrame.Count() == 1 && fontFrame.Count() == 1)
                        {
                            transform = fontFrame[0].getValueTransform(column);
                            needSetTransform = true;
                        }
                        //插入新的关键帧
                        MFrame frame = fontFrame.Clone(this);
                        frame.timeBegin = column;
                        frame.timeLast = (fontFrame.timeBegin + fontFrame.timeLast) - column;
                        this.Insert(frame, fontFrame.GetID()+1);
                        //调整前端长度
                        fontFrame.timeLast = frame.timeBegin - fontFrame.timeBegin;
                        //≌≌注意实现补间动画的中间状态
                        if (needSetTransform)
                        {
                            frame[0].setValueTransform(ref transform);
                        }
                    }
                }
            }
        }
        //移除帧间隔(单选)
        public bool removeFrameDelay(int column,bool clearKeyRoom)
        {
            MFrame headFrame = getFrontFrameByX(column);
            MFrameType frameType = getFrameTypeByX(column);
            if (headFrame == null)
            {
                return false;
            }
            if (frameType == MFrameType.TYPE_KEY || frameType == MFrameType.TYPE_MID)
            {
                if (frameType == MFrameType.TYPE_KEY)//在关键帧执行删除
                {
                    int id = headFrame.GetID();
                    if (clearKeyRoom)
                    {
                        if (headFrame.timeLast > 1)
                        {
                            headFrame.timeLast--;
                            addOffX(id + 1, -1);
                        }
                        else
                        {
                            this.RemoveAt(id);
                            addOffX(id, -1);
                        }
                    }
                    else
                    {
                        if (id == 0)
                        {
                            if (headFrame.timeLast > 1)
                            {
                                if (headFrame.timeBegin == column)
                                {
                                    headFrame.Clear();
                                }
                                headFrame.timeLast--;
                                addOffX(id + 1, -1);
                            }
                            else
                            {
                                this.RemoveAt(id);
                                addOffX(id, -1);
                            }
                        }
                        else
                        {
                            MFrame headBefore = headFrame.GetBefore();
                            headBefore.timeLast += headFrame.timeLast;
                            RemoveAt(id);
                        }
                    }

                }
                else if (frameType == MFrameType.TYPE_MID)//在中间帧上执行删除
                {
                    int id = headFrame.GetID();
                    headFrame.timeLast--;
                    //从之前的后端帧开始，将所有帧前推
                    addOffX(id+1, -1);
                }
            }
            return true;

        }
        //移除帧间隔(多选)
        public bool removeFrameDelay(int column,int count,bool clearKeyRoom)
        {
            MFrame headFrame = getFrontFrameByX(column);
            if (headFrame == null)
            {
                return false;
            }
            bool changed = false;
            for (int i = 0; i < count; i++)
            {
                if (removeFrameDelay(column, clearKeyRoom))
                {
                    changed = true;
                }
            }
            return changed;
        }
        //从第N个帧开始，每个帧长度增加M个间隔
        public void addDelay(int frameN, int delayM)
        {
            MFrame frame = this[frameN];
            frame.timeLast += delayM;
            addOffX(frameN + 1, delayM);
        }
        //从第N个帧开始，每个帧位置增加M个偏移
        public void addOffX(int frameN, int offM)
        {
            for (int i = frameN; i < Count(); i++)
            {
                MFrame frame = this[i];
                frame.timeBegin += offM;
                if (frame.timeBegin < 0)
                {
                    frame.timeBegin = 0;
                }
            }
        }
        public override void ReadObject(System.IO.Stream s)
        {
            base.ReadObject(s);
            isVisible = IOUtil.readBoolean(s);
            isLocked = IOUtil.readBoolean(s);
        }
        public override void WriteObject(System.IO.Stream s)
        {
            base.WriteObject(s);
            IOUtil.writeBoolean(s, isVisible);
            IOUtil.writeBoolean(s, isLocked);
        }
    }
    /// <summary>
    /// 帧单元类型
    /// </summary>
    public enum MFrameType
    {
        TYPE_NUL,//帧类型-不存在的帧
        TYPE_KEY,//帧类型-关键帧
        TYPE_MID //帧类型-中间帧
    }
    /// <summary>
    /// 帧单元
    /// </summary>
    public class MFrame : MNode<MFrameUnit>
    {
        public int timeBegin;//帧位置(在时间轴上所在的位置)
        public int timeLast=1;//帧长度(在时间轴上持续间隔数，只有第一帧含有内容，后面的帧都是第一帧内容的延续)
        public MFrameType frameType = MFrameType.TYPE_KEY;//帧类型(只可能是关键帧)
        public bool hasMotion = false;//是否含有补间动画
        public MFrame()
        {
        }
        public MFrame(MTimeLine parenT)
        {
            parent = parenT;
        }
        public MFrame Clone()
        {
            return Clone((MTimeLine)parent);
        }
        public MFrame Clone(MTimeLine parent)
        {
            MFrame newInstance = new MFrame(parent);
            newInstance.parent = parent;
            newInstance.name = name;
            newInstance.allowUpdateUI = allowUpdateUI;
            newInstance.ui = ui;
            newInstance.timeBegin = timeBegin;
            newInstance.frameType = frameType;
            newInstance.timeLast = timeLast;
            newInstance.hasMotion = hasMotion;
            foreach (MFrameUnit son in m_sonList)
            {
                newInstance.m_sonList.Add((MFrameUnit)son.Clone(newInstance));
            }
            return newInstance;
        }
        //替换内容
        public void replaceContent(MFrame newInstance)
        {
            name = newInstance.name;
            hasMotion = newInstance.hasMotion;
            this.Clear();
            foreach (MFrameUnit son in newInstance.m_sonList)
            {
                Add((MFrameUnit)son.Clone(this));
            }
        }
        public new MTimeLine GetParent()
        {
            if (parent == null || (!(parent is MTimeLine)))
            {
                return null;
            }
            return (MTimeLine)parent;
        }
        //获取紧邻的下一个帧单元
        public MFrame GetNext()
        {
            MTimeLine timeLine = GetParent();
            if (timeLine == null)
            {
                return null;
            }
            int id = parent.GetSonID(this);
            return timeLine[id + 1];
        }
        //获取紧邻的前一个帧单元
        public MFrame GetBefore()
        {
            MTimeLine timeLine = GetParent();
            if (timeLine == null)
            {
                return null;
            }
            int id = parent.GetSonID(this);
            return timeLine[id - 1];
        }
        //检查是否是正常的补间帧
        public bool IsNormalMotion()
        {
            if (!hasMotion)
            {
                return false;
            }
            if (this.Count() > 1)
            {
                return false;
            }
            MFrame next = GetNext();
            if (next == null||next.Count() != 1)
            {
                return false;
            }
            return true;
        }
        //显示
        //public void display(Graphics g, int WindowH, int x, int y, float zoomLevel, float parentAlpha,float timePos)
        //{
        //    foreach (MFrameUnit son in sonList)
        //    {
        //        son.GdiDisplayLikeGL(g, WindowH, x, y, zoomLevel,  parentAlpha,timePos);
        //    }
        //}
        //带图片映射的中心对齐显示
        public void display(Graphics g, int x, int y, float zoomlevel, Rect limitRect, float alpha,float timePos,ObjectVector imgMapList)
        {
            if (this.Count() <= 0 || zoomlevel <= 0)
            {
                return;
            }
            //绘制到缓冲
            for (int i = 0; i < this.Count(); i++)
            {
                this[i].GdiDisplay(g, x, y, alpha, zoomlevel, timePos, imgMapList, limitRect);
            }

        }
        public void glDisplay(int x, int y, float zoomLevel, List<MFrameUnit> selectList, bool allowDrawAnchor, float timePos)
        {
            glDisplay(x, y, zoomLevel, selectList, allowDrawAnchor, 1.0f, timePos);
        }
        public void glDisplay(int x, int y, float zoomLevel, List<MFrameUnit> selectList, bool allowDrawAnchor, float parentAlpha,float timePos)
        {
            foreach (MFrameUnit son in m_sonList)
            {
                son.GlDisplay(x, y, zoomLevel, (selectList != null && selectList.Contains(son)) && allowDrawAnchor, parentAlpha, timePos);
            }
        }
        public override void ReadObject(System.IO.Stream s)
        {
            timeBegin=IOUtil.readInt(s);
            timeLast=IOUtil.readInt(s);
            frameType = (MFrameType)IOUtil.readInt(s);
            hasMotion=IOUtil.readBoolean(s);
            name = IOUtil.readString(s);
            short len;
            m_sonList.Clear();
            len = IOUtil.readShort(s);
            for (short i = 0; i < len; i++)
            {
                MFrameUnit elem=null;
                MFrameUnit.UnitType type = (MFrameUnit.UnitType)IOUtil.readInt(s);
                if (type == MFrameUnit.UnitType.UnitType_Bitmap)
                {
                    elem = new MFrameUnit_Bitmap();
                }
                else if (type == MFrameUnit.UnitType.UnitType_MC)
                {
                    elem = new MFrameUnit_MC();
                }
                elem.unitType = type;
                elem.SetParent(this);
                elem.ReadObject(s);
                m_sonList.Add(elem);
            }
        }
        public override void WriteObject(System.IO.Stream s)
        {
            IOUtil.writeInt(s, timeBegin);
            IOUtil.writeInt(s, timeLast);
            IOUtil.writeInt(s, (int)frameType);
            IOUtil.writeBoolean(s, hasMotion);

            IOUtil.writeString(s, name);
            short len = (short)m_sonList.Count;
            IOUtil.writeShort(s, len);
            for (short i = 0; i < len; i++)
            {
                MFrameUnit elem = m_sonList[i];
                IOUtil.writeInt(s, (int)elem.unitType);
                elem.WriteObject(s);
            }
        }
        public override void ExportObject(System.IO.Stream s)
        {
            IOUtil.writeInt(s, timeBegin);
            IOUtil.writeInt(s, timeLast);
            IOUtil.writeInt(s, (int)frameType);
            IOUtil.writeBoolean(s, hasMotion);

            short len = (short)m_sonList.Count;
            IOUtil.writeShort(s, len);
            for (short i = 0; i < len; i++)
            {
                MFrameUnit elem = m_sonList[i];
                IOUtil.writeInt(s, (int)elem.unitType);
                elem.ExportObject(s);
            }
        }
        //获得包围框
        public virtual RectangleF getBox(int timePos)
        {
            List<RectangleF> rects =new List<RectangleF>();
            for (int i = 0; i < this.Count(); i++)
            {
                RectangleF rectI = this[i].getTransformBox(timePos);
                rects.Add(rectI);
            }
            RectangleF all = MathUtil.getRectsBox(rects);
            return all;
        }
    }
    /// <summary>
    /// 帧内容(可以是位图动作切片、矢量图形、库元素等)
    /// </summary>
    public class MFrameUnit : MIO, MParentNode, MSonNode
    {
        public enum UnitType
        {
            UnitType_Bitmap,
            UnitType_MC
        }
        protected static TextureImage imgUnitCenter = null;
        protected static TextureImage imgTransCenter = null;
        public UnitType unitType = UnitType.UnitType_Bitmap;
        public MFrameUnit()
        {
            for (int i = 0; i < pBorderCorner.Length; i++)
            {
                pBorderCorner[i] = new Point();
            }
        }
        //父结构索引
        public MFrame parent;
        public float posX = 0;//中心位置X坐标
        public float posY = 0;//中心位置Y坐标
        public float anchorX = 0.5f;//锚点X比率
        public float anchorY = 0.5f;//锚点Y比率
        public float rotateDegree=0.0f;//旋转角度(度数)
        public float scaleX = 1.0f;//水平方向缩放
        public float scaleY = 1.0f;//垂直方向缩放
        public float alpha = 1.0f;//透明度
        //public float shearX = 0.0f;//水平方向斜切
        //public float shearY = 0.0f;//垂直方向斜切

        public static Matrix4 mtrixGL = new Matrix4();//GL公共绘图矩阵【先自身缩放、接着自身斜切、接着自身旋转、再整体平移】
        public static Matrix mtrixSys = new Matrix();//系统公共绘图矩阵【先自身缩放、接着自身斜切、接着自身旋转、再整体平移】
        public PointF[] pBorderCorner = new PointF[4];//变形辅助点

        public bool isVisible = true;//是否可见(用作辅助编辑)
        public bool isLocked = false;//是否锁定(用作辅助编辑)

        public int GetID()
        {
            if (parent == null)
            {
                return -1;
            }
            return parent.GetSonID(this);
        }
        public virtual void ReadObject(Stream s)
        {
            posX = IOUtil.readFloat(s);
            posY = IOUtil.readFloat(s);
            anchorX = IOUtil.readFloat(s);
            anchorY = IOUtil.readFloat(s);
            rotateDegree = IOUtil.readFloat(s);
            scaleX = IOUtil.readFloat(s);
            scaleY = IOUtil.readFloat(s);
            alpha = IOUtil.readFloat(s);
            isVisible = IOUtil.readBoolean(s);
            isLocked = IOUtil.readBoolean(s);
        }

        public virtual void WriteObject(Stream s)
        {
            IOUtil.writeFloat(s, posX);
            IOUtil.writeFloat(s, posY);
            IOUtil.writeFloat(s, anchorX);
            IOUtil.writeFloat(s, anchorY);
            IOUtil.writeFloat(s, rotateDegree);
            IOUtil.writeFloat(s, scaleX);
            IOUtil.writeFloat(s, scaleY);
            IOUtil.writeFloat(s, alpha);
            IOUtil.writeBoolean(s, isVisible);
            IOUtil.writeBoolean(s, isLocked);
        }
        public virtual void ExportObject(Stream s)
        {
            IOUtil.writeFloat(s, posX);
            IOUtil.writeFloat(s, posY);
            IOUtil.writeFloat(s, anchorX);
            IOUtil.writeFloat(s, anchorY);
            IOUtil.writeFloat(s, rotateDegree);
            IOUtil.writeFloat(s, scaleX);
            IOUtil.writeFloat(s, scaleY);
            IOUtil.writeFloat(s, alpha);
        }
        public virtual int GetSonID(MSonNode son)
        {
            throw new Exception("can't use this method");
        }
        #region MSonNode 成员

        public MParentNode GetParent()
        {
            return parent;
        }
        public void SetParent(MParentNode parentT)
        {
            parent = (MFrame)parentT;
        }
        public MParentNode GetTopParent()
        {
            MParentNode parent = GetParent();
            if (parent != null)
            {
                if (!(parent is MSonNode))
                {
                    Console.WriteLine("error");
                }
                while (((MSonNode)parent).GetParent() != null)
                {
                    parent = ((MSonNode)parent).GetParent();
                    if (!(parent is MSonNode))
                    {
                        Console.WriteLine("error");
                    }
                }
            }
            return parent;
        }
        #endregion

        public virtual MFrameUnit Clone()
        {
            return Clone(parent);
        }
        public virtual MFrameUnit Clone(MFrame parent)
        {
            throw new Exception("can't use this method");
        }
        public virtual MFrameUnit CloneAtTime()
        {
            throw new Exception("can't use this method");
        }
        //左上角为原点，使用GDI绘图，左上角对齐到目标位置
        public virtual void GdiDisplay(Graphics g, float x, float y, float zoomLevel, float timePos)
        {
            throw new Exception("can't use this method");
        }
        //左上角为原点，使用GDI绘图，中心对齐到目标位置（带映射表）
        public virtual void GdiDisplay(Graphics g, float x, float y, float parentAlpha, float zoomLevel, float timePos, ObjectVector imgMapList,Rect limitRect)
        {
            throw new Exception("can't use this method");
        }
        ////左下角为原点，使用GDI绘图
        //public virtual void GdiDisplayLikeGL(Graphics g, int WindowH, int x, int y, float zoomLevel, float parentAlpha, float timePos)
        //{
        //    throw new Exception("can't use this method");
        //}
        //
        // 左下角为原点，使用OpenGL绘图，绘制时间轴上指定的一帧
        //
        // 参数:
        //   xScreen: 绘制到屏幕目标位置的X坐标
        //   yScreen: 绘制到屏幕目标位置的Y坐标
        //   zoomScreen: 绘制到屏幕时叠加的缩放比率
        //   withBorder:  是否绘制边缘
        //   parentAlpha: 父类的透明度
        //   timePos: 位于时间轴上的位置
        public virtual void GlDisplay(int xScreen, int yScreen, float zoomScreen, bool withBorder, float parentAlpha, float timePos)
        {
            throw new Exception("can't use this method");
        }
        //public virtual void displayBorder(Graphics g, int x, int y, float zoomLevel)
        //{
        //    throw new Exception("can't use this method");
        //}
        public virtual bool hitRegion(float xOffsetUI, float yOffsetUI, float zoomLevel, int xMouse, int yMouse, float timePos)
        {
            throw new Exception("can't use this method");
        }
        public virtual bool hitRegion(float xOffsetUI, float yOffsetUI, float zoomLevel, Rectangle selectRect, float timePos)
        {
            throw new Exception("can't use this method");
        }
        //原始大小
        public virtual SizeF getSize()
        {
            throw new Exception("can't use this method");
        }
        public Operation_Transform getOpreTransForm()
        {
            SizeF size = getSize();
            Operation_Transform transform = new Operation_Transform();
            ValueTransform vt = getValueTransform(Form_MTimeLine.timePosition);
            transform.size = size;
            transform.setValue(ref vt);
            return transform;
        }
        public void applyOpreTransForm(Operation_Transform transform)
        {
            posX = transform.posX;
            posY = transform.posY;
            anchorX = transform.anchorX;
            anchorY = transform.anchorY;
            rotateDegree = transform.rotateDegree;
            scaleX = transform.scaleX;
            scaleY = transform.scaleY;
            //shearX = transform.shearX;
            //shearY = transform.shearY;
        }
        //获得变形框
        public RectangleF getTransformBox()
        {
            return getTransformBox(Form_MTimeLine.timePosition);
        }
        public RectangleF getTransformBox(int timePos)
        {
            SizeF size = getSize();
            ValueTransform transform = getValueTransform(timePos);
            genMatrixGL_NT(ref transform);
            mtrixGL.postTranslate(transform.pos.X, transform.pos.Y);
            setCorner(pBorderCorner, size, transform.anchor.X, transform.anchor.Y);
            mtrixGL.TransformPoints(pBorderCorner);
            for (int i = 0; i < pBorderCorner.Length; i++)
            {
                if (pBorderCorner[i].X > Int32.MaxValue || pBorderCorner[i].X < Int32.MinValue)
                {
                    Console.WriteLine("error");
                }
                if (pBorderCorner[i].Y > Int32.MaxValue || pBorderCorner[i].Y < Int32.MinValue)
                {
                    Console.WriteLine("error");
                }
            }
            return MathUtil.getPointsBox(pBorderCorner);
        }
        //获得变形大小
        public SizeF getTransformSize(float zoomLevel)
        {
            SizeF size = getSize();
            ValueTransform transform = getValueTransform(Form_MTimeLine.timePosition);
            genMatrixGL_NT(ref transform);
            mtrixGL.postScale(zoomLevel, zoomLevel);
            mtrixGL.postTranslate(transform.pos.X, transform.pos.Y);
            setCorner(pBorderCorner, size, transform.anchor.X, transform.anchor.Y);
            mtrixGL.TransformPoints(pBorderCorner);
            for (int i = 0; i < pBorderCorner.Length; i++)
            {
                if (pBorderCorner[i].X > Int32.MaxValue || pBorderCorner[i].X < Int32.MinValue)
                {
                    Console.WriteLine("error");
                }
                if (pBorderCorner[i].Y > Int32.MaxValue || pBorderCorner[i].Y < Int32.MinValue)
                {
                    Console.WriteLine("error");
                }
            }
            RectangleF rect = MathUtil.getPointsBox(pBorderCorner);
            lastTransformSize.Width = rect.Width;
            lastTransformSize.Height = rect.Height;
            return lastTransformSize;
        }
        //获得上次变形大小
        private SizeF lastTransformSize = new SizeF();
        public SizeF getLastTransformSize()
        {
            return lastTransformSize;
        }
        //生成OpenGL变形矩阵(不含位移信息)
        protected void genMatrixGL_NT()
        {
            mtrixGL.identity();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixGL.preRotate(MathUtil.degreesToRadians(rotateDegree));
            mtrixGL.preScale(scaleX, scaleY);
        }
        protected void genMatrixGL_NT(ref ValueTransform transform)
        {
            mtrixGL.identity();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixGL.preRotate(MathUtil.degreesToRadians(transform.rotateDegree));
            mtrixGL.preScale(transform.scale.X, transform.scale.Y);
        }
        //生成系统变形矩阵(不含位移和锚点信息，即左上角对齐)
        protected void genMatrixSys_NTA()
        {
            mtrixSys.Reset();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixSys.Rotate(rotateDegree,MatrixOrder.Prepend);
            mtrixSys.Scale(scaleX, scaleY, MatrixOrder.Prepend);
        }
        protected void genMatrixSys_NTA(ref ValueTransform transform)
        {
            mtrixSys.Reset();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixSys.Rotate(transform.rotateDegree, MatrixOrder.Prepend);
            mtrixSys.Scale(transform.scale.X, transform.scale.Y, MatrixOrder.Prepend);
        }
        //生成中心对齐的系统变形矩阵(含位移和锚点信息)
        protected void genMatrixSys_CT()
        {
            mtrixSys.Reset();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixSys.Rotate(rotateDegree, MatrixOrder.Prepend);
            mtrixSys.Scale(scaleX, scaleY, MatrixOrder.Prepend);
            SizeF size = getSize();
            mtrixSys.Translate((0.5f - anchorX) * size.Width, (0.5f - anchorY) * size.Height, MatrixOrder.Prepend);
            mtrixSys.Translate(posX, posY, MatrixOrder.Append);
        }
        protected void genMatrixSys_CT(ref ValueTransform transform)
        {
            mtrixSys.Reset();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixSys.Rotate(transform.rotateDegree, MatrixOrder.Prepend);
            mtrixSys.Scale(transform.scale.X, transform.scale.Y, MatrixOrder.Prepend);
            SizeF size = getSize();
            mtrixSys.Translate((0.5f-transform.anchor.X) * size.Width,  (0.5f-transform.anchor.Y) * size.Height, MatrixOrder.Prepend);
            mtrixSys.Translate(transform.pos.X, transform.pos.Y, MatrixOrder.Append);
        }
        //生成OpenGL变形矩阵(包含所有信息)
        protected void genMatrixGL()
        {
            mtrixGL.identity();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixGL.preRotate(MathUtil.degreesToRadians(rotateDegree));
            mtrixGL.preScale(scaleX, scaleY);
            SizeF size = getSize();
            mtrixGL.preTranslate(-anchorX * size.Width, -anchorY * size.Height);
            mtrixGL.postTranslate(posX, posY);
        }
        //生成指定参数的OpenGL变形矩阵(包含所有信息)
        protected void genMatrixGL(float rotateDegree, ref ValueFloat2 scale, ref ValueFloat2 anchor, ref ValueFloat2 pos)
        {
            mtrixGL.identity();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixGL.preRotate(MathUtil.degreesToRadians(rotateDegree));
            mtrixGL.preScale(scale.X, scale.Y);
            SizeF size = getSize();
            mtrixGL.preTranslate(-anchor.X * size.Width, -anchor.Y * size.Height);
            mtrixGL.postTranslate(pos.X, pos.Y);
        }
        //生成系统变形矩阵(包含所有信息)
        //protected void genMatrixAllSys()
        //{
        //    mtrixSys.Reset();
        //    //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
        //    mtrixSys.Rotate(rotateDegree, MatrixOrder.Prepend);
        //    mtrixSys.Scale(scaleX, scaleY, MatrixOrder.Prepend);
        //    SizeF size = getSize();
        //    mtrixSys.Translate(-anchorX * size.Width, (1-anchorY) * size.Height, MatrixOrder.Prepend);
        //    mtrixSys.Translate(posX, -posY, MatrixOrder.Append);
        //}
        //生成指定参数的系统变形矩阵(包含所有信息)
        protected void genMatrixAllSys(float rotateDegree, ValueFloat2 scale, ValueFloat2 anchor, ValueFloat2 pos)
        {
            mtrixSys.Reset();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixSys.Rotate(rotateDegree, MatrixOrder.Prepend);
            mtrixSys.Scale(scale.X, scale.Y, MatrixOrder.Prepend);
            SizeF size = getSize();
            mtrixSys.Translate(-anchor.X * size.Width, (1 - anchor.Y) * size.Height, MatrixOrder.Prepend);
            mtrixSys.Translate(pos.X, -pos.Y, MatrixOrder.Append);
        }
        //
        // 根据指定的时间轴位置，获取该单元当时的变形信息
        //
        // 参数:
        //   timePos: 位于时间轴上的位置
        public ValueTransform getValueTransform(float timePos)
        {
            MFrame headFrame = parent;
            MFrame endFrame = null;
            if (headFrame != null)
            {
               endFrame = headFrame.GetNext();
            }
            ValueTransform transform=new ValueTransform();
            if (headFrame == null || !headFrame.IsNormalMotion() || timePos < headFrame.timeBegin || endFrame==null)
            {
                transform.setValue(this);
            }
            else
            {
                if (timePos >= endFrame.timeBegin)
                {
                    transform.setValue(endFrame[0]);
                }
                else
                {
                    float transit = (timePos - headFrame.timeBegin) * 1.0f / (endFrame.timeBegin - headFrame.timeBegin);
                    transform.rotateDegree = headFrame[0].rotateDegree + (endFrame[0].rotateDegree - headFrame[0].rotateDegree) * transit;
                    transform.alpha = headFrame[0].alpha + (endFrame[0].alpha - headFrame[0].alpha) * transit;
                    transform.scale.X = headFrame[0].scaleX + (endFrame[0].scaleX - headFrame[0].scaleX) * transit;
                    transform.scale.Y = headFrame[0].scaleY + (endFrame[0].scaleY - headFrame[0].scaleY) * transit;
                    transform.anchor.X = headFrame[0].anchorX + (endFrame[0].anchorX - headFrame[0].anchorX) * transit;
                    transform.anchor.Y = headFrame[0].anchorY + (endFrame[0].anchorY - headFrame[0].anchorY) * transit;
                    transform.pos.X = headFrame[0].posX + (endFrame[0].posX - headFrame[0].posX) * transit;
                    transform.pos.Y = headFrame[0].posY + (endFrame[0].posY - headFrame[0].posY) * transit;
                }
            }
            return transform;
        }
        //
        // 设置当前单元的变形数值
        //
        // 参数:
        //   transform: 变形数值
        public void setValueTransform(ref ValueTransform transform)
        {
            rotateDegree = transform.rotateDegree;
            alpha = transform.alpha;
            scaleX = transform.scale.X;
            scaleY = transform.scale.Y;
            anchorX = transform.anchor.X;
            anchorY = transform.anchor.Y;
            posX = transform.pos.X;
            posY = transform.pos.Y;
        }
        //设置四角点
        public static void setCorner(PointF[] pBorderCorner, SizeF size, float anchorX, float anchorY)
        {
            pBorderCorner[0].X = -size.Width * anchorX;
            pBorderCorner[0].Y = -size.Height * anchorY;
            pBorderCorner[1].X = -size.Width * anchorX;
            pBorderCorner[1].Y = size.Height * (1 - anchorY);
            pBorderCorner[2].X = size.Width * (1 - anchorX);
            pBorderCorner[2].Y = size.Height * (1 - anchorY);
            pBorderCorner[3].X = size.Width * (1 - anchorX);
            pBorderCorner[3].Y = -size.Height * anchorY;
        }
        //重置变形参数
        public void resetTransform()
        {
            scaleX = (float)1.0f;
            scaleY = (float)1.0f;
            rotateDegree = (float)0.0f;
            //shearX = (float)0.0f;
            //shearY = (float)0.0f;
            anchorX = (float)0.5f;
            anchorY = (float)0.5f;
        }

        #region MSonNode 成员


        public string getValueToLenString()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
    /// <summary>
    /// 帧内容-位图动作切片
    /// </summary>
    public class MFrameUnit_Bitmap : MFrameUnit
    {

        public MClipElement clipElement = null;//基础切片索引
        #region MIO 成员

        public override void ReadObject(Stream s)
        {
            int id = IOUtil.readInt(s);
            if (id<0)
            {
                id = 0;
                Console.WriteLine("error");
            }
            clipElement = (MClipElement)(((Form_MAnimation)(GetTopParent())).form_MImgsList.MClipsManager[id]);
            base.ReadObject(s);
        }

        public override void WriteObject(Stream s)
        {
            IOUtil.writeInt(s, clipElement.GetID());
            base.WriteObject(s);
        }
        public override void ExportObject(Stream s)
        {
            if (clipElement == null)
            {
                Console.WriteLine("error");
            }
            else
            {
                IOUtil.writeShort(s, (short)(clipElement.GetID()));
            }
            base.ExportObject(s);
        }
        #endregion

        public override int GetSonID(MSonNode son)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override MFrameUnit CloneAtTime()
        {
            MFrameUnit_Bitmap newInstance = new MFrameUnit_Bitmap();
            newInstance.parent = parent;
            newInstance.clipElement = clipElement;
            ValueTransform value = getValueTransform(Form_MTimeLine.timePosition);
            newInstance.setValueTransform(ref value);
            return newInstance;
        }
        public override MFrameUnit Clone(MFrame parent)
        {
            MFrameUnit_Bitmap newInstance = new MFrameUnit_Bitmap();
            newInstance.parent = parent;
            newInstance.clipElement = clipElement;
            newInstance.posX = posX;
            newInstance.posY = posY;
            newInstance.anchorX = anchorX;
            newInstance.anchorY = anchorY;
            newInstance.rotateDegree = rotateDegree;
            newInstance.scaleX = scaleX;
            newInstance.scaleY = scaleY;
            newInstance.alpha = alpha;
            //newInstance.shearX = shearX;
            //newInstance.shearY = shearY;
            return newInstance;
        }
        //左上角为原点，使用GDI绘图
        public override void GdiDisplay(Graphics g, float x, float y, float zoomLevel, float timePos)
        {
            ValueTransform transform = getValueTransform(timePos);
            float currentAlpha = transform.alpha;
            if (transform.scale.X == 0 || transform.scale.Y == 0 || clipElement==null)
            {
                return;
            }
            genMatrixSys_NTA(ref transform);
            Rectangle rectS = clipElement.clipRect;
            mtrixSys.Scale(zoomLevel, zoomLevel, MatrixOrder.Append);
            mtrixSys.Translate(x, y, MatrixOrder.Append);
            GraphicsState gState = g.Save();
            g.ResetTransform();
            g.ResetClip();
            g.Transform = mtrixSys;
            if (Consts.textureLinear)
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            }
            else
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            }
            ImageAttributes imageAttNow = GraphicsUtil.getImageAttributes(currentAlpha);
            if (scaleX != 0.0f && scaleY != 0.0f)
            {
                try
                {
                    Rectangle rectD = new Rectangle(-rectS.Width / 2, -rectS.Height / 2, rectS.Width, rectS.Height);
                    PointF ulCorner1 = new PointF(-rectS.Width / 2.0f, -rectS.Height / 2.0f);
                    PointF urCorner1 = new PointF(rectS.Width / 2.0f, -rectS.Height / 2.0f);
                    PointF llCorner1 = new PointF(-rectS.Width / 2.0f, rectS.Height / 2.0f);
                    PointF[] destPara1 = { ulCorner1, urCorner1, llCorner1 };

                    rectS.X = 0;
                    rectS.Y = 0;
                    g.DrawImage(clipElement.imgClip, destPara1, rectS, GraphicsUnit.Pixel, imageAttNow);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            g.Restore(gState);
        }
        //中心为原点，使用GDI绘图(带图片映射）
        public override void GdiDisplay(Graphics g, float x, float y, float parentAlpha, float zoomLevel, float timePos, ObjectVector imgMapList, Rect limitRect)
        {
            ValueTransform transform = getValueTransform(timePos);
            if (transform.scale.X == 0 || transform.scale.Y == 0)
            {
                return;
            }
            genMatrixSys_CT(ref transform);
            Rectangle rectS = clipElement.clipRect;
            mtrixSys.Scale(zoomLevel, zoomLevel, MatrixOrder.Append);
            mtrixSys.Translate(x, y, MatrixOrder.Append);
            GraphicsState gState = g.Save();
            g.ResetTransform();
            g.ResetClip();
            if (limitRect != null)
            {
                g.SetClip(new RectangleF(limitRect.X, limitRect.Y, limitRect.W, limitRect.H));
            }
            g.Transform = mtrixSys;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            ImageAttributes imageAttNow = GraphicsUtil.getImageAttributes(alpha * parentAlpha);
            if (scaleX != 0.0f && scaleY != 0.0f)
            {
                try
                {
                    Rectangle rectD = new Rectangle(-rectS.Width / 2, -rectS.Height / 2, rectS.Width, rectS.Height);
                    MImgElement imgElementMapped = null;
                    if (imgMapList != null)
                    {
                        imgElementMapped = MClipElement.getMappedImage(imgMapList, clipElement.imageElement);
                    }
                    if (imgElementMapped == null || imgElementMapped.Equals(clipElement.imageElement))
                    {
                        rectS.X = 0;
                        rectS.Y = 0;
                        g.DrawImage(clipElement.imgClip, rectD, rectS.X, rectS.Y, rectS.Width, rectS.Height, GraphicsUnit.Pixel,imageAttNow);
                    }
                    else
                    {
                        g.DrawImage(imgElementMapped.image, rectD, rectS.X, rectS.Y, rectS.Width, rectS.Height, GraphicsUnit.Pixel,imageAttNow);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            g.Restore(gState);
        }

        //public override void GdiDisplayLikeGL(Graphics g, int WindowH, int xOffsetUI, int yOffsetUI, float zoomLevel, float parentAlpha, float timePos)
        //{
        //    MFrame headFrame = (MFrame)parent;
        //    if (!headFrame.IsNormalMotion())
        //    {
        //        genMatrixAllSys();
        //        Rectangle rectS = clipElement.clipRect;
        //        rectS.X = 0;
        //        rectS.Y = 0;
        //        mtrixSys.Scale(zoomLevel, zoomLevel, MatrixOrder.Append);
        //        mtrixSys.Translate(xOffsetUI, -yOffsetUI, MatrixOrder.Append);
        //        mtrixSys.Translate(0, WindowH, MatrixOrder.Append);
        //        GraphicsState gState = g.Save();
        //        g.ResetTransform();
        //        g.ResetClip();
        //        g.Transform = mtrixSys;
        //        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
        //        float newAlpha = alpha * parentAlpha;
        //        ImageAttributes imageAttNow = GraphicsUtil.getImageAttributes(newAlpha);
        //        if (scaleX != 0.0f && scaleY != 0.0f)
        //        {
        //            try
        //            {
        //                Rectangle rectD = new Rectangle(0, -rectS.Height, rectS.Width, rectS.Height);
        //                g.DrawImage(clipElement.imgClip, rectD, rectS.X, rectS.Y, rectS.Width, rectS.Height, GraphicsUnit.Pixel, imageAttNow);

        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e.Message);
        //            }
        //        }
        //        g.Restore(gState);
        //    }
        //    else
        //    {
        //        MFrame endFrame = headFrame.GetNext();
        //        float rotateDegree, alpha;
        //        ValueFloat2 scale, anchor, position;
        //        if (endFrame == null || endFrame.Count() == 0)
        //        {
        //            rotateDegree = headFrame[0].rotateDegree;
        //            alpha = headFrame[0].alpha;
        //            scale = new ValueFloat2(headFrame[0].scaleX, headFrame[0].scaleY);
        //            anchor = new ValueFloat2(headFrame[0].anchorX, headFrame[0].anchorY);
        //            position = new ValueFloat2(headFrame[0].posX, headFrame[0].posY);
        //        }
        //        else
        //        {
        //            float transit = (timePos - headFrame.timeBegin) * 1.0f / (endFrame.timeBegin - headFrame.timeBegin);
        //            rotateDegree = headFrame[0].rotateDegree + (endFrame[0].rotateDegree - headFrame[0].rotateDegree) * transit;
        //            alpha = headFrame[0].alpha + (endFrame[0].alpha - headFrame[0].alpha) * transit;
        //            scale = new ValueFloat2();
        //            scale.X = headFrame[0].scaleX + (endFrame[0].scaleX - headFrame[0].scaleX) * transit;
        //            scale.Y = headFrame[0].scaleY + (endFrame[0].scaleY - headFrame[0].scaleY) * transit;
        //            anchor = new ValueFloat2();
        //            anchor.X = headFrame[0].anchorX + (endFrame[0].anchorX - headFrame[0].anchorX) * transit;
        //            anchor.Y = headFrame[0].anchorY + (endFrame[0].anchorY - headFrame[0].anchorY) * transit;
        //            position = new ValueFloat2();
        //            position.X = headFrame[0].posX + (endFrame[0].posX - headFrame[0].posX) * transit;
        //            position.Y = headFrame[0].posY + (endFrame[0].posY - headFrame[0].posY) * transit;
        //        }
        //        genMatrixAllSys(rotateDegree, scale, anchor, position);
        //        Rectangle rectS = clipElement.clipRect;
        //        rectS.X = 0;
        //        rectS.Y = 0;
        //        mtrixSys.Scale(zoomLevel, zoomLevel, MatrixOrder.Append);
        //        mtrixSys.Translate(xOffsetUI, -yOffsetUI, MatrixOrder.Append);
        //        mtrixSys.Translate(0, WindowH, MatrixOrder.Append);
        //        GraphicsState gState = g.Save();
        //        g.ResetTransform();
        //        g.ResetClip();
        //        g.Transform = mtrixSys;
        //        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
        //        float newAlpha = alpha * parentAlpha;
        //        ImageAttributes imageAttNow = GraphicsUtil.getImageAttributes(newAlpha);
        //        if (scaleX != 0.0f && scaleY != 0.0f)
        //        {
        //            try
        //            {
        //                Rectangle rectD = new Rectangle(0, -rectS.Height, rectS.Width, rectS.Height);
        //                g.DrawImage(clipElement.imgClip, rectD, rectS.X, rectS.Y, rectS.Width, rectS.Height, GraphicsUnit.Pixel, imageAttNow);

        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e.Message);
        //            }
        //        }
        //        g.Restore(gState);
        //    }

        //}
        //
        // 左下角为原点，使用OpenGL绘图，绘制时间轴上指定的一帧
        //
        // 参数:
        //   xScreen: 绘制到屏幕目标位置的X坐标
        //   yScreen: 绘制到屏幕目标位置的Y坐标
        //   zoomScreen: 绘制到屏幕时叠加的缩放比率
        //   withBorder:  是否绘制边缘
        //   parentAlpha: 父类的透明度
        //   timePos: 位于时间轴上的位置
        public override void GlDisplay(int xScreen, int yScreen, float zoomScreen, bool withBorder, float parentAlpha, float timePos)
        {
            if (clipElement == null)
            {
                return;
            }
            ValueTransform transform =  getValueTransform(timePos);
            genMatrixGL(transform.rotateDegree, ref transform.scale, ref transform.anchor, ref transform.pos);
            Rectangle rectS = clipElement.clipRect;
            mtrixGL.postScale(zoomScreen, zoomScreen);
            mtrixGL.postTranslate(xScreen, yScreen);
            OpenTK.Graphics.OpenGL.GL.PushMatrix();
            OpenTK.Graphics.OpenGL.GL.MultMatrix(mtrixGL.getValue());
            if (scaleX != 0.0f && scaleY != 0.0f && isVisible)
            {
                float newAlpha = transform.alpha * parentAlpha;
                RectangleF rectD = new RectangleF(0, 0, rectS.Width, rectS.Height);
                GLGraphics.drawTextureImage(clipElement.imageElement.ImageTextured, rectS, rectD, null, newAlpha);
                if (withBorder)
                {
                    //绘制选中边框
                    GLGraphics.setRGBColor(0x00A8FF);
                    GLGraphics.drawRect(0, 0, rectS.Width, rectS.Height,true);
                }
            }
            OpenTK.Graphics.OpenGL.GL.PopMatrix();
            if (withBorder)
            {
                mtrixGL.identity();
                mtrixGL.postTranslate(transform.pos.X, transform.pos.Y);
                mtrixGL.postScale(zoomScreen, zoomScreen);
                mtrixGL.postTranslate(xScreen, yScreen);
                OpenTK.Graphics.OpenGL.GL.PushMatrix();
                OpenTK.Graphics.OpenGL.GL.MultMatrix(mtrixGL.getValue());
                //绘制中心图片
                if (imgUnitCenter == null)
                {
                    Bitmap imgCenterT = global::Cyclone.Properties.Resources.unitCenter;
                    imgUnitCenter = ConstTextureImgs.createImage(imgCenterT);
                }
                GLGraphics.drawTextureImage(imgUnitCenter, new RectangleF(0, 0, imgUnitCenter.TextureWidth, imgUnitCenter.TextureHight),
                     new RectangleF(-5, -5, 10, 10), null, 1.0f);
                OpenTK.Graphics.OpenGL.GL.PopMatrix();
            }
        }
        public override bool hitRegion(float xOffsetUI, float yOffsetUI, float zoomLevel, int xMouse, int yMouse, float timePos)
        {
            if (clipElement==null)
            {
                return false;
            }
            ValueTransform transform = getValueTransform(timePos);
            genMatrixGL_NT(ref transform);
            Rectangle rs = clipElement.clipRect;
            int destW = (int)(rs.Width * zoomLevel);
            int destH = (int)(rs.Height * zoomLevel);
            int destX = (int)(xOffsetUI + transform.pos.X * zoomLevel);
            int destY = (int)(yOffsetUI + transform.pos.Y * zoomLevel);
            mtrixGL.preScale(zoomLevel, zoomLevel);
            mtrixGL.postTranslate(destX, destY);
            setCorner(pBorderCorner, new SizeF(rs.Width, rs.Height), transform.anchor.X, transform.anchor.Y);
            mtrixGL.TransformPoints(pBorderCorner);
            return MathUtil.pointInRegion(new PointF(xMouse, yMouse), pBorderCorner);
        }
        public static PointF[] pBorderRect = new PointF[] { new PointF(), new PointF(), new PointF(), new PointF() };    //矩形选框辅助点
        public override bool hitRegion(float xOffsetUI, float yOffsetUI, float zoomLevel, Rectangle selectRect, float timePos)
        {
            if (clipElement == null)
            {
                return false;
            }
            ValueTransform transform = getValueTransform(timePos);
            genMatrixGL_NT(ref transform);
            Rectangle rs = clipElement.clipRect;
            int destW = (int)(rs.Width * zoomLevel);
            int destH = (int)(rs.Height * zoomLevel);
            int destX = (int)(xOffsetUI + transform.pos.X * zoomLevel);
            int destY = (int)(yOffsetUI + transform.pos.Y * zoomLevel);
            mtrixGL.postScale(zoomLevel, zoomLevel);
            mtrixGL.postTranslate(destX, destY);
            setCorner(pBorderCorner, new SizeF(rs.Width, rs.Height), transform.anchor.X, transform.anchor.Y);
            mtrixGL.TransformPoints(pBorderCorner);

            pBorderRect[0].X = selectRect.X;
            pBorderRect[0].Y = selectRect.Y;
            pBorderRect[1].X = selectRect.X + selectRect.Width;
            pBorderRect[1].Y = selectRect.Y;
            pBorderRect[2].X = selectRect.X + selectRect.Width;
            pBorderRect[2].Y = selectRect.Y + selectRect.Height;
            pBorderRect[3].X = selectRect.X;
            pBorderRect[3].Y = selectRect.Y + selectRect.Height;

            if (MathUtil.pointInRegion(pBorderRect[0], pBorderCorner) ||
                MathUtil.pointInRegion(pBorderRect[1], pBorderCorner) ||
                MathUtil.pointInRegion(pBorderRect[2], pBorderCorner) ||
                MathUtil.pointInRegion(pBorderRect[3], pBorderCorner))
            {
                return true;
            }
            if (MathUtil.pointInRegion(pBorderCorner[0], pBorderRect) ||
                MathUtil.pointInRegion(pBorderCorner[1], pBorderRect) ||
                MathUtil.pointInRegion(pBorderCorner[2], pBorderRect) ||
                MathUtil.pointInRegion(pBorderCorner[3], pBorderRect))
            {
                return true;
            }
            return false;
        }

        public override SizeF getSize()
        {
            if (clipElement != null && clipElement.clipRect != null)
            {
                return new SizeF(clipElement.clipRect.Width, clipElement.clipRect.Height);
            }
            else
            {
                return new SizeF(1, 1);
            }
        }


    }
    /// <summary>
    /// 帧内容-影片剪辑
    /// </summary>
    public class MFrameUnit_MC : MFrameUnit
    {
        public MLib_MovieClip movieClip;

        public override void ExportObject(Stream s)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int GetSonID(MSonNode son)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override MFrameUnit Clone(MFrame parent)
        {
            MFrameUnit_MC newInstance = new MFrameUnit_MC();
            newInstance.movieClip = movieClip;
            return newInstance;
        }

        public override bool hitRegion(float xOffsetUI, float yOffsetUI, float zoomLevel, int xMouse, int yMouse, float timePos)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override bool hitRegion(float xOffsetUI, float yOffsetUI, float zoomLevel, Rectangle selectRect, float timePos)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override SizeF getSize()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
    /// <summary>
    /// 变形操作
    /// </summary>
    public class Operation_Transform : MFrameUnit
    {
        //public float shearX = 0.0f;//水平方向斜切
        //public float shearY = 0.0f;//垂直方向斜切
        public SizeF size = new SizeF(1,1);//基础切片索引
        public PointF[] pBorderCenter = new PointF[4];//变形辅助点-中间点
        //获取位置中心
        public PointF getCenter(int xOffsetUI, int yOffsetUI, float zoomLevel)
        {
            genTransformMatrix(zoomLevel);
            int destX = (int)(xOffsetUI + posX * zoomLevel);
            int destY = (int)(yOffsetUI + posY * zoomLevel);
            mtrixGL.postTranslate(destX, destY);
            PointF[] pCenter = new PointF[1];
            pCenter[0] = new PointF(0, 0);
            mtrixGL.TransformPoints(pCenter);
            return pCenter[0];
            //return new PointF((pBorderCorner[0].X + pBorderCorner[2].X) / 2, (pBorderCorner[0].Y + pBorderCorner[2].Y) / 2);
        }
        //获得形状的中心
        public PointF getCenter()
        {
            return new PointF((pBorderCorner[0].X + pBorderCorner[2].X) / 2, (pBorderCorner[0].Y + pBorderCorner[2].Y) / 2);
        }
        //获取位置坐标
        public PointF getPosition()
        {
            PointF ps = new PointF(posX, posY);
            return ps;
        }
        //获取变换后的X方向边缘中点
        public PointF getTransedXBCenter()
        {
            Matrix4 mtrixAll = new Matrix4();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixAll.preRotate(MathUtil.degreesToRadians(rotateDegree));
            mtrixAll.preScale(scaleX, scaleY);
            mtrixAll.preTranslate(size.Width * (anchorX - 0.5f), size.Height * (anchorY - 0.5f));
            mtrixAll.postTranslate(posX, posY);

            PointF[] pXBC = new PointF[1];
            pXBC[0] = new PointF(size.Width / 2.0f, 0);
            mtrixAll.TransformPoints(pXBC);
            return pXBC[0];
        }
        //获取变换后的中心
        public PointF getTransedCenter()
        {
            Matrix4 mtrixAll = new Matrix4();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixAll.preRotate(MathUtil.degreesToRadians( rotateDegree));
            mtrixAll.preScale(scaleX, scaleY);
            mtrixAll.preTranslate(size.Width * (anchorX - 0.5f), size.Height * (anchorY - 0.5f));
            mtrixAll.postTranslate(posX, posY);

            PointF[] pC = new PointF[1];
            pC[0] = new PointF(0, 0);
            mtrixAll.TransformPoints(pC);
            return pC[0];
        }
        //获取位置坐标的世界坐标
        public PointF getPosition(int xOffsetUI, int yOffsetUI, float zoomLevel)
        {
            genTransformMatrix(zoomLevel);
            int destX = (int)(xOffsetUI + posX * zoomLevel);
            int destY = (int)(yOffsetUI + posY * zoomLevel);
            mtrixGL.postTranslate(destX, destY);
            PointF[] ps = new PointF[1];
            ps[0] = new PointF(0,0);
            mtrixGL.TransformPoints(ps);
            if (ps[0].X == float.NaN || ps[0].Y == float.NaN)
            {
                Console.WriteLine("error");
            }
            return ps[0];
        }
        //根据世界坐标设置新的位置
        public void setPosition(PointF posNew,int xOffsetUI, int yOffsetUI, float zoomLevel)
        {
            posX = (posNew.X - xOffsetUI) / zoomLevel;
            posY = (posNew.Y - yOffsetUI) / zoomLevel;
        }
        //将当前矩阵的逆反矩阵应用于指定的顶点向量
        public PointF getInverseVector(PointF vector, float zoomLevel)
        {
            genTransformMatrix(zoomLevel);
            mtrixGL.inverse();
            PointF[] ps = new PointF[1];
            ps[0] = vector;
            mtrixGL.TransformPoints(ps);
            return ps[0];
        }
        //克隆
        public override MFrameUnit Clone()
        {
            Operation_Transform newInstance = new Operation_Transform();
            newInstance.posX = posX;
            newInstance.posY = posY;
            newInstance.anchorX = anchorX;
            newInstance.anchorY = anchorY;
            newInstance.rotateDegree = rotateDegree;
            newInstance.scaleX = scaleX;
            newInstance.scaleY = scaleY;
            //newInstance.shearX = shearX;
            //newInstance.shearY = shearY;
            newInstance.size = size;
            for (int i = 0; i < 4; i++)
            {
                newInstance.pBorderCorner[i] = pBorderCorner[i];
                newInstance.pBorderCenter[i] = pBorderCenter[i];
                if (pBorderCorner[i].X > Int32.MaxValue || pBorderCorner[i].X < Int32.MinValue)
                {
                    Console.WriteLine("error");
                }
                if (pBorderCorner[i].Y > Int32.MaxValue || pBorderCorner[i].Y < Int32.MinValue)
                {
                    Console.WriteLine("error");
                }
            }
            return newInstance;
        }
        //设置新的数值
        public void setValue(Operation_Transform otherInstance)
        {
            if (otherInstance == null)
            {
                return;
            }
            posX = otherInstance.posX;
            posY = otherInstance.posY;
            anchorX = otherInstance.anchorX;
            anchorY = otherInstance.anchorY;
            rotateDegree = otherInstance.rotateDegree;
            scaleX = otherInstance.scaleX;
            scaleY = otherInstance.scaleY;
            alpha = otherInstance.alpha;
            //shearX = otherInstance.shearX;
            //shearY = otherInstance.shearY;
            size = otherInstance.size;
            for (int i = 0; i < 4; i++)
            {
                pBorderCorner[i] = otherInstance.pBorderCorner[i];
                pBorderCenter[i] = otherInstance.pBorderCenter[i];
                if (pBorderCorner[i].X > Int32.MaxValue || pBorderCorner[i].X < Int32.MinValue)
                {
                    Console.WriteLine("error");
                }
                if (pBorderCorner[i].Y > Int32.MaxValue || pBorderCorner[i].Y < Int32.MinValue)
                {
                    Console.WriteLine("error");
                }
            }
        }
        public void setValue(ref ValueTransform vt)
        {
            posX = vt.pos.X;
            posY = vt.pos.Y;
            anchorX = vt.anchor.X;
            anchorY = vt.anchor.Y;
            rotateDegree = vt.rotateDegree;
            scaleX = vt.scale.X;
            scaleY = vt.scale.Y;
            alpha = vt.alpha;
        }
        //显示
        public void glDisplay(int xOffsetUI, int yOffsetUI, float zoomLevel,int color)
        {
            genAssistPoints(xOffsetUI, yOffsetUI, zoomLevel);
            //if (pen == null)
            //{
            //    pen = new Pen(GraphicsUtil.getColor(0), 1.0f);
            //}
            //g.ResetClip();
            //System.Drawing.Drawing2D.InterpolationMode i_mode = g.InterpolationMode;
            //System.Drawing.Drawing2D.PixelOffsetMode p_mode = g.PixelOffsetMode;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            GLGraphics.setRGBColor(color);

            GLGraphics.drawLine(pBorderCorner[0], pBorderCorner[1], true);
            GLGraphics.drawLine(pBorderCorner[1], pBorderCorner[2], true);
            GLGraphics.drawLine(pBorderCorner[2], pBorderCorner[3], true);
            GLGraphics.drawLine(pBorderCorner[3], pBorderCorner[0], true);
            int S1 = 9;
            int S2 = 7;
            for (int i = 0; i < pBorderCorner.Length; i++)
            {
                GLGraphics.setRGBColor(0xFFFFFF);
                GLGraphics.fillRect((pBorderCorner[i].X - S1 / 2.0f), (pBorderCorner[i].Y - S1 / 2.0f), S1, S1);
                GLGraphics.setRGBColor(0);
                GLGraphics.fillRect((pBorderCorner[i].X - S2 / 2.0f), (pBorderCorner[i].Y - S2 / 2.0f), S2, S2);
            }
            for (int i = 0; i < pBorderCenter.Length; i++)
            {
                GLGraphics.setRGBColor(0xFFFFFF);
                GLGraphics.fillRect((pBorderCenter[i].X - S1 / 2.0f), (pBorderCenter[i].Y - S1 / 2.0f), S1, S1);
                GLGraphics.setRGBColor(0);
                GLGraphics.fillRect((pBorderCenter[i].X - S2 / 2.0f), (pBorderCenter[i].Y - S2 / 2.0f), S2, S2);
            }
            PointF center = getPosition(xOffsetUI, yOffsetUI, zoomLevel);
            if (imgTransCenter == null)
            {
                Bitmap imgCenterT = global::Cyclone.Properties.Resources.transformCenter;
                imgTransCenter = ConstTextureImgs.createImage(imgCenterT);
            }
            GLGraphics.drawTextureImage(imgTransCenter, new RectangleF(0, 0, imgTransCenter.TextureWidth, imgTransCenter.TextureHight),
                new RectangleF(center.X - imgTransCenter.TextureWidth / 2.0f, center.Y - imgTransCenter.TextureHight / 2.0f, imgTransCenter.TextureWidth, imgTransCenter.TextureHight), null, 1.0f);
        }
        //生成辅助点
        public void genAssistPoints(int xOffsetUI, int yOffsetUI, float zoomLevel)
        {
            genTransformMatrix(zoomLevel);
            int destX = (int)(xOffsetUI + posX * zoomLevel);
            int destY = (int)(yOffsetUI + posY * zoomLevel);
            mtrixGL.postTranslate(destX, destY);
            MFrameUnit.setCorner(pBorderCorner, size, anchorX, anchorY);
            mtrixGL.TransformPoints(pBorderCorner);
            pBorderCenter[0].X = (pBorderCorner[0].X + pBorderCorner[1].X) / 2.0f;
            pBorderCenter[0].Y = (pBorderCorner[0].Y + pBorderCorner[1].Y) / 2.0f;
            pBorderCenter[1].X = (pBorderCorner[1].X + pBorderCorner[2].X) / 2.0f;
            pBorderCenter[1].Y = (pBorderCorner[1].Y + pBorderCorner[2].Y) / 2.0f;
            pBorderCenter[2].X = (pBorderCorner[2].X + pBorderCorner[3].X) / 2.0f;
            pBorderCenter[2].Y = (pBorderCorner[2].Y + pBorderCorner[3].Y) / 2.0f;
            pBorderCenter[3].X = (pBorderCorner[3].X + pBorderCorner[0].X) / 2.0f;
            pBorderCenter[3].Y = (pBorderCorner[3].Y + pBorderCorner[0].Y) / 2.0f;
        }
        //生成变形矩阵
        protected void genTransformMatrix(float zoomLevel)
        {
            mtrixGL.identity();
            //mtrixAll.Shear(shearX, shearY, MatrixOrder.Prepend);
            mtrixGL.preRotate(MathUtil.degreesToRadians(rotateDegree));
            mtrixGL.preScale(scaleX * zoomLevel, scaleY * zoomLevel);
        }

        public override void ExportObject(Stream s)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int GetSonID(MSonNode son)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MFrameUnit Clone(MFrame parent)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool hitRegion(float xOffsetUI, float yOffsetUI, float zoomLevel, int xMouse, int yMouse, float timePos)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool hitRegion(float xOffsetUI, float yOffsetUI, float zoomLevel, Rectangle selectRect, float timePos)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override SizeF getSize()
        {
            return size;
        }
    }
    /// <summary>
    /// 超级变形操作【备用】
    /// </summary>
    public class SuperOperation_Transform : Operation_Transform
    {
        List<Operation_Transform> subUnitsOrg = new List<Operation_Transform>();//存储原始子单元空间信息
        List<Operation_Transform> subUnitsNew = new List<Operation_Transform>();//存储最新子单元空间信息
        List<MFrameUnit> subUnitsModles = null;//存储子单元模型
        public SuperOperation_Transform(List<MFrameUnit> subUnits)
        {
            subUnitsModles = subUnits;
            foreach (MFrameUnit unit in subUnitsModles)
            {
                subUnitsOrg.Add(unit.getOpreTransForm());
                subUnitsNew.Add(unit.getOpreTransForm());
            }
        }
        //将最新自身变形反馈到子单元
        public void SendTransform()
        {
            //先获得自身矩阵
            Matrix4 mtrixParent = new Matrix4();
            mtrixParent.identity();
            mtrixParent.preRotate(MathUtil.degreesToRadians(rotateDegree));
            mtrixParent.preScale(scaleX, scaleY);
            mtrixParent.preTranslate(posX, posY);
            mtrixParent.postTranslate(-posX, -posY);
            //分布计算数值
            for (int i = 0; i < subUnitsOrg.Count;i++ )
            {
                Operation_Transform ot = subUnitsOrg[i];
                Operation_Transform nt = subUnitsNew[i];
                //-------------------------计算位置
                PointF[] position = new PointF[1];
                position[0] = ot.getPosition();
                mtrixParent.TransformPoints(position);
                nt.posX = position[0].X;
                nt.posY = position[0].Y;
                //-------------------------获取数据
                //获得原单元变换后的X轴边缘中点和它叠加父类变换后
                PointF xBCenterOrg = ot.getTransedXBCenter();
                PointF[] xBCenterNew = new PointF[1];
                xBCenterNew[0] = xBCenterOrg;
                mtrixParent.TransformPoints(xBCenterNew);
                //获得原单元变换后的中心和它叠加父类变换后
                PointF centerOrg = ot.getTransedCenter();
                PointF[] centerNew = new PointF[1];
                centerNew[0] = centerOrg;
                mtrixParent.TransformPoints(centerNew);
                //获得X轴向量
                PointF vXNew = new PointF(xBCenterNew[0].X - centerNew[0].X, xBCenterNew[0].Y - centerNew[0].Y);
                //-------------------------计算缩放

                //-------------------------计算旋转
                float nRotateDegree = MathUtil.getVectorAngle(vXNew.X, vXNew.Y);
                nt.rotateDegree = nRotateDegree;


            }
        }


    }
   /// <summary>
    /// 元件库管理器
    /// </summary>
    public class MLibrary : MNode< MTimeLineHoder>
    {
        public MLibrary(Form_MCLib parentT)
        {
            parent = parentT;
        }

    }
    /// <summary>
    /// 库元件-影片剪辑
    /// </summary>
    public class MLib_MovieClip : MTimeLineHoder
    {
        public MLib_MovieClip(MActor parenT)
        {
            parent = parenT;
        }
        public override MTimeLineHoder Clone()
        {
            MLib_MovieClip newInstance = new MLib_MovieClip((MActor)parent);
            newInstance.parent = parent;
            newInstance.name = name;
            newInstance.allowUpdateUI = allowUpdateUI;
            newInstance.ui = ui;
            foreach (MTimeLine son in m_sonList)
            {
                newInstance.m_sonList.Add((MTimeLine)son.Clone());
            }
            return newInstance;
        }
    }

}
