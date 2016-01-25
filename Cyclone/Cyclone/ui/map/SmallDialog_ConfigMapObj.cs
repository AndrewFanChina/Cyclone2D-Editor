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
using System.Collections;
using Cyclone.mod.anim;
using Cyclone.alg.util;
using Cyclone.alg.math;
using Cyclone.Cyclone.ui.exctl;

namespace Cyclone.mod.map
{
    public partial class SmallDialog_ConfigMapObj : Form, MNodeUI<TileObjKeyValue>
    {
        public SmallDialog_ConfigMapObj(String text)
        {
            InitializeComponent();
            this.Text = text;
        }
        //单选按钮事件响应
        private bool noEvent = false;
        //获得地图块对象单元
        public static TileObjectElement element = null;
        private static Form_Main form_main = null;
        //设置地图块对象单元
        public static void configElement(Form_Main form_mainT,TileObjectElement elementT, String title)
        {
            form_main = form_mainT;
            if (elementT == null)
            {
                Console.WriteLine("error in configElement");
                return;
            }
            element = elementT;
            SmallDialog_ConfigMapObj dialog = new SmallDialog_ConfigMapObj(title);
            element.keyValueManager.MNodeUI = dialog;
            dialog.showParamsList();
            dialog.ShowDialog();
        }
        //配置时显示参数列表
        ArrayList paramsBoxs = new ArrayList();
        private void showParamsList()
        {
            noEvent = true;
            if (element != null)
            {
                //AT_Name
                if (element.antetype != null)
                {
                    textBox_ATName.Text = element.antetype.name + "[" + element.antetype.getFolderName() + "]";
                }
                //NpcID
                numericUpDown_ID.Value = element.NpcID;
                comboBox_ActionID.Items.Clear();
                comboBox_ActionID.SelectedIndex = -1;
                numericUpDown_FrameID.Value = 0;
                if (element.antetype != null&&element.antetype.Actor!=null)
                {
                    //actionID
                    MActor actor=element.antetype.Actor;
                    for (int i = 0; i < actor.Count(); i++)
                    {
                        comboBox_ActionID.Items.Add(actor[i].name);
                    }
                    if (element.actionID >= 0 && element.actionID < comboBox_ActionID.Items.Count)
                    {
                        comboBox_ActionID.SelectedIndex = element.actionID;
                    }
                    else
                    {
                        comboBox_ActionID.SelectedIndex = -1;
                    }
                    if (element.startTime >= numericUpDown_FrameID.Minimum && element.startTime <= numericUpDown_FrameID.Maximum)
                    {
                        numericUpDown_FrameID.Value = element.startTime;
                    }
                }
                //isActive
                checkBox_active.Checked = element.isVisible;
                //keyValue
                panel_SelfLeft.Controls.Clear();
                panel_SelfRight.Controls.Clear();
                for (int i = 0; i < element.keyValueManager.Count(); i++)
                {
                    TileObjKeyValue item = element.keyValueManager[i];
                    TextBoxEX textBoxLeft = createNewTextBox(item.strKey);
                    panel_SelfLeft.Controls.Add(textBoxLeft);

                    TextBoxEX textBoxRight = createNewTextBox(item.strValue);
                    panel_SelfRight.Controls.Add(textBoxRight);
                }
            }
            noEvent = false;
        }
        //保存调整
        private void saveModify(bool update)
        {
            if (element != null)
            {
                element.NpcID = (short)numericUpDown_ID.Value;
                element.actionID = (short)comboBox_ActionID.SelectedIndex;
                element.startTime = (short)numericUpDown_FrameID.Value;
                element.isVisible = checkBox_active.Checked;
                if (update)
                {
                    element.mapTileElement.exploreDirtyRect();
                    form_main.updateMap();
                }
            }
        }
        //按钮事件响应
        private void button_OK_Click(object sender, EventArgs e)
        {
            form_main = null;
            element = null;
            this.Close();
        }
        private void comboBox_ActionID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            saveModify(true);

        }

        private void numericUpDown_FrameID_ValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            saveModify(true);

        }

        private void numericUpDown_ID_ValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            saveModify(false);
        }

        private void checkBox_active_CheckedChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            saveModify(false);
        }

        private void SmallDialog_MapEventObjConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            element.keyValueManager.MNodeUI = null;
            form_main = null;
            element = null;
        }
        private TextBoxEX createNewTextBox(String value)
        {
            TextBoxEX textBox = new TextBoxEX();
            textBox.Text = value;
            textBox.Width = getBoxWidth();
            textBox.MouseDown += new MouseEventHandler(textBoxCommon_MouseDown);
            textBox.Enter += new EventHandler(textBoxCommon_Enter);
            textBox.TextChanged += new EventHandler(textBoxCommon_TextChanged);
            textBox.BackColor = colorUnSelected;
            return textBox;
        }

        private void textBoxCommon_TextChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            TextBoxEX textBox = ((TextBoxEX)(sender));
            int id = textBox.Parent.Controls.IndexOf(textBox);
            TextBoxEX textBoxL = (TextBoxEX)panel_SelfLeft.Controls[id];
            TextBoxEX textBoxR = (TextBoxEX)panel_SelfRight.Controls[id];
            element.keyValueManager[id].setKeyValue(textBoxL.Text, textBoxR.Text);
        }
        private void textBoxCommon_MouseDown(object sender, MouseEventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip_SelfDefine.Show((Control)sender, e.X, e.Y);
            }
        }
        private void textBoxCommon_Enter(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            TextBoxEX textBox = ((TextBoxEX)(sender));
            int id = textBox.Parent.Controls.IndexOf(textBox);
            element.keyValueManager.MNodeUI.SetSelectedItem(id);
        }
        //获取文本框应显示的宽度
        private int getBoxWidth()
        {
            return panel_SelfLeft.Width - (panel_SelfLeft.Padding.Left + panel_SelfLeft.Padding.Right);
        }
        //获取当前被选中的文本框所在行
        private Color colorSelected = Color.White;
        private Color colorUnSelected = GraphicsUtil.getColor(0xE6E6E6);
        private int getSKVIndex()
        {
            int id = -1;
            for (int i = 0; i < panel_SelfLeft.Controls.Count; i++)
            {
                TextBoxEX textBoxLeft = (TextBoxEX)panel_SelfLeft.Controls[i];
                if (textBoxLeft.BackColor == colorSelected)
                {
                    id = i;
                    break;
                }
            }
            return id;
        }
        #region MNodeUI<TileObjKeyValue> 成员

        public void AddItem(TileObjKeyValue item)
        {
            if (noEvent)
            {
                return;
            }
            TextBoxEX textBoxLeft = createNewTextBox(item.strKey);
            panel_SelfLeft.Controls.Add(textBoxLeft);

            TextBoxEX textBoxRight = createNewTextBox(item.strValue);
            panel_SelfRight.Controls.Add(textBoxRight);
        }

        public void SetItem(int index, TileObjKeyValue item)
        {
            if (noEvent)
            {
                return;
            }
            TextBoxEX textBoxLeft = (TextBoxEX)panel_SelfLeft.Controls[item.GetID()];
            TextBoxEX textBoxRight = (TextBoxEX)panel_SelfRight.Controls[item.GetID()];
            noEvent = true;
            textBoxLeft.Text = item.strKey;
            textBoxRight.Text = item.strValue;
            noEvent = false;
        }

        public void UpdateItem(int index)
        {
            if (noEvent || index < 0)
            {
                return;
            }
            TextBoxEX textBoxLeft = (TextBoxEX)panel_SelfLeft.Controls[index];
            TextBoxEX textBoxRight = (TextBoxEX)panel_SelfRight.Controls[index];
            noEvent = true;
            textBoxLeft.Text = element.keyValueManager[index].strKey;
            textBoxRight.Text = element.keyValueManager[index].strValue;
            noEvent = false;
        }

        public void SetSelectedItem(int index)
        {
            if (noEvent)
            {
                return;
            }
            for (int i = 0; i < panel_SelfLeft.Controls.Count; i++)
            {
                TextBoxEX textBoxLeftI = (TextBoxEX)panel_SelfLeft.Controls[i];
                TextBoxEX textBoxRightI = (TextBoxEX)panel_SelfRight.Controls[i];
                textBoxLeftI.BackColor = colorUnSelected;
                textBoxRightI.BackColor = colorUnSelected;
            }
            if (index >= 0)
            {
                TextBoxEX textBoxLeft = (TextBoxEX)panel_SelfLeft.Controls[index];
                TextBoxEX textBoxRight = (TextBoxEX)panel_SelfRight.Controls[index];
                textBoxLeft.BackColor = colorSelected;
                textBoxRight.BackColor = colorSelected;
            }
        }

        public void InsertItem(int index, TileObjKeyValue item)
        {
            if (noEvent)
            {
                return;
            }
            TextBoxEX textBoxLeft = createNewTextBox(item.strKey);
            panel_SelfLeft.Controls.Add(textBoxLeft);
            panel_SelfLeft.Controls.SetChildIndex(panel_SelfLeft.Controls[panel_SelfLeft.Controls.Count - 1], index);

            TextBoxEX textBoxRight = createNewTextBox(item.strValue);
            panel_SelfRight.Controls.Add(textBoxRight);
            panel_SelfRight.Controls.SetChildIndex(panel_SelfRight.Controls[panel_SelfRight.Controls.Count - 1], index);
        }

        public void RemoveItemAt(int index)
        {
            if (noEvent)
            {
                return;
            }
            panel_SelfLeft.Controls.RemoveAt(index);
            panel_SelfRight.Controls.RemoveAt(index);
        }

        public void ClearItems()
        {
            if (noEvent)
            {
                return;
            }
            panel_SelfLeft.Controls.Clear();
            panel_SelfRight.Controls.Clear();
        }

        #endregion

        private void panel_SelfRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip_SelfDefine.Show((Control)sender, e.X, e.Y);
            }
        }

        private void 添加单元ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileObjKeyValue newInstance = new TileObjKeyValue(element.keyValueManager);
            element.keyValueManager.Add(newInstance);
        }

        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            element.keyValueManager.MoveUpElement(getSKVIndex());
        }
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            element.keyValueManager.MoveDownElement(getSKVIndex());
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            element.keyValueManager.RemoveAt(getSKVIndex());
        }



    }
}