using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.alg;
using Cyclone.alg.type;
using Cyclone.alg.util;

namespace Cyclone.mod.prop
{
    public partial class SmallDialog_PropertyValue : Form
    {
        private static Object objValue = null;
        private static ObjectVector vectorList = null;
        private static SmallDialog_PropertyValue smallDialog_PropertyValue = null;
        private static byte valueType = Consts.PARAM_INT;
        public SmallDialog_PropertyValue(String title)
        {
            InitializeComponent();
            this.Text = title;
        }
        public Object getValue()
        {
            return objValue;
        }
        private void setValueType(byte valueTypeT,Object defValue)
        {
            valueType = valueTypeT;
            textBox_value.Text = "";
            textBox_value.Enabled = false;
            numericUpDown_value.Value = 0;
            numericUpDown_value.Enabled = false;
            comboBox_value.Items.Clear();
            comboBox_value.SelectedIndex = -1;
            comboBox_value.Enabled = false;
            switch (valueType)
            {
                case Consts.PARAM_INT:
                    numericUpDown_value.Enabled = true;
                    if (defValue != null)
                    {
                        try
                        {
                            int value = Convert.ToInt32(defValue);
                            numericUpDown_value.Value = value;
                        }
                        catch (System.Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    break;
                case Consts.PARAM_STR:
                    textBox_value.Enabled = true;
                    if (defValue != null)
                    {
                        textBox_value.Text = (String)defValue;
                    }
                    break;
                case Consts.PARAM_PROP:
                    comboBox_value.Enabled = true;
                    if (vectorList != null)
                    {
                        for (int i = 0; i < vectorList.getElementCount(); i++)
                        {
                            comboBox_value.Items.Add(((ObjectElement)vectorList.getElement(i)).name);
                        }
                        if (defValue != null && defValue is ObjectElement)
                        {
                            comboBox_value.SelectedIndex = ((ObjectElement)defValue).getID();
                        }

                    }
                    break;
                case Consts.PARAM_INT_ID:
                    comboBox_value.Enabled = true;
                    if (vectorList != null)
                    {
                        for (int i = 0; i < vectorList.getElementCount(); i++)
                        {
                            comboBox_value.Items.Add(((ObjectElement)vectorList.getElement(i)).name);
                        }
                        if (defValue != null && defValue is ObjectElement)
                        {
                            comboBox_value.SelectedIndex = ((ObjectElement)defValue).getID();
                        }
                    }
                    break;
            }
        }
        public static Object getValue(String title, Object defValue, byte valueType, ObjectVector vectorListT)
        {
            vectorList = vectorListT;
            if (smallDialog_PropertyValue == null)
            {
                smallDialog_PropertyValue = new SmallDialog_PropertyValue(title);
            }
            else
            {
                smallDialog_PropertyValue.Text = title;
            }
            smallDialog_PropertyValue.setValueType(valueType, defValue);
            smallDialog_PropertyValue.ShowDialog();
            return smallDialog_PropertyValue.getValue();
        }
        private void button_Ok_Click(object sender, EventArgs e)
        {
            okEvent();
        }

        private void Form_TextDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Enter)
            {
                okEvent();
            }
        }
        private void okEvent()
        {
            if (numericUpDown_value.Enabled)
            {
                objValue = numericUpDown_value.Value;
            }
            else if (textBox_value.Enabled)
            {
                objValue = textBox_value.Text;
            }
            else if (comboBox_value.Enabled)
            {
                if (vectorList != null)
                {
                    objValue = vectorList.getElement(comboBox_value.SelectedIndex);
                }
            }
            this.Close();
        }

        private void button_clearPropID_Click(object sender, EventArgs e)
        {
            if (comboBox_value.Enabled)
            {
                comboBox_value.SelectedIndex = -1;
            }
        }
    }
}