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
    /// ͼƬ��Ԫ,�����˵�ǰͼƬ������ͼƬ
    /// </summary>
    public class MImgElement : MIO, MParentNode, MSonNode
    {
        private MImgsManager parent;
        public String name = "";
        public Image image = null;
        public TextureImage imageTextured = null;
        public bool forbidOptimize = false;//��ֹ�Ż�
        public String strAlphaImage = "";//Alpha���ͼ����
        public String strPmt = "";       //��ɫ��ӳ�������
        public int alpha = 255;//͸����
        public int linkID = -1;//�Ż�����ID
        //����ʱ��¼�Ż���ͼƬ�ߴ�
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
        //�ж������Ƿ����(������͸��ͼƬ����͸���ȡ���ɫ����Ϣ)
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
        //���ԭͼ
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
        //��ð�͸�����ͼ
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
        //�Ƿ��а�͸�����ͼ
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
            //�ı��ļ�����
            String path = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_IMG + name;
            String newPath = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_IMG + newName;
            if (File.Exists(newPath))
            {
                MessageBox.Show("�Ѿ�����ͬ���ļ�");
                return false;
            }
            if (File.Exists(path))
            {
                File.Copy(path, newPath);
                File.Delete(path);
            }
            //��鶯��ͼƬ�е�ͬ������
            MImgsManager imsManager = ((MImgsManager)parent).form_Main.form_MAnimation.form_MImgsList.mImgsManager;
            for (int i = 0; i < imsManager.Count(); i++)
            {
                MImgElement imgElement = imsManager[i];
                if (!imgElement.Equals(this) && imgElement.name.Equals(name))
                {
                    imgElement.name = newName;
                }
            }
            //����ͼͼƬ�е�ͬ������
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
        //����ʹ����Ϣ
        public String getUsedTimeInfor()
        {
            String usedInfor = "";
            int usedTime = getUsedTime(false);
            usedInfor += "--------------���п�ʹ����" + usedTime + "��--------------\n";
            //...����ӳ�����
            usedInfor += "--------------�ܼƱ�ʹ����" + usedTime + "��--------------\n";
            return usedInfor;
        }
        //����ʹ�ô���
        public int getUsedTime()
        {
            return getUsedTime(true);
        }
        public int getUsedTime(bool includeMapping)
        {
            int usedTime = 0;
            if (parent.Equals(parent.form_Main.mapImagesManager))//��ͼͼƬ
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
            else if (parent.Equals(parent.form_Main.form_MAnimation.form_MImgsList.mImgsManager))//����ͼƬ
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
            else if (parent.Equals(parent.form_Main.animImgsManagerForExport))//�����ö���ͼƬ
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
            else if (parent.Equals(parent.form_Main.mapImgsManagerForExport))//�����õ�ͼͼƬ
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
            //...����ӳ�����
            return usedTime;
        }
        //��ȡ�����õ���ͼƬ���п��б�
        public List<MClipElement> getAllClipsUsingMe()
        {
            List<MClipElement> list = new List<MClipElement>();
            if (parent.Equals(parent.form_Main.mapImagesManager))//��ͼͼƬ
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
            else if (parent.Equals(parent.form_Main.form_MAnimation.form_MImgsList.mImgsManager))//����ͼƬ
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
            else if (parent.Equals(parent.form_Main.animImgsManagerForExport))//�����ö���ͼƬ
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
            else if (parent.Equals(parent.form_Main.mapImgsManagerForExport))//�����õ�ͼͼƬ
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
                s += "[��]";
            }
            else if (linkID >= 0)
            {
                s += "[" + MiscUtil.intToFixLenString(linkID, 2) + "]";
            }

            return s; 
        }

        #region MIO ��Ա

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

            ////����pmt�ļ�
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
