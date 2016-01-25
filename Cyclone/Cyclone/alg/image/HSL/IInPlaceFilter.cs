using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;

namespace Cyclone.alg.image.HSL
{

    public interface IInPlaceFilter
    {
        void ApplyInPlace(Bitmap image);
        void ApplyInPlace(BitmapData imageData);
    }
}
