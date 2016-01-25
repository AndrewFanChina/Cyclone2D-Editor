using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.mod;
using System.Collections;
using System.Threading;
using System.IO;
using DockingUI.WinFormsUI.Docking;
using Cyclone.mod.anim;
using Cyclone.alg;
using Cyclone.alg.util;
using Cyclone.alg.win32;

namespace Cyclone.mod.anim
{
    public partial class Form_MAnimation : Form, MIO, MParentNode, MSonNode
    {
        //########################################### 界面定义 ###########################################################
        public Form_Main form_Main;
        public Form_MActorsList form_MActorList;
        public Form_MFrameEdit form_MFrameEdit;
        public Form_MTimeLine form_MTimeLine;
        public Form_MFrameLevel form_MFrameLevel;
        public Form_MConfig form_MConfig;
        public Form_MImgsList form_MImgsList;
        public Form_MCLib form_MCLib;
        public Form_MAnimPlay form_MAnimPW;
        //########################################### 数据定义 ###########################################################
        //常量
        private const int BORDER_1 = 1;//帧预览图边框
        private const int GAP_1 = 1;//帧预览图间隔
        public int LEVEL_GAP = 1;//切片层之间的间隔
        public static bool noScrollEvent = false;//屏蔽非用户操作的滚动条改变
        public static bool noCheckEvent = false;//屏蔽非用户操作的多选框改变
        public static bool noListBoxEvent = false;//屏蔽非用户操作的列表框改变
        public static bool noNumericEvent = false;//屏蔽非用户操作的数字框改变
        public static bool noNodeEvent = false;//屏蔽非用户操作的TreeView事件
        public static bool transCmdKey = false;//转发命令按键

        //布局存储路径
        String layoutPath;
        ArrayList dockContentes = new ArrayList();
        //########################################### 构造和初始化 #######################################################
        //private Form_BaseClipsEditor clipsManager = null;
        public Form_MAnimation(Form_Main form_MainT)
        {
            InitializeComponent();
            form_Main = form_MainT;
            Control.CheckForIllegalCrossThreadCalls = false;
            layoutPath = Consts.PATH_EXE_FOLDER + "\\layout_local.xml";

            form_MActorList = new Form_MActorsList(this);
            form_MFrameEdit = new Form_MFrameEdit(this);
            form_MFrameLevel = new Form_MFrameLevel(this);
            form_MConfig = new Form_MConfig(this);
            form_MImgsList = new Form_MImgsList(this);
            form_MCLib = new Form_MCLib();
            form_MTimeLine = new Form_MTimeLine(this);
            form_MAnimPW = new Form_MAnimPlay(this);

            dockContentes.Add(form_MActorList);
            dockContentes.Add(form_MFrameEdit);
            dockContentes.Add(form_MFrameLevel);
            dockContentes.Add(form_MConfig);
            dockContentes.Add(form_MImgsList);
            dockContentes.Add(form_MCLib);
            dockContentes.Add(form_MTimeLine);
            dockContentes.Add(form_MAnimPW);

            for (int i = 0; i < dockContentes.Count; i++)
            {
                DockContent dockI = ((DockContent)dockContentes[i]);
                dockI.HideOnClose = true;
                dockI.DockStateChanged += new EventHandler(all_DockStateChanged);
                dockI.SizeChanged += new EventHandler(all_DockStateChanged);
                dockI.FormClosing += new FormClosingEventHandler(all_DockContentClosing);
            }
            if (File.Exists(layoutPath))
            {
                DeserializeDockContent dsd = new DeserializeDockContent(IDockContentMe);
                panel_DockPanel.LoadFromXml(layoutPath, dsd);
                refreshDockState();
            }
            else
            {
                resetPanels();
            }
            historyManager = new MA_HistoryManager(this);
        }
        public IDockContent IDockContentMe(String s)
        {
            if (s.Equals("Form_MActorsList"))
            {
                return form_MActorList;
            }
            else if (s.Equals("Form_MFrameEdit"))
            {
                return form_MFrameEdit;
            }
            if (s.Equals("Form_MTimeLine"))
            {
                return form_MTimeLine;
            }
            if (s.Equals("Form_MConfig"))
            {
                return form_MConfig;
            }
            if (s.Equals("Form_MImgsList"))
            {
                return form_MImgsList;
            }
            if (s.Equals("Form_MCLib"))
            {
                return form_MCLib;
            }
            if (s.Equals("Form_MFrameLevel"))
            {
                return form_MFrameLevel;
            }
            if (s.Equals("Form_MAnimPW"))
            {
                return form_MAnimPW;
            }
            return null;
        }
        public void refreshDockState()
        {
            //同步显示状态
            影片角色列表ToolStripMenuItem.Checked = !form_MActorList.IsHidden;
            影片帧编辑区ToolStripMenuItem.Checked = !form_MFrameEdit.IsHidden;
            影片时间轴ToolStripMenuItem.Checked = !form_MTimeLine.IsHidden;
            配置面板ToolStripMenuItem.Checked = !form_MConfig.IsHidden;
            影片图库ToolStripMenuItem.Checked = !form_MImgsList.IsHidden;
            元件库ToolStripMenuItem.Checked = !form_MCLib.IsHidden;
            帧图层ToolStripMenuItem.Checked = !form_MFrameLevel.IsHidden;
            动画演示ToolStripMenuItem.Checked = !form_MAnimPW.IsHidden;
        }
        public void all_DockStateChanged(object sender, EventArgs e)
        {
            if (!this.Visible || inResetPanels)
            {
                return;
            }
            refreshDockState();
            panel_DockPanel.SaveAsXml(layoutPath);
        }
        public void all_DockContentClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Visible)
            {
                return;
            }
            refreshDockState();
            panel_DockPanel.SaveAsXml(layoutPath);
        }
        //第一次初始化
        public void initFirstTime()
        {
            noCheckEvent = true;

            noCheckEvent = false;
        }
        //初始化参数
        public void initParams()
        {
            if (form_MActorList.treeView_Animation.Nodes.Count == 0)
            {
                form_MActorList.updateTreeView_Animation();
            }
        }
        //释放资源
        public void releaseRes()
        {
            //当前基础帧
            form_MTimeLine.focusFrame = null;
        }
        private void button_closeImageManager_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_closeAE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_AnimationEditor_SizeChanged(object sender, EventArgs e)
        {
            if (this.Size.Width > 0)
            {
                if (form_MFrameEdit != null)
                {
                    form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
                }
                if (form_MAnimPW != null)
                {
                    form_MAnimPW.Update_GlView(true);
                }
            }
        }

        private void comboBox_screen_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_showLogicBox_CheckedChanged(object sender, EventArgs e)
        {
            if (noCheckEvent)
            {
                return;
            }
            this.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
        }


        private void checkBox_showScreen_CheckedChanged(object sender, EventArgs e)
        {

        }



        private void button_ClearSpilth_MouseLeave(object sender, EventArgs e)
        {
            this.showInfor("");
        }


        private void Form_AnimationEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void button_Save_Click(object sender, EventArgs e)
        {
            if (form_Main.saveUserData())
            {
                MessageBox.Show("文档保存完毕", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void button_BgColor_Click(object sender, EventArgs e)
        {

        }

        //---------------------------------------历史记录相关---------------------------------------
        public MA_HistoryManager historyManager;
        public void button_Undo_Click(object sender, EventArgs e)
        {
            historyManager.Undo();
            refreshHistoryButtons();
        }

        public void button_Redo_Click(object sender, EventArgs e)
        {
            historyManager.Redo();
            refreshHistoryButtons();
        }
        public void clearHistory()
        {
            historyManager.ClearHistory();
            refreshHistoryButtons();
        }

        public void refreshHistoryButtons()
        {
            撤销ToolStripMenuItem.Enabled = historyManager.CanUndo();
            重做ToolStripMenuItem.Enabled = historyManager.CanRedo();
            //button_ClearHistory.Enabled = animUndoManager.CanUndo() || animUndoManager.CanRedo();
        }

        //---------------------------------------其它菜单事件---------------------------------------
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form_Main.saveUserData())
            {
                MessageBox.Show("文档保存完毕", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void 影片角色列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            影片角色列表ToolStripMenuItem.Checked = !影片角色列表ToolStripMenuItem.Checked;
            if (影片角色列表ToolStripMenuItem.Checked)
            {
                form_MActorList.Show(panel_DockPanel);
            }
            else
            {
                form_MActorList.Hide();
            }

        }

        private void 影片帧编辑区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            影片帧编辑区ToolStripMenuItem.Checked = !影片帧编辑区ToolStripMenuItem.Checked;
            if (影片帧编辑区ToolStripMenuItem.Checked)
            {
                form_MFrameEdit.Show(panel_DockPanel);
            }
            else
            {
                form_MFrameEdit.Hide();
            }
        }

        private void 影片时间轴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            影片时间轴ToolStripMenuItem.Checked = !影片时间轴ToolStripMenuItem.Checked;
            if (影片时间轴ToolStripMenuItem.Checked)
            {
                form_MTimeLine.Show(panel_DockPanel);
            }
            else
            {
                form_MTimeLine.Hide();
            }

        }

        private void 配置面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            配置面板ToolStripMenuItem.Checked = !配置面板ToolStripMenuItem.Checked;
            if (配置面板ToolStripMenuItem.Checked)
            {
                form_MConfig.Show(panel_DockPanel);
            }
            else
            {
                form_MConfig.Hide();
            }
        }

        private void 影片图库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            影片图库ToolStripMenuItem.Checked = !影片图库ToolStripMenuItem.Checked;
            if (影片图库ToolStripMenuItem.Checked)
            {
                form_MImgsList.Show(panel_DockPanel);
            }
            else
            {
                form_MImgsList.Hide();
            }
        }

        private void 元件库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            元件库ToolStripMenuItem.Checked = !元件库ToolStripMenuItem.Checked;
            if (元件库ToolStripMenuItem.Checked)
            {
                form_MCLib.Show(panel_DockPanel);
            }
            else
            {
                form_MCLib.Hide();
            }
        }
        private void 帧图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            帧图层ToolStripMenuItem.Checked = !帧图层ToolStripMenuItem.Checked;
            if (帧图层ToolStripMenuItem.Checked)
            {
                form_MFrameLevel.Show(panel_DockPanel);
            }
            else
            {
                form_MFrameLevel.Hide();
            }
        }


        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == (int)Msgs.WM_KEYDOWN)
        //    {
        //        Console.WriteLine("msg:" + m.Msg + "->" + m.WParam.ToInt32());
        //        char c=(char)m.WParam.ToInt32();
        //        MessageBox.Show("inputkey:" + c);
        //        m.Result = (IntPtr)1;
        //    }
        //    else
        //    {
        //        base.WndProc(ref m);
        //    }
        //}

        protected override bool ProcessCmdKey(ref Message msg, Keys e)
        {
            if (transCmdKey && (e == Keys.Up || e == Keys.Down || e == Keys.Left || e == Keys.Right)
                && (panel_DockPanel.ActiveContent != null))
            {
                NativeMethods.SendMessage(((Control)panel_DockPanel.ActiveContent).Handle, msg.Msg, (uint)msg.WParam, (uint)msg.LParam);
                msg.Result = (IntPtr)1;
                return true;
            }
            if (!(e == Keys.Z || e == Keys.Y))
            {
                base.ProcessCmdKey(ref msg, e);
            }
            return false;
        }


        private void 测试面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 复位所有面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetPanels();
        }
        //复位所有面板
        public static bool inResetPanels = false;
        private void resetPanels()
        {
            inResetPanels = true;
            for (int i = 0; i < dockContentes.Count; i++)
            {
                DockContent content = (DockContent)dockContentes[i];
                if (content.IsHidden)
                {
                    content.Show(panel_DockPanel);
                }
            }
            form_MActorList.DockTo(panel_DockPanel, DockStyle.Left);
            form_MFrameEdit.DockTo(panel_DockPanel, DockStyle.Fill);
            form_MFrameLevel.DockTo(panel_DockPanel, DockStyle.Right);

            form_MImgsList.DockTo(form_MActorList.Pane, DockStyle.Fill, 1);
            form_MAnimPW.DockTo(form_MActorList.Pane, DockStyle.Fill, 2);
            form_MCLib.DockTo(form_MFrameEdit.Pane, DockStyle.Top, 0);
            form_MTimeLine.DockTo(form_MCLib.Pane, DockStyle.Fill, 0);
            form_MConfig.DockTo(form_MFrameLevel.Pane, DockStyle.Bottom, 0);
            this.refreshDockState();
            inResetPanels = false;
            //主动重新加载贴图
            if (form_MFrameEdit.loaded || form_MAnimPW.loaded)
            {
                form_MImgsList.mImgsManager.rebindTextures();
                ConstTextureImgs.rebindTextures();
            }
            //主动记录Dock面板XML配置信息
            panel_DockPanel.SaveAsXml(layoutPath);
        }

        private void 动画演示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            动画演示ToolStripMenuItem.Checked = !动画演示ToolStripMenuItem.Checked;
            if (动画演示ToolStripMenuItem.Checked)
            {
                form_MAnimPW.Show(panel_DockPanel);
            }
            else
            {
                form_MAnimPW.Hide();
            }
        }
        //显示提示信息
        public void showInfor(String s)
        {
            form_MConfig.showInfor(s);
        }
        #region MIO 成员

        public void ReadObject(Stream s)
        {
            form_MImgsList.ReadObject(s);
            form_MActorList.ReadObject(s);
        }

        public void WriteObject(Stream s)
        {
            form_MImgsList.WriteObject(s);
            form_MActorList.WriteObject(s);
        }

        public void ExportObject(Stream s)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region MParentNode 成员

        public int GetSonID(MSonNode son)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public MParentNode GetTopParent()
        {
            return null;
        }

        #endregion

        private void Form_MAnimation_Shown(object sender, EventArgs e)
        {
            noCheckEvent = true;
            贴图插值渲染ToolStripMenuItem.Checked = Consts.textureLinear;
            noCheckEvent = false;
            form_MActorList.updateTreeView_Animation();
            if (form_MActorList.treeView_Animation.Nodes.Count > 0)
            {
                form_MActorList.treeView_Animation.SelectedNode = form_MActorList.treeView_Animation.Nodes[0];
            }
            form_MImgsList.updateAllList();
        }

        #region MSonNode 成员

        public MParentNode GetParent()
        {
            return null;
        }

        public void SetParent(MParentNode parent)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        //刷新跟Action相关的所有界面
        public void refreshActionUIs()
        {
            form_MTimeLine.updateTLNaviRegion();
            form_MTimeLine.updateTLFrameRegion();
            form_MTimeLine.updateTLRulerRegion();
            form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
        }
        //刷新跟Actor相关的所有界面
        public void refreshActorUIs()
        {
            form_MActorList.updateTreeView_Animation();
            refreshActionUIs();
        }

        private void 刷新所有贴图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form_MFrameEdit.loaded || form_MAnimPW.loaded)
            {
                form_MImgsList.mImgsManager.rebindTextures();
                ConstTextureImgs.rebindTextures();
                form_MFrameEdit.UpdateRegion_EditFrame();
            }
        }

        private void 贴图插值渲染ToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (noCheckEvent)
            {
                return;
            }
            Consts.textureLinear = 贴图插值渲染ToolStripMenuItem.Checked;
            if (form_MFrameEdit.loaded || form_MAnimPW.loaded)
            {
                form_MImgsList.mImgsManager.rebindTextures();
                ConstTextureImgs.rebindTextures();
                form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }

        #region MSonNode 成员


        public string getValueToLenString()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }


}