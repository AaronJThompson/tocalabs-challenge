const express = require('express');
const router = express.Router();

router.use(express.json());

router.post('/key', async (req, res) => {
  try {
    if (rdpController) {
      rdpController.sendKey(req.body.keycode)
      res.status(201).json({message: "Key sent"});
    } else {
      throw Error("No RDPController");
    }
  } catch (error) {
    res.status(500).json({message: "Server error", error: error.message});
  }
})

router.post('/mouse_move', async (req, res) => {
  try {
    const { x, y } = req.body;
    if (rdpController) {
      rdpController.sendMouseMove(x, y);
      res.status(201).json({message: "Mouse move sent"});
    } else {
      throw Error("No RDPController");
    }
  } catch (error) {
    res.status(500).json({message: "Server error", error: error.message});
  }
})