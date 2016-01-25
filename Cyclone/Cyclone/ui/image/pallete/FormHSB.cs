using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cyclone.mod.image.pallete
{
    public delegate void DelegateHSB(int hue, int saturation, int brightness);

    public partial class FormHSB : Form
    {
        public event DelegateHSB RateImage;
        public bool applyChange = false;

        public FormHSB()
        {
            InitializeComponent();
        }

        private void trackBarR_ValueChanged(object sender, EventArgs e)
        {
            labelR.Text = trackBar_Hue.Value + "%";

            if (RateImage != null)
            {
                RateImage(trackBar_Hue.Value, trackBar_Saturation.Value, trackBar_Brightness.Value);
            }
        }

        private void trackBarG_ValueChanged(object sender, EventArgs e)
        {
            labelG.Text = trackBar_Saturation.Value + "%";

            if (RateImage != null)
            {
                RateImage(trackBar_Hue.Value, trackBar_Saturation.Value, trackBar_Brightness.Value);
            }
        }

        private void trackBarB_ValueChanged(object sender, EventArgs e)
        {
            labelB.Text = trackBar_Brightness.Value + "%";

            if (RateImage != null)
            {
                RateImage(trackBar_Hue.Value, trackBar_Saturation.Value, trackBar_Brightness.Value);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            applyChange = true;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (RateImage != null)
            {
                RateImage(0, 0, 0);
            }

            Close();
        }

        private void FormRate_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (!applyChange)
            //{
            //    if (RateImage != null)
            //    {
            //        RateImage(0, 0, 0);
            //    }
            //}
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            trackBar_Hue.Value = 0;
            trackBar_Saturation.Value = 0;
            trackBar_Brightness.Value = 0;
        }
    }
}