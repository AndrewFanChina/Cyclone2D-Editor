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
using Cyclone.mod.anim;
using Cyclone.mod.map;
using Cyclone.mod.animimg;

namespace Cyclone.mod.map
{
    public partial class SmallDialog_Stage : Form
    {
        private static Form_Main form_Main = null;
        private bool noEvent = false;
        private SmallDialog_Stage(String text,bool create)
        {
            InitializeComponent();
            textBox_mapName.Text = element.name;
            this.Text = text;
            if (!create)
            {
                //图片映射表
                for (int i = 0; i < element.imgMappingList.getElementCount(); i++)
                {
                    ImageMappingElement imgMapElement = (ImageMappingElement)element.imgMappingList.getElement(i);
                    addImgMapElement(imgMapElement);
                }
            }
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
        //获得地图单元
        public static StageElement element = null;
        public static StageElement createStageElement(StageGroup group, Form_Main form_MainT)
        {
            form_Main = form_MainT;
            element = new StageElement(group);
            SmallDialog_Stage dialog = new SmallDialog_Stage("新建场景",true);
            dialog.ShowDialog();
            return element;
        }
        //设置地图单元
        public static void configMapElement(StageElement elementT, Form_Main form_MainT)
        {
            form_Main = form_MainT;
            if (elementT == null)
            {
                Console.WriteLine("error in configMapElement");
                return;
            }
            element = elementT;
            SmallDialog_Stage dialog = new SmallDialog_Stage("设置场景",false);
            dialog.ShowDialog();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if(element!=null)
            {
                element.name = (textBox_mapName.Text.Trim());
            }
            //图片映射表
            element.imgMappingList.removeAll();
            for (int i = 0; i < panel_ImgMap.Controls.Count / 3; i++)
            {
                ComboBox comboBoxFrom = (ComboBox)panel_ImgMap.Controls[i * 3 + 1];
                ComboBox comboBoxTo = (ComboBox)panel_ImgMap.Controls[i * 3 + 2];
                ImageMappingElement imgMapElement = new ImageMappingElement(form_Main.mapImagesManager);
                imgMapElement.ImgFrom = form_Main.mapImagesManager[comboBoxFrom.SelectedIndex];
                imgMapElement.ImgTo = form_Main.mapImagesManager[comboBoxTo.SelectedIndex];
                element.imgMappingList.addElement(imgMapElement);
            }
            this.Close();
        }

        //向图片映射列表中添加一条数据
        private void addImgMapElement(ImageMappingElement imgMapElement)
        {

            CheckBox checkBox = new CheckBox();
            checkBox.Width = 24;

            ComboBox boxMapFrom = new ComboBox();
            boxMapFrom.Width = 160;
            boxMapFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            boxMapFrom.MaxDropDownItems = 20;
            addItemsToComboBox(boxMapFrom);
            boxMapFrom.SelectedIndexChanged += new EventHandler(ComboBox_SelectIndexChanged);

            ComboBox boxMapTo = new ComboBox();
            boxMapTo.Width = 160;
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
                    checkBox.BackColor = Color.Yellow;
                }
                noEvent = false;
            }
        }
        //下拉列表框事件
        private void ComboBox_SelectIndexChanged(object sender, EventArgs e)
        {
            if (noEvent || element == null)
            {
                return;
            }
            if (sender is ComboBox)
            {
                noEvent = true;
                ComboBox comboBox = (ComboBox)sender;
                int ctrID = panel_ImgMap.Controls.IndexOf(comboBox);
                CheckBox checkBox = (CheckBox)panel_ImgMap.Controls[(ctrID / 3) * 3];
                ComboBox combBoxFrom = (ComboBox)panel_ImgMap.Controls[(ctrID / 3) * 3 + 1];
                ComboBox combBoxTo = (ComboBox)panel_ImgMap.Controls[(ctrID / 3) * 3 + 2];
                MImgElement ImgFrom = form_Main.mapImagesManager[combBoxFrom.SelectedIndex];
                MImgElement ImgTo = form_Main.mapImagesManager[combBoxTo.SelectedIndex];
                if ((ImgFrom == null || ImgTo == null) || (ImgFrom.image != null && ImgTo.image != null && (!ImgFrom.image.Size.Equals(ImgTo.image.Size))))
                {
                    checkBox.Text = "！";
                }
                else
                {
                    checkBox.Text = "";
                }
                noEvent = false;
            }
        }
        //为下拉列表框添加图片选项
        private void addItemsToComboBox(ComboBox comboBox)
        {
            if (element == null || comboBox == null)
            {
                return;
            }
            MImgsManager imgsManager = form_Main.mapImagesManager;
            for (int i = 0; i < imgsManager.Count(); i++)
            {
                MImgElement imgElement = imgsManager[i];
                comboBox.Items.Add(imgElement.getShowName());
            }

        }
        private void button_AddImgMap_Click(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            addImgMapElement(null);
        }

        private void button_DelImgMap_Click(object sender, EventArgs e)
        {
            if (noEvent || element == null)
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
                    i--;
                }
            }
        }

        private void button_SelectAllImgMap_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < panel_ImgMap.Controls.Count / 3; i++)
            {
                CheckBox checkBox = (CheckBox)panel_ImgMap.Controls[i * 3];
                checkBox.Checked = true;
            }
        }

        private void button_SelectNullImgMap_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < panel_ImgMap.Controls.Count / 3; i++)
            {
                CheckBox checkBox = (CheckBox)panel_ImgMap.Controls[i * 3];
                checkBox.Checked = false;
            }
        }

    }
}