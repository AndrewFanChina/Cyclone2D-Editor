using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.mod;
using System.Drawing.Imaging;
using Cyclone.alg;
using Cyclone.mod.script;
using Cyclone.alg.math;
using Cyclone.alg.type;
using Cyclone.alg.util;

namespace Cyclone.mod.prop
{
    public partial class SmallDialog_PropertyDefine : Form
    {
        private static bool needRefreshUI = false;
        public SmallDialog_PropertyDefine(String text)
        {
            InitializeComponent();
            this.Text = text;
            checkBox_refreshAll.Checked = false;
        }
        //按钮事件响应
        private void button_Cancle_Click(object sender, EventArgs e)
        {
            element = null;
            needRefreshUI = false;
            this.Close();
        }
        //单选按钮事件响应
        private bool noEvent = false;
        //创建单元
        public static PropertyElement element = null;
        private static PropertiesManager manager = null;
        private static SmallDialog_PropertyDefine dialog = null;
        private static void setDialog(String tile)
        {
            if (dialog == null)
            {
                dialog = new SmallDialog_PropertyDefine("配置");
            }
            if (tile != null)
            {
                dialog.Text = tile;
            }
            dialog.checkBox_refreshAll.Checked = false;
        }
        private void setPropsList()
        {
            comboBox_Prop.Items.Clear();
            comboBox_constDef.Items.Clear();
            comboBox_PropDef.Items.Clear();
            if (manager != null && manager.form_main != null && manager.form_main.propertyTypesManager!=null)
            {
                for (int i = 0; i < manager.form_main.propertyTypesManager.getElementCount(); i++)
                {
                    comboBox_Prop.Items.Add(((ObjectElement)manager.form_main.propertyTypesManager.getElement(i)).name);
                }
                for (int i = 0; i < manager.form_main.iDsManager.getElementCount(); i++)
                {
                    comboBox_constDef.Items.Add(((ObjectElement)manager.form_main.iDsManager.getElement(i)).name);
                }
                if (element.ValueType == Consts.PARAM_PROP)
                {
                    noEvent = true;
                    comboBox_Prop.SelectedIndex = element.getPropertyTypeElementUsedID();
                    noEvent = false;
                }
                if (element!=null&&element.defaultValue != null)
                {
                    //默认值
                    if (element.ValueType == Consts.PARAM_INT)
                    {
                        numericUpDown_def.Value = (int)element.defaultValue;
                    }
                    else if (element.ValueType == Consts.PARAM_STR)
                    {
                        textBox_def.Text = (String)element.defaultValue;
                    }
                    else if (element.ValueType == Consts.PARAM_PROP && element.propertyTypeElementUsed != null)
                    {
                        PropertyTypeElement propertyTypeElement = element.propertyTypeElementUsed;
                        for (int i = 0; i < propertyTypeElement.instancesManager.getElementCount(); i++)
                        {
                            comboBox_PropDef.Items.Add(((ObjectElement)propertyTypeElement.instancesManager.getElement(i)).name);
                        }
                        comboBox_PropDef.SelectedIndex = MathUtil.limitNumber((int)element.defaultValue, -1, element.propertyTypeElementUsed.instancesManager.getElementCount() - 1);
                    }
                    else if (element.ValueType == Consts.PARAM_INT_ID)
                    {
                        comboBox_constDef.SelectedIndex = MathUtil.limitNumber((int)element.defaultValue, -1, manager.form_main.iDsManager.getElementCount() - 1);
                    }
                }
            }
        }
        public static PropertyElement createElement(PropertiesManager managerT)
        {
            manager = managerT;
            element = new PropertyElement(manager);
            setDialog("新建属性单元");
            dialog.comboBox_Value.SelectedIndex = 0;
            dialog.setPropsList();
            dialog.ShowDialog();
            return element;
        }
        //设置单元
        public static bool configElement(PropertyElement elementT)
        {
            if (elementT == null)
            {
                Console.WriteLine("error in configElement");
                return false;
            }
            manager = (PropertiesManager)elementT.parent;
            element = elementT;
            setDialog("设置属性单元");
            dialog.textBox_name.Text = element.name;
            dialog.comboBox_Value.SelectedIndex = element.ValueType == Consts.PARAM_INT ? 0 : element.ValueType == Consts.PARAM_STR ? 1 : element.ValueType == Consts.PARAM_PROP ? 2 : 3;
            dialog.setPropsList();
            dialog.ShowDialog();
            return needRefreshUI;
        }

        private static byte[] types = new byte[] { Consts.PARAM_INT, Consts.PARAM_STR, Consts.PARAM_PROP, Consts.PARAM_INT_ID };
        private void button_OK_Click(object sender, EventArgs e)
        {
            if(element!=null)
            {
                for(int i=0;i<manager.getElementCount();i++)
                {
                    PropertyElement elementExist = (PropertyElement)manager.getElement(i);
                    if (elementExist.Equals(element))
                    {
                        continue;
                    }
                    if (elementExist.name.Equals(textBox_name.Text))
                    {
                        MessageBox.Show("存在相同的单元名称!", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                bool needRefreshData = false;
                if (!element.name.Equals(textBox_name.Text))
                {
                    element.name = textBox_name.Text;
                    needRefreshUI = true;
                    if(element.getID()>=0)
                    {
                        ((PropertiesManager)element.parent).parent.instancesManager.configPropertyName(element);
                    }
                }
                if (element.ValueType != types[comboBox_Value.SelectedIndex])
                {
                    element.ValueType = types[comboBox_Value.SelectedIndex];
                    needRefreshUI = true;
                    needRefreshData = true;
                }
                if (element.ValueType == Consts.PARAM_PROP&&((element.propertyTypeElementUsed == null) || (element.propertyTypeElementUsed!=null&&comboBox_Prop.SelectedIndex!=element.propertyTypeElementUsed.getID())))
                {
                    element.setPropertyType(manager.form_main.propertyTypesManager.getElement(comboBox_Prop.SelectedIndex));
                    needRefreshUI = true;
                    needRefreshData = true;
                }
                if (checkBox_refreshAll.Checked)
                {
                    needRefreshUI = true;
                    needRefreshData = true;
                }
                //默认值
                if (element.ValueType == Consts.PARAM_INT)
                {
                    element.defaultValue = (int)numericUpDown_def.Value;
                }
                else if (element.ValueType == Consts.PARAM_STR)
                {
                    element.defaultValue = textBox_def.Text + "";
                }
                else if (element.ValueType == Consts.PARAM_PROP)
                {
                    element.defaultValue = (int)comboBox_PropDef.SelectedIndex;
                }
                else if (element.ValueType == Consts.PARAM_INT_ID)
                {
                    element.defaultValue = (int)comboBox_constDef.SelectedIndex;
                }
                //刷新
                if (needRefreshData && element.getID()>=0)
                {
                    ((PropertiesManager)element.parent).parent.instancesManager.configProperty(element);
                }

            }
            this.Close();

        }

        private void comboBox_Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex == 2)
            {
                this.comboBox_Prop.Enabled = true;
            }
            else
            {
                this.comboBox_Prop.Enabled = false;
            }
            //默认值组件
            if (((ComboBox)sender).SelectedIndex == 0)
            {
                this.numericUpDown_def.Enabled = true;
            }
            else
            {
                this.numericUpDown_def.Enabled = false;
            }
            if (((ComboBox)sender).SelectedIndex == 1)
            {
                this.textBox_def.Enabled = true;
            }
            else
            {
                this.textBox_def.Enabled = false;
            }
            if (((ComboBox)sender).SelectedIndex == 2)
            {
                this.comboBox_PropDef.Enabled = true;
            }
            else
            {
                this.comboBox_PropDef.Enabled = false;
            }
            if (((ComboBox)sender).SelectedIndex == 3)
            {
                this.comboBox_constDef.Enabled = true;
            }
            else
            {
                this.comboBox_constDef.Enabled = false;
            }
        }

        private void button_clearPropID_Click(object sender, EventArgs e)
        {
            if (comboBox_Prop.Enabled)
            {
                comboBox_Prop.SelectedIndex = -1;
            }
        }

        private void comboBox_Prop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            comboBox_PropDef.Items.Clear();
            if (manager != null && manager.form_main != null && manager.form_main.propertyTypesManager != null)
            {
                int index = comboBox_Prop.SelectedIndex;
                if (index >= 0)
                {
                    PropertyTypeElement propertyTypeElement = (PropertyTypeElement)manager.form_main.propertyTypesManager.getElement(index);
                    for (int i = 0; i < propertyTypeElement.instancesManager.getElementCount(); i++)
                    {
                        comboBox_PropDef.Items.Add(((ObjectElement)propertyTypeElement.instancesManager.getElement(i)).name);
                    }
                }

            }
        }

        private void button_clearPropIDDef_Click(object sender, EventArgs e)
        {
            if (comboBox_PropDef.Enabled)
            {
                comboBox_PropDef.SelectedIndex = -1;
            }
        }

    }
}