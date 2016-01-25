using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Cyclone.mod.menu
{
    public class DynamicNode:TreeNode
    {
        public DynamicNode()
        {
            this.Text = "未命名";
        }
        public DynamicNode(String TextT)
        {
            this.Text = TextT;
        }
        //添加新单元
        public void addElement(DynamicNode element)
        {
            if (!Nodes.Contains(element))
            {
                Nodes.Add(element);
            }
        }
        //获取单元
        public DynamicNode getElement(int index)
        {
            if (index < 0 || index >= this.getElementCount())
            {
                return null;
            }
            return (DynamicNode)Nodes[index];

        }
        //插入单元
        public void insertElement(DynamicNode element, int index)
        {
            if (index < 0 || index >= this.getElementCount())
            {
                return;
            }
            if (!Nodes.Contains(element))
            {
                Nodes.Insert(index, element);
            }
        }
        //设置单元
        public void setElement(DynamicNode element, int index)
        {
            if (index < 0 || index >= this.getElementCount())
            {
                return;
            }
            Nodes[index] = element;
        }
        //获得元素ID
        public int getElementID(DynamicNode element)
        {
            return Nodes.IndexOf(element);
        }
        //获得元素个数
        public int getElementCount()
        {
            return Nodes.Count;
        }
        //删除单元
        public void removeElement(int index)
        {
            if (index < 0 || index >= this.getElementCount())
            {
                return;
            }
            Nodes.RemoveAt(index);
        }
        //删除一系列单元
        public void removeElements(ArrayList indexArray)
        {
            indexArray.Sort();//须保证从大到小进行数据排列
            for (int i = 0; i < indexArray.Count; i++)
            {
                int index = Convert.ToInt32( indexArray[i]);
                Nodes.RemoveAt(index);
            }

        }
        //删除所有单元
        public void removeAll()
        {
            Nodes.Clear();
        }
        //向上移动单元
        public void moveUpElement(int index)
        {
            if (index <= 0 || index >= this.getElementCount())
            {
                return;
            }
            DynamicNode nodeTemp = (DynamicNode)Nodes[index - 1];
            Nodes[index - 1] = Nodes[index];
            Nodes[index] = nodeTemp;
        }
        //向下移动单元
        public void moveDownElement(int index)
        {
            if (index < 0 || index >= this.getElementCount()-1)
            {
                return;
            }
            DynamicNode nodeTemp = (DynamicNode)Nodes[index + 1];
            Nodes[index + 1] = Nodes[index];
            Nodes[index] = nodeTemp;
        }
        //置顶单元
        public void moveTopElement(int index)
        {
            if (index <= 0 || index >= this.getElementCount())
            {
                return;
            }
            DynamicNode nodeTemp = (DynamicNode)Nodes[0];
            Nodes[0] = Nodes[index];
            Nodes[index] = nodeTemp;
        }
        //置底单元
        public void moveBottomElement(int index)
        {
            int lastID=getElementCount() - 1;
            if (index < 0 || index >= lastID)
            {
                return;
            }
            DynamicNode nodeTemp = (DynamicNode)Nodes[lastID];
            Nodes[lastID] = Nodes[index];
            Nodes[index] = nodeTemp;
        }
        

    }
}
