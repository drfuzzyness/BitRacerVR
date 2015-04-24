using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public int currentLevel;
	public int lives;

	public int ageAddition;
	public float playerBaseSpeed;
	public float maxSpeedAddition;
	public int baseCoins;
	public int coinsAddition;
	public int baseMines;
	public int minesAddition;
	public int baseColumns;
	public int columnsAddition;

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
	public Text livesText;
	private bool died;

	public AudioSource music;

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
		saveMusicPos();
		Application.LoadLevel( chooseNextLevel() );
	}

	public void lostLevel() {
		Debug.Log( "Lost level" );

		player.GetComponent<ShipDriver>().stopped = true;
		levelEndText.text = "CRASHED\n-1 life!";
		pressButton.SetActive( true );
		levelEndText.gameObject.SetActive( true );

		if( !died ) // basically, don't charge the player twice for dying
			StartCoroutine( "lostLevelScreen" );
	}

	IEnumerator lostLevelScreen() {
		died = true;
		lives--;
		PlayerPrefs.SetInt( "Lives", lives );
		livesText.text = lives.ToString();
		while( !Cardboard.SDK.CardboardTriggered )
			yield return null;
		saveMusicPos();

		if( lives > 0 ) {
			Application.LoadLevel( Application.loadedLevel );
		} else {
		PlayerPrefs.SetInt( "Score", 0 );
		if( PlayerPrefs.GetInt( "HighScore", 0 ) < scoreManager.score ) {
			PlayerPrefs.SetInt( "HighScore", scoreManager.score );
		}
		PlayerPrefs.SetInt( "Score", 0 );
		
		Application.LoadLevel( 0 );
		}
	}

	// Use this for initialization
	void Start () {
		died = false;
		lives = PlayerPrefs.GetInt("Lives", lives);
		currentLevel = PlayerPrefs.GetInt("Level", 1);
		scoreManager.score = PlayerPrefs.GetInt( "Score", 0 );
		scoreManager.updateDisplays();
		setupLevelDifficulty();
//		Debug.Log ( chooseNextLevel() + "" );
	}

	void setupLevelDifficulty() {
		roadNetworkBuilder.maxAge = ageAddition * currentLevel;
		roadNetworkBuilder.averageCoins = baseCoins + coinsAddition * currentLevel;
		roadNetworkBuilder.averageMines = baseMines + minesAddition * currentLevel;
		roadNetworkBuilder.averageColumns = baseColumns + Mathf.FloorToInt( columnsAddition * currentLevel / 1.25f );

		player.GetComponent<ShipDriver>().maxForwardVelocity = playerBaseSpeed + maxSpeedAddition * currentLevel;
		player.GetComponent<ShipDriver>().acceleration = player.GetComponent<ShipDriver>().maxForwardVelocity / 2;


		livesText.text = lives.ToString();
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

	void saveMusicPos () {
		PlayerPrefs.SetInt( "MusicPos", music.timeSamples );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
