using UnityEngine;
using System.Collections;

public class EnemyMine : MonoBehaviour {

	public LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
