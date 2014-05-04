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
	private float minTime;
	private bool isTimer;
	private string oldAnimation;

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
		oldAnimation = "idle";
		minTime      = -1;
		timer        = minTime;
		isTimer      = false;
		spriteRenderer = renderer as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		if (isTimer)//in emotional animation
		{
			timer -= Time.deltaTime;
			//print ("in timer "+ timer);
			if(timer <=0)
			{
				print("done");
				timer = minTime;
				WoOzChatLayer.emotion = "idle";
				oldAnimation = "idle";
			}
		}
		int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);

		//make her type when message arrives and no emotion is being sent
		if(!isTimer && WoOzChatLayer.getChat() != "" && WoOzChatLayer.emotion == "idle")
		{
			WoOzChatLayer.emotion = "type";
		}

		checkIfNew (WoOzChatLayer.emotion);
		switch (WoOzChatLayer.emotion) {
			case "angry":
				index = index % angry.Length;
				spriteRenderer.sprite = angry[ index ];
				if(timer == minTime)
					setTimer(angry.Length);
				break;
			case "confused":
				index = index % confused.Length;
				spriteRenderer.sprite = confused[ index ];
				if(timer == minTime)
					setTimer(confused.Length);
				break;
			case "frustrated":
				index = index % frustrated.Length;
				spriteRenderer.sprite = frustrated[ index ];
				if(timer == minTime)
					setTimer(frustrated.Length);
				break;
			case "happy":
				index = index % happy.Length;
				spriteRenderer.sprite = happy[ index ];
				if(timer == minTime)
					setTimer(happy.Length);
			break;
			case "sad":
				index = index % sad.Length;
				spriteRenderer.sprite = sad[ index ];
				if(timer == minTime)
					setTimer(sad.Length);
				break;
			case "surprised":
				index = index % surprised.Length;
				spriteRenderer.sprite = surprised[ index ];
				if(timer == minTime)
					setTimer(surprised.Length);
				break;
			case "type":
				index = index % idleT.Length;
				spriteRenderer.sprite = idleT[ index ];
				if(timer == minTime)
					setTimer(idleT.Length);
				break;
			default:
				index = index % idle.Length;
				spriteRenderer.sprite = idle[ index ];
				isTimer = false;
				timer = minTime;
				break;
			}
	}

	//check if new emotion sent before old one ended
	void checkIfNew(string a)
	{
		if (a != oldAnimation)
		{
			timer        = minTime;
			oldAnimation = a;
		}
	}
	//set timer for animation
	void setTimer(int length)
	{
		isTimer = true;
		timer   =  length / framesPerSecond;
	}


	//init sprites dynamically [not using now]
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
