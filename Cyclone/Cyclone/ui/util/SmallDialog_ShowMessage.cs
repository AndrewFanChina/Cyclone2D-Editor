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
    public partial class SmallDialog_ShowMessage : Form
    {
        private SmallDialog_ShowMessage()
        {
            InitializeComponent();
        }
        private static SmallDialog_ShowMessage dialog = null;
        public static void showMessage(String title,String text)
        {
            if (dialog == null || dialog.IsDisposed)
            {
                dialog = new SmallDialog_ShowMessage();
            }
            if (text == null)
            {
                text = "";
            }
            dialog.Text = title;
            dialog.richTextBox_Content.Text = text;
            dialog.ShowDialog();
        }


        private void button_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}