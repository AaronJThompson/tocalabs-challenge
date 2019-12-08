const WebSocket = require('ws');

class RDPController {
  constructor(server) {
    this.server = server;
    this.init();
    this.frames = 0;
  }

  init() {
    this.server.on('connection', this.onConnection.bind(this));
    setInterval(this.outputFPS.bind(this), 1000);
  }

  outputFPS() {
    console.log("FPS:", this.frames);
    this.frames = 0;
  }

  onConnection(ws) {
    ws.on('message', this.onMessage.bind(this));
  }

  onMessage(m) {
    this.frames += 1;
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

function CreateWSServer(server) {
  const wss = new WebSocket.Server({ server });
  return new RDPController(wss);
}

module.exports = {
  RDPController,
  CreateWSServer
}