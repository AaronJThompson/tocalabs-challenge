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
            // Intialize a websocket connection
            wsClient = new WebSocket(host);
            wsClient.Compression = CompressionMethod.Deflate;
            wsClient.Connect();
            wsClient.OnMessage += this.onMessage;
            return wsClient;
        }

        public void SendImage(Bitmap img)
        {
            if(wsClient != null && wsClient.IsAlive)
            {
                // output our bitmap to a memory stream of a jpeg so we can send it as a byte array
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                wsClient.Send(ms.ToArray());
                // Cleanup the memory stream
                ms.Dispose();
                ms.Close();
            }
        }

        public void SendImageAsync(Bitmap img, Action<bool> act)
        {
            //Same as SendImage, but we send it asynchronously and perform a callback action when sent.
            if (wsClient != null && wsClient.IsAlive)
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                wsClient.SendAsync(ms.ToArray(), act);
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
            //Assume all messages are input events and deserialize the JSON into a InputEvent object
            InputEvent ie = JsonConvert.DeserializeObject<InputEvent>(e.Data);
            //Run the converted input event 
            inputControl.RunEvent(ie);
        }
    }
}
