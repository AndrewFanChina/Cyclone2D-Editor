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
            //�ð�ɫ�����Ļ
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            // ����ƽ����ɫ��Ĭ���ǲ���Ҫ��
            GL.ShadeModel(ShadingModel.Flat);
            // ������Ȼ���
            GL.ClearDepth(1.0f);
            // ������Ȳ���
            GL.Enable(EnableCap.DepthTest);
            //GL.Disable(EnableCap.DepthTest);
            //���������
            GL.Disable(EnableCap.Lighting);
            // ָ����ȱȽϵķ���
            GL.DepthFunc(DepthFunction.Lequal);
            // ��õ�͸�Ӽ���
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
            // �ӿڱ任
            GL.Viewport(0, 0, WorldWidth, WorldHeight);
            // ͶӰ�任
            GL.MatrixMode(MatrixMode.Projection);
            // ����ͶӰ����
            GL.LoadIdentity();
            //			// ����͸��ͶӰ����
            //			GLU.gluPerspective(gl10, 45.0f, (float) width / (float) height, 0.1f, 1000.0f);
            // ������ͶӰ
            //			GL.glOrthof(-VIEW_WIDTH/2.0f, VIEW_WIDTH/2.0f, -VIEW_HEIGHT/2.0f, VIEW_HEIGHT/2.0f, -1, 1);
            GL.Ortho(0, WorldWidth, 0, WorldHeight, -1, 1);
            // ģ�ͱ任
            GL.MatrixMode(MatrixMode.Modelview);
            // ����ģ�;���
            GL.LoadIdentity();
            // ������ɫ���
            GL.Enable(EnableCap.Blend);
            // ������ɫ���ģʽ
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }
 
    }
}
