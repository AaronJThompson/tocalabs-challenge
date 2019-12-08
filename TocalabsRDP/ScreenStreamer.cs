using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace TocalabsRDP
{
    class ScreenStreamer
    {
        private ScreenCapture screenCapture;
        private SocketClient socket;

        public ScreenStreamer(string host)
        {
            screenCapture = new ScreenCapture();
            socket = new SocketClient(host);
        }

        public void StartStream()
        {
            
        }
    }
}
