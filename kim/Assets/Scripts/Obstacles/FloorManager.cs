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
	public int MAX_TILE_POOL_SIZE;
	// Use this for initialization
	void Start () {
		blockIndex = 0;
		
		obstaclePool = new Queue<Transform>(MAX_TILE_POOL_SIZE);
		floorPool = new Queue<Transform>(MAX_TILE_POOL_SIZE);
		cielingPool = new Queue<Transform>(MAX_TILE_POOL_SIZE);
		
		nextPosition = transform.localPosition;
		cielingPosition = transform.localPosition;
		cielingPosition.y = cielingPosition.y + 9;
		
		for(int i = 0; i < MAX_TILE_POOL_SIZE; i++){
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
