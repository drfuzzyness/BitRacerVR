using UnityEngine;
using System.Collections;

public class RotationFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.rotation = Cardboard.SDK.HeadPose.Orientation;
	}
}
