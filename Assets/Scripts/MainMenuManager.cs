using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

	public Text highScore;

	// Use this for initialization
	void Start () {
		Cardboard.SDK.Recenter();
		highScore.text = PlayerPrefs.GetInt( "HighScore", 0 ).ToString();
		PlayerPrefs.SetInt("Level", 0 );
		PlayerPrefs.SetInt("Score", 0 );
	}
	
	// Update is called once per frame
	void Update () {
		if( Cardboard.SDK.CardboardTriggered ) {
			Application.LoadLevel( 1 );
		}
	}
}
