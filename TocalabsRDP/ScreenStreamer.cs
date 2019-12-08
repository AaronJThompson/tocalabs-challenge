using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing.Imaging;
using System.Drawing;
using System.Diagnostics;

namespace TocalabsRDP
{
    class ScreenStreamer
    {
        private ScreenCapture screenCapture;
        private SocketClient socket;
        private string host;
        private Thread thread;
        private bool running = false;

        public ScreenStreamer(string host)
        {
            screenCapture = new ScreenCapture();
            this.host = host;
            running = false;
        }

        public void StartStream()
        {
            socket = new SocketClient(host);
            running = true;
            while(running)
            {
                Bitmap cap = screenCapture.Capture();
                socket.SendImage(cap);
            }
            socket.Close();
            socket = null;
            Debug.WriteLine("Closing stream thread gracefully");
        }

        public void Toggle()
        {
            if(thread != null)
            {
                Debug.WriteLine("Starting stream");
                thread = new Thread(StartStream);
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                running = false;
                if (thread.Join(500) == false)
                {
                    thread.Abort();
                    Debug.WriteLine("Closing stream thread forcefully");
                }
                thread = null;
            }
        }
    }
}
