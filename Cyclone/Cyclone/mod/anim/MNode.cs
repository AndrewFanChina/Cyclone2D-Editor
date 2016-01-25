using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.alg;
using System.Collections;
using Cyclone.alg.util;

namespace Cyclone.mod.anim
{
    public interface MIO
    {
        void ReadObject(System.IO.Stream s);
        void WriteObject(System.IO.Stream s);
        void ExportObject(System.IO.Stream s);
    }
    public interface MParentNode
    {
        int GetSonID(MSonNode son);
        MParentNode GetTopParent();
    }
    public interface MSonNode
    {
        MParentNode GetParent();
        void SetParent(MParentNode parent);
        string getValueToLenString();
    }
    public interface MNodeUI<Item>
    {
        void AddItem(Item item);
        void SetItem(int index,Item item);
        void UpdateItem(int index);
        void SetSelectedItem(int index);
        void InsertItem(int index, Item item);
        void RemoveItemAt(int index);
        void ClearItems();
    }
    public class MNode<Son> : MIO, MParentNode, MSonNode where Son : class, MParentNode, MSonNode, MIO,new() 

    {
        protected MParentNode parent;
        protected List<Son> m_sonList = new List<Son>();
        public String name="";
        public bool allowUpdateUI = true;
        protected MNodeUI<Son> ui;
        public MNode()
        {
        }
        public MNode(MParentNode parenT)
        {
            parent = parenT;
        }
        public MNodeUI<Son> MNodeUI
        {
            get
            {
                return ui;
            }
            set
            {
                ui = value;
            }
        }
        public int Count()
        {
            return m_sonList.Count;
        }
        public int GetID()
        {
            return parent.GetSonID(this);
        }
        public Son this[int index]
        {
            get
            { //检查索引范围
                if (index < 0 ||index>= m_sonList.Count)
                {
                    return null;
                }
                else
                {
                    return m_sonList[index];
                }
            }
            set
            {
                if (!(index < 0 || index >= m_sonList.Count))
                {
                    m_sonList[index] = value;
                    if (allowUpdateUI && ui != null)
                    {
                        ui.SetSelectedItem(index);
                    }
                }
            }

        }
        public void setAllSon(List<Son> sonList)
        {
            if (sonList!=null)
            {
                m_sonList = sonList;
            }
        }
        //默认操作会自动转移父类索引
        public bool Add(Son element)
        {
            return Add(element, true);
        }
        public bool Add(Son element,bool resetParent)
        {
            if (element == null || m_sonList.Contains(element))
            {
                return false;
            }
            else
            {
                m_sonList.Add(element);
                if (resetParent)
                {
                    element.SetParent(this);
                }
                if (allowUpdateUI && ui != null)
                {
                    ui.AddItem(element);
                    ui.SetSelectedItem(m_sonList.Count - 1);
                }

            }
            return true;
        }

        public bool Insert(Son element, int index)
        {
            if (element == null || m_sonList.Contains(element))
            {
                return false;
            }
            else if (index < 0 || index >= m_sonList.Count)
            {
                Add(element);
            }
            else
            {
                m_sonList.Insert(index, element);
                element.SetParent(this);
                if (allowUpdateUI && ui != null)
                {
                    ui.InsertItem(index, element);
                    ui.SetSelectedItem(index);
                }
            }
            return true;
        }
        public bool Remove(Son son)
        {
            if (son != null && m_sonList.Contains(son))
            {
                int index = m_sonList.IndexOf(son);
                m_sonList.RemoveAt(index);
                if (allowUpdateUI && ui != null)
                {
                    ui.RemoveItemAt(index);
                    if (index >= m_sonList.Count)
                    {
                        index--;
                    }
                    ui.SetSelectedItem(index);
                }
                return true;
            }
            return false;
        }
        public bool RemoveAt(int indexRemove)
        {
            int index = indexRemove;
            if (index < 0 || index >= m_sonList.Count)
            {
                Console.WriteLine("error index " + index + " in removeElement");
                return false;
            }
            m_sonList.RemoveAt(index);
            if (allowUpdateUI && ui != null)
            {
                ui.RemoveItemAt(index);
                if (index >= m_sonList.Count)
                {
                    index--;
                }
                ui.SetSelectedItem(index);
            }
            return true;
        }
        public bool RemoveElements(ArrayList indexArray)
        {
            if (indexArray == null)
            {
                return false;
            }
            int gap = 0;
            for (int i = 0; i < indexArray.Count; i++)
            {
                int index = (int)indexArray[i] - gap;
                if (index < 0 || index >= m_sonList.Count)
                {
                    Console.WriteLine("error index " + index + " in removeElement");
                    continue;
                }
                m_sonList.RemoveAt(index);
                gap++;
                if (allowUpdateUI && ui != null)
                {
                    ui.RemoveItemAt(index);
                }
            }
            return true;
        }
        public void Clear()
        {
            m_sonList.Clear();
            if (allowUpdateUI && ui != null)
            {
                ui.ClearItems();
            }
        }
        public bool MoveUpElement(int index)
        {
            if (index <= 0 || index >= m_sonList.Count)
            {
                return false;
            }
            Son currentObj = m_sonList[index];
            m_sonList.RemoveAt(index);
            m_sonList.Insert(index - 1, currentObj);
            if (!allowUpdateUI)
            {
                return true;
            }
            if (allowUpdateUI && ui != null)
            {
                ui.SetItem(index - 1,m_sonList[index - 1]);
                ui.SetItem(index,m_sonList[index]);
                ui.SetSelectedItem(index - 1);
            }
            return true;
        }
        public bool MoveDownElement(int index)
        {
            if (index < 0 || index >= m_sonList.Count - 1)
            {
                return false;
            }
            Son currentObj = m_sonList[index];
            m_sonList.RemoveAt(index);
            m_sonList.Insert(index + 1, currentObj);
            if (!allowUpdateUI)
            {
                return true;
            }
            if (allowUpdateUI && ui != null)
            {
                ui.SetItem(index, m_sonList[index]);
                ui.SetItem(index + 1, m_sonList[index + 1]);
                ui.SetSelectedItem(index + 1);
            }
            return true;
        }
        public bool MoveTopElement(int index)
        {
            if (index <= 0 || index >= m_sonList.Count)
            {
                return false;
            }
            Son currentObj = m_sonList[index];
            m_sonList.RemoveAt(index);
            m_sonList.Insert(0, currentObj);
            if (!allowUpdateUI)
            {
                return true;
            }
            if (allowUpdateUI && ui != null)
            {
                ui.RemoveItemAt(index);
                ui.InsertItem(0, currentObj);
                ui.SetSelectedItem(0);
            }
            return true;
        }
        public bool MoveBottomElement(int index)
        {
            if (index < 0 || index >= m_sonList.Count - 1)
            {
                return false;
            }
            Son currentObj = m_sonList[index];
            m_sonList.RemoveAt(index);
            m_sonList.Add(currentObj);
            if (allowUpdateUI && ui != null)
            {
                ui.RemoveItemAt(index);
                ui.AddItem(currentObj);
                ui.SetSelectedItem(m_sonList.Count - 1);
            }
            return true;
        }
        public bool Contains(MSonNode son)
        {
            return GetSonID(son) >= 0;
        }
        #region MParentNode 成员

        public int GetSonID(MSonNode son)
        {
            if (!(son is Son))
            {
                return -1;
            }
            return m_sonList.IndexOf((Son)son);
        }

        #endregion

        #region MSonNode 成员

        public MParentNode GetParent()
        {
            return parent;
        }
        public MParentNode GetTopParent()
        {
            MParentNode parent = GetParent();
            while (((MSonNode)parent).GetParent() != null)
            {
                parent = ((MSonNode)parent).GetParent();
            }
            return parent;
        }
        public void SetParent(MParentNode parentT)
        {
            parent = parentT;
        }
        #endregion
        public virtual void ReadObject(System.IO.Stream s)
        {
            name = IOUtil.readString(s);
            short len;
            m_sonList.Clear();
            len = IOUtil.readShort(s);
            for (short i = 0; i < len; i++)
            {
                Son elem = new Son();
                elem.SetParent(this);
                elem.ReadObject(s);
                m_sonList.Add(elem);
            }
        }
        public virtual void WriteObject(System.IO.Stream s)
        {
            IOUtil.writeString(s, name);
            short len = (short)m_sonList.Count;
            IOUtil.writeShort(s, len);
            for (short i = 0; i < len; i++)
            {
                Son elem = m_sonList[i];
                elem.WriteObject(s);
            }
        }
        public virtual void ExportObject(System.IO.Stream s)
        {
            short len = (short)m_sonList.Count;
            IOUtil.writeShort(s, len);
            for (short i = 0; i < len; i++)
            {
                Son elem = m_sonList[i];
                elem.ExportObject(s);
            }
        }
        public virtual string getValueToLenString()
        {
            return name;
        }
    }
}
