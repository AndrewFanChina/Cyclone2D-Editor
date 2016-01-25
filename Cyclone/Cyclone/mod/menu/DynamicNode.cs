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
            this.Text = "δ����";
        }
        public DynamicNode(String TextT)
        {
            this.Text = TextT;
        }
        //����µ�Ԫ
        public void addElement(DynamicNode element)
        {
            if (!Nodes.Contains(element))
            {
                Nodes.Add(element);
            }
        }
        //��ȡ��Ԫ
        public DynamicNode getElement(int index)
        {
            if (index < 0 || index >= this.getElementCount())
            {
                return null;
            }
            return (DynamicNode)Nodes[index];

        }
        //���뵥Ԫ
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
        //���õ�Ԫ
        public void setElement(DynamicNode element, int index)
        {
            if (index < 0 || index >= this.getElementCount())
            {
                return;
            }
            Nodes[index] = element;
        }
        //���Ԫ��ID
        public int getElementID(DynamicNode element)
        {
            return Nodes.IndexOf(element);
        }
        //���Ԫ�ظ���
        public int getElementCount()
        {
            return Nodes.Count;
        }
        //ɾ����Ԫ
        public void removeElement(int index)
        {
            if (index < 0 || index >= this.getElementCount())
            {
                return;
            }
            Nodes.RemoveAt(index);
        }
        //ɾ��һϵ�е�Ԫ
        public void removeElements(ArrayList indexArray)
        {
            indexArray.Sort();//�뱣֤�Ӵ�С������������
            for (int i = 0; i < indexArray.Count; i++)
            {
                int index = Convert.ToInt32( indexArray[i]);
                Nodes.RemoveAt(index);
            }

        }
        //ɾ�����е�Ԫ
        public void removeAll()
        {
            Nodes.Clear();
        }
        //�����ƶ���Ԫ
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
        //�����ƶ���Ԫ
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
        //�ö���Ԫ
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
        //�õ׵�Ԫ
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
