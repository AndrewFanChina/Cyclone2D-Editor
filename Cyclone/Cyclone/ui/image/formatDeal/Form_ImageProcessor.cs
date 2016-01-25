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
using Cyclone.alg.image;
using Cyclone.alg.util;

namespace Cyclone.mod.image.formatDeal
{
    public partial class Form_ImageProcessor : Form
    {
        public Form_ImageProcessor()
        {
            InitializeComponent();
        }
        private const int TRANSFORM_PNG_GIF = 0;//png->gif
        private const int TRANSFORM_GIF_PNG = 1;//gif->png
        private const int TRANSFORM_PNG_24_PNG_8A = 2;//png24->png-8+alpha
        private int currentTransform = -1;
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String folderSrc = this.textBox_src.Text;
            String folderDest = this.textBox_dest.Text;
            if (folderSrc == null ||!Directory.Exists(folderSrc))
            {
                MessageBox.Show("Դͼ·�����ô���");
                return;
            }
            if (folderDest == null || !Directory.Exists(folderDest))
            {
                MessageBox.Show("Ŀ��·�����ô���");
                return;
            }
            currentTransform = this.comboBox1.SelectedIndex;
            String formatSrc = "";
            String formatDest = "";
            if (currentTransform < 0)
            {
                MessageBox.Show("ѡ������Ҫת���ĸ�ʽ��");
                return;
            }
            else
            {
                switch (currentTransform)
                {
                    case TRANSFORM_PNG_GIF://png->gif
                        formatSrc = "png";
                        formatDest = "gif";
                        break;
                    case TRANSFORM_GIF_PNG://gif->png
                        formatSrc = "gif";
                        formatDest = "png";
                        break;
                    case TRANSFORM_PNG_24_PNG_8A://png24->png-8+alpha
                        formatSrc = "png";
                        formatDest = "png";
                        break;
                }
            }

            //��ʼת��
            this.richTextBox.AppendText("=============��ʼת��=============\n");
            String[] files = Directory.GetFiles(folderSrc+"\\", "*." + formatSrc);
            this.richTextBox.AppendText("������"+files.Length+"����ת���ļ�"+"\n");
            ImageFormat imgFormat = null;
            String fileAlpha = null;
            String fileDest = null;
            for (int i = 0; i < files.Length; i++)
            {
                Image img = IOUtil.createImage(files[i]);
                Image imgAlpha = null;
                fileDest = files[i].Replace(folderSrc, folderDest);
                fileDest = fileDest.Substring(0, fileDest.LastIndexOf('.'));
                fileAlpha = fileDest + "_alpha." + formatDest;
                fileDest = fileDest + "." + formatDest;
                String fileName = fileDest.Substring(fileDest.LastIndexOf("\\")+1, fileDest.Length - fileDest.LastIndexOf("\\")-1);
                switch (currentTransform)
                {
                    case TRANSFORM_PNG_GIF://png->gif
                        imgFormat = ImageFormat.Gif;
                        img = IndexImageProcessor.getIndexColorImage(img, files[i]);
                        break;
                    case TRANSFORM_GIF_PNG://gif->png
                        imgFormat = ImageFormat.Png;
                        img = IndexImageProcessor.getIndexColorImage(img, files[i]);
                        break;
                    case TRANSFORM_PNG_24_PNG_8A://png24->png-8+alpha
                        imgFormat = ImageFormat.Png;
                        Image[] imgs = PNG24Processor.getPng_8(img);
                        img = imgs[0];
                        imgAlpha = imgs[1];
                        break;
                }
                if (img != null)
                {
                    img.Save(fileDest, imgFormat);//ImageCodecInfo encoder, EncoderParameters encoderParams
                    if (imgAlpha != null)
                    {
                        imgAlpha.Save(fileAlpha, imgFormat);
                    }
                    richTextBox.AppendText("ת���ɹ�->" + fileName + "\n");
                }
                else
                {
                    richTextBox.AppendText("��ת��ʧ�ܡ�->" + fileName + "\n");
                }
                this.Refresh();
            }
            this.richTextBox.AppendText("=============ת�����=============\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
        }
    }
}