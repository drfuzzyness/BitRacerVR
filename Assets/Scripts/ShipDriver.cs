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
	public AudioSource DeathSFX;

	private Quaternion previousHeadRot = new Quaternion();
	private Rigidbody rbod;

	// Use this for initialization
	void Start () {
		rbod = GetComponent<Rigidbody>();
		Cardboard.SDK.Recenter();
	}
	

	void OnCollisionEnter( Collision col) {
		Debug.Log( gameObject + " collided with " + col.collider.gameObject );
		DeathSFX.Play();
		levelManager.lostLevel();
	}

	void FixedUpdate() {


		if( rbod.velocity.magnitude < maxForwardVelocity && !stopped ) {
			rbod.AddForce( transform.forward * acceleration, ForceMode.Acceleration );
		}
	}

	void LateUpdate() {
//		Quaternion rot = Cardboard.SDK.HeadRotation;
//		Vector3 difference = rot.eulerAngles - transform.eulerAngles;
		//		difference.x = 0;
		//		difference.y = Mathf.Clamp( difference.y, -angularVelocity, angularVelocity );
		//		difference.z = difference.y / 10f;
		//		Debug.Log ( difference );
		
		if( !stopped ) {
//			transform.Rotate( difference, Space.Self );
//			Vector3 headRot = Cardboard.SDK.HeadRotation.eulerAngles;
//			Vector3 currentRot = transform.eulerAngles;
//		
//			Vector3 applied = ( headRot - previousHeadRot.eulerAngles );
//			Debug.Log( applied );
//			transform.Rotate( applied, Space.Self );
//			Quaternion effrot = Cardboard.SDK.HeadRotation;
//			effrot.y = 0f;
//			GetComponent<Rigidbody>().MoveRotation( Quaternion.Euler( eulrot ) );
//			transform.localEulerAngles = eulrot;

			// Quaternion difference = Quaternion.Inverse( Cardboard.SDK.HeadPose.Orientation ) * previousHeadRot;
            // Debug.Log( Cardboard.SDK.HeadPose.Orientation.eulerAngles );

			// float tiltAngle = Cardboard.SDK.HeadPose.Orientation.eulerAngles.z;
			// float backAngle = Cardboard.SDK.HeadPose.Orientation.eulerAngles.x;


            // New code for rotation
			// float headRotationInY = Cardboard.SDK.HeadPose.Orientation.eulerAngles.y;
			// Quaternion headRotationJustY = Quaternion.Euler(0, headRotationInY, 0);

			// transform.rotation = Cardboard.SDK.HeadPose.Orientation * Quaternion.Inverse( headRotationJustY );
            
            // Strange errors, just going to ignore this math for now 
            transform.rotation = Cardboard.SDK.HeadPose.Orientation;
		}
		Vector3 euler = transform.eulerAngles;
		previousHeadRot = Cardboard.SDK.HeadPose.Orientation;
		if( clampXRot )
			euler.x = 0f;
//		if( !stopped )
//			transform.eulerAngles = euler;
	}
}
