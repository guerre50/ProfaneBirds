using UnityEngine;
using System.Collections;

public class WaitingAnimation : MonoBehaviour {
	public Vector2 animationRange = new Vector2(5.0f, 15.0f);
	
	// Use this for initialization
	void Start () {
		StartCoroutine(Animate());	
	}
					
	IEnumerator Animate() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(animationRange.x, animationRange.y));
			animation.Play();
		}
	}
}
