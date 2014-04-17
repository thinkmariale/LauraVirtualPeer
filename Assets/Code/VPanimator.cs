using UnityEngine;
using System.Collections;

public class VPanimator : MonoBehaviour {

	public Sprite[] idle;
	public Sprite[] angry;
	public Sprite[] confused;
	public Sprite[] frustrated;
	public Sprite[] happy;
	public Sprite[] sad;
	public Sprite[] surprised;

	public Sprite[] sprites;
	public float framesPerSecond;

	private SpriteRenderer spriteRenderer;
	void Awake()
	{

	}
	// Use this for initialization
	void Start () {
		spriteRenderer = renderer as SpriteRenderer;
		addSprite(idle);

	}
	
	// Update is called once per frame
	void Update () {

		int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
		switch (WoOzChatLayer.emotion) {
			case "angry":
				addSprite(angry);
				break;
			case "confused":
				addSprite(confused);
				break;
			case "frustrated":
				addSprite(frustrated);
				break;
			case "happy":
				addSprite(happy);
				break;
			case "sad":
				addSprite(sad);
				break;
			case "excited":
				addSprite(surprised);
				break;
			default:
				addSprite(idle);
				break;
		}

		index = index % sprites.Length;
		spriteRenderer.sprite = sprites[ index ];
	}
	void addSprite(Sprite[] x)
	{
	
		for(int i=0;i<x.Length;i++)
		{
			sprites[i] = x[i];
		}

	}
}
