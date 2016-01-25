using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Net;
using System.Management;
using System.Security.Cryptography;
using System.Diagnostics;
using Cyclone.mod.util;


namespace Cyclone.alg.util
{
    class Lisence
    {
        //用户使用期限
        public static int timeToYear = 2015;//截止年
        public static int timeToMonth = 10;//截止月
        public static int timeToDay = 2;//截止日
        public static int restrictedNbDay = 3;//受限时间（超出则将禁止）
        public static int userOverDay = 1;//用户使用过期时间
        //用户企业ID
        public static int CDKey_Try = 0;    //试用版，不能导出资源,有网络时间和本地时间限制和key验证
        public static int CDKey_Release = 1;//正式版，可以导出资源,有网络时间和本地时间限制和key验证
        public static int CDKey_Develop = 2;//开发版，可以导出资源,本地时间限制、计算机描述或Key验证，导出时有网络时间限制
        public static int currentLiscence = CDKey_Develop;

        //用户CDKEY集合
        private static String[][] keys =
        {
              new String[]//我的公司
             {
                "02e457f315c3f21441d2e81edcddf7c0",//SIYN001
                "da0eb353679ce57e296625fa6bb4e60d",//SIYN002
                "464baf48c3059e3299393492d8d395b1",//SIYN003
                "60c1be1ab52426771b447e3e22f68a57",//SIYN003
                "9bf34a7975f161b7a4f6fc5834f93843",//陈悦家里

             },
             new String[]//家里电脑
             {
                "cd04a2dd62191c7a7ec4f62d4ef42c60",
                "fbcc2ec4e842982048f4ce18beacc47c"
             },
             new String[]//南京视趣
             {
                 "e076715f37c789d1ba50c55a4e21ab48",
                 "3e229aef98487685ef27f1ad75ee406c",
                 "31129730b93579264daf1dfd751499ff",
                 "87cb4334e53075c6a6f7489a891ea969",
                 "0be891c4a772d8c4ea395045a63c0f2c",
                 "f6ef147becf59fbad5f771f2f339bd1a",
                 "17963ca39765fa56a18900e4c134a586",
                 "ea8d27a365c503af390799eba45e08a7",
                 "c10a6ef7bd93d5e16c88b355493c3568",
                 "03c4990afc000e8a25be48897fe2751a",
                 "7c029cb68c3a352c4075d943ead67dab",
             },
             new String[]//其它
             { 
                 "e60bb357ff7df47a15df81f4a8946cd5",//彭飞
                 "084f9c85cc0d949dc413859ed26553d6",
             }

        };
        //设置主窗口
        private static Form form_main = null;
        public static void setForm(Form main)
        {
            form_main = main;
        }
        //检测许可
        public static void checkLisense()
        {
            Boolean success = false;
            DateTime dtCurrent = DateTime.Now;
            DateTime dtRestricted = new DateTime(timeToYear, timeToMonth, timeToDay, 0, 0, 0);
            TimeSpan ts = dtCurrent - dtRestricted;
            int overDays = ts.Days;
            if (overDays > 0)
            {
                MessageBox.Show("您的版本已过期，请更换新版本！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                closeApp();
                return;
            }
            if (currentLiscence != CDKey_Develop)
            {
                checkNetworkTime();
            }
            success = true;
            //if (currentLiscence==CDKey_Try|| inAllowedMacList() )
            //{
            //    success = true;
            //}
            if (!success)
            {
                MessageBox.Show("非法使用者，软件即将关闭。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                closeApp();
                return;
            }
        }
        //检测是否在允许的KEY表内
        private static bool inAllowedMacList()
        {
            String key = getCDKey();
            for (int i = 0; i < keys.Length; i++)
            {
                for (int j = 0; j < keys[i].Length; j++)
                {
                    if (keys[i][j].Equals(key))
                    {
                        return true;
                    }
                }
            }
            SmallDialog_ShowMessage.showMessage("会员认证错误", "key:" + key);
            return false;
        }

        //得到本机的MAC
        private static string GetMacAddressBak()
        {
            string MAC = "";
            ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection MOC = MC.GetInstances();
            foreach (ManagementObject moc in MOC)
            {
                if (moc["IPEnabled"].ToString() == "True")
                {
                    MAC = moc["MacAddress"].ToString();
                    Console.WriteLine("mac:" + MAC);
                    break;
                }
            }
            MAC = MAC.Replace(':', '-');
            return MAC;
        }
        [DllImport("ws2_32.dll")]
        private static extern int inet_addr(string cp);
        [DllImport("IPHLPAPI.dll")]
        private static extern int SendARP(Int32 DestIP, Int32 SrcIP, ref Int64 pMacAddr, ref Int32 PhyAddrLen);
        //获取远程IP（不能跨网段）的MAC地址
        private static string GetMacAddress()
        {
            String hostip = null;
            if (Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length > 0)
            {
                hostip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            }
            string Mac = "";
            try
            {
                Int32 ldest = inet_addr(hostip);
                Int64 macinfo = new Int64();
                Int32 len = 6;
                SendARP(ldest, 0, ref macinfo, ref len);
                string TmpMac = Convert.ToString(macinfo, 16).PadLeft(12, '0');//转换成16进制　　注意有些没有十二位
                Mac = TmpMac.Substring(0, 2).ToUpper();//
                Boolean allZero = true;
                for (int i = 2; i < TmpMac.Length; i = i + 2)
                {
                    String doubleChar = TmpMac.Substring(i, 2).ToUpper();
                    Mac = doubleChar + "-" + Mac;
                    if (!doubleChar.Equals("00"))
                    {
                        allZero = false;
                    }
                }
                if (allZero)
                {
                    Mac = GetMacAddressBak();
                }
            }
            catch (Exception Mye)
            {
                Console.WriteLine("获取远程主机的MAC错误：" + Mye.Message);
            }
            return Mac;
        }
        //得到cpu序列号
        private static String GetCPUInfo()
        {
            string cpuInfo = "";
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }
            return cpuInfo;
        }
        //得到硬盘序列号
        private static String GetHardDisc()
        {
            String HDid = "";   //获取硬盘ID  
            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDid = (string)mo.Properties["Model"].Value;
                if (!HDid.ToUpper().Contains("USB"))
                {
                    break;
                }
            }
            return HDid;
        }

        //获得域名
        public static String getDomain()
        {
            return System.Environment.UserDomainName;
        }
        //获得用户名
        public static String getUserName()
        {
            return System.Environment.UserName;
        }
        //获得计算机描述
        public static String getComputerDetail()
        {
            IPHostEntry hostInfo = Dns.GetHostByName(Dns.GetHostName());
            return hostInfo.HostName;
        }
        //获得IP
        public static void getIP()
        {
            String commonIP = null;
            String localIP = null;
            System.Net.IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            if (addressList.Length > 1)
            {
                commonIP = addressList[0].ToString();
                localIP = addressList[1].ToString();
            }
            else
            {
                commonIP = addressList[0].ToString();
                localIP = "Break the line...";
            }
        }
        //得到CDKeyInfor
        public static String getCDKeyInfor()
        {
            String macAddress = GetMacAddress();
            if (macAddress == null || macAddress.Length <= 0)
            {
                return null;
            }
            String cpuAddress = GetCPUInfo();
            if (cpuAddress == null || cpuAddress.Length <= 0)
            {
                return null;
            }
            String hdAddress = GetHardDisc();
            if (hdAddress == null || hdAddress.Length <= 0)
            {
                return null;
            }
            String s = "";
            s += ("MacAddress:" + macAddress + "\n");
            s += ("CpuAddress:" + cpuAddress + "\n");
            s += ("Hard disc:" + hdAddress + "\n");
            return s;
        }
        //得到CDKey
        public static String getCDKey()
        {
            String macAddress = GetMacAddress();
            Console.WriteLine("macAddress:" + macAddress);
            if (macAddress == null || macAddress.Length <= 0)
            {
                return null;
            }
            String cpuAddress = GetCPUInfo();
            Console.WriteLine("cpuAddress:" + cpuAddress);
            if (cpuAddress == null || cpuAddress.Length <= 0)
            {
                return null;
            }
            String hdAddress = GetHardDisc();
            Console.WriteLine("HardDisc:" + hdAddress);
            if (hdAddress == null || hdAddress.Length <= 0)
            {
                return null;
            }
            String s = macAddress + cpuAddress + hdAddress;
            s = s.Replace("-", "");
            s = s.Replace(" ", "");
            s = s.Replace(":", "");
            s = s.ToLower();
            s = Md532(s);
            return s;
        }
        //MD5 算法
        public static string Md532(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("x2");
            }
            return pwd;
        }
        //获取网络时间
        private static byte CONNECT_NULL = 0;      //无连接
        private static byte CONNECT_CONNECTING = 1;//连接中
        private static byte CONNECT_SUCCESS = 2;   //连接成功
        private static byte CONNECT_FAIL = 3;      //连接失败
        private static byte CONNECT_TIMEOUT = 4;   //连接超时

        private static byte connectState = CONNECT_NULL;  //连接状态
        private static byte[] bytes = new byte[1024];     //读取的数据
        private static int bytesRead = 0;
        private static String hostName = "";
        private static int portNum = 13;
        private static void connectHost()
        {
            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
            System.Net.Sockets.NetworkStream ns = null;
            Console.WriteLine("尝试连接 主机： " + hostName);
            try
            {
                client.Connect(hostName, portNum);
                ns = client.GetStream();
                bytesRead = ns.Read(bytes, 0, bytes.Length);

                string strData = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
                string[] strSplittedData = strData.Split(new char[] { ' ' });
                if (strSplittedData.Length < 3)
                {
                    setConnectState(CONNECT_FAIL);
                    Console.WriteLine(strSplittedData[0]);
                }
                else
                {
                    setConnectState(CONNECT_SUCCESS);
                }

            }
            catch (Exception e)
            {
                setConnectState(CONNECT_FAIL);
                Console.WriteLine("连接错误：" + e.Message);
            }
            finally
            {
                try
                {
                    if (client != null)
                    {
                        client.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private static String[] strStates ={ "无连接", "连接中", "连接成功", "连接失败", "连接超时，重新连接" };
        private static void setConnectState(byte stateNow)
        {
            connectState = stateNow;
            if (connectState >= CONNECT_NULL && connectState <= CONNECT_TIMEOUT)
            {
                Console.WriteLine(strStates[connectState]);
            }
            else
            {
                Console.WriteLine("错误状态：" + connectState);
            }
        }
        //导出前网络时间检查
        private static bool softwareManagered = false;
        public static void checkNetworkTime()
        {
            if (softwareManagered)
            {
                return;
            }
            //返回国际标准时间
            //只使用的时间服务器的IP地址，未使用域名
            string[,] timeServers = new string[14, 2];//时间服务器
            int[] searchOrder = new int[] { 8, 9, 6, 5, 10, 0, 1, 7, 3, 11, 12, 2, 4, 13 };//搜索顺序
            timeServers[0, 0] = "time-a.nist.gov";
            timeServers[0, 1] = "129.6.15.28";
            timeServers[1, 0] = "time-b.nist.gov";
            timeServers[1, 1] = "129.6.15.29";
            timeServers[2, 0] = "time-a.timefreq.bldrdoc.gov";
            timeServers[2, 1] = "132.163.4.101";
            timeServers[3, 0] = "time-b.timefreq.bldrdoc.gov";
            timeServers[3, 1] = "132.163.4.102";
            timeServers[4, 0] = "time-c.timefreq.bldrdoc.gov";
            timeServers[4, 1] = "132.163.4.103";
            timeServers[5, 0] = "utcnist.colorado.edu";
            timeServers[5, 1] = "128.138.140.44";
            timeServers[6, 0] = "time.nist.gov";
            timeServers[6, 1] = "192.43.244.18";
            timeServers[7, 0] = "time-nw.nist.gov";
            timeServers[7, 1] = "131.107.1.10";
            timeServers[8, 0] = "nist1.symmetricom.com";
            timeServers[8, 1] = "69.25.96.13";
            timeServers[9, 0] = "nist1-dc.glassey.com";
            timeServers[9, 1] = "216.200.93.8";
            timeServers[10, 0] = "nist1-ny.glassey.com";
            timeServers[10, 1] = "208.184.49.9";
            timeServers[11, 0] = "nist1-sj.glassey.com";
            timeServers[11, 1] = "207.126.98.204";
            timeServers[12, 0] = "nist1.aol-ca.truetime.com";
            timeServers[12, 1] = "207.200.81.113";
            timeServers[13, 0] = "nist1.aol-va.truetime.com";
            timeServers[13, 1] = "64.236.96.53";
            for (int i = 0; i < 14; i++)
            {
                hostName = timeServers[searchOrder[i], 0];
                if (connectState != CONNECT_SUCCESS)
                {
                    setConnectState(CONNECT_CONNECTING);
                    Thread connectThread = new Thread(new ThreadStart(connectHost));
                    connectThread.Start();
                    int sleepTime = 0;
                    while (connectState == CONNECT_CONNECTING)
                    {
                        Thread.Sleep(10);
                        sleepTime++;
                        if (sleepTime > 500)
                        {
                            if (connectThread != null && connectThread.IsAlive)
                            {
                                connectThread.Abort();
                                setConnectState(CONNECT_TIMEOUT);
                            }
                            break;
                        }
                        Console.WriteLine("等待中...");
                    }
                    if (connectThread != null && connectThread.IsAlive)
                    {
                        connectThread.Abort();
                    }
                    connectThread = null;
                }
                if (connectState == CONNECT_SUCCESS)
                {
                    break;
                }

            }
            if (connectState == CONNECT_SUCCESS)
            {
                System.DateTime dtCurrent = new DateTime();
                string strData = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
                string[] strSplittedData;
                strSplittedData = strData.Split(new char[] { ' ' });
                if (strSplittedData.Length < 3)
                {
                    MessageBox.Show("网络验证失败，请检查网络！");
                    closeApp();
                }
                else
                {
                    dtCurrent = System.DateTime.Parse(strSplittedData[1] + " " + strSplittedData[2]);//得到标准时间
                    dtCurrent = dtCurrent.AddHours(8);//得到北京时间
                    DateTime dtRestricted = new DateTime(timeToYear, timeToMonth, timeToDay, 0, 0, 0);
                    TimeSpan ts = dtCurrent - dtRestricted;
                    userOverDay = ts.Days;
                    //Console.WriteLine("Consts.userNbDay:" + Consts.userNbDay);
                    //Console.WriteLine(dtCurrent.Year + "," + dtCurrent.Month + "," + dtCurrent.Day + "userNbDay:" + Consts.userNbDay);
                    if (userOverDay > 0)
                    {
                        if (userOverDay < restrictedNbDay)
                        {
                            MessageBox.Show("您的版本已过期，部分功能受限！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("您的版本已过期，请更换新版本！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            closeApp();
                        }
                    }
                    softwareManagered = true;
                }
                Console.WriteLine("time:" + dtCurrent.TimeOfDay);
            }
            else
            {
                MessageBox.Show("网络连接失败，请检查网络！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                closeApp();
            }

        }
        //关闭应用程序
        private static void closeApp()
        {
            if (form_main != null)
            {
                form_main.Close();
                form_main.Dispose();
            }
            Process.GetCurrentProcess().Kill();
        }

    }

}
