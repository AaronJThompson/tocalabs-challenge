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
            this.wsClient = new WebSocket(host);
            this.wsClient.Connect();
        }

        public void SendImage(Bitmap img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            this.wsClient.Send(ms.ToArray());
            ms.Close();
        }
    }
}
