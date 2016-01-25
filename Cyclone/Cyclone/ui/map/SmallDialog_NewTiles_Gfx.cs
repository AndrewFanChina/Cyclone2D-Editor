using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.mod;
using Cyclone.alg;
using Cyclone.mod.anim;
using Cyclone.mod.map;
using Cyclone.alg.util;
using Cyclone.mod.animimg;

namespace Cyclone.mod.map
{
    public partial class SmallDialog_NewTiles_Gfx : Form
    {
        private static MImgsManager imgManager;
        private static TileGfxContainer tileGfxManager;
        private SmallDialog_NewTiles_Gfx(MImgsManager imgManagerT, TileGfxContainer tileGfxManagerT)
        {
            InitializeComponent();
            imgManager = imgManagerT;
            tileGfxManager = tileGfxManagerT;
            initParams();
        }
        private void initParams()
        {
            updateRes();
            if (listBox_ImageManager.Items.Count > 0)
            {
                listBox_ImageManager.SelectedIndex = 0;
            }
        }
        //释放资源
        public void releaseRes()
        {
            imgManager = null;
        }
        //设置管理器
        public void setImagesManager(MImgsManager imgManagerT)
        {
            imgManager = imgManagerT;
        }
        //刷新数据显示========================================================
        public void updateRes()
        {
            listBox_ImageManager.Items.Clear();
            MImgElement imgElement;
            for (int i = 0; i < imgManager.Count(); i++)
            {
                imgElement = imgManager[i];
                listBox_ImageManager.Items.Add(imgElement.name + "   [" + imgElement.getUsedTime()+ "]");
            }
            listBox_ImageManager.SelectedIndex = listBox_ImageManager.Items.Count - 1;
            updateImage();
        }
        //刷新图片显示========================================================
        public void updateImage()
        {
            if (listBox_ImageManager.Items.Count <= 0)//列表为空
            {
                pictureBox_ImageManager.Image = null;
                return;
            }
            int itemIndex = listBox_ImageManager.SelectedIndex;
            if (imgManager[itemIndex].image != null)
            {
                pictureBox_ImageManager.Image = imgManager[itemIndex].image;
                pictureBox_ImageManager.Size = pictureBox_ImageManager.Image.Size;
            }
            else
            {
                //创建一个显示警告的缓冲图片
                Image imgBuf = new Bitmap(100,30);
                GraphicsUtil.drawString(Graphics.FromImage(imgBuf), 2, 2, "image lost", Consts.colorRed, Consts.LEFT | Consts.TOP);
                pictureBox_ImageManager.Size = imgBuf.Size;
                pictureBox_ImageManager.Image = imgBuf;

            }

        }
        //自动生成图片方格
        public static int generateTiles(MImgsManager imgManagerT, TileGfxContainer tileGfxManagerT)
        {
            number = 0;
            SmallDialog_NewTiles_Gfx dialog = new SmallDialog_NewTiles_Gfx(imgManagerT, tileGfxManagerT);
            dialog.ShowDialog();
            return number;
        }
        //事件响应============================================================
        private void button_closeImageManager_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateImage();
        }
        private static int number = 0;
        private void button_Ok_Click(object sender, EventArgs e)
        {
            if (tileGfxManager != null)
            {
                number = tileGfxManager.generateTiles(imgManager[listBox_ImageManager.SelectedIndex],(int)numericUpDown_TileW.Value, (int)numericUpDown_TileH.Value);
            }
            this.Close();
        }

    }
}