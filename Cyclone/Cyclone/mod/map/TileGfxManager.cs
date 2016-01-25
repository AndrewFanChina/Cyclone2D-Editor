using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.alg;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using Cyclone.mod.anim;
using Cyclone.alg.util;
using Cyclone.mod.animimg;

namespace Cyclone.mod.map
{
    /// <summary>
    /// 地图图形方格文件夹，拥有一种风格类型的图片元素序列
    /// </summary>
    public class TileGfxManager : MNode<TileGfxContainer>
    {
        public MapsManager mapsManager = null;　　//拥有者为地图管理器
        public MImgsManager imagesManager = null;
        public TileGfxManager(MapsManager mapsManagerT, MImgsManager imagesManagerT)
        {
            mapsManager = mapsManagerT;
            imagesManager = imagesManagerT;
            //加入默认文件夹
            TileGfxContainer gfxContainer = new TileGfxContainer(this);
            this.Add(gfxContainer);
        }
        //复制自己
        public TileGfxManager cloneForExport(MapsManager mapsManagerT, MImgsManager imagesManagerT)
        {
            TileGfxManager newInstance = new TileGfxManager(mapsManagerT, imagesManagerT);
            newInstance.RemoveAt(0);
            for (short i = 0; i < this.Count(); i++)
            {
                TileGfxContainer gfxContainer = this[i];
                TileGfxContainer newGfxContainer = gfxContainer.cloneForExceport(mapsManagerT, imagesManagerT);
                newInstance.Add(newGfxContainer);
            }
            return newInstance;
        }
        //复制自己
        public TileGfxManager clone()
        {
            TileGfxManager newInstance = new TileGfxManager(mapsManager, imagesManager);
            newInstance.RemoveAt(0);
            for (short i = 0; i < this.Count(); i++)
            {
                TileGfxContainer gfxContainer = this[i];
                TileGfxContainer newGfxContainer = gfxContainer.clone();
                newInstance.Add(newGfxContainer);
            }
            return newInstance;
        }
        ////复制一个备份，并且清除其中没有使用的单元
        //public TileGfxManager cloneClean()
        //{
        //    TileGfxManager newInstance = new TileGfxManager(mapsManager, imagesManager);
        //    newInstance.removeElement(0);
        //    for (short i = 0; i < this.Size(); i++)
        //    {
        //        TileGfxContainer gfxContainer = [i);
        //        TileGfxContainer newGfxContainer = gfxContainer.cloneClean();
        //        newInstance.addElement(newGfxContainer);
        //    }
        //    return newInstance;
        //}
        //清除未冗余单元(mantainUnused指定是否保留未使用的单元)
        public void clearSpilth(bool mantainUnused)
        {
            for (int i = 0; i < Count(); i++)
            {
                this[i].ClearSpilth(mantainUnused);
            }
        }
        //合并
        public String combine(TileGfxManager src_Manager, ArrayList mapsID)
        {
            String errorString = null;
            for (int srcConIndex = 0; srcConIndex < src_Manager.Count(); srcConIndex++)
            {
                TileGfxContainer srcContainer = src_Manager[srcConIndex];
                //检查是否被源关卡所用到
                bool usingBySrc = false;
                for (int j = 0; j < mapsID.Count; j++)
                {
                    MapElement map = src_Manager.mapsManager[(int)mapsID[j]];
                    if (map.tileGfxContainer.Equals(srcContainer))
                    {
                        usingBySrc = true;
                        break;
                    }
                }
                if (!usingBySrc)
                {
                    continue;
                }
                TileGfxContainer destContainer = null;
                //检查重复的图形容器
                for (int localConIndex = 0; localConIndex < Count(); localConIndex++)
                {
                    TileGfxContainer localContainer = this[localConIndex];
                    if (localContainer.name.Equals(srcContainer.name))
                    {
                        destContainer = localContainer;
                        break;
                    }
                }
                if (destContainer == null)
                {
                    destContainer = new TileGfxContainer(this, srcContainer.name);
                    this.Add(destContainer);
                }
                //转移地图中的图形容器的引用
                for (int k = 0; k < src_Manager.mapsManager.Count(); k++)
                {
                    MapElement map = src_Manager.mapsManager[k];
                    if (map.tileGfxContainer.Equals(srcContainer))
                    {
                        map.tileGfxContainer = destContainer;
                    }
                }
                //向目标容器添加图形元素
                for (int elementIndex = 0; elementIndex < srcContainer.Count(); elementIndex++)
                {
                    TileGfxElement srcElement = (TileGfxElement)srcContainer[elementIndex];
                    TileGfxElement newElement = null;
                    //检查重复的图形元素
                    if (!destContainer.Equals(srcContainer))
                    {
                        for (int j = 0; j < destContainer.Count(); j++)
                        {
                            TileGfxElement localClip = (TileGfxElement)destContainer[j];
                            if (localClip.equalsClip(srcElement))
                            {
                                newElement = localClip;
                                break;
                            }
                        }
                    }
                    if (newElement == null)
                    {
                        newElement = srcElement.Clone(destContainer);
                    }
                    if (!destContainer.Contains(newElement))
                    {
                        if (!destContainer.Add(newElement))
                        {
                            errorString = "合并图形容器“" + srcContainer.name + "”时，图形元素数量超出65536。";
                            break;
                        }
                    }
                    //转移地图中的块的引用
                    for (int k = 0; k < src_Manager.mapsManager.Count(); k++)
                    {
                        MapElement map = src_Manager.mapsManager[k];
                        for (int x = 0; x < map.getMapW(); x++)
                        {
                            for (int y = 0; y < map.getMapH(); y++)
                            {
                                MapTileElement mapTile = map.getTile(x, y);
                                if (mapTile.tile_gfx_ground != null && mapTile.tile_gfx_ground.tileGfxElement != null && mapTile.tile_gfx_ground.tileGfxElement.Equals(srcElement))
                                {
                                    mapTile.tile_gfx_ground.tileGfxElement = newElement;
                                }
                                if (mapTile.tile_gfx_surface != null && mapTile.tile_gfx_surface.tileGfxElement != null && mapTile.tile_gfx_surface.tileGfxElement.Equals(srcElement))
                                {
                                    mapTile.tile_gfx_surface.tileGfxElement = newElement;
                                }
                            }
                        }
                    }
                }
                if (errorString != null)
                {
                    break;
                }
            }
            return errorString;
        }
        public override void ReadObject(System.IO.Stream s)
        {
            TileGfxContainer gfxContainer = null;
            //读入文件夹
            short len = IOUtil.readShort(s);
            if (len > 0)
            {
                this.Clear();
                for (short i = 0; i < len; i++)
                {
                    gfxContainer = new TileGfxContainer(this);
                    gfxContainer.ReadObject(s);
                    Add(gfxContainer);
                }
            }

        }

        public override void WriteObject(System.IO.Stream s)
        {
            short len = (short)this.Count();
            IOUtil.writeShort(s, len);
            for (short i = 0; i < len; i++)
            {
                TileGfxContainer gfxContainer = this[i];
                gfxContainer.WriteObject(s);
            }
        }

        public override void ExportObject(System.IO.Stream s)
        {
            //输出图片索引
            String imgsDic = Consts.exportC2DBinFolder + Consts.exportFileName + "_mapimgs.bin";
            FileStream fs_imgs = null;
            if (File.Exists(imgsDic))
            {
                fs_imgs = File.Open(imgsDic, FileMode.Truncate);
            }
            else
            {
                fs_imgs = File.Open(imgsDic, FileMode.OpenOrCreate);
            }
            imagesManager.ExportObject(fs_imgs);
            fs_imgs.Flush();
            fs_imgs.Close();

            //输出地形风格
            for (int i = 0; i < Count(); i++)
            {
                String styleName = Consts.exportC2DBinFolder + Consts.exportFileName + "_mapstyle_" + i + ".bin";
                FileStream fs_bin = null;
                if (File.Exists(styleName))
                {
                    fs_bin = File.Open(styleName, FileMode.Truncate);
                }
                else
                {
                    fs_bin = File.Open(styleName, FileMode.OpenOrCreate);
                }
                TileGfxContainer elem = this[i];
                elem.ExportObject(fs_bin);
                fs_bin.Flush();
                fs_bin.Close();
            }
        }
        //执行操作与访问===================================================================
        #region MImagesUser 成员

        public List<MClipsManager> GetClipsManagers()
        {
            List<MClipsManager> clipsManagers = new List<MClipsManager>();
            for (int i = 0; i < this.Count(); i++)
            {
                clipsManagers.Add(this[i]);
            }
            return clipsManagers;
        }

        #endregion

        #region MImagesUser 成员


        public void resetImgClips()
        {
            for (int i = 0; i < this.Count(); i++)
            {
                this[i].resetImgClips();
            }
        }

        #endregion

        #region MImgsManagerHolder 成员

        public MImgsManager GetImgsManager()
        {
            return imagesManager;
        }

        #endregion
    }
    /// <summary>
    /// 地图图形方格管理器，管理所有图形方格的容器，其中的图形方格用于铺设地面层和地表层
    /// </summary>
    public class TileGfxContainer : MClipsManager
    {
        public TileGfxContainer()
        {
            name = "默认风格";
        }
        public TileGfxContainer(TileGfxManager tileGfxManagerT):base(tileGfxManagerT.mapsManager.form_Main)
        {
            name = "默认风格";
            parent = tileGfxManagerT;
        }
        public TileGfxContainer(TileGfxManager tileGfxManagerT, String nameT): base(tileGfxManagerT.mapsManager.form_Main)
        {
            name = "默认风格";
            parent = tileGfxManagerT;
            if (nameT != null)
            {
                name = nameT;
            }
        }
        public override MImgsManager ImgsManager
        {
            get
            {
                return form_Main.mapsManager.tileGfxManager.imagesManager;
            }
        }
        public TileGfxContainer clone()
        {
            TileGfxContainer newInstance = new TileGfxContainer((TileGfxManager)parent, name);
            for (int i = 0; i < Count(); i++)
            {
                TileGfxElement baseClip = (TileGfxElement)this[i];
                TileGfxElement newBaseClip = baseClip.Clone(newInstance);
                newInstance.Add(newBaseClip);
            }
            return newInstance;
        }
        public TileGfxContainer cloneForExceport(MapsManager mapsManager, MImgsManager imagesManager)
        {
            TileGfxContainer newInstance = new TileGfxContainer((TileGfxManager)parent, name);
            for (int i = 0; i < Count(); i++)
            {
                TileGfxElement baseClip = (TileGfxElement)this[i];
                TileGfxElement newBaseClip = baseClip.Clone(newInstance);
                newBaseClip.imageElement = imagesManager[newBaseClip.imageElement.GetID()];
                newInstance.Add(newBaseClip);
            }
            return newInstance;
        }
        //复制一个备份，并且清除其中没有使用的单元
        public TileGfxContainer cloneClean()
        {
            TileGfxContainer newInstance = new TileGfxContainer((TileGfxManager)parent, name);
            for (int i = 0; i < Count(); i++)
            {
                TileGfxElement baseClip = (TileGfxElement)this[i];
                if (baseClip.getUsedTime() > 0)
                {
                    TileGfxElement newBaseClip = baseClip.Clone(newInstance);
                    newInstance.Add(newBaseClip);
                }
            }
            return newInstance;
        }
        public bool Contains(MClipElement clipElement)
        {
            return this.GetSonID(clipElement)>=0;
        }
        //自动添加地图元素
        public int generateTiles(MImgElement imgElment, int tileW, int tileH)
        {
            int number = 0;
            bool full = false;
            if(imgElment!=null)
            {
                for (int j = 0; j < imgElment.getHeight() / tileH; j++)
                {
                    for (int i = 0; i < imgElment.getWidth() / tileW; i++)
                    {

                        TileGfxElement element = new TileGfxElement(this);
                        element.setImageValue(imgElment, new Rectangle(i * tileW, j * tileH, tileW, tileH));
                        if (Add(element))
                        {
                            number++;
                        }
                        else
                        {
                            full = true;
                            break;
                        }
                    }
                    if (full)
                    {
                        break;
                    }
                }
            }
            return number;
        }
        //清除所有图形元素
        public void clearAllElement()
        {
            //清除地图引用
            for (int i = 0; i < Count(); i++)
            {
                TileGfxElement element = (TileGfxElement)this[i];
                MapsManager mapsManager = ((TileGfxManager)parent).mapsManager;
                for (int j = 0; j < mapsManager.Count(); j++)
                {
                    MapElement map = mapsManager[j];
                    map.deleteTileUsed(element);
                }
            }
            Clear();
        }
        //获得使用的图片ID列表
        public ArrayList getUsedImgs()
        {
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < Count(); i++)
            {
                TileGfxElement element = (TileGfxElement)this[i];
                if (element != null && element.imageElement != null && !arrayList.Contains(element.imageElement))
                {
                    arrayList.Add(element.imageElement);
                }
            }
            return arrayList;
        }
        //返回使用情况信息
        public String getCondition()
        {
            String text = "";
            text += "----------------------数目统计----------------------\n";
            text += "当前容器中的图形元素数目：" + Count();
            if (Count() > byte.MaxValue)
            {
                text += " (已经超出最大限度[255])\n";
            }
            else
            {
                text += " (在最大限度[255]以内)\n";
            }
            int nbUnUsed=0;
            for (int i = 0; i < Count(); i++)
            {
                TileGfxElement element = (TileGfxElement)this[i];
                if (element.getUsedTime() == 0)
                {
                    nbUnUsed++;
                }
            }
            text += "图形元素中未被使用到的元素数目：" + nbUnUsed + "\n";
            text += "----------------------重复检查----------------------\n";
            //检查重复的图形块
            int count = 0;
            for (int i = 0; i < Count(); i++)
            {
                TileGfxElement element = (TileGfxElement)this[i];
                bool finRepeated = false;
                for (int j = i+1; j < Count(); j++)
                {
                    TileGfxElement elementT = (TileGfxElement)this[j];
                    if (elementT.equalsClip(element))
                    {
                        text += "重复的图形元素：(" + i + ","+j+")\n";
                        finRepeated = true;
                    }
                }
                if (finRepeated)
                {
                    count++;
                }
            }
            text += "共发现" + count + "个重复的图形元素\n";
            text += "----------------------翻转检查----------------------\n";
            count = 0;
            for (int i = 0; i < Count(); i++)
            {
                TileGfxElement element = (TileGfxElement)this[i];
                byte transFlag=element.getTansFlag();
                if (transFlag != Consts.TRANS_NONE)
                {
                    count++;
                }
            }
            text += "共发现" + count + "个翻转的元素\n";
            return text;
        }
        public override void ReadObject(System.IO.Stream s)
        {
            Clear();
            short len = IOUtil.readShort(s);
            for (short i = 0; i < len; i++)
            {
                TileGfxElement clipElem = new TileGfxElement(this);
                clipElem.ReadObject(s);
                Add(clipElem);
            }
            name = IOUtil.readString(s);
        }

        public override void WriteObject(System.IO.Stream s)
        {
            short len = (short)this.Count();
            IOUtil.writeShort(s,len);
            for (short i = 0; i < len; i++)
            {
                TileGfxElement clipElem = (TileGfxElement)this[i];
                clipElem.WriteObject(s);
            }
            IOUtil.writeString(s, name);
        }
        public override void ExportObject(System.IO.Stream fs_bin)
        {
            //导出地形风格单元
            short len = (short)this.Count();
            IOUtil.writeShort(fs_bin, len);
            for (short i = 0; i < len; i++)
            {
                TileGfxElement clipElem = (TileGfxElement)this[i];
                clipElem.ExportObject(fs_bin);
            }
            //导出所使用到的图片ID
            ArrayList array = getUsedImgs();
            len = (short)array.Count;
            IOUtil.writeShort(fs_bin, len);
            for (short i = 0; i < len; i++)
            {
                MImgElement imgElement = (MImgElement)array[i];
                short id = (short)imgElement.GetID();
                IOUtil.writeShort(fs_bin, id);
            }
        }
    }

    /// <summary>
    /// 地图图形方格类，拥有一原始图片索引以及剪切矩形，和基本的反转信息
    /// </summary>
    public class TileGfxElement : MClipElement
    {
        public TileGfxContainer tileGfxContainer = null;
        private byte transFlag = 0;//翻转信息
        //在容器中的位置，辅助数据，仅用于UI操作
        public int xInContainer = 0;
        public int yInContainer = 0;
        public TileGfxElement(TileGfxContainer tileGfxManagerT)
            : base(tileGfxManagerT)
        {
            tileGfxContainer = tileGfxManagerT;
        }
        public TileGfxElement Clone(TileGfxContainer clipsManager)
        {
            TileGfxElement newInstance = new TileGfxElement(clipsManager);
            newInstance.copyBase(this);
            newInstance.transFlag = transFlag;
            newInstance.xInContainer = xInContainer;
            newInstance.yInContainer = yInContainer;
            return newInstance;
        }
        public new TileGfxElement Clone()
        {
            TileGfxElement newInstance = new TileGfxElement(tileGfxContainer);
            newInstance.copyBase(this);
            newInstance.transFlag = transFlag;
            newInstance.xInContainer = xInContainer;
            newInstance.yInContainer = yInContainer;
            return newInstance;
        }
        public bool equalsClip(TileGfxElement clip)
        {
            if (!base.equalsClip(clip))
            {
                return false;
            }
            if (transFlag != clip.transFlag)
            {
                return false;
            }
            return true;
        }
        public void copyBase(MClipElement baseElement)
        {
            imageElement = baseElement.imageElement;
            clipRect = baseElement.clipRect;
            transFlag = 0;
        }
        public void setTansFlag(byte flag)
        {
            transFlag = flag;
        }
        public byte getTansFlag()
        {
            return transFlag;
        }
        //返回使用次数
        public override int getUsedTime()
        {
            return ((TileGfxManager)tileGfxContainer.GetParent()).mapsManager.getTileUsedTime(this);
        }
        //返回是用次数
        public override String getUsedInfor()
        {
            return ((TileGfxManager)tileGfxContainer.GetParent()).mapsManager.getTileUsedInfor(this);
        }

        public override void ReadObject(System.IO.Stream s)
        {
            short imgElemIndex,x, y, w, h;
            //读入
            imgElemIndex = IOUtil.readShort(s);
            x = IOUtil.readShort(s);
            y = IOUtil.readShort(s);
            w = IOUtil.readShort(s);
            h = IOUtil.readShort(s);
            transFlag = IOUtil.readByte(s);
            //初始化
            Rectangle clipRectT = new Rectangle(x, y, w, h);
            MImgElement imageElementT = this.tileGfxContainer.ImgsManager[imgElemIndex];
            setImageValue(imageElementT, clipRectT);
        }

        public override void WriteObject(System.IO.Stream s)
        {
            short imgElemIndex = this.getResID();
            int x = clipRect.X;
            int y = clipRect.Y;
            int w = clipRect.Width;
            int h = clipRect.Height;
            IOUtil.writeShort(s, imgElemIndex);
            IOUtil.writeShort(s, (short)clipRect.X);
            IOUtil.writeShort(s, (short)clipRect.Y);
            IOUtil.writeShort(s, (short)clipRect.Width);
            IOUtil.writeShort(s, (short)clipRect.Height);
            IOUtil.writeByte(s, transFlag);
        }
        public override void ExportObject(System.IO.Stream fs_bin)
        {
            short resID = this.getResID();
            int x = clipRect.X;
            int y = clipRect.Y;
            int w = clipRect.Width;
            int h = clipRect.Height;
            //先输出ID(图片编号)
            IOUtil.writeShort(fs_bin, resID);
            //再输出图片方格位于源图坐标(x、y)
            IOUtil.writeShort(fs_bin, (short)clipRect.X);
            IOUtil.writeShort(fs_bin, (short)clipRect.Y);
            //接着输出尺寸
            IOUtil.writeShort(fs_bin, (short)clipRect.Width);
            IOUtil.writeShort(fs_bin, (short)clipRect.Height);
            //接着输出翻转标志(对于矩形此数据不起作用)
            IOUtil.writeByte(fs_bin, transFlag);
        }
    }
    /// <summary>
    /// 带变形标记的地图图形方格类，包装了一个地图方格对象和针对它的翻转信息，存储在地图单元
    /// </summary>
    public class TransTileGfxElement
    {
        public TileGfxElement tileGfxElement;
        public byte transFlag = Consts.TRANS_NONE;//叠加翻转信息
        public TransTileGfxElement(TileGfxElement tileGfxElementT, byte transFlagT)
        {
            tileGfxElement = tileGfxElementT;
            transFlag = transFlagT;
        }
        public TransTileGfxElement Clone()
        {
            return new TransTileGfxElement(tileGfxElement, transFlag);
        }
    }
}
