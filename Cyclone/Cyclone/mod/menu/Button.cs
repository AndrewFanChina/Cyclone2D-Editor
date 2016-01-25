using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Cyclone.mod.menu
{
    class Button : Module
    {
        MovieClip []movieClips=new MovieClip[3];//δѡ��״̬��ѡ��״̬������״̬
        #region Module ��Ա
        public void display(Graphics g, int x, int y, int zoomLevel, int stateID)
        {
            //...go on
        }

        public int getWidth()
        {
            if (movieClips[0] != null)
            {
                return movieClips[0].getWidth();
            }
            return -1;
        }

        public int getHeight()
        {
            if (movieClips[0] != null)
            {
                return movieClips[0].getHeight();
            }
            return -1;
        }

        #endregion
    }
}
