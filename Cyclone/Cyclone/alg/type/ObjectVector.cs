using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Cyclone.alg.type
{
    public class ObjectVector : ElementArray
    {
        public ArrayList objList = new ArrayList();
        public ListBox listBox = null;//数据对应列表框
        public ListBox listBoxAide = null;//数据对应辅助列表框
        public static bool allowUpdateUI = true;//允许更新UI
        public void cloneTo(ObjectVector newInstance)
        {
            newInstance.listBox = listBox;
            newInstance.listBoxAide = listBoxAide;
            for (int i = 0; i < objList.Count; i++)
            {
                ObjectElement element = (ObjectElement)objList[i];
                ObjectElement newElement=element.clone();
                newInstance.addElement(newElement);
                newElement.parent = newInstance;
            }
        }
        //重新设置父类
        public void resetElementParent()
        {
            for (int i = 0; i < getElementCount(); i++)
            {
                ObjectElement element = (ObjectElement)getElement(i);
                element.parent = this;
            }
        }
        //设置显示UI
        public void setUI(ListBox listBoxT)
        {
            listBox = listBoxT;
        }
        public void setUIAide(ListBox listBoxT)
        {
            listBoxAide = listBoxT;
        }
        //刷新显示UI
        public void refreshUI()
        {
            if (listBox == null || !allowUpdateUI)
            {
                return;
            }
            listBox.BeginUpdate();
            listBox.Items.Clear();
            for (int i = 0; i < getElementCount(); i++)
            {
                listBox.Items.Add(((ObjectElement)getElement(i)).getValueToLenString());
            }
            listBox.EndUpdate();
        }
        public void refreshUI(ListBox listBoxT)
        {
            listBox = listBoxT;
            refreshUI();
        }
        public void refreshUIAide()
        {
            if (listBoxAide == null || !allowUpdateUI)
            {
                return;
            }
            listBoxAide.Items.Clear();
            for (int i = 0; i < getElementCount(); i++)
            {
                listBoxAide.Items.Add(((ObjectElement)getElement(i)).getValueToLenString());
            }
        }
        public void refreshUIAide(ListBox listBoxT)
        {
            listBoxAide = listBoxT;
            refreshUIAide();
        }
        //刷新显示UI中的一条
        public void refreshUI_Element(int index)
        {
            if(!allowUpdateUI)
            {
                return;
            }
            String s = null;
            if (listBox != null && index >=0 && index < listBox.Items.Count && index < getElementCount())
            {
                if (s == null)
                {
                    s = ((ObjectElement)getElement(index)).getValueToLenString();
                }
                listBox.Items[index] = s;
            }
            if (listBoxAide != null && index >= 0 && index < listBoxAide.Items.Count && index < getElementCount())
            {
                if (s == null)
                {
                    s = ((ObjectElement)getElement(index)).getValueToLenString();
                }
                listBoxAide.Items[index] = s;
            }
        }
        #region Vector Members

        public bool addElement(Object element)
        {
            if (objList.Count >= short.MaxValue || element == null || objList.Contains(element))
            {
                return false;
            }
            else
            {
                objList.Add(element);
                if (allowUpdateUI)
                {
                    String s = null;
                    if (listBox != null)
                    {
                        if (s == null)
                        {
                            s = ((ObjectElement)element).getValueToLenString();
                        }
                        listBox.BeginUpdate();
                        listBox.Items.Add(s);
                        listBox.EndUpdate();
                        listBox.SelectedIndex = listBox.Items.Count - 1;
                    }
                    if (listBoxAide != null)
                    {
                        if (s == null)
                        {
                            s = ((ObjectElement)element).getValueToLenString();
                        }
                        listBoxAide.BeginUpdate();
                        listBoxAide.Items.Add(s);
                        listBoxAide.EndUpdate();
                        listBoxAide.SelectedIndex = listBoxAide.Items.Count - 1;
                    }
                }

            }
            return true;
        }

        public bool insertElement(Object element, int index)
        {
            if (objList.Count >= short.MaxValue || element == null || objList.Contains(element))
            {
                return false;
            }
            else if (index < 0 || index >= objList.Count)
            {
                addElement(element);
            }
            else
            {
                objList.Insert(index, element);
                if (allowUpdateUI)
                {
                    String s = null;
                    if (listBox != null && index >= 0 && index < listBox.Items.Count)
                    {
                        if (s == null)
                        {
                            s = ((ObjectElement)element).getValueToLenString();
                        }
                        listBox.Items.Insert(index, s);
                        listBox.SelectedIndex = index;
                    }
                    if (listBoxAide != null && index >= 0 && index < listBoxAide.Items.Count)
                    {
                        if (s == null)
                        {
                            s = ((ObjectElement)element).getValueToLenString();
                        }
                        listBoxAide.Items.Insert(index, s);
                        listBoxAide.SelectedIndex = index;
                    }
                }
            }
            return true;
        }
        //设置单元
        public bool setElement(Object element, int index)
        {
            if (objList.Count >= short.MaxValue || element == null)
            {
                return false;
            }
            else if (index < 0 || index >= objList.Count)
            {
                return false;
            }
            else
            {
                objList[index] = element;
                if (allowUpdateUI)
                {
                    String s = null;
                    if (listBox != null && index >= 0 && index < listBox.Items.Count)
                    {
                        if (s == null)
                        {
                            s = ((ObjectElement)getElement(index)).getValueToLenString();
                        }
                        listBox.Items[index] = s;
                        listBox.SelectedIndex = index;
                    }
                    if (listBoxAide != null && index >= 0 && index < listBoxAide.Items.Count)
                    {
                        if (s == null)
                        {
                            s = ((ObjectElement)getElement(index)).getValueToLenString();
                        }
                        listBoxAide.Items[index] = s;
                        listBoxAide.SelectedIndex = index;
                    }
                }
            }
            return true;
        }

        public Object getElement(int index)
        {
            if (index<0||index >= this.objList.Count)
            {
                return null;
            }
            return objList[index];
        }

        public int getElementID(Object element)
        {
            return objList.IndexOf(element);
        }

        public int getElementCount()
        {
            return objList.Count;
        }

        public bool removeElement(int indexRemove)
        {
            int index = indexRemove;
            if (index < 0 || index >= objList.Count)
            {
                Console.WriteLine("error index " + index + " in removeElement");
                return false;
            }
            objList.RemoveAt(index);
            if (allowUpdateUI)
            {
                if (listBox != null && index >= 0 && index < listBox.Items.Count)
                {
                    listBox.Items.RemoveAt(index);
                    if (index >= listBox.Items.Count)
                    {
                        index--;
                    }
                    listBox.SelectedIndex = index;
                }
                index = indexRemove;
                if (listBoxAide != null && index >= 0 && index < listBoxAide.Items.Count)
                {
                    listBoxAide.Items.RemoveAt(index);
                    if (index >= listBoxAide.Items.Count)
                    {
                        index--;
                    }
                    listBoxAide.SelectedIndex = index;
                }
            }
            return true;
        }
        public bool removeElements(ArrayList indexArray)
        {
            if (indexArray == null)
            {
                return false;
            }
            int gap = 0;
            for (int i = 0; i < indexArray.Count; i++)
            {
                int index = (int)indexArray[i] - gap;
                if (index < 0 || index >= objList.Count)
                {
                    Console.WriteLine("error index " + index + " in removeElement");
                    continue;
                }
                objList.RemoveAt(index);
                gap++;
                if (allowUpdateUI)
                {
                    if (listBox != null && index >= 0 && index < listBox.Items.Count)
                    {
                        listBox.Items.RemoveAt(index);
                    }
                    if (listBoxAide != null && index >= 0 && index < listBoxAide.Items.Count)
                    {
                        listBoxAide.Items.RemoveAt(index);
                    }
                }
            }
            return true;
        }
        public void removeAll()
        {
            objList.Clear();
            if (allowUpdateUI)
            {
                if (listBox != null)
                {
                    listBox.Items.Clear();
                }
                if (listBoxAide != null)
                {
                    listBoxAide.Items.Clear();
                }
            }
        }
        public void moveUpElement(int index)
        {
            if (index <= 0 || index >= objList.Count)
            {
                return;
            }
            Object currentObj = objList[index];
            objList.RemoveAt(index);
            objList.Insert(index - 1, currentObj);
            if (!allowUpdateUI)
            {
                return;
            }
            if (listBox != null && index > 0 && index < listBox.Items.Count)
            {
                currentObj=listBox.Items[index];
                listBox.Items.RemoveAt(index);
                listBox.Items.Insert(index - 1, currentObj);
                listBox.SelectedIndex = index-1;
            }
            if (listBoxAide != null && index > 0 && index < listBoxAide.Items.Count)
            {
                currentObj = listBoxAide.Items[index];
                listBoxAide.Items.RemoveAt(index);
                listBoxAide.Items.Insert(index - 1, currentObj);
                listBoxAide.SelectedIndex = index-1;
            }
        }
        public void moveDownElement(int index)
        {
            if (index < 0 || index >= objList.Count-1)
            {
                return;
            }
            Object currentObj = objList[index];
            objList.RemoveAt(index);
            objList.Insert(index+1, currentObj);
            if (!allowUpdateUI)
            {
                return;
            }
            if (listBox != null && index >= 0 && index < listBox.Items.Count-1)
            {
                currentObj = listBox.Items[index];
                listBox.Items.RemoveAt(index);
                listBox.Items.Insert(index+1, currentObj);
                listBox.SelectedIndex = index+1;
            }
            if (listBoxAide != null && index >= 0 && index < listBoxAide.Items.Count - 1)
            {
                currentObj = listBoxAide.Items[index];
                listBoxAide.Items.RemoveAt(index);
                listBoxAide.Items.Insert(index + 1, currentObj);
                listBoxAide.SelectedIndex = index + 1;
            }
        }
        public void moveTopElement(int index)
        {
            if (index <= 0 || index >= objList.Count)
            {
                return;
            }
            Object currentObj = objList[index];
            objList.RemoveAt(index);
            objList.Insert(0, currentObj);
            if (!allowUpdateUI)
            {
                return;
            }
            if (listBox != null && index > 0 && index < listBox.Items.Count)
            {
                currentObj = listBox.Items[index];
                listBox.Items.RemoveAt(index);
                listBox.Items.Insert(0, currentObj);
                listBox.SelectedIndex = 0;
            }
            if (listBoxAide != null && index > 0 && index < listBoxAide.Items.Count)
            {
                currentObj = listBoxAide.Items[index];
                listBoxAide.Items.RemoveAt(index);
                listBoxAide.Items.Insert(0, currentObj);
                listBoxAide.SelectedIndex = 0;
            }
        }
        public void moveBottomElement(int index)
        {
            if (index < 0 || index >= objList.Count - 1)
            {
                return;
            }
            Object currentObj = objList[index];
            objList.RemoveAt(index);
            objList.Add(currentObj);
            if (!allowUpdateUI)
            {
                return;
            }
            if (listBox != null && index >= 0 && index < listBox.Items.Count - 1)
            {
                currentObj = listBox.Items[index];
                listBox.Items.RemoveAt(index);
                listBox.Items.Add(currentObj);
                listBox.SelectedIndex = listBox.Items.Count - 1;
            }
            if (listBoxAide != null && index >= 0 && index < listBoxAide.Items.Count - 1)
            {
                currentObj = listBoxAide.Items[index];
                listBoxAide.Items.RemoveAt(index);
                listBoxAide.Items.Add(currentObj);
                listBoxAide.SelectedIndex = listBoxAide.Items.Count - 1;
            }
        }

        public bool cloneElement(int index)
        {
            if (index < 0 || index >= objList.Count)
            {
                return false;
            }
            ObjectElement elementIndex = ((ObjectElement)getElement(index));
            ObjectElement element = elementIndex.clone();
            addElement(element);
            return true;
        }
        #endregion
    }
}
