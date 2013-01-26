using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FloorManager : MonoBehaviour {
	
	private int blockIndex;
	public Transform tile;
	public int recycleOffset;
	
	private Queue<Transform> obstaclePool;
	private Queue<Transform> floorPool;
	private Queue<Transform> cielingPool;
	
	Vector3 cielingPosition;
	Vector3 nextPosition;
	public int TILE_POOL;
	// Use this for initialization
	void Start () {
		floorPool = new Queue<Transform>(TILE_POOL);
		cielingPool = new Queue<Transform>(TILE_POOL);
		
		nextPosition = transform.localPosition;
		cielingPosition = transform.localPosition;
		cielingPosition.y = cielingPosition.y + 9;
		
		for(int i = 0; i < TILE_POOL; i++){
			Transform o = (Transform)Instantiate(tile);
			o.localPosition = nextPosition;
			nextPosition.x += o.localScale.x;
			floorPool.Enqueue(o);
			
			Transform o1 = (Transform) Instantiate(tile);
			o1.localPosition = cielingPosition;
			cielingPosition.x += o1.localScale.x;
			cielingPool.Enqueue(o1);
			blockIndex++;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(floorPool.Peek().localPosition.x + recycleOffset < PlayerScript.distanceTraveled){
			Transform o = (Transform)floorPool.Dequeue();
			o.localPosition = nextPosition; 
			nextPosition.x += o.localScale.x;
			floorPool.Enqueue(o);
			blockIndex++;
			
			
		}
		if(cielingPool.Peek().localPosition.x + recycleOffset < PlayerScript.distanceTraveled){
			Transform c = (Transform) cielingPool.Dequeue();
			c.localPosition = cielingPosition;
			cielingPosition.x += c.localScale.x;
			cielingPool.Enqueue(c);
		}
		print(blockIndex);
	}
}
