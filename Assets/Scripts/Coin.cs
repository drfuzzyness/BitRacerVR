using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public int points;
	public Vector3 spinSpeed;

	// Use this for initialization
	void Start () {
		StartCoroutine("grow");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate( spinSpeed * Time.deltaTime );
	}

	IEnumerator grow() {
		Vector3 orig = transform.localScale;
		transform.localScale = new Vector3( 0, 0, 0);
		for( int i = 0; i < 10; i++ ) {
			yield return null;
			transform.localScale += orig / 10;
		}
		transform.localScale = orig;
	}
}
