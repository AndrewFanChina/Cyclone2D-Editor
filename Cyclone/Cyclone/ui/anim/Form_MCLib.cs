using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DockingUI.WinFormsUI.Docking;
using Cyclone.mod.anim;

namespace Cyclone.mod.anim
{
    public partial class Form_MCLib : DockContent, MParentNode
    {
        public Form_MCLib()
        {
            InitializeComponent();
        }
        protected override string GetPersistString()
        {
            return "Form_MCLib";
        }
        #region MParentNode 成员

        public int GetSonID(MSonNode son)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region MIO 成员

        public void readObject(System.IO.Stream s)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void writeObject(System.IO.Stream s)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void exportObject(System.IO.Stream s)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region MParentNode 成员


        public MParentNode GetTopParent()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}