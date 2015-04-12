using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class PitchWithVelocity : MonoBehaviour {
	public float startingPitch = .8f;
	public float maxPitch = 2f;
	void Start() {
		GetComponent<AudioSource>().pitch = startingPitch;
	}
	void Update() {
		float ratio = GetComponent<Rigidbody>().velocity.magnitude / GetComponent<ShipDriver>().maxForwardVelocity;
		GetComponent<AudioSource>().pitch = Mathf.Lerp( startingPitch, maxPitch,
		                                               ratio );
		
	}
}