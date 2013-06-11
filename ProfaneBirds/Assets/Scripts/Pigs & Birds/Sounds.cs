using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {
	public AudioClip[] idleSounds;
	public Vector2 timeRange = new Vector2(3.0f, 3.0f);
	public AudioClip[] collisionSounds;
	public AudioClip[] explosionSounds;
	public AudioClip[] damageSounds;
	
	void Start() {
		if (idleSounds != null && idleSounds.Length > 0) {
			StartCoroutine(WaitForIt());	
		}
	}
	
	
	IEnumerator WaitForIt() {
		while(true) {
			yield return new WaitForSeconds(Random.Range (timeRange.x, timeRange.y));
			if (!audio.isPlaying) {
				audio.clip = idleSounds[Random.Range(0, idleSounds.Length)];
				audio.Play();
			} 
		}
	}
	
	public void OnCollision() {
		PlayRandomClip(collisionSounds);
	}
	
	public void OnDamage() {
		PlayRandomClip(damageSounds);
	}
	
	public void OnExplode() {
		PlayRandomClip(explosionSounds);
	}
	
	protected void PlayRandomClip(AudioClip[] clipArray) {
		if (clipArray != null && clipArray.Length > 0) {
			audio.clip = clipArray[Random.Range(0, clipArray.Length)];	
			audio.Play();
		}
	}
}
