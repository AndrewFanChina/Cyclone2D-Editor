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
using Cyclone.alg.util;

namespace Cyclone.mod.misc
{
    public partial class SmallDialog_LevelEyes : Form
    {
        private static int alpha_phy = 0;
        private static int alpha_bg = 0;
        private static int alpha_sur = 0;
        private static int alpha_tile_bg = 0;
        private static int alpha_obj_bg = 0;
        private static int alpha_obj = 0;
        private static Form_Main form_main = null;
        public SmallDialog_LevelEyes(Form_Main form)
        {
            InitializeComponent();
            needUpdate = false;
            trackBar_PHY.Value = Consts.LEVEL_ALPHA_FLAG_PHY;
            trackBar_BG.Value = Consts.LEVEL_ALPHA_TILE_BG;
            trackBar_SUR.Value = Consts.LEVEL_ALPHA_TILE_SUR;
            trackBar_Tile_OBJ.Value = Consts.LEVEL_ALPHA_TILE_OBJ;
            trackBar_OBJ_Mask.Value = Consts.LEVEL_ALPHA_OBJ_MASK;
            trackBar_OBJ.Value = Consts.LEVEL_ALPHA_OBJ_TRIGEER;
            alpha_phy = Consts.LEVEL_ALPHA_FLAG_PHY;
            alpha_bg = Consts.LEVEL_ALPHA_TILE_BG;
            alpha_sur = Consts.LEVEL_ALPHA_TILE_SUR;
            alpha_tile_bg = Consts.LEVEL_ALPHA_TILE_OBJ;
            alpha_obj_bg = Consts.LEVEL_ALPHA_OBJ_MASK;
            alpha_obj = Consts.LEVEL_ALPHA_OBJ_TRIGEER;
            form_main = form;
        }
        public static bool needUpdate=false;
        //按钮事件响应
        private void button_Cancle_Click(object sender, EventArgs e)
        {
            Consts.LEVEL_ALPHA_FLAG_PHY = alpha_phy;
            Consts.LEVEL_ALPHA_TILE_BG = alpha_bg;
            Consts.LEVEL_ALPHA_TILE_SUR = alpha_sur;
            Consts.LEVEL_ALPHA_TILE_OBJ = alpha_tile_bg;
            Consts.LEVEL_ALPHA_OBJ_MASK = alpha_obj_bg;
            Consts.LEVEL_ALPHA_OBJ_TRIGEER = alpha_obj;
            this.Close();
        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            needUpdate = true;
            Consts.LEVEL_ALPHA_FLAG_PHY = trackBar_PHY.Value;
            Consts.LEVEL_ALPHA_TILE_BG = trackBar_BG.Value;
            Consts.LEVEL_ALPHA_TILE_SUR = trackBar_SUR.Value;
            Consts.LEVEL_ALPHA_TILE_OBJ = trackBar_Tile_OBJ.Value;
            Consts.LEVEL_ALPHA_OBJ_MASK = trackBar_OBJ_Mask.Value;
            Consts.LEVEL_ALPHA_OBJ_TRIGEER = trackBar_OBJ.Value;
            this.Close();
        }
        private void button_Apply_Click(object sender, EventArgs e)
        {
            needUpdate = true;
            if (form_main != null)
            {
                Consts.LEVEL_ALPHA_FLAG_PHY = trackBar_PHY.Value;
                Consts.LEVEL_ALPHA_TILE_BG = trackBar_BG.Value;
                Consts.LEVEL_ALPHA_TILE_SUR = trackBar_SUR.Value;
                Consts.LEVEL_ALPHA_TILE_OBJ = trackBar_Tile_OBJ.Value;
                Consts.LEVEL_ALPHA_OBJ_MASK = trackBar_OBJ_Mask.Value;
                Consts.LEVEL_ALPHA_OBJ_TRIGEER = trackBar_OBJ.Value;
                form_main.updateMap_Refresh();
            }
        }
        private void trackBar_PHY_ValueChanged(object sender, EventArgs e)
        {
            this.label_0S.Text = (((TrackBar)sender).Value * 100 /255 ) + "%";
        }

        private void trackBar_BG_ValueChanged(object sender, EventArgs e)
        {
            this.label_1S.Text = (((TrackBar)sender).Value * 100 / 255) + "%";
        }

        private void trackBar_SUR_ValueChanged(object sender, EventArgs e)
        {
            this.label_2S.Text = (((TrackBar)sender).Value * 100 / 255) + "%";
        }
        private void trackBar_Tile_OBJ_ValueChanged(object sender, EventArgs e)
        {
            this.label_3S.Text = (((TrackBar)sender).Value * 100 / 255) + "%";
        }
        private void trackBar_OBJ_BG_ValueChanged(object sender, EventArgs e)
        {
            this.label_4S.Text = (((TrackBar)sender).Value * 100 / 255) + "%";
        }

        private void trackBar_OBJ_ValueChanged(object sender, EventArgs e)
        {
            this.label_5S.Text = (((TrackBar)sender).Value * 100 / 255) + "%";
        }




    }
}