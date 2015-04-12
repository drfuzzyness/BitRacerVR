using UnityEngine;
using System.Collections;

public class DirectionIndicator : MonoBehaviour {

	private Rigidbody rbod;
	
	// Use this for initialization
	void Start () {
		rbod = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		Quaternion rot = Cardboard.SDK.HeadRotation;
//		Vector3 difference = rot.eulerAngles - transform.eulerAngles;
		//		difference.x = 0;
		//		difference.y = Mathf.Clamp( difference.y, -angularVelocity, angularVelocity );
		//		difference.z = difference.y / 10f;;
		
//		transform.Rotate( difference, Space.Self );
		transform.rotation = rot;
//		Vector3 euler = transform.eulerAngles;
//		euler.x = 0f;
//		transform.eulerAngles = euler;

	}
}
