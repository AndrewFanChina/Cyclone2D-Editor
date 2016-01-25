using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.alg;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using Cyclone.mod.script;
using Cyclone.mod.anim;
using Cyclone.alg.math;
using Cyclone.alg.type;
using Cyclone.alg.util;
using Cyclone.mod.animimg;

namespace Cyclone.mod.map
{
    /// <summary>
    /// 角色原型管理器，管理所有用于管理所有的角色原型
    /// </summary>
    public class AntetypesManager : MNode<AntetypeFolder>
    {
        public MapsManager mapsManager = null;　　//拥有者为地图管理器
        public MActorsManager actorsManager = null;//
        //public ArrayList antetypesList = new ArrayList();
        //public ObjectVector antetypeFolders = new ObjectVector();//角色原型文件夹列表
        public AntetypesManager(MapsManager mapsManagerT, MActorsManager actorsManagerT)
        {
            mapsManager = mapsManagerT;
            actorsManager = actorsManagerT;
            AntetypeFolder defFolder = new AntetypeFolder(this);
            this.Add(defFolder);
        }
        //清除冗余单元
        public void clearSpilth()
        {
            clearSpilth(true);
        }
        public void clearSpilth(bool clearNotUsed)
        {
            //清除未使用的单元
            if (clearNotUsed)
            {
                for (int i = 0; i < this.Count(); i++)
                {
                    AntetypeFolder folder = this[i];
                    for (int j = 0; j < folder.Count(); j++)
                    {
                        if (folder[i].getUsedTime() <= 0)
                        {
                            folder.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }
            //清除重复的单元
            for (int i = 0; i < GetSonsCount(); i++)
            {
                Antetype srcElement = (Antetype)getAntetypeBySumID(i);
                for (int j = i + 1; j < GetSonsCount(); j++)
                {
                    Antetype compElement = (Antetype)getAntetypeBySumID(j);
                    if (compElement.equalsAnteType(srcElement))//重复的单元
                    {
                        //转移引用
                        for (int k = 0; k < mapsManager.getElementCount(); k++)
                        {
                            MapElement map = mapsManager.getElement(k);
                            for (int x = 0; x < map.getMapW(); x++)
                            {
                                for (int y = 0; y < map.getMapH(); y++)
                                {
                                    MapTileElement mapTile = map.getTile(x, y);
                                    for (int z = 0; z < mapTile.tile_objectList.Count; z++)
                                    {
                                        if (mapTile.tile_objectList[z] != null && mapTile.tile_objectList[z].antetype != null && mapTile.tile_objectList[z].antetype.Equals(compElement))
                                        {
                                            mapTile.tile_objectList[z].antetype = srcElement;
                                        }
                                    }
                                    if (mapTile.tile_object_mask != null && mapTile.tile_object_mask.antetype != null && mapTile.tile_object_mask.antetype.Equals(compElement))
                                    {
                                        mapTile.tile_object_mask.antetype = srcElement;
                                    }
                                    if (mapTile.tile_object_bg != null && mapTile.tile_object_bg.antetype != null && mapTile.tile_object_bg.antetype.Equals(compElement))
                                    {
                                        mapTile.tile_object_bg.antetype = srcElement;
                                    }
                                }
                            }
                        }
                        //删除重复的单元
                        compElement.getFolder().Remove(compElement);
                        j--;
                    }
                }
            }
        }
        //合并
        public void combine(AntetypesManager src_Manager, ArrayList anteTypesID)
        {
            //开始合并
            for (int i = 0; i < anteTypesID.Count; i++)
            {
                int id = Convert.ToInt32(anteTypesID[i]);
                Antetype srcElement = src_Manager.getAntetypeBySumID(id);
                Antetype newElement = null;
                //检查是否已经含有同名和同文件夹的角色原型
                for (int j = 0; j < this.GetSonsCount(); j++)
                {
                    Antetype localAT = (Antetype)getAntetypeBySumID(j);
                    if (localAT.equalsNameAndFolder(srcElement))
                    {
                        newElement = localAT;
                    }
                }
                //没有找到的话，则将源角色原型加入当前列表，优先加入到相同文件夹，如果文件夹不存在则创建
                if (newElement == null)
                {
                    AntetypeFolder folderDest = this.getFolderByName(srcElement.getFolderName());
                    if (folderDest == null)
                    {
                        folderDest = new AntetypeFolder(this);
                        folderDest.name = srcElement.getFolderName();
                        this.Add(folderDest);
                    }
                    newElement = srcElement.clone(folderDest);
                    folderDest.Add(newElement);
                }
                //转移源角色原型引用
                for (int k = 0; k < src_Manager.mapsManager.getElementCount(); k++)
                {
                    MapElement map = src_Manager.mapsManager.getElement(k);
                    for (int x = 0; x < map.getMapW(); x++)
                    {
                        for (int y = 0; y < map.getMapH(); y++)
                        {
                            MapTileElement mapTile = map.getTile(x, y);
                            for (int z = 0; z < mapTile.tile_objectList.Count; z++)
                            {
                                if (mapTile.tile_objectList[z] != null && mapTile.tile_objectList[z].antetype != null && mapTile.tile_objectList[z].antetype.Equals(srcElement))
                                {
                                    mapTile.tile_objectList[z].antetype = newElement;
                                }
                            }

                            if (mapTile.tile_object_mask != null && mapTile.tile_object_mask.antetype != null && mapTile.tile_object_mask.antetype.Equals(srcElement))
                            {
                                mapTile.tile_object_mask.antetype = newElement;
                            }
                            if (mapTile.tile_object_bg != null && mapTile.tile_object_bg.antetype != null && mapTile.tile_object_bg.antetype.Equals(srcElement))
                            {
                                mapTile.tile_object_bg.antetype = newElement;
                            }
                        }
                    }
                }
            }
        }
        public Antetype getAntetypeBySumID(int sumID)
        {
            for (int i = 0; i < this.Count(); i++)
            {
                AntetypeFolder folder = this[i];
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
        public AntetypeFolder getFolderByName(String folderName)
        {
            for (int i = 0; i < this.Count(); i++)
            {
                AntetypeFolder folder = this[i];
                if (folder.name.Equals(folderName))
                {
                    return folder;
                }
            }
            return null;
        }
        public Antetype getActorByName(String folderName, String actorName)
        {
            for (int i = 0; i < this.Count(); i++)
            {
                AntetypeFolder folder = this[i];
                if (folder.name.Equals(folderName))
                {
                    for (int j = 0; j < folder.Count(); j++)
                    {
                        Antetype actor = folder[j];
                        if (actor.name.Equals(actorName))
                        {
                            return actor;
                        }
                    }
                }
            }
            return null;
        }
        //统计子级列表内容的总数
        public int GetSonsCount()
        {
            int count = 0;
            for (int i = 0; i < this.Count(); i++)
            {
                AntetypeFolder folder = this[i];
                count += folder.Count();
            }
            return count;
        }
        //串行化输入与输出===================================================================


        public override void ReadObject(System.IO.Stream s)
        {
            short len = IOUtil.readShort(s);
            if (len > 0)
            {
                this.Clear();
            }
            for (short i = 0; i < len; i++)
            {
                AntetypeFolder folder = new AntetypeFolder(this);
                folder.ReadObject(s);
                this.Add(folder);
            }

        }

        public override void WriteObject(System.IO.Stream s)
        {
            short len = (short)this.Count();
            IOUtil.writeShort(s,len);
            for (int i = 0; i < len; i++)
            {
                AntetypeFolder folder = (AntetypeFolder)this[i];
                folder.WriteObject(s);
            }

        }
        public override void ExportObject(System.IO.Stream fs_bin)
        {
            short len = (short)this.Count();
            IOUtil.writeShort(fs_bin, len);
            for (short i = 0; i < len; i++)
            {
                AntetypeFolder folder = (AntetypeFolder)this[i];
                folder.ExportObject(fs_bin);
            }
        }
    }
    /// <summary>
    /// 角色原型类文件夹
    /// </summary>
    public class AntetypeFolder : MNode<Antetype>
    {
        public AntetypeFolder()
        {
            name = "默认文件夹";
        }
        public AntetypeFolder(AntetypesManager antetypesManager)
        {
            parent = antetypesManager;
            name = "默认文件夹";
        }
        public override void ReadObject(Stream s)
        {
            name = IOUtil.readString(s);
            short nbElement = IOUtil.readShort(s);
            for (int i = 0; i < nbElement; i++)
            {
                Antetype antetype = new Antetype(this);
                antetype.ReadObject(s);
                this.Add(antetype);
            }
        }

        public override void WriteObject(Stream s)
        {
            IOUtil.writeString(s, name);
            short nbElement = (short)this.Count();
            IOUtil.writeShort(s, nbElement);
            for (int i = 0; i < nbElement; i++)
            {
                Antetype antetype = this[i];
                antetype.WriteObject(s);
            }
        }

        public override void ExportObject(System.IO.Stream fs_bin)
        {
            short nbElement = (short)this.Count();
            IOUtil.writeShort(fs_bin, nbElement);
            for (int i = 0; i < nbElement; i++)
            {
                Antetype antetype = this[i];
                antetype.ExportObject(fs_bin);
            }
        }

    }
    public class ImageMappingElement : SerializeAble
    {
        private MImgElement imgFrom = null;
        private MImgElement imgTo = null;
        public MImgsManager imagesManager = null;
        public ImageMappingElement(MImgsManager imagesManagerT)
        {
            imagesManager = imagesManagerT;
        }
        public ImageMappingElement clone()
        {
            ImageMappingElement newInstance = new ImageMappingElement(imagesManager);
            newInstance.imgFrom = imgFrom;
            newInstance.imgTo = imgTo;
            return newInstance;
        }
        public MImgElement ImgFrom
        {
            get { return imgFrom; }
            set { imgFrom = value; }
        }
        public MImgElement ImgTo
        {
            get { return imgTo; }
            set { imgTo = value; }
        }

        #region SerializeAble 成员

        public void ReadObject(Stream s)
        {
            int idFrom = IOUtil.readInt(s);
            imgFrom = imagesManager[idFrom];
            int idTo = IOUtil.readInt(s);
            imgTo = imagesManager[idTo];

        }

        public void WriteObject(Stream s)
        {
            int id = -1;
            if (imgFrom != null)
            {
                id = imgFrom.GetID();
            }
            IOUtil.writeInt(s, id);
            id = -1;
            if (imgTo != null)
            {
                id = imgTo.GetID();
            }
            IOUtil.writeInt(s, id);
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
            short id = -1;
            if (imgFrom != null)
            {
                id = (short)imgFrom.GetID();
            }
            IOUtil.writeShort(fs_bin, id);
            id = -1;
            if (imgTo != null)
            {
                id = (short)imgTo.GetID();
            }
            IOUtil.writeShort(fs_bin, id);
        }

        #endregion
    }
    /// <summary>
    /// 角色原型类，存储地图中的角色引用的角色动画
    /// </summary>
    public class Antetype : MIO, MParentNode, MSonNode
    {
        public AntetypeFolder parent = null;
        private String actorFolderName;//所在动画文件夹名称
        private String actorName;      //所在动画角色名称
        public MActor Actor
        {
            get
            {
                return ((AntetypesManager)parent.GetParent()).actorsManager.getActorByName(actorFolderName,actorName);
            }
            set 
            { 
                if(value!=null)
                {
                    actorFolderName = ((MActorFolder)value.GetParent()).name;
                    actorName = value.name;
                }
            }
        }
        public String name;
        //图片映射
        public ObjectVector imgMappingList = new ObjectVector();
        public Antetype()
        {
            name = "未命名";
        }
        public Antetype(AntetypeFolder parentT)
        {
            parent = parentT;
            name = "未命名";
        }
        public Antetype(AntetypeFolder parentT, MActor actorT)
        {
            parent = parentT;
            Actor = actorT;
            name = Actor.name + "";
        }
        //获得映射图片
        public MImgElement getMappedImage(MImgElement imgElementFrom)
        {
            return MClipElement.getMappedImage(imgMappingList, imgElementFrom);
        }
        public String getFolderName()
        {
            return parent.name;
        }
        public AntetypeFolder getFolder()
        {
            return parent;
        }
        public bool equalsAnteType(Antetype antetype)
        {
            if (!equalsNameAndFolder(antetype))
            {
                return false;
            }
            if (imgMappingList.getElementCount() != antetype.imgMappingList.getElementCount())
            {
                return false;
            }
            for (int i = 0; i < imgMappingList.getElementCount(); i++)
            {
                ImageMappingElement imgMapElementLocal = (ImageMappingElement)imgMappingList.getElement(i);
                ImageMappingElement imgMapElementCompare = (ImageMappingElement)antetype.imgMappingList.getElement(i);
                if (!imgMapElementLocal.ImgFrom.Equals(imgMapElementCompare.ImgFrom))
                {
                    return false;
                }
                if (!imgMapElementLocal.ImgTo.Equals(imgMapElementCompare.ImgTo))
                {
                    return false;
                }
            }
            return true;
        }
        public bool equalsNameAndFolder(Antetype antetype)
        {
            if (antetype==null)
            {
                return false;
            }
            String nameLocal=name;
            String nameSrc = antetype.name;
            if (nameLocal == null || nameSrc == null || !nameLocal.Equals(nameSrc))
            {
                return false;
            }
            nameLocal=getFolderName();
            nameSrc = antetype.getFolderName();
            if (!nameLocal.Equals(nameSrc))
            {
                return false;
            }
            return true;
        }
        public Antetype clone(AntetypeFolder folder)
        {
            Antetype newInstance = new Antetype(folder, Actor);
            newInstance.name = name;
            for(int i=0;i<imgMappingList.getElementCount();i++)
            {
                ImageMappingElement imgMapElement = (ImageMappingElement)imgMappingList.getElement(i);
                ImageMappingElement newImgMapElement = imgMapElement.clone();
                newInstance.imgMappingList.addElement(newImgMapElement);
            }
            return newInstance;
        }
        public int GetID()
        {
            if (parent == null)
            {
                return -1;
            }
            return parent.GetSonID(this);
        }
        //返回角色原型的使用次数(搜索所有地图的所有单元)
        public int getUsedTime()
        {
            AntetypesManager antetypesManager = (AntetypesManager)this.GetTopParent();
            return antetypesManager.mapsManager.getTileUsedTime(this);
        }
        //绘制自身
        private static Rect disRect = new Rect(0, 0, 1, 1);
        public void display(Graphics g, int x, int y, int w, int h, float zoomLevel, bool setRect)
        {
            display(g, x, y,w,h, zoomLevel,0, 0,setRect);
        }
        //传入坐标基于左上角
        public void display(Graphics g, int x, int y, int w, int h, float zoomLevel, int actionID, int frameID, bool setRect)
        {
            display( g,  x,  y, w, h,  zoomLevel, actionID, frameID,  setRect,0xFF);
        }
        public bool display(Graphics g, int x, int y,int w,int h, float zoomLevel,int actionID,int frameID, bool setRect,int alpha)
        {
            if (Actor == null || actionID < 0 || actionID >= Actor.Count() || frameID < 0 || frameID >= Actor[actionID].getMaxFrameLen())
            {
                GraphicsUtil.drawRect(g, x, y, w, h, Consts.colorRed);
                GraphicsUtil.drawLine(g, x, y, x + w, y + h, Consts.colorRed, 1);
                GraphicsUtil.drawLine(g, x + w, y, x, y + h, Consts.colorRed, 1);
                return true;
            }
            if (setRect)
            {
                disRect.setValue(x, y, w, h);
                return Actor.display(g, x + w / 2, y + h / 2, actionID, frameID, zoomLevel, disRect, alpha / 255.0f, imgMappingList);
            }
            else
            {
                return Actor.display(g, x + w / 2, y + h / 2, actionID, frameID, zoomLevel, null, alpha / 255.0f, imgMappingList);
            }
        }
        //串行化输入与输出===================================================================

        #region SerializeAble Members

        public void ReadObject(System.IO.Stream s)
        {
            short actorFolderIndex = IOUtil.readShort(s);
            short actorIndex = IOUtil.readShort(s);
            AntetypesManager antetypesManager = (AntetypesManager)GetTopParent();
            if (actorFolderIndex>=0 && actorIndex >= 0)
            {
                if (antetypesManager.actorsManager[actorFolderIndex] == null || antetypesManager.actorsManager[actorFolderIndex][actorIndex] == null)
                {
                    MessageBox.Show("角色原型中动画索引丢失，需重新指定。", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.Actor = antetypesManager.actorsManager[actorFolderIndex][actorIndex];
                }
            }
            name = IOUtil.readString(s);
            int imgMappingLen = IOUtil.readInt(s);
            for (int i = 0; i < imgMappingLen; i++)
            {
                ImageMappingElement imgMapElement = new ImageMappingElement(antetypesManager.mapsManager.form_Main.form_MAnimation.form_MImgsList.mImgsManager);
                imgMapElement.ReadObject(s);
                imgMappingList.addElement(imgMapElement);
            }
        }
        public void WriteObject(System.IO.Stream s)
        {
            if (Actor == null)
            {
                IOUtil.writeShort(s, -1);
                IOUtil.writeShort(s, -1);
            }
            else
            {
                IOUtil.writeShort(s, (short)((MActorFolder)Actor.GetParent()).GetID());
                IOUtil.writeShort(s, (short)Actor.GetID());
            }
            IOUtil.writeString(s, name);
            int imgMappingLen = imgMappingList.getElementCount();
            IOUtil.writeInt(s, imgMappingLen);
            for (int i = 0; i < imgMappingLen; i++)
            {
                ImageMappingElement imgMapElement = (ImageMappingElement)imgMappingList.getElement(i);
                imgMapElement.WriteObject(s);
            }
        }
        public void ExportObject(System.IO.Stream fs_bin)
        {
            //导出ID
            short actorFolderID = -1;
            short actorID = -1;
            if (Actor != null)
            {
                actorFolderID = (short)((MActorFolder)Actor.GetParent()).GetID();
                actorID = (short)Actor.GetID();
            }
            IOUtil.writeShort(fs_bin, actorFolderID);
            IOUtil.writeShort(fs_bin, actorID);
            //导出图片映射信息(暂未实现)
            //IOUtil.writeShort(fs_bin, (short)imgMappingList.getElementCount());
            //for (int j = 0; j < imgMappingList.getElementCount(); j++)
            //{
            //    ImageMappingElement imgMapElement = (ImageMappingElement)imgMappingList.getElement(j);
            //    imgMapElement.ExportObject(fs_bin);
            //}

        }
        #endregion

        #region MParentNode 成员

        public int GetSonID(MSonNode son)
        {
            throw new Exception("The method or operation is not implemented.");
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

        #region MSonNode 成员

        public MParentNode GetParent()
        {
            return parent;
        }

        public void SetParent(MParentNode parent)
        {
            this.parent = (AntetypeFolder)parent;
        }

        #endregion

        #region MSonNode 成员


        public string getValueToLenString()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
    //角色和文件夹类，用于合并时包装引用
    public class ActorAndFolder
    {
        public MActorFolder folder;
        public MActor actor;
        public ActorAndFolder(MActorFolder folderT, MActor antetypeT)
        {
            folder = folderT;
            actor = antetypeT;
        }
    }

}
