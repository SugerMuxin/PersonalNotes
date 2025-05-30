using UnityEngine;
using System.Collections;

public class PlayerCollisionCheck : MonoBehaviour {

	public bool mHitObstacle = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("hit enter, collider: " + other.gameObject.name);
		mHitObstacle = true;
	}

	void OnTriggerExit(Collider other)
	{
		//Debug.Log("hit exit");
		mHitObstacle = false;
	}
}
