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

namespace Cyclone.mod.imgage
{
    public partial class Form_Image_Compress : Form,ShowString
    {
        public Form_Image_Compress()
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

        private void button_openDest_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox_PathDest.Text))
            {
                folderBrowserDialog.SelectedPath = textBox_PathDest.Text;
            }
            else
            {
                folderBrowserDialog.SelectedPath = null;
            }
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBox_PathDest.Text = folderBrowserDialog.SelectedPath;
            }
        }
        public void showString(String text)
        {
            this.richTextBox.Text += text + "\n";
        }

        private void button_excute_Click(object sender, EventArgs e)
        {
            String srcPath = this.textBox_PathSrc.Text;
            String destPath = this.textBox_PathDest.Text;
            if (!Directory.Exists(srcPath))
            {
                showString("源文件目录不存在，请重新选择。");
                return;
            }
            if (srcPath.Equals(destPath))
            {
                showString("目标目录不能与源目录相同，否则会覆盖目录。");
                return;
            }

            if (destPath == null || destPath.Equals(""))
            {
                destPath = srcPath + "\\压缩混淆";
                this.textBox_PathDest.Text = destPath;
                if (!Directory.Exists(destPath))
                {
                    Directory.CreateDirectory(destPath);
                }
            }
            else if (!Directory.Exists(destPath))
            {
                showString("目标目录不存在，请重新选择。");
                return;
            }
            //开始转换
            String[] files = IOUtil.listFiles(srcPath, "*.png|*.gif|*.bmp");
            showString("--------------共发现" + files.Length + "个图片--------------");
            for (int i = 0; i < files.Length; i++)
            {
                int pos = files[i].LastIndexOf('\\');
                String name = files[i].Substring(pos + 1, files[i].Length - (pos + 1));
                String newFilePath = destPath + "\\" + name;
                showString(">>>>>>>>>>拷贝" + name);
                IOUtil.Copy(files[i], newFilePath, true);
                //压缩
                if (this.checkBox_Compress.Checked)
                {
                    showString(">>>>>>>>>>压缩" + name);
                    IOUtil.OpenProcess(Consts.PATH_EXE_FOLDER + @"\tools\pngout.exe", " /c3 /q " + newFilePath, this, true);
                }
                //混淆
                if (this.checkBox_Confuse.Checked)
                {
                    showString("混淆" + name);
                    showString(confuseFile(newFilePath, newFilePath, name));
                }
            }
            
        }
        public static String confuseFile(String srcPath, String destPath,String name)
        {
            if (name == null)
            {
                name = destPath;
            }
            String s = "";
            try
            {
                byte[] content = File.ReadAllBytes(srcPath);
                //处理
                for (int j = 0; j < content.Length; j++)
                {
                    //int b = content[j] & 0x80;
                    //content[j] <<= 1;
                    //if (b > 0)
                    //{
                    //    content[j] |= 0x1;
                    //}
                    content[j] ^= 0xFF;
                }
                //存储
                File.WriteAllBytes(destPath, content);
                s = "导出混淆图片:" + name + "," + content.Length + "字节";
            }
            catch (Exception ex)
            {
                s ="ERROR:"+ex.Message;
            }
            return s;
        }
        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}