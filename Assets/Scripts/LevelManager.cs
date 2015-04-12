using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public int currentLevel;

	public int ageAddition;
	public float maxSpeedAddition;

	public Transform playerShipMesh;
	public ScoreManager scoreManager;

	public void wonLevel() {
		Debug.Log( "Won level" );
		PlayerPrefs.SetInt( "Score", scoreManager.score );
		Application.LoadLevel( Application.loadedLevel + 1 );
	}

	IEnumerator wonLevelScreen() {
		yield return null;
	}

	public void lostLevel() {
		Debug.Log( "Lost level" );
		PlayerPrefs.SetInt( "Score", 0 );
	}

	// Use this for initialization
	void Start () {
		scoreManager.score = PlayerPrefs.GetInt( "Score", 0 );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
