using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using Cyclone.alg;
using Cyclone.alg.util;

namespace Cyclone.alg.image.HSL
{
    public class HSLModifier : FilterColorToColor
    {
        private int _hue;
        private double _saturation;
        private double _lightness;

        public HSLModifier()
        {
        }

        public HSLModifier(
            int hue,
            double saturation,
            double lightness)
        {
            Hue = hue;
            Saturation = saturation;
            Lightness = lightness;
        }

        public int Hue
        {
            get { return _hue; }
            set
            {
                _hue = Math.Max(0, Math.Min(value, 360));
            }
        }

        public double Saturation
        {
            get { return _saturation; }
            set
            {
                _saturation = Math.Max(-1, Math.Min(value, 1));
            }
        }

        public double Lightness
        {
            get { return _lightness; }
            set
            {
                _lightness = Math.Max(-1, Math.Min(value, 1));
            }
        }

        protected override unsafe void ProcessFilter(BitmapData imageData)
        {
            int width = imageData.Width;
            int height = imageData.Height;
            int perPixelLength = base.GetPerPixelFormatLength(
                imageData.PixelFormat);
            int offset = imageData.Stride - (width * perPixelLength);
            Color rgb;
            HSLColor hsl = new HSLColor();

            byte* csan0 = (byte*)imageData.Scan0.ToPointer();

            for (int i = 0; i < height; i++)
            {
                int widthOffset = 0;
                while (widthOffset < width)
                {
                    rgb = Color.FromArgb(csan0[2], csan0[1], csan0[0]);
                    hsl.Color = rgb;

                    hsl.Hue += _hue;
                    hsl.Saturation += _saturation;
                    hsl.Lightness += _lightness;

                    rgb = hsl.Color;

                    csan0[2] = rgb.R;
                    csan0[1] = rgb.G;
                    csan0[0] = rgb.B;

                    widthOffset++;
                    csan0 += perPixelLength;
                }
                csan0 += offset;
            }
        }

        protected override int ProcessColor(int Color)
        {
            Color rgb;
            HSLColor hsl = new HSLColor();

            hsl.Color = GraphicsUtil.getColor(Color);

            hsl.Hue += _hue;
            hsl.Saturation += _saturation;
            hsl.Lightness += _lightness;

            rgb = hsl.Color;

            Color = (0xFF<<24)|(rgb.R<<16)|(rgb.G<<8)|(rgb.B<<0);
            return Color;
        }
    }
}
