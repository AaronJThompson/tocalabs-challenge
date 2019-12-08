const express = require('express');
const http = require('http');
const inputRouter = require('./routes/input');
const {CreateWSServer, RDPController } = require('./websocket');

const app = express();

app.use('/input', inputRouter);

const server = http.createServer(app);

rdpController = CreateWSServer(server);

server.listen(process.env.PORT || 8080, () => {
  console.log(`Server started on port ${server.address().port}`);
})