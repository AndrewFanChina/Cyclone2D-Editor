using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Cyclone.alg;
using System.Collections;
using Cyclone.mod;
using Cyclone.alg.image;
using Cyclone.alg.math;
using Cyclone.alg.util;

namespace Cyclone.mod.image.pallete
{
    public partial class Form_Pallete : Form
    {
        Bitmap imgOrg;
        Bitmap imgEdit;
        Cursor handCursor, addCursor, addDelCursor, strawCursor,defaultCursor;
        byte[] imgDataOrg;
        byte[] imgDataEdit;
        byte[] palDataOrg;
        byte[] palDataEdit;//ChunkLen,PLTE,{data0,data1,...,CRC}，包含数据和CRC
        public int transID=-1;//透明色编号
        int widthOrg, heightOrg;
        int widthZoom, heightZoom;
        int[] zoom = { 1, 2,4, 6, 8,10, 12,14, 16 };
        int zoomLevel;

        //映射表
        public ColorTable srcColorTable = new ColorTable();
        public ColorTable destColorTable = new ColorTable();
        public ColorTable destColorTableBak = new ColorTable();
        //当前选择的所有颜色ID
        public ArrayList currentColorIDs = new ArrayList();
        public Form_Pallete()
        {
            InitializeComponent();
            srcColorTable.registerPicBox(pictureBox_srcTable);
            destColorTable.registerPicBox(pictureBox_destTable);
            handCursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.hand.ico"));
            addCursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursorAdd.ico"));
            addDelCursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursorAddDel.ico"));
            strawCursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursorStraw.ico"));
            defaultCursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.cursorDefault.ico"));
            panelViewOrg.Cursor = defaultCursor;
            panelViewEdit.Cursor = defaultCursor;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            panelViewOrg.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panelView_MouseWheel);
            panelViewEdit.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panelView_MouseWheel);
            //panelViewOrg.Visible = false;
            //panelViewEdit.Visible = false;
            //splitContainerView.Enabled = false;
            色相饱和度ToolStripMenuItem.Enabled = false;
            色彩平衡ToolStripMenuItem.Enabled = false;
            载入映射表ToolStripMenuItem.Enabled = false;
            保存映射表ToolStripMenuItem.Enabled = false;
            复位映射表ToolStripMenuItem.Enabled = false;
            导出调色板ToolStripMenuItem.Enabled = false;
            toolStripButtonOpenPmt.Enabled = false;
            toolStripButtonSavePmt.Enabled = false;
            toolStripComboBoxZoom.Enabled = false;
            toolStripButtonZoomIn.Enabled = false;
            toolStripButtonZoomOut.Enabled = false;
            toolStripButtonReset.Enabled = false;
            toolStripButton_ColorBalance.Enabled = false;
            toolStripButton_HSB.Enabled = false;
            tsb_resetPos.Enabled = false;
            //刷新
            srcColorTable.updateUI(currentColorIDs);
            destColorTable.updateUI(currentColorIDs);
        }

        private void toolStripButtonOpenPNG_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.CheckFileExists = true;
            dlg.Filter = "PNG文件 (*.png)|*.png";
            dlg.DefaultExt = "*.png";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgDataOrg = IOUtil.ReadFile(dlg.FileName);

                MemoryStream ms = new MemoryStream(imgDataOrg);
                imgOrg = new Bitmap(ms);
                ms.Close();

                imgDataEdit = new byte[imgDataOrg.Length];
                imgDataOrg.CopyTo(imgDataEdit, 0);
                
                MemoryStream msEdit = new MemoryStream(imgDataEdit);
                imgEdit = new Bitmap(msEdit);
                msEdit.Close();


                palDataOrg = GraphicsUtil.getPngPal(imgDataOrg);
                if (palDataOrg == null)
                {
                    MessageBox.Show("无法读取调色板信息，文件可能不是PNG_8格式", "错误");
                    return;
                }

                palDataEdit = new byte[palDataOrg.Length];
                palDataOrg.CopyTo(palDataEdit, 0);

                widthOrg = imgOrg.Width;
                heightOrg = imgOrg.Height;

                widthZoom = widthOrg;
                heightZoom = heightOrg;

                toolStripStatusLabelSize.Text = widthOrg + "x" + heightOrg;
                //panelViewOrg.Visible = true;
                //panelViewEdit.Visible = true;
                //splitContainerView.Enabled = true;
                色相饱和度ToolStripMenuItem.Enabled = true;
                色彩平衡ToolStripMenuItem.Enabled = true;
                载入映射表ToolStripMenuItem.Enabled = true;
                保存映射表ToolStripMenuItem.Enabled = true;
                复位映射表ToolStripMenuItem.Enabled = true;
                导出调色板ToolStripMenuItem.Enabled = true;
                toolStripButtonOpenPmt.Enabled = true;
                toolStripButtonSavePmt.Enabled = true;
                toolStripComboBoxZoom.Enabled = true;
                toolStripButtonZoomIn.Enabled = true;
                toolStripButtonZoomOut.Enabled = true;
                toolStripButtonReset.Enabled = true;
                toolStripButton_ColorBalance.Enabled = true;
                toolStripButton_HSB.Enabled = true;
                tsb_resetPos.Enabled = true;
                zoomLevel = 0;
                toolStripComboBoxZoom.SelectedIndex = zoomLevel;

                //刷新映射表
                transID = GraphicsUtil.getPngTransID(imgDataOrg);
                ResetMappingTable(palDataEdit, transID);
                //显示图片
                ShowImage(0);
            }
        }
        //复位映射表
        public void ResetMappingTable(byte[] palData,int transID)
        {
            srcColorTable.updateFromPal(palData, transID);
            destColorTable.updateFromPal(palData, transID);
            srcColorTable.updateUI(currentColorIDs);
            destColorTable.updateUI(currentColorIDs);
        }


        //导出调色板
        private void exportPal()
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "调色板文件 (*.pal)|*.pal";
            dlg.DefaultExt = "*.pal";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(dlg.FileName))
                {
                    File.SetAttributes(dlg.FileName, FileAttributes.Normal);
                }
                FileStream fs = new FileStream(dlg.FileName, FileMode.Create);
                applyMappingTable(transID);
                fs.Write(palDataEdit, 0, palDataEdit.Length);
                fs.Close();
            }
        }
        //编辑图片
        private void EditImage()
        {
            //应用映射表
            applyMappingTable(transID);
            //更新CRC
            GraphicsUtil.updatePalCrc(palDataEdit);
            //生成新图片
            imgDataEdit = GraphicsUtil.changePalData(imgDataOrg, palDataEdit);
            MemoryStream ms = new MemoryStream(imgDataEdit);
            imgEdit = new Bitmap(ms);
            ms.Close();
        }
        //应用映射表
        private void applyMappingTable(int ignoreID)
        {
            palDataOrg.CopyTo(palDataEdit, 0);
            GraphicsUtil.applyPmt(palDataEdit, srcColorTable, destColorTable, ignoreID);
        }
        //显示图片
        int offsetX = 0;//图片中心距离画板中心的偏移
        int offsetY = 0;
        Bitmap buffLeft, buffRight;
        public void ShowImage(int refreshSide)
        {
            int zoomLevelValue = zoom[zoomLevel];
            if (refreshSide <= 0&&imgOrg != null)
            {
                if (buffLeft == null)
                {
                    buffLeft = new Bitmap(panelViewOrg.Width, panelViewOrg.Height);
                }
                else if (buffLeft.Width < panelViewOrg.Width || buffLeft.Height < panelViewOrg.Height)
                {
                    int wOld = buffLeft.Width;
                    int hOld = buffLeft.Height;
                    buffLeft.Dispose();
                    buffLeft = new Bitmap(Math.Max(panelViewOrg.Width, wOld), Math.Max(panelViewOrg.Height, hOld));
                }
                Graphics gOrg = Graphics.FromImage(buffLeft);
                gOrg.Clear(Color.Transparent);
                GraphicsUtil.drawClip(gOrg, imgOrg, panelViewOrg.Width / 2 + offsetX * zoomLevelValue - (imgOrg.Width / 2) * zoomLevelValue,
                panelViewOrg.Height / 2 + offsetY * zoomLevelValue - (imgOrg.Height / 2) * zoomLevelValue, 0, 0, imgOrg.Width * zoomLevelValue,
                imgOrg.Height * zoomLevelValue, 0, zoomLevelValue);
                gOrg.Dispose();
                if (panelViewOrg.Image == null || !panelViewOrg.Image.Equals(buffLeft))
                {
                    panelViewOrg.Image = buffLeft;
                }
                else
                {
                    panelViewOrg.Refresh();
                }
            }
            if (refreshSide >= 0 && imgEdit!=null)
            {
                if (buffRight == null)
                {
                    buffRight = new Bitmap(panelViewEdit.Width, panelViewEdit.Height);
                }
                else if (buffRight.Width < panelViewEdit.Width || buffRight.Height < panelViewEdit.Height)
                {
                    int wOld = buffRight.Width;
                    int hOld = buffRight.Height;
                    buffRight.Dispose();
                    buffRight = new Bitmap(Math.Max(panelViewEdit.Width, wOld), Math.Max(panelViewEdit.Height, hOld));
                }
                Graphics gEdit = Graphics.FromImage(buffRight);
                gEdit.Clear(Color.Transparent);
                GraphicsUtil.drawClip(gEdit, imgEdit, panelViewEdit.Width / 2 + offsetX * zoomLevelValue - (imgEdit.Width / 2) * zoomLevelValue,
                panelViewEdit.Height / 2 + offsetY * zoomLevelValue - (imgEdit.Height / 2) * zoomLevelValue, 0, 0, imgEdit.Width * zoomLevelValue,
                imgEdit.Height * zoomLevelValue, 0, zoomLevelValue);
                gEdit.Dispose();
                if (panelViewEdit.Image == null || !panelViewEdit.Image.Equals(buffRight))
                {
                    panelViewEdit.Image = buffRight;
                }
                else
                {
                    panelViewEdit.Refresh();
                }
            }
    
        }
        //当调整色彩平衡时刷新
        public void updateInColorBalance()
        {
            destColorTable.updateUI(currentColorIDs);
            EditImage();
            ShowImage(1);
        }
        private void ZoomIn()
        {
            if (zoomLevel < zoom.Length - 1)
            {
                zoomLevel++;
            }

            toolStripComboBoxZoom.SelectedIndex = zoomLevel;
            ShowImage(0);
        }

        private void ZoomOut()
        {
            if (zoomLevel > 0)
            {
                zoomLevel--;
            }

            toolStripComboBoxZoom.SelectedIndex = zoomLevel;
            ShowImage(0);
        }

        //选择颜色
        private void chooseColor(object sender, MouseEventArgs e)
        {
            int x = e.X - ((PictureBox)sender).Left;
            int y = e.Y - ((PictureBox)sender).Top;
            int id = srcColorTable.getIDByPosition(x, y);
            if (id < 0)
            {
                if (ModifierKeys != Keys.Shift && ModifierKeys != Keys.Control)
                {
                    currentColorIDs.Clear();
                }
            }
            else
            {
                if (ModifierKeys == Keys.Control)
                {
                    if (currentColorIDs.Contains(id))
                    {
                        currentColorIDs.Remove(id);
                    }
                    else
                    {
                        currentColorIDs.Add(id);
                    }
                }
                else if (ModifierKeys == Keys.Shift)
                {
                    if (currentColorIDs.Contains(id))
                    {
                        for (int i = id; i < srcColorTable.getColorCount(); i++)
                        {
                            if (currentColorIDs.Contains(i))
                            {
                                currentColorIDs.Remove(i);
                            }
                        }
                    }
                    else
                    {
                        if (currentColorIDs.Count > 0)
                        {
                            int idCurrent = (int)currentColorIDs[currentColorIDs.Count - 1];
                            for (int i = Math.Min(idCurrent, id); i <= Math.Max(idCurrent, id); i++)
                            {
                                if (i == idCurrent)
                                {
                                    continue;
                                }
                                if (!currentColorIDs.Contains(i))
                                {
                                    currentColorIDs.Add(i);
                                }
                                else
                                {
                                    currentColorIDs.Remove(i);
                                }
                            }
                        }
                        else
                        {
                            currentColorIDs.Clear();
                            currentColorIDs.Add(id);
                        }

                    }
                }
                else
                {
                    currentColorIDs.Clear();
                    currentColorIDs.Add(id);
                }
            }
            //刷新容器
            srcColorTable.updateUI(currentColorIDs);
            destColorTable.updateUI(currentColorIDs);
            refreshLabel();

        }
        //刷新显示标签
        private void refreshLabel()
        {
            //标签
            if (currentColorIDs.Count == 1)
            {
                int color = srcColorTable.getColor((int)currentColorIDs[0]);
                String content = "0x" + MathUtil.convertHexString(color);
                textBox_SrcColor.Text = content;

                color = destColorTable.getColor((int)currentColorIDs[0]);
                content = "0x" + MathUtil.convertHexString(color);
                textBox_DestColor.Text = content;
            }
            else
            {
                textBox_SrcColor.Text = "颜色数:" + currentColorIDs.Count;
                textBox_DestColor.Text = "颜色数:" + currentColorIDs.Count;
            }
        }
        //替换颜色
        private void ReplaceColor(int color)
        {
            if (currentColorIDs.Count == 1)
            {
                int id=(int)currentColorIDs[0];
                destColorTable.setColor(id,color);
                destColorTable.updateUI(currentColorIDs);
                EditImage();
                ShowImage(1);
            }

        }
        //编辑颜色
        private void editColor(int color)
        {
            if (color == 0xFFFFFF || color == 0x0)
            {
                return;
            }
            FormRGB dlg = new FormRGB(color);
            dlg.EditImage += new DelegateRGB(dlg_EditImage);
            dlg.ShowDialog();
        }
        //保存映射表
        private void savePalMappingTable()
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "调色板映射表文件 (*.pmt)|*.pmt";
            dlg.DefaultExt = "*.pmt";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(dlg.FileName))
                {
                    File.SetAttributes(dlg.FileName, FileAttributes.Normal);
                }
                FileStream fs = new FileStream(dlg.FileName, FileMode.Create);
                srcColorTable.writeObject(fs);
                destColorTable.writeObject(fs);
                fs.Close();
            }
        }
        //读入映射表
        private void loadPalMappingTable()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.CheckFileExists = true;
            dlg.Filter = "调色板映射表文件 (*.pmt)|*.pmt";
            dlg.DefaultExt = "*.pmt";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open);
                ColorTable srcColorTableNew = new ColorTable();
                ColorTable destColorTableNew = new ColorTable();
                srcColorTableNew.readObject(fs,false);
                destColorTableNew.readObject(fs, false);
                fs.Close();
                //处理
                ArrayList modifiedColors = new ArrayList();
                for (int i = 0; i < srcColorTableNew.getColorCount(); i++)
                {
                    int color = (int)srcColorTableNew.getColor(i);
                    for (int j = 0; j < srcColorTable.getColorCount(); j++)
                    {
                        if (modifiedColors.Contains(j))
                        {
                            continue;
                        }
                        int colorJ = (int)srcColorTable.getColor(j);
                        if (colorJ == color)
                        {
                            int mappedColor = (int)destColorTableNew.getColor(i);
                            destColorTable.setColor(j,mappedColor);
                            modifiedColors.Add(j);
                        }
                    }
                }
                srcColorTableNew = null;
                destColorTableNew = null;
                modifiedColors.Clear();
                modifiedColors = null;
                //重新显示
                EditImage();
                ShowImage(1);
                srcColorTable.updateUI(currentColorIDs);
                destColorTable.updateUI(currentColorIDs);
            }
        }
        //色相、饱和度调整
        public void dlg_RateImage(int hOffset, int sOffset, int bOffset)
        {
            int R, G, B;
            ColorDialog colorDialog = new ColorDialog();
            int oldColor;
            int newColor;
            if (currentColorIDs.Count == 0)
            {
                int count = destColorTableBak.getColorCount();
                for (int i = 0; i < count; i++)
                {
                    oldColor = destColorTableBak.getColor(i);
                    if (i == transID)
                    {
                        continue;//略过透明色
                    }
                    //当前RGB
                    R = (oldColor >> 16) & 0xFF;
                    G = (oldColor >> 8) & 0xFF;
                    B = (oldColor) & 0xFF;

                    //转换到HSB
                    double[] hsb = GraphicsUtil.RGBtoHSB(R, G, B);
                    hsb[0] = (hsb[0] + hOffset / 360.0f) % 1.0f;
                    if (sOffset > 0)
                    {
                        hsb[1] = hsb[1] + (1 - hsb[1]) * sOffset / 100.0f;
                    }
                    else if (sOffset < 0)
                    {
                        hsb[1] = hsb[1] + hsb[1] * (sOffset) / 100.0f;
                    }
                    hsb[1] = MathUtil.limitNumber(hsb[1], 0.0D, 1.0D);
                    if (bOffset > 0)
                    {
                        hsb[2] = hsb[2] + (1 - hsb[2]) * bOffset / 100.0f;
                    }
                    else if (bOffset < 0)
                    {
                        hsb[2] = hsb[2] + hsb[2] * (bOffset) / 100.0f;
                    }
                    hsb[2] = MathUtil.limitNumber(hsb[2], 0.0f, 1.0f);
                    //设置新颜色
                    newColor = GraphicsUtil.HSBtoRGB(hsb[0], hsb[1], hsb[2]);
                    destColorTable.setColor(i, newColor);

                    //destColorTable.setColor(i, GetColor(oldColor,hOffset,sOffset,bOffset));
                }
            }
            else
            {
                for (int i = 0; i < currentColorIDs.Count; i++)
                {
                    int id = (int)currentColorIDs[i];
                    oldColor = destColorTableBak.getColor(id);
                    if (id ==transID)
                    {
                        continue;//略过透明色
                    }
                    //当前RGB
                    R = (oldColor >> 16) & 0xFF;
                    G = (oldColor >> 8) & 0xFF;
                    B = (oldColor) & 0xFF;

                    //转换到HSB
                    double[] hsb = GraphicsUtil.RGBtoHSB(R, G, B);
                    hsb[0] = (hsb[0] + hOffset / 360.0f) % 1.0f;
                    if (sOffset > 0)
                    {
                        hsb[1] = hsb[1] + (1 - hsb[1]) * sOffset / 100.0f;
                    }
                    else if (sOffset < 0)
                    {
                        hsb[1] = hsb[1] + hsb[1] * (sOffset) / 100.0f;
                    }
                    hsb[1] = MathUtil.limitNumber(hsb[1], 0.0D, 1.0D);
                    if (bOffset > 0)
                    {
                        hsb[2] = hsb[2] + (1 - hsb[2]) * bOffset / 100.0f;
                    }
                    else if (bOffset < 0)
                    {
                        hsb[2] = hsb[2] + hsb[2] * (bOffset) / 100.0f;
                    }
                    hsb[2] = MathUtil.limitNumber(hsb[2], 0.0f, 1.0f);
                    //设置新颜色
                    newColor = GraphicsUtil.HSBtoRGB(hsb[0], hsb[1], hsb[2]);
                    destColorTable.setColor(id, newColor);
                    Console.WriteLine(MathUtil.convertHexString(oldColor) + "->" + MathUtil.convertHexString(newColor));

                    //destColorTable.setColor(i, GetColor(oldColor, hOffset, sOffset, bOffset));
                }
            }

            //重新显示
            EditImage();
            ShowImage(1);
            //srcColorTable.updateUI(currentColorIDs);
            destColorTable.updateUI(currentColorIDs);
        }
        void ColorBalance()
        {
            destColorTable.copyTo(destColorTableBak);
            FormColorBalance dlg = new FormColorBalance(this);
            dlg.ShowDialog();
        }
        private void dealHSB()
        {
            destColorTable.copyTo(destColorTableBak);
            FormHSB dlg = new FormHSB();
            dlg.RateImage += new DelegateHSB(dlg_RateImage);
            dlg.ShowDialog();
        }
        //事件处理--------------------------------------------------------------------------------------------------------------------

        public void dlg_EditImage(int color)
        {
            ReplaceColor(color);
        }
        private void 复位调色板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonReset_Click(sender, e);
        }

        private void 色彩平衡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dealHSB();
        }

        private void 载入调色板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonOpenPal_Click(sender, e);
        }

        private void 保存调色板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonSavePal_Click(sender, e);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_srcTable_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            ((Control)sender).Focus();
            if (e.Button == MouseButtons.Left)
            {
                chooseColor(sender, e);
            }
        }

        private void pictureBox_srcTable_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void pictureBox_destTable_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            ((Control)sender).Focus();
            if (e.Button == MouseButtons.Left)
            {
                chooseColor(sender, e);
            }
        }

        private void 保存映射表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savePalMappingTable();
        }

        private void 载入映射表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadPalMappingTable();
        }

        private void pictureBox_srcTable_DoubleClick(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Control || ModifierKeys == Keys.Alt || ModifierKeys == Keys.Shift)
            {
                return;
            }
            if (currentColorIDs.Count != 1)
            {
                return;
            }
            int id = (int)currentColorIDs[0];
            int color = (int)destColorTable.getColor(id);
            this.editColor(color);
        }

        private void pictureBox_destTable_DoubleClick(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Control || ModifierKeys == Keys.Alt || ModifierKeys == Keys.Shift)
            {
                return;
            }
            if (currentColorIDs.Count != 1)
            {
                return;
            }
            int id = (int)currentColorIDs[0];
            int color = (int)destColorTable.getColor(id);
            this.editColor(color);
        }


        private void 复位映射表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //复位映射表
            DialogResult res = MessageBox.Show("复位映射表将丢失当前映射信息", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                ResetMappingTable(palDataOrg, transID);
                EditImage();
                ShowImage(1);
            }
        }
        private void toolStripButtonSavePal_Click(object sender, EventArgs e)
        {
            savePalMappingTable();
        }


        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void toolStripButtonReset_Click(object sender, EventArgs e)
        {
            //复位映射表
            DialogResult res = MessageBox.Show("复位映射表将丢失当前映射信息", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                ResetMappingTable(palDataOrg, transID);
                EditImage();
                ShowImage(1);
            }
        }

        private void toolStripButtonRate_Click(object sender, EventArgs e)
        {
            ColorBalance();
        }



        private void toolStripMenuItemOpenPNG_Click(object sender, EventArgs e)
        {
            toolStripButtonOpenPNG_Click(sender, e);
        }

        private void toolStripMenuItemOpenPal_Click(object sender, EventArgs e)
        {
            toolStripButtonOpenPal_Click(sender, e);
        }

        private void toolStripMenuItemSavePal_Click(object sender, EventArgs e)
        {
            toolStripButtonSavePal_Click(sender, e);
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripMenuItemReset_Click(object sender, EventArgs e)
        {
            toolStripButtonReset_Click(sender, e);
        }

        private void toolStripMenuItemRate_Click(object sender, EventArgs e)
        {
            toolStripButtonRate_Click(sender, e);
        }

        private void toolStripComboBoxZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            zoomLevel = toolStripComboBoxZoom.SelectedIndex;
            ShowImage(0);
        }

        private void splitContainerView_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            splitContainerView.Focus();
        }

        private void panelView_MouseWheel(object sender, MouseEventArgs e)
        {
            int levelCurrent = zoomLevel;
            zoomLevel += e.Delta / 120;
            zoomLevel = MathUtil.limitNumber(zoomLevel, 0, zoom.Length - 1);
            if (zoomLevel != levelCurrent)
            {
                toolStripComboBoxZoom.SelectedIndex = zoomLevel;
            }
        }
        private void toolStripButtonOpenPal_Click(object sender, EventArgs e)
        {
            loadPalMappingTable();
        }

        private void 导出调色板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportPal();
        }

        private void Form_Pallete_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("即将关闭调色板编辑器，是否继续？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (res.Equals(DialogResult.Cancel))
            {
                e.Cancel = true;
            }
        }

        private void 色彩平衡ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorBalance();
        }

        private void toolStripButton_HSB_Click(object sender, EventArgs e)
        {
            dealHSB();
        }

        private void 载入映射表ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            loadPalMappingTable();
        }

        private void 保存映射表ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            savePalMappingTable();
        }

        private void 批量转换映射表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult res = folder.ShowDialog();
            if (res.Equals(DialogResult.OK))
            {
                String srcPath = folder.SelectedPath;
                if (!Directory.Exists(srcPath))
                {
                    return;
                }
                //开始转换
                String[] files = Directory.GetFiles(srcPath, "*.pmt");
                for (int i = 0; i < files.Length; i++)
                {
                    FileStream fs = new FileStream(files[i], FileMode.Open);
                    ColorTable srcColorTableNew = new ColorTable();
                    ColorTable destColorTableNew = new ColorTable();
                    srcColorTableNew.readObject(fs, true);
                    destColorTableNew.readObject(fs, true);
                    fs.Close();
                    fs = new FileStream(files[i], FileMode.Truncate);
                    srcColorTableNew.writeObject(fs);
                    destColorTableNew.writeObject(fs);
                    fs.Close();
                }
                MessageBox.Show("共完成" + files.Length + "次转换", "提醒", MessageBoxButtons.OK);
            }

        }

        private void tsb_resetPos_Click(object sender, EventArgs e)
        {
            this.offsetX = 0;
            this.offsetY = 0;
            this.ShowImage(0);
        }

        private void splitContainerView_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.ShowImage(0);
        }



        //移动图片位置逻辑
        private static bool inMovingRightCanvas = false;//是否正在移动图片坐标中
        private static bool inMovingLeftCanvas = false;//是否正在移动图片坐标中
        private static int movingCanvasFromX = 0;     //移动时的起始坐标
        private static int movingCanvasFromY = 0;
        private static int movingCanvasCurrentX = 0;
        private static int movingCanvasCurrentY = 0;
        private static int movingCanvasOffsetX = 0;     //移动时屏幕中心偏移
        private static int movingCanvasOffsetY = 0;
        private void Form_Pallete_KeyDown(object sender, KeyEventArgs e)
        {
            if (imgOrg == null || imgEdit == null)
            {
                return;
            }
            if (panelViewOrg.Focused && !inMovingLeftCanvas)
            {
                Control control = (Control)panelViewOrg;
                int mx = MousePosition.X;
                int my = MousePosition.Y;
                Point p = new Point(0, 0);
                p = control.PointToScreen(p);
                int pX1 = p.X;
                int pY1 = p.Y;
                p.X = control.Width;
                p.Y = control.Height;
                p = control.PointToScreen(p);
                int pX2 = p.X;
                int pY2 = p.Y;
                if (mx >= pX1 && mx < pX2 && my >= pY1 && my < pY2)
                {
                    if (panelViewOrg.Cursor == defaultCursor)
                    {
                        if (e.KeyCode == Keys.Space)
                        {
                            panelViewOrg.Cursor = handCursor;
                        }
                        else if (e.Shift)
                        {
                            panelViewOrg.Cursor = addCursor;
                        }
                        else if (e.Control)
                        {
                            panelViewOrg.Cursor = addDelCursor;
                        }
                        else if (e.Alt)
                        {
                            panelViewOrg.Cursor = strawCursor;
                        }
                    }

                }
            }
            else if (panelViewEdit.Focused && !inMovingRightCanvas)
            {
                Control control = (Control)panelViewEdit;
                int mx = MousePosition.X;
                int my = MousePosition.Y;
                Point p = new Point(0, 0);
                p = control.PointToScreen(p);
                int pX1 = p.X;
                int pY1 = p.Y;
                p.X = control.Width;
                p.Y = control.Height;
                p = control.PointToScreen(p);
                int pX2 = p.X;
                int pY2 = p.Y;
                if (mx >= pX1 && mx < pX2 && my >= pY1 && my < pY2)
                {
                    if (panelViewEdit.Cursor == defaultCursor)
                    {
                        if (e.KeyCode == Keys.Space)
                        {
                            panelViewEdit.Cursor = handCursor;
                        }
                        else if (e.Shift)
                        {
                            panelViewEdit.Cursor = addCursor;
                        }
                        else if (e.Control)
                        {
                            panelViewEdit.Cursor = addDelCursor;
                        }
                        else if (e.Alt)
                        {
                            panelViewEdit.Cursor = strawCursor;
                        }
                    }
                }
            }



        }


        private void panelViewEdit_MouseEnter(object sender, EventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            ((Control)sender).Focus();
        }

        private void panelViewOrg_MouseEnter(object sender, EventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            ((Control)sender).Focus();
        }


        private void Form_Pallete_KeyUp(object sender, KeyEventArgs e)
        {
            if (imgOrg == null || imgEdit == null)
            {
                return;
            }
            if ((panelViewOrg.Cursor == handCursor && e.KeyCode == Keys.Space) || (panelViewOrg.Cursor == addCursor && !e.Shift) ||
                (panelViewOrg.Cursor == addDelCursor && !e.Control) ||(panelViewOrg.Cursor == strawCursor && !e.Alt)||
                (panelViewEdit.Cursor == handCursor && e.KeyCode == Keys.Space) || (panelViewEdit.Cursor == addCursor && !e.Shift) ||
                (panelViewEdit.Cursor == addDelCursor && !e.Control) || (panelViewEdit.Cursor == strawCursor && !e.Alt))
            {
                inMovingRightCanvas = false;
                inMovingLeftCanvas = false;
                panelViewOrg.Cursor = defaultCursor;
                panelViewEdit.Cursor = defaultCursor;
            }

        }
        private void panelViewOrg_MouseDown(object sender, MouseEventArgs e)
        {
            if (imgOrg == null)
            {
                return;
            }
            if (panelViewOrg.Cursor == handCursor)
            {
                if (!inMovingLeftCanvas)
                {
                    movingCanvasFromX = MousePosition.X;
                    movingCanvasFromY = MousePosition.Y;
                    movingCanvasOffsetX = offsetX;
                    movingCanvasOffsetY = offsetY;
                    inMovingLeftCanvas = true;
                }
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    int zoomLevelValue = zoom[zoomLevel];
                    int accOrgX = (panelViewOrg.Width / 2) % zoomLevelValue;
                    int accOrgY = (panelViewOrg.Height / 2) % zoomLevelValue;
                    int x = (e.X - accOrgX) / zoomLevelValue - (panelViewOrg.Width / 2 - accOrgX) / zoomLevelValue - (offsetX - imgOrg.Width / 2);
                    int y = (e.Y - accOrgY) / zoomLevelValue - (panelViewOrg.Height / 2 - accOrgY) / zoomLevelValue - (offsetY - imgOrg.Height / 2);
                    Color color = Color.Transparent;
                    if (x >= 0 && x < imgOrg.Width && y >= 0 && y < imgOrg.Height)
                    {
                        color = imgOrg.GetPixel(x, y);
                    }
                    if (ModifierKeys == Keys.Alt)
                    {
                        if (currentColorIDs.Count > 0 && !color.Equals(Color.Transparent))
                        {
                            for (int i = 0; i < currentColorIDs.Count; i++)
                            {
                                destColorTable.setColor((int)currentColorIDs[i], color.ToArgb());
                            }
                            refreshLabel();
                            destColorTable.updateUI(currentColorIDs);
                            EditImage();
                            ShowImage(1);
                        }
                    }
                    else if (ModifierKeys == Keys.Control)
                    {
                        int[] id = srcColorTable.searchColor(color.ToArgb());
                        if (id.Length > 0)
                        {
                            for (int i = 0; i < id.Length; i++)
                            {
                                if (currentColorIDs.Contains(id[i]))
                                {
                                    currentColorIDs.Remove(id[i]);
                                }
                                else
                                {
                                    currentColorIDs.Add(id[i]);
                                }
                            }
                            refreshLabel();
                            srcColorTable.updateUI(currentColorIDs);
                            destColorTable.updateUI(currentColorIDs);
                        }
                    }
                    else if (ModifierKeys == Keys.Shift)
                    {
                        int[] id = srcColorTable.searchColor(color.ToArgb());
                        if (id.Length > 0)
                        {
                            for (int i = 0; i < id.Length; i++)
                            {
                                if (!currentColorIDs.Contains(id[i]))
                                {
                                    currentColorIDs.Add(id[i]);
                                }
                            }
                            refreshLabel();
                            srcColorTable.updateUI(currentColorIDs);
                            destColorTable.updateUI(currentColorIDs);
                        }
                    }
                    else
                    {
                        int[] id = srcColorTable.searchColor(color.ToArgb());
                        currentColorIDs.Clear();
                        if (id.Length > 0)
                        {
                            for (int i = 0; i < id.Length; i++)
                            {
                                currentColorIDs.Add(id[i]);
                            }
                        }
                        refreshLabel();
                        srcColorTable.updateUI(currentColorIDs);
                        destColorTable.updateUI(currentColorIDs);
                    }
                }
            }

        }
        private void panelViewEdit_MouseDown(object sender, MouseEventArgs e)
        {
            if (imgEdit == null)
            {
                return;
            }
            if (panelViewEdit.Cursor == handCursor)
            {
                if (!inMovingRightCanvas)
                {
                    movingCanvasFromX = MousePosition.X;
                    movingCanvasFromY = MousePosition.Y;
                    movingCanvasOffsetX = offsetX;
                    movingCanvasOffsetY = offsetY;
                    inMovingRightCanvas = true;
                }
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    int zoomLevelValue = zoom[zoomLevel];
                    int accOrgX=(panelViewEdit.Width / 2)%zoomLevelValue;
                    int accOrgY = (panelViewEdit.Height / 2) % zoomLevelValue;
                    int x = (e.X - accOrgX) / zoomLevelValue - (panelViewEdit.Width / 2 - accOrgX) / zoomLevelValue - (offsetX - imgEdit.Width / 2);
                    int y = (e.Y - accOrgY) / zoomLevelValue - (panelViewEdit.Height / 2 - accOrgY) / zoomLevelValue - (offsetY - imgEdit.Height / 2);
                    Color color = Color.Transparent;
                    if (x >= 0 && x < imgEdit.Width && y >= 0 && y < imgEdit.Height)
                    {
                        color = imgEdit.GetPixel(x, y);
                    }
                    if (ModifierKeys == Keys.Alt)
                    {
                        if (currentColorIDs.Count > 0 && !color.Equals(Color.Transparent))
                        {
                            for (int i = 0; i < currentColorIDs.Count; i++)
                            {
                                destColorTable.setColor((int)currentColorIDs[i], color.ToArgb());
                            }
                            refreshLabel();
                            destColorTable.updateUI(currentColorIDs);
                            EditImage();
                            ShowImage(1);
                        }
                    }
                    else if (ModifierKeys == Keys.Control)
                    {
                        int[] id = destColorTable.searchColor(color.ToArgb());
                        if (id.Length > 0)
                        {
                            for (int i = 0; i < id.Length; i++)
                            {
                                if (currentColorIDs.Contains(id[i]))
                                {
                                    currentColorIDs.Remove(id[i]);
                                }
                                else
                                {
                                    currentColorIDs.Add(id[i]);
                                }
                            }
                            refreshLabel();
                            srcColorTable.updateUI(currentColorIDs);
                            destColorTable.updateUI(currentColorIDs);
                        }
                    }
                    else if (ModifierKeys == Keys.Shift)
                    {
                        int[] id = destColorTable.searchColor(color.ToArgb());
                        if (id.Length > 0)
                        {
                            for (int i = 0; i < id.Length; i++)
                            {
                                if (!currentColorIDs.Contains(id[i]))
                                {
                                    currentColorIDs.Add(id[i]);
                                }
                            }
                            refreshLabel();
                            srcColorTable.updateUI(currentColorIDs);
                            destColorTable.updateUI(currentColorIDs);
                        }
                    }
                    else
                    {
                        currentColorIDs.Clear();
                        int[] id = destColorTable.searchColor(color.ToArgb());
                        if (id.Length > 0)
                        {
                            for (int i = 0; i < id.Length; i++)
                            {
                                currentColorIDs.Add(id[i]);
                            }
                        }
                        refreshLabel();
                        srcColorTable.updateUI(currentColorIDs);
                        destColorTable.updateUI(currentColorIDs);
                    }
                }
            }

            
        }


        private void panelViewEdit_DoubleClick(object sender, EventArgs e)
        {
            if (imgEdit == null)
            {
                return;
            }
            if (ModifierKeys == Keys.Control || ModifierKeys == Keys.Alt || ModifierKeys == Keys.Shift)
            {
                return;
            }
            int zoomLevelValue = zoom[zoomLevel];
            int accOrgX = (panelViewEdit.Width / 2) % zoomLevelValue;
            int accOrgY = (panelViewEdit.Height / 2) % zoomLevelValue;
            int x = (((MouseEventArgs)e).X - accOrgX) / zoomLevelValue - (panelViewEdit.Width / 2 - accOrgX) / zoomLevelValue - (offsetX - imgEdit.Width / 2);
            int y = (((MouseEventArgs)e).Y - accOrgY) / zoomLevelValue - (panelViewEdit.Height / 2 - accOrgY) / zoomLevelValue - (offsetY - imgEdit.Height / 2);
            Color color = Color.Transparent;
            if (x >= 0 && x < imgEdit.Width && y >= 0 && y < imgEdit.Height)
            {
                color = imgEdit.GetPixel(x, y);
            }
            if (color == Color.Transparent)
            {
                return;
            }
            currentColorIDs.Clear();
            int[] id = destColorTable.searchColor(color.ToArgb());
            if (id.Length > 0)
            {
                for (int i = 0; i < id.Length; i++)
                {
                    currentColorIDs.Add(id[i]);
                }
            }
            srcColorTable.updateUI(currentColorIDs);
            destColorTable.updateUI(currentColorIDs);
            editColor(color.ToArgb());
        }

        private void panelViewOrg_DoubleClick(object sender, EventArgs e)
        {
            if (imgOrg == null)
            {
                return;
            }
            if (ModifierKeys == Keys.Control || ModifierKeys == Keys.Alt || ModifierKeys == Keys.Shift)
            {
                return;
            }
            int zoomLevelValue = zoom[zoomLevel];
            int accOrgX = (panelViewOrg.Width / 2) % zoomLevelValue;
            int accOrgY = (panelViewOrg.Height / 2) % zoomLevelValue;
            int x = (((MouseEventArgs)e).X - accOrgX) / zoomLevelValue - (panelViewOrg.Width / 2 - accOrgX) / zoomLevelValue - (offsetX - imgOrg.Width / 2);
            int y = (((MouseEventArgs)e).Y - accOrgY) / zoomLevelValue - (panelViewOrg.Height / 2 - accOrgY) / zoomLevelValue - (offsetY - imgOrg.Height / 2);
            Color color = Color.Transparent;
            if (x >= 0 && x < imgOrg.Width && y >= 0 && y < imgOrg.Height)
            {
                color = imgOrg.GetPixel(x, y);
            }
            if (color == Color.Transparent)
            {
                return;
            }
            currentColorIDs.Clear();
            int[] id = srcColorTable.searchColor(color.ToArgb());
            if (id.Length > 0)
            {
                for (int i = 0; i < id.Length; i++)
                {
                    currentColorIDs.Add(id[i]);
                }
            }
            srcColorTable.updateUI(currentColorIDs);
            destColorTable.updateUI(currentColorIDs);
            editColor(color.ToArgb());
        }

        private void panelViewOrg_MouseLeave(object sender, EventArgs e)
        {
            inMovingLeftCanvas = false;
        }

        private void panelViewEdit_MouseLeave(object sender, EventArgs e)
        {
            inMovingRightCanvas = false;
        }
        private void panelViewEdit_MouseMove(object sender, MouseEventArgs e)
        {
            if (imgOrg == null || imgEdit == null)
            {
                return;
            }
            if (inMovingRightCanvas)
            {
                int zoomLevelValue = zoom[zoomLevel];
                int mx = MousePosition.X;
                int my = MousePosition.Y;
                Control control = (Control)sender;
                Point p = new Point(control.Left, control.Top);
                p = control.PointToScreen(p);
                int pX1 = p.X;
                int pY1 = p.Y;
                p.X = control.Right;
                p.Y = control.Bottom;
                p = control.PointToScreen(p);
                int pX2 = p.X;
                int pY2 = p.Y;
                //更新移动起点
                movingCanvasCurrentX = mx;
                movingCanvasCurrentY = my;
                //增加偏移
                offsetX = movingCanvasOffsetX + (movingCanvasCurrentX - movingCanvasFromX) / zoomLevelValue;
                offsetY = movingCanvasOffsetY + (movingCanvasCurrentY - movingCanvasFromY) / zoomLevelValue;
                //重绘
                ShowImage(0);
            }
        }

        private void panelViewOrg_MouseMove(object sender, MouseEventArgs e)
        {
            if (imgOrg == null || imgEdit == null)
            {
                return;
            }
            if (inMovingLeftCanvas)
            {
                int zoomLevelValue = zoom[zoomLevel];
                int mx = MousePosition.X;
                int my = MousePosition.Y;
                Control control = (Control)sender;
                Point p = new Point(control.Left, control.Top);
                p = control.PointToScreen(p);
                int pX1 = p.X;
                int pY1 = p.Y;
                p.X = control.Right;
                p.Y = control.Bottom;
                p = control.PointToScreen(p);
                int pX2 = p.X;
                int pY2 = p.Y;
                //更新移动起点
                movingCanvasCurrentX = mx;
                movingCanvasCurrentY = my;
                //增加偏移
                offsetX = movingCanvasOffsetX + (movingCanvasCurrentX - movingCanvasFromX) / zoomLevelValue;
                offsetY = movingCanvasOffsetY + (movingCanvasCurrentY - movingCanvasFromY) / zoomLevelValue;
                //重绘
                ShowImage(0);
            }
        }

        private void panelViewOrg_MouseUp(object sender, MouseEventArgs e)
        {
            inMovingLeftCanvas = false;
        }

        private void panelViewEdit_MouseUp(object sender, MouseEventArgs e)
        {
            inMovingRightCanvas = false;
        }

        private void splitContainerView_SizeChanged(object sender, EventArgs e)
        {
            this.ShowImage(0);
        }
        private void splitContainer_All_SplitterMoved(object sender, SplitterEventArgs e)
        {
            pictureBox_srcTable.Size = new Size(193, 193);
            pictureBox_destTable.Size = new Size(193, 193);
        }


    }
}