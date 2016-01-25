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

namespace Cyclone.mod.util
{
    public partial class SmallDialog_ShowParagraph : Form
    {
        private SmallDialog_ShowParagraph()
        {
            InitializeComponent();
        }
        private static SmallDialog_ShowParagraph dialog = null;
        public static void showString(String title,String text)
        {
            if (dialog == null || dialog.IsDisposed)
            {
                dialog = new SmallDialog_ShowParagraph();
            }
            if (text == null)
            {
                text = "";
            }
            dialog.Text = title;
            dialog.richTextBox_Content.Text = text;
            dialog.Show();
            dialog.Activate();
        }


        private void button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SmallDialog_ShowString_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //((Control)sender).Hide();
        }
    }
}