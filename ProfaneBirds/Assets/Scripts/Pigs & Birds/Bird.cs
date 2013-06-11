using UnityEngine;
using System.Collections;

public enum BirdStatus { Idle, Slingshot, Flying, Collided};

public class Bird : MonoBehaviour {
	public BirdStatus status = BirdStatus.Idle;
	private BirdSounds _sounds;
	
	void Awake() {
		_sounds = transform.FindChild ("Sounds").GetComponent<BirdSounds>();	
	}
	
	void Start() {
		transform.GetComponent<AnimateTrail>().enabled = true;	
		gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
	}
	
	void OnCollisionEnter(Collision collision) {
		if (status == BirdStatus.Flying) {
			status = BirdStatus.Collided;
			//StartCoroutine(Logic.SetIdle ());
		}
	}
	
	public void LoadtoSlingshot() {
		status = BirdStatus.Slingshot;
		_sounds.OnSelected();
		Destroy(transform.GetComponentInChildren<WaitingAnimation>());
	}
	
	public void Shot(Vector3 velocity) {
		status = BirdStatus.Flying;
		rigidbody.isKinematic = false;
		rigidbody.velocity = velocity;
		_sounds.OnFly();
		gameObject.layer = LayerMask.NameToLayer("Bird");
	}
	
	void Update() {
		if (transform.position.y < -4) {
			AnimateTrail trail = transform.GetComponent<AnimateTrail>();	
			trail.DestroyTrail();
			OnCollisionEnter(null);
			Destroy (gameObject);
		}
	}
	
	IEnumerator SetIdle() {
		yield return new WaitForSeconds(3.0f);
		Logic.gameState = GameState.Idle;
	}
}
