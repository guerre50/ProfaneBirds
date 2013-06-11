using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {
	private GUIText _text;
	// Use this for initialization
	void Start () {
		_text = guiText;
	}
	
	// Update is called once per frame
	void Update () {
		int score = int.Parse(_text.text);
		int scoreDifference = Logic.score - score;
		_text.text = (score + Mathf.Ceil(scoreDifference/2.0f)) + "";
	}
}
