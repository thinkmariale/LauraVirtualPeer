'''
Created on May 1, 2014

@author: jfren_000
'''

import socket
import threading
import json
import time
import VPResponses

targetuser = "Me"
emotion = "idle"
stopped = "false"

def print_help():
    print ("\nAvailiable commands:")
    print ("/h or /help: display help menu")
    print ("/t [username] or /target [username]: sends chat input to that user")
    print ("/s or /stop or /start: respectively toggles, stops, or starts the bot's responses")
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
            elif (command == "stop"):
                stopped = "true"
            elif (command == "start"):
                stopped = "false"
            elif (command == "s"):
                stopped = (not stopped)
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


currstate = 0

def respond(s, usermsg):
    global currstate
    global emotion
    if ("stopped" == "true"):
        return

    nextstate = VPResponses.get_next_state(currstate, usermsg)
    if (nextstate == -1):
        # if get_next_state returns -1 then we stay in the same state and don't respond
        return

    #simulate typing
    s.send(json.dumps(["message", "laura", targetuser, "", "idletyping"]).encode())
    currstate = nextstate
    time.sleep(3)
    
    #send response message
    print (currstate)
    newmsg = VPResponses.states[currstate].statements[0]
    emotion = VPResponses.states[currstate].emotion
    s.send(json.dumps(["message", "laura", targetuser, newmsg, emotion]).encode())
    
    #if state does not require a response, traverse the tree to the next state
    while (VPResponses.states[currstate].replyneeded == "false"):
        nextstate = VPResponses.get_next_state(currstate, "")
        s.send(json.dumps(["message", "laura", targetuser, "", "idletyping"]).encode())
        currstate = nextstate
        time.sleep(2)
        newmsg = VPResponses.states[currstate].statements[0]
        emotion = VPResponses.states[currstate].emotion
        s.send(json.dumps(["message", "laura", targetuser, newmsg, emotion]).encode())

def read_output_respond(s):
    while 1:
        msg = s.recv(BUFFER_SIZE)
        if len(msg) == 0:
            print("disconnected from server")
            return
        else:
            o = json.loads(msg.decode())
            print(o[1] + ": " + o[3])
            if (o[1] == targetuser):
                respond(s, o[3])


TCP_IP = '25.185.230.14'#'25.185.228.26'
TCP_PORT = 5007
BUFFER_SIZE = 1024

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((TCP_IP, TCP_PORT))

readthr = threading.Thread(target=read_output_respond, args={(s)})
readthr.start()

s.send(json.dumps(["connect", "laura", "lesson1"]).encode())

while 1:
    if (readthr.is_alive() == False):
        print("disconnected from server")
        break
    msg = input()
    if msg == "end" or msg == "/end":
        break
    msg = parse_input(msg)
    s.send(json.dumps(["message", "laura", targetuser, msg, emotion]).encode())

readthr.join(10)
s.close()
