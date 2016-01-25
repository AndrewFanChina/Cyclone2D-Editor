using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Cyclone.alg.image;
using Cyclone.alg.type;
using Cyclone.alg.math;

namespace Cyclone.alg.util
{

    class GraphicsUtil
    {
        private static GraphicsUtil instance = null;
        //###################################### 图形操作 ######################################
        //根据0xXXXXXX返回颜色
        public static Color getColor(int pColor)
        {
            Color color = Color.FromArgb((pColor >> 16) & 0xFF, (pColor >> 8) & 0xFF, (pColor >> 0) & 0xFF);
            return color;
        }
        public static Color getAlphaColor(int pColor)
        {
            Color color = Color.FromArgb((pColor >> 24) & 0xFF, (pColor >> 16) & 0xFF, (pColor >> 8) & 0xFF, (pColor >> 0) & 0xFF);
            return color;
        }
        //得到反色
        public static int getOpposingColor(int pColor)
        {
            int color =((0xFF-((pColor >> 16) & 0xFF))<<16)|((0xFF-((pColor >> 8) & 0xFF))<<8)|((0xFF-((pColor >> 0) & 0xFF))<<0);
            return color;
        }
        //以两种相间的颜色绘制方格
        public static void fillBufWithTiles(Graphics g, int width, int height, int colorBright, int colorDark, int tileWH)
        {
            if (tileWH == 0 || g == null) return;
            Color brightColor = Color.FromArgb((colorBright >> 16) & 0xFF, (colorBright >> 8) & 0xFF, (colorBright >> 0) & 0xFF);
            Color darkcustomColor = Color.FromArgb((colorDark >> 16) & 0xFF, (colorDark >> 8) & 0xFF, (colorDark >> 0) & 0xFF);
            SolidBrush brushBright = new SolidBrush(brightColor);
            SolidBrush brushDark = new SolidBrush(darkcustomColor);
            for (int i = 0; i < width / tileWH + 1; i++)
            {
                for (int j = 0; j < height / tileWH + 1; j++)
                {
                    g.FillRectangle((i + j) % 2 == 1 ? brushBright : brushDark, i * tileWH, j * tileWH, tileWH, tileWH);
                }
            }
            brushBright.Dispose();
            brushDark.Dispose();
            brushBright = null;
            brushDark = null;
        }


        //填充矩形(包括边缘如x为0，跨度为10，填充0-9)
        public static void fillRect(Graphics g, int x, int y, int width, int height, Color color, Rect limitRect)
        {
            if (width <= 0 || height <= 0 || g == null) return;
            g.ResetTransform();
            g.SetClip(new Rectangle(x - 1, y - 1, width + 2, height + 2));
            if (limitRect != null)
            {
                Rectangle rect = limitRect.rect;
                if (rect.Width > 0 && rect.Height > 0)
                {
                    g.SetClip(rect, CombineMode.Intersect);
                }
            }
            Brush myBrush = new SolidBrush(color);
            g.FillRectangle(myBrush, x, y, width, height);
            myBrush.Dispose();
        }
        public static void fillRect(Graphics g, int x, int y, int width, int height, int pColor, int alpha, Rect limitRect)
        {
            Color color = Color.FromArgb(alpha, (pColor >> 16) & 0xFF, (pColor >> 8) & 0xFF, (pColor >> 0) & 0xFF);
            if (width <= 0 || height <= 0 || g == null) return;
            g.SetClip(new Rectangle(x - 1, y - 1, width + 2, height + 2));
            if (limitRect != null)
            {
                Rectangle rect = limitRect.rect;
                if (rect.Width > 0 && rect.Height > 0)
                {
                    g.SetClip(rect, CombineMode.Intersect);
                }
            }
            Brush myBrush = new SolidBrush(color);
            g.FillRectangle(myBrush, x, y, width, height);
            myBrush.Dispose();
        }
        public static void fillRectF(Graphics g, float x, float y, float width, float height, int pColor, int alpha, Rect limitRect)
        {
            Color color = Color.FromArgb(alpha, (pColor >> 16) & 0xFF, (pColor >> 8) & 0xFF, (pColor >> 0) & 0xFF);
            if (width <= 0 || height <= 0 || g == null) return;
            g.SetClip(new RectangleF(x - 1, y - 1, width + 2, height + 2));
            if (limitRect != null)
            {
                Rectangle rect = limitRect.rect;
                if (rect.Width > 0 && rect.Height > 0)
                {
                    g.SetClip(rect, CombineMode.Intersect);
                }
            }
            Brush myBrush = new SolidBrush(color);
            g.FillRectangle(myBrush, x, y, width, height);
            myBrush.Dispose();
        }
        public static void fillRect(Graphics g, int x, int y, int width, int height, int pColor)
        {
            fillRect(g, x, y, width, height, pColor, 0xFF, null);
        }
        public static void fillRectF(Graphics g, float x, float y, float width, float height, int pColor)
        {
            fillRectF(g, x, y, width, height, pColor, 0xFF, null);
        }
        public static void fillRect(Graphics g, float x, float y, float width, float height, int pColor, int alpha)
        {
            fillRect(g, (int)Math.Floor(x), (int)Math.Floor(y), (int)Math.Ceiling(width), (int)Math.Ceiling(height), pColor, alpha);
        }
        public static void fillRect(Graphics g, int x, int y, int width, int height, int pColor, int alpha)
        {
            fillRect(g, x, y, width, height, pColor, alpha, null);
        }
        public static void fillRect(Graphics g, int x, int y, int width, int height, int pColor, Rect limitRect)
        {
            fillRect(g, x, y, width, height, pColor, 0xFF, limitRect);
        }
        //填充排除矩形
        public static void fillExcludeRect(Graphics g, Rectangle totalRect, int pColor, int alpha, Rectangle excludeRect)
        {
            Color color = Color.FromArgb(alpha, (pColor >> 16) & 0xFF, (pColor >> 8) & 0xFF, (pColor >> 0) & 0xFF);
            if (totalRect.Width <= 0 || totalRect.Height <= 0 || g == null) return;
            if (excludeRect.Width <= 0 || excludeRect.Height <= 0) return;
            g.SetClip(totalRect);
            g.SetClip(excludeRect, CombineMode.Exclude);
            Brush myBrush = new SolidBrush(color);
            g.FillRectangle(myBrush, totalRect);
            myBrush.Dispose();
        }
        //清空区域
        public static void clearRegion(Graphics g, int x, int y, int w, int h)
        {
            g.SetClip(new Rectangle(x, y, w, h));
            g.Clear(Color.Transparent);
        }
        //绘制矩形(包括边缘如x为0，跨度为10，填充0-9)
        public static void drawRect(Graphics g, int x, int y, int width, int height, int pColor, int alpha, int borderWidth, Rect limitRect)
        {
            if (width <= 0 || height <= 0 || borderWidth <= 0 || g == null) return;
            fillRect(g, x, y, width, borderWidth, pColor, alpha, limitRect);
            fillRect(g, x, y, borderWidth, height, pColor, alpha, limitRect);
            fillRect(g, x + width - borderWidth, y, borderWidth, height, pColor, alpha, limitRect);
            fillRect(g, x, y + height - borderWidth, width, borderWidth, pColor, alpha, limitRect);
        }
        public static void drawRect(Graphics g, int x, int y, int width, int height, int pColor, int alpha, Rect limitRect)
        {
            if (width <= 0 || height <= 0 || g == null) return;
            fillRect(g, x, y, width, 1, pColor, alpha, limitRect);
            fillRect(g, x, y, 1, height, pColor, alpha, limitRect);
            fillRect(g, x + width - 1, y, 1, height, pColor, alpha, limitRect);
            fillRect(g, x, y + height - 1, width, 1, pColor, alpha, limitRect);
        }
        public static void drawRect(Graphics g, float x, float y, float width, float height, int pColor)
        {
            drawRect(g, (int)x, (int)y, (int)width, (int)height, pColor);
        }
        public static void drawRect(Graphics g, int x, int y, int width, int height, int pColor)
        {
            if (width <= 0 || height <= 0 || g == null) return;
            fillRect(g, x, y, width, 1, pColor);
            fillRect(g, x, y, 1, height, pColor);
            fillRect(g, x + width - 1, y, 1, height, pColor);
            fillRect(g, x, y + height - 1, width, 1, pColor);
        }
        public static void drawRect(Graphics g, int x, int y, int width, int height, int pColor, int alpha)
        {
            if (width <= 0 || height <= 0 || g == null) return;
            fillRect(g, x, y, width, 1, pColor, alpha);
            fillRect(g, x, y, 1, height, pColor, alpha);
            fillRect(g, x + width - 1, y, 1, height, pColor, alpha);
            fillRect(g, x, y + height - 1, width, 1, pColor, alpha);
        }
        public static void drawDashRect(Graphics g, int x, int y, int width, int height, int pColor, int borderWidth)
        {

            Color color = Color.FromArgb((pColor >> 16) & 0xFF, (pColor >> 8) & 0xFF, (pColor >> 0) & 0xFF);
            if (width <= 0 || height <= 0 || g == null) return;
            g.SetClip(new Rectangle(x - 1, y - 1, width + 2, height + 2));
            SolidBrush myBrush = new SolidBrush(color);
            Pen myPen = new Pen(myBrush, borderWidth);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawRectangle(myPen, x, y, width, height);
            myBrush.Dispose();
            myPen.Dispose();
        }
        //填充三角形
        public static void fillTiangle(Graphics g, Point[] points, int pColor)
        {
            if (points == null || points.Length < 3)
            {
                return;
            }
            int xMin = points[0].X;
            int yMin = points[0].Y;
            int xMax = points[0].X;
            int yMax = points[0].Y;
            for (int i = 1; i < points.Length; i++)
            {
                if (points[i].X < xMin)
                {
                    xMin = points[i].X;
                }
                if (points[i].X > xMax)
                {
                    xMax = points[i].X;
                }
                if (points[i].Y < yMin)
                {
                    yMin = points[i].Y;
                }
                if (points[i].Y > yMax)
                {
                    yMax = points[i].Y;
                }
            }
            int width = xMax - xMin + 1;
            int height = yMax - yMin + 1;
            g.SetClip(new Rectangle(xMin, yMin, width, height));
            SolidBrush drawBrush = new SolidBrush(getColor(pColor));
            g.FillPolygon(drawBrush, points, FillMode.Alternate);
            drawBrush.Dispose();
            drawBrush = null;
        }
        //绘制线段(C#的绘制线段缺少一个端点)
        public static void drawLine(Graphics g, int x1, int y1, int x2, int y2, int pColor, int borderWidth)
        {
            System.Drawing.Drawing2D.PixelOffsetMode poMode = g.PixelOffsetMode;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
            Rectangle clipRect = new Rectangle(Math.Min(x1, x2) - borderWidth, Math.Min(y1, y2) - borderWidth, Math.Abs(x1 - x2) + 1 + borderWidth * 2, Math.Abs(y1 - y2) + 1 + borderWidth * 2);
            g.SetClip(clipRect);
            Color color = Color.FromArgb((pColor >> 16) & 0xFF, (pColor >> 8) & 0xFF, (pColor >> 0) & 0xFF);
            Pen myPen = new Pen(color, borderWidth);
            g.DrawLine(myPen, x1, y1, x2, y2);
            myPen.Dispose();
            myPen = null;
            g.PixelOffsetMode = poMode;
        }
        public static void drawLine(Graphics g, int x1, int y1, int x2, int y2, int pColor, int borderWidth,int alpha)
        {
            System.Drawing.Drawing2D.PixelOffsetMode poMode = g.PixelOffsetMode;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
            Rectangle clipRect = new Rectangle(Math.Min(x1, x2) - borderWidth, Math.Min(y1, y2) - borderWidth, Math.Abs(x1 - x2) + 1 + borderWidth * 2, Math.Abs(y1 - y2) + 1 + borderWidth * 2);
            g.SetClip(clipRect);
            Color color = Color.FromArgb(alpha&0xFF,(pColor >> 16) & 0xFF, (pColor >> 8) & 0xFF, (pColor >> 0) & 0xFF);
            Pen myPen = new Pen(color, borderWidth);
            g.DrawLine(myPen, x1, y1, x2, y2);
            myPen.Dispose();
            myPen = null;
            g.PixelOffsetMode = poMode;
        }
        //绘制虚线段线段
        public static void drawDashLine(Graphics g, int x1, int y1, int x2, int y2, int pColor, int borderWidth)
        {
            System.Drawing.Drawing2D.PixelOffsetMode poMode = g.PixelOffsetMode;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
            Rectangle clipRect = new Rectangle(Math.Min(x1, x2) - borderWidth, Math.Min(y1, y2) - borderWidth, Math.Abs(x1 - x2) + borderWidth * 2, Math.Abs(y1 - y2) + borderWidth * 2);
            g.SetClip(clipRect);
            Color color = Color.FromArgb((pColor >> 16) & 0xFF, (pColor >> 8) & 0xFF, (pColor >> 0) & 0xFF);
            Pen myPen = new Pen(color, borderWidth);
            myPen.DashStyle = DashStyle.Dash;
            g.DrawLine(myPen, x1, y1, x2, y2);
            myPen.Dispose();
            myPen = null;
            g.PixelOffsetMode = poMode;
        }
        //绘制图片
        public static void drawImage(Graphics gDest, Image imgSrc, int x, int y, int _x, int _y, int _w, int _h)
        {
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            gDest.SetClip(new Rectangle(x, y, _w, _h));
            gDest.DrawImage(imgSrc, x, y, new Rectangle(_x, _y, _w, _h), GraphicsUnit.Pixel);
        }
        //绘制变形图片
        public static void drawImage(Graphics gDest, Image imgSrc, int destX, int destY, int destW, int destH, int srcX, int srcY, int srcW, int srcH)
        {
            gDest.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            gDest.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            gDest.SetClip(new Rectangle(destX, destY, destW, destH));
            gDest.DrawImage(imgSrc, new Rectangle(destX, destY, destW, destH), new Rectangle(srcX, srcY, srcW, srcH), GraphicsUnit.Pixel);
        }
        //绘制字符串
        public static void drawString(Graphics g, int x, int y, String s, Font font, int color, int anchor)
        {
            if (g == null)
            {
                return;
            }
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            int x_offset_clip = 0;
            int y_offset_clip = 0;
            SizeF size = g.MeasureString(s, font);
            int _w = (int)size.Width;
            int _h = (int)size.Height;
            //横向偏移
            if ((Consts.LEFT & anchor) != 0)
            {
                x_offset_clip = 0;
            }
            else if ((Consts.HCENTER & anchor) != 0)
            {
                x_offset_clip = -_w / 2;
            }
            else if ((Consts.RIGHT & anchor) != 0)
            {
                x_offset_clip = -_w;
            }
            //纵向偏移
            if ((Consts.TOP & anchor) != 0)
            {
                y_offset_clip = 0;
            }
            else if ((Consts.VCENTER & anchor) != 0)
            {
                y_offset_clip = -_h / 2;
            }
            else if ((Consts.BOTTOM & anchor) != 0)
            {
                y_offset_clip = -_h;
            }
            SolidBrush drawBrush = new SolidBrush(getColor(color));
            PointF drawPoint = new PointF(x + x_offset_clip, y + y_offset_clip);
            g.SetClip(new Rectangle(x + x_offset_clip, y + y_offset_clip, _w, _h));
            g.DrawString(s, font, drawBrush, drawPoint);
            drawBrush.Dispose();
        }
        public static void drawBorderString(Graphics g, int x, int y, String s, int color, int colorBg, int anchor)
        {
            drawString(g, x - 1, y - 1, s, Consts.fontDef, colorBg, anchor);
            drawString(g, x - 1, y + 1, s, Consts.fontDef, colorBg, anchor);
            drawString(g, x + 1, y - 1, s, Consts.fontDef, colorBg, anchor);
            drawString(g, x + 1, y + 1, s, Consts.fontDef, colorBg, anchor);
            drawString(g, x, y, s, Consts.fontDef, color, anchor);
        }
        public static void drawString(Graphics g, int x, int y, String s, int color, int anchor)
        {
            drawString(g, x, y, s, Consts.fontDef, color, anchor);
        }
        //在限定区域内绘制文本字符串(注意左边2像素空白)
        public static void drawTexts(Graphics g, int x, int y, String s, Font font, float zoomLevel, int color, Rect limitRect)
        {
            if (g == null)
            {
                return;
            }
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            int width = (int)g.MeasureString(s, font).Width;
            g.SetClip(new Rectangle((int)(x - zoomLevel * 2), y, (int)(width * zoomLevel + 1), (int)(font.GetHeight() * zoomLevel + 1)));
            if (limitRect != null)
            {
                Rectangle rectT = limitRect.rect;
                if (rectT.Width > 0 && rectT.Height > 0)
                {
                    g.SetClip(rectT, CombineMode.Intersect);
                }
            }
            SolidBrush drawBrush = new SolidBrush(getColor(color));

            PointF drawPoint = new PointF(x - zoomLevel * 2, y);
            Font fontT = new Font(font.Name, font.Size * zoomLevel, font.Style, GraphicsUnit.Pixel);
            g.DrawString(s, fontT, drawBrush, drawPoint);
            fontT.Dispose();
            fontT = null;
            drawBrush.Dispose();
        }


        //得到字符大小
        public static Size getStringSize(Graphics g, String measureString, Font stringFont)
        {

            if (measureString == null || measureString.Equals(""))
            {
                return new Size(0, 0);
            }
            if (g == null)
            {
                g = Form_Main.gFont;
            }
            SizeF fontSize = g.MeasureString(measureString, stringFont);
            Size size = new Size((int)fontSize.Width, (int)fontSize.Height);
            return size;
        }
        //得到字符大小
        public static SizeF getStringSizeF(Graphics g, String measureString, Font stringFont)
        {

            if (measureString == null || measureString.Equals(""))
            {
                return new Size(0, 0);
            }
            if (g == null)
            {
                g = Form_Main.gFont;
            }

            SizeF fontSize = g.MeasureString(measureString, stringFont);
            return fontSize;
        }
        //绘制翻转图片
        //public const int TRANS_NONE = 0;
        //public const int TRANS_FLIPY = 1;
        //public const int TRANS_FLIPX = 2;
        //public const int TRANS_ROT180 = 3;
        //public const int TRANS_FLIPX_ROT270 = 4;
        //public const int TRANS_ROT90 = 5;
        //public const int TRANS_ROT270 = 6;
        //public const int TRANS_FLIPX_ROT90 = 7;
        private static Point[] srcPara = { new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0) };
        private static Point[] destPara = new Point[3];
        private static void setParaIndex(int indexUL, int indexUR, int indexBL)
        {
            destPara[0] = srcPara[indexUL];
            destPara[1] = srcPara[indexUR];
            destPara[2] = srcPara[indexBL];
        }
        public static void drawClip(Graphics g, Image image, int destX, int destY, int _x, int _y, int _w, int _h, int operate)
        {
            if (g == null)
            {
                return;
            }
            drawClip(g, image, destX, destY, _x, _y, _w, _h, operate, 1);
        }
        public static void drawClip(Graphics g, Image image, int destX, int destY, int _x, int _y, int _w, int _h, int operate, float zoomLevel)
        {
            drawClip(g, image, destX, destY, _x, _y, _w, _h, operate, zoomLevel, null);
        }
        private static Rectangle srcRect = new Rectangle(0, 0, 1, 1);
        private static Rectangle destRect = new Rectangle(0, 0, 1, 1);
        private static float current_alpha = 0.9f;
        private static float[][] matrixItems ={ 
               new float[] {1, 0, 0, 0,    0},
               new float[] {0, 1, 0, 0,    0},
               new float[] {0, 0, 1, 0,    0},
               new float[] {0, 0, 0, 0.3f, 0}, 
               new float[] {0, 0, 0, 0,    1}
            };
        private static ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
        private static ImageAttributes imageAtt = new ImageAttributes();
        public static void drawClip(Graphics g, Image image, int destX, int destY, int _x, int _y, int _w, int _h, int operate, float zoomLevel, Rect limitRect)
        {
            drawClip(g, image, destX, destY, _x, _y, _w, _h, operate, zoomLevel, limitRect, 0xFF);
        }
        //alpha[0-255]
        public static void drawClip(Graphics g, Image image, int destX, int destY, int _x, int _y, int _w, int _h, int operate, float zoomLevel, Rect limitRect, float alpha)
        {
            if (image == null || zoomLevel <= 0 || g == null)
            {
                return;
            }
            g.ResetTransform();
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            try
            {
                int wReal = (int)(_w * zoomLevel);
                int hReal = (int)(_h * zoomLevel);

                if (operate == Consts.TRANS_ROT90 ||
                    operate == Consts.TRANS_ROT270 ||
                    operate == Consts.TRANS_MIRROR_ROT90 ||
                    operate == Consts.TRANS_MIRROR_ROT270)
                {
                    int t = wReal;
                    wReal = hReal;
                    hReal = t;
                }
                // Create parallelogram for drawing image.
                srcPara[0].X = destX;
                srcPara[0].Y = destY;
                srcPara[1].X = destX + wReal;
                srcPara[1].Y = destY;
                srcPara[2].X = destX;
                srcPara[2].Y = destY + hReal;
                srcPara[3].X = destX + wReal;
                srcPara[3].Y = destY + hReal;


                // Create rectangle for source image.
                srcRect.X = _x;
                srcRect.Y = _y;
                srcRect.Width = _w;
                srcRect.Height = _h;
                switch (operate)
                {
                    case Consts.TRANS_NONE://无
                        setParaIndex(0, 1, 2);
                        break;
                    case Consts.TRANS_MIRROR_ROT180://垂直翻转
                        setParaIndex(2, 3, 0);
                        break;
                    case Consts.TRANS_MIRROR://水平翻转
                        setParaIndex(1, 0, 3);
                        break;
                    case Consts.TRANS_ROT180://旋转180度
                        setParaIndex(3, 2, 1);
                        break;
                    case Consts.TRANS_MIRROR_ROT270://右上角对折左下角
                        setParaIndex(0, 2, 1);
                        break;
                    case Consts.TRANS_ROT90://旋转90度
                        setParaIndex(1, 3, 0);
                        break;
                    case Consts.TRANS_ROT270://旋转270度
                        setParaIndex(2, 0, 3);
                        break;
                    case Consts.TRANS_MIRROR_ROT90://左上角对折右下角
                        setParaIndex(3, 1, 2);
                        break;
                    default:
                        Console.WriteLine("------- error ------");
                        break;
                }
                destRect.X = destX;
                destRect.Y = destY;
                destRect.Width = wReal;
                destRect.Height = hReal;
                g.SetClip(destRect);
                if (limitRect != null)
                {
                    Rectangle rect = limitRect.rect;
                    if (rect.Width > 0 && rect.Height > 0)
                    {
                        g.SetClip(rect, CombineMode.Intersect);
                    }
                }
                if (alpha < 0 || alpha >= 0xFF)
                {
                    g.DrawImage(image, destPara, srcRect, GraphicsUnit.Pixel);
                }
                else
                {
                    ImageAttributes imageAttNow = getImageAttributes(alpha/0xFF);
                    g.DrawImage(image, destPara, srcRect, GraphicsUnit.Pixel, imageAttNow);
                }
                //g.ResetTransform();
                //GraphicsUtil.drawRect(g, destX, destY, wReal, hReal, 0xFF00);//debug
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("drawClip    destX:" + destX + ",destY:" + destY + ",_x:" + _x + ",_y:" + _y + ",_w:" + _w + ",_h:" + _h + ",operate:" + operate);
            }
            //Monitor.Exit(image);
        }
        public static void drawClip(Graphics g, Image image, Rectangle rectS, Rectangle rectD, Matrix matrix, float alpha)
        {
            if (image == null || g == null)
            {
                return;
            }
            GraphicsState gState = g.Save();
            g.ResetTransform();
            if (matrix != null)
            {
                g.Transform = matrix;
            }
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            ImageAttributes imageAttNoew = getImageAttributes(alpha);
            g.DrawImage(image, rectD, rectS.X, rectS.Y, rectS.Width, rectS.Height, GraphicsUnit.Pixel, imageAttNoew);
            g.Restore(gState);
        }
        public static ImageAttributes getImageAttributes(float alpha)
        {
            float newAlpha = alpha;
            if (newAlpha != current_alpha)
            {
                matrixItems[0][0] = newAlpha;
                matrixItems[1][1] = newAlpha;
                matrixItems[2][2] = newAlpha;
                matrixItems[3][3] = newAlpha;
                current_alpha = newAlpha;
                colorMatrix = new ColorMatrix(matrixItems);
                imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            }
            return imageAtt;
        }
        /** @todo 根据数字绘制图片 */
        private static Image[] imgFonts;
        public static void drawNumberByImage(Graphics g, int number, int x, int y, int anchor)
        {
            if (imgFonts == null)
            {
                imgFonts = new Image[11];
                if (instance == null)
                {
                    instance = new GraphicsUtil();
                }
                imgFonts[0] = IOUtil.createImage(instance.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.font_0.png"));
                imgFonts[1] = IOUtil.createImage(instance.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.font_1.png"));
                imgFonts[2] = IOUtil.createImage(instance.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.font_2.png"));
                imgFonts[3] = IOUtil.createImage(instance.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.font_3.png"));
                imgFonts[4] = IOUtil.createImage(instance.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.font_4.png"));
                imgFonts[5] = IOUtil.createImage(instance.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.font_5.png"));
                imgFonts[6] = IOUtil.createImage(instance.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.font_6.png"));
                imgFonts[7] = IOUtil.createImage(instance.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.font_7.png"));
                imgFonts[8] = IOUtil.createImage(instance.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.font_8.png"));
                imgFonts[9] = IOUtil.createImage(instance.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.font_9.png"));
                imgFonts[10] = IOUtil.createImage(instance.GetType().Assembly.GetManifestResourceStream("Cyclone.Resources.font_10.png"));
            }
            drawNumberByImage(g, imgFonts, number, x, y, 1, anchor);
        }
        public static void drawNumberByImage(Graphics g, Image[] img, int number, int x, int y, int anchor)
        {
            if (img == null)
            {
                return;
            }
            drawNumberByImage(g, img, number, x, y, 1, anchor);
        }
        private static int[] chars = new int[20];
        public static void drawNumberByImage(Graphics g, Image[] img, int number, int x, int y, int leastNum, int anchor)
        {
            if (number < 0)
                return;
            if (img == null || img.Length < 10)
            {
                return;
            }
            for (int i = 0; i < img.Length; i++)
            {
                if (img[i] == null)
                {
                    return;
                }
            }
            //计算------------------------------
            int k = 10;
            int numOfChar = 1;
            while ((number / k) >= 1)
            {
                numOfChar++;
                k *= 10;
            }
            //最小数位
            if (numOfChar < leastNum) numOfChar = leastNum;
            //存储------------------------------
            if (chars == null || numOfChar > chars.Length)
            {
                chars = new int[numOfChar];
            }
            //存放各位数字。
            for (int i = 0; i < numOfChar; i++)
            {
                chars[chars.Length - i - 1] = number % 10;
                number /= 10;
            }
            //偏移------------------------------
            int x_offset_clip = 0;
            int y_offset_clip = 0;
            int _w = 0;
            int _h = 0;
            Image imgChar;
            int imgIndex;
            for (int i = 0; i < numOfChar; i++)
            {
                imgIndex = chars[chars.Length - numOfChar + i];
                imgChar = img[imgIndex];
                _w += imgChar.Width;
                if (_h < imgChar.Height)
                {
                    _h = imgChar.Height;
                }
            }
            //横向偏移
            if ((Consts.LEFT & anchor) != 0)
            {
                x_offset_clip = 0;
            }
            else if ((Consts.HCENTER & anchor) != 0)
            {
                x_offset_clip = -_w / 2;
            }
            else if ((Consts.RIGHT & anchor) != 0)
            {
                x_offset_clip = -_w;
            }
            //纵向偏移
            if ((Consts.TOP & anchor) != 0)
            {
                y_offset_clip = 0;
            }
            else if ((Consts.VCENTER & anchor) != 0)
            {
                y_offset_clip = -_h / 2;
            }
            else if ((Consts.BOTTOM & anchor) != 0)
            {
                y_offset_clip = -_h;
            }
            //绘制
            x += x_offset_clip;
            y += y_offset_clip;
            for (int i = 0; i < numOfChar; i++)
            {
                imgIndex = chars[chars.Length - numOfChar + i];
                imgChar = img[imgIndex];
                drawClip(g, imgChar, x, y, 0, 0, imgChar.Width, imgChar.Height, 0);
                x += imgChar.Width;
            }
        }
        //###################################### 图片格式相关 ######################################
        //获得图像编码
        public static ImageCodecInfo GetEncoderInfo(string strExt)
        {
            StringBuilder sbExt = new StringBuilder();
            sbExt.Append(@"*");
            sbExt.AppendFormat(strExt.ToLower());
            ImageCodecInfo[] infoArray1 = ImageCodecInfo.GetImageEncoders();
            for (int num1 = 0; num1 < infoArray1.Length; num1++)
            {
                string[] strs = infoArray1[num1].FilenameExtension.Split(';');
                foreach (string str in strs)
                {
                    if (str.ToLower() == sbExt.ToString())
                    {
                        return infoArray1[num1];
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Make the image transparent. 
        /// The input is the color which you want to make transparent.
        /// </summary>
        /// <param name="color">The color to make transparent.</param>
        /// <param name="bitmap">The bitmap to make transparent.</param>
        /// <returns>New memory stream containing transparent background gif.</returns>
        public static Bitmap MakeTransparentGif(Bitmap bitmap, Color color)
        {
            byte R = color.R;
            byte G = color.G;
            byte B = color.B;

            MemoryStream fin = new MemoryStream();
            bitmap.Save(fin, System.Drawing.Imaging.ImageFormat.Gif);

            MemoryStream fout = new MemoryStream((int)fin.Length);
            int count = 0;
            byte[] buf = new byte[256];
            byte transparentIdx = 0;
            fin.Seek(0, SeekOrigin.Begin);
            //header
            count = fin.Read(buf, 0, 13);
            if ((buf[0] != 71) || (buf[1] != 73) || (buf[2] != 70)) return null; //GIF

            fout.Write(buf, 0, 13);

            int i = 0;
            if ((buf[10] & 0x80) > 0)
            {
                i = 1 << ((buf[10] & 7) + 1) == 256 ? 256 : 0;
            }

            for (; i != 0; i--)
            {
                fin.Read(buf, 0, 3);
                if ((buf[0] == R) && (buf[1] == G) && (buf[2] == B))
                {
                    transparentIdx = (byte)(256 - i);
                }
                fout.Write(buf, 0, 3);
            }

            bool gcePresent = false;
            while (true)
            {
                fin.Read(buf, 0, 1);
                fout.Write(buf, 0, 1);
                if (buf[0] != 0x21) break;
                fin.Read(buf, 0, 1);
                fout.Write(buf, 0, 1);
                gcePresent = (buf[0] == 0xf9);
                while (true)
                {
                    fin.Read(buf, 0, 1);
                    fout.Write(buf, 0, 1);
                    if (buf[0] == 0) break;
                    count = buf[0];
                    if (fin.Read(buf, 0, count) != count) return null;
                    if (gcePresent)
                    {
                        if (count == 4)
                        {
                            buf[0] |= 0x01;
                            buf[3] = transparentIdx;
                        }
                    }
                    fout.Write(buf, 0, count);
                }
            }
            while (count > 0)
            {
                count = fin.Read(buf, 0, 1);
                fout.Write(buf, 0, 1);
            }
            fin.Close();
            fout.Flush();

            return new Bitmap(fout);
        }
        //从PNG图片数据获得调色板(包括数据块和CRC)
        public static byte[] getPngPal(byte[] imgData)
        {
            int startIndex = 0;

            for (int i = 0; i < imgData.Length - 8; i++)
            {
                if (imgData[i] == 'P' && imgData[i + 1] == 'L' && imgData[i + 2] == 'T' && imgData[i + 3] == 'E')
                {
                    startIndex = i;
                    break;
                }
            }

            if (startIndex == 0) return null;

            int palDataLength = (imgData[startIndex - 4] & 0xff) << 24 | (imgData[startIndex - 3] & 0xff) << 16 | (imgData[startIndex - 2] & 0xff) << 8 | (imgData[startIndex - 1] & 0xff) + 4;

            byte[] palData = new byte[palDataLength];

            for (int i = 0; i < palDataLength; i++)
            {
                palData[i] = imgData[startIndex + 4 + i];
            }

            return palData;
        }
        //从PNG图片数据获得透明色编号
        public static short getPngTransID(byte[] imgData)
        {
            int startIndex = -1;
            short transID = -1;
            for (int i = 0; i < imgData.Length - 8; i++)
            {
                if (imgData[i] == 't' && imgData[i + 1] == 'R' && imgData[i + 2] == 'N' && imgData[i + 3] == 'S')
                {
                    startIndex = i;
                    break;
                }
            }

            if (startIndex <= 0)
            {
                return -1;
            }

            int transDataLength = (imgData[startIndex - 4] & 0xff) << 24 | (imgData[startIndex - 3] & 0xff) << 16 | (imgData[startIndex - 2] & 0xff) << 8 | (imgData[startIndex - 1] & 0xff) + 4;

            for (short i = 0; i < transDataLength; i++)
            {
                byte alpha = imgData[startIndex + 4 + i];
                if (alpha == 00)
                {
                    transID = i;
                    break;
                }
            }
            return transID;
        }
        //创建缓冲图片
        public static Image createImage(int width, int height, int bgColor, int testNum)
        {
            Image img = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(img);
            fillRect(g, 0, 0, width, height, bgColor);
            return img;
        }
        //创建缓冲图片
        public static Image createImage(int width, int height, Color color)
        {
            Image img = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(img);
            g.Clear(color);
            return img;
        }
        //创建Alpha图片，并有相应的调色板映射表
        public static Image createAlphaPmtImage(String path_folder, String imgName, String alphaName, String pmtName,int alpha)
        {
            String imgPath = path_folder + Consts.SUBPARH_IMG + imgName;
            String alphaPath = path_folder + Consts.SUBPARH_IMG + alphaName;
            String pmtPath = path_folder + Consts.SUBPARH_IMG + pmtName;
            if (imgPath.ToLower().EndsWith("bmp"))
            {
                byte[] bmpData = IOUtil.ReadFile(imgPath);
                if (bmpData == null)
                {
                    MessageBox.Show("未找到指定图片：" + imgName, "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
                //替换调色板映射表
                if (pmtName != null && !pmtName.Equals(""))
                {
                    MessageBox.Show("BMP图片暂不支持调色板映射，因为颜色太多。后面需要使用调色公式来转换。", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //生成图片
                MemoryStream ms = new MemoryStream(bmpData);
                Bitmap img = new Bitmap(ms);
                //Alpha混合
                bool hasAlphaImg = alphaName != null && !alphaName.Equals("");
                if (hasAlphaImg || alpha != 0xFF)
                {
                    Bitmap imgAlpha = null;
                    if (File.Exists(alphaPath))
                    {
                        imgAlpha = (Bitmap)(IOUtil.createImage(alphaPath));
                    }
                    else if (hasAlphaImg)
                    {
                        MessageBox.Show("Alpha文件丢失：" + alphaName, "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    img = createAlpahImage(img, imgAlpha, alpha);
                }
                return img;
            }
            else if (imgPath.ToLower().EndsWith("png"))
            {
                byte[] pngData = IOUtil.ReadFile(imgPath);
                if (pngData == null)
                {
                    MessageBox.Show("未找到指定图片：" + imgName, "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
                byte[] palData = GraphicsUtil.getPngPal(pngData);
                if (palData!= null)
                {
                    //替换调色板映射表
                    if (pmtName != null && !pmtName.Equals(""))
                    {
                        if (File.Exists(pmtPath))
                        {
                            FileStream fs = new FileStream(pmtPath, FileMode.Open);
                            if (fs.CanRead)
                            {
                                ColorTable srcColorTableNew = new ColorTable();
                                ColorTable destColorTableNew = new ColorTable();
                                srcColorTableNew.readObject(fs, false);
                                destColorTableNew.readObject(fs, false);
                                fs.Close();
                                //int ignoreID = GraphicsUtil.getPngTransID(pngData);
                                applyPmt(palData, srcColorTableNew, destColorTableNew, -1);
                                srcColorTableNew.removeAllColors();
                                destColorTableNew.removeAllColors();
                                srcColorTableNew = null;
                                destColorTableNew = null;
                                //更新CRC
                                GraphicsUtil.updatePalCrc(palData);
                                //替换调色板数据
                                pngData = GraphicsUtil.changePalData(pngData, palData);
                            }
                        }
                        else
                        {
                            MessageBox.Show("映射表文件丢失：" + pmtName, "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }

                //生成图片
                MemoryStream ms = new MemoryStream(pngData);
                Bitmap img = new Bitmap(ms);
                //Alpha混合
                bool hasAlphaImg = alphaName != null && !alphaName.Equals("");
                if (hasAlphaImg || alpha != 0xFF)
                {
                    Bitmap imgAlpha = null;
                    if (File.Exists(alphaPath))
                    {
                        imgAlpha = (Bitmap)(IOUtil.createImage(alphaPath));
                    }
                    else if (hasAlphaImg)
                    {
                        MessageBox.Show("Alpha文件丢失：" + alphaName, "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    img = createAlpahImage(img, imgAlpha, alpha);
                }
                return img;
            }
  
            return null;
        }
        //对调色板数据应用映射表
        public static void applyPmt(byte[] palData, ColorTable srcColorTable, ColorTable destColorTable, int ignoreID)
        {
            int color, colorJ;
            int palLen = (palData.Length - 4) / 3;
            ArrayList modifiedColors = new ArrayList();
            for (int i = 0; i < srcColorTable.getColorCount(); i++)
            {
                color = (int)srcColorTable.getColor(i);
                for (int j = 0; j < palLen; j++)
                {
                    if (modifiedColors.Contains(j))
                    {
                        continue;
                    }
                    if (j == ignoreID)
                    {
                        continue;//跳过透明色
                    }
                    else
                    {
                        colorJ = (0xFF << 24) | (palData[j * 3] << 16) | (palData[j * 3 + 1] << 8) | (palData[j * 3 + 2]);
                    }
                    if (colorJ == color)
                    {
                        int mappedColor = (int)destColorTable.getColor(i);
                        palData[j * 3] = (byte)((mappedColor >> 16) & 0xFF);
                        palData[j * 3 + 1] = (byte)((mappedColor >> 8) & 0xFF);
                        palData[j * 3 + 2] = (byte)((mappedColor) & 0xFF);
                        modifiedColors.Add(j);
                    }
                }
            }
            modifiedColors.Clear();
            modifiedColors = null;
        }
        //创建Alpha混合的半透明图片
        public static Bitmap createAlpahImage(Bitmap imgSrc, Bitmap imgAlpha, int alpha)
        {
            //Bitmap imgSrc = new Bitmap(imgSrcP);
            //Bitmap imgAlpha = new Bitmap(imgAlphaP);
            alpha &= 0xFF;
            if (imgSrc == null)
            {
                return null;
            }
            if (imgAlpha != null)
            {
                if (imgSrc.Width != imgAlpha.Width || imgAlpha.Height != imgAlpha.Height)
                {
                    return null;
                }
            }
            else
            {
                if (alpha == 0xFF)
                {
                    return imgSrc;
                }
            }
            Bitmap imgBlend = new Bitmap(imgSrc.Width, imgSrc.Height, PixelFormat.Format32bppArgb);
            unsafe
            {
                BitmapData srcBitmapData = imgSrc.LockBits(new Rectangle(0, 0, imgSrc.Width, imgSrc.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                BitmapData alphaBitmapData = null;
                BitmapData blendBitmapData = imgBlend.LockBits(new Rectangle(0, 0, imgBlend.Width, imgBlend.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                int* srcData = (int*)(srcBitmapData.Scan0).ToPointer();
                int* blendData = (int*)(blendBitmapData.Scan0).ToPointer();
                int* destData = null;
                int colorAlpha = 0;
                int pixelCount = imgSrc.Width * imgSrc.Height;
                if (imgAlpha != null)
                {
                    alphaBitmapData = imgAlpha.LockBits(new Rectangle(0, 0, imgAlpha.Width, imgAlpha.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                    destData = (int*)(alphaBitmapData.Scan0).ToPointer();
                    for (int i = 0; i < pixelCount; i++)
                    {
                        colorAlpha = (srcData[i]>>24) & 0xFF;
                        if (colorAlpha != 0)
                        {
                            colorAlpha = destData[i] & 0xFF;
                            colorAlpha = colorAlpha * alpha / 0xFF;
                            blendData[i] = (srcData[i] & 0xFFFFFF) | ((colorAlpha) << 24);
                        }
                        else
                        {
                            blendData[i] = srcData[i];
                        }

                    }
                    imgAlpha.UnlockBits(alphaBitmapData);
                }
                else
                {
                    for (int i = 0; i < pixelCount; i++)
                    {
                        colorAlpha = (srcData[i] >> 24) & 0xFF;
                        if (colorAlpha != 0)
                        {
                            blendData[i] = (srcData[i] & 0xFFFFFF) | ((colorAlpha * alpha / 0xFF) << 24);
                        }
                        else
                        {
                            blendData[i] = srcData[i];
                        }

                    }
                }
                imgSrc.UnlockBits(srcBitmapData);
                imgBlend.UnlockBits(blendBitmapData);

            }
            return imgBlend;
        }
        //创建固定透明度的半透明图片
        public static Image createAlpahImage(Bitmap imgSrc, int alpha)
        {
            if (imgSrc == null )
            {
                return null;
            }
            Bitmap imgBlend = new Bitmap(imgSrc.Width, imgSrc.Height, PixelFormat.Format32bppArgb);
            unsafe
            {
                BitmapData srcBitmapData = imgSrc.LockBits(new Rectangle(0, 0, imgSrc.Width, imgSrc.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                BitmapData blendBitmapData = imgBlend.LockBits(new Rectangle(0, 0, imgBlend.Width, imgBlend.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                int* srcData = (int*)(srcBitmapData.Scan0).ToPointer();
                int* blendData = (int*)(blendBitmapData.Scan0).ToPointer();
                for (int i = 0; i < imgSrc.Width * imgSrc.Height; i++)
                {
                    int colorSrc = srcData[i];
                    int colorAlpha = (colorSrc >> 24) & 0xFF;
                    colorAlpha = colorAlpha * alpha / 0xFF;
                    int clolorBlending = (colorSrc & 0xFFFFFF) | ((colorAlpha & 0xFF) << 24);
                    blendData[i] = clolorBlending;
                }
                imgSrc.UnlockBits(srcBitmapData);
                imgBlend.UnlockBits(blendBitmapData);

            }
            return imgBlend;
        }
        private static long[] crcTable;
        //生成CRC表
        public static void makeCrcTable()
        {
            crcTable = new long[256];
            long value;

            for (int i = 0; i < 256; i++)
            {
                value = (long)i;

                for (int j = 0; j < 8; j++)
                {
                    if ((value & 1) != 0)
                    {
                        value = 0xedb88320L ^ (value >> 1);
                    }
                    else
                    {
                        value = value >> 1;
                    }
                }
                crcTable[i] = value;
            }
        }
        //更新CRC
        public static void updatePalCrc(byte[] data)
        {
            int colorLength = data.Length - 4;
            byte[] palData = new byte[data.Length];

            palData[0] = (byte)'P';
            palData[1] = (byte)'L';
            palData[2] = (byte)'T';
            palData[3] = (byte)'E';

            for (int i = 0; i < colorLength; i++)
            {
                palData[i + 4] = data[i];
            }

            long value = 0xffffffffL;

            if (crcTable == null) makeCrcTable();

            for (int i = 0; i < palData.Length; i++)
            {
                value = crcTable[(int)((value ^ palData[i]) & 0xff)] ^ (value >> 8);
            }
            int crc = (int)(value ^ 0xffffffffL);
            data[colorLength] = (byte)((crc >> 24) & 0xff);
            data[colorLength + 1] = (byte)((crc >> 16) & 0xff);
            data[colorLength + 2] = (byte)((crc >> 8) & 0xff);
            data[colorLength + 3] = (byte)(crc & 0xff);
        }
        //替换调色板
        public static byte[] changePalData(byte[] imgData, byte[] palData)
        {
            int startIndex = 0;

            for (int i = 0; i < imgData.Length - 8; i++)
            {
                if (imgData[i] == 'P' && imgData[i + 1] == 'L' && imgData[i + 2] == 'T' && imgData[i + 3] == 'E')
                {
                    startIndex = i;
                    break;
                }
            }

            if (startIndex == 0) return imgData;

            int palDataLength = (imgData[startIndex - 4] & 0xff) << 24 | (imgData[startIndex - 3] & 0xff) << 16 | (imgData[startIndex - 2] & 0xff) << 8 | (imgData[startIndex - 1] & 0xff) + 4;

            if (palDataLength != palData.Length) return imgData;

            byte[] imgDataEdit = new byte[imgData.Length];
            imgData.CopyTo(imgDataEdit, 0);

            for (int i = 0; i < palDataLength; i++)
            {
                imgDataEdit[startIndex + 4 + i] = palData[i];
            }

            return imgDataEdit;
        }
        //HSB到RGB颜色模式转换
        public static int HSBtoRGB(double hue, double saturation, double brightness)
        {
            int r = 0, g = 0, b = 0;
            if (saturation == 0)
            {
                r = g = b = (int)(brightness * 255.0f + 0.5f);
            }
            else
            {
                double h = (hue - (double)Math.Floor(hue)) * 6.0D;
                double f = h - (double)Math.Floor(h);
                double p = brightness * (1.0f - saturation);
                double q = brightness * (1.0f - saturation * f);
                double t = brightness * (1.0f - (saturation * (1.0f - f)));
                switch ((int)h)
                {
                    case 0:
                        r = (int)(brightness * 255.0f + 0.5f);
                        g = (int)(t * 255.0f + 0.5f);
                        b = (int)(p * 255.0f + 0.5f);
                        break;
                    case 1:
                        r = (int)(q * 255.0f + 0.5f);
                        g = (int)(brightness * 255.0f + 0.5f);
                        b = (int)(p * 255.0f + 0.5f);
                        break;
                    case 2:
                        r = (int)(p * 255.0f + 0.5f);
                        g = (int)(brightness * 255.0f + 0.5f);
                        b = (int)(t * 255.0f + 0.5f);
                        break;
                    case 3:
                        r = (int)(p * 255.0f + 0.5f);
                        g = (int)(q * 255.0f + 0.5f);
                        b = (int)(brightness * 255.0f + 0.5f);
                        break;
                    case 4:
                        r = (int)(t * 255.0f + 0.5f);
                        g = (int)(p * 255.0f + 0.5f);
                        b = (int)(brightness * 255.0f + 0.5f);
                        break;
                    case 5:
                        r = (int)(brightness * 255.0f + 0.5f);
                        g = (int)(p * 255.0f + 0.5f);
                        b = (int)(q * 255.0f + 0.5f);
                        break;
                }
            }
            return (0xFF<<24) | (r << 16) | (g << 8) | (b << 0);
        }
        //RGB到HSB颜色模式转换(返回以1为最大值的浮点数)
        private static double[] hsbvals = new double[3];
        public static double[] RGBtoHSB(int r, int g, int b)
        {
            double hue, saturation, brightness;
            hsbvals[0] = 0.0F;
            hsbvals[1] = 0.0F;
            hsbvals[2] = 0.0F;
            int cmax = (r > g) ? r : g;
            if (b > cmax) cmax = b;
            int cmin = (r < g) ? r : g;
            if (b < cmin) cmin = b;

            brightness = ((double)cmax) / 255.0D;
            if (cmax != 0)
                saturation = ((double)(cmax - cmin)) / ((double)cmax);
            else
                saturation = 0;
            if (saturation == 0)
                hue = 0;
            else
            {
                double redc = ((double)(cmax - r)) / ((double)(cmax - cmin));
                double greenc = ((double)(cmax - g)) / ((double)(cmax - cmin));
                double bluec = ((double)(cmax - b)) / ((double)(cmax - cmin));
                if (r == cmax)
                    hue = bluec - greenc;
                else if (g == cmax)
                    hue = 2.0f + redc - bluec;
                else
                    hue = 4.0f + greenc - redc;
                hue = hue / 6.0f;
                if (hue < 0)
                    hue = hue + 1.0f;
            }
            hsbvals[0] = hue;
            hsbvals[1] = saturation;
            hsbvals[2] = brightness;
            return hsbvals;
        }

          
        int  CheckRGB(int Value)   
        {   
            if (Value < 0) Value = 0;   
            else if (Value > 255) Value = 255;
            return Value;
        }   
          
        //void AssignRGB(int R, BYTE &G, BYTE &B, int intR, int intG, int intB)   
        //{   
        //    R = intR;   
        //    G = intG;   
        //    B = intB;   
        //}   
          
        int[] SetBright(int R, int G, int B, int bValue)   
        {   
            int intR = R;   
            int intG = G;   
            int intB = B;   
            if (bValue > 0)   
            {   
                intR = intR + (255 - intR) * bValue / 255;   
                intG = intG + (255 - intG) * bValue / 255;   
                intB = intB + (255 - intB) * bValue / 255;   
            }   
            else if (bValue < 0)   
            {   
                intR = intR + intR * bValue / 255;   
                intG = intG + intG * bValue / 255;   
                intB = intB + intB * bValue / 255;   
            }
            return new int[] { CheckRGB(intR), CheckRGB(intG), CheckRGB(intB) };

        }

        void SetHueAndSaturation(int R, int G, int B, int hValue, int sValue)   
        {   
            int intR = R;   
            int intG = G;   
            int intB = B;
            int temp;
            if (intR < intG)
            {
                temp = intG;
                intR = intG;
                intG = temp;
            }
            if (intR < intB)
            {
                temp = intB;
                intB = intR;
                intR = temp;
            }
            if (intB > intG)
            {
                temp = intG;
                intG = intB;
                intB = temp;
            }
          
            int delta = intR - intB;   
            if (delta==0) return;   
          
            int entire = intR + intB;   
            int H, S, L = entire >> 1;   
            if (L < 128)   
                S = delta * 255 / entire;   
            else  
                S = delta * 255 / (510 - entire);   
            if (hValue!=0)   
            {   
                if (intR == R)   
                    H = (G - B) * 60 / delta;   
                else if (intR == G)   
                    H = (B - R) * 60 / delta + 120;   
                else  
                    H = (R - G) * 60 / delta + 240;   
                H += hValue;   
                if (H < 0) H += 360;   
                else if (H > 360) H -= 360;   
                int index = H / 60;   
                int extra = H % 60;   
                if ((index & 1)!=0) extra = 60 - extra;   
                extra = (extra * 255 + 30) / 60;   
                intG = extra - (extra - 128) * (255 - S) / 255;   
                int Lum = L - 128;   
                if (Lum > 0)   
                    intG += (((255 - intG) * Lum + 64) / 128);   
                else if (Lum < 0)   
                    intG += (intG * Lum / 128);
                intG=CheckRGB(intG);   
                switch (index)   
                {   
                    case 1:   
                        temp = intR;
                        intR = intG;
                        intG = temp;
                        break;   
                    case 2:   
                        temp = intR;
                        intR = intB;
                        intB = temp;

                        temp = intG;
                        intG = intB;
                        intB = temp;
                        break;   
                    case 3:   
                        temp = intR;
                        intR = intB;
                        intB = temp;
                        break;   
                    case 4:
                        temp = intR;
                        intR = intG;
                        intG = temp;

                        temp = intG;
                        intG = intB;
                        intB = temp; 
                        break;   
                    case 5:
                        temp = intG;
                        intG = intB;
                        intB = temp;
                        break;   
                }   
            }   
            else  
            {   
                intR = R;   
                intG = G;   
                intB = B;   
            }   
            if (sValue!=0)   
            {   
                if (sValue > 0)   
                {   
                    sValue = sValue + S >= 255? S: 255 - sValue;   
                    sValue = 65025 / sValue - 255;   
                }   
                intR += ((intR - L) * sValue / 255);   
                intG += ((intG - L) * sValue / 255);   
                intB += ((intB - L) * sValue / 255);
                intR=CheckRGB(intR);
                intG=CheckRGB(intG);
                intB=CheckRGB(intB);   
            }
            R = intR;
            G = intG;
            B = intB;  
        }   
          
        //void GdipHSBAdjustment(Bitmap *Bmp, int hValue, int sValue, int bValue)   
        //{   
        //    sValue = sValue * 255 / 100;   
        //    bValue = bValue * 255 / 100;   
        //    BitmapData data;   
        //    Rect r(0, 0, Bmp->GetWidth(), Bmp->GetHeight());   
        //    Bmp->LockBits(&r, ImageLockModeRead | ImageLockModeWrite, PixelFormat32bppARGB, &data);   
        //    try  
        //    {   
        //        int offset = data.Stride - data.Width * 4;   
        //        unsigned char *p = (unsigned char*)data.Scan0;   
        //        for (int y = 0; y < data.Height; y ++, p += offset)   
        //            for (int x = 0; x < data.Width; x ++, p += 4)   
        //            {   
        //                if (sValue > 0 && bValue)   
        //                    SetBright(p[2], p[1], p[0], bValue);   
        //                SetHueAndSaturation(p[2], p[1], p[0], hValue, sValue);   
        //                if (bValue && sValue <= 0)   
        //                    SetBright(p[2], p[1], p[0], bValue);   
        //            }   
          
        //    }   
        //    __finally  
        //    {   
        //        Bmp->UnlockBits(&data);   
        //    }   
        //}  
        //根据指定位图，返回合适大小的OpenGL贴图
        public static Bitmap getMatchImage(Bitmap image)
        {
            Size mPixelSize = new Size(image.Width, image.Height);
            int width = MathUtil.getMultipleOfTwo(mPixelSize.Width);
            int height = MathUtil.getMultipleOfTwo(mPixelSize.Height);
            //转移到新的位图
            Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            GraphicsUtil.drawClip(g, image, 0, 0, 0, 0, image.Width, image.Height, 0);
            return bitmap;
        }
    }
}
