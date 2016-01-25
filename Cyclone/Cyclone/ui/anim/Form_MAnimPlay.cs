using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Cyclone.alg;
using Cyclone.mod;
using System.Drawing.Drawing2D;
using Cyclone.mod.anim;
using DockingUI.WinFormsUI.Docking;
using OpenTK.Graphics.OpenGL;
using Cyclone.alg.opengl;
using Cyclone.alg.util;
using Cyclone.alg.math;

namespace Cyclone.mod.anim
{
    public partial class Form_MAnimPlay : DockContent
    {
        Form_MAnimation form_MA;
        public Form_MAnimPlay(Form_MAnimation form_MAT)
        {
            form_MA = form_MAT;
            InitializeComponent();
            comboBox_PlayMode.SelectedIndex = 0;
            glView.MouseWheel += new MouseEventHandler(pictureBox_Preview_MouseWheel);
        }
        protected override string GetPersistString()
        {
            return "Form_MAnimPW";
        }
        //动画御览部分======================================================================================
        //private Image imgPreviewBoxBuf = null;//预览框区域缓冲
        private float previewZoomLevel = 1;//缩放比率
        private const float previewZoomLevelMin = 0.25f;//缩放最大级别
        private const float previewZoomLevelMax = 32;//缩放最大级别
        private int play_frameID = 0;   //播放帧编号
        private int delayLevel = 26;//帧播放间隔倍率
        private bool bePaused = false;//暂停
        //缩放
        public void previewZoom(float level)
        {
            previewZoomLevel *= level;
            if (previewZoomLevel > previewZoomLevelMax)
            {
                previewZoomLevel = previewZoomLevelMax;
            }
            if (previewZoomLevel < previewZoomLevelMin)
            {
                previewZoomLevel = previewZoomLevelMin;
            }
        }
        //显示动画
        public void Update_GlView()
        {
            Update_GlView(false);
        }
        private Matrix4 matrixDraw = new Matrix4();
        public void Update_GlView(bool desire)
        {
            if (this.IsDisposed || !loaded || this.IsHidden || this.Width <= 0 || this.Height <= 0)
            {
                return;
            }
            if (Form_MAnimation.inResetPanels)
            {
                return;
            }
            if (!desire)
            {
                if (!(comboBox_PlayMode.SelectedIndex <= 0||(comboBox_PlayMode.SelectedIndex == 1 && mouseInView)))
                {
                    return;
                }
            }
            if (!glView.MakeCurrent())
            {
                return;
            }
            //清除颜色和深度为缓冲
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //重置模型视图矩阵
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            //获取屏幕尺寸
            int glViewW = glView.Width;
            int glViewH = glView.Height;
            GLGraphics.resetClip(glViewW, glViewH);
            //用白色清空屏幕
            GL.ClearColor(GraphicsUtil.getColor(Consts.colorDarkGray));
            //映射坐标系
            matrixDraw.identity();
            matrixDraw.preScale(1.0f, -1.0f, 1.0f);
            matrixDraw.postTranslate(0, glViewH, 0.0f);
            float[] data = matrixDraw.getValue();
            GL.MultMatrix(data);
            //绘制内容
            MTimeLineHoder currentTimeLineHoder = form_MA.form_MTimeLine.currentTimeLineHoder;
            if (currentTimeLineHoder != null)
            {
                List<MFrame> frameEditList = form_MA.form_MTimeLine.getSameTimeFrames_Visible(play_frameID);
                foreach (MFrame frameEdit in frameEditList)
                {
                    if (frameEdit != null)
                    {
                        frameEdit.glDisplay(glViewW / 2, glViewH / 2, previewZoomLevel, null, false, play_frameID);
                    }
                }
                //步进
                play_frameID++;
                if (play_frameID >= currentTimeLineHoder.getMaxFrameLen())
                {
                    play_frameID = 0;
                }
            }
            else
            {
                play_frameID = 0;
            }
            glView.SwapBuffers();
        }
        private int playIndexOld = 0;
        private int nbFramesOld = 0;
        public void updateFrameIndex(int playIndexT, int nbFrames)
        {
            if (playIndexOld != playIndexT || nbFramesOld != nbFrames)
            {
                playIndexOld = playIndexT;
                nbFramesOld = nbFrames;
            }
        }
        //启动循环播放线程
        public void startPlayPrieviewBox()
        {
            timer_play.Enabled = true;
        }
        //停止播放线程
        public void stopPlayPrieviewBox()
        {
            timer_play.Enabled = false;
        }
        //(预览框)滚轮事件
        private void pictureBox_Preview_MouseWheel(object sender, MouseEventArgs e)
        {
            int value = e.Delta / 120;
            previewZoom((float)Math.Pow(2, value));
        }

        private void Form_DisplayAnimation_FormClosed(object sender, FormClosedEventArgs e)
        {
            stopPlayPrieviewBox();
        }

        private void trackBar_PreviewBox_ValueChanged(object sender, EventArgs e)
        {
            int fps = (int)trackBar_PreviewBox.Value;
            delayLevel = 1000 / fps;
            timer_play.Interval = delayLevel;
            label_fps.Text = "FPS:" + fps;
        }

        private void button_ZoomOut_Click(object sender, EventArgs e)
        {
            previewZoom(0.5f);
        }

        private void button_ZoomIn_Click(object sender, EventArgs e)
        {
            previewZoom(2.0f);
        }
        private bool mouseInView = false;
        private void glView_MouseEnter(object sender, EventArgs e)
        {
            if (!this.form_MA.Equals(Form.ActiveForm))
            {
                return;
            }
            mouseInView = true;
            glView.Focus();
        }
        private void glView_MouseLeave(object sender, EventArgs e)
        {
            mouseInView = false;
        }
        private void button_Play_PreviewBox_Click(object sender, EventArgs e)
        {
            if (bePaused)
            {
                bePaused = false;
                button_Play.BackgroundImage = global::Cyclone.Properties.Resources.pause;
                this.startPlayPrieviewBox();
            }
            else
            {
                bePaused = true;
                button_Play.BackgroundImage = global::Cyclone.Properties.Resources.play;
                this.stopPlayPrieviewBox();
            }
            panel_PreviewBoxTools.Refresh();

        }

        private void button_Pause_PreviewBox_Click(object sender, EventArgs e)
        {
            bePaused = true;
            this.stopPlayPrieviewBox();
            panel_PreviewBoxTools.Refresh();
        }

        private void button_loop_Click(object sender, EventArgs e)
        {
            bePaused = false;
            panel_PreviewBoxTools.Refresh();
        }

        private void Form_MAnimPW_Shown(object sender, EventArgs e)
        {
            startPlayPrieviewBox();
        }
        public bool loaded = false;
        private void Form_GLViewParent_Load(object sender, EventArgs e)
        {
            glView.MakeCurrent();
            GLWorld.InitContext();
            GLWorld.SetupViewport(glView);
            this.loaded = true;
        }
        private void glView_Resize(object sender, EventArgs e)
        {
            if (loaded)
            {
                glView.MakeCurrent();
                GLWorld.SetupViewport(glView);
                Update_GlView();
            }
        }
        private void Form_GLViewParent_RegionChanged(object sender, EventArgs e)
        {
            if (glContextRereated >= 0)
            {
                glContextRereated = -1;
                Console.WriteLine("Form_MFrameEdit_RegionChanged");
                if (!Form_MAnimation.inResetPanels)
                {
                    form_MA.form_MImgsList.mImgsManager.rebindTextures();
                    ConstTextureImgs.rebindTextures();
                }
                Update_GlView();
            }
        }
        private int glContextRereated = -1;
        private void Form_GLViewParent_ParentChanged(object Sender, EventArgs e)
        {
            if (loaded && Parent != null)
            {
                glContextRereated = 0;
            }
            Console.WriteLine("Form_MFrameEdit_ParentChanged:" + this.Parent);
        }

        private void glView_Paint(object sender, PaintEventArgs e)
        {
            this.Update_GlView();
        }

        private void timer_play_Tick(object sender, EventArgs e)
        {
            Update_GlView();
        }


    }
}