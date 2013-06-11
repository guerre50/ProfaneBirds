using UnityEngine;
using Streamer;
using System.Collections;

public enum GameState { CameraMovement, Pause, Aiming, Shot, Reloading, Idle, Result };
public enum Action { None, Reload, LevelSelect };


public class Logic : GameObjectSingleton<Logic> {
	public static GameState gameState;
	public static int score;
	public static int tweets;
	public static Tweet tweet;
	private AnimateTweet _tweetAnimator;
	
	public static bool mouseUp;
	public static bool mouseDown;
	public static bool animatingTweet;
	public static Vector2 mousePosition;
	public GameObject screenResult;
	public GameObject screenFailed;
	public int tweetsAllowed = 10;
	public AudioClip[] clips;
	
	void Awake() {
		animatingTweet = false;
		mouseUp = false;
		mouseDown = false;
	}
	
	void Start() {
		score = 0;
		tweets = 0;
		gameState = GameState.CameraMovement;
		_tweetAnimator = GameObject.Find ("Tweet Animator").GetComponent<AnimateTweet>();
	}
	
	void Update() {
		if (gameState == GameState.CameraMovement) return;
		if (ProcessTweets.pendingTweets != null && ProcessTweets.pendingTweets.Count > 0 && !animatingTweet) {
			if (!CheckWin()) {
				tweet = ProcessTweets.pendingTweets[0];
				ProcessTweets.pendingTweets.RemoveAt(0);
				_tweetAnimator.Animate(tweet);
				animatingTweet = true;
			}
		}
		if (!animatingTweet) {
			mousePosition = Input.mousePosition;
			mouseUp = Input.GetMouseButtonUp(0);
			mouseDown = Input.GetMouseButtonDown(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha0)) Application.LoadLevel(0);
		if (Input.GetKeyDown(KeyCode.Alpha1))Application.LoadLevel(1);
		if (Input.GetKeyDown(KeyCode.Alpha2))Application.LoadLevel(2);
		if (Input.GetKeyDown(KeyCode.Alpha3))Application.LoadLevel(3);
		if (Input.GetKeyDown(KeyCode.Alpha4))Application.LoadLevel(4);
				
	}
	
	void LateUpdate() {
		mouseDown = false;
		mouseUp = false;
	}
	
	public static void Execute(Action action) {
		switch (action) {
			case Action.Reload:
				Application.LoadLevel(Application.loadedLevel);
				break;
			case Action.LevelSelect:
				Application.LoadLevel(0);
				break;
		}
	}
	
	public static IEnumerator SetIdle() {
		yield return new WaitForSeconds(5.0f);
		Logic.gameState = GameState.Idle;
	}
	
	public static IEnumerator Restart(int level) {
		yield return new WaitForSeconds(5.0f);
		Application.LoadLevel(level);
	}
	
	public bool CheckWin() {
		GameObject[] go = GameObject.FindGameObjectsWithTag("Pig");
		if (go.Length == 0) {
			_tweetAnimator.renderer.enabled = false;
			screenResult.SetActive(true);
			gameState = GameState.Result;
			int level = Application.loadedLevel + 1;
			if (level + 1 == Application.levelCount) level = 1;
			StartCoroutine(Restart(level));
			return true;
		}
		if (Logic.tweets == tweetsAllowed) {
			_tweetAnimator.renderer.enabled = false;
			screenFailed.SetActive(true);
			gameState = GameState.Result;
			StartCoroutine(Restart(Application.loadedLevel));
			return true;
		}
		
		return false;
	}
}
