const express = require('express');
const router = express.Router();

router.post('/key', async (req, res) => {
  try {
    if (rdpController) {
      rdpController.sendKey(req.keycode)
    }
    res.status(201).json({message: "Key sent"});
  } catch (error) {
    res.status(500).json({message: "Server error"});
  }
})