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
        private Bitmap lastCapture;
        private bool running = false;

        public ScreenStreamer(string host)
        {
            screenCapture = new ScreenCapture();
            this.host = host;
            running = false;
        }

        public void StartStream()
        {
            // This is a synchronous version (Not used in main application). It waits for screen capture and then sends synchronously upon completion
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

        public void StartAsyncStream()
        {
            socket = new SocketClient(host);
            running = true;
            // Capture our initial image to start the loop
            lastCapture = screenCapture.Capture();
            Action<bool> action = this.NextSend;
            socket.SendImageAsync(lastCapture, action);
        }

        private void NextSend(bool success)
        {
            if (running)
            {
                Action<bool> action = this.NextSend;
                // Send the last image asynchronously and capture another whilst it sends 
                socket.SendImageAsync(lastCapture, action);
                lastCapture = screenCapture.Capture();
            }
            else
            {
                socket.Close();
                socket = null;
                Debug.WriteLine("Closing stream thread gracefully");
            }
        }

        public Thread Toggle()
        {
            if(thread == null)
            {
                Debug.WriteLine("Starting stream");
                // Create a new thread for the screen capture and sending to start in
                thread = new Thread(StartAsyncStream);
                thread.Start();
                return thread;
            }
            else
            {
                this.Close();
                return null;
            }
        }

        public void Close()
        {
            if (thread != null)
            {
                running = false;
                // Join thread, with 0.5s timeout
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
