using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.DirectoryServices;
using System.Management;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
namespace Cyclone.alg.util
{
    public class IOUtil
    {
        private static byte[] buffer = new byte[4];
        //写出数据
        public static void writeData(Stream s,byte[]data)
        {
            if (s == null)
            {
                return;
            }
            if (data == null)
            {
                return;
            }
            s.Write(data, 0, data.Length);
        }
        //写出单个数据===================================================
        //输出byte
        public static void writeByte(Stream s, byte data)
        {
            if (s == null)
            {
                return;
            }
            s.WriteByte(data);
        }
        //输出short
        public static void writeShort(Stream s, short data)
        {
            if (s == null)
            {
                return;
            }
            for (int i = 0; i < 2; i++)
            {
                buffer[i] = 0;
                buffer[i] |= (byte)((data >> 8 * (2 - 1 - i)) & 0xFF);
            }
            s.Write(buffer, 0, 2);
        }
        //输出int
        public static void writeInt(Stream s, int data)
        {
            if (s == null)
            {
                return;
            }
            for (int i = 0; i < 4; i++)
            {
                buffer[i] = 0;
                buffer[i] |= (byte)((data >> 8 * (4 - 1 - i)) & 0xFF);
            }
            s.Write(buffer, 0, 4);
        }
        //输出float

        public static void writeFloat(Stream s,float data)
        {
            int iData = MiscUtil.floatToInt(data);
            writeInt(s, iData);
        }
        //输出文字
        public static void writeString(Stream s, String str)
        {
            if (s == null)
            {
                return;
            }
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            writeShort(s, (short)bytes.Length);
            s.Write(bytes, 0, bytes.Length);
        }

        public static void writeText(Stream s, String str)
        {
            if (s == null)
            {
                return;
            }
            byte[] bytes = System.Text.Encoding.GetEncoding("GBK").GetBytes(str);
            if (bytes != null)
            {
                s.Write(bytes, 0, bytes.Length);
            }
        }
        public static void writeTextLine(Stream s, String str)
        {
            writeTextLineGBK(s, str);
        }
        public static void writeTextLineGBK(Stream s, String str)
        {
            if (s == null)
            {
                return;
            }
            if (str == null)
            {
                str = "";
            }
            byte[] bytes = System.Text.Encoding.GetEncoding("GBK").GetBytes(str + "\r\n");
            if (bytes != null)
            {
                s.Write(bytes, 0, bytes.Length);
            }
        }
        public static void writeTextLineUTF8(Stream s, String str)
        {
            if (s == null)
            {
                return;
            }
            if (str == null)
            {
                str = "";
            }
            byte[] bytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(str + "\r\n");
            if (bytes != null)
            {
                s.Write(bytes, 0, bytes.Length);
            }
        }
        public static void writeTextLineUnicode(Stream s, String str)
        {
            if (s == null)
            {
                return;
            }
            if (str == null)
            {
                str = "";
            }
            byte[] bytes = System.Text.Encoding.GetEncoding("Unicode").GetBytes(str + "\r\n");
            if (bytes != null)
            {
                s.Write(bytes, 0, bytes.Length);
            }
        }
        public static void writeTextAllGBK(Stream s, String str)
        {
            StreamWriter streamWriter = new StreamWriter(s, Encoding.GetEncoding("GBK"));
            streamWriter.Write(str);
            streamWriter.Flush();
            streamWriter.Close();
        }
        //读入布尔数据
        public static void writeBoolean(Stream s,bool data)
        {
            if (data)
            {
                writeByte(s, 1);
            }
            else
            {
                writeByte(s, 0);
            }
        }

        //读入单个数据===================================================
        //读入byte
        private static byte[] byteBuf = new byte[1];
        public static byte readByte(Stream s)
        {
            s.Read(byteBuf, 0, byteBuf.Length);
            return byteBuf[0];
            //return (byte)s.ReadByte();
        }
        //读入short
        public static short readShort(Stream s)
        {
            short data = 0;
            s.Read(buffer, 0, 2);
            for (int i = 0; i < 2; i++)
            {
                data |= (short)(buffer[i] << (8 * (2 - 1 - i)));
            }
            return data;
        }
        //读入int

        public static int readInt(Stream s)
        {
            int data = 0;
            s.Read(buffer, 0, 4);
            for (int i = 0; i < 4; i++)
            {
                data |= buffer[i] << 8 * (4 - 1 - i);
            }
            return data;
        }
        //读入float

        public static float readFloat(Stream s)
        {
            int data = readInt(s);
            float fData = MiscUtil.intToFloat(data);
            return fData;
        }
        //读入文字
        public static String readString(Stream s)
        {
            String str = null;
            int len = readShort(s);
            byte[] bytes = new byte[len];
            s.Read(bytes, 0, bytes.Length);
            str = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return str;
        }
        //读入文字
        public static String readTextAllGBK(Stream s)
        {
            StreamReader streamReader = new StreamReader(s, Encoding.GetEncoding("GBK"));
            String strAll = streamReader.ReadToEnd();
            streamReader.Close();
            return strAll;
        }
        public static String readTextAllUnicode(Stream s)
        {
            StreamReader streamReader = new StreamReader(s,Encoding.Unicode);
            String strAll = streamReader.ReadToEnd();
            streamReader.Close();
            return strAll;
        }
        public static ArrayList readTextLinesGBK(Stream s)
        {
            ArrayList arrayText = new ArrayList();
            String allTxt = readTextAllGBK(s);
            String[] allTxts = allTxt.Split(new String[] { "\r\n", "\n" }, StringSplitOptions.None);
            for (int i = 0; i < allTxts.Length; i++)
            {
                arrayText.Add(allTxts[i].Trim(new char[] { '\r', '\n' }));
            }
            return arrayText;
        }
        public static ArrayList readTextLinesUnicode(Stream s)
        {
            ArrayList arrayText = new ArrayList();
            String allTxt = readTextAllUnicode(s);
            String[] allTxts = allTxt.Split(new String[] { "\r\n", "\n" }, StringSplitOptions.None);
            for (int i = 0; i < allTxts.Length; i++)
            {
                arrayText.Add(allTxts[i].Trim(new char[] { '\r', '\n' }));
            }
            return arrayText;
        }
        //读入布尔数据
        public static bool readBoolean(Stream s)
        {
            byte t = IOUtil.readByte(s);
            bool res = false;
            if (t == 1)
            {
                res = true;
            }
            else if (t == 0)
            {
                res = false;
            }
            else
            {
                Console.WriteLine("error in readBoolean");
            }
            return res;
        }
        //读入整个文件
        public static byte[] ReadFile(string name)
        {
            int fileLength;
            Byte[] data = null;
            if (File.Exists(name))
            {
                File.SetAttributes(name, FileAttributes.Normal);
                FileStream fs = new FileStream(name, FileMode.Open);
                fileLength = (int)fs.Length;
                data = new Byte[fileLength];
                fs.Read(data, 0, fileLength);
                fs.Close();
            }
            return data;
        }
        //写出数组数据===================================================
        //读入数组数据===================================================

        //资源IO操作=====================================================
        //读入图片
        public static Image createImage(String path)
        {
            Image img = null;
            FileStream fs = null;
            try
            {
                if(File.Exists(path))
                {
                    File.SetAttributes(path, FileAttributes.Normal); 
                }
                fs = new FileStream(path, FileMode.Open);
                img=Image.FromStream(fs);
                //Image imgSrc = Image.FromStream(fs);
                //img = new Bitmap(imgSrc);
                //imgSrc.Dispose();
                //imgSrc = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("load " + path + " error:" + e.Message);
            }
            finally
            {
                try
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs = null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return img;
        }
        public static Image createImage(Stream s)
        {
            Image img = null;
            try
            {
                img = Image.FromStream(s);
                //Image imgSrc = Image.FromStream(s);
                //img = new Bitmap(imgSrc);
                //imgSrc.Dispose();
                //imgSrc = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("load image error:" + e.Message);
            }
            finally
            {
                try
                {
                    if (s != null)
                    {
                        s.Close();
                        s = null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return img;
        }
        //获得文件大小
        public static long getFileSize(String path)
        {
            if (!File.Exists(path))
            {
                return 0;
            }
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Length;
        }
        //启动外部程序
        public static void OpenProcess(string cmd, String parems, ShowString dialogRes)
        {
            OpenProcess(cmd, parems, dialogRes, true);
        }
        public static void OpenProcess(string cmd, String parems, ShowString dialogRes, bool waiteToEnd)
        {
            System.Diagnostics.Process myProcess = new Process();
            //设置外部程序名
            myProcess.StartInfo.FileName = cmd;
            //设置外部程序的启动参数（命令行参数）为test.txt
            myProcess.StartInfo.Arguments = parems;
            //设置外部程序工作目录为  C:\
            myProcess.StartInfo.WorkingDirectory = "C:\\";
            myProcess.StartInfo.UseShellExecute = false; //此处必须为false否则引发异常 

            myProcess.StartInfo.RedirectStandardInput = dialogRes != null; //标准输入 
            myProcess.StartInfo.RedirectStandardOutput = dialogRes != null; //标准输出 
            myProcess.StartInfo.RedirectStandardError = dialogRes != null;   //重定向错误输出


            //不显示命令行窗口界面 
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            //声明一个程序类

            try
            {
                myProcess.Start();
                if (dialogRes!=null)
                {
                    dialogRes.showString(myProcess.StandardOutput.ReadToEnd());
                }
                if (waiteToEnd)
                {
                    myProcess.WaitForExit();
                }
                myProcess.Close();
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                if (dialogRes != null)
                {
                    dialogRes.showString("系统找不到指定的程序文件。\n" + e.Message);
                }
            }



        }
        //获得域
        public static bool  inRightDomain(String domainName)
        {
            if (domainName == null)
            {
                return false;
            }
            //查询网络上的计算机IP和用户需要引用
            //获取网络邻居中的所有计算机IP和当前登录用户
            String DomainName = System.Environment.UserDomainName;
            string strDomain;
            string strComputer;
            DirectoryEntry root = new DirectoryEntry("WinNT:");
            foreach (DirectoryEntry Domain in root.Children)
            {
                //枚举工作组或域
                strDomain = Domain.Name;
                //Console.WriteLine("strDomain:" + strDomain);
                if (!domainName.Equals(strDomain))
                {
                    continue;
                }
                foreach (DirectoryEntry Computer in Domain.Children)
                {
                    //枚举指定工作组或域的计算机 
                    if (Computer.SchemaClassName.Equals("Computer"))
                    {
                        strComputer = Computer.Name;
                        //Console.WriteLine("strComputer:" + strComputer);
                        if (strComputer.Equals(DomainName))
                        {
                            return true;
                        }
                    }
                }
            } 
            return false;
        }
        //拷贝文件
        public static void Copy(String src, String dest, bool overwrite)
        {
            if (File.Exists(src))
            {
                if (dest != null && !dest.Equals(src))
                {
                    try
                    {
                        File.Copy(src, dest, overwrite);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("error when copy from " + src + " to " + dest, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }
        //列出指定目录下面的所有匹配文件，searchPattern可以使"*.txt|*.bmp"
        public static String[] listFiles(String folder, String searchPattern)
        {
            String[] seachPattens = searchPattern.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            Hashtable array = new Hashtable();
            for (int i = 0; i < seachPattens.Length; i++)
            {
                String []files = Directory.GetFiles(folder, seachPattens[i]);
                for (int j = 0; j < files.Length; j++)
                {
                    if (!array.ContainsValue(files[j]))
                    {
                        array.Add(array.Count, files[j]);
                    }
                }
            }
            String[] allFiles = new String[array.Count];
            for (int j = 0; j < allFiles.Length; j++)
            {
                allFiles[j] = (String)array[j];
            }
            array.Clear();
            array = null;
            return allFiles;
        }
    }
}
