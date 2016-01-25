using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.mod;
using Cyclone.alg;
using System.Collections;
using System.IO;
using Cyclone.mod.anim;
using Cyclone.mod.map;
using Cyclone.mod.script;
using Cyclone.ui_script;
using Cyclone.mod.util;
using Cyclone.alg.type;
using Cyclone.alg.util;

namespace Cyclone.mod.prop
{
    public partial class Form_ProptiesManager : Form, ActiveItem
    {
        private Form_Main form_main=null;
        public Form_ProptiesManager(Form_Main form_mainT)
        {
            InitializeComponent();
            form_main = form_mainT;
            initParams(true);
        }
        //��ʼ������
        public void initParams(bool forbitUpdate)
        {
            propertyTypesManager = form_main.propertyTypesManager;
            iDsManager = form_main.iDsManager;
            if (propertyTypesManager.listBox == null || !propertyTypesManager.listBox.Equals(listBox_Obj_Define) || forbitUpdate)
            {
                propertyTypesManager.refreshUI(listBox_Obj_Define);
            }
            if (iDsManager.listBox == null || !iDsManager.listBox.Equals(listBox_IDs) || forbitUpdate)
            {
                iDsManager.refreshUI(listBox_IDs);
            }
        }
        //################################################ ͨ���¼����� ################################################
        //ContextMenu
        private void ��ӵ�ԪToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl_Lists.SelectedIndex == 0)
            {
                addElement_ObjDefine();
            }
        }

        private void ��������ԪToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl_Lists.SelectedIndex == 0)
            {
                configElement_ObjDefine();
            }
            else if (tabControl_Lists.SelectedIndex == 1)
            {

            }
        }
        private void ���Ƶ�ԪToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl_Lists.SelectedIndex == 0)
            {
                moveUpElement_ObjDefine();
            }
        }

        private void ���Ƶ�ԪToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl_Lists.SelectedIndex == 0)
            {
                moveDownElement_ObjDefine();
            }
        }
        private void ɾ����ԪToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tabControl_Lists.SelectedIndex == 0)
            {
                deleteElement_ObjDefine();
            }
            else if (tabControl_Lists.SelectedIndex == 1)
            {

            }
        }

        //commonMouseDown
        private void commonMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            ((Control)sender).Focus();
            if (e.Button.Equals(MouseButtons.Right))
            {
                CMS_Common.Show((Control)sender, new Point(e.X, e.Y));
            }
        }


        private void listBox_TriggerList_MouseDown(object sender, MouseEventArgs e)
        {
            commonMouseDown(sender, e);
        }

        private void listBox_TriggerCondition_MouseDown(object sender, MouseEventArgs e)
        {
            commonMouseDown(sender, e);
        }

        private void listBox_ContextCondition_MouseDown(object sender, MouseEventArgs e)
        {
            commonMouseDown(sender, e);
        }

        private void listBox_StructList_MouseDown(object sender, MouseEventArgs e)
        {
            commonMouseDown(sender, e);
        }
        private void listBox_StructContext_MouseDown(object sender, MouseEventArgs e)
        {
            commonMouseDown(sender, e);
        }

        private void listBox_StructExecution_MouseDown(object sender, MouseEventArgs e)
        {
            commonMouseDown(sender, e);
        }
        //commonDoubleClick
        private void commonDoubleClick(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            if (((MouseEventArgs)e).Y < listBox.Items.Count * listBox.ItemHeight)
            {
                if (tabControl_Lists.SelectedIndex == 0)
                {
                    configElement_ObjDefine();
                }
                else if (tabControl_Lists.SelectedIndex == 1)
                {
                    configElement_IDs();
                }
            }
        }
        //commonKeyDown
        private void commonKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete&&((ListBox)sender).SelectedIndex>=0)
            {
                if (tabControl_Lists.SelectedIndex == 0)
                {
                    deleteElement_ObjDefine();
                }
                else if (tabControl_Lists.SelectedIndex ==1)
                {
                    deleteElement_IDs();
                }
            }
            else if (e.Control)
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (tabControl_Lists.SelectedIndex == 0)
                    {
                        moveUpElement_ObjDefine();
                    }
                    else if (tabControl_Lists.SelectedIndex == 1)
                    {
                        moveUpElement_IDs();
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (tabControl_Lists.SelectedIndex == 0)
                    {
                        moveDownElement_ObjDefine();
                    }
                    else if (tabControl_Lists.SelectedIndex == 1)
                    {
                        moveDownElement_IDs();
                    }
                }
                e.Handled = true;
            }
        }
        private void commonKeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar&0x60)==0)
            {
                e.Handled = true;
                //Console.WriteLine("listBox_Sentences_KeyPress handled" + e.KeyChar);
            }
        }
        //################################################ �������ݱ༭���� ################################################
        private PropertyTypesManager propertyTypesManager = null;
        private PropertyTypeElement currentObjTypeElement = null;
        private InstanceElement currentInstanceElement = null;
        //�������ý���������͵�Ԫ
        private void resetCurrentObjTypeElement()
        {
            if (listBox_Obj_Define.SelectedIndex < 0)
            {
                currentObjTypeElement = null;
            }
            else
            {
                currentObjTypeElement = (PropertyTypeElement)propertyTypesManager.getElement(listBox_Obj_Define.SelectedIndex);
                currentObjTypeElement.instancesManager.setUI(listBox_ObjInstances);
            }
            //��ʾ������Ϣ
            if (currentObjTypeElement != null)
            {
                currentObjTypeElement.propertiesManager.refreshUI(listBox_ObjProperties);
            }
            else
            {
                listBox_ObjProperties.Items.Clear();
            }
            //��ʾʵ����������Ϣ
            if (currentObjTypeElement != null)
            {
                currentObjTypeElement.instancesManager.refreshUI(listBox_ObjInstances);
            }
            else
            {
                listBox_ObjInstances.Items.Clear();
            }
            resetCurrentInstanceElement();
        }
        //�������ý���ʵ����Ԫ
        private void resetCurrentInstanceElement()
        {
            if (currentObjTypeElement == null)
            {
                listBox_ObjInstances.Items.Clear();
                listBox_InstanceProperty.Items.Clear();
                return;
            }
            if (listBox_ObjInstances.SelectedIndex < 0)
            {
                currentInstanceElement = null;
            }
            else
            {
                currentInstanceElement = (InstanceElement)currentObjTypeElement.instancesManager.getElement(listBox_ObjInstances.SelectedIndex);
            }
            //��ʾ������Ϣ
            if (currentInstanceElement != null)
            {
                currentInstanceElement.propertyValueManager.refreshUI(listBox_InstanceProperty);
            }
            else
            {
                listBox_InstanceProperty.Items.Clear();
            }
        }
        //���ӵ�Ԫ
        private void addElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//���Ӷ������͵�Ԫ
            {
                PropertyTypeElement element = new PropertyTypeElement(propertyTypesManager);
                element.setName(SmallDialog_WordEdit.getNewName("�¶������͵�Ԫ", element.name));
                propertyTypesManager.insertElement(element, listBox_Obj_Define.SelectedIndex + 1);
            }
            else if (currentObjTypeElement != null)
            {
                if (listBox_ObjProperties.Focused)//�������Ե�Ԫ
                {
                    PropertyElement element = SmallDialog_PropertyDefine.createElement(currentObjTypeElement.propertiesManager);
                    if (element != null)
                    {
                        int index = listBox_ObjProperties.SelectedIndex+1;
                        currentObjTypeElement.propertiesManager.insertElement(element, index);
                        currentObjTypeElement.instancesManager.inseartProperty(element, index);
                    }
                }
                else if (listBox_ObjInstances.Focused)//���Ӷ���ʵ����Ԫ
                {
                    InstanceElement element = new InstanceElement(currentObjTypeElement.instancesManager);
                    element.propertyValueManager.refreshProperty();
                    element.setName(SmallDialog_WordEdit.getNewName("�¶���ʵ����Ԫ", element.name));
                    currentObjTypeElement.instancesManager.insertElement(element, listBox_ObjInstances.SelectedIndex + 1);
                    element.propertyValueManager.refreshUI(listBox_InstanceProperty);
                }
            }
        }
        //���Ƶ�Ԫ
        private void moveUpElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//�������͵�Ԫ
            {
                propertyTypesManager.moveUpElement(listBox_Obj_Define.SelectedIndex);
            }
            else if (currentObjTypeElement != null)
            {
                if (listBox_ObjProperties.Focused)//���Ե�Ԫ
                {
                    int index = listBox_ObjProperties.SelectedIndex;
                    currentObjTypeElement.propertiesManager.moveUpElement(index);
                    currentObjTypeElement.instancesManager.moveUpProperty(index);
                }
                else if (listBox_ObjInstances.Focused)//����ʵ����Ԫ
                {
                    currentObjTypeElement.instancesManager.moveUpElement(listBox_ObjInstances.SelectedIndex);
                }
            }
        }
        //���Ƶ�Ԫ
        private void moveDownElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//�������͵�Ԫ
            {
                propertyTypesManager.moveDownElement(listBox_Obj_Define.SelectedIndex);
            }
            else if (currentObjTypeElement != null)
            {
                if (listBox_ObjProperties.Focused)//���Ե�Ԫ
                {
                    int index = listBox_ObjProperties.SelectedIndex;
                    currentObjTypeElement.propertiesManager.moveDownElement(index);
                    currentObjTypeElement.instancesManager.moveDownProperty(index);
                }
                else if (listBox_ObjInstances.Focused)//����ʵ����Ԫ
                {
                    currentObjTypeElement.instancesManager.moveDownElement(listBox_ObjInstances.SelectedIndex);
                }
            }
        }
        //��¡��Ԫ
        private void cloneElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//������൥Ԫ
            {
                int id = listBox_Obj_Define.SelectedIndex;
                if (id >= 0)
                {
                    propertyTypesManager.cloneElement(id);
                    Console.WriteLine("---");
                }
            }
            else if (currentObjTypeElement != null)
            {
                if (listBox_ObjProperties.Focused)//���Ե�Ԫ
                {
                    int id = listBox_ObjProperties.SelectedIndex;
                    if (id >= 0)
                    {
                        currentObjTypeElement.propertiesManager.cloneElement(id);
                        currentObjTypeElement.instancesManager.addProperty((PropertyElement)currentObjTypeElement.propertiesManager.
                            getElement(currentObjTypeElement.propertiesManager.getElementCount()-1));
                    }
                }
                else if (currentInstanceElement != null)
                {
                    if (listBox_ObjInstances.Focused)//ʵ������Ԫ
                    {
                        int id = listBox_ObjInstances.SelectedIndex;
                        if (id >= 0)
                        {
                            currentObjTypeElement.instancesManager.cloneElement(id);
                        }
                    }
                }

            }
        }
        //���õ�Ԫ
        private void configElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//���ö���Ԫ
            {
                if (currentObjTypeElement == null)
                {
                    return;
                }
                currentObjTypeElement.setName(SmallDialog_WordEdit.getNewName("���ö������͵�Ԫ", currentObjTypeElement.name));
            }
            else if (currentObjTypeElement != null)
            {
                if (listBox_ObjProperties.Focused)//�����������͵�Ԫ
                {
                    int id = listBox_ObjProperties.SelectedIndex;
                    if (id >= 0)
                    {
                        PropertyElement currentProperty = (PropertyElement)(currentObjTypeElement.propertiesManager.getElement(id));
                        if (SmallDialog_PropertyDefine.configElement(currentProperty))
                        {
                            currentObjTypeElement.propertiesManager.refreshUI_Element(id);
                            //currentObjTypeElement.instancesManager.configProperty(currentProperty);
                            if (currentInstanceElement != null)
                            {
                                currentInstanceElement.propertyValueManager.refreshUI_Element(currentProperty.getID());
                            }
                        }
                    }
                }
                else if (currentInstanceElement != null)
                {
                    if (listBox_ObjInstances.Focused)//����ʵ������Ԫ
                    {
                        currentInstanceElement.setName(SmallDialog_WordEdit.getNewName("����ʵ������Ԫ", currentInstanceElement.name));
                    }
                    else if (listBox_InstanceProperty.Focused)//����ʵ������Ԫ������
                    {
                        ObjectElement elementObj = (ObjectElement)currentInstanceElement.propertyValueManager.getElement(listBox_InstanceProperty.SelectedIndex);
                        if (elementObj != null)
                        {
                            PropertyValueElement element = (PropertyValueElement)elementObj;
                            if(element.ValueType == Consts.PARAM_PROP)
                            {
                                PropertyValueManager pvalueManager = (PropertyValueManager)element.parent;
                                int id = element.getID();
                                PropertyElement pElement = (PropertyElement)pvalueManager.propertiesManager.getElement(id);
                                element.setValue(SmallDialog_PropertyValue.getValue("��������ֵ", element.getValue(), element.ValueType, pElement.propertyTypeElementUsed != null ? pElement.propertyTypeElementUsed.instancesManager : null));
                            }
                            else if (element.ValueType == Consts.PARAM_INT_ID)
                            {
                                element.setValue(SmallDialog_PropertyValue.getValue("��������ֵ", element.getValue(), element.ValueType, form_main.iDsManager));
                            }
                            else
                            {
                                element.setValue(SmallDialog_PropertyValue.getValue("��������ֵ", element.getValue(), element.ValueType, null));
                            }
                            currentInstanceElement.propertyValueManager.refreshUI_Element(element.getID());
                        }
                        //currentInstanceElement.setName(SmallDialog_Text.getNewName("����ʵ������Ԫ", currentInstanceElement.name));
                    }
                }

            }
        }
        //ɾ����Ԫ
        private void deleteElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//ɾ��PropertyTypeElement��Ԫ
            {
                if (currentObjTypeElement == null)
                {
                    return;
                }
                DialogResult res = MessageBox.Show("ȷ��ɾ���������͡�" + currentObjTypeElement.name + "����", "����", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (!res.Equals(DialogResult.OK))
                {
                    return;
                }
                if (currentObjTypeElement.getUsedTime() > 0)
                {
                    SmallDialog_ShowParagraph.showString("���󾯸�(Ŀ�걻����)", currentObjTypeElement.getUsedTimeInf());
                    return;
                }
                propertyTypesManager.removeElement(currentObjTypeElement.getID());
            }
            else if (currentObjTypeElement != null)
            {
                if (listBox_ObjProperties.Focused)//ɾ��PropertyElement��Ԫ
                {
                    int id = listBox_ObjProperties.SelectedIndex;
                    if (id >= 0)
                    {
                        DialogResult res = MessageBox.Show("ȷ��ɾ�����ԡ�" + ((ObjectElement)currentObjTypeElement.propertiesManager.getElement(id)).name + "����", "����", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (!res.Equals(DialogResult.OK))
                        {
                            return;
                        }
                        PropertyElement currentProperty = (PropertyElement)(currentObjTypeElement.propertiesManager.getElement(id));
                        if (currentObjTypeElement.instancesManager.removeProperty(currentProperty))
                        {
                            currentObjTypeElement.propertiesManager.removeElement(id);
                            resetCurrentInstanceElement();
                        }
                    }
                }
                else if (currentInstanceElement != null)
                {
                    if (listBox_ObjInstances.Focused)//ɾ��InstanceElement��Ԫ
                    {
                        DialogResult res = MessageBox.Show("ȷ��ɾ��ʵ������" + currentInstanceElement.name + "����", "����", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (!res.Equals(DialogResult.OK))
                        {
                            return;
                        }
                        if (currentInstanceElement.getUsedTime() > 0)
                        {
                            SmallDialog_ShowParagraph.showString("���󾯸�(Ŀ�걻����)", currentInstanceElement.getUsedTimeInf());
                            return;
                        }
                        currentObjTypeElement.instancesManager.removeElement(currentInstanceElement.getID());
                    }
                }
            }
        }
        //commonMouseDown
        private void listBox_Obj_Define_MouseDown(object sender, MouseEventArgs e)
        {
            commonMouseDown(sender, e);
        }
        private void listBox_ObjProperties_MouseDown(object sender, MouseEventArgs e)
        {
            commonMouseDown(sender, e);
        }

        private void listBox_ObjInstances_MouseDown(object sender, MouseEventArgs e)
        {
            commonMouseDown(sender, e);
        }

        private void listBox_Obj_Define_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetCurrentObjTypeElement();
        }

        private void listBox_ObjInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetCurrentInstanceElement();
        }

        //################################################ ������������� ################################################
        private VarsManager iDsManager = null;
        //���ӵ�Ԫ
        private void addElement_IDs()
        {
            if (listBox_IDs.Focused)//��������
            {
                VarElement element = SmallDialog_NewVar_INT.createElement(iDsManager);
                if (element != null)
                {
                    iDsManager.insertElement(element, listBox_IDs.SelectedIndex+1);
                }

            }
        }
        //���õ�Ԫ
        private void configElement_IDs()
        {
            if (listBox_IDs.Focused)//��������
            {
                if (listBox_IDs.SelectedIndex >= 0)
                {
                    SmallDialog_NewVar_INT.configElement((VarElement)iDsManager.getElement(listBox_IDs.SelectedIndex));
                    iDsManager.refreshUI_Element(listBox_IDs.SelectedIndex);
                }
            }
        }
        //ɾ����Ԫ
        private void deleteElement_IDs()
        {
            if (listBox_IDs.Focused)//��������
            {
                ArrayList array = new ArrayList();
                for (int i = 0; i < listBox_IDs.SelectedIndices.Count; i++)
                {
                    int id = (int)listBox_IDs.SelectedIndices[i];
                    array.Add(id);
                }
                int skip = 0;
                while (array.Count>0)
                {
                    int currentIndex = (int)array[0] + skip;
                    bool allowDel = true;
                    if (currentIndex >= 0)
                    {
                        VarElement element = (VarElement)iDsManager.getElement(currentIndex);
                        if (element == null)
                        {
                            allowDel = false;
                        }
                        if (element.isFixed || element.isUsedInHeadFile)
                        {
                            allowDel = false;
                            DialogResult dres = MessageBox.Show("��Ԫ����ɹ̶����߱�ͷ�ļ�ʹ�ã�����ɾ��", "����", MessageBoxButtons.OKCancel);
                            if (dres.Equals(DialogResult.Cancel))
                            {
                                break;
                            }

                        }
                        int usedTime = element.getUsedTime();
                        if (usedTime > 0)
                        {
                            allowDel = false;
                            DialogResult dres = MessageBox.Show("������������" + usedTime + "�Σ�����ɾ��", "����", MessageBoxButtons.OKCancel);
                            if (dres.Equals(DialogResult.Cancel))
                            {
                                break;
                            }
                        }
                        if (allowDel)
                        {
                            iDsManager.removeElement(currentIndex);
                            skip--;
                        }
                        array.RemoveAt(0);
                    }
                }
                array.Clear();
                array = null;

            }
        }
        //���Ƶ�Ԫ
        private void moveUpElement_IDs()
        {
            if (listBox_IDs.Focused)//��������
            {
                iDsManager.moveUpElement(listBox_IDs.SelectedIndex);
            }
        }
        //���Ƶ�Ԫ
        private void moveDownElement_IDs()
        {
            if (listBox_IDs.Focused)//��������
            {
                iDsManager.moveDownElement(listBox_IDs.SelectedIndex);
            }
        }
        //��¡��Ԫ
       private void cloneElement_IDs()
        {
            if (listBox_IDs.Focused)//��������
            {
                iDsManager.cloneElement(listBox_IDs.SelectedIndex);
            }
        }
        private void listBox_IDs_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            ((Control)sender).Focus();
            if (e.Button.Equals(MouseButtons.Right))
            {
                CMS_IDs.Show((Control)sender, new Point(e.X, e.Y));
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addElement_IDs();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            configElement_IDs();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            deleteElement_IDs();
        }

        private void ���������ɳ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateElement_IDS_Prop();
        }

        private void tabControl_Lists_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ���Ƶ�ԪToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            moveUpElement_IDs();
        }

        private void ���Ƶ�ԪToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            moveDownElement_IDs();
        }

        private void ��¡��ԪToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl_Lists.SelectedIndex == 0)
            {
                cloneElement_ObjDefine();
            }
        }

        private void ��¡��ԪToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cloneElement_IDs();
        }

        private void �Ӷ������ɳ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateElement_IDS_Anim();
        }
        //�Ӷ��������Զ�����
        private void generateElement_IDS_Anim()
        {
            listBox_IDs.BeginUpdate();
            MActorsManager actorsManager = form_main.form_MAnimation.form_MActorList.actorsManager;
            for (int i = 0; i < actorsManager.Count(); i++)
            {
                String IDName = "SpriteFolder_" + actorsManager[i].name;
                VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                varElementNew.setValue(i);
                varElementNew.name = IDName;
                iDsManager.updateVarElement(varElementNew);
                for (int j = 0; j < actorsManager[i].Count(); j++)
                {
                    MActor actorElement = actorsManager[i][j];
                    IDName = "Sprite_" + actorsManager[i].name + "_" + actorElement.name;
                    varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                    varElementNew.setValue(j);
                    varElementNew.name = IDName;
                    iDsManager.updateVarElement(varElementNew);
                }
            }
            listBox_IDs.EndUpdate();
        }
        //�����������Զ�����
        private void generateElement_IDS_Prop()
        {
            listBox_IDs.BeginUpdate();
            //���ɶ������ͳ���
            for (int i = 0; i < propertyTypesManager.getElementCount(); i++)
            {
                PropertyTypeElement propertyTypeElement = (PropertyTypeElement)propertyTypesManager.getElement(i);
                String IDName = "Prop_Gene_" + propertyTypeElement.name;
                int IDValue = i;
                VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                varElementNew.setValue(IDValue);
                varElementNew.name = IDName;
                iDsManager.updateVarElement(varElementNew);
            }

            for (int i = 0; i < propertyTypesManager.getElementCount(); i++)
            {
                PropertyTypeElement propertyTypeElement = (PropertyTypeElement)propertyTypesManager.getElement(i);
                //�����������Գ���
                for (int j = 0; j < propertyTypeElement.propertiesManager.getElementCount(); j++)
                {
                    PropertyElement priopertyElement = (PropertyElement)propertyTypeElement.propertiesManager.getElement(j);
                    String IDName = "Prop_Attr_" + propertyTypeElement.name + "_" + priopertyElement.name;
                    int IDValue = j;
                    VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                    varElementNew.setValue(IDValue);
                    varElementNew.name = IDName;
                    iDsManager.updateVarElement(varElementNew);
                }
                //����ʵ������
                for (int j = 0; j < propertyTypeElement.instancesManager.getElementCount(); j++)
                {
                    InstanceElement instanceElement = (InstanceElement)propertyTypeElement.instancesManager.getElement(j);
                    String IDName = "Prop_Inst_" + propertyTypeElement.name + "_" + instanceElement.name;
                    int IDValue = j;
                    VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                    varElementNew.setValue(IDValue);
                    varElementNew.name = IDName;
                    iDsManager.updateVarElement(varElementNew);
                }
            }
            listBox_IDs.EndUpdate();

        }
        //�ӵ�ͼ�����Զ�����
        private void generateElement_IDS_Map()
        {
            listBox_IDs.BeginUpdate();
            for (int i = 0; i < form_main.mapsManager.getElementCount(); i++)
            {
                MapElement mapElement = form_main.mapsManager.getElement(i);
                String IDName = "Map_" + mapElement.getName();
                int IDValue = mapElement.getID();
                VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                varElementNew.setValue(IDValue);
                varElementNew.name = IDName;
                iDsManager.updateVarElement(varElementNew);
                for (int j = 0; j < mapElement.stageList.getElementCount(); j++)
                {
                    StageElement stage = (StageElement)mapElement.stageList.getElement(j);
                    IDName = "Stage_" + mapElement.getName() + "_" + stage.name;
                    IDValue = j;
                    varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                    varElementNew.setValue(IDValue);
                    varElementNew.name = IDName;
                    iDsManager.updateVarElement(varElementNew);
                }
            }
            listBox_IDs.EndUpdate();
        }
        ////�ӽű������Զ�����
        //private void generateElement_IDS_Script()
        //{
            //for (int i = 0; i < triggersManager.getElementCount(); i++)
            //{
            //    TriggerPackElement triggerElement = (TriggerPackElement)triggersManager.getElement(i);
            //    String IDName = "TRIGGER_" + triggerElement.name;
            //    VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
            //    varElementNew.setValue(i);
            //    varElementNew.name = IDName;
            //    iDsManager.updateVarElement(varElementNew);
            //}
            //for (int i = 0; i < triggersManager.getElementCount(); i++)
            //{
            //    TriggerPackElement trigger = (TriggerPackElement)triggersManager.getElement(i);
            //    for (int j = 0; j < trigger.triggersManager.getElementCount(); j++)
            //    {
            //        Trigger exeStruct = (Trigger)trigger.triggersManager.getElement(j);
            //        String IDName = "TRIGGER_" + trigger.name + "_" + exeStruct.name;
            //        VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
            //        varElementNew.setValue(j);
            //        varElementNew.name = IDName;
            //        iDsManager.updateVarElement(varElementNew);
            //    }
            //}
        //}
        //�ӽ�ɫԭ���Զ�����
        private void generateElement_IDS_AnteType()
        {
            listBox_IDs.BeginUpdate();
            for (int i = 0; i < form_main.mapsManager.antetypesManager.Count(); i++)
            {
                AntetypeFolder folder = form_main.mapsManager.antetypesManager[i];
                String IDName = "AntetypeFolder_" + folder.name;
                VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                varElementNew.setValue(i);
                varElementNew.name = IDName;
                iDsManager.updateVarElement(varElementNew);
                for (int j = 0; j < folder.Count(); j++)
                {
                    Antetype anteType = folder[j];
                    IDName = "Antetype_" + folder.name + "_" + anteType.name;
                    varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                    varElementNew.setValue(j);
                    varElementNew.name = IDName;
                    iDsManager.updateVarElement(varElementNew);
                }
            }
            listBox_IDs.EndUpdate();
        }
        private void �ӵ�ͼ���ɳ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateElement_IDS_Map();
        }

        private void button_order_Click(object sender, EventArgs e)
        {
            DialogResult res= MessageBox.Show("��������������", "����", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (res.Equals(DialogResult.OK))
            {
                iDsManager.orderByname();
            }
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("���δʹ�õĳ�����", "����", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (res.Equals(DialogResult.OK))
            {
                iDsManager.clearSpilth();
            }
        }

        private void button_Check_Click(object sender, EventArgs e)
        {
            if (listBox_IDs.SelectedIndex >= 0)
            {
                VarElement IDElement = (VarElement)iDsManager.getElement(listBox_IDs.SelectedIndex);
                SmallDialog_ShowParagraph.showString("������Ԫʹ�ü��", IDElement.getUsedInfor());
            }
        }

        private void listBox_IDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_IDs.SelectedIndices.Count == 1 && listBox_IDs.SelectedIndex >= 0)
            {
                VarElement IDElement = (VarElement)iDsManager.getElement(listBox_IDs.SelectedIndex);
                checkBox_fixed.Checked = IDElement.isFixed;
                checkBox_UsedByHeadFile.Checked = IDElement.isUsedInHeadFile;
            }
        }
        bool noEvent = false;
        private void checkBox_fixed_CheckedChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            noEvent = true;
            ArrayList array = new ArrayList();
            for (int i = 0; i < listBox_IDs.SelectedIndices.Count; i++)
            {
                int id = (int)listBox_IDs.SelectedIndices[i];
                array.Add(id);
            }
            for (int i = 0; i < array.Count; i++)
            {
                int id = (int)array[i];
                if (id >= 0)
                {
                    VarElement IDElement = (VarElement)iDsManager.getElement(id);
                    IDElement.isFixed = checkBox_fixed.Checked;
                    iDsManager.refreshUI_Element(id);
                }
                listBox_IDs.SelectedItems.Add(listBox_IDs.Items[id]);
            }
            array.Clear();
            array = null;
            noEvent = false;

        }
        private void checkBoxUsedInHeadFile_CheckedChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            noEvent = true;
            ArrayList array = new ArrayList();
            for (int i = 0; i < listBox_IDs.SelectedIndices.Count; i++)
            {
                int id = (int)listBox_IDs.SelectedIndices[i];
                array.Add(id);
            }
            for (int i = 0; i < array.Count; i++)
            {
                int id = (int)array[i];
                if (id >= 0)
                {
                    VarElement IDElement = (VarElement)iDsManager.getElement(id);
                    IDElement.isUsedInHeadFile = checkBox_UsedByHeadFile.Checked;
                    iDsManager.refreshUI_Element(id);
                }
                listBox_IDs.SelectedItems.Add(listBox_IDs.Items[id]);
            }
            array.Clear();
            array = null;
            noEvent = false;

        }
        private void button_es_propertyTypes_Click(object sender, EventArgs e)
        {
            listBox_IDs.BeginUpdate();
            //���ɶ������ͳ���
            for (int i = 0; i < propertyTypesManager.getElementCount(); i++)
            {
                PropertyTypeElement propertyTypeElement = (PropertyTypeElement)propertyTypesManager.getElement(i);
                String IDName = "PROP_GENE_" + propertyTypeElement.name;
                int IDValue = i;
                VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                varElementNew.setValue(IDValue);
                varElementNew.name = IDName;
                iDsManager.updateVarElement(varElementNew);
            }
            iDsManager.refreshUI();
            listBox_IDs.EndUpdate();
        }

        private void button_es_Properties_Click(object sender, EventArgs e)
        {
            PropertyTypeElement propertyTypeElement = currentObjTypeElement;
            if (currentObjTypeElement == null)
            {
                return;
            }
            listBox_IDs.BeginUpdate();
            //�����������Գ���
            for (int j = 0; j < propertyTypeElement.propertiesManager.getElementCount(); j++)
            {
                PropertyElement priopertyElement = (PropertyElement)propertyTypeElement.propertiesManager.getElement(j);
                String IDName = "PROP_ATTR_" + propertyTypeElement.name + "_" + priopertyElement.name;
                int IDValue = j;
                VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                varElementNew.setValue(IDValue);
                varElementNew.name = IDName;
                iDsManager.updateVarElement(varElementNew);
            }
            iDsManager.refreshUI();
            listBox_IDs.EndUpdate();
        }

        private void button_es_PropertyInstants_Click(object sender, EventArgs e)
        {
            PropertyTypeElement propertyTypeElement = currentObjTypeElement;
            if (currentObjTypeElement == null)
            {
                return;
            }
            listBox_IDs.BeginUpdate();
            //����ʵ������
            for (int j = 0; j < propertyTypeElement.instancesManager.getElementCount(); j++)
            {
                InstanceElement instanceElement = (InstanceElement)propertyTypeElement.instancesManager.getElement(j);
                String IDName = "PROP_INST_" + propertyTypeElement.name + "_" + instanceElement.name;
                int IDValue = j;
                VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                varElementNew.setValue(IDValue);
                varElementNew.name = IDName;
                iDsManager.updateVarElement(varElementNew);
            }
            iDsManager.refreshUI();
            listBox_IDs.EndUpdate();
        }

        private void �ӽ�ɫԭ�����ɳ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateElement_IDS_AnteType();
        }

        private void Form_ScriptsManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Form_ScriptsManager_Activated(object sender, EventArgs e)
        {
        }
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            if (form_main.saveUserData())
            {
                MessageBox.Show("�ĵ��������","����",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
        }
        //ʵ����Ա�е����Ը�����ճ��
        private ArrayList copyedValueElement = new ArrayList();//���Լ�����
        private void copyValues__toLocal()
        {
            copyedValueElement.Clear();
            if (currentInstanceElement != null)
            {
                if (listBox_InstanceProperty.Focused)//����ʵ������Ԫ������
                {
                    for (int i = 0; i < listBox_InstanceProperty.SelectedIndices.Count; i++)
                    {
                        ObjectElement elementObj = (ObjectElement)currentInstanceElement.propertyValueManager.getElement(listBox_InstanceProperty.SelectedIndices[i]);
                        copyedValueElement.Add(elementObj);
                    }
                }
            }
        }
        private void pasteValues_fromLocal()
        {
            if (currentInstanceElement != null)
            {
                ArrayList arraySelected = new ArrayList();
                for (int i = 0; i < listBox_InstanceProperty.SelectedIndices.Count; i++)
                {
                    arraySelected.Add(listBox_InstanceProperty.SelectedIndices[i]);
                }
                if (listBox_InstanceProperty.Focused)//����ʵ������Ԫ������
                {
                    for (int i = 0; i < arraySelected.Count; i++)
                    {
                        ObjectElement elementObj = (ObjectElement)currentInstanceElement.propertyValueManager.getElement(Convert.ToInt32(arraySelected[i]));
                        if (elementObj != null && copyedValueElement != null)
                        {
                            //�������������ݵĴ�����
                            if (i < copyedValueElement.Count && copyedValueElement[i] != null)
                            {
                                PropertyValueElement elementCopy = (PropertyValueElement)copyedValueElement[i];
                                if (elementCopy.getID() >= 0)
                                {
                                    PropertyValueElement element = (PropertyValueElement)elementObj;
                                    if (element.ValueType == elementCopy.ValueType)
                                    {
                                        element.setValue(((PropertyValueElement)elementCopy.clone()).getValue());
                                        currentInstanceElement.propertyValueManager.refreshUI_Element(element.getID());
                                    }
                                }
                            }

                        }
                    }

                }
            }
        }
        //���Ƶ�ϵͳ������
        private void copyValues_toSystem()
        {
            if (currentInstanceElement != null)
            {
                if (listBox_InstanceProperty.Focused)
                {
                    String sCopy = "";
                    for (int i = 0; i < listBox_InstanceProperty.SelectedIndices.Count; i++)
                    {
                        PropertyValueElement element = (PropertyValueElement)currentInstanceElement.propertyValueManager.getElement(listBox_InstanceProperty.SelectedIndices[i]);
                        if (element != null && (element.ValueType == Consts.PARAM_INT || element.ValueType == Consts.PARAM_STR))
                        {
                            if (element.ValueType == Consts.PARAM_STR)
                            {
                                sCopy += (String)element.getValue() + "\n";
                            }
                            else
                            {
                                sCopy += Convert.ToInt32(element.getValue()) + "\n";
                            }
                        }
                        else
                        {
                            sCopy += "��\n";
                        }
                    }
                    Clipboard.SetDataObject(sCopy, false);
                }
            }
        }
        private void pasteValues_fromSystem()
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                String data = (String)iData.GetData(DataFormats.Text);
                String[] dataList = data.Split(new char[] { '\n' });
                ArrayList arraySelected = new ArrayList();
                for (int i = 0; i < listBox_InstanceProperty.SelectedIndices.Count; i++)
                {
                    arraySelected.Add(listBox_InstanceProperty.SelectedIndices[i]);
                }
                if (listBox_InstanceProperty.Focused)//����ʵ������Ԫ������
                {
                    for (int i = 0; i < arraySelected.Count; i++)
                    {
                        PropertyValueElement element = (PropertyValueElement)currentInstanceElement.propertyValueManager.getElement(Convert.ToInt32(arraySelected[i]));
                        if (element != null && i < dataList.Length && dataList[i] != null)
                        {
                            if (element.ValueType == Consts.PARAM_INT || element.ValueType == Consts.PARAM_STR)
                            {
                                if (element.ValueType == Consts.PARAM_STR)
                                {
                                    element.setValue(dataList[i]);
                                }
                                else
                                {
                                    try
                                    {
                                        int intValue = Convert.ToInt32(dataList[i]);
                                        element.setValue(intValue);
                                    }
                                    catch (Exception)
                                    {
                                        MessageBox.Show("��" + dataList[i] + "������ת����������ֵ\n");
                                    }
                                }
                                currentInstanceElement.propertyValueManager.refreshUI_Element(element.getID());
                            }

                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("�������ʽ����");
            }
        }
        private void listBox_InstanceProperty_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)//����
            {
                if(!e.Shift)
                {
                    copyValues_toSystem();
                }
                else
                {
                    copyValues__toLocal();
                }

            }
            else if (e.Control && e.KeyCode == Keys.V)//ճ��
            {
                if (!e.Shift)
                {
                    pasteValues_fromSystem();
                }
                else
                {
                    pasteValues_fromLocal();
                }

            }
        }
        private void �߼�����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyValues__toLocal();
        }

        private void �߼�ճ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasteValues_fromLocal();
        }
        private void �Զ��������г���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectVector.allowUpdateUI = false;
            generateElement_IDS_Prop();
            generateElement_IDS_Anim();
            generateElement_IDS_Map();
            generateElement_IDS_AnteType();
            ObjectVector.allowUpdateUI = true;
            iDsManager.refreshUI();
        }


        private void button_save_Click(object sender, EventArgs e)
        {
            if (form_main.saveUserData())
            {
                MessageBox.Show("�ĵ��������", "����", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }





        #region ActiveItem ��Ա

        void ActiveItem.activeItem(ShowItem showItem)
        {
            if (showItem == null)
            {
                return;
            }

        }

        #endregion

        private void button_genHead_Click(object sender, EventArgs e)
        {
            form_main.form_VarsManager.generateHeadFile();
        }
        public void exportToHereadFile(FileStream fs)
        {
            for (int i = 0; i < iDsManager.getElementCount(); i++)
            {
                int id = i;
                VarElement IDElement = (VarElement)iDsManager.getElement(id);
                if (IDElement.isUsedInHeadFile)
                {
                    IOUtil.writeTextLine(fs, "#define " + IDElement.name + " " + Convert.ToInt32(IDElement.getValue()));
                }
            }
        }

        private void button_exportTXT_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            dialog.FileName = "";
            dialog.Title = "�����ı��ĵ�";
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                String filePath_Bin = dialog.FileName;
                FileStream fs_bin = null;
                try
                {
                    fs_bin = File.Open(filePath_Bin, FileMode.Create);
                    propertyTypesManager.exportAllToTxt(fs_bin);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (fs_bin != null)
                    {
                        try
                        {
                            fs_bin.Flush();
                            fs_bin.Close();
                            fs_bin = null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        private void button_importTXT_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            dialog.FileName = "";
            dialog.Title = "�����ı��ĵ�";
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                String filePath_Bin = dialog.FileName;
                FileStream fs_bin = null;
                try
                {
                    fs_bin = File.Open(filePath_Bin, FileMode.Open);
                    propertyTypesManager.importAllFromTxt(fs_bin);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (fs_bin != null)
                    {
                        try
                        {
                            fs_bin.Close();
                            fs_bin = null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                initParams(true);
                resetCurrentObjTypeElement();
            }
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyValues_toSystem();
        }

        private void ճ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasteValues_fromSystem();
        }

        private void listBox_InstanceProperty_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            ((Control)sender).Focus();
            if (e.Button.Equals(MouseButtons.Right))
            {
                CMS_propValues.Show((Control)sender, new Point(e.X, e.Y));
            }
        }

        private void btn_exportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "xlsx files (*.xlsx)|*.xlsx";
            dialog.FileName = "";
            dialog.Title = "����Excel�ĵ�";
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                String filePath_excel = dialog.FileName;
                try
                {
                    propertyTypesManager.exportAllToExcel(filePath_excel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
              
            }
        }

        private void btn_importExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "xlsx files (*.xlsx)|*.xlsx";
            dialog.FileName = "";
            dialog.Title = "����Excel�ĵ�";
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                String filePath_excel = dialog.FileName;
                try
                {
                    propertyTypesManager.importAllFromExcel(filePath_excel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                initParams(true);
                resetCurrentObjTypeElement();
            }
        }


    }
}