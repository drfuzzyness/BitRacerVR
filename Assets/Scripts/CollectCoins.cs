using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectCoins : MonoBehaviour {
	

	public ScoreManager scoreManager;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter( Collider col ) {
		if( col.gameObject.tag == "Coin" ) {
			scoreManager.score += col.gameObject.GetComponent<Coin>().points;
			Destroy( col.gameObject );
			scoreManager.updateDisplays();
		}
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
