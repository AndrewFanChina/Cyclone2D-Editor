using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.alg;
using System.Collections;
using Cyclone.mod;
using System.Diagnostics;
using Cyclone.mod.anim;
using System.Drawing;
using Cyclone.alg.opengl;
using Cyclone.mod.animimg;

namespace Cyclone.mod.anim
{
    public class HistoryItem
    {
        public HistoryItem()
        {
        }
        public virtual void Undo()
        {
        }
        public virtual void Redo()
        {
        }
    }
    public class HistoryManager
    {
        private int m_maxUndoLevel;
        private ArrayList m_undoList;
        private Stack m_redoStack;
        public HistoryManager()
        {
            m_maxUndoLevel = 300;
            m_undoList = new ArrayList();
            m_redoStack = new Stack();
        }
        public HistoryManager(int m_maxUndoLevelT)
        {
            m_maxUndoLevel = m_maxUndoLevelT;
            m_undoList = new ArrayList();
            m_redoStack = new Stack();
        }
        public int MaxUndoLevel
        {
            get
            {
                return m_maxUndoLevel;
            }
            set
            {
                Debug.Assert(value >= 0);
                if (value != m_maxUndoLevel)
                {
                    ClearHistory();
                    m_maxUndoLevel = value;
                }
            }
        }
        public void AddItem(HistoryItem item)
        {
            Debug.Assert(item != null);
            Debug.Assert(m_undoList.Count <= m_maxUndoLevel);

            if (m_maxUndoLevel == 0)
                return;

            if (m_undoList.Count == m_maxUndoLevel)
            {
                // Remove the oldest entry from the undo list to make room.
                item = (HistoryItem)m_undoList[0];
                m_undoList.RemoveAt(0);
            }

            // Insert the new undoable command into the undo list.
            m_undoList.Add(item);

            // Clear the redo stack.
            ClearRedo();
        }

        public virtual void ClearHistory()
        {
            ClearUndo();
            ClearRedo();
        }
        public bool CanUndo()
        {
            return m_undoList.Count > 0;
        }
        public bool CanRedo()
        {
            return m_redoStack.Count > 0;
        }

        public void Undo()
        {
            if (!CanUndo())
                return;

            // Remove newest entry from the undo list.
            HistoryItem info = (HistoryItem)m_undoList[m_undoList.Count - 1];
            m_undoList.RemoveAt(m_undoList.Count - 1);

            // Perform the undo.
            Debug.Assert(info != null);
            info.Undo();
            m_redoStack.Push(info);
        }
        public void Redo()
        {
            if (!CanRedo())
                return;

            // Remove newest entry from the redo stack.
            HistoryItem info = (HistoryItem)m_redoStack.Pop();

            // Perform the redo.
            Debug.Assert(info != null);
            info.Redo();
            m_undoList.Add(info);
        }

        public HistoryItem GetNextMA_UndoItem()
        {
            if (m_undoList.Count == 0)
                return null;
            HistoryItem info = (HistoryItem)m_undoList[m_undoList.Count - 1];
            return info;
        }
        public HistoryItem GetNextRedoItem()
        {
            if (m_redoStack.Count == 0)
                return null;
            HistoryItem info = (HistoryItem)m_redoStack.Peek();
            return info;
        }

        private void ClearUndo()
        {
            while (m_undoList.Count > 0)
            {
                m_undoList.RemoveAt(m_undoList.Count - 1);
            }
        }
        private void ClearRedo()
        {
            while (m_redoStack.Count > 0)
            {
                m_redoStack.Pop();
            }
        }
    }
    public class HistoryItem_MA : HistoryItem
    {
        protected MA_HistoryManager container;
        protected HistoryValue preValue;
        protected HistoryValue nextValue;
        public HistoryItem_MA(MA_HistoryManager containerT)
        {
            container = containerT;
        }
        public void setValue(HistoryValue preValueT, HistoryValue nextValueT)
        {
            preValue = preValueT;
            nextValue = nextValueT;
        }
        public override void Undo()
        {
            container.form_MA.form_MFrameEdit.ClearClipboard();
            container.form_MA.form_MTimeLine.ClearClipboard();
            updateTo(preValue);
        }
        public override void Redo()
        {
            container.form_MA.form_MFrameEdit.ClearClipboard();
            container.form_MA.form_MTimeLine.ClearClipboard();
            updateTo(nextValue);
        }
        protected virtual void updateTo(HistoryValue value)
        { 
        }
    }
    public class HistoryValue
    {

    }
    public class MA_HistoryManager : HistoryManager
    {
        public Form_MAnimation form_MA;
        public MA_HistoryManager(Form_MAnimation form_MAT)
            : base()
        {
            form_MA = form_MAT;
        }
        //ǰһ��¼ֵ
        private HistoryValue_Action preValue_Action = null;//����
        private HistoryValue_Actor preValue_Actor = null;  //��ɫ
        private HistoryValue_Clips preValue_Clips = null;  //��Ƭ
        private HistoryValue_Imgs preValue_Imgs = null;    //ͼƬ
        private HistoryValue_ImgRename preValue_ImgRename = null;//ͼƬ����
        private HistoryValue_ImgProp preValue_ImgProp= null;     //ͼƬ����
        //׼����ʷ��¼
        public void ReadyHistory(HistoryType type)
        {
            if (type == HistoryType.Action)
            {
                if (form_MA.form_MTimeLine != null && form_MA.form_MTimeLine.currentTimeLineHoder != null)
                {
                    preValue_Action = getHistoryValue_Action();
                }
            }
            if (type == HistoryType.Actor)
            {
                if (form_MA.form_MActorList != null && form_MA.form_MActorList.actorsManager != null)
                {
                    preValue_Actor = getHistoryValue_Actor();
                }
            }
            if (type == HistoryType.Clips)
            {
                if (form_MA.form_MImgsList != null && form_MA.form_MImgsList.MClipsManager != null)
                {
                    preValue_Clips = getHistoryValue_Clips();
                }
            }
            if (type == HistoryType.Imgs)
            {
                if (form_MA.form_MImgsList != null && form_MA.form_MImgsList.mImgsManager != null)
                {
                    preValue_Imgs = getHistoryValue_Imgs();
                }
            }
        }
        //������ʷ��¼
        public void AddHistory(HistoryType type)
        {
            if (type == HistoryType.Action)
            {
                if (form_MA.form_MTimeLine != null && form_MA.form_MTimeLine.currentTimeLineHoder != null && preValue_Action != null)
                {
                    HistoryItem_Action item = new HistoryItem_Action(this);
                    item.setValue(preValue_Action, getHistoryValue_Action());
                    AddItem(item);
                }
            }
            if (type == HistoryType.Actor)
            {
                if (form_MA.form_MActorList != null && form_MA.form_MActorList.actorsManager != null && preValue_Actor!=null)
                {
                    HistoryItem_Actor item = new HistoryItem_Actor(this);
                    item.setValue(preValue_Actor, getHistoryValue_Actor());
                    AddItem(item);
                }
            }
            if (type == HistoryType.Clips)
            {
                if (form_MA.form_MImgsList != null && form_MA.form_MImgsList.MClipsManager != null && preValue_Clips!=null)
                {
                    HistoryItem_Clips item = new HistoryItem_Clips(this);
                    item.setValue(preValue_Clips, getHistoryValue_Clips());
                    AddItem(item);
                }
            }
            if (type == HistoryType.Imgs)
            {
                if (form_MA.form_MImgsList != null && form_MA.form_MImgsList.mImgsManager != null && preValue_Imgs != null)
                {
                    HistoryItem_Imgs item = new HistoryItem_Imgs(this);
                    item.setValue(preValue_Imgs, getHistoryValue_Imgs());
                    AddItem(item);
                }
            }
            form_MA.refreshHistoryButtons();
            ReadyHistory(type);
        }
        //��ʷ��¼--------ͼƬ������
        public void ReadyHistory_ImgRename(MImgElement imgelement)
        {
            preValue_ImgRename = new HistoryValue_ImgRename();
            preValue_ImgRename.imgElement = imgelement;
            preValue_ImgRename.destName = imgelement.name;
        }
        public void AddHistory_ImgRename(MImgElement imgelement)
        {
            HistoryValue_ImgRename nextValue_ImgRename = new HistoryValue_ImgRename();
            nextValue_ImgRename.imgElement = imgelement;
            nextValue_ImgRename.destName = imgelement.name;

            HistoryItem_ImgRename item = new HistoryItem_ImgRename(this);
            item.setValue(preValue_ImgRename, nextValue_ImgRename);
            AddItem(item);
            form_MA.refreshHistoryButtons();
            ReadyHistory_ImgRename(imgelement);
        }
        //��ʷ��¼--------ͼƬ��������
        public void ReadyHistory_ImgProp(List<MImgElement> imgElements)
        {
            preValue_ImgProp = new HistoryValue_ImgProp();
            foreach (MImgElement element in imgElements)
            {
                ImgProp imgProp = new ImgProp();
                imgProp.imgElement = element;
                imgProp.forbidOptimize = element.forbidOptimize;
                imgProp.strAlphaImage = element.strAlphaImage;
                imgProp.strPmt = element.strPmt;
                imgProp.alpha = element.alpha;
                imgProp.linkID = element.linkID;
                preValue_ImgProp.imgProps.Add(imgProp);
            }
        }
        public void AddHistory_ImgProp(List<MImgElement> imgElements)
        {
            HistoryValue_ImgProp nextValue_ImgProp = new HistoryValue_ImgProp();
            foreach (MImgElement element in imgElements)
            {
                ImgProp imgProp = new ImgProp();
                imgProp.imgElement = element;
                imgProp.forbidOptimize = element.forbidOptimize;
                imgProp.strAlphaImage = element.strAlphaImage;
                imgProp.strPmt = element.strPmt;
                imgProp.alpha = element.alpha;
                imgProp.linkID = element.linkID;
                nextValue_ImgProp.imgProps.Add(imgProp);
            }

            HistoryItem_ImgProp item = new HistoryItem_ImgProp(this);
            item.setValue(preValue_ImgProp, nextValue_ImgProp);
            AddItem(item);
            form_MA.refreshHistoryButtons();
            ReadyHistory_ImgProp(imgElements);
        }
        //��ȡ��¼ֵ_����
        private HistoryValue_Action getHistoryValue_Action()
        {
            HistoryValue_Action HV_Action = new HistoryValue_Action();
            HV_Action.timeLineHoder = form_MA.form_MTimeLine.currentTimeLineHoder.Clone();
            HV_Action.timeLinePos = Form_MTimeLine.timePosition;
            HV_Action.actorFolderID = form_MA.form_MActorList.currentActorFolder.GetID();
            HV_Action.actorID = form_MA.form_MActorList.currentActorElement.GetID();
            HV_Action.actionID = form_MA.form_MActorList.currentActionElement.GetID();
            return HV_Action;
        }
        //��ȡ��¼ֵ_��ɫ
        private HistoryValue_Actor getHistoryValue_Actor()
        {
            HistoryValue_Actor HV_Actor = new HistoryValue_Actor();
            HV_Actor.actorsManager = form_MA.form_MActorList.actorsManager.Clone();
            HV_Actor.timeLinePos = Form_MTimeLine.timePosition;
            HV_Actor.actorFolderID = -1;
            if (form_MA.form_MActorList.currentActorFolder != null)
            {
                HV_Actor.actorFolderID = form_MA.form_MActorList.currentActorFolder.GetID();
            }
            HV_Actor.actorID = -1;
            if (form_MA.form_MActorList.currentActorElement != null)
            {
                HV_Actor.actorID=form_MA.form_MActorList.currentActorElement.GetID();
            }
            HV_Actor.actionID = -1;
            if (form_MA.form_MActorList.currentActionElement != null)
            {
                HV_Actor.actionID=form_MA.form_MActorList.currentActionElement.GetID();
            }
            return HV_Actor;
        }
        //��ȡ��¼ֵ_��Ƭ
        private HistoryValue_Clips getHistoryValue_Clips()
        {
            HistoryValue_Clips HV_Clips = new HistoryValue_Clips();
            MClipsManager  manager= form_MA.form_MImgsList.MClipsManager;
            HV_Clips.clipsManager = manager;
            for (int i=0;i<manager.Count();i++)
            {
                MClipElement clip = manager[i];
                HV_Clips.clipsData.Add(clip.getMClipData());
            }
            HV_Clips.imgListIndex = form_MA.form_MImgsList.listBox_Images.SelectedIndex;
            return HV_Clips;
        }
        //��ȡ��¼ֵ_ͼƬ
        private HistoryValue_Imgs getHistoryValue_Imgs()
        {
            HistoryValue_Imgs HV_Imgs = new HistoryValue_Imgs();
            MImgsManager manager = form_MA.form_MImgsList.mImgsManager;
            HV_Imgs.imgsManager = manager.CloneReference();
            for (int i = 0; i < manager.Count(); i++)
            {
                MImgElement img = manager[i];
                HV_Imgs.imgsData.Add(img.getMImgData());
            }
            HV_Imgs.imgListIndex = form_MA.form_MImgsList.listBox_Images.SelectedIndex;
            return HV_Imgs;
        }
        //�����ʷ��¼
        public override void ClearHistory()
        {
            base.ClearHistory();
            preValue_Action = null;
            form_MA.refreshHistoryButtons();
        }
    }
    //��ʷ��¼����
    public enum HistoryType
    {
        Action,     //�����ṹ
        Actor,      //��ɫ�ṹ
        Clips,      //��Ƭ����
        Imgs,       //ͼƬ����
    }
    //��ʷ��¼��Ŀ_����
    public class HistoryItem_Action : HistoryItem_MA
    {
        public HistoryItem_Action(MA_HistoryManager containerT):base(containerT)
        { 
        }
        protected override void updateTo(HistoryValue valueT)
        {
            HistoryValue_Action value = (HistoryValue_Action)valueT;
            MActorsManager MAM = container.form_MA.form_MActorList.actorsManager;
            //������������
            MAM[value.actorFolderID][value.actorID][value.actionID] = (MAction)value.timeLineHoder;
            container.form_MA.form_MActorList.setCurrentActorFolder(value.actorFolderID, false, 0);
            container.form_MA.form_MActorList.setCurrentActor(value.actorID, false, 1);
            container.form_MA.form_MActorList.setCurrentAction(value.actionID, false, 2);
            container.form_MA.form_MTimeLine.setTimeLinePos(value.timeLinePos);
            container.form_MA.refreshActionUIs();
        }
    }
    //��ʷ��¼��Ŀֵ_����
    public class HistoryValue_Action : HistoryValue
    {
        public MTimeLineHoder timeLineHoder;//ʱ����ӵ������ֵ
        public int timeLinePos;             //��ʱ��ʱ���λ��
        public int actorFolderID;           //��ɫ�ļ���ID
        public int actorID;                 //��ɫID
        public int actionID;                //��ɫ����
    }
    //��ʷ��¼��Ŀ_��ɫ
    public class HistoryItem_Actor : HistoryItem_MA
    {
        public HistoryItem_Actor(MA_HistoryManager containerT)
            : base(containerT)
        { 
        }
        protected override void updateTo(HistoryValue valueT)
        {
            HistoryValue_Actor value = (HistoryValue_Actor)valueT;
            container.form_MA.form_MActorList.actorsManager = (MActorsManager)value.actorsManager;
            container.form_MA.form_MActorList.updateTreeView_Animation();
            container.form_MA.form_MActorList.setCurrentActorFolder(value.actorFolderID, false, -1);
            container.form_MA.form_MActorList.setCurrentActor(value.actorID, false, -1);
            container.form_MA.form_MActorList.setCurrentAction(value.actionID, false, 2);
            container.form_MA.form_MTimeLine.setTimeLinePos(value.timeLinePos);
            container.form_MA.refreshActionUIs();
        }
    }
    //��ʷ��¼��Ŀֵ_��ɫ
    public class HistoryValue_Actor : HistoryValue
    {
        public MActorsManager actorsManager;//��ɫ������
        public int timeLinePos;             //��ʱ��ʱ���λ��
        public int actorFolderID;           //��ɫ�ļ���ID
        public int actorID;                 //��ɫID
        public int actionID;                //��ɫ����
    }
    //��ʷ��¼��Ŀ_��Ƭ����
    public class HistoryItem_Clips : HistoryItem_MA
    {
        public HistoryItem_Clips(MA_HistoryManager containerT)
            : base(containerT)
        { 
        }
        protected override void updateTo(HistoryValue valueT)
        {
            HistoryValue_Clips value = (HistoryValue_Clips)valueT;
            container.form_MA.form_MImgsList.MClipsManager = value.clipsManager;
            value.setDataToClip();
            container.form_MA.form_MImgsList.releaseFocus();
            container.form_MA.form_MImgsList.updateAllList(value.imgListIndex);
            container.form_MA.refreshActionUIs();
        }
    }
    //��ʷ��¼��Ŀֵ_��Ƭ����
    public class HistoryValue_Clips : HistoryValue
    {
        public MClipsManager clipsManager;   //��Ƭ������-�����ò����޸�
        public List<MClipData> clipsData = new List<MClipData>();//��Ƭ�����б�
        public int imgListIndex;             //ͼƬ�б�ID
        public void setDataToClip()
        {
            List<MClipElement> clipsList = new List<MClipElement>();
            foreach (MClipData cd in clipsData)
            {
                cd.clipElement.clipRect = cd.clipRect;
                cd.clipElement.imageElement = cd.imageElement;
                cd.clipElement.clipsManager = clipsManager;
                cd.clipElement.resetImgClip();
                clipsList.Add(cd.clipElement);
            }
            clipsManager.setAllSon(clipsList);
        }
    }
    //��ʷ��¼��Ŀֵ_������Ƭ
    public class MClipData
    {
        public MClipElement clipElement;
        public MImgElement imageElement = null;//����ͼƬ
        public Rectangle clipRect;             //���о���
    }
    //��ʷ��¼��Ŀ_ͼƬ����
    public class HistoryItem_Imgs : HistoryItem_MA
    {
        public HistoryItem_Imgs(MA_HistoryManager containerT)
            : base(containerT)
        { 
        }
        protected override void updateTo(HistoryValue valueT)
        {
            HistoryValue_Imgs value = (HistoryValue_Imgs)valueT;
            container.form_MA.form_MImgsList.mImgsManager = value.imgsManager;
            value.setDataToImg(container.form_MA.form_MImgsList.mImgsManager);
            container.form_MA.form_MImgsList.releaseFocus();
            container.form_MA.form_MImgsList.updateAllList(value.imgListIndex);
            container.form_MA.form_MImgsList.mImgsManager.reloadImageElements();
            container.form_MA.refreshActionUIs();
        }
    }
    //��ʷ��¼��Ŀֵ_ͼƬ����
    public class HistoryValue_Imgs : HistoryValue
    {
        public MImgsManager imgsManager;                      //ͼƬ������-ͼƬ��������
        public List<MImgData> imgsData = new List<MImgData>();//ͼƬ�����б�
        public int imgListIndex;                              //ͼƬ�б�ID
        public void setDataToImg(MImgsManager mImgsManager)
        {
            foreach (MImgData img in imgsData)
            {
                img.imgElement.name = img.name;
                img.imgElement.image = img.image;
                img.imgElement.imageTextured = img.imageTextured;
                img.imgElement.imageTextured = img.imageTextured;
                img.imgElement.strAlphaImage = img.strAlphaImage;
                img.imgElement.strPmt = img.strPmt;
                img.imgElement.alpha = img.alpha;
                img.imgElement.setParent(mImgsManager);
            }
        }
    }
    //��ʷ��¼��Ŀֵ_����ͼƬ
    public class MImgData
    {
        public MImgElement imgElement;
        public String name;
        public Image image;
        public TextureImage imageTextured;
        public bool forbidOptimize;
        public String strAlphaImage;
        public String strPmt;
        public int alpha;
    }
    //��ʷ��¼��Ŀֵ_ͼƬ������
    public class HistoryItem_ImgRename : HistoryItem_MA
    {
        public HistoryItem_ImgRename(MA_HistoryManager containerT)
            : base(containerT)
        { 
        }
        protected override void updateTo(HistoryValue valueT)
        {
            HistoryValue_ImgRename value = (HistoryValue_ImgRename)valueT;
            value.imgElement.renameImage(value.destName);
            MImgsManager imgsManager = (MImgsManager)value.imgElement.GetParent();
            if (imgsManager != null && imgsManager.MNodeUI != null)
            {
                imgsManager.MNodeUI.UpdateItem(value.imgElement.GetID());
            }
        }
    }
    public class HistoryValue_ImgRename : HistoryValue
    {
        public MImgElement imgElement;
        public String destName;
    }
    //��ʷ��¼��Ŀֵ_ͼƬ����
    public class HistoryItem_ImgProp : HistoryItem_MA
    {
        public HistoryItem_ImgProp(MA_HistoryManager containerT)
            : base(containerT)
        {
        }
        protected override void updateTo(HistoryValue valueT)
        {
            HistoryValue_ImgProp value = (HistoryValue_ImgProp)valueT;
            foreach (ImgProp imgProp in value.imgProps)
            {
                MImgElement imgElement = imgProp.imgElement;
                imgElement.forbidOptimize = imgProp.forbidOptimize;
                imgElement.strAlphaImage = imgProp.strAlphaImage;
                imgElement.strPmt = imgProp.strPmt;
                imgElement.alpha = imgProp.alpha;
                imgElement.linkID = imgProp.linkID;

                MImgsManager imgsManager = (MImgsManager)imgElement.GetParent();
                if (imgsManager != null && imgsManager.MNodeUI != null)
                {
                    imgsManager.MNodeUI.UpdateItem(imgElement.GetID());
                }
            }

        }
    }
    public class HistoryValue_ImgProp : HistoryValue
    {
        public List<ImgProp> imgProps = new List<ImgProp>();
    }
    public class ImgProp
    {
        public  MImgElement imgElement;
        public bool forbidOptimize = false;
        public String strAlphaImage = "";
        public String strPmt = "";
        public int alpha = 255;
        public int linkID = -1;
    }
}
