using System;
using System.Collections.Generic;
using System.Text;

namespace Cyclone.alg.opengl
{
    class FPS
    {
        private static DateTime timeMarkCurrent = DateTime.Now;
        private static DateTime timeMarkLast;
        private static long oneSecond = 1000;
        public static TimeSpan timePassed;
        private int fpsT = 0;
        private int fps = 0;
        public void fpsLogic()
        {
            timeMarkCurrent = DateTime.Now;
            timePassed = timeMarkCurrent - timeMarkLast;
            timeMarkLast = timeMarkCurrent;
            //Console.WriteLine("timePassed:" + timePassed);
            oneSecond -= timePassed.Seconds*1000 + timePassed.Milliseconds;
            if (oneSecond < 0)
            {
                oneSecond += 1000;
                if (oneSecond < 0)
                {
                    oneSecond = 1000;
                }
                fps = fpsT;
                fpsT = 0;
                //Console.WriteLine("fps:" + fps);
            }
            else
            {
                fpsT++;
            }

            //Console.WriteLine("oneSecond:" + oneSecond);
        }
        private int oldFPS = 0;
        public bool checkFPS(bool print)
        {
            if (fps != oldFPS)
            {
                oldFPS = fps;
                if (print)
                {
                    Console.WriteLine("fps:" + fps);
                }
                return true;
            }
            return false;
        }
        public int getFPS()
        {
            return fps;
        }
    }
}
