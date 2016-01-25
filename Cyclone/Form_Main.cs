//#define TryLisence
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Cyclone.mod;
using Cyclone.alg;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Drawing.Imaging;
using System.Threading;
using System.Collections;
using Cyclone.mod.menu;
using Cyclone.mod.anim;
using Cyclone.mod.script;
using Cyclone.mod.map;
using Cyclone.mod.image.pallete;
using Cyclone.mod.image.formatDeal;
using Cyclone.mod.misc;
using Cyclone.mod.prop;
using Cyclone.ui_script;
using Cyclone.mod.util;
using Cyclone.mod.imgage;
using Cyclone.alg.math;
using Cyclone.alg.type;
using Cyclone.alg.util;
using Cyclone.mod.animimg;
using Cyclone.alg.opengl;
using c2d.ui_misc;
namespace Cyclone
{
    public partial class Form_Main : Form,SerializeAble,IUndoHandler
    {
        //***************************************************************************************************************
        //**********                                                                                               ******
        //**********                                                                                               ******
        //**********                                  软件整体结构框架的构建                                       ******
        //**********                                                                                               ******
        //**********                                                                                               ******
        //***************************************************************************************************************
        public String path_file = null;
        public String path_folder = null;
        private UserDoc userDoc = null;
        //常量
        public static Graphics gFont = null;
        public Form_Main(String path)
        {
            InitializeComponent();
            path_file = path;
            path_folder = path_file.Substring(0, path_file.LastIndexOf('\\') + 1);
            if (form_Main == null)
            {
                form_Main = this;
            }
            if (gFont == null)
            {
                gFont = CreateGraphics();
            }
            initVectorEnviroment();
        }
        public void setStep(int percent, String loadText)
        {
            Console.WriteLine("set step:" + percent);
            if (TSPB_load != null && percent >= TSPB_load.Minimum && percent <= TSPB_load.Maximum)
            {
                TSPB_load.Value = percent;
                if (percent == 100)
                {
                    TSPB_load.Visible = false;
                }
                else if (!TSPB_load.Visible)
                {
                    TSPB_load.Visible = true;
                }
                this.showFunction(loadText);
                statusStrip_Main.Refresh();
            }
        }
        public void initWorld()
        {
            //tabControl_Center_L_T.SelectedIndex = 1;
            this.Enabled = false;
            this.Refresh();
            setStep(10, "正在载入数据");
            userDoc = new UserDoc(this);
            userDoc.initRegisterData();
            String domain = System.Environment.UserDomainName;
            //Lisence.setForm(this);
            //Lisence.checkLisense();
            setStep(20, "读入数据信息");
            initData(path_file);
            initUI();
            refreshTileSyleList();
            resetCurrentGfxContainer();
            refreshAntetypeSyleList();
            setStep(100, "欢迎使用");
            this.updateAllContainers();
            this.Enabled = true;
            this.Activate();
        }
        //初始化参数===========================================================
        public void initData(String path)
        {
            path_file = path;
            path_folder = path_file.Substring(0, path_file.LastIndexOf('\\') + 1);

            m_HistoryManager = new UndoManager();
            m_HistoryManager.MaxUndoLevel = 100;
            textsManager = new TextsManager(this);

            mapImagesManager = new MImgsManager(this);
            mapsManager = new MapsManager(this, mapImagesManager);

            varIntManager = new VarsManager(this);
            varStringManager = new VarsManager(this);
            triggerFunctionManager = new FunctionsManager(this);
            contextFunctionManager = new FunctionsManager(this);
            executionFunctionManager = new FunctionsManager(this);
            iDsManager = new VarsManager(this);
            propertyTypesManager = new PropertyTypesManager(this);

            form_MAnimation = new Form_MAnimation(this);
            mapsManager.initAntetype(form_MAnimation.form_MActorList.actorsManager);
            if (Consts.PATH_PROJECT_FILEPATH != null)
            {
                this.Text = Consts.PATH_PROJECT_FILEPATH;
            }
            userDoc.initUserData(path);//读入数据
        }
        private void initUI()
        {
            setStep(80, "主窗口容器刷新");
            this.pictureBox_Physics.MouseWheel += new MouseEventHandler(pictureBoxPhy_MouseWheel);
            this.setEditMode(EDITMOD_PENCIL);
            this.pictureBox_Gfx.MouseWheel += new MouseEventHandler(pictureBoxGfx_MouseWheel);
            this.pictureBox_AT.MouseWheel += new MouseEventHandler(pictureBoxAT_MouseWheel);
            historyDialog = new SmallDialog_History(this);
            setStep(85, "属性与数值窗口初始化");
            if (form_ProptiesManager == null)
            {
                form_ProptiesManager = new Form_ProptiesManager(this);
            }
            setStep(90, "变量窗口初始化");
            if (form_VarsManager == null)
            {
                form_VarsManager = new Form_VarsAndFunctions(this);
            }
            //初始化窗口
            form_MapImagesManager = new Form_MImgsList(mapImagesManager);
            form_MapImagesManager.Text = "地图图片管理器";
        }

        public bool saveUserData()
        {
            return userDoc.saveUserData();
        }
        //成员定义=============================================================
        private UndoManager m_HistoryManager;                        //历史记录管理   
        public TextsManager textsManager;                         //字符管理
        public MImgsManager mapImagesManager;                     //地图图片管理
        public MapsManager mapsManager;                           //地图数据管理器

        public VarsManager varIntManager;                         //脚本整型变量管理器
        public VarsManager varStringManager;                      //脚本字符变量管理器
        public VarsManager iDsManager;                            //常量ID管理器
        public FunctionsManager triggerFunctionManager;           //脚本触发函数管理器
        public FunctionsManager contextFunctionManager;           //脚本环境函数管理器
        public FunctionsManager executionFunctionManager;         //脚本执行函数管理器
        public PropertyTypesManager propertyTypesManager;         //属性类型管理器

        //窗体定义=============================================================
        public Form_Main form_Main = null;//自身
        public Form_MImgsList form_MapImagesManager = null;          //地图图片管理窗口
        public static Form_TextsManager form_TextsManager = null;    //文字管理窗口
        public SmallDialog_History historyDialog = null;           //历史记录窗口

        public Form_VarsAndFunctions form_VarsManager = null;         //脚本变量容器窗口
        public Form_ProptiesManager form_ProptiesManager = null;      //属性和数值编辑窗口

        //智能机版本窗体定义=======================================================
        public Form_MAnimation form_MAnimation = null;                //影片动画编辑
        //合并资源============================================================
        public void combineRes(Form_Main src_main,
            ArrayList actorsID, ArrayList anteTypesID, ArrayList mapsID, ArrayList triggersID, ArrayList propsID,
            bool withAnimImg, bool withMapImg, bool withFunctionVar, bool withConsts,
             bool withTexts, Form_CombineRes dialog)
        {
            if (actorsID.Count > 0||mapsID.Count>0 ||anteTypesID.Count>0|| withAnimImg)
            {
                dialog.showProcess("合并动画图片", 10);
                form_MAnimation.form_MImgsList.mImgsManager.combine(src_main.form_MAnimation.form_MImgsList.mImgsManager);
            }

            if (actorsID.Count > 0 || mapsID.Count > 0 || triggersID.Count > 0 || withTexts)
            {
                dialog.showProcess("合并文字信息", 20);
                textsManager.combine(src_main.textsManager);
            }

            if (actorsID.Count > 0 || mapsID.Count > 0 || anteTypesID.Count > 0 )
            {
                dialog.showProcess("合并动画切片数据", 40);
                form_MAnimation.form_MImgsList.MClipsManager.combine(src_main.form_MAnimation.form_MImgsList.MClipsManager);
                dialog.showProcess("合并动画帧数据", 50);
                form_MAnimation.form_MActorList.actorsManager.combine(src_main.form_MAnimation.form_MActorList.actorsManager, actorsID);
            }

            if (withMapImg || mapsID.Count>0)
            {
                dialog.showProcess("合并地图图片", 60);
                mapImagesManager.combine(src_main.mapImagesManager);
            }
            if (mapsID.Count > 0)
            {
                dialog.showProcess("合并地图关卡数据", 65);
                mapsManager.combine(src_main.mapsManager, mapsID, anteTypesID);
                refreshTileSyleList();
                setCurrentGfxContainer(mapsManager.tileGfxManager.Count() - 1);
            }
            if (triggersID.Count > 0 || propsID.Count > 0 || withConsts)
            {
                dialog.showProcess("合并脚本数据-常量表", 70);
                iDsManager.combine(src_main.iDsManager);
            }

            if (triggersID.Count > 0 || propsID.Count > 0 || withFunctionVar)
            {
                dialog.showProcess("合并脚本数据-变量表", 80);
                varIntManager.combine(src_main.varIntManager);
                varStringManager.combine(src_main.varStringManager);
            }

            if (triggersID.Count > 0 || propsID.Count > 0 || withFunctionVar)
            {
                dialog.showProcess("合并脚本数据-函数表", 85);
                triggerFunctionManager.combine(src_main.triggerFunctionManager);
                contextFunctionManager.combine(src_main.contextFunctionManager);
                executionFunctionManager.combine(src_main.executionFunctionManager);
            }
            if (propsID.Count > 0)
            {
                dialog.showProcess("合并脚本数据-属性表", 90);
                propertyTypesManager.combine(src_main.propertyTypesManager, propsID);
            }

            if (triggersID.Count > 0)
            {
                //dialog.showProcess("合并脚本数据-触发器表", 95);
                //triggersManager.combine(src_main.triggersManager, triggersID);
            }

            dialog.showProcess("刷新整个场景", 99);
            updateAllContainers();
            form_MAnimation.refreshActorUIs();
            dialog.showProcess("合并完成", 100);
        }
        public void releaseRes()
        {
            m_HistoryManager=null;
            textsManager = null;
            mapImagesManager = null;
            mapsManager = null;
            varIntManager = null;
            varStringManager = null;
            triggerFunctionManager = null;
            contextFunctionManager = null;
            executionFunctionManager = null;
            //triggersManager = null;
            propertyTypesManager = null;
        }
        public void releaseWindowsData()
        {
            if (form_TextsManager != null && !form_TextsManager.IsDisposed)
            {
                form_TextsManager.Close();
                form_TextsManager.Dispose();
                form_TextsManager.releaseRes();
                form_TextsManager = null;
            }
            if (form_MapImagesManager != null && !form_MapImagesManager.IsDisposed)
            {
                form_MapImagesManager.Close();
                form_MapImagesManager.Dispose();
                form_MapImagesManager.releaseRes();
                form_MapImagesManager = null;
            }
            if (form_VarsManager != null && !form_VarsManager.IsDisposed)
            {
                form_VarsManager.Close();
                form_VarsManager.Dispose();
                form_VarsManager.releaseRes();
                form_VarsManager = null;
            }
            if (form_ProptiesManager != null && !form_ProptiesManager.IsDisposed)
            {
                form_ProptiesManager.Close();
                form_ProptiesManager.Dispose();
                form_ProptiesManager = null;
            }
            if (form_MAnimation != null && !form_MAnimation.IsDisposed)
            {
                form_MAnimation.form_MAnimPW.stopPlayPrieviewBox();
                form_MAnimation.form_MFrameEdit.Dispose();
                form_MAnimation.form_MFrameLevel.Dispose();
                form_MAnimation.form_MConfig.Dispose();
                form_MAnimation.form_MCLib.Dispose();
                form_MAnimation.form_MImgsList.Dispose();
                form_MAnimation.form_MTimeLine.Dispose();
                form_MAnimation.Close();
                form_MAnimation.Dispose();
                form_MAnimation = null;
            }
            ArrayList arrayList = new ArrayList();
            int openCount = Application.OpenForms.Count;
            for (int i = 0; i < openCount;i++ )
            {
                arrayList.Add(Application.OpenForms[i]);
            }
            for (int i = 0; i < arrayList.Count; i++)
            {
                Form openForm = (Form)arrayList[i];
                if (!openForm.Equals(this))
                {
                    openForm.Close();
                    openForm.Dispose();
                }
            }

        }
        //串行化输入与输出====================================================
        #region SerializeAble Members

        public void ReadObject(System.IO.Stream s)
        {
            //读取配置信息-----------------------
                Consts.textureLinear = IOUtil.readBoolean(s);
                Consts.transferClipMode = IOUtil.readBoolean(s);
                Consts.exp_confuseImgs = IOUtil.readBoolean(s);
                IOUtil.readByte(s);
                Consts.exp_splitAnimation = IOUtil.readBoolean(s);
                Consts.exp_bakboolean = IOUtil.readBoolean(s);
                IOUtil.readInt(s);
                Consts.exp_ImgFormat_Anim = IOUtil.readInt(s);
                Consts.exp_ImgFormat_Map = IOUtil.readInt(s);
                Consts.exp_ActionOffset = IOUtil.readBoolean(s);
                Consts.exp_ActionOffsetType = IOUtil.readInt(s);
                Consts.exp_copileScripts = IOUtil.readBoolean(s);
            //other flag...

            //读取资源数据-----------------------
            setStep(30, "读取动画资源");
            form_MAnimation.ReadObject(s);
            setStep(40, "文字资源初始化");
            textsManager.ReadObject(s);
            setStep(50, "地图图片初始化");
            mapImagesManager.ReadObject(s);
            setStep(60, "地图数据初始化");
            mapsManager.ReadObject(s);
            setStep(70, "常量数据初始化");
            iDsManager.ReadObject(s);
            setStep(72, "变量数据初始化");
            varIntManager.ReadObject(s);
            varStringManager.ReadObject(s);
            setStep(74, "函数数据初始化");
            triggerFunctionManager.ReadObject(s);
            contextFunctionManager.ReadObject(s);
            executionFunctionManager.ReadObject(s);
            setStep(76, "属性数据初始化");
            propertyTypesManager.ReadObject(s);
        }

        public void WriteObject(System.IO.Stream s)
        {
            //写出配置信息-----------------------
            IOUtil.writeBoolean(s, Consts.textureLinear);
            IOUtil.writeBoolean(s, Consts.transferClipMode);
            //导出参数
            IOUtil.writeBoolean(s, Consts.exp_confuseImgs);
            IOUtil.writeByte(s, (byte)0);
            IOUtil.writeBoolean(s, Consts.exp_splitAnimation);
            IOUtil.writeBoolean(s, Consts.exp_bakboolean);
            IOUtil.writeInt(s, (int)0);
            IOUtil.writeInt(s, Consts.exp_ImgFormat_Anim);
            IOUtil.writeInt(s, Consts.exp_ImgFormat_Map);
            IOUtil.writeBoolean(s, Consts.exp_ActionOffset);
            IOUtil.writeInt(s, Consts.exp_ActionOffsetType);
            IOUtil.writeBoolean(s, Consts.exp_copileScripts);
            
            //other flag
            //写出资源数据-----------------------
            form_MAnimation.WriteObject(s);
            textsManager.WriteObject(s);
            mapImagesManager.WriteObject(s);
            mapsManager.WriteObject(s);

            iDsManager.WriteObject(s);
            varIntManager.WriteObject(s);
            varStringManager.WriteObject(s);
            triggerFunctionManager.WriteObject(s);
            contextFunctionManager.WriteObject(s);
            executionFunctionManager.WriteObject(s);
            propertyTypesManager.WriteObject(s);

        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
            //输出是否混淆图片信息
            IOUtil.writeBoolean(fs_bin, Consts.exp_confuseImgs);
            //导出动作位移类型
            IOUtil.writeByte(fs_bin,(byte)(Consts.exp_ActionOffset ? (Consts.exp_ActionOffsetType+1) :0));
            //分立动画数据开关
            IOUtil.writeBoolean(fs_bin, Consts.exp_splitAnimation);
            //导出动画资源数据
            animImgsManagerForExport.ExportObject(fs_bin); //动画图片表
            animClipsManagerForExport.ExportObject(fs_bin);//动画切片表
            animActorsManagerExport.ExportObject(fs_bin);  //动画动作数据
            //导出字符串列表
            textsManager.ExportObject(fs_bin);
            //导出角色原型信息
            mapsManagerForExport.antetypesManager.ExportObject(fs_bin);
            //导出地图资源数据
            mapsManagerForExport.readyForExportScript();
            mapsManagerForExport.ExportObject(fs_bin);
            //导出脚本数据
            exportScript();
            //导出属性和常量数据
            exportPropAndConsts();
        }
        //导出脚本搭载数据
        private void exportScript()
        {
            String filePath_Script = Consts.exportC2DBinFolder + Consts.exportFileName + "_script.bin";
            FileStream fs_script = null;
            try
            {
                if (File.Exists(filePath_Script))
                {
                    fs_script = File.Open(filePath_Script, FileMode.Truncate);
                }
                else
                {
                    fs_script = File.Open(filePath_Script, FileMode.OpenOrCreate);
                }
                UserDoc.ArrayTxts_Head.Add("//=============================ScriptData=============================");
                UserDoc.ArrayTxts_Java.Add("//=============================ScriptData=============================");
                //导出整形和字符型变量-----------------------
                VarsManager.inExportVARTable = true;
                varIntManager.ExportObject(fs_script);
                varStringManager.ExportObject(fs_script);
                VarsManager.inExportVARTable = false;
                //使用全局ID导出所有函数---------------------
                int functionCount = triggerFunctionManager.getElementCount() + contextFunctionManager.getElementCount() + executionFunctionManager.getElementCount();
                IOUtil.writeShort(fs_script, (short)functionCount);
                FunctionElement.FunctionGlobleID = 0;
                triggerFunctionManager.ExportObject(fs_script);
                contextFunctionManager.ExportObject(fs_script);
                executionFunctionManager.ExportObject(fs_script);
                //输出脚本文件名称列表
                short kssfileCount = (short)mapsManagerForExport.listExpScriptFiles.Count;
                IOUtil.writeShort(fs_script, kssfileCount);
                for (int i = 0; i < mapsManagerForExport.listExpScriptFiles.Count; i++)
                {
                    String file = (String)mapsManagerForExport.listExpScriptFiles[i];
                    file = file.Substring(0, file.LastIndexOf('.'));
                    IOUtil.writeString(fs_script, file);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("导出过程中发生了异常:" + e.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (fs_script != null)
                {
                    try
                    {
                        fs_script.Flush();
                        fs_script.Close();
                        fs_script = null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        //导出属性和常量数据
        private void exportPropAndConsts()
        {
            //导出属性数据
            String filePath_Script = Consts.exportC2DBinFolder + Consts.exportFileName + "_prop.bin";
            FileStream fs_script = null;
            try
            {
                if (File.Exists(filePath_Script))
                {
                    fs_script = File.Open(filePath_Script, FileMode.Truncate);
                }
                else
                {
                    fs_script = File.Open(filePath_Script, FileMode.OpenOrCreate);
                }
                //array_txt.Add("/*=============================属性数据=============================*/");
                propertyTypesManager.ExportObject(fs_script);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (fs_script != null)
                {
                    try
                    {
                        fs_script.Flush();
                        fs_script.Close();
                        fs_script = null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            UserDoc.ArrayTxts_Head.Add("//=============================ConstsData=============================");
            iDsManager.ExportObject(fs_script);//输出常量文本
        }
        #endregion
        //动画资源导出处理========================================================
        public MClipsManager animClipsManagerForExport;
        public MImgsManager animImgsManagerForExport;
        public MActorsManager animActorsManagerExport;
        public void resExport_Animation(String subFolderName, String strFormat, SmallDialog_ExportConfig dialogRes)
        {
            //克隆会改变的文档数据
            animClipsManagerForExport = form_MAnimation.form_MImgsList.MClipsManager.CloneForExport();
            animImgsManagerForExport = form_MAnimation.form_MImgsList.mImgsManager.Clone();
            animActorsManagerExport = form_MAnimation.form_MActorList.actorsManager.Clone();
            //关联引用(动画->切块)
            MActorsManager MAM = animActorsManagerExport;     
            for (int i1 = 0; i1 < MAM.Count(); i1++)
            {
                for (int i2 = 0; i2 < MAM[i1].Count(); i2++)
                {
                    for (int i3 = 0; i3 < MAM[i1][i2].Count(); i3++)
                    {
                        for (int i4 = 0; i4 < MAM[i1][i2][i3].Count(); i4++)
                        {
                            for (int i5 = 0; i5 < MAM[i1][i2][i3][i4].Count(); i5++)
                            {
                                for (int i6 = 0; i6 < MAM[i1][i2][i3][i4][i5].Count(); i6++)
                                {
                                    MFrameUnit unit = MAM[i1][i2][i3][i4][i5][i6];
                                    if (unit is MFrameUnit_Bitmap)
                                    {
                                        MFrameUnit_Bitmap unitBitmap = (MFrameUnit_Bitmap)unit;
                                        unitBitmap.clipElement = animClipsManagerForExport[unitBitmap.clipElement.GetID()];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //关联引用(切块->图片)
            for (int i=0;i<animClipsManagerForExport.Count();i++)
            {
                MClipElement clip = animClipsManagerForExport[i];
                clip.imageElement = animImgsManagerForExport[clip.imageElement.GetID()];
            }
            //清除冗余
            animClipsManagerForExport.ClearSpilth(false);
            //新生成的图片组
            MImgsManager mImgsManagerGenerated = new MImgsManager();
            //获取链接分组列表
            List<List<MImgElement>> allGroups = animImgsManagerForExport.getAllUsedGroups();
            //处理优化分组
            for (int i = 0; i < allGroups.Count - 1; i++)
            {
                List<MImgElement> group = allGroups[i];
                Size needSize;
                int usedSpace;
                Image imgExort;
                MImgElement imgElement = MImgsManager.optimizeClips(group, out needSize, out usedSpace, out imgExort, true);
                mImgsManagerGenerated.Add(imgElement);
                SaveGenImage(strFormat, subFolderName, imgElement);
            }
            //处理禁止优化分组
            List<MImgElement> groupForbidOpt = allGroups[allGroups.Count-1];
            foreach (MImgElement imgElement in groupForbidOpt)
            {
                mImgsManagerGenerated.Add(imgElement);
                imgElement.image = GraphicsUtil.getMatchImage((Bitmap)imgElement.image);
                SaveGenImage(strFormat, subFolderName, imgElement);
            }
            //变更图片管理器
            animImgsManagerForExport = mImgsManagerGenerated;
            dialogRes.setStep(1, 1);
        }
        //生成处理后的图片，此图片在图片对象newImgElement中，并且当保存完成之后会被销毁
        private void SaveGenImage(String strFormat, String subFolderName, MImgElement newImgElement)
        {
            Image imgExort = newImgElement.image;
            //bmp图片添加背景色
            if (strFormat.Equals("bmp") && imgExort != null)
            {
                Bitmap imgTemp = new Bitmap(imgExort.Width, imgExort.Height, PixelFormat.Format32bppArgb);
                Graphics gTemp = Graphics.FromImage(imgTemp);
                GraphicsUtil.fillRect(gTemp, 0, 0, imgExort.Width, imgExort.Height, 0xFF00FF);
                GraphicsUtil.drawClip(gTemp, imgExort, 0, 0, 0, 0, imgExort.Width, imgExort.Height, 0);
                imgExort.Dispose();
                gTemp.Dispose();
                imgExort = imgTemp;
            }
            //保存图片
            String newName = subFolderName + MiscUtil.getPureFileName(newImgElement.name) + "." + strFormat;
            if (imgExort != null)
            {
                imgExort.Save(newName);
            }
            else
            {
                MessageBox.Show("导出图片" + newImgElement.name + "时发生错误，请检查源图格式！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            newImgElement.exportSize = imgExort.Size;
            imgExort.Dispose();
            newImgElement.image = null;
        }
        //地图资源导出处理========================================================
        public MapsManager mapsManagerForExport;           //地图数据管理器(导出用)
        public MImgsManager mapImgsManagerForExport;
        public void resExport_Map(String subFolderName,  String strFormat, SmallDialog_ExportConfig dialogRes)
        {
            mapImgsManagerForExport = mapImagesManager.Clone();
            mapsManagerForExport = mapsManager.cloneForExport(mapImgsManagerForExport);
            mapsManagerForExport.tileGfxManager.clearSpilth(false);
            //新生成的图片组
            MImgsManager mImgsManagerGenerated = new MImgsManager();
            //获取链接分组列表
            List<List<MImgElement>> allGroups = mapImgsManagerForExport.getAllUsedGroups();
            //处理优化分组
            for (int i = 0; i < allGroups.Count - 1; i++)
            {
                List<MImgElement> group = allGroups[i];
                Size needSize;
                int usedSpace;
                Image imgExort;
                MImgElement imgElement = MImgsManager.optimizeClips(group, out needSize, out usedSpace, out imgExort, true);
                mImgsManagerGenerated.Add(imgElement);
                SaveGenImage(strFormat, subFolderName, imgElement);
            }
            //处理禁止优化分组
            List<MImgElement> groupForbidOpt = allGroups[allGroups.Count - 1];
            foreach (MImgElement imgElement in groupForbidOpt)
            {
                mImgsManagerGenerated.Add(imgElement);
                imgElement.image = GraphicsUtil.getMatchImage((Bitmap)imgElement.image);
                SaveGenImage(strFormat, subFolderName, imgElement);
            }
            //变更图片管理器
            mapImgsManagerForExport = mImgsManagerGenerated;
            mapsManagerForExport.tileGfxManager.imagesManager = mapImgsManagerForExport;

            dialogRes.setStep(1, 1);
        }
        //切片优化工具类
        class TileClip
        {
            public MImgElement imageElement;
            public Rectangle clipRect;
            public int x;
            public int y;
            public TileClip(MImgElement imageElementP,Rectangle clipRectP, int xP, int yP)
            {
                imageElement = imageElementP;
                clipRect = clipRectP;
                x = xP;
                y = yP;
            }
        }
        class TileClipSorter
        {
            ArrayList tileClipsArray = new ArrayList();
            ArrayList imgClipsArray = new ArrayList();
            int xAddUp = 0;
            int yAddUp = 0;
            int wAddUp = 0;
            int hAddUp = 0;
            public TileClip conatainsClip(MClipElement baseClipElementP)
            {
                for (int i = 0; i < tileClipsArray.Count; i++)
                {
                    TileClip tileClip = (TileClip)tileClipsArray[i];
                    if (tileClip.imageElement.Equals(baseClipElementP.imageElement) && tileClip.clipRect.Equals(baseClipElementP.clipRect))
                    {
                        return tileClip;
                    }
                }
                return null;
            }
            public void addClip(MClipElement baseClipElement)
            {
                TileClip tielClip = new TileClip(baseClipElement.imageElement,baseClipElement.clipRect, xAddUp, yAddUp);
                tileClipsArray.Add(tielClip);//加入切片数据
                Image imgClip = new Bitmap(tielClip.clipRect.Width, tielClip.clipRect.Height);
                Graphics g = Graphics.FromImage(imgClip);
                GraphicsUtil.drawClip(g, tielClip.imageElement.image, 0, 0, tielClip.clipRect.X, tielClip.clipRect.Y, tielClip.clipRect.Width, tielClip.clipRect.Height, 0);
                imgClipsArray.Add(imgClip);//加入切片图片
                g.Dispose();
                //累加坐标
                xAddUp += tielClip.clipRect.Width;
                wAddUp = xAddUp;
                if (hAddUp < tielClip.clipRect.Height)
                {
                    hAddUp = tielClip.clipRect.Height;
                }
            }
            //改变原有数据为现今排列数据
            public void setPos(MClipElement baseClipElement, TileClip tielClip)
            {
                baseClipElement.clipRect.X = tielClip.x;
                baseClipElement.clipRect.Y = tielClip.y;
            }
            //调整数据
            public void adjustData(bool optimize)
            {
                if (optimize)
                {
                    int widthStandird = ((int)Math.Sqrt(wAddUp / hAddUp)) * hAddUp;
                    int height = 0;
                    int rowHeight = 0;
                    int rowWidth = 0;
                    //先计算高度
                    for (int i = 0; i < tileClipsArray.Count; i++)
                    {
                        TileClip tileClip = (TileClip)tileClipsArray[i];
                        Image imgClip = (Image)imgClipsArray[i];
                        if (rowHeight < imgClip.Height)
                        {
                            rowHeight = imgClip.Height;
                        }
                        if (rowWidth + imgClip.Width > widthStandird)
                        {
                            height += rowHeight;
                            rowWidth = 0;
                        }
                        tileClip.x = rowWidth;
                        tileClip.y = height;
                        rowWidth += imgClip.Width;
                    }
                    height += rowHeight;
                    wAddUp = widthStandird;
                    hAddUp = height;
                }
            }
            //生成图片
            public Image createImage()
            {
                if (wAddUp <= 0 || hAddUp <= 0)
                {
                    return null;
                }
                Image newImg  = new Bitmap(wAddUp, hAddUp);
                Graphics g = Graphics.FromImage(newImg);
                for (int i = 0; i < tileClipsArray.Count; i++)
                {
                    TileClip tileClip = (TileClip)tileClipsArray[i];
                    Image imgClip = (Image)imgClipsArray[i];
                    GraphicsUtil.drawClip(g, imgClip, tileClip.x, tileClip.y, 0, 0, imgClip.Width, imgClip.Height, 0);
                }
                g.Dispose();
                //if (optimize == Consts.OptimizeLine)
                //{
                //    newImg = new Bitmap(wAddUp, hAddUp);
                //    Graphics g = Graphics.FromImage(newImg);
                //    for (int i = 0; i < tileClipsArray.Count; i++)
                //    {
                //        TileClip tileClip = (TileClip)tileClipsArray[i];
                //        Image imgClip = (Image)imgClipsArray[i];
                //        GraphicsUtil.drawClip(g, imgClip, tileClip.x, tileClip.y, 0, 0, imgClip.Width, imgClip.Height, 0);
                //    }
                //    g.Dispose();
                //}
                //else if (optimize == Consts.OptimizeSquare)
                //{
                //    int widthStandird = ((int)Math.Sqrt(wAddUp / hAddUp)) * hAddUp;
                //    int height = 0;
                //    int rowHeight = 0;
                //    int rowWidth=0;
                //    //先计算高度
                //    for (int i = 0; i < tileClipsArray.Count; i++)
                //    {
                //        TileClip tileClip = (TileClip)tileClipsArray[i];
                //        Image imgClip = (Image)imgClipsArray[i];
                //        if (rowHeight < imgClip.Height)
                //        {
                //            rowHeight = imgClip.Height;
                //        }
                //        if (rowWidth + imgClip.Width > widthStandird)
                //        {
                //            height += rowHeight;
                //            rowWidth = 0;
                //        }
                //        rowWidth += imgClip.Width;
                //    }
                //    height += rowHeight;
                //    //绘制
                //    newImg = new Bitmap(widthStandird, height);
                //    height = 0;
                //    rowHeight = 0;
                //    rowWidth = 0;
                //    Graphics g = Graphics.FromImage(newImg);
                //    for (int i = 0; i < tileClipsArray.Count; i++)
                //    {
                //        TileClip tileClip = (TileClip)tileClipsArray[i];
                //        Image imgClip = (Image)imgClipsArray[i];
                //        if (rowHeight < imgClip.Height)
                //        {
                //            rowHeight = imgClip.Height;
                //        }
                //        if (rowWidth + imgClip.Width > widthStandird)
                //        {
                //            height += rowHeight;
                //            rowWidth = 0;
                //        }
                //        GraphicsUtil.drawClip(g, imgClip, rowWidth, height, 0, 0, imgClip.Width, imgClip.Height, 0);
                //        tileClip.x = rowWidth;
                //        tileClip.y = height;
                //        setPos(tileClip.baseClipElement, tileClip);
                //        rowWidth += imgClip.Width;

                //    }
                //    g.Dispose();
                //}
                return newImg;
            }

        }





        //***************************************************************************************************************
        //**********                                                                                               ******
        //**********                                                                                               ******
        //**********                                  以下为窗体操作与事件                                         ******
        //**********                                                                                               ******
        //**********                                                                                               ******
        //***************************************************************************************************************

        //########################################### 数据定义 ###########################################################
        //参数标志
        //public const byte LEVEL_PHYSICS = 0;//物理层(存放地图物理标记)
        //public const byte LEVEL_TILE_BG = 1; //底层地形层(存放最底层非透明地图方块)
        //public const byte LEVEL_TILE_SUR = 2;//融合地形层(存放半透明地图方块)
        //public const byte LEVEL_TILE_OBJ = 3;//对象地形层(以对象为地形，制造更加复杂的地形)
        //public const byte LEVEL_OBJ_MASK = 4;//无关对象层(存放位于修饰动态对象，不可以添加事件，不设事件ID)
        //public const byte LEVEL_OBJ_TRIGEER = 5;//角色事件层(存放角色对象，可以添加事件，设事件ID)
        //public static byte Consts.currentLevel = Consts.LEVEL_PHYSICS;//焦点图层
        //事件响应标志
        private bool noScrollEvent = false;//屏蔽非用户操作的滚动条改变
        //private bool noCheckEvent = false;//屏蔽非用户操作的多选框改变
        private bool noListBoxEvent = false;//屏蔽非用户操作的列表框改变
        //private bool noNumericEvent = false;//屏蔽非用户操作的数字框改变

        //########################################### UI和数据操作 #######################################################
        //============================系统菜单部分================================
        private void 保存工程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveUserData())
            {
                MessageBox.Show("文档保存完毕", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void 打开工程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProject();
        }
        //打开工程
        private void openProject()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "c2d files (*.c2dx)|*.c2dx";
            dialog.FileName = "";
            dialog.Title = "打开工程文件";
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                DialogResult res = MessageBox.Show("是否保存当前工程？", "关闭警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (res.Equals(DialogResult.Yes))
                {
                    if (saveUserData())
                    {
                        MessageBox.Show("文档保存完毕", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("文档保存失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                }
                if (res.Equals(DialogResult.Cancel))
                {
                    return;
                }
                Consts.PATH_PROJECT_FILEPATH = dialog.FileName;
                Consts.updateFolders();
                this.path_file = Consts.PATH_PROJECT_FILEPATH;
                this.releaseRes();
                this.releaseWindowsData();
                this.initWorld();
            }
        }

        private void 导出数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Lisence.currentLiscence == Lisence.CDKey_Try )
            //{
            //    return;
            //}
            //if (Lisence.currentLiscence != Lisence.CDKey_Develop)
            //{
            //    Lisence.checkNetworkTime();
            //    if (Lisence.userOverDay > 0)
            //    {
            //        Console.WriteLine("Consts.userNbDay:" + Lisence.userOverDay);
            //        return;
            //    }
            //}
            //显示导出窗口
            SmallDialog_ExportConfig exportDialog = new SmallDialog_ExportConfig(this);
            exportDialog.initDialog();
            exportDialog.ShowDialog();
        }
        private void 版本信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SmallDialog_SoftWareInf().ShowDialog();
        }
        //地图切片编辑
        private void 地图切片编辑toolStripMenuItem_MapClip_Click(object sender, EventArgs e)
        {

        }
        //地图图片管理
        private void 地图图片管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_MapImagesManager.MClipsManager = currentGfxContainer;
            form_MapImagesManager.initParams(null);
            form_MapImagesManager.ShowDialog();
        }

        //============================地图列表与编辑部分================================
        //加入新单元
        public void addElement()
        {
            if (listBox_Maps.Focused)
            {
                MapElement element = SmallDialog_NewMap.getMapElement(mapsManager);
                if (element != null)
                {
                    currentStageID = 0;
                    mapsManager.Add(element);
                    currentMap = element;
                    updateContainer_MapList();
                    setCurrentMap(mapsManager.Count() - 1);
                    updateMap_Refresh();
                }
            }
            if (listBox_stage.Focused && currentMap != null)
            { 
                String name = "场景" + currentMap.stageList.getElementCount();
                StageElement element = SmallDialog_Stage.createStageElement(currentMap.stageList,this);
                if (element != null)
                {
                    //在地图块数据中增加
                    for (int i = 0; i < currentMap.mapData.GetLength(0); i++)
                    {
                        for (int j = 0; j < currentMap.mapData.GetLength(1); j++)
                        {
                            currentMap.mapData[i, j].tile_objectList.Add(new TileObjectElement(currentMap.mapData[i, j]));
                        }
                    }
                    //在场景层数据中添加
                    currentMap.stageList.addElement(element);
                }
            }
        }
        //复制单元
        public void cloneElement()
        {
            //....go on
            if (listBox_Maps.Focused)
            {
                int index = listBox_Maps.SelectedIndex;
                if (index < 0)
                {
                    return;
                }
                //MapElement element = ((MapElement)mapsManager.mapsList[index]).clone();
                //String name = element.getName();
                //SmallDialog_Text txtDialog = new SmallDialog_Text("复制单元", name);
                //txtDialog.ShowDialog();
                //name = txtDialog.getValue();
                //element.setName(name);
                //mapsManager.mapsList.Add(element);
                //updateContainer_MapList();
                //setCurrentMap(listBox_Maps.Items.Count - 1);
                //updateMap_Refresh();
            }

        }
        //向上移动单元
        public void moveUpElement()
        {
            if (listBox_Maps.Focused)
            {
                int index = listBox_Maps.SelectedIndex;
                if (index < 1)
                {
                    return;
                }
                MapElement element = (MapElement)mapsManager[index];
                mapsManager[index] = mapsManager[index - 1];
                mapsManager[index - 1] = element;
                updateContainer_MapList();
                setCurrentMap(index - 1);
            }
            if (listBox_stage.Focused&&currentMap!=null)
            {
                int index = listBox_stage.SelectedIndex;
                if (index < 1)
                {
                    return;
                }
                currentMap.stageList.moveUpElement(index);
            }
        }
        //向下移动单元
        public void moveDownElement()
        {
            if (listBox_Maps.Focused)
            {
                int index = listBox_Maps.SelectedIndex;
                if (index >= listBox_Maps.Items.Count - 1)
                {
                    return;
                }
                MapElement element = (MapElement)mapsManager[index];
                mapsManager[index] = mapsManager[index + 1];
                mapsManager[index + 1] = element;
                updateContainer_MapList();
                setCurrentMap(index + 1);
            }
            if (listBox_stage.Focused && currentMap != null)
            {
                int index = listBox_stage.SelectedIndex;
                if (index < 1)
                {
                    return;
                }
                currentMap.stageList.moveDownElement(index);
            }
        }
        //删除单元
        public void deleteElement()
        {
            if (listBox_Maps.Focused)
            {
                int index = listBox_Maps.SelectedIndex;
                if (index < 0)
                {
                    return;
                }
                if (!MessageBox.Show("确定删除地图 [" + ((MapElement)mapsManager[index]).getName() + "]？删除后无法恢复。", "地图删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.Yes))
                {
                    return;
                }
                clearHistory();
                mapsManager.RemoveAt(index);
                updateContainer_MapList();
                int newIndex = index - 1;
                if (newIndex < 0 && mapsManager.Count() > 0)
                {
                    newIndex = 0;
                }
                setCurrentMap(newIndex);
                currentStageID = 0;
                updateMap_Refresh();
                clearClipboard();
            }
            if (listBox_stage.Focused && currentMap != null)
            {
                int index = listBox_stage.SelectedIndex;
                if (index < 0)
                {
                    return;
                }
                if (currentMap.stageList.getElementCount() <= 1)
                {
                    MessageBox.Show("不能删除最后一个场景","场景删除",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                if (!MessageBox.Show("确定删除场景[" + ((StageElement)currentMap.stageList.getElement(index)).name + "]？删除后无法恢复。", "场景删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.Yes))
                {
                    return;
                }
                //在地图块数据中删除
                for (int i = 0; i < currentMap.mapData.GetLength(0); i++)
                {
                    for (int j = 0; j < currentMap.mapData.GetLength(1); j++)
                    {
                        currentMap.mapData[i, j].tile_objectList.RemoveAt(currentStageID);
                    }
                }
                if (currentStageID >= currentMap.stageList.getElementCount() - 1)
                {
                    currentStageID = currentMap.stageList.getElementCount() - 2;
                }
                //删除场景层数据
                currentMap.stageList.removeElement(index);
            }
        }
        //配置地图单元
        public void configElement()
        {
            if (listBox_Maps.Focused)
            {
                int index = listBox_Maps.SelectedIndex;
                if (index < 0)
                {
                    return;
                }
                MapElement element = mapsManager.getElement(index);
                SmallDialog_NewMap.configMapElement(element);
                updateContainer_MapList();
                updateMap_Refresh();
            }
            if (listBox_stage.Focused&&currentMap!=null)
            {
                int index = listBox_stage.SelectedIndex;
                if (index >= 0)
                {
                    SmallDialog_Stage.configMapElement((StageElement)currentMap.stageList.getElement(index), this);
                    currentMap.stageList.refreshUI_Element(index);
                }
            }
        }
        //重新设置当前单元
        public void resetCurrentElement()
        {
            int index = listBox_Maps.SelectedIndex;
            if (index < 0 || index >= mapsManager.Count())
            {
                index = -1;
            }
            currentMap = mapsManager.getElement(index);
            if (currentMap != null)
            {
                currentMap.stageList.refreshUI();
            }
            else
            {
                this.listBox_stage.Items.Clear();
            }
            kss_refreshUI(currentMap, currentStageID);
        }
        //设置当前焦点地图
        public void setCurrentMap(int index)
        {
            if (index < 0 || index >= mapsManager.Count())
            {
                index = -1;
            }
            this.noListBoxEvent = true;
            listBox_Maps.SelectedIndex = index;
            resetCurrentElement();
            listBox_Maps.Refresh();
            this.noListBoxEvent = false;
        }
        //刷新单元列表
        public void updateContainer_MapList()
        {
            int oldIndex = listBox_Maps.SelectedIndex;
            listBox_Maps.BeginUpdate();
            listBox_Maps.Items.Clear();
            this.noListBoxEvent = true;
            for (int i = 0; i < mapsManager.Count(); i++)
            {
                listBox_Maps.Items.Add(((MapElement)mapsManager[i]).getName());
            }
            this.noListBoxEvent = false;
            listBox_Maps.EndUpdate();
            listBox_Maps.Refresh();
            this.setCurrentMap(oldIndex);
        }

        private void listBox_Maps_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            listBox_Maps.Focus();
            if (e.Button == MouseButtons.Right)
            {
                if (listBox_Maps.SelectedIndex >= 0)
                {
                    contextMenuStrip_listItem.Items[4].Enabled = true;
                    contextMenuStrip_listItem.Items[5].Enabled = true;
                }
                else
                {
                    contextMenuStrip_listItem.Items[4].Enabled = false;
                    contextMenuStrip_listItem.Items[5].Enabled = false;
                }
                contextMenuStrip_listItem.Show((Control)sender, e.X, e.Y);
            }
        }
        //新建单元
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addElement();
        }
        //复制单元
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cloneElement();
        }
        //单元上移
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveUpElement();
        }
        //单元下移
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveDownElement();
        }
        //删除单元
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteElement();
        }
        //双击“设置单元”或者“新建单元”
        private void listBox_Maps_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            listBox_Maps.Focus();  
            if (e.Button == MouseButtons.Left)
            {
                MouseEventArgs ex = (MouseEventArgs)e;
                if (ex.Y < listBox_Maps.ItemHeight * listBox_Maps.Items.Count)
                {
                    configElement();
                }
                else
                {
                    addElement();
                }
            }
        }
        //右键单击“设置单元”
        private void 设置双击条目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configElement();
        }
        //切换当前单元
        private void listBox_Maps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.noListBoxEvent)
            {
                return;
            }
            if (listBox_Maps.Focused)
            {
                resetCurrentElement();
                currentStageID = 0;
                updateMap_Refresh();
            }
        }
        private static bool showCloseWaring=true;//显示关系警告
        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(showCloseWaring)
            {
                DialogResult res = MessageBox.Show("即将关闭，是否保存工程？", "关闭警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (res.Equals(DialogResult.Cancel))
                {
                    e.Cancel = true;
                    return;
                }
                if (res.Equals(DialogResult.Yes))
                {
                    saveUserData();
                }
            }
            releaseWindowsData();
            releaseRes();
        }

        private void TSB_levelActors_Eye_Click(object sender, EventArgs e)
        {
            Image imgOn = global::Cyclone.Properties.Resources.eyeOn;
            Image imgOff = global::Cyclone.Properties.Resources.eyeOff;
            Consts.levelEye = !Consts.levelEye;
            if (!Consts.levelEye)
            {
                TSB_levelActors_Eye.Image = imgOff;
            }
            else
            {
                TSB_levelActors_Eye.Image = imgOn;
            }
            this.updateMap_Refresh();
        }
        private void setLevel(byte newLevel)
        {
            if (newLevel < Consts.LEVEL_PHYSICS || newLevel > Consts.LEVEL_OBJ_TRIGEER)
            {
                return;
            }
            int oldLevel = Consts.currentLevel;
            Consts.currentLevel = newLevel;
            if (Consts.currentLevel == Consts.LEVEL_PHYSICS)
            {
                TSB_levelPhysic.Checked = true;
            }
            else
            {
                TSB_levelPhysic.Checked = false;
            }
            if (Consts.currentLevel == Consts.LEVEL_TILE_BG)
            {
                TSB_level_Ground.Checked = true;
            }
            else
            {
                TSB_level_Ground.Checked = false;
            }
            if (Consts.currentLevel == Consts.LEVEL_TILE_SUR)
            {
                TSB_level_Surface.Checked = true;
            }
            else
            {
                TSB_level_Surface.Checked = false;
            }
            if (Consts.currentLevel == Consts.LEVEL_TILE_OBJ)
            {
                TSB_level_Tile_Obj.Checked = true;
            }
            else
            {
                TSB_level_Tile_Obj.Checked = false;
            }
            if (Consts.currentLevel == Consts.LEVEL_OBJ_MASK)
            {
                TSB_level_Obj_Mask.Checked = true;
            }
            else
            {
                TSB_level_Obj_Mask.Checked = false;
            }
            if (Consts.currentLevel == Consts.LEVEL_OBJ_TRIGEER)
            {
                TSB_level_Object.Checked = true;
            }
            else
            {
                TSB_level_Object.Checked = false;
            }
            if (oldLevel != Consts.currentLevel&&!Consts.levelEye)
            {
                updateMap_Refresh();
            }
        }
        private void TSB_levelPhysic_Click(object sender, EventArgs e)
        {
            setLevel(Consts.LEVEL_PHYSICS);
        }

        private void TSB_levelGround_Click(object sender, EventArgs e)
        {
            setLevel(Consts.LEVEL_TILE_BG);
        }


        private void TSB_levelSurface_Click(object sender, EventArgs e)
        {
            setLevel(Consts.LEVEL_TILE_SUR);
        }
        private void TSB_levelObjectMask_Click(object sender, EventArgs e)
        {
            setLevel(Consts.LEVEL_TILE_OBJ);
        }
        private void TSB_levelGroundObject_Click(object sender, EventArgs e)
        {
            setLevel(Consts.LEVEL_OBJ_MASK);
        }
        private void TSB_levelActor_Click(object sender, EventArgs e)
        {
            setLevel(Consts.LEVEL_OBJ_TRIGEER);
        }


        private void TSB_open_Click(object sender, EventArgs e)
        {
            openProject();
        }

        private void TSB_save_Click(object sender, EventArgs e)
        {
            if (saveUserData())
            {
                MessageBox.Show("文档保存完毕", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void toolStripMenuItem_MapClip_Click(object sender, EventArgs e)
        {
            if (mapImagesManager.Count() <= 0)
            {
                MessageBox.Show("请先在“地图图片管理”中添加图片!", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //form_ClipsManager = new Form_BaseClipsManager(mapBaseClipsManager, "地图切片编辑");
                //form_ClipsManager.initParams(true, Consts.mapClipW, Consts.mapClipH);
                //form_ClipsManager.ShowDialog();
                //releaseDatas();
            }
        }
        //============================地图编辑区部分================================
        MapElement currentMap = null;//当前地图
        public int currentStageID = 0;//当前场景ID
        //当前活动对象
        TileObjectElement currentActiveObj = null;
        //编辑模式
        private const byte EDITMOD_PENCIL = 0;                                    //铅笔模式
        private const byte EDITMOD_RECT_SELECT = (byte)(EDITMOD_PENCIL + 1);        //框选模式
        private const byte EDITMOD_ERASER = (byte)(EDITMOD_RECT_SELECT + 1);//橡皮模式
        private const byte EDITMOD_STRAW = (byte)(EDITMOD_ERASER + 1);//吸管模式
        private byte editMode = EDITMOD_ERASER;
        //缩放比率
        public static float zoomLevel = 1;
        private void setZoomLevel(float newLevel)
        {
            float oldLevel = zoomLevel;
            zoomLevel = newLevel;
            if (zoomLevel != oldLevel)
            {
                refreshCanvasBuffer_tile = true;
                refreshCanvasBuffer_Obj = true;
            }
        }
        //地图缓冲
        public Image mapBuffer_Window = null;
        public Image mapBuffer_World = null;
        public Image mapBuffer_World_Obj = null;
        public Graphics gMapWindow = null;
        public Graphics gMapBuffer = null;
        public Graphics gMapBuffer_Obj = null;
        public int mapBufferW=1;
        public int mapBufferH=1;
        public int mapBufferW_OBJ = 1;
        public int mapBufferH_OBJ = 1;
        public int mapWindowW = 1;
        public int mapWindowH = 1;
        public int mapWindowTop = 0;
        public int mapWindowLeft = 0;
        public int mapContentWidth = 0;
        public int mapContentHeight = 0;
        //界面参数
        private int cameraX_inMap = 0;//地图左上角被窗口遮盖部分的尺寸
        private int cameraY_inMap = 0;
        private int mouseXInMap = 0;//鼠标点击点在地图中坐标
        private int mouseYInMap = 0;
        private int TileW = 1;//图块大小
        private int TileH = 1;
        private int zoomTileW = 1;//缩放图块大小
        private int zoomTileH = 1;
        private int mouseDownXInWindow = 0;//鼠标按下坐标(相对窗口)
        private int mouseDownYInWindow = 0;
        private int mouseMoveXInWindow = 0;//鼠标移动坐标(相对窗口)
        private int mouseMoveYInWindow = 0;
        private int frameX_0 = -1;//框选区域(相对地图)
        private int frameY_0 = -1;
        private int frameX_1 = -1;
        private int frameY_1 = -1;
        private bool rebuildBuffer()
        {
            mapWindowW = pictureBox_Canvas.Width;
            mapWindowH = pictureBox_Canvas.Height;
            if (currentMap == null)
            {
                return false;
            }
            bool needRefresh = false;
            //计算缩放TILE大小
            TileW = currentMap.getTileW();
            TileH = currentMap.getTileH();
            zoomTileW = (int)(TileW * zoomLevel);
            if (zoomTileW < 1)
            {
                zoomTileW = 1;
            }
            zoomTileH = (int)(TileH * zoomLevel);
            if (zoomTileH < 1)
            {
                zoomTileH = 1;
            }
            //计算内容大小
            VScrollBar vBar = this.vScrollBar_Canvas;
            HScrollBar hBar = this.hScrollBar_Canvas;
            int oldW = mapContentWidth;
            int oldH = mapContentHeight;
            mapContentWidth = (int)(zoomTileW * currentMap.getMapW());
            mapContentHeight = (int)(zoomTileH * currentMap.getMapH());
            if (oldW != mapContentWidth || oldH != mapContentHeight)
            {
                needRefresh = true;
            }

            if (mapContentWidth > mapWindowW)
            {
                cameraX_inMap = (hBar.Value - hBar.Minimum) * (mapContentWidth - mapWindowW) / (hBar.Maximum - 9 - hBar.Minimum);
            }
            else
            {
                cameraX_inMap = 0;
                mapWindowW = mapContentWidth;
            }
            if (mapContentHeight > mapWindowH)
            {
                cameraY_inMap = (vBar.Value - vBar.Minimum) * (mapContentHeight - mapWindowH) / (vBar.Maximum - 9 - vBar.Minimum);
            }
            else
            {
                cameraY_inMap = 0;
                mapWindowH = mapContentHeight;
            }
            //重建世界缓冲
            int mapBufferWNeed = (mapWindowW / zoomTileW + (mapWindowW % zoomTileW == 0 ? 1 : 2)) * zoomTileW;
            int mapBufferHNeed = (mapWindowH / zoomTileH + (mapWindowH % zoomTileH == 0 ? 1 : 2)) * zoomTileH;
            if (currentMap != null)
            {
                //地图方块缓冲
                int realW = 0;
                int realH = 0;
                if (mapBuffer_World != null)
                {
                    realW = mapBuffer_World.Width;
                    realH = mapBuffer_World.Height;
                }
                if (realW < mapBufferWNeed)
                {
                    realW = mapBufferWNeed;
                }
                if (realH < mapBufferHNeed)
                {
                    realH = mapBufferHNeed;
                }
                mapBufferW = realW;
                mapBufferH = realH;
                if (mapBuffer_World == null || mapBuffer_World.Width != realW || mapBuffer_World.Height != realH)
                {
                    mapBuffer_World = new Bitmap(realW, realH);
                    gMapBuffer = Graphics.FromImage(mapBuffer_World);
                    needRefresh = true;
                }
                //对象缓冲
                realW = 0;
                realH = 0;
                if (mapBuffer_World_Obj != null)
                {
                    realW = mapBuffer_World_Obj.Width;
                    realH = mapBuffer_World_Obj.Height;
                }
                mapBufferWNeed = currentMap.getMapW() * currentMap.getTileW();
                mapBufferHNeed = currentMap.getMapH() * currentMap.getTileH();
                if (realW < mapBufferWNeed)
                {
                    realW = mapBufferWNeed;
                }
                if (realH < mapBufferHNeed)
                {
                    realH = mapBufferHNeed;
                }
                mapBufferW_OBJ = realW;
                mapBufferH_OBJ = realH;
                if (mapBuffer_World_Obj == null || mapBuffer_World_Obj.Width < mapBufferW_OBJ || mapBuffer_World_Obj.Height < mapBufferH_OBJ)
                {
                    mapBuffer_World_Obj = new Bitmap(mapBufferW_OBJ, mapBufferH_OBJ);
                    gMapBuffer_Obj = Graphics.FromImage(mapBuffer_World_Obj);
                    needRefresh = true;
                }
            }
            return needRefresh;
        }
        public void updateMap_Refresh()
        {
            //Console.WriteLine("updateMap_Refresh");
            refreshCanvasBuffer_tile = true;
            refreshCanvasBuffer_Obj = true;
            this.updateMap();
        }
        //更新窗口
        private bool paintPenRect = false;
        public void updateMap()
        {
            //更新窗口缓冲
            if (mapBuffer_Window == null || mapBuffer_Window.Width != pictureBox_Canvas.Width || mapBuffer_Window.Height != pictureBox_Canvas.Height)
            {
                mapBuffer_Window = new Bitmap(pictureBox_Canvas.Width, pictureBox_Canvas.Height);
                gMapWindow = Graphics.FromImage(mapBuffer_Window);
            }
            int windowW = pictureBox_Canvas.Width;
            int windowH = pictureBox_Canvas.Height;
            GraphicsUtil.fillRect(gMapWindow, 0, 0, windowW, windowH, Consts.colorBlack);
            //重建缓冲
            bool refresh = rebuildBuffer();
            if (refresh)
            {
                refreshCanvasBuffer_tile = true;
                refreshCanvasBuffer_Obj = true;
            }
            if (currentMap != null)
            {
                //显示地图
                if (this.mapBuffer_World != null)
                {
                    drawMap(cameraX_inMap, cameraY_inMap);
                    drawMap_Obj(cameraX_inMap, cameraY_inMap);
                    //currentMap.displayObjs(gMapWindow, cameraX_inMap - (cameraX_inMap % zoomTileW), cameraY_inMap - (cameraY_inMap % zoomTileH), zoomLevel,cameraX_inMap / zoomTileW,cameraY_inMap / zoomTileH, (cameraX_inMap + windowW) / zoomTileW, (cameraY_inMap + windowH) / zoomTileH);
                }
                //显示剪贴板
                xPEN = mouseMoveXInWindow - ((mouseMoveXInWindow + cameraX_inMap) % zoomTileW);
                yPEN = mouseMoveYInWindow - ((mouseMoveYInWindow + cameraY_inMap) % zoomTileH);
                if (inPasting)
                {
                    GraphicsUtil.drawClip(gMapWindow, imgBuffer_CB, xPEN, yPEN, 0, 0, copyedW * TileW, copyedH * TileH, 0, zoomLevel);
                    int xMin = xPEN;
                    int yMin = yPEN;
                    int xMax = xMin + copyedW * zoomTileW;
                    int yMax = yMin + copyedH * zoomTileH;
                    GraphicsUtil.fillRect(gMapWindow, xMin, yMin, xMax - xMin, yMax - yMin, Consts.color_MapMask, 0x44);
                    GraphicsUtil.drawDashLine(gMapWindow, xMin, yMin, xMax, yMin, Consts.colorBlue, 1);
                    GraphicsUtil.drawDashLine(gMapWindow, xMin, yMin, xMin, yMax, Consts.colorBlue, 1);
                    GraphicsUtil.drawDashLine(gMapWindow, xMax, yMin, xMax, yMax, Consts.colorBlue, 1);
                    GraphicsUtil.drawDashLine(gMapWindow, xMin, yMax, xMax, yMax, Consts.colorBlue, 1);
                }
                //显示选区
                if ((editMode == EDITMOD_RECT_SELECT || editMode == EDITMOD_STRAW) && (!inPasting))
                {
                    int xMin = Math.Min(frameX_0, frameX_1) * zoomTileW - cameraX_inMap;
                    int yMin = Math.Min(frameY_0, frameY_1) * zoomTileH - cameraY_inMap;
                    int xMax = (Math.Max(frameX_0, frameX_1) + 1) * zoomTileW - cameraX_inMap;
                    int yMax = (Math.Max(frameY_0, frameY_1) + 1) * zoomTileH - cameraY_inMap;
                    GraphicsUtil.fillRect(gMapWindow, xMin, yMin, xMax - xMin, yMax - yMin, Consts.color_MapMask, 0x44);
                    GraphicsUtil.drawDashLine(gMapWindow, xMin, yMin, xMax, yMin, Consts.colorWhite, 1);
                    GraphicsUtil.drawDashLine(gMapWindow, xMin, yMin, xMin, yMax, Consts.colorWhite, 1);
                    GraphicsUtil.drawDashLine(gMapWindow, xMax, yMin, xMax, yMax, Consts.colorWhite, 1);
                    GraphicsUtil.drawDashLine(gMapWindow, xMin, yMax, xMax, yMax, Consts.colorWhite, 1);
                }
                //显示笔触单元框
                if (paintPenRect && (!inPasting)&&
                    xPEN + cameraX_inMap < zoomTileW * currentMap.getMapW() &&
                    yPEN + cameraY_inMap < zoomTileH * currentMap.getMapH())
                {
                    GraphicsUtil.drawRect(gMapWindow, xPEN, yPEN, zoomTileW, zoomTileH, Consts.colorWhite);
                }
                if (currentActiveObj != null)
                {
                    currentActiveObj.displayBorder(gMapWindow, xPEN + zoomTileW / 2, yPEN + zoomTileH / 2, zoomLevel);
                }
                //....go on
            }
            else
            {

            }
            ////绘制到屏幕
            //Graphics g = panel_Canvas.CreateGraphics();
            //g.DrawImage(mapBuffer_Window, 0, 0);
            //g.Dispose();
            //绘制到屏幕
            if (pictureBox_Canvas.Image == null || !pictureBox_Canvas.Image.Equals(mapBuffer_Window))
            {
                pictureBox_Canvas.Image = mapBuffer_Window;
            }
            else
            {
                pictureBox_Canvas.Refresh();
            }
        }
    private int   m_prevX0; // X left position of last screen in background
    private int   m_prevX1; // X right position of last screen in background
    private int   m_prevY0; // X top position of last screen in background
    private int   m_prevY1; // X bottom position of last screen in background
    private bool refreshCanvasBuffer_tile=true;
    private void drawMap(int camX, int camY)
    {
            int alignedX0, alignedY0, alignedX1, alignedY1, start, end; // tile
            int modX0, modX1, modY0, modY1; // pixel
            alignedX0 = camX / zoomTileW;
            alignedX1 = alignedX0 + (mapWindowW + zoomTileW-1) / zoomTileW;
            if (alignedX1 >= mapContentWidth / zoomTileW)
            {
                alignedX1 = mapContentWidth / zoomTileW - 1;
            }
            alignedY0 = camY / zoomTileH;
            alignedY1 = alignedY0 + (mapWindowH + zoomTileW - 1) / zoomTileH;
            if (alignedY1 >= mapContentHeight / zoomTileW)
            {
                alignedY1 = mapContentHeight / zoomTileW - 1;
            }
            if (refreshCanvasBuffer_tile)
            {
                    GraphicsUtil.fillRect(gMapBuffer, 0, 0, mapBuffer_World.Width, mapBuffer_World.Height, currentMap.getColor());
                    refreshCanvasBuffer_tile = false;
                    drawMapBuffer(gMapBuffer, alignedX0, alignedY0, alignedX1, alignedY1);
                    m_prevX0 = alignedX0;
                    m_prevY0 = alignedY0;
                    m_prevX1 = alignedX1;
                    m_prevY1 = alignedY1;
            }
            if (m_prevX0 != alignedX0)
            {
                    if(m_prevX0 < alignedX0)
                    {
                            start = m_prevX1+1;
                            end = alignedX1;
                    }
                    else
                    {
                            start = alignedX0;
                            end = m_prevX0-1;
                            if (end > alignedX1)
                            {
                                end = alignedX1;
                            }
                    }
                    drawMapBuffer(gMapBuffer, start, alignedY0, end, alignedY1);
                    m_prevX0 = alignedX0;
                    m_prevX1 = alignedX1;
            }
            if (m_prevY0 != alignedY0)
            {
                    if(m_prevY0 < alignedY0)
                    {
                            start = m_prevY1+1;
                            end = alignedY1;
                    }
                    else
                    {
                            start = alignedY0;
                            end = m_prevY0-1;
                            if (end > alignedY1)
                            {
                                end = alignedY1;
                            }
                    }
                    drawMapBuffer(gMapBuffer, alignedX0, start, alignedX1, end);
                    m_prevY0 = alignedY0;
                    m_prevY1 = alignedY1;
            }

            modX0 = camX % mapBufferW;
            modY0 = camY % mapBufferH;
            modX1 = (camX + mapWindowW) % mapBufferW;
            modY1 = (camY + mapWindowH) % mapBufferH;

            if(modX1 > modX0)
            {
                    if(modY1 > modY0)
                    {
                            copyFromBuffer(modX0, modY0, mapWindowW, mapWindowH, 0, mapWindowTop);
                    }
                    else
                    {
                            copyFromBuffer(modX0, modY0, mapWindowW, mapWindowH - modY1, 0, mapWindowTop);
                            copyFromBuffer(modX0, 0, mapWindowW, modY1, 0, mapWindowTop + mapWindowH - modY1);
                    }
            }
            else
            {
                    if(modY1 > modY0)
                    {
                            copyFromBuffer(modX0, modY0, mapWindowW - modX1, mapWindowH, 0, mapWindowTop);
                            copyFromBuffer(0, modY0, modX1, mapWindowH, mapWindowW - modX1, mapWindowTop);
                    }
                    else
                    {
                            copyFromBuffer(modX0, modY0, mapWindowW - modX1, mapWindowH - modY1, 0, mapWindowTop);//Top-Left
                            copyFromBuffer(modX0, 0, mapWindowW - modX1, modY1, 0, mapWindowTop + mapWindowH - modY1);//Bottom-Left
                            copyFromBuffer(0, modY0, modX1, mapWindowH - modY1, mapWindowW - modX1, mapWindowTop);//Top-Right
                            copyFromBuffer(0, 0, modX1, modY1, mapWindowW - modX1, mapWindowTop + mapWindowH - modY1);//ottom-Right
                    }
            }
    }
    private void drawMapBuffer(Graphics gb, int tileX0, int tileY0, int tileX1, int tileY1)
    {
        //int posX = (tileX0 * zoomTileW) % mapBufferW;
        //int posY = (tileY0 * zoomTileH) % mapBufferH;
        //Console.WriteLine("drawMapBuffer:" + tileX0 + "," + tileY0+"|"+tileX1 + "," + tileY1);
        //Console.WriteLine("posX:" + posX + ",posY:" + posY);
        //if (tileX0 > tileX1)
        //{
        //    Console.WriteLine("tileX0:" + tileX0 + ",tileX1:" + tileX1);
        //}
        //if (tileY0 > tileY1)
        //{
        //    Console.WriteLine("tileY0:" + tileY0 + ",tileY1:" + tileY1);
        //}
        currentMap.displayTile(gMapBuffer, mapBufferW, mapBufferH, zoomLevel, tileX0, tileY0, tileX1, tileY1);
    }

        private void copyFromBuffer(int modX, int modY, int w, int h, int screenX, int screenY)
        {
            GraphicsUtil.drawClip(gMapWindow, mapBuffer_World, screenX, screenY, modX, modY, w, h, 0);
        }
        bool refreshCanvasBuffer_Obj = false;
        private void drawMap_Obj(int camX, int camY)
        {
            if (currentMap == null)
            {
                return;
            }
            if (refreshCanvasBuffer_Obj)
            {
                gMapBuffer_Obj.SetClip(new Rectangle(0,0,mapBuffer_World_Obj.Width,mapBuffer_World_Obj.Height));
                gMapBuffer_Obj.Clear(Color.Transparent);
                refreshCanvasBuffer_Obj = false;
                currentMap.redrawAll(gMapBuffer_Obj);
            }
            else
            {
                //currentMap.clearNeedUpdateRegion(gMapBuffer_Obj);
                currentMap.redrawNeedUpdateRegion(gMapBuffer_Obj,zoomLevel);
            }
            currentMap.clearNeedUpdateFlag();
            GraphicsUtil.drawClip(gMapWindow, mapBuffer_World_Obj, 0, 0, (int)(camX / zoomLevel), (int)(camY / zoomLevel), (int)(mapWindowW / zoomLevel), (int)(mapWindowH / zoomLevel), 0,zoomLevel);
        }
        private void copyFromBuffer_Obj(int modX, int modY, int w, int h, int screenX, int screenY)
        {
            GraphicsUtil.drawClip(gMapWindow, mapBuffer_World_Obj, screenX, screenY, modX, modY, w, h, 0);
        }
        //鼠标移入
        private void pictureBox_Canvas_MouseEnter(object sender, EventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
             pictureBox_Canvas.Focus();  
        }
        //鼠标按下事件
        private void pictureBox_Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownXInWindow = MathUtil.limitNumber(e.X, 0, mapWindowW - 1);
            mouseDownYInWindow = MathUtil.limitNumber(e.Y, 0, mapWindowH - 1);
            mouseMoveXInWindow = mouseDownXInWindow;
            mouseMoveYInWindow = mouseDownYInWindow;

            if (!e.Button.Equals(MouseButtons.Left))
            {
                if (e.Button.Equals(MouseButtons.Right))
                {
                    inPasting = false;
                    if (currentActiveObj != null && currentActiveObj.antetype!=null)
                    {
                        SmallDialog_ConfigMapObj.configElement(this, currentActiveObj, "对象参数设置");
                        currentActiveObj = null;
                        this.kss_refreshUI(currentMap, currentStageID);
                    }
                    this.updateMap();
                }
                return;
            }
            if (currentMap == null)
            {
                MessageBox.Show("当前没有选中的地图，请先在“地图列表”中创建或选择");
                return;
            }
            //风格限定
            if (Consts.currentLevel == Consts.LEVEL_TILE_BG || Consts.currentLevel == Consts.LEVEL_TILE_SUR)
            {
                if (!currentMap.tileGfxContainer.Equals(currentGfxContainer))
                {
                    MessageBox.Show("当前的地图风格不是“" + currentGfxContainer.name + "”");
                    return;
                }
            }
            //粘贴剪贴板中的内容
            if (this.inPasting)
            {
                int destX = (xPEN + cameraX_inMap) / zoomTileW;
                int destY = (yPEN + cameraY_inMap) / zoomTileH;
                this.pasteRectFinished(destX, destY);
                this.updateMap();
                return;
            }
            //鼠标按下------------------------------------------(绘图、框选和擦除)
            //计算参数
            //Console.WriteLine("down:"+e.X + "," + e.Y);
            mouseXInMap = e.X + cameraX_inMap;
            mouseYInMap = e.Y + cameraY_inMap;
            Object currentFocusElement = getCurrentFocusElement();
            //执行操作
            switch (this.editMode)
            {
                case EDITMOD_PENCIL://铅笔模式
                    if (currentFocusElement != null)
                    {
                        if (currentFocusElement is Antetype)
                        {
                            TileObjectElement obj = new TileObjectElement(currentMap.getTile(mouseXInMap / zoomTileW, mouseYInMap / zoomTileH));
                            obj.antetype = (Antetype)currentFocusElement;
                            currentFocusElement = obj;
                        }
                        inRemTileEdit = true;
                        fillPointsCmd = new FillRandomPointsCommand(currentMap);
                        canAutoTile();
                        if (TSB_autoTile.Checked)
                        {
                            fillPointWithAutoTile(currentMap, mapBufferW, mapBufferH, zoomLevel, mouseXInMap / zoomTileW, mouseYInMap / zoomTileH, Consts.currentLevel, true, currentStageID,false);
                        }
                        else
                        {
                            fillPoint(currentMap, mapBufferW, mapBufferH, zoomLevel, mouseXInMap / zoomTileW, mouseYInMap / zoomTileH, Consts.currentLevel, currentFocusElement, true, currentStageID);
                        }
                    }
                    break;
                case EDITMOD_RECT_SELECT://框选模式
                case EDITMOD_STRAW://吸管模式
                    countRectSelect();
                    break;
                case EDITMOD_ERASER://橡皮模式
                    inRemTileEdit = true;
                    fillPointsCmd = new FillRandomPointsCommand(currentMap);
                    canAutoTile();
                    if (TSB_autoTile.Checked)
                    {
                        fillPointWithAutoTile(currentMap, mapBufferW, mapBufferH, zoomLevel, mouseXInMap / zoomTileW, mouseYInMap / zoomTileH, Consts.currentLevel, true, currentStageID, true);
                    }
                    else
                    {
                        fillPoint(currentMap, mapBufferW, mapBufferH, zoomLevel, mouseXInMap / zoomTileW, mouseYInMap / zoomTileH, Consts.currentLevel, null, true, currentStageID);
                    }
                    break;
            }
            this.updateMap();
        }
        //鼠标抬起事件
        private void pictureBox_Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (inRemTileEdit)
            {
                if (fillPointsCmd != null)
                {
                    m_HistoryManager.AddUndoCommand(fillPointsCmd, this);
                }
                inRemTileEdit = false;
            }
            else if (editMode == EDITMOD_STRAW)
            {
                if (e.Button.Equals(MouseButtons.Left))
                {
                    copyRectSelect();
                    pasteRectSelect();
                    updateMap();
                }
            }

        }
        //鼠标移动事件
        private void pictureBox_Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if ((editMode == EDITMOD_RECT_SELECT || editMode == EDITMOD_STRAW) && e.Button.Equals(MouseButtons.Left))
            {
                this.paintPenRect = false;
            }
            else
            {
                this.paintPenRect = true;
            }
            mouseMoveXInWindow = MathUtil.limitNumber(e.X, 0, mapWindowW);
            mouseMoveYInWindow = MathUtil.limitNumber(e.Y, 0, mapWindowH);
            //计算参数
            int mouseXImMapOld = mouseXInMap;
            int mouseYImMapOld = mouseYInMap;
            mouseXInMap = mouseMoveXInWindow + cameraX_inMap;
            mouseYInMap = mouseMoveYInWindow + cameraY_inMap;
            if (this.currentMap == null)
            {
                return;
            }
            //鼠标拖动抬起时------------------------------------------(剪贴板)
            if (e.Button.Equals(MouseButtons.None))
            {
                int xPEN_T = mouseMoveXInWindow - ((mouseMoveXInWindow + cameraX_inMap) % zoomTileW);
                int yPEN_T = mouseMoveYInWindow - ((mouseMoveYInWindow + cameraY_inMap) % zoomTileH);
                if (xPEN_T != xPEN || yPEN != yPEN_T)
                {
                    this.showFunction("当前方格坐标：" + "(" + mouseXInMap / zoomTileW + "," + mouseYInMap / zoomTileH + ")");
                    MapTileElement tile = currentMap.getTile(mouseXInMap / zoomTileW, mouseYInMap / zoomTileH);
                    if (tile != null)
                    {
                        if (Consts.currentLevel == Consts.LEVEL_TILE_OBJ)
                        {
                            currentActiveObj = tile.tile_object_bg;
                        }
                        else if (Consts.currentLevel == Consts.LEVEL_OBJ_MASK)
                        {
                            currentActiveObj = tile.tile_object_mask;
                        }
                        else if (Consts.currentLevel == Consts.LEVEL_OBJ_TRIGEER)
                        {
                            currentActiveObj = tile.tile_objectList[currentStageID];
                        }
                        else
                        {
                            currentActiveObj = null;
                        }
                    }
                    this.updateMap();
                }
                return;
            }
            //鼠标按下后拖动------------------------------------------(绘图、框选和擦除)
            if (!e.Button.Equals(MouseButtons.Left))
            {
                return;
            }
            if (currentMap == null)
            {
                MessageBox.Show("请先在“地图列表”中创建地图");
                return;
            }
            //Console.WriteLine("move:"+e.X+","+e.Y);
            Object currentFocusElement = getCurrentFocusElement();
            //执行操作
            switch (this.editMode)
            {
                case EDITMOD_PENCIL://铅笔模式
                    if (currentFocusElement != null)
                    {
                        fillLine(mapBufferW, mapBufferH, zoomLevel, mouseXImMapOld, mouseYImMapOld, mouseXInMap, mouseYInMap, Consts.currentLevel, currentFocusElement);
                        updateMap();
                    }
                    break;
                case EDITMOD_RECT_SELECT://框选模式
                case EDITMOD_STRAW://吸管模式
                    if (countRectSelect())
                    {
                        updateMap();
                    }
                    break;
                case EDITMOD_ERASER://橡皮模式
                    fillLine(mapBufferW, mapBufferH, zoomLevel, mouseXImMapOld, mouseYImMapOld, mouseXInMap, mouseYInMap, Consts.currentLevel, null);
                    updateMap();
                    break;
            }
        }
        private void pictureBox_Canvas_MouseLeave(object sender, EventArgs e)
        {
            paintPenRect = false;
            this.updateMap();
        }
        
        //获得当前焦点元素
        private Object getCurrentFocusElement()
        {
            switch (Consts.currentLevel)
            {
                case Consts.LEVEL_PHYSICS:
                    return currentTile_Phy;
                case Consts.LEVEL_TILE_BG:
                case Consts.LEVEL_TILE_SUR:
                    return new TransTileGfxElement(currentTile_Gfx,0);
                case Consts.LEVEL_TILE_OBJ:
                case Consts.LEVEL_OBJ_MASK:
                case Consts.LEVEL_OBJ_TRIGEER:
                    return currentAntetype;
            }
            return null;
        }
        //按键事件
        private void pictureBox_Canvas_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //模式切换
            if (!e.Control && !e.Shift)
            {
                if (e.KeyValue == (int)Keys.B)
                {
                    setEditMode(EDITMOD_PENCIL);
                }
                if (e.KeyValue == (int)Keys.M)
                {
                    setEditMode(EDITMOD_RECT_SELECT);
                }
                if (e.KeyValue == (int)Keys.E)
                {
                    setEditMode(EDITMOD_ERASER);
                }
                if (e.KeyValue == (int)Keys.I)
                {
                    setEditMode(EDITMOD_STRAW);
                }
                if (e.KeyValue == (int)Keys.F)
                {
                    TSB_fillCorner.Checked = !TSB_fillCorner.Checked;
                }
            }
            //删除
            if (e.KeyValue == (int)Keys.Delete)
            {
                if (this.editMode == EDITMOD_RECT_SELECT)
                {
                    if (deleteRectSelect())
                    {
                        this.updateMap();
                    }
                }
                return;
            }
            //复制
            if (e.Control && e.KeyValue == (int)Keys.C)
            {
                copyRectSelect();
                return;
            }
            //粘贴
            if (e.Control && e.KeyValue == (int)Keys.V)
            {
                pasteRectSelect();
                updateMap();
                return;
            }
            //移动光标
            if (this.editMode == EDITMOD_PENCIL)
            {
                if (this.tabControl_Center_L_T.SelectedIndex == 1)//目前选择的是图形方格容器
                {
                    switch (e.KeyValue)
                    {
                        case (int)Keys.W:
                            if (e.Alt || e.Control)
                            {
                                break;
                            }
                            scanGfxTile(-pictureBox_Gfx.Width / currentTile_Gfx.clipRect.Width);
                            return;
                        case (int)Keys.S:
                            if (e.Alt || e.Control)
                            {
                                break;
                            }
                            scanGfxTile(pictureBox_Gfx.Width / currentTile_Gfx.clipRect.Width);
                            return;
                        case (int)Keys.A:
                            if (e.Alt || e.Control)
                            {
                                break;
                            }
                            scanGfxTile(-1);
                            return;
                        case (int)Keys.D:
                            if (e.Alt || e.Control)
                            {
                                break;
                            }
                            scanGfxTile(1);
                            return;
                    }
                }
            }
            //粘贴时针对图形层的翻转变换
            if (inPasting && MathUtil.inRegionClose(copyedLevel, Consts.LEVEL_TILE_BG, Consts.LEVEL_TILE_SUR))
            {
                byte tansType=Consts.TRANS_NONE;
                //改变剪贴板内容
                int newCopyedW = copyedW;
                int newCopyedH = copyedH;
                switch (e.KeyValue)
                {
                    case (int)Keys.Up:
                    case (int)Keys.Down:
                        tansType=Consts.TRANS_MIRROR_ROT180;
                        break;
                    case (int)Keys.Left:
                    case (int)Keys.Right:
                        if(e.Control)
                        {
                            if(e.KeyValue==(int)Keys.Left)
                            {
                                tansType=Consts.TRANS_ROT270;
                            }
                            else
                            {
                                tansType=Consts.TRANS_ROT90;
                            }
                            newCopyedW = copyedH;
                            newCopyedH = copyedW;
                        }
                        else
                        {
                            tansType=Consts.TRANS_MIRROR;
                        }
                        break;
                }
                if(tansType!=Consts.TRANS_NONE)
                {
                    //生成新的缓冲
                    Object[,] newClipBoard = new Object[newCopyedW, newCopyedH];
                    for (int i = 0; i < copyedW; i++)
                    {
                        for (int j = 0; j < copyedH; j++)
                        {
                            //源坐标
                            float xOld = i + 0.5f - copyedW / 2.0f;
                            float yOld = j + 0.5f - copyedH / 2.0f;
                            //新坐标
                            float xNew = xOld;
                            float yNew = yOld;
                            //变换坐标
                            switch (tansType)
                            {
                                case Consts.TRANS_MIRROR_ROT180://垂直翻转
                                    yNew = -yOld;
                                    break;
                                case Consts.TRANS_MIRROR://水平翻转
                                    xNew = -xOld;
                                    break;
                                case Consts.TRANS_ROT270://向左旋转90度
                                    xNew = yOld;
                                    yNew = -xOld;
                                    break;
                                case Consts.TRANS_ROT90://向右旋转90度
                                    xNew = -yOld;
                                    yNew = xOld;
                                    break;
                            }
                            //将引用移至变换后位置
                            int newI = (int)(xNew + newCopyedW / 2.0f - 0.5f);
                            int newJ = (int)(yNew + newCopyedH / 2.0f - 0.5f);
                            newClipBoard[newI, newJ] = clipBoard[i, j];
                            TransTileGfxElement gfxElement = (TransTileGfxElement)newClipBoard[newI, newJ];
                            if (gfxElement != null)
                            {
                                //变换翻转信息
                                byte newFlag = gfxElement.transFlag;
                                newFlag = Consts.getTransFlag(newFlag, tansType);
                                gfxElement.transFlag = newFlag;
                            }
                        }
                }
                //更新引用
                clipBoard = newClipBoard;
                copyedW = newCopyedW;
                copyedH = newCopyedH;
                //重新生成缓冲
                generateCBImage();
                updateMap();
                }

            }

        }
        //删除框选内容
        private bool deleteRectSelect()
        {
            if (this.editMode != EDITMOD_RECT_SELECT||this.currentMap==null)
            {
                return false;
            }
            return fillRect(mapBufferW, mapBufferH, zoomLevel, frameX_0, frameY_0, frameX_1, frameY_1, Consts.currentLevel, null);
        }
        //填充框选内容
        private bool fillRectSelect()
        {
            if (this.editMode != EDITMOD_RECT_SELECT || this.currentMap == null)
            {
                return false;
            }
            Object currentFocusElement = getCurrentFocusElement();
            return fillRect(mapBufferW, mapBufferH, zoomLevel, frameX_0, frameY_0, frameX_1, frameY_1, Consts.currentLevel, currentFocusElement);
        }
        //计算框选坐标
        private bool countRectSelect()
        {
            if(this.currentMap==null)
            {
                return false;
            }
            int frameX_0_Old = frameX_0;
            int frameY_0_Old = frameY_0;
            int frameX_1_Old = frameX_1;
            int frameY_1_Old = frameY_1;
            frameX_0 = (mouseDownXInWindow + cameraX_inMap) / zoomTileW;
            frameY_0 = (mouseDownYInWindow + cameraY_inMap) / zoomTileH;
            frameX_1 = (mouseMoveXInWindow + cameraX_inMap) / zoomTileW;
            frameY_1 = (mouseMoveYInWindow + cameraY_inMap) / zoomTileH;
            frameX_0 = MathUtil.limitNumber(frameX_0, 0, currentMap.getMapW() - 1);
            frameX_1 = MathUtil.limitNumber(frameX_1, 0, currentMap.getMapW() - 1);
            frameY_0 = MathUtil.limitNumber(frameY_0, 0, currentMap.getMapH() - 1);
            frameY_1 = MathUtil.limitNumber(frameY_1, 0, currentMap.getMapH() - 1);
            if (frameX_0_Old != frameX_0 || frameY_0_Old != frameY_0 || frameX_1_Old != frameX_1 || frameY_1_Old != frameY_1)
            {
                return true;
            }
            return false;
        }

        //复位框选坐标
        private void resetRectSelect()
        {
            frameX_0 = -1;
            frameY_0 = -1;
            frameX_1 = -1;
            frameY_1 = -1;
        }
        //切换鼠标模式
        private void setEditMode(byte mode)
        {
            if (mode < EDITMOD_PENCIL || mode > EDITMOD_STRAW)
            {
                return;
            }
            editMode = mode;
            if (editMode == EDITMOD_PENCIL)
            {
                TSB_pencil.Checked = true;
            }
            else
            {
                TSB_pencil.Checked = false;
            }
            if (editMode == EDITMOD_RECT_SELECT)
            {
                TSB_rectSelect.Checked = true;
            }
            else
            {
                TSB_rectSelect.Checked = false;
            }
            if (editMode == EDITMOD_ERASER)
            {
                TSB_pointSelect.Checked = true;
            }
            else
            {
                TSB_pointSelect.Checked = false;
            }
            if (editMode == EDITMOD_STRAW)
            {
                TSB_Straw.Checked = true;
            }
            else
            {
                TSB_Straw.Checked = false;
            }
            setMouseCursor();
            //
            if (editMode != EDITMOD_RECT_SELECT)
            {
                resetRectSelect();
                inPasting = false;
                updateMap();
            }
        }
        //设置鼠标指针
        private void setMouseCursor()
        {
            try
            {
                switch (editMode)
                {
                    case EDITMOD_PENCIL:
                        pictureBox_Canvas.Cursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.pencil.ico"));
                        break;
                    case EDITMOD_RECT_SELECT:
                        pictureBox_Canvas.Cursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.rectSelect.ico"));
                        break;
                    case EDITMOD_ERASER:
                        pictureBox_Canvas.Cursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.eraser.ico"));
                        break;
                    case EDITMOD_STRAW:
                        pictureBox_Canvas.Cursor = new Cursor(this.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.straw.ico"));
                        break;
                    default:
                        pictureBox_Canvas.Cursor = Cursors.Default;
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //复制选框内容
        private const int CB_SIZE=50;
        private Object[,] clipBoard = new Object[CB_SIZE, CB_SIZE];
        private int copyedW = 0;//复制区域大小
        private int copyedH = 0;
        private short copyedLevel = -1;//源图层
        int xPEN = 0;//笔触坐标(被规范到单元格)
        int yPEN = 0;
        private Image imgBuffer_CB = null;//剪贴板图形
        private void copyRectSelect()
        {
            if ((this.editMode != EDITMOD_RECT_SELECT && this.editMode != EDITMOD_STRAW) || currentMap == null)
            {
                return;
            }
            if (frameX_0 < 0 || frameY_0 < 0 || frameX_1 < 0 || frameY_1 < 0)
            {
                return;
            }
            //计算参数
            int xMin = Math.Min(frameX_0, frameX_1);
            int yMin = Math.Min(frameY_0, frameY_1);
            int xMax = Math.Max(frameX_0, frameX_1);
            int yMax = Math.Max(frameY_0, frameY_1);
            copyedW = xMax - xMin + 1;
            copyedH = yMax - yMin + 1;
            //重设缓存大小
            clipBoard = new Object[copyedW, copyedH];
            //复制数据
            for (int i = 0; i < copyedW; i++)
            {
                for (int j = 0; j < copyedH; j++)
                {
                    clipBoard[i, j] = currentMap.getTileClone(xMin + i, yMin + j, Consts.currentLevel, currentStageID);
                }
            }
            copyedLevel = Consts.currentLevel;
            generateCBImage();
        }
        //生成剪贴板内容缓冲
        private void generateCBImage()
        {
            //准备剪贴板缓冲
            int bufferWidth = copyedW * TileW;
            int bufferHeight = copyedH * TileH;
            if (imgBuffer_CB == null || imgBuffer_CB.Width < bufferWidth || imgBuffer_CB.Height < bufferHeight)
            {
                imgBuffer_CB = new Bitmap(bufferWidth, bufferHeight);
            }
            Graphics g = Graphics.FromImage(imgBuffer_CB);
            g.Clear(Color.Transparent);
            int zoomLevel = 1;
            for (int i = 0; i < copyedW; i++)
            {
                for (int j = 0; j < copyedH; j++)
                {
                    if (clipBoard[i, j] == null)
                    {
                        continue;
                    }
                    switch (copyedLevel)
                    {
                        case Consts.LEVEL_PHYSICS:
                            TilePhysicsElement phyElement = (TilePhysicsElement)clipBoard[i, j];
                            phyElement.display(g, i * TileW, j * TileH, TileW, TileH, Consts.showStringInPhyLevel);
                            break;
                        case Consts.LEVEL_TILE_BG:
                        case Consts.LEVEL_TILE_SUR:
                            TransTileGfxElement gfxElement = (TransTileGfxElement)clipBoard[i, j];
                            byte flag = gfxElement.tileGfxElement.getTansFlag();
                            flag = Consts.getTransFlag(flag, gfxElement.transFlag);
                            gfxElement.tileGfxElement.display(g, i * TileW, j * TileH, zoomLevel, flag, null);
                            break;
                        case Consts.LEVEL_TILE_OBJ:
                        case Consts.LEVEL_OBJ_MASK:
                        case Consts.LEVEL_OBJ_TRIGEER:
                            TileObjectElement objElement = (TileObjectElement)clipBoard[i, j];
                            objElement.display(g, i * TileW, j * TileH, zoomLevel);
                            break;
                    }
                }
            }
            g.Dispose();
            g = null;
        }
        //清除剪贴板
        private void clearClipboard()
        {
             copyedW = 0;
             copyedH = 0;
             copyedLevel = -1;
             inPasting = false;
        }
        //粘贴框选内容
        bool inPasting = false;//是否等待粘贴
        private void pasteRectSelect()
        {
            resetRectSelect();
            if (!(copyedW > 0 && copyedH > 0))
            {
                return;
            }
            if (
                (MathUtil.inRegionClose(copyedLevel, Consts.LEVEL_PHYSICS, Consts.LEVEL_PHYSICS) && !MathUtil.inRegionClose(Consts.currentLevel, Consts.LEVEL_PHYSICS, Consts.LEVEL_PHYSICS)) ||
                (MathUtil.inRegionClose(copyedLevel, Consts.LEVEL_TILE_BG, Consts.LEVEL_TILE_SUR) && !MathUtil.inRegionClose(Consts.currentLevel, Consts.LEVEL_TILE_BG, Consts.LEVEL_TILE_SUR)) ||
                (MathUtil.inRegionClose(copyedLevel, Consts.LEVEL_TILE_OBJ, Consts.LEVEL_TILE_OBJ) && !MathUtil.inRegionClose(Consts.currentLevel, Consts.LEVEL_TILE_OBJ, Consts.LEVEL_OBJ_TRIGEER)) || currentMap == null)
            {
                return;
            }
            //检查剪贴板
            for (int i = 0; i < copyedW; i++)
            {
                for (int j = 0; j < copyedH; j++)
                {
                    if (clipBoard[i, j] !=null && !mapsManager.includeElement(clipBoard[i, j]))//索引源已经被删除
                    {
                        clearClipboard();
                        return;
                    }
                }
            }
            inPasting = true;
        }
        //完成粘贴
        private void pasteRectFinished(int x,int y)
        {
            if(currentMap==null||!inPasting)
            {
                clearClipboard();
                return;
            }
            this.inRemTileEdit = true;
            this.fillPointsCmd = new FillRandomPointsCommand(currentMap);
            //粘贴到目标位置
            for (int i = 0; i < copyedW; i++)
            {
                for (int j = 0; j < copyedH; j++)
                {
                    if (clipBoard[i, j] != null)
                    {
                        bool inBuffer = false;
                        int alignedX0 = cameraX_inMap / zoomTileW;
                        int alignedX1 = alignedX0 + mapWindowW / zoomTileW + 1;
                        int alignedY0 = cameraY_inMap / zoomTileH;
                        int alignedY1 = alignedY0 + mapWindowH / zoomTileH + 1;
                        if (x + i >= alignedX0 && x + i <= alignedX1
                            && y + j >= alignedY0 && y + j <= alignedY1)
                        {
                            inBuffer = true;
                        }
                        if (clipBoard[i, j] != null)
                        {
                            Object destObj=clipBoard[i, j];
                            if (destObj is TileObjectElement)//这里进行了特殊处理，可能需要修改
                            {
                                if (((TileObjectElement)destObj).antetype == null)
                                {
                                    continue;
                                }
                            }
                            else if (destObj is TransTileGfxElement)
                            {
                                destObj = ((TransTileGfxElement)destObj).Clone();
                            }
                            fillPoint(currentMap, mapBufferW, mapBufferH, zoomLevel, x + i, y + j, Consts.currentLevel, destObj, inBuffer, currentStageID);
                        }
                    }
                }
            }
            this.m_HistoryManager.AddUndoCommand(fillPointsCmd,this);
            this.inRemTileEdit = false;
            inPasting = false;
        }
        //自动地形处理========================================================================================================
        //获取当前地图，底层地形层或者融合地形层指定位置的单元ID，如果不存在指定单元则返回-2，如果指定单元为空则返回-1
        public int getGfxTileID(int x,int y)
        {
            if (currentMap == null || x < 0 || y < 0 || x >= currentMap.getMapW() || y >= currentMap.getMapH())
            {
                return -2;
            }
            if (Consts.currentLevel != Consts.LEVEL_TILE_BG && Consts.currentLevel != Consts.LEVEL_TILE_SUR)
            {
                return -1;
            }
            TileGfxElement element = null;
            if (Consts.currentLevel == Consts.LEVEL_TILE_BG && currentMap.getTile(x, y).tile_gfx_ground != null)
            {
                element = currentMap.getTile(x, y).tile_gfx_ground.tileGfxElement;
            }
            else if (Consts.currentLevel == Consts.LEVEL_TILE_SUR && currentMap.getTile(x, y).tile_gfx_surface != null)
            {
                element = currentMap.getTile(x, y).tile_gfx_surface.tileGfxElement;
            }
            if (element == null)
            {
                return -1;
            }
            return element.GetID();
        }

        //自动地形单元矢量对应表(指定矢量模式下会出现的块ID)
        VectorModel[] autoTileVector =
        {
            //4个顶点8个矢量数据(左上角、右上角、左下角、右下角)
            new VectorModel( 1, 1, 0, 1, 1, 0, 0, 0),//0
            new VectorModel( 0, 1, 0, 1, 0, 0, 0, 0),//1
            new VectorModel( 0, 1, 0, 1, 0, 0, 0, 0),//2
            new VectorModel( 0, 1, 0, 1, 0, 0, 0, 0),//3
            new VectorModel( 0, 1, 0, 1, 0, 0, 0, 0),//4
            new VectorModel( 0, 1,-1, 1, 0, 0,-1, 0),//5

            new VectorModel( 1, 0, 0, 0, 1, 0, 0, 0),//6
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//7
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//8
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//9
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//10
            new VectorModel( 0, 0,-1, 0, 0, 0,-1, 0),//11

            new VectorModel( 1, 0, 0, 0, 1, 0, 0, 0),//12
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//13
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//14
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//15
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//16
            new VectorModel( 0, 0,-1, 0, 0, 0,-1, 0),//17

            new VectorModel( 1, 0, 0, 0, 1, 0, 0, 0),//18
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//19
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//20
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//21
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//22
            new VectorModel( 0, 0,-1, 0, 0, 0,-1, 0),//23

            new VectorModel( 1, 0, 0, 0, 1, 0, 0, 0),//24
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//25
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//26
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//27
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//28
            new VectorModel( 0, 0,-1, 0, 0, 0,-1, 0),//29

            new VectorModel( 1, 0, 0, 0, 1,-1, 0,-1),//30
            new VectorModel( 0, 0, 0, 0, 0,-1, 0,-1),//31
            new VectorModel( 0, 0, 0, 0, 0,-1, 0,-1),//32
            new VectorModel( 0, 0, 0, 0, 0,-1, 0,-1),//33
            new VectorModel( 0, 0, 0, 0, 0,-1, 0,-1),//34
            new VectorModel( 0, 0,-1, 0, 0,-1,-1,-1),//35

            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//36
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//37
            new VectorModel( 0, 0, 0, 0, 0,-1, 0,-1),//38
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//39
            new VectorModel( 8, 8, 8, 8, 8, 8, 8, 8),//40
            new VectorModel( 1, 0, 0, 0, 1, 0, 0, 0),//41
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//42
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//43
            new VectorModel( 8, 8, 8, 8, 8, 8, 8, 8),//44
            new VectorModel( 1, 0, 0, 0, 1, 0, 0, 0),//45
            new VectorModel( 0, 1, 0, 1, 0, 0, 0, 0),//46
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//47
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//48
            new VectorModel( 0, 0, 0, 0, 0,-1, 0,-1),//49
            new VectorModel( 0, 0,-1, 0, 0, 0,-1, 0),//50
            new VectorModel( 8, 8, 8, 8, 8, 8, 8, 8),//51
            new VectorModel( 0, 0, 0, 0, 0, 0, 0, 0),//52
            new VectorModel( 0, 1, 0, 1, 0, 0, 0, 0),//53
            new VectorModel( 0, 0,-1, 0, 0, 0,-1, 0),//54
            new VectorModel( 8, 8, 8, 8, 8, 8, 8, 8),//55
        };
        VectorModel vEmpty = new VectorModel(VectorModel.v_EMPTY, VectorModel.v_EMPTY, VectorModel.v_EMPTY,
                                             VectorModel.v_EMPTY, VectorModel.v_EMPTY, VectorModel.v_EMPTY,
                                             VectorModel.v_EMPTY, VectorModel.v_EMPTY);//空矢量单元
        VectorModel vNull = new VectorModel(VectorModel.v_NULL, VectorModel.v_NULL, VectorModel.v_NULL,
                                            VectorModel.v_NULL, VectorModel.v_NULL, VectorModel.v_NULL,
                                            VectorModel.v_NULL, VectorModel.v_NULL);//场景外的矢量单元
        VectorModel vZero = new VectorModel(0, 0, 0, 0, 0, 0,0, 0);//零矢量单元
        VectorModel vCorner= new VectorModel( 8, 8, 8, 8, 8, 8, 8, 8);//拐角矢量单元
        //当前填充图块周围的矢量环境状态表
        static VectorModel[][] vectorEnviroment;
        public static void initVectorEnviroment()
        {
            if (vectorEnviroment == null)
            {
                vectorEnviroment = new VectorModel[8][];
                for(int i=0;i<vectorEnviroment.Length;i++)
                {
                    vectorEnviroment[i]=new VectorModel[8];
                    for (int j = 0; j < vectorEnviroment[i].Length; j++)
                    {
                        vectorEnviroment[i][j] = new VectorModel();

                    }
                }
            }
        }
        //更新当前填充图块周围的矢量环境到矢量环境状态表，并填入一个点状模型矢量阵列
        public void updateVectorEnviroment(int x,int y,bool blank)
        {
            //得到填充单元双倍方格左上角和状态表地图影响区在地图中对应左上角
            x = x - x % 2 - 3;
            y = y - y % 2 - 3;
            //更新当前填充图块周围的矢量环境到矢量环境状态表
            int idAutoTileOrg = currentTile_Gfx.GetID();
            int rowCount = pictureBox_Gfx.Width / currentMap.getTileW();
            int idAutoTileX = idAutoTileOrg % rowCount;
            int idAutoTileY = idAutoTileOrg / rowCount;
            for (int i = 0; i < vectorEnviroment.Length; i++)
            {
                for (int j = 0; j < vectorEnviroment[i].Length; j++)
                {
                    int idTile = getGfxTileID(x + j, y + i);
                    int idTileX = idTile % rowCount;
                    int idTileY = idTile / rowCount;
                    int toOrgX = idTileX - idAutoTileX;
                    int toOrgY = idTileY - idAutoTileY;
                    int idToOrg = toOrgX + toOrgY * 6;
                    VectorModel vI =getVectorByID(toOrgX, toOrgY);
                    if (idTile <= -2)
                    {
                        vectorEnviroment[i][j].setValue(vNull);
                    }
                    else if (toOrgX >= 0 && toOrgX < 6 && toOrgY >= 0 && toOrgY < 10 && idToOrg <= 55
                        && vI != null)
                    {
                        vectorEnviroment[i][j].setValue(vI);
                    }
                    else
                    {
                        vectorEnviroment[i][j].setValue(vEmpty);
                    }
                }
            }
            int xCLT = 3;
            int yCLT = 3;
            int forCorner = 0;
            VectorModel vCompute = getVectorInEviroment(xCLT, yCLT);
            if (!blank)
            {
                //填入一个四单元点状模型矢量阵列，如果允许更改，要先重新初始化四单元点状模型矢量阵列
                if (vCompute.equalsValue(vEmpty))
                {
                    vectorEnviroment[yCLT][xCLT].setValue(0, 0, 0, 0, 0, 0, 0, 0);
                    addVectorIntoEnviroment(xCLT, yCLT, 4, -1, 0, 6, 1);
                    addVectorIntoEnviroment(xCLT, yCLT, 0, -1, 0, 2, 1);
                    addVectorIntoEnviroment(xCLT, yCLT, 1, 0, -1, 5, 1);
                    addVectorIntoEnviroment(xCLT, yCLT, 3, 0, -1, 7, 1);
                }
                else if (vCompute.equalsValue(vCorner))
                {
                    forCorner++;
                }
                vCompute = getVectorInEviroment(xCLT + 1, yCLT);
                if (vCompute.equalsValue(vEmpty))
                {
                    vectorEnviroment[yCLT][xCLT + 1].setValue(0, 0, 0, 0, 0, 0, 0, 0);
                    addVectorIntoEnviroment(xCLT + 1, yCLT, 1, 0, -1, 5, 1);
                    addVectorIntoEnviroment(xCLT + 1, yCLT, 3, 0, -1, 7, 1);
                    addVectorIntoEnviroment(xCLT + 1, yCLT, 2, 1, 0, 0, -1);
                    addVectorIntoEnviroment(xCLT + 1, yCLT, 6, 1, 0, 4, -1);
                }
                else if (vCompute.equalsValue(vCorner))
                {
                    forCorner++;
                }
                vCompute = getVectorInEviroment(xCLT + 1, yCLT + 1);
                if (vCompute.equalsValue(vEmpty))
                {
                    vectorEnviroment[yCLT + 1][xCLT + 1].setValue(0, 0, 0, 0, 0, 0, 0, 0);
                    addVectorIntoEnviroment(xCLT + 1, yCLT + 1, 2, 1, 0, 0, -1);
                    addVectorIntoEnviroment(xCLT + 1, yCLT + 1, 6, 1, 0, 4, -1);
                    addVectorIntoEnviroment(xCLT + 1, yCLT + 1, 7, 0, 1, 3, -1);
                    addVectorIntoEnviroment(xCLT + 1, yCLT + 1, 5, 0, 1, 1, -1);
                }
                else if (vCompute.equalsValue(vCorner))
                {
                    forCorner++;
                }
                vCompute = getVectorInEviroment(xCLT, yCLT + 1);
                if (vCompute.equalsValue(vEmpty))
                {
                    vectorEnviroment[yCLT + 1][xCLT].setValue(0, 0, 0, 0, 0, 0, 0, 0);
                    addVectorIntoEnviroment(xCLT, yCLT + 1, 7, 0, 1, 3, -1);
                    addVectorIntoEnviroment(xCLT, yCLT + 1, 5, 0, 1, 1, -1);
                    addVectorIntoEnviroment(xCLT, yCLT + 1, 4, -1, 0, 6, 1);
                    addVectorIntoEnviroment(xCLT, yCLT + 1, 0, -1, 0, 2, 1);
                }
                else if (vCompute.equalsValue(vCorner))
                {
                    forCorner++;
                }
                //检测是否是四角菱形
                if (forCorner == 4)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            int xInEnviroment = xCLT + j;
                            int yInEnviroment = yCLT + i;
                            VectorModel model = getVectorInEviroment(xInEnviroment, yInEnviroment);
                            VectorModel left = getVectorInEviroment(xInEnviroment - 1, yInEnviroment);
                            VectorModel right = getVectorInEviroment(xInEnviroment + 1, yInEnviroment);
                            VectorModel top = getVectorInEviroment(xInEnviroment, yInEnviroment - 1);
                            VectorModel bottom = getVectorInEviroment(xInEnviroment, yInEnviroment + 1);
                            VectorModel topRight = getVectorInEviroment(xInEnviroment + 1, yInEnviroment - 1);
                            VectorModel topLeft = getVectorInEviroment(xInEnviroment - 1, yInEnviroment - 1);
                            VectorModel bottomRight = getVectorInEviroment(xInEnviroment + 1, yInEnviroment + 1);
                            VectorModel bottomLeft = getVectorInEviroment(xInEnviroment - 1, yInEnviroment + 1);
                            model.setValue(vZero);
                            right.setValueAvoid(0, 0, 8);
                            right.setValueAvoid(1, 0, 8);
                            right.setValueAvoid(4, 0, 8);
                            right.setValueAvoid(5, 0, 8);
                            bottom.setValueAvoid(0, 0, 8);
                            bottom.setValueAvoid(1, 0, 8);
                            bottom.setValueAvoid(2, 0, 8);
                            bottom.setValueAvoid(3, 0, 8);
                            left.setValueAvoid(2, 0, 8);
                            left.setValueAvoid(3, 0, 8);
                            left.setValueAvoid(6, 0, 8);
                            left.setValueAvoid(7, 0, 8);
                            top.setValueAvoid(4, 0, 8);
                            top.setValueAvoid(5, 0, 8);
                            top.setValueAvoid(6, 0, 8);
                            top.setValueAvoid(7, 0, 8);
                        }
                    }
                }
            }
            else
            {
                //填入一个四单元空白模型矢量阵列，并将改变其周围矢量
                int xI = xCLT;
                int yI = yCLT;
                if (!vCompute.equalsValue(vEmpty))
                {
                    vectorEnviroment[yI][xI].setValue(vEmpty);
                    vectorEnviroment[yI][xI - 1].setNormalValue(2, -1);
                    vectorEnviroment[yI][xI - 1].setNormalValue(6, -1);
                    vectorEnviroment[yI - 1][xI].setNormalValue(5, -1);
                    vectorEnviroment[yI - 1][xI].setNormalValue(7, -1);
                }
                xI = xCLT + 1;
                yI = yCLT;
                vCompute = getVectorInEviroment(xI, yI);
                if (!vCompute.equalsValue(vEmpty))
                {
                    vectorEnviroment[yI][xI].setValue(vEmpty);
                    vectorEnviroment[yI][xI + 1].setNormalValue(0, 1);
                    vectorEnviroment[yI][xI + 1].setNormalValue(4, 1);
                    vectorEnviroment[yI - 1][xI].setNormalValue(5, -1);
                    vectorEnviroment[yI - 1][xI].setNormalValue(7, -1);
                }
                xI = xCLT;
                yI = yCLT + 1;
                vCompute = getVectorInEviroment(xI, yI);
                if (!vCompute.equalsValue(vEmpty))
                {
                    vectorEnviroment[yI][xI].setValue(vEmpty);
                    vectorEnviroment[yI][xI - 1].setNormalValue(2, -1);
                    vectorEnviroment[yI][xI - 1].setNormalValue(6, -1);
                    vectorEnviroment[yI + 1][xI].setNormalValue(1, 1);
                    vectorEnviroment[yI + 1][xI].setNormalValue(3, 1);
                }
                xI = xCLT + 1;
                yI = yCLT + 1;
                vCompute = getVectorInEviroment(xI, yI);
                if (!vCompute.equalsValue(vEmpty))
                {
                    vectorEnviroment[yI][xI].setValue(vEmpty);
                    vectorEnviroment[yI][xI + 1].setNormalValue(0, 1);
                    vectorEnviroment[yI][xI + 1].setNormalValue(4, 1);
                    vectorEnviroment[yI + 1][xI].setNormalValue(1, 1);
                    vectorEnviroment[yI + 1][xI].setNormalValue(3, 1);
                }
            }
            //检测是否需要替换某些拐角单元
            for (int yInEnviroment = 1; yInEnviroment < 7; yInEnviroment++)
            {
                for (int xInEnviroment = 1; xInEnviroment < 7; xInEnviroment++)
                {
                    VectorModel model = getVectorInEviroment(xInEnviroment, yInEnviroment);
                    VectorModel left = getVectorInEviroment(xInEnviroment - 1, yInEnviroment);
                    VectorModel right = getVectorInEviroment(xInEnviroment + 1, yInEnviroment);
                    VectorModel top = getVectorInEviroment(xInEnviroment, yInEnviroment - 1);
                    VectorModel bottom = getVectorInEviroment(xInEnviroment, yInEnviroment + 1);
                    VectorModel topRight = getVectorInEviroment(xInEnviroment + 1, yInEnviroment - 1);
                    VectorModel topLeft = getVectorInEviroment(xInEnviroment - 1, yInEnviroment - 1);
                    VectorModel bottomRight = getVectorInEviroment(xInEnviroment + 1, yInEnviroment + 1);
                    VectorModel bottomLeft = getVectorInEviroment(xInEnviroment - 1, yInEnviroment + 1);
                    if (vCorner.equalsValue(model))
                    {
                        //检测拐角单元是否应该被填充
                        if (!blank)
                        {
                            if (top.equalsValue(autoTileVector[31]) && left.equalsValue(autoTileVector[11]))
                            {
                                if ((vEmpty.equalsValue(right) || vCorner.equalsValue(right)) &&
                                    (vEmpty.equalsValue(bottom) || vCorner.equalsValue(bottom)) &&
                                    (vEmpty.equalsValue(bottomRight) || vCorner.equalsValue(bottomRight)))
                                {
                                    continue;
                                }
                            }
                            if (top.equalsValue(autoTileVector[31]) && right.equalsValue(autoTileVector[6]))
                            {
                                if ((vEmpty.equalsValue(left) || vCorner.equalsValue(left)) &&
                                    (vEmpty.equalsValue(bottom) || vCorner.equalsValue(bottom)) &&
                                    (vEmpty.equalsValue(bottomLeft) || vCorner.equalsValue(bottomLeft)))
                                {
                                    continue;
                                }
                            }
                            if (bottom.equalsValue(autoTileVector[1]) && left.equalsValue(autoTileVector[11]))
                            {
                                if ((vEmpty.equalsValue(right) || vCorner.equalsValue(right)) &&
                                    (vEmpty.equalsValue(top) || vCorner.equalsValue(top)) &&
                                    (vEmpty.equalsValue(topRight) || vCorner.equalsValue(topRight)))
                                {
                                    continue;
                                }
                            }
                            if (bottom.equalsValue(autoTileVector[1]) && right.equalsValue(autoTileVector[6]))
                            {
                                if ((vEmpty.equalsValue(left) || vCorner.equalsValue(left)) &&
                                    (vEmpty.equalsValue(top) || vCorner.equalsValue(top)) &&
                                    (vEmpty.equalsValue(topLeft) || vCorner.equalsValue(topLeft)))
                                {
                                    continue;
                                }
                            }
                            model.setValue(vZero);
                            right.setValueAvoid(0, 0, 8);
                            right.setValueAvoid(1, 0, 8);
                            right.setValueAvoid(4, 0, 8);
                            right.setValueAvoid(5, 0, 8);
                            bottom.setValueAvoid(0, 0, 8);
                            bottom.setValueAvoid(1, 0, 8);
                            bottom.setValueAvoid(2, 0, 8);
                            bottom.setValueAvoid(3, 0, 8);
                            left.setValueAvoid(2, 0, 8);
                            left.setValueAvoid(3, 0, 8);
                            left.setValueAvoid(6, 0, 8);
                            left.setValueAvoid(7, 0, 8);
                            top.setValueAvoid(4, 0, 8);
                            top.setValueAvoid(5, 0, 8);
                            top.setValueAvoid(6, 0, 8);
                            top.setValueAvoid(7, 0, 8);
                        }
                        else//检测拐角单元是否应该被删除
                        {
                            if (top.equalsValue(autoTileVector[31]) && left.equalsValue(autoTileVector[11]))
                            {
                                continue;
                            }
                            if (top.equalsValue(autoTileVector[31]) && right.equalsValue(autoTileVector[6]))
                            {
                                continue;
                            }
                            if (bottom.equalsValue(autoTileVector[1]) && left.equalsValue(autoTileVector[11]))
                            {
                                continue;
                            }
                            if (bottom.equalsValue(autoTileVector[1]) && right.equalsValue(autoTileVector[6]))
                            {
                                continue;
                            }
                            model.setValue(vEmpty);
                        }
                    }
                }
            }
        }

        //在矢量环境状态表增加一个单个矢量
        public void addVectorIntoEnviroment(int x, int y, int id, int cx, int cy, int cid,int value)
        {
            VectorModel vMe = getVectorInEviroment(x, y);//本点
            int vSum = 0;
            //相比较的矢量单元
            VectorModel vCompute = getVectorInEviroment(x + cx, y + cy);
            int vCValue = vCompute.getValue(cid);
            if (vCValue == VectorModel.v_NULL)
            {
                vMe.setValue(id,0);
            }
            else if (vCValue == VectorModel.v_EMPTY || vCValue == 0 || vCValue == VectorModel.v_CORNER)
            {
                vMe.setValue(id, value);
            }
            else
            {
                vSum = (vCompute.getValue(cid) + value) / 2;
                vCompute.setValue(cid,vSum);
                vMe.setValue(id, vSum);
            }
        }
        //根据所在矢量环境表的方格坐标返回矢量环境对象
        public VectorModel getVectorInEviroment(int x, int y)
        {
            if (y < 0 || y >= vectorEnviroment.Length)
            {
                return null;
            }
            if (x < 0 || x >= vectorEnviroment[y].Length)
            {
                return null;
            }
            return vectorEnviroment[y][x];
        }
        //根据相对自动地形单元起始ID返回矢量环境对象
        public VectorModel getVectorByID(int x, int y)
        {
            int id = x + y * 6;
            if (id < 0 || id >= autoTileVector.Length)
            {
                return null;
            }
            return autoTileVector[id];
        }
        //根据所需要的矢量模型、所在地图方格坐标、所在矢量环境表的方格坐标，返回指定的模块相对起始模块ID
        public int getModelID(int xInMap,int yInMap,int xInEnviroment,int yInEnviroment)
        {
            VectorModel model = getVectorInEviroment(xInEnviroment, yInEnviroment);
            VectorModel left = getVectorInEviroment(xInEnviroment - 1, yInEnviroment);
            VectorModel right = getVectorInEviroment(xInEnviroment + 1, yInEnviroment);
            VectorModel top = getVectorInEviroment(xInEnviroment, yInEnviroment - 1);
            VectorModel bottom = getVectorInEviroment(xInEnviroment, yInEnviroment + 1);
            VectorModel topRight = getVectorInEviroment(xInEnviroment + 1, yInEnviroment - 1);
            VectorModel topLeft = getVectorInEviroment(xInEnviroment - 1, yInEnviroment - 1);
            VectorModel bottomRight = getVectorInEviroment(xInEnviroment + 1, yInEnviroment + 1);
            VectorModel bottomLeft = getVectorInEviroment(xInEnviroment - 1, yInEnviroment + 1);
            int id = -1;
            //四个凸角的判断
            if (autoTileVector[0].equalsValue(model))
            {
                return 0;
            }
            if (autoTileVector[5].equalsValue(model))
            {
                return 5;
            }
            if (autoTileVector[30].equalsValue(model))
            {
                return 30;
            }
            if (autoTileVector[35].equalsValue(model))
            {
                return 35;
            }
            //中间格的判断(根据在双倍格的方位)
            if (vZero.equalsValue(model) || vCorner.equalsValue(model))
            {
                if (autoTileVector[6].equalsValue(right) && autoTileVector[1].equalsValue(bottom) && (vEmpty.equalsValue(topLeft) || vCorner.equalsValue(topLeft)))//vEmpty.equalsValue(left) || vEmpty.equalsValue(top)
                {
                    return 40;
                }
                if (autoTileVector[6].equalsValue(right) && autoTileVector[31].equalsValue(top) && (vEmpty.equalsValue(bottomLeft) || vCorner.equalsValue(bottomLeft)))//vEmpty.equalsValue(left) || vEmpty.equalsValue(bottom)
                {
                    return 44;
                }
                if (autoTileVector[11].equalsValue(left) && autoTileVector[1].equalsValue(bottom) && (vEmpty.equalsValue(topRight) || vCorner.equalsValue(topRight)))//vEmpty.equalsValue(right) || vEmpty.equalsValue(top)
                {
                    return 51;
                }
                if (autoTileVector[11].equalsValue(left) && autoTileVector[31].equalsValue(top) && (vEmpty.equalsValue(bottomRight) || vCorner.equalsValue(bottomRight)))//vEmpty.equalsValue(right) || vEmpty.equalsValue(bottom)
                {
                    return 55;
                }
                if (xInMap % 2 == 0)
                {
                    if (yInMap % 2 == 0)//左上角
                    {
                        return 14;
                    }
                    else
                    {
                        return 8; //左下角
                    }
                }
                else
                {
                    if (yInMap % 2 == 0)//右下角
                    {
                        return 13;
                    }
                    else
                    {
                        return 7;
                    }
                }
            }
            //上边缘格的判断
            if (autoTileVector[1].equalsValue(model))
            {
                if (vCorner.equalsValue(top))
                {
                    if (!vEmpty.equalsValue(topRight) && !vCorner.equalsValue(topRight))
                    {
                        return 46;
                    }
                    else
                    {
                        return 53;
                    }
                }
                if (autoTileVector[0].equalsValue(left))//左边是凸左上角
                {
                    return 1;
                }
                if (autoTileVector[5].equalsValue(right))//右边是凸右上角
                {
                    return 4;
                }
                if (xInMap % 2 == 0)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            //下边缘格的判断
            if (autoTileVector[31].equalsValue(model))
            {
                if (vCorner.equalsValue(bottom))
                {
                    if (!vEmpty.equalsValue(bottomRight) && !vCorner.equalsValue(bottomRight))
                    {
                        return 38;
                    }
                    else
                    {
                        return 49;
                    }
                }
                if (autoTileVector[30].equalsValue(left))//左边是凸左下角
                {
                    return 31;
                }
                if (autoTileVector[35].equalsValue(right))//右边是凸右下角
                {
                    return 34;
                }
                if (xInMap % 2 == 0)
                {
                    return 32;
                }
                else
                {
                    return 33;
                }
            }
            //左边缘格的判断
            if (autoTileVector[6].equalsValue(model))
            {
                if (vCorner.equalsValue(left))
                {
                    if (!vEmpty.equalsValue(bottomLeft) && !vCorner.equalsValue(bottomLeft))
                    {
                        return 41;
                    }
                    else
                    {
                        return 45;
                    }
                }
                if (autoTileVector[0].equalsValue(top))//上边是凸左上角
                {
                    return 6;
                }
                if (autoTileVector[30].equalsValue(bottom))//下边是凸左下角
                {
                    return 24;
                }
                if (yInMap % 2 == 0)
                {
                    return 12;
                }
                else
                {
                    return 18;
                }
            }
            //右边缘格的判断
            if (autoTileVector[11].equalsValue(model))
            {
                if (vCorner.equalsValue(right))
                {
                    if (!vEmpty.equalsValue(bottomRight) && !vCorner.equalsValue(bottomRight))
                    {
                        return 50;
                    }
                    else
                    {
                        return 54;
                    }
                }
                if (autoTileVector[5].equalsValue(top))//上边是凸右上角
                {
                    return 11;
                }
                if (autoTileVector[35].equalsValue(bottom))//下边是凸右下角
                {
                    return 29;
                }
                if (yInMap % 2 == 0)
                {
                    return 17;
                }
                else
                {
                    return 23;
                }
            }
            return id;
        }
        //根据相对起始模块ID返回相应的地图方格单元
        public TransTileGfxElement getGfxTileByAutoID(int id)
        {
            if (currentTile_Gfx == null || id < 0 || id >= autoTileVector.Length)
            {
                return null;
            }
            int idStart = currentTile_Gfx.GetID();
            int rowCount = pictureBox_Gfx.Width / currentMap.getTileW();
            int rowCountAutoTile=6;
            int idXoff = id % rowCountAutoTile;
            int idYoff = id / rowCountAutoTile;
            int index = idStart + idXoff + rowCount * idYoff;
            return  new TransTileGfxElement((TileGfxElement)currentGfxContainer[index],0);
        }
        
        public class VectorModel
        {
            public static int v_NULL = 6;
            public static int v_EMPTY = 7;
            public static int v_CORNER = 8;
            public int x1,y1,x2,y2,x3,y3,x4,y4;
            public VectorModel()
            {
            }
            public VectorModel(int x1T, int y1T, int x2T, int y2T, int x3T, int y3T, int x4T, int y4T)
            {
                setValue(x1T, y1T, x2T, y2T, x3T, y3T, x4T, y4T);
            }
            public void setValue(int x1T, int y1T, int x2T, int y2T, int x3T, int y3T, int x4T, int y4T)
            {
                x1 = x1T;
                y1 = y1T;
                x2 = x2T;
                y2 = y2T;
                x3 = x3T;
                y3 = y3T;
                x4 = x4T;
                y4 = y4T;
            }
            public void setValue(VectorModel model)
            {
                setValue(model.x1, model.y1, model.x2, model.y2, model.x3, model.y3, model.x4, model.y4);
            }
            public bool equalsValue(VectorModel model)
            {
                if(model==null)
                {
                    return false;
                }
                return x1==model.x1&&y1==model.y1&&x2==model.x2&&y2==model.y2&&x3==model.x3&&y3==model.y3&&x4==model.x4&&y4==model.y4;
            }
            public int getValue(int id)
            {
                switch (id)
                {
                    case 0:
                        return x1;
                    case 1:
                        return y1;
                    case 2:
                        return x2;
                    case 3:
                        return y2;
                    case 4:
                        return x3;
                    case 5:
                        return y3;
                    case 6:
                        return x4;
                    case 7:
                        return y4;
                }
                return 0;
            }
            public void setValue(int id ,int value)
            {
                switch (id)
                {
                    case 0:
                        x1 = value;
                        break;
                    case 1:
                        y1 = value;
                        break;
                    case 2:
                        x2 = value;
                        break;
                    case 3:
                        y2 = value;
                        break;
                    case 4:
                        x3 = value;
                        break;
                    case 5:
                        y3 = value;
                        break;
                    case 6:
                        x4 = value;
                        break;
                    case 7:
                        y4 = value;
                        break;
                }
            }
            public void setValueAvoid(int id, int value,int avoid)
            {
                int currentValue = getValue(id);
                if (currentValue == avoid)
                {
                    return;
                }
                setValue(id, value);
            }
            public void setNormalValue(int id, int value)
            {
                int currentValue = getValue(id);
                if (currentValue == v_NULL || currentValue == v_EMPTY || currentValue == v_CORNER)
                {
                    return;
                }
                setValue(id, value);
            }

        }

        //使用自动地形填充地图
        public bool fillPointWithAutoTile(MapElement currentMap, int bufW, int bufH, float zoomLevel, int indexX, int indexY, short level, bool drawInBuffer, int currentStageID,bool blank)
        {
            //历史记录
            bool changed = false;
            if (fillPointsCmd != null)
            {
                updateVectorEnviroment(indexX, indexY, blank);
                //得到双倍方格左上角和状态表矢量影响区在地图中对应左上角
                int x = indexX;
                int y = indexY;
                int xCLT = x - x % 2;
                int yCLT = y - y % 2;
                x = xCLT - 2;
                y = yCLT - 2;
                for (int iy = 0; iy < 6; iy++)
                {
                    for (int ix = 0; ix < 6; ix++)
                    {
                        int idx = x + ix;
                        int idy = y + iy;
                        if (idx < 0 || idx >= currentMap.getMapW() || idy < 0 || idy >= currentMap.getMapH())
                        {
                            continue;
                        }
                        Object oldElement = currentMap.getTile(idx, idy, level, currentStageID);
                        int idTile = getModelID( idx, idy, 1 + ix, 1 + iy);
                        Object element = getGfxTileByAutoID(idTile);
                        bool changedOnce = currentMap.fillPoint(gMapBuffer, bufW, bufH, zoomLevel, idx, idy, level, element, drawInBuffer, currentStageID);
                        Object newElement = currentMap.getTile(idx, idy, level, currentStageID);
                        FillPointHistory pointCmd = new FillPointHistory(currentMap, oldElement, newElement, idx, idy, level, currentStageID);
                        fillPointsCmd.addPoint(pointCmd);
                        if (changedOnce)
                        {
                            changed = true;
                        }
                    }
                }
                //再检查内凹角模块
                for (int iy = 0; iy < 6; iy++)
                {
                    for (int ix = 0; ix < 6; ix++)
                    {
                        int idx = x + ix;
                        int idy = y + iy;
                        if (idx < 0 || idx >= currentMap.getMapW() || idy < 0 || idy >= currentMap.getMapH())
                        {
                            continue;
                        }
                        Object oldElement = currentMap.getTile(idx, idy, level, currentStageID);
                        int idXEn = 1 + ix;
                        int idYEn = 1 + iy;
                        VectorModel model = getVectorInEviroment(idXEn, idYEn);
                        if (model == null)
                        {
                            continue;
                        }
                        if (!model.equalsValue(vZero))
                        {
                            continue;
                        }
                        bool fillCorner = TSB_fillCorner.Checked;
                        if (isEnviroment(idXEn, idYEn, -1, 0, 0, -1, -1, -1) && ix % 2 == 0 && iy % 2 == 0)
                        {
                            if (!fillCorner)
                            {
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy, 36);
                            }
                            else
                            {
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy, 47);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx-1, idy, 46);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy-1, 41);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx-1, idy-1, 40);
                            }
                        }
                        else if (isEnviroment(idXEn, idYEn, 1, 0, 0, -1, 1, -1) && ix % 2 == 1 && iy % 2 == 0)
                        {
                            if (!fillCorner)
                            {
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy, 37);
                            }
                            else
                            {
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy, 52);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx + 1, idy, 53);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy - 1, 50);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx + 1, idy - 1, 51);
                            }
                        }
                        else if (isEnviroment(idXEn, idYEn, -1, 0, 0, 1, -1, 1) && ix % 2 == 0 && iy % 2 == 1)
                        {
                            if (!fillCorner)
                            {
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy, 42);
                            }
                            else
                            {
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy, 39);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx -1, idy, 38);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx-1, idy + 1, 44);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy + 1, 45);
                            }
                        }
                        else if (isEnviroment(idXEn, idYEn, 1, 0, 0, 1, 1, 1) && ix % 2 == 1 && iy % 2 == 1)
                        {
                            if (!fillCorner)
                            {
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy, 43);
                            }
                            else
                            {
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy, 48);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx + 1, idy, 49);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx + 1, idy + 1, 55);
                                setAutoTile(currentMap, bufW, bufH, zoomLevel, level, drawInBuffer, currentStageID, idx, idy + 1, 54);
                            }
                        }
                        else
                        {
                            continue;
                        }
                        changed = true;
                    }
                }
            }
            return changed;
        }
        //指定的地图方格填充一个指定自动地形编号的图形单元，这个编号是相对于当前的自动地形起始方格的。
        private void setAutoTile(MapElement currentMap, int bufW, int bufH, float zoomLevel, short level, bool drawInBuffer, int currentStageID, int idx, int idy, int idTile)
        {
            Object oldElement = currentMap.getTile(idx, idy, level, currentStageID);
            Object element = getGfxTileByAutoID(idTile);
            currentMap.fillPoint(gMapBuffer, bufW, bufH, zoomLevel, idx, idy, level, element, drawInBuffer, currentStageID);
            Object newElement = currentMap.getTile(idx, idy, level, currentStageID);
            FillPointHistory pointCmd = new FillPointHistory(currentMap, oldElement, newElement, idx, idy, level, currentStageID);
            fillPointsCmd.addPoint(pointCmd);
        }
        //检查矢量环境表中指定方格的周围环境是否存在情况是指定的数值
        //即前两个矢量单元不为空，但是最后一个矢量单元为空
        public bool  isEnviroment(int x,int y,int x1,int y1,int x2,int y2,int xE,int yE)
        {
            VectorModel model1 = getVectorInEviroment(x + x1, y + y1);
            VectorModel model2 = getVectorInEviroment(x + x2, y + y2);
            VectorModel modelEmpty = getVectorInEviroment(x + xE, y + yE);
            return (!model1.equalsValue(vEmpty)) && (!model2.equalsValue(vEmpty)) && (modelEmpty.equalsValue(vEmpty));

        }
        //点填充地图
        public bool fillPoint(MapElement currentMap, int bufW, int bufH, float zoomLevel, int indexX, int indexY, short level, Object element, bool drawInBuffer, int currentStageID)
        {
            if (currentMap == null)
            {
                return false;
            }
            //对于对象层的填充
            Graphics g = null;
            if (level >= Consts.LEVEL_TILE_OBJ)
            {
                g = gMapBuffer_Obj;
            }
            else
            {
                g = gMapBuffer;
            }
            //历史记录
            bool changed = false;
            if (inRemTileEdit)
            {
                if (fillPointsCmd != null)
                {
                    Object oldElement = currentMap.getTile(indexX, indexY, level, currentStageID);
                    changed = currentMap.fillPoint(g, bufW, bufH, zoomLevel, indexX, indexY, level, element, drawInBuffer, currentStageID);
                    Object newElement = currentMap.getTile(indexX, indexY, level, currentStageID);
                    FillPointHistory pointCmd = new FillPointHistory(currentMap,oldElement, newElement, indexX, indexY, level,currentStageID);
                    fillPointsCmd.addPoint(pointCmd);
                    //Console.WriteLine(indexX + "," + indexY);
                }
            }
            else
            {
                changed = currentMap.fillPoint(g, bufW, bufH, zoomLevel, indexX, indexY, level, element, drawInBuffer, currentStageID);
            }
            return changed;
        }
        //线填充地图
        public bool fillLine(int bufW, int bufH, float zoomLevel, int x1, int y1, int x2, int y2, short level, Object element)
        {
            if (currentMap == null)
            {
                return false;
            }
            if (x1 == x2 && y1 == y2)
            {
                return false;
            }
            bool changed = false;
            bool currentChanged = false;
            int x = x1;
            int y = y1;
            int zoomW = (int)(zoomTileW);
            if (zoomW < 1)
            {
                zoomW = 1;
            }
            int zoomH = (int)(zoomTileH);
            if (zoomH < 1)
            {
                zoomH = 1;
            }
            canAutoTile();
            //Console.WriteLine("fillLine_Phy:" + x1 + "," + y1 +"|"+ x2 + "," + y2);
            if (Math.Abs(x1 - x2) > Math.Abs(y1 - y2))//偏横向
            {
                int dir = (x2 - x1) / Math.Abs(x1 - x2);
                int step = (int)(zoomW);
                int distance = (int)Math.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
                if (distance < step)
                {
                    step = distance;
                }
                step *= dir;
                while (x1 < x2 ? x < x2 : x >= x2)
                {
                    x += step;
                    y = y1 + (x - x1) * (y2 - y1) / (x2 - x1);
                    Object elementT = element;
                    if (elementT is Antetype)
                    {
                        TileObjectElement obj = new TileObjectElement(currentMap.getTile(x / zoomW, y / zoomH));
                        obj.antetype = (Antetype)elementT;
                        elementT = obj;
                    }
                    if (TSB_autoTile.Checked)
                    {
                        currentChanged = fillPointWithAutoTile(currentMap, bufW, bufH, zoomLevel, x / zoomW, y / zoomH, level, true, currentStageID, editMode != EDITMOD_PENCIL);
                    }
                    else
                    {
                        currentChanged = fillPoint(currentMap, bufW, bufH, zoomLevel, x / zoomW, y / zoomH, level, elementT, true, currentStageID);
                    }
                    if (!changed)
                    {
                        changed = currentChanged;
                    }
                }
            }
            else
            {
                int dir = (y2 - y1) / Math.Abs(y1 - y2);
                int step = (int)(zoomH);
                int distance = (int)Math.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
                if (distance < step)
                {
                    step = distance;
                }
                step *= dir;
                while (y1 < y2 ? y < y2 : y >= y2)
                {
                    y += step;
                    x = x1 + (y - y1) * (x2 - x1) / (y2 - y1);
                    Object elementT = element;
                    if (elementT is Antetype)
                    {
                        TileObjectElement obj = new TileObjectElement(currentMap.getTile(x / zoomW, y / zoomH));
                        obj.antetype = (Antetype)elementT;
                        elementT = obj;
                    }
                    if (TSB_autoTile.Checked)
                    {
                        currentChanged = fillPointWithAutoTile(currentMap, bufW, bufH, zoomLevel, x / zoomW, y / zoomH, level, true, currentStageID, editMode != EDITMOD_PENCIL);
                    }
                    else
                    {
                        currentChanged = fillPoint(currentMap, bufW, bufH, zoomLevel, x / zoomW, y / zoomH, level, elementT, true, currentStageID);
                    }

                    if (!changed)
                    {
                        changed = currentChanged;
                    }
                }
            }
            return changed;
        }
        //矩形填充(参数为索引坐标)
        public bool fillRect(int bufW, int bufH, float zoomLevel, int x1, int y1, int x2, int y2, short level, Object element)
        {
            if (currentMap == null)
            {
                return false;
            }
            this.inRemTileEdit = true;
            this.fillPointsCmd = new FillRandomPointsCommand(currentMap);
            int xMin = Math.Min(x1, x2);
            int yMin = Math.Min(y1, y2);
            int xMax = Math.Max(x1, x2);
            int yMax = Math.Max(y1, y2);
            xMin = MathUtil.limitNumber(xMin, 0, currentMap.getMapW() - 1);
            xMax = MathUtil.limitNumber(xMax, 0, currentMap.getMapW() - 1);
            yMin = MathUtil.limitNumber(yMin, 0, currentMap.getMapH() - 1);
            yMax = MathUtil.limitNumber(yMax, 0, currentMap.getMapH() - 1);
            bool changed = false;
            for (int i = xMin; i <= xMax; i++)
            {
                for (int j = yMin; j <= yMax; j++)
                {
                    Object elementT = element;
                    if (elementT is Antetype)
                    {
                        TileObjectElement obj = new TileObjectElement(currentMap.getTile(i, j));
                        obj.antetype = (Antetype)elementT;
                        elementT = obj;
                    }
                    bool changedOnce = false;
                    if (!TSB_autoTile.Checked)
                    {
                        changedOnce = fillPoint(currentMap, bufW, bufH, zoomLevel, i, j, level, elementT, true, currentStageID);
                    }
                    else
                    {
                        changedOnce = fillPointWithAutoTile(currentMap, bufW, bufH, zoomLevel, i, j, level, true, currentStageID, element == null);
                    }
                    if (changedOnce)
                    {
                        changed = true;
                    }
                }
            }
            this.m_HistoryManager.AddUndoCommand(fillPointsCmd,this);
            this.inRemTileEdit = false;
            return changed;
        }
        class FillPointHistory
        {
            private MapElement mapElement;
            private Object tileElement_Org;
            private Object tileElement_New;
            private short tileLevel = -1;
            private int tileIndex_X;
            private int tileIndex_Y;
            private int stageID = 0;
            public FillPointHistory(MapElement mapElementT, Object tileElement_OrgT, Object tileElement_NewT,
                int tileIndex_XT, int tileIndex_YT, short tileLevelT, int stageIDT)
            {
                mapElement = mapElementT;
                tileElement_Org = tileElement_OrgT;
                tileElement_New = tileElement_NewT;
                tileIndex_X = tileIndex_XT;
                tileIndex_Y = tileIndex_YT;
                tileLevel = tileLevelT;
                stageID = stageIDT;
            }
            public MapElement getMapElement()
            {
                return mapElement;
            }
            public Object getTileElement_Org()
            {
                return tileElement_Org;
            }
            public Object getTileElement_New()
            {
                return tileElement_New;
            }
            public int getTileIndex_X()
            {
                return tileIndex_X;
            }
            public int getTileIndex_Y()
            {
                return tileIndex_Y;
            }
            public short getTileLevel()
            {
                return tileLevel;
            }
            public int getStageID()
            {
                return stageID;
            }
        }
        //随机绘制地图
        class FillRandomPointsCommand : UndoCommand
        {
            private ArrayList pointsList = new ArrayList();
            private MapElement mapElement = null;
            public FillRandomPointsCommand(MapElement mapElementT)
            {
                mapElement = mapElementT;
            }
            public void addPoint(FillPointHistory point)
            {
                if (point == null)
                {
                    return;
                }
                pointsList.Add(point);
            }
            public MapElement getMapElement()
            {
                return mapElement;
            }
            public int getPointsCount()
            {
                return pointsList.Count;
            }
            public FillPointHistory getPoint(int index)
            {
                if (index >= pointsList.Count)
                {
                    return null;
                }
                return (FillPointHistory)pointsList[index];
            }
            public override string GetText()
            {
                return "随机绘制地图";
            }
        }
        private void TSB_pencil_Click(object sender, EventArgs e)
        {
            setEditMode(EDITMOD_PENCIL);
        }

        private void TSB_FillIn_Click(object sender, EventArgs e)
        {
            if (fillRectSelect())
            {
                this.updateMap();
            }
        }

        private void TSB_rectSelect_Click(object sender, EventArgs e)
        {
            setEditMode(EDITMOD_RECT_SELECT);
        }

        private void TSB_erase_Click(object sender, EventArgs e)
        {
            setEditMode(EDITMOD_ERASER);
        }
        private void vScrollBar_Canvas_ValueChanged(object sender, EventArgs e)
        {
            updateMap();
        }
        private void hScrollBar_Canvas_ValueChanged(object sender, EventArgs e)
        {
            updateMap();
        }
        private void TSB_Copy_Click(object sender, EventArgs e)
        {
            copyRectSelect();
        }
        private void TSB_Straw_Click(object sender, EventArgs e)
        {
            setEditMode(EDITMOD_STRAW);
        }

        //============================地图物理方格容器部分================================
        //地图缓冲
        public Image mapBuffer_physic = null;
        //物理容器中的焦点单元
        public TilePhysicsElement currentTile_Phy = null;
        //单元尺寸
        private int width_phy = 32;
        private int height_phy = 32;
        private int gap = 2;//间距
        //更新地图缓冲
        public void updateContainer_Physics()
        {
            int windowWidth=pictureBox_Physics.Width;
            int windowHeight=pictureBox_Physics.Height;
            if (windowWidth <= 0|| windowHeight <= 0)
            {
                return;
            }
            VScrollBar bar = this.vScrollBar_Physics;
            if (mapBuffer_physic == null || mapBuffer_physic.Width!=windowWidth|| mapBuffer_physic.Height!=windowHeight)
            {
                mapBuffer_physic = new Bitmap(windowWidth, windowHeight);
            }
            Graphics g = Graphics.FromImage(mapBuffer_physic);
            GraphicsUtil.fillRect(g, 0, 0, windowWidth, windowHeight, Consts.colorDarkGray);
            //绘制到缓冲
            int elementCount=mapsManager.tilePhysicsManager.getElementCount();
            if(elementCount>0)
            {
                int nbX = windowWidth / (width_phy + gap);
                int contentHeight = ((elementCount + nbX - 1) / nbX) * (height_phy + gap);
                int maskedHeight =0;
                if (contentHeight > windowHeight)
                {
                    maskedHeight = (bar.Value - bar.Minimum) * (contentHeight - windowHeight) / (bar.Maximum-9 - bar.Minimum);
                }
                for (int i = (maskedHeight / (height_phy + gap)) * nbX; i < elementCount; i++)
                {
                    int x = (i % nbX) * (width_phy + gap);
                    int y = -maskedHeight + (i / nbX) * (height_phy + gap);
                    if (y > windowHeight)
                    {
                        break;
                    }
                    TilePhysicsElement elementI= mapsManager.tilePhysicsManager.getElement(i);
                    if(elementI!=null)
                    {
                        elementI.display(g, x, y, width_phy, height_phy, true);
                        if (currentTile_Phy != null && currentTile_Phy.Equals(elementI))
                        {
                            GraphicsUtil.drawRect(g, x, y, width_phy, height_phy, Consts.color_border);
                            GraphicsUtil.drawRect(g, x - 1, y - 1, width_phy + 2, height_phy + 2, Consts.colorWhite);
                            GraphicsUtil.drawRect(g, x - 2, y - 2, width_phy + 4, height_phy + 4, Consts.color_border);
                        }
                    }
                }
            }

            //绘制到屏幕
            if (pictureBox_Physics.Image == null || !pictureBox_Physics.Image.Equals(mapBuffer_physic))
            {
                pictureBox_Physics.Image = mapBuffer_physic;
            }
            else
            {
                pictureBox_Physics.Refresh();
            }

        }
        //设置焦点物理单元
        public void setFocusTile_Phy(int index)
        {
            TilePhysicsElement elemn = mapsManager.tilePhysicsManager.getElement(index);
            //if (elemn != null)
            //{
                currentTile_Phy = elemn;
            //}
        }
        //添加物理方格单元
        public void addPhyElement()
        {
            TilePhysicsElement element = SmallDialog_NewTile_Physics.createElement(mapsManager.tilePhysicsManager);
            if (element != null)
            {
                mapsManager.tilePhysicsManager.addElement(element);
            }
        }
        private void button_add_Phy_Click(object sender, EventArgs e)
        {
            addPhyElement();
            updateContainer_Physics();
        }
        //删除物理方格单元
        public bool deletePhyElement()
        {
            if (currentTile_Phy == null)
            {
                return false;
            }
            int index = currentTile_Phy.getID();
            int usedTime=currentTile_Phy.getUsedTime();
            if (usedTime > 0)
            {
                DialogResult res = MessageBox.Show("该单元被" + usedTime + "处引用，确定删除？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (!res.Equals(DialogResult.Yes))
                {
                    return false;
                }
                //删除所有地图中的所有引用
                mapsManager.deleteTileUsed(currentTile_Phy);
                updateMap_Refresh();
                clearHistory();
            }
            clearHistory();
            mapsManager.tilePhysicsManager.removeElement(currentTile_Phy.getID());
            if (index >= mapsManager.tilePhysicsManager.getElementCount())
            {
                index--;
            }
            if (index < 0)
            {
                index = 0;
            }
            setFocusTile_Phy(index);
            clearClipboard();
            return true;

        }
        private void button_del_Phy_Click(object sender, EventArgs e)
        {
            if (currentTile_Phy != null)
            {
                if (deletePhyElement())
                {
                    updateContainer_Physics();
                }
            }

        }
        //自动添加地图物理方格
        private void button_AutoGen_Click(object sender, EventArgs e)
        {
            //if (mapsManager.tilePhysicsManager.getElementCount() != 0)
            //{
            //    MessageBox.Show("已存在物理方格，不能自动添加", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            int color=0xFF;
            short checkValue = 1;
            for (int i = 0; i < this.numericUpDown_autoGen_Phy.Value; i++)
            {
                TilePhysicsElement element = new TilePhysicsElement(mapsManager.tilePhysicsManager);
                while (mapsManager.tilePhysicsManager.isValueExist(checkValue))
                {
                    checkValue++;
                }
                element.setValue(checkValue, color);
                color += ((0x22 * (1 + i / 36)) << 16) + ((0x22 * (1 + (i % 36) / 6)) << 8) + ((0x22 * (1 + (i % 6))));
                if (!mapsManager.tilePhysicsManager.addElement(element))
                {
                    break;
                }
            }
            updateContainer_Physics();
        }
        private void vScrollBar_Physics_ValueChanged(object sender, EventArgs e)
        {
            updateContainer_Physics();
        }
        //编辑地图物理方格
        private void editTile_Phy()
        {
            if (currentTile_Phy != null)
            {
                SmallDialog_NewTile_Physics.configElement(currentTile_Phy);
            }
        }
        private void button_config_Click(object sender, EventArgs e)
        {
            editTile_Phy();
            updateContainer_Physics();
            updateMap_Refresh();
        }
        private void pictureBox_Physics_DoubleClick(object sender, EventArgs e)
        {
            editTile_Phy();
            updateContainer_Physics();
            updateMap_Refresh();
        }
        //滚轮事件
        private void pictureBoxPhy_MouseWheel(object sender, MouseEventArgs e)
        {
            processSrollBar(e.Delta / 120,vScrollBar_Physics);
        }
        //处理滚轮事件
        private void processSrollBar(int value,VScrollBar bar)
        {
            int destValue = bar.Value - value * 4;
            if (destValue < bar.Minimum)
            {
                destValue = bar.Minimum;
            }
            else if (destValue > bar.Maximum-9)
            {
                destValue = bar.Maximum-9;
            }
            if (destValue != bar.Value)
            {
                bar.Value = destValue;
            }
        }
        //处理鼠标移入获得界面焦点
        private void pictureBox_Physics_MouseEnter(object sender, EventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            pictureBox_Physics.Focus();
        }
        //处理鼠标事件
        private void pictureBox_Physics_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int windowWidth = pictureBox_Physics.Width;
                int windowHeight = pictureBox_Physics.Height;
                VScrollBar bar = this.vScrollBar_Physics;
                int elementCount = mapsManager.tilePhysicsManager.getElementCount();
                if (elementCount > 0)
                {
                    int nbX = windowWidth / (width_phy + gap);
                    int contentHeight = ((elementCount + nbX - 1) / nbX) * (height_phy + gap);
                    int maskedHeight = 0;
                    if (contentHeight > windowHeight)
                    {
                        maskedHeight = (bar.Value - bar.Minimum) * (contentHeight - windowHeight) / (bar.Maximum - 9 - bar.Minimum);
                    }
                    int x = e.X;
                    int y = e.Y + maskedHeight;
                    if (x <= nbX * (width_phy + gap))
                    {
                        int index = (y / (height_phy + gap)) * nbX + x / (width_phy + gap);
                        setFocusTile_Phy(index);
                        this.updateContainer_Physics();
                    }

                }

            }

        }
        //处理按键事件
        private void pictureBox_Physics_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (int)Keys.Up:
                    break;
                case (int)Keys.Down:
                    break;
                case (int)Keys.Left:
                    break;
                case (int)Keys.Right:
                    break;
                case (int)Keys.Delete:
                    if (currentTile_Phy != null)
                    {
                        if (deletePhyElement())
                        {
                            updateContainer_Physics();
                            updateMap_Refresh();
                        }
                    }
                    break;
            }
        }
        private void TSB_Refresh_Click(object sender, EventArgs e)
        {
            this.updateMap_Refresh();
        }

        //============================地图图形方格容器部分================================
        //图形容器中的当前单元
        public TileGfxElement currentTile_Gfx = null;
        private TileGfxContainer currentGfxContainer = null;
        //刷新风格列表
        public void refreshTileSyleList()
        {
            this.noListBoxEvent = true;
            comboBox_GfxType.Items.Clear();
            for (int i = 0; i < mapsManager.tileGfxManager.Count(); i++)
            {
                comboBox_GfxType.Items.Add(mapsManager.tileGfxManager[i].name);
            }
            if (comboBox_GfxType.Items.Count > 0)
            {
                comboBox_GfxType.SelectedIndex = 0;
            }
            this.noListBoxEvent = false;
        }
        //设置当前图形方格容器
        public void resetCurrentGfxContainer()
        {
            currentGfxContainer = mapsManager.tileGfxManager[comboBox_GfxType.SelectedIndex];
            updateContainer_Gfx_Buffer();
        }
        public void setCurrentGfxContainer(int index)
        {
            if (index < 0 || index >= comboBox_GfxType.Items.Count)
            {
                return;
            }
            this.noListBoxEvent = true;
            comboBox_GfxType.SelectedIndex = index;
            this.noListBoxEvent = false;
            currentGfxContainer = mapsManager.tileGfxManager[index];
            currentTile_Gfx = null;
            updateContainer_Gfx_Buffer();
        }
        //添加多个地图元素
        private void button_importElement_Click(object sender, EventArgs e)
        {
            int number = SmallDialog_NewTiles_Gfx.generateTiles(mapImagesManager, currentGfxContainer);
            if (number > 0)
            {
                MessageBox.Show("新增" + number + "个地图图形单元");
                updateContainer_Gfx_Buffer();
            }
        }
        //添加单个地图元素
        private void button_addOne_Gfx_Click(object sender, EventArgs e)
        {
            form_MapImagesManager.MClipsManager = currentGfxContainer;
            form_MapImagesManager.initParams(null);
            form_MapImagesManager.ShowDialog();
            if (form_MapImagesManager.currentClipElemnt != null)
            {
                TileGfxElement newElement = new TileGfxElement(currentGfxContainer);
                newElement.copyBase(form_MapImagesManager.currentClipElemnt);
                if (currentTile_Gfx == null)
                {
                    currentGfxContainer.Add(newElement);
                }
                else
                {
                    currentGfxContainer.Insert(newElement, currentTile_Gfx.GetID());
                }
            }
            updateContainer_Gfx_Buffer();
        }
        //修改单元
        private void editTile_Gfx()
        {
            if (currentTile_Gfx != null)
            {
                form_MapImagesManager.MClipsManager = currentGfxContainer;
                form_MapImagesManager.initParams(currentTile_Gfx);
                form_MapImagesManager.ShowDialog();
                this.updateContainer_Gfx_Buffer();
            }
        }
        private void pictureBox_Gfx_DoubleClick(object sender, EventArgs e)
        {
            editTile_Gfx();
        }

        //复制单元
        private void clone_Gfx()
        {
            if (currentTile_Gfx != null)
            {
                TileGfxElement newElement = currentTile_Gfx.Clone(currentTile_Gfx.tileGfxContainer);
                currentGfxContainer.Add(newElement);
                updateContainer_Gfx_Buffer();
            }
        }

        private void button_Copy_Gfx_Click(object sender, EventArgs e)
        {
            clone_Gfx();
        }

        //设置焦点地图图形单元
        public void setFocusTile_Gfx(int index)
        {
            TileGfxElement oldEment = currentTile_Gfx;
            TileGfxElement element = (TileGfxElement)currentGfxContainer[index];
            //if (elemn != null)
            //{
            currentTile_Gfx = element;
            if ((oldEment != null && !oldEment.Equals(currentTile_Gfx))||
                (currentTile_Gfx != null && !currentTile_Gfx.Equals(oldEment)))
            {
                TSB_autoTile.Checked = false;
            }
            showCurrentTileGfxInf();
            //}
        }
        //显示当前地图图形单元反转标志
        public void showCurrentTileGfxInf()
        {
            if (currentTile_Gfx == null)
            {
                label_TileGfxID.Text = "单元编号:";
                label_TileGfxUsedTime.Text = "使用次数:";
            }
            else
            {
                label_TileGfxID.Text = "单元编号:" + currentTile_Gfx.GetID();
                label_TileGfxUsedTime.Text = "使用次数:" + currentTile_Gfx.getUsedTime();
            }
            if (currentTile_Gfx == null || currentTile_Gfx.getTansFlag() == Consts.TRANS_NONE)
            {
                panel_Flag.BackgroundImage = global::Cyclone.Properties.Resources.flipNone;
            }
            else
            {
                switch (currentTile_Gfx.getTansFlag())
                {
                    case Consts.TRANS_MIRROR_ROT180:
                        panel_Flag.BackgroundImage = global::Cyclone.Properties.Resources.flipV;
                        break;
                    case Consts.TRANS_MIRROR:
                        panel_Flag.BackgroundImage = global::Cyclone.Properties.Resources.flipH;
                        break;
                    case Consts.TRANS_ROT180:
                        panel_Flag.BackgroundImage = global::Cyclone.Properties.Resources.flipTrans180;
                        break;
                    case Consts.TRANS_MIRROR_ROT270:
                        panel_Flag.BackgroundImage = global::Cyclone.Properties.Resources.flipRT2LB;
                        break;
                    case Consts.TRANS_ROT90:
                        panel_Flag.BackgroundImage = global::Cyclone.Properties.Resources.flipTrans90;
                        break;
                    case Consts.TRANS_ROT270:
                        panel_Flag.BackgroundImage = global::Cyclone.Properties.Resources.flipTrans270;
                        break;
                    case Consts.TRANS_MIRROR_ROT90:
                        panel_Flag.BackgroundImage = global::Cyclone.Properties.Resources.flipLT2RB;
                        break;
                }
            }
            panel_Flag.Refresh();
        }
        //删除图形方格单元
        public bool deleteGfxElement()
        {
            if (currentTile_Gfx == null)
            {
                return false;
            }
            int index = currentTile_Gfx.GetID();
            int usedTime = currentTile_Gfx.getUsedTime();
            if (usedTime > 0)
            {
                DialogResult res = MessageBox.Show("该单元被" + usedTime + "处引用，确定删除？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (!res.Equals(DialogResult.Yes))
                {
                    return false;
                }
                //删除所有地图中的所有引用
                mapsManager.deleteTileUsed(currentTile_Gfx);
                updateMap_Refresh();
                clearHistory();
            }
            clearHistory();
            currentGfxContainer.RemoveAt(currentTile_Gfx.GetID());
            if (index >= currentGfxContainer.Count())
            {
                index--;
            }
            if (index < 0)
            {
                index = 0;
            }
            setFocusTile_Gfx(index);
            clearClipboard();
            return true;

        }
        //旋转图形方格单元
        private void rotateCurrentTile_Gfx(byte transFlag)
        {
            if (this.currentTile_Gfx == null)
            {
                return;
            }
            currentTile_Gfx.setTansFlag(Consts.getTransFlag(currentTile_Gfx.getTansFlag(),transFlag));
            updateContainer_Gfx_Buffer();
            if(currentTile_Gfx.getUsedTime()>0)
            {
                updateMap_Refresh();
            }
            showCurrentTileGfxInf();
        }
        private void button_TileGfx_FlipH_Click(object sender, EventArgs e)
        {
            rotateCurrentTile_Gfx(Consts.TRANS_MIRROR);
        }

        private void button_TileGfx_FlipV_Click(object sender, EventArgs e)
        {
            rotateCurrentTile_Gfx(Consts.TRANS_MIRROR_ROT180);
        }

        private void button_TileGfx_TransP_Click(object sender, EventArgs e)
        {
            rotateCurrentTile_Gfx(Consts.TRANS_ROT270);
        }

        private void button_TileGfx_TransN_Click(object sender, EventArgs e)
        {
            rotateCurrentTile_Gfx(Consts.TRANS_ROT90);
        }
        //在容器中交换图形方格单元
        private void moveCurrentTile_Gfx(int step)
        {
            if (this.currentTile_Gfx == null)
            {
                return;
            }
            int id = currentTile_Gfx.GetID();
            int newId = id + step;
            if (newId < 0)
            {
                newId = 0;
            }
            if (newId >= currentGfxContainer.Count())
            {
                newId = currentGfxContainer.Count() - 1;
            }
            if (id == newId)
            {
                return;
            }
            currentGfxContainer.RemoveAt(id);
            if (newId >= currentGfxContainer.Count())
            {
                currentGfxContainer.Add(currentTile_Gfx);
            }
            else
            {
                currentGfxContainer.Insert(currentTile_Gfx, newId);
            }
            updateContainer_Gfx_Buffer();
            TSB_autoTile.Checked = false;
        }
        //在容器中移动当前光标
        private void scanGfxTile(int step)
        {
            if (this.currentTile_Gfx == null)
            {
                return;
            }
            int id = currentTile_Gfx.GetID();
            int newId = id + step;
            if (newId < 0)
            {
                newId = 0;
            }
            if (newId >= currentGfxContainer.Count())
            {
                newId = currentGfxContainer.Count() - 1;
            }
            if (id == newId)
            {
                return;
            }
            setFocusTile_Gfx(newId);
            updateContainer_Gfx();
        }
        private void button_Up_Gfx_Click(object sender, EventArgs e)
        {
            if (currentTile_Gfx != null)
            {
                moveCurrentTile_Gfx(-pictureBox_Gfx.Width / currentTile_Gfx.clipRect.Width);
            }
        }

        private void button_Down_Gfx_Click(object sender, EventArgs e)
        {
            if (currentTile_Gfx != null)
            {
                moveCurrentTile_Gfx(pictureBox_Gfx.Width / currentTile_Gfx.clipRect.Width);
            }
        }

        private void button_Left_Gfx_Click(object sender, EventArgs e)
        {
            if (currentTile_Gfx != null)
            {
                moveCurrentTile_Gfx(-1);
            }
        }

        private void button_Right_Gfx_Click(object sender, EventArgs e)
        {
            moveCurrentTile_Gfx(1);
        }


        //地图缓冲
        public Image buffer_gfx_Window = null;
        public Image buffer_gfx_World = null;
        public Graphics gWorld_Gfx = null;
        int maskedHeight_Gfx = 0;
        int contentHeight_Gfx = 0;
        //更新地图缓冲
        public void updateContainer_Gfx()
        {
            int windowWidth = pictureBox_Gfx.Width;
            int windowHeight = pictureBox_Gfx.Height;
            if (windowWidth <= 0 || windowHeight <= 0)
            {
                return;
            }
            VScrollBar bar = this.vScrollBar_Gfx;
            if (buffer_gfx_Window == null || buffer_gfx_Window.Width != windowWidth || buffer_gfx_Window.Height != windowHeight)
            {
                buffer_gfx_Window = new Bitmap(windowWidth, windowHeight);
            }
            Graphics g = Graphics.FromImage(buffer_gfx_Window);
            GraphicsUtil.fillRect(g, 0, 0, windowWidth, windowHeight, Consts.colorDarkGray);
            //计算参数
            if (contentHeight_Gfx > windowHeight)
            {
                maskedHeight_Gfx = (bar.Value - bar.Minimum) * (contentHeight_Gfx - windowHeight) / (bar.Maximum - 9 - bar.Minimum);
            }
            else
            {
                maskedHeight_Gfx = 0;
            }
            //绘制缓冲
            if (buffer_gfx_World != null)
            {
                GraphicsUtil.drawClip(g, buffer_gfx_World, 0, 0, 0, maskedHeight_Gfx, buffer_gfx_World.Width, windowHeight, 0);
            }
            //绘制焦点
            if (currentTile_Gfx != null)
            {
                int width_Fox = currentTile_Gfx.clipRect.Width;
                int height_Fox = currentTile_Gfx.clipRect.Height;
                //GraphicsUtil.fillRect(g, currentTile_Gfx.xInContainer, currentTile_Gfx.yInContainer - maskedHeight_Gfx, width_Fox, height_Fox, Consts.colorRed,50);
                GraphicsUtil.drawRect(g, currentTile_Gfx.xInContainer - 1, currentTile_Gfx.yInContainer - maskedHeight_Gfx - 1, width_Fox + 2, height_Fox + 2, Consts.colorBlack, 100);
                GraphicsUtil.drawRect(g, currentTile_Gfx.xInContainer - 2, currentTile_Gfx.yInContainer - maskedHeight_Gfx - 2, width_Fox + 4, height_Fox + 4, Consts.colorWhite, 100);
                GraphicsUtil.drawRect(g, currentTile_Gfx.xInContainer - 3, currentTile_Gfx.yInContainer - maskedHeight_Gfx - 3, width_Fox + 6, height_Fox + 6, Consts.colorBlack, 100);
            }
            //绘制拷贝区域边框
            if (inCpyingGfx && currentTile_Gfx!=null)
            { 
                TileGfxElement tile_Gfx0= (TileGfxElement)currentGfxContainer[idStart];
                TileGfxElement tile_Gfx1= (TileGfxElement)currentGfxContainer[idEnd];
                int xMin = Math.Min(tile_Gfx0.xInContainer, tile_Gfx1.xInContainer);
                int yMin = Math.Min(tile_Gfx0.yInContainer, tile_Gfx1.yInContainer);
                int xMax = Math.Max(tile_Gfx0.xInContainer, tile_Gfx1.xInContainer) + currentTile_Gfx.clipRect.Width;
                int yMax = Math.Max(tile_Gfx0.yInContainer, tile_Gfx1.yInContainer) + currentTile_Gfx.clipRect.Height;
                int width_Fox = xMax - xMin;
                int height_Fox = yMax - yMin;
                GraphicsUtil.drawRect(g, xMin - 1, yMin - maskedHeight_Gfx - 1, width_Fox + 2, height_Fox + 2, Consts.color_border, 150);
                GraphicsUtil.drawRect(g, xMin - 2, yMin - maskedHeight_Gfx - 2, width_Fox + 4, height_Fox + 4, Consts.colorWhite, 150);
                GraphicsUtil.drawRect(g, xMin - 3, yMin - maskedHeight_Gfx - 3, width_Fox + 6, height_Fox + 6, Consts.color_border, 150);
            }
            //绘制到屏幕
            if (pictureBox_Gfx.Image == null || !pictureBox_Gfx.Image.Equals(buffer_gfx_Window))
            {
                pictureBox_Gfx.Image = buffer_gfx_Window;
            }
            else
            {
                pictureBox_Gfx.Refresh();
            }

        }
        //更新缓冲
        public void updateContainer_Gfx_Buffer()
        {
            //获得窗口参数
            contentHeight_Gfx = remXYInGfxWindow();
            int windowWidth = pictureBox_Gfx.Width;
            int windowHeight = pictureBox_Gfx.Height;
            int worldWidth = windowWidth;
            int worldHeight = contentHeight_Gfx;
            //更新世界缓冲
            int realW = 1;
            int realH = 1;
            if (buffer_gfx_World != null)
            {
                realW = buffer_gfx_World.Width;
                realH = buffer_gfx_World.Height;
            }
            if (realW < worldWidth)
            {
                realW = worldWidth;
            }
            if (realH < worldHeight)
            {
                realH = worldHeight;
            }
            if (buffer_gfx_World == null || buffer_gfx_World.Width != realW || buffer_gfx_World.Height != realH)
            {
                buffer_gfx_World = new Bitmap(realW, realH);
                gWorld_Gfx = Graphics.FromImage(buffer_gfx_World);
            }
            GraphicsUtil.fillRect(gWorld_Gfx, 0, 0, realW, realH, Consts.colorDarkGray);
            //绘制到缓冲
            int elementCount = currentGfxContainer.Count();
            if (elementCount > 0)
            {
                //Console.WriteLine("gfx elementCount:" + elementCount + ",maskedHeight:" + maskedHeight_Gfx);
                int x = 0;
                int y = 0;
                int maxGap = 0;
                TileGfxElement element;
                Rectangle rect;
                for (int i = 0; i < currentGfxContainer.Count(); i++)
                {
                    element = (TileGfxElement)currentGfxContainer[i];
                    rect = element.clipRect;
                    if (x + rect.Width > windowWidth)
                    {
                        y += maxGap;
                        x = 0;
                        maxGap = 0;
                    }
                    element.display(gWorld_Gfx, x, y, 1, element.getTansFlag(), null);
                    x += rect.Width;
                    if (maxGap < rect.Height)
                    {
                        maxGap = rect.Height;
                    }
                }
            }
            updateContainer_Gfx();
            updateMap_Refresh();
        }
        //保存每个点的位置并返回内容高度
        public int remXYInGfxWindow()
        {
            int containerWidth = pictureBox_Gfx.Width;
            int height = 0;
            int x = 0;
            int maxGap = 0;
            TileGfxElement element;
            for (int i = 0; i < currentGfxContainer.Count(); i++)
            {
                element = (TileGfxElement)currentGfxContainer[i];
                Rectangle rect = element.clipRect;
                if (x + rect.Width > containerWidth)
                {
                    height += maxGap;
                    x = 0;
                    maxGap = 0;
                }
                element.xInContainer = x;
                element.yInContainer = height;
                x += rect.Width;
                if (maxGap < rect.Height)
                {
                    maxGap = rect.Height;
                }
            }
            height += maxGap;
            return height;
        }
        private void tabPage_Gfx_Enter(object sender, EventArgs e)
        {
            //updateContainer_Gfx();
        }
        private void vScrollBar_Gfx_ValueChanged(object sender, EventArgs e)
        {
            updateContainer_Gfx();
        }

        private void pictureBox_Gfx_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (currentTile_Gfx == null)
            {
                return;
            }
            switch (e.KeyValue)
            {
                case (int)Keys.C:
                    if (e.Control)
                    {
                        clone_Gfx();
                    }
                    break;
                case (int)Keys.W:
                    if (e.Alt || e.Control)
                    {
                        break;
                    }
                    scanGfxTile(-pictureBox_Gfx.Width / currentTile_Gfx.clipRect.Width);
                    break;
                case (int)Keys.S:
                    if (e.Alt || e.Control)
                    {
                        break;
                    }
                    scanGfxTile(pictureBox_Gfx.Width / currentTile_Gfx.clipRect.Width);
                    break;
                case (int)Keys.A:
                    if (e.Alt || e.Control)
                    {
                        break;
                    }
                    scanGfxTile(-1);
                    break;
                case (int)Keys.D:
                    if (e.Alt || e.Control)
                    {
                        break;
                    }
                    scanGfxTile(1);
                    break;
                case (int)Keys.Up:
                    if (e.Control)
                    {
                        moveCurrentTile_Gfx(-pictureBox_Gfx.Width / currentTile_Gfx.clipRect.Width);
                    }
                    else if (e.Alt)
                    {
                        rotateCurrentTile_Gfx(Consts.TRANS_MIRROR);
                    }
                    else
                    {
                        scanGfxTile(-pictureBox_Gfx.Width / currentTile_Gfx.clipRect.Width);
                    }
                    break;
                case (int)Keys.Down:
                    if (e.Control)
                    {
                        moveCurrentTile_Gfx(pictureBox_Gfx.Width / currentTile_Gfx.clipRect.Width);
                    }
                    else if (e.Alt)
                    {
                        rotateCurrentTile_Gfx(Consts.TRANS_MIRROR_ROT180);
                    }
                    else
                    {
                        scanGfxTile(pictureBox_Gfx.Width / currentTile_Gfx.clipRect.Width);
                    }
                    break;
                case (int)Keys.Left:
                    if (e.Control)
                    {
                        moveCurrentTile_Gfx(-1);

                    }
                    else if (e.Alt)
                    {
                        rotateCurrentTile_Gfx(Consts.TRANS_ROT270);
                    }
                    else
                    {
                        scanGfxTile(-1);
                    }

                    break;
                case (int)Keys.Right:
                    if (e.Control)
                    {
                        moveCurrentTile_Gfx(1);
                    }
                    else if (e.Alt)
                    {
                        rotateCurrentTile_Gfx(Consts.TRANS_ROT90);
                    }
                    else
                    {
                        scanGfxTile(1);
                    }

                    break;
                case (int)Keys.Delete:
                    if (deleteGfxElement())
                    {
                        this.updateContainer_Gfx_Buffer();
                    }
                    break;
            }
        }
        //滚轮事件
        private void pictureBoxGfx_MouseWheel(object sender, MouseEventArgs e)
        {
            processSrollBar(e.Delta / 120, vScrollBar_Gfx);
        }
        //鼠标移入
        private void pictureBox_Gfx_MouseEnter(object sender, EventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            pictureBox_Gfx.Focus();
        }
        //鼠标按下
        int GfxMouseDonwX, GfxMouseDonwY;
        private void pictureBox_Gfx_MouseDown(object sender, MouseEventArgs e)
        {
            focusToGfxTile(e);
            if (e.Button == MouseButtons.Left)
            {
                GfxMouseDonwX = e.X;
                GfxMouseDonwY = e.Y;
            }
        }
        private void focusToGfxTile(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int windowWidth = pictureBox_Gfx.Width;
                int windowHeight = pictureBox_Gfx.Height;
                if (windowWidth <= 0 || windowHeight <= 0)
                {
                    return;
                }
                VScrollBar bar = this.vScrollBar_Gfx;
                int elementCount = currentGfxContainer.Count();
                if (elementCount > 0)
                {
                    int x = 0;
                    int y = 0;
                    //int maxGap = 0;
                    TileGfxElement element;
                    Rectangle rect;
                    Rect limitRect = new Rect(0, 0, windowWidth, windowHeight);
                    //二分查找
                    int left = 0;
                    int right = currentGfxContainer.Count() - 1;
                    int center = (left + right) / 2;
                    bool lastLoop = false;
                    while (true)
                    {
                        element = (TileGfxElement)currentGfxContainer[center];
                        x = element.xInContainer;
                        y = element.yInContainer;
                        rect = element.clipRect;
                        if (e.Y < y - maskedHeight_Gfx)//Y向左找
                        {
                            if (lastLoop)
                            {
                                break;
                            }
                            if (center == right)
                            {
                                lastLoop = true;
                            }
                            right = center;
                            center = (left + right) / 2;
                            continue;
                        }
                        if (e.Y >= y - maskedHeight_Gfx + rect.Height)//Y向右找
                        {
                            if (lastLoop)
                            {
                                break;
                            }
                            if (center == left)
                            {
                                lastLoop = true;
                            }
                            left = center;
                            center = (left + right + 1) / 2;
                            continue;
                        }
                        if (e.X < x)//X向左找
                        {
                            if (lastLoop)
                            {
                                break;
                            }
                            if (center == right)
                            {
                                lastLoop = true;
                            }
                            right = center;
                            center = (left + right) / 2;
                            continue;
                        }
                        if (e.X >= x + rect.Width)//X向右找
                        {
                            if (lastLoop)
                            {
                                break;
                            }
                            if (center == left)
                            {
                                lastLoop = true;
                            }
                            left = center;
                            center = (left + right + 1) / 2;
                            continue;
                        }
                        setFocusTile_Gfx(element.GetID());
                        //Console.WriteLine("find Index:" + element.getID());
                        updateContainer_Gfx();
                        break;
                    }
                }

            }
        }
        //========================实现从容器面板复制一块区域用以绘制========================
        int idStart, idEnd;
        bool inCpyingGfx = false;
        //鼠标移动事件
        private void pictureBox_Gfx_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                focusToGfxTile(e);
                if (currentTile_Gfx == null)
                {
                    return;
                }
                if (!inCpyingGfx)
                {
                    inCpyingGfx = true;
                    idStart = currentTile_Gfx.GetID();
                    idEnd = idStart;
                }
                else
                {
                    idEnd = currentTile_Gfx.GetID();
                }
                this.updateContainer_Gfx();
            }

        }
        //鼠标抬起事件
        private void pictureBox_Gfx_MouseUp(object sender, MouseEventArgs e)
        {
            if (inCpyingGfx)
            {
                if (idStart != idEnd)
                {
                    copyFromGfxSelect();
                    pasteRectSelect();
                }
                inCpyingGfx = false;
                if (currentTile_Gfx != null)
                {
                    idStart = currentTile_Gfx.GetID();
                    idEnd = idStart;
                }
                this.updateContainer_Gfx();
            }

        }
        //从容器复制一块区域并且粘贴
        private void copyFromGfxSelect()
        {
            if (currentMap == null || (Consts.currentLevel != Consts.LEVEL_TILE_BG && Consts.currentLevel != Consts.LEVEL_TILE_SUR))
            {
                return;
            }
            if (this.editMode != EDITMOD_STRAW)
            {
                setEditMode(EDITMOD_STRAW);
            }
            TileGfxElement tile_Gfx0 = (TileGfxElement)currentGfxContainer[idStart];
            TileGfxElement tile_Gfx1 = (TileGfxElement)currentGfxContainer[idEnd];
            int rowCount = pictureBox_Gfx.Width/currentMap.getTileW();
            int xMin = Math.Min(tile_Gfx0.GetID() % rowCount, tile_Gfx1.GetID() % rowCount);
            int yMin = Math.Min(tile_Gfx0.GetID() / rowCount, tile_Gfx1.GetID() / rowCount);
            int xMax = Math.Max(tile_Gfx0.GetID() % rowCount, tile_Gfx1.GetID() % rowCount);
            int yMax = Math.Max(tile_Gfx0.GetID() / rowCount, tile_Gfx1.GetID() / rowCount);
            int width_Fox = xMax - xMin;
            int height_Fox = yMax - yMin;
            copyedW = xMax - xMin + 1;
            copyedH = yMax - yMin + 1;
            //重设缓存大小
            clipBoard = new Object[copyedW, copyedH];
            //复制数据
            for (int i = 0; i < copyedW; i++)
            {
                for (int j = 0; j < copyedH; j++)
                {
                    int index = xMin + (j+yMin) * rowCount + i;
                    clipBoard[i, j] = new TransTileGfxElement((TileGfxElement)currentGfxContainer[index],0);
                }
            }
            copyedLevel = Consts.currentLevel;
            //准备剪贴板缓冲
            int bufferWidth = copyedW * TileW;
            int bufferHeight = copyedH * TileH;
            if (imgBuffer_CB == null || imgBuffer_CB.Width < bufferWidth || imgBuffer_CB.Height < bufferHeight)
            {
                imgBuffer_CB = new Bitmap(bufferWidth, bufferHeight);
            }
            Graphics g = Graphics.FromImage(imgBuffer_CB);
            g.Clear(Color.Transparent);
            int zoomLevel = 1;
            for (int i = 0; i < copyedW; i++)
            {
                for (int j = 0; j < copyedH; j++)
                {
                    if (clipBoard[i, j] == null)
                    {
                        continue;
                    }
                    TransTileGfxElement gfxElement = (TransTileGfxElement)clipBoard[i, j];
                    byte flag=gfxElement.tileGfxElement.getTansFlag();
                    flag = Consts.getTransFlag(flag, gfxElement.transFlag);
                    gfxElement.tileGfxElement.display(g, i * TileW, j * TileH, zoomLevel, flag, null);
                }
            }
            g.Dispose();
            g = null;
        }
        private void button_del_Gfx_Click(object sender, EventArgs e)
        {
            if (deleteGfxElement())
            {
                updateContainer_Gfx_Buffer();
            }
        }
        private void button_ClearSpilth_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("即将清除冗余图形元素，合并重复图形元素，将丢失原来布局，确定操作？", "警告：", MessageBoxButtons.YesNo);
            if (res.Equals(DialogResult.Yes))
            {
                currentTile_Gfx = null;
                //setFocusTile_Gfx(-1);
                currentGfxContainer.ClearSpilth(true);
                updateContainer_Gfx_Buffer();
            }
        }
        private void button_AddGfxFolder_Click(object sender, EventArgs e)
        {
            String name = "风格" + mapsManager.tileGfxManager.Count();
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("新建地图图形元素风格", name);
            txtDialog.ShowDialog();
            name = txtDialog.getValue();
            TileGfxContainer gfxContainer = new TileGfxContainer(mapsManager.tileGfxManager, name);
            mapsManager.tileGfxManager.Add(gfxContainer);
            refreshTileSyleList();
            setCurrentGfxContainer(gfxContainer.GetID());
        }
        private void button_NameFolder_Click(object sender, EventArgs e)
        {
            if (currentGfxContainer != null)
            {
                String name = currentGfxContainer.name;
                SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("重命名地图图形元素风格", name);
                txtDialog.ShowDialog();
                name = txtDialog.getValue();
                currentGfxContainer.name = name;
                refreshTileSyleList();
                setCurrentGfxContainer(currentGfxContainer.GetID());
            }
        }
        //删除当前地图图形元素风格
        private void button_DelGfxFolder_Click(object sender, EventArgs e)
        {
            if (currentGfxContainer == null)
            {
                return;
            }
            int styleCount=mapsManager.tileGfxManager.Count();
            if (styleCount == 1)
            {
                MessageBox.Show("不能删除最后一个风格");
                return;
            }
            DialogResult res = MessageBox.Show("即将删除图形元素风格" + currentGfxContainer .name+
                "，并且在地图中除去对所有单元的调用，是否确定？", "警告：", MessageBoxButtons.YesNo);
            if (res.Equals(DialogResult.Yes))
            {
                currentGfxContainer.clearAllElement();
                mapsManager.tileGfxManager.RemoveAt(currentGfxContainer.GetID());
                currentGfxContainer = null;
                refreshTileSyleList();
                resetCurrentGfxContainer();
                updateContainer_Gfx_Buffer();
            }

        }
        private void comboBox_GfxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noListBoxEvent)
            {
                return;
            }
            setCurrentGfxContainer(comboBox_GfxType.SelectedIndex);
        }

        //============================地图角色原型容器部分================================
        //角色原型容器中的当前单元
        Antetype currentAntetype = null;
        //当前角色原型文件夹
        AntetypeFolder currentAntetypeFolder = null;
        //容器缓冲
        public Image buffer_AT_Window = null;
        public Image buffer_AT_World = null;
        public Graphics gWorld_AT = null;
        //容器尺寸
        int maskedHeight_AT = 0;
        int contentHeight_AT = 0;
        //单元尺寸
        private const int ANTETYPE_WIDTH = 80;
        private const int ANTETYPE_HEIGHT = 80;
        private int AT_GAP = 2;//单元之间的间隔
        private int AT_ROW_COUNT = 3;//一行显示的个数
        //刷新角色原型文件夹列表
        public void refreshAntetypeSyleList()
        {
            if (mapsManager == null)
            {
                return;
            }
            setCurrentAT(null);
            this.noListBoxEvent = true;
            comboBox_ATFolders.Items.Clear();
            for (int i = 0; i < mapsManager.antetypesManager.Count(); i++)
            {
                comboBox_ATFolders.Items.Add(((AntetypeFolder)mapsManager.antetypesManager[i]).name);
            }
            if (comboBox_ATFolders.Items.Count > 0)
            {
                comboBox_ATFolders.SelectedIndex = 0;
                resetCurrentATFolder();
            }
            this.noListBoxEvent = false;
        }
        //更新窗口
        public void updateContainer_AT()
        {
            int windowWidth = pictureBox_AT.Width;
            int windowHeight = pictureBox_AT.Height;
            if (windowWidth <= 0 || windowHeight <= 0)
            {
                return;
            }
            VScrollBar bar = this.vScrollBar_AT;
            if (buffer_AT_Window == null || buffer_AT_Window.Width != windowWidth || buffer_AT_Window.Height != windowHeight)
            {
                buffer_AT_Window = new Bitmap(windowWidth, windowHeight);
            }
            Graphics g = Graphics.FromImage(buffer_AT_Window);
            GraphicsUtil.fillRect(g, 0, 0, windowWidth, windowHeight, Consts.colorWhite);
            //计算参数
            if (contentHeight_AT > windowHeight)
            {
                maskedHeight_AT = (bar.Value - bar.Minimum) * (contentHeight_AT - windowHeight) / (bar.Maximum - 9 - bar.Minimum);
            }
            else
            {
                maskedHeight_AT = 0;
            }
            //绘制缓冲
            if (buffer_AT_World != null)
            {
                GraphicsUtil.drawClip(g, buffer_AT_World, 0, 0, 0, maskedHeight_AT, buffer_AT_World.Width, windowHeight, 0);
            }
            //绘制焦点
            if (currentAntetypeFolder != null)
            {
                if (currentAntetype != null)
                {
                    int index = currentAntetype.GetID();
                    if (index >= 0)
                    {
                        int x = AT_GAP + (AT_GAP + ANTETYPE_WIDTH) * (index % AT_ROW_COUNT);
                        int y = AT_GAP + (AT_GAP + ANTETYPE_HEIGHT) * (index / AT_ROW_COUNT) - maskedHeight_AT;
                        GraphicsUtil.drawRect(g, x, y, ANTETYPE_WIDTH, ANTETYPE_HEIGHT, Consts.color_border);
                        GraphicsUtil.drawRect(g, x - 1, y - 1, ANTETYPE_WIDTH + 2, ANTETYPE_HEIGHT + 2, Consts.colorWhite);
                        GraphicsUtil.drawRect(g, x - 2, y - 2, ANTETYPE_WIDTH + 4, ANTETYPE_HEIGHT + 4, Consts.color_border);
                    }
                }
            }


            //绘制到屏幕
            if (pictureBox_AT.Image == null || !pictureBox_AT.Image.Equals(buffer_AT_Window))
            {
                pictureBox_AT.Image = buffer_AT_Window;
            }
            else
            {
                pictureBox_AT.Refresh();
            }

        }
        //更新缓冲
        public void updateContainer_AT_Buffer()
        {
            //获得窗口参数
            resetParams_AT();
            int windowWidth = pictureBox_AT.Width;
            int windowHeight = pictureBox_AT.Height;
            int worldWidth = windowWidth;
            int worldHeight = contentHeight_AT;
            //更新世界缓冲
            int realW = 1;
            int realH = 1;
            if (buffer_AT_World != null)
            {
                realW = buffer_AT_World.Width;
                realH = buffer_AT_World.Height;
            }
            if (realW < worldWidth)
            {
                realW = worldWidth;
            }
            if (realH < worldHeight)
            {
                realH = worldHeight;
            }
            if (buffer_AT_World == null || buffer_AT_World.Width != realW || buffer_AT_World.Height != realH)
            {
                buffer_AT_World = new Bitmap(realW, realH);
                gWorld_AT = Graphics.FromImage(buffer_AT_World);
            }
            GraphicsUtil.fillRect(gWorld_AT, 0, 0, realW, realH, Consts.colorWhite);
            //在容器中绘制角色原型
            int x = 0;
            int y = AT_GAP;
            if (currentAntetypeFolder != null)
            {
                for (int i = 0; i < currentAntetypeFolder.Count(); i++)
                {
                    x += AT_GAP;
                    //绘制
                    GraphicsUtil.fillRect(gWorld_AT, x, y, ANTETYPE_WIDTH, ANTETYPE_HEIGHT, Consts.colorLightGray);
                    Antetype actorAntetype = (Antetype)currentAntetypeFolder[i];
                    if (actorAntetype != null)
                    {
                        actorAntetype.display(gWorld_AT, x, y, ANTETYPE_WIDTH, ANTETYPE_HEIGHT, 1, true);
                    }
                    x += ANTETYPE_WIDTH;
                    if (i > 0 && (i + 1) % AT_ROW_COUNT == 0)
                    {
                        x = 0;
                        y += ANTETYPE_HEIGHT + AT_GAP;
                    }

                }
            }
            updateContainer_AT();
        }
        //设定角色原型面板参数
        private void resetParams_AT()
        {
            int containerWidth = pictureBox_Gfx.Width;
            AT_ROW_COUNT = containerWidth / ANTETYPE_WIDTH;
            if (AT_ROW_COUNT <= 0) 
            {
                contentHeight_AT = 0;
                return; 
            }
            AT_GAP = (containerWidth - AT_ROW_COUNT * ANTETYPE_WIDTH) / (AT_ROW_COUNT + 1);
            if (currentAntetypeFolder != null)
            {
                contentHeight_AT = AT_GAP + ((currentAntetypeFolder.Count() + AT_ROW_COUNT - 1) / AT_ROW_COUNT) * (ANTETYPE_WIDTH + AT_GAP);
            }
        }
        //刷新当前所有角色原型可引用动画列表
        //private void refreshAT_SRCList()
        //{
            //ActorsManager actorsManager = mapsManager.antetypesManager.actorsManager;
            //if (actorsManager != null)
            //{
            //    comboBox_AT.Items.Clear();
            //    for (int i = 0; i < actorsManager.getElementCount(); i++)
            //    {
            //        comboBox_AT.Items.Add(actorsManager.getElement(i).getName());
            //    }
            //}
            //this.noListBoxEvent = true;
            //if (currentAntetype != null)
            //{
            //    comboBox_AT.SelectedIndex = currentAntetype.actor.getID();
            //}
            //else
            //{
            //    comboBox_AT.SelectedIndex = -1;
            //}
            //this.noListBoxEvent = false;
        //}
        //重建当前所有角色原型与动画索引之间的连接(有可能出现丢失的情况)
        private void rebuildConAT2Actor()
        {
            for (int i = 0; i < mapsManager.antetypesManager.Count(); i++)
            {
                AntetypeFolder folder = mapsManager.antetypesManager[i];
                for (int j = 0; j < folder.Count(); j++)
                {
                    Antetype antetype = folder[j];
                    if (antetype.Actor != null)
                    {
                        int id = antetype.Actor.GetID();
                        if (id < 0)
                        {
                            antetype.Actor = null;
                        }
                    }
                }
            }
        }

        //设置当前角色原型
        private void setCurrentAT(int index)
        {
            if (currentAntetypeFolder != null)
            {
                Antetype antetype = currentAntetypeFolder[index];
                if (antetype != null)
                {
                    setCurrentAT(antetype);
                }
                else
                {
                    setCurrentAT(null);
                }

            }


        }
        //删除当前角色原型
        private bool delCurrentAT()
        {
            if (currentAntetype == null || currentAntetypeFolder==null)
            {
                return false;
            }
            int usedTime = currentAntetype.getUsedTime();
            if (usedTime > 0)
            {
                DialogResult res = MessageBox.Show("该单元被" + usedTime + "处引用，确定删除？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (!res.Equals(DialogResult.Yes))
                {
                    return false;
                }
                //删除所有地图中的所有引用
                mapsManager.deleteTileUsed(currentAntetype);
                updateMap_Refresh();
                clearHistory();
            }
            int index = currentAntetype.GetID();
            int folderID = currentAntetypeFolder.GetID();
            bool removed = mapsManager.antetypesManager[folderID].RemoveAt(index);
            if (index >= currentAntetypeFolder.Count())
            {
                index = currentAntetypeFolder.Count() - 1;
            }
            setCurrentAT(index);
            return removed;
        }
        private void setCurrentAT(Antetype antetype)
        {
            this.currentAntetype = antetype;
            if (currentAntetype != null)
            {
                String name = currentAntetype.name;
                lable_AT.Text = name;
                showFunction("角色原型ID：" + antetype.GetID() + "，引用自：" + (antetype.Actor != null ? (antetype.Actor.name + "，ID：" + antetype.Actor.GetID()) : "空") + "，被使用次数：" + currentAntetype.getUsedTime());
            }
            else
            {
                lable_AT.Text = "";
                showFunction("");
            }
        }
        private void resetCurrentATFolder()
        {
            int index = comboBox_ATFolders.SelectedIndex;
            if (index < 0)
            {
                currentAntetypeFolder = null;
            }
            else
            {
                AntetypeFolder obj = mapsManager.antetypesManager[comboBox_ATFolders.SelectedIndex];
                if(obj!=null)
                {
                    currentAntetypeFolder = obj;
                }
            }
        }
        //移动光标
        private bool moveFoxus_AT(int step)
        {
            if (currentAntetype == null||currentAntetypeFolder==null)
            {
                return false;
            }
            int index = currentAntetype.GetID();
            int newIndex = MathUtil.limitNumber(index + step, 0, currentAntetypeFolder.Count() - 1);
            if (newIndex != index)
            {
                setCurrentAT(newIndex);
                //改变滚动条
                int maskedHeight=0;
                int windowHeight = pictureBox_AT.Height;
                if (contentHeight_AT > windowHeight)
                {
                    maskedHeight = (vScrollBar_AT.Value - vScrollBar_AT.Minimum) * (contentHeight_AT - windowHeight) / (vScrollBar_AT.Maximum - 9 - vScrollBar_AT.Minimum);
                    int needShowIndex = newIndex / AT_ROW_COUNT;
                    bool needModify = false;
                    int newValue = 0;
                    if (needShowIndex * (ANTETYPE_HEIGHT + AT_GAP) < maskedHeight)
                    {
                        needModify = true;
                        maskedHeight = needShowIndex * (ANTETYPE_HEIGHT + AT_GAP);
                        newValue = maskedHeight * (vScrollBar_AT.Maximum - 9 - vScrollBar_AT.Minimum) / (contentHeight_AT - windowHeight) + vScrollBar_AT.Minimum;
                    }
                    else if ((needShowIndex + 1) * (ANTETYPE_HEIGHT + AT_GAP) + AT_GAP > maskedHeight + windowHeight)
                    {
                        needModify = true;
                        maskedHeight = (needShowIndex + 1) * (ANTETYPE_HEIGHT + AT_GAP) + AT_GAP - windowHeight;
                        newValue = maskedHeight * (vScrollBar_AT.Maximum - 9 - vScrollBar_AT.Minimum) / (contentHeight_AT - windowHeight) + vScrollBar_AT.Minimum;
                    }
                    if (needModify)
                    {
                        noScrollEvent = true;
                        vScrollBar_AT.Value = MathUtil.limitNumber(newValue, vScrollBar_AT.Minimum, vScrollBar_AT.Maximum);
                        noScrollEvent = false;
                    }
                }
                return true;
            }
            return false;
        }
        //移动角色原型(step 相对位置偏移)
        private bool moveAT(int step)
        {
            if (currentAntetype == null || currentAntetypeFolder == null)
            {
                return false;
            }
            int index = currentAntetype.GetID();
            int newIndex = MathUtil.limitNumber(index + step, 0, currentAntetypeFolder.Count() - 1);
            if (newIndex != index)
            {
                currentAntetypeFolder.RemoveAt(index);
                currentAntetypeFolder.Insert(currentAntetype, newIndex);
                setCurrentAT(newIndex);
                //改变滚动条
                int maskedHeight = 0;
                int windowHeight = pictureBox_AT.Height;
                if (contentHeight_AT > windowHeight)
                {
                    maskedHeight = (vScrollBar_AT.Value - vScrollBar_AT.Minimum) * (contentHeight_AT - windowHeight) / (vScrollBar_AT.Maximum - 9 - vScrollBar_AT.Minimum);
                    int needShowIndex = newIndex / AT_ROW_COUNT;
                    bool needModify = false;
                    int newValue = 0;
                    if (needShowIndex * (ANTETYPE_HEIGHT + AT_GAP) < maskedHeight)
                    {
                        needModify = true;
                        maskedHeight = needShowIndex * (ANTETYPE_HEIGHT + AT_GAP);
                        newValue = maskedHeight * (vScrollBar_AT.Maximum - 9 - vScrollBar_AT.Minimum) / (contentHeight_AT - windowHeight) + vScrollBar_AT.Minimum;
                    }
                    else if ((needShowIndex + 1) * (ANTETYPE_HEIGHT + AT_GAP) + AT_GAP > maskedHeight + windowHeight)
                    {
                        needModify = true;
                        maskedHeight = (needShowIndex + 1) * (ANTETYPE_HEIGHT + AT_GAP) + AT_GAP - windowHeight;
                        newValue = maskedHeight * (vScrollBar_AT.Maximum - 9 - vScrollBar_AT.Minimum) / (contentHeight_AT - windowHeight) + vScrollBar_AT.Minimum;
                    }
                    if (needModify)
                    {
                        noScrollEvent = true;
                        vScrollBar_AT.Value = MathUtil.limitNumber(newValue, vScrollBar_AT.Minimum, vScrollBar_AT.Maximum);
                        noScrollEvent = false;
                    }
                }
                return true;
            }
            return false;
        }
        //滚轮事件
        private void pictureBoxAT_MouseWheel(object sender, MouseEventArgs e)
        {
            //Console.WriteLine(e.Delta);
            processSrollBar(e.Delta / 120, vScrollBar_AT);
        }
        //鼠标按下
        private void pictureBox_AT_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                int y = e.Y + maskedHeight_AT;
                int index = (y / (AT_GAP + ANTETYPE_HEIGHT)) * AT_ROW_COUNT + x / (AT_GAP + ANTETYPE_WIDTH);
                if (currentAntetypeFolder != null)
                {
                    Antetype antetype = currentAntetypeFolder[index];
                    if (antetype != null)
                    {
                        setCurrentAT(antetype);
                        updateContainer_AT();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (currentAntetype == null && comboBox_ATFolders.SelectedIndex>=0)
                {
                    return;
                }
                ToolStripMenuItem_MoveAnteType.DropDownItems.Clear();
                System.Windows.Forms.ToolStripItem[] items = new System.Windows.Forms.ToolStripItem[mapsManager.antetypesManager.Count()];
                for (int i = 0; i < items.Length; i++)
                {
                    ToolStripMenuItem ToolStripMenuItem_temp = new ToolStripMenuItem();
                    AntetypeFolder antetypeFolder = mapsManager.antetypesManager[i];
                    ToolStripMenuItem_temp.Name = "item" + i;
                    ToolStripMenuItem_temp.Size = new System.Drawing.Size(152, 22);
                    ToolStripMenuItem_temp.Text = antetypeFolder.name;
                    items[i] = ToolStripMenuItem_temp;
                    ToolStripMenuItem_temp.Click += new System.EventHandler(this.ToolStripMenuItem_MoveTo_Click);
                }
                ToolStripMenuItem_MoveAnteType.DropDownItems.AddRange(items);
                items[comboBox_ATFolders.SelectedIndex].Enabled = false;
                contextMenuStrip_AnteType.Show((Control)sender, e.X, e.Y);
            }
        }
        private void ToolStripMenuItem_MoveTo_Click(object sender, EventArgs e)
        {
            if (currentAntetype != null && comboBox_ATFolders.SelectedIndex >= 0)
            {
                ToolStripItem item = (ToolStripItem)sender;
                int index = ToolStripMenuItem_MoveAnteType.DropDownItems.IndexOf(item);
                if (index >= 0 && index != comboBox_ATFolders.SelectedIndex)
                {
                    AntetypeFolder antetypeFolderSrc = (AntetypeFolder)mapsManager.antetypesManager[comboBox_ATFolders.SelectedIndex];
                    AntetypeFolder antetypeFolderDest = (AntetypeFolder)mapsManager.antetypesManager[index];
                    antetypeFolderSrc.Remove(currentAntetype);
                    antetypeFolderDest.Add(currentAntetype);
                    updateContainer_AT_Buffer();
                }
            }
        }
        //按键响应
        private void pictureBox_AT_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)//交换位置
            {
                switch (e.KeyValue)
                {
                    case (int)Keys.Up:
                    case (int)Keys.W:
                        if (moveAT(-AT_ROW_COUNT))
                        {
                            updateContainer_AT_Buffer();
                        }
                        break;
                    case (int)Keys.Down:
                    case (int)Keys.S:
                        if (moveAT(AT_ROW_COUNT))
                        {
                            updateContainer_AT_Buffer();
                        }
                        break;
                    case (int)Keys.Left:
                    case (int)Keys.A:
                        if (moveAT(-1))
                        {
                            updateContainer_AT_Buffer();
                        }
                        break;
                    case (int)Keys.Right:
                    case (int)Keys.D:
                        if (moveAT(1))
                        {
                            updateContainer_AT_Buffer();
                        }
                        break;
                }
            }
            else//光标切换
            {
                switch (e.KeyValue)
                {
                    case (int)Keys.Up:
                    case (int)Keys.W:
                        if (moveFoxus_AT(-AT_ROW_COUNT))
                        {
                            updateContainer_AT();
                        }
                        break;
                    case (int)Keys.Down:
                    case (int)Keys.S:
                        if (moveFoxus_AT(AT_ROW_COUNT))
                        {
                            updateContainer_AT();
                        }
                        break;
                    case (int)Keys.Left:
                    case (int)Keys.A:
                        if (moveFoxus_AT(-1))
                        {
                            updateContainer_AT();
                        }
                        break;
                    case (int)Keys.Right:
                    case (int)Keys.D:
                        if (moveFoxus_AT(1))
                        {
                            updateContainer_AT();
                        }
                        break;
                    case (int)Keys.Delete:
                        if (delCurrentAT())
                        {
                            updateContainer_AT_Buffer();
                        }
                        break;
                } 
            }

        }
        //重新设定角色原型使用的角色动画
        //private void comboBox_AT_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (noListBoxEvent)
        //    {
        //        return;
        //    }
        //    if (currentAntetype != null)
        //    {
        //        ActorElement actor = mapsManager.antetypesManager.actorsManager.getElement(comboBox_AT.SelectedIndex);
        //        if (actor != null)
        //        {
        //            currentAntetype.actor = actor;
        //            updateContainer_AT_Buffer();
        //            if (currentAntetype.getUsedTime() > 0)
        //            {
        //                updateMap_Refresh();
        //            }
        //        }
        //    }
        //}
        //刷新角色原型与其引用的角色动画列表
        private void refreshAT2Actors()
        {
            //refreshAT_SRCList();
            rebuildConAT2Actor();
            updateContainer_AT_Buffer();
            updateMap_Refresh();
        }

        //private void button_AT_Add_Click(object sender, EventArgs e)
        //{
        //    addElement_AT();
        //}
        //自动增加角色原型
        private void button_AT_import_Click(object sender, EventArgs e)
        {
            //if (mapsManager.antetypesManager.getElementCount() > 0)
            //{
            //    MessageBox.Show("已经存在角色原型，不能自动增加", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            List<ActorAndFolder> arrayList = Form_CreateAnteType.getActors(mapsManager.antetypesManager.actorsManager);
            if (arrayList.Count == 0)
            {
                return;
            }
            for (int i = 0; i < arrayList.Count; i++)
            {
                ActorAndFolder doubleName = arrayList[i];
                Antetype antetype = new Antetype(currentAntetypeFolder, arrayList[i].actor);
                currentAntetypeFolder.Add(antetype);
            }
            updateContainer_AT_Buffer();
        }
        

        private void button_AT_refresh_Click(object sender, EventArgs e)
        {
            refreshAT2Actors();
        }


        private void vScrollBar_AT_ValueChanged(object sender, EventArgs e)
        {
            if (this.noScrollEvent)
            {
                return;
            }
            updateContainer_AT();
        }
        private void pictureBox_AT_MouseEnter(object sender, EventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            pictureBox_AT.Focus();
        }
        private void button_Obj_del_Click(object sender, EventArgs e)
        {
            if (delCurrentAT())
            {
                updateContainer_AT_Buffer();
            }
        }
        //===============================通用UI设置函数===================================
        public void resetBufferSize()
        {

        }
        //功能指示区
        private void showFunction(String s)
        {
            label_showFunction.Text = s;
        }
        //刷新所有容器
        public void updateAllContainers()
        {
            if (TSPB_load.Value != 100)
            {
                Console.WriteLine("窗口未显示");
                return;
            }
            refreshAntetypeSyleList();
            rebuildConAT2Actor();
            updateContainer_MapList();
            updateContainer_Physics();
            updateContainer_Gfx_Buffer();
            updateContainer_AT_Buffer();
            updateMap_Refresh();
        }
        //改变框体大小时刷新
        private int preFormWidth=0;
        private int preFormHeight = 0;
        private void Form_Main_SizeChanged(object sender, EventArgs e)
        {
            if (TSPB_load.Value != 100)
            {
                return;
            }
            Console.WriteLine("Form_Main_SizeChanged:this.Width:" + this.Width + ",this.Height:" + this.Height);
            if (this.Width >= this.MinimumSize.Width && this.Height >= this.MinimumSize.Height && (preFormWidth != this.Width || preFormHeight != this.Height))
            {
                updateAllContainers();
                preFormWidth = this.Width;
                preFormHeight = this.Height;
            }
        }
        //切换场景缩放比率
        private void releaseChecks()
        {
            TSMI_P100.Checked = false;
            TSMI_P200.Checked = false;
            TSMI_P400.Checked = false;
            TSMI_P800.Checked = false;
            TSMI_P50.Checked = false;
            TSMI_P25.Checked = false;
            TSMI_P12dot5.Checked = false;
        }
        private void TSMI_P100_Click(object sender, EventArgs e)
        {
            setZoomLevel(1);
            releaseChecks();
            TSMI_P100.Checked = true;
            this.updateMap_Refresh();
        }

        private void TSMI_P200_Click(object sender, EventArgs e)
        {
            setZoomLevel(2);
            releaseChecks();
            TSMI_P200.Checked = true;
            this.updateMap_Refresh();
        }

        private void TSMI_P400_Click(object sender, EventArgs e)
        {
            setZoomLevel(4);
            releaseChecks();
            TSMI_P400.Checked = true;
            this.updateMap_Refresh();
        }

        private void TSMI_P800_Click(object sender, EventArgs e)
        {
            setZoomLevel(8);
            releaseChecks();
            TSMI_P800.Checked = true;
            this.updateMap_Refresh();
        }

        private void TSMI_P50_Click(object sender, EventArgs e)
        {
            setZoomLevel(0.5f);
            releaseChecks();
            TSMI_P50.Checked = true;
            this.updateMap_Refresh();
        }

        private void TSMI_P25_Click(object sender, EventArgs e)
        {
            setZoomLevel(0.25f);
            releaseChecks();
            TSMI_P25.Checked = true;
            this.updateMap_Refresh();
        }

        private void TSMI_P12dot5_Click(object sender, EventArgs e)
        {
            setZoomLevel(0.125f);
            releaseChecks();
            TSMI_P12dot5.Checked = true;
            this.updateMap_Refresh();
        }

        private void 物理标记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.showStringInPhyLevel = !Consts.showStringInPhyLevel;
            物理标记ToolStripMenuItem.Checked = Consts.showStringInPhyLevel;
            updateMap_Refresh();
        }

        private void TSB_Paste_Click(object sender, EventArgs e)
        {
            pasteRectSelect();
            updateMap();
        }

        private void button_addMore_Grx_MouseHover(object sender, EventArgs e)
        {
            showFunction("导入多个图形元素");
        }

        private void button_edit_Gfx_MouseHover(object sender, EventArgs e)
        {
            showFunction("编辑焦点图形元素");
        }

        private void button_addOne_Gfx_MouseHover(object sender, EventArgs e)
        {
            showFunction("增加单个图形元素");
        }

        private void button_TileGfx_FlipH_MouseHover(object sender, EventArgs e)
        {
            showFunction("水平翻转图形元素");
        }

        private void button_TileGfx_FlipV_MouseHover(object sender, EventArgs e)
        {
            showFunction("垂直翻转图形元素");
        }

        private void button_TileGfx_TransP_MouseHover(object sender, EventArgs e)
        {
            showFunction("逆时针旋转图形元素");
        }

        private void button_TileGfx_TransN_MouseHover(object sender, EventArgs e)
        {
            showFunction("顺时针旋转图形元素");
        }

        private void button_del_Gfx_MouseHover(object sender, EventArgs e)
        {
            showFunction("删除焦点图形元素");
        }

        private void button_add_Phy_MouseHover(object sender, EventArgs e)
        {
            showFunction("增加单个物理元素");
        }

        private void button_config_MouseHover(object sender, EventArgs e)
        {
            showFunction("编辑焦点物理元素");
        }

        private void button_del_Phy_MouseHover(object sender, EventArgs e)
        {
            showFunction("删除焦点物理元素");
        }
        private void button_Up_Gfx_MouseHover(object sender, EventArgs e)
        {
            showFunction("向上移动当前图形元素");
        }

        private void button_Left_Gfx_MouseHover(object sender, EventArgs e)
        {
            showFunction("向前移动当前图形元素");
        }

        private void button_Down_Gfx_MouseHover(object sender, EventArgs e)
        {
            showFunction("向下移动当前图形元素");
        }

        private void button_Right_Gfx_MouseHover(object sender, EventArgs e)
        {
            showFunction("向右移动当前图形元素");
        }

        private void tabControl_Center_L_T_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int pageIndex = tabControl_Center_L_T.SelectedIndex;
            //if (pageIndex == 0)//物理元素
            //{
            //    if (Consts.currentLevel!=LEVEL_PHYSICS)
            //    {
            //        setLevel(LEVEL_PHYSICS);
            //    }
            //}
            //if (pageIndex == 1)//地图方格元素
            //{
            //    if (Consts.currentLevel != LEVEL_GROUND && Consts.currentLevel != LEVEL_SURFACE)
            //    {
            //        setLevel(LEVEL_GROUND);
            //    }
            //}
            //if (pageIndex == 2)//对象元素
            //{
            //    if (Consts.currentLevel != LEVEL_OBJECT_BG && Consts.currentLevel != LEVEL_OBJECT)
            //    {
            //        setLevel(LEVEL_OBJECT_BG);
            //    }
            //}
        }

        private void 图层透明度调整ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SmallDialog_LevelEyes levelEyes = new SmallDialog_LevelEyes(this);
            levelEyes.ShowDialog();
            if (SmallDialog_LevelEyes.needUpdate)
            {
                this.updateMap_Refresh();
            }
        }

        private void 顶层显示物理层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.LVL_PHY_TOP = !Consts.LVL_PHY_TOP;
            顶层显示物理层ToolStripMenuItem.Checked = Consts.LVL_PHY_TOP;
            this.updateMap_Refresh();
        }

        private void button_Copy_Gfx_MouseHover(object sender, EventArgs e)
        {
            showFunction("克隆当前图形元素");
        }

        private void button_obj_import_MouseHover(object sender, EventArgs e)
        {
            showFunction("根据当前动画自动生成角色原型");
        }

        private void button_obj_refresh_MouseHover(object sender, EventArgs e)
        {
            showFunction("刷新被使用的角色动画");
        }
        private void button_Obj_Add_MouseHover(object sender, EventArgs e)
        {
            showFunction("添加单个角色原型");
        }
        private void button_Obj_Del_MouseHover(object sender, EventArgs e)
        {
            showFunction("删除当前角色原型");
        }
        #region IUndoHandler Members
        private bool inRemTileEdit = false;//地图编辑是否正在记录中
        private FillRandomPointsCommand fillPointsCmd = null;
        void IUndoHandler.Undo(UndoCommand cmd)
        {
            if (cmd is FillRandomPointsCommand)
            {
                FillRandomPointsCommand fillPointsCmd = (FillRandomPointsCommand)cmd;
                FillPointHistory pointCmd = null;
                MapElement mapElement = fillPointsCmd.getMapElement();
                for (int i = fillPointsCmd.getPointsCount()-1; i >=0; i--)
                {
                    pointCmd = fillPointsCmd.getPoint(i);
                    Object oldElement = pointCmd.getTileElement_Org();
                    int tileIndex_X = pointCmd.getTileIndex_X();
                    int tileIndex_Y = pointCmd.getTileIndex_Y();
                    short tileLevel = pointCmd.getTileLevel();
                    int currentStageID = pointCmd.getStageID();
                    fillPoint(currentMap, mapBufferW, mapBufferH, zoomLevel, tileIndex_X, tileIndex_Y, tileLevel, oldElement, true, currentStageID);
                }
                if (mapElement != null && !mapElement.Equals(currentMap))
                {
                    setCurrentMap(mapElement.getID());
                    updateMap_Refresh();
                }
                else
                {
                    this.updateMap();
                }
            }
        }

        void IUndoHandler.Redo(UndoCommand cmd)
        {
            if (cmd is FillRandomPointsCommand)
            {
                FillRandomPointsCommand fillPointsCmd = (FillRandomPointsCommand)cmd;
                FillPointHistory pointCmd = null;
                MapElement mapElement = fillPointsCmd.getMapElement();
                for (int i = 0; i < fillPointsCmd.getPointsCount(); i++)
                {
                    pointCmd = fillPointsCmd.getPoint(i);
                    //Object oldElement = pointCmd.getTileElement_Org();
                    Object newElement = pointCmd.getTileElement_New();
                    int tileIndex_X = pointCmd.getTileIndex_X();
                    int tileIndex_Y = pointCmd.getTileIndex_Y();
                    short tileLevel = pointCmd.getTileLevel();
                    fillPoint(pointCmd.getMapElement(), mapBufferW, mapBufferH, zoomLevel, tileIndex_X, tileIndex_Y, tileLevel, newElement, true, pointCmd.getStageID());
                }
                if (mapElement != null && !mapElement.Equals(currentMap))
                {
                    setCurrentMap(mapElement.getID());
                    updateMap_Refresh();
                }
                else
                {
                    this.updateMap();
                }
            }
        }
        
        public void refreshHistory(ArrayList history)
        {
            if (historyDialog != null && historyDialog.checkBox＿Refresh.Checked)
            {
                historyDialog.refreshList(history);
            }
            refrehUndoRedoTSB();
        }
        #endregion

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_HistoryManager.Undo();
            refrehUndoRedoTSB();
        }

        private void 重做ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_HistoryManager.Redo();
            refrehUndoRedoTSB();
        }
        private void TSB_Undo_Click(object sender, EventArgs e)
        {
            m_HistoryManager.Undo();
            refrehUndoRedoTSB();
        }
        private void TSB_Redo_Click(object sender, EventArgs e)
        {
            m_HistoryManager.Redo();
            refrehUndoRedoTSB();
        }
        private void refrehUndoRedoTSB()
        {
            if (m_HistoryManager.CanUndo())
            {
                TSB_Undo.Enabled = true;
            }
            else
            {
                TSB_Undo.Enabled = false;
            }
            if (m_HistoryManager.CanRedo())
            {
                TSB_Redo.Enabled = true;
            }
            else
            {
                TSB_Redo.Enabled = false;
            }
        }
        private void 历史记录面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (historyDialog != null)
            {
                historyDialog.ShowDialog();
            }
        }
        //清空历史记录
        public void clearHistory()
        {
            m_HistoryManager.ClearUndoRedo();
            refrehUndoRedoTSB();
        }
        //合并场景和动画
        private void ToolStripMenuItem_Combine_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "c2d files (*.c2dx)|*.c2dx";
            dialog.FileName = "";
            dialog.Title = "合并场景和动画";
            DialogResult dr = dialog.ShowDialog();
            if (dr != DialogResult.OK)
            {
                return;
            }
            String path = dialog.FileName;
            if (Form_CombineRes.importAnims(this, path))
            {
                updateAllContainers();
                if (form_ProptiesManager != null)
                {
                    form_ProptiesManager.initParams(true);
                }

            }
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_ClearSpilth_MouseHover(object sender, EventArgs e)
        {
            showFunction("清除多余的图形元素");
        }

        private void button_CheckContainer_MouseHover(object sender, EventArgs e)
        {
            showFunction("图形容器使用统计与错误检查");
        }
        private void button_CheckContainer_Click(object sender, EventArgs e)
        {
            SmallDialog_ShowParagraph.showString("图形容器使用统计与错误检查", currentGfxContainer.getCondition());
        }

        private void button_AddGfxFolder_MouseHover(object sender, EventArgs e)
        {
            showFunction("添加图形元素风格");
        }


        private void button_DelGfxFolder_MouseHover(object sender, EventArgs e)
        {
            showFunction("删除图形元素风格");
        }



        private void button_NameFolder_MouseHover(object sender, EventArgs e)
        {
            showFunction("重新命名图形元素风格");
        }
        //清除多余的角色原型
        private void button_ClearATSpilth_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("即将合并相同角色原型，默认保留未使用原型，如果不保留请选择“否”，确定操作？", "警告：", MessageBoxButtons.YesNoCancel);
            if (res.Equals(DialogResult.Yes) || res.Equals(DialogResult.No))
            {
                mapsManager.antetypesManager.clearSpilth(res.Equals(DialogResult.No));
                updateContainer_AT_Buffer();
            }
        }
        private void 调色板编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        //图片混淆工具
        private void TMI_图片压缩工具_Click(object sender, EventArgs e)
        {
            
        }

        private void button_obj_refresh_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_ClearATSpilth_MouseHover(object sender, EventArgs e)
        {
            showFunction("清除冗余的角色原型");
        }

        private void button_ClearATSpilth_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_obj_import_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_Obj_Add_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_Obj_del_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_AutoGen_MouseEnter(object sender, EventArgs e)
        {
            showFunction("自动增加多个物理元素");
        }

        private void button_AutoGen_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_config_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_del_Phy_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_add_Phy_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_AddGfxFolder_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_DelGfxFolder_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_NameFolder_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_addMore_Grx_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_ClearSpilth_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_CheckContainer_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_del_Gfx_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void panel_Flag_MouseEnter(object sender, EventArgs e)
        {
            showFunction("翻转标志显示区域");
        }

        private void panel_Flag_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_addOne_Gfx_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_Copy_Gfx_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_Up_Gfx_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_Down_Gfx_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_Left_Gfx_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_Right_Gfx_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void 变量容器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form_VarsManager == null)
            {
                form_VarsManager = new Form_VarsAndFunctions(this);
            }
            else
            {
                form_VarsManager.initParams();
            }
            form_VarsManager.Show();
            form_VarsManager.Focus();

        }

        private void 脚本编辑器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form_ProptiesManager == null)
            {
                form_ProptiesManager = new Form_ProptiesManager(this);
            }
            else
            {
                form_ProptiesManager.initParams(false);
            }
            form_ProptiesManager.Show();
            form_ProptiesManager.Focus();
        }

        private void 图形IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.TILE_BG_GFX_ID = !Consts.TILE_BG_GFX_ID;
            图形IDToolStripMenuItem.Checked = Consts.TILE_BG_GFX_ID;
            this.updateMap_Refresh();
        }

        private void 文字编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form_TextsManager == null)
            {
                form_TextsManager = new Form_TextsManager(textsManager);
            }
            else
            {
                form_TextsManager.initParams();
            }
            form_TextsManager.ShowDialog();
        }


        private void button_ATPack_Add_Click(object sender, EventArgs e)
        {
            String name = "文件夹" + mapsManager.antetypesManager.Count();
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("新建角色原型文件夹", name);
            txtDialog.ShowDialog();
            name = txtDialog.getValue();
            AntetypeFolder folder = new AntetypeFolder(mapsManager.antetypesManager);
            folder.name = name;
            mapsManager.antetypesManager.Add(folder);
            refreshAntetypeSyleList();
            comboBox_ATFolders.SelectedIndex = mapsManager.antetypesManager.Count() - 1;
            updateContainer_AT_Buffer();
        }

        private void comboBox_ATFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noListBoxEvent)
            {
                return;
            }
            try
            {
                setCurrentAT(null);
                resetCurrentATFolder();
                updateContainer_AT_Buffer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void button_ATPack_Del_Click(object sender, EventArgs e)
        {
            int styleCount = mapsManager.antetypesManager.Count();
            if (styleCount == 1)
            {
                MessageBox.Show("不能删除最后一个文件夹");
                return;
            }
            AntetypeFolder currentFolder = mapsManager.antetypesManager[comboBox_ATFolders.SelectedIndex];
            if (currentFolder.Count() > 0)
            {
                MessageBox.Show("不能删除非空文件夹");
                return;
            }
            mapsManager.antetypesManager.RemoveAt(comboBox_ATFolders.SelectedIndex);
            refreshAntetypeSyleList();
            updateContainer_AT_Buffer();
        }

        private void button_ATPack_Rename_Click(object sender, EventArgs e)
        {
            AntetypeFolder currentFolder = (AntetypeFolder)mapsManager.antetypesManager[comboBox_ATFolders.SelectedIndex];
            if (currentFolder == null)
            {
                return;
            }
            String name = currentFolder.name;
            SmallDialog_WordEdit txtDialog = new SmallDialog_WordEdit("重命名角色原型文件夹", name);
            txtDialog.ShowDialog();
            name = txtDialog.getValue();
            currentFolder.name = name;
            refreshAntetypeSyleList();
            comboBox_ATFolders.SelectedIndex = currentFolder.GetID();
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button_configAT_Click(object sender, EventArgs e)
        {
            if (currentAntetype == null)
            {
                return;
            }
            Form_ConfigAnteType.configAntetype(currentAntetype);
            updateContainer_AT_Buffer();
            updateMap_Refresh();
            if (currentAntetype != null)
            {
                lable_AT.Text = currentAntetype.name+"";
            }

        }

        private void pictureBox_AT_DoubleClick(object sender, EventArgs e)
        {
            button_configAT_Click(sender, e);
        }

        private void 地图角色初始帧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.LVL_Obj_FrameID = !Consts.LVL_Obj_FrameID;
            地图角色初始帧ToolStripMenuItem.Checked = Consts.LVL_Obj_FrameID;
            this.updateMap_Refresh();
        }

        private void button_cloneAnteType_Click(object sender, EventArgs e)
        {
            if (currentAntetype == null||currentAntetypeFolder == null)
            {
                return;
            }
            Antetype newInstance = currentAntetype.clone(currentAntetypeFolder);
            currentAntetypeFolder.Add(newInstance);
            moveFoxus_AT(currentAntetypeFolder.Count() - 1);
            updateContainer_AT_Buffer();
        }

        private void TSB_Replace_Click(object sender, EventArgs e)
        {
            if (currentMap == null)
            {
                return;
            }
            if (Form_ReplaceAnteType.replaceAnteTypes(currentMap))
            {
                refreshCanvasBuffer_Obj = true;
                updateMap();
            }
        }

        private void button_GenIDs_Click(object sender, EventArgs e)
        {
            if (currentAntetypeFolder == null)
            {
                return;
            }
            for (int i = 0; i < currentAntetypeFolder.Count(); i++)
            {
                Antetype anteType = (Antetype)currentAntetypeFolder[i];
                String IDName = "ANTETYPE_" + currentAntetypeFolder.name+"_" + anteType.name;
                int IDValue = anteType.GetID();
                VarElement varElementNew = new VarElement(iDsManager, Consts.PARAM_INT);
                varElementNew.setValue(IDValue);
                varElementNew.name = IDName;
                iDsManager.updateVarElement(varElementNew);
            }
            iDsManager.refreshUI();
        }

        private void button_GenIDs_MouseHover(object sender, EventArgs e)
        {
            showFunction("导出到ID常量表");
        }

        private void button_GenIDs_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_cloneAnteType_MouseHover(object sender, EventArgs e)
        {
            showFunction("复制当前角色原型");
        }

        private void button_cloneAnteType_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void button_configAT_MouseHover(object sender, EventArgs e)
        {
            showFunction("编辑当前角色原型");
        }

        private void button_configAT_MouseLeave(object sender, EventArgs e)
        {
            showFunction("");
        }

        private void 选项ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 生成地图位图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG图片 (*.png)|*.png|BMP位图 (*.bmp)|*.bmp|GIF图片 (*.gif)|*.gif";
            dialog.DefaultExt = "*.png";
            dialog.Title = "生成地图位图";
            DialogResult dr = dialog.ShowDialog();
            if (dr.Equals(DialogResult.OK))
            {
                String fileNameOld = dialog.FileName;
                String fileName = fileNameOld.Substring(0, fileNameOld.LastIndexOf('.'));
                String postfix = fileNameOld.Replace(fileName, "");
                for (int i = 0; i < mapsManager.getElementCount(); i++)
                {
                    Bitmap bitmapDraw = null;
                    MapElement mapElement = mapsManager.getElement(i);
                    Bitmap bitmap = new Bitmap(mapElement.getTileW() * mapElement.getMapW(), mapElement.getTileW() * mapElement.getMapH());
                    Graphics g = Graphics.FromImage(bitmap);
                    mapElement.displayTile(g, bitmap.Width, bitmap.Height, 1, 0, 0, mapElement.getMapW()-1, mapElement.getMapH()-1);
                    mapElement.redrawAll(g);
                    g.Dispose();
                    bitmapDraw = bitmap;
                    if (zoomLevel != 1)
                    {
                        Bitmap bitmap2 = new Bitmap((int)(bitmap.Width * zoomLevel), (int)(bitmap.Height * zoomLevel));
                        Graphics g2 = Graphics.FromImage(bitmap2);
                        g2.DrawImage(bitmap, new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                        g2.Dispose();
                        bitmap.Dispose();
                        bitmapDraw = bitmap2;
                    }
                    if (bitmapDraw != null)
                    {
                        bitmapDraw.Save(fileName + i + postfix);
                        bitmapDraw.Dispose();
                    }

                }
            }
        }

        private void Form_Main_Activated(object sender, EventArgs e)
        {
        }

        private void 生成脚本文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "TXT文档 (*.txt)|*.txt";
            dialog.DefaultExt = "*.txt";
            dialog.Title = "生成脚本文本";
            DialogResult dr = dialog.ShowDialog();
            if (dr.Equals(DialogResult.OK))
            {
                String filePath_Bin = dialog.FileName;
                FileStream fs_bin = null;
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
                    //triggersManager.exportTexts(fs_bin);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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

            }
        }
        //单元上下移动(Ctrl+上/下箭头)
        private void listBox_Maps_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox_Maps.Focused)
            {
                if (e.Control)
                {
                    if (e.KeyValue == (int)Keys.Up)
                    {
                        moveUpElement();
                    }
                    else if (e.KeyValue == (int)Keys.Down)
                    {
                        moveDownElement();
                    }
                    e.Handled = true;
                }
                else if (e.KeyValue == (int)Keys.Delete)
                {
                    deleteElement();
                }
            }
        }
        public Object getParamByIndex(byte type,int index)
        {
            switch (type)
            {
                case Consts.PARAM_INT:
                    return index;
                case Consts.PARAM_STR:
                    return textsManager.getElement(index);
                case Consts.PARAM_INT_VAR:
                    return varIntManager.getElement(index);
                case Consts.PARAM_STR_VAR:
                    return varStringManager.getElement(index);
                case Consts.PARAM_INT_ID:
                    return iDsManager.getElement(index);
                case Consts.PARAM_PROP:
                    return propertyTypesManager.getElement(index);
            }
            return null;
        }
        public Object getDefaultParams(byte type)
        {
            switch (type)
            {
                case Consts.PARAM_INT:
                    return 0;
                case Consts.PARAM_STR:
                case Consts.PARAM_INT_VAR:
                case Consts.PARAM_STR_VAR:
                case Consts.PARAM_PROP:
                case Consts.PARAM_INT_ID:
                    return null;
            }
            return null;
        }
        //Form_WebBrowser browser = null;
        private void Form_Main_Shown(object sender, EventArgs e)
        {
            form_Main.initWorld();
            //if (browser == null)
            //{
            //    browser = new Form_WebBrowser();
            //    browser.Show();
            //}

        }
        private void Form_Main_Load(object sender, EventArgs e)
        {

        }

        private void 文件批量打包ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form_PackFiles().ShowDialog();
        }

        private void 调色板编辑器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form_Pallete().ShowDialog();
        }

        private void 图片格式转换ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Form_ImageProcessor().ShowDialog();
        }

        private void 压缩混淆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form_Image_Compress().ShowDialog();
        }

        private void 按比例缩放ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form_ImageZoomer().ShowDialog();
        }

        private void listBox_stage_KeyDown(object sender, KeyEventArgs e)
        {
            int id=listBox_stage.SelectedIndex;
            if (listBox_stage.Focused && id >= 0 && currentMap != null)
            {
                if (e.Control)
                {
                    if (e.KeyValue == (int)Keys.Up)
                    {
                        moveUpElement();
                    }
                    else if (e.KeyValue == (int)Keys.Down)
                    {
                        moveDownElement();
                    }
                    e.Handled = true;
                }
                else if (e.KeyValue == (int)Keys.Delete)
                {
                    deleteElement();
                }
            }
        }

        private void listBox_stage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            listBox_stage.Focus();
            if (e.Button == MouseButtons.Left && currentMap != null)
            {
                MouseEventArgs ex = (MouseEventArgs)e;
                if (ex.Y < listBox_stage.ItemHeight * listBox_stage.Items.Count)
                {
                    configElement();
                }
                else
                {
                    addElement();
                }
            }
        }

        private void listBox_stage_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.Equals(Form.ActiveForm))
            {
                return;
            }
            listBox_stage.Focus();
            if (e.Button == MouseButtons.Right)
            {
                if (listBox_stage.SelectedIndex >= 0)
                {
                    contextMenuStrip_listItem.Items[4].Enabled = true;
                    contextMenuStrip_listItem.Items[5].Enabled = true;
                }
                else
                {
                    contextMenuStrip_listItem.Items[4].Enabled = false;
                    contextMenuStrip_listItem.Items[5].Enabled = false;
                }
                contextMenuStrip_listItem.Show((Control)sender, e.X, e.Y);
            }
        }

        private void listBox_stage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.noListBoxEvent || currentMap==null)
            {
                return;
            }
            int imgMappingCount = 0;
            if (currentStageID >= 0)
            {
                imgMappingCount+=((StageElement)currentMap.stageList.getElement(currentStageID)).imgMappingList.getElementCount();
            }
            if (listBox_stage.SelectedIndex >= 0)
            {
                currentStageID = listBox_stage.SelectedIndex;
                imgMappingCount+=((StageElement)currentMap.stageList.getElement(currentStageID)).imgMappingList.getElementCount();
            }
            refreshCanvasBuffer_Obj = true;
            if (imgMappingCount > 0)
            {
                refreshCanvasBuffer_tile = true;
            }
            this.updateMap();
            kss_refreshUI(currentMap, currentStageID);
        }

        private void 地图角色NPC编号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.LVL_Obj_NPC_ID = !Consts.LVL_Obj_NPC_ID;
            地图角色NPC编号ToolStripMenuItem.Checked = Consts.LVL_Obj_NPC_ID;
            this.updateMap_Refresh();
        }

        private void 地图角色锚点坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.LVL_Obj_Anchor = !Consts.LVL_Obj_Anchor;
            地图角色锚点坐标ToolStripMenuItem.Checked = Consts.LVL_Obj_Anchor;
            this.updateMap_Refresh();
        }

        //编译全部脚本文件
        public void compileScripts(ShowString shower)
        {
            String scriptSuffix = ".c2ds";//脚本代码文件后缀名
            String scriptFilePath = "";
            bool lostKssExe = false;
            if (FileTypeRegister.FileTypeRegistered(scriptSuffix))
            {
                FileTypeRegInfo regInfo = FileTypeRegister.GetFileTypeRegInfo(scriptSuffix);
                String exePath = regInfo.ExePath;
                if (File.Exists(exePath))
                {
                    for (int i = 0; i < mapsManagerForExport.listExpScriptFiles.Count; i++)
                    {
                        String file = (String)mapsManagerForExport.listExpScriptFiles[i];
                        scriptFilePath = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_KSS + file;
                        if (File.Exists(scriptFilePath))
                        {
                            if (shower != null)
                            {
                                shower.showString("开始编译脚本文件[" + scriptFilePath + "]");
                            }
                            IOUtil.OpenProcess(exePath, "-CF " + scriptFilePath, null, true);
                            String kseFilePath = scriptFilePath.Replace(".c2ds", ".bin");
                            String kseFileName = kseFilePath.Substring(kseFilePath.LastIndexOf('\\') + 1, kseFilePath.Length - (kseFilePath.LastIndexOf('\\') + 1));
                            if (File.Exists(kseFilePath))
                            {
                                File.Copy(kseFilePath, Consts.exportC2DBinFolder + kseFileName, true);
                                File.Delete(kseFilePath);
                            }
                            else
                            {
                                if (shower != null)
                                {
                                    shower.showString("编译脚本文件[" + scriptFilePath + "]过程中发生问题");
                                }
                            }
                        }
                        else
                        {
                            if (shower != null)
                            {
                                shower.showString("丢失脚本文件[" + scriptFilePath + "]，因此无法汇编此脚本");
                            }
                        }
                    }
                }
                else
                {
                    lostKssExe = true;
                }

            }
            else
            {
                lostKssExe = true;
            }
            if (lostKssExe)
            {
                MessageBox.Show("没有找到被注册的\"CycloneScriptEditor.exe\"，请在任意位置打开一次，它将自动被注册。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UIToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void TSB_autoTile_Click(object sender, EventArgs e)
        {
            if (TSB_autoTile.Checked)
            {
                TSB_autoTile.Checked = false;
                return;
            }
            String canAutoTileInf = canAutoTile();
            if (canAutoTileInf != null)
            {
                MessageBox.Show(canAutoTileInf, "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TSB_autoTile.Checked = true;
        }
        public String canAutoTile()
        {
            if (currentMap == null || currentTile_Gfx == null || (Consts.currentLevel != Consts.LEVEL_TILE_BG && Consts.currentLevel != Consts.LEVEL_TILE_SUR))
            {
                TSB_autoTile.Checked = false;
                return("请在选中一个地图，切换至底层地形层或者融合地形层，并激活一个指定的图形元素，才能激活自动地形。");
            }
            //if (editMode != EDITMOD_PENCIL && editMode != EDITMOD_ERASER)
            //{
            //    TSB_autoTile.Checked = false;
            //    return ("只有在铅笔或者橡皮模式下才能激活自动地形。");
            //}
            if (!currentMap.tileGfxContainer.Equals(currentGfxContainer))
            {
                TSB_autoTile.Checked = false;
                return("当前的地图风格不是“" + currentGfxContainer.name + "”");
            }
            int idStart = currentTile_Gfx.GetID();
            int rowCount = pictureBox_Gfx.Width / currentMap.getTileW();
            int idStartX = idStart % rowCount;
            int idStartY = idStart / rowCount;
            if (idStartX + 6 - 1 >= rowCount || (idStartX + 6 - 1 + (idStartY + 10 - 1) * rowCount) >= currentGfxContainer.Count())
            {
                TSB_autoTile.Checked = false;
                return("你激活的图形元素不能作为自动地形元素集合的左上角单元！");
            }
            return null;
        }

        private void 融合地形层上图形元素编号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.TILE_SUR_GFX_ID = !Consts.TILE_SUR_GFX_ID;
            融合地形层上图形元素编号ToolStripMenuItem.Checked = Consts.TILE_SUR_GFX_ID;
            this.updateMap_Refresh();
        }

        private void TSB_fillCorner_Click(object sender, EventArgs e)
        {
            TSB_fillCorner.Checked = !TSB_fillCorner.Checked;
        }

        private void 影片动画编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_MAnimation.Show();
        }
        //##################################### 脚本管理界面开始 #############################################
        private List<ObjectVector> listCarrier=new List<ObjectVector>();
        private List<String> listFiles = new List<String>();
        public bool isShown = false;
        public void kss_refreshUI(MapElement map,int stageID)
        {
            if (map == null || stageID < 0 || stageID >= map.stageList.getElementCount())
            {
                return;
            }
            //记录之前的选择ID
            int idCarrier = listBox_Carrier.SelectedIndex;
            int idKssFile = listBox_Files.SelectedIndex;
            //开始刷新数据
            listBox_Carrier.BeginUpdate();
            listBox_Files.BeginUpdate();
            listCarrier.Clear();
            listFiles.Clear();
            listBox_Carrier.Items.Clear();
            listBox_Files.Items.Clear();
            //加入场景脚本
            listCarrier.Add(((StageElement)map.stageList.getElement(stageID)).scriptList);
            listBox_Carrier.Items.Add("**场景脚本**");
            listCarrier[0].setUI(listBox_Files);
            //加入NPC脚本
            for (int i = 0; i < map.getMapW(); i++)
            {
                for (int j = 0; j < map.getMapH(); j++)
                {
                    if (map.mapData[i, j] != null)
                    {
                        TileObjectElement element = map.mapData[i, j].tile_objectList[stageID];
                        if (element != null && element.NpcID > 0)
                        {
                            listCarrier.Add(element.scriptList);
                            element.scriptList.setUI(listBox_Files);
                            String name = "[NPC_" + MathUtil.getStringOfInt(element.NpcID,4) + "]";
                            if (element.antetype != null)
                            {
                                name += element.antetype.name;
                            }
                            listBox_Carrier.Items.Add(name);
                        }

                    }
                }
            }
            //还原条目焦点
            if (idCarrier < 0)
            {
                idCarrier = 0;
            }
            if (idCarrier >= listBox_Carrier.Items.Count)
            {
                idCarrier = listBox_Carrier.Items.Count - 1;
            }
            listBox_Carrier.SelectedIndex = idCarrier;
            //刷新kss文件列表
            listCarrier[idCarrier].refreshUI(listBox_Files);
            //重新绘制窗口
            listBox_Carrier.EndUpdate();
            listBox_Files.EndUpdate();
        }
        //设置kss脚本单元
        public void kss_configElement()
        {
            int idCarrier = listBox_Carrier.SelectedIndex;
            int idFile = listBox_Files.SelectedIndex;
            if (idCarrier >= 0 && idFile >= 0)
            {
                kss_checkPath();
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;
                dialog.Filter = "c2dx files (*.c2dx)|*.c2dx";
                dialog.FileName = "";
                dialog.Title = "重设kss脚本文件";
                String subFolderName = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_KSS;
                dialog.InitialDirectory = subFolderName;
                DialogResult dr = dialog.ShowDialog();
                if (dr != DialogResult.OK)
                {
                    return;
                }
                ScriptFileElement element = new ScriptFileElement(listCarrier[idCarrier]);
                String value = dialog.FileName.Replace(subFolderName, "");
                element.setValue(value);
                listCarrier[idCarrier].setElement(element, idFile);
            }
        }
        //增加kss脚本单元
        public void kss_addElement()
        {
            int idCarrier = listBox_Carrier.SelectedIndex;
            int idFile = listBox_Files.SelectedIndex;
            if (idCarrier >= 0)
            {
                kss_checkPath();
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = true;
                dialog.Filter = "c2ds files (*.c2ds)|*.c2ds";
                dialog.FileName = "";
                dialog.Title = "添加kss脚本文件";
                String subFolderName = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_KSS;
                dialog.InitialDirectory = subFolderName;
                DialogResult dr = dialog.ShowDialog();
                if (dr != DialogResult.OK)
                {
                    return;
                }
                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    ScriptFileElement element = new ScriptFileElement(listCarrier[idCarrier]);
                    String srcPath = dialog.FileNames[i];
                    String strFile = srcPath.Substring(srcPath.LastIndexOf('\\') + 1, srcPath.Length - srcPath.LastIndexOf('\\') - 1);
                    String strFolder = srcPath.Replace(strFile, "");
                    String destPath = subFolderName + strFile;
                    bool dontCopy = false;
                    if (strFolder.Equals(subFolderName)||srcPath.Equals(destPath))
                    {
                        dontCopy = true;
                    }
                    if (!dontCopy && File.Exists(destPath))
                    {
                        DialogResult drI = MessageBox.Show("工程目录已经包含同名文件，是否替换？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (drI == DialogResult.No)
                        {
                            dontCopy = true;
                        }
                    }
                    if (!dontCopy)
                    {
                        File.Copy(srcPath, destPath,true);
                    }
                    element.setValue(strFile);
                    listCarrier[idCarrier].addElement(element);
                }
            }
        }
        //打开kss脚本单元
        public void kss_openElement()
        {
            int idCarrier = listBox_Carrier.SelectedIndex;
            int idFile = listBox_Files.SelectedIndex;
            if (idCarrier >= 0 && idFile >= 0)
            {
                String KSS_SUFFIX = ".c2ds";//kss脚本代码文件后缀名
                String filePath = "";
                int errorType = 0;
                if (FileTypeRegister.FileTypeRegistered(KSS_SUFFIX))
                {
                    FileTypeRegInfo regInfo = FileTypeRegister.GetFileTypeRegInfo(KSS_SUFFIX);
                    String exePath = regInfo.ExePath;
                    if (File.Exists(exePath))
                    {
                        filePath = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_KSS + (String)((ScriptFileElement)listCarrier[idCarrier].getElement(idFile)).getValue();
                        if (File.Exists(filePath))
                        {
                            IOUtil.OpenProcess(exePath, filePath, null, false);
                        }
                        else
                        {
                            errorType = 1;

                        }
                    }
                    else
                    {
                        errorType = 2;
                    }

                }
                else
                {
                    errorType = 2;
                }
                //显示错误
                if (errorType == 1)
                {
                    MessageBox.Show("kss脚本文件不存在，" + filePath, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (errorType == 2)
                {
                    MessageBox.Show("没有找到被注册的\"CycloneScriptEditor.exe\"，请在任意位置打开一次，它将自动被注册。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        //检查子文件夹
        private void kss_checkPath()
        {
            String subFolderName = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_KSS;
            if (!Directory.Exists(subFolderName))
            {
                Directory.CreateDirectory(subFolderName);
            }
        }


        private void kss_listBox_Files_KeyDown(object sender, KeyEventArgs e)
        {
            int idCarrier = listBox_Carrier.SelectedIndex;
            int idFile = listBox_Files.SelectedIndex;
            if (listBox_Files.Focused && idCarrier >=0 && idFile >= 0)
            {
                if (e.Control)
                {
                    if (e.KeyValue == (int)Keys.Up)
                    {
                        listCarrier[idCarrier].moveUpElement(idFile);
                    }
                    else if (e.KeyValue == (int)Keys.Down)
                    {
                        listCarrier[idCarrier].moveDownElement(idFile);
                    }
                    e.Handled = true;
                }
                else if (e.KeyValue == (int)Keys.Delete)
                {
                    listCarrier[idCarrier].removeElement(idFile);
                }
            }
        }
        private void kss_listBox_Files_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBox_Files.Focus();
            if (e.Button == MouseButtons.Left)
            {
                MouseEventArgs ex = (MouseEventArgs)e;
                if (ex.Y < listBox_Files.ItemHeight * listBox_Files.Items.Count)
                {
                    kss_openElement();
                }
                else
                {
                    kss_addElement();
                }
            }
        }


        private void kss_listBox_Files_MouseDown(object sender, MouseEventArgs e)
        {
            listBox_Files.Focus();
            if (e.Button == MouseButtons.Right)
            {
                if (listBox_Files.SelectedIndex >= 0)
                {
                    for (int i = 1; i < contextMenuStrip_listItemkss.Items.Count; i++)
                    {
                        contextMenuStrip_listItemkss.Items[i].Enabled = true;
                    }
                }
                else
                {
                    for (int i = 1; i < contextMenuStrip_listItemkss.Items.Count; i++)
                    {
                        contextMenuStrip_listItemkss.Items[i].Enabled = false;
                    }
                }
                contextMenuStrip_listItemkss.Show((Control)sender, e.X, e.Y);
            }
        }

        private void kss_新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kss_addElement();
        }

        private void kss_编辑kss脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kss_openElement();
        }
        private void kss_上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idCarrier = listBox_Carrier.SelectedIndex;
            int idFile = listBox_Files.SelectedIndex;
            if (listBox_Files.Focused && idCarrier >= 0 && idFile >= 0)
            {
                listCarrier[idCarrier].moveUpElement(idFile);
            }
        }
        private void kss_下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idCarrier = listBox_Carrier.SelectedIndex;
            int idFile = listBox_Files.SelectedIndex;
            if (listBox_Files.Focused && idCarrier >= 0 && idFile >= 0)
            {
                listCarrier[idCarrier].moveDownElement(idFile);
            }
        }

        private void kss_删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idCarrier = listBox_Carrier.SelectedIndex;
            int idFile = listBox_Files.SelectedIndex;
            if (listBox_Files.Focused && idCarrier >= 0 && idFile >= 0)
            {
                listCarrier[idCarrier].removeElement(idFile);
            }
        }
        private void kss_设置双击条目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kss_configElement();
        }

        private void kss_listBox_Carrier_SelectedIndexChanged(object sender, EventArgs e)
        {
            //刷新kss文件列表
            int idCarrier = listBox_Carrier.SelectedIndex;
            if (idCarrier >= 0)
            {
                listCarrier[idCarrier].refreshUI(listBox_Files);
            }
        }
        private void Form_ScriptsManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            this.isShown = false;
            Console.WriteLine("hide");
        }

        private void listBox_Carrier_MouseDown(object sender, MouseEventArgs e)
        {
            listBox_Carrier.Focus();
            if (e.Button == MouseButtons.Right)
            {
                CMS_RefreshKss.Show((Control)sender, e.X, e.Y);
            }
        }

        private void 刷新载体列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.kss_refreshUI(currentMap, currentStageID);
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.kss_refreshUI(currentMap, currentStageID);
        }

        private void tabPage_Scripts_Enter(object sender, EventArgs e)
        {
            this.kss_refreshUI(currentMap, currentStageID);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.cyclone2d.cn");
        }
        //##################################### 脚本管理界面完毕 #############################################
    }
}