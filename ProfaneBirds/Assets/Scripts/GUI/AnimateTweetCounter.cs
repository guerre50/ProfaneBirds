using UnityEngine;
using System.Collections;

public class AnimateTweetCounter : MonoBehaviour {
	private TextMesh _text;
	// Use this for initialization
	void Start () {
		_text = transform.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		int score = int.Parse(_text.text);
		int scoreDifference = ProcessTweets.pendingTweets.Count - score;
		if (scoreDifference > 0)
			_text.text = (score + Mathf.Ceil(scoreDifference/2.0f)) + "";
		else _text.text = (score + Mathf.Floor(scoreDifference/2.0f)) + "";
		
		
	}
}
