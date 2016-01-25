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
    /// ���ֹ�������ӵ��һ������������ֵ��б����������������Ȳ���
    /// </summary>
    public class TextsManager : ObjectVector,SerializeAble
    {
        public MClipsManager baseClipsManager = null;
        public Form_Main form_main = null;
        //���ֵ�Ԫ�б�
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
        //�ϲ�������Դ
        public void combine(TextsManager src_Manager)
        {

            for (int i = 0; i < src_Manager.getElementCount(); i++)
            {
                TextElement srcElement = src_Manager.getElement(i);
                TextElement newElement = null;
                //Ѱ���ظ�
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
            //����ʾ�����ÿ�
            this.listBox = null;
            this.listBoxAide = null;
            Console.WriteLine(this.GetHashCode());
        }
        //�������
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
                //Ѱ���ظ�
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
        //���л����������===================================================================
        //���ж���
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
                addElement(txtElement);//���ӵ��б�
                //Consts.loadingDialog.setStep(40 + i * 10 / textsLen, "��������:" + txtElement.getValue());
            }
        }
        //�������
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
        //�������
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
        //���ֵ�Ԫ����=====================================================================
        ////�������ֵ�Ԫ���б�
        //public void addTextElement(TextElement imgElement)
        //{
        //    objList.Add(imgElement);
        //}
        //ɾ���б��е����ֵ�Ԫ
        public bool removeTextElementAt(int index)
        {
            if (index < 0 || index >= objList.Count)
            {
                return false;
            }
            TextElement textElem = (TextElement)objList[index];
            int usedTime = textElem.getUsedTime();
            if (usedTime > 0)//��������Ƭ��ʹ��
            {
                MessageBox.Show("�����ֱ�" + usedTime + "�����ã�����ɾ����", "�������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                this.removeElement(index);
                return true;
            }
        }
        //�����������ص�Ԫ
        public new TextElement getElement(int index)
        {
            if (objList == null || index < 0 || index >= this.objList.Count)
            {
                return null;
            }
            return (TextElement)objList[index];
        }
        //���ݵ�Ԫ��������
        public int getElementIndx(TextElement textElement)
        {
            return objList.IndexOf(textElement);
        }
        
        //���ص�Ԫ��Ŀ
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
        //����ʹ�ô���
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
