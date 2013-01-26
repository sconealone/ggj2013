using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FloorManager : MonoBehaviour {
	
	public Transform tile;
	public int recycleOffset;
	
	private Queue<Transform> tilePool;
	
	Vector3 nextPosition;
	public int TILE_POOL = 10;
	// Use this for initialization
	void Start () {
	
		tilePool = new Queue<Transform>(TILE_POOL);
		nextPosition = transform.localPosition;
		for(int i = 0; i < TILE_POOL; i++){
			Transform o = (Transform)Instantiate(tile);
			o.localPosition = nextPosition;
			nextPosition.x += o.localScale.x;
			tilePool.Enqueue(o);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(tilePool.Peek().localPosition.x + recycleOffset < PlayerScript.distanceTraveled){
			Transform o = (Transform)tilePool.Dequeue();
			o.localPosition = nextPosition; 
			nextPosition.x += o.localScale.x;
			tilePool.Enqueue(o);
		}
	}
}
