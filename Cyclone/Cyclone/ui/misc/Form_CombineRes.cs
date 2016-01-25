using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.mod;
using Cyclone.alg;
using System.Collections;
using Cyclone.mod.anim;
using Cyclone.mod.map;
using Cyclone.mod.script;

namespace Cyclone.mod.misc
{
    public partial class Form_CombineRes : Form
    {
        private Form_Main currentForm = null;
        private Form_Main srcForm = null;
        private static bool updated = false;
        private ArrayList actorsID = new ArrayList();
        private ArrayList antetypesID = new ArrayList();
        private ArrayList mapsID = new ArrayList();
        private ArrayList triggersID = new ArrayList();
        private ArrayList propsID = new ArrayList();
        private Form_CombineRes(Form_Main currentFormT, Form_Main srcFormT)
        {
            InitializeComponent();
            currentForm = currentFormT;
            srcForm = srcFormT;
            updated = false;
            initParams();
        }
        private void initParams()
        {
            updateRes();
        }
        //刷新数据显示========================================================
        public void updateRes()
        {
            //载入地图
            listBox_Maps.Items.Clear();
            for (int i = 0; i < srcForm.mapsManager.getElementCount(); i++)
            {
                String text = "" + srcForm.mapsManager.getElement(i).getName();
                listBox_Maps.Items.Add(text);
            }
            listBox_Maps.Refresh();
            //载入角色原型
            ListBox_AnteType.Items.Clear();
            for (int i = 0; i < srcForm.mapsManager.antetypesManager.Count(); i++)
            {
                AntetypeFolder folder = srcForm.mapsManager.antetypesManager[i];
                for (int j = 0; j < folder.Count(); j++)
                {
                    Antetype anteType = folder[i];
                    String text = "";
                    if (anteType != null)
                    {
                        text = "[" + anteType.getFolderName() + "]" + anteType.name;
                        if (anteType.Actor == null)
                        {
                            MessageBox.Show("角色原型“" + anteType.name + "”指向的角色为空！", "警告", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        text = "错误的角色原型";
                    }
                    ListBox_AnteType.Items.Add(text);
                }
            }
            ListBox_AnteType.Refresh();
            //载入动画
            listBox_Anims.Items.Clear();
            MActorsManager actorsManager = srcForm.form_MAnimation.form_MActorList.actorsManager;
            for (int i = 0; i < actorsManager.Count(); i++)
            {
                MActorFolder folder =  actorsManager[i];
                for (int j = 0; j < folder.Count(); j++)
                {
                    MActor actor = folder[j];
                    String text = folder.name + "_" + actor.name;
                    listBox_Anims.Items.Add(text);
                }
            }
            listBox_Anims.Refresh();
            //载入触发器
            listBox_Triggers.Items.Clear();
            listBox_Triggers.Refresh();
            //载入属性表
            listBox_Props.Items.Clear();
            for (int i = 0; i < srcForm.propertyTypesManager.getElementCount(); i++)
            {
                String text = "" + ((PropertyTypeElement)srcForm.propertyTypesManager.getElement(i)).name;
                listBox_Props.Items.Add(text);
            }
            listBox_Props.Refresh();
        }
        //刷新角色原型数据状态
        public void updateAnteTypeItems()
        {
            noEvent = true;
            for (int j = 0; j < ListBox_AnteType.Items.Count; j++)
            {
                Antetype anteType = (Antetype)srcForm.mapsManager.antetypesManager.getAntetypeBySumID(j);
                if (ListBox_AnteType.GetItemCheckState(j) == CheckState.Checked)
                {
                    continue;
                }
                for (int k = 0; k < listBox_Maps.Items.Count; k++)
                {
                    MapElement map = srcForm.mapsManager.getElement(k);
                    if (listBox_Maps.GetItemCheckState(k) == CheckState.Unchecked)
                    {
                        continue;
                    }
                    if (map.usingAnteType(anteType))
                    {
                        ListBox_AnteType.SetItemCheckState(j, CheckState.Checked);
                        break;
                    }
                }
            }
            noEvent = false;
        }
        //刷新Anims数据状态
        public void updateAnimsItems()
        {
            noEvent = true;

            for (int i = 0; i < listBox_Anims.Items.Count; i++)
            {
                if (listBox_Anims.GetItemCheckState(i) == CheckState.Checked)
                {
                    continue;
                }
                MActorsManager actorsManager = srcForm.form_MAnimation.form_MActorList.actorsManager;
                MActor actor = actorsManager.getActorBySumID(i);
                bool used = false;
                for (int j = 0; j < ListBox_AnteType.Items.Count; j++)
                {
                    if (ListBox_AnteType.GetItemCheckState(j) == CheckState.Checked)
                    {
                        Antetype anteType = (Antetype)srcForm.mapsManager.antetypesManager.getAntetypeBySumID(j);
                        if (anteType.Actor != null && anteType.Actor.Equals(actor))
                        {
                            used = true;
                            break;
                        }
                    }
                }
                if (used)
                {
                    listBox_Anims.SetItemCheckState(i, CheckState.Checked);
                }
            }
            noEvent = false;
        }
        //从其它文档导入动画到当前工程
        public static bool importAnims(Form_Main currentForm, String srcPath)
        {
            Form_Main srcForm = new Form_Main(srcPath);
            srcForm.initWorld();
            Form_CombineRes dialog = new Form_CombineRes(currentForm, srcForm);
            dialog.ShowDialog();
            return updated;
        }
        //事件响应============================================================
        private void button_Cancle_Click(object sender, EventArgs e)
        {
            updated = false;
            srcForm.releaseRes();
            srcForm.Dispose();
            this.Close();
        }
        private void button_Combine_Click(object sender, EventArgs e)
        {
            //搜集需要合并的信息，准备合并资源
            actorsID.Clear();
            for (int i = 0; i < listBox_Anims.Items.Count; i++)
            {
                if (listBox_Anims.CheckedIndices.Contains(i))
                {
                    actorsID.Add(i);
                }
            }
            antetypesID.Clear();
            for (int i = 0; i < ListBox_AnteType.Items.Count; i++)
            {
                if (ListBox_AnteType.CheckedIndices.Contains(i))
                {
                    antetypesID.Add(i);
                }
            }
            mapsID.Clear();
            for (int i = 0; i < listBox_Maps.Items.Count; i++)
            {
                if (listBox_Maps.CheckedIndices.Contains(i))
                {
                    mapsID.Add(i);
                }
            }
            triggersID.Clear();
            for (int i = 0; i < listBox_Triggers.Items.Count; i++)
            {
                if (listBox_Triggers.CheckedIndices.Contains(i))
                {
                    triggersID.Add(i);
                }
            }
            propsID.Clear();
            for (int i = 0; i < listBox_Props.Items.Count; i++)
            {
                if (listBox_Props.CheckedIndices.Contains(i))
                {
                    propsID.Add(i);
                }
            }
            //进入合并
            currentForm.combineRes(
                srcForm, actorsID,antetypesID, mapsID, triggersID, propsID,
                checkBox_animImg.Checked,checkBox_mapImg.Checked,checkBox_Function.Checked,
                checkBox_Consts.Checked,checkBox_Texts.Checked,this);
            updated = true;
            srcForm.releaseRes();
            srcForm.Dispose();
            //塌陷历史记录
            currentForm.clearHistory();
            currentForm.form_MAnimation.clearHistory();
            this.Close();
        }
        public void showProcess(String s, int percent)
        {
            if (percent > 100 || percent<0)
            {
                percent = 0;
            }
            this.progressBar_Combine.Value = (progressBar_Combine.Maximum - progressBar_Combine.Minimum) * percent / 100 + progressBar_Combine.Minimum;
            this.panel_Process.Refresh();
        }
        private bool noEvent = false;
        private void listBox_Maps_SelectedValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            updateAnteTypeItems();
            updateAnimsItems();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox_Maps.Items.Count; i++)
            {
                listBox_Maps.SetItemChecked(i, true);
            }
            updateAnteTypeItems();
            updateAnimsItems();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox_Maps.Items.Count; i++)
            {
                listBox_Maps.SetItemChecked(i, false);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox_Anims.Items.Count; i++)
            {
                listBox_Anims.SetItemChecked(i, true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            noEvent = true;
            for (int i = 0; i < listBox_Anims.Items.Count; i++)
            {
                listBox_Anims.SetItemChecked(i, false);
            }
            noEvent = false;
            updateAnimsItems();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox_Triggers.Items.Count; i++)
            {
                listBox_Triggers.SetItemChecked(i, true);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox_Triggers.Items.Count; i++)
            {
                listBox_Triggers.SetItemChecked(i, false);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox_Props.Items.Count; i++)
            {
                listBox_Props.SetItemChecked(i, true);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox_Props.Items.Count; i++)
            {
                listBox_Props.SetItemChecked(i, false);
            }
        }

        private void listBox_Anims_MouseUp(object sender, MouseEventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            updateAnimsItems();

        }

        private void ListBox_AnteType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            updateAnteTypeItems();
            updateAnimsItems();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox_AnteType.Items.Count; i++)
            {
                ListBox_AnteType.SetItemChecked(i, true);
            }
            updateAnimsItems();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox_AnteType.Items.Count; i++)
            {
                ListBox_AnteType.SetItemChecked(i, false);
            }
            updateAnteTypeItems();
            updateAnimsItems();
        }

    }
}