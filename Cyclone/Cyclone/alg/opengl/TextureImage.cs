using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using Cyclone.alg;
using Cyclone.alg.util;
using Cyclone.alg.math;

namespace Cyclone.alg.opengl
{
    public class TextureImage
    {
        public uint _name = 0;
        private Size mOrgSize; // ԭ��ʵ����Ч���صĳߴ�
        private Size mTexSize; // ��ͼ�������ͼ�ߴ磬�����˸��ӿհ�����
        private byte[] pixles;
        /**
         * ����GDIλͼ������ͼͼƬ
         * @param image GDIλͼ
         * @param retainBitmap
         */
        public TextureImage(Bitmap image)
        {
            rebindBitmap(image);
        }
        /**
         *��ͼ���
         */
        public int TextureWidth
        {
            get
            {
                return mTexSize.Width;
            }
        }

        /**
         * λͼ�߶�
         */
        public int TextureHight
        {
            get
            {
                return mTexSize.Height;
            }
        }
        /**
         *ԭ����Ч���ؿ��
         */
        public int OrgWidth
        {
            get 
            {
                return mOrgSize.Width;
            }
        }
        /**
         *ʵ����Ч���ظ߶�
         */
        public int OrgHeight
        {
            get { return mOrgSize.Height; }
        }
        /**
         *��ͼ����
         */
        public uint name()
        {
            return _name;
        }

        /**
         * ���°���ͼ����ʹ�ñ���λͼ��ʽ����TextureImageʱ������ʹ��
         */
        public void rebindBitmap(Bitmap bitmap)
        {
            mOrgSize = new Size(bitmap.Width, bitmap.Height);
            Bitmap bitmapNew = GraphicsUtil.getMatchImage(bitmap);
            mTexSize = bitmapNew.Size;
            pixles = getPixels(bitmapNew);
            reBindToGL();
            bitmapNew.Dispose();
            bitmapNew = null;
        }
        //�ذ󶨵�GL
        public  void reBindToGL()
        {
            if (pixles == null)
            {
                return;
            }

            GL.Enable(EnableCap.Texture2D);
            if (_name != 0)
            {
                GL.DeleteTextures(1, new uint[] { _name });
                _name = 0;
            }
            if (_name == 0)
            {
                uint[] textures = new uint[1];
                GL.GenTextures(1, textures);
                _name = textures[0];
            }

            GL.BindTexture(TextureTarget.Texture2D, _name);
            if (Consts.textureLinear)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)All.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)All.Linear);
            }
            else
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)All.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)All.Nearest);
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)All.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)All.Repeat);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, mTexSize.Width, mTexSize.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, pixles);

            GL.Disable(EnableCap.Texture2D);
        }
        //��λͼ�����������
        public static byte[] getPixels(Bitmap imgSrc)
        {
            if (imgSrc == null)
            {
                return null;
            }
            BitmapData srcBitmapData = imgSrc.LockBits(new Rectangle(0, 0, imgSrc.Width, imgSrc.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            int pixelCount = imgSrc.Width * imgSrc.Height;
            byte[] pixels = new byte[pixelCount * 4];
            unsafe
            {
                uint* srcData = (uint*)(srcBitmapData.Scan0).ToPointer();
                int bID;
                uint uIData;
                for (int i = 0; i < pixelCount; i++)
                {
                    bID=i<<2;
                    uIData = srcData[i];
                    pixels[bID + 3] = (byte)((uIData >> 24) & 0xFF);
                    pixels[bID + 0] = (byte)((uIData >> 16) & 0xFF);
                    pixels[bID + 1] = (byte)((uIData >> 8) & 0xFF);
                    pixels[bID + 2] = (byte)((uIData >> 0) & 0xFF);
                }
                imgSrc.UnlockBits(srcBitmapData);
            }
            return pixels;
        }
        /**
         * �ͷ���Դ
         */
        public void releaseRes()
        {
            if (_name != 0)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.DeleteTextures(1, new uint[] { _name });
                GL.Disable(EnableCap.Texture2D);
                _name = 0;
            }
            pixles = null;
        }

    }
}
