using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

	public Text highScore;
	public int startingLives;
	public AudioSource music;

	// Use this for initialization
	void Start () {
		Cardboard.SDK.Recenter();
		highScore.text = PlayerPrefs.GetInt( "HighScore", 0 ).ToString();
		PlayerPrefs.SetInt("Level", 0 );
		PlayerPrefs.SetInt("Score", 0 );
		PlayerPrefs.SetFloat( "MusicPos", 0f );
		PlayerPrefs.SetInt("Lives", startingLives );


	}
	
	// Update is called once per frame
	void Update () {
		if( Cardboard.SDK.CardboardTriggered ) {
			PlayerPrefs.SetInt( "MusicPos", music.timeSamples );
			Application.LoadLevel( 1 );
		}
	}
}
