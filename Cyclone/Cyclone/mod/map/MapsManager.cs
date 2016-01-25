using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Cyclone.alg;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using Cyclone.mod.script;
using Cyclone.mod.anim;
using Cyclone.mod.map;
using Cyclone.alg.type;
using Cyclone.alg.util;
using Cyclone.alg.math;
using Cyclone.mod.animimg;

namespace Cyclone.mod.map
{
    public class MapsManager : MNode<MapElement>
    {
        public TilePhysicsManager tilePhysicsManager = null;
        public TileGfxManager tileGfxManager = null ;
        public AntetypesManager antetypesManager = null;
        //public ArrayList mapsList = new ArrayList();//��ͼ�б�
        public Form_Main form_Main = null;
        public MapsManager(Form_Main form_MainT, MImgsManager imgsManager)
        {
            form_Main = form_MainT;
            tilePhysicsManager = new TilePhysicsManager(this);
            tileGfxManager = new TileGfxManager(this, imgsManager);
        }
        public void initAntetype(MActorsManager actorsManager)
        {
            antetypesManager = new AntetypesManager(this, actorsManager);
        }
        public MapsManager cloneForExport(MImgsManager imagesManager)
        {
            MapsManager newMapsManager = new MapsManager(form_Main, imagesManager);
            newMapsManager.tilePhysicsManager = tilePhysicsManager.cloneForExport(newMapsManager);
            newMapsManager.tileGfxManager = tileGfxManager.cloneForExport(newMapsManager, imagesManager);
            newMapsManager.antetypesManager = antetypesManager;
            short len = (short)this.Count();
            for (short i = 0; i < len; i++)
            {
                MapElement elem = getElement(i);
                int gfxContainerID = elem.tileGfxContainer.GetID();
                MapElement mapElement = elem.cloneForExport(newMapsManager, newMapsManager.tileGfxManager[gfxContainerID]);
                newMapsManager.Add(mapElement);
            }
            return newMapsManager;
        }
        public void combine(MapsManager src_Manager, ArrayList mapsID, ArrayList anteTypesID)
        {
            //�ϲ�����Ԫ��
            tilePhysicsManager.combine(src_Manager.tilePhysicsManager, mapsID);
            tileGfxManager.combine(src_Manager.tileGfxManager, mapsID);
            antetypesManager.combine(src_Manager.antetypesManager, anteTypesID);
            //�ϲ��ؿ�Ԫ��
            for (int i = 0; i < mapsID.Count; i++)
            {
                int index = Convert.ToInt32(mapsID[i]);
                MapElement map = src_Manager.getElement(index);
                map.combineTo(this);
                map.stageList.setUI(form_Main.listBox_stage);
            }
        }

        public override void ReadObject(System.IO.Stream s)
        {
            //���뷽�񼯺�
            tilePhysicsManager.ReadObject(s);//��ȡ����ʼ��-�����������
            tileGfxManager.ReadObject(s);    //��ȡ����ʼ��-ͼ�η��������
            antetypesManager.ReadObject(s);  //��ȡ����ʼ��-��ɫԭ�͹�����(���ڽ�ɫ����Ķ�������)
            //�����ͼ����
            this.Clear();
            short len = IOUtil.readShort(s);
            for (short i = 0; i < len; i++)
            {
                MapElement elem = new MapElement(this);
                elem.ReadObject(s);
                addElement(elem);
            }
        }

        public override void WriteObject(System.IO.Stream s)
        {
            //���뷽�񼯺�
            tilePhysicsManager.WriteObject(s); //���-�����������
            tileGfxManager.WriteObject(s);     //���-ͼ�η��������
            antetypesManager.WriteObject(s);   //���-��ɫԭ�͹�����
            //д����ͼ����
            short len = (short)this.Count();
            IOUtil.writeShort(s, len);
            for (short i = 0; i < len; i++)
            {
                MapElement elem = getElement(i);
                if (elem != null)
                {
                    elem.WriteObject(s);
                }
                else
                {
                    Console.WriteLine("error elem is null");
                }
            }
        }
        public override void ExportObject(System.IO.Stream s)
        {
            short mapsCount = (short)this.Count();
            UserDoc.ArrayTxts_Head.Add("//=============================MapData=============================");
            UserDoc.ArrayTxts_Head.Add("//Generated " + mapsCount + " maps");
            UserDoc.ArrayTxts_Head.Add(" ");
            UserDoc.ArrayTxts_Java.Add("//=============================MapData=============================");
            UserDoc.ArrayTxts_Java.Add("//Generated " + mapsCount + " maps");
            UserDoc.ArrayTxts_Java.Add(" ");
            for (short i = 0; i < mapsCount; i++)
            {
                MapElement map = (MapElement)this[i];
                UserDoc.ArrayTxts_Head.Add("#define Map_" + map.getName() + " " + i + "");
                UserDoc.ArrayTxts_Java.Add("public static final short Map_" + map.getName() + " = " + i + ";");
                for (int j = 0; j < map.stageList.getElementCount(); j++)
                {
                    StageElement stage = (StageElement)map.stageList.getElement(j);
                    UserDoc.ArrayTxts_Head.Add("#define Stage_" + map.getName() + "_" + stage.name + " " + j + "");
                    UserDoc.ArrayTxts_Java.Add("public static final short Stage_" + map.getName() + "_" + stage.name + " = " + j + ";");
                }
            }
            //������η������
            tileGfxManager.ExportObject(s);
            //����ؿ�����
            for (short i = 0; i < mapsCount; i++)
            {
                String mapName = Consts.exportC2DBinFolder + Consts.exportFileName + "_map_" + i + ".bin";
                FileStream fs_mapBin = null;
                if (File.Exists(mapName))
                {
                    fs_mapBin = File.Open(mapName, FileMode.Truncate);
                }
                else
                {
                    fs_mapBin = File.Open(mapName, FileMode.OpenOrCreate);
                }
                MapElement elem = (MapElement)this[i];
                elem.ExportObject(fs_mapBin);
                fs_mapBin.Flush();
                fs_mapBin.Close();
            }
        }
        //׼�������ű�
        public ArrayList listExpScriptFiles = new ArrayList();
        public void readyForExportScript()
        {
            //�Ѽ����нű��ļ�
            listExpScriptFiles.Clear();
            short mapsCount = (short)this.Count();
            for (short iMap = 0; iMap < mapsCount; iMap++)
            {
                MapElement mapElement = (MapElement)this[iMap];
                //�����ű��ļ�
                for (int iS = 0; iS < mapElement.stageList.getElementCount(); iS++)
                {
                    StageElement stage = (StageElement)mapElement.stageList.getElement(iS);
                    for (int iF = 0; iF < stage.scriptList.getElementCount(); iF++)
                    {
                        ScriptFileElement c2dsFile = (ScriptFileElement)stage.scriptList.getElement(iF);
                        String fileName = (String)c2dsFile.getValue();
                        if (!listExpScriptFiles.Contains(fileName))
                        {
                            listExpScriptFiles.Add(fileName);
                        }
                    }
                }
                //�����е�NPC�ű��ļ�
                for (int iY = 0; iY < mapElement.getMapH(); iY++)
                {
                    for (int iX = 0; iX < mapElement.getMapW(); iX++)
                    {
                        if (mapElement.mapData[iX, iY] != null)
                        {
                            for (int iS = 0; iS < mapElement.mapData[iX, iY].tile_objectList.Count; iS++)
                            {
                                TileObjectElement tile = mapElement.mapData[iX, iY].tile_objectList[iS];
                                if (tile != null && tile.NpcID > 0)
                                {
                                    for (int iF = 0; iF < tile.scriptList.getElementCount(); iF++)
                                    {
                                        ScriptFileElement c2dsFile = (ScriptFileElement)tile.scriptList.getElement(iF);
                                        String fileName = (String)c2dsFile.getValue();
                                        if (!listExpScriptFiles.Contains(fileName))
                                        {
                                            listExpScriptFiles.Add(fileName);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("error");
                        }
                    }
                }
            }
        }
        //�����µ�Ԫ
        public void addElement(MapElement element)
        {
            if (this.Count() >= short.MaxValue)
            {
                MessageBox.Show("������ȡ�����������������" + short.MaxValue, "���棺", MessageBoxButtons.OK);
            }
            else
            {
                this.Add(element);
            }
        }
        //ͨ����Ż�ý�ɫ��Ԫ
        public MapElement getElement(int index)
        {
            if (index < 0 || index >= this.Count())
            {
                return null;
            }
            return (MapElement)this[index];
        }
        public int getElementCount()
        {
            return this.Count();
        }
        //�Ƿ�ӵ�е�Ԫ
        public bool includeElement(Object element)
        {
            if (element == null)
            {
                return false;
            }
            if (element is TilePhysicsElement)
            {
                return tilePhysicsManager.getElementID((TilePhysicsElement)element) >= 0;
            }
            if (element is TileGfxElement)
            {
                for (int i = 0; i < tileGfxManager.Count(); i++)
                {
                    if (tileGfxManager[i].GetSonID((TileGfxElement)element) >= 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            if (element is TransTileGfxElement)
            {
                TileGfxElement tileGfxElement = ((TransTileGfxElement)element).tileGfxElement;
                for (int i = 0; i < tileGfxManager.Count(); i++)
                {
                    if (tileGfxManager[i].GetSonID(tileGfxElement) >= 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            if (element is Antetype)
            {
                return ((Antetype)element).GetID() >= 0;
            }
            if (element is TileObjectElement)
            {
                return true;
            }
            return false;
        }
        //ɾ����ͼ�������
        public void deleteTileUsed(Object element)
        {
            if (element == null)
            {
                return;
            }
            for(int i=0;i<this.Count();i++)
            {
                MapElement map=getElement(i);
                map.deleteTileUsed(element);
            }
        }
        //���ص�ͼ�鱻���õĴ���
        public int getTileUsedTime(Object element)
        {
            if (element == null)
            {
                return -1;
            }
            int time = 0;
            for (int i = 0; i < this.Count(); i++)
            {
                MapElement map = getElement(i);
                time+=map.getTileUsedTime(element);
            }
            return time;
        }
        //���ص�ͼ�鱻���õĴ���
        public String getTileUsedInfor(Object element)
        {
            if (element == null)
            {
                return "";
            }
            int time = 0;
            String s = "";
            for (int i = 0; i < this.Count(); i++)
            {
                MapElement map = getElement(i);
                int usedTI=map.getTileUsedTime(element);
                time += usedTI;
                s += "�ڵ�ͼ[" + i + "]��ʹ����" + usedTI + "��\n";
            }
            s += "�ڵ�ͼ�й���ʹ����" + time + "��\n";
            return s;
        }
        //�Ƿ��õ�ĳ����ɫ
        public bool usingActor(MActor actor)
        {
            for (int i = 0; i < antetypesManager.Count(); i++)
            {
                AntetypeFolder folder = antetypesManager[i];
                for (int j = 0; j < folder.Count(); j++)
                {
                    Antetype antetype = folder[j];
                    if (antetype.Actor != null && antetype.Actor.Equals(actor))
                    {
                        return true;
                    }
                }
  
            }
            return false;
        }

    }
    public class MapElement : MIO, MParentNode, MSonNode
    {
        //�ؿ�������
        public MapsManager mapsManager = null;
        //ͼ�η������
        public TileGfxContainer tileGfxContainer = null;
        //����
        String name = "δ����";
        ////ͼƬӳ���
        //public ObjectVector imgMappingList = new ObjectVector();

        //----------------����--------------------
        //��ש�ߴ�
        private short TILE_W = 16;
        private short TILE_H = 16;
        //��ͼ�ߴ�
        private short TILE_NB_X = 20;
        private short TILE_NB_Y = 20;
        //������ɫ
        private int mapColor = 0x0;
        //��ͼ����
        public static byte TYPE_COMMON = 1;
        public static byte TYPE_45 = (byte)(TYPE_COMMON+1);

        private byte mapType = TYPE_COMMON;
        //ͼ�㵼�����
        private byte LEVEL_FLAG = 55;

        //��ͼ����
        public MapTileElement[,] mapData = null;
        
        //��������
        public StageGroup stageList = null;
        
        //----------------����--------------------
        public MapElement()
        { 
        }
        public MapElement(MapsManager mapsManagerT)
        {
            this.mapsManager = mapsManagerT;
            stageList = new StageGroup(this);
            if (mapData == null)
            {
                mapData = new MapTileElement[TILE_NB_X, TILE_NB_Y];
                for (int i = 0; i < mapData.GetLength(0); i++)
                {
                    for (int j = 0; j < mapData.GetLength(1); j++)
                    {
                        mapData[i, j] = new MapTileElement(this);
                    }
                }
            }
            stageList.setUI(mapsManager.form_Main.listBox_stage);
            StageElement element = new StageElement(stageList);
            ObjectVector.allowUpdateUI = false;
            element.name = "Ĭ�ϳ���";
            stageList.addElement(element);
            ObjectVector.allowUpdateUI = true;
        }
        public MapElement cloneForExport(MapsManager mapsManagerT, TileGfxContainer tileGfxContaineT)
        {
            MapElement newElement = new MapElement();
            newElement.mapsManager = mapsManagerT;
            newElement.stageList = new StageGroup(newElement);
            //������ֵ����
            newElement.tileGfxContainer = tileGfxContaineT;
            if(name!=null)
            {
            newElement.name = name+"";
            }
            newElement.TILE_W = TILE_W;
            newElement.TILE_H = TILE_H;
            newElement.TILE_NB_X = TILE_NB_X;
            newElement.TILE_NB_Y = TILE_NB_Y;
            newElement.mapColor = mapColor;
            newElement.mapType = mapType;
            newElement.LEVEL_FLAG = LEVEL_FLAG;
            //������ͼ����
            if (newElement.mapData == null)
            {
                newElement.mapData = new MapTileElement[TILE_NB_X, TILE_NB_Y];
                for (int i = 0; i < newElement.mapData.GetLength(0); i++)
                {
                    for (int j = 0; j < newElement.mapData.GetLength(1); j++)
                    {
                        newElement.mapData[i, j] = mapData[i, j].cloneForExport(newElement);
                    }
                }
            }
            //������������
            for (int i = 0; i < stageList.getElementCount(); i++)
            {
                StageElement stageI = (StageElement)stageList.getElement(i);
                StageElement newStage = stageI.cloneForExport(newElement.stageList);
                newElement.stageList.addElement(newStage);
            }
            return newElement;
        }
        //�ϲ���
        public void combineTo(MapsManager mapsManagerT)
        {
            mapsManager = mapsManagerT;
            mapsManager.addElement(this);
        }
        //ɾ�����±�־
        public void clearNeedUpdateFlag()
        {
            for (int i = 0; i < TILE_NB_X; i++)
            {
                for (int j = 0; j < TILE_NB_Y; j++)
                {
                    if (mapData[i, j] != null)
                    {
                        mapData[i, j].needRedraw = false;
                    }
                }
            }
        }
        //������������
        public void clearDirtyRegion(TileObjectElement tileObj, Graphics g, int indexX, int indexY)
        {
            if (tileObj == null)
            {
                return;
            }
            tileObj.clearDirtyRegion(g, indexX * TILE_W, indexY * TILE_H, TILE_W, TILE_H);
        }

        //�ػ��������
        public void redrawNeedUpdateRegion(Graphics g, float zoomLevel)
        {
            //for (int j = 0; j < TILE_NB_Y; j++)
            //{
            //    for (int i = 0; i < TILE_NB_X; i++)
            //    {
            //        if (mapData[i, j] != null && mapData[i, j].needRedraw)
            //        {
            //            GraphicsUtil.clearRegion(g, (int)(i * TILE_W * zoomLevel), (int)(j * TILE_H * zoomLevel), (int)(TILE_W * zoomLevel), (int)(TILE_H * zoomLevel));
            //        }
            //    }
            //}
            for (int j = 0; j < TILE_NB_Y; j++)
            {
                for (int i = 0; i < TILE_NB_X; i++)
                {
                    if (mapData[i, j] != null && mapData[i, j].needRedraw)
                    {
                        mapData[i, j].disPlayObj(g, i * TILE_W, j * TILE_H, null, Consts.LEVEL_TILE_OBJ);
                    }
                }
            }
            for (int j = 0; j < TILE_NB_Y; j++)
            {
                for (int i = 0; i < TILE_NB_X; i++)
                {
                    if (mapData[i, j] != null && mapData[i, j].needRedraw)
                    {
                        mapData[i, j].disPlayObj(g, i * TILE_W, j * TILE_H, null, Consts.LEVEL_OBJ_MASK | Consts.LEVEL_OBJ_TRIGEER);
                    }
                }
            }
        }
        public void redrawAll(Graphics g)
        {
            for (int j = 0; j < TILE_NB_Y; j++)
            {
                for (int i = 0; i < TILE_NB_X; i++)
                {
                    if (mapData[i, j] != null)
                    {
                        mapData[i, j].disPlayObj(g, i * TILE_W, j * TILE_H, null, Consts.LEVEL_TILE_OBJ);
                    }
                }
            }
            for (int j = 0; j < TILE_NB_Y; j++)
            {
                for (int i = 0; i < TILE_NB_X; i++)
                {
                    if (mapData[i, j] != null)
                    {
                        mapData[i, j].disPlayObj(g, i * TILE_W, j * TILE_H, null, Consts.LEVEL_OBJ_MASK | Consts.LEVEL_OBJ_TRIGEER);
                    }
                }
            }
        }
        public void setName(String nameT)
        {
            this.name = nameT;
        }
        public String getName()
        {
            return name;
        }

        //���õ�ש�ߴ�
        public void setTileSize(short w, short h)
        {
            this.TILE_W = w;
            this.TILE_H = h;
        }
        public int getTileW()
        {
            return this.TILE_W;
        }
        public int getTileH()
        {
            return this.TILE_H;
        }
        //��õ�ש����
        public MapTileElement getTile(int x, int y)
        {
            if (x < 0 || x >= this.TILE_NB_X || y < 0 || y >= this.TILE_NB_Y)
            {
                return null;
            }
            return mapData[x, y];
        }
        //��õ�ש����
        public Object getTile(int x, int y, short level, int currentStageID)
        {
            if (x < 0 || x >= this.TILE_NB_X || y < 0 || y >= this.TILE_NB_Y)
            {
                return null;
            }
            switch (level)
            {
                case Consts.LEVEL_PHYSICS:
                    return mapData[x, y].tile_physic;
                case Consts.LEVEL_TILE_BG:
                    return mapData[x, y].tile_gfx_ground;
                case Consts.LEVEL_TILE_SUR:
                    return  mapData[x, y].tile_gfx_surface;
                case Consts.LEVEL_TILE_OBJ:
                    return mapData[x, y].tile_object_bg;
                case Consts.LEVEL_OBJ_MASK:
                    return mapData[x, y].tile_object_mask;
                case Consts.LEVEL_OBJ_TRIGEER:
                    return mapData[x, y].tile_objectList[currentStageID];
            }
            return null;
        }
        //��õ�ש����
        public Object getTileClone(int x, int y, short level, int currentStageID)
        {
            if (x < 0 || x >= this.TILE_NB_X || y < 0 || y >= this.TILE_NB_Y)
            {
                return null;
            }
            switch (level)
            {
                case Consts.LEVEL_PHYSICS:
                    if(mapData[x, y].tile_physic!=null)
                    {
                       return mapData[x, y].tile_physic;
                    }
                    else
                    {
                        return null;
                    }
                case Consts.LEVEL_TILE_BG:
                    if (mapData[x, y].tile_gfx_ground != null && mapData[x, y].tile_gfx_ground.tileGfxElement!=null)
                    {
                        return new TransTileGfxElement(mapData[x, y].tile_gfx_ground.tileGfxElement,mapData[x, y].tile_gfx_ground.transFlag);
                    }
                    else
                    {
                        return null;
                    }
                case Consts.LEVEL_TILE_SUR:
                    if (mapData[x, y].tile_gfx_surface != null && mapData[x, y].tile_gfx_surface.tileGfxElement!=null)
                    {
                        return new TransTileGfxElement(mapData[x, y].tile_gfx_surface.tileGfxElement,mapData[x, y].tile_gfx_surface.transFlag);
                    }
                    else
                    {
                        return null;
                    }
                case Consts.LEVEL_TILE_OBJ:
                    if(mapData[x, y].tile_object_bg!=null)
                    {
                        return mapData[x, y].tile_object_bg.clone();
                    }
                    else
                    {
                        return null;
                    }
                case Consts.LEVEL_OBJ_MASK:
                    if(mapData[x, y].tile_object_mask!=null)
                    {
                        return mapData[x, y].tile_object_mask.clone();
                    }
                    else
                    {
                        return null;
                    }
                case Consts.LEVEL_OBJ_TRIGEER:
                    if (mapData[x, y].tile_objectList!=null)
                    {
                        if (mapData[x, y].tile_objectList[currentStageID] != null)
                        {
                            return mapData[x, y].tile_objectList[currentStageID].clone();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
            }
            return null;
        }
        //�Ƿ�ʹ�õ�ĳ����ɫԭ��
        public bool usingAnteType(Antetype anteType)
        {
            if (anteType == null)
            {
                return false;
            }
            for (int i = 0; i < TILE_NB_X; i++)
            {
                for (int j = 0; j < TILE_NB_Y; j++)
                {
                    if (mapData[i, j] != null)
                    {
                        if (mapData[i, j].tile_object_bg != null && mapData[i, j].tile_object_bg.usingAntetype(anteType))
                        {
                            return true;
                        }
                        if (mapData[i, j].tile_object_mask != null && mapData[i, j].tile_object_mask.usingAntetype(anteType))
                        {
                            return true;
                        }
                        for (int k = 0; k < mapData[i, j].tile_objectList.Count; k++)
                        {
                            if (mapData[i, j].tile_objectList[k] != null && mapData[i, j].tile_objectList[k].usingAntetype(anteType))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        //��ȡ�����õ��Ľ�ɫԭ��
        public ArrayList getAnteTypesUsed()
        {
            ArrayList anteTypes = new ArrayList();
            for (int i = 0; i < TILE_NB_X; i++)
            {
                for (int j = 0; j < TILE_NB_Y; j++)
                {
                    if (mapData[i, j] != null)
                    {
                        if (mapData[i, j].tile_object_bg != null && mapData[i, j].tile_object_bg.antetype!=null&& (!anteTypes.Contains(mapData[i, j].tile_object_bg.antetype)))
                        {
                            anteTypes.Add(mapData[i, j].tile_object_bg.antetype);
                        }
                        if (mapData[i, j].tile_object_mask != null && mapData[i, j].tile_object_mask.antetype != null && (!anteTypes.Contains(mapData[i, j].tile_object_mask.antetype)))
                        {
                            anteTypes.Add(mapData[i, j].tile_object_mask.antetype);
                        }
                        for (int k = 0; k < mapData[i, j].tile_objectList.Count; k++)
                        {
                            if (mapData[i, j].tile_objectList[k] != null && mapData[i, j].tile_objectList[k].antetype != null && (!anteTypes.Contains(mapData[i, j].tile_objectList[k].antetype)))
                            {
                                anteTypes.Add(mapData[i, j].tile_objectList[k].antetype);
                            }
                        }
                    }
                }
            }
            return anteTypes;
        }
        //�滻��ɫԭ��
        public void replaceAnteType(Antetype currentAT,Antetype replaceAT)
        {
            if (currentAT == null)
            {
                return;
            }
            for (int i = 0; i < TILE_NB_X; i++)
            {
                for (int j = 0; j < TILE_NB_Y; j++)
                {
                    if (mapData[i, j] != null)
                    {
                        if (mapData[i, j].tile_object_bg != null && mapData[i, j].tile_object_bg.antetype != null && (mapData[i, j].tile_object_bg.antetype.Equals(currentAT)))
                        {
                            mapData[i, j].tile_object_bg.antetype = replaceAT;
                        }
                        if (mapData[i, j].tile_object_mask != null && mapData[i, j].tile_object_mask.antetype != null && (mapData[i, j].tile_object_mask.antetype.Equals(currentAT)))
                        {
                            mapData[i, j].tile_object_mask.antetype = replaceAT;
                        }
                        for (int k = 0; k < mapData[i, j].tile_objectList.Count; k++)
                        {
                            if (mapData[i, j].tile_objectList[k] != null && mapData[i, j].tile_objectList[k].antetype != null && (mapData[i, j].tile_objectList[k].antetype.Equals(currentAT)))
                            {
                                mapData[i, j].tile_objectList[k].antetype = replaceAT;
                            }
                        }
                    }
                }
            }
        }
        ////���õ�ש����
        //public void setTile(int x, int y, short level,Object element)
        //{
        //    if (x < 0 || x >= this.mapWidth || y < 0 || y >= this.mapHeight)
        //    {
        //        return;
        //    }
        //    switch (level)
        //    {
        //        case Consts.LEVEL_PHYSICS:
        //            mapData[x, y].tile_physic = (TilePhysicsElement)element;
        //            break;
        //        case Consts.LEVEL_GROUND:
        //            mapData[x, y].tile_gfx_ground = (TileGfxElement)element;
        //            break;
        //        case Consts.LEVEL_SURFACE:
        //            mapData[x, y].tile_gfx_surface = (TileGfxElement)element;
        //            break;
        //        case Consts.LEVEL_OBJECT_BG:
        //            mapData[x, y].tile_object_bg = (TileObjectElement)element;
        //            break;
        //        case Consts.LEVEL_OBJECT:
        //            mapData[x, y].tile_object = (TileObjectElement)element;
        //            break;
        //    }
        //}
        //���õ�ͼ�ߴ�
        public void setMapSize(short w, short h)
        {
            if (w < 0 || h < 0)
            {
                return;
            }
            if (mapData == null)
            {
                this.TILE_NB_X = w;
                this.TILE_NB_Y = h;
                mapData = new MapTileElement[TILE_NB_X, TILE_NB_Y];
            }
            else
            {
                if (w < TILE_NB_X || h < TILE_NB_Y)
                {
                    if (!MessageBox.Show("���轫�ü���������ȷ���ü���", "�����ͼ�ߴ�", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.Yes))
                    {
                        return;
                    }
                }
                MapTileElement[,] mapDataT = new MapTileElement[w, h];
                //copy data
                for (int i = 0; i < w; i++)
                {
                    if (i >= TILE_NB_X)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            mapDataT[i, j] = new MapTileElement(this);
                            mapDataT[i, j].tile_objectList.Clear();
                            for (int k = 0; k < mapDataT[0, 0].tile_objectList.Count; k++)
                            {
                                mapDataT[i, j].tile_objectList.Add(new TileObjectElement(mapDataT[i, j]));
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < h; j++)
                        {
                            if (j >= TILE_NB_Y)
                            {
                                mapDataT[i, j] = new MapTileElement(this);
                                mapDataT[i, j].tile_objectList.Clear();
                                for (int k = 0; k < mapDataT[0, 0].tile_objectList.Count; k++)
                                {
                                    mapDataT[i, j].tile_objectList.Add(new TileObjectElement(mapDataT[i, j]));
                                }
                            }
                            else
                            {
                                mapDataT[i, j] = mapData[i, j];
                            }
                        }
                    }

                }
                this.TILE_NB_X = w;
                this.TILE_NB_Y = h;
                mapData = mapDataT;
            }
        }
        public int getID()
        {
            return this.mapsManager.GetSonID(this);
        }
        public int getMapW()
        {
            return this.TILE_NB_X;
        }
        public int getMapH()
        {
            return this.TILE_NB_Y;
        }
        //���ñ�����ɫ
        public void setColor(int color)
        {
            this.mapColor = color;
        }
        public int getColor()
        {
            return this.mapColor;
        }
        //���õ�ͼ����
        public void setMapType(byte mapTypeT)
        {
            this.mapType = mapTypeT;
            //....go on����б�ӽǵ�ͼ
        }
        //���÷������
        public void setMapStyle(TileGfxContainer style)
        {
            tileGfxContainer = style;
        }
        public byte getMapType()
        {
            return this.mapType;
        }
        //���õ�ͼ������־
        public void setMapExpFlag(byte newFlag)
        {
            this.LEVEL_FLAG = newFlag;
        }
        //���ص�ͼ������־
        public byte getMapExpFlag()
        {
            return LEVEL_FLAG;
        }
        //����ͼ
        //�����(����Ϊ��������)
        public bool fillPoint(Graphics g,int bufW,int bufH,float zoomLevel,int indexX, int indexY,short level, Object element,bool drawInBuffer,int currentStageID)
        {
            if (indexX < 0 || indexY < 0 || indexX >= TILE_NB_X || indexY >= TILE_NB_Y)
            {
                return false;
            }
            bool changed = false;
            switch (level)
            {
                case Consts.LEVEL_PHYSICS:
                    if (element != null && !(element is TilePhysicsElement))
                    {
                        changed = false;
                        break;
                    }
                    if (element == null)
                    {
                        changed = mapData[indexX, indexY].tile_physic != null;
                        mapData[indexX, indexY].tile_physic = null;
                        break;
                    }
                    if (mapData[indexX, indexY].tile_physic == null || !mapData[indexX, indexY].tile_physic.Equals(element))
                    {
                        mapData[indexX, indexY].tile_physic = (TilePhysicsElement)element;
                        changed = true;
                        break;
                    }
                    break;
                case Consts.LEVEL_TILE_BG:
                    if (element != null && !(element is TransTileGfxElement))
                    {
                        changed = false;
                        break;
                    }
                    if (element == null)
                    {
                        changed = mapData[indexX, indexY].tile_gfx_ground != null;
                        mapData[indexX, indexY].tile_gfx_ground = null;
                        break;
                    }
                    if (mapData[indexX, indexY].tile_gfx_ground == null || !mapData[indexX, indexY].tile_gfx_ground.Equals(element))
                    {
                        mapData[indexX, indexY].tile_gfx_ground = (TransTileGfxElement)element;
                        changed = true;
                        break;
                    }
                    break;
                case Consts.LEVEL_TILE_SUR:
                    if (element != null && !(element is TransTileGfxElement))
                    {
                        changed = false;
                        break;
                    }
                    if (element == null)
                    {
                        changed = mapData[indexX, indexY].tile_gfx_surface != null;
                        mapData[indexX, indexY].tile_gfx_surface = null;
                        break;
                    }
                    if (mapData[indexX, indexY].tile_gfx_surface == null || !mapData[indexX, indexY].tile_gfx_surface.Equals(element))
                    {
                        mapData[indexX, indexY].tile_gfx_surface = (TransTileGfxElement)element;
                        changed = true;
                        break;
                    }
                    break;
                case Consts.LEVEL_TILE_OBJ:
                    if (element != null && !(element is TileObjectElement))
                    {
                        changed = false;
                        break;
                    }
                    if (element == null)
                    {
                        //���ԭ��������ռͼ������
                        if (mapData[indexX, indexY].tile_object_bg != null)
                        {
                            clearDirtyRegion(mapData[indexX, indexY].tile_object_bg,g, indexX, indexY);
                        }
                        changed = mapData[indexX, indexY].tile_object_bg != null;
                        mapData[indexX, indexY].tile_object_bg = null;
                        break;
                    }
                    if (mapData[indexX, indexY].tile_object_bg == null || !mapData[indexX, indexY].tile_object_bg.Equals(element))
                    {
                        //���ԭ��������ռͼ������
                        if (mapData[indexX, indexY].tile_object_bg != null)
                        {
                            clearDirtyRegion(mapData[indexX, indexY].tile_object_bg,g, indexX, indexY);
                        }
                        //���������������ͼ������
                        clearDirtyRegion(((TileObjectElement)(element)),g, indexX, indexY);
                        //��ֵ
                        mapData[indexX, indexY].tile_object_bg = (TileObjectElement)element;
                        changed = true;
                        break;
                    }
                    break;
                case Consts.LEVEL_OBJ_MASK:
                    if (element != null && !(element is TileObjectElement))
                    {
                        changed = false;
                        break;
                    }
                    if (element == null)
                    {
                        //���ԭ��������ռͼ������
                        if (mapData[indexX, indexY].tile_object_mask!=null)
                        {
                            clearDirtyRegion(mapData[indexX, indexY].tile_object_mask,g, indexX, indexY);
                        }
                        changed = mapData[indexX, indexY].tile_object_mask != null;
                        mapData[indexX, indexY].tile_object_mask = null;
                        break;
                    }
                    if (mapData[indexX, indexY].tile_object_mask == null || !mapData[indexX, indexY].tile_object_mask.Equals(element))
                    {
                        //���ԭ��������ռͼ������
                        if (mapData[indexX, indexY].tile_object_mask != null)
                        {
                            clearDirtyRegion(mapData[indexX, indexY].tile_object_mask,g, indexX, indexY);
                        }
                        //���������������ͼ������
                        clearDirtyRegion(((TileObjectElement)(element)),g, indexX, indexY);
                        //��ֵ
                        mapData[indexX, indexY].tile_object_mask = (TileObjectElement)element;
                        changed = true;
                        break;
                    }
                    break;
                case Consts.LEVEL_OBJ_TRIGEER:
                    if (element != null && !(element is TileObjectElement))
                    {
                        changed = false;
                        break;
                    }
                    if (element == null)
                    {
                        //���ԭ��������ռͼ������
                        if (mapData[indexX, indexY].tile_objectList != null)
                        {
                            clearDirtyRegion(mapData[indexX, indexY].tile_objectList[currentStageID],g, indexX, indexY);
                        }
                        changed = mapData[indexX, indexY].tile_objectList[currentStageID] != null;
                        mapData[indexX, indexY].tile_objectList[currentStageID]=null;
                        break;
                    }
                    if (mapData[indexX, indexY].tile_objectList == null || !mapData[indexX, indexY].tile_objectList.Equals(element))
                    {
                        //���ԭ��������ռͼ������
                        if (mapData[indexX, indexY].tile_objectList != null&&mapData[indexX, indexY].tile_objectList[currentStageID]!=null)
                        {
                            clearDirtyRegion(mapData[indexX, indexY].tile_objectList[currentStageID], g, indexX, indexY);
                        }
                        //���������������ͼ������
                        clearDirtyRegion(((TileObjectElement)(element)),g, indexX, indexY);
                        //��ֵ
                        mapData[indexX, indexY].tile_objectList[currentStageID] = (TileObjectElement)element;
                        changed = true;
                    }
                    break;
            }
            if (changed && g != null && drawInBuffer)
            {
                if (level >= Consts.LEVEL_PHYSICS && level <= Consts.LEVEL_TILE_SUR)
                {
                    displayTile(g, bufW, bufH, zoomLevel, indexX, indexY, indexX, indexY);
                }
                else if (level >= Consts.LEVEL_TILE_OBJ && level <= Consts.LEVEL_OBJ_TRIGEER)
                {
                    mapData[indexX, indexY].needRedraw = true;
                    //��ɢ�����(�����㷨)
                    int dirtyW = 3;
                    int dirtyH = 3;
                    for (int dI = indexX - dirtyW; dI < indexX + dirtyW; dI++)
                    {
                        if (dI < 0 || dI >= TILE_NB_X)
                        {
                            continue;
                        }
                        for (int dJ = indexY - dirtyH; dJ < indexY + dirtyH; dJ++)
                        {
                            if (dJ < 0 || dJ >= TILE_NB_Y)
                            {
                                continue;
                            }
                            mapData[dI, dJ].needRedraw = true;
                        }
                    }
                }
            }
            return changed;
        }
        //��ĳ�����滻ָ���ķ���
        public void replaceTile(Object element, Object elementNew)
        {
            if (element == null)
            {
                return;
            }
            for (int i = 0; i < TILE_NB_X; i++)
            {
                for (int j = 0; j < TILE_NB_Y; j++)
                {
                    if (mapData[i, j] != null)
                    {
                        if ((element is TilePhysicsElement) && mapData[i, j].tile_physic != null && mapData[i, j].tile_physic.Equals(element))
                        {
                            mapData[i, j].tile_physic = (TilePhysicsElement)elementNew;
                        }
                        else if ((element is TransTileGfxElement))
                        {
                            if (mapData[i, j].tile_gfx_ground != null && mapData[i, j].tile_gfx_ground.Equals(element))
                            {
                                mapData[i, j].tile_gfx_ground = (TransTileGfxElement)elementNew;
                            }
                            if (mapData[i, j].tile_gfx_surface != null && mapData[i, j].tile_gfx_surface.Equals(element))
                            {
                                mapData[i, j].tile_gfx_surface = (TransTileGfxElement)elementNew;
                            }
                        }
                        else if ((element is TileObjectElement))
                        {
                            if (mapData[i, j].tile_object_bg != null && mapData[i, j].tile_object_bg.Equals(element))
                            {
                                mapData[i, j].tile_object_bg = (TileObjectElement)elementNew;
                            }
                            if (mapData[i, j].tile_object_mask != null && mapData[i, j].tile_object_mask.Equals(element))
                            {
                                mapData[i, j].tile_object_mask = (TileObjectElement)elementNew;
                            }
                            for (int k = 0; k < mapData[i, j].tile_objectList.Count; k++)
                            {
                                if (mapData[i, j].tile_objectList[k] != null && mapData[i, j].tile_objectList[k].Equals(element))
                                {
                                    mapData[i, j].tile_objectList[k] = (TileObjectElement)elementNew;
                                }
                            }

                        }
                        else if ((element is Antetype))
                        {
                            if (mapData[i, j].tile_object_bg != null && mapData[i, j].tile_object_bg.usingAntetype((Antetype)element))
                            {
                                mapData[i, j].tile_object_bg.setAntetype((Antetype)element);
                            }
                            if (mapData[i, j].tile_object_mask != null && mapData[i, j].tile_object_mask.usingAntetype((Antetype)element))
                            {
                                mapData[i, j].tile_object_mask.setAntetype((Antetype)element);
                            }
                            for (int k = 0; k < mapData[i, j].tile_objectList.Count; k++)
                            {
                                if (mapData[i, j].tile_objectList[k] != null && mapData[i, j].tile_objectList[k].usingAntetype((Antetype)element))
                                {
                                    mapData[i, j].tile_objectList[k].setAntetype((Antetype)element);
                                }
                            }
                        }
                    }
                }
            }
        }
        //ɾ��ָ���ķ���
        public void deleteTileUsed(Object element)
        {
            if(element==null)
            {
                return;
            }
                for (int i = 0; i < TILE_NB_X; i++)
                {
                    for (int j = 0; j < TILE_NB_Y; j++)
                    {
                        if(mapData[i,j]!=null)
                        {
                            if ((element is TilePhysicsElement) && mapData[i, j].tile_physic != null && mapData[i, j].tile_physic.Equals(element))
                            {
                                mapData[i, j].tile_physic = null;
                            }
                            else if ((element is TileGfxElement) )
                            {
                                if (mapData[i, j].tile_gfx_ground != null && mapData[i, j].tile_gfx_ground.tileGfxElement.Equals(element))
                                {
                                    mapData[i, j].tile_gfx_ground = null;
                                }
                                if (mapData[i, j].tile_gfx_surface != null && mapData[i, j].tile_gfx_surface.tileGfxElement.Equals(element))
                                {
                                    mapData[i, j].tile_gfx_surface = null;
                                }
                            }
                            else if ((element is TileObjectElement))
                            {
                                if (mapData[i, j].tile_object_bg != null && mapData[i, j].tile_object_bg.Equals(element))
                                {
                                    mapData[i, j].tile_object_bg = null;
                                }
                                if (mapData[i, j].tile_object_mask != null && mapData[i, j].tile_object_mask.Equals(element))
                                {
                                    mapData[i, j].tile_object_mask = null;
                                }
                                if (mapData[i, j].tile_objectList != null && mapData[i, j].tile_objectList.Equals(element))
                                {
                                    mapData[i, j].tile_objectList[mapsManager.form_Main.currentStageID].clearAntetype();
                                }
                            }
                            else if ((element is Antetype))
                            {
                                if (mapData[i, j].tile_object_bg != null && mapData[i, j].tile_object_bg.usingAntetype((Antetype)element))
                                {
                                    mapData[i, j].tile_object_bg.clearAntetype();
                                }
                                if (mapData[i, j].tile_object_mask != null && mapData[i, j].tile_object_mask.usingAntetype((Antetype)element))
                                {
                                    mapData[i, j].tile_object_mask.clearAntetype();
                                }
                                for (int k = 0; k < mapData[i, j].tile_objectList.Count; k++)
                                {
                                    if (mapData[i, j].tile_objectList[k] != null && mapData[i, j].tile_objectList[k].usingAntetype((Antetype)element))
                                    {
                                        mapData[i, j].tile_objectList[k].clearAntetype();
                                    }
                                }

                            }
                        }
                    }
                }
        }
        //����ָ�������ʹ�ô���
        public int getTileUsedTime(Object element)
        {
            if (element == null)
            {
                return -1;
            }
            int time = 0;
            for (int i = 0; i < TILE_NB_X; i++)
            {
                for (int j = 0; j < TILE_NB_Y; j++)
                {
                    if (mapData[i, j] != null)
                    {
                        if ((element is TilePhysicsElement) && mapData[i, j].tile_physic != null && mapData[i, j].tile_physic.Equals(element))
                        {
                            time++;
                        }
                        else if ((element is TileGfxElement))
                        {
                            if (mapData[i, j].tile_gfx_ground != null && mapData[i, j].tile_gfx_ground.tileGfxElement != null && mapData[i, j].tile_gfx_ground.tileGfxElement.Equals(element))
                            {
                                time++;
                            }
                            if (mapData[i, j].tile_gfx_surface != null && mapData[i, j].tile_gfx_surface.tileGfxElement != null && mapData[i, j].tile_gfx_surface.tileGfxElement.Equals(element))
                            {
                                time++;
                            }
                        }
                        else if ((element is TileObjectElement))
                        {
                            if (mapData[i, j].tile_object_bg != null && mapData[i, j].tile_object_bg.Equals(element))
                            {
                                time++;
                            }
                            if (mapData[i, j].tile_object_mask != null && mapData[i, j].tile_object_mask.Equals(element))
                            {
                                time++;
                            }
                            if (mapData[i, j].tile_objectList != null && mapData[i, j].tile_objectList.Equals(element))
                            {
                                time++;
                            }
                        }
                        else if ((element is Antetype))
                        {
                            if (mapData[i, j].tile_object_bg != null && mapData[i, j].tile_object_bg.usingAntetype((Antetype)element))
                            {
                                time++;
                            }
                            if (mapData[i, j].tile_object_mask != null && mapData[i, j].tile_object_mask.usingAntetype((Antetype)element))
                            {
                                time++;
                            }
                            for (int k = 0; k < mapData[i, j].tile_objectList.Count; k++)
                            {
                                if (mapData[i, j].tile_objectList[k] != null && mapData[i, j].tile_objectList[k].usingAntetype((Antetype)element))
                                {
                                    time++;
                                }
                            }
                        }
                    }
                }
            }
            return time;
        }

        public void ReadObject(System.IO.Stream s)
        {
            //����
            name = IOUtil.readString(s);
            //Console.WriteLine("readString name is :" + name);
            //��ɫ
            mapColor = IOUtil.readInt(s);
            //���η��
            short tileGfxStyle = IOUtil.readShort(s);
            tileGfxContainer = mapsManager.tileGfxManager[tileGfxStyle];
            //��ש�ߴ�
            TILE_W = IOUtil.readShort(s);
            TILE_H = IOUtil.readShort(s);
            //��ͼ�ߴ�
            TILE_NB_X = IOUtil.readShort(s);
            TILE_NB_Y = IOUtil.readShort(s);
            //����ͼ�㵼�����
            LEVEL_FLAG = IOUtil.readByte(s);
            //��ȡ��������
            stageList.ReadObject(s);
            //��ͼ����
            mapData = new MapTileElement[TILE_NB_X, TILE_NB_Y];
            for (int i = 0; i < mapData.GetLength(0); i++)
            {
                for (int j = 0; j < mapData.GetLength(1); j++)
                {
                    mapData[i, j] = new MapTileElement(this);
                    mapData[i, j].ReadObject(s);
                }
            }

        }

        public void WriteObject(System.IO.Stream s)
        {
            //Console.WriteLine("writeString name is :" + name);
            //����
            IOUtil.writeString(s,name);
            //��ɫ
            IOUtil.writeInt(s, mapColor);
            //���η��
            IOUtil.writeShort(s,(short)(tileGfxContainer.GetID()));
            //��ש�ߴ�
            IOUtil.writeShort(s,TILE_W);
            IOUtil.writeShort(s, TILE_H);
            //��ͼ�ߴ�
            IOUtil.writeShort(s, TILE_NB_X);
            IOUtil.writeShort(s, TILE_NB_Y);
            //д����ͼ���
            IOUtil.writeByte(s, LEVEL_FLAG);
            //д����������
            stageList.WriteObject(s);
            //��ͼ����
            for (int i = 0; i < TILE_NB_X; i++)
            {
                for (int j = 0; j < TILE_NB_Y; j++)
                {
                    if (mapData[i, j] == null)
                    {
                        Console.WriteLine("mapData is null at:"+i+","+j);
                    }
                    else
                    {
                        mapData[i, j].WriteObject(s);
                    }
                }
            }
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
            //����ؿ�����
            //��ɫ
            IOUtil.writeInt(fs_bin, mapColor);
            //���η������
            short styleIndex=(short)(tileGfxContainer.GetID());
            IOUtil.writeShort(fs_bin, styleIndex);
            //��ש�ߴ�
            IOUtil.writeShort(fs_bin, TILE_W);
            IOUtil.writeShort(fs_bin, TILE_H);
            //��ͼ�ߴ�
            IOUtil.writeShort(fs_bin, TILE_NB_X);
            IOUtil.writeShort(fs_bin, TILE_NB_Y);
            //ͼ���־
            IOUtil.writeByte(fs_bin, LEVEL_FLAG);
            //��ͼ����-------------------------------------------------------
            //�����ǲ�
            if ((LEVEL_FLAG & Consts.LEVEL_PHYSICS) != 0)
            {
                for (int i = 0; i < TILE_NB_X; i++)
                {
                    for (int j = 0; j < TILE_NB_Y; j++)
                    {
                        if (mapData[i, j] == null||mapData[i, j].tile_physic==null)
                        {
                            IOUtil.writeShort(fs_bin, 0);
                        }
                        else
                        {
                            short value = mapData[i, j].tile_physic.getFlagInf();
                            IOUtil.writeShort(fs_bin, value);
                        }
                    }
                }
            }
            //�ײ���β�
            if ((LEVEL_FLAG & Consts.LEVEL_TILE_BG) != 0)
            {
                for (int i = 0; i < TILE_NB_X; i++)
                {
                    for (int j = 0; j < TILE_NB_Y; j++)
                    {
                        if (mapData[i, j] == null || mapData[i, j].tile_gfx_ground == null || mapData[i, j].tile_gfx_ground.tileGfxElement==null)
                        {
                            IOUtil.writeShort(fs_bin, 0);
                        }
                        else
                        {
                            short value = (short)(1 + mapData[i, j].tile_gfx_ground.tileGfxElement.GetID());
                            IOUtil.writeShort(fs_bin, value);
                            IOUtil.writeByte(fs_bin, mapData[i, j].tile_gfx_ground.transFlag);
                        }
                    }
                }
            }
            //�ںϵ��β�
            if ((LEVEL_FLAG & Consts.LEVEL_TILE_SUR) != 0)
            {
                for (int i = 0; i < TILE_NB_X; i++)
                {
                    for (int j = 0; j < TILE_NB_Y; j++)
                    {
                        if (mapData[i, j] == null || mapData[i, j].tile_gfx_surface == null || mapData[i, j].tile_gfx_surface.tileGfxElement == null)
                        {
                            IOUtil.writeShort(fs_bin, 0);
                        }
                        else
                        {
                            short value = (short)(1 + mapData[i, j].tile_gfx_surface.tileGfxElement.GetID());
                            IOUtil.writeShort(fs_bin, value);
                            IOUtil.writeByte(fs_bin, mapData[i, j].tile_gfx_surface.transFlag);
                        }
                    }
                }
            }
            //������β�
            if ((LEVEL_FLAG & Consts.LEVEL_TILE_OBJ) != 0)
            {
                ArrayList objList = new ArrayList();
                for (int i = 0; i < TILE_NB_X; i++)
                {
                    for (int j = 0; j < TILE_NB_Y; j++)
                    {
                        if (mapData[i, j] == null || mapData[i, j].tile_object_bg == null)
                        {
                        }
                        else
                        {
                            if (mapData[i, j].tile_object_bg.antetype != null)
                            {
                                objList.Add(new TileObjectForExport(i, j, mapData[i, j].tile_object_bg));
                            }
                        }
                    }
                }
                IOUtil.writeShort(fs_bin, (short)objList.Count);
                for (int i = 0; i < objList.Count; i++)
                {
                    TileObjectForExport tile = (TileObjectForExport)objList[i];
                    IOUtil.writeInt(fs_bin, tile.X);
                    IOUtil.writeInt(fs_bin, tile.Y);
                    TileObjectElement tileObj = (TileObjectElement)tile.obj;
                    tileObj.ExportObject(fs_bin);
                }
            }
            //�޹ض����
            if ((LEVEL_FLAG & Consts.LEVEL_OBJ_MASK) != 0)
            {
                ArrayList objList = new ArrayList();
                for (int i = 0; i < TILE_NB_X; i++)
                {
                    for (int j = 0; j < TILE_NB_Y; j++)
                    {
                        if (mapData[i, j] == null || mapData[i, j].tile_object_mask == null)
                        {
                        }
                        else
                        {
                            if (mapData[i, j].tile_object_mask.antetype != null)
                            {
                                objList.Add(new TileObjectForExport(i, j, mapData[i, j].tile_object_mask));
                            }
                        }
                    }
                }
                IOUtil.writeShort(fs_bin, (short)objList.Count);
                for (int i = 0; i < objList.Count; i++)
                {
                    TileObjectForExport tile = (TileObjectForExport)objList[i];
                    IOUtil.writeInt(fs_bin, tile.X);
                    IOUtil.writeInt(fs_bin, tile.Y);
                    TileObjectElement tileObj = (TileObjectElement)tile.obj;
                    tileObj.ExportObject(fs_bin);
                }
            }
            IOUtil.writeShort(fs_bin, (short)stageList.getElementCount());
            //��ɫ�¼���
            if ((LEVEL_FLAG & Consts.LEVEL_OBJ_TRIGEER) != 0)
            {
                for (int si = 0; si < stageList.getElementCount(); si++)
                {
                    //���������ű�
                    StageElement stageElement = (StageElement)stageList.getElement(si);
                    byte lenKss = (byte)stageElement.scriptList.getElementCount();
                    IOUtil.writeByte(fs_bin, lenKss);
                    for (int i = 0; i < lenKss; i++)
                    {
                        ScriptFileElement element = (ScriptFileElement)stageElement.scriptList.getElement(i);
                        String file = (String)element.getValue();
                        int id = Convert.ToInt32(mapsManager.listExpScriptFiles.IndexOf(file));
                        IOUtil.writeShort(fs_bin, (short)id);
                    }
                    //����NPC���ݺͽű�
                    ArrayList objList = new ArrayList();
                    for (int i = 0; i < TILE_NB_X; i++)
                    {
                        for (int j = 0; j < TILE_NB_Y; j++)
                        {
                            if (mapData[i, j] == null || mapData[i, j].tile_objectList == null)//|| mapData[i, j].tile_objectList[si] == null
                            {
                            }
                            else
                            {
                                if (mapData[i, j].tile_objectList[si] != null && mapData[i, j].tile_objectList[si].antetype != null)
                                {
                                    objList.Add(new TileObjectForExport(i, j, mapData[i, j].tile_objectList));
                                }
                            }
                        }
                    }
                    IOUtil.writeShort(fs_bin, (short)objList.Count);
                    for (int i = 0; i < objList.Count; i++)
                    {
                        TileObjectForExport tile = (TileObjectForExport)objList[i];
                        IOUtil.writeInt(fs_bin, tile.X);
                        IOUtil.writeInt(fs_bin, tile.Y);
                        List<TileObjectElement> tileObj = (List<TileObjectElement>)tile.obj;
                        tileObj[si].ExportObject(fs_bin);
                    }
                }
            }
            //���ӳ����Ϣ(����ʹ�õ�ͼͼƬӳ��)
            //for (int si = 0; si < stageList.getElementCount(); si++)
            //{
            //    StageElement stageElement = (StageElement)stageList.getElement(si);
            //    IOUtil.writeShort(fs_bin, (short)stageElement.imgMappingList.getElementCount());
            //    for (int j = 0; j < stageElement.imgMappingList.getElementCount(); j++)
            //    {
            //        ImageMappingElement imgMapElement = (ImageMappingElement)stageElement.imgMappingList.getElement(j);
            //        imgMapElement.ExportObject(fs_bin);
            //    }
            //}
        }
        //����ʱ��ʱ�õķ��������
        class TileObjectForExport
        {
            public int X;
            public int Y;
            public Object obj;
            public TileObjectForExport(int XT, int YT, Object objT)
            {
                X = XT;
                Y = YT;
                obj = objT;
            }
        }
        //��ʾ������ͼ��
        public void displayTile(Graphics g, int mapBufferW, int mapBufferH, float zoomLevel, int startX, int startY, int endX, int endY)
        {
            if (g == null || mapBufferW <= 0 || mapBufferH<=0)
            {
                return;
            }
            if (endX > TILE_NB_X-1)
            {
                endX = TILE_NB_X - 1;
            }
            if (endY > TILE_NB_Y - 1)
            {
                endY = TILE_NB_Y - 1;
            }
            int posX = (int)((startX * TILE_W * zoomLevel)) % mapBufferW;
            int posY = (int)((startY * TILE_H * zoomLevel)) % mapBufferH;
            int x = posX;
            int y = posY;
            if (mapData != null)
            {
                for (int j = startY; j <= endY; j++)
                {
                    int xT = x;
                    for (int i = startX; i <= endX; i++)
                    {
                        GraphicsUtil.fillRect(g, xT, y, (int)(TILE_W * zoomLevel), (int)(TILE_H * zoomLevel), mapColor);
                        if (mapData[i, j] != null)
                        {
                            mapData[i, j].displayTile(g, xT, y, zoomLevel, null);
                        }
                        xT += (int)(TILE_W * zoomLevel);
                        xT %= mapBufferW;

                    }
                    y += (int)(this.TILE_H * zoomLevel);
                    y %= mapBufferH;
                }
            }
        }

        #region MParentNode ��Ա

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

        #region MSonNode ��Ա

        public MParentNode GetParent()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void SetParent(MParentNode parent)
        {
            mapsManager = (MapsManager)parent;
        }

        #endregion

        #region MSonNode ��Ա


        public string getValueToLenString()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
    //������ԪȺ��
    public class StageGroup : ObjectVector, SerializeAble
    {
        public Object parent = null;//������
        public StageGroup(Object parentT)
        {
            parent = parentT;
        }

        #region SerializeAble ��Ա

        public void ReadObject(Stream s)
        {
            this.removeAll();
            int len = IOUtil.readShort(s);
            for (int i = 0; i < len; i++)
            {
                StageElement element = new StageElement(this);
                element.ReadObject(s);
                addElement(element);
            }
        }

        public void WriteObject(Stream s)
        {
            short len = (short)getElementCount();
            IOUtil.writeShort(s, len);
            for (int i = 0; i < len; i++)
            {
                StageElement element = (StageElement)getElement(i);
                element.WriteObject(s);
            }
        }

        public void ExportObject(Stream fs_bin)
        {
            short len = (short)getElementCount();
            IOUtil.writeShort(fs_bin, len);
            for (int i = 0; i < len; i++)
            {
                StageElement element = (StageElement)getElement(i);
                element.WriteObject(fs_bin);
            }
        }

        #endregion
    }
    //������Ԫ
    public class StageElement : ObjectElement, SerializeAble
    {
        //�����ű��б�
        public ObjectVector scriptList = new ObjectVector();
        //ͼƬӳ���
        public ObjectVector imgMappingList = new ObjectVector();
        public StageElement(StageGroup parentT)
        {
            parent = parentT;
            name = "�½�����";
        }
        #region SerializeAble ��Ա

        public void ReadObject(Stream s)
        {
            name = IOUtil.readString(s);
            short sLen = IOUtil.readShort(s);
            for (int i = 0; i < sLen; i++)
            {
                ScriptFileElement obj = new ScriptFileElement(scriptList);
                obj.ReadObject(s);
                scriptList.addElement(obj);
            }
            //��ͼͼƬӳ��
            imgMappingList.removeAll();
            int imgMappingLen = IOUtil.readInt(s);
            for (int i = 0; i < imgMappingLen; i++)
            {
                ImageMappingElement imgMapElement = new ImageMappingElement(((MapElement)((StageGroup)parent).parent).mapsManager.form_Main.mapImagesManager);
                imgMapElement.ReadObject(s);
                imgMappingList.addElement(imgMapElement);
            }
        }

        public void WriteObject(Stream s)
        {
            IOUtil.writeString(s,name);
            short sLen = (short)scriptList.getElementCount();
            IOUtil.writeShort(s, sLen);
            for (int i = 0; i < sLen; i++)
            {
                ScriptFileElement obj = (ScriptFileElement)scriptList.getElement(i);
                obj.WriteObject(s);
            }
            //��ͼͼƬӳ��
            int imgMappingLen = imgMappingList.getElementCount();
            IOUtil.writeInt(s, imgMappingLen);
            for (int i = 0; i < imgMappingLen; i++)
            {
                ImageMappingElement imgMapElement = (ImageMappingElement)imgMappingList.getElement(i);
                imgMapElement.WriteObject(s);
            }
        }

        public void ExportObject(Stream fs_bin)
        {
            IOUtil.writeString(fs_bin,name);
        }

        #endregion

        public override string getValueToLenString()
        {
            return name;
        }

        public override int getUsedTime()
        {
            return 0;
        }
        public override ObjectElement clone()
        {
            StageElement newElement = new StageElement((StageGroup)parent);
            newElement.name = name;
            newElement.scriptList = scriptList;
            newElement.imgMappingList = imgMappingList;
            return newElement;
        }
        public StageElement cloneForExport(StageGroup parentT)
        {
            StageElement newElement=new StageElement(parentT);
            newElement.name = name;
            newElement.scriptList = scriptList;
            newElement.imgMappingList = imgMappingList;
            return newElement;
        }
    }
    /// <summary>
    /// ��ͼTILE��Ԫ��ӵ��һ����ͼTILE��Ԫ����Ϣ
    /// </summary>
    public class MapTileElement : SerializeAble
    {
        //���ṹ����
        public MapElement parent_mapElement;
        public TilePhysicsElement tile_physic = null;       //������
        public TransTileGfxElement tile_gfx_ground = null;    //ͼ�η���(����)
        public TransTileGfxElement tile_gfx_surface = null;   //ͼ�η���(�ر�)
        public TileObjectElement tile_object_bg = null;     //����(������β�)
        public TileObjectElement tile_object_mask = null;   //����(�޹ض����)
        public List<TileObjectElement> tile_objectList = new List<TileObjectElement>();       //����(��ɫ�¼��㣬���ȶ�Ӧ��������)

        public bool needRedraw = false;//��������������UI��ͼ
        //���л����������===================================================================
        #region SerializeAble Members

        public void ReadObject(System.IO.Stream s)
        {
            //���������Ƭ����
            short index = IOUtil.readShort(s);//������
            if (index >= 0)
            {
                tile_physic = parent_mapElement.mapsManager.tilePhysicsManager.getElement(index);
            }
            else
            {
                tile_physic = null;
            }
            index = IOUtil.readShort(s);//ͼ�ε��淽��
            if (index >= 0)
            {
                byte flag = IOUtil.readByte(s);
                tile_gfx_ground = new TransTileGfxElement((TileGfxElement)parent_mapElement.tileGfxContainer[index], flag);
            }
            else
            {
                tile_gfx_ground = null;
            }
            index = IOUtil.readShort(s);//ͼ�εر���
            if (index >= 0)
            {
                byte flag = IOUtil.readByte(s);
                tile_gfx_surface = new TransTileGfxElement((TileGfxElement)parent_mapElement.tileGfxContainer[index], flag);
            }
            else
            {
                tile_gfx_surface = null;
            }
            byte hasObj;
            //��ȡ�ײ���β�
            hasObj = IOUtil.readByte(s);
            if (hasObj > 0)
            {
                tile_object_bg = new TileObjectElement(this);
                tile_object_bg.ReadObject(s);
            }
            //��ȡ�޹ض����
            hasObj = IOUtil.readByte(s);
            if (hasObj > 0)
            {
                tile_object_mask = new TileObjectElement(this);
                tile_object_mask.ReadObject(s);
            }
            tile_objectList.Clear();
            for (int i = 0; i < parent_mapElement.stageList.getElementCount(); i++)
            {
                hasObj = IOUtil.readByte(s);
                TileObjectElement tile_objectTemp = null;
                if (hasObj > 0)
                {
                    tile_objectTemp = new TileObjectElement(this);
                    tile_objectTemp.ReadObject(s);
                }
                tile_objectList.Add(tile_objectTemp);
            }


        }

        public void WriteObject(System.IO.Stream s)
        {
            if (tile_physic != null)
            {
                IOUtil.writeShort(s, (short)parent_mapElement.mapsManager.tilePhysicsManager.getElementID(tile_physic));
            }
            else
            {
                IOUtil.writeShort(s, -1);
            }
            if (tile_gfx_ground != null && tile_gfx_ground.tileGfxElement!=null)
            {
                IOUtil.writeShort(s, (short)parent_mapElement.tileGfxContainer.GetSonID(tile_gfx_ground.tileGfxElement));
                IOUtil.writeByte(s, tile_gfx_ground.transFlag);
            }
            else
            {
                IOUtil.writeShort(s, -1);
            }
            if (tile_gfx_surface != null)
            {
                IOUtil.writeShort(s, (short)parent_mapElement.tileGfxContainer.GetSonID(tile_gfx_surface.tileGfxElement));
                IOUtil.writeByte(s, tile_gfx_surface.transFlag);
            }
            else
            {
                IOUtil.writeShort(s, -1);
            }
            byte hasObj = (byte)(tile_object_bg == null ? 0 : 1);
            IOUtil.writeByte(s, hasObj);
            if (hasObj > 0)
            {
                tile_object_bg.WriteObject(s);
            } 
            hasObj = (byte)(tile_object_mask == null ? 0 : 1);
            IOUtil.writeByte(s, hasObj);
            if (hasObj > 0)
            {
                tile_object_mask.WriteObject(s);
            }
            for (int i = 0; i < parent_mapElement.stageList.getElementCount(); i++)
            {
                if (i < tile_objectList.Count && tile_objectList[i] != null)
                {
                    hasObj = 1;
                }
                else
                {
                    hasObj = 0;
                }
                IOUtil.writeByte(s, hasObj);
                if (hasObj > 0)
                {
                    tile_objectList[i].WriteObject(s);
                }
            }


        }
        public void ExportObject(System.IO.Stream fs_bin)
        {
            //....
            IOUtil.writeShort(fs_bin, (short)parent_mapElement.tileGfxContainer.GetSonID(tile_gfx_ground.tileGfxElement));
        }
        #endregion

        //���캯��===========================================================================
        public MapTileElement(MapElement parentT)
        {
            this.parent_mapElement = parentT;
            tile_object_bg = new TileObjectElement(this);
            tile_object_mask = new TileObjectElement(this);
            tile_objectList.Add(new TileObjectElement(this));
        }
        public MapTileElement clone()
        {
            MapTileElement newClipElement = new MapTileElement(parent_mapElement);
            newClipElement.tile_physic = tile_physic.clone(parent_mapElement.mapsManager.tilePhysicsManager);
            newClipElement.tile_gfx_ground = new TransTileGfxElement(tile_gfx_ground.tileGfxElement.Clone(parent_mapElement.tileGfxContainer), tile_gfx_ground.transFlag);
            newClipElement.tile_gfx_surface =  new TransTileGfxElement(tile_gfx_surface.tileGfxElement.Clone(parent_mapElement.tileGfxContainer),tile_gfx_surface.transFlag);
            newClipElement.tile_object_bg = tile_object_bg.clone();
            newClipElement.tile_object_mask = tile_object_mask.clone();
            newClipElement.tile_objectList.Clear();
            for (int i = 0; i < tile_objectList.Count; i++)
            {
                newClipElement.tile_objectList.Add(tile_objectList[i].clone());
            }
            return newClipElement;
        }
        public MapTileElement cloneForExport(MapElement parentT)
        {
            MapTileElement newClipElement = new MapTileElement(parentT);
            int id = -1;
            if (tile_physic != null)
            {
                id = tile_physic.getID();
            }
            if (id >= 0)
            {
                newClipElement.tile_physic = parentT.mapsManager.tilePhysicsManager.getElement(id);
            }
            else
            {
                newClipElement.tile_physic = null;
            }
            id = -1;
            if (tile_gfx_ground != null && tile_gfx_ground.tileGfxElement!=null)
            {
                id = tile_gfx_ground.tileGfxElement.GetID();
            }
            if (id >= 0)
            {
                newClipElement.tile_gfx_ground = new TransTileGfxElement((TileGfxElement)parentT.tileGfxContainer[id],tile_gfx_ground.transFlag);
            }
            else
            {
                newClipElement.tile_gfx_ground = null;
            }
            id = -1;
            if (tile_gfx_surface != null && tile_gfx_surface.tileGfxElement!=null)
            {
                id = tile_gfx_surface.tileGfxElement.GetID();
            }
            if (id >= 0)
            {
                newClipElement.tile_gfx_surface = new TransTileGfxElement((TileGfxElement)parentT.tileGfxContainer[id],tile_gfx_surface.transFlag);
            }
            else
            {
                newClipElement.tile_gfx_surface = null;
            }
            if (tile_object_bg != null)
            {
                newClipElement.tile_object_bg = tile_object_bg.cloneForExceport(newClipElement);
            }
            else
            {
                newClipElement.tile_object_bg = null;
            }
            if (tile_object_mask!=null)
            {
                newClipElement.tile_object_mask = tile_object_mask.cloneForExceport(newClipElement);
            }
            else
            {
                newClipElement.tile_object_mask = null;
            }
            newClipElement.tile_objectList.Clear();
            for (int i = 0; i < tile_objectList.Count; i++)
            {
                if (tile_objectList[i] == null)
                {
                    newClipElement.tile_objectList.Add(null);
                }
                else
                {
                    TileObjectElement newObjElement = tile_objectList[i].cloneForExceport(newClipElement);
                    newClipElement.tile_objectList.Add(newObjElement);
                }
            }
            return newClipElement;
        }
        //��ո�������
        public void clearUpdateRegion(Graphics g,int xCenter,int yCenter)
        {
            if (tile_object_bg != null)
            {
                Rect bgSize = tile_object_bg.getSize();
                GraphicsUtil.clearRegion(g, xCenter + bgSize.X, yCenter + bgSize.Y, bgSize.W, bgSize.H);
            }
            if (tile_object_mask != null)
            {
                Rect bgSize=tile_object_mask.getSize();
                GraphicsUtil.clearRegion(g, xCenter + bgSize.X, yCenter + bgSize.Y, bgSize.W, bgSize.H);
            }
            if (tile_objectList != null && tile_objectList[parent_mapElement.mapsManager.form_Main.currentStageID]!=null)
            {
                Rect bgSize = tile_objectList[parent_mapElement.mapsManager.form_Main.currentStageID].getSize();
                GraphicsUtil.clearRegion(g, xCenter + bgSize.X, yCenter + bgSize.Y, bgSize.W, bgSize.H);
            }
        }
        //��Ԫ����===========================================================================
        //���ؿ��
        public int getWidth()
        {
            return parent_mapElement.getTileW();
        }
        //���ظ߶�
        public int getHeight(byte frameFlag)
        {
            return parent_mapElement.getTileH();
        }
        //��ʾ================================================================================
        //��ʾ�������ϽǶ���ķ�ʽ���Ƶ�Ŀ���(�����ţ����ڷ�ת��Ƭ��ʾ)
        public void displayTile(Graphics g, int destX, int destY, float zoomLevel, Rect limitClip)
        {
            if (!Consts.LVL_PHY_TOP)
            {
                if (tile_physic != null && (Consts.currentLevel == Consts.LEVEL_PHYSICS || Consts.levelEye) && Consts.LEVEL_ALPHA_FLAG_PHY > 0)
                {
                    tile_physic.display(g, destX, destY, (int)(zoomLevel * parent_mapElement.getTileW()), (int)(zoomLevel * parent_mapElement.getTileH()), Consts.showStringInPhyLevel, Consts.LEVEL_ALPHA_FLAG_PHY);
                }
            }
            if (tile_gfx_ground != null && tile_gfx_ground.tileGfxElement!=null && (Consts.currentLevel == Consts.LEVEL_TILE_BG || Consts.levelEye) && Consts.LEVEL_ALPHA_TILE_BG > 0)
            {
                byte flag = tile_gfx_ground.tileGfxElement.getTansFlag();
                flag = Consts.getTransFlag(flag, tile_gfx_ground.transFlag);
                tile_gfx_ground.tileGfxElement.display(g, destX, destY, zoomLevel, flag, limitClip, Consts.LEVEL_ALPHA_TILE_BG, ((StageElement)(parent_mapElement.stageList.getElement(parent_mapElement.mapsManager.form_Main.currentStageID))).imgMappingList);
                if (Consts.TILE_BG_GFX_ID)
                {
                    GraphicsUtil.drawBorderString(g, (int)(destX + parent_mapElement.getTileW() * zoomLevel / 2), (int)(destY + parent_mapElement.getTileH() * zoomLevel / 2), "" + tile_gfx_ground.tileGfxElement.GetID(), Consts.colorWhite, Consts.colorBlack, Consts.HCENTER | Consts.VCENTER);
                    //GraphicsUtil.drawNumberByImage(g,(int)flagInf, x + width / 2, y + height / 2, Consts.HCENTER | Consts.VCENTER);
                }
            }
            if (tile_gfx_surface != null && tile_gfx_surface.tileGfxElement!=null && (Consts.currentLevel == Consts.LEVEL_TILE_SUR || Consts.levelEye) && Consts.LEVEL_ALPHA_TILE_SUR > 0)
            {
                byte flag = tile_gfx_surface.tileGfxElement.getTansFlag();
                flag = Consts.getTransFlag(flag, tile_gfx_surface.transFlag);
                tile_gfx_surface.tileGfxElement.display(g, destX, destY, zoomLevel, flag, limitClip, Consts.LEVEL_ALPHA_TILE_SUR, ((StageElement)(parent_mapElement.stageList.getElement(parent_mapElement.mapsManager.form_Main.currentStageID))).imgMappingList);
                if (Consts.TILE_SUR_GFX_ID)
                {
                    GraphicsUtil.drawBorderString(g, (int)(destX + parent_mapElement.getTileW() * zoomLevel / 2), (int)(destY + parent_mapElement.getTileH() * zoomLevel / 2), "" + tile_gfx_surface.tileGfxElement.GetID(), Consts.colorWhite, Consts.colorBlack, Consts.HCENTER | Consts.VCENTER);
                    //GraphicsUtil.drawNumberByImage(g,(int)flagInf, x + width / 2, y + height / 2, Consts.HCENTER | Consts.VCENTER);
                }
            }
            if (Consts.LVL_PHY_TOP)
            {
                if (tile_physic != null && (Consts.currentLevel == Consts.LEVEL_PHYSICS || Consts.levelEye) && Consts.LEVEL_ALPHA_FLAG_PHY > 0)
                {
                    tile_physic.display(g, destX, destY, (int)(zoomLevel * parent_mapElement.getTileW()), (int)(zoomLevel * parent_mapElement.getTileH()), Consts.showStringInPhyLevel, Consts.LEVEL_ALPHA_FLAG_PHY);
                }
            }
        }
        //��ʾ����(destX,destY Ϊ�������Ͻ�)
        public void disPlayObj(Graphics g, int destX, int destY, Rect limitClip,int level)
        {
            if ((level & Consts.LEVEL_TILE_OBJ)!=0 && tile_object_bg != null && (Consts.currentLevel == Consts.LEVEL_TILE_OBJ || Consts.levelEye) && Consts.LEVEL_ALPHA_TILE_OBJ > 0)
            {
                tile_object_bg.display(g, destX, destY, 1,Consts.LEVEL_ALPHA_TILE_OBJ);
            }
            if ((level & Consts.LEVEL_OBJ_MASK)!=0 && tile_object_mask != null && (Consts.currentLevel == Consts.LEVEL_OBJ_MASK || Consts.levelEye) && Consts.LEVEL_ALPHA_OBJ_MASK > 0)
            {
                tile_object_mask.display(g, destX, destY, 1, Consts.LEVEL_ALPHA_OBJ_MASK);
            }
            if ((level & Consts.LEVEL_OBJ_TRIGEER)!=0 && tile_objectList != null && (Consts.currentLevel == Consts.LEVEL_OBJ_TRIGEER || Consts.levelEye) && Consts.LEVEL_ALPHA_OBJ_TRIGEER > 0)
            {
                if (tile_objectList[parent_mapElement.mapsManager.form_Main.currentStageID]!=null&&tile_objectList[parent_mapElement.mapsManager.form_Main.currentStageID].antetype != null)
                {
                    tile_objectList[parent_mapElement.mapsManager.form_Main.currentStageID].display(g, destX, destY, 1, Consts.LEVEL_ALPHA_OBJ_TRIGEER);
                }
            }
        }
        //��ɢ�����
        public void exploreDirtyRect()
        {
            int indexX = -1;
            int indexY = -1;
            bool find = false;
            for (int i = 0; i < parent_mapElement.getMapW(); i++)
            {
                for (int j = 0; j < parent_mapElement.getMapH(); j++)
                {
                    if (parent_mapElement.mapData[i, j].Equals(this))
                    {
                        indexX = i;
                        indexY = j;
                        find = true;
                        break;
                    }
                }
                if (find)
                {
                    break;
                }
            }
            if (!find)
            {
                return;
            }
            parent_mapElement.mapData[indexX, indexY].needRedraw = true;
            //��ɢ�����(�����㷨)
            int dirtyW = 3;
            int dirtyH = 3;
            for (int dI = indexX - dirtyW; dI < indexX + dirtyW; dI++)
            {
                if (dI < 0 || dI >= parent_mapElement.getMapW())
                {
                    continue;
                }
                for (int dJ = indexY - dirtyH; dJ < indexY + dirtyH; dJ++)
                {
                    if (dJ < 0 || dJ >= parent_mapElement.getMapH())
                    {
                        continue;
                    }
                    parent_mapElement.mapData[dI, dJ].needRedraw = true;
                }
            }
        }
    }
    /// <summary>
    /// ��ͼ���󷽸��࣬����ӵ��һ����ɫ������Ϣ��һЩ��������Ϣ��
    /// </summary>
    public class TileObjectElement : SerializeAble
    {
        public MapTileElement mapTileElement = null;//����
        public Antetype antetype = null;//��������
        public short NpcID = 0;//NPC���
        public short actionID = 0;//��ʼ����ID
        public short startTime = 0;//��ʼʱ��֡ID
        public bool isVisible = true;//�Ƿ��ڿɼ�״̬
        public short trigerID = -1;//���������(����)
        public ObjectVector scriptList = new ObjectVector();//NPC�ű��б�
        public TileObjKeyValueManager keyValueManager = new TileObjKeyValueManager();
        public TileObjectElement(MapTileElement mapTileElementT)
        {
            mapTileElement = mapTileElementT;
        }
        public TileObjectElement clone()
        {
            TileObjectElement newInstance = new TileObjectElement(mapTileElement);
            newInstance.antetype = antetype;
            newInstance.NpcID = NpcID;
            newInstance.actionID = actionID;
            newInstance.startTime = startTime;
            newInstance.isVisible = isVisible;
            newInstance.trigerID = trigerID;
            for (int i = 0; i < scriptList.getElementCount(); i++)
            {
                ScriptFileElement element = (ScriptFileElement)scriptList.getElement(i);
                newInstance.scriptList.addElement(element.clone());
            }
            newInstance.keyValueManager = keyValueManager.Clone();
            return newInstance;
        }
        public TileObjectElement cloneForExceport(MapTileElement mapTileElementT)
        {
            TileObjectElement newInstance = new TileObjectElement(mapTileElementT);
            newInstance.antetype = antetype;
            newInstance.NpcID = NpcID;
            newInstance.actionID = actionID;
            newInstance.startTime = startTime;
            newInstance.isVisible = isVisible;
            newInstance.trigerID = trigerID;
            for (int i = 0; i < scriptList.getElementCount(); i++)
            {
                ScriptFileElement element = (ScriptFileElement)scriptList.getElement(i);
                newInstance.scriptList.addElement(element.cloneForExceport(newInstance.scriptList));
            }
            newInstance.keyValueManager = keyValueManager.Clone();
            return newInstance;
        }
        public bool usingAntetype(Antetype antetypeT)
        {
            if (antetype != null && antetype.Equals(antetypeT))
            {
                return true;
            }
            return false;
        }
        public void setAntetype(Antetype antetypeT)
        {
            antetype = antetypeT;
        }
        public void clearAntetype()
        {
            antetype = null;
            actionID = 0;
            startTime = 0;
        }
        //��������
        public void display(Graphics g, int x, int y, float zoomLevel)
        {
            display(g, x, y, zoomLevel, 0xFF);
        }
        private static Image imgAnchor = null;
        public void display(Graphics g, int x, int y, float zoomLevel, int alpha)
        {
            int tileW = (int)(zoomLevel * mapTileElement.parent_mapElement.getTileW());
            int tileH = (int)(zoomLevel * mapTileElement.parent_mapElement.getTileH());
            if (antetype != null)
            {
                bool paintActor = antetype.display(g, x, y, (int)(zoomLevel * mapTileElement.parent_mapElement.getTileW()), (int)(zoomLevel * mapTileElement.parent_mapElement.getTileH()), zoomLevel, actionID, startTime, false, alpha);
                if (paintActor)
                {
                    //���Ƶ׿�
                    Rect region = null;
                    if (antetype.Actor != null)
                    {
                        region = antetype.Actor.getBoxGDI(actionID, startTime);
                    }
                    if (region == null)
                    {
                        region = new Rect(0, 0, mapTileElement.parent_mapElement.getTileW(), mapTileElement.parent_mapElement.getTileH());
                    }
                    int xT = (int)(x + zoomLevel * (mapTileElement.parent_mapElement.getTileW() / 2 + region.X));
                    int yT = (int)(y + zoomLevel * (mapTileElement.parent_mapElement.getTileH() / 2 + region.Y));
                    if (((Consts.LVL_Obj_FrameID && startTime != 0) || (Consts.LVL_Obj_NPC_ID && NpcID != 0) || Consts.LVL_Obj_Anchor))
                    {
                        GraphicsUtil.drawRect(g, xT, yT, (int)((region.W + 1) * zoomLevel), (int)((region.H + 1) * zoomLevel), 0XFF);
                    }
                    if (Consts.LVL_Obj_FrameID && startTime != 0)
                    {
                        int w = MathUtil.getLenOfInt(startTime) * 5 + 2;
                        GraphicsUtil.fillRect(g, xT, yT, w, 10, 0xFF);
                        GraphicsUtil.drawNumberByImage(g, startTime, xT + 1, yT + 1, 0);
                    }
                    if (Consts.LVL_Obj_NPC_ID && NpcID != 0)
                    {
                        int w = MathUtil.getLenOfInt(NpcID) * 5 + 2;
                        GraphicsUtil.fillRect(g, xT + (int)((region.W + 1 - w) * zoomLevel), yT, w, 10, 0xFF);
                        GraphicsUtil.drawNumberByImage(g, NpcID, xT + (int)((region.W) * zoomLevel), yT + 1, Consts.RIGHT);
                    }
                    if (Consts.LVL_Obj_Anchor)
                    {
                        //int anchorLen = 4;
                        xT = (int)(x + zoomLevel * (mapTileElement.parent_mapElement.getTileW() / 2));
                        yT = (int)(y + zoomLevel * (mapTileElement.parent_mapElement.getTileH() / 2));
                        if (imgAnchor == null)
                        {
                            imgAnchor = global::Cyclone.Properties.Resources.transformCenter;
                        }
                        if (imgAnchor != null)
                        {
                            GraphicsUtil.drawClip(g, imgAnchor, xT - imgAnchor.Width / 2, yT - imgAnchor.Height / 2, 0, 0, imgAnchor.Width, imgAnchor.Height, 0);
                        }
                    }
                }
            }
        }
        //��������߿����Ķ���
        public void displayBorder(Graphics g, int x, int y, float zoomLevel)
        {
            if (antetype == null)
            {
                return;
            }
            MActor theActor = antetype.Actor;
            if (theActor == null)
            {
                return;
            }
            Rect theRect = theActor.getBoxGDI(actionID, startTime);
            if (theRect != null)
            {
                GraphicsUtil.drawRect(g, x + (int)(theRect.X * zoomLevel), y + (int)(theRect.Y * zoomLevel), (int)((theRect.W + 1) * zoomLevel), (int)((theRect.H + 1) * zoomLevel), 0xFF0000);
            }
        }
        private static Rect mySize = new Rect(0, 0, 0, 0);
        public Rect getSize()
        {
            if (antetype == null || antetype.Actor == null)
            {
                mySize.clear();
            }
            else
            {
                //..
                //mySize = antetype.actor.getSize(actionID, frameID);
            }
            return mySize;
        }
        public Rect clearDirtyRegion(Graphics g, int x, int y, int tileW, int tileH)
        {
            Rect size = getSize();
            if (size.isEmpty())
            {
                GraphicsUtil.clearRegion(g, x, y, tileW, tileH);
                return null;
            }
            else
            {
                GraphicsUtil.clearRegion(g, x + tileW / 2 + size.X, y + tileH / 2 + size.Y, size.W, size.H);
                return size;
            }
        }
        public short[] getAntetypeID()
        {
            short[] value = new short[2];
            value[0] = -1;
            value[1] = -1;
            if (antetype != null)
            {
                value[0] = (short)antetype.getFolder().GetID();
                value[1] = (short)antetype.GetID();
            }
            return value;
        }
        //���л����������===================================================================

        #region SerializeAble Members

        public void ReadObject(System.IO.Stream s)
        {
            short[] atIndex = new short[] { -1, -1 };
            atIndex[0] = IOUtil.readShort(s);
            atIndex[1] = IOUtil.readShort(s);
            if (atIndex[0] >= 0 && atIndex[1] >= 0)
            {
                antetype = mapTileElement.parent_mapElement.mapsManager.antetypesManager[atIndex[0]][atIndex[1]];
            }
            this.actionID = IOUtil.readShort(s);
            this.startTime = IOUtil.readShort(s);
            trigerID = IOUtil.readShort(s);
            this.NpcID = IOUtil.readShort(s);
            isVisible = IOUtil.readBoolean(s);
            short sLen = IOUtil.readShort(s);
            for (int i = 0; i < sLen; i++)
            {
                ScriptFileElement obj = new ScriptFileElement(scriptList);
                obj.ReadObject(s);
                scriptList.addElement(obj);
            }
            short kvLen = IOUtil.readShort(s);
            for (int i = 0; i < kvLen; i++)
            {
                TileObjKeyValue obj = new TileObjKeyValue(keyValueManager);
                obj.ReadObject(s);
                keyValueManager.Add(obj);
            }
        }
        public void WriteObject(System.IO.Stream s)
        {
            short[] atIndex = getAntetypeID();
            IOUtil.writeShort(s, atIndex[0]);
            IOUtil.writeShort(s, atIndex[1]);
            IOUtil.writeShort(s, actionID);
            IOUtil.writeShort(s, startTime);
            IOUtil.writeShort(s, trigerID);
            IOUtil.writeShort(s, NpcID);
            IOUtil.writeBoolean(s, isVisible);
            short sLen = (short)scriptList.getElementCount();
            IOUtil.writeShort(s, sLen);
            for (int i = 0; i < sLen; i++)
            {
                ScriptFileElement obj = (ScriptFileElement)scriptList.getElement(i);
                obj.WriteObject(s);
            }
            short kvLen = (short)keyValueManager.Count();
            IOUtil.writeShort(s, kvLen);
            for (int i = 0; i < kvLen; i++)
            {
                TileObjKeyValue obj = keyValueManager[i];
                obj.WriteObject(s);
            }
        }
        public void ExportObject(System.IO.Stream fs_bin)
        {
            if (antetype == null || antetype.Actor == null)
            {
                MessageBox.Show("��������ô��������ͼ");
                return;
            }
            short anteTypeID = (short)(antetype.GetID());
            short actorFolderID = (short)(((MActorFolder)antetype.Actor.GetParent()).GetID());
            short actorID = (short)(antetype.Actor.GetID());
            IOUtil.writeShort(fs_bin, NpcID);
            IOUtil.writeShort(fs_bin, anteTypeID);
            IOUtil.writeShort(fs_bin, actorFolderID);
            IOUtil.writeShort(fs_bin, actorID);
            IOUtil.writeShort(fs_bin, actionID);
            IOUtil.writeShort(fs_bin, startTime);
            IOUtil.writeBoolean(fs_bin, isVisible);
            //����ű�ʹ����Ϣ
            if (NpcID > 0)
            {
                byte lenKss = (byte)scriptList.getElementCount();
                IOUtil.writeByte(fs_bin, lenKss);
                for (int i = 0; i < lenKss; i++)
                {
                    ScriptFileElement element = (ScriptFileElement)scriptList.getElement(i);
                    String file = (String)element.getValue();
                    int id = Convert.ToInt32(mapTileElement.parent_mapElement.mapsManager.listExpScriptFiles.IndexOf(file));
                    IOUtil.writeShort(fs_bin, (short)id);
                }
                //�����ֵ��
                short kvLen = (short)keyValueManager.Count();
                IOUtil.writeShort(fs_bin, kvLen);
                for (int i = 0; i < kvLen; i++)
                {
                    TileObjKeyValue obj = keyValueManager[i];
                    obj.ExportObject(fs_bin);
                }
            }
        }
        #endregion
    }
    //��ͼ����ļ�ֵ�Թ�����
    public class TileObjKeyValueManager:MNode<TileObjKeyValue>
    {
        public TileObjKeyValueManager()
        {
        }
        public TileObjKeyValueManager Clone()
        {
            TileObjKeyValueManager newInstance = new TileObjKeyValueManager();
            foreach (TileObjKeyValue keyValue in m_sonList)
            {
                newInstance.Add(keyValue.Clone(newInstance));
            }
            return newInstance;
        }
    }
    //��ͼ����ļ�ֵ�Թ�����
    public class TileObjKeyValue : MIO, MParentNode, MSonNode
    {
        TileObjKeyValueManager manager;
        public String strKey="";
        public String strValue = "";
        public TileObjKeyValue()
        {
        }
        public TileObjKeyValue(TileObjKeyValueManager managerT)
        {
            manager = managerT;
        }
        public TileObjKeyValue Clone()
        {
            return Clone(manager);
        }
        public TileObjKeyValue Clone(TileObjKeyValueManager managerT)
        {
            TileObjKeyValue newInstance = new TileObjKeyValue(managerT);
            newInstance.strKey = strKey + "";
            newInstance.strValue = strValue + "";
            return newInstance;
        }
        public int GetID()
        {
            return manager.GetSonID(this);
        }
        #region MIO ��Ա

        public void ReadObject(Stream s)
        {
            strKey = IOUtil.readString(s);
            strValue = IOUtil.readString(s);
        }

        public void WriteObject(Stream s)
        {
            IOUtil.writeString(s, strKey);
            IOUtil.writeString(s, strValue);
        }

        public void ExportObject(Stream s)
        {
            IOUtil.writeString(s, strKey);
            IOUtil.writeString(s, strValue);
        }

        #endregion

        #region MParentNode ��Ա

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

        #region MSonNode ��Ա

        public MParentNode GetParent()
        {
            return manager;
        }

        public void SetParent(MParentNode parent)
        {
            manager = (TileObjKeyValueManager)parent;
        }

        #endregion

        public void setKeyValue(string strKeyT, string strValueT)
        {
            if (strKeyT == null || strValueT == null)
            {
                return;
            }
            this.strKey = strKeyT;
            this.strValue = strValueT;
        }

        #region MSonNode ��Ա


        public string getValueToLenString()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
