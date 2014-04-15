using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class InputStream : MonoBehaviour {

	private Rect windowRect = new Rect((Screen.width/2)+150,50,Screen.width/3,Screen.height - 300);
	public delegate void recvMessagesDelegate();
	public recvMessagesDelegate recvDelegate;
	public string user = "";
	public string messBox = "", messageToSend = "";
	public string emotion = "happy";
	public GUISkin mySkin;

	private Vector2 scrollPos = new Vector2(0,0);

	// Use this for initialization
	void Start () {

		mySkin.textField.wordWrap = true;
		mySkin.textField.clipping = TextClipping.Clip;
		recvDelegate = new recvMessagesDelegate(this.recvMessages);
	}
	
	
	void OnGUI()
	{
		GUI.skin = mySkin;
		//if (NetworkPeerType.Disconnected != Network.peerType)
			windowRect = GUI.Window(1, windowRect, InputStreamPlayer, "Chat");

	}

	void InputStreamPlayer(int id)
	{
		GUILayout.Box(messBox,GUILayout.Height(350));

		GUILayout.BeginHorizontal();
		messageToSend = GUILayout.TextField(messageToSend);
		if(GUILayout.Button("Send",GUILayout.Width(75)))
		{
			if (WoOzChatLayer.isConnected())
				WoOzChatLayer.sendMessage(messageToSend);
				messBox += WoOzChatLayer.userName + ": " + messageToSend + "\n";

				messageToSend = "";
		}
		GUILayout.EndHorizontal();
		
		// start a listen thread, calls recvMessages
		recvDelegate.BeginInvoke(null, null);
		
		GUI.DragWindow(new Rect(0,0,Screen.width,Screen.height));
	}

	private void recvMessages ()
	{
		int i = 0;
		while (true) {
			if (WoOzChatLayer.isConnected())
			{
				// blocks until next message is recieved
				string mess = WoOzChatLayer.getMessage(); 
				messBox += mess + "\n";
				emotion = WoOzChatLayer.getEmotion();
			}
			else {
				Thread.Sleep(100);
				i++;
				if (i > 100) {
					return;
				}
			}
		}
	}
	
	/* message bw server and client*/
	[RPC]
	private void SendMessage(string mess)
	{
		messBox += mess;
	}
}
