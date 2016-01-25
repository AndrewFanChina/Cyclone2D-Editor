using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Cyclone.mod.exctl.floatPanel
{
    public partial class FloatControl : UserControl
    {
        public FloatControl()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor
              | ControlStyles.UserPaint
              | ControlStyles.AllPaintingInWmPaint
              | ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
        }

        private Image img;
        public Image Image
        {
            get
            {
                return img;
            }
            set
            {
                img = value;
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            this.Refresh();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                //return base.CreateParams;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT 
                return cp;
            }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (img != null)
            {
                //base.OnPaint(pe);
                pe.Graphics.Clear(Color.FromArgb(0,Color.Transparent));
                pe.Graphics.DrawImage(img, 0, 0);
            }
            else
            {

            }
        }
    }


}
