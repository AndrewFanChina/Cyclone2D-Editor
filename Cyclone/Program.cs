using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Cyclone.alg;
using Cyclone.alg.util;

namespace Cyclone
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            //===================================================
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //====路径初始化=====================================
            if (!Consts.initPaths(args))
            {
                return;
            }
            //====启动===========================================
            Application.Run(new Form_Main(Consts.PATH_PROJECT_FILEPATH));
            //===================================================


        }
    }
}