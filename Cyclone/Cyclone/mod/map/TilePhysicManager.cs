using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.alg;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Cyclone.alg.type;
using Cyclone.alg.util;

namespace Cyclone.mod.map
{
    /// <summary>
    /// ��ͼ�������������������������������������е��������������������
    /// </summary>
    public class TilePhysicsManager : SerializeAble
    {
        public MapsManager mapsManager = null;����//ӵ����Ϊ��ͼ������

        public ArrayList PhyTilesList = new ArrayList();
        public TilePhysicsManager(MapsManager mapsManagerT)
        {
            mapsManager = mapsManagerT;
        }
        public TilePhysicsManager cloneForExport(MapsManager mapsManagerT)
        {
            TilePhysicsManager newInstance = new TilePhysicsManager(mapsManagerT);
            for (int i = 0; i < getElementCount(); i++)
            {
                TilePhysicsElement elementI = getElement(i);
                newInstance.addElement(elementI.clone(newInstance));
            }
            return newInstance;
        }
        //���δ���൥Ԫ
        public void clearSpilth()
        {
            for (int i = 0; i < getElementCount(); i++)
            {
                if (getElement(i).getUsedTime() <= 0)
                {
                    removeElement(i);
                    i--;
                }
            }
        }
        public void combine(TilePhysicsManager src_Manager, ArrayList mapsID)
        {
            for (int i = 0; i < src_Manager.getElementCount(); i++)
            {
                TilePhysicsElement srcTile = src_Manager.getElement(i);
                //����Ƿ�Դ�ؿ����õ�
                bool usingBySrc = false;
                for (int j = 0; j < mapsID.Count; j++)
                {
                    MapElement map = src_Manager.mapsManager.getElement((int)mapsID[j]);
                    if (map.getTileUsedTime(srcTile)>0)
                    {
                        usingBySrc = true;
                        break;
                    }
                }
                if (!usingBySrc)
                {
                    continue;
                }
                //����ϲ�
                TilePhysicsElement newTile = null;
                for (int j = 0; j < getElementCount(); j++)
                {
                    TilePhysicsElement localTile = getElement(j);
                    if (localTile.equlasTo(srcTile))
                    {
                        newTile = localTile;
                        break;
                    }
                }
                if (newTile == null)
                {
                    newTile = srcTile.clone(this);
                }
                if (!PhyTilesList.Contains(newTile))
                {
                    this.addElement(newTile);
                }
                //ת������
                for (int k = 0; k < src_Manager.mapsManager.getElementCount(); k++)
                {
                    MapElement map = src_Manager.mapsManager.getElement(k);
                    for (int x = 0; x < map.getMapW(); x++)
                    {
                        for (int y = 0; y < map.getMapH(); y++)
                        {
                            MapTileElement mapTile = map.getTile(x, y);
                            if (mapTile.tile_physic != null && mapTile.tile_physic.Equals(srcTile))
                            {
                                mapTile.tile_physic = newTile;
                            }
                        }
                    }
                }
            }

        }
        //ִ�в��������===================================================================
        //������ƬID
        public int getElementID(TilePhysicsElement element)
        {
            int index = PhyTilesList.IndexOf(element);
            return index;
        }
        //�����µ�Ԫ
        public bool  addElement(TilePhysicsElement element)
        {
            if (PhyTilesList.Count >= short.MaxValue)
            {
                MessageBox.Show("������ȡ�����������������" + short.MaxValue, "���棺", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                PhyTilesList.Add(element);
                return true;
            }
        }
        //�����������ص�Ԫ
        public TilePhysicsElement getElement(int index)
        {
            if (index >= this.PhyTilesList.Count)
            {
                return null;
            }
            return (TilePhysicsElement)PhyTilesList[index];
        }
        //���ݵ�Ԫ��������
        public int getElementIndx(TilePhysicsElement element)
        {
            return PhyTilesList.IndexOf(element);
        }
        //���ص�Ԫ��Ŀ
        public int getElementCount()
        {
            return PhyTilesList.Count;
        }
        //�������е�Ԫ
        public ArrayList getAllClips()
        {
            return PhyTilesList;
        }
        //ɾ����Ԫ
        public bool removeElement(int index)
        {
            if (index<0 || index >= PhyTilesList.Count)
            {
                Console.WriteLine("error index " + index + " in removeElement");
                return false;
            }
            PhyTilesList.RemoveAt(index);
            return true;
        }
        //����ָ������ֵ�ĵ�Ԫ
        public TilePhysicsElement getElemWithValue(byte value)
        {
            for (int i = 0; i < this.getElementCount(); i++)
            {
                TilePhysicsElement element=(TilePhysicsElement)PhyTilesList[i];
                if (element!=null && element.getFlagInf() == value)
                {
                    return element;
                }
            }
            return null;
        }
        //�Ƿ����ָ����ֵ�ĵ�Ԫ
        public bool isValueExist(int value)
        {
            for (int i = 0; i < this.getElementCount(); i++)
            {
                TilePhysicsElement element = (TilePhysicsElement)PhyTilesList[i];
                if (element != null && element.getFlagInf() == value)
                {
                    return true;
                }
            }
            return false;
        }
        //���л����������===================================================================

        #region SerializeAble Members

        public void ReadObject(System.IO.Stream s)
        {
            PhyTilesList.Clear();
            short len = IOUtil.readShort(s);
            for (short i = 0; i < len; i++)
            {
                TilePhysicsElement clipElem = new TilePhysicsElement(this);
                clipElem.ReadObject(s);
                addElement(clipElem);
            }
        }

        public void WriteObject(System.IO.Stream s)
        {
            short len = (short)PhyTilesList.Count;
            IOUtil.writeShort(s,len);
            for (short i = 0; i < len; i++)
            {
                TilePhysicsElement clipElem = (TilePhysicsElement)PhyTilesList[i];
                clipElem.WriteObject(s);
            }
        }
        public void ExportObject(System.IO.Stream fs_bin)
        {
            //short len = (short)gfxClipsList.Count;
            //IOUtil.writeShort(fs_bin, len);
            //IOUtil.writeString_Default(fs_txt, "\n/*��ʹ����"+len+"��������Ƭ��Ԫ*/\n");
            //for (short i = 0; i < len; i++)
            //{
            //    TileGfxClipElement clipElem = (TileGfxClipElement)gfxClipsList[i];
            //    clipElem.exportObject(fs_bin, fs_txt);
            //}
        }
        #endregion
    }
    /// <summary>
    /// ��ͼ�������࣬ӵ���������Ϣ�Լ���Ӧ����ɫ��Ϣ
    /// </summary>
    public class TilePhysicsElement : SerializeAble
    {
        public TilePhysicsManager tilePhysicsManager = null;
        private short flagInf = 0;//��־��Ϣ
        private int color = 0;   //��ɫ��Ϣ(���ڿ��ӻ�������ʾ)
        private TilePhysicsElement()
        {
        }
        public TilePhysicsElement(TilePhysicsManager tilePhysicsManagerT)
        {
            tilePhysicsManager = tilePhysicsManagerT;
        }
        public bool equlasTo(TilePhysicsElement element)
        {
            return element != null && element.flagInf == flagInf;
        }
        public TilePhysicsElement clone(TilePhysicsManager tilePhysicsManagerT)
        {
            TilePhysicsElement newInstance = new TilePhysicsElement(tilePhysicsManagerT);
            newInstance.flagInf = flagInf;
            newInstance.color = color;
            return newInstance;
        }
        public TilePhysicsElement clone()
        {
            TilePhysicsElement newInstance = new TilePhysicsElement(tilePhysicsManager);
            newInstance.flagInf = flagInf;
            newInstance.color = color;
            return newInstance;
        }
        public void setValue(short flagInfT, int colorInfT)
        {
            this.flagInf = flagInfT;
            this.color = colorInfT;
        }
        public short getFlagInf()
        {
            return flagInf;
        }
        public int getColor()
        {
            return color;
        }
        //���ID
        public int getID()
        {
            return tilePhysicsManager.getElementID(this);
        }
        //��ʾ������(�������Ͻ����꣬���γߴ�)
        public void display(Graphics g, int x, int y, int width, int height, bool showString)
        {
            display(g, x, y, width, height, showString, 0xFF);
        }
        public void display(Graphics g, int x, int y,int width,int height,bool showString,int alpha)
        {
            if (flagInf <= 0)
            {
                return;
            }
            GraphicsUtil.fillRect(g, x, y, width, height, this.color, alpha);
            if (showString)
            {
                GraphicsUtil.drawString(g, x + width / 2, y + height / 2, "" + flagInf,Consts.fontSmall, Consts.colorWhite, Consts.HCENTER | Consts.VCENTER);
                //GraphicsUtil.drawNumberByImage(g,(int)flagInf, x + width / 2, y + height / 2, Consts.HCENTER | Consts.VCENTER);
            }
        }
        //�������ô���
        public int getUsedTime()
        {
            return this.tilePhysicsManager.mapsManager.getTileUsedTime(this);
        }
        //���л����������===================================================================

        #region SerializeAble Members

        public void ReadObject(System.IO.Stream s)
        {
            flagInf = IOUtil.readShort(s);
            color = IOUtil.readInt(s);
        }

        public void WriteObject(System.IO.Stream s)
        {
            IOUtil.writeShort(s, flagInf);
            IOUtil.writeInt(s, color);
        }
        public void ExportObject(System.IO.Stream fs_bin)
        {
        }
        #endregion
    }
}
