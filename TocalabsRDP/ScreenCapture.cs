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

        }
    }
}
