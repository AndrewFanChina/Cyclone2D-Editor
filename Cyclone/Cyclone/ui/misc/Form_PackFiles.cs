using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.mod;
using System.Drawing.Imaging;
using Cyclone.alg;
using System.Collections;
using System.IO;
using Cyclone.alg.util;

namespace Cyclone.mod.misc
{
    public partial class Form_PackFiles : Form,ShowString
    {
        public Form_PackFiles()
        {
            InitializeComponent();
        }
        public void setStep(int step, int stepAll)
        {
 
        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_openSrc_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox_PathSrc.Text))
            {
                folderBrowserDialog.SelectedPath = textBox_PathSrc.Text;
            }
            else
            {
                folderBrowserDialog.SelectedPath = null;
            }
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBox_PathSrc.Text = folderBrowserDialog.SelectedPath;
            }
            
        }

        public void showString(String text)
        {
            this.richTextBox.Text += text + "\n";
            richTextBox.Select(richTextBox.TextLength - 1, 0);
            richTextBox.ScrollToCaret();
        }

        private void button_excute_Click(object sender, EventArgs e)
        {
            String srcPath = this.textBox_PathSrc.Text;
            if (!Directory.Exists(srcPath))
            {
                showString("源文件目录不存在，请重新选择。");
                return;
            }
            String ruleFileTxt = textBox_ruleFile.Text;
            if (ruleFileTxt != null && !ruleFileTxt.Equals(""))//按表文件打包
            {
                if (!File.Exists(ruleFileTxt))
                {
                    showString("列表文件不存在，请重新选择。");
                    return;
                }
                packByRuleFile(ruleFileTxt);
            }
            else//按大小打包所有文件
            {
                int maxPack = Convert.ToInt32(numericUpDown_packSize.Value) * 1024;
                packFiles(this,srcPath,"*.*", maxPack,false);
            }
        }
        //压缩图片
        public static void packFiles(ShowString dialog,String srcPath, String searchPatten, int maxPack,bool deleteOld)
        {
            //清除包裹文件
            dialog.showString("==>清除旧包裹文件");
            String[] files = Directory.GetFiles(srcPath, "*.*");
            for (int i = 0; i < files.Length; i++)
            {
                int pos = files[i].LastIndexOf('\\');
                String name = files[i].Substring(pos + 1, files[i].Length - (pos + 1));
                if (name.IndexOf("filePacks")>=0 || name.IndexOf("aep")>=0)
                {
                    File.Delete(files[i]);
                }
            }
            //开始转换
            files = IOUtil.listFiles(srcPath, searchPatten);
            dialog.showString("==>共发现" + files.Length + "个文件");
            ArrayList filePackList = new ArrayList();
            FilePack filePack = new FilePack();
            filePackList.Add(filePack);
            int addUpSize = 0;
            for (int i = 0; i < files.Length; i++)
            {
                int pos = files[i].LastIndexOf('\\');
                String name = files[i].Substring(pos + 1, files[i].Length - (pos + 1));
                byte[] data = IOUtil.ReadFile(files[i]);
                addUpSize += data.Length;
                if (i == 0)//第一个包，则将当前文件压入这个包
                {
                    filePack.addFile(name, data);
                    if (addUpSize >= maxPack)
                    {
                        filePack = new FilePack();
                        filePackList.Add(filePack);
                        addUpSize = data.Length;
                    }
                }
                else//不是第一个包，则将当前文件压入后一个包
                {
                    if (addUpSize >= maxPack)
                    {
                        filePack = new FilePack();
                        filePackList.Add(filePack);
                        addUpSize = data.Length;
                    }
                    filePack.addFile(name, data);
                }
            }
            //开始输出
            String dictionFile = srcPath + "\\" + Consts.exportFileName + "_filePacks.bin";
            FileStream fsDictionary = new FileStream(dictionFile, FileMode.Create);
            IOUtil.writeShort(fsDictionary, (short)filePackList.Count);
            IOUtil.writeShort(fsDictionary, (short)files.Length);
            dialog.showString("==>即将输出" + (filePackList.Count) + "个包裹");
            int outputFiles = 0;
            for (int i = 0; i < filePackList.Count; i++)
            {
                FilePack pack = (FilePack)filePackList[i];
                IOUtil.writeShort(fsDictionary, (short)pack.fileNames.Count);
                String strPack = srcPath + "\\" + Consts.exportFileName + "_" + i + ".aep";
                FileStream fsPackI = new FileStream(strPack, FileMode.Create);
                int dataPos = 0;
                dialog.showString("-->开始输出第" + (i + 1) + "个包裹，共" + pack.fileNames.Count + "个文件");
                for (int j = 0; j < pack.fileNames.Count; j++)
                {
                    String fileName = (String)(pack.fileNames[j]);
                    byte[] data = (byte[])pack.fileDatas[j];
                    IOUtil.writeString(fsDictionary, fileName);
                    IOUtil.writeInt(fsDictionary, dataPos);
                    IOUtil.writeInt(fsDictionary, data.Length);
                    IOUtil.writeData(fsPackI, data);
                    dataPos += data.Length;
                    dialog.showString("->加入第" + (j + 1) + "个文件[" + fileName + "]，长度[" + data.Length + "]，当前包裹大小[" + dataPos + "]");
                    if (deleteOld)
                    {
                        File.Delete(srcPath+"\\"+fileName);
                    }
                    outputFiles++;
                    dialog.setStep(outputFiles, files.Length);
                }
                fsPackI.Flush();
                fsPackI.Close();
                dialog.showString("->完成输出第" + (i + 1) + "个包裹");
            }
            fsDictionary.Flush();
            fsDictionary.Close();
        }
        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public int convertStrToInt(String s, int row)
        {
            int value = -1;
            if (s == null)
            {
                showString("【错误】字符" + s + "转换成数值失败，发生在行:" + row);
                return value;
            }
            try
            {
                value = Convert.ToInt32(s);
            }
            catch (Exception)
            {
                showString("【错误】字符" + s + "转换成数值失败，发生在行:" + row + ",内容:" + s);
            }
            return value;
        }
        class FilePack
        {
            public ArrayList fileNames = new ArrayList();
            public ArrayList fileDatas = new ArrayList();
            public void addFile(String name,byte []data)
            {
                fileNames.Add(name);
                fileDatas.Add(data);
            }

        }

        private void button_openRuleFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            dialog.FileName = "*.txt";
            dialog.Title = "打开列表文件";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_ruleFile.Text = dialog.FileName;
            }
        }
        //根据列表文件载入和打包
        public VarList varList = null;
        private ArrayList allPacks = null;
        public void packByRuleFile(String filePath)
        {
            //检验打包列表文件
            FileStream fsRuleFile = new FileStream(filePath, FileMode.Open);
            ArrayList arrayBuff = null;
            try
            {
                arrayBuff = IOUtil.readTextLinesGBK(fsRuleFile);
                int rowID = 0;
                varList = new VarList(this);
                allPacks = new ArrayList();
                while (true)
                {
                    String s = (String)arrayBuff[rowID];
                    if (s.Trim().Equals("#defineBegin"))
                    {
                        rowID++;
                        rowID = varList.readElement(arrayBuff, rowID);
                    }
                    else if (s.Trim().Equals("#packBegin"))
                    {
                        rowID++;
                        FilePackElement filePack = new FilePackElement(this);
                        rowID = filePack.readElement(arrayBuff, rowID);
                        allPacks.Add(filePack);
                    }
                    rowID++;
                    if (rowID < 0 || rowID >= arrayBuff.Count)
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {
 
            }
            //生成包裹序列

        }
        public class VarList
        {
            Form_PackFiles form = null;
            Hashtable hushTable = new Hashtable();
            public int readElement(ArrayList array, int scroll)
            {
                bool reachEnd = false;
                bool error = false;
                while (true)
                {
                    String s = (String)array[scroll];
                    if (s.StartsWith("#defineEnd"))
                    {
                        reachEnd = true;
                    }
                    else
                    {
                        String []sSeperate = s.Trim().Split(' ');
                        int equalsID = -1;
                        for (int i = 0; i < sSeperate.Length; i++)
                        {
                            if (sSeperate[i].Equals("="))
                            {
                                equalsID = i;
                                break;
                            }
                        }
                        if (equalsID<=0 || equalsID + 1 >= sSeperate.Length)
                        {
                            error = true;
                        }
                        else
                        {
                            String name = sSeperate[equalsID - 1];
                            String vlue = sSeperate[equalsID + 1];
                            vlue = vlue.Replace(";", "");
                            int intValue = form.convertStrToInt(vlue, scroll);
                            if(!addElement(name,intValue))
                            {
                                error = true;
                            }
                        }
                    }
                    if (error)
                    {
                        form.showString("【错误】发生在行:" + (scroll + 1));
                        scroll = array.Count;
                        break;
                    }
                    scroll++;
                    if (scroll >= array.Count || reachEnd)
                    {
                        break;
                    }
                }
                return scroll;
            }
            public VarList(Form_PackFiles formT)
            {
                form = formT;
            }
            public bool addElement(String name, int value)
            {
                if (name==null||hushTable.Contains(name))
                {
                    return false;
                }
                hushTable.Add(name, value);
                return true;
            }
            public int getVarElement(String name)
            {
                if (name == null || !hushTable.Contains(name))
                {
                    return -1;
                }
                return (int)hushTable[name];
            }
        }
        class FilePackElement
        {
            public int mapStart;
            public int mapEnd;
            public ArrayList scene_ad_IDList = new ArrayList();
            public ArrayList scene_tp_IDList = new ArrayList();
            public ArrayList pic_nameList = new ArrayList();
            Form_PackFiles form = null;
            public FilePackElement(Form_PackFiles formT)
            {
                form = formT;
            }
            public int readElement(ArrayList array, int scroll)
            {
                if (scroll < 0 || scroll >= array.Count)
                {
                    scroll = array.Count;
                    return scroll;
                }
                bool reachElementEnd = false;
                bool error = false;
                String s = null;
                String []sSeperate = null;
                while (true)
                {
                    s = (String)array[scroll];
                    if (s.StartsWith("#map_ID:"))
                    {
                        s = s.Trim().Replace("#map_ID:", "");
                        sSeperate = s.Split(',');
                        if (sSeperate != null && sSeperate.Length >= 2)
                        {
                            mapStart = form.convertStrToInt(sSeperate[0], scroll);
                            mapEnd = form.convertStrToInt(sSeperate[1], scroll);
                        }
                        else
                        {
                            form.showString("【错误】MAP ID长度应该==2");
                            error = true;
                        }
                    }
                    else if (s.StartsWith("#scene_ad_ID:"))
                    {
                        s = s.Trim().Replace("#scene_ad_ID:", "");
                        sSeperate = s.Split(',');
                        for (int i = 0; i < sSeperate.Length; i++)
                        {
                            if (sSeperate[i].Equals(""))
                            {
                                continue;
                            }
                            int data = form.varList.getVarElement(sSeperate[i]);
                            if (data < 0)
                            {
                                form.showString("【错误】未能找到定义的参数:" + (sSeperate[i]));
                                error = true;
                                break;
                            }
                            scene_ad_IDList.Add(data);
                        }
                    }
                    else if (s.StartsWith("#scene_tp_ID:"))
                    {
                        s = s.Trim().Replace("#scene_tp_ID:", "");
                        sSeperate = s.Split(',');
                        for (int i = 0; i < sSeperate.Length; i++)
                        {
                            if (sSeperate[i].Equals(""))
                            {
                                continue;
                            }
                            int data = form.convertStrToInt(sSeperate[i], scroll);
                            if (data < 0)
                            {
                                form.showString("【错误】未能找到定义的参数:" + (sSeperate[i]));
                                error = true;
                                break;
                            }
                            scene_ad_IDList.Add(data);
                        }
                    }
                    else if (s.StartsWith("#pic_name:"))
                    {
                        s = s.Trim().Replace("#scene_tp_ID:", "");
                        sSeperate = s.Split(',');
                        for (int i = 0; i < sSeperate.Length; i++)
                        {
                            if (sSeperate[i] == null||sSeperate[i].Equals(""))
                            {
                                continue;
                            }
                            scene_ad_IDList.Add(sSeperate[i]);
                        }
                    }
                    else if (s.Trim().Equals("#packEnd"))
                    {
                        reachElementEnd = true;   
                    }
                    else if (s.Trim().Equals(""))
                    {

                    }
                    else
                    {
                        error = true;
                    }
                    if (error)
                    {
                        form.showString("【错误】发生在行:" + (scroll + 1));
                        scroll = array.Count;
                        break;
                    }
                    scroll++;
                    if (scroll >= array.Count || reachElementEnd)
                    {
                        break;
                    }
                }
                return scroll;
            }

        }
    }
}