using UnityEngine;
using System.Collections;

public class RoadBuilder : MonoBehaviour {

	public GameObject roadPrefab;
	public RoadSegment currentSegment;
	public int age;
	public GameObject exitPrefab;

	public RoadSegment buildRoadForward() {
		if( canBuildRoad( currentSegment.transform.forward ) ) {
			GameObject newRoadGameObj = Instantiate( roadPrefab,
			                                        currentSegment.transform.position + currentSegment.transform.forward * currentSegment.size,
			                                        currentSegment.transform.rotation ) as GameObject;
			RoadSegment newRoad = newRoadGameObj.GetComponent<RoadSegment>();

			currentSegment.nodeRight.nextNodes.Add( newRoad.nodeRight );
			currentSegment.nodeRight.isConnected = true;
			newRoad.nodeLeft.nextNodes.Add( currentSegment.nodeLeft );
			currentSegment.sidewalkFront.gameObject.SetActive( false );
			currentSegment.nodeLeft.isConnected = true;
			newRoad.sidewalkBack.gameObject.SetActive( false );

			currentSegment = newRoad;
			age++;
			return currentSegment;
		}
//		makeFrontLoop();
		return null;
	}

	public RoadSegment buildRoadRight() {
		if( canBuildRoad( currentSegment.transform.right ) ) {
			Quaternion newRoadDirection = currentSegment.transform.rotation;
			Vector3 rotation = newRoadDirection.eulerAngles;
			rotation.y += 90f;
			newRoadDirection.eulerAngles = rotation;

			GameObject newRoadGameObj = Instantiate( roadPrefab,
			                                        currentSegment.transform.position + currentSegment.transform.right * currentSegment.size,
			                                        newRoadDirection ) as GameObject;
			RoadSegment newRoad = newRoadGameObj.GetComponent<RoadSegment>();
			
			currentSegment.nodeRight.nextNodes.Add( newRoad.nodeRight );
			currentSegment.nodeRight.isConnected = true;
			newRoad.nodeLeft.nextNodes.Add( currentSegment.nodeLeft );
			currentSegment.sidewalkRight.gameObject.SetActive( false );
			currentSegment.nodeLeft.isConnected = true;
			newRoad.sidewalkBack.gameObject.SetActive( false );
			
			currentSegment = newRoad;
			age++;
			return currentSegment;
		}
//		makeFrontLoop();
		return null;
	}
	public RoadSegment buildRoadLeft() {
		if( canBuildRoad( -currentSegment.transform.right ) ) {
			Quaternion newRoadDirection = currentSegment.transform.rotation;
			Vector3 rotation = newRoadDirection.eulerAngles;
			rotation.y -= 90f;
			newRoadDirection.eulerAngles = rotation;
			
			GameObject newRoadGameObj = Instantiate( roadPrefab,
			                                        currentSegment.transform.position + -currentSegment.transform.right * currentSegment.size,
			                                        newRoadDirection ) as GameObject;
			RoadSegment newRoad = newRoadGameObj.GetComponent<RoadSegment>();
			
			currentSegment.nodeRight.nextNodes.Add( newRoad.nodeRight );
			currentSegment.nodeRight.isConnected = true;
			newRoad.nodeLeft.nextNodes.Add( currentSegment.nodeLeft );
			currentSegment.sidewalkLeft.gameObject.SetActive( false );
			currentSegment.sidewalkLeft.gameObject.SetActive( false );
			currentSegment.nodeLeft.isConnected = true;
			newRoad.sidewalkBack.gameObject.SetActive( false );
			
			currentSegment = newRoad;
			age++;
			return currentSegment;
		}
//		makeFrontLoop();
		return null;
	}

	bool canBuildRoad( Vector3 direction ) {
		Ray testAhead = new Ray( currentSegment.transform.position + direction.normalized * currentSegment.size * 0.5f, direction.normalized );
		return !Physics.Raycast( testAhead, currentSegment.size * 0.5f);
	}

	public void makeFrontLoop() {
		currentSegment.nodeRight.nextNodes.Add( currentSegment.nodeLeft );
	}

	public void makeBackLoop() {
		currentSegment.nodeLeft.nextNodes.Add( currentSegment.nodeRight );
	}

	public void makeExit() {
		currentSegment.sidewalkFront.gameObject.SetActive( false );
		Instantiate( exitPrefab, currentSegment.sidewalkFront.transform.position,
		            currentSegment.transform.rotation );
	}

	public void generateCoins( int numCoins = 3 ) {
		for( int i = 0; i < numCoins; i++ ) {
			int limiter = 0; // makes sure this doesn't run forever, if shit goes down
			int ranCoin = Random.Range( 0, currentSegment.coins.Count );
//			while( limiter < 20 && currentSegment.coins[ ranCoin ].gameObject.activeSelf ) {
//				ranCoin = Random.Range( 0, currentSegment.coins.Count );
//			}
			currentSegment.coins[ ranCoin ].gameObject.SetActive( true );
		}

	}

	public void generateMines( int mines = 3 ) {
		for( int i = 0; i < mines; i++ ) {
			int ranMine = Random.Range( 0, currentSegment.enemyMines.Count );
			currentSegment.enemyMines[ ranMine ].gameObject.SetActive( true );
		}
	}

	// Use this for initialization
	void Start () {
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
