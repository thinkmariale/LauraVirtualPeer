using UnityEngine;
using System.Collections;

public class VPanimator : MonoBehaviour {

	public Sprite[] idle;
	public Sprite[] idleT;
	public Sprite[] angry;
	public Sprite[] confused;
	public Sprite[] frustrated;
	public Sprite[] happy;
	public Sprite[] sad;
	public Sprite[] surprised;

	public Sprite[] sprites;
	public float framesPerSecond;

	private float timer;
	private bool isTimer;
	private SpriteRenderer spriteRenderer;
	void Awake()
	{
		//initSprites(idle,"ArtAssets/Idle/v_idle_000");
		idle = Resources.LoadAll<Sprite>("ArtAssets/Idle");
		idleT = Resources.LoadAll<Sprite>("ArtAssets/Idle-typing");
		angry = Resources.LoadAll<Sprite>("ArtAssets/Angry");
		confused = Resources.LoadAll<Sprite>("ArtAssets/Confused");
		frustrated = Resources.LoadAll<Sprite>("ArtAssets/Frustrated");
		happy = Resources.LoadAll<Sprite>("ArtAssets/Happy");
		sad = Resources.LoadAll<Sprite>("ArtAssets/Sad");
		surprised = Resources.LoadAll<Sprite>("ArtAssets/Surprised");
		//initSprites(idleT,"Assets/ArtAssets/Idle-typing/v_typing_000");
		//initSprites(angry,"Assets/ArtAssets/Angry/v_angry_000");
		//initSprites(confused,"Assets/ArtAssets/Confused/v_confused_000");
		//initSprites(frustrated,"Assets/ArtAssets/Frustrated/v_frustrated_000");
		//initSprites(happy,"Assets/ArtAssets/Happy/v_happy_000");
		//initSprites(sad,"Assets/ArtAssets/Sad/v_sad_000");
		//initSprites(surprised,"Assets/ArtAssets/Surprised/v_surprised_000");


	}

	// Use this for initialization
	void Start () {
		spriteRenderer = renderer as SpriteRenderer;
		timer = 300; ///3 seconds
		isTimer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isTimer)
		{
			timer -= Time.deltaTime;
			if(timer <=0)
			{
				timer = 300;
				WoOzChatLayer.emotion = "idle_";
			}
		}
		int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);

		//make her type when message arrives and no emotion is being sent
		if(WoOzChatLayer.getChat() != "" && WoOzChatLayer.emotion == "idle")
		{
			WoOzChatLayer.emotion = "type";
		}
		switch (WoOzChatLayer.emotion) {
			case "angry":
				index = index % angry.Length;
				spriteRenderer.sprite = angry[ index ];
				isTimer = true;
				break;
			case "confused":
				index = index % confused.Length;
				spriteRenderer.sprite = confused[ index ];
				isTimer = true;
				break;
			case "frustrated":
				index = index % frustrated.Length;
				spriteRenderer.sprite = frustrated[ index ];
				isTimer = true;
				break;
			case "happy":
				index = index % happy.Length;
				spriteRenderer.sprite = happy[ index ];
				isTimer = true;	
			break;
			case "sad":
				index = index % sad.Length;
				spriteRenderer.sprite = sad[ index ];
				isTimer = true;
				break;
			case "surprised":
				index = index % surprised.Length;
				spriteRenderer.sprite = surprised[ index ];
				isTimer = true;
				break;
			case "type":
				index = index % surprised.Length;
				spriteRenderer.sprite = idleT[ index ];
				isTimer = true;
				break;
			default:
				index = index % idle.Length;
				spriteRenderer.sprite = idle[ index ];
				isTimer = false;
				break;
			}
	}

	void initSprites(Sprite[]s,string path)
	{
		string j="";

		for(int i=0;i<s.Length;i++)
		{
			j = i.ToString();
			if(i<10)
				j = "0"+i.ToString();

			string path1 = path+j+".png";
			Sprite ss = (Sprite)Resources.LoadAssetAtPath(path1, typeof(Sprite));

			s[i] = ss;
		}
	}
}
