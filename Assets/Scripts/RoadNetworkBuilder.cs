using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadNetworkBuilder : MonoBehaviour {

	public List<RoadBuilder> builders;

	public int blockSize;
	public int maxAge;

	public float chanceForward;
	public float chanceTurn;
	public float chanceDie;
	public float chanceFork;
	public float chanceRight;
	public float chanceLeft;
	public int averageCoins;


	// Use this for initialization
	void Start () {
		builders[0].makeBackLoop();
		for( int i = 0; i < blockSize && builders[0].currentSegment != null; i++ ) {
			builders[0].currentSegment = builders[0].buildRoadForward(); // generates and moves head
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach( RoadBuilder builder in builders ) {
			instructBuilder( builder );
		}
	}

	void instructBuilder( RoadBuilder builder ) {
		float totalDirect = chanceDie + chanceFork + chanceForward + chanceTurn;
		float direct = Random.Range( 0f, totalDirect );
		RoadSegment seg = null;
		if        ( direct < chanceForward ) { // forward
			Debug.Log( builder + " goes forward" );
			for( int i = 0; i < blockSize && builder.currentSegment != null; i++ ) {
				seg = builder.buildRoadForward(); // generates and moves head
			}
		} else if ( direct < chanceForward + chanceTurn ) { // turn
			if( turnRight() ) { // right
				Debug.Log( builder + " turns right" );
				seg = builder.buildRoadRight();
			} else { // left
				Debug.Log( builder + " turns left" );
				seg = builder.buildRoadLeft();
			}
//		} else if ( direct < chanceForward + chanceTurn + chanceFork ) { // fork
//			if( turnRight() ) {
//				Debug.Log( builder + " forks right" );
//				builders.Add( newBuilderFromSegment( builder, builder.buildRoadRight() ) );
//			} else {
//				Debug.Log( builder + " forks left" );
//				builders.Add( newBuilderFromSegment( builder, builder.buildRoadLeft() ) );
//			}
		} else if ( direct < chanceForward + chanceTurn + chanceFork + chanceDie ) { // die
			Debug.Log( builder + " dies" );
//			builders.Remove( builder );
//			Destroy( builder.gameObject );
		}

		if( seg != null ) {
			builder.currentSegment = seg;
			builder.generateCoins( Random.Range( Mathf.Min ( 0, averageCoins - 3), Mathf.Max (7, averageCoins + 3 ) ) );
		}
		if( builder.age >= maxAge ) {
			Debug.Log( builder + " aged out at " + builder.age );
			builder.makeExit();
			builders.Remove( builder );
			Destroy( builder );
		}
	}

	RoadBuilder newBuilderFromSegment( RoadBuilder builder, RoadSegment theSeg ) {
		GameObject theGamOb = Instantiate( builder.gameObject ) as GameObject;
		theGamOb.GetComponent<RoadBuilder>().age = 0;
		theGamOb.GetComponent<RoadBuilder>().currentSegment = theSeg;

		return theGamOb.GetComponent<RoadBuilder>();
	}

	bool turnRight() {
		float seed = Random.Range( 0f, chanceLeft + chanceRight );
		return ( seed < chanceRight );
	}


}
