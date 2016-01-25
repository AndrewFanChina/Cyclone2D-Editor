using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cyclone.ui_script
{
    public partial class SmallDialog_MessageBox : Form
    {
        private SmallDialog_MessageBox(String item0, String item1, String item2, String item3, String waring)
        {
            InitializeComponent();
            this.button_B1.Text = item0;
            this.button_B2.Text = item1;
            this.button_B3.Text = item2;
            this.button_B4.Text = item3;
            this.text_Warning.Text = waring;
        }
        private static int result = 0;
        public static int getResult(String item0, String item, String item2, String item3, String waring)
        {
            SmallDialog_MessageBox dialog = new SmallDialog_MessageBox(item0, item, item2, item3, waring);
            dialog.ShowDialog();
            return result;
        }

        private void button_BL_Click(object sender, EventArgs e)
        {
            result = 0;
            this.Close();
        }

        private void button_BC_Click(object sender, EventArgs e)
        {
            result = 1;
            this.Close();
        }

        private void button_BR_Click(object sender, EventArgs e)
        {
            result = 2;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            result = 3;
            this.Close();
        }
    }
}