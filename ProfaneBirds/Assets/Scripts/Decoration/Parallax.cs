using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	private Vector3 _originalPosition;
	public float distance = 0.0f;
	// Use this for initialization
	void Start () {
		_originalPosition = transform.position;
		if (distance == 0.0f) {
			distance = transform.position.z;	
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = _originalPosition + Vector3.right*(Camera.main.transform.position.x/distance);
	}
}
