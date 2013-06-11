using UnityEngine;
using System.Collections;

public class BirdSounds : Sounds {
	public AudioClip[] flySounds;
	public AudioClip[] selectSounds;
	public AudioClip[] shotSounds;
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void OnSelected() {
		PlayRandomClip(selectSounds);
	}
	
	public void OnShot() {
		PlayRandomClip(shotSounds);
	}
	
	public void OnFly() {
		PlayRandomClip(flySounds);	
	}
}
