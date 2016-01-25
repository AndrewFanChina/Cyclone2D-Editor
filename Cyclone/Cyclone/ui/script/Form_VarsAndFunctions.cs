using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.mod;
using Cyclone.alg;
using System.IO;
using System.Collections;
using Cyclone.mod.script;
using Cyclone.mod.util;
using Cyclone.alg.util;

namespace Cyclone.ui_script
{
    public partial class Form_VarsAndFunctions : Form
    {

        private Form_Main form_main = null;

        public Form_VarsAndFunctions(Form_Main form_mainT)
        {
            InitializeComponent();
            form_main = form_mainT;

            initParams();
        }
        //���ӵ�Ԫ
        private void addElement()
        {
            if (tabControl_Lists.SelectedIndex==0)//�������͵�Ԫ
            {
                VarElement element = SmallDialog_NewVar_INT.createElement(form_main.varIntManager);
                if (element != null)
                {
                    form_main.varIntManager.addElement(element);
                }
            }
            if (tabControl_Lists.SelectedIndex == 1)//�����ַ���Ԫ
            {
                VarElement element = SmallDialog_NewVar_String.createElement(form_main.varStringManager);
                if (element != null)
                {
                    form_main.varStringManager.addElement(element);
                }
            }
            if (tabControl_Lists.SelectedIndex == 2)//���Ӵ�����������Ԫ
            {
                FunctionElement element = SmallDialog_FunctionsConfig.createElement(form_main.triggerFunctionManager, "�½�����������Ԫ");
                if (element != null)
                {
                    form_main.triggerFunctionManager.addElement(element);
                }
            }
            if (tabControl_Lists.SelectedIndex == 3)//���ӻ���������Ԫ
            {
                FunctionElement element = SmallDialog_FunctionsConfig.createElement(form_main.contextFunctionManager, "�½�����������Ԫ");
                if (element != null)
                {
                    form_main.contextFunctionManager.addElement(element);
                }
            }
            if (tabControl_Lists.SelectedIndex == 4)//����ִ�к�����Ԫ
            {
                FunctionElement element = SmallDialog_FunctionsConfig.createElement(form_main.executionFunctionManager, "�½�ִ�к�����Ԫ");
                if (element != null)
                {
                    form_main.executionFunctionManager.addElement(element);
                }
            }
        }
        //���ñ���
        private void configElement()
        {
            if (tabControl_Lists.SelectedIndex == 0)//�������͵�Ԫ
            {
                if (listBox_VarInt.SelectedIndex >= 0)
                {
                    SmallDialog_NewVar_INT.configElement((VarElement)form_main.varIntManager.getElement(listBox_VarInt.SelectedIndex));
                    form_main.varIntManager.refreshUI_Element(listBox_VarInt.SelectedIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 1)//�����ַ���Ԫ
            {
                if (listBox_VarString.SelectedIndex >= 0)
                {
                    SmallDialog_NewVar_String.configElement((VarElement)form_main.varStringManager.getElement(listBox_VarString.SelectedIndex));
                    form_main.varStringManager.refreshUI_Element(listBox_VarString.SelectedIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 2)//���ô���������Ԫ
            {
                if (listBox_Trigger.SelectedIndex >= 0)
                {
                    SmallDialog_FunctionsConfig.configElement((FunctionElement)form_main.triggerFunctionManager.getElement(listBox_Trigger.SelectedIndex), "���ô���������Ԫ");
                    form_main.triggerFunctionManager.refreshUI_Element(listBox_Trigger.SelectedIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 3)//���û���������Ԫ
            {
                if (listBox_Condition.SelectedIndex >= 0)
                {
                    SmallDialog_FunctionsConfig.configElement((FunctionElement)form_main.contextFunctionManager.getElement(listBox_Condition.SelectedIndex), "���û���������Ԫ");
                    form_main.contextFunctionManager.refreshUI_Element(listBox_Condition.SelectedIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 4)//����ִ�к�����Ԫ
            {
                if (listBox_Execution.SelectedIndex >= 0)
                {
                    SmallDialog_FunctionsConfig.configElement((FunctionElement)form_main.executionFunctionManager.getElement(listBox_Execution.SelectedIndex), "����ִ�к�����Ԫ");
                    form_main.executionFunctionManager.refreshUI_Element(listBox_Execution.SelectedIndex);
                }
            }
        }
        //ɾ������
        private void deleteElement()
        {
            if (tabControl_Lists.SelectedIndex == 0)//ɾ�����͵�Ԫ
            {
                int currentIndex = listBox_VarInt.SelectedIndex;
                if (currentIndex >= 0)
                {
                    VarElement element = (VarElement)form_main.varIntManager.getElement(currentIndex);
                    if (element == null)
                    {
                        return;
                    }
                    int usedTime=element.getUsedTime();
                    if (usedTime > 0)
                    {
                        MessageBox.Show("������������" + usedTime + "�Σ�����ɾ��");
                        return;
                    }
                    form_main.varIntManager.removeElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 1)//ɾ���ַ���Ԫ
            {
                int currentIndex = listBox_VarString.SelectedIndex;
                if (currentIndex >= 0)
                {
                    VarElement element = (VarElement)form_main.varStringManager.getElement(currentIndex);
                    if (element == null)
                    {
                        return;
                    }
                    int usedTime = element.getUsedTime();
                    if (usedTime > 0)
                    {
                        MessageBox.Show("������������" + usedTime + "�Σ�����ɾ��");
                        return;
                    }
                    form_main.varStringManager.removeElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 2)//ɾ��������������Ԫ
            {
                int currentIndex = listBox_Trigger.SelectedIndex;
                if (currentIndex >= 0)
                {
                    FunctionElement element = (FunctionElement)form_main.triggerFunctionManager.getElement(currentIndex);
                    if (element == null)
                    {
                        return;
                    }
                    int usedTime = element.getUsedTime();
                    if (usedTime > 0)
                    {
                        MessageBox.Show("������������" + usedTime + "�Σ�����ɾ��");
                        return;
                    }
                    form_main.triggerFunctionManager.removeElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 3)//ɾ�������жϺ�����Ԫ
            {
                int currentIndex = listBox_Condition.SelectedIndex;
                if (currentIndex >= 0)
                {
                    FunctionElement element = (FunctionElement)form_main.contextFunctionManager.getElement(currentIndex);
                    if (element == null)
                    {
                        return;
                    }
                    int usedTime = element.getUsedTime();
                    if (usedTime > 0)
                    {
                        MessageBox.Show("������������" + usedTime + "�Σ�����ɾ��");
                        return;
                    }
                    form_main.contextFunctionManager.removeElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 4)//ɾ��ִ�к�����Ԫ
            {
                int currentIndex = listBox_Execution.SelectedIndex;
                if (currentIndex >= 0)
                {
                    FunctionElement element = (FunctionElement)form_main.executionFunctionManager.getElement(currentIndex);
                    if (element == null)
                    {
                        return;
                    }
                    int usedTime = element.getUsedTime();
                    if (usedTime > 0)
                    {
                        MessageBox.Show("������������" + usedTime + "�Σ�����ɾ��");
                        return;
                    }
                    form_main.executionFunctionManager.removeElement(currentIndex);
                }
            }
        }
        //������
        private void checkElement()
        {
            if (tabControl_Lists.SelectedIndex == 0)//������͵�Ԫ
            {
                int currentIndex = listBox_VarInt.SelectedIndex;
                if (currentIndex >= 0)
                {
                    VarElement element = (VarElement)form_main.varIntManager.getElement(currentIndex);
                    if (element == null)
                    {
                        return;
                    }
                    //String usedInfor = element.getUsedInfor();
                    //SmallDialog_ShowString.showString("���ͱ������", usedInfor);
                    SmallDialog_ShowList.showList(element.getUsedMeory());
                }
            }
            if (tabControl_Lists.SelectedIndex == 1)//����ַ���Ԫ
            {
                int currentIndex = listBox_VarString.SelectedIndex;
                if (currentIndex >= 0)
                {
                    VarElement element = (VarElement)form_main.varStringManager.getElement(currentIndex);
                    if (element == null)
                    {
                        return;
                    }
                    //String usedInfor = element.getUsedInfor();
                    //SmallDialog_ShowString.showString("�ַ��������", usedInfor);
                    SmallDialog_ShowList.showList(element.getUsedMeory());
                }
            }
            if (tabControl_Lists.SelectedIndex == 2)//��鴥��������Ԫ
            {
                int currentIndex = listBox_Trigger.SelectedIndex;
                if (currentIndex >= 0)
                {
                    FunctionElement element = (FunctionElement)form_main.triggerFunctionManager.getElement(currentIndex);
                    if (element == null)
                    {
                        return;
                    }
                    //String usedInfor = element.getUsedInfor();
                    //SmallDialog_ShowString.showString("�������������", usedInfor);
                    SmallDialog_ShowList.showList(element.getUsedMeory());
                }
            }
            if (tabControl_Lists.SelectedIndex == 3)//��黷��������Ԫ
            {
                int currentIndex = listBox_Condition.SelectedIndex;
                if (currentIndex >= 0)
                {
                    FunctionElement element = (FunctionElement)form_main.contextFunctionManager.getElement(currentIndex);
                    if (element == null)
                    {
                        return;
                    }
                    //String usedInfor = element.getUsedInfor();
                    //SmallDialog_ShowString.showString("�����������", usedInfor);
                    SmallDialog_ShowList.showList(element.getUsedMeory());
                }
            }
            if (tabControl_Lists.SelectedIndex == 4)//���ִ�к�����Ԫ
            {
                int currentIndex = listBox_Execution.SelectedIndex;
                if (currentIndex >= 0)
                {
                    FunctionElement element = (FunctionElement)form_main.executionFunctionManager.getElement(currentIndex);
                    if (element == null)
                    {
                        return;
                    }
                    //String usedInfor = element.getUsedInfor();
                    //SmallDialog_ShowString.showString("ִ�к������", usedInfor);
                    SmallDialog_ShowList.showList(element.getUsedMeory());
                }
            }
        }
        //�����ƶ���Ԫ
        private void moveUpElement()
        {
            if (tabControl_Lists.SelectedIndex == 0)//���͵�Ԫ
            {
                int currentIndex = listBox_VarInt.SelectedIndex;
                if (currentIndex >= 0)
                {
                    form_main.varIntManager.moveUpElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 1)//�ַ���Ԫ
            {
                int currentIndex = listBox_VarString.SelectedIndex;
                if (currentIndex >= 0)
                {
                    form_main.varStringManager.moveUpElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 2)//������������Ԫ
            {
                int currentIndex = listBox_Trigger.SelectedIndex;
                if (currentIndex >= 0)
                {
                    form_main.triggerFunctionManager.moveUpElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 3)//����������Ԫ
            {
                int currentIndex = listBox_Condition.SelectedIndex;
                if (currentIndex >= 0)
                {
                    form_main.contextFunctionManager.moveUpElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 4)//ִ�к�����Ԫ
            {
                int currentIndex = listBox_Execution.SelectedIndex;
                if (currentIndex >= 0)
                {
                    form_main.executionFunctionManager.moveUpElement(currentIndex);
                }
            }
        }
        //�����ƶ���Ԫ
        private void moveDownElement()
        {
            if (tabControl_Lists.SelectedIndex == 0)//���͵�Ԫ
            {
                int currentIndex = listBox_VarInt.SelectedIndex;
                if (currentIndex >= 0)
                {
                    form_main.varIntManager.moveDownElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 1)//�ַ���Ԫ
            {
                int currentIndex = listBox_VarString.SelectedIndex;
                if (currentIndex >= 0)
                {
                    form_main.varStringManager.moveDownElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 2)//������������Ԫ
            {
                int currentIndex = listBox_Trigger.SelectedIndex;
                if (currentIndex >= 0)
                {
                    form_main.triggerFunctionManager.moveDownElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 3)//����������Ԫ
            {
                int currentIndex = listBox_Condition.SelectedIndex;
                if (currentIndex >= 0)
                {
                    form_main.contextFunctionManager.moveDownElement(currentIndex);
                }
            }
            if (tabControl_Lists.SelectedIndex == 4)//ִ�к�����Ԫ
            {
                int currentIndex = listBox_Execution.SelectedIndex;
                if (currentIndex >= 0)
                {
                    form_main.executionFunctionManager.moveDownElement(currentIndex);
                }
            }
        }
        //����
        private void orderElements()
        {
            if (tabControl_Lists.SelectedIndex == 0)//���͵�Ԫ
            {
                form_main.varIntManager.orderByname();
            }
            if (tabControl_Lists.SelectedIndex == 1)//�ַ���Ԫ
            {
                form_main.varStringManager.orderByname();
            }
            if (tabControl_Lists.SelectedIndex == 2)//������������Ԫ
            {
                form_main.triggerFunctionManager.orderByname();
            }
            if (tabControl_Lists.SelectedIndex == 3)//����������Ԫ
            {
                form_main.contextFunctionManager.orderByname();
            }
            if (tabControl_Lists.SelectedIndex == 4)//ִ�к�����Ԫ
            {
                form_main.executionFunctionManager.orderByname();
            }
        }
        public void initParams()
        {
            if (form_main.varIntManager == null || form_main.varStringManager == null)
            {
                return;
            }
            if (form_main.varIntManager.listBox == null || !form_main.varIntManager.listBox.Equals(listBox_VarInt))
            {
                form_main.varIntManager.refreshUI(listBox_VarInt);
            }
            if (form_main.varStringManager.listBox == null || !form_main.varStringManager.listBox.Equals(listBox_VarString))
            {
                form_main.varStringManager.refreshUI(listBox_VarString);
            }
            if (form_main.triggerFunctionManager.listBox == null || !form_main.triggerFunctionManager.listBox.Equals(listBox_Trigger))
            {
                form_main.triggerFunctionManager.refreshUI(listBox_Trigger);
            }
            if (form_main.contextFunctionManager.listBox == null || !form_main.contextFunctionManager.listBox.Equals(listBox_Condition))
            {
                form_main.contextFunctionManager.refreshUI(listBox_Condition);
            }
            if (form_main.executionFunctionManager.listBox == null || !form_main.executionFunctionManager.listBox.Equals(listBox_Execution))
            {
                form_main.executionFunctionManager.refreshUI(listBox_Execution);
            }
        }
        public void releaseRes()
        {
 
        }

        private void button_AddElement_Click(object sender, EventArgs e)
        {
            addElement();
        }

        private void listBox_VarInt_DoubleClick(object sender, EventArgs e)
        {
            configElement();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_ActionFrame_Del_Click(object sender, EventArgs e)
        {
            deleteElement();
        }


        private void listBox_VarInt_KeyDown(object sender, KeyEventArgs e)
        {
            CommonKeyDown(e);
        }

        private void listBox_VarString_DoubleClick(object sender, EventArgs e)
        {
            configElement();
        }

        private void listBox_VarString_KeyDown(object sender, KeyEventArgs e)
        {
            CommonKeyDown(e);
        }

        private void CommonKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.Up)
                {
                    moveUpElement();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    moveDownElement();
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
                this.deleteElement();
            }
        }

        private void listBox_Trigger_KeyDown(object sender, KeyEventArgs e)
        {
            CommonKeyDown(e);
        }

        private void listBox_Trigger_DoubleClick(object sender, EventArgs e)
        {
            configElement();
        }

        private void listBox_Condition_DoubleClick(object sender, EventArgs e)
        {
            configElement();
        }

        private void listBox_Condition_KeyDown(object sender, KeyEventArgs e)
        {
            CommonKeyDown(e);
        }

        private void listBox_Execution_KeyDown(object sender, KeyEventArgs e)
        {
            CommonKeyDown(e);
        }


        private void listBox_Execution_DoubleClick(object sender, EventArgs e)
        {
            configElement();
        }

        private void button_Check_Click(object sender, EventArgs e)
        {
            checkElement();
        }

        private void button_moveUp_Click(object sender, EventArgs e)
        {
            moveUpElement();
        }

        private void button_moveDown_Click(object sender, EventArgs e)
        {
            moveDownElement();
        }

        private void button_order_Click(object sender, EventArgs e)
        {
            orderElements();
        }
        //��ʾ�к�
        private void showLineNumber()
        {
            ListBox listBoxTemp = null;
            if (tabControl_Lists.SelectedIndex == 0)//���͵�Ԫ
            {
                listBoxTemp = listBox_VarInt;
            }
            if (tabControl_Lists.SelectedIndex == 1)//�ַ���Ԫ
            {
                listBoxTemp = listBox_VarString;
            }
            if (tabControl_Lists.SelectedIndex == 2)//������������Ԫ
            {
                listBoxTemp = listBox_Trigger;
            }
            if (tabControl_Lists.SelectedIndex == 3)//����������Ԫ
            {
                listBoxTemp = listBox_Condition;
            }
            if (tabControl_Lists.SelectedIndex == 4)//ִ�к�����Ԫ
            {
                listBoxTemp = listBox_Execution;
            }
            if (listBoxTemp != null)
            {
                label_lineNumber.Text = "��ǰ�к�:" + listBoxTemp.SelectedIndex;
            }
        }
        private void listBox_common_SelectedIndexChanged(object sender, EventArgs e)
        {
            showLineNumber();
        }

        private void Form_VarsAndFunctions_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Form_VarsAndFunctions_Activated(object sender, EventArgs e)
        {
        }

        private void button_genHead_Click(object sender, EventArgs e)
        {
            generateHeadFile();
        }

        //����ͷ�ļ�
        public void generateHeadFile()
        {
            checkPath();
            try
            {
                String path = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_KSS + "Globle.kss";
                FileStream fs = new FileStream(path, FileMode.Create);
                //��������ID
                for (int i = 0; i < form_main.varIntManager.getElementCount(); i++)
                {
                    IOUtil.writeTextLine(fs, "#define " + ((VarElement)form_main.varIntManager.getElement(i)).name + " " + i);
                }
                for (int i = 0; i < form_main.varStringManager.getElementCount(); i++)
                {
                    IOUtil.writeTextLine(fs, "#define " + ((VarElement)form_main.varStringManager.getElement(i)).name + " " + i);
                }
                //����������
                genFunctionToHead(fs, form_main.triggerFunctionManager);
                genFunctionToHead(fs, form_main.contextFunctionManager);
                genFunctionToHead(fs, form_main.executionFunctionManager);
                //����������
                form_main.form_ProptiesManager.exportToHereadFile(fs);
                fs.Flush();
                fs.Close();
                SmallDialog_ShowMessage.showMessage("����","ͷ�ļ�������ϣ�����ڡ�" + path + "��");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //���ɺ�����
        private void genFunctionToHead(FileStream fs, FunctionsManager functionManager)
        {
            for (int i = 0; i < functionManager.getElementCount(); i++)
            {
                FunctionElement fun = (FunctionElement)functionManager.getElement(i);
                String strParams = " (";
                ArrayList paramList = (ArrayList)fun.getValue();
                for (int j = 0; j < paramList.Count; j++)
                {
                    strParams += "p" + j;
                    if (j < paramList.Count - 1)
                    {
                        strParams += ",";
                    }
                }
                strParams += ");";
                IOUtil.writeTextLine(fs, "host " + fun.name + strParams);
            }
        }
        //������ļ���
        private void checkPath()
        {
            String subFolderName = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_KSS;
            if (!Directory.Exists(subFolderName))
            {
                Directory.CreateDirectory(subFolderName);
            }
        }

    }
}