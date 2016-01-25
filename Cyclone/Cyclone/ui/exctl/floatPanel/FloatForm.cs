using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Cyclone.mod.exctl.floatPanel
{
    public partial class FloatForm:Form
    {
        public FloatForm()
        {
            this.InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000; //WS_EX_LAYERED
                return cp;
            }
        }
		
        public static void fSetBackground(System.Windows.Forms.Control control, Bitmap bitmap)
        {
            // Set our control's size to be the same as the bitmap
            control.Width = bitmap.Width;
            control.Height = bitmap.Height;

            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
            {
                throw new ApplicationException("must be 32 bits per pixel");
            }

            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;
            IntPtr screenDc = Win32.GetDC(IntPtr.Zero);
            IntPtr memDc = Win32.CreateCompatibleDC(screenDc);

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = Win32.SelectObject(memDc, hBitmap);
                Win32.Size size = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.Point pointSource = new Win32.Point(0, 0);
                Win32.Point topPos = new Win32.Point(control.Left, control.Top);
                Win32.BLENDFUNCTION blend = new Win32.BLENDFUNCTION();
                blend.BlendOp = 0;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = byte.MaxValue;
                blend.AlphaFormat = 1;
                Win32.UpdateLayeredWindow(control.Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Win32.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBitmap);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.DeleteDC(memDc);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TSPForm
            // 
            this.ClientSize = new System.Drawing.Size(197, 164);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TSPForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ResumeLayout(false);

        }
 
    }
    #region "API"

    public class Win32
    {
        public enum Bool : int
        {
            @False = 0,

            @True = 1

        }

        public struct Point
        {
            public int x;

            public int y;


            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public struct Size
        {
            public int cx;

            public int cy;


            public Size(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        public struct BLENDFUNCTION
        {
            public byte BlendOp;

            public byte BlendFlags;

            public byte SourceConstantAlpha;

            public byte AlphaFormat;

        }

        public const int ULW_ALPHA = 2;

        public const byte AC_SRC_OVER = 0;

        public const byte AC_SRC_ALPHA = 1;


        [DllImportAttribute("user32.dll")]
        public extern static Bool UpdateLayeredWindow(IntPtr handle, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);

        [DllImportAttribute("user32.dll")]
        public extern static IntPtr GetDC(IntPtr handle);

        [DllImportAttribute("user32.dll", ExactSpelling = true)]
        public extern static int ReleaseDC(IntPtr handle, IntPtr hDC);

        [DllImportAttribute("gdi32.dll")]
        public extern static IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImportAttribute("gdi32.dll")]
        public extern static Bool DeleteDC(IntPtr hdc);

        [DllImportAttribute("gdi32.dll")]
        public extern static IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImportAttribute("gdi32.dll")]
        public extern static Bool DeleteObject(IntPtr hObject);
    }

    #endregion



}
