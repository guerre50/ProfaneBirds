using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public Vector2 fontSizeRange = new Vector2(40,60);
	public float increaseVelocity = 1;
	public float decreaseVelocity = 0.2f;
	public float relativeScore;
	private float _maxScale;
	private float _velocity;
	
	
	// Use this for initialization
	void Start () {
		_maxScale = Mathf.Lerp(fontSizeRange.x, fontSizeRange.y, relativeScore);
		transform.localScale = Vector3.zero;
		_velocity = increaseVelocity;
		Vector3 pos = transform.position;
		pos.z = Camera.main.transform.position.z + Camera.main.nearClipPlane*1.1f;
		transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = Vector3.one*Mathf.Max(0, transform.localScale.x + Time.deltaTime*_velocity);
		if (transform.localScale.x > _maxScale && _velocity > 0) {
			_velocity = -decreaseVelocity;
		}
		if (transform.localScale.magnitude == 0) Destroy (gameObject);
	}
}
