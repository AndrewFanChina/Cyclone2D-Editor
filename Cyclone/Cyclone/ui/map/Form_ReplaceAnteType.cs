using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Cyclone.mod;
using Cyclone.alg;
using Cyclone.mod.map;
using Cyclone.alg.util;

namespace Cyclone.mod.map
{
    public partial class Form_ReplaceAnteType : Form
    {
        public Form_ReplaceAnteType()
        {
            InitializeComponent();
            init();
        }
        private static ArrayList anteTypes_current = new ArrayList();
        private static ArrayList anteTypes_replace = new ArrayList();
        private static MapElement mapElement = null;
        private static bool needUpdate=false;
        private bool noEvent = false;
        public static bool replaceAnteTypes(MapElement mapElementT)
        {
            mapElement = mapElementT;
            anteTypes_current.Clear();
            anteTypes_replace.Clear();
            Form_ReplaceAnteType dialog = new Form_ReplaceAnteType();
            dialog.ShowDialog();
            return needUpdate;
        }
        private void init()
        {
            needUpdate = false;
            if (mapElement == null)
            {
                return;
            }
            noEvent = true;
            //初始化角色原型文件夹
            comboBox_folder.Items.Clear();
            for (int i = 0; i < mapElement.mapsManager.antetypesManager.Count(); i++)
            {
                AntetypeFolder folder = mapElement.mapsManager.antetypesManager[i];
                comboBox_folder.Items.Add(folder.name);
            }
            //初始化当前角色原型列表
            anteTypes_current = mapElement.getAnteTypesUsed();
            MiscUtil.copyArrayList(anteTypes_current, anteTypes_replace);
            listBox_Actors.Items.Clear();
            Graphics g = listBox_Actors.CreateGraphics();
            float spaceLen = GraphicsUtil.getStringSizeF(g, "__________", listBox_Actors.Font).Width / 10.0F;
            for (int i = 0; i < anteTypes_current.Count; i++)
            {
                Antetype anteType = (Antetype)anteTypes_current[i];
                String name = anteType.name + "";
                String usedTime = "[" + mapElement.getTileUsedTime(anteType) + "]";
                float space = 40 - GraphicsUtil.getStringSizeF(g, usedTime, listBox_Actors.Font).Width;
                for (int j = 0; j < space / spaceLen; j++)
                {
                    usedTime += " ";
                }
                listBox_Actors.Items.Add(usedTime+name);
            }
            g.Dispose();
            noEvent = false;
        }
        private void refreshActorList()
        {
            if (mapElement == null)
            {
                return;
            }
            if (comboBox_folder.Items.Count <= 0)
            {
                return;
            }
            Object folderObj = mapElement.mapsManager.antetypesManager[comboBox_folder.SelectedIndex];
            if (folderObj == null)
            {
                return;
            }
            AntetypeFolder folder = (AntetypeFolder)folderObj;
            listBox_Replace.Items.Clear();
            for (int i = 0; i < folder.Count(); i++)
            {
                Antetype anteType = folder[i];
                listBox_Replace.Items.Add(anteType.name);
            }
            //设定焦点
            int index = listBox_Actors.SelectedIndex;
            if (index < 0 || index >= anteTypes_current.Count)
            {
                return;
            }
            if (anteTypes_replace[index] != null)
            {
                Antetype replaceAT = (Antetype)anteTypes_replace[index];
                int itemID = replaceAT.GetID();
                if (itemID >= 0)
                {
                    noEvent = true;
                    listBox_Replace.SelectedIndex = itemID;
                    noEvent = false;
                }
            }
            
        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            if (mapElement != null)
            {
                for (int i = 0; i < anteTypes_current.Count; i++)
                {
                    if (anteTypes_current[i] != null && anteTypes_replace[i] != null)
                    {
                        Antetype local = (Antetype)anteTypes_current[i];
                        Antetype replace = (Antetype)anteTypes_replace[i];
                        mapElement.replaceAnteType(local, replace);
                    }
                }
                needUpdate = true;
            }
            Close();
        }

        private void comboBox_folder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            refreshActorList();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            needUpdate = false;
            this.Close();
        }

        private void listBox_Actors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            int index = listBox_Actors.SelectedIndex;
            if (index < 0 || index >= anteTypes_current.Count || anteTypes_current[index] == null)
            {
                return;
            }
            if (anteTypes_replace[index] == null)
            {
                return;
            }
            Antetype replaceAT = (Antetype)anteTypes_replace[index];
            AntetypeFolder folder = replaceAT.getFolder();
            if (folder == null)
            {
                return;
            }
            noEvent = true;
            comboBox_folder.SelectedIndex = folder.GetID();
            refreshActorList();
            noEvent = false;
        }

        private void listBox_Replace_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            //设定焦点
            Object folderObj = mapElement.mapsManager.antetypesManager[comboBox_folder.SelectedIndex];
            if (folderObj != null)
            {
                AntetypeFolder folder = (AntetypeFolder)folderObj;
                int index = listBox_Actors.SelectedIndex;
                if (index >= 0 && index < anteTypes_current.Count)
                {
                    if (anteTypes_replace[index] != null)
                    {
                        Antetype replaceAT = (Antetype)anteTypes_replace[index];
                        int itemID = replaceAT.GetID();
                        if (itemID >= 0 && e.Index == itemID)
                        {
                            myBrush = Brushes.Red;
                        }
                    }
                }
            }

            // Draw the current item text based on the current Font and the custom brush settings.
            e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();

        }

        private void listBox_Replace_DoubleClick(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            int index = listBox_Actors.SelectedIndex;
            if (index < 0 || index >= anteTypes_current.Count)
            {
                return;
            }
            AntetypeFolder folder = mapElement.mapsManager.antetypesManager[comboBox_folder.SelectedIndex];
            if (folder == null)
            {
                return;
            }
            anteTypes_replace[index] = folder[listBox_Replace.SelectedIndex];
            listBox_Replace.Refresh();
        }


    }
}