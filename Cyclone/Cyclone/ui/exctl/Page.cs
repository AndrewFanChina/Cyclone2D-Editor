using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Cyclone.alg;
using System.Drawing;
using System.Collections;
using Cyclone.alg.type;
using Cyclone.mod.menu;

namespace Cyclone.mod.exctl
{
    public class PageManager : TreeView, SerializeAble
    {
        public PageManager()
        {
        }
        //����µ�Ԫ
        public void addElement(String pageName)
        {
            Page newPage = new Page(this, pageName);
            Nodes.Add(newPage);
        }
        //ɾ����Ԫ
        public void removeElement(Page pageElement)
        {
            if (pageElement==null)
            {
                return;
            }
            if (Nodes.Contains(pageElement))
            {
                Nodes.Remove(pageElement);
            }
        }
        //��ȡ��Ԫ
        public Page getElement(int index)
        {
            if (index < 0 || index >= this.getElementCount())
            {
                return null;
            }
            return (Page)Nodes[index];

        }
        //���뵥Ԫ
        public void insertElement(Page element, int index)
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
        public void setElement(Page element, int index)
        {
            if (index < 0 || index >= this.getElementCount())
            {
                return;
            }
            Nodes[index] = element;
        }
        //���Ԫ��ID
        public int getElementID(Page element)
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
                int index = Convert.ToInt32(indexArray[i]);
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
            Page nodeTemp = (Page)Nodes[index - 1];
            Nodes[index - 1] = Nodes[index];
            Nodes[index] = nodeTemp;
        }
        //�����ƶ���Ԫ
        public void moveDownElement(int index)
        {
            if (index < 0 || index >= this.getElementCount() - 1)
            {
                return;
            }
            Page nodeTemp = (Page)Nodes[index + 1];
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
            Page nodeTemp = (Page)Nodes[0];
            Nodes[0] = Nodes[index];
            Nodes[index] = nodeTemp;
        }
        //�õ׵�Ԫ
        public void moveBottomElement(int index)
        {
            int lastID = getElementCount() - 1;
            if (index < 0 || index >= lastID)
            {
                return;
            }
            Page nodeTemp = (Page)Nodes[lastID];
            Nodes[lastID] = Nodes[index];
            Nodes[index] = nodeTemp;
        }
        #region SerializeAble ��Ա

        public void ReadObject(System.IO.Stream s)
        {
        }

        public void WriteObject(System.IO.Stream s)
        {
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
        }

        #endregion
    }
    public class Page : DynamicNode, SerializeAble
    {
        PageManager pageManager;
        public List<Module>modules = new List<Module>();
        public Page()
        {
            this.Text = "δ����";
        }
        public Page(PageManager pageManagerT, String TextT)
        {
            pageManager = pageManagerT;
            this.Text = TextT;
        }
        //����µ�Ԫ
        public void addElement(String pageName)
        {
            Page page = new Page(pageManager, pageName);
            Nodes.Add(page);
        }
        //ɾ����Ԫ
        public void removeElement(Page pageElement)
        {
            if (pageElement == null)
            {
                return;
            }
            if (Nodes.Contains(pageElement))
            {
                Nodes.Remove(pageElement);
            }
        }
        //��ʾҳ��
        public void display(Graphics g,int x,int y,int zoomLevel)
        {
            for (int i = 0; i < modules.Count; i++)
            {
                modules[i].display(g, x,y,zoomLevel,0);
            }
        }
        #region SerializeAble ��Ա

        public void ReadObject(System.IO.Stream s)
        {
        }

        public void WriteObject(System.IO.Stream s)
        {
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
        }

        #endregion
    }

}
