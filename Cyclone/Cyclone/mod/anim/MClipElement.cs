using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.mod.anim;
using System.Drawing;
using Cyclone.alg;
using Cyclone.mod.map;
using Cyclone.alg.type;
using Cyclone.alg.util;
using Cyclone.mod.animimg;

namespace Cyclone.mod.anim
{
    /// <summary>
    /// 基础切片单元，拥有一原始图片索引以及剪切矩形
    /// </summary>
    public class MClipElement : MIO, MParentNode, MSonNode
    {
        public MClipsManager clipsManager = null;
        public MImgElement imageElement = null;//引用图片
        public Rectangle clipRect;              //剪切矩形
        public int m_idForImage;               //位于同一张图片上的ID
        //图片类型的切片中所包含的图片单元
        public Image imgClip = null;
        public MClipElement()
        {
        }
        public MClipElement(MClipsManager clipManagerT)
        {
            clipsManager = clipManagerT;
        }
        public MClipElement Clone(MClipsManager p_baseClipsManagerT)
        {
            MClipElement newClip = new MClipElement(p_baseClipsManagerT);
            newClip.imageElement = imageElement;
            newClip.clipRect = clipRect;
            newClip.imgClip = imgClip;
            return newClip;
        }
        public MClipData getMClipData()
        {
            MClipData clipData = new MClipData();
            clipData.clipElement = this;
            clipData.clipRect = clipRect;
            clipData.imageElement = imageElement;
            return clipData;
        }
        public int GetID()
        {
            return clipsManager.GetSonID(this);
        }
        //返回是用次数
        public virtual int getUsedTime()
        {
            int number = 0;
            MActorsManager MAM=null;
            MClipsManager clipsManagerM = clipsManager.form_Main.form_MAnimation.form_MImgsList.MClipsManager;
            Console.WriteLine("m:" + clipsManagerM.GetHashCode() + ",c:" + clipsManager.GetHashCode());
            if (clipsManager.Equals(clipsManagerM))
            {
                MAM = clipsManager.form_Main.form_MAnimation.form_MActorList.actorsManager;
            }
            else if (clipsManager.Equals(clipsManager.form_Main.animClipsManagerForExport))
            {
                MAM = clipsManager.form_Main.animActorsManagerExport;
            }
            if (MAM == null)
            {
                return 0;
            }
            for (int i1 = 0; i1 < MAM.Count(); i1++)
            {
                for (int i2 = 0; i2 < MAM[i1].Count(); i2++)
                {
                    for (int i3 = 0; i3 < MAM[i1][i2].Count(); i3++)
                    {
                        for (int i4 = 0; i4 < MAM[i1][i2][i3].Count(); i4++)
                        {
                            for (int i5 = 0; i5 < MAM[i1][i2][i3][i4].Count(); i5++)
                            {
                                for (int i6 = 0; i6 < MAM[i1][i2][i3][i4][i5].Count(); i6++)
                                {
                                    MFrameUnit unit = MAM[i1][i2][i3][i4][i5][i6];
                                    if (unit is MFrameUnit_Bitmap)
                                    {
                                        MFrameUnit_Bitmap unitBitmap = (MFrameUnit_Bitmap)unit;
                                        if (unitBitmap.clipElement!=null && unitBitmap.clipElement.Equals(this))
                                        {
                                            number++;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            } 
            return number;

        }
        //返回使用情况
        public virtual String getUsedInfor()
        {
            String s = ""; 
            int number = 0;
            MActorsManager MAM = null;
            if (clipsManager.Equals(clipsManager.form_Main.form_MapImagesManager.MClipsManager))
            {
                MAM = clipsManager.form_Main.form_MAnimation.form_MActorList.actorsManager;
            }
            else if (clipsManager.Equals(clipsManager.form_Main.animClipsManagerForExport))
            {
                MAM = clipsManager.form_Main.animActorsManagerExport;
            }
            if (MAM == null)
            {
                return s;
            }
            for (int i1 = 0; i1 < MAM.Count(); i1++)
            {
                for (int i2 = 0; i2 < MAM[i1].Count(); i2++)
                {
                    for (int i3 = 0; i3 < MAM[i1][i2].Count(); i3++)
                    {
                        for (int i4 = 0; i4 < MAM[i1][i2][i3].Count(); i4++)
                        {
                            for (int i5 = 0; i5 < MAM[i1][i2][i3][i4].Count(); i5++)
                            {
                                for (int i6 = 0; i6 < MAM[i1][i2][i3][i4][i5].Count(); i6++)
                                {
                                    MFrameUnit unit = MAM[i1][i2][i3][i4][i5][i6];
                                    if (unit is MFrameUnit_Bitmap)
                                    {
                                        MFrameUnit_Bitmap unitBitmap = (MFrameUnit_Bitmap)unit;
                                        if (unitBitmap.clipElement.Equals(this))
                                        {
                                            s += "" + MAM[i1].name + "->" + MAM[i1][i2].name + "->" + MAM[i1][i2][i3].name + "->" +
                                                      MAM[i1][i2][i3][i4].name + "->第" + MAM[i1][i2][i3][i4][i5].timeBegin + "帧" + "->第" +
                                                      MAM[i1][i2][i3][i4][i5][i6].GetID()+"个单元" +"\n";
                                            number++;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            s += "共被使用了" + number + "次\n";
            return s;

        }
        //串行化输入与输出===================================================================

        #region MIO Members

        public virtual void ReadObject(System.IO.Stream s)
        {
            short imgElemIndex, x, y, w, h;
            imgElemIndex = IOUtil.readShort(s);
            x = IOUtil.readShort(s);
            y = IOUtil.readShort(s);
            w = IOUtil.readShort(s);
            h = IOUtil.readShort(s);
            //初始化
            Rectangle clipRectT = new Rectangle(x, y, w, h);
            MImgElement imageElementT = clipsManager.ImgsManager[imgElemIndex];
            setImageValue(imageElementT, clipRectT);
        }

        public virtual void WriteObject(System.IO.Stream s)
        {
            short imgElemIndex = this.getResID();
            IOUtil.writeShort(s, imgElemIndex);
            IOUtil.writeShort(s, (short)clipRect.X);
            IOUtil.writeShort(s, (short)clipRect.Y);
            IOUtil.writeShort(s, (short)clipRect.Width);
            IOUtil.writeShort(s, (short)clipRect.Height);
        }
        public virtual void ExportObject(System.IO.Stream fs_bin)
        {
            short imgElemIndex = this.getResID();
            if (imgElemIndex < 0)
            {
                Console.WriteLine("error");
            }
            IOUtil.writeShort(fs_bin, imgElemIndex);
            IOUtil.writeShort(fs_bin, (short)clipRect.X);
            IOUtil.writeShort(fs_bin, (short)clipRect.Y);
            IOUtil.writeShort(fs_bin, (short)clipRect.Width);
            IOUtil.writeShort(fs_bin, (short)clipRect.Height);
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
            return clipsManager;
        }

        public void SetParent(MParentNode parent)
        {
            clipsManager = (MClipsManager)parent;
        }

        #endregion
        public bool equalsClip(MClipElement clip)
        {
            if ((imageElement != null && clip.imageElement == null) ||
                (imageElement == null && clip.imageElement != null) ||
                (imageElement != null && (!imageElement.equalsOnName(clip.imageElement))))
            {
                return false;
            }
            if (!clipRect.Equals(clip.clipRect))
            {
                return false;
            }
            return true;
        }
        public MClipElement Clone()
        {
            MClipElement newClip = new MClipElement(clipsManager);
            newClip.imageElement = imageElement;
            newClip.clipRect = clipRect;
            return newClip;
        }
        public Image getImage()
        {
            if (imageElement != null)
            {
                return imageElement.image;
            }
            return null;
        }
        public int getImageID()
        {
            return clipsManager.ImgsManager.GetSonID(imageElement);
        }
        public void setImageId(int imgIndex)
        {
            setImageValue(clipsManager.ImgsManager[imgIndex], clipRect);
        }
        //设置属性
        public void setImageValue(MImgElement p_ImageElement, Rectangle p_clipRect)
        {
            imageElement = p_ImageElement;
            clipRect = p_clipRect;
            resetImgClip();
        }
        public void resetImgClip()
        {
            if (imgClip == null || imgClip.Width != clipRect.Width || imgClip.Height != clipRect.Height)
            {
                if (clipRect.Width > 0 && clipRect.Height > 0)
                {
                    imgClip = new Bitmap(clipRect.Width, clipRect.Height);
                }
            }
            Image img = getImage();
            if (img != null)
            {
                Graphics g = Graphics.FromImage(imgClip);
                g.SetClip(new Rectangle(0, 0, img.Width, img.Height));
                g.Clear(Color.Empty);
                GraphicsUtil.drawClip(g, img, 0, 0, clipRect.X, clipRect.Y, clipRect.Width, clipRect.Height, 0);
            }
        }
        //返回宽度
        private int getWidth(byte flag)
        {
            if ((flag & Consts.TRANS_MIRROR_ROT270) != 0)
            {
                return clipRect.Height;
            }
            return clipRect.Width;
        }
        //返回高度
        private int getHeight(byte flag)
        {
            if ((flag & Consts.TRANS_MIRROR_ROT270) != 0)
            {
                return clipRect.Width;
            }
            return clipRect.Height;
        }
        //获得映射图片
        public static MImgElement getMappedImage(ObjectVector imgMappingList, MImgElement imgElementFrom)
        {
            if (imgMappingList == null)
            {
                return null;
            }
            for (int i = 0; i < imgMappingList.getElementCount(); i++)
            {
                Object obj = imgMappingList.getElement(i);
                if (!(obj is ImageMappingElement))
                {
                    return null;
                }
                ImageMappingElement imgMaElement = (ImageMappingElement)obj;
                if (imgMaElement.ImgFrom.Equals(imgElementFrom))
                {
                    return imgMaElement.ImgTo;
                }
            }
            return null;
        }
        //绘制自身
        public void display(Graphics g, float destX, float destY, float zoomLevel)
        {
            display(g, destX, destY, zoomLevel, 0, null, 0xFF);
        }
        public void display(Graphics g, float destX, float destY, float zoomLevel, byte transFlag, Rect limitClip)
        {
            display(g, destX, destY, zoomLevel, transFlag, limitClip, 0xFF);
        }
        public void display(Graphics g, float destX, float destY, float zoomLevel, byte transFlag, Rect limitClip, int alpha)
        {
            if (imageElement != null)
            {
                if (imgClip != null)
                {
                    GraphicsUtil.drawClip(g, imgClip, (int)(destX), (int)(destY), 0, 0, imgClip.Width, imgClip.Height, transFlag, zoomLevel, limitClip, alpha);
                }
                else
                {
                    GraphicsUtil.drawClip(g, imageElement.image, (int)(destX), (int)(destY), clipRect.X, clipRect.Y, clipRect.Width, clipRect.Height, transFlag, zoomLevel, limitClip, alpha);
                }
            }
        }
        //带图片映射的绘制
        public void display(Graphics g, float destX, float destY, float zoomLevel, byte transFlag, Rect limitClip, int alpha, ObjectVector imgMapList)
        {
            if (imageElement != null)
            {
                MImgElement imageElementMapped = getMappedImage(imgMapList, imageElement);
                if (imgMapList != null && imageElementMapped != null && imageElementMapped.image != null && imageElement.image != null && (imageElementMapped.image.Size.Equals(imageElement.image.Size)))
                {
                    GraphicsUtil.drawClip(g, imageElementMapped.image, (int)(destX), (int)(destY), clipRect.X, clipRect.Y, clipRect.Width, clipRect.Height, transFlag, zoomLevel, limitClip, alpha);
                }
                else
                {
                    if (imgClip != null)
                    {
                        GraphicsUtil.drawClip(g, imgClip, (int)(destX), (int)(destY), 0, 0, imgClip.Width, imgClip.Height, transFlag, zoomLevel, limitClip, alpha);
                    }
                    else
                    {
                        GraphicsUtil.drawClip(g, imageElement.image, (int)(destX), (int)(destY), clipRect.X, clipRect.Y, clipRect.Width, clipRect.Height, transFlag, zoomLevel, limitClip, alpha);
                    }
                }
            }
        }
        //显示边框
        private static Point[] edgePoints = new Point[3];
        //绘制半透明蒙板(相对自身图片，带缩放参数)
        public void displayMask(Graphics g, int zoomLevel, int offsetX, int offsetY, int color, int innerAlpha, int colorBorder)
        {
            int _xz = offsetX + clipRect.X * zoomLevel;
            int _yz = offsetY + clipRect.Y * zoomLevel;
            int _wz = clipRect.Width * zoomLevel;
            int _hz = clipRect.Height * zoomLevel;
            if (innerAlpha > 0)
            {
                GraphicsUtil.fillRect(g, _xz, _yz, _wz, _hz, color, innerAlpha);
            }
            if (colorBorder >= 0)
            {
                GraphicsUtil.drawDashRect(g, _xz, _yz, _wz, _hz, colorBorder, 1);
            }
        }
        //绘制边框(相对传入坐标显示边框，不带缩放参数，带有下角是用次数)
        public void displayBorder(Graphics g, int x, int y, int color)
        {
            int _x = x;
            int _y = y;
            int _w = clipRect.Width;
            int _h = clipRect.Height;
            GraphicsUtil.drawRect(g, _x, _y, _w, _h, color);
            //GraphicsUtil.drawString(g, _x + _w / 2, _y + _h, MapClipsManager.getElementID(this) + "-" + usedTime, Consts.fontTiny, Consts.colorNumber, Consts.HCENTER | Consts.TOP);
        }
        //获得当前的图片Index(矩形返回SEPERATE_RECT)
        public short getResID()
        {
            short id = (short)imageElement.GetID();
            return id;
        }

        #region MSonNode 成员


        public string getValueToLenString()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
