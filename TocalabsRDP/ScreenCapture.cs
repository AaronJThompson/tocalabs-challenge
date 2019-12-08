using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace TocalabsRDP
{
    class ScreenCapture
    {
        private Rectangle resolution;

        public ScreenCapture()
        {
            this.resolution = Screen.PrimaryScreen.Bounds;
        }

        public Bitmap Capture()
        {
            Bitmap capture = new Bitmap(this.resolution.Width, this.resolution.Height, PixelFormat.Format32bppArgb);
            Graphics captureGraphics = Graphics.FromImage(capture);
            captureGraphics.CopyFromScreen(this.resolution.Left, this.resolution.Top, 0, 0, this.resolution.Size);

            return capture;
        }
    }
}
