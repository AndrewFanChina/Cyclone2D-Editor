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
        //д������
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
        //д����������===================================================
        //���byte
        public static void writeByte(Stream s, byte data)
        {
            if (s == null)
            {
                return;
            }
            s.WriteByte(data);
        }
        //���short
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
        //���int
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
        //���float

        public static void writeFloat(Stream s,float data)
        {
            int iData = MiscUtil.floatToInt(data);
            writeInt(s, iData);
        }
        //�������
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
        //���벼������
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

        //���뵥������===================================================
        //����byte
        private static byte[] byteBuf = new byte[1];
        public static byte readByte(Stream s)
        {
            s.Read(byteBuf, 0, byteBuf.Length);
            return byteBuf[0];
            //return (byte)s.ReadByte();
        }
        //����short
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
        //����int

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
        //����float

        public static float readFloat(Stream s)
        {
            int data = readInt(s);
            float fData = MiscUtil.intToFloat(data);
            return fData;
        }
        //��������
        public static String readString(Stream s)
        {
            String str = null;
            int len = readShort(s);
            byte[] bytes = new byte[len];
            s.Read(bytes, 0, bytes.Length);
            str = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return str;
        }
        //��������
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
        //���벼������
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
        //���������ļ�
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
        //д����������===================================================
        //������������===================================================

        //��ԴIO����=====================================================
        //����ͼƬ
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
        //����ļ���С
        public static long getFileSize(String path)
        {
            if (!File.Exists(path))
            {
                return 0;
            }
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Length;
        }
        //�����ⲿ����
        public static void OpenProcess(string cmd, String parems, ShowString dialogRes)
        {
            OpenProcess(cmd, parems, dialogRes, true);
        }
        public static void OpenProcess(string cmd, String parems, ShowString dialogRes, bool waiteToEnd)
        {
            System.Diagnostics.Process myProcess = new Process();
            //�����ⲿ������
            myProcess.StartInfo.FileName = cmd;
            //�����ⲿ��������������������в�����Ϊtest.txt
            myProcess.StartInfo.Arguments = parems;
            //�����ⲿ������Ŀ¼Ϊ  C:\
            myProcess.StartInfo.WorkingDirectory = "C:\\";
            myProcess.StartInfo.UseShellExecute = false; //�˴�����Ϊfalse���������쳣 

            myProcess.StartInfo.RedirectStandardInput = dialogRes != null; //��׼���� 
            myProcess.StartInfo.RedirectStandardOutput = dialogRes != null; //��׼��� 
            myProcess.StartInfo.RedirectStandardError = dialogRes != null;   //�ض���������


            //����ʾ�����д��ڽ��� 
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            //����һ��������

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
                    dialogRes.showString("ϵͳ�Ҳ���ָ���ĳ����ļ���\n" + e.Message);
                }
            }



        }
        //�����
        public static bool  inRightDomain(String domainName)
        {
            if (domainName == null)
            {
                return false;
            }
            //��ѯ�����ϵļ����IP���û���Ҫ����
            //��ȡ�����ھ��е����м����IP�͵�ǰ��¼�û�
            String DomainName = System.Environment.UserDomainName;
            string strDomain;
            string strComputer;
            DirectoryEntry root = new DirectoryEntry("WinNT:");
            foreach (DirectoryEntry Domain in root.Children)
            {
                //ö�ٹ��������
                strDomain = Domain.Name;
                //Console.WriteLine("strDomain:" + strDomain);
                if (!domainName.Equals(strDomain))
                {
                    continue;
                }
                foreach (DirectoryEntry Computer in Domain.Children)
                {
                    //ö��ָ�����������ļ���� 
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
        //�����ļ�
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
        //�г�ָ��Ŀ¼���������ƥ���ļ���searchPattern����ʹ"*.txt|*.bmp"
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
