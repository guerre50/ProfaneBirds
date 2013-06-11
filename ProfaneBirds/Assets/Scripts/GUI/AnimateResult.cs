using UnityEngine;
using System.Collections;

public class AnimateResult : MonoBehaviour {
	public ParticleSystem particles1;
	public ParticleSystem particles2;
	public ParticleSystem particles3;
	public GameObject star1;
	public GameObject star2;
	public GameObject star3;
	public TextMesh score;
	public TextMesh tweets;
	public TextMesh tweet;
	// Use this for initialization
	void Start () {
		star1.animation.Play();
		star2.animation.Play();
		star3.animation.Play();
		
		StartCoroutine(EmitInSeconds(0.2f, particles1));
		StartCoroutine(EmitInSeconds(0.5f, particles2));
		StartCoroutine(EmitInSeconds(0.7f, particles3));
	}
	
	// Update is called once per frame
	void Update () {
		int _score = int.Parse(score.text);
		int scoreDifference = Logic.score - _score;
		score.text = (_score + Mathf.Ceil(scoreDifference)) + "";
		
		_score = int.Parse(tweets.text);
		scoreDifference = Logic.tweets - _score;
		tweets.text = (_score + Mathf.Ceil(scoreDifference)) + "";
	}
	
	IEnumerator EmitInSeconds(float seconds, ParticleSystem particle) {
		yield return new WaitForSeconds(seconds);
		particle.Emit (100);
	}
}
