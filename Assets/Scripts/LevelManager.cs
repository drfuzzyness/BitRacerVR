using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public int currentLevel;

	public int ageAddition;
	public float maxSpeedAddition;

	public Transform playerShipMesh;
	public GameObject player;
	public ScoreManager scoreManager;

	public Text levelEndText;
	public GameObject pressButton; 

	public void wonLevel() {
		Debug.Log( "Won level" );
		PlayerPrefs.SetInt( "Score", scoreManager.score );
		levelEndText.text = "won\nlevel!";
		pressButton.SetActive( true );
		levelEndText.gameObject.SetActive( true );
//		Application.LoadLevel( Application.loadedLevel + 1 );
	}

	IEnumerator wonLevelScreen() {
		yield return null;
	}

	public void lostLevel() {
		Debug.Log( "Lost level" );
		PlayerPrefs.SetInt( "Score", 0 );
		player.GetComponent<ShipDriver>().stopped = true;
		levelEndText.text = "ship\ndestroy!";
		pressButton.SetActive( true );
		levelEndText.gameObject.SetActive( true );
		StartCoroutine( "lostLevelScreen" );
	}

	public void lostLevelScreen() {

	}

	// Use this for initialization
	void Start () {
		scoreManager.score = PlayerPrefs.GetInt( "Score", 0 );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
