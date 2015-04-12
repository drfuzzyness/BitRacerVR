using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	public float speed;
	public int damage;
	public ParticleSystem hitDecal;


	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter( Collision col ) {
		if( col.gameObject.tag == "Enemy" ) {
			Debug.Log( gameObject + " hit " + col.gameObject + " for " + damage);
//			col.gameObject.GetComponent<>();
		}
		Instantiate( hitDecal, col.contacts[0].point, transform.rotation );
		Destroy( gameObject );
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 vel = GetComponent<Rigidbody>().velocity;
		vel.z = speed;
		GetComponent<Rigidbody>().velocity = vel;
	}
}
