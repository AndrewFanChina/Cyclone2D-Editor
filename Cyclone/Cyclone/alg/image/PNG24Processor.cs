using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Cyclone.alg;

namespace Cyclone.alg.image
{
    class PNG24Processor
    {
        public static Image[] getPng_8(Image image)
        {
            Image[] imgs = new Image[2];
            int Width = image.Width;
            int Height = image.Height;
            //拷贝到32位位图
            Bitmap BmpCopy = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            {
                Graphics g = Graphics.FromImage(BmpCopy);
                g.PageUnit = GraphicsUnit.Pixel;
                g.DrawImage(image, 0, 0, Width, Height);
                g.Dispose();
            }
            //获取调色板
            Bitmap imgBase = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            Bitmap imgAlpha = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            for (uint row = 0; row < Height; row++)
            {
                for (uint col = 0; col < Width; col++)
                {
                    Color pixel = BmpCopy.GetPixel((int)col, (int)row);
                    if (pixel.A == 0)
                    {
                        imgBase.SetPixel((int)col, (int)row, Color.FromArgb(0,0, 0,0));
                        imgAlpha.SetPixel((int)col, (int)row, Color.FromArgb(0, 0, 0));
                    }
                    else
                    {
                        imgBase.SetPixel((int)col, (int)row, Color.FromArgb(pixel.R, pixel.G, pixel.B));
                        imgAlpha.SetPixel((int)col, (int)row, Color.FromArgb(pixel.A, pixel.A, pixel.A));
                    }
                    

                } /* end loop for col */
            } /* end loop for row */
            imgs[0] = imgBase;
            imgs[1] = imgAlpha;
            return imgs;
        }
    }
}
