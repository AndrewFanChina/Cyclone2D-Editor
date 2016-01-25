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
using Cyclone.alg.type;

namespace Cyclone.mod.util
{
    public partial class SmallDialog_ShowList : Form
    {
        private SmallDialog_ShowList()
        {
            InitializeComponent();
        }
        private static SmallDialog_ShowList dialog = null;
        private static ShowItemList showItemList = null;
        public static void showList(ShowItemList showItemListT)
        {
            if (dialog == null || dialog.IsDisposed)
            {
                dialog = new SmallDialog_ShowList();
            }
            if (showItemList != null)
            {
                showItemList.removeAll();
            }
            showItemList = showItemListT;
            dialog.init();
            dialog.Show();
            dialog.Activate();
        }
        public void init()
        {
            if (showItemList != null)
            {
                showItemList.refreshUI(listBox_content);
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_goto_Click(object sender, EventArgs e)
        {
            activeGoto();
        }

        private void listBox_content_DoubleClick(object sender, EventArgs e)
        {
            activeGoto();
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            activeDelete();
        }

        private void listBox_content_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                activeDelete();
            }
        }
        //执行转到
        public void activeGoto()
        {
            if (showItemList == null)
            {
                return;
            }
            showItemList.activeGoToItem(listBox_content.SelectedIndex);
        }
        //执行删除
        public void activeDelete()
        {
            if (showItemList == null)
            {
                return;
            }
            ArrayList arrayList = new ArrayList();
            foreach (int index in listBox_content.SelectedIndices)
            {
                arrayList.Add(index);
            }
            while (arrayList.Count > 0)
            {
                int index = (int)arrayList[arrayList.Count - 1];
                arrayList.RemoveAt(arrayList.Count - 1);
                showItemList.removeElement(index);
            }
        }

    }
    public class ShowItem : ObjectElement
    {
        public ShowItem(ShowItemList parentT,String textT, ArrayList contentT)
        {
            parent = parentT;
            name = textT;
            value = contentT;
        }

        public override string getValueToLenString()
        {
            return name;
        }

        public override int getUsedTime()
        {
            return 0;
        }

        public override ObjectElement clone()
        {
            return null;
        }
    }
    public class ShowItemList : ObjectVector
    {
        public ActiveItem showWindow = null;
        public ShowItemList(ActiveItem showWindowT)
        {
            showWindow = showWindowT;
        }
        public void activeGoToItem(int index)
        {
            if(index < 0 || index >= getElementCount())
            {
                return;
            }

            ShowItem showItem = (ShowItem)getElement(index);
            showWindow.activeItem(showItem);
        }
    }
    public interface ActiveItem
    {
        void activeItem(ShowItem showItem);
    }
}