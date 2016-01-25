using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.mod;
using Cyclone.alg;
using System.IO;
using System.Collections;
using Cyclone.mod.map;
using Cyclone.mod.util;
using Cyclone.alg.util;

namespace Cyclone.mod.misc
{
    public partial class Form_TextsManager : Form
    {
        private TextsManager textsManager;
        private bool noEvent = false;
        public Form_TextsManager(TextsManager textsManagerT)
        {
            InitializeComponent();
            this.textsManager = textsManagerT;
            initParams();
        }
        public void initParams()
        {
            updateData();
            initEditTextBox();
            refreshUsedTime();
        }
        public void releaseRes()
        {
            textsManager = null;
        }
        //刷新数据
        public void updateData()
        {
            noEvent = true;
            Console.WriteLine(textsManager.GetHashCode());
            if (textsManager.listBox == null || !textsManager.listBox.Equals(listBox_Texts))
            {
                textsManager.refreshUI(listBox_Texts);
            }
            noEvent = false;
        }
        //返回所选文字
        public TextElement getTextElement()
        {
            ShowDialog();
            return  textsManager.getElement(listBox_Texts.SelectedIndex);;
        }
        //编辑所选文字
        public TextElement editTextElement(TextElement element)
        {
            if (element != null)
            {
                listBox_Texts.SelectedIndex = element.getID();
            }
            this.ShowDialog();
            if (closedByUser && textsManager != null)
            {
                TextElement returnElement = textsManager.getElement(listBox_Texts.SelectedIndex);
                return returnElement;
            }
            return null;
        }

        //删除所选行
        private void button_delete_Click(object sender, EventArgs e)
        {
            textsManager.removeTextElementAt(listBox_Texts.SelectedIndex);
        }
        bool closedByUser = false;
        private void button_OK_Click(object sender, EventArgs e)
        {
            closedByUser = true;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Check_Click(object sender, EventArgs e)
        {
            int index = listBox_Texts.SelectedIndex;
            if (index>=0 && textsManager != null)
            {
                TextElement element = textsManager.getElement(index);
                String s = element.getUsedInfor();
                SmallDialog_ShowParagraph.showString("文字使用情况检查", s);
            }
        }

        private void button_Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "bin files (*.txt)|*.txt";
            dialog.FileName = "";
            dialog.Title = "导出文本";
            DialogResult dr = dialog.ShowDialog();
            bool bExport = (dr == DialogResult.OK);
            dialog.Dispose();
            FileStream fs = null;
            if (bExport)
            {
                String fileName = dialog.FileName;
                try
                {
                    if (File.Exists(fileName))
                    {
                        fs = File.Open(fileName, FileMode.Truncate);
                    }
                    else
                    {
                        fs = File.Open(fileName, FileMode.OpenOrCreate);
                    }
                    for (int i = 0; i < textsManager.getElementCount(); i++)
                    {
                        TextElement element = textsManager.getElement(i);
                        int usedTime = element.getUsedTime();
                        String text = element.getValue();
                        IOUtil.writeTextLine(fs, text);
                    }
                }
                catch (Exception ex1)
                {
                    Console.WriteLine(ex1.StackTrace);

                }
                finally
                {
                    if (fs != null)
                    {
                        try
                        {
                            fs.Flush();
                            fs.Close();
                            fs = null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

        }

        private void button_Import_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "bin files (*.txt)|*.txt";
            dialog.FileName = "";
            dialog.Title = "导入文本";
            DialogResult dr = dialog.ShowDialog();
            bool bImport = (dr == DialogResult.OK);
            dialog.Dispose();
            FileStream fs = null;
            if (bImport)
            {
                String fileName = dialog.FileName;
                try
                {
                    if (File.Exists(fileName))
                    {
                        fs = File.Open(fileName, FileMode.Open);
                    }
                    else
                    {
                        return;
                    }
                    ArrayList texts = IOUtil.readTextLinesGBK(fs);
                    for (int i = 0; i < textsManager.getElementCount(); i++)
                    {
                        if (i >= texts.Count)
                        {
                            break;
                        }
                        String text = (String)texts[i];
                        TextElement element = textsManager.getElement(i);
                        element.setValue(text);
                    }
                    textsManager.refreshUI();
                    textsManager.refreshUIAide();
                }
                catch (Exception ex1)
                {
                    Console.WriteLine(ex1.StackTrace);

                }
                finally
                {
                    if (fs != null)
                    {
                        try
                        {
                            fs.Close();
                            fs = null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            textsManager.refreshUI(listBox_Texts);
        }
        //向上移动文字条目
        private void moveUpTextElement()
        {
            textsManager.moveUpElement(listBox_Texts.SelectedIndex);
            refreshUsedTime();
        }
        //向下移动文字条目
        private void moveDownTextElement()
        {
            textsManager.moveDownElement(listBox_Texts.SelectedIndex);
            refreshUsedTime();
        }
        //置顶文字条目
        private void moveTopTextElement()
        {
            textsManager.moveTopElement(listBox_Texts.SelectedIndex);
            refreshUsedTime();
        }
        //置底文字条目
        private void moveBottomTextElement()
        {
            textsManager.moveBottomElement(listBox_Texts.SelectedIndex);
            refreshUsedTime();
        }
        private void button_moveUp_Click(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            moveUpTextElement();
            refreshUsedTime();
        }

        private void button_moveDown_Click(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            moveDownTextElement();
            refreshUsedTime();
        }

        private void button_moveTop_Click(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            moveTopTextElement();
            refreshUsedTime();
        }

        private void button_moveBottom_Click(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            moveBottomTextElement();
            refreshUsedTime();
        }

        private void listBox_Text_KeyDown(object sender, KeyEventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            if (e.Control)
            {
                if (e.KeyCode == Keys.Up)
                {
                    moveUpTextElement();
                }
                if (e.KeyCode == Keys.Down)
                {
                    moveDownTextElement();
                }
                e.Handled = true;
            }
            else
            {
                if (e.KeyCode == Keys.Delete)
                {
                    textsManager.removeTextElementAt(listBox_Texts.SelectedIndex);
                }
            }
        }
        //-----------------------------------文字编辑区域-----------------------------------
        TextBox editBox = null;
        //TextBox tagText = new TextBox();
        int delta = 0;
        private void initEditTextBox()
        {
            editBox = new System.Windows.Forms.TextBox();
            editBox.Location = new System.Drawing.Point(0, 0);
            editBox.Size = new System.Drawing.Size(0, 0);
            editBox.Hide();
            editBox.Text = "";
            editBox.BackColor = Color.Beige;
            editBox.Font = new Font("宋体", 15, FontStyle.Regular | FontStyle.Underline, GraphicsUnit.Pixel);
            editBox.ForeColor = Color.Blue;
            editBox.BorderStyle = BorderStyle.FixedSingle;
            editBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditOver);
            editBox.LostFocus += new System.EventHandler(this.FocusOver);
        }
        private void CreateEditBox(object sender)
        {
            int itemSelected = listBox_Texts.SelectedIndex;
            if (itemSelected < 0)
            {
                return;
            }
            Rectangle r = listBox_Texts.GetItemRectangle(itemSelected);
            string itemText = (string)listBox_Texts.Items[itemSelected];
            if (editBox != null)
            {
                editBox.Location = new System.Drawing.Point(r.X + delta, r.Y + delta);
                editBox.Size = new System.Drawing.Size(r.Width - 10, r.Height - delta);
                editBox.Show();
                listBox_Texts.Controls.Add(editBox);//AddRange(new System.Windows.Forms.Control[] { this.editBox });
                editBox.Text = itemText;
                editBox.Focus();
                editBox.SelectAll();
                //editBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditOver);
                //editBox.LostFocus += new System.EventHandler(this.FocusOver);
            }
        }

        private void FocusOver(object sender, System.EventArgs e)
        {
            int itemSelected = listBox_Texts.SelectedIndex;
            if (itemSelected >= 0)
            {
                textsManager.getElement(itemSelected).setValue(editBox.Text);
                textsManager.refreshUI_Element(itemSelected);
                //listBox_Texts.Items[itemSelected] = editBox.Text;
            }
            if (listBox_Texts.Controls.Contains(editBox))
            {
                listBox_Texts.Controls.Remove(editBox);
            }
            editBox.Hide();
        }

        private void EditOver(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int itemSelected = listBox_Texts.SelectedIndex;
            if (itemSelected >= 0)
            {
                if (e.KeyChar == (int)Keys.Enter)
                {
                    textsManager.getElement(itemSelected).setValue(editBox.Text);
                    textsManager.refreshUI_Element(itemSelected);
                    //listBox_Texts.Items[itemSelected] = editBox.Text;

                    if (listBox_Texts.Controls.Contains(editBox))
                    {
                        listBox_Texts.Controls.Remove(editBox);
                    }
                    editBox.Hide();
                    e.Handled = true;
                    listBox_Texts.Focus();
                }
            }

        }
        private void listBox_Edit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                CreateEditBox(sender);
            }
        }

        private void listBox_Edit_DoubleClick(object sender, System.EventArgs e)
        {
            CreateEditBox(sender);
        }

        private void listBox_Texts_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip_Fun.Show(listBox_Texts,new Point(e.X, e.Y));
            }
        }

        private void 添加文字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextElement newText = new TextElement(textsManager);
            newText.setValue("新添加的文本");
            textsManager.addElement(newText);
            refreshUsedTime();
        }

        private void 插入文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextElement newText = new TextElement(textsManager);
            newText.setValue("新插入的文本");
            textsManager.insertElement(newText, listBox_Texts.SelectedIndex);
            refreshUsedTime();
        }

        private void 删除字符ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textsManager.removeTextElementAt(listBox_Texts.SelectedIndex);
            refreshUsedTime();
        }

        private void 向上移动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            moveUpTextElement();
            refreshUsedTime();
        }

        private void 向下移动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            moveDownTextElement();
            refreshUsedTime();
        }

        private void 移至顶端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            moveTopTextElement();
            refreshUsedTime();
        }

        private void 移至低端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            moveBottomTextElement();
            refreshUsedTime();
        }

        private void button_Inseart_Click(object sender, EventArgs e)
        {
            TextElement newText = new TextElement(textsManager);
            newText.setValue("新插入的文本");
            textsManager.insertElement(newText, listBox_Texts.SelectedIndex);
            refreshUsedTime();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            TextElement newText = new TextElement(textsManager);
            newText.setValue("新添加的文本");
            textsManager.addElement(newText);
            refreshUsedTime();
        }

        private void button_clearSpilth_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("是否保留未使用的文本？","提醒", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.OK)
            {
                textsManager.clearSpilth(false);
            }
            else if (res == DialogResult.No)
            {
                textsManager.clearSpilth(true);
            }
            refreshUsedTime();
        }
        //刷新使用次数
        public void refreshUsedTime()
        {
            int index = listBox_Texts.SelectedIndex;
            if (index >= 0)
            {
                int time = textsManager.getElement(index).getUsedTime();
                label_UsedTime.Text = "使用次数：" + time;
                label_LineNumber.Text = "当前行号：" + index;
            }
            else
            {
                label_UsedTime.Text = "";
                label_LineNumber.Text = "";
            }
        }

        private void listBox_Texts_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshUsedTime();
        }

    }
}