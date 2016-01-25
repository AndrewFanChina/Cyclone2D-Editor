using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Cyclone.mod;
using Cyclone.mod.anim;
using Cyclone.mod.map;

namespace Cyclone.mod.map
{
    public partial class Form_CreateAnteType : Form
    {
        public Form_CreateAnteType()
        {
            InitializeComponent();
            init();
        }
        private static List<ActorAndFolder> actors = new List<ActorAndFolder>();
        private static MActorsManager actorsManager = null;
        public static List<ActorAndFolder> getActors(MActorsManager actorsManagerT)
        {
            actorsManager = actorsManagerT;
            actors.Clear();
            Form_CreateAnteType dialog = new Form_CreateAnteType();
            dialog.ShowDialog();
            actorsManager = null;
            return actors;
        }
        private void init()
        {
            if (actorsManager == null)
            {
                return;
            }
            comboBox_folder.Items.Clear();
            for (int i = 0; i < actorsManager.Count(); i++)
            {
                MActorFolder folder = actorsManager[i];
                comboBox_folder.Items.Add(folder.name);
            }
            if (comboBox_folder.Items.Count > 0)
            {
                comboBox_folder.SelectedIndex = 0;
            }
        }
        private void refreshActorList()
        {
            if (actorsManager == null)
            {
                return;
            }
            if (comboBox_folder.Items.Count <= 0)
            {
                return;
            }
            MActorFolder folder = actorsManager[comboBox_folder.SelectedIndex];
            listBox_Actors.Items.Clear();
            for (int i = 0; i < folder.Count(); i++)
            {
                MActor actor = folder[i];
                listBox_Actors.Items.Add(actor.name);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox_Actors.Items.Count; i++)
            {
                listBox_Actors.SetItemChecked(i, true);
            }
            this.refreshMemory();
        }

        private void comboBox_folder_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshActorList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            actors.Clear();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox_Actors.Items.Count; i++)
            {
                listBox_Actors.SetItemChecked(i, false);
            }
            this.refreshMemory();
        }


        private void listBox_Actors_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshMemory();
        }

        //刷新记录
        private void refreshMemory()
        {
            actors.Clear();
            MActorFolder folder = actorsManager[comboBox_folder.SelectedIndex];
            for (int i = 0; i < folder.Count(); i++)
            {
                if (listBox_Actors.GetItemChecked(i))
                {
                    MActor actor = folder[i];
                    actors.Add(new ActorAndFolder(folder, actor));
                }
            }
        }
    }
}