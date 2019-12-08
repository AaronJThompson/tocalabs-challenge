using System;
using System.Collections.Generic;
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
        }
    }
}
