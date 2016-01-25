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
    public partial class SmallDialog_NewMap : Form
    {
        TileGfxManager tileGfxManager=null;
        public SmallDialog_NewMap(String text, TileGfxManager tileGfxManagerT,bool create)
        {
            InitializeComponent();
            tileGfxManager=tileGfxManagerT;
            comboBox_Style.Items.Clear();
            for (int i = 0; i < tileGfxManager.Count(); i++)
            {
                comboBox_Style.Items.Add(tileGfxManager[i].name);
            }
            byte expFlag = 55;
            if (!create)
            {
                textBox_mapName.Text = element.getName();
                numericUpDown_TileW.Value = element.getTileW();
                numericUpDown_TileH.Value = element.getTileH();
                numericUpDown_MapW.Value = element.getMapW();
                numericUpDown_MapH.Value = element.getMapH();
                comboBox_mapType.SelectedIndex = element.getMapType() - MapElement.TYPE_COMMON;
                comboBox_Style.SelectedIndex = element.tileGfxContainer.GetID();
                comboBox_Style.Enabled = false;
                panel_MapColor.BackColor = GraphicsUtil.getColor(element.getColor());
                //地图类型
                if (element.getMapType() == MapElement.TYPE_COMMON)
                {
                    comboBox_mapType.SelectedIndex = 0;
                }
                else if (element.getMapType() == MapElement.TYPE_45)
                {
                    comboBox_mapType.SelectedIndex = 1;
                }
                //地图导出标记
                expFlag = element.getMapExpFlag();
            }
            else
            {
                comboBox_Style.Enabled = true;
                comboBox_mapType.SelectedIndex = 0;
                comboBox_Style.SelectedIndex = 0;
            }
            //地图导出标记
            
            for (int i = 0; i < checkedListBox_Level.Items.Count; i++)
            {
                if ((expFlag & (1 << i)) != 0)
                {
                    checkedListBox_Level.SetItemChecked(i, true);
                }
                else
                {
                    checkedListBox_Level.SetItemChecked(i, false);
                }
            }

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
        //获得地图单元
        public static MapElement element = null;
        public static MapElement getMapElement(MapsManager manager)
        {
            element = new MapElement(manager);
            SmallDialog_NewMap dialog = new SmallDialog_NewMap("新建地图", manager.tileGfxManager,true);
            dialog.ShowDialog();
            return element;
        }
        //设置地图单元
        public static void configMapElement(MapElement elementT)
        {
            if (elementT == null)
            {
                Console.WriteLine("error in configMapElement");
                return;
            }
            element = elementT;
            SmallDialog_NewMap dialog = new SmallDialog_NewMap("设置地图属性", element.mapsManager.tileGfxManager,false);
            dialog.ShowDialog();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if(element!=null)
            {
                element.setName(textBox_mapName.Text.Trim());
                element.setTileSize((short)numericUpDown_TileW.Value, (short)numericUpDown_TileH.Value);
                element.setMapSize((short)numericUpDown_MapW.Value, (short)numericUpDown_MapH.Value);
                element.setColor(this.panel_MapColor.BackColor.ToArgb());
                element.setMapStyle(tileGfxManager[comboBox_Style.SelectedIndex]);
                if (comboBox_mapType.SelectedIndex == 0)
                {
                    element.setMapType(MapElement.TYPE_COMMON);
                }
                else if (comboBox_mapType.SelectedIndex == 1)
                {
                    element.setMapType(MapElement.TYPE_45);
                }
                //地图导出标记
                byte expFlag = 0;
                for (int i = 0; i < checkedListBox_Level.Items.Count; i++)
                {
                    if (this.checkedListBox_Level.CheckedIndices.Contains(i))
                    {
                        expFlag |= (byte)(1 << i);
                    }
                }
                element.setMapExpFlag(expFlag);
            }
            this.Close();
        }



    }
}