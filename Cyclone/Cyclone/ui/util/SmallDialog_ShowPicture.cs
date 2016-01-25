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
    public partial class SmallDialog_ShowPicture : Form
    {
        private SmallDialog_ShowPicture()
        {
            InitializeComponent();
        }
        private static SmallDialog_ShowPicture dialog = null;
        public static void ShowPicture(String title,String text,Image image)
        {
            if (dialog == null || dialog.IsDisposed)
            {
                dialog = new SmallDialog_ShowPicture();
            }
            if (text == null)
            {
                text = "";
            }
            dialog.Text = title;
            dialog.richTextBox_Content.Text = text;
            dialog.panel_ImgBG.Controls.Remove(dialog.pictureBox_Img);
            dialog.pictureBox_Img.Size = new Size(image.Width, image.Height);
            dialog.pictureBox_Img.Image = image;
            dialog.pictureBox_Img.Location = new Point(0, 0);
            dialog.panel_ImgBG.Controls.Add(dialog.pictureBox_Img);
            dialog.ShowDialog();
        }


        private void button_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}