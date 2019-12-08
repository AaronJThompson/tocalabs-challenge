using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebSocketSharp;

namespace TocalabsRDP
{
    class SocketClient
    {
        private WebSocket wsClient;
        private InputControl inputControl;

        public SocketClient(string host)
        {
            wsClient = this.Connect(host);
            inputControl = new InputControl();
        }

        public SocketClient()
        {
            inputControl = new InputControl();
        }

        public WebSocket Connect(string host)
        {
            this.Close();
            wsClient = new WebSocket(host);
            wsClient.Connect();
            wsClient.OnMessage += this.onMessage;
            return wsClient;
        }

        public void SendImage(Bitmap img)
        {
            if(wsClient != null && wsClient.IsAlive)
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);
                wsClient.Send(ms.ToArray());
                ms.Dispose();
                ms.Close();
            }
        }

        public void Close()
        {
            if (wsClient != null && wsClient.IsAlive)
                wsClient.Close();
        }

        private void onMessage(object sender, WebSocketSharp.MessageEventArgs e)
        {
            InputEvent ie = JsonConvert.DeserializeObject<InputEvent>(e.Data);
            inputControl.RunEvent(ie);
        }
    }
}
