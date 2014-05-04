using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
//using WoOzChatLayer;

public class ConnectionScript : MonoBehaviour {

	private System.Net.Sockets.TcpListener chatServer;
	private string serverIP = "127.0.0.1";
	private string port = "5007";
	private string userName = "Me";
	private int iport;
	private Rect windowRect = new Rect(0,0,150,100);
	public bool isConnected = false;
	public GUISkin mySkin;
	
	// Use this for initialization
	IEnumerator Start () {
		WWW www = new WWW(serverIP);
		yield return www;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnGUI()
	{
		GUI.skin = mySkin;
		//windowRect = GUI.Window(0,windowRect,windowFunc,"Server");
		if(!isConnected)
		{
			GUILayout.Label("Username");
			userName = GUILayout.TextField(userName);
			WoOzChatLayer.userName = userName;
			GUILayout.Label("Server IP");
			serverIP = GUILayout.TextField(serverIP);
			GUILayout.Label("Port");
			port = GUILayout.TextField(port);
			iport = Int32.Parse(port);

			if(GUILayout.Button("Connect"))
			{

				try{
					WoOzChatLayer.connectToServer(serverIP, iport);
				}
				catch (Exception e)
				{
					print("An exception occurred in WoOzChatLayer.connectToServer: " + e.ToString());
				}
				isConnected = true;
			}

		}
		else
		{
			if(GUILayout.Button("Disconnect"))
			{
				WoOzChatLayer.disconnect();
				isConnected = false;
			}
		}
	}

	private void windowFunc(int id) // id of window
	{
		GUI.DragWindow(new Rect(0,0,Screen.width,Screen.height));
	}
}
