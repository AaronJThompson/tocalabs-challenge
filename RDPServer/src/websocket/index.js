const WebSocket = require('ws');
const fs = require('fs');

class RDPController {
  constructor(server) {
    this.server = server;
    this.init();
    this.frames = 0;
    this.lastImage = null;
  }

  init() {
    this.server.on('connection', this.onConnection.bind(this));
    setInterval(this.outputFPS.bind(this), 1000);
    setInterval(() => {
      console.log("Saving last capture...");
      fs.writeFileSync(__dirname + "../../../last_capture.jpg", this.lastImage);
    }, 5000)
  }

  outputFPS() {
    if (this.frames > 0) {
      console.log("FPS:", this.frames);
      this.frames = 0;
    }
  }

  onConnection(ws) {
    ws.on('message', this.onMessage.bind(this));
  }

  onMessage(m) {
    this.frames += 1;
    this.lastImage = m;
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