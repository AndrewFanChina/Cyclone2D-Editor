using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Cyclone.alg.opengl;

namespace Cyclone.mod.anim
{
    class ConstTextureImgs
    {
        private static List<TextureImage> imgs = new List<TextureImage>();
        public static TextureImage createImage(Bitmap bitmap)
        {
            TextureImage image = new TextureImage(bitmap);
            imgs.Add(image);
            return image;
        }
        public static void rebindTextures()
        {
            foreach(TextureImage img in imgs)
            {
                img.reBindToGL();
            }
        }
    }
}
