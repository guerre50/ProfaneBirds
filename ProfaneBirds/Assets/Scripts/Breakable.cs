using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {
	public float minDamageForce = 4.0f;
	private Material _material;
	private ParticleSystem _particlesExplosion = null;
	private ParticleSystem _particlesCollision = null;
	private float minCollisionForce = 1.0f;
	public Vector2 particleEmitRange = new Vector2(10.0f, 20.0f);
	public Vector2 scoreRange = new Vector2(50, 300);
	private Sounds _sounds = null;
	
	// Use this for initialization
	void Start () {
		_material = transform.FindChild("Texture").renderer.material;
		Transform t = transform.FindChild("Explosion Particles");
		if (t != null) _particlesExplosion = t.particleSystem;
		t = transform.FindChild("Collision Particles");
		if (t !=  null) _particlesCollision = t.particleSystem;
		t = transform.FindChild("Sounds");
		if (t != null) _sounds = t.GetComponent<Sounds>();
	}
	
	void OnCollisionEnter(Collision collision) {
		if (Time.timeSinceLevelLoad < 1.0f) return;
		if (collision.impactForceSum.magnitude < minCollisionForce) return;
		if (_particlesCollision != null) _particlesCollision.Play();
		if (_sounds != null) _sounds.OnCollision();
		
		float collisionForce = collision.impactForceSum.magnitude;
		float relativeForce = (collisionForce - minDamageForce)/(2*minDamageForce);
		if (collisionForce > minDamageForce) {
			Vector2 offset = _material.mainTextureOffset;
			if (_sounds != null) _sounds.OnDamage();
			if (offset.x + _material.mainTextureScale.x >= 0.95f) {
				if (_particlesExplosion) {
					_particlesExplosion.Emit((int)Mathf.Lerp(particleEmitRange.x, particleEmitRange.y, relativeForce));
					if (_sounds != null) {
						_sounds.transform.parent = _particlesExplosion.transform;
						_sounds.OnExplode();
					}
					_particlesExplosion.transform.parent = null;
					Destroy (_particlesExplosion.gameObject, _particlesExplosion.duration);
				}
				Destroy (gameObject);	
			}
			
			offset.x += _material.mainTextureScale.x;
			_material.mainTextureOffset = offset;
		}
		
		int score = (int)Mathf.Lerp (scoreRange.x, scoreRange.y, relativeForce);
		if (score > 0) {
			Logic.score += score;
			TextMesh scoreText = (Instantiate(Resources.Load ("Score", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject).GetComponent<TextMesh>();
			scoreText.GetComponent<Score>().relativeScore = relativeForce;
			scoreText.text = score + "";
		}
	}
}
