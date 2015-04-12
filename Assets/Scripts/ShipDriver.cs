using UnityEngine;
using System.Collections;

[RequireComponent(typeof( Rigidbody ) )]
public class ShipDriver : MonoBehaviour {

	public float angularVelocity;
	public float currentForwardVelocity;
	public float acceleration;
	public float maxForwardVelocity;
	public bool stopped;
	public bool clampXRot;
	public LevelManager levelManager;

	private Rigidbody rbod;

	// Use this for initialization
	void Start () {
		rbod = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
//		if( Cardboard.SDK.CardboardTriggered ) {
//			if( stopped ) {
//				stopped = false;
//			} else {
//				stopped = true;
//			}
//		}
	}

	void OnCollisionEnter( Collision col) {
		Debug.Log( gameObject + " collided with " + col.collider.gameObject );
		levelManager.lostLevel();
	}

	void FixedUpdate() {


		if( rbod.velocity.magnitude < maxForwardVelocity && !stopped ) {
			rbod.AddForce( transform.forward * acceleration, ForceMode.Acceleration );
		}
	}

	void LateUpdate() {
		Quaternion rot = Cardboard.SDK.HeadRotation;
		Vector3 difference = rot.eulerAngles - transform.eulerAngles;
		//		difference.x = 0;
		//		difference.y = Mathf.Clamp( difference.y, -angularVelocity, angularVelocity );
		//		difference.z = difference.y / 10f;
		//		Debug.Log ( difference );
		
		if( !stopped )
			transform.Rotate( difference, Space.Self );
		Vector3 euler = transform.eulerAngles;
		if( clampXRot )
			euler.x = 0f;
		if( !stopped )
			transform.eulerAngles = euler;
	}
}
