using UnityEngine;
using System.Collections;

public class AnimateTrail : MonoBehaviour {
	private LineRenderer _trail;
	private Vector3 _lastPosition;
	public float stepDistance = 1.0f;
	private int _numPositions;
	public float offsetIncreaseStep = 0.3f;
	private float _totalDistance;
	private float _currentStepDistance;
	private Bird _bird;
	
	
	// Use this for initialization
	void Start () {
		_trail = transform.GetComponentInChildren<LineRenderer>();
		_bird = transform.GetComponent<Bird>();
		_lastPosition = transform.position;
		_numPositions = 1;
		_trail.SetVertexCount(1);
		_trail.SetPosition(0, transform.position);
		_trail.gameObject.SetActive(false);
		_totalDistance = _currentStepDistance = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (_bird.status == BirdStatus.Flying) {
			if (!_trail.gameObject.activeSelf) _trail.gameObject.SetActive(true);
			float deltaDistance = Vector3.Distance(_lastPosition, transform.position);
			_currentStepDistance += deltaDistance;
			_totalDistance += deltaDistance;
			
			if (_currentStepDistance > stepDistance) {
				_currentStepDistance = 0.0f;
				_trail.SetVertexCount(_numPositions);
				_trail.SetPosition(_numPositions - 1, transform.position);
				_numPositions++;
				Vector2 tiling = _trail.material.mainTextureScale;
				tiling.x += offsetIncreaseStep;
				_trail.material.mainTextureScale = tiling;
			}
		}
		_lastPosition = transform.position;
		
	}
	
	void OnCollisionEnter(Collision collision) {
		if (!this.enabled) return;
		DestroyTrail();
	}
	
	public void DestroyTrail() {
		GameObject[] trails = GameObject.FindGameObjectsWithTag("Trail");
		foreach (GameObject trail in trails) {
			if (trail != _trail.gameObject) {
				Destroy (trail);
			}
		}
		_trail.transform.parent = null;
		Destroy (this);	
	}
}
