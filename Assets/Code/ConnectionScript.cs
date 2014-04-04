using UnityEngine;
using System.Collections;
using System;

public class ConnectionScript : MonoBehaviour {

	private string serverName = "" , maxUsers = "1",port = "25566";
	private Rect windowRect = new Rect(0,0,400,400);
	public GUISkin mySkin;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnGUI()
	{
		GUI.skin = mySkin;
		windowRect = GUI.Window(0,windowRect,windowFunc,"Server");
		if(Network.peerType == NetworkPeerType.Disconnected)
		{
			GUILayout.Label("Server Name");
			serverName = GUILayout.TextField(serverName);
			GUILayout.Label("Port");
			port = GUILayout.TextField(port);
			GUILayout.Label("Max Users");
			maxUsers = GUILayout.TextField(maxUsers);

			if(GUILayout.Button("Create Server"))
			{
				try{
					Network.InitializeSecurity();
					Network.InitializeServer(int.Parse(maxUsers),int.Parse(port),!Network.HavePublicAddress());
					MasterServer.RegisterHost("testing VP",serverName);
				}
				catch (Exception)
				{
					print("please type the info");
				}
			}

		}
		else
		{
			if(GUILayout.Button("Disconnect"))
			{
				Network.Disconnect();
			}
		}
	}

	private void windowFunc(int id) // id of window
	{
		if(GUILayout.Button("Refresh"))
		{
			MasterServer.RequestHostList("testing VP");
		}
		GUILayout.BeginHorizontal();
		GUILayout.Box("Server Name");
		GUILayout.EndHorizontal();

		if(MasterServer.PollHostList().Length != 0)
		{
			HostData[] data = MasterServer.PollHostList();
			foreach(HostData c in data)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Box(c.gameName);
				if(GUILayout.Button("Connect") )
				{
					Network.Connect(c);
				}
				GUILayout.EndHorizontal();
			}
		}
		GUI.DragWindow(new Rect(0,0,Screen.width,Screen.height));
	}
}
