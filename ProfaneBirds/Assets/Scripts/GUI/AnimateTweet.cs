using UnityEngine;
using System.Collections;
using Streamer;
using System.Text.RegularExpressions;
using radical;


public class AnimateTweet : MonoBehaviour {
	private TextMesh _text;
	private Tweet _tweet;
	private Slingshot _slingshot;
	public GameObject cursor;
	float force;
	Vector3 direction = Vector2.one;
	public static SmoothVector3 _cursorTarget;
	private bool animating = false;
	private float _cursorZ;
	private Vector2 _cursorGrab = new Vector2(0.78f, 0.0f);
	private Vector2 _cursorIdle = new Vector2(0.26f, 0.0f);
	public Camera camera;
	
	enum State { Idle, TowardsSlingshot, Aiming, Shot };
	private State _state = State.Idle;
	private int _cursorIdleDirection = 1;
	private string _scoreRegex;
	private Regex _regex;
	
	public Tweet tweet {
		get {
			return _tweet;
		}
		set{
			_tweet = value;
			if (value != null) {
				SetInternalValues(_tweet);
			}
			
		}
	}
	
	// Use this for initialization
	void Start () {
		_text = transform.GetComponent<TextMesh>();
		_slingshot = GameObject.Find ("Slingshot").GetComponent<Slingshot>();
		_cursorZ = cursor.transform.position.z;
		direction = Vector3.zero;
		_cursorTarget = cursor.transform.position + transform.up;
		_scoreRegex = Config.instance.config.GetProperty("scoreRegex").str;
		_regex = new Regex(_scoreRegex);
	}
	
	// Update is called once per frame
	void Update () {
		switch (_state) {
		case State.Idle:
			Idle();
			break;
		case State.TowardsSlingshot:
			TowardsSlingshot();
			break;
		case State.Aiming:
			Aiming ();
			break;
		case State.Shot:
			Shot();
			
			
			break;
		}
	}
	private void Idle() {
		if (!_cursorTarget.IsComplete) {
			cursor.transform.position = _cursorTarget.Value;	
		} else {
			_cursorIdleDirection *= -1;
			_cursorTarget.Value = cursor.transform.position + transform.up*_cursorIdleDirection*0.2f;
			_cursorTarget.Mode = SmoothingMode.slerp;
			_cursorTarget.Duration = 1.0f;
		}
	}
	
	private void TowardsSlingshot() {
		if (!_cursorTarget.IsComplete) {
			Vector3 pos = camera.ScreenToWorldPoint(_cursorTarget.Value);
			pos.z = _cursorZ;
			cursor.transform.position = pos;
		} else {
			_state = State.Aiming;
			cursor.renderer.material.mainTextureOffset = _cursorGrab;
			_cursorTarget.Value -= direction*(Mathf.Min (force*8,400));
			_slingshot.OnAim();
		}	
	}
	
	private void Aiming() {
		if (!_cursorTarget.IsComplete) {
			Vector3 pos = camera.ScreenToWorldPoint(_cursorTarget.Value);
			pos.z = _cursorZ;
			cursor.transform.position = pos;
			Logic.mousePosition = _cursorTarget.Value;
		} else {
			_state = State.Shot;
			Logic.mouseUp = true;
			cursor.renderer.material.mainTextureOffset = _cursorIdle;	
		}	
	}

	public void Shot() {
		if (Logic.gameState == GameState.Idle) {
			_cursorTarget = cursor.transform.position;
			_state = State.Idle;
			Logic.animatingTweet = false;
		}
	}
	
	public void Animate(Tweet tweet) {
		this.tweet = tweet;
		In();
		Logic.mouseDown = true;
	}
	
	void  In() {
		animation.Play("statusIn2",PlayMode.StopAll);
	}
	
	void Out() {
		animation.Play("statusOut",PlayMode.StopAll);
	}
	
	private void SetInternalValues(Tweet tweet) {
		string status = tweet.status;
		int length = status.Length;
		if (status.Length > 70) {
			int i = status.IndexOf(" ", 60);
			if (i <= 60 || i > 75) i = 60;
			status = status.Insert(i, "\n");
		}
		_text.text = status;
		
		float angle = (length%40 + 25)*Mathf.Deg2Rad;
		direction.x = Mathf.Sin (angle);
		direction.y = Mathf.Cos (angle);
		
		force = ComputeProfanity(tweet)*30.0f;
		
		Vector3 pos = Camera.main.WorldToScreenPoint(_slingshot.transform.position);
		Vector3 cursorPos = camera.WorldToScreenPoint(cursor.transform.position);
		
		_cursorTarget = cursorPos;
		_cursorTarget.Mode = SmoothingMode.slerp;
		_cursorTarget.Duration = Vector3.Distance(cursorPos, pos)/Screen.width*4;
		_cursorTarget.Value = pos;
		Logic.mousePosition = pos;
		_state = State.TowardsSlingshot;
	}
	
	float ComputeProfanity(Tweet t) {
		float total = 0.0f;
		string status = t.status.ToLower();
		
		MatchCollection result = _regex.Matches(status);
		total += result.Count;
		
		return Mathf.Max (total, 1.0f);
	}
}
