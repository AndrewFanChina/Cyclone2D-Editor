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
        //初始化界面
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
        //################################################ 通用事件处理 ################################################
        //ContextMenu
        private void 添加单元ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl_Lists.SelectedIndex == 0)
            {
                addElement_ObjDefine();
            }
        }

        private void 重命名单元ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl_Lists.SelectedIndex == 0)
            {
                configElement_ObjDefine();
            }
            else if (tabControl_Lists.SelectedIndex == 1)
            {

            }
        }
        private void 上移单元ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl_Lists.SelectedIndex == 0)
            {
                moveUpElement_ObjDefine();
            }
        }

        private void 下移单元ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl_Lists.SelectedIndex == 0)
            {
                moveDownElement_ObjDefine();
            }
        }
        private void 删除单元ToolStripMenuItem1_Click(object sender, EventArgs e)
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
        //################################################ 对象数据编辑界面 ################################################
        private PropertyTypesManager propertyTypesManager = null;
        private PropertyTypeElement currentObjTypeElement = null;
        private InstanceElement currentInstanceElement = null;
        //重新设置焦点对象类型单元
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
            //显示属性信息
            if (currentObjTypeElement != null)
            {
                currentObjTypeElement.propertiesManager.refreshUI(listBox_ObjProperties);
            }
            else
            {
                listBox_ObjProperties.Items.Clear();
            }
            //显示实例化对象信息
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
        //重新设置焦点实例单元
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
            //显示属性信息
            if (currentInstanceElement != null)
            {
                currentInstanceElement.propertyValueManager.refreshUI(listBox_InstanceProperty);
            }
            else
            {
                listBox_InstanceProperty.Items.Clear();
            }
        }
        //增加单元
        private void addElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//增加对象类型单元
            {
                PropertyTypeElement element = new PropertyTypeElement(propertyTypesManager);
                element.setName(SmallDialog_WordEdit.getNewName("新对象类型单元", element.name));
                propertyTypesManager.insertElement(element, listBox_Obj_Define.SelectedIndex + 1);
            }
            else if (currentObjTypeElement != null)
            {
                if (listBox_ObjProperties.Focused)//增加属性单元
                {
                    PropertyElement element = SmallDialog_PropertyDefine.createElement(currentObjTypeElement.propertiesManager);
                    if (element != null)
                    {
                        int index = listBox_ObjProperties.SelectedIndex+1;
                        currentObjTypeElement.propertiesManager.insertElement(element, index);
                        currentObjTypeElement.instancesManager.inseartProperty(element, index);
                    }
                }
                else if (listBox_ObjInstances.Focused)//增加对象实例单元
                {
                    InstanceElement element = new InstanceElement(currentObjTypeElement.instancesManager);
                    element.propertyValueManager.refreshProperty();
                    element.setName(SmallDialog_WordEdit.getNewName("新对象实例单元", element.name));
                    currentObjTypeElement.instancesManager.insertElement(element, listBox_ObjInstances.SelectedIndex + 1);
                    element.propertyValueManager.refreshUI(listBox_InstanceProperty);
                }
            }
        }
        //上移单元
        private void moveUpElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//对象类型单元
            {
                propertyTypesManager.moveUpElement(listBox_Obj_Define.SelectedIndex);
            }
            else if (currentObjTypeElement != null)
            {
                if (listBox_ObjProperties.Focused)//属性单元
                {
                    int index = listBox_ObjProperties.SelectedIndex;
                    currentObjTypeElement.propertiesManager.moveUpElement(index);
                    currentObjTypeElement.instancesManager.moveUpProperty(index);
                }
                else if (listBox_ObjInstances.Focused)//对象实例单元
                {
                    currentObjTypeElement.instancesManager.moveUpElement(listBox_ObjInstances.SelectedIndex);
                }
            }
        }
        //下移单元
        private void moveDownElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//对象类型单元
            {
                propertyTypesManager.moveDownElement(listBox_Obj_Define.SelectedIndex);
            }
            else if (currentObjTypeElement != null)
            {
                if (listBox_ObjProperties.Focused)//属性单元
                {
                    int index = listBox_ObjProperties.SelectedIndex;
                    currentObjTypeElement.propertiesManager.moveDownElement(index);
                    currentObjTypeElement.instancesManager.moveDownProperty(index);
                }
                else if (listBox_ObjInstances.Focused)//对象实例单元
                {
                    currentObjTypeElement.instancesManager.moveDownElement(listBox_ObjInstances.SelectedIndex);
                }
            }
        }
        //克隆单元
        private void cloneElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//对象分类单元
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
                if (listBox_ObjProperties.Focused)//属性单元
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
                    if (listBox_ObjInstances.Focused)//实例对象单元
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
        //配置单元
        private void configElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//配置对象单元
            {
                if (currentObjTypeElement == null)
                {
                    return;
                }
                currentObjTypeElement.setName(SmallDialog_WordEdit.getNewName("配置对象类型单元", currentObjTypeElement.name));
            }
            else if (currentObjTypeElement != null)
            {
                if (listBox_ObjProperties.Focused)//配置属性类型单元
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
                    if (listBox_ObjInstances.Focused)//配置实例对象单元
                    {
                        currentInstanceElement.setName(SmallDialog_WordEdit.getNewName("配置实例对象单元", currentInstanceElement.name));
                    }
                    else if (listBox_InstanceProperty.Focused)//配置实例对象单元的属性
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
                                element.setValue(SmallDialog_PropertyValue.getValue("配置属性值", element.getValue(), element.ValueType, pElement.propertyTypeElementUsed != null ? pElement.propertyTypeElementUsed.instancesManager : null));
                            }
                            else if (element.ValueType == Consts.PARAM_INT_ID)
                            {
                                element.setValue(SmallDialog_PropertyValue.getValue("配置属性值", element.getValue(), element.ValueType, form_main.iDsManager));
                            }
                            else
                            {
                                element.setValue(SmallDialog_PropertyValue.getValue("配置属性值", element.getValue(), element.ValueType, null));
                            }
                            currentInstanceElement.propertyValueManager.refreshUI_Element(element.getID());
                        }
                        //currentInstanceElement.setName(SmallDialog_Text.getNewName("配置实例对象单元", currentInstanceElement.name));
                    }
                }

            }
        }
        //删除单元
        private void deleteElement_ObjDefine()
        {
            if (listBox_Obj_Define.Focused)//删除PropertyTypeElement单元
            {
                if (currentObjTypeElement == null)
                {
                    return;
                }
                DialogResult res = MessageBox.Show("确定删除对象类型“" + currentObjTypeElement.name + "”？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (!res.Equals(DialogResult.OK))
                {
                    return;
                }
                if (currentObjTypeElement.getUsedTime() > 0)
                {
                    SmallDialog_ShowParagraph.showString("错误警告(目标被引用)", currentObjTypeElement.getUsedTimeInf());
                    return;
                }
                propertyTypesManager.removeElement(currentObjTypeElement.getID());
            }
            else if (currentObjTypeElement != null)
            {
                if (listBox_ObjProperties.Focused)//删除PropertyElement单元
                {
                    int id = listBox_ObjProperties.SelectedIndex;
                    if (id >= 0)
                    {
                        DialogResult res = MessageBox.Show("确定删除属性“" + ((ObjectElement)currentObjTypeElement.propertiesManager.getElement(id)).name + "”？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
                    if (listBox_ObjInstances.Focused)//删除InstanceElement单元
                    {
                        DialogResult res = MessageBox.Show("确定删除实例对象“" + currentInstanceElement.name + "”？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (!res.Equals(DialogResult.OK))
                        {
                            return;
                        }
                        if (currentInstanceElement.getUsedTime() > 0)
                        {
                            SmallDialog_ShowParagraph.showString("错误警告(目标被引用)", currentInstanceElement.getUsedTimeInf());
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

        //################################################ 常量索引表界面 ################################################
        private VarsManager iDsManager = null;
        //增加单元
        private void addElement_IDs()
        {
            if (listBox_IDs.Focused)//常量索引
            {
                VarElement element = SmallDialog_NewVar_INT.createElement(iDsManager);
                if (element != null)
                {
                    iDsManager.insertElement(element, listBox_IDs.SelectedIndex+1);
                }

            }
        }
        //配置单元
        private void configElement_IDs()
        {
            if (listBox_IDs.Focused)//常量索引
            {
                if (listBox_IDs.SelectedIndex >= 0)
                {
                    SmallDialog_NewVar_INT.configElement((VarElement)iDsManager.getElement(listBox_IDs.SelectedIndex));
                    iDsManager.refreshUI_Element(listBox_IDs.SelectedIndex);
                }
            }
        }
        //删除单元
        private void deleteElement_IDs()
        {
            if (listBox_IDs.Focused)//常量索引
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
                            DialogResult dres = MessageBox.Show("单元被设成固定或者被头文件使用，不能删除", "警告", MessageBoxButtons.OKCancel);
                            if (dres.Equals(DialogResult.Cancel))
                            {
                                break;
                            }

                        }
                        int usedTime = element.getUsedTime();
                        if (usedTime > 0)
                        {
                            allowDel = false;
                            DialogResult dres = MessageBox.Show("变量被引用了" + usedTime + "次，不能删除", "警告", MessageBoxButtons.OKCancel);
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
        //上移单元
        private void moveUpElement_IDs()
        {
            if (listBox_IDs.Focused)//常量索引
            {
                iDsManager.moveUpElement(listBox_IDs.SelectedIndex);
            }
        }
        //下移单元
        private void moveDownElement_IDs()
        {
            if (listBox_IDs.Focused)//常量索引
            {
                iDsManager.moveDownElement(listBox_IDs.SelectedIndex);
            }
        }
        //克隆单元
       private void cloneElement_IDs()
        {
            if (listBox_IDs.Focused)//常量索引
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

        private void 从属性生成常量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateElement_IDS_Prop();
        }

        private void tabControl_Lists_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void 上移单元ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            moveUpElement_IDs();
        }

        private void 下移单元ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            moveDownElement_IDs();
        }

        private void 克隆单元ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl_Lists.SelectedIndex == 0)
            {
                cloneElement_ObjDefine();
            }
        }

        private void 克隆单元ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cloneElement_IDs();
        }

        private void 从动画生成常量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateElement_IDS_Anim();
        }
        //从动画数据自动生成
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
        //从属性数据自动生成
        private void generateElement_IDS_Prop()
        {
            listBox_IDs.BeginUpdate();
            //生成对象类型常量
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
                //生成类型属性常量
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
                //生成实例常量
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
        //从地图数据自动生成
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
        ////从脚本数据自动生成
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
        //从角色原型自动生成
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
        private void 从地图生成常量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateElement_IDS_Map();
        }

        private void button_order_Click(object sender, EventArgs e)
        {
            DialogResult res= MessageBox.Show("将按照名称排序？", "提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (res.Equals(DialogResult.OK))
            {
                iDsManager.orderByname();
            }
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("清除未使用的常量？", "提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
                SmallDialog_ShowParagraph.showString("常量单元使用检查", IDElement.getUsedInfor());
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
            //生成对象类型常量
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
            //生成类型属性常量
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
            //生成实例常量
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

        private void 从角色原型生成常量ToolStripMenuItem_Click(object sender, EventArgs e)
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
                MessageBox.Show("文档保存完毕","提醒",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
        }
        //实例成员中的属性复制与粘贴
        private ArrayList copyedValueElement = new ArrayList();//属性剪贴板
        private void copyValues__toLocal()
        {
            copyedValueElement.Clear();
            if (currentInstanceElement != null)
            {
                if (listBox_InstanceProperty.Focused)//配置实例对象单元的属性
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
                if (listBox_InstanceProperty.Focused)//配置实例对象单元的属性
                {
                    for (int i = 0; i < arraySelected.Count; i++)
                    {
                        ObjectElement elementObj = (ObjectElement)currentInstanceElement.propertyValueManager.getElement(Convert.ToInt32(arraySelected[i]));
                        if (elementObj != null && copyedValueElement != null)
                        {
                            //检查剪贴板中数据的存在性
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
        //复制到系统剪贴板
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
                            sCopy += "≈\n";
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
                if (listBox_InstanceProperty.Focused)//配置实例对象单元的属性
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
                                        MessageBox.Show("“" + dataList[i] + "”不能转换成整型数值\n");
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
                MessageBox.Show("剪贴板格式有误。");
            }
        }
        private void listBox_InstanceProperty_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)//复制
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
            else if (e.Control && e.KeyCode == Keys.V)//粘贴
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
        private void 高级复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyValues__toLocal();
        }

        private void 高级粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasteValues_fromLocal();
        }
        private void 自动生成所有常量ToolStripMenuItem_Click(object sender, EventArgs e)
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
                MessageBox.Show("文档保存完毕", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }





        #region ActiveItem 成员

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
            dialog.Title = "导出文本文档";
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
            dialog.Title = "导入文本文档";
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

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyValues_toSystem();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
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
            dialog.Title = "导出Excel文档";
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
            dialog.Title = "导入Excel文档";
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