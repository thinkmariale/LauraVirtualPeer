using UnityEngine;
using System.Collections;

public class VPanimator : MonoBehaviour {

	public Sprite[] sprites;
	public float framesPerSecond;

	private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		spriteRenderer = renderer as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		//int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
		int index = 0;
		switch (WoOzChatLayer.emotion) {
			case "happy":
				index = 0;
				break;
			case "sad":
				index = 1;
				break;
			case "excited":
				index = 2;
				break;
			default:
				index = 0;
				break;
		}
		
		index = index % sprites.Length;
		spriteRenderer.sprite = sprites[ index ];
	}
}
