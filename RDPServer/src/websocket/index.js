const WebSocket = require('ws');

class RDPController {
  constructor(server) {
    this.clients = [];
    this.server = server;
  }

}

function CreateServer(server) {
  const wss = new WebSocket.Server({ port: 8080 });
  return new RDPController(wss);
}