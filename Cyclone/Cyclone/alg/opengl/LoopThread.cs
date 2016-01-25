using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Cyclone.alg.opengl
{
    class LoopThread
    {
        //############################### 多线程播放 #########################################
        /// <summary>
        /// thrOpenGL thread start
        /// </summary>
        private LoopThreadHoder holder = null;
        private Control uiControl;
        private int threadSleep = 0;
        public LoopThread( Control uiControl, LoopThreadHoder holder,int sleepTime)
        {
            this.uiControl = uiControl;
            this.holder = holder;
            threadSleep = sleepTime;
        }
        private void threadLoop()
        {
            holder.doThreadThings();
        }
        private Thread playThread = null;//播放线程
        private bool beShown = false;
        private delegate void updateBox_PreviewBoxSafe();
        //启动循线程
        public void startThread()
        {
            if (playThread != null)
            {
                playThread.Abort();
            }
            playThread = new Thread(playLoop);
            beShown = true;
            playThread.Start();
        }
        //停止线程
        public void stopThread()
        {
            if (playThread != null)
            {
                playThread.Abort();
            }
            playThread = null;
            beShown = false;
        }
        updateBox_PreviewBoxSafe updateDelegate = null;
        //循环执行
        private void playLoop()
        {
            if (updateDelegate == null)
            {
                updateDelegate = new updateBox_PreviewBoxSafe(threadLoop);
            }
            while (beShown)
            {
                if (uiControl.InvokeRequired)
                {
                    uiControl.Invoke(updateDelegate);
                }
                else
                {
                    threadLoop();
                }
                if (threadSleep > 0)
                {
                    Thread.Sleep(threadSleep);
                }
            }
        }
    }
    public interface LoopThreadHoder
    {
        void doThreadThings();
    }
}
