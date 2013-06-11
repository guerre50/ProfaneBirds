using UnityEngine;
using System.Collections;
using radical;

public class ZoomOnOver : MonoBehaviour {
	private int _direction;
	private SmoothVector3 _scale;
	private Vector3 _fromScale;
	private Vector3 _toScale;
	public float duration = 1.0f;
	public float zoom = 1.2f;
	public SmoothingMode smoothMode = SmoothingMode.lerp;
	public EasingType ease = EasingType.Linear;
	
	// Use this for initialization
	void Start () {
		_fromScale = transform.localScale;
		_toScale = _fromScale*zoom;
		
		_scale = _fromScale;
		_scale.Duration = duration;
		_scale.Mode = smoothMode;
		_scale.Ease = ease;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = _scale.Value;
	}
	
	void OnMouseEnter() {
		_scale.Value = _toScale;
	}
	
	void OnMouseExit() {
		_scale.Value = _fromScale;
	}
}
