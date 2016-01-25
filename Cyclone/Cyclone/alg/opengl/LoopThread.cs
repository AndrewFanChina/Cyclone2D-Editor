using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Cyclone.alg.opengl
{
    class LoopThread
    {
        //############################### ���̲߳��� #########################################
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
        private Thread playThread = null;//�����߳�
        private bool beShown = false;
        private delegate void updateBox_PreviewBoxSafe();
        //����ѭ�߳�
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
        //ֹͣ�߳�
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
        //ѭ��ִ��
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
