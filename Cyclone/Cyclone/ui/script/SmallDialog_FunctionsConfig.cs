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
using System.Collections;
using Cyclone.mod.script;
using Cyclone.mod.util;
using Cyclone.alg.util;

namespace Cyclone.ui_script
{
    public partial class SmallDialog_FunctionsConfig : Form
    {
        public SmallDialog_FunctionsConfig(String text)
        {
            InitializeComponent();
            this.Text = text;
        }
        private const int LINE_WIDTH=188;
        private const int LINE_WIDTH_VALUE = 268;
        private const int LINE_HEIGHT = 21;
        //按钮事件响应
        private void button_Cancle_Click(object sender, EventArgs e)
        {
            element = null;
            this.Close();
        }
        //单选按钮事件响应
        private bool noEvent = false;
        //获得地图物理单元
        public static FunctionElement element = null;
        private static FunctionsManager manager = null;
        private static int beUsedTime = 0;
        public static FunctionElement createElement(FunctionsManager managerT,String title)
        {
            manager = managerT;
            element = new FunctionElement(manager);
            SmallDialog_FunctionsConfig dialog = new SmallDialog_FunctionsConfig(title);
            dialog.ShowDialog();
            return element;
        }
        //设置地图单元
        public static void configElement(FunctionElement elementT, String title)
        {
            if (elementT == null)
            {
                Console.WriteLine("error in configElement");
                return;
            }
            manager = (FunctionsManager)elementT.parent;
            element = elementT;
            SmallDialog_FunctionsConfig dialog = new SmallDialog_FunctionsConfig(title);
            dialog.textBox_name.Text = element.name;
            dialog.textBox_Commet.Text = element.commet;
            dialog.showParamsList();
            dialog.ShowDialog();
        }
        //配置时显示参数列表
        //ArrayList paramsBoxs = new ArrayList();
        private void showParamsList()
        {
            noEvent = true;
            FLP_params.Controls.Clear();
            FLP_defValue.Controls.Clear();
            FLP_Modify.Controls.Clear();
            //paramsBoxs.Clear();
            if (element != null)
            {
                beUsedTime = element.getUsedTime();
                textBox_used.Text = beUsedTime+"";
                textBox_used.Enabled = false;
                Object paramsObj = element.getValue();
                if (paramsObj != null)
                {
                    ArrayList paramsArray = (ArrayList)paramsObj;
                    numericUpDown_Value.Value = paramsArray.Count;
                    if (beUsedTime > 0 && !checkBox_Modify.Checked)
                    {
                        numericUpDown_Value.Enabled = false;
                    }
                    else
                    {
                        numericUpDown_Value.Enabled = true;
                    }
                    for (int i = 0; i < paramsArray.Count; i++)
                    {
                        generateParamUI(paramsArray, i);
                    }
                }
            }
            noEvent = false;
        }
        //根据参数生成UI
        private void generateParamUI(ArrayList paramsArray, int i)
        {
            //添加参数i
            ComboBox boxI = new ComboBox();
            boxI.DropDownStyle = ComboBoxStyle.DropDownList;
            for (byte j = Consts.PARAM_INT; j <= Consts.PARAM_INT_ID; j++)
            {
                boxI.Items.Add(Consts.getParamType(j));
            }
            boxI.Width = LINE_WIDTH;
            boxI.Height = LINE_HEIGHT;
            boxI.Margin = new Padding(2);
            FLP_params.Controls.Add(boxI);
            int type = Consts.PARAM_INT;
            if (i>=0 && i < paramsArray.Count)
            {
                boxI.SelectedIndex = (byte)paramsArray[i];
                type = ((byte)paramsArray[i]) + Consts.PARAM_INT;
            }
            else
            {
                boxI.SelectedIndex = 0;
            }
            boxI.SelectedIndexChanged+=new EventHandler(comboBox_I_selectedIndexChanaged);

            if (beUsedTime > 0 && !checkBox_Modify.Checked)
            {
                boxI.Enabled = false;
            }
            //添加参数默认值i
            switch (type)
            {
                case Consts.PARAM_INT:
                    NumericUpDown numericBoxI = new NumericUpDown();
                    numericBoxI.Width = LINE_WIDTH_VALUE;
                    numericBoxI.Height = LINE_HEIGHT;
                    numericBoxI.Margin = new Padding(2,1,2,2);
                    //numericBoxI.
                    FLP_defValue.Controls.Add(numericBoxI);
                    numericBoxI.Maximum = short.MaxValue;
                    numericBoxI.Minimum = short.MinValue;
                    numericBoxI.Value = 0;
                    if (beUsedTime == 0 || (beUsedTime > 0 && !checkBox_Modify.Checked))
                    {
                        numericBoxI.Enabled = false;
                    }
                    break;
                case Consts.PARAM_STR:
                case Consts.PARAM_INT_ID:
                case Consts.PARAM_INT_VAR:
                case Consts.PARAM_STR_VAR:
                    ComboBox comboBoxI = new ComboBox();
                    comboBoxI.Width = LINE_WIDTH_VALUE;
                    comboBoxI.Height = LINE_HEIGHT;
                    comboBoxI.Margin = new Padding(2);
                    comboBoxI.DropDownStyle = ComboBoxStyle.DropDownList;
                    if (type == Consts.PARAM_INT_VAR)
                    {
                        for (int j = 0; j < manager.form_main.varIntManager.getElementCount(); j++)
                        {
                            VarElement var = (VarElement)manager.form_main.varIntManager.getElement(j);
                            comboBoxI.Items.Add(var.name);
                        }

                    }
                    else if (type == Consts.PARAM_STR_VAR)
                    {
                        for (int j = 0; j < manager.form_main.varStringManager.getElementCount(); j++)
                        {
                            VarElement var = (VarElement)manager.form_main.varStringManager.getElement(j);
                            comboBoxI.Items.Add(var.name);
                        }
                    }
                    FLP_defValue.Controls.Add(comboBoxI);
                    if (beUsedTime==0 || (beUsedTime > 0 && !checkBox_Modify.Checked))
                    {
                        comboBoxI.Enabled = false;
                    }
                    break;

            }
            //添加自动刷新标志i
            CheckBox checkBoxI = new CheckBox();
            checkBoxI.Text = "置为默认值";
            checkBoxI.Height = LINE_HEIGHT;
            checkBoxI.Margin = new Padding(2,1,2,2);
            if (beUsedTime == 0 || (beUsedTime > 0 && !checkBox_Modify.Checked))
            {
                checkBoxI.Enabled = false;
            }
            FLP_Modify.Controls.Add(checkBoxI);
        }
        //改变参数类型
        public void changeParamType(int index,int type)
        {
            if (index < 0 || index >= FLP_defValue.Controls.Count)
            {
                return;
            }
            ArrayList array=new ArrayList();
            for(int i=0;i<FLP_defValue.Controls.Count;i++)
            {
                array.Add(FLP_defValue.Controls[i]);
            }
            //添加参数默认值i
            switch (type)
            {
                case Consts.PARAM_INT:
                    NumericUpDown numericBoxI = new NumericUpDown();
                    numericBoxI.Width = LINE_WIDTH_VALUE;
                    numericBoxI.Height = LINE_HEIGHT;
                    numericBoxI.Margin = new Padding(2, 1, 2, 2);
                    array[index]=numericBoxI;
                    numericBoxI.Maximum = short.MaxValue;
                    numericBoxI.Minimum = short.MinValue;
                    numericBoxI.Value = 0;
                    if (beUsedTime == 0 || (beUsedTime > 0 && !checkBox_Modify.Checked))
                    {
                        numericBoxI.Enabled = false;
                    }
                    break;
                case Consts.PARAM_STR:
                case Consts.PARAM_INT_ID:
                case Consts.PARAM_INT_VAR:
                case Consts.PARAM_STR_VAR:
                    ComboBox comboBoxI = new ComboBox();
                    comboBoxI.Width = LINE_WIDTH_VALUE;
                    comboBoxI.Height = LINE_HEIGHT;
                    comboBoxI.Margin = new Padding(2);
                    comboBoxI.DropDownStyle = ComboBoxStyle.DropDownList;
                    if (type == Consts.PARAM_INT_VAR)
                    {
                        for (int j = 0; j < manager.form_main.varIntManager.getElementCount(); j++)
                        {
                            VarElement var = (VarElement)manager.form_main.varIntManager.getElement(j);
                            comboBoxI.Items.Add(var.name);
                        }

                    }
                    else if (type == Consts.PARAM_STR_VAR)
                    {
                        for (int j = 0; j < manager.form_main.varStringManager.getElementCount(); j++)
                        {
                            VarElement var = (VarElement)manager.form_main.varStringManager.getElement(j);
                            comboBoxI.Items.Add(var.name);
                        }
                    }
                    array[index] = comboBoxI;
                    if (beUsedTime == 0 || (beUsedTime > 0 && !checkBox_Modify.Checked))
                    {
                        comboBoxI.Enabled = false;
                    }
                    break;

            }
            FLP_defValue.Controls.Clear();
            for (int i = 0; i < array.Count; i++)
            {
                FLP_defValue.Controls.Add((Control)array[i]);
            }
        }
        //创建时改变参数列表
        private void modifyParamsList()
        {
            if (element != null)
            {
                Object paramsObj = element.getValue();
                if (paramsObj != null)
                {
                    ArrayList paramsArray = (ArrayList)paramsObj;
                    //增加
                    for (int i = FLP_params.Controls.Count; i < (int)numericUpDown_Value.Value; i++)
                    {
                        generateParamUI(paramsArray, i);
                    }
                    //减少
                    while (FLP_params.Controls.Count > (int)numericUpDown_Value.Value)
                    {
                        FLP_params.Controls.RemoveAt(FLP_params.Controls.Count - 1);
                        FLP_defValue.Controls.RemoveAt(FLP_defValue.Controls.Count - 1);
                        FLP_Modify.Controls.RemoveAt(FLP_Modify.Controls.Count - 1);
                    }
                }
            }
        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            if(element!=null)
            {
                for(int i=0;i<manager.getElementCount();i++)
                {
                    FunctionElement elementExist = (FunctionElement)manager.getElement(i);
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
                element.name = textBox_name.Text;
                element.commet = textBox_Commet.Text;
                ArrayList paramsList = new ArrayList();
                ArrayList paramsValue = new ArrayList();
                ArrayList paramsValueRest = new ArrayList();
                for (int i = 0; i < (int)numericUpDown_Value.Value; i++)
                {
                    paramsList.Add((byte)(((ComboBox)FLP_params.Controls[i]).SelectedIndex));
                }
                for (int i = 0; i < (int)numericUpDown_Value.Value; i++)
                {
                    if (FLP_defValue.Controls[i] is NumericUpDown)
                    {
                        paramsValue.Add((int)(((NumericUpDown)FLP_defValue.Controls[i]).Value));
                    }
                    else if (FLP_defValue.Controls[i] is ComboBox)
                    {
                        paramsValue.Add((int)(((ComboBox)FLP_defValue.Controls[i]).SelectedIndex));
                    }
                    else
                    {
                        paramsValue.Add(0);
                        MessageBox.Show("格式错误"); 
                    }
                }
                for (int i = 0; i < (int)numericUpDown_Value.Value; i++)
                {
                    paramsValueRest.Add((bool)(((CheckBox)FLP_Modify.Controls[i]).Checked));
                }
                if(beUsedTime == 0)
                {
                    element.setValue(paramsList);
                }
                else if (beUsedTime > 0)
                {
                    if (checkBox_Modify.Checked)
                    {
                        element.configValue(paramsList, paramsValue, paramsValueRest);
                    }
                }

            }
            this.Close();

        }

        private void numericUpDown_Value_ValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            modifyParamsList();
        }

        private void checkBox_Modify_CheckedChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            showParamsList();
        }
        private void comboBox_I_selectedIndexChanaged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            ComboBox combBox = (ComboBox)sender;
            int index = combBox.Parent.Controls.IndexOf(combBox);
            changeParamType(index, combBox.SelectedIndex+Consts.PARAM_INT);
        }

        private void button_checkUsed_Click(object sender, EventArgs e)
        {
            SmallDialog_ShowList.showList(element.getUsedMeory());
        }
    }
}