using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Cyclone.mod;
using OpenTK.Graphics.OpenGL;
using Cyclone.alg.math;

namespace Cyclone.alg.opengl
{
    class GLGraphics
    {
        private static float[,] rectVertices = new float[4, 3];//�����ζ��㻺��
        /**
         * ���ƾ��α߿�
         * @param x �������Ͻ�X����
         * @param y �������Ͻ�Y����
         * @param w ���ο��
         * @param h ���θ߶�
         */
        public static void drawRect(float x, float y, float w, float h, bool allowSmooth)
        {
            drawRect(x, y, 0, w, h, allowSmooth);
        }
        public static void drawRect(float x, float y, float z, float w, float h, bool allowSmooth)
        {
            drawRectBase(BeginMode.LineLoop, x, y, z, w, h, allowSmooth);
        }
        /**
         * ���ƾ������
         * @param x �������Ͻ�X����
         * @param y �������Ͻ�Y����
         * @param w ���ο��
         * @param h ���θ߶�
         */
        public static void fillRect(float x, float y, float w, float h, bool allowSmooth)
        {
            fillRect(x, y, 0, w, h, allowSmooth);
        }
        public static void fillRect(float x, float y, float z, float w, float h, bool allowSmooth)
        {
            drawRectBase(BeginMode.TriangleFan, x, y, z, w, h, allowSmooth);
        }
        public static void fillRect(float x, float y, float w, float h)
        {
            fillRect(x, y, 0, w, h);
        }
        public static void fillRect(float x, float y, float z, float w, float h)
        {
            drawRectBase(BeginMode.TriangleFan, x, y, z, w, h, false);
        }
        private static void drawRectBase(BeginMode mode, float x, float y, float z, float w, float h, bool allowSmooth)
        {
            if (allowSmooth)
            {
                GL.Enable(EnableCap.LineSmooth);
            }
            else
            {
                GL.Disable(EnableCap.LineSmooth);
            }
            rectVertices[0, 0] = x;
            rectVertices[0, 1] = y;
            rectVertices[0, 2] = z;
            rectVertices[1, 0] = x + w;
            rectVertices[1, 1] = y;
            rectVertices[1, 2] = z;
            rectVertices[2, 0] = x + w;
            rectVertices[2, 1] = y + h;
            rectVertices[2, 2] = z;
            rectVertices[3, 0] = x;
            rectVertices[3, 1] = y + h;
            rectVertices[3, 2] = z;
            GL.PushMatrix();
            //��ʼ����
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, rectVertices);
            GL.DrawArrays(mode, 0, 4);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.PopMatrix();
            if (!allowSmooth)
            {
                GL.Enable(EnableCap.LineSmooth);
            }
            else
            {
                GL.Disable(EnableCap.LineSmooth);
            }
        }
        /**
         * ���õ�ǰ��ɫ(���а�͸��)
         * @param color AARRGGBB��ʽ����ɫ��ֵ
         */
        public static void setColor(uint color)
        {
            float r = ((color >> 16) & 0xFF) / 255.0f;
            float g = ((color >> 8) & 0xFF) / 255.0f;
            float b = ((color >> 0) & 0xFF) / 255.0f;
            float a = ((color >> 24) & 0xFF) / 255.0f;
            GL.Color4(r, g, b, a);
        }
        /**
         * ���õ�ǰ��ɫ(��ȫ��͸��)
         * @param color RRGGBB��ʽ����ɫ��ֵ
         */
        public static void setRGBColor(int color)
        {
            float r = ((color >> 16) & 0xFF) / 255.0f;
            float g = ((color >> 8) & 0xFF) / 255.0f;
            float b = ((color >> 0) & 0xFF) / 255.0f;
            float a = 1.0f;
            GL.Color4(r, g, b, a);
        }
        /**
         * ���õ�ǰ��ɫ(����͸��)
         * @param color RRGGBB��ʽ����ɫ��ֵ
         * @param alpha AA��ʽ�İ�͸����ֵ
         */
        public static void setARGBColor(int color,int alpha)
        {
            float r = ((color >> 16) & 0xFF) / 255.0f;
            float g = ((color >> 8) & 0xFF) / 255.0f;
            float b = ((color >> 0) & 0xFF) / 255.0f;
            float a = ((alpha >> 0) & 0xFF) / 255.0f;
            GL.Color4(r, g, b, a);
        }
        /**
         * ʹ��ָ���ı任���������ͼ�п飬��ʹ��ָ������ɫ��Ϸ�ʽ
         * RectangleF srcRect Դͼ������
         * RectangleF destRect Ŀ���������
         * Matrix4 matrix �ڴ�Դͼ���������Ƶ�Ŀ������֮�����õķ���任
         */
        private static Matrix4 matrixDraw = new Matrix4();
        public static void drawTextureImage(TextureImage textrueImage, RectangleF srcRect, RectangleF destRect, Matrix4 matrix,float alpha)
	    {
            alpha = MathUtil.limitNumber(alpha, 0, 1);
            //GLGraphics.setColor((uint)(0xFFFFFF | (((int)(alpha*255))<<24)));
            GL.Color4(1.0f, 1.0f, 1.0f, alpha);
            GL.PushMatrix();
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.LineSmooth);

            //Ŀ��ߴ�
            float _dw = destRect.Width;
            float _dh = destRect.Height;

            //��ͼ����
            float txW = textrueImage.TextureWidth;
            float txH = textrueImage.TextureHight;
            float ltX = srcRect.X / txW;
            float ltY = srcRect.Y / txH;
            float rbX = (srcRect.X + srcRect.Width) / txW;
            float rbY = (srcRect.Y + srcRect.Height) / txH;

            GL.BindTexture(TextureTarget.Texture2D, textrueImage._name);
            matrixDraw.identity();
            matrixDraw.preTranslate(destRect.X, destRect.Y);
            if (matrix != null)
            {
                matrix.multiply(matrixDraw, matrixDraw);
            }
            float[] data = matrixDraw.getValue();
            GL.MultMatrix(data);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(ltX, ltY);
            GL.Vertex3(0, 0, 0);
            GL.TexCoord2(rbX, ltY);
            GL.Vertex3(_dw, 0, 0);
            GL.TexCoord2(rbX, rbY);
            GL.Vertex3(_dw, _dh, 0);
            GL.TexCoord2(ltX, rbY);
            GL.Vertex3(0, _dh, 0);
            GL.End();


            GL.Disable(EnableCap.Texture2D);
		    GL.PopMatrix();
	    }
        /**
         * ���׵Ļ�ͼ��ʽ
         */
        public static void drawTextureImage(TextureImage textrueImage, RectangleF srcRect,float destX,float destY)
        {
            drawTextureImage(textrueImage, srcRect, new RectangleF(destX, destY, srcRect.Width, srcRect.Height), null, 1.0f);
        }

	    /**
	     * ʹ��ָ����ɫ�����Ļ
	     * 
	     * @param g
	     * @param color AARRGGBB��ʽ����ɫ��ֵ
	     */
	    public static void clearScreen(uint color)
	    {
		    float r = ((color >> 16) & 0xFF) / 255.0f;
		    float g = ((color >> 8) & 0xFF) / 255.0f;
		    float b = ((color >> 0) & 0xFF) / 255.0f;
		    float a = ((color >> 24) & 0xFF) / 255.0f;
		    GL.ClearColor(r, g, b, a);
	    }
        /**
         * �����߶�
         * @param x1 ��1������X����
         * @param y1 ��1������Y����
         * @param x2 ��2������X����
         * @param y2 ��2������Y����
         * !?��glDrawArray����������Ƶĵ㻮�߻�䶯�γ�����֪����������
         */
        public static void drawLine(float x1, float y1, float x2, float y2,bool allowSmooth)
        {
            if (allowSmooth)
            {
                GL.Enable(EnableCap.LineSmooth);
            }
            else
            {
                GL.Disable(EnableCap.LineSmooth);
            }
            GL.Begin(BeginMode.Lines);
            GL.Vertex2((x1), (y1));
            GL.Vertex2((x2), (y2));
            GL.End();
            if (!allowSmooth)
            {
                GL.Enable(EnableCap.LineSmooth);
            }
            else
            {
                GL.Disable(EnableCap.LineSmooth);
            }
        }
        public static void drawLine(PointF pA, PointF pB, bool allowSmooth)
        {
            drawLine(pA.X, pA.Y, pB.X, pB.Y, allowSmooth);
        }
        /**
         * ���Ƶ㻮���߶�
         * @param x1 ��1������X����
         * @param y1 ��1������Y����
         * @param x2 ��2������X����
         * @param y2 ��2������Y����
         */
        public static void drawDashLine(int x1, int y1, int x2, int y2, bool allowSmooth)
        {
            GL.Enable(EnableCap.LineStipple);
            GL.LineStipple(1, 0xF0F0);
            drawLine(x1, y1, x2, y2, allowSmooth);
            GL.Disable(EnableCap.LineStipple);
        }
        /**
         * ���ü������� 
         */
        private static Rectangle clipAngle = new Rectangle();
        public static void setClip(int x, int y, int width, int height)
        {
            clipAngle.X = x;
            clipAngle.Y = y;
            clipAngle.Width = width;
            clipAngle.Height = height;
            GL.Scissor(clipAngle.X, clipAngle.Y, clipAngle.Width, clipAngle.Height);
        }
        public static void setClip(RectangleF limitRect)
        {
            setClip((int)limitRect.X, (int)limitRect.Y, (int)limitRect.Width, (int)limitRect.Height);
        }
        public static void setClip(Rectangle limitRect)
        {
            setClip(limitRect.X, limitRect.Y, limitRect.Width, limitRect.Height);
        }
        public static void resetClip(int WorldWidth, int WorldHeight)
        {
            clipAngle.X = 0;
            clipAngle.Y = 0;
            clipAngle.Width = WorldWidth;
            clipAngle.Height = WorldHeight;
            GL.Scissor(clipAngle.X, clipAngle.Y, clipAngle.Width, clipAngle.Height);
        }
        private static Stack<Rectangle> clipStack = new Stack<Rectangle>();
        public static void pushClip()
        {
            clipStack.Push(new Rectangle(clipAngle.X, clipAngle.Y, clipAngle.Width, clipAngle.Height));
        }
        public static void pushClipAndReset(int width,int height)
        {
            clipStack.Push(new Rectangle(clipAngle.X, clipAngle.Y, clipAngle.Width, clipAngle.Height));
            resetClip(width,height);
        }
        public static void popClip()
        {
            Rectangle poped = clipStack.Pop();
            setClip(poped.X, poped.Y, poped.Width, poped.Height);
        }
        public static void PushMatrix(float[] mtrixGLValue)
        {
            OpenTK.Graphics.OpenGL.GL.PushMatrix();
            OpenTK.Graphics.OpenGL.GL.MultMatrix(mtrixGLValue);
        }
        public static void PushMatrix()
        {
            OpenTK.Graphics.OpenGL.GL.PushMatrix();
        }
        public static void PopMatrix()
        {
            OpenTK.Graphics.OpenGL.GL.PopMatrix();
        }
    }
}
