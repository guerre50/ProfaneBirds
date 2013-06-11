using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	public Bird birdPrefab;
	public float forceFactor;
	public float forceRadius = 2.0f;
	public GameObject wrapper;
	public GameObject frontEndPoint;
	public GameObject backEndPoint;
	public GameObject frontSling;
	public GameObject backSling;
	public GameObject focus;
	
	private Bird _currentBird;
	private SlingshotSounds _sounds;
	
	// Use this for initialization
	void Start () {
		_sounds = transform.GetComponent<SlingshotSounds>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_currentBird != null) {
			if (Logic.mouseUp) {
				_currentBird.Shot((transform.position - _currentBird.transform.position)*forceFactor);
				_sounds.OnShot();
				_currentBird = null;
				Logic.gameState = GameState.Shot;
				Logic.tweets += 1;
				StartCoroutine(Logic.SetIdle());
			} else {
				Vector3 inputPosition = Camera.main.ScreenToWorldPoint(Logic.mousePosition);
				Vector3 inputClamp = Vector3.ClampMagnitude(inputPosition - transform.position, forceRadius);
				inputPosition = inputClamp +  transform.position;
				
				inputPosition.z = focus.transform.position.z;
				_currentBird.transform.position = inputPosition;
				
				AnimateWrapper(_currentBird.gameObject);
			}
		} else {
			if (Logic.mouseDown) {
				OnMouseDown();	
			}
			AnimateWrapper(focus);	
		}
	}
	
	void OnMouseDown() {
		Logic.gameState = GameState.Aiming;
		_currentBird = Instantiate(birdPrefab, transform.position, transform.rotation) as Bird;
		_currentBird.LoadtoSlingshot();
	}
	
	void OnDrawGizmosSelected() {
		Color previousColor = Gizmos.color;
		
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, forceRadius);	
		
		Gizmos.color = previousColor;
	}
	
	public void OnAim() {
		_sounds.OnAim();	
	}
	
	void AnimateWrapper(GameObject target) {
		Vector3 wrapperPosition = target.transform.position 
				+ (target.transform.position - focus.transform.position).normalized*target.transform.localScale.magnitude/3.0f;
		wrapperPosition.z = wrapper.transform.position.z;
		wrapper.transform.position = wrapperPosition;
		
		Vector3 focusPosition = focus.transform.position;
		focusPosition.z = wrapper.transform.position.z;
		wrapper.transform.LookAt(focusPosition, wrapper.transform.up);
		
		Vector3 frontSlingPosition = (frontEndPoint.transform.position + wrapper.transform.position)/2;
		frontSlingPosition.z = frontSling.transform.position.z;
		frontSling.transform.position = frontSlingPosition;
		frontSling.transform.LookAt(frontEndPoint.transform, frontSling.transform.up);
		
		Vector3 frontSlingScale = frontSling.transform.localScale;
		frontSlingScale.z = (wrapper.transform.position - frontEndPoint.transform.position).magnitude/10;
		frontSling.transform.localScale = frontSlingScale;
		
		Vector3 backSlingPosition = (backEndPoint.transform.position + wrapper.transform.position)/2;
		backSlingPosition.z = backSling.transform.position.z;
		backSling.transform.position = backSlingPosition;
		backSling.transform.LookAt(backEndPoint.transform, backSling.transform.up);
		
		Vector3 backSlingScale = backSling.transform.localScale;
		backSlingScale.z = (wrapper.transform.position - backEndPoint.transform.position).magnitude/12;
		backSling.transform.localScale = backSlingScale;
	}
}
