using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using Cyclone.alg;
using System.Collections;
using Cyclone.alg.image;
using Cyclone.alg.util;

namespace Cyclone.mod.image.formatDeal
{
    public partial class Form_ImageZoomer : Form
    {
        public Form_ImageZoomer()
        {
            InitializeComponent();
        }
        private const int TRANSFORM_PNG_GIF = 0;//png->gif
        private const int TRANSFORM_GIF_PNG = 1;//gif->png
        private const int TRANSFORM_PNG_24_PNG_8A = 2;//png24->png-8+alpha
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String folderSrc = this.textBox_src.Text;
            String folderDest = this.textBox_dest.Text;
            if (folderSrc == null ||!Directory.Exists(folderSrc))
            {
                MessageBox.Show("源图路径设置错误！");
                return;
            }
            if (folderDest == null || !Directory.Exists(folderDest))
            {
                MessageBox.Show("目标路径设置错误！");
                return;
            }
            int transPercentBegin = Convert.ToInt32(numericUpDown_min.Value);
            int transCount = Convert.ToInt32(numericUpDown_count.Value);
            //开始转换
            this.richTextBox.AppendText("=============开始转换=============\n");
            String[] files = Directory.GetFiles(folderSrc+"\\", "*.png");
            this.richTextBox.AppendText("共发现"+files.Length+"个被转换文件"+"\n");
            String fileDest = null;
            for (int i = 0; i < files.Length; i++)
            {
                Image img = IOUtil.createImage(files[i]);
                String fileName = files[i].Substring(files[i].LastIndexOf("\\") + 1, files[i].Length - files[i].LastIndexOf("\\") - 1);
                fileName = fileName.Substring(0, fileName.LastIndexOf("."));
                ArrayList printPercent = new ArrayList();
                //遍历每个缩放级别
                for (int j = 0; j < transCount; j++)
                {
                    fileDest = folderDest +"\\"+ fileName +"_z"+(j)+ ".png";
                    int percent=transPercentBegin+(transCount-j)*(100-transPercentBegin)/transCount;
                    percent = transPercentBegin * transCount * 100 / ((j+1) * (100 - transPercentBegin) + transPercentBegin * transCount);
                    int wN=percent*img.Width/100;
                    int hN=percent*img.Height/100;
                    printPercent.Add(percent);
                    Image zoomImg = new Bitmap(wN, hN);
                    //缩放图片
                    Graphics g = Graphics.FromImage(zoomImg);
                    GraphicsUtil.drawClip(g, img, 0, 0, 0, 0, img.Width, img.Height, 0, percent / 100.0f);
                    g.Dispose();
                    //转换成PNG
                    Image pngImg = IndexImageProcessor.getIndexColorImage(zoomImg, files[i]);
                    if (pngImg != null)
                    {
                        pngImg.Save(fileDest, ImageFormat.Png);//ImageCodecInfo encoder, EncoderParameters encoderParams
                        richTextBox.AppendText("转换成功->" + fileDest + "\n");
                    }
                    else
                    {
                        richTextBox.AppendText("【转换失败】->" + fileDest + "\n");
                    }
                    pngImg.Dispose();
                    zoomImg.Dispose();
                    this.Refresh();
                }
                richTextBox.AppendText("public static int percents[]=new int[]{");
                richTextBox.AppendText("100,");
                for (int j = 0; j < printPercent.Count; j++)
                {
                    richTextBox.AppendText(""+printPercent[j]);
                    if (j != printPercent.Count - 1)
                    {
                        richTextBox.AppendText(",");
                    }
                }
                richTextBox.AppendText("};\n");
            }
            this.richTextBox.AppendText("=============转换完毕=============\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
        }
    }
}