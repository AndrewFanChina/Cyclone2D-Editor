using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Cyclone.mod.menu
{
    public interface Module
    {
        void display(Graphics g,int x,int y,int zoomLevel,int stateID);
        int getWidth();
        int getHeight();
    }
}
