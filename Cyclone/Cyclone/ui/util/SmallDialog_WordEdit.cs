using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cyclone.mod.util
{
    public partial class SmallDialog_WordEdit : Form
    {
        private static String txtValue = null;
        private static String oldValue = null;
        private static SmallDialog_WordEdit smallDialog_Text = null;
        private static bool cancle = true;//被初始化为true，如果用户不是通过或者或者按钮关闭的话，不会改变cancle
        public SmallDialog_WordEdit(String title,String defValue)
        {
            oldValue = defValue;
            txtValue = defValue;
            InitializeComponent();
            this.Text = title;
            textBox_value.Text = txtValue;
            this.text_warning.Text = "请在文本框内输入文字，然后按回车或者确定按钮";
            cancle = true;
        }
        public SmallDialog_WordEdit(String title,String notice, String defValue)
        {
            oldValue = defValue;
            txtValue = defValue;
            InitializeComponent();
            this.Text = title;
            textBox_value.Text = txtValue;
            this.text_warning.Text = notice;
            cancle = true;
        }
        public String getValue()
        {
            return txtValue;
        }
        public void setValue(String title, String value)
        {
            this.Text = title;
            txtValue = value;
            textBox_value.Text = value;
        }
        public static String getNewName(String title, String defValue)
        {
            if (smallDialog_Text == null)
            {
                smallDialog_Text = new SmallDialog_WordEdit(title, defValue);
            }
            else
            {
                smallDialog_Text.setValue(title,defValue);
            }
            smallDialog_Text.ShowDialog();
            return smallDialog_Text.getValue();
        }
        private void button_closeImageManager_Click(object sender, EventArgs e)
        {
            txtValue = textBox_value.Text;
            cancle = false;
            this.Close();
        }

        private void Form_TextDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Enter)
            {
                txtValue = textBox_value.Text;
                cancle = false;
                this.Close();
            }
        }

        private void SmallDialog_WordEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cancle)
            {
                txtValue = oldValue;
            }
        }
    }
}