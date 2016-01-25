using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Cyclone.alg;
using Cyclone.mod.anim;
using Cyclone.alg.type;
using Cyclone.alg.util;

namespace Cyclone.mod.map
{
    /// <summary>
    /// 文字管理器，拥有一个存放所有文字的列表，并管理文字增减等操作
    /// </summary>
    public class TextsManager : ObjectVector,SerializeAble
    {
        public MClipsManager baseClipsManager = null;
        public Form_Main form_main = null;
        //文字单元列表
        //public ArrayList objList = new ArrayList();
        public TextsManager(Form_Main form_mainT)
        {
            form_main = form_mainT;
            objList = new ArrayList();
        }
        public void setBaseClipsManager(MClipsManager baseClipsManager)
        {
            this.baseClipsManager = baseClipsManager;
        }
        //合并文字资源
        public void combine(TextsManager src_Manager)
        {

            for (int i = 0; i < src_Manager.getElementCount(); i++)
            {
                TextElement srcElement = src_Manager.getElement(i);
                TextElement newElement = null;
                //寻找重复
                for (int j = 0; j < getElementCount(); j++)
                {
                    TextElement localElement = getElement(j);
                    if (localElement.getValue().Equals(srcElement.getValue()))
                    {
                        newElement = localElement;
                        break;
                    }
                }
                if (newElement == null)
                {
                    newElement=new TextElement(this);
                    newElement.setValue(srcElement.getValue());
                    addElement(newElement);
                }
            }
            //将显示容器置空
            this.listBox = null;
            this.listBoxAide = null;
            Console.WriteLine(this.GetHashCode());
        }
        //清除冗余
        public void clearSpilth(bool clearUnUsed)
        {

            for (int i = 0; i < getElementCount(); i++)
            {
                TextElement srcElement = getElement(i);
                if (clearUnUsed)
                {
                    if (srcElement.getUsedTime() == 0)
                    {
                        removeElement(i);
                        i--;
                        continue;
                    }
                }
                //寻找重复
                for (int iLocal = i + 1; iLocal < getElementCount(); iLocal++)
                {
                    TextElement localElement = getElement(iLocal);
                    if (localElement.getValue().Equals(srcElement.getValue()))
                    {
                        removeElement(iLocal);
                        iLocal--;
                    }
                }
            }

        }
        //串行化输入与输出===================================================================
        //串行读入
        public void ReadObject(Stream s)
        {
            int textsLen = IOUtil.readInt(s);
            if (textsLen <= 0)
            {
                return;
            }
            objList.Clear();
            for (int i = 0; i < textsLen; i++)
            {
                TextElement txtElement = new TextElement(this);
                txtElement.ReadObject(s);
                addElement(txtElement);//增加到列表
                //Consts.loadingDialog.setStep(40 + i * 10 / textsLen, "载入文字:" + txtElement.getValue());
            }
        }
        //串行输出
        public void WriteObject(Stream s)
        {
            int imgsNum = objList.Count;
            IOUtil.writeInt(s, imgsNum);
            for (int i = 0; i < imgsNum; i++)
            {
                TextElement element = getElement(i);
                element.WriteObject(s);
            }
        }
        //串行输出
        public void ExportObject(Stream fs_bin)
        {
            fs_bin = null;
            String fileName = Consts.exportC2DBinFolder + Consts.exportFileName + "_texts.bin";
            if (File.Exists(fileName))
            {
                fs_bin = File.Open(fileName, FileMode.Truncate);
            }
            else
            {
                fs_bin = File.Open(fileName, FileMode.OpenOrCreate);
            }

            int txtsNum = objList.Count;
            IOUtil.writeShort(fs_bin, (short)txtsNum);
            for (int i = 0; i < txtsNum; i++)
            {
                TextElement element = getElement(i);
                element.ExportObject(fs_bin);
            }

            if (fs_bin != null)
            {
                fs_bin.Flush();
                fs_bin.Close();
            }
        }
        //文字单元操作=====================================================================
        ////增加文字单元到列表
        //public void addTextElement(TextElement imgElement)
        //{
        //    objList.Add(imgElement);
        //}
        //删除列表中的文字单元
        public bool removeTextElementAt(int index)
        {
            if (index < 0 || index >= objList.Count)
            {
                return false;
            }
            TextElement textElem = (TextElement)objList[index];
            int usedTime = textElem.getUsedTime();
            if (usedTime > 0)//提醒有切片在使用
            {
                MessageBox.Show("该文字被" + usedTime + "处引用，不能删除。", "错误操作", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                this.removeElement(index);
                return true;
            }
        }
        //根据索引返回单元
        public new TextElement getElement(int index)
        {
            if (objList == null || index < 0 || index >= this.objList.Count)
            {
                return null;
            }
            return (TextElement)objList[index];
        }
        //根据单元返回索引
        public int getElementIndx(TextElement textElement)
        {
            return objList.IndexOf(textElement);
        }
        
        //返回单元数目
        public new int getElementCount()
        {
            return objList.Count;
        }
    }
    public class TextElement : ObjectElement,SerializeAble
    { 
        private  String text = null;

        public TextElement(String textT, TextsManager parentT)
        {
            this.parent = parentT;
            text = textT;
        }

        public TextElement(TextsManager parentT)
        {
            this.parent = parentT;
        }
        public new int getID()
        {
            if (this.parent == null)
            {
                return -1;
            }
            return parent.getElementID(this);
        }
        public void setValue(String textT)
        {
            text = textT;
        }
        public new String getValue()
        {
            return text;
        }
        //返回使用次数
        public override int getUsedTime()
        {
            int usedTime = 0;
            return usedTime;
        }
        public String getUsedInfor()
        {
            String s = null;
            return s;
        }
        #region SerializeAble Members

        public void ReadObject(Stream s)
        {
            text = IOUtil.readString(s);
        }

        public void WriteObject(Stream s)
        {
            IOUtil.writeString(s, text);
        }

        public void ExportObject(Stream fs_bin)
        {
            IOUtil.writeString(fs_bin, text);
        }
        #endregion

        public override string getValueToLenString()
        {
            return this.text;
        }

        public override ObjectElement clone()
        {
            TextElement newInstance = new TextElement((TextsManager)parent);
            newInstance.setValue(text + "");
            return newInstance;
        }
    }
}
