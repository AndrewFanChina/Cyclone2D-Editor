using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;

namespace Cyclone.alg.opengl
{
    public class GLWorld
    {
        public static void InitContext()
        {
            //用白色清空屏幕
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            // 允许平滑着色，默认是不需要的
            GL.ShadeModel(ShadingModel.Flat);
            // 设置深度缓存
            GL.ClearDepth(1.0f);
            // 允许深度测试
            GL.Enable(EnableCap.DepthTest);
            //GL.Disable(EnableCap.DepthTest);
            //不允许光照
            GL.Disable(EnableCap.Lighting);
            // 指定深度比较的方法
            GL.DepthFunc(DepthFunction.Lequal);
            // 最好的透视计算
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.Enable(EnableCap.PointSmooth);
            GL.Disable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest); // Make round points, not square points   
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);  // Antialias the lines   
            GL.Enable(EnableCap.ScissorTest);
        }
        public static int WorldWidth,WorldHeight;
        public static void SetupViewport(Control control)
        {
            WorldWidth = control.Size.Width;
            WorldHeight = control.Size.Height;
            // 视口变换
            GL.Viewport(0, 0, WorldWidth, WorldHeight);
            // 投影变换
            GL.MatrixMode(MatrixMode.Projection);
            // 重置投影矩阵
            GL.LoadIdentity();
            //			// 设置透视投影矩阵
            //			GLU.gluPerspective(gl10, 45.0f, (float) width / (float) height, 0.1f, 1000.0f);
            // 设置正投影
            //			GL.glOrthof(-VIEW_WIDTH/2.0f, VIEW_WIDTH/2.0f, -VIEW_HEIGHT/2.0f, VIEW_HEIGHT/2.0f, -1, 1);
            GL.Ortho(0, WorldWidth, 0, WorldHeight, -1, 1);
            // 模型变换
            GL.MatrixMode(MatrixMode.Modelview);
            // 重置模型矩阵
            GL.LoadIdentity();
            // 允许颜色混合
            GL.Enable(EnableCap.Blend);
            // 设置颜色混合模式
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }
 
    }
}
