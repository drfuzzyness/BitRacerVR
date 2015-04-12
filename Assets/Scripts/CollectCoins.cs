using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectCoins : MonoBehaviour {
	

	public ScoreManager scoreManager;
	public AudioSource CoinSFX;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter( Collider col ) {
		if( col.gameObject.tag == "Coin" ) {
			scoreManager.score += col.gameObject.GetComponent<Coin>().points;
			Destroy( col.gameObject );
			scoreManager.updateDisplays();
			CoinSFX.Play();
			CoinSFX.pitch = Random.Range( .8f, 1.2f );
		}
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
