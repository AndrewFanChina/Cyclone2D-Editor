using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Windows.Forms;
using Cyclone.mod;


namespace Cyclone.alg.image
{   
    public class IndexImageProcessor   
    {
        private static ColorPalette GetColorPalette(uint nColors)
        {
            // Assume monochrome image.
            PixelFormat bitscolordepth = PixelFormat.Format1bppIndexed;
            ColorPalette palette;    // The Palette we are stealing
            Bitmap bitmap;     // The source of the stolen palette

            // Determine number of colors.
            if (nColors > 2)
                bitscolordepth = PixelFormat.Format4bppIndexed;
            if (nColors > 16)
                bitscolordepth = PixelFormat.Format8bppIndexed;

            // Make a new Bitmap object to get its Palette.
            bitmap = new Bitmap(1, 1, bitscolordepth);

            palette = bitmap.Palette;   // Grab the palette

            bitmap.Dispose();           // cleanup the source Bitmap

            return palette;             // Send the palette back
        }
        public static Image getIndexColorImage(Image image,String fileName)
        {
            uint nColors = 0;
            // Make a new 8-BPP indexed bitmap that is the same size as the source image.
            int Width = image.Width;
            int Height = image.Height;

            // Always use PixelFormat8bppIndexed because that is the color
            // table-based interface to the GIF codec.
            Bitmap bitmap = new Bitmap(Width, Height,PixelFormat.Format8bppIndexed);

            // Use GetPixel below to pull out the color data of Image.
            // Because GetPixel isn't defined on an Image, make a copy 
            // in a Bitmap instead. Make a new Bitmap that is the same size as the
            // image that you want to export. Or, try to
            // interpret the native pixel format of the image by using a LockBits
            // call. Use PixelFormat32BppARGB so you can wrap a Graphics  
            // around it.
            Bitmap BmpCopy = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            {
                Graphics g = Graphics.FromImage(BmpCopy);

                g.PageUnit = GraphicsUnit.Pixel;

                // Transfer the Image to the Bitmap
                g.DrawImage(image, 0, 0, Width, Height);

                // g goes out of scope and is marked for garbage collection.
                // Force it, just to keep things clean.
                g.Dispose();
            }

            Hashtable hushTable = new Hashtable();
            //获取调色板
            for (uint row = 0; row < Height; ++row)
            {
                for (uint col = 0; col < Width; ++col)
                {
                    Color pixel = BmpCopy.GetPixel((int)col, (int)row);
                    if (pixel.A == 0)
                    {
                        pixel = Color.Empty;
                    }
                    if (!hushTable.Contains(pixel))
                    {
                        hushTable.Add(pixel, hushTable.Count);
                    }
                } /* end loop for col */
            } /* end loop for row */
            //foreach(DictionaryEntry de in hushTable) //ht为一个Hashtable实例 
            //{ 
            //    Console.WriteLine(de.Key);//de.Key对应于key/value键值对key 
            //    Console.WriteLine(de.Value);//de.Key对应于key/value键值对value 
            //}
            nColors = (uint)hushTable.Count;
            // GIF codec supports 256 colors maximum, monochrome minimum.
            if (nColors > 256)
            {
                Console.WriteLine("图片颜色超过256【" + (fileName == null ? "" : fileName)+"】");
                return null;
            }
            //创建调色板
            ColorPalette pal = GetColorPalette(nColors);
            foreach (DictionaryEntry de in hushTable) //ht为一个Hashtable实例 
            {
                pal.Entries[(int)(de.Value)] = (Color)de.Key;
            }

            // Set the palette into the new Bitmap object.
            bitmap.Palette = pal;


            // Lock a rectangular portion of the bitmap for writing.
            BitmapData bitmapData;
            Rectangle rect = new Rectangle(0, 0, Width, Height);

            bitmapData = bitmap.LockBits(rect,ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Write to the temporary buffer that is provided by LockBits.
            // Copy the pixels from the source image in this loop.
            // Because you want an index, convert RGB to the appropriate
            // palette index here.
            IntPtr pixels = bitmapData.Scan0;

            unsafe
            {
                // Get the pointer to the image bits.
                // This is the unsafe operation.
                byte* pBits;
                if (bitmapData.Stride > 0)
                {
                    pBits = (byte*)pixels.ToPointer();
                }
                else
                {
                    pBits = (byte*)pixels.ToPointer() + bitmapData.Stride * (Height - 1);
                }
                uint stride = (uint)Math.Abs(bitmapData.Stride);

                for (uint row = 0; row < Height; ++row)
                {
                    for (uint col = 0; col < Width; ++col)
                    {

                        Color pixel;    // The source pixel.

                        byte* p8bppPixel = pBits + row * stride + col;

                        pixel = BmpCopy.GetPixel((int)col, (int)row);

                        if (pixel.A == 0)
                        {
                            pixel = Color.Empty;
                        }

                        *p8bppPixel = (byte)((int)hushTable[pixel]);

                    } /* end loop for col */
                } /* end loop for row */
            } /* end unsafe */

            // To commit the changes, unlock the portion of the bitmap.  
            bitmap.UnlockBits(bitmapData);
            //修改调色板冗余
            if (nColors < bitmap.Palette.Entries.Length)
            {
                //....go on
            }

            // Bitmap goes out of scope here and is also marked for
            // garbage collection.
            // Pal is referenced by bitmap and goes away.
            // BmpCopy goes out of scope here and is marked for garbage
            // collection. Force it, because it is probably quite large.
            // The same applies to bitmap.
            BmpCopy.Dispose();
            hushTable.Clear();
            return bitmap;
        }

    }   
}   
  
