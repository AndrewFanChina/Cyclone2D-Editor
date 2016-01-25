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
        //��ť�¼���Ӧ
        private void button_Cancle_Click(object sender, EventArgs e)
        {
            element = null;
            this.Close();
        }
        //��ѡ��ť�¼���Ӧ
        //private bool noEvent = false;
        //�ı���ɫ
        private void panel_MapColor_MouseDown(object sender, MouseEventArgs e)
        {
            colorDialog_clipEditorBg.Color = panel_MapColor.BackColor;
            DialogResult dr = colorDialog_clipEditorBg.ShowDialog();
            if (dr.Equals(DialogResult.OK))
            {
                panel_MapColor.BackColor = colorDialog_clipEditorBg.Color;
            }
        }
        //��õ�ͼ����Ԫ
        public static TilePhysicsElement element = null;
        private static TilePhysicsManager manager = null;
        public static TilePhysicsElement createElement(TilePhysicsManager managerT)
        {
            manager = managerT;
            element = new TilePhysicsElement(manager);
            SmallDialog_NewTile_Physics dialog = new SmallDialog_NewTile_Physics("�½�����Ԫ");
            dialog.ShowDialog();
            return element;
        }
        //���õ�ͼ��Ԫ
        public static void configElement(TilePhysicsElement elementT)
        {
            if (elementT == null)
            {
                Console.WriteLine("error in configElement");
                return;
            }
            manager = elementT.tilePhysicsManager;
            element = elementT;
            SmallDialog_NewTile_Physics dialog = new SmallDialog_NewTile_Physics("��������Ԫ����");
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
                //if (comboBox_mapType.SelectedText.Equals("���ӵ�ͼ"))
                //{
                //    element.setMapType(MapElement.TYPE_COMMON);
                //}
                //else if(comboBox_mapType.SelectedText.Equals("б������"))
                //{
                //    element.setMapType(MapElement.TYPE_45);
                //}
                byte newValue = (byte)numericUpDown_FlagInf.Value;
                TilePhysicsElement sameValueElem = manager.getElemWithValue(newValue);
                if (sameValueElem != null && !sameValueElem.Equals(element))
                {
                    MessageBox.Show("������ͬ��ǵ�����Ԫ!", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                element.setValue(newValue,this.panel_MapColor.BackColor.ToArgb());
            }
            this.Close();

        }

    }
}