using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing.Imaging;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace TocalabsRDP
{
    class ScreenStreamer
    {
        private ScreenCapture screenCapture;
        private SocketClient socket;
        private string host;
        private Thread thread;
        private Bitmap lastCapture;
        private bool screen_changed = true;
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
                Bitmap temp_last = lastCapture;
                Action<bool> action = this.NextSend;
                if (screen_changed)
                {
                    // Send the last image asynchronously and capture another whilst it sends
                    socket.SendImageAsync(lastCapture, action);
                }
                lastCapture = screenCapture.Capture();

            }
            else
            {
                socket.Close();
                socket = null;
                Debug.WriteLine("Closing stream thread gracefully");
            }
        }

        private bool CompareBitmaps(Bitmap imgA, Bitmap imgB)
        {
            byte[] imgA_bytes;
            byte[] imgB_bytes;

            using (var ms = new MemoryStream())
            {
                imgA.Save(ms, imgA.RawFormat);
                imgA_bytes = ms.ToArray();
            }

            using (var ms = new MemoryStream())
            {
                imgB.Save(ms, imgB.RawFormat);
                imgB_bytes = ms.ToArray();
            }

            String imgA64 = Convert.ToBase64String(imgA_bytes);
            String imgB64 = Convert.ToBase64String(imgB_bytes);

            return string.Equals(imgA64, imgB64);
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
