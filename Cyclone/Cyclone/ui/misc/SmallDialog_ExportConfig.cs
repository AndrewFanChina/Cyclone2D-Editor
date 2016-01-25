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
using System.IO;
using System.Collections;
using Cyclone.mod.imgage;
using Cyclone.alg.util;
using Cyclone.alg.math;

namespace Cyclone.mod.misc
{
    public partial class SmallDialog_ExportConfig : Form, ShowString
    {
        Form_Main mainForm = null;
        public SmallDialog_ExportConfig(Form_Main mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        public void initDialog()
        {
            noEvent = true;
            checkBox_confuseImg.Checked = Consts.exp_confuseImgs;
            checkBox_SplitAnimation.Checked = Consts.exp_splitAnimation;

            checkBox_ActionOffset.Checked = Consts.exp_ActionOffset;

            comboBox_animFormat.SelectedIndex = 0;
            if (Consts.exp_ImgFormat_Anim>= 0&& Consts.exp_ImgFormat_Anim < comboBox_animFormat.Items.Count)
            {
                comboBox_animFormat.SelectedIndex = Consts.exp_ImgFormat_Anim;
            }
            comboBox_ActionOffset.SelectedIndex = Consts.exp_ActionOffsetType;

            checkBox_CompileScrpts.Checked = Consts.exp_copileScripts;
            noEvent = false;
        }
        public void showString(String s)
        {
            //richTextBox_export.Text += s + "\n";
            richTextBox_export.AppendText(s + "\n");
            richTextBox_export.Select(richTextBox_export.TextLength - 1, 0);
            richTextBox_export.ScrollToCaret();
        }
        public void setStep(int step, int stepAll)
        {
            if (stepAll <= 0)
            {
                return;
            }
            int newValue=progressBar_Export.Minimum + (progressBar_Export.Maximum - progressBar_Export.Minimum) * step / stepAll;
            if (progressBar_Export.Value != newValue)
            {
                progressBar_Export.Value = newValue;
                this.Refresh();
            }
        }
        //��ť�¼���Ӧ
        private void button_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button_OK_Click(object sender, EventArgs e)
        {

            //����ϴα���
            richTextBox_export.Clear();
            //���Ʋ���
            this.button_Cancle.Enabled = false;
            this.button_OK.Enabled = false;
            this.ControlBox = false;
            this.Refresh();
            //����·��
            Consts.exportFolder = Consts.PATH_PROJECT_FOLDER + "export\\";
            Consts.exportC2DBinFolder = Consts.exportFolder + "cyclone2d\\";
            Consts.exportOhterBinFolder = null;
            Consts.exportFileName = System.IO.Path.GetFileNameWithoutExtension(Consts.PATH_PROJECT_FILENAME);
            Consts.exportFilePath = Consts.exportC2DBinFolder + Consts.exportFileName + ".bin";
            //���������ļ��У������Ҫ�Ļ�
            if (!System.IO.Directory.Exists(Consts.exportFolder))
            {
                System.IO.Directory.CreateDirectory(Consts.exportFolder);
            }
            //����c2d������Դ�ļ��У������Ҫ�Ļ�
            if (!System.IO.Directory.Exists(Consts.exportC2DBinFolder))
            {
                System.IO.Directory.CreateDirectory(Consts.exportC2DBinFolder);
            }
            //������������������Դ�ļ��У������Ҫ�Ļ�
            if (Consts.exportOhterBinFolder != null && !System.IO.Directory.Exists(Consts.exportOhterBinFolder))
            {
                System.IO.Directory.CreateDirectory(Consts.exportOhterBinFolder);
            }
            //��ʼ����
            {
                showString("=====================��鵼��Ŀ¼=====================");
                String subFolderName = Consts.exportFolder + Consts.SUBPARH_IMG;
                if (!Directory.Exists(subFolderName))
                {
                    Directory.CreateDirectory(subFolderName);
                }
                showString("=====================������Դ����=====================");
                mainForm.resExport_Animation(subFolderName, comboBox_animFormat.Text, this);
                showString("=====================��ͼ��Դ����=====================");
                mainForm.resExport_Map(subFolderName, comboBox_animFormat.Text, this);
                showString("=====================����������Դ=====================");
                UserDoc.exportUserData(Consts.exportFilePath, mainForm);
                if (Consts.exp_copileScripts)
                {
                    showString("===================��ʼ����ȫ���ű�===================");
                    mainForm.compileScripts(this);
                }
                if (Consts.exp_confuseImgs)
                {
                    showString("=====================����ͼƬ��Դ=====================");
                    String[] files = IOUtil.listFiles(subFolderName, "*.png|*.gif|*.bmp");
                    for (int i = 0; i < files.Length; i++)
                    {
                        Form_Image_Compress.confuseFile(files[i], files[i], files[i]);
                        setStep(i + 1, files.Length);
                    }
                }
                //if (Consts.exp_byOtherEngineFormat)
                //{
                //    showString("=====================��ʼ��Դ���=====================");
                //    String strFolderName = filePath + Consts.SUBPARH_IMG;
                //    Form_PackFiles.packFiles(this, strFolderName, "*.png|*.gif|*.bmp", Consts.exp_otherEngineFormat * 1024, true);
                //}
                showString("=====================��Դ�������=====================");
            }
            //�����������
            this.button_Cancle.Enabled = true;
            this.button_OK.Enabled = true;
            this.ControlBox = true;
            this.Refresh();

        }
        //��ѡ��ť�¼���Ӧ
        private bool noEvent = false;

        private void checkBox_confuseImg_CheckedChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            Consts.exp_confuseImgs = checkBox_confuseImg.Checked;
        }
        private void checkBox_SplitAnimation_CheckedChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            Consts.exp_splitAnimation = ((CheckBox)sender).Checked;
        }

        private void comboBox_animFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            Consts.exp_ImgFormat_Anim = ((ComboBox)sender).SelectedIndex;
        }

        private void comboBox_tileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            Consts.exp_ImgFormat_Map = ((ComboBox)sender).SelectedIndex;
        }




        private void checkBox_ActionOffset_CheckedChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            Consts.exp_ActionOffset = checkBox_ActionOffset.Checked;
        }

        private void comboBox_ActionOffset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            Consts.exp_ActionOffsetType = ((ComboBox)sender).SelectedIndex;
        }

        private void NUD_packSize_ValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
        }

        private void checkBox_CompileScrpts_CheckedChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            Consts.exp_copileScripts = checkBox_CompileScrpts.Checked;
        }

        private void CB_EngineFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}