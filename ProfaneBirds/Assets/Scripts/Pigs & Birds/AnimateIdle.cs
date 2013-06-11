using UnityEngine;
using System.Collections;

public class AnimateIdle : MonoBehaviour {
	private Material _material;
	private int _currentFrame;
	public float animationTime;
	public int frames;
	public Vector2 animationLapse = new Vector2(3.0f, 8.0f);
	private float _initialOffset;
	
	// Use this for initialization
	void Start () {
		_material = transform.FindChild("Texture").renderer.material;
		_initialOffset = _material.mainTextureOffset.y;
		StartCoroutine(Animate());	
	}
					
	IEnumerator Animate() {
		while (true) {
			float time;
			
			if (_currentFrame < 2) {
				time = animationTime/frames;
				_currentFrame += Random.Range (1,frames);
			} else {
				_currentFrame = 0;
				time = Random.Range(animationLapse.x, animationLapse.y);
			}
			Vector2 offset = _material.mainTextureOffset;
			offset.y = _initialOffset + (float)_currentFrame/frames;
			_material.mainTextureOffset = offset;
			yield return new WaitForSeconds(time);			
		}
	}
}
