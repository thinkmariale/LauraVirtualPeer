using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class InputStream : MonoBehaviour {

	private Rect windowRect = new Rect(480,50,250,330);
	public string user = "";
	public string messBox = "", messageToSend = "";
	public string emotion = "happy";
	public GUISkin mySkin;

	private Vector2 scrollPos;
	private Vector2 scrollPos1;
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
	void posEvent(int index){
		// Force the scrollbar to the bottom position.
		if(index == 0)
			scrollPos.y = Mathf.Infinity;
		else
			scrollPos1.y = Mathf.Infinity;
	}
	void InputStreamPlayer(int id)
	{
		//scoll bar
		scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width (230), GUILayout.Height (220));
		GUILayout.Box(messBox);
		GUILayout.EndScrollView();

		//GUILayout.BeginHorizontal();
		scrollPos1 = GUILayout.BeginScrollView(scrollPos1, GUILayout.Width (230), GUILayout.Height (50));
		messageToSend = GUILayout.TextField(messageToSend);
		GUILayout.EndScrollView();

		if (!Input.GetMouseButton (0)) 
		{
			if(GUI.skin.textField.CalcHeight (new GUIContent (messBox), 180) > 220)
				posEvent(0);
			if(GUI.skin.textField.CalcHeight (new GUIContent (messageToSend), 180) > 50)
				posEvent(1);
		}

				
		bool enterP = false;
		if (Event.current.type == EventType.keyDown){//if(e == Event.KeyboardEvent(KeyCode.Return.ToString())){//(e.type == EventType.KeyDown && e.keyCode == KeyCode.Return){
			print (Event.current.keyCode);
			if(Event.current.keyCode == KeyCode.None)
				enterP = true;

		}

		if(GUILayout.Button("Send",GUILayout.Width(75)) || enterP)
		{
			if (WoOzChatLayer.isConnected()) {
				WoOzChatLayer.sendMessage(messageToSend);
				messBox += WoOzChatLayer.userName + ": " + messageToSend + "\n";

				messageToSend = "";
			} else {
				messBox += WoOzChatLayer.userName + ": " + "not connected to a server" + "\n";
			}
		}

	//	GUILayout.EndHorizontal();

		sendMessage();

		//GUI.DragWindow(new Rect(0,0,Screen.width,Screen.height));
	}

	void sendMessage()
	{
		if (WoOzChatLayer.isConnected())
		{
			//should not block
			string mess = WoOzChatLayer.getMessage(); 
			if (mess != "")
				messBox += mess + "\n";
			emotion = WoOzChatLayer.getEmotion();
		}

	}
	/* message bw server and client*/
	[RPC]
	private void SendMessage(string mess)
	{
		messBox += mess;
	}
}