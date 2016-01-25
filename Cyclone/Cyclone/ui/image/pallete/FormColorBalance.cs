using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Cyclone.alg;
using Cyclone.mod;
using Cyclone.alg.image;
using Cyclone.alg.math;
using Cyclone.alg.util;

namespace Cyclone.mod.image.pallete
{
    //public delegate void DelegateBalance(int hue, int saturation, int brightness);

    public partial class FormColorBalance : Form
    {
        //public event DelegateBalance RateImage;
        public bool applyChange = false;
        Form_Pallete formPallete = null;
        public FormColorBalance(Form_Pallete formPalleteT)
        {
            InitializeComponent();
            formPallete = formPalleteT;
            Color_Transfer_Init();
            Color_Balance_Initialize();
        }
        int GetIndex_TransferMode()
        {
            if (radioButton_dark.Checked)
            {
                return c_Shadows;
            }
            else if (radioButton_middle.Checked)
            {
                return c_Midtones;
            }
            else if (radioButton_Light.Checked)
            {
                return c_Highlights;
            }
            return -1;
        }
        private void trackBarR_ValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            int a_Value = trackBar_1.Value;
            labelR.Text = a_Value + "";
            int v_Index = GetIndex_TransferMode();
            if (f_Cyan_Red[v_Index] != a_Value)
            {
                f_Cyan_Red[v_Index] = a_Value;
                updateImage();
            }
            //if (RateImage != null)
            //{
            //    RateImage(trackBar_Hue.Value, trackBar_Saturation.Value, trackBar_Brightness.Value);
            //}
        }

        private void trackBarG_ValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            int a_Value = trackBar_2.Value;
            labelG.Text = a_Value + "";
            int v_Index = GetIndex_TransferMode();
            if (f_Yellow_Blue[v_Index] != a_Value)
            {
                f_Yellow_Blue[v_Index] = a_Value;
                updateImage();
            }
            //if (RateImage != null)
            //{
            //    RateImage(trackBar_Hue.Value, trackBar_Saturation.Value, trackBar_Brightness.Value);
            //}
        }

        private void trackBarB_ValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            //labelB.Text = trackBar_Brightness.Value + "";
            int a_Value = trackBar_3.Value;
            labelB.Text = a_Value + "";
            int v_Index = GetIndex_TransferMode();
            if (f_Magenta_Green[v_Index] != a_Value)
            {
                f_Magenta_Green[v_Index] = a_Value;
                updateImage();
            }
            //updateImage();
            //if (RateImage != null)
            //{
            //    RateImage(trackBar_Hue.Value, trackBar_Saturation.Value, trackBar_Brightness.Value);
            //}
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            applyChange = true;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //if (RateImage != null)
            //{
            //    RateImage(0, 0, 0);
            //}

            Close();
        }

        private void FormRate_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!applyChange)
            {
                //if (RateImage != null)
                //{
                //    RateImage(0, 0, 0);
                //}
            }
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            trackBar_1.Value = 0;
            trackBar_2.Value = 0;
            trackBar_3.Value = 0;
        }
        //色彩平衡计算

        double[] TColorTransferArray = new double[256];
        double[] pColorTransferArray = null;

        const int c_Shadows    = 0;
        const int c_Midtones   = 1;
        const int c_Highlights = 2;

        byte []f_Red_Lookup=new byte[256];
        byte[] f_Green_Lookup = new byte[256];
        byte[] f_Blue_Lookup = new byte[256];

        int []f_Cyan_Red=new int[3];
        int []f_Magenta_Green=new int[3];
        int []f_Yellow_Blue=new int[3];

        // for lightening
        double[] v_Highlights_Add = new double[256];
        double[] v_Midtones_Add = new double[256];
        double[] v_Shadows_Add = new double[256];

        // for darkening
        double[] v_Highlights_Sub = new double[256];
        double[] v_Midtones_Sub = new double[256];
        double[] v_Shadows_Sub = new double[256];

        bool f_PreserveLuminosity=true;

        int IntToByte(int a_I){

          if(a_I > 255)
          {
              return 255;
          }
          else if (a_I < 0)
          {
              return 0;
          }
          return a_I;
        }
        void Color_Transfer_Init()
        {

          for(int v_i= 0;v_i<=255;v_i++)
          {
            v_Shadows_Sub[255 - v_i] = (1.075D - 1 / (v_i / 16.0D + 1));
            v_Highlights_Add[v_i]    = v_Shadows_Sub[255 - v_i];
            v_Midtones_Sub[v_i] = 0.667D * (1 - MathUtil.Sqr((v_i - 127.0D) / 127.0D));
            v_Midtones_Add[v_i]      = v_Midtones_Sub[v_i];
            v_Highlights_Sub[v_i] = 0.667D * (1 - MathUtil.Sqr((v_i - 127.0D) / 127.0D));
            v_Shadows_Add[v_i]       = v_Highlights_Sub[v_i];
          }
        }
        private void  Color_Balance_Initialize()
        {
          pColorTransferArray = TColorTransferArray;
          for (int v_i= 0; v_i<=2;v_i++)
          {
            f_Cyan_Red[v_i]      = 0;
            f_Magenta_Green[v_i] = 0;
            f_Yellow_Blue[v_i]   = 0;
          }
          Color_Balance_Create_Lookup_Tables();
        }

        private void Color_Balance_Create_Lookup_Tables()
        {
                Object[] v_Cyan_Red_Transfer = new Object[3];
                Object[] v_Magenta_Green_Transfer =new Object[3];
                Object[] v_Yellow_Blue_Transfer = new Object[3];  
                int v_i, v_r_n, v_g_n, v_b_n;
                // Set the transfer arrays  (for speed)
                // Cyan_Red
                if (f_Cyan_Red[c_Shadows] > 0)
                {
                    v_Cyan_Red_Transfer[c_Shadows] = v_Shadows_Add;
                }
                else
                {
                    v_Cyan_Red_Transfer[c_Shadows] = v_Shadows_Sub;
                }

                if (f_Cyan_Red[c_Midtones] > 0)
                {
                    v_Cyan_Red_Transfer[c_Midtones] = v_Midtones_Add;
                }
                else
                {
                    v_Cyan_Red_Transfer[c_Midtones] = v_Midtones_Sub;
                }

                if (f_Cyan_Red[c_Highlights] > 0)
                {
                    v_Cyan_Red_Transfer[c_Highlights] = v_Highlights_Add;
                }
                else
                {
                    v_Cyan_Red_Transfer[c_Highlights] = v_Highlights_Sub;
                }

                // Magenta_Green
                if (f_Magenta_Green[c_Shadows] > 0)
                {
                    v_Magenta_Green_Transfer[c_Shadows] = v_Shadows_Add;
                }
                else
                {
                    v_Magenta_Green_Transfer[c_Shadows] = v_Shadows_Sub;
                }

                if (f_Magenta_Green[c_Midtones] > 0)
                {
                    v_Magenta_Green_Transfer[c_Midtones] = v_Midtones_Add;
                }
                else
                {
                    v_Magenta_Green_Transfer[c_Midtones] = v_Midtones_Sub;
                }

                if (f_Magenta_Green[c_Highlights] > 0)
                {
                    v_Magenta_Green_Transfer[c_Highlights] = v_Highlights_Add;
                }
                else
                {
                    v_Magenta_Green_Transfer[c_Highlights] = v_Highlights_Sub;
                }

                // Yellow_Blue
                if (f_Yellow_Blue[c_Shadows] > 0)
                {
                    v_Yellow_Blue_Transfer[c_Shadows] = v_Shadows_Add;
                }
                else
                {
                    v_Yellow_Blue_Transfer[c_Shadows] = v_Shadows_Sub;
                }

                if (f_Yellow_Blue[c_Midtones] > 0)
                {
                    v_Yellow_Blue_Transfer[c_Midtones] = v_Midtones_Add;
                }
                else
                {
                    v_Yellow_Blue_Transfer[c_Midtones] = v_Midtones_Sub;
                }

                if (f_Yellow_Blue[c_Highlights] > 0)
                {
                    v_Yellow_Blue_Transfer[c_Highlights] = v_Highlights_Add;
                }
                else
                {
                    v_Yellow_Blue_Transfer[c_Highlights] = v_Highlights_Sub;
                }
                for (v_i = 0; v_i <= 255; v_i++)
                {
                    v_r_n = v_i;
                    v_g_n = v_i;
                    v_b_n = v_i;

                    v_r_n = (int)(v_r_n + Math.Round(f_Cyan_Red[c_Shadows] * (((double[])v_Cyan_Red_Transfer[c_Shadows])[v_r_n])));
                    v_r_n = IntToByte(v_r_n);
                    v_r_n = (int)(v_r_n + Math.Round(f_Cyan_Red[c_Midtones] * (((double[])v_Cyan_Red_Transfer[c_Midtones])[v_r_n])));
                    v_r_n = IntToByte(v_r_n);
                    v_r_n = (int)(v_r_n + Math.Round(f_Cyan_Red[c_Highlights] * (((double[])v_Cyan_Red_Transfer[c_Highlights])[v_r_n])));
                    v_r_n = IntToByte(v_r_n);

                    v_g_n = (int)(v_g_n + Math.Round(f_Magenta_Green[c_Shadows] * (((double[])v_Magenta_Green_Transfer[c_Shadows])[v_g_n])));
                    v_g_n = IntToByte(v_g_n);
                    v_g_n = (int)(v_g_n + Math.Round(f_Magenta_Green[c_Midtones] * (((double[])v_Magenta_Green_Transfer[c_Midtones])[ v_g_n])));
                    v_g_n = IntToByte(v_g_n);
                    v_g_n = (int)(v_g_n + Math.Round(f_Magenta_Green[c_Highlights] *(((double[])v_Magenta_Green_Transfer[c_Highlights])[v_g_n])));
                    v_g_n = IntToByte(v_g_n);

                    v_b_n = (int)(v_b_n + Math.Round(f_Yellow_Blue[c_Shadows] * (((double[])v_Yellow_Blue_Transfer[c_Shadows])[v_b_n])));
                    v_b_n = IntToByte(v_b_n);
                    v_b_n = (int)(v_b_n + Math.Round(f_Yellow_Blue[c_Midtones] * (((double[])v_Yellow_Blue_Transfer[c_Midtones])[ v_b_n])));
                    v_b_n = IntToByte(v_b_n);
                    v_b_n = (int)(v_b_n + Math.Round(f_Yellow_Blue[c_Highlights] *(((double[])v_Yellow_Blue_Transfer[c_Highlights])[ v_b_n])));
                    v_b_n = IntToByte(v_b_n);


                    f_Red_Lookup[v_i] = (byte)v_r_n;
                    f_Green_Lookup[v_i] = (byte)v_g_n;
                    f_Blue_Lookup[v_i] = (byte)v_b_n;
                    //if (v_r_n != 0 || v_g_n != 0 || v_b_n != 0)
                    //{
                    //    Console.WriteLine("v_r_n:" + v_r_n + "," + "v_g_n:" + v_g_n + "," + "v_b_n:" + v_b_n);
                    //}
                }

        }
        public void makeColorBalance()
        {
            ColorTable v_SourceTable = formPallete.destColorTableBak;
            ColorTable v_DestTable = formPallete.destColorTable;
            int v_A;
            double v_H, v_L, v_S;
            int v_R, v_G, v_B, v_r_n, v_g_n, v_b_n;
            int RGB_Src;
            int colorNew;
            double[] hsb;
            int colorCount = formPallete.currentColorIDs.Count;
            if (colorCount == 0)
            {
                colorCount = v_SourceTable.getColorCount();
                for (int v_i = 0; v_i < colorCount; v_i++)
                {
                    // Reserve the Alpha channel of the Dest.
                    RGB_Src = v_SourceTable.getColor(v_i);
                    v_A = 0xFF;
                    if (v_i == formPallete.transID)//透明色
                    {
                        continue;
                    }
                    // Extract the RGB values from the Source.
                    v_R = (RGB_Src >> 16) & 0xFF;
                    v_G = (RGB_Src >> 8) & 0xFF;
                    v_B = (RGB_Src) & 0xFF;
                    // Get the new RGB values.
                    v_r_n = f_Red_Lookup[v_R];
                    v_g_n = f_Green_Lookup[v_G];
                    v_b_n = f_Blue_Lookup[v_B];
                    // Modify the RGB values of the Dest.
                    colorNew = (v_A << 24) | (v_r_n << 16) | (v_g_n << 8) | v_b_n;
                    v_DestTable.setColor(v_i, colorNew);

                    if (f_PreserveLuminosity)
                    {
                        // Get the new HLS.
                        //RGBToHLS_32(colorNew);
                        hsb = GraphicsUtil.RGBtoHSB(v_r_n, v_g_n, v_b_n);
                        v_H = hsb[0] * 360;
                        v_S = hsb[1] * 100;
                        v_L = hsb[2] * 255;
                        //v_S = IntToSaturation((int)v_S);
                        // Get the original L.
                        //colorNew  = RGB_Src;
                        //v_L = IntToByte(RGBToLightness_32(RGB_Src));
                        hsb = GraphicsUtil.RGBtoHSB(v_R, v_G, v_B);
                        // Combine the new H, S and the original L.
                        //colorNew=HLSToRGB_32(v_A, v_H, v_L, v_S);
                        v_L = hsb[2] * 255;
                        colorNew = GraphicsUtil.HSBtoRGB(v_H / 360.0D, v_S / 100.0D, v_L / 255.0D);
                        v_DestTable.setColor(v_i, colorNew);
                        //Console.WriteLine(MiscUtil.convertHexString(RGB_Src) + "," + MiscUtil.convertHexString(colorNew));
                    }
                }
            }
            else
            {
                int id;
                for (int v_i = 0; v_i < colorCount; v_i++)
                {
                    id = (int)formPallete.currentColorIDs[v_i];
                    // Reserve the Alpha channel of the Dest.
                    RGB_Src = v_SourceTable.getColor(id);
                    v_A = 0xFF;
                    if (id == formPallete.transID)//透明色
                    {
                        continue;
                    }
                    // Extract the RGB values from the Source.
                    v_R = (RGB_Src >> 16) & 0xFF;
                    v_G = (RGB_Src >> 8) & 0xFF;
                    v_B = (RGB_Src) & 0xFF;
                    // Get the new RGB values.
                    v_r_n = f_Red_Lookup[v_R];
                    v_g_n = f_Green_Lookup[v_G];
                    v_b_n = f_Blue_Lookup[v_B];
                    // Modify the RGB values of the Dest.
                    colorNew = (v_A << 24) | (v_r_n << 16) | (v_g_n << 8) | v_b_n;
                    v_DestTable.setColor(id, colorNew);

                    if (f_PreserveLuminosity)
                    {
                        // Get the new HLS.
                        //RGBToHLS_32(colorNew);
                        hsb = GraphicsUtil.RGBtoHSB(v_r_n, v_g_n, v_b_n);
                        v_H = hsb[0] * 360;
                        v_S = hsb[1] * 100;
                        v_L = hsb[2] * 255;
                        //v_S = IntToSaturation((int)v_S);
                        // Get the original L.
                        //colorNew  = RGB_Src;
                        //v_L = IntToByte(RGBToLightness_32(RGB_Src));
                        hsb = GraphicsUtil.RGBtoHSB(v_R, v_G, v_B);
                        // Combine the new H, S and the original L.
                        //colorNew=HLSToRGB_32(v_A, v_H, v_L, v_S);
                        v_L = hsb[2] * 255;
                        colorNew = GraphicsUtil.HSBtoRGB(v_H / 360.0D, v_S / 100.0D, v_L / 255.0D);
                        v_DestTable.setColor(id, colorNew);
                        //Console.WriteLine(MiscUtil.convertHexString(RGB_Src) + "," + MiscUtil.convertHexString(colorNew));
                    }
                }
            }

        }

        private void checkBox_maintainBright_CheckedChanged(object sender, EventArgs e)
        {
            f_PreserveLuminosity = checkBox_maintainBright.Checked;
            updateImage();
        }
        private void updateImage()
        {
            Color_Balance_Create_Lookup_Tables();
            makeColorBalance();
            formPallete.updateInColorBalance();
        }
        private bool noEvent = false;
        private void setBarValue()
        {
            int v_Index = GetIndex_TransferMode();
            noEvent = true;
            trackBar_1.Value = f_Cyan_Red[v_Index];
            trackBar_2.Value = f_Magenta_Green[v_Index];
            trackBar_3.Value = f_Yellow_Blue[v_Index];
            labelR.Text = f_Cyan_Red[v_Index]+"";
            labelG.Text = f_Magenta_Green[v_Index] + "";
            labelB.Text = f_Yellow_Blue[v_Index] + "";
            noEvent = false;
        }
        private void radioButton_dark_CheckedChanged(object sender, EventArgs e)
        {
            setBarValue();
        }

        private void radioButton_middle_CheckedChanged(object sender, EventArgs e)
        {
            setBarValue();
        }

        private void radioButton_Light_CheckedChanged(object sender, EventArgs e)
        {
            setBarValue();
        }
    }
}