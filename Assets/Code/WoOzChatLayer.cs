using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using SimpleJSON;

public static class WoOzChatLayer {

	static System.Net.Sockets.TcpClient chatClient;
	public static string userName = "";
	public static string emotion = "idle";

	// initializes the chat client socket and attempts a tcp connection
	// any calls to this function should be try/catch, as socket failure will throw an error
	public static void connectToServer (string serverIP, int port)
	{
		chatClient = new System.Net.Sockets.TcpClient(serverIP, port);
		if (chatClient.Connected)
		{
			string [] msg = { "connect", userName, "lesson1"};
			NetworkStream stream = chatClient.GetStream();
			Byte [] data = System.Text.Encoding.ASCII.GetBytes("[\"" + msg[0] + "\", \"" + msg[1] + "\", \"" + msg[2] + "\"]");
			stream.Write(data, 0, data.Length);
		}
	}

	public static void sendMessage (string message)
	{
		NetworkStream stream = chatClient.GetStream();
		JSONArray arr = new JSONArray();
		arr.Add("message");
		arr.Add(userName);
		arr.Add("laura");
		arr.Add(message);

		Byte[] data = System.Text.Encoding.ASCII.GetBytes(arr.ToString());
		stream.Write(data, 0, data.Length);
	}

	public static NetworkStream getStream ()
	{
		return chatClient.GetStream();
	}

	public static string getMessage ()
	{	
		Byte [] buffer = new Byte[1024];
		NetworkStream stream = chatClient.GetStream();
		int bytesread = 0;

		if (stream.CanRead && stream.DataAvailable)
			bytesread = stream.Read(buffer, 0, 1024);
		else 
			return "";

		if (bytesread == 0)
			return "";

		JSONArray arr = (JSONArray)JSON.Parse(System.Text.Encoding.UTF8.GetString(buffer));
		if (arr.Count == 5)
			emotion = arr[4];

		string ret =  "Laura: " + arr[3].ToString();
		return ret;
	}

	public static string getEmotion ()
	{
		return emotion;
	}

	public static bool isConnected ()
	{
		if (chatClient != null)
			return chatClient.Connected;
		else return false;
	}

	public static void disconnect ()
	{
		if (chatClient != null)
			chatClient.Close();
	}
}