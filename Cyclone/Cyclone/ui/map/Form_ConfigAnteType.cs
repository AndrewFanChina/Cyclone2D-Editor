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
using Cyclone.mod.anim;
using Cyclone.mod.map;
using Cyclone.alg.util;
using Cyclone.mod.animimg;

namespace Cyclone.mod.map
{
    public partial class Form_ConfigAnteType : Form
    {
        public Form_ConfigAnteType()
        {
            InitializeComponent();
            init();
        }
        private bool noEvent = false;
        private static Antetype antetype = null;
        public static bool configAntetype(Antetype antetypeT)
        {
            antetype = antetypeT;
            Form_ConfigAnteType dialog = new Form_ConfigAnteType();
            dialog.ShowDialog();
            antetype = null;
            return true;
        }
        private void init()
        {
            if (antetype == null)
            {
                return;
            }
            noEvent = true;
            //引用设定界面刷新
            textBox_Name.Text = antetype.name;
            comboBox_folder.Items.Clear();
            AntetypesManager antetypesManager = ((AntetypesManager)antetype.GetTopParent());
            for (int i = 0; i < antetypesManager.actorsManager.Count(); i++)
            {
                MActorFolder actorsFolder = antetypesManager.actorsManager[i];
                comboBox_folder.Items.Add(actorsFolder.name);
            }
            if (antetype.Actor != null)
            {
                comboBox_folder.SelectedIndex = ((MActorFolder)(antetype.Actor.GetParent())).GetID();
                refreshActorList();
                if (antetype.Actor != null)
                {
                    comboBox_Actor.SelectedIndex = antetype.Actor.GetID();
                }
            }
            //图片映射界面刷新
            refreshImgMapList();
            //刷新角色预览
            refreshActor();
            noEvent = false;
        }
        //图片映射界面刷新
        private void refreshImgMapList()
        {
            for (int i = 0; i < antetype.imgMappingList.getElementCount(); i++)
            {
                ImageMappingElement imgMapElent = (ImageMappingElement)antetype.imgMappingList.getElement(i);
                addImgMapElement(imgMapElent);
            }
        }
        //刷新可选角色列表
        private void refreshActorList()
        {
            AntetypesManager antetypesManager = (AntetypesManager)antetype.GetTopParent();
            MActorFolder actorsFolder = antetypesManager.actorsManager[comboBox_folder.SelectedIndex];
            comboBox_Actor.Items.Clear();
            if (actorsFolder != null)
            {
                for (int i = 0; i < actorsFolder.Count(); i++)
                {
                    MActor actor = actorsFolder[i];
                    comboBox_Actor.Items.Add(actor.name);
                }
            }
        }
        //刷新角色预览
        private Bitmap imgBuffer = new Bitmap(400, 400);
        private void refreshActor()
        {
            panel_Actor.Refresh();

        }
        //切换角色文件夹时刷新
        private void comboBox_folder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noEvent || antetype==null)
            {
                return;
            }
            refreshActorList();
            antetype.Actor = null;
            this.refreshActor();
        }



        private void comboBox_Actor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noEvent || antetype == null)
            {
                return;
            }
            AntetypesManager antetypesManager = (AntetypesManager)antetype.GetTopParent();
            MActorFolder actorsFolder = antetypesManager.actorsManager[comboBox_folder.SelectedIndex];
            MActor actor = actorsFolder[comboBox_Actor.SelectedIndex];
            if (actor != null )
            {
                antetype.Actor = actor;
                antetype.name = antetype.Actor.name + "";
                textBox_Name.Text = antetype.name;
                this.refreshActor();
            }
        }

        private void textBox_Name_TextChanged(object sender, EventArgs e)
        {
            if (noEvent || antetype == null)
            {
                return;
            }
            antetype.name = textBox_Name.Text + "";
        }

        private void panel_Actor_Paint(object sender, PaintEventArgs e)
        {
            Graphics gBuffer = Graphics.FromImage(imgBuffer);
            GraphicsUtil.fillRect(gBuffer, 0, 0, imgBuffer.Width, imgBuffer.Height, 0x666666);
            if (antetype != null && antetype.Actor != null)
            {
                antetype.display(gBuffer, 0, 0, imgBuffer.Width, imgBuffer.Height,1,false);
            }
            gBuffer.Dispose();
            Graphics g = e.Graphics;
            int x = (panel_Actor.Width - imgBuffer.Width) / 2;
            int y = (panel_Actor.Height - imgBuffer.Height) / 2;
            g.DrawImage(imgBuffer, x, y);
        }

        private void button_AddMap_Click(object sender, EventArgs e)
        {
            if (noEvent || antetype==null)
            {
                return;
            }
            AntetypesManager antetypesManager = (AntetypesManager)antetype.GetTopParent();
            MImgsManager imgsManager = antetypesManager.mapsManager.form_Main.form_MAnimation.form_MImgsList.mImgsManager;
            if (imgsManager.Count() > 0)
            {
                ImageMappingElement element = new ImageMappingElement(imgsManager);
                element.ImgFrom = imgsManager[0];
                element.ImgTo = imgsManager[0];
                //添加到数据
                antetype.imgMappingList.addElement(element);
                //添加到UI
                addImgMapElement(element);
                //刷新显示
                refreshActor();
            }
        }
        //向图片映射列表中添加一条数据
        private void addImgMapElement(ImageMappingElement imgMapElement)
        {

            CheckBox checkBox = new CheckBox();
            checkBox.Width = 24;

            ComboBox boxMapFrom = new ComboBox();
            boxMapFrom.Width = (panel_ImgMap.Width - 60) / 2;
            boxMapFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            boxMapFrom.MaxDropDownItems = 20;
            addItemsToComboBox(boxMapFrom);
            boxMapFrom.SelectedIndexChanged += new EventHandler(ComboBox_SelectIndexChanged);

            ComboBox boxMapTo = new ComboBox();
            boxMapTo.Width = (panel_ImgMap.Width - 60) / 2;
            boxMapTo.DropDownStyle = ComboBoxStyle.DropDownList;
            boxMapTo.MaxDropDownItems = 20;
            addItemsToComboBox(boxMapTo);
            boxMapTo.SelectedIndexChanged += new EventHandler(ComboBox_SelectIndexChanged);


            panel_ImgMap.Controls.Add(checkBox);
            panel_ImgMap.Controls.Add(boxMapFrom);
            panel_ImgMap.Controls.Add(boxMapTo);

            if (imgMapElement != null)
            {
                noEvent = true;
                if (imgMapElement.ImgFrom != null)
                {
                    boxMapFrom.SelectedIndex = imgMapElement.ImgFrom.GetID();
                }
                if (imgMapElement.ImgTo != null)
                {
                    boxMapTo.SelectedIndex = imgMapElement.ImgTo.GetID();
                }
                if (imgMapElement.ImgFrom.image != null && imgMapElement.ImgTo.image != null && (!imgMapElement.ImgFrom.image.Size.Equals(imgMapElement.ImgTo.image.Size)))
                {
                    checkBox.Text = "！";
                }
                noEvent = false;
            }
        }
        //为下拉列表框添加图片选项
        private void addItemsToComboBox(ComboBox comboBox)
        {
            if (antetype == null || comboBox==null)
            {
                return;
            }
            AntetypesManager antetypesManager = (AntetypesManager)antetype.GetTopParent();
            MImgsManager imgsManager = antetypesManager.mapsManager.form_Main.form_MAnimation.form_MImgsList.mImgsManager;
            for (int i = 0; i < imgsManager.Count(); i++)
            {
                MImgElement imgElement = imgsManager[i];
                comboBox.Items.Add(imgElement.getShowName());
            }
            
        }
        //下拉列表框事件
        private void ComboBox_SelectIndexChanged(object sender, EventArgs e)
        {
            if (noEvent || antetype == null)
            {
                return;
            }
            if (sender is ComboBox)
            {
                noEvent = true;
                ComboBox comboBox = (ComboBox)sender;
                int ctrID=panel_ImgMap.Controls.IndexOf(comboBox);
                ImageMappingElement imgMapElement  = (ImageMappingElement)antetype.imgMappingList.getElement(ctrID / 3);
                AntetypesManager antetypesManager = (AntetypesManager)antetype.GetTopParent();
                MImgsManager imgsManager = antetypesManager.mapsManager.form_Main.form_MAnimation.form_MImgsList.mImgsManager;
                if (ctrID % 3 == 1)
                {
                    imgMapElement.ImgFrom = imgsManager[comboBox.SelectedIndex];
                }
                else if (ctrID % 3 == 2)
                {
                    imgMapElement.ImgTo = imgsManager[comboBox.SelectedIndex];
                }
                CheckBox checkBox = (CheckBox)panel_ImgMap.Controls[(ctrID / 3) * 3];
                if (imgMapElement.ImgFrom.image != null && imgMapElement.ImgTo.image != null && (!imgMapElement.ImgFrom.image.Size.Equals(imgMapElement.ImgTo.image.Size)))
                {
                    checkBox.Text = "！";
                }
                else
                {
                    checkBox.Text = "";
                }
                checkBox.Refresh();
                //刷新显示
                refreshActor();
                noEvent = false;
            }
        }
        private void button_SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < panel_ImgMap.Controls.Count/3; i++)
            {
                CheckBox checkBox = (CheckBox)panel_ImgMap.Controls[i * 3];
                checkBox.Checked = true;
            }
        }

        private void button_SelectNull_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < panel_ImgMap.Controls.Count / 3; i++)
            {
                CheckBox checkBox = (CheckBox)panel_ImgMap.Controls[i * 3];
                checkBox.Checked = false;
            }
        }

        private void button_DelMap_Click(object sender, EventArgs e)
        {
            if (noEvent || antetype == null)
            {
                return;
            }
            for (int i = 0; i < panel_ImgMap.Controls.Count / 3; i++)
            {
                CheckBox checkBox = (CheckBox)panel_ImgMap.Controls[i * 3];
                if (checkBox.Checked)
                {
                    panel_ImgMap.Controls.RemoveAt(i * 3);
                    panel_ImgMap.Controls.RemoveAt(i * 3);
                    panel_ImgMap.Controls.RemoveAt(i * 3);
                    antetype.imgMappingList.removeElement(i);
                    i--;
                }
            }
            //刷新显示
            refreshActor();
        }

        private void panel_ImgMap_SizeChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            for (int i = 0; i < panel_ImgMap.Controls.Count / 3; i++)
            {
                ComboBox comboBox1 = (ComboBox)panel_ImgMap.Controls[i * 3 + 1];
                ComboBox comboBox2 = (ComboBox)panel_ImgMap.Controls[i * 3 + 2];
                comboBox1.Width = (panel_ImgMap.Width - 60) / 2;
                comboBox2.Width = (panel_ImgMap.Width - 60) / 2;
            }
        }


    }
}