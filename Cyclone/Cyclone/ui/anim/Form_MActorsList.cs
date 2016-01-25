using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.mod;
using DockingUI.WinFormsUI.Docking;
using Cyclone.mod.anim;
using Cyclone.mod.util;

namespace Cyclone.mod.anim
{
    public partial class Form_MActorsList : DockContent, MParentNode, MSonNode,MIO
    {
        Form_MAnimation form_MA;
        public MActorsManager actorsManager;
        public MActorFolder currentActorFolder;
        public MActor currentActorElement;
        public MAction currentActionElement;
        public Form_MActorsList(Form_MAnimation form_MAT)
        {
            InitializeComponent();
            form_MA = form_MAT;
            actorsManager = new MActorsManager(this);
        }
        protected override string GetPersistString()
        {
            return "Form_MActorsList";
        }
        //角色文件夹列表部分==================================================================================================
        //(角色文件夹列表)加入新单元
        public void addActorFolderElement()
        {

            String name = "文件夹" + actorsManager.Count();
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("新建文件夹", name);
            txtDialog.ShowDialog();
            name = txtDialog.getValue();
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            MActorFolder element = new MActorFolder(actorsManager);
            element.name = name;
            actorsManager.Add(element);
            updateTreeView_Animation();
            setCurrentActorFolder(element.GetID(), true, 0);
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(角色文件夹列表)设置当前文件夹(foxID代表将要设置焦点的层级)
        public void setCurrentActorFolder(int index, bool updateUI, int foxID)
        {
            //if (index < 0 || index >= actorsManager.Size())
            //{
            //    return;
            //}
            currentActorFolder = actorsManager[index];
            if (currentActorFolder != null && foxID == 0)
            {
                Form_MAnimation.noNodeEvent = true;
                treeView_Animation.SelectedNode = treeView_Animation.Nodes[currentActorFolder.GetID()];
                Form_MAnimation.noNodeEvent = false;
            }
            if (updateUI)
            {
                int id = -1;
                if (currentActorFolder!=null && currentActorFolder.Count() > 0)
                {
                    id = 0;
                }
                setCurrentActor(id, true, foxID);
            }
        }
        //(角色文件夹列表)向上移动单元
        public void moveUpActorFolder()
        {
            if (currentActorFolder == null)
            {
                return;
            }
            int index = currentActorFolder.GetID();
            if (index <= 0)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            if (!actorsManager.MoveUpElement(index))
            {
                return;
            }
            updateTreeView_Animation();
            index--;
            setCurrentActorFolder(index, true, 0);
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(角色文件夹列表)向下移动单元
        public void moveDownActorFolder()
        {
            if (currentActorFolder == null)
            {
                return;
            }
            int index = currentActorFolder.GetID();
            if (index >= actorsManager.Count() - 1)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            if (!actorsManager.MoveDownElement(index))
            {
                return;
            }
            updateTreeView_Animation();
            index++;
            setCurrentActorFolder(index, true, 0);
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(角色文件夹列表)删除单元
        public void deleteActorFolder()
        {
            if (currentActorFolder == null)
            {
                return;
            }
            if (!MessageBox.Show("确定删除角色文件夹 [" + currentActorFolder.name + "]？", "删除角色文件夹", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.Yes))
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            //先删除文件夹中的内容
            while (currentActorFolder.Count() > 0)
            {
                currentActorFolder.RemoveAt(0);
            }
            int index = currentActorFolder.GetID();
            if (actorsManager.RemoveAt(index))
            {
                updateTreeView_Animation();
                int newIndex = index - 1;
                if (newIndex < 0 && actorsManager.Count() > 0)
                {
                    newIndex = 0;
                }
                setCurrentActorFolder(newIndex, true, 0);
            }
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(文件夹列表)重命名单元
        public void renameActorFolder()
        {
            if (currentActorFolder == null)
            {
                return;
            }
            String name = currentActorFolder.name;
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("重命名", name);
            txtDialog.ShowDialog();
            String nameNew = txtDialog.getValue();
            if (!nameNew.Equals(name))
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Actor);
                currentActorFolder.name = nameNew;
                treeView_Animation.Nodes[currentActorFolder.GetID()].Text = nameNew;
                form_MA.historyManager.AddHistory(HistoryType.Actor);
            }
        }
        //角色列表部分==================================================================================================
        //(角色列表)加入新单元
        public void addActorElement()
        {
            if (currentActorFolder == null)
            {
                return;
            }
            String name = "角色" + currentActorFolder.Count();
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("新建角色", name);
            txtDialog.ShowDialog();
            name = txtDialog.getValue();
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            MActor element = new MActor(currentActorFolder);
            element.name=name;
            currentActorFolder.Add(element);
            updateTreeNode_ActorFolder();
            setCurrentActor(currentActorFolder.GetSonID(element), true, 1);
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(角色列表)重命名单元
        public void renameActorElement()
        {
            if (currentActorElement == null || currentActorFolder == null)
            {
                return;
            }
            MActor actor = currentActorElement;
            String name = actor.name;
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("重命名", name);
            txtDialog.ShowDialog();
            String nameNew = txtDialog.getValue();
            if (!nameNew.Equals(name))
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Actor);
                actor.name =nameNew;
                treeView_Animation.Nodes[currentActorFolder.GetID()].Nodes[currentActorElement.GetID()].Text = nameNew;
                form_MA.historyManager.AddHistory(HistoryType.Actor);
            }
        }
        //(角色列表)复制单元
        public void cloneActorElement()
        {
            if (currentActorFolder == null || currentActorElement == null)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            MActor actor = (MActor)currentActorElement.Clone(currentActorFolder);
            String name = actor.name;
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("复制单元", name);
            txtDialog.ShowDialog();
            name = txtDialog.getValue();
            actor.name = name;
            currentActorFolder.Add(actor);
            updateTreeNode_ActorFolder();
            setCurrentActor(currentActorFolder.GetSonID(actor), true, 1);
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(角色列表)向上移动单元
        public void moveUpActorElement()
        {
            if (currentActorElement == null || currentActorFolder==null)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            MActor actor = currentActorElement;
            MActorFolder actorFolder = currentActorFolder;
            int index = actor.GetID();
            if (!actorFolder.MoveUpElement(index))
            {
                return;
            }
            updateTreeNode_ActorFolder();
            index--;
            if (index < 0)
            {
                index = 0;
            }
            setCurrentActor(index, true, 1);
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(角色列表)向下移动单元
        public void moveDownActorElement()
        {
            if (currentActorElement == null || currentActorFolder==null)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            MActor actor = currentActorElement;
            MActorFolder actorFolder = currentActorFolder;
            int index = actorFolder.GetSonID(actor);
            if (!actorFolder.MoveDownElement(index))
            {
                return;
            }
            updateTreeNode_ActorFolder();
            index++;
            if (index > actorFolder.Count() - 1)
            {
                index = actorFolder.Count() - 1;
            }
            setCurrentActor(index, true, 1);
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(角色列表)删除单元
        public void deleteActorElement()
        {
            if (currentActorElement == null || currentActorFolder == null || currentActorFolder.GetSonID(currentActorElement) < 0)
            {
                return;
            }
            if (!MessageBox.Show("确定删除角色 [" + currentActorElement.name + "]？", "删除角色", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.Yes))
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            int index = currentActorFolder.GetSonID(currentActorElement);
            if (currentActorFolder.RemoveAt(index))
            {
                updateTreeNode_ActorFolder();
                int newIndex = index - 1;
                if (newIndex < 0 && currentActorFolder.Count() > 0)
                {
                    newIndex = 0;
                }
                setCurrentActor(newIndex, true, 1);
                form_MA.historyManager.AddHistory(HistoryType.Actor);
            }

        }

        //(角色列表)设置当前焦点角色单元
        public void setCurrentActor(int index, bool updateUI, int foxID)
        {
            if (currentActorFolder != null)
            {
                currentActorElement = currentActorFolder[index];
            }
            else
            {
                currentActorElement = null;
            }
            int newIndex = -1;
            if (currentActorElement != null && currentActorElement.GetID() >= 0)
            {
                newIndex = currentActorElement.GetID();
                if (foxID == 1)
                {
                    Form_MAnimation.noNodeEvent = true;
                    treeView_Animation.SelectedNode = treeView_Animation.Nodes[currentActorFolder.GetID()].Nodes[newIndex];
                    Form_MAnimation.noNodeEvent = false;
                }
            }
            else
            {
                currentActorElement = null;
                currentActionElement = null;
            }
            if (updateUI)
            {
                this.setCurrentAction(0, true, foxID);
            }
        }
        //在treeView中初始化当前结构
        public void updateTreeView_Animation()
        {
            treeView_Animation.Nodes.Clear();
            for (int i = 0; i < actorsManager.Count(); i++)
            {
                TreeNode actorFolderNode = new TreeNode();
                MActorFolder actorFolder = actorsManager[i];
                currentActorFolder = actorFolder;
                actorFolderNode.Text = actorFolder.name;
                actorFolderNode.ImageIndex = 0;
                actorFolderNode.SelectedImageIndex = 0;
                for (int j = 0; j < actorFolder.Count(); j++)
                {
                    TreeNode actorNode = new TreeNode();
                    MActor actor = actorFolder[j];
                    currentActorElement = actor;
                    actorNode.Text = actor.name;
                    actorNode.ImageIndex = 1;
                    actorNode.SelectedImageIndex = 1;
                    for (int k = 0; k < actor.Count(); k++)
                    {
                        TreeNode actionNode = new TreeNode();
                        MAction action = actor[k];
                        currentActionElement = action;
                        actionNode.Text = action.name;
                        actionNode.ImageIndex = 2;
                        actionNode.SelectedImageIndex = 2;
                        actorNode.Nodes.Add(actionNode);
                    }
                    actorFolderNode.Nodes.Add(actorNode);
                }
                treeView_Animation.Nodes.Add(actorFolderNode);
            }
            treeView_Animation.ExpandAll();

        }
        //更新MActorFolder级别的节点
        public void updateTreeNode_ActorFolder()
        {
            if (currentActorFolder == null)
            {
                return;
            }
            int folderID = currentActorFolder.GetID();
            TreeNode actorFolderNode = treeView_Animation.Nodes[folderID];
            actorFolderNode.Nodes.Clear();
            for (int i = 0; i < currentActorFolder.Count(); i++)
            {
                TreeNode actorNode = new TreeNode();
                MActor actor = currentActorFolder[i];
                actorNode.Text = actor.name;
                actorNode.ImageIndex = 1;
                actorNode.SelectedImageIndex = 1;
                for (int k = 0; k < actor.Count(); k++)
                {
                    TreeNode actionNode = new TreeNode();
                    MAction action = actor[k];
                    actionNode.Text = action.name;
                    actionNode.ImageIndex = 2;
                    actionNode.SelectedImageIndex = 2;
                    actorNode.Nodes.Add(actionNode);
                }
                actorFolderNode.Nodes.Add(actorNode);
            }
        }
        //更新Actor级别的节点
        public void updateTreeNode_Actor()
        {
            if (currentActorFolder == null || currentActorElement == null)
            {
                return;
            }
            TreeNode actorNode = treeView_Animation.Nodes[currentActorFolder.GetID()].Nodes[currentActorFolder.GetSonID(currentActorElement)];
            MActor actor = currentActorElement;
            actorNode.Nodes.Clear();
            for (int k = 0; k < actor.Count(); k++)
            {
                TreeNode actionNode = new TreeNode();
                MAction action = actor[k];
                actionNode.Text = action.name;
                actionNode.ImageIndex = 2;
                actionNode.SelectedImageIndex = 2;
                actorNode.Nodes.Add(actionNode);
            }
        }
        //动作列表部分=====================================================================================================
        //(动作列表)加入新单元
        public void addActionElement()
        {
            if (currentActorFolder == null || currentActorElement == null)
            {
                return;
            }
            String name = "动作" + currentActorElement.Count();
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("新建动作", name);
            txtDialog.ShowDialog();
            name = txtDialog.getValue();
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            MAction element = new MAction(currentActorElement);
            element.name = name;
            currentActorElement.Add(element);
            currentActionElement = element;
            updateTreeNode_Actor();
            TreeNode actionTreeNode = treeView_Animation.Nodes[currentActorFolder.GetID()].Nodes[currentActorElement.GetID()].Nodes[currentActionElement.GetID()];
            treeView_Animation.SelectedNode = actionTreeNode;
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(动作列表)重命名单元
        public void renameActionElement()
        {
            if (currentActorFolder == null || currentActorElement == null || currentActionElement == null)
            {
                return;
            }
            MAction action = currentActionElement;
            String name = action.name;
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("重命名", name);
            txtDialog.ShowDialog();
            String newName = txtDialog.getValue();
            if (!name.Equals(newName))
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Actor);
                action.name=newName;
                treeView_Animation.Nodes[currentActorFolder.GetID()].Nodes[currentActorFolder.GetSonID(currentActorElement)].Nodes[currentActionElement.GetID()].Text = newName;
                form_MA.historyManager.AddHistory(HistoryType.Actor);
            }

        }
        //(动作列表)复制单元
        public void cloneActionElement()
        {
            if (currentActorFolder == null || currentActorElement == null || currentActionElement == null)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            MAction action = (MAction)currentActionElement.Clone();
            String name = action.name;
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("复制单元", name);
            txtDialog.ShowDialog();
            name = txtDialog.getValue();
            action.name = name;
            currentActorElement.Add(action);
            updateTreeNode_Actor();
            setCurrentAction(currentActorElement.Count() - 1);
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(动作列表)向上移动单元
        public void moveUpActionElement()
        {
            if (currentActorFolder == null || currentActorElement == null || currentActionElement == null)
            {
                return;
            }
            //改变数据
            int index = currentActionElement.GetID();
            if (index <= 0)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            if (!currentActorElement.MoveUpElement(index))
            {
                return;
            }
            updateTreeNode_Actor();
            setCurrentAction(index - 1, true, 2);
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(动作列表)向下移动单元
        public void moveDownActionElement()
        {
            if (currentActorFolder == null || currentActorElement == null || currentActionElement == null)
            {
                return;
            }
            //改变数据
            int index = currentActionElement.GetID();
            if (index >= currentActorElement.Count() - 1)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            if (!currentActorElement.MoveDownElement(index))
            {
                return;
            }
            updateTreeNode_Actor();
            setCurrentAction(index + 1, true, 2);
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }
        //(动作列表)删除动作
        public void deleteActionElement()
        {
            if (currentActorFolder == null || currentActorElement == null || currentActionElement == null)
            {
                return;
            }
            if (!MessageBox.Show("确定删除动作 [" + currentActionElement.name + "]？", "删除动作", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.Yes))
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Actor);
            int index = currentActionElement.GetID();
            currentActorElement.RemoveAt(index);
            //改变UI
            TreeNode parentTreeNode = treeView_Animation.Nodes[currentActorFolder.GetID()].Nodes[currentActorFolder.GetSonID(currentActorElement)];
            parentTreeNode.Nodes.RemoveAt(index);
            int newIndex = index - 1;
            if (newIndex < 0 && currentActorElement.Count() > 0)
            {
                newIndex = 0;
            }
            setCurrentAction(newIndex);
            form_MA.historyManager.AddHistory(HistoryType.Actor);
        }

        //(动作列表)设置当前焦点动作单元
        public void setCurrentAction(int index)
        {
            setCurrentAction(index, true, 2);
        }
        public void setCurrentAction(int index, bool updateUI, int foxID)
        {
            if (currentActorElement != null)
            {
                currentActionElement = currentActorElement[index];
            }
            else
            {
                currentActionElement = null;
            }
            if (currentActionElement != null)
            {
                if (foxID == 2)
                {
                    Form_MAnimation.noNodeEvent = true;
                    int folderID = currentActorFolder.GetID();
                    int actorID = currentActorElement.GetID();
                    treeView_Animation.SelectedNode = treeView_Animation.Nodes[folderID].Nodes[actorID].Nodes[index];
                    Form_MAnimation.noNodeEvent = false;
                }
            }
            form_MA.form_MTimeLine.setHolder(currentActionElement);
            if (updateUI)
            {
                form_MA.form_MTimeLine.updateTLNaviRegion();
                form_MA.form_MTimeLine.updateTLFrameRegion();
                form_MA.form_MTimeLine.updateTLRulerRegion();
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }
        private void treeView_Animation_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Form_MAnimation.noNodeEvent)
            {
                return;
            }
            TreeNode nodeCurrent = treeView_Animation.SelectedNode;
            //设置焦点
            if (nodeCurrent.Level == 0)
            {
                setCurrentActorFolder(nodeCurrent.Index, true, 0);
            }
            else if (nodeCurrent.Level == 1)
            {
                if (currentActorFolder != null && nodeCurrent.Parent.Index != currentActorFolder.GetID())
                {
                    setCurrentActorFolder(nodeCurrent.Parent.Index, true, 1);
                }
                setCurrentActor(nodeCurrent.Index, true, 1);
                if (currentActorElement != null)
                {
                    form_MA.showInfor("Sprite：" + currentActorElement.name + "，ID：" + currentActorElement.GetID());
                }
            }
            else if (nodeCurrent.Level == 2)
            {
                if (currentActorFolder == null || currentActorFolder.GetID() != nodeCurrent.Parent.Parent.Index)
                {
                    setCurrentActorFolder(nodeCurrent.Parent.Parent.Index, true, 2);
                }
                if (currentActorElement == null || currentActorFolder.GetSonID(currentActorElement) != nodeCurrent.Parent.Index)
                {
                    setCurrentActor(nodeCurrent.Parent.Index, true, 2);
                }
                setCurrentAction(nodeCurrent.Index, true, 2);
                if (currentActionElement != null)
                {
                    form_MA.showInfor("Action：" + currentActionElement.name + "，ID：" + currentActionElement.GetID());
                }
            }
        }
        private void treeView_Animation_DragDrop(object sender, DragEventArgs e)
        {
            //得到拖放数据，并转换为TreeNode型
            TreeNode theNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            TreeView theTree = (TreeView)sender;
            //得到鼠标进入TreeNode，而拖放目标targetNode
            TreeNode targetNode = theTree.GetNodeAt(treeView_Animation.PointToClient(new Point(e.X, e.Y)));
            if (targetNode != null && !targetNode.Equals(theNode.Parent) && 
                ((theNode.Level == 1 && targetNode.Level == 0) || (theNode.Level == 2 && targetNode.Level == 1)))
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Actor);
                //先调整数据
                if (theNode.Level == 1)
                {
                    MActorFolder folderSrc = (MActorFolder)actorsManager[theNode.Parent.Index];
                    MActor actor = folderSrc[theNode.Index];
                    MActorFolder folderDest = (MActorFolder)actorsManager[targetNode.Index];
                    folderSrc.RemoveAt(theNode.Index);
                    folderDest.Add(actor);
                }
                else if (theNode.Level == 2)
                {
                    MActorFolder folderSrc = (MActorFolder)actorsManager[theNode.Parent.Parent.Index];
                    MActor actorSrc = folderSrc[theNode.Parent.Index];
                    MAction actionSrc = actorSrc[theNode.Index];
                    MActorFolder folderDest = (MActorFolder)actorsManager[targetNode.Parent.Index];
                    MActor actorDest = folderDest[targetNode.Index];
                    actorSrc.RemoveAt(theNode.Index);
                    actorDest.Add(actionSrc);
                }
                //调整UI
                TreeNode targetParent = targetNode.Parent;
                //删除拖放的TreeNode
                theNode.Remove();
                //添加到目标TreeView下
                targetNode.Nodes.Add(theNode);
                theTree.SelectedNode = targetNode;

                form_MA.historyManager.AddHistory(HistoryType.Actor);
            }

        }

        private void treeView_Animation_DragEnter(object sender, DragEventArgs e)
        {
            //获取TreeNode类型的数据内容．
            object data = e.Data.GetData(typeof(TreeNode));
            //如果有数据拖放时不允许显示拖放形态
            if (data != null)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void treeView_Animation_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode currentNode = (TreeNode)e.Item;
            if (currentNode != null && (currentNode.Level == 1||currentNode.Level == 2))
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void treeView_Animation_KeyDown(object sender, KeyEventArgs e)
        {
            TreeNode nodeCurrent = treeView_Animation.SelectedNode;
            if (nodeCurrent == null)
            {
                return;
            }
            if (e.Control)
            {
                if (e.KeyCode.Equals(Keys.Up))
                {
                    if (nodeCurrent.Level == 0)
                    {
                        moveUpActorFolder();
                    }
                    else if (nodeCurrent.Level == 1)
                    {
                        moveUpActorElement();
                    }
                    else if (nodeCurrent.Level == 2)
                    {
                        moveUpActionElement();
                    }
                }
                else if (e.KeyCode.Equals(Keys.Down))
                {
                    if (nodeCurrent.Level == 0)
                    {
                        moveDownActorFolder();
                    }
                    else if (nodeCurrent.Level == 1)
                    {
                        moveDownActorElement();
                    }
                    else if (nodeCurrent.Level == 2)
                    {
                        moveDownActionElement();
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.Delete))
            {
                if (nodeCurrent.Level == 0)
                {
                    deleteActorFolder();
                }
                else if (nodeCurrent.Level == 1)
                {
                    deleteActorElement();
                }
                else if (nodeCurrent.Level == 2)
                {
                    deleteActionElement();
                }
            }
        }

        private void treeView_Animation_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode nodeCurrent = treeView_Animation.SelectedNode;
            if (e.Button == MouseButtons.Right)
            {
                if (nodeCurrent != null && nodeCurrent.Level != 2)
                {
                    添加子单元ToolStripMenuItem.Enabled = true;
                }
                else
                {
                    添加子单元ToolStripMenuItem.Enabled = false;
                }
                if (nodeCurrent != null && nodeCurrent.Level != 0)
                {
                    克隆单元ToolStripMenuItem.Enabled = true;
                }
                else
                {
                    克隆单元ToolStripMenuItem.Enabled = false;
                }
                if (nodeCurrent != null)
                {
                    if (nodeCurrent.Level == 1)
                    {
                        添加子单元ToolStripMenuItem.Enabled = true;
                    }
                    contextMenuStrip_ItemFocused.Show((Control)sender, e.X, e.Y);
                }
                else
                {
                    contextMenuStrip_NoItemFocused.Show((Control)sender, e.X, e.Y);
                }
            }
        }

        private void treeView_Animation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Form_MAnimation.noNodeEvent)
            {
                return;
            }
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
        }

        //########################################### 事件处理 #################################################################
        //(事件响应)角色和动作列表框------------------------------------------------------------------------
        //新建角色或者动作或者基础帧文件夹
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView_Animation.Focused)
            {
                TreeNode nodeCurrent = treeView_Animation.SelectedNode;
                if (nodeCurrent != null)
                {
                    if (nodeCurrent.Level == 0)
                    {
                        addActorFolderElement();
                    }
                    else if (nodeCurrent.Level == 1)
                    {
                        addActorElement();
                    }
                    else if (nodeCurrent.Level == 2)
                    {
                        addActionElement();
                    }
                }
                else
                {
                    addActorFolderElement();
                }
            }
        }

        //重命名角色或者动作
        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView_Animation.Focused)
            {
                TreeNode treeNode = treeView_Animation.SelectedNode;
                if (treeNode == null)
                {
                    return;
                }
                if (treeNode.Level == 0)
                {
                    renameActorFolder();
                }
                else if (treeNode.Level == 1)
                {
                    renameActorElement();
                }
                else if (treeNode.Level == 2)
                {
                    renameActionElement();
                }
            }
        }
        //复制角色或者动作
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView_Animation.Focused)
            {
                TreeNode treeNode = treeView_Animation.SelectedNode;
                if (treeNode == null)
                {
                    return;
                }
                if (treeNode.Level == 1)
                {
                    cloneActorElement();
                }
                else if (treeNode.Level == 2)
                {
                    cloneActionElement();
                }
            }
        }
        //角色上移或者动作
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView_Animation.Focused)
            {
                TreeNode treeNode = treeView_Animation.SelectedNode;
                if (treeNode == null)
                {
                    return;
                }

                if (treeNode.Level == 0)
                {
                    moveUpActorFolder();
                }
                else if (treeNode.Level == 1)
                {
                    moveUpActorElement();
                }
                else if (treeNode.Level == 2)
                {
                    moveUpActionElement();
                }
            }
        }
        //角色或者动作下移
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView_Animation.Focused)
            {
                TreeNode treeNode = treeView_Animation.SelectedNode;
                if (treeNode == null)
                {
                    return;
                }
                if (treeNode.Level == 0)
                {
                    moveDownActorFolder();
                }
                else if (treeNode.Level == 1)
                {
                    moveDownActorElement();
                }
                else if (treeNode.Level == 2)
                {
                    moveDownActionElement();
                }
            }
        }
        //删除角色或者动作
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (treeView_Animation.Focused)
            {
                TreeNode treeNode = treeView_Animation.SelectedNode;
                if (treeNode == null)
                {
                    return;
                }
                if (treeNode.Level == 0)
                {
                    deleteActorFolder();
                }
                else if (treeNode.Level == 1)
                {
                    deleteActorElement();
                }
                else if (treeNode.Level == 2)
                {
                    deleteActionElement();
                }
            }
        }

        private void 添加子单元ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView_Animation.Focused)
            {
                TreeNode nodeCurrent = treeView_Animation.SelectedNode;
                if (nodeCurrent != null)
                {
                    if (nodeCurrent.Level == 0)
                    {
                        addActorElement();
                    }
                    else if (nodeCurrent.Level == 1)
                    {
                        addActionElement();
                    }
                }
            }
        }



        #region MIO 成员

        public void ReadObject(System.IO.Stream s)
        {
            actorsManager.ReadObject(s);
        }

        public void WriteObject(System.IO.Stream s)
        {
            actorsManager.WriteObject(s);
        }

        public void ExportObject(System.IO.Stream s)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void setParent(MIO parentT)
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
            return form_MA;
        }

        #endregion

        #region MSonNode 成员

        public MParentNode GetParent()
        {
            return form_MA;
        }

        public void SetParent(MParentNode parent)
        {
            form_MA = (Form_MAnimation)parent;
        }

        #endregion

        #region MSonNode 成员


        public string getValueToLenString()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}