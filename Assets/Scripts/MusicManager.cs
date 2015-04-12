using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	private AudioSource music;


	void Awake() {
		music = GetComponent<AudioSource>();
		Debug.Log ( PlayerPrefs.GetInt( "MusicPos", 0 ) );
		music.timeSamples = PlayerPrefs.GetInt( "MusicPos", 0 );
		music.Play ();
	}

//	void OnDisable() {
//
//		PlayerPrefs.SetInt( "MusicPos", music.timeSamples );
//	}
}
