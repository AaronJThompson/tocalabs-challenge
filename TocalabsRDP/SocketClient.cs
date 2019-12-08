using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace TocalabsRDP
{
    class SocketClient
    {
        private WebSocket wsClient;

        public SocketClient(string host)
        {
            wsClient = new WebSocket(host);
            wsClient.Connect();
        }

        public SocketClient()
        {
        }

        public WebSocket Connect(string host)
        {
            this.Close();
            wsClient = new WebSocket(host);
            wsClient.Connect();
            return wsClient;
        }

        public void SendImage(Bitmap img)
        {
            if(wsClient != null && wsClient.IsAlive)
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);
                wsClient.Send(ms.ToArray());
                ms.Close();
            }
        }

        public void Close()
        {
            if (wsClient != null && wsClient.IsAlive)
                wsClient.Close();
        }
    }
}
