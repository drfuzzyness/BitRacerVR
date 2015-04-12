using UnityEngine;
using System.Collections;

public class SelfDestructAfter : MonoBehaviour {

	public float destructAfter;

	// Use this for initialization
	void Start () {
		StartCoroutine( "Destruct" );
	}

	IEnumerator Destruct() {
		yield return new WaitForSeconds( destructAfter );
		Destroy( gameObject );
	}
}
