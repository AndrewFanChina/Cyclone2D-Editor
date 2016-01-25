using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.mod;
using System.Drawing;
using Cyclone.alg;
using Cyclone.mod.map;
using Cyclone.alg.type;

namespace Cyclone.mod.menu
{
    class MovieClip : Module
    {
        private Antetype antetype;
        private int x, y, actionID,frameID;
        public MovieClip(Antetype antetypeT, int xT, int yT)
        {
            antetype = antetypeT;
            setPos(xT, yT);
        }
        public void setPos(int xT, int yT)
        {
            x = xT;
            y = yT;
        }
        public void setFrame(int frameIDT)
        {
            frameID = frameIDT;
        }
        public void setActionID(int actionIDT)
        {
            actionID = actionIDT;
        }
        private static Size errorClipSzie = new Size(80, 80);
        #region Module ≥…‘±

        public void display(Graphics g, int xS, int yS, int zoomLevel, int stateID)
        {
            if (antetype != null)
            {
                antetype.display(g, xS + x * zoomLevel - errorClipSzie.Width * zoomLevel / 2, yS + y * zoomLevel - errorClipSzie.Height * zoomLevel / 2,
                    errorClipSzie.Width * zoomLevel, errorClipSzie.Height * zoomLevel, zoomLevel,actionID,frameID, false);
            }
        }

        public int getWidth()
        {
            Rect rect = getSize();
            if (rect != null)
            {
                return rect.W;
            }
            return -1;
        }
        public int getHeight()
        {
            Rect rect = getSize();
            if (rect != null)
            {
                return rect.H;
            }
            return -1;
        }
        public Rect getSize()
        {
            if (antetype != null)
            {
                //ActionElement action = antetype.actor.getActionElement(actionID);
                //ActionFrameElement actionFrame = action.getElement(frameID);
                //Rect rect = actionFrame.getSize();
                //return rect;
            }
            return null;
        }

        #endregion
    }
}
