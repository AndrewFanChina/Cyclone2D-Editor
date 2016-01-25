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
        //添加新单元
        public void addElement(String pageName)
        {
            Page newPage = new Page(this, pageName);
            Nodes.Add(newPage);
        }
        //删除单元
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
        //获取单元
        public Page getElement(int index)
        {
            if (index < 0 || index >= this.getElementCount())
            {
                return null;
            }
            return (Page)Nodes[index];

        }
        //插入单元
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
        //设置单元
        public void setElement(Page element, int index)
        {
            if (index < 0 || index >= this.getElementCount())
            {
                return;
            }
            Nodes[index] = element;
        }
        //获得元素ID
        public int getElementID(Page element)
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
                int index = Convert.ToInt32(indexArray[i]);
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
            Page nodeTemp = (Page)Nodes[index - 1];
            Nodes[index - 1] = Nodes[index];
            Nodes[index] = nodeTemp;
        }
        //向下移动单元
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
        //置顶单元
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
        //置底单元
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
        #region SerializeAble 成员

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
            this.Text = "未命名";
        }
        public Page(PageManager pageManagerT, String TextT)
        {
            pageManager = pageManagerT;
            this.Text = TextT;
        }
        //添加新单元
        public void addElement(String pageName)
        {
            Page page = new Page(pageManager, pageName);
            Nodes.Add(page);
        }
        //删除单元
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
        //显示页面
        public void display(Graphics g,int x,int y,int zoomLevel)
        {
            for (int i = 0; i < modules.Count; i++)
            {
                modules[i].display(g, x,y,zoomLevel,0);
            }
        }
        #region SerializeAble 成员

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
