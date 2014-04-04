using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputStream : MonoBehaviour {

	private Rect windowRect = new Rect((Screen.width/2)+150,50,Screen.width/3,Screen.height - 100);
	public string user = "";
	public string messBox = "", messageToSend = "";
	public GUISkin mySkin;

	private Vector2 scrollPos = new Vector2(0,0);

	// Use this for initialization
	void Start () {

		mySkin.textField.wordWrap = true;
		mySkin.textField.clipping = TextClipping.Clip;
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
			networkView.RPC("SendMessage",RPCMode.All,user+ ": "+messageToSend + "\n");
			messageToSend = "";
		}
		GUILayout.EndHorizontal();

		//users
		GUILayout.BeginHorizontal();
		GUILayout.Label("User");
		user = GUILayout.TextField(user);
		GUILayout.EndHorizontal();

		/*GUI.Label(new Rect(40,50,200,20),"Laura's Messages:");
		//
		// calculate full height of scrollview	
		float totalHeight = 0f;
		float logBoxWidth = 180.0f;
		float innerScrollHeight = 0.0f;
		float []logBoxHeights= new float[log.Count];
		int i = 0;
		
		float logBoxHeight = 0f;
		foreach(string st in log)		
		{
			float x = 10f;//mySkin.GetStyle("textfield").CalcHeight(GUIContent(st), logBoxWidth);
			logBoxHeight = Mathf.Min(maxLogLabelHeight,x);
			logBoxHeights[i++] = logBoxHeight;
			totalHeight += logBoxHeight+10.0f;

			innerScrollHeight = totalHeight;
		}
		scrollPos = new Vector2(0.0f, innerScrollHeight);
		lastLogLen = log.Count;
		scrollPos = GUI.BeginScrollView( Rect(0.0, Screen.height-150.0-50.0, 200, 150), scrollPos, Rect(0.0, 0.0, 180, innerScrollHeight));

		float currY = 0.0f;
		i = 0;

		foreach(string st in log)
		{
			logBoxHeight = logBoxHeights[i++];	
			GUI.Label(new Rect(10, currY, logBoxWidth, logBoxHeight),st);
			currY += logBoxHeight+10.0f;
		}
		
		GUI.EndScrollView();
		
	

		//laurasTxt = GUI.TextField(new Rect(50,80,250,100),laurasTxt,150,mySkin.GetStyle("textfield") );

		GUI.Label(new Rect(40,500,100,20),"Text to Laura:");
		inputTxt = GUI.TextField(new Rect(50,520,250,100),inputTxt,150,mySkin.GetStyle("textfield") );

		if(GUI.Button(new Rect(250,630,50,20),"Send"))
		{

		}*/
		GUI.DragWindow(new Rect(0,0,Screen.width,Screen.height));
	}

	/* message bw server and client*/
	[RPC]
	private void SendMessage(string mess)
	{
		messBox += mess;
	}
}
