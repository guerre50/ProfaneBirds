using UnityEngine;
using System.Collections;

public class AnimateTweetsLeft : MonoBehaviour {

	private TextMesh _text;
	// Use this for initialization
	void Start () {
		_text = transform.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		int score = int.Parse(_text.text);
		int scoreDifference = (Logic.instance.tweetsAllowed - Logic.tweets) - score;
		_text.text = (score + scoreDifference) + "";
	}
}
