# Tocalabs Interview Challenge

## Description
This is my submission for the Tocalabs interview challenge.  
The challenge aims to create an RDP-like server and remote-client, sending screen capture and receiving mouse and keyboard events.
The server and remote-client are split into two separate programs, with the remote-client written in C# using .Net, and the server written in Node.JS using express with WebSockets.
<br/>
[RDP Client](/TocalabsRDP)
<br/>
[RDP Server](/RDPServer)  

## What I found easy

* I found the screen capture to be quite easy to implement
* Code and object structure came easily after a while
* Networking was surprisingly easy
* Compression and optimization were relatively problem-free

## What I found difficult

* C# in the beginning was somewhat difficult to get started with, as I haven't used it in over 5 years, but became much easier as it clicked
* Threading had some problems. Whilst I knew what to do and how to conceptualize, some of the implementations gave me errors.
* Some of the system IO and windows functions were unfamiliar to me, and I couldn't find much information about turning this into a service that can start a user session. I will continue to work on this.