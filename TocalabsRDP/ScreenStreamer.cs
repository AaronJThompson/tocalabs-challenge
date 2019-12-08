using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing.Imaging;
using System.Drawing;

namespace TocalabsRDP
{
    class ScreenStreamer
    {
        private ScreenCapture screenCapture;
        private SocketClient socket;
        private Thread thread;
        private bool running = false;

        public ScreenStreamer(string host)
        {
            screenCapture = new ScreenCapture();
            socket = new SocketClient(host);
            running = false;
        }

        public void StartStream()
        {
            running = true;
            while(running)
            {
                Bitmap cap = screenCapture.Capture();
                socket.SendImage(cap);
            }
            Console.WriteLine("Closing stream gracefully");
        }

        public void Toggle()
        {
            if(thread != null)
            {
                thread = new Thread(StartStream);
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                running = false;
                if (thread.Join(200) == false)
                {
                    thread.Abort();
                }
                thread = null;
            }
        }
    }
}
