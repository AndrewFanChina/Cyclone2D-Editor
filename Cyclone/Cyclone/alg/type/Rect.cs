using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Cyclone.alg.type
{
    public class Rect
    {
        public Rectangle rect;
        public Rect(int x,int y,int w,int h)
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = w;
            rect.Height = h;
        }
        public void setValue(int x, int y, int w, int h)
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = w;
            rect.Height = h;
        }
        public void clear()
        {
            rect.X = 0;
            rect.Y = 0;
            rect.Width = 0;
            rect.Height = 0;
        }
        public bool isEmpty()
        {
            return X == 0 && Y == 0 && W == 0 && H == 0;
        }
        public int X
        {
            get
            {
                return rect.X;

            }
            set
            {
                rect.X = value;
            }
        }
        public int Y
        {
            get
            {
                return rect.Y;

            }
            set
            {
                rect.Y = value;
            }
        }
        public int W
        {
            get
            {
                return rect.Width;

            }
            set
            {
                rect.Width = value;
            }
        }
        public int H
        {
            get
            {
                return rect.Height;

            }
            set
            {
                rect.Height = value;
            }
        } 
    }
}
