using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Cyclone.alg.util
{
    class Consts
    {
        public static String PATH_PROJECT_FILEPATH = null;//工程文件路径
        public static String PATH_PROJECT_FOLDER = null;//工作目录
        public static String PATH_PROJECT_FILENAME = null;//工程文件名称
        public static String PATH_EXE = null;//软件文件路径
        public static String PATH_EXE_FOLDER = null;//软件目录路径
        public static String SUBPARH_IMG = "imgs\\";//图片子目录
        public static String SUBPARH_SOUND = "sounds\\";//声音子目录
        public static String SUBPARH_KSS = "c2ds\\";//kss脚本子目录

        //====路径初始化=====================================
        public static bool initPaths(String []args)
        {
            Consts.PATH_EXE = Application.ExecutablePath;
            Consts.PATH_EXE_FOLDER = Consts.PATH_EXE.Substring(0, Consts.PATH_EXE.LastIndexOf('\\'));
            bool findFile = false;

            if (args != null && args.Length > 0)//从已经存在的文件启动。
            {
                for (int i = 0; i < args.Length; i++)
                {
                    Consts.PATH_PROJECT_FILEPATH += args[i];
                    if (Consts.PATH_PROJECT_FILEPATH.EndsWith(".C2DX") || Consts.PATH_PROJECT_FILEPATH.EndsWith(".c2dx"))
                    {
                        break;
                    }
                    else
                    {
                        Consts.PATH_PROJECT_FILEPATH += " ";
                    }
                }
                if (File.Exists(Consts.PATH_PROJECT_FILEPATH))
                {
                    findFile = true;                  
                }
                else
                {
                    MessageBox.Show("注意不要在路径中含有空格！");
                }
            }
            if (!findFile) //从软件启动，要求建立一个空白文档。                               
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "c2d files (*.c2dx)|*.c2dx";
                dialog.FileName = "c2d文档";
                dialog.Title = "新建工程文件";
                DialogResult dr = dialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Consts.PATH_PROJECT_FILEPATH = dialog.FileName;
                    try
                    {
                        FileStream fs=File.Create(Consts.PATH_PROJECT_FILEPATH);
                        fs.Flush();
                        fs.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    return false;//退出软件(不允许没有工作文档的情况下启动)
                }
            }
            updateFolders();
            return true;
        }
        //刷新目录
        public static void updateFolders()
        {
            Consts.PATH_PROJECT_FOLDER = Consts.PATH_PROJECT_FILEPATH.Substring(0, Consts.PATH_PROJECT_FILEPATH.LastIndexOf('\\') + 1);
            Directory.SetCurrentDirectory(Consts.PATH_PROJECT_FOLDER);
            Consts.PATH_PROJECT_FOLDER = Directory.GetCurrentDirectory() + "\\";
            int folderLen=Consts.PATH_PROJECT_FILEPATH.LastIndexOf('\\')+1;
            Consts.PATH_PROJECT_FILEPATH = Consts.PATH_PROJECT_FOLDER+Consts.PATH_PROJECT_FILEPATH.Substring(folderLen, Consts.PATH_PROJECT_FILEPATH.Length - folderLen);
            PATH_PROJECT_FILENAME = Consts.PATH_PROJECT_FILEPATH.Replace(Consts.PATH_PROJECT_FOLDER, "");
        }
        //绘图常量定义==============================================
        //MIDP2.0中的翻转标志(顺时针)
        public const byte TRANS_NONE = 0;
        public const byte TRANS_MIRROR_ROT180 = 1;//垂直翻转         (先旋转再镜像)
        public const byte TRANS_MIRROR = 2;//水平翻转
        public const byte TRANS_ROT180 = 3;//旋转180度
        public const byte TRANS_MIRROR_ROT270 = 4;//右上角对折左下角 (先旋转再镜像)
        public const byte TRANS_ROT90 = 5;//旋转90度
        public const byte TRANS_ROT270 = 6;//旋转270度
        public const byte TRANS_MIRROR_ROT90 = 7;//左上角对折右下角  (先旋转再镜像)
        //MIDP2.0中的定位标志
        public const int BASELINE = 64;
        public const int BOTTOM = 32;
        public const int DOTTED = 1;
        public const int HCENTER = 1; 
        public const int LEFT = 4;
        public const int RIGHT = 8;
        public const int SOLID = 0;
        public const int TOP = 16;
        public const int VCENTER = 2;
        //翻转变换常量
        public static byte [][]TRANS_ARRAY = new byte[][]
          {
              new byte[]{TRANS_NONE, TRANS_MIRROR_ROT180, TRANS_MIRROR, TRANS_ROT180, TRANS_MIRROR_ROT270, TRANS_ROT90, TRANS_ROT270, TRANS_MIRROR_ROT90}, //TRANS_NONE
              new byte[]{TRANS_MIRROR_ROT180, TRANS_NONE, TRANS_ROT180, TRANS_MIRROR, TRANS_ROT90, TRANS_MIRROR_ROT270, TRANS_MIRROR_ROT90, TRANS_ROT270}, //TRANS_MIRROR_ROT180
              new byte[]{TRANS_MIRROR, TRANS_ROT180, TRANS_NONE, TRANS_MIRROR_ROT180, TRANS_ROT270, TRANS_MIRROR_ROT90, TRANS_MIRROR_ROT270, TRANS_ROT90}, //TRANS_MIRROR
              new byte[]{TRANS_ROT180, TRANS_MIRROR, TRANS_MIRROR_ROT180, TRANS_NONE, TRANS_MIRROR_ROT90, TRANS_ROT270, TRANS_ROT90, TRANS_MIRROR_ROT270}, //TRANS_ROT180
              new byte[]{TRANS_MIRROR_ROT270, TRANS_ROT270, TRANS_ROT90, TRANS_MIRROR_ROT90, TRANS_NONE, TRANS_MIRROR, TRANS_MIRROR_ROT180, TRANS_ROT180}, //TRANS_MIRROR_ROT270
              new byte[]{TRANS_ROT90, TRANS_MIRROR_ROT90, TRANS_MIRROR_ROT270, TRANS_ROT270, TRANS_MIRROR_ROT180, TRANS_ROT180, TRANS_NONE, TRANS_MIRROR}, //TRANS_ROT90
              new byte[]{TRANS_ROT270, TRANS_MIRROR_ROT270, TRANS_MIRROR_ROT90, TRANS_ROT90, TRANS_MIRROR, TRANS_NONE, TRANS_ROT180, TRANS_MIRROR_ROT180}, //TRANS_ROT270
              new byte[]{TRANS_MIRROR_ROT90, TRANS_ROT90, TRANS_ROT270, TRANS_MIRROR_ROT270, TRANS_ROT180, TRANS_MIRROR_ROT180, TRANS_MIRROR, TRANS_NONE}, //TRANS_MIRROR_ROT90
          };
        //翻转变换
          public static byte getTransFlag(byte flagOrg,byte flagTrans)
          {
            if(flagOrg<0||flagOrg>TRANS_MIRROR_ROT90||flagTrans<0||flagTrans>TRANS_MIRROR_ROT90)
            {
              return 0;
            }
            return TRANS_ARRAY[flagOrg][flagTrans];
          }
        //返回翻转标志描述
        public static String getTransFlagInf(byte flag)
        {
            String s = null;
            switch (flag)
            {
                case Consts.TRANS_NONE://无
                    s = "无翻转";
                    break;
                case Consts.TRANS_MIRROR_ROT180://垂直翻转
                    s = "垂直翻转";
                    break;
                case Consts.TRANS_MIRROR://水平翻转
                    s = "水平翻转";
                    break;
                case Consts.TRANS_ROT180://旋转180度
                    s = "旋转180度";
                    break;
                case Consts.TRANS_MIRROR_ROT270://右上角对折左下角
                    s = "右上角对折左下角";
                    break;
                case Consts.TRANS_ROT90://旋转90度
                    s = "旋转90度";
                    break;
                case Consts.TRANS_ROT270://旋转270度
                    s = "旋转270度";
                    break;
                case Consts.TRANS_MIRROR_ROT90://左上角对折右下角
                    s = "左上角对折右下角";
                    break;
            }
            return s;
        }
        //普通常量-------------------------------------------------
        //字体
        public static Font fontDef = new Font("SimSun", 16, FontStyle.Regular, GraphicsUnit.Pixel);
        public static Font fontSmall = new Font("SimSun", 12, FontStyle.Regular, GraphicsUnit.Pixel);
        public static Font fontTiny = new Font("SimSun", 10, FontStyle.Regular, GraphicsUnit.Pixel);
        public static Font fontMicro = new Font("SimSun", 9, FontStyle.Regular, GraphicsUnit.Pixel);
        public static Font fontBig = new Font("SimSun", 20, FontStyle.Regular, GraphicsUnit.Pixel);
        //颜色
        public static int colorRed = 0xFF0000;
        public static int colorDarkBlue = 0x1F99CE;
        public static int colorDarkGreen = 0x009543;
        public static int colorDarkYellow = 0xFADF00;
        public static int colorBlack = 0x00;
        public static int colorWhite = 0xFFFFFF;
        public static int colorBlue = 0x0000FF;
        public static int colorGray = 0xECE9D8;
        public static int colorGrid = 0x009394;//0x80A7A7;//0x29594f;
        public static int colorGrid1 = 0xAAAAAA;//0x80A7A7;//0x29594f;
        public static int colorDarkGray = 0x777777;
        public static int colorLightGray = 0xd3d3d3;
        public static int colorDarkGray2 = 0x333333;

        public static int colorNumber = 0x5B2B23;
        public static int color_ClipMask = 0xB71807;
        public static int color_MapMask = 0xb5858d;
        public static int color_ClipBorder = 0x00A9D2;
        public static int color_ruler = 0x00A9D2;
        public static int color_green = 0x00FF00;
        public static int color_red = 0xFF0000;
        public static int color_border = 0x7F9DB9;
        public static int color_text = 0xFF0000;
        public static int color_white = 0xFFFFFF;
        public static int colorFrameInf = 0x00;//动画编辑器帧信息文字颜色
        public static int colorAnimBG = 0xFFFFFF;//动画编辑器背景颜色
        public static int colorAnimSelect = 0x0;//动画编辑器选择线的颜色
        public static int colorMFrameEdit_Axis = 0x009394;//影片编辑器XY轴颜色
        public static float loadingProcess = 0;//载入进度
        //用户配置信息设定-------------------------------------------------
        /***********************载入工程界面***********************/
        //public static SmallDialog_Loading loadingDialog = null;
        /***********************切片编辑界面***********************/
        public static bool showCrossLine = true;//显示中心线
        public static int clipEditorBgColor = 0xFFFFFF;//切片编辑区背景颜色
        public static int animClipW = 16;//动画切片默认选取大小
        public static int animClipH = 16;
        public static int mapClipW = 16; //地图切片固定选取大小
        public static int mapClipH = 16;
        /***********************动画编辑界面***********************/
        public static bool showBgGrid = false;//显示动画编辑界面的网格
        public static bool showLogicBox = true;//显示逻辑框
        public static bool showFrameInf = true;//显示动画帧信息
        public static int screenWidth = 240;//屏幕宽度
        public static int screenHeight = 320;//屏幕高度
        public static bool showScreenFrame = true;//显示屏幕周围框
        public static bool showMFrameEdit_Axis = true;//影片动画编辑器界面显示XY轴
        public static bool moveAlignPixel = true;//移动时对齐到像素
        public static bool textureLinear = true; //OpenGL的贴图渲染是否采用线性插值
        public static bool transferClipMode = false;//允许跨图片转移切块
        /***********************导出界面***********************/
        //被数据包含标志信息
        public static bool exp_ActionOffset = false;      //是否导出动作位移信息
        public static int exp_ActionOffsetType = ActionOffset_X;//导出动作偏移类型
        public static bool exp_confuseImgs = false;      //是否混淆图片
        public static bool exp_splitAnimation = false;   //是否分立动画数据

        public static int exp_ImgFormat_Anim = 0;        //导出动画图片的格式
        public static int exp_ImgFormat_Map = 0;         //导出地图图片的格式

        public const int ActionOffset_X = 0;             //导出数据包含X方向动作偏移
        public const int ActionOffset_XY = 1;            //导出数据包含XY方向动作偏移
        public const int ActionOffset_XYZ = 2;           //导出数据包含XYZ方向动作偏移

        public static bool exp_copileScripts = false;    //导出时是否重新编译脚本
        public static bool exp_bakboolean = false;       //备用
        //非数据包含标志信息
        public static String exportFolder = "";         //导出资源的文件夹
        public static String exportC2DBinFolder = "";    //导出C2D引擎的数据资源的文件夹
        public static String exportOhterBinFolder = "";    //导出其它引擎的数据资源的文件夹
        public static String exportFileName = "";    //导出资源文件名称，不带后缀名
        public static String exportFilePath = "";       //导出资源文件路径
        /***********************地图编辑界面***********************/
        public static int MapImgFixWidth = 256;//固定地图图片的宽度
        public static bool showStringInPhyLevel = false;//在物理层显示数字标记
        //图层定义
        public const byte LEVEL_PHYSICS = 1;//物理标记层
        public const byte LEVEL_TILE_BG = 2;//底层地形层
        public const byte LEVEL_TILE_SUR = 4;//融合地形层
        public const byte LEVEL_TILE_OBJ = 8;//对象地形层
        public const byte LEVEL_OBJ_MASK = 16;//无关对象层
        public const byte LEVEL_OBJ_TRIGEER = 32;//角色事件层
        public static byte currentLevel = Consts.LEVEL_PHYSICS;//焦点图层
        //图层透明度
        public static int LEVEL_ALPHA_FLAG_PHY = 0xFF;
        public static int LEVEL_ALPHA_TILE_BG = 0xFF;
        public static int LEVEL_ALPHA_TILE_SUR = 0xFF;
        public static int LEVEL_ALPHA_TILE_OBJ = 0xFF;
        public static int LEVEL_ALPHA_OBJ_MASK = 0xFF;
        public static int LEVEL_ALPHA_OBJ_TRIGEER = 0xFF;   
        public static bool levelEye = true;//显示所有图层
        public static bool LVL_PHY_TOP = false;//顶层显示物理层
        public static bool TILE_BG_GFX_ID = false;//显示底层地形上的图形元素ID
        public static bool TILE_SUR_GFX_ID = false;//显示荣格地形上的图形元素ID
        public static bool LVL_Obj_FrameID = false;//显示地图中角色原型初始帧编号
        public static bool LVL_Obj_NPC_ID = false;//显示地图中角色原型NPC编号
        public static bool LVL_Obj_Anchor = false;//显示地图中角色原型锚点标记
        public static int TILE_ClipW = 16;//地图切片默认选取大小
        public static int TILE_ClipH = 16;
        /***********************触发器编辑界面***********************/

        //参数格式类型定义
        public const byte PARAM_INT=0;//整型常量参数
        public const byte PARAM_STR = (byte)(PARAM_INT + 1);//字符型常量参数
        public const byte PARAM_INT_VAR = (byte)(PARAM_STR + 1);//整型变量参数(参数为指向公共整型变量表中某个变量的ID)
        public const byte PARAM_STR_VAR = (byte)(PARAM_INT_VAR + 1);//字符型变量参数(参数为指向公共字符变量表中某个变量的ID)
        public const byte PARAM_INT_ID = (byte)(PARAM_STR_VAR + 1);//整型ID常量参数
        public const byte PARAM_PROP = (byte)(PARAM_INT_ID + 1);//属性型参数
        public static String getParamType(byte type)
        {
            switch (type)
            {
                case PARAM_INT:
                    return "Int";
                case PARAM_STR:
                    return "String";
                case PARAM_INT_VAR:
                    return "IntV";
                case PARAM_STR_VAR:
                    return "StringV";
                case PARAM_INT_ID:
                    return "Int_ID";
                case PARAM_PROP:
                    return "propID";
            }
            return "NULL";
        }
        /***********************地图切片编辑界面***********************/
        public static int Tile_EDI_BG_COLOR = 0x333333;
        /***********************---------------------用户软件数据---------------------***********************/

        public const int VERSION_10000 = 10000;
        public static int[] softWareVersions = new int[] 
        {
            VERSION_10000
        };//所有软件版本

        public static int softWareVersion = softWareVersions[softWareVersions.Length-1];//当前软件版本
        public static int userFileVersion = softWareVersion;      //用户文档的版本号，根据文档信息获得
    }
}
