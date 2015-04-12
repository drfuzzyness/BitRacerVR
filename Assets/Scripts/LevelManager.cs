using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public int currentLevel;

	public int ageAddition;
	public float playerBaseSpeed;
	public float maxSpeedAddition;
	public int baseCoins;
	public int coinsAddition;
	public int baseMines;
	public int minesAddition;

	public List<int> scenes;

	public Transform playerShipMesh;
	public GameObject player;
	public ScoreManager scoreManager;
	public RoadNetworkBuilder roadNetworkBuilder;

	public Text levelEndText;
	public GameObject pressButton; 
	public Text levelNumberText;
	public Text maxSpeedText;
	public Text levelLengthText;

	public void wonLevel() {
		Debug.Log( "Won level" );
		PlayerPrefs.SetInt( "Score", scoreManager.score );
		levelEndText.text = "won\nlevel!";
		pressButton.SetActive( true );
		levelEndText.gameObject.SetActive( true );
		StartCoroutine( "wonLevelScreen" );

	}

	IEnumerator wonLevelScreen() {
		player.GetComponent<ShipDriver>().stopped = true;
		while( !Cardboard.SDK.CardboardTriggered ) {
			yield return null;
			playerShipMesh.position += playerShipMesh.forward;
		}
		if( PlayerPrefs.GetInt( "HighScore", 0 ) < scoreManager.score )
			PlayerPrefs.SetInt( "HighScore", scoreManager.score );
		PlayerPrefs.SetInt( "Score", scoreManager.score );
		PlayerPrefs.SetInt( "Level", currentLevel + 1 );
		Application.LoadLevel( chooseNextLevel() );
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

	IEnumerator lostLevelScreen() {
		while( !Cardboard.SDK.CardboardTriggered )
			yield return null;
		if( PlayerPrefs.GetInt( "HighScore", 0 ) < scoreManager.score )
			PlayerPrefs.SetInt( "HighScore", scoreManager.score );
		PlayerPrefs.SetInt( "Score", 0 );
		Application.LoadLevel( 0 );
	}

	// Use this for initialization
	void Start () {
		currentLevel = PlayerPrefs.GetInt("Level", 1);
		scoreManager.score = PlayerPrefs.GetInt( "Score", 0 );
		scoreManager.updateDisplays();
		setupLevelDifficulty();
//		Debug.Log ( chooseNextLevel() + "" );
	}

	void setupLevelDifficulty() {
		roadNetworkBuilder.maxAge = ageAddition * currentLevel;
		roadNetworkBuilder.averageCoins = baseCoins + coinsAddition * currentLevel;
		player.GetComponent<ShipDriver>().maxForwardVelocity = playerBaseSpeed + maxSpeedAddition * currentLevel;
		player.GetComponent<ShipDriver>().acceleration = player.GetComponent<ShipDriver>().maxForwardVelocity / 2;
		roadNetworkBuilder.averageMines = baseMines + minesAddition * currentLevel;


		levelNumberText.text = currentLevel.ToString();
		levelLengthText.text = roadNetworkBuilder.maxAge.ToString();
		maxSpeedText.text = (player.GetComponent<ShipDriver>().maxForwardVelocity * 100).ToString();
	}

	int chooseNextLevel() {
		int next = Random.Range( 0, scenes.Count );
//		while( scenes[ next ] == Application.loadedLevel )
//			next = Random.Range( 0, scenes.Count );
		return scenes[ next ];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
