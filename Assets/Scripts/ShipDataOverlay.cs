using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipDataOverlay : MonoBehaviour {

	public Text velocityText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int velocity = Mathf.RoundToInt( GetComponent<Rigidbody>().velocity.magnitude );
		velocityText.text = velocity.ToString();
	}
}
