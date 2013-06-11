using UnityEngine;
using System.Collections;
using radical;


public class CameraMovement : MonoBehaviour {
	public Vector2 limits;
	public float sensitivity = 10.0f;
	private float _zoom;
	private Vector3 _leftLimit;
	private Vector3 _rightLimit;
	private Vector2 _position;
	private float _originalY;
	private Vector2 _positionPreShot;
	
	private Vector2 _zoomLimits;
	public Vector2 mouseSensitivity = Vector2.one;
	private SmoothVector3 _cameraPosition;
	private SmoothFloat _ortographicSize;
	
	// Use this for initialization
	void Start () {
		_leftLimit = GameObject.FindGameObjectWithTag("Slingshot").transform.position;
		RaycastHit info;
		if (Physics.SphereCast(Vector3.right*1000.0f,20.0f, -Vector3.right, out info, 2000.0f, 1 << LayerMask.NameToLayer("Pig"))) {
			_rightLimit = info.transform.position;
		}
		_originalY = Camera.main.transform.position.y;
		
		_zoomLimits.x = Camera.mainCamera.orthographicSize;
		_zoomLimits.y = (Mathf.Abs (limits.x) + Mathf.Abs (limits.y))/2;
		_position.x = (Camera.mainCamera.transform.position.x - _leftLimit.x)/(_rightLimit.x - _leftLimit.x);
		_cameraPosition = Camera.main.transform.position;
		_cameraPosition.Mode = SmoothingMode.lerp;
		_ortographicSize = Camera.main.orthographicSize;
		_ortographicSize.Mode = SmoothingMode.lerp;
		_positionPreShot = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		float deltaMouseWheel = Input.GetAxis("Mouse ScrollWheel")*sensitivity*Time.deltaTime;
		deltaMouseWheel = Time.deltaTime/2;
		_zoom = Mathf.Clamp01(_zoom + deltaMouseWheel);
		if (Logic.gameState == GameState.CameraMovement && _zoom >= 1.0f && _ortographicSize.IsComplete && _cameraPosition.IsComplete) {
			Logic.gameState = GameState.Idle;	
		}
		
		if (Input.GetMouseButton(0) && Logic.gameState != GameState.Aiming) {
			float horizontalMovement = -Input.GetAxis ("Mouse X")*mouseSensitivity.x*Time.deltaTime;
			_position.x = Mathf.Clamp01(_position.x + horizontalMovement);
		}
		
		if (Logic.gameState == GameState.Shot) {
			if (_positionPreShot.magnitude == 0.0f) {
				_positionPreShot = _position;	
			}
			_position.x = Mathf.Clamp01(_position.x + Time.deltaTime*0.4f);
		} else if (_positionPreShot.magnitude != 0) {
			if (_position != _positionPreShot) {
				_position = _positionPreShot;
				_cameraPosition.Duration = 3.0f;
				Logic.gameState = GameState.Reloading;
				_positionPreShot = Vector3.zero;
			}
		} else if (Logic.gameState == GameState.Reloading) {
			if (Input.GetMouseButtonDown(0)) {
				_cameraPosition.Duration = 1.0f;
			}
			if (_cameraPosition.IsComplete) {
				Logic.gameState = GameState.Idle;
				_cameraPosition.Duration = 0.1f;
			}
		}
		_ortographicSize.Value = Mathf.Lerp(_zoomLimits.x, _zoomLimits.y, Mathf.Sin (_zoom*Mathf.PI/2.0f));
		Camera.main.orthographicSize = _ortographicSize.Value;
		Vector3 cameraPosition = Camera.main.transform.position;
		cameraPosition.x = Mathf.Lerp(_leftLimit.x, _rightLimit.x, _position.x+Mathf.Lerp (0.0f, 0.5f-_position.x, _zoom));
		cameraPosition.y = Mathf.Lerp (_originalY, (limits.x+limits.y)/2, _zoom);
		_cameraPosition.Value = cameraPosition;
		
		Camera.main.transform.position = _cameraPosition.Value; 
	}
	
	
	void OnDrawGizmos() {
		Gizmos.DrawLine(-Vector3.right*1000.0f + limits.x*Vector3.up, Vector3.right*1000.0f + limits.x*Vector3.up);
		
		
		Gizmos.DrawLine(-Vector3.right*1000.0f + limits.y*Vector3.up, Vector3.right*1000.0f + limits.y*Vector3.up);
	}
}
