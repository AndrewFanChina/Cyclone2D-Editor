using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Cyclone.alg.win32;
namespace Cyclone.alg.image.HSL
{

    public abstract class FilterColorToColor 
        : IFilter, IInPlaceFilter
    {
        protected FilterColorToColor()
        {
        }

        public virtual Bitmap Apply(Bitmap image)
        {
            Bitmap bitmap;
            BitmapData imageData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                image.PixelFormat);
            try
            {
                bitmap = Apply(imageData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                image.UnlockBits(imageData);
            }
            return bitmap;
        }

        public virtual Bitmap Apply(BitmapData imageData)
        {
            if (imageData.PixelFormat != PixelFormat.Format24bppRgb &&
                imageData.PixelFormat != PixelFormat.Format32bppArgb &&
                imageData.PixelFormat != PixelFormat.Format32bppRgb)
            {
                throw new ArgumentException();
            }

            int width = imageData.Width;
            int height = imageData.Height;

            Bitmap bitmap = new Bitmap(
                width, height, PixelFormat.Format24bppRgb);
            BitmapData bitmapdata = bitmap.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            NativeMethods.memcpy(
                bitmapdata.Scan0,
                imageData.Scan0,
                imageData.Stride * height);
            ProcessFilter(bitmapdata);
            bitmap.UnlockBits(bitmapdata);
            return bitmap;
        }

        public virtual void ApplyInPlace(Bitmap image)
        {
            BitmapData imageData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadWrite,
                image.PixelFormat);
            try
            {
                ApplyInPlace(imageData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                image.UnlockBits(imageData);
            }
        }
        public virtual void ApplyInPlace(BitmapData imageData)
        {
            if (imageData.PixelFormat != PixelFormat.Format24bppRgb &&
               imageData.PixelFormat != PixelFormat.Format32bppArgb &&
               imageData.PixelFormat != PixelFormat.Format32bppRgb)
            {
                throw new ArgumentException();
            }
            ProcessFilter(imageData);
        }
        public int ApplyColor(int Color)
        {
            return ProcessColor(Color);
        }
        protected abstract void ProcessFilter(BitmapData imageData);

        protected abstract int ProcessColor(int Color);


        protected int GetPerPixelFormatLength(PixelFormat pixelFormat)
        {
            int length = 0;
            switch (pixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    length = 3;
                    break;
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Format32bppArgb:
                    length = 4;
                    break;
            }

            return length;
        }

        protected bool UsedAlpha(PixelFormat pixelFormat)
        {
            return pixelFormat == PixelFormat.Format32bppArgb;
        }
    }
}
