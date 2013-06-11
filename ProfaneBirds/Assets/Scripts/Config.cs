using UnityEngine;
using System.Collections;
using Streamer;
using System;
using System.IO;

public class Config : GameObjectSingleton<Config> {
	
	public JSONObject config { get; private set;}
	
	void Awake () {
		FileInfo configJSON = new FileInfo ("config.json");
		if (configJSON.Exists) {
        	StreamReader reader = configJSON.OpenText();
			config = new JSONObject(reader.ReadToEnd());
		} else {
			config = null;	
		}
	}
}
