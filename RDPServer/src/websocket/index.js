const WebSocket = require('ws');

class RDPController {
  constructor(server) {
    this.clients = [];
    this.server = server;
  }

  init() {

  }

  onConnection(ws) {

  }

  onMessage(m) {

  }

  sendKey(k) {

  }

  sendMouseMove(x, y) {
    
  }
}

function CreateServer(server) {
  const wss = new WebSocket.Server({ server });
  return new RDPController(wss);
}