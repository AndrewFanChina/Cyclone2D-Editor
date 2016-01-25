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
using Cyclone.mod.map;
using Cyclone.alg.util;

namespace Cyclone.mod.map
{
    public partial class SmallDialog_NewTile_Physics : Form
    {
        public SmallDialog_NewTile_Physics(String text)
        {
            InitializeComponent();
            this.Text = text;
        }
        public void initDialog()
        {
        }
        //按钮事件响应
        private void button_Cancle_Click(object sender, EventArgs e)
        {
            element = null;
            this.Close();
        }
        //单选按钮事件响应
        //private bool noEvent = false;
        //改变颜色
        private void panel_MapColor_MouseDown(object sender, MouseEventArgs e)
        {
            colorDialog_clipEditorBg.Color = panel_MapColor.BackColor;
            DialogResult dr = colorDialog_clipEditorBg.ShowDialog();
            if (dr.Equals(DialogResult.OK))
            {
                panel_MapColor.BackColor = colorDialog_clipEditorBg.Color;
            }
        }
        //获得地图物理单元
        public static TilePhysicsElement element = null;
        private static TilePhysicsManager manager = null;
        public static TilePhysicsElement createElement(TilePhysicsManager managerT)
        {
            manager = managerT;
            element = new TilePhysicsElement(manager);
            SmallDialog_NewTile_Physics dialog = new SmallDialog_NewTile_Physics("新建物理单元");
            dialog.ShowDialog();
            return element;
        }
        //设置地图单元
        public static void configElement(TilePhysicsElement elementT)
        {
            if (elementT == null)
            {
                Console.WriteLine("error in configElement");
                return;
            }
            manager = elementT.tilePhysicsManager;
            element = elementT;
            SmallDialog_NewTile_Physics dialog = new SmallDialog_NewTile_Physics("设置物理单元属性");
            dialog.numericUpDown_FlagInf.Value = element.getFlagInf();
            dialog.panel_MapColor.BackColor = GraphicsUtil.getColor(element.getColor());
            dialog.ShowDialog();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if(element!=null)
            {
                //element.setName(textBox_mapName.Text.Trim());
                //element.setTileSize((short)numericUpDown_TileW.Value, (short)numericUpDown_TileH.Value);
                //element.setMapSize((short)numericUpDown_MapW.Value, (short)numericUpDown_MapH.Value);
                //element.setColor(this.panel_MapColor.BackColor.ToArgb());
                //if (comboBox_mapType.SelectedText.Equals("正视地图"))
                //{
                //    element.setMapType(MapElement.TYPE_COMMON);
                //}
                //else if(comboBox_mapType.SelectedText.Equals("斜４５度"))
                //{
                //    element.setMapType(MapElement.TYPE_45);
                //}
                byte newValue = (byte)numericUpDown_FlagInf.Value;
                TilePhysicsElement sameValueElem = manager.getElemWithValue(newValue);
                if (sameValueElem != null && !sameValueElem.Equals(element))
                {
                    MessageBox.Show("存在相同标记的物理单元!", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                element.setValue(newValue,this.panel_MapColor.BackColor.ToArgb());
            }
            this.Close();

        }

    }
}