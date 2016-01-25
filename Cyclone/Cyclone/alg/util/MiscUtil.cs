using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Management;

namespace Cyclone.alg.util
{
    class MiscUtil
    {
        //--------------------------------声音播放--------------------------------
        private static MiscUtil instance = new MiscUtil();
        //private static AxWMPLib.AxWindowsMediaPlayer mediaPlayer = null;
        //private static int mediaPlayerLoop = 1;
        //声音播放
        //public static void playSound(AxWMPLib.AxWindowsMediaPlayer mediaPlayerT,String url,int loopCount)
        //{
        //    mediaPlayer = mediaPlayerT;
        //    mediaPlayer.PlayStateChange -= instance.mediaPlayer_PlayStateChange;
        //    mediaPlayer.PlayStateChange += instance.mediaPlayer_PlayStateChange;
        //    mediaPlayerLoop = loopCount;
        //    if (mediaPlayerLoop != 0)
        //    {
        //        mediaPlayer.URL = url;
        //    }
        //}
        //private void mediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        //{
        //    if (e.newState == 1)
        //    {
        //        if (mediaPlayerLoop < 0)
        //        {
        //            Console.WriteLine("===============restart===============");
        //            mediaPlayer.Ctlcontrols.play();
        //        }
        //        else
        //        {
        //            mediaPlayerLoop--;
        //            if (mediaPlayerLoop > 0)
        //            {
        //                mediaPlayer.Ctlcontrols.play();
        //            }
        //        }
        //    }
        //}
        public static void playSound(String sound)
        {
            System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(sound);
            sndPlayer.PlayLooping(); 
        }
        //--------------------------------字符串处理--------------------------------
        //空白字符串集合
        public static ArrayList vBlanks = null;
        public static void initVBlanks()
        {
            if (vBlanks == null)
            {
                vBlanks = new ArrayList();
                String s100 = "";
                String s10 = "";
                String s1 = " ";
                for (int i = 0; i < 10; i++)
                {
                    s10 += s1;
                }
                for (int i = 0; i < 10; i++)
                {
                    s100 += s10;
                }
                int len, num;
                for (int i = 0; i < 170; i++)
                {
                    String s = "";
                    num = i;
                    len = num / 100;
                    for (int j = 0; j < len; j++)
                    {
                        s += s100;
                    }
                    num %= 100;
                    len = num / 10;
                    for (int j = 0; j < len; j++)
                    {
                        s += s10;
                    }
                    num %= 10;
                    len = num;
                    for (int j = 0; j < len; j++)
                    {
                        s += s1;
                    }
                    vBlanks.Add(s);
                }
            }
        }
        //将指定字符内容，以附加空白的形式格式化成指定的宽度的字符内容
        public static String AppendBlanksToString(String content,int uiWidth)
        {
            MiscUtil.initVBlanks();

            Encoding encode = Encoding.Default;
            int contentLen = encode.GetByteCount(content) * 6;
            int balnkLen = (int)((uiWidth - contentLen) / 6);
            if (balnkLen >= 0 && balnkLen < MiscUtil.vBlanks.Count)
            {
                content += (String)(MiscUtil.vBlanks[balnkLen]);
            }
            return content;
        }
        //将指定的整数数值格式化成固定的位数，如果不够则前面增加0
        public static string intToFixLenString(int value,int count)
        {
            String s = value+"";
            String falg = "";
            if (value < 0)
            {
                falg = "-";
                count--;
                value = -value;
            }
            for (int i = 0; i < count; i++)
            {
                if (value >= 10)
                {
                    value /= 10;
                }
                else if (value >= 0)
                {
                    value = -1;
                }
                else
                {
                    s = "0" + s;
                }
            }
            return falg+s;
        }

        //获取不带后缀名的文件名称【截取以指定字符分割的前半部分字符,如果该字符为空，则截取第二段字符】
        public static String getPureFileName(String fileName)
        {
            int indexDot = fileName.IndexOf('.');
            String strVar = "";
            if (indexDot > 0)
            {
                strVar = fileName.Substring(0, indexDot);
            }
            else if (indexDot == 0 && strVar.Length > 1)
            {
                strVar = fileName.Split(new char[] { '.' })[1];
            }
            return strVar;
        }
        //--------------------------------其它函数--------------------------------
        //拷贝ArrayList
        public static void copyArrayList(ArrayList src, ArrayList dest)
        {
            if (dest == null || src == null)
            {
                return;
            }
            dest.Clear();
            for (int i = 0; i < src.Count; i++)
            {
                dest.Add(src[i]);
            }
        }

      /******************************************************************************************
        *
        *	printf (String infor)
        *
        *	输出信息
        */
        public static void print(String s)
        {
            Console.Write(s);
        }
        //将字符串转换成整型
        public static int stringToInt(String s)
        {
            int value = -1;
            try
            {
                value = Convert.ToInt32(s);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return value;
        }
        //将字符串转换成浮点型
        public static float stringToFloat(String s)
        {
            float value = 0.0F;
            try
            {
                value = Convert.ToSingle(s);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return value;
        }
        //获得当前时间
        public static long GetCurrTime()
        {
            return DateTime.Now.ToFileTime();
        }
        //将浮点型转换成整型(格式转换)
        public static int floatToInt(float data)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(data), 0);  
        }
        //将整型转换成浮点型(根式转换)
        public static float intToFloat(int data)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(data), 0);
        }
        /***********************************************控件的重绘控制***********************************************/
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB; 

        //停止控件的重绘 
        public static void BeginPaint(Control control)
        {
            if (control == null || !control.CanFocus)
            {
                return;
            }
            SendMessage(control.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
        }

        //允许控件重绘. 
        public static void EndPaint(Control control)
        {
            if (control == null || !control.CanFocus)
            {
                return;
            }
            SendMessage(control.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            control.Refresh();
        }
        /***********************************************滚动条位置设置***********************************************/
        [DllImport("user32.dll")]
        private static extern int GetScrollPos(IntPtr hwnd, int nbar);
        [DllImport("user32.dll", EntryPoint = "SetScrollPos")]
        private static extern int SetScrollPos(IntPtr hwnd, int nbar, int nPos,int bRedraw);
        //获取滚动条位置
        public static int getScrollPos(Control control)
        {
            return GetScrollPos(control.Handle, 1);
        }
        //设置滚动条位置
        public static int setScrollPos(Control control,int pos)
        {
            return SetScrollPos(control.Handle, 1, pos, 1);
        }
        /***************************************************获得控件的顶级父类**************************************************/
        public static Control getTopParent(Control control)
        {
            if (control == null)
            {
                return null;
            }
            while (control.Parent != null)
            {
                control = control.Parent;
            }
            return control;
        }
        /***************************************************其它功能**************************************************/
        //得到字符大小
        public static Size getStringSize(Graphics g, String measureString, Font stringFont)
        {

            if (measureString == null || measureString.Equals(""))
            {
                return new Size(0, 0);
            }
            SizeF fontSize = g.MeasureString(measureString, stringFont);
            Size size = new Size((int)fontSize.Width, (int)fontSize.Height);
            return size;
        }


    }
}
