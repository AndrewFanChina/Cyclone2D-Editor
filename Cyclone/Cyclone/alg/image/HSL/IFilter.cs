using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Cyclone.alg.image.HSL
{
    public interface IFilter
    {
        Bitmap Apply(Bitmap image);
        Bitmap Apply(BitmapData imageData);
    }
}
