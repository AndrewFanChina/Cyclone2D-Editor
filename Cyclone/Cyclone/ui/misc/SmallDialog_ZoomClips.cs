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

namespace Cyclone.mod.misc
{
    public partial class ZoomClipsDialog : Form
    {
        private  ZoomClipsDialog()
        {
            InitializeComponent();
            needUpdate = false;
        }
        private static ZoomClipsDialog zoomClipsDialog = null;
        private static int level0 = 100;
        private static int level1 = 100;
        public static bool needUpdate=false;
        public static int[] getZoomClipsLevel()
        {
            if (zoomClipsDialog == null)
            {
                zoomClipsDialog = new ZoomClipsDialog();
            }
            zoomClipsDialog.ShowDialog();
            if (needUpdate)
            {
                return new int[] { level0, level1 };
            }
            else
            {
                return null;
            }
        }
        //按钮事件响应
        private void button_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            needUpdate = true;
            this.Close();
        }
        private void trackBar_PHY_ValueChanged(object sender, EventArgs e)
        {
            level0 = Convert.ToInt32(((TrackBar)sender).Value);
            this.label_0S.Text = level0 + "%";
        }

        private void trackBar_BG_ValueChanged(object sender, EventArgs e)
        {
            level1 = Convert.ToInt32(((TrackBar)sender).Value);
            this.label_1S.Text = level1 + "%";
        }




    }
}