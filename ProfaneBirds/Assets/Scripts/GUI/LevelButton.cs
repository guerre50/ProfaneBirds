using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour {
	public int level;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {
		audio.Play();
		Application.LoadLevel(level);	
	}
}
