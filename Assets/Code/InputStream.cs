using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class InputStream : MonoBehaviour {

	private Rect windowRect = new Rect(200,50,200,300);
	public string user = "";
	public string messBox = "", messageToSend = "";
	public string emotion = "happy";
	public GUISkin mySkin;

	private Vector2 scrollPos;

	// Use this for initialization
	void Start () {
		scrollPos = new Vector2(scrollPos.x, Mathf.Infinity);
		mySkin.textField.wordWrap = true;
		mySkin.textField.clipping = TextClipping.Clip;
	}


	void OnGUI()
	{
		GUI.skin = mySkin;
		//if (NetworkPeerType.Disconnected != Network.peerType)
			windowRect = GUI.Window(1, windowRect, InputStreamPlayer, "Chat Box");

	}

	void InputStreamPlayer(int id)
	{
		//scoll bar
		scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width (180), GUILayout.Height (220));
		GUILayout.Box(messBox);
		GUILayout.EndScrollView();

		GUILayout.BeginHorizontal();
		messageToSend = GUILayout.TextField(messageToSend);
		if(GUILayout.Button("Send",GUILayout.Width(75)))
		{
			if (WoOzChatLayer.isConnected()) {
				WoOzChatLayer.sendMessage(messageToSend);
				messBox += WoOzChatLayer.userName + ": " + messageToSend + "\n";

				messageToSend = "";
			} else {
				messBox += WoOzChatLayer.userName + ": " + "not connected to a server" + "\n";
			}
		}
		GUILayout.EndHorizontal();
	
		if (WoOzChatLayer.isConnected())
			{
				//should not block
				string mess = WoOzChatLayer.getMessage(); 
				if (mess != "")
					messBox += mess + "\n";
				emotion = WoOzChatLayer.getEmotion();
			}

		GUI.DragWindow(new Rect(0,0,Screen.width,Screen.height));
	}

	/* message bw server and client*/
	[RPC]
	private void SendMessage(string mess)
	{
		messBox += mess;
	}
}