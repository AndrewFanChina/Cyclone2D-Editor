using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Cyclone.alg.image.HSL
{
    public class HSLColor
    {
        private int _alpha = 255;
        private int _hue = 0;
        private double _saturation = 1d;
        private double _lightness = 1d;

        public HSLColor()
        {
        }

        /// <summary>
        /// 用一个RGB颜色构造HSLColor。
        /// </summary>
        /// <param name="color"></param>
        public HSLColor(Color color)
        {
            _alpha = color.A;
            FromRGB(color);
        }

        /// <summary>
        /// 用色彩、饱和度、亮度构造HSLColor。
        /// </summary>
        /// <param name="hue">色彩。</param>
        /// <param name="saturation">饱和度。</param>
        /// <param name="lightness">亮度。</param>
        public HSLColor(
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
                if (value < 0)
                {
                    _hue = value + 360;
                }
                else if (_hue > 360)
                {
                    _hue = value % 360;
                }
                else
                {
                    _hue = value;
                }
            }
        }

        public double Saturation
        {
            get { return _saturation; }
            set
            {
                if (_saturation < 0)
                {
                    _saturation = 0;
                }
                else
                {
                    _saturation = Math.Min(value, 1d);
                }
            }
        }

        public double Lightness
        {
            get { return _lightness; }
            set
            {
                if (_lightness < 0)
                {
                    _lightness = 0;
                }
                else
                {
                    _lightness = Math.Min(value, 1d);
                }
            }
        }

        public Color Color
        {
            get { return ToRGB(); }
            set { FromRGB(value); }
        }

        public static bool operator ==(HSLColor left, HSLColor right)
        {
            return (left.Hue == right.Hue &&
                    left.Lightness == right.Lightness &&
                    left.Saturation == right.Saturation);
        }

        public static bool operator !=(HSLColor left, HSLColor right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null && !(obj is HSLColor))
            {
                return false;
            }

            HSLColor color = (HSLColor)obj;
            return this == color;
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(
                "HSL({0:f2}, {1:f2}, {2:f2})",
                Hue, Saturation, Lightness);
        }

        private void FromRGB(Color color)
        {
            double r = ((double)color.R) / 255;
            double g = ((double)color.G) / 255;
            double b = ((double)color.B) / 255;

            double min = Math.Min(Math.Min(r, g), b);
            double max = Math.Max(Math.Max(r, g), b);
            double distance = max - min;

            _lightness = (max + min) / 2;
            if (distance == 0)
            {
                _hue = 0;
                _saturation = 0;
            }
            else
            {
                double hueTmp;
                _saturation =
                    (_lightness < 0.5) ?
                    (distance / (max + min)) : (distance / ((2 - max) - min));
                double tempR = (((max - r) / 6) + (distance / 2)) / distance;
                double tempG = (((max - g) / 6) + (distance / 2)) / distance;
                double tempB = (((max - b) / 6) + (distance / 2)) / distance;
                if (r == max)
                {
                    hueTmp = tempB - tempG;
                }
                else if (g == max)
                {
                    hueTmp = (0.33333333333333331 + tempR) - tempB;
                }
                else
                {
                    hueTmp = (0.66666666666666663 + tempG) - tempR;
                }
                if (hueTmp < 0)
                {
                    hueTmp += 1;
                }
                if (hueTmp > 1)
                {
                    hueTmp -= 1;
                }
                _hue = (int)(hueTmp * 360);
            }
        }

        private Color ToRGB()
        {
            byte r;
            byte g;
            byte b;

            if (_saturation == 0)
            {
                r = (byte)(_lightness * 255);
                g = r;
                b = r;
            }
            else
            {
                double vH = ((double)_hue) / 360;
                double v2 =
                    (_lightness < 0.5) ?
                    (_lightness * (1 + _saturation)) :
                    ((_lightness + _saturation) - (_lightness * _saturation));
                double v1 = (2 * _lightness) - v2;
                r = (byte)(255 * HueToRGB(v1, v2, vH + 0.33333333333333331));
                g = (byte)(255 * HueToRGB(v1, v2, vH));
                b = (byte)(255 * HueToRGB(v1, v2, vH - 0.33333333333333331));
            }
            return Color.FromArgb(r, g, b); 
        }

        private double HueToRGB(double v1, double v2, double vH)
        {
            if (vH < 0)
            {
                vH += 1;
            }
            if (vH > 1)
            {
                vH -= 1;
            }
            if ((6 * vH) < 1)
            {
                return v1 + (((v2 - v1) * 6) * vH);
            }
            if ((2 * vH) < 1)
            {
                return v2;
            }
            if ((3 * vH) < 2)
            {
                return v1 + (((v2 - v1) * (0.66666666666666663 - vH)) * 6);
            }
            return v1;
        } 
    }
}
