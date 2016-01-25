using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cyclone.mod.image.pallete
{
    public delegate void DelegateRGB(int color);

    public partial class FormRGB : Form
    {
        public event DelegateRGB EditImage;
        int colorOrg;

        public FormRGB(int color)
        {
            InitializeComponent();

            colorOrg = color;
            trackBarR.Value = (color >> 16) & 0xFF;
            trackBarG.Value = (color >> 8) & 0xFF;
            trackBarB.Value = (color) & 0xFF;       
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (EditImage != null)
            {
                EditImage(colorOrg);
            }
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackBarR_ValueChanged(object sender, EventArgs e)
        {
            labelR.Text = trackBarR.Value.ToString();
            ShowColor();
        }

        private void trackBarG_ValueChanged(object sender, EventArgs e)
        {
            labelG.Text = trackBarG.Value.ToString();
            ShowColor();
        }

        private void trackBarB_ValueChanged(object sender, EventArgs e)
        {
            labelB.Text = trackBarB.Value.ToString();
            ShowColor();
        }

        private void ShowColor()
        {
            pictureBoxColor.BackColor = Color.FromArgb(trackBarR.Value, trackBarG.Value, trackBarB.Value);
            int color = pictureBoxColor.BackColor.ToArgb();
            if (EditImage != null)
            {
                EditImage(color);
            }
            
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            cd.Color = pictureBoxColor.BackColor;
            cd.FullOpen = true;

            if (cd.ShowDialog() == DialogResult.OK)
            {
                trackBarR.Value = cd.Color.R;
                trackBarG.Value = cd.Color.G;
                trackBarB.Value = cd.Color.B;
            }
        }

        private void pictureBoxColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            cd.Color = pictureBoxColor.BackColor;
            cd.FullOpen = true;

            if (cd.ShowDialog() == DialogResult.OK)
            {
                trackBarR.Value = cd.Color.R;
                trackBarG.Value = cd.Color.G;
                trackBarB.Value = cd.Color.B;
            }
        }

    }
}