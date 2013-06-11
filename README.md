Profane Birds
=======

Hack for [Art Hack Day Stockholm](http://arthackday.net/stockholm/)


![Profane Bird](http://25.media.tumblr.com/bb21f9cc6c9f6c11ff63596a21765d7b/tumblr_mmd5ymxl8I1rlcnubo2_500.png)

### Description:
Grab the tweets of people saying "f*ck" and converting them into movements in Angry Birds => number of bad words determined the force and length of the tweet the angle. 

#### Instructions:
1. Set Twitter **username** and **password** in `config.json`
2. Run the app and choose a level
3. Enjoy!
4. If you want to return to level selection, press 0

#### Configuration:

You can change the search term and the words used to increase force by modifying file `config.json`<br>
```
{
	"twitterAccount" : {
		"username":"INSERT_USERNAME",
		"password":"INSERT_PASSWORD"
	},
	"searchTerm":"team_iso",
	"scoreRegex":"example|example2|yet_another_example"
}
```

#### This project contains work of:

* **Angry Birds** textures and music by [Rovio](http://www.rovio.com/)
* **Streamer** Twitter streaming library, by [Eddie Cameron](http://grapefruitgames.com/)
* **Radical library** by [Mike Talbot](http://whydoidoit.com/)


#### Disclaimer:
_Angry Birds is property of Rovio. Profane Birds is not related with Rovio._<br>
Textures and music ripped from [Angry Birds Chrome](http://chrome.angrybirds.com/) version 
