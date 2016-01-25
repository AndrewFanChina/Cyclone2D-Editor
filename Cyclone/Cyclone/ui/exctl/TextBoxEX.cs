using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Cyclone.Cyclone.ui.exctl
{
        public class TextBoxEX : System.Windows.Forms.TextBox
        {
            public TextBoxEX()
            {
            }
            protected override void WndProc(ref   Message m)
            {
                if (m.Msg != 0x007B)
                {
                    base.WndProc(ref   m);
                }
            }
        }
}
