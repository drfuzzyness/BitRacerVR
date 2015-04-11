using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter( Collision col ) {
		Destroy( col.gameObject );
	}

	void OnTriggerEnter( Collider col ) {
		Destroy( col.gameObject );
	}
}
