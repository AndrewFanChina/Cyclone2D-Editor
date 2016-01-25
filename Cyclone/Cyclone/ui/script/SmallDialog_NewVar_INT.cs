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
using Cyclone.alg.util;

namespace Cyclone.ui_script
{
    public partial class SmallDialog_NewVar_INT : Form
    {
        public SmallDialog_NewVar_INT(String text)
        {
            InitializeComponent();
            this.Text = text;
        }
        public void initDialog()
        {
        }
        //��ť�¼���Ӧ
        private void button_Cancle_Click(object sender, EventArgs e)
        {
            element = null;
            this.Close();
        }
        //��ѡ��ť�¼���Ӧ
        //private bool noEvent = false;
        //��õ�ͼ����Ԫ
        public static VarElement element = null;
        private static VarsManager manager = null;
        public static VarElement createElement(VarsManager managerT)
        {
            manager = managerT;
            element = new VarElement(manager, Consts.PARAM_INT);
            SmallDialog_NewVar_INT dialog = new SmallDialog_NewVar_INT("�½����ͱ���");
            dialog.ShowDialog();
            return element;
        }
        //���õ�ͼ��Ԫ
        public static void configElement(VarElement elementT)
        {
            if (elementT == null)
            {
                Console.WriteLine("error in configElement");
                return;
            }
            manager = (VarsManager)elementT.parent;
            element = elementT;
            SmallDialog_NewVar_INT dialog = new SmallDialog_NewVar_INT("�������ͱ���");
            dialog.textBox_name.Text = element.name;
            dialog.numericUpDown_Value.Value = (int)element.getValue();
            dialog.ShowDialog();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if(element!=null)
            {
                for(int i=0;i<manager.getElementCount();i++)
                {
                    VarElement elementExist = (VarElement)manager.getElement(i);
                    if (elementExist.Equals(element))
                    {
                        continue;
                    }
                    if (elementExist.name.Equals(textBox_name.Text))
                    {
                        MessageBox.Show("������ͬ�ĵ�Ԫ����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                element.name = textBox_name.Text;
                element.setValue((int)numericUpDown_Value.Value);
            }
            this.Close();

        }

    }
}