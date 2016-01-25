using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.alg;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using Cyclone.alg.type;
using Cyclone.alg.util;

namespace Cyclone.mod
{
    public class UserDoc
    {
        private Form_Main mainForm = null;
        //从用户文档创建数据资源(如果文档不存在，则创建空数据资源)
        public UserDoc(Form_Main mainFormP)
        {
            mainForm = mainFormP;
        }
        public void initRegisterData()
        {
            //注册软件信息
            //FileTypeRegister.RegisterFileType(new FileTypeRegInfo(".ks", "KhaosStageEditor project file", Consts.PATH_EXE, Consts.PATH_EXE));
        }

        //==========串行化输入与输出======================================

        //初始化用户数据,读取所有串行对象
        public void initUserData(String filePath)
        {
            //if (form.Validate())
            //{
            //    Console.WriteLine("==");
            //}
            if (filePath == null)
            {
                return;
            }
            FileStream fs = null;
            //先读取版本号
            try
            {
                fs = File.Open(filePath, FileMode.OpenOrCreate);
                Consts.userFileVersion = IOUtil.readInt(fs);
                if (Consts.userFileVersion > Consts.softWareVersion)
                {
                    MessageBox.Show("您的软件版本太低，不能打开高版本文档，否则可能破坏数据。","警告",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    mainForm.Close();
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (fs != null)
                {
                    try
                    {
                        fs.Flush();
                        fs.Close();
                        fs = null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            //再读取文档信息
            try
            {
                fs = File.Open(filePath, FileMode.OpenOrCreate);
                IOUtil.readInt(fs);//跳过版本号
                mainForm.ReadObject(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine("============error in initUserData:" + e.Message + "============");
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("在读取过程中发生异常，软件自动关闭", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mainForm.Close();
                return;
            }
            finally
            {
                if (fs != null)
                {
                    try
                    {
                        fs.Flush();
                        fs.Close();
                        fs = null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        }
        //保存用户数据,输出所有串行对象
        public bool saveUserData()
        {
            return saveUserData(Consts.PATH_PROJECT_FILEPATH, mainForm);
        }
        public bool saveUserData(String filePath, SerializeAble SAobj)
        {
            if (filePath == null)
            {
                return false;
            }
            FileStream fs = null;
            String strFilePathTemp = filePath + ".temp";
            bool exceptionHappend = false;
            String excetionInSave = "";
            try
            {
                if (File.Exists(strFilePathTemp))
                {
                    fs = File.Open(strFilePathTemp, FileMode.Truncate);
                }
                else
                {
                    fs = File.Open(strFilePathTemp, FileMode.OpenOrCreate);
                }
                IOUtil.writeInt(fs, Consts.softWareVersion);//保存版本信息
                SAobj.WriteObject(fs);
            }
            catch (Exception e)
            {
                exceptionHappend = true;
                excetionInSave = e.Message;
                Console.WriteLine(e.StackTrace);

            }
            finally
            {
                if (fs != null)
                {
                    try
                    {
                        fs.Flush();
                        fs.Close();
                        fs = null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            if (exceptionHappend)
            {
                MessageBox.Show("保存失败，保存过程中发生了异常【" + excetionInSave + "】！", "警报", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            IOUtil.Copy(strFilePathTemp, filePath, true);
            //生成备份
            String strFolderBak = filePath.Substring(0, filePath.LastIndexOf('\\'));
            String fileName = filePath.Substring(filePath.LastIndexOf('\\'), filePath.Length - strFolderBak.Length);
            strFolderBak += "\\BAKS";
            if (!Directory.Exists(strFolderBak))
            {
                Directory.CreateDirectory(strFolderBak);
            }
            fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
            //系统时间
            String timeString = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            fileName = strFolderBak + fileName + "_" + timeString + ".bak";
            IOUtil.Copy(strFilePathTemp, fileName, true);
            File.Delete(strFilePathTemp);
            return true;
        }
        //导出用户数据,输出所有串行对象
        public static ArrayList ArrayTxts_Head = new ArrayList();
        public static ArrayList ArrayTxts_Java = new ArrayList();
        public static void exportUserData(String filePath_Bin,SerializeAble SAobj)
        {
            if (filePath_Bin == null)
            {
                return;
            }
            ArrayTxts_Head.Clear();
            ArrayTxts_Java.Clear();
            FileStream fs_bin = null;
            String resName = null;
            String name = null;
            try
            {
                if (File.Exists(filePath_Bin))
                {
                    fs_bin = File.Open(filePath_Bin, FileMode.Truncate);
                }
                else
                {
                    fs_bin = File.Open(filePath_Bin, FileMode.OpenOrCreate);
                }
                try
                {
                    resName = filePath_Bin.Substring(filePath_Bin.LastIndexOf('\\') + 1);
                    name = resName.Substring(0, resName.LastIndexOf('.'));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                if(resName == null)
                {
                    resName = "NULL";
                }
                if (name == null)
                {
                    name = "";
                }
                ArrayTxts_Head.Add("//=============================AnimationData=============================");
                ArrayTxts_Head.Add(" ");
                ArrayTxts_Head.Add("#define C2D_NAME_" + name + " \"" + resName + "\"");
                ArrayTxts_Head.Add(" ");
                ArrayTxts_Java.Add("//=============================AnimationData=============================");
                ArrayTxts_Java.Add(" ");
                ArrayTxts_Java.Add("public static final String C2D_NAME_" + name + " = \"" + resName + "\";");
                ArrayTxts_Java.Add(" ");
                SAobj.ExportObject(fs_bin);
            }
            catch (Exception e)
            {
                String err = "未知";
                if (e != null && e.Message != null)
                {
                    err = e.Message;
                }
                MessageBox.Show("导出数据过程中发生了异常：“" + err + "”", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (fs_bin != null)
                {
                    try
                    {
                        fs_bin.Flush();
                        fs_bin.Close();
                        fs_bin = null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            //处理Include文本----------------------------------------------------------------------
            saveConstTxt(ArrayTxts_Java, name, true);
            saveConstTxt(ArrayTxts_Head, name, false);
 
        }
        public static void saveConstTxt(ArrayList array,String name,Boolean javaOrHead)
        {
            FileStream fs_txt = null;
            int splitCount = 1000;
            int includeFileNumber = (array.Count + splitCount - 1) / splitCount;
            for (int i = 0; i < includeFileNumber; i++)
            {
                String filePath_Txt = null;
                String className = null;
                String subfix = javaOrHead ? ".java" : ".h";
                try
                {

                    if (i == 0)
                    {
                        className = "UserConsts_" + name;
                    }
                    else
                    {
                        className = "UserConsts_" + name + "_" + i;
                    }
                    filePath_Txt = Consts.exportFolder + className + subfix;
                    if (File.Exists(filePath_Txt))
                    {
                        fs_txt = File.Open(filePath_Txt, FileMode.Truncate);
                    }
                    else
                    {
                        fs_txt = File.Open(filePath_Txt, FileMode.OpenOrCreate);
                    }
                    if (!javaOrHead)
                    {
                        IOUtil.writeTextLineUTF8(fs_txt, "#ifndef cyclone2d_x_" + className +"_h");
                        IOUtil.writeTextLineUTF8(fs_txt, "#define cyclone2d_x_" + className + "_h");
                    }
                    else
                    {
                        IOUtil.writeTextLineUTF8(fs_txt, "package game.core;");
                        String strExtends = "";
                        if (i == 0)
                        {
                            strExtends = includeFileNumber > 1 ? " extends " : " ";
                            for (int j = 1; j < includeFileNumber; j++)
                            {
                                strExtends += "UserConsts_" + name + "_" + j;
                                if (j + 1 < includeFileNumber)
                                {
                                    strExtends += ",";
                                }
                            }
                        }
                        IOUtil.writeTextLineUTF8(fs_txt, "public interface " + className + strExtends);
                        IOUtil.writeTextLineUTF8(fs_txt, "{");
                    }

                    int from = i * splitCount;
                    int end = (i + 1) * splitCount - 1;
                    if (end >= array.Count)
                    {
                        end = array.Count - 1;
                    }
                    for (int j = from; j <= end; j++)
                    {
                        IOUtil.writeTextLineUTF8(fs_txt, (String)array[j]);
                    }
                    if (!javaOrHead)
                    {
                        IOUtil.writeTextLineUTF8(fs_txt, "#endif //cyclone2d_x_" + className + "_h");
                    }
                    else
                    {
                        IOUtil.writeTextLineGBK(fs_txt, "}");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (fs_txt != null)
                    {
                        try
                        {
                            fs_txt.Flush();
                            fs_txt.Close();
                            fs_txt = null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
        //================================================================
    }
}
