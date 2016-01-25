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
using Cyclone.alg.util;

namespace Cyclone.mod.misc
{
    public partial class SmallDialog_History : Form
    {
        private static Form_Main form_main = null;
        public SmallDialog_History(Form_Main form)
        {
            InitializeComponent();
            form_main = form;
        }

        private void listBox_History_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void refreshList(ArrayList array)
        {
            if (array == null)
            {
                return;
            }
            this.listBox_History.Items.Clear();
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] is UndoInfo)
                {
                    UndoInfo info = (UndoInfo)array[i];
                    listBox_History.Items.Add(info.m_undoCommand.GetText());
                }
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}