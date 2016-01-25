using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.alg;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Cyclone.mod.util;
using Cyclone.alg.type;
using Cyclone.alg.util;

namespace Cyclone.mod.script
{
    /****************************** ���������� ******************************/
    public class VarsManager :ObjectVector, SerializeAble
    {
        public Form_Main form_main = null;
        public static bool inExportVARTable = true;//�Ƿ����ڵ�����������
        public VarsManager(Form_Main form_mainT)
        {
            form_main = form_mainT;
        }
        //�ϲ�
        public void combine(VarsManager srcManager)
        {
            for (int i = 0; i < srcManager.getElementCount(); i++)
            {
                VarElement elementDest = (VarElement)srcManager.getElement(i);
                //Ѱ���ظ�
                bool  findSame = false;
                VarElement elementLocal = null;
                for (int j = 0; j < getElementCount(); j++)
                {
                    elementLocal = (VarElement)getElement(j);
                    if (elementDest.equalsNameAndType(elementLocal))
                    {
                        findSame = true;
                        break;
                    }
                }
                if (this.Equals(form_main.iDsManager))
                {
                    Console.WriteLine("׼���ϲ�ID������" + elementDest.name);
                }
                //����
                if (!findSame)
                {
                    elementDest.combineTo(this);//�������뱾�б�
                    Console.WriteLine("�ϲ����룺" + elementDest.name);
                }
                else
                {
                    Console.WriteLine("�ϲ����£�" + elementDest.name);
                    elementLocal.setValue(elementDest.getValue());//����ֵ�滻��ǰֵ���������ȡ���������ֵ���ж�ʱ��Ч
                    elementDest.changeUseInfor(elementLocal);//����ת��
                }
            }
        }
        //���³���
        public void updateVarElement(VarElement elementNew)
        {
            if (elementNew == null)
            {
                return;
            }
            for (int i = 0; i < getElementCount(); i++)
            {
                VarElement elementLocal = (VarElement)getElement(i);
                if (elementNew.equalsNameAndType(elementLocal))
                {
                    elementLocal.setValue(elementNew.getValue());
                    refreshUI_Element(i);
                    return;
                }
            }
            addElement(elementNew);
        }
        //������������
        public void orderByname()
        {
            ArrayList arrayListTemp = new ArrayList();
            while (getElementCount() > 0)
            {
                int maxIndex = 0;
                VarElement elementCurrent = (VarElement)getElement(maxIndex);
                String nameLocal = elementCurrent.name;
                for (int i = 1; i < getElementCount(); i++)
                {
                    VarElement elementCompare = (VarElement)getElement(i);
                    String nameCompare = elementCompare.name;
                    if (String.Compare(nameLocal, nameCompare, StringComparison.CurrentCulture) > 0)
                    {
                        elementCurrent = elementCompare;
                        nameLocal = nameCompare;
                        maxIndex = i;
                    }
                }
                arrayListTemp.Add(elementCurrent);
                objList.RemoveAt(maxIndex);
            }
            for (int i = 0; i < arrayListTemp.Count; i++)
            {
                objList.Add(arrayListTemp[i]);
            }
            refreshUI();
            refreshUIAide();
        }
        //���δʹ�õĳ���
        public void clearSpilth()
        {
            for (int i = 0; i < getElementCount(); i++)
            {
                if (i >= getElementCount())
                {
                    break;
                }
                VarElement element = (VarElement)getElement(i);
                if (element.getUsedTime() == 0 && !element.isFixed && !element.isUsedInHeadFile)
                {
                    removeElement(element.getID());
                    i--;
                }
            }
        }
        #region SerializeAble Members

        public void ReadObject(System.IO.Stream s)
        {
            short len = 0;
            len = IOUtil.readShort(s);

            for (int i = 0; i < len; i++)
            {
                VarElement element = new VarElement(this, Consts.PARAM_INT);
                element.ReadObject(s);
                addElement(element);
            }
        }

        public void WriteObject(System.IO.Stream s)
        {
            short len=(short)getElementCount();
            IOUtil.writeShort(s,len);
            for (int i = 0; i < len; i++)
            {
                VarElement element = (VarElement)getElement(i);
                element.WriteObject(s);
            }
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
            short len = (short)getElementCount();
            if (VarsManager.inExportVARTable)
            {
                IOUtil.writeShort(fs_bin, len);
            }
            for (int i = 0; i < len; i++)
            {
                VarElement element = (VarElement)getElement(i);
                element.ExportObject(fs_bin);
            }
        }

        #endregion
    }
    /****************************** ������Ԫ ******************************/
    public class VarElement : ObjectElement,SerializeAble
    {
        public byte varType = 0;
        public bool isFixed=false;//�̶��ģ�������ɾ��
        public bool isUsedInHeadFile = false;//��ͷ�ļ�ʹ��
        public VarElement(VarsManager parentT, byte varTypeT)
            : base(parentT)
        {
            varType= varTypeT;
        }
        //�ϲ���
        public void combineTo(VarsManager parentT)
        {
            parent = parentT;
            parent.addElement(this);
        }
        public override String getValueToLenString()
        {
            MiscUtil.initVBlanks();

            Encoding encode = Encoding.Default;
            String content = name + " = " + value;
            int contentLen = encode.GetByteCount(content)*6;
            int balnkLen = (int)((350 - contentLen) / 6);
            if (balnkLen >= 0 && balnkLen < MiscUtil.vBlanks.Count)
            {
                content += (String)(MiscUtil.vBlanks[balnkLen]);
            }
            return content + "[" + getUsedTime() + "]" + (isFixed ? "��" : "") + (isUsedInHeadFile ? "��" : "");
        }
        //�ж����ƺ�������ͬ(��������ֵ�жϣ��ںϲ�ʱ��ֵ������ֵ����)
        public bool equalsNameAndType(VarElement destVar)
        {
            if (destVar==null||name == null || !name.Equals(destVar.name))
            {
                return false;
            }
            if (varType != destVar.varType)
            {
                return false;
            }
            return true;
            //switch (varType)
            //{
            //    case Consts.PARAM_INT:
            //    case Consts.PARAM_INT_VAR:
            //    case Consts.PARAM_STR_VAR:
            //    case Consts.PARAM_INT_ID:
            //        int intValueLocal = Convert.ToInt32(value);
            //        int intValueDest = Convert.ToInt32(destVar.value);
            //        if (intValueLocal == intValueDest)
            //        {
            //            return true;
            //        }
            //        break;
            //    case Consts.PARAM_STR:
            //        String strValueLocal = (String)(value);
            //        String strValueDest = (String)(destVar.value);
            //        if (strValueLocal.Equals(strValueDest))
            //        {
            //            return true;
            //        }
            //        break;
            //}
            //return false;
        }
        //�����Լ�������ת�Ƶ���Ķ�����
        public void changeUseInfor(VarElement varDest)
        {
            //�ڽű��в���-------------------------------------------------------------------
            //TriggerPacksManager triggersManager = ((VarsManager)parent).form_main.triggersManager;
            //for (int i = 0; i < triggersManager.getElementCount(); i++)
            //{
            //    TriggerPackElement trigger = (TriggerPackElement)triggersManager.getElement(i);
            //    for (int j = 0; j < trigger.sentences_Trigger.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)trigger.sentences_Trigger.getElement(j);
            //        for (int k = 0; k < sentence.paramsList.Count; k++)
            //        {
            //            if (sentence.paramsList[k] != null && sentence.paramsList[k].Equals(this))
            //            {
            //                sentence.paramsList[k] = varDest;
            //            }
            //        }
            //    }
            //    for (int j = 0; j < trigger.sentences_Context.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)trigger.sentences_Context.getElement(j);
            //        for (int k = 0; k < sentence.paramsList.Count; k++)
            //        {
            //            if (sentence.paramsList[k] != null && sentence.paramsList[k].Equals(this))
            //            {
            //                sentence.paramsList[k] = varDest;
            //            }
            //        }
            //    }
            //    for (int j = 0; j < trigger.triggersManager.getElementCount(); j++)
            //    {
            //        Trigger exeStruct = (Trigger)trigger.triggersManager.getElement(j);
            //        for (int k = 0; k < exeStruct.sentences_Context.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)exeStruct.sentences_Context.getElement(k);
            //            for (int m = 0; m < sentence.paramsList.Count; m++)
            //            {
            //                if (sentence.paramsList[m] != null && sentence.paramsList[m].Equals(this))
            //                {
            //                    sentence.paramsList[m] = varDest;
            //                }
            //            }
            //        }
            //        for (int k = 0; k < exeStruct.sentences_Executions.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)exeStruct.sentences_Executions.getElement(k);
            //            for (int m = 0; m < sentence.paramsList.Count; m++)
            //            {
            //                if (sentence.paramsList[m] != null && sentence.paramsList[m].Equals(this))
            //                {
            //                    sentence.paramsList[m] = varDest;
            //                }
            //            }
            //        }
            //    }
            //}
            //�������в���-------------------------------------------------------------------
            PropertyTypesManager propertyTypesManager = (PropertyTypesManager)(((VarsManager)parent).form_main.propertyTypesManager);
            for (int i = 0; i < propertyTypesManager.getElementCount(); i++)
            {
                PropertyTypeElement propertyTypeElement = (PropertyTypeElement)propertyTypesManager.getElement(i);
                InstancesManager instancesManager = (InstancesManager)propertyTypeElement.instancesManager;
                for (int j = 0; j < instancesManager.getElementCount(); j++)
                {
                    InstanceElement instanceElement = (InstanceElement)instancesManager.getElement(j);
                    for (int k = 0; k < instanceElement.propertyValueManager.getElementCount(); k++)
                    {
                        PropertyValueElement propertyValueElement = (PropertyValueElement)(instanceElement.propertyValueManager.getElement(k));
                        if (propertyValueElement != null)
                        {
                            Object value = propertyValueElement.getValue();
                            if (value != null && value.Equals(this))
                            {
                                propertyValueElement.setValue(varDest);
                            }
                        }
                    }
                }
            }
        }
        //����ʹ�ô���
        public override int getUsedTime()
        {
            int time = 0;
            //�ڽű��в���-------------------------------------------------------------------
            //TriggerPacksManager triggersManager = ((VarsManager)parent).form_main.triggersManager;
            //for (int i = 0; i < triggersManager.getElementCount(); i++)
            //{
            //    TriggerPackElement trigger = (TriggerPackElement)triggersManager.getElement(i);
            //    for (int j = 0; j < trigger.sentences_Trigger.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)trigger.sentences_Trigger.getElement(j);
            //        for (int k = 0; k < sentence.paramsList.Count; k++)
            //        {
            //            if (sentence.paramsList[k] != null && sentence.paramsList[k].Equals(this))
            //            {
            //                time++;
            //            }
            //        }
            //    }
            //    for (int j = 0; j < trigger.sentences_Context.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)trigger.sentences_Context.getElement(j);
            //        for (int k = 0; k < sentence.paramsList.Count; k++)
            //        {
            //            if (sentence.paramsList[k] != null && sentence.paramsList[k].Equals(this))
            //            {
            //                time++;
            //            }
            //        }
            //    }
            //    for (int j = 0; j < trigger.triggersManager.getElementCount(); j++)
            //    {
            //        Trigger exeStruct = (Trigger)trigger.triggersManager.getElement(j);
            //        for (int k = 0; k < exeStruct.sentences_Context.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)exeStruct.sentences_Context.getElement(k);
            //            for (int m = 0; m < sentence.paramsList.Count; m++)
            //            {
            //                if (sentence.paramsList[m] != null && sentence.paramsList[m].Equals(this))
            //                {
            //                    time++;
            //                }
            //            }
            //        }
            //        for (int k = 0; k < exeStruct.sentences_Executions.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)exeStruct.sentences_Executions.getElement(k);
            //            for (int m = 0; m < sentence.paramsList.Count; m++)
            //            {
            //                if (sentence.paramsList[m] != null && sentence.paramsList[m].Equals(this))
            //                {
            //                    time++;
            //                }
            //            }
            //        }
            //    }

            //}
            //�������в���-------------------------------------------------------------------
            PropertyTypesManager propertyTypesManager = (PropertyTypesManager)(((VarsManager)parent).form_main.propertyTypesManager);
            for (int i = 0; i < propertyTypesManager.getElementCount(); i++)
            {
                PropertyTypeElement propertyTypeElement = (PropertyTypeElement)propertyTypesManager.getElement(i);
                InstancesManager instancesManager = (InstancesManager)propertyTypeElement.instancesManager;
                for (int j = 0; j < instancesManager.getElementCount(); j++)
                {
                    InstanceElement instanceElement = (InstanceElement)instancesManager.getElement(j);
                    for (int k = 0; k < instanceElement.propertyValueManager.getElementCount(); k++)
                    {
                        PropertyValueElement propertyValueElement = (PropertyValueElement)(instanceElement.propertyValueManager.getElement(k));
                        if (propertyValueElement != null)
                        {
                            Object value = propertyValueElement.getValue();
                            if (value !=null && value.Equals(this))
                            {
                                time++;
                            }
                        }
                    }
                }
            }
            return time;
        }
        //����ʹ����Ϣ
        public String getUsedInfor()
        {
            String s = "";
            int time = 0;
            //�ڽű��в���-------------------------------------------------------------------
            //TriggerPacksManager triggersManager = ((VarsManager)parent).form_main.triggersManager;
            //for (int i = 0; i < triggersManager.getElementCount(); i++)
            //{
            //    TriggerPackElement trigger = (TriggerPackElement)triggersManager.getElement(i);
            //    for (int j = 0; j < trigger.sentences_Trigger.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)trigger.sentences_Trigger.getElement(j);
            //        for (int k = 0; k < sentence.paramsList.Count; k++)
            //        {
            //            if (sentence.paramsList[k] != null && sentence.paramsList[k].Equals(this))
            //            {
            //                time++;
            //                s += "[������:" + trigger.name + ",����������" + sentence.getID() + "�����,��" + (k + 1) + "������]\n";
            //            }
            //        }
            //    }
            //    for (int j = 0; j < trigger.sentences_Context.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)trigger.sentences_Context.getElement(j);
            //        for (int k = 0; k < sentence.paramsList.Count; k++)
            //        {
            //            if (sentence.paramsList[k] != null && sentence.paramsList[k].Equals(this))
            //            {
            //                time++;
            //                s += "[������:" + trigger.name + ",����������" + sentence.getID() + "�����,��" + (k + 1) + "������]\n";
            //            }
            //        }
            //    }
            //    for (int j = 0; j < trigger.triggersManager.getElementCount(); j++)
            //    {
            //        Trigger exeStruct = (Trigger)trigger.triggersManager.getElement(j);
            //        for (int k = 0; k < exeStruct.sentences_Context.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)exeStruct.sentences_Context.getElement(k);
            //            for (int m = 0; m < sentence.paramsList.Count; m++)
            //            {
            //                if (sentence.paramsList[m] != null && sentence.paramsList[m].Equals(this))
            //                {
            //                    time++;
            //                    s += "[������:" + trigger.name + ",ִ�нṹ:" + exeStruct.name + ",�ṹ����������" + sentence.getID() + "�����,��" + (k + 1) + "������]\n";
            //                }
            //            }
            //        }
            //        for (int k = 0; k < exeStruct.sentences_Executions.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)exeStruct.sentences_Executions.getElement(k);
            //            for (int m = 0; m < sentence.paramsList.Count; m++)
            //            {
            //                if (sentence.paramsList[m] != null && sentence.paramsList[m].Equals(this))
            //                {
            //                    time++;
            //                    s += "[������:" + trigger.name + ",ִ�нṹ:" + exeStruct.name + ",ִ������б��" + sentence.getID() + "�����,��" + (k + 1) + "������]\n";
            //                }
            //            }
            //        }
            //    }

            //}
            //�������в���-------------------------------------------------------------------
            PropertyTypesManager propertyTypesManager = (PropertyTypesManager)(((VarsManager)parent).form_main.propertyTypesManager);
            for (int i = 0; i < propertyTypesManager.getElementCount(); i++)
            {
                PropertyTypeElement propertyTypeElement = (PropertyTypeElement)propertyTypesManager.getElement(i);
                InstancesManager instancesManager = (InstancesManager)propertyTypeElement.instancesManager;
                for (int j = 0; j < instancesManager.getElementCount(); j++)
                {
                    InstanceElement instanceElement = (InstanceElement)instancesManager.getElement(j);
                    for (int k = 0; k < instanceElement.propertyValueManager.getElementCount(); k++)
                    {
                        PropertyValueElement propertyValueElement = (PropertyValueElement)(instanceElement.propertyValueManager.getElement(k));
                        if (propertyValueElement != null)
                        {
                            Object value = propertyValueElement.getValue();
                            if (value != null && value.Equals(this))
                            {
                                time++;
                                s += "[���Ա�:" + propertyTypeElement.name + ",ʵ��:" + instanceElement.name + ",������:"+propertyValueElement.name+"]\n";
                            }
                        }
                    }
                }
            }
            s += "\n";
            s += "------------------------��ʹ����"+time+"��------------------------";
            return s;
        }
        public ShowItemList getUsedMeory()
        {
            ShowItemList showItemList = new ShowItemList(((VarsManager)parent).form_main.form_ProptiesManager);
            //int time = 0;
            //TriggerPacksManager triggersManager = ((VarsManager)parent).form_main.triggersManager;
            //for (int i = 0; i < triggersManager.getElementCount(); i++)
            //{
            //    TriggerPackElement triggerPack = (TriggerPackElement)triggersManager.getElement(i);
            //    for (int j = 0; j < triggerPack.sentences_Trigger.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)triggerPack.sentences_Trigger.getElement(j);
            //        if (sentence.paramsList != null && sentence.paramsList.Contains(this))
            //        {
            //            time++;
            //            String s = "(" + time + ")." + " λ�ڴ�����[" + triggerPack.name + "],����������  " + sentence.getValueToLenString();
            //            ArrayList array = new ArrayList();
            //            array.Add(triggerPack);
            //            array.Add(sentence);
            //            ShowItem showItem = new ShowItem(showItemList, s, array);
            //            showItemList.addElement(showItem);
            //        }
            //    }
            //    for (int j = 0; j < triggerPack.sentences_Context.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)triggerPack.sentences_Context.getElement(j);
            //        if (sentence.paramsList != null && sentence.paramsList.Contains(this))
            //        {
            //            time++;
            //            String s = "(" + time + ")." + " λ�ڴ�����[" + triggerPack.name + "],����������  " + sentence.getValueToLenString();
            //            ArrayList array = new ArrayList();
            //            array.Add(triggerPack);
            //            array.Add(sentence);
            //            ShowItem showItem = new ShowItem(showItemList, s, array);
            //            showItemList.addElement(showItem);
            //        }
            //    }
            //    for (int j = 0; j < triggerPack.triggersManager.getElementCount(); j++)
            //    {
            //        Trigger trigger = (Trigger)triggerPack.triggersManager.getElement(j);
            //        for (int k = 0; k < trigger.sentences_Context.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)trigger.sentences_Context.getElement(k);
            //            if (sentence.paramsList != null && sentence.paramsList.Contains(this))
            //            {
            //                time++;
            //                String s = "(" + time + ")." + " λ�ڴ�����[" + triggerPack.name + "]," + "������[" + trigger.name + "],������������  " + sentence.getValueToLenString();
            //                ArrayList array = new ArrayList();
            //                array.Add(triggerPack);
            //                array.Add(trigger);
            //                array.Add(sentence);
            //                ShowItem showItem = new ShowItem(showItemList, s, array);
            //                showItemList.addElement(showItem);
            //            }
            //        }
            //        for (int k = 0; k < trigger.sentences_Executions.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)trigger.sentences_Executions.getElement(k);
            //            if (sentence.paramsList != null && sentence.paramsList.Contains(this))
            //            {
            //                time++;
            //                String s = "(" + time + ")." + " λ�ڴ�����[" + triggerPack.name + "]," + "������[" + trigger.name + "],������������  " + sentence.getValueToLenString();
            //                ArrayList array = new ArrayList();
            //                array.Add(triggerPack);
            //                array.Add(trigger);
            //                array.Add(sentence);
            //                ShowItem showItem = new ShowItem(showItemList, s, array);
            //                showItemList.addElement(showItem);
            //            }
            //        }
            //    }

            //}
            return showItemList;
        }
        #region SerializeAble Members
        public void ReadObject(System.IO.Stream s)
        {
            name = IOUtil.readString(s);
            varType = IOUtil.readByte(s);
            switch (varType)
            {
                case Consts.PARAM_INT:
                case Consts.PARAM_INT_VAR:
                case Consts.PARAM_STR_VAR:
                case Consts.PARAM_INT_ID:
                    value = IOUtil.readInt(s);
                    break;
                case Consts.PARAM_STR:
                    value = IOUtil.readString(s);
                    break;
            }
            isFixed = IOUtil.readBoolean(s);
            isUsedInHeadFile = IOUtil.readBoolean(s);
        }

        public void WriteObject(System.IO.Stream s)
        {
            IOUtil.writeString(s, name);
            IOUtil.writeByte(s,varType);
            switch (varType)
            {
                case Consts.PARAM_INT:
                case Consts.PARAM_INT_VAR:
                case Consts.PARAM_STR_VAR:
                case Consts.PARAM_INT_ID:
                    IOUtil.writeInt(s, (int)value);
                    break;
                case Consts.PARAM_STR:
                    IOUtil.writeString(s, (String)value);
                    break;
            }
            IOUtil.writeBoolean(s, isFixed);
            IOUtil.writeBoolean(s, isUsedInHeadFile);
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
            if (VarsManager.inExportVARTable)
            {
                switch (varType)
                {
                    case Consts.PARAM_INT:
                    case Consts.PARAM_INT_VAR:
                    case Consts.PARAM_STR_VAR:
                    case Consts.PARAM_INT_ID:
                        IOUtil.writeInt(fs_bin, (int)value);
                        break;
                    case Consts.PARAM_STR:
                        IOUtil.writeString(fs_bin, (String)value);
                        break;
                }
            }
            if (VarsManager.inExportVARTable)
            {
                UserDoc.ArrayTxts_Head.Add("#define SCRIPT_VAR_" + name + " " + getID() + "");
                UserDoc.ArrayTxts_Java.Add("public static final short SCRIPT_VAR_" + name + " = " + getID() + ";");
            }
            else
            {
                UserDoc.ArrayTxts_Head.Add("#define FLAG_" + name + " " + (int)value + "");
                UserDoc.ArrayTxts_Java.Add("public static final short FLAG_" + name + " = " + (int)value + ";");
            }
        }

        #endregion

        public override ObjectElement clone()
        {
            VarElement newInstance = new VarElement((VarsManager)parent, varType);
            baseCloneTo(newInstance);
            newInstance.isFixed=isFixed;
            newInstance.isUsedInHeadFile = isUsedInHeadFile;
            switch (varType)
            {
                case Consts.PARAM_INT:
                case Consts.PARAM_INT_VAR:
                case Consts.PARAM_STR_VAR:
                case Consts.PARAM_INT_ID:
                    newInstance.value = (int)value;
                    break;
                case Consts.PARAM_STR:
                    newInstance.value = name + "";
                    break;
            }
            return newInstance;
        }
    }
    /****************************** ���������� ******************************/
    public class FunctionsManager : ObjectVector, SerializeAble
    {
        public Form_Main form_main = null;
        public FunctionsManager(Form_Main form_mainT)
        {
            form_main = form_mainT;
        }
        //������������
        public void orderByname()
        {
            ArrayList arrayListTemp = new ArrayList();
            while (getElementCount() > 0)
            {
                int maxIndex = 0;
                FunctionElement elementCurrent = (FunctionElement)getElement(maxIndex);
                String nameLocal = elementCurrent.name;
                for (int i = 1; i < getElementCount(); i++)
                {
                    FunctionElement elementCompare = (FunctionElement)getElement(i);
                    String nameCompare = elementCompare.name;
                    if (String.Compare(nameLocal, nameCompare, StringComparison.CurrentCulture) > 0)
                    {
                        elementCurrent = elementCompare;
                        nameLocal = nameCompare;
                        maxIndex = i;
                    }
                }
                arrayListTemp.Add(elementCurrent);
                objList.RemoveAt(maxIndex);
            }
            for (int i = 0; i < arrayListTemp.Count; i++)
            {
                objList.Add(arrayListTemp[i]);
            }
            refreshUI();
        }
        //�ϲ�
        public void combine(FunctionsManager srcFunctionsManager)
        {
            short len = (short)srcFunctionsManager.getElementCount();
            for (int i = 0; i < len; i++)
            {
                FunctionElement srcFunctionElement = (FunctionElement)srcFunctionsManager.getElement(i);
                //�����ͬ
                FunctionElement localFunctionElement = null;
                bool findSame=false;
                for (int j = 0; j < getElementCount(); j++)
                {
                    localFunctionElement = (FunctionElement)getElement(j);
                    if (localFunctionElement.equalsFunctionElement(srcFunctionElement))
                    {
                        findSame = true;
                        break;
                    }
                }
                //����
                if (!findSame)//������ϲ���
                {
                    srcFunctionElement.combineTo(this);
                }
                else
                {
                    localFunctionElement.commet = srcFunctionElement.commet+"";//����ע��
                    srcFunctionElement.changeUseInfor(localFunctionElement);//ת������
                }
            }
        }
        //��д
        #region SerializeAble Members

        public void ReadObject(System.IO.Stream s)
        {
            short len = 0;
            len = IOUtil.readShort(s);

            for (int i = 0; i < len; i++)
            {
                FunctionElement element = new FunctionElement(this);
                element.ReadObject(s);
                addElement(element);
            }
        }

        public void WriteObject(System.IO.Stream s)
        {
            short len = (short)getElementCount();
            IOUtil.writeShort(s, len);
            for (int i = 0; i < len; i++)
            {
                FunctionElement element = (FunctionElement)getElement(i);
                element.WriteObject(s);
            }
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
            short len = (short)getElementCount();
            //IOUtil.writeShort(fs_bin, len);
            for (int i = 0; i < len; i++)
            {
                FunctionElement element = (FunctionElement)getElement(i);
                element.ExportObject(fs_bin);
            }
        }

        #endregion
    }
    /****************************** ������Ԫ ******************************/
    public class FunctionElement : ObjectElement, SerializeAble
    {
        public String commet = "";
        public FunctionElement(FunctionsManager parentT)
            : base(parentT)
        {
            value = new ArrayList();
        }
        //�ϲ���
        public void combineTo(FunctionsManager parentT)
        {
            parent = parentT;
            parent.addElement(this);
        }
        //�ж��Ƿ����
        public bool equalsFunctionElement(FunctionElement destElement)
        {
            if (destElement == null || value == null || destElement.value==null)
            {
                return false;
            }
            if (name == null || destElement.name == null || !destElement.name.Equals(name))
            {
                return false;
            }
            ArrayList paramsArrayLocal = (ArrayList)value;
            ArrayList paramsArrayDest = (ArrayList)destElement.value;
            if (paramsArrayDest.Count != paramsArrayLocal.Count)
            {
                return false;
            }
            for (int i = 0; i < paramsArrayLocal.Count; i++)
            {
                byte byteLocal = (byte)paramsArrayLocal[i];
                byte byteDest = (byte)paramsArrayDest[i];
                if (byteLocal != byteDest)
                {
                    return false;
                }
            }
            return true;
        }

        //��¡
        public override ObjectElement clone()
        {
            FunctionElement newInstance = new FunctionElement((FunctionsManager)parent);
            baseCloneTo(newInstance);
            newInstance.commet = commet+"";
            ArrayList newArray = (ArrayList)newInstance.value;
            ArrayList currentArray=(ArrayList)value;
            for (int i = 0; i < currentArray.Count; i++)
            {
                newArray.Add((int)currentArray[i]);
            }
            return newInstance;
        }
        public override String getValueToLenString()
        {
            ArrayList paramsArray = (ArrayList)value;
            String s = name + "(";
            for (int i = 0; i < paramsArray.Count; i++)
            {
                s += Consts.getParamType((byte)paramsArray[i]);
                if (i < paramsArray.Count - 1)
                {
                    s += ",";
                }
            }
            s += ")";
            MiscUtil.initVBlanks();
            Encoding encode = Encoding.Default;
            int contentLen = encode.GetByteCount(s) * 6;
            int balnkLen = (int)((350 - contentLen) / 6);
            if (balnkLen >= 0 && balnkLen < MiscUtil.vBlanks.Count)
            {
                s += (String)(MiscUtil.vBlanks[balnkLen]);
            }
            s += "//" + "{" + getUsedTime() + "}" + commet;
            return s;
        }
        public ArrayList getFormatList()
        {
            return (ArrayList)value;
        }
        //ת������
        public void changeUseInfor(FunctionElement destFunctionElement)
        {
            //TriggerPacksManager triggersManager = ((FunctionsManager)parent).form_main.triggersManager;
            //for (int i = 0; i < triggersManager.getElementCount(); i++)
            //{
            //    TriggerPackElement triggerPack = (TriggerPackElement)triggersManager.getElement(i);
            //    for (int j = 0; j < triggerPack.sentences_Trigger.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)triggerPack.sentences_Trigger.getElement(j);
            //        if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //        {
            //            sentence.functionElement = destFunctionElement;
            //        }
            //    }
            //    for (int j = 0; j < triggerPack.sentences_Context.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)triggerPack.sentences_Context.getElement(j);
            //        if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //        {
            //            sentence.functionElement = destFunctionElement;
            //        }
            //    }
            //    for (int j = 0; j < triggerPack.triggersManager.getElementCount(); j++)
            //    {
            //        Trigger trigger = (Trigger)triggerPack.triggersManager.getElement(j);
            //        for (int k = 0; k < trigger.sentences_Context.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)trigger.sentences_Context.getElement(k);
            //            if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //            {
            //                sentence.functionElement = destFunctionElement;
            //            }
            //        }
            //        for (int k = 0; k < trigger.sentences_Executions.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)trigger.sentences_Executions.getElement(k);
            //            if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //            {
            //                sentence.functionElement = destFunctionElement;
            //            }
            //        }
            //    }

            //}
        }
        //���ʹ�ô���
        public override int getUsedTime()
        {
            int time = 0;
            //TriggerPacksManager triggersManager = ((FunctionsManager)parent).form_main.triggersManager;
            //for (int i = 0; i < triggersManager.getElementCount(); i++)
            //{
            //    TriggerPackElement triggerPack = (TriggerPackElement)triggersManager.getElement(i);
            //    for (int j = 0; j < triggerPack.sentences_Trigger.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)triggerPack.sentences_Trigger.getElement(j);
            //        if (sentence.functionElement!= null && sentence.functionElement.Equals(this))
            //        {
            //            time++;
            //        }
            //    }
            //    for (int j = 0; j < triggerPack.sentences_Context.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)triggerPack.sentences_Context.getElement(j);
            //        if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //        {
            //            time++;
            //        }
            //    }
            //    for (int j = 0; j < triggerPack.triggersManager.getElementCount(); j++)
            //    {
            //        Trigger trigger = (Trigger)triggerPack.triggersManager.getElement(j);
            //        for (int k = 0; k < trigger.sentences_Context.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)trigger.sentences_Context.getElement(k);
            //            if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //            {
            //                time++;
            //            }
            //        }
            //        for (int k = 0; k < trigger.sentences_Executions.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)trigger.sentences_Executions.getElement(k);
            //            if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //            {
            //                time++;
            //            }
            //        }
            //    }

            //}
            return time;
        }
        public String getUsedInfor()
        {
            String s = "";
            //int time = 0;
            //TriggerPacksManager triggersManager = ((FunctionsManager)parent).form_main.triggersManager;
            //for (int i = 0; i < triggersManager.getElementCount(); i++)
            //{
            //    TriggerPackElement triggerPack = (TriggerPackElement)triggersManager.getElement(i);
            //    for (int j = 0; j < triggerPack.sentences_Trigger.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)triggerPack.sentences_Trigger.getElement(j);
            //        if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //        {
            //            time++;
            //            s += "������[" + triggerPack.name + "],������������[" + sentence.getID() + "]�����\n";
            //        }
            //    }
            //    for (int j = 0; j < triggerPack.sentences_Context.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)triggerPack.sentences_Context.getElement(j);
            //        if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //        {
            //            time++;
            //            s += "������[" + triggerPack.name + "],������������[" + sentence.getID() + "]�����\n";
            //        }
            //    }
            //    for (int j = 0; j < triggerPack.triggersManager.getElementCount(); j++)
            //    {
            //        Trigger trigger = (Trigger)triggerPack.triggersManager.getElement(j);
            //        for (int k = 0; k < trigger.sentences_Context.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)trigger.sentences_Context.getElement(k);
            //            if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //            {
            //                time++;
            //                s += "������[" + triggerPack.name + "],������[" + trigger.name + "]����������[" + sentence.getID() + "]�����" + "\n";
            //            }
            //        }
            //        for (int k = 0; k < trigger.sentences_Executions.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)trigger.sentences_Executions.getElement(k);
            //            if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //            {
            //                time++;
            //                s += "������[" + triggerPack.name + "],������[" + trigger.name + "]ִ������[" + sentence.getID() + "]�����" + "\n";
            //            }
            //        }
            //    }

            //}

            //s += "\n";
            //s += "------------------------��ʹ����" + time + "��------------------------";
            return s;
        }
        public ShowItemList getUsedMeory()
        {
            ShowItemList showItemList = new ShowItemList(((FunctionsManager)parent).form_main.form_ProptiesManager);
            //int time = 0;
            //TriggerPacksManager triggersManager = ((FunctionsManager)parent).form_main.triggersManager;
            //for (int i = 0; i < triggersManager.getElementCount(); i++)
            //{
            //    TriggerPackElement triggerPack = (TriggerPackElement)triggersManager.getElement(i);
            //    for (int j = 0; j < triggerPack.sentences_Trigger.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)triggerPack.sentences_Trigger.getElement(j);
            //        if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //        {
            //            time++;
            //            String s = "(" + time + ")." + " λ�ڴ�����[" + triggerPack.name + "],����������  "+sentence.getValueToLenString();
            //            ArrayList array=new ArrayList();
            //            array.Add(triggerPack);
            //            array.Add(sentence);
            //            ShowItem showItem = new ShowItem(showItemList, s, array);
            //            showItemList.addElement(showItem);
            //        }
            //    }
            //    for (int j = 0; j < triggerPack.sentences_Context.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)triggerPack.sentences_Context.getElement(j);
            //        if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //        {
            //            time++;
            //            String s = "(" + time + ")." + " λ�ڴ�����[" + triggerPack.name + "],����������  " + sentence.getValueToLenString();
            //            ArrayList array = new ArrayList();
            //            array.Add(triggerPack);
            //            array.Add(sentence);
            //            ShowItem showItem = new ShowItem(showItemList, s, array);
            //            showItemList.addElement(showItem);
            //        }
            //    }
            //    for (int j = 0; j < triggerPack.triggersManager.getElementCount(); j++)
            //    {
            //        Trigger trigger = (Trigger)triggerPack.triggersManager.getElement(j);
            //        for (int k = 0; k < trigger.sentences_Context.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)trigger.sentences_Context.getElement(k);
            //            if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //            {
            //                time++;
            //                String s = "(" + time + ")." + " λ�ڴ�����[" + triggerPack.name + "]," + "������[" + trigger.name + "],������������  " + sentence.getValueToLenString();
            //                ArrayList array = new ArrayList();
            //                array.Add(triggerPack);
            //                array.Add(trigger);
            //                array.Add(sentence);
            //                ShowItem showItem = new ShowItem(showItemList, s, array);
            //                showItemList.addElement(showItem);
            //            }
            //        }
            //        for (int k = 0; k < trigger.sentences_Executions.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)trigger.sentences_Executions.getElement(k);
            //            if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //            {
            //                time++;
            //                String s = "(" + time + ")." + " λ�ڴ�����[" + triggerPack.name + "]," + "������[" + trigger.name + "],������������  " + sentence.getValueToLenString();
            //                ArrayList array = new ArrayList();
            //                array.Add(triggerPack);
            //                array.Add(trigger);
            //                array.Add(sentence);
            //                ShowItem showItem = new ShowItem(showItemList, s, array);
            //                showItemList.addElement(showItem);
            //            }
            //        }
            //    }

            //}

            return showItemList;
        }
        public void configValue(ArrayList paramNew, ArrayList paramValue, ArrayList paramValueReset)
        {
            if (paramNew == null || paramValue == null || paramValueReset==null)
            {
                return;
            }
            int time = 0;
            //TriggerPacksManager triggersManager = ((FunctionsManager)parent).form_main.triggersManager;
            //for (int i = 0; i < triggersManager.getElementCount(); i++)
            //{
            //    TriggerPackElement trigger = (TriggerPackElement)triggersManager.getElement(i);
            //    for (int j = 0; j < trigger.sentences_Trigger.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)trigger.sentences_Trigger.getElement(j);
            //        if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //        {
            //            configParam(sentence.paramsList, (ArrayList)value, paramNew, paramValue, paramValueReset);
            //            time++;
            //        }
            //    }
            //    for (int j = 0; j < trigger.sentences_Context.getElementCount(); j++)
            //    {
            //        Sentence sentence = (Sentence)trigger.sentences_Context.getElement(j);
            //        if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //        {
            //            configParam(sentence.paramsList, (ArrayList)value, paramNew, paramValue, paramValueReset);
            //            time++;
            //        }
            //    }
            //    for (int j = 0; j < trigger.triggersManager.getElementCount(); j++)
            //    {
            //        Trigger exeStruct = (Trigger)trigger.triggersManager.getElement(j);
            //        for (int k = 0; k < exeStruct.sentences_Context.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)exeStruct.sentences_Context.getElement(k);
            //            if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //            {
            //                configParam(sentence.paramsList, (ArrayList)value, paramNew, paramValue, paramValueReset);
            //                time++;
            //            }
            //        }
            //        for (int k = 0; k < exeStruct.sentences_Executions.getElementCount(); k++)
            //        {
            //            Sentence sentence = (Sentence)exeStruct.sentences_Executions.getElement(k);
            //            if (sentence.functionElement != null && sentence.functionElement.Equals(this))
            //            {
            //                configParam(sentence.paramsList, (ArrayList)value, paramNew, paramValue, paramValueReset);
            //                time++;
            //            }
            //        }
            //    }

            //}
            value = paramNew;
            if (time > 0)
            {
                MessageBox.Show("���ƽ�����" + time + "�����ġ�");
            }
        }
        public void configParam(ArrayList paramList, ArrayList paramFormatOld, ArrayList paramFormatNew,
                                ArrayList paramValueList, ArrayList paramValueReset)
        {

            //�����²�����ʽ��������
            while (paramList.Count > paramFormatNew.Count)
            {
                paramList.RemoveAt(paramList.Count - 1);
            }

            for (int i = paramList.Count; i < paramFormatNew.Count; i++)
            {
                int format = Convert.ToInt32(paramFormatNew[i]);
                paramList.Add(((FunctionsManager)parent).form_main.getDefaultParams((byte)format));
            }

            //�����²�����ʽ��������
            for (int i = 0; i < paramList.Count; i++)
            {
                int formatNew = Convert.ToInt32(paramFormatNew[i]);
                int formatOld = formatNew;
                if (i < paramFormatOld.Count)
                {
                    formatOld = Convert.ToInt32(paramFormatOld[i]);
                }
                if (formatOld != formatNew || ((bool)paramValueReset[i]))
                {
                    if (!((bool)paramValueReset[i]))
                    {
                        paramList[i] = ((FunctionsManager)parent).form_main.getDefaultParams((byte)formatNew);
                    }
                    else
                    {
                        paramList[i] = ((FunctionsManager)parent).form_main.getParamByIndex((byte)formatNew, (int)paramValueList[i]);
                    }
                }
            }

        }
        #region SerializeAble Members
        public void ReadObject(System.IO.Stream s)
        {
            name = IOUtil.readString(s);
            commet = IOUtil.readString(s);
            ArrayList paramsArray = new ArrayList();
            short len = 0;
            len = IOUtil.readShort(s);
            for (int i = 0; i < len; i++)
            {
                byte byteValue = IOUtil.readByte(s);
                paramsArray.Add(byteValue);
            }
            value = paramsArray;

        }

        public void WriteObject(System.IO.Stream s)
        {
            IOUtil.writeString(s, name);
            IOUtil.writeString(s, commet);
            ArrayList paramsArray = (ArrayList)value;
            IOUtil.writeShort(s, (short)paramsArray.Count);
            for (int i = 0; i < paramsArray.Count; i++)
            {
                IOUtil.writeByte(s, (byte)paramsArray[i]);
            }

        }
        public static int FunctionGlobleID = 0;
        public void ExportObject(System.IO.Stream fs_bin)
        {
            ArrayList paramsArray = (ArrayList)value;
            IOUtil.writeByte(fs_bin, (byte)paramsArray.Count);
            UserDoc.ArrayTxts_Head.Add("#define SCRIPT_FUN_" + name + " " + FunctionGlobleID + "");
            UserDoc.ArrayTxts_Java.Add("public static final short SCRIPT_FUN_" + name + " = " + FunctionGlobleID + ";");
            FunctionGlobleID++;

        }

        #endregion
    }

}
