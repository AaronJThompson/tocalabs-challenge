# Tocalabs Interview Challenge - Server

This is the server backend for the Tocalabs Interview challenge.
Created using Node.JS, Express and Websockets

## Install and run

Install: `npm install`
Run: `npm start`

## Endpoints

POST: `/input/mouse_move`
<br />
Body:
<br />
```
{
  "x": int, (Amount to move mouse along x-axis)
  "y": int (Amount to move mouse along y-axis)
}
```
<br />
POST: `/input/key`
<br />
Body:
<br />
```
{
  "keycode": int (Keycode to press)
}
```

## Note

This runs by default on port 8080 but can be changed using the `PORT` enviroment variable