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
using DockingUI.WinFormsUI.Docking;
using Cyclone.mod.exctl.floatPanel;
using Cyclone.mod.anim;
using Cyclone.mod.util;
using Cyclone.alg.math;
using Cyclone.alg.util;
using Cyclone.mod.animimg;
using Cyclone.alg.type;
using System.Drawing.Imaging;

namespace Cyclone.mod.anim
{
    public partial class Form_MImgsList : DockContent, MIO, MNodeUI<MImgElement>
    {
        public Form_MAnimation form_MA;
        public MImgsManager mImgsManager;
        private MClipsManager mClipsManager = null;

        public MClipsManager MClipsManager
        {
            get { return mClipsManager;}
            set { mClipsManager = value; }
        }
        private bool noNumericEvent = false;
        private bool noListBoxEvent = false;
        public static int bgColor = 0xbfbfbf;//背景颜色
        private Cursor handCursor;
        private bool inMoveImgCLip = false;
        private FloatForm moveImgClipBox = new FloatForm();
        public Form_MImgsList(Form_MAnimation form_MAT)//动画用
        {
            InitializeComponent();
            form_MA = form_MAT;
            mClipsManager = new MClipsManager(form_MA.form_Main);
            mImgsManager = new MImgsManager(form_MA.form_Main);
            initUI();
        }
        public Form_MImgsList(MImgsManager mImgsManagerT)//地图用
        {
            InitializeComponent();
            mImgsManager = mImgsManagerT;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            initUI();
        }
        private void initUI()
        {
            mImgsManager.MNodeUI = this;
            panel_clipEdit.MouseWheel += new MouseEventHandler(panel_EditorZoom_MouseWheel);
            handCursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.hand.ico"));
        }
        protected override string GetPersistString()
        {
            return "Form_MImgsList";
        }
        public void initParams(MClipElement setFoucElement)
        {
            int listBoxID = -1;
            currentClipElemnt = null;
            currentClipElments.Clear();
            if (setFoucElement != null)
            {
                currentClipElemnt = setFoucElement;
                currentClipElments.Add(currentClipElemnt);
                listBoxID = currentClipElemnt.getImageID();
            }
            else if (listBox_Images.Items.Count > 0)
            {
                listBoxID = 0;
            }
            regetBgBuffer();
            alignCenter(currentClipElemnt);
            updateAllList(listBoxID);
            this.updateEditRegion();
        }
        //释放资源
        public void releaseRes()
        {
            mImgsManager = null;
            MClipsManager = null;
            currentClipElemnt = null;
            if (imgEditWindowBuffer != null)
            {
                imgEditWindowBuffer.Dispose();
                imgEditWindowBuffer = null;
            }
        }
        //设置管理器
        public void setImagesManager(MImgsManager imgManagerT)
        {
            mImgsManager = imgManagerT;
        }
        //刷新整个列表显示
        public void updateAllList()
        {
            updateAllList(-1);
        }
        public void updateAllList(int selectID)
        {
            this.noListBoxEvent = true;
            listBox_Images.Items.Clear();
            MImgElement imgElement;
            noListBoxEvent = true;
            for (int i = 0; i < mImgsManager.Count(); i++)
            {
                imgElement = mImgsManager[i];
                listBox_Images.Items.Add(imgElement.getValueToLenString());
            }
            noListBoxEvent = false;
            if (selectID >= 0 && selectID < listBox_Images.Items.Count)
            {
                listBox_Images.SelectedIndex = selectID;
            }
            else
            {
                listBox_Images.SelectedIndex = listBox_Images.Items.Count - 1;
            }
            this.noListBoxEvent = false;
        }
        //事件响应============================================================
        private void button_closeImageManager_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_importImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files (*.png)|*.png|bmp files (*.bmp)|*.bmp";
            //dialog.FileName = "*.png|*.bmp";
            dialog.Title = "导入图片";
            dialog.Multiselect = true;
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK && dialog.FileNames.Length>0)
            {
                readyHistory(HistoryType.Imgs);
                foreach (String name in dialog.FileNames)
                {
                    mImgsManager.importImage(name, false);
                }
                updateAllList();
                addHistory(HistoryType.Imgs);
            }

        }

        private void listBoxImgList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noListBoxEvent)
            {
                return;
            }
            if (!Consts.transferClipMode)
            {
                currentClipElemnt = null;
                currentClipElments.Clear();
            }
            if (currentClipElemnt != null && listBox_Images.SelectedIndex >= 0)
            {
                int id1 = currentClipElemnt.getImageID();
                readyHistory(HistoryType.Clips);
                currentClipElemnt.setImageId(listBox_Images.SelectedIndex);
                addHistory(HistoryType.Clips);
                int id2 = currentClipElemnt.getImageID();
                noListBoxEvent = true;
                mImgsManager.MNodeUI.UpdateItem(id1);
                mImgsManager.MNodeUI.UpdateItem(id2);
                noListBoxEvent = false;
                regetBgBuffer();
            }
            else
            {
                regetBgBuffer();
                alignCenter(null);
            }
            adjustValue();
            updateEditRegion();
            if (listBox_Images.SelectedIndex >= 0)
            {
                toolTip.SetToolTip(listBox_Images, listBox_Images.Text);
            }
        }
        private void button_update_Click(object sender, EventArgs e)
        {
            mImgsManager.reloadImageElements();
            mImgsManager.rebindTextures();
            noListBoxEvent = true;
            for (int i = 0; i < mImgsManager.Count(); i++)
            {
                mImgsManager.MNodeUI.UpdateItem(i);
            }
            noListBoxEvent = false;
            this.updateEditRegion();
        }

        private void button_delImage_Click(object sender, EventArgs e)
        {
            int count = this.listBox_Images.SelectedIndices.Count;
            if (count < 0)
            {
                return;
            }
            //搜集条目
            List<MImgElement> imgElements = new List<MImgElement>();
            int minIndex = Int16.MaxValue;
            for (int i = 0; i < listBox_Images.SelectedIndices.Count; i++)
            {
                int id = listBox_Images.SelectedIndices[i];
                imgElements.Add(mImgsManager[id]);
                if (id < minIndex)
                {
                    minIndex = id;
                }
            }
            //删前检查
            foreach (MImgElement imgElement in imgElements)
            {
                int usedTime = imgElement.getUsedTime();
                if (usedTime > 0)
                {
                    MessageBox.Show("图片[" + imgElement.name + "]有" + usedTime + "处使用，不允许删除！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //准备记录
            readyHistory(HistoryType.Imgs);
            //开始删除
            int removedCount = 0;
            foreach (MImgElement imgElement in imgElements)
            {
                if (!mImgsManager.Remove(imgElement))
                {
                    break;
                }
                removedCount++;
            }
            //删后记录存储和界面更新
            if (removedCount > 0)
            {
                addHistory(HistoryType.Imgs);
                if (mImgsManager.Count() > 0)
                {
                    minIndex--;
                    if (minIndex < 0)
                    {
                        minIndex = 0;
                    }
                }
                else
                {
                    minIndex = -1;
                }
                updateAllList(minIndex);
            }
        }

        private void button_rename_Click(object sender, EventArgs e)
        {
            int index = this.listBox_Images.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            MImgElement currentImageElement = mImgsManager[index];
            String oldName = currentImageElement.name.Substring(0, currentImageElement.name.IndexOf('.'));
            String oldNamePS = currentImageElement.name.Substring(currentImageElement.name.LastIndexOf('.'));
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("重命名",oldName);
            txtDialog.ShowDialog();
            String nameNew = txtDialog.getValue();
            if (!oldName.Equals(nameNew))
            {
                if (form_MA != null)
                {
                    form_MA.historyManager.ReadyHistory_ImgRename(currentImageElement);
                }
                if (currentImageElement.renameImage(nameNew + oldNamePS))
                {
                    if (form_MA != null)
                    {
                        form_MA.historyManager.AddHistory_ImgRename(currentImageElement);
                    }
                    mImgsManager.MNodeUI.UpdateItem(index);
                }
            }
        }

        private void checkBox_forbbidOptimize_CheckedChanged(object sender, EventArgs e)
        {
            int count = this.listBox_Images.SelectedIndices.Count;
            if (count < 0)
            {
                return;
            }
            List<MImgElement> imgElements = new List<MImgElement>();
            for (int i = 0; i < listBox_Images.SelectedIndices.Count; i++)
            {
                int id = listBox_Images.SelectedIndices[i];
                imgElements.Add(mImgsManager[id]);
            }
            if (form_MA != null)
            {
                form_MA.historyManager.ReadyHistory_ImgProp(imgElements);
            }

            bool allForbid = true;
            foreach (MImgElement imgElement in imgElements)
            {
                if (!imgElement.forbidOptimize)
                {
                    allForbid = false;
                    break;
                }
            }

            foreach (MImgElement imgElement in imgElements)
            {
                imgElement.forbidOptimize = (!allForbid);
                mImgsManager.MNodeUI.UpdateItem(imgElement.GetID());
            }

            if (form_MA != null)
            {
                form_MA.historyManager.AddHistory_ImgProp(imgElements);
            }
        }
        private void ToolStripMenuItem_MoveTo_Click(object sender, EventArgs e)
        {

        }

        private void button_AlpahSet_Click(object sender, EventArgs e)
        {
            int index = this.listBox_Images.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            MImgElement currentImageElement = mImgsManager[index];
            if (currentImageElement == null)
            {
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files (*.png)|*.png";
            dialog.FileName = "*.png";
            dialog.Title = "设置Alpha图片";
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                String srcFileName = dialog.FileName;
                if (!File.Exists(srcFileName))
                {
                    return;
                }
                Image imgAlpha = IOUtil.createImage(srcFileName);
                Image imgSrc = currentImageElement.image;
                if (imgAlpha == null || imgSrc==null)
                {
                    return;
                }
                if (imgAlpha.Width != imgSrc.Width || imgAlpha.Height != imgSrc.Height)
                {
                    MessageBox.Show("图片尺寸与原图不一致！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                imgAlpha.Dispose();
                imgAlpha = null;

                int lastIndex = srcFileName.LastIndexOf('\\') + 1;
                String newFileName = srcFileName.Substring(lastIndex, srcFileName.Length - lastIndex);
                String sunFolderName = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_IMG;
                String newFileFullPath = sunFolderName + newFileName;
                if (File.Exists(newFileFullPath) && !newFileFullPath.Equals(srcFileName))
                {
                    DialogResult dr2 = MessageBox.Show("KS本地文件夹已经存在同名文件，是否覆盖？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr2.Equals(DialogResult.Yes))
                    {
                        IOUtil.Copy(srcFileName, newFileFullPath, true);
                    }
                }
                else
                {
                    try
                    {
                        //检查子文件夹
                        if (!Directory.Exists(sunFolderName))
                        {
                            Directory.CreateDirectory(sunFolderName);
                        }
                        //拷贝图片
                        if (!srcFileName.Equals(newFileFullPath))
                        {
                            IOUtil.Copy(srcFileName, newFileFullPath, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                readyHistory(HistoryType.Imgs);
                currentImageElement.strAlphaImage = newFileName;
                addHistory(HistoryType.Imgs);
                currentImageElement.loadImage();
                updateItem();
                updateEditRegion();
            }
        }

        private void button_AlphaDel_Click(object sender, EventArgs e)
        {
            int index = this.listBox_Images.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            readyHistory(HistoryType.Imgs);
            MImgElement currentImageElement = mImgsManager[index];
            currentImageElement.strAlphaImage = "";
            addHistory(HistoryType.Imgs);
            currentImageElement.loadImage();
            updateItem();
            updateEditRegion();
        }

        private void button_PMT_Click(object sender, EventArgs e)
        {
            //if (beForMap)
            //{
            //    MessageBox.Show("不能为地图图片设置调色板", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            int index = this.listBox_Images.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            MImgElement currentImageElement = mImgsManager[index];
            if (currentImageElement == null)
            {
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "调色板映射表文件 (*.pmt)|*.pmt";
            dialog.FileName = "*.pmt";
            dialog.Title = "设置调色板映射表文件";
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                String srcFileName = dialog.FileName;
                if (!File.Exists(srcFileName))
                {
                    return;
                }
                int lastIndex = srcFileName.LastIndexOf('\\') + 1;
                String newFileName = srcFileName.Substring(lastIndex, srcFileName.Length - lastIndex);
                String sunFolderName = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_IMG;
                String newFileFullPath = sunFolderName + newFileName;
                if (File.Exists(newFileFullPath) && !newFileFullPath.Equals(srcFileName))
                {
                    DialogResult dr2 = MessageBox.Show("KS本地文件夹已经存在同名文件，是否覆盖？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr2.Equals(DialogResult.Yes))
                    {
                        IOUtil.Copy(srcFileName, newFileFullPath, true);
                    }
                }
                else
                {
                    try
                    {
                        //检查子文件夹
                        if (!Directory.Exists(sunFolderName))
                        {
                            Directory.CreateDirectory(sunFolderName);
                        }
                        //拷贝图片
                        if (!srcFileName.Equals(newFileFullPath))
                        {
                            IOUtil.Copy(srcFileName, newFileFullPath, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                readyHistory(HistoryType.Imgs);
                currentImageElement.strPmt = newFileName;
                addHistory(HistoryType.Imgs);
                currentImageElement.loadImage();
                updateItem();
                updateEditRegion();
            }
        }

        private void button_PmtDel_Click(object sender, EventArgs e)
        {
            int index = this.listBox_Images.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            MImgElement currentImageElement = mImgsManager[index];
            readyHistory(HistoryType.Imgs);
            currentImageElement.strPmt = "";
            addHistory(HistoryType.Imgs);
            currentImageElement.loadImage();
            updateItem();
            updateEditRegion();
        }

        private void button_orderPositive_Click(object sender, EventArgs e)
        {
            if (mImgsManager.Count() <= 0)
            {
                return;
            }
            List<MImgElement> arrayListTemp = new List<MImgElement>();
            readyHistory(HistoryType.Imgs);
            while (mImgsManager.Count() > 0)
            {
                int maxIndex = 0;
                MImgElement elementCurrent = (MImgElement)mImgsManager[maxIndex];
                String nameLocal = elementCurrent.name;
                for (int i = 1; i < mImgsManager.Count(); i++)
                {
                    MImgElement elementCompare = (MImgElement)mImgsManager[i];
                    String nameCompare = elementCompare.name;
                    if (String.Compare(nameLocal, nameCompare, StringComparison.CurrentCulture) > 0)
                    {
                        elementCurrent = elementCompare;
                        nameLocal = nameCompare;
                        maxIndex = i;
                    }
                }
                arrayListTemp.Add(elementCurrent);
                mImgsManager.RemoveAt(maxIndex);
            }
            for (int i = 0; i < arrayListTemp.Count; i++)
            {
                mImgsManager.Add(arrayListTemp[i]);
            }
            addHistory(HistoryType.Imgs);
            updateAllList();
        }

        private void button_orderBackwards_Click(object sender, EventArgs e)
        {
            if (mImgsManager.Count() <= 0)
            {
                return;
            }
            List<MImgElement> arrayListTemp = new List<MImgElement>();
            readyHistory(HistoryType.Imgs);
            while (mImgsManager.Count() > 0)
            {
                int maxIndex = 0;
                MImgElement elementCurrent = (MImgElement)mImgsManager[maxIndex];
                String nameLocal = elementCurrent.name;
                for (int i = 1; i < mImgsManager.Count(); i++)
                {
                    MImgElement elementCompare = (MImgElement)mImgsManager[i];
                    String nameCompare = elementCompare.name;
                    if (String.Compare(nameLocal, nameCompare, StringComparison.CurrentCulture) > 0)
                    {
                        elementCurrent = elementCompare;
                        nameLocal = nameCompare;
                        maxIndex = i;
                    }
                }
                arrayListTemp.Add(elementCurrent);
                mImgsManager.RemoveAt(maxIndex);
            }
            for (int i = arrayListTemp.Count-1; i >=0; i--)
            {
                mImgsManager.Add(arrayListTemp[i]);
            }
            addHistory(HistoryType.Imgs);
            updateAllList();
        }

        private void button_moveUp_Click(object sender, EventArgs e)
        {
            noListBoxEvent = true;
            readyHistory(HistoryType.Imgs);
            if (mImgsManager.MoveUpElement(listBox_Images.SelectedIndex))
            {
                addHistory(HistoryType.Imgs);
            }
            noListBoxEvent = false;
        }

        private void button_moveDown_Click(object sender, EventArgs e)
        {
            noListBoxEvent = true;
            readyHistory(HistoryType.Imgs);
            if (mImgsManager.MoveDownElement(listBox_Images.SelectedIndex))
            {
                addHistory(HistoryType.Imgs);
            }
            noListBoxEvent = false;
        }

        private void button_moveTop_Click(object sender, EventArgs e)
        {
            noListBoxEvent = true;
            readyHistory(HistoryType.Imgs);
            if (mImgsManager.MoveTopElement(listBox_Images.SelectedIndex))
            {
                addHistory(HistoryType.Imgs);
            }
            noListBoxEvent = false;
        }

        private void button_moveBottom_Click(object sender, EventArgs e)
        {
            noListBoxEvent = true;
            readyHistory(HistoryType.Imgs);
            if (mImgsManager.MoveBottomElement(listBox_Images.SelectedIndex))
            {
                addHistory(HistoryType.Imgs);
            }
            noListBoxEvent = false;
        }

        private void listBox_ImageManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && ((ListBox)sender).SelectedIndex >= 0)
            {
                button_delImage_Click(sender, e);
            }
            else if (e.Control)
            {
                noListBoxEvent = true;
                if (e.KeyCode == Keys.Up)
                {
                    readyHistory(HistoryType.Imgs);
                    if (mImgsManager.MoveUpElement(listBox_Images.SelectedIndex))
                    {
                        addHistory(HistoryType.Imgs);
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    readyHistory(HistoryType.Imgs);
                    if (mImgsManager.MoveDownElement(listBox_Images.SelectedIndex))
                    {
                        addHistory(HistoryType.Imgs);
                    }
                }
                noListBoxEvent = false;
                e.Handled = true;
            }
        }


        private void button_Check_Click(object sender, EventArgs e)
        {
            int index = this.listBox_Images.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            MImgElement currentImageElement = mImgsManager[index];
            String useInfor=currentImageElement.getUsedTimeInfor();
            SmallDialog_ShowParagraph.showString("图片引用检查", useInfor);
        }

        private void listBox_ImageManager_DoubleClick(object sender, EventArgs e)
        {
            int index=listBox_Images.SelectedIndex;
            if(index>=0)
            {
                SmallDialog_ShowParagraph.showString("图片信息",mImgsManager[index].getValueToLenString() + "\n行号：【" + index + "】");
            }
        }
        //###################################### 切片编辑 ###############################################################
        //==============================编辑区操作和显示================================
        private const int defaultW = 16;//切片默认尺寸
        private const int defaultH = 16;
        private Image imgEditWindowBuffer = null;//窗口缓存
        //private Image imgEditWindowBgBuffer = null;//窗口备份缓存
        private int imgEditBgLeft = 0;//图片缓存左上角坐标X
        private int imgEditBgTop = 0; //图片缓存左上角坐标Y
        private int imgEditCenterPixelX = 0;//中心点像素坐标X
        private int imgEditCenterPixelY = 0; //中心点像素坐标
        private int imgEditCenterPixelX_Pre = 0;//中心点像素坐标X备份
        private int imgEditCenterPixelY_Pre = 0; //中心点像素坐标备份
        private int imgEditBgWidth = 32;//底层图片宽度
        private int imgEditBgHeight = 32; //底层图片高度
        private int imgEditWindowWidth = 0;//窗口缓存宽度
        private int imgEditWindowHeight = 0; //窗口缓存后高度
        private int zoomLevel = 1;//当前缩放级别
        private const int zoomLevelMin = 1;//缩放最大级别
        private const int zoomLevelMax = 16;//缩放最大级别
        private const int margin_w_Edit = 20;//图片框边缘宽度
        private const int margin_h_Edit = 20;//图片框边缘高度
        public MClipElement currentClipElemnt = null;//当前切片
        public List<MClipElement> currentClipElments = new List<MClipElement>();//当前切片集合
        //编辑区操作++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        ////增加新切片
        //private void addDefBaseClip()
        //{
        //    MClipElement newClipElement = null;
        //    newClipElement = createImageClip(this.listBox_Images.SelectedIndex, 0, 0, defaultW, defaultH);
        //    //updateCurrentBaseClips();
        //    setFocusClip(newClipElement, true);
        //}
        private MClipElement createImageClip(int imgIndex, int x, int y, int w, int h)
        {
            MClipElement element = new MClipElement(MClipsManager);
            element.setImageValue(mImgsManager[imgIndex], new Rectangle(x, y, w, h));
            MClipsManager.Add(element);
            return element;
        }
        //删除切片
        private void deleteCurrentClip()
        {
            if (currentClipElments.Count == 0)
            {
                return;
            }
            int idImg = -1;
            readyHistory(HistoryType.Clips);
            for (int i = 0; i < currentClipElments.Count; i++)
            {
                MClipElement clipI = currentClipElments[0];
                if (clipI.getUsedTime() > 0)
                {
                    MessageBox.Show("不能删除被使用" + currentClipElemnt.getUsedTime() + "次的切片", "错误操作", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                idImg = clipI.getImageID();
                currentClipElments.Remove(clipI);
                if (clipI.Equals(currentClipElemnt))
                {
                    currentClipElemnt = null;
                }
                int index = clipI.GetID();
                MClipsManager.RemoveAt(index);
                i--;
            }
            addHistory(HistoryType.Clips);
            updateEditRegion();
            clipEditMode = EMODE_NONE;
            setCursorState();
            if (idImg >= 0)
            {
                noListBoxEvent = true;
                mImgsManager.MNodeUI.UpdateItem(idImg);
                noListBoxEvent = false;
            }
        }
        //将某个切片设置为焦点切片(删除其余所有焦点)，可以设置是否将视野对齐到它的中心，还可以设置在最后是否需要不让目标选中
        public void setFocusClip(MClipElement clipElement, bool focusItsCenter)
        {
            setFocusClip(clipElement, focusItsCenter, false);
        }
        public void setFocusClip(MClipElement clipElement, bool focusItsCenter,bool unSelect)
        {
            currentClipElemnt = clipElement;
            if (currentClipElemnt != null)
            {
                setFocusedImgItem(currentClipElemnt.getImageID(), false);
            }
            currentClipElments.Clear();
            if (currentClipElemnt != null)
            {
                currentClipElments.Add(currentClipElemnt);
            }
            regetBgBuffer();
            if (focusItsCenter)
            {
                alignCenter(clipElement);
            }
            if (unSelect)
            {
                currentClipElemnt = null;
                currentClipElments.Clear();
            }
            updateEditRegion();
        }
        private void alignCenter(MClipElement clipElement)
        {
            if (clipElement == null)
            {
                imgEditCenterPixelX = -(imgEditBgWidth / zoomLevel / 2);
                imgEditCenterPixelY = -(imgEditBgHeight / zoomLevel / 2);
            }
            else
            {
                imgEditCenterPixelX = -(clipElement.clipRect.X + clipElement.clipRect.Width / 2);
                imgEditCenterPixelY = -(clipElement.clipRect.Y + clipElement.clipRect.Height / 2);
            }
        }
        public void releaseFocus()
        {
            currentClipElemnt = null;
            currentClipElments.Clear();
            clipEditMode = EMODE_NONE;
            setCursorState();
            inEditing = false;
            Console.WriteLine("inEditing:" + inEditing);
        }
        //在编辑区是否点击到某个切片
        private static int focusLevel = 0;
        private static ArrayList focusArray = new ArrayList();
        private MClipElement focousEditClip(int X, int Y,bool changeLevel)
        {
            MClipElement clipTemp = null;
            focusArray.Clear();
            for(int i=0;i<MClipsManager.Count();i++)
            {
                MClipElement clip = (MClipElement)MClipsManager[i];
                Rectangle clipRect = clip.clipRect;
                if (clip.getImageID() == listBox_Images.SelectedIndex &&
                    MathUtil.inRegion(X, imgEditBgLeft + clipRect.X * zoomLevel, imgEditBgLeft + (clipRect.X + clipRect.Width) * zoomLevel)
                    && MathUtil.inRegion(Y, imgEditBgTop + clipRect.Y * zoomLevel, imgEditBgTop + (clipRect.Y + clipRect.Height) * zoomLevel))
                {
                    focusArray.Add(clip);
                }
            }
            if (currentClipElemnt != null && focusArray.Contains(currentClipElemnt) && ModifierKeys != Keys.Shift && !changeLevel)
            {
                clipTemp = currentClipElemnt;
            }
            else
            {
                if (changeLevel)
                {
                    focusLevel++;
                }
                if (focusLevel < focusArray.Count)
                {
                    clipTemp = (MClipElement)focusArray[focusLevel];
                }
                else
                {
                    focusLevel = 0;
                    if (focusArray.Count > 0)
                    {
                        clipTemp = (MClipElement)focusArray[focusLevel];
                    }
                }
            }

            //Console.WriteLine(focusArray.Count+"focusLevel:" + focusLevel);
            focusArray.Clear();
            return clipTemp;
        }

        //刷新编辑区显示++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Image imgPicBuffer = null;
        //重新获得背景图片
        public void regetBgBuffer()
        {
            //窗口缓冲尺寸
            imgEditWindowWidth = panel_clipEdit.Width;
            imgEditWindowHeight = panel_clipEdit.Height;

            imgPicBuffer = null;
            imgPicBuffer = mImgsManager.getImage(listBox_Images.SelectedIndex);
            if (imgPicBuffer != null)
            {
                imgEditBgWidth = imgPicBuffer.Width * zoomLevel;
                imgEditBgHeight = imgPicBuffer.Height * zoomLevel;
            }
        }
        public void updateEditRegion()
        {
            ////设置辅助属性(图片索引)
            //if (currentClipElemnt != null)
            //{
            //    setFocusedImgItem(currentClipElemnt.getImageID(), false);//当前图片索引
            //}
            //设置辅助属性(剪切矩形，ID,使用次数)
            if (currentClipElemnt != null)
            {
                saveValueToTxtBox(currentClipElemnt.clipRect);    //剪切矩形
                //附加属性
                label_ClipUsedTime.Text = "" + currentClipElemnt.getUsedTime();
            }
            //调整缓冲------------------------------------------------------

            //背景图片
            regetBgBuffer();
            //刷新窗口缓存大小
            if (imgEditWindowBuffer == null || imgEditWindowBuffer.Width != imgEditWindowWidth || imgEditWindowBuffer.Height != imgEditWindowHeight)
            {
                imgEditWindowBuffer = new Bitmap(imgEditWindowWidth, imgEditWindowHeight);
            }
            //绘制----------------------------------------------------------
            Graphics gBuffer = Graphics.FromImage(imgEditWindowBuffer);
            //窗口背景
            //GraphicsUtil.drawClip(gBuffer, imgEditWindowBgBuffer, 0, 0, 0, 0, imgEditWindowBgBuffer.Width, imgEditWindowBgBuffer.Height, 0);
            GraphicsUtil.fillRect(gBuffer, 0, 0, imgEditWindowWidth, imgEditWindowHeight, bgColor);
            //背景图片坐标
            imgEditBgLeft = imgEditWindowWidth / 2 + imgEditCenterPixelX * zoomLevel;
            imgEditBgTop = imgEditWindowHeight / 2 + imgEditCenterPixelY * zoomLevel;
            if (imgPicBuffer != null)
            {
                GraphicsUtil.fillRect(gBuffer, imgEditBgLeft, imgEditBgTop, imgEditBgWidth, imgEditBgHeight, 0x808080);
                GraphicsUtil.drawImage(gBuffer, imgPicBuffer, imgEditBgLeft, imgEditBgTop, imgEditBgWidth, imgEditBgHeight, 0, 0, imgPicBuffer.Width, imgPicBuffer.Height);
            }
            //else
            //{
            //    GraphicsUtil.drawRect(gBuffer, imgEditBgLeft - 1, imgEditBgTop - 1, imgEditBgWidth + 2, imgEditBgHeight + 2, Consts.colorGrid);
            //}
            //切片区域
            if (listBox_Images.SelectedIndex >= 0)
            {
                ArrayList clipsList = MClipsManager.getAllClipsUsingImg(mImgsManager[listBox_Images.SelectedIndex]);
                foreach (MClipElement clip in clipsList)
                {
                    if (!currentClipElments.Contains(clip))
                    {
                        int _xz = imgEditBgLeft + clip.clipRect.X * zoomLevel;
                        int _yz = imgEditBgTop + clip.clipRect.Y * zoomLevel;
                        int _wz = clip.clipRect.Width * zoomLevel;
                        int _hz = clip.clipRect.Height * zoomLevel;
                        GraphicsUtil.drawRect(gBuffer, _xz, _yz, _wz, _hz, Consts.color_green, 0xFF);
                    }
                }
                foreach (MClipElement clip in clipsList)
                {
                    if (currentClipElments.Contains(clip))
                    {
                        int _xz = imgEditBgLeft + clip.clipRect.X * zoomLevel;
                        int _yz = imgEditBgTop + clip.clipRect.Y * zoomLevel;
                        int _wz = clip.clipRect.Width * zoomLevel;
                        int _hz = clip.clipRect.Height * zoomLevel;
                        GraphicsUtil.fillRect(gBuffer, _xz + 1, _yz + 1, _wz - 2, _hz - 2, Consts.colorBlue, 30);
                        GraphicsUtil.drawRect(gBuffer, _xz, _yz, _wz, _hz, Consts.colorBlue, 0xFF);
                    }
                }
            }
            if (currentClipElemnt != null)
            {
                MClipElement clip = currentClipElemnt;
                int _xz = imgEditBgLeft + clip.clipRect.X * zoomLevel;
                int _yz = imgEditBgTop + clip.clipRect.Y * zoomLevel;
                int _wz = clip.clipRect.Width * zoomLevel;
                int _hz = clip.clipRect.Height * zoomLevel;
                int sizeTrans = 3;
                GraphicsUtil.drawRect(gBuffer, _xz - sizeTrans, _yz - sizeTrans, sizeTrans * 2, sizeTrans * 2, Consts.colorBlue, 0xFF);
                GraphicsUtil.drawRect(gBuffer, _xz + _wz - sizeTrans, _yz - sizeTrans, sizeTrans * 2, sizeTrans * 2, Consts.colorBlue, 0xFF);
                GraphicsUtil.drawRect(gBuffer, _xz - sizeTrans, _yz + _hz - sizeTrans, sizeTrans * 2, sizeTrans * 2, Consts.colorBlue, 0xFF);
                GraphicsUtil.drawRect(gBuffer, _xz + _wz - sizeTrans, _yz + _hz - sizeTrans, sizeTrans * 2, sizeTrans * 2, Consts.colorBlue, 0xFF);
                sizeTrans = 4;
                GraphicsUtil.drawRect(gBuffer, _xz - sizeTrans, _yz - sizeTrans, sizeTrans * 2, sizeTrans * 2, Consts.color_white, 0x66);
                GraphicsUtil.drawRect(gBuffer, _xz + _wz - sizeTrans, _yz - sizeTrans, sizeTrans * 2, sizeTrans * 2, Consts.color_white, 0x66);
                GraphicsUtil.drawRect(gBuffer, _xz - sizeTrans, _yz + _hz - sizeTrans, sizeTrans * 2, sizeTrans * 2, Consts.color_white, 0x66);
                GraphicsUtil.drawRect(gBuffer, _xz + _wz - sizeTrans, _yz + _hz - sizeTrans, sizeTrans * 2, sizeTrans * 2, Consts.color_white, 0x66);
            }

            //参考线
            //if (currentClipElemnt != null)
            //{
            //    int t = currentClipElemnt.clipRect.Y * zoomLevel + imgEditBgTop;
            //    GraphicsUtil.drawDashLine(gBuffer, 0, t, imgEditWindowWidth, t, Consts.colorBlue, 1);
            //    t += zoomLevel * currentClipElemnt.clipRect.Height;
            //    GraphicsUtil.drawDashLine(gBuffer, 0, t, imgEditWindowWidth, t, Consts.colorBlue, 1);
            //    t = currentClipElemnt.clipRect.X * zoomLevel + imgEditBgLeft;
            //    GraphicsUtil.drawDashLine(gBuffer, t, 0, t, imgEditWindowHeight, Consts.colorBlue, 1);
            //    t += zoomLevel * currentClipElemnt.clipRect.Width;
            //    GraphicsUtil.drawDashLine(gBuffer, t, 0, t, imgEditWindowHeight, Consts.colorBlue, 1);
            //}
            //绘制到屏幕
            Graphics g = panel_clipEdit.CreateGraphics();
            //g.Clear(Color.Transparent);
            if (imgEditWindowBuffer != null)
            {
                GraphicsUtil.drawImage(g, imgEditWindowBuffer, 0, 0, 0, 0, imgEditWindowBuffer.Width, imgEditWindowBuffer.Height);
            }
            //清除临时资源
            gBuffer.Dispose();
            gBuffer = null;
            g.Dispose();
            g = null;
        }
        //更新列表中的单个条目信息
        public void updateItem()
        {
            if (listBox_Images.Items.Count <= 0)//列表为空
            {
                return;
            }
            int itemIndex = listBox_Images.SelectedIndex;
            noListBoxEvent = true;
            listBox_Images.Items[itemIndex] = mImgsManager[itemIndex].getValueToLenString();
            noListBoxEvent = false;
        }
        //缩放处理++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //增加缩放
        public void zoomIn()
        {
            zoom(1);
        }
        public void zoom(int level)
        {
            int tLevel = zoomLevel;
            zoomLevel += level;
            if (zoomLevel > zoomLevelMax)
            {
                zoomLevel = zoomLevelMax;
            }
            if (zoomLevel < zoomLevelMin)
            {
                zoomLevel = zoomLevelMin;
            }
            if (zoomLevel != tLevel)
            {
                updateEditRegion();
            }
        }
        //减小缩放
        public void zoomOut()
        {
            zoom(-1);
        }
        //重置缩放
        public void zoomReset()
        {
            if (zoomLevel != zoomLevelMin)
            {
                zoomLevel = zoomLevelMin;
                updateEditRegion();
            }
        }
        //==============================浏览区操作和显示====================================
   
        //切片容器操作和显示========================================================

        //从数字文本框保存剪切矩形数值

        private void saveValueFromTxtBox()
        {
            readyHistory(HistoryType.Clips);
            this.currentClipElemnt.clipRect.X = (int)numericUpDown_x.Value;
            this.currentClipElemnt.clipRect.Y = (int)numericUpDown_y.Value;
            this.currentClipElemnt.clipRect.Width = (int)numericUpDown_w.Value;
            this.currentClipElemnt.clipRect.Height = (int)numericUpDown_h.Value;
            currentClipElemnt.resetImgClip();
            addHistory(HistoryType.Clips);
        }
        //数字文本框接受剪切矩形数值
        private void saveValueToTxtBox(Rectangle clipRect)
        {
            noNumericEvent = true;
            if (clipRect.X >= numericUpDown_x.Minimum && clipRect.X <= numericUpDown_x.Maximum)
            {
                this.numericUpDown_x.Value = new Decimal(clipRect.X);
            }
            if (clipRect.Y >= numericUpDown_y.Minimum && clipRect.Y <= numericUpDown_y.Maximum)
            {
                this.numericUpDown_y.Value = new Decimal(clipRect.Y);
            }
            if (clipRect.Width >= numericUpDown_w.Minimum && clipRect.Width <= numericUpDown_w.Maximum)
            {
                this.numericUpDown_w.Value = new Decimal(clipRect.Width);
            }
            if (clipRect.Height >= numericUpDown_h.Minimum && clipRect.Height <= numericUpDown_h.Maximum)
            {
                this.numericUpDown_h.Value = new Decimal(clipRect.Height);
            }
            noNumericEvent = false;
        }
        //当切换图片时校验区域
        private void adjustValue()
        {
            if (currentClipElemnt == null)
            {
                return;
            }
            //校验
            int x = currentClipElemnt.clipRect.X;
            int y = currentClipElemnt.clipRect.Y;
            int w = currentClipElemnt.clipRect.Width;
            int h = currentClipElemnt.clipRect.Height;
            if (x < 0)
            {
                x = 0;
            }
            else if (x + w >= imgEditBgWidth)
            {
                x = imgEditBgWidth - w;
                if (x < 0)
                {
                    x = 0;
                    w = imgEditBgWidth;
                }
            }
            if (y < 0)
            {
                y = 0;
            }
            else if (y + h >= imgEditBgHeight)
            {
                y = imgEditBgHeight - h;
                if (y < 0)
                {
                    y = 0;
                    h = imgEditBgHeight;
                }
            }
            if (w < 1)
            {
                w = 1;
            }
            else if (w + x > imgEditBgWidth)
            {
                w = imgEditBgWidth - x;
                if (w < 1)
                {
                    w = 1;
                    x = imgEditBgWidth - w;
                }
            }
            if (h < 1)
            {
                h = 1;
            }
            else if (h + y > imgEditBgHeight)
            {
                h = imgEditBgHeight - y;
                if (h < 1)
                {
                    h = 1;
                    y = imgEditBgHeight - h;
                }
            }
            saveValueToTxtBox(new Rectangle(x, y, w, h));
        }
        //设置当前图片列表被选择的焦点项，并清除多选项
        private void setFocusedImgItem(int index, bool focusCenter)
        {
            if (listBox_Images == null || index >= this.listBox_Images.Items.Count)
            {
                return;
            }
            noListBoxEvent = true;
            listBox_Images.SelectedIndices.Clear();
            listBox_Images.SelectedIndex = index;
            noListBoxEvent = false;
            if (focusCenter)
            {
                regetBgBuffer();
                imgEditCenterPixelX = -(imgEditBgWidth / zoomLevel / 2);
                imgEditCenterPixelY = -(imgEditBgHeight / zoomLevel / 2);
            }
        }
        //获取当前图片列表被选择的焦点项
        private int getFocusedImgItem()
        {
            if (listBox_Images == null)
            {
                return -1;
            }
            return listBox_Images.SelectedIndex;
        }
        ////设置垂直滚动条数值
        //private void setVScroolBarValue(int value)
        //{
        //    if (value >= 0 && value <= vScrollBar_editRegion.Maximum - 9)
        //    {
        //        noScrollEvent = true;
        //        vScrollBar_editRegion.Value = value;
        //        noScrollEvent = false;
        //    }
        //}
        ////设置水平滚动条数值
        //private void setHScroolBarValue(int value)
        //{
        //    if (value >= 0 && value <= hScrollBar_editRegion.Maximum - 9)
        //    {
        //        noScrollEvent = true;
        //        hScrollBar_editRegion.Value = value;
        //        noScrollEvent = false;
        //    }
        //}
        //校正中心点像素坐标
        private void adjustCenterPixel()
        {
            int Wbg = imgEditBgWidth / zoomLevel;
            int Hbg = imgEditBgHeight / zoomLevel;
            bool saveC = false;
            if (imgEditCenterPixelX < -Wbg)
            {
                imgEditCenterPixelX = -Wbg;
                saveC = true;
            }
            else if (imgEditCenterPixelX > 0)
            {
                imgEditCenterPixelX = 0;
                saveC = true;
            }
            if (imgEditCenterPixelY < -Hbg)
            {
                imgEditCenterPixelY = -Hbg;
                saveC = true;
            }
            else if (imgEditCenterPixelY > 0)
            {
                imgEditCenterPixelY = 0;
                saveC = true;
            }
            if (saveC)
            {
                imgEditCenterPixelX_Pre = imgEditCenterPixelX;
                imgEditCenterPixelY_Pre = imgEditCenterPixelY;
                cursorX = cursorXNew;
                cursorY = cursorYNew;
            }
        }
        //==================================事件处理========================================
        //用户点击关闭按钮
        //private bool bCloseButton = true;

        //点击按钮缩放

        private void button_ZoomReset_Click(object sender, EventArgs e)
        {
            zoomReset();
        }

        private void button_ZoomOut_Click(object sender, EventArgs e)
        {
            zoomOut();
        }

        private void button_ZoomIn_Click(object sender, EventArgs e)
        {
            zoomIn();
        }


        //切片当前切片的图片索引

        //从数字文本框调整切片剪切矩形
        private void numericUpDown_x_ValueChanged(object sender, EventArgs e)
        {
            if (noNumericEvent || currentClipElemnt == null)
            {
                return;
            }
            NumericUpDown num = (NumericUpDown)sender;
            if (num.Value < 0)
            {
                num.Value = 0;
            }
            else if (num.Value + numericUpDown_w.Value >= imgEditBgWidth / zoomLevel)
            {
                num.Value = imgEditBgWidth / zoomLevel - numericUpDown_w.Value;
                if (num.Value < 0)
                {
                    num.Value = 0;
                }
            }
            //更新数值
            saveValueFromTxtBox();
            updateEditRegion();
        }

        private void numericUpDown_y_ValueChanged(object sender, EventArgs e)
        {
            if (noNumericEvent || currentClipElemnt == null)
            {
                return;
            }
            noNumericEvent = true;
            NumericUpDown num = (NumericUpDown)sender;
            if (num.Value < 0)
            {
                num.Value = 0;
            }
            else if (num.Value + numericUpDown_h.Value >= imgEditBgHeight / zoomLevel)
            {
                num.Value = imgEditBgHeight / zoomLevel - numericUpDown_h.Value;
                if (num.Value < 0)
                {
                    num.Value = 0;
                }
            }
            //更新数值
            saveValueFromTxtBox();
            updateEditRegion();
            noNumericEvent = false;
        }

        private void numericUpDown_w_ValueChanged(object sender, EventArgs e)
        {
            if (noNumericEvent || currentClipElemnt == null)
            {
                return;
            }
            noNumericEvent = true;
            NumericUpDown num = (NumericUpDown)sender;
            if (num.Value < 1)
            {
                num.Value = 1;
            }
            else if (num.Value + numericUpDown_x.Value > imgEditBgWidth / zoomLevel)
            {
                num.Value = imgEditBgWidth / zoomLevel - numericUpDown_x.Value;
                if (num.Value < 1)
                {
                    num.Value = 1;
                }
            }
            //更新数值
            saveValueFromTxtBox();
            updateEditRegion();
            noNumericEvent = false;
        }

        private void numericUpDown_h_ValueChanged(object sender, EventArgs e)
        {
            if (noNumericEvent || currentClipElemnt == null)
            {
                return;
            }
            NumericUpDown num = (NumericUpDown)sender;
            if (num.Value < 1)
            {
                num.Value = 1;
            }
            else if (num.Value + numericUpDown_y.Value > imgEditBgHeight / zoomLevel)
            {
                num.Value = imgEditBgHeight / zoomLevel - numericUpDown_y.Value;
                if (num.Value < 1)
                {
                    num.Value = 1;
                }
            }
            //更新数值
            saveValueFromTxtBox();
            updateEditRegion();
        }

        //编辑区事件--------------------------------------------------------------
        //编辑区滚轮缩放
        private void panel_EditorZoom_MouseWheel(object sender, MouseEventArgs e)
        {
            zoom(-e.Delta / 120);
        }
        //编辑区窗口大小调整
        private void panel_EditorBg_SizeChanged(object sender, EventArgs e)
        {
            updateEditRegion();
        }
        //获得焦点
        private void pictureBox_clipEdit_MouseEnter(object sender, EventArgs e)
        {
            if (!MiscUtil.getTopParent(this).Equals(Form.ActiveForm))
            {
                return;
            }
            panel_clipEdit.Focus();
            panel_clipEdit.Cursor = Cursors.Default;
            Form_MAnimation.transCmdKey = true;
        }

        private void pictureBox_clipEdit_MouseLeave(object sender, EventArgs e)
        {
            Form_MAnimation.transCmdKey = false;
        }

        private void pictureBox_clipEdit_SizeChanged(object sender, EventArgs e)
        {
            panel_clipEdit.Refresh();
            this.updateEditRegion();
        }
        //编辑区单击
        int cursorX = 0;
        int cursorY = 0;
        int cursorXNew = 0;
        int cursorYNew = 0;
        //编辑区鼠标移动
        //private static bool noScrollEvent = false;//屏蔽非用户操作的滚动条改变
        private byte clipEditMode = 0;//当前切片编辑模式(只针对左键)
        private const byte EMODE_NONE = 0;//无移动
        private const byte EMODE_L = EMODE_NONE + 1;//左边界移动
        private const byte EMODE_R = EMODE_L + 1;//右边界移动
        private const byte EMODE_T = EMODE_R + 1;//上边界移动
        private const byte EMODE_B = EMODE_T + 1;//底边界移动
        private const byte EMODE_LT = EMODE_B + 1;//左上边界移动
        private const byte EMODE_RT = EMODE_LT + 1;//右上边界移动
        private const byte EMODE_LB = EMODE_RT + 1;//左下边界移动
        private const byte EMODE_RB = EMODE_LB + 1;//右下界移动
        private const byte EMODE_DRAG = EMODE_RB + 1;//整体拖动
        private bool inEditing = false;//是否正在编辑中
        private int x_clip, y_clip, xr_clip, yb_clip;//焦点切片的边界
        private int x_clip_temp, y_clip_temp, xr_clip_temp, yb_clip_temp, imgW_temp, imgH_temp;//焦点切片的边界(临时)
        private void pictureBox_clipEdit_MouseMove(object sender, MouseEventArgs e)
        {
            cursorXNew = e.X;
            cursorYNew = e.Y;
            if (!panel_clipEdit.Cursor.Equals(handCursor))//当前处于编辑切片状态
            {
                if (currentClipElemnt == null)
                {
                    return;
                }
                //切片相对于窗口的坐标
                x_clip_temp = this.imgEditBgLeft + this.currentClipElemnt.clipRect.X * this.zoomLevel;
                y_clip_temp = this.imgEditBgTop + this.currentClipElemnt.clipRect.Y * this.zoomLevel;
                xr_clip_temp = x_clip_temp + this.currentClipElemnt.clipRect.Width * this.zoomLevel - 1;
                yb_clip_temp = y_clip_temp + this.currentClipElemnt.clipRect.Height * this.zoomLevel - 1;
                imgW_temp = this.imgEditBgWidth;
                imgH_temp = this.imgEditBgHeight;
                if (inEditing&&e.Button.Equals(MouseButtons.Left))//正在编辑中
                {
                    switch (clipEditMode)
                    {
                        case EMODE_L://左边界移动
                            editEdge(cursorXNew, cursorYNew, 0);
                            break;
                        case EMODE_R://右边界移动
                            editEdge(cursorXNew, cursorYNew, 1);
                            break;
                        case EMODE_T://上边界移动
                            editEdge(cursorXNew, cursorYNew, 2);
                            break;
                        case EMODE_B://底边界移动
                            editEdge(cursorXNew, cursorYNew, 3);
                            break;
                        case EMODE_LT://左上边界移动
                            editEdge(cursorXNew, cursorYNew, 0);
                            editEdge(cursorXNew, cursorYNew, 2);
                            break;
                        case EMODE_RB://右下界移动
                            editEdge(cursorXNew, cursorYNew, 1);
                            editEdge(cursorXNew, cursorYNew, 3);
                            break;
                        case EMODE_RT://右上边界移动
                            editEdge(cursorXNew, cursorYNew, 1);
                            editEdge(cursorXNew, cursorYNew, 2);
                            break;
                        case EMODE_LB://左下边界移动
                            editEdge(cursorXNew, cursorYNew, 0);
                            editEdge(cursorXNew, cursorYNew, 3);
                            break;
                        case EMODE_DRAG://整体拖动
                            editEdge(cursorXNew, cursorYNew, 4);
                            break;
                    }
                    updateEditRegion();
                }
                else //查找可编辑状态
                {
                    if (closeTo(cursorXNew, x_clip_temp))
                    {
                        if (closeTo(y_clip_temp, cursorYNew))
                        {
                            clipEditMode = EMODE_LT;
                        }
                        else if (closeTo(yb_clip_temp, cursorYNew))
                        {
                            clipEditMode = EMODE_LB;
                        }
                        else if (MathUtil.inRegion(cursorYNew, y_clip_temp, yb_clip_temp))
                        {
                            clipEditMode = EMODE_L;
                        }
                        else
                        {
                            clipEditMode = EMODE_NONE;
                        }
                    }
                    else if (closeTo(cursorXNew, xr_clip_temp))
                    {
                        if (closeTo(y_clip_temp, cursorYNew))
                        {
                            clipEditMode = EMODE_RT;
                        }
                        else if (closeTo(yb_clip_temp, cursorYNew))
                        {
                            clipEditMode = EMODE_RB;
                        }
                        else if (MathUtil.inRegion(cursorYNew, y_clip_temp, yb_clip_temp))
                        {
                            clipEditMode = EMODE_R;
                        }
                        else
                        {
                            clipEditMode = EMODE_NONE;
                        }
                    }
                    else if (closeTo(cursorYNew, y_clip_temp))
                    {
                        if (closeTo(x_clip_temp, cursorXNew))
                        {
                            clipEditMode = EMODE_LT;
                        }
                        else if (closeTo(xr_clip_temp, cursorYNew))
                        {
                            clipEditMode = EMODE_RT;
                        }
                        else if (MathUtil.inRegion(cursorXNew, x_clip_temp, xr_clip_temp))
                        {
                            clipEditMode = EMODE_T;
                        }
                        else
                        {
                            clipEditMode = EMODE_NONE;
                        }
                    }
                    else if (closeTo(cursorYNew, yb_clip_temp))
                    {
                        if (closeTo(x_clip_temp, cursorXNew))
                        {
                            clipEditMode = EMODE_LB;
                        }
                        else if (closeTo(xr_clip_temp, cursorYNew))
                        {
                            clipEditMode = EMODE_RB;
                        }
                        else if (MathUtil.inRegion(cursorXNew, x_clip_temp, xr_clip_temp))
                        {
                            clipEditMode = EMODE_B;
                        }
                        else
                        {
                            clipEditMode = EMODE_NONE;
                        }
                    }
                    else if (MathUtil.inRegion(cursorXNew, x_clip_temp, xr_clip_temp) && MathUtil.inRegion(cursorYNew, y_clip_temp, yb_clip_temp))
                    {
                        clipEditMode = EMODE_DRAG;
                    }
                    else
                    {
                        clipEditMode = EMODE_NONE;
                    }
                    setCursorState();
                }
            }
            else //当前处于拖动窗口状态，按下左键拖动窗口或者拖动当前切片
            {
                if (e.Button.Equals(MouseButtons.Left))//按下左键拖动窗口
                {
                    imgEditCenterPixelX = imgEditCenterPixelX_Pre + (cursorXNew - cursorX) / zoomLevel;
                    imgEditCenterPixelY = imgEditCenterPixelY_Pre + (cursorYNew - cursorY) / zoomLevel;
                    adjustCenterPixel();
                    updateEditRegion();
                }
            }
        }
        //编辑边框
        private void editEdge(int cursorXNew, int cursorYNew, int index)
        {
            switch (index)
            {
                case 0://左边
                    x_clip_temp = x_clip + cursorXNew - cursorX;
                    if (x_clip_temp > xr_clip - zoomLevel)
                    {
                        x_clip_temp = xr_clip - zoomLevel;
                    }
                    if (x_clip_temp < imgEditBgLeft)
                    {
                        x_clip_temp = imgEditBgLeft;
                    }
                    currentClipElemnt.clipRect.X = (x_clip_temp - imgEditBgLeft) / zoomLevel;
                    currentClipElemnt.clipRect.Width = (xr_clip - (imgEditBgLeft + currentClipElemnt.clipRect.X * zoomLevel)) / zoomLevel;
                    break;
                case 1://右边
                    xr_clip_temp = xr_clip + cursorXNew - cursorX;
                    if (xr_clip_temp < x_clip + zoomLevel)
                    {
                        xr_clip_temp = x_clip + zoomLevel;
                    }
                    if (xr_clip_temp > imgEditBgLeft + imgW_temp)
                    {
                        xr_clip_temp = imgEditBgLeft + imgW_temp;
                    }
                    currentClipElemnt.clipRect.Width = (xr_clip_temp - x_clip_temp) / zoomLevel;
                    break;
                case 2://上边
                    y_clip_temp = y_clip + cursorYNew - cursorY;
                    if (y_clip_temp > yb_clip - zoomLevel)
                    {
                        y_clip_temp = yb_clip - zoomLevel;
                    }
                    if (y_clip_temp < imgEditBgTop)
                    {
                        y_clip_temp = imgEditBgTop;
                    }
                    currentClipElemnt.clipRect.Y = (y_clip_temp - imgEditBgTop) / zoomLevel;
                    currentClipElemnt.clipRect.Height = (yb_clip - (imgEditBgTop + currentClipElemnt.clipRect.Y * zoomLevel)) / zoomLevel;
                    break;
                case 3://下边
                    yb_clip_temp = yb_clip + cursorYNew - cursorY;
                    if (yb_clip_temp < y_clip + zoomLevel)
                    {
                        yb_clip_temp = y_clip + zoomLevel;
                    }
                    if (yb_clip_temp > imgEditBgTop + imgH_temp)
                    {
                        yb_clip_temp = imgEditBgTop + imgH_temp;
                    }
                    currentClipElemnt.clipRect.Height = (yb_clip_temp - y_clip_temp) / zoomLevel;
                    break;
                case 4:
                    int clipW = currentClipElemnt.clipRect.Width * zoomLevel;
                    int clipH = currentClipElemnt.clipRect.Height * zoomLevel;
                    x_clip_temp = x_clip + cursorXNew - cursorX;
                    if (x_clip_temp > imgEditBgLeft + imgW_temp - clipW)
                    {
                        x_clip_temp = imgEditBgLeft + imgW_temp - clipW;
                    }
                    if (x_clip_temp < imgEditBgLeft)
                    {
                        x_clip_temp = imgEditBgLeft;
                    }
                    y_clip_temp = y_clip + cursorYNew - cursorY;
                    if (y_clip_temp > imgEditBgTop + imgH_temp - clipH)
                    {
                        y_clip_temp = imgEditBgTop + imgH_temp - clipH;
                    }
                    if (y_clip_temp < imgEditBgTop)
                    {
                        y_clip_temp = imgEditBgTop;
                    }
                    currentClipElemnt.clipRect.X = (x_clip_temp - imgEditBgLeft) / zoomLevel;
                    currentClipElemnt.clipRect.Y = (y_clip_temp - imgEditBgTop) / zoomLevel;
                    break;
            }
            if (currentClipElemnt.clipRect.Width < 0)
            {
                currentClipElemnt.clipRect.Width = 1;
            }
            if (currentClipElemnt.clipRect.Height < 0)
            {
                currentClipElemnt.clipRect.Height = 1;
            }
            currentClipElemnt.resetImgClip();
        }
        //是否在一定范围内接近
        private const int coloseGap = 3;
        private bool closeTo(int num0, int num1)
        {
            return Math.Abs(num0 - num1) < coloseGap;
        }
        //按照当前编辑模式设定鼠标状态
        private void setCursorState()
        {
            switch (clipEditMode)
            {
                case EMODE_NONE://无移动
                    panel_clipEdit.Cursor = Cursors.Default;
                    break;
                case EMODE_L://左边界移动
                case EMODE_R://右边界移动
                    panel_clipEdit.Cursor = Cursors.SizeWE;
                    break;
                case EMODE_T://上边界移动
                case EMODE_B://底边界移动
                    panel_clipEdit.Cursor = Cursors.SizeNS;
                    break;
                case EMODE_LT://左上边界移动
                case EMODE_RB://右下界移动
                    panel_clipEdit.Cursor = Cursors.SizeNWSE;
                    break;
                case EMODE_RT://右上边界移动
                case EMODE_LB://左下边界移动
                    panel_clipEdit.Cursor = Cursors.SizeNESW;
                    break;
                case EMODE_DRAG://整体拖动
                    panel_clipEdit.Cursor = Cursors.SizeAll;
                    break;
            }
        }
        private void pictureBox_clipEdit_MouseDown(object sender, MouseEventArgs e)
        {
            panel_clipEdit.Focus();
            cursorX = e.X;
            cursorY = e.Y;
            imgEditCenterPixelX_Pre = imgEditCenterPixelX;
            imgEditCenterPixelY_Pre = imgEditCenterPixelY;
            if (!panel_clipEdit.Cursor.Equals(handCursor) && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right))//非布局拖动状态
            {
                if (ModifierKeys == Keys.Control)//拖动
                {
                    if (!inMoveImgCLip && currentClipElments.Count > 0 && form_MA != null)
                    {
                        inMoveImgCLip = true;
                        moveImgClipBox = new FloatForm();
                        Rectangle region = getCurrentClipElmentsSize();
                        int zoomClips = form_MA.form_MFrameEdit.zoomLevel;
                        moveImgClipBox.Size = new Size(region.Width * zoomClips, region.Height * zoomClips);
                        moveImgClipBox.MouseMove += new MouseEventHandler(moveATPanel_MouseMove);
                        moveImgClipBox.MouseUp += new MouseEventHandler(moveATPanel_MouseUp);
                        //设置位置
                        Point p = panel_clipEdit.PointToScreen(e.Location);
                        p = this.PointToClient(p);
                        p.X -= moveImgClipBox.Width / 2;
                        p.Y -= moveImgClipBox.Height / 2;
                        moveImgClipBox.Location = p;
                        //绘制
                        Image img = GraphicsUtil.createImage(moveImgClipBox.Width, moveImgClipBox.Height, Color.Transparent);
                        Graphics g = Graphics.FromImage(img);
                        //currentAntetype.display(g, 0, 0, moveATBox.Width, moveATBox.Height, 1, true);
                        foreach (MClipElement bClip in currentClipElments)
                        {
                            bClip.display(g, (bClip.clipRect.X - region.X) * zoomClips, (bClip.clipRect.Y - region.Y) * zoomClips, zoomClips);
                        }
                        //刷新背景
                        FloatForm.fSetBackground(moveImgClipBox, (Bitmap)img);
                        moveImgClipBox.Refresh();
                        //显示窗体
                        moveImgClipBox.Show();
                        moveImgClipBox.Capture = true;
                    }
                }
                else//选取
                {
                    if (clipEditMode == EMODE_NONE || clipEditMode == EMODE_DRAG)
                    {
                        MClipElement clip = focousEditClip(e.X, e.Y, e.Button == MouseButtons.Right);
                        if (ModifierKeys == Keys.Shift)//多选
                        {
                            if (currentClipElments.Contains(clip))//反选
                            {
                                currentClipElments.Remove(clip);
                                if (currentClipElments.Count > 0)
                                {
                                    clip = currentClipElments[currentClipElments.Count - 1];
                                }
                                else
                                {
                                    clip = null;
                                }
                            }
                            else if (clip != null)
                            {
                                currentClipElments.Add(clip);
                            }
                        }
                        else
                        {
                            currentClipElments.Clear();//取消选择
                            if (clip != null)
                            {
                                currentClipElments.Add(clip);
                            }
                        }
                        currentClipElemnt = clip;
                        //setFocusClip(clip, false);
                        updateEditRegion();
                    }
                    if (clipEditMode != EMODE_NONE && currentClipElemnt != null && !inEditing)//转到编辑状态
                    {
                        this.inEditing = true;
                        Console.WriteLine("inEditing:" + inEditing);
                        readyHistory(HistoryType.Clips);
                        x_clip = this.imgEditBgLeft + this.currentClipElemnt.clipRect.X * this.zoomLevel;
                        y_clip = this.imgEditBgTop + this.currentClipElemnt.clipRect.Y * this.zoomLevel;
                        xr_clip = x_clip + this.currentClipElemnt.clipRect.Width * this.zoomLevel;
                        yb_clip = y_clip + this.currentClipElemnt.clipRect.Height * this.zoomLevel;
                    }
                }
            }

        }

        private void pictureBox_clipEdit_MouseUp(object sender, MouseEventArgs e)
        {
            if (inEditing)
            {
                this.inEditing = false;
                Console.WriteLine("inEditing:" + inEditing);
                addHistory(HistoryType.Clips);
                if (form_MA != null)
                {
                    form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
                }
            }
            cursorX = e.X;
            cursorY = e.Y;
            cursorXNew = cursorX;
            cursorYNew = cursorY;
        }

        //private void pictureBox_Browse_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    if (currentClipElemnt == null)
        //    {
        //        return;
        //    }
        //    if (e.KeyValue == (int)Keys.Up || e.KeyValue == (int)Keys.Down || e.KeyValue == (int)Keys.Left || e.KeyValue == (int)Keys.Right || e.KeyValue == (int)Keys.Delete)
        //    {
        //        int index = animBaseClipsManager.getElementID(currentClipElemnt);
        //        int count = animBaseClipsManager.getElementCount();
        //        switch (e.KeyValue)
        //        {
        //            case (int)Keys.Up:
        //            case (int)Keys.Left:
        //                if (index > 0)
        //                {
        //                    index--;
        //                    this.setFocusClip((MAnimBaseClipElement)animBaseClipsManager[index), true);
        //                }
        //                break;
        //            case (int)Keys.Down:
        //            case (int)Keys.Right:
        //                if (index < count - 1)
        //                {
        //                    index++;
        //                    this.setFocusClip((MAnimBaseClipElement)animBaseClipsManager[index), true);
        //                }
        //                break;
        //            case (int)Keys.Delete:
        //                if (currentClipElemnt != null)
        //                {
        //                    deleteCurrentClip();
        //                }
        //                break;
        //        }
        //        updateEditRegion();
        //    }
        //}

        //private void pictureBox_Browse_MouseEnter(object sender, EventArgs e)
        //{
        //    //pictureBox_Browse.Focus();
        //}

        private void pictureBox_clipEdit_DoubleClick(object sender, EventArgs e)
        {
            if (panel_clipEdit.Cursor.Equals(handCursor))//非拖动状态
            {
                return;
            }
            MouseEventArgs ex = (MouseEventArgs)e;
            if (!ex.Button.Equals(MouseButtons.Left))
            {
                return;
            }
            if (currentClipElemnt != null)
            {
                return;
            }
            int x = (cursorX - imgEditBgLeft) / zoomLevel;
            int y = (cursorY - imgEditBgTop) / zoomLevel;
            int w = defaultW;
            int h = defaultH;
            MClipElement newClipElement = null;
            if (x < 0 || y < 0)
            {
                return;
            }
            int imgIndex = listBox_Images.SelectedIndex;
            if (x + w > imgEditBgWidth / zoomLevel)
            {
                w = imgEditBgWidth / zoomLevel - x;
                if (w < 1)
                {
                    return;
                }
            }
            if (y + h > imgEditBgHeight / zoomLevel)
            {
                h = imgEditBgHeight / zoomLevel - y;
                if (h < 1)
                {
                    return;
                }
            }
            readyHistory(HistoryType.Clips);
            newClipElement = createImageClip(imgIndex, x, y, w, h);
            addHistory(HistoryType.Clips);
            currentClipElments.Clear();
            currentClipElments.Add(newClipElement);
            setFocusClip(newClipElement, true);

            noListBoxEvent = true;
            mImgsManager.MNodeUI.UpdateItem(newClipElement.getImageID());
            noListBoxEvent = false;

        }




        private void panel_EditorBg_Paint(object sender, PaintEventArgs e)
        {
            this.updateEditRegion();
        }


        private void Form_MImgsList_KeyUp(object sender, KeyEventArgs e)
        {
            if (panel_clipEdit.Focused)
            {
                if (e.KeyCode == Keys.Space)
                {
                    if (!panel_clipEdit.Cursor.Equals(Cursors.Default))
                    {
                        panel_clipEdit.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void Form_MImgsList_KeyDown(object sender, KeyEventArgs e)
        {
            if (panel_clipEdit.Focused)
            {
                if (!panel_clipEdit.Cursor.Equals(handCursor))
                {
                    if (currentClipElemnt != null)
                    {
                        switch (e.KeyValue)
                        {
                            case (int)Keys.Up:
                                currentClipElemnt.clipRect.Y--;
                                if (currentClipElemnt.clipRect.Y < 0)
                                {
                                    currentClipElemnt.clipRect.Y = 0;
                                }
                                currentClipElemnt.resetImgClip();
                                break;
                            case (int)Keys.Down:
                                currentClipElemnt.clipRect.Y++;
                                if (currentClipElemnt.clipRect.Y + currentClipElemnt.clipRect.Height > imgEditBgHeight / zoomLevel)
                                {
                                    currentClipElemnt.clipRect.Y--;
                                }
                                currentClipElemnt.resetImgClip();
                                break;
                            case (int)Keys.Left:
                                currentClipElemnt.clipRect.X--;
                                if (currentClipElemnt.clipRect.X < 0)
                                {
                                    currentClipElemnt.clipRect.X = 0;
                                }
                                currentClipElemnt.resetImgClip();
                                break;
                            case (int)Keys.Right:
                                currentClipElemnt.clipRect.X++;
                                if (currentClipElemnt.clipRect.X + currentClipElemnt.clipRect.Width > imgEditBgWidth / zoomLevel)
                                {
                                    currentClipElemnt.clipRect.X--;
                                }
                                currentClipElemnt.resetImgClip();
                                break;
                            case (int)Keys.Delete:
                                if (currentClipElemnt != null)
                                {
                                    deleteCurrentClip();
                                }
                                break;
                        }
                    }
                    if (e.KeyValue == (int)Keys.Space)
                    {
                        this.inEditing = false;
                        Console.WriteLine("inEditing:" + inEditing);
                        panel_clipEdit.Cursor = handCursor;
                        Point p = MousePosition;
                        p = panel_clipEdit.PointToClient(p);
                        cursorX = p.X;
                        cursorY = p.Y;
                        Console.WriteLine(p.X + "," + p.Y);
                        imgEditCenterPixelX_Pre = imgEditCenterPixelX;
                        imgEditCenterPixelY_Pre = imgEditCenterPixelY;
                    }
                }
                else
                {
                    switch (e.KeyValue)
                    {
                        case (int)Keys.Up:
                            this.imgEditCenterPixelY--;
                            break;
                        case (int)Keys.Down:
                            this.imgEditCenterPixelY++;
                            break;
                        case (int)Keys.Left:
                            this.imgEditCenterPixelX--;
                            break;
                        case (int)Keys.Right:
                            this.imgEditCenterPixelX++;
                            break;
                    }
                    adjustCenterPixel();
                }
                updateEditRegion();
            }
            else
            {
                Control c = ActiveControl;
            }
        }
        //----------------------------移动切片到帧内的逻辑-------------------------------------------------
        //获取当前多选切片的整体剪切区域
        public Rectangle getCurrentClipElmentsSize()
        {
            if (currentClipElments.Count == 0)
            {
                return new Rectangle(0, 0,0,0);
            }
            int xMin = int.MaxValue, xMax = 0, yMin = int.MaxValue, yMax = 0;
            foreach (MClipElement clip in currentClipElments)
            {
                if (xMin > clip.clipRect.X)
                {
                    xMin = clip.clipRect.X;
                }
                if (xMax < clip.clipRect.X + clip.clipRect.Width)
                {
                    xMax = clip.clipRect.X + clip.clipRect.Width;
                }
                if (yMin > clip.clipRect.Y)
                {
                    yMin = clip.clipRect.Y;
                }
                if (yMax < clip.clipRect.Y + clip.clipRect.Height)
                {
                    yMax = clip.clipRect.Y + clip.clipRect.Height;
                }
            }
            return new Rectangle(xMin,yMin,xMax - xMin, yMax - yMin);

        }
        private void moveATPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (inMoveImgCLip && e.Button == MouseButtons.Left)
            {
                Point p = moveImgClipBox.PointToScreen(e.Location);
                //p = this.PointToClient(p);
                p.X -= moveImgClipBox.Width / 2;
                p.Y -= moveImgClipBox.Height / 2;
                moveImgClipBox.Location = p;
            }
        }
        private void moveATPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (inMoveImgCLip)
            {
                if (moveImgClipBox != null)
                {

                    Point editPoint = moveImgClipBox.PointToScreen(e.Location);
                    if (Controls.Contains(moveImgClipBox))
                    {
                        Controls.Remove(moveImgClipBox);
                    }
                    moveImgClipBox.Close();
                    moveImgClipBox.Dispose();
                    moveImgClipBox = null;
                    //是否在当前编辑区内
                    form_MA.form_MFrameEdit.addClips(editPoint, currentClipElments);
                }
                inMoveImgCLip = false;
                clipEditMode = EMODE_NONE;
                setCursorState();
            }
        }

        #region MIO 成员

        public void ReadObject(Stream s)
        {
            mImgsManager.ReadObject(s);
            MClipsManager.ReadObject(s);
        }

        public void WriteObject(Stream s)
        {
            mImgsManager.WriteObject(s);
            MClipsManager.WriteObject(s);
        }

        public void ExportObject(Stream s)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        private void TSB_Help_Click(object sender, EventArgs e)
        {
            SmallDialog_ShowMessage.showMessage("图片库帮助", TSB_Help.ToolTipText);
        }

        #region MNodeUI<MImageElement> 成员

        public void AddItem(MImgElement item)
        {
            listBox_Images.Items.Add(item.getValueToLenString());
        }

        public void SetItem(int index, MImgElement item)
        {
            listBox_Images.Items[index] = item.getValueToLenString();
        }
        public void UpdateItem(int index)
        {
            if (index < 0)
            {
                return;
            }
            String value = mImgsManager[index].getValueToLenString();
            listBox_Images.Items[index] = value;
        }
        public void SetSelectedItem(int index)
        {
            listBox_Images.SelectedIndices.Clear();
            listBox_Images.SelectedIndex = index;
        }

        public void InsertItem(int index, MImgElement item)
        {
            listBox_Images.Items.Insert(index, item.getValueToLenString());
        }

        public void RemoveItemAt(int index)
        {
            listBox_Images.Items.RemoveAt(index);
        }

        public void ClearItems()
        {
            this.listBox_Images.Items.Clear();
        }

        #endregion

        #region MImgsManagerHolder 成员

        public MImgsManager GetImgsManager()
        {
            return this.mImgsManager;
        }

        #endregion
        //准备切片更改记录
        private void readyHistory(HistoryType ht)
        {
            if (form_MA == null)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(ht);
        }
        private void addHistory(HistoryType ht)
        {
            if (form_MA == null)
            {
                return;
            }
            form_MA.historyManager.AddHistory(ht);
        }
        private void clearHistory()
        {
            if (form_MA == null)
            {
                return;
            }
            form_MA.historyManager.ClearHistory();
        }
        private void pictureBox_ClipUse_MouseDown(object sender, MouseEventArgs e)
        {
            Point pLocal = ((PictureBox)(sender)).Location;
            pLocal.Y += 1;
            pLocal.X += 1;
            ((PictureBox)(sender)).Location = pLocal;
        }

        private void pictureBox_ClipCombine_MouseDown(object sender, MouseEventArgs e)
        {
            Point pLocal = ((PictureBox)(sender)).Location;
            pLocal.Y += 1;
            pLocal.X += 1;
            ((PictureBox)(sender)).Location = pLocal;
        }

        private void pictureBox_ClipUse_Click(object sender, EventArgs e)
        {
            if (currentClipElemnt == null)
            {
                return;
            }
            String s = currentClipElemnt.getUsedInfor();
            SmallDialog_ShowParagraph.showString("切片使用检查", s);
        }

        private void pictureBox_ClipCombine_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("即将进行重复合并，是否保留未使用的单元？", "警告：", MessageBoxButtons.YesNoCancel);
            if (!res.Equals(DialogResult.Cancel))
            {
                readyHistory(HistoryType.Clips);
                MClipsManager.ClearSpilth(res.Equals(DialogResult.Yes));
                addHistory(HistoryType.Clips);
                currentClipElemnt = null;
                updateEditRegion();
            }
        }

        private void pictureBox_ClipUse_MouseUp(object sender, MouseEventArgs e)
        {
            Point pLocal = ((PictureBox)(sender)).Location;
            pLocal.Y -= 1;
            pLocal.X -= 1;
            ((PictureBox)(sender)).Location = pLocal;
        }

        private void pictureBox_ClipCombine_MouseUp(object sender, MouseEventArgs e)
        {
            Point pLocal = ((PictureBox)(sender)).Location;
            pLocal.Y -= 1;
            pLocal.X -= 1;
            ((PictureBox)(sender)).Location = pLocal;
        }

        private void TSB_link_Click(object sender, EventArgs e)
        {
            int count = this.listBox_Images.SelectedIndices.Count;
            if (count < 0)
            {
                return;
            }
            //搜集目标
            List<MImgElement> imgElements = new List<MImgElement>();
            for (int i = 0; i < listBox_Images.SelectedIndices.Count; i++)
            {
                int id = listBox_Images.SelectedIndices[i];
                imgElements.Add(mImgsManager[id]);
            }
            //准备历史记录
            if (form_MA != null)
            {
                form_MA.historyManager.ReadyHistory_ImgProp(imgElements);
            }
            //找到最小的链接ID
            int minLinkID = int.MaxValue;
            foreach (MImgElement imgElement in imgElements)
            {
                if (imgElement.linkID>=0 && imgElement.linkID < minLinkID)
                {
                    minLinkID = imgElement.linkID;
                }
            }
            if (minLinkID == int.MaxValue)
            {
                minLinkID = mImgsManager.getMinUnunsedLinkID();
            }
            //更新所有链接ID
            foreach (MImgElement imgElement in imgElements)
            {
                imgElement.linkID = minLinkID;
                mImgsManager.MNodeUI.UpdateItem(imgElement.GetID());
            }
            //加入历史记录
            if (form_MA != null)
            {
                form_MA.historyManager.AddHistory_ImgProp(imgElements);
            }
        }

        private void TSB_linkBreak_Click(object sender, EventArgs e)
        {
            int count = this.listBox_Images.SelectedIndices.Count;
            if (count < 0)
            {
                return;
            }
            //搜集目标
            List<MImgElement> imgElements = new List<MImgElement>();
            for (int i = 0; i < listBox_Images.SelectedIndices.Count; i++)
            {
                int id = listBox_Images.SelectedIndices[i];
                imgElements.Add(mImgsManager[id]);
            }
            //准备历史记录
            if (form_MA != null)
            {
                form_MA.historyManager.ReadyHistory_ImgProp(imgElements);
            }
            //更新所有链接ID
            foreach (MImgElement imgElement in imgElements)
            {
                imgElement.linkID = -1;
                mImgsManager.MNodeUI.UpdateItem(imgElement.GetID());
            }
            //加入历史记录
            if (form_MA != null)
            {
                form_MA.historyManager.AddHistory_ImgProp(imgElements);
            }
        }

        private void TSB_linkTest_Click(object sender, EventArgs e)
        {
            int count = this.listBox_Images.SelectedIndices.Count;
            if (count < 0)
            {
                MessageBox.Show("请先选择一个你需要测试的链接图片组", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //搜集目标
            List<MImgElement> imgElements = new List<MImgElement>();
            for (int i = 0; i < listBox_Images.SelectedIndices.Count; i++)
            {
                int id = listBox_Images.SelectedIndices[i];
                imgElements.Add(mImgsManager[id]);
            }
            //检查是否是相同的图片组
            int sameLinkID = imgElements[0].linkID;
            bool same = sameLinkID >= 0;
            if (same)
            {
                foreach (MImgElement imgElement in imgElements)
                {
                    if (imgElement.linkID != sameLinkID || imgElement.forbidOptimize)
                    {
                        same = false;
                        break;
                    }
                }
            }
            if (!same)
            {
                MessageBox.Show("优化测试被终止，原因可能是：\n\n1、你的选择包含未链接的图片\n2、你的选择包含禁止优化的图片\n3、你同时选择了多个链接图片组\n这些情况下均无法进行优化测试。", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //重新搜集目标
            imgElements.Clear();
            for (int i = 0; i < mImgsManager.Count(); i++)
            {
                if (mImgsManager[i].linkID == sameLinkID && !mImgsManager[i].forbidOptimize)
                {
                    imgElements.Add(mImgsManager[i]);
                }
            }
            //进入测试
            Size needSize;
            int usedSpace;
            Image imgGenerate;
            MImgsManager.optimizeClips(imgElements, out needSize, out usedSpace, out imgGenerate,false);
            int needSpace=needSize.Width * needSize.Height;
            //统计数据
            String report = "贴图需求尺寸：" + needSize.Width + "×" + needSize.Height + "，占用面积：" + needSpace + "像素\n";
            report += "实际使用尺寸：" + usedSpace + "像素，图片空白尺寸：" + (needSpace - usedSpace) + "像素\n";
            report += "图片使用率：" +( usedSpace*100.0f / needSpace) + "%";
            //将结果显示出来
            SmallDialog_ShowPicture.ShowPicture("优化测试结果", report, imgGenerate);
        }
        private void pictureBox_transferClip_Click(object sender, EventArgs e)
        {
            setTransferClipMode(!Consts.transferClipMode);
        }
        private void setTransferClipMode(bool modeOn)
        {
            if (modeOn)
            {
                pictureBox_transferClip.BackgroundImage = global::Cyclone.Properties.Resources.transferClipOn;
                this.toolTip.SetToolTip(this.pictureBox_transferClip, "跨图转移切片模式(开)");
            }
            else
            {
                pictureBox_transferClip.BackgroundImage = global::Cyclone.Properties.Resources.transferClipOff;
                this.toolTip.SetToolTip(this.pictureBox_transferClip, "跨图转移切片模式(关)");
            }
            Consts.transferClipMode = modeOn;
        }

        private void Form_MImgsList_Shown(object sender, EventArgs e)
        {
            setTransferClipMode(Consts.transferClipMode);
        }
    }
}