'''
Created on Apr 4, 2014

@author: jfren_000
'''
#!/usr/bin/env python

# Messages are json-packaged arrays of strings:
# There are two types: connect and message
# The username of "laura" indicates the bot or WoOz client
# {string type = "connect", string username, string lessonid}
# {string type = "message", string username, string targetuser, string message, [string emotion]}

import socket
import socketserver
import select
import json
from socket import error as SocketError
import errno

BUFFER_SIZE = 1024  # Normally 1024, but we want fast response

# The policy that is sent to the clients.
POLICY_DATA = """<?xml version="1.0"?><cross-domain-policy><allow-access-from domain="*" to-ports="*" /></cross-domain-policy>\0"""
# The string the client has to send in order to receive the policy.
POLICYREQUEST = "<policy-file-request/>"
# Set socket timeout to 1 min
socket.setdefaulttimeout(60)

# Create server socket
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# Bind to host and port
s.bind(('25.185.228.26', 5007))
# Set socket to be non-blocking
s.setblocking(0)
# Queue max 5 connection requests
s.listen(5)

clientuidmap = dict()
clientsockets = []

messages = []

def send_policy(sock):
    try:
        print("e")
       # s  = "<?xml version=\"1.0\"?><cross-domain-policy><allow-access-from domain=\"*\" to-ports=\"843\" /></cross-domain-policy>"
        sock.send(bytes(POLICY_DATA, 'UTF-8')) #(json.dumps(s).encode())
        print('policy sent to %s:%s' % sock.getpeername())
    except socket.error:
        print('can not send policy. socket error.')

        
def handle_read(s):
    msg = ""
    try:
        msg = s.recv(BUFFER_SIZE)
    except SocketError as e:
        if e.errno != errno.ECONNRESET:
            raise
        pass
        
    if len(msg) == 0:
        print ("a client socket has closed")
        # socket has disconnected
        s.close()
        clientsockets.remove(s)
    else:
        print (msg)
        if msg == b"<policy-file-request/>\x00":
            send_policy(s)
        else:
            o = json.loads(msg.decode())
            if type(o) is list:
                msgtype = o[0]
                if msgtype == "connect":
                    username = o[1]
                    clientuidmap[username] = s
                    messages.append(json.dumps(["message", "server", "laura", username + " has connected"]).encode())
                elif msgtype == "message":
                    messages.append(msg)

def handle_write(s):
    pass

def handle_connect(s):
    cs = s.accept()
    print ("New client")
    clientsockets.append(cs[0])
    print (clientsockets)

while 1:
    # Check sockets
    read, write, error = select.select([s]+clientsockets, clientsockets, [], 60)
    
    for sock in read:
        
        if sock is s:
            # New connection
            handle_connect(s)
        else:
            # New message
            handle_read(sock)
    # Write queued messages to client sockets
    for msg in messages:
        o = json.loads(msg.decode())
        sock = clientuidmap.get(o[2])
        if (sock in clientsockets):
            sock.send(msg)
    messages.clear()

s.close()
