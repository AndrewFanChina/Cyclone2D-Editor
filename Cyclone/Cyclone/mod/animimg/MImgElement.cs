using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.mod.anim;
using System.Drawing;
using System.IO;
using Cyclone.alg;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using Cyclone.alg.opengl;
using Cyclone.alg.util;
using Cyclone.mod.map;

namespace Cyclone.mod.animimg
{
    /// <summary>
    /// 图片单元,保存了当前图片的名称图片
    /// </summary>
    public class MImgElement : MIO, MParentNode, MSonNode
    {
        private MImgsManager parent;
        public String name = "";
        public Image image = null;
        public TextureImage imageTextured = null;
        public bool forbidOptimize = false;//禁止优化
        public String strAlphaImage = "";//Alpha混合图名称
        public String strPmt = "";       //调色板映射表名称
        public int alpha = 255;//透明度
        public int linkID = -1;//优化链接ID
        //导出时记录优化后图片尺寸
        public Size exportSize = new Size();
        public MImgElement()
        {

        }
        public MImgElement(String nameT, MImgsManager parentT)
        {
            this.parent = parentT;
            name = nameT;
            loadImage();
        }
        public MImgElement(MImgsManager parentT)
        {
            this.parent = parentT;
        }
        public MImgElement Clone()
        {
            return Clone(parent);
        }
        public MImgElement Clone(MImgsManager parentT)
        {
            MImgElement newElement = new MImgElement(parentT);
            newElement.name = name + "";
            newElement.image = image;
            newElement.imageTextured = imageTextured;
            newElement.forbidOptimize = forbidOptimize;
            newElement.strAlphaImage = strAlphaImage;
            newElement.strPmt = strPmt;
            newElement.alpha = alpha;
            newElement.linkID = linkID;
            return newElement;
        }
        public MImgData getMImgData()
        {
            MImgData id = new MImgData();
            id.imgElement = this;
            id.name = this.name;
            id.image = this.image;
            id.imageTextured = this.imageTextured;
            id.forbidOptimize = this.forbidOptimize;
            id.strAlphaImage = this.strAlphaImage;
            id.strPmt = this.strPmt;
            id.alpha = this.alpha;
            return id;
        }

        public TextureImage ImageTextured
        {
            get
            {
                if (imageTextured == null)
                {
                    imageTextured = new TextureImage((Bitmap)image);
                }
                return imageTextured;
            }
        }

        public void setParent(MImgsManager parentT)
        {
            parent = parentT;
        }
        public int GetID()
        {
            if (parent == null)
            {
                return -1;
            }
            return parent.GetSonID(this);
        }
        public virtual int GetSonID(MSonNode son)
        {
            throw new Exception("can't use this method");
        }
        public MParentNode GetParent()
        {
            return parent;
        }
        public void SetParent(MParentNode parentT)
        {
            parent = (MImgsManager)parentT;
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
        //判断名字是否相等(包括半透明图片、半透明度、调色板信息)
        public bool equalsOnName(MImgElement compareElement)
        {
            if (compareElement == null)
            {
                return false;
            }
            if (!name.Equals(compareElement.name))
            {
                return false;
            }
            if (!strAlphaImage.Equals(compareElement.strAlphaImage))
            {
                return false;
            }
            if (!strPmt.Equals(compareElement.strPmt))
            {
                return false;
            }
            if (alpha != compareElement.alpha)
            {
                return false;
            }
            return true;

        }
        public String getFullName()
        {
            return ((MImgsManager)parent).form_Main.path_folder + Consts.SUBPARH_IMG + name;
        }
        public String getFullName(String nameT)
        {
            String s = ((MImgsManager)parent).form_Main.path_folder + Consts.SUBPARH_IMG;
            if (nameT != null)
            {
                s += nameT;
            }
            else
            {
                s += name;
            }
            return s;
        }
        public String getFullAlphaName()
        {
            return getFullAlphaName(null);
        }
        public String getFullAlphaName(String nameT)
        {
            String s = ((MImgsManager)parent).form_Main.path_folder + Consts.SUBPARH_IMG;
            if (nameT != null)
            {
                s += nameT;
            }
            else
            {
                s += strAlphaImage;
            }
            return s;
        }
        public String getFullPmtName()
        {
            return getFullPmtName(null);
        }
        public String getFullPmtName(String nameT)
        {
            String s = ((MImgsManager)parent).form_Main.path_folder + Consts.SUBPARH_IMG;
            if (nameT != null)
            {
                s += nameT;
            }
            else
            {
                s += strPmt;
            }
            return s;
        }
        public String getShowName()
        {
            String nameT = name + "";
            if (strAlphaImage != null && !strAlphaImage.Equals(""))
            {
                nameT += " [A: " + strAlphaImage.Replace(".png", "") + "]";
            }
            if (alpha != 255)
            {
                nameT += " [A: " + alpha + "]";
            }
            if (strPmt != null && !strPmt.Equals(""))
            {
                nameT += " [P: " + strPmt.Replace(".pmt", "") + "]";
            }
            return nameT;
        }
        public void rebindTexture()
        {
            if (GraphicsContext.CurrentContext == null)
            {
                return;
            }
            if (imageTextured == null)
            {
                imageTextured = new TextureImage((Bitmap)image);
            }
            else
            {
                imageTextured.rebindBitmap((Bitmap)image);
            }
        }
        public void loadImage()
        {
            try
            {
                MImgsManager imgsManager = ((MImgsManager)parent);
                image = GraphicsUtil.createAlphaPmtImage(imgsManager.form_Main.path_folder, name, strAlphaImage, strPmt, alpha);
                rebindTexture();
            }
            catch (Exception e)
            {
                Console.WriteLine("load Image Error:"+e.Message);
            }
        }
        //获得原图
        public Image getSrcImage()
        {
            String path = ((MImgsManager)parent).form_Main.path_folder + Consts.SUBPARH_IMG + name;
            Image image = null;
            try
            {
                image = IOUtil.createImage(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return image;
        }
        public String getAlphaImagePath()
        {
            String path = ((MImgsManager)parent).form_Main.path_folder + Consts.SUBPARH_IMG + strAlphaImage;
            return path;
        }
        //获得半透明混合图
        public Image getAlphaImage()
        {
            if (strAlphaImage.Equals(""))
            {
                return null;
            }
            String path = ((MImgsManager)parent).form_Main.path_folder + Consts.SUBPARH_IMG + strAlphaImage;
            Image imageAlpha = null;
            try
            {
                imageAlpha = IOUtil.createImage(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return imageAlpha;
        }
        //是否含有半透明混合图
        public bool hasAlphaImage()
        {
            if (strAlphaImage.Equals(""))
            {
                return false;
            }
            return true;
        }
        public int getWidth()
        {
            if (this.image == null)
            {
                return 0;
            }
            return image.Width;
        }
        public int getHeight()
        {
            if (this.image == null)
            {
                return 0;
            }
            return image.Height;
        }
        public bool renameImage(String newName)
        {
            if (this.name == null)
            {
                return false;
            }
            //改变文件名称
            String path = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_IMG + name;
            String newPath = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_IMG + newName;
            if (File.Exists(newPath))
            {
                MessageBox.Show("已经存在同名文件");
                return false;
            }
            if (File.Exists(path))
            {
                File.Copy(path, newPath);
                File.Delete(path);
            }
            //检查动画图片中的同类名字
            MImgsManager imsManager = ((MImgsManager)parent).form_Main.form_MAnimation.form_MImgsList.mImgsManager;
            for (int i = 0; i < imsManager.Count(); i++)
            {
                MImgElement imgElement = imsManager[i];
                if (!imgElement.Equals(this) && imgElement.name.Equals(name))
                {
                    imgElement.name = newName;
                }
            }
            //检查地图图片中的同类名字
            imsManager = ((MImgsManager)parent).form_Main.mapImagesManager;
            for (int i = 0; i < imsManager.Count(); i++)
            {
                MImgElement imgElement = imsManager[i];
                if (!imgElement.Equals(this) && imgElement.name.Equals(name))
                {
                    imgElement.name = newName;
                }
            }
            this.name = newName;
            return true;
        }
        //返回使用信息
        public String getUsedTimeInfor()
        {
            String usedInfor = "";
            int usedTime = getUsedTime(false);
            usedInfor += "--------------被切块使用了" + usedTime + "次--------------\n";
            //...其它映射表检查
            usedInfor += "--------------总计被使用了" + usedTime + "次--------------\n";
            return usedInfor;
        }
        //返回使用次数
        public int getUsedTime()
        {
            return getUsedTime(true);
        }
        public int getUsedTime(bool includeMapping)
        {
            int usedTime = 0;
            if (parent.Equals(parent.form_Main.mapImagesManager))//地图图片
            {
                TileGfxManager tileGfxManager = parent.form_Main.mapsManager.tileGfxManager;
                for (int i = 0; i < tileGfxManager.Count(); i++)
                {
                    for (int j = 0; j < tileGfxManager[i].Count(); j++)
                    {
                        MClipElement clip = tileGfxManager[i][j];
                        if (clip.imageElement != null && clip.imageElement.Equals(this))
                        {
                            usedTime++;
                        }
                    }
                }
            }
            else if (parent.Equals(parent.form_Main.form_MAnimation.form_MImgsList.mImgsManager))//动画图片
            {
                MClipsManager mClipsManager = parent.form_Main.form_MAnimation.form_MImgsList.MClipsManager;
                for (int i = 0; i < mClipsManager.Count(); i++)
                {
                    MClipElement clip = mClipsManager[i];
                    if (clip.imageElement != null && clip.imageElement.Equals(this))
                    {
                        usedTime++;
                    }
                }
            }
            else if (parent.Equals(parent.form_Main.animImgsManagerForExport))//导出用动画图片
            {
                MClipsManager mClipsManager = parent.form_Main.animClipsManagerForExport;
                for (int i = 0; i < mClipsManager.Count(); i++)
                {
                    MClipElement clip = mClipsManager[i];
                    if (clip.imageElement != null && clip.imageElement.Equals(this))
                    {
                        usedTime++;
                    }
                }
            }
            else if (parent.Equals(parent.form_Main.mapImgsManagerForExport))//导出用地图图片
            {
                TileGfxManager tileGfxManager = parent.form_Main.mapsManagerForExport.tileGfxManager;
                for (int i = 0; i < tileGfxManager.Count(); i++)
                {
                    for (int j = 0; j < tileGfxManager[i].Count(); j++)
                    {
                        MClipElement clip = tileGfxManager[i][j];
                        if (clip.imageElement != null && clip.imageElement.Equals(this))
                        {
                            usedTime++;
                        }
                    }
                }
            }
            //...其它映射表检查
            return usedTime;
        }
        //获取所有用到此图片的切块列表
        public List<MClipElement> getAllClipsUsingMe()
        {
            List<MClipElement> list = new List<MClipElement>();
            if (parent.Equals(parent.form_Main.mapImagesManager))//地图图片
            {
                TileGfxManager tileGfxManager = parent.form_Main.mapsManager.tileGfxManager;
                for (int i = 0; i < tileGfxManager.Count(); i++)
                {
                    for (int j = 0; j < tileGfxManager[i].Count(); j++)
                    {
                        MClipElement clip = tileGfxManager[i][j];
                        if (clip.imageElement != null && clip.imageElement.Equals(this))
                        {
                            list.Add(clip);
                        }
                    }
                }
            }
            else if (parent.Equals(parent.form_Main.form_MAnimation.form_MImgsList.mImgsManager))//动画图片
            {
                MClipsManager mClipsManager = parent.form_Main.form_MAnimation.form_MImgsList.MClipsManager;
                for (int i = 0; i < mClipsManager.Count(); i++)
                {
                    MClipElement clip = mClipsManager[i];
                    if (clip.imageElement != null && clip.imageElement.Equals(this))
                    {
                        list.Add(clip);
                    }
                }
            }
            else if (parent.Equals(parent.form_Main.animImgsManagerForExport))//导出用动画图片
            {
                MClipsManager mClipsManager = parent.form_Main.animClipsManagerForExport;
                for (int i = 0; i < mClipsManager.Count(); i++)
                {
                    MClipElement clip = mClipsManager[i];
                    if (clip.imageElement != null && clip.imageElement.Equals(this))
                    {
                        list.Add(clip);
                    }
                }
            }
            else if (parent.Equals(parent.form_Main.mapImgsManagerForExport))//导出用地图图片
            {
                TileGfxManager tileGfxManager = parent.form_Main.mapsManagerForExport.tileGfxManager;
                for (int i = 0; i < tileGfxManager.Count(); i++)
                {
                    for (int j = 0; j < tileGfxManager[i].Count(); j++)
                    {
                        MClipElement clip = tileGfxManager[i][j];
                        if (clip.imageElement != null && clip.imageElement.Equals(this))
                        {
                            list.Add(clip);
                        }
                    }
                }
            }
            return list;
        }
        public string getValueToLenString()
        {
            String s = MiscUtil.AppendBlanksToString(getShowName(), 160) + "<" + MiscUtil.intToFixLenString(getUsedTime(), 3) + ">     ";
            if (forbidOptimize)
            {
                s += "[×]";
            }
            else if (linkID >= 0)
            {
                s += "[" + MiscUtil.intToFixLenString(linkID, 2) + "]";
            }

            return s; 
        }

        #region MIO 成员

        public void ReadObject(Stream s)
        {
            name = IOUtil.readString(s);
            forbidOptimize = IOUtil.readBoolean(s);
            strAlphaImage = IOUtil.readString(s);
            strPmt = IOUtil.readString(s);
            alpha = IOUtil.readInt(s);
            linkID = IOUtil.readInt(s);
            loadImage();

        }
        public void WriteObject(Stream s)
        {
            IOUtil.writeString(s, name);
            IOUtil.writeBoolean(s, forbidOptimize);
            IOUtil.writeString(s, strAlphaImage);
            IOUtil.writeString(s, strPmt);
            IOUtil.writeInt(s, alpha);
            IOUtil.writeInt(s, linkID);
        }

        public void ExportObject(Stream fs_bin)
        {
            IOUtil.writeString(fs_bin, MiscUtil.getPureFileName(name));
            IOUtil.writeShort(fs_bin, (short)exportSize.Width);
            IOUtil.writeShort(fs_bin, (short)exportSize.Height);
            //IOUtil.writeString(fs_bin, MiscUtil.getPureFileName(strAlphaImage));
            //IOUtil.writeString(fs_bin, MiscUtil.getPureFileName(strPmt));
            //IOUtil.writeByte(fs_bin, (byte)alpha);

            ////复制pmt文件
            //if (strPmt != null && !strPmt.Equals(""))
            //{
            //    String pmtLocal = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_IMG + strPmt;
            //    String pmtDest = Consts.exportFolder + Consts.SUBPARH_IMG + strPmt;
            //    IOUtil.Copy(pmtLocal, pmtDest, true);
            //}

        }
        #endregion
    }
}
