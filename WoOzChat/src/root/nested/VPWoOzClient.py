'''
Created on Apr 4, 2014

@author: jfren_000
'''
#!/usr/bin/env python

import socket
import threading
import json

targetuser = "laura"
emotion = "idle"

def print_help():
    print ("\nAvailiable commands:")
    print ("/h or /help: display help menu")
    print ("/t [username] or /target [username]: sends chat input to that user")
    print ("/[]: sets emotion to []")
    print ("end or /end (no other message): closes the chat connection\n")
    return

def parse_input(str):
    msg = ""
    global targetuser
    global emotion
    setTarget = False
    for word in str.split():
        if setTarget == True:
            targetuser = word
            print ("Target set to " + targetuser)
            setTarget = False
        elif (word.startswith('/')):
            command = word[1:]
            if (command == "help" or command == "h"):
                print_help()
            elif (command == "target" or command == "t"):
                setTarget = True
            else:
                emotion = command
                print ("Emotion set to " + emotion)
        else:
            msg += word
            msg += " "
    msg = msg.rstrip() #remove trailing whitespace
    
    if (msg == ""):
        return " ";
    return msg

def read_output(s):
    while 1:
        msg = s.recv(BUFFER_SIZE)
        if len(msg) == 0:
            print("disconnected from server")
            return
        else:
            o = json.loads(msg.decode())
            print(o[1] + ": " + o[3])

TCP_IP = '127.0.0.1' #'25.185.230.14' #'127.0.0.1'
TCP_PORT = 5007
BUFFER_SIZE = 1024

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((TCP_IP, TCP_PORT))

readthr = threading.Thread(target=read_output, args={(s)})
readthr.start()

print("Enter Username: ")
username = input()
s.send(json.dumps(["connect", username, "lesson1"]).encode())

while 1:
    if (readthr.is_alive() == False):
        break
    msg = input()
    if msg == "end" or msg == "/end":
        break
    msg = parse_input(msg)
    s.send(json.dumps(["message", username, targetuser, msg, emotion]).encode())
        

readthr.join(10)
s.close()
