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
    /// ��ͼͼ�η����ļ��У�ӵ��һ�ַ�����͵�ͼƬԪ������
    /// </summary>
    public class TileGfxManager : MNode<TileGfxContainer>
    {
        public MapsManager mapsManager = null;����//ӵ����Ϊ��ͼ������
        public MImgsManager imagesManager = null;
        public TileGfxManager(MapsManager mapsManagerT, MImgsManager imagesManagerT)
        {
            mapsManager = mapsManagerT;
            imagesManager = imagesManagerT;
            //����Ĭ���ļ���
            TileGfxContainer gfxContainer = new TileGfxContainer(this);
            this.Add(gfxContainer);
        }
        //�����Լ�
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
        //�����Լ�
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
        ////����һ�����ݣ������������û��ʹ�õĵ�Ԫ
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
        //���δ���൥Ԫ(mantainUnusedָ���Ƿ���δʹ�õĵ�Ԫ)
        public void clearSpilth(bool mantainUnused)
        {
            for (int i = 0; i < Count(); i++)
            {
                this[i].ClearSpilth(mantainUnused);
            }
        }
        //�ϲ�
        public String combine(TileGfxManager src_Manager, ArrayList mapsID)
        {
            String errorString = null;
            for (int srcConIndex = 0; srcConIndex < src_Manager.Count(); srcConIndex++)
            {
                TileGfxContainer srcContainer = src_Manager[srcConIndex];
                //����Ƿ�Դ�ؿ����õ�
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
                //����ظ���ͼ������
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
                //ת�Ƶ�ͼ�е�ͼ������������
                for (int k = 0; k < src_Manager.mapsManager.Count(); k++)
                {
                    MapElement map = src_Manager.mapsManager[k];
                    if (map.tileGfxContainer.Equals(srcContainer))
                    {
                        map.tileGfxContainer = destContainer;
                    }
                }
                //��Ŀ���������ͼ��Ԫ��
                for (int elementIndex = 0; elementIndex < srcContainer.Count(); elementIndex++)
                {
                    TileGfxElement srcElement = (TileGfxElement)srcContainer[elementIndex];
                    TileGfxElement newElement = null;
                    //����ظ���ͼ��Ԫ��
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
                            errorString = "�ϲ�ͼ��������" + srcContainer.name + "��ʱ��ͼ��Ԫ����������65536��";
                            break;
                        }
                    }
                    //ת�Ƶ�ͼ�еĿ������
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
            //�����ļ���
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
            //���ͼƬ����
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

            //������η��
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
        //ִ�в��������===================================================================
        #region MImagesUser ��Ա

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

        #region MImagesUser ��Ա


        public void resetImgClips()
        {
            for (int i = 0; i < this.Count(); i++)
            {
                this[i].resetImgClips();
            }
        }

        #endregion

        #region MImgsManagerHolder ��Ա

        public MImgsManager GetImgsManager()
        {
            return imagesManager;
        }

        #endregion
    }
    /// <summary>
    /// ��ͼͼ�η������������������ͼ�η�������������е�ͼ�η���������������͵ر��
    /// </summary>
    public class TileGfxContainer : MClipsManager
    {
        public TileGfxContainer()
        {
            name = "Ĭ�Ϸ��";
        }
        public TileGfxContainer(TileGfxManager tileGfxManagerT):base(tileGfxManagerT.mapsManager.form_Main)
        {
            name = "Ĭ�Ϸ��";
            parent = tileGfxManagerT;
        }
        public TileGfxContainer(TileGfxManager tileGfxManagerT, String nameT): base(tileGfxManagerT.mapsManager.form_Main)
        {
            name = "Ĭ�Ϸ��";
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
        //����һ�����ݣ������������û��ʹ�õĵ�Ԫ
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
        //�Զ���ӵ�ͼԪ��
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
        //�������ͼ��Ԫ��
        public void clearAllElement()
        {
            //�����ͼ����
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
        //���ʹ�õ�ͼƬID�б�
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
        //����ʹ�������Ϣ
        public String getCondition()
        {
            String text = "";
            text += "----------------------��Ŀͳ��----------------------\n";
            text += "��ǰ�����е�ͼ��Ԫ����Ŀ��" + Count();
            if (Count() > byte.MaxValue)
            {
                text += " (�Ѿ���������޶�[255])\n";
            }
            else
            {
                text += " (������޶�[255]����)\n";
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
            text += "ͼ��Ԫ����δ��ʹ�õ���Ԫ����Ŀ��" + nbUnUsed + "\n";
            text += "----------------------�ظ����----------------------\n";
            //����ظ���ͼ�ο�
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
                        text += "�ظ���ͼ��Ԫ�أ�(" + i + ","+j+")\n";
                        finRepeated = true;
                    }
                }
                if (finRepeated)
                {
                    count++;
                }
            }
            text += "������" + count + "���ظ���ͼ��Ԫ��\n";
            text += "----------------------��ת���----------------------\n";
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
            text += "������" + count + "����ת��Ԫ��\n";
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
            //�������η��Ԫ
            short len = (short)this.Count();
            IOUtil.writeShort(fs_bin, len);
            for (short i = 0; i < len; i++)
            {
                TileGfxElement clipElem = (TileGfxElement)this[i];
                clipElem.ExportObject(fs_bin);
            }
            //������ʹ�õ���ͼƬID
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
    /// ��ͼͼ�η����࣬ӵ��һԭʼͼƬ�����Լ����о��Σ��ͻ����ķ�ת��Ϣ
    /// </summary>
    public class TileGfxElement : MClipElement
    {
        public TileGfxContainer tileGfxContainer = null;
        private byte transFlag = 0;//��ת��Ϣ
        //�������е�λ�ã��������ݣ�������UI����
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
        //����ʹ�ô���
        public override int getUsedTime()
        {
            return ((TileGfxManager)tileGfxContainer.GetParent()).mapsManager.getTileUsedTime(this);
        }
        //�������ô���
        public override String getUsedInfor()
        {
            return ((TileGfxManager)tileGfxContainer.GetParent()).mapsManager.getTileUsedInfor(this);
        }

        public override void ReadObject(System.IO.Stream s)
        {
            short imgElemIndex,x, y, w, h;
            //����
            imgElemIndex = IOUtil.readShort(s);
            x = IOUtil.readShort(s);
            y = IOUtil.readShort(s);
            w = IOUtil.readShort(s);
            h = IOUtil.readShort(s);
            transFlag = IOUtil.readByte(s);
            //��ʼ��
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
            //�����ID(ͼƬ���)
            IOUtil.writeShort(fs_bin, resID);
            //�����ͼƬ����λ��Դͼ����(x��y)
            IOUtil.writeShort(fs_bin, (short)clipRect.X);
            IOUtil.writeShort(fs_bin, (short)clipRect.Y);
            //��������ߴ�
            IOUtil.writeShort(fs_bin, (short)clipRect.Width);
            IOUtil.writeShort(fs_bin, (short)clipRect.Height);
            //���������ת��־(���ھ��δ����ݲ�������)
            IOUtil.writeByte(fs_bin, transFlag);
        }
    }
    /// <summary>
    /// �����α�ǵĵ�ͼͼ�η����࣬��װ��һ����ͼ��������������ķ�ת��Ϣ���洢�ڵ�ͼ��Ԫ
    /// </summary>
    public class TransTileGfxElement
    {
        public TileGfxElement tileGfxElement;
        public byte transFlag = Consts.TRANS_NONE;//���ӷ�ת��Ϣ
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
