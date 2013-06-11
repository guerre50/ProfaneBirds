using UnityEngine;
using System.Collections;

public class SlingshotSounds : Sounds {
	public AudioClip[] shotSounds;
	public AudioClip[] slingSounds;
	
	
	public void OnShot() {
		PlayRandomClip(shotSounds);	
	}
	
	public void OnAim() {
		PlayRandomClip(slingSounds);	
	}
}
