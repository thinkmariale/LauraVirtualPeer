'''
Created on May 4, 2014

@author: jfren_000
'''
#statements is an array of strings
#            indicates what laura should say for this state
#emotion is a string
#            indicates laura's emotion for this state
#response is a map from string to int
#            the keys are match strings for the user's response, and the value is the id of the next state
#replyneeded is a boolean string "true" or "false"
#            should be set to true if laura should wait for a reply
class state(object):
    __slots__ = ['statements', 'emotion','response', 'replyneeded']
    def __init__(self, statements, emotion, response, replyneeded):
        self.statements = statements
        self.emotion = emotion
        self.response = response
        self.replyneeded = replyneeded

state0 = state([""],
               "idle",
               {"__default__": 1},
               "true"
               )

state1 = state(["Hi"],
               "happy",
               {"__default__": 2},
               "true"
               )

state2 = state(["Thanks for helping me!"],
               "idle",
               {"__default__": 3},
               "true"
               )

state3 = state(["I got a letter from my credit card. They raised my interest rate and charged me a fee!"],
               "frustrated",
               {"__default__": 4},
               "false"
               )

state4 = state(["It's all because I was late last month. I couldn't pay it until after I got my paycheck. I should've had an emergency fund..."],
               "sad",
               {"__default__": 5},
               "true"
               )

state5 = state(["I should've learned more about credit before I started a credit card! I guess it's never too late to learn. Can you answer some questions for me?"],
               "idle",
               {"yes": 7, "no": 6,
                "__default__": 7},
               "true"
               )

state6 = state(["Okay. Well, you should still read the linked articles!"],
               "happy",
               {"__default__": 5},
               "true"
               )

state7 = state(["Then let's get started!"],
               "happy",
               {"__default__": 8},
               "false"
               )

state8 = state(["So, for starters, what is a credit score used for?"],
               "idle",
               {"loan": 9, "credit": 9, "card": 9, "lease": 9, "renting": 9,
                "rating": 10, "pay": 10, "debt": 10, "assessment": 10, "ability": 10, 
                "__default__": 10},
               "true"
               )

state9 = state(["But what does the number mean?"],
               "confused",
               {"__default__": 11},
               "true"
               )

state10 = state(["Who will want to see my credit score?"],
               "idle",
               {"__default__": 11},
               "true"
               )

state11 = state(["Okay, I think it makes sense, thanks!"],
               "happy",
               {"__default__": 12},
               "true"
               )

state12 = state(["So what's good about having a good credit score?"],
               "idle",
               {"rent": 13, "lease": 13, "property": 13, "apartment":13,
                "rate": 14, "car": 14, "loan": 14, "interest": 14,
                "__default__": 15},
               "true"
               )

state13 = state(["I might want to rent a new apartment soon! That's important!"],
               "happy",
               {"__default__": 15},
               "true"
               )

state14 = state(["I might need a car loan soon. That's important!"],
               "happy",
               {"__default__": 15},
               "true"
               )

state15 = state(["Alright, my credit score is 589. Is that good or bad?", "good or bad?"],
               "idle",
               {"good": 17,
                "bad": 16,
                "__default__": 16},
               "true"
               )

state16 = state(["Augh! That's bad!"],
               "surprised",
               {"__default__": 18},
               "true"
               )

state17 = state(["Are you sure? Isn't it below average?"],
               "idle",
               {"__default__": 15},
               "false"
               )

state18 = state(["What do I do to improve my credit score?", "What else can I do to improve my score?", "anything else?"],
               "idle",
               {"open": 19, "new": 19, "account": 19,
                "on time": 20, "budget": 20, "payment history": 20,
                "pay": 21, "off": 21, "down": 21, "credit": 21, "utilization": 21,
                "__default__": 22},
               "true"
               )

state19 = state(["I already have $1000 in credit card debt. Is starting a new credit card a good idea?"],
               "surpirsed",
               {"__default__": 18},
               "true"
               )

state20 = state(["That's a good idea! I'll try to budget so that I don't miss any more payments!"],
               "happy",
               {"__default__": 18},
               "false"
               )

state21 = state(["That's a good idea! I'll try to continue paying down my credit card and student loans!"],
               "happy",
               {"__default__": 18},
               "false"
               )

state22 = state(["Oh, also, what can lower my credit score?", "what else should I avoid?", "anything else?"],
               "idle",
               {"missed": 23, "late": 23, "payment": 23, "history": 23,
                "balance": 24, "utilization": 24, "limit": 24,
                "bankrupt": 25, "bankrupcy": 25,
                "__default__": 26},
               "true"
               )

state23 = state(["I'll definitely try not to miss anymore payments!"],
               "happy",
               {"__default__": 22},
               "false"
               )

state24 = state(["Good point. I'll pay my credit card and student loans down before I take on any new debt!"],
               "happy",
               {"__default__": 22},
               "false"
               )

state25 = state(["I'll try to avoid going bankrupt!"],
               "surprised",
               {"__default__": 22},
               "false"
               )

state26 = state(["There are some terms I don't understand. What is a hard inquiry?"],
               "idle",
               {"__default__": 27},
               "true"
               )

state27 = state(["Is it a hard inquiry when I check my credit score?"],
               "idle",
               {"__default__": 28},
               "true"
               )

state28 = state(["I think I get it!"],
               "happy",
               {"__default__": 29},
               "true"
               )

state29 = state(["What is credit utilization?"],
               "idle",
               {"__default__": 30},
               "true"
               )

state30 = state(["Okay, so I should stay away from my credit card maximum?", ""],
               "idle",
               {"__default__": 31},
               "true"
               )

state31 = state(["Are you sure? I think that could lower my credit score"],
               "idle",
               {"__default__": 32},
               "true"
               )

state32 = state(["Alright I think I get it!"],
               "happy",
               {"__default__": 33},
               "true"
               )

state33 = state(["So in summary, I should maintain a good payment history by paying my loans and credit card on time. I should keep my credit card balance low, so that I have low credit utilization, which will increase my score. This will help me get better credit offers and interest rates, as well as make it easier to rent property."],
               "happy",
               {"__default__": 34},
               "true"
               )

state34 = state(["Wow I learned a lot from this! Thanks!"],
               "happy",
               {"__default__": 34},
               "true"
               )

states = [state0, state1, state2, state3, state4, state5, state6, state7, state8,
          state9, state10, state11, state12, state13, state14, state15,
          state16, state17, state18, state19, state20, state21, state22,
          state23, state24, state25, state26, state27, state28, state29,
          state30, state31, state32, state33, state34]

def get_next_state(currstate, usermsg):
    # universal rules for short messages
    if (len(usermsg.split()) <= 3):
        if ((usermsg.count("...") > 0) or
            (usermsg.count("um") > 0) or
            (usermsg.count("hold") > 0) or
            (usermsg.count("sec") > 0) or
            (usermsg.count("uh") > 0) or
            (usermsg.count("idk") > 0) or
            (usermsg.count("don't know") > 0) or
            (usermsg.count("kk") > 0)
            ):
            return -1
            
    #apply rules for current state
    for word in usermsg.split():
        if (word in states[currstate].response):
            return states[currstate].response[word]
    return states[currstate].response["__default__"]