using UnityEngine;
using System.Collections;

public class Anchor : MonoBehaviour {
	public Camera camera;
	public TextAnchor alignment;
	
	// Use this for initialization
	void Start () {
		switch (alignment) {
		case TextAnchor.UpperLeft:	
			transform.position = camera.ScreenToWorldPoint(new Vector3(0, Screen.height,0));
			break;
		case TextAnchor.LowerLeft:
			transform.position = camera.ScreenToWorldPoint(new Vector3(0,0,0));
			break;
		}
	}
}
