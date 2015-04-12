using UnityEngine;
using System.Collections;

[RequireComponent(typeof( Rigidbody ) )]
public class ShipDriver : MonoBehaviour {

	public float angularVelocity;
	public float currentForwardVelocity;
	public float acceleration;
	public float maxForwardVelocity;
	public bool controlling;

	private Rigidbody rbod;

	// Use this for initialization
	void Start () {
		rbod = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void StepTurnLeft(float angle) {

	}

	void FixedUpdate() {


		if( rbod.velocity.magnitude < maxForwardVelocity ) {
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
		
		transform.Rotate( difference, Space.Self );
		Vector3 euler = transform.eulerAngles;
		euler.x = 0f;
		transform.eulerAngles = euler;
	}
}
