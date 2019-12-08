const WebSocket = require('ws');

class RDPController {
  constructor(server) {
    this.server = server;
  }

  init() {
    this.server.on('connection', this.onConnection.bind(this));
  }

  onConnection(ws) {
    ws.on('message', this.onMessage.bind(this));
  }

  onMessage(m) {
    console.log(m);
  }

  sendKey(k) {
    this.sendToClients({
      EventType: "key",
      KeyCode: k
    })
  }

  sendToClients(data) {
    this.server.clients.forEach((client, idx) => {
      if(client.readyState === WebSocket.OPEN) {
        client.send(JSON.stringify(data));
      }
    })
  }

  sendMouseMove(x, y) {
    this.sendToClients({
      EventType: "mouse",
      DeltaX: x,
      DeltaY: y
    })
  }
}

function CreateServer(server) {
  const wss = new WebSocket.Server({ server });
  return new RDPController(wss);
}