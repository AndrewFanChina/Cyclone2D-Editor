using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;

namespace Cyclone.alg.image
{
   public class ThumbMaker   
    {   
        Double xFactor;   
        Double yFactor;   
        System.IntPtr sourceScan0;   
        int sourceStride;   
        Bitmap scaledBitmap, bitmap;   
  
        public ThumbMaker(string fileName)   
        {   
            bitmap = new Bitmap(fileName);   
        }   
  
        public ThumbMaker(Stream stream)   
        {   
            bitmap = new Bitmap(stream);   
        }

        public ThumbMaker()
        {

        }
       public void Dispose()
       {
           if (scaledBitmap != null)
           {
               scaledBitmap.Dispose();
               scaledBitmap = null;
           }
           if (bitmap != null)
           {
               bitmap.Dispose();
               bitmap = null;
           }
       }
       public void setBitmap(Bitmap bitmapT)
       {
           if (bitmap != null)
           {
               bitmap.Dispose();
               bitmap = null;
           }
           bitmap = new Bitmap(bitmapT);
       }   
        void AdjustSizes(ref int xSize, ref int ySize)   
        {   
            if (xSize != 0 && ySize == 0)   
                ySize = Math.Abs((int)(xSize * bitmap.Height / bitmap.Width));   
            else if (xSize == 0 && ySize != 0)   
                xSize = Math.Abs((int)(ySize * bitmap.Width / bitmap.Height));   
            else if (xSize == 0 && ySize == 0)   
            {   
                xSize = bitmap.Width;   
                ySize = bitmap.Height;   
            }   
        }   
  
        void IndexedRezise(int xSize, int ySize)   
        {   
            BitmapData sourceData;   
            BitmapData targetData;   
  
            //AdjustSizes(ref xSize, ref ySize);   
  
            scaledBitmap = new Bitmap(xSize, ySize, bitmap.PixelFormat);   
            scaledBitmap.Palette = bitmap.Palette;   
            sourceData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),   
                ImageLockMode.ReadOnly, bitmap.PixelFormat);   
            try  
            {   
                targetData = scaledBitmap.LockBits(new Rectangle(0, 0, xSize, ySize),   
                    ImageLockMode.WriteOnly, scaledBitmap.PixelFormat);   
                try  
                {   
                    xFactor = (Double)bitmap.Width / (Double)scaledBitmap.Width;   
                    yFactor = (Double)bitmap.Height / (Double)scaledBitmap.Height;   
                    sourceStride = sourceData.Stride;   
                    sourceScan0 = sourceData.Scan0;   
                    int targetStride = targetData.Stride;   
                    System.IntPtr targetScan0 = targetData.Scan0;   
                    unsafe  
                    {   
                        byte* p = (byte*)(void*)targetScan0;   
                        int nOffset = targetStride - scaledBitmap.Width;   
                        int nWidth = scaledBitmap.Width;   
                        for (int y = 0; y < scaledBitmap.Height; ++y)   
                        {   
                            for (int x = 0; x < nWidth; ++x)   
                            {   
                                p[0] = GetSourceByteAt(x, y);   
                                ++p;   
                            }   
                            p += nOffset;   
                        }   
                    }   
                }   
                finally  
                {   
                    scaledBitmap.UnlockBits(targetData);   
                }   
            }   
            finally  
            {   
                bitmap.UnlockBits(sourceData);   
            }   
        }   
  
        byte GetSourceByteAt(int x, int y)   
        {   
            unsafe  
            {   
                return ((byte*)((int)sourceScan0 + (int)(Math.Floor(y * yFactor) * sourceStride) +   
                    (int)Math.Floor(x * xFactor)))[0];   
            }   
        }   
  
        void RGBRezise(int xSize, int ySize)   
        {   
            //AdjustSizes(ref xSize, ref ySize);   
            scaledBitmap = new Bitmap(xSize, ySize, PixelFormat.Format24bppRgb);   
            Graphics g = Graphics.FromImage(scaledBitmap);   
            Rectangle destRect = new Rectangle(0, 0, xSize, ySize);   
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;   
            g.SmoothingMode = SmoothingMode.AntiAlias;   
            g.DrawImage(bitmap, destRect, new Rectangle(100, 100, bitmap.Width - 100, bitmap.Height - 100), GraphicsUnit.Pixel);   
        }   
  
        void Save(string fileName, ImageFormat format)   
        {   
            scaledBitmap.Save(fileName, format);   
        }   
  
        void Save(string fileName, long jQuality, ImageFormat format)   
        {   
            ImageCodecInfo jpegCodecInfo = GetEncoderInfo("image/jpeg");   
            Encoder qualityEncoder = Encoder.Quality;   
            EncoderParameters encoderParams = new EncoderParameters(1);   
            EncoderParameter qualityEncoderParam = new EncoderParameter(qualityEncoder, jQuality);   
            encoderParams.Param[0] = qualityEncoderParam;   
            scaledBitmap.Save(fileName, jpegCodecInfo, encoderParams);   
        }   
  
        void Save(Stream stream, ImageFormat format)   
        {   
            scaledBitmap.Save(stream, format);   
        }   
  
        void Save(Stream stream, long jQuality, ImageFormat format)   
        {   
            ImageCodecInfo jpegCodecInfo = GetEncoderInfo("image/jpeg");   
            Encoder qualityEncoder = Encoder.Quality;   
            EncoderParameters encoderParams = new EncoderParameters(1);   
            EncoderParameter qualityEncoderParam = new EncoderParameter(qualityEncoder, jQuality);   
            encoderParams.Param[0] = qualityEncoderParam;   
            scaledBitmap.Save(stream, jpegCodecInfo, encoderParams);   
        }   
  
        ImageCodecInfo GetEncoderInfo(String mimeType)   
        {   
            int j;   
            ImageCodecInfo[] encoders;   
            encoders = ImageCodecInfo.GetImageEncoders();   
            for (j = 0; j < encoders.Length; ++j)   
            {   
                if (encoders[j].MimeType.ToUpper() == mimeType.ToUpper())   
                    return encoders[j];   
            }   
            return null;   
        }   
  
        public void ResizeToJpeg(int xSize, int ySize, string fileName)   
        {   
            this.RGBRezise(xSize, ySize);   
            this.Save(fileName, ImageFormat.Jpeg);   
        }   
  
        public void ResizeToJpeg(int xSize, int ySize, Stream stream)   
        {   
            this.RGBRezise(xSize, ySize);   
            this.Save(stream, ImageFormat.Jpeg);   
        }   
  
        public void ResizeToJpeg(int xSize, int ySize, long jQuality, string fileName)   
        {   
            this.RGBRezise(xSize, ySize);   
            this.Save(fileName, jQuality, ImageFormat.Jpeg);   
        }   
  
        public void ResizeToJpeg(int xSize, int ySize, long jQuality, Stream stream)   
        {   
            this.RGBRezise(xSize, ySize);   
            this.Save(stream, jQuality, ImageFormat.Jpeg);   
        }   
  
  
        public void ResizeToGif(int xSize, int ySize, string fileName)   
        {   
            this.IndexedRezise(xSize, ySize);   
            this.Save(fileName, ImageFormat.Gif);   
        }   
  
        public void ResizeToGif(int xSize, int ySize, Stream stream)   
        {   
            this.IndexedRezise(xSize, ySize);   
            this.Save(stream, ImageFormat.Gif);   
        }   
  
        public Bitmap ResizeToGif(int xSize, int ySize)   
        {   
            this.IndexedRezise(xSize, ySize);   
            return scaledBitmap;   
        }   
  
        public void ResizeToPng(int xSize, int ySize, string fileName)   
        {   
            this.IndexedRezise(xSize, ySize);   
            this.Save(fileName, ImageFormat.Png);   
        }   
  
        public void ResizeToPng(int xSize, int ySize, Stream stream)   
        {   
            this.IndexedRezise(xSize, ySize);   
            this.Save(stream, ImageFormat.Png);   
        }   
    }   
}
