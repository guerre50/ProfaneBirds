using UnityEngine;
using System.Collections;
using Streamer;
using System.Collections.Generic;
using System;
using System.IO;

public class ProcessTweets : MonoBehaviour {

	private TwitterAccess _twitterAcces;
	private TwitterQuery _twitterQuery;
	public int availableTweets = 200;
	public static List<Tweet> pendingTweets = null;
	private bool _started = false;
	private string _username; 
	private string _password; 
	private string _searchTerm;
	private Config _config;
	private string _errorMessage = "";
	private Rect _errorRect;
	private Vector2 _errorMessageSize = new Vector2(400,200);
	
	void Awake() {
		GameObject[] go = GameObject.FindGameObjectsWithTag("Tweet");
		if (go.Length > 1 && !_started) {
			Destroy(gameObject);
			return;
		}
		pendingTweets = new List<Tweet>();
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Use this for initialization
	void Start () {
		_errorRect = new Rect((Screen.width - _errorMessageSize.x)*0.5f, (Screen.height - _errorMessageSize.y)*0.5f, _errorMessageSize.x, _errorMessageSize.y);
		
		if (PlayerPrefs.HasKey("angrytweet")) {
			_config = Config.instance;
			
			if (_config) {
				JSONObject twitterAccount = _config.config.GetProperty("twitterAccount");
				_username = twitterAccount.GetProperty("username").str;
				_password = twitterAccount.GetProperty("password").str;
				_searchTerm = _config.config.GetProperty("searchTerm").str;
			} else {
				_searchTerm = "Please set config.json file";
			}
			
			_twitterAcces = new TwitterAccess();
			_twitterAcces.authErrorHandler = ProcessAuthError;
			_twitterAcces.BasicAuthUserPassword(_username, _password);
			// Get all tweets with a location tag
			string query = _searchTerm;
			
			QueryTrack track = new QueryTrack(query);
			_twitterAcces.AddQueryParameter(track);
			_twitterAcces.Connect(true);
			_started = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_config && availableTweets > 0 && _twitterAcces.tweets.Count > 0) {
			Tweet t = _twitterAcces.tweets.Dequeue();
			if (!t.json.HasProperty("retweeted_status")) pendingTweets.Add(t);
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			pendingTweets.Clear();
			if (PlayerPrefs.HasKey("angrytweet")) {
				PlayerPrefs.DeleteKey("angrytweet");	
			}else {
				PlayerPrefs.SetInt ("angrytweet", 1);
			}
			
			if (_config) _twitterAcces.Disconnect();
			Destroy(gameObject);
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	void ProcessAuthError(string error) {
		_errorMessage = error + "\n Please write a correct Twitter username and password in config.json.";
	}
	
	void OnGUI() {
		GUI.Label(_errorRect, _errorMessage);
	}
}
