using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FloorManager : MonoBehaviour {
	
	private bool DEBUG = false;
	
	private int blockIndex;
	public Transform tile;
	public int recycleOffset;
	
	private Queue<Transform> obstaclePool;
	private Queue<Transform> floorPool;
	private Queue<Transform> cielingPool;
	
	Vector3 cielingPosition;
	Vector3 floorPosition;
	public int MAX_TILE_POOL_SIZE;
	private float FLOOR_Y;
	
	/*
	 * 
	 * tiles for obstacles
	 * 
	 * E = empty (no obstacle)
	 * S = shelf (1 block)
	 * 
	 */
	private string LEVEL_TILES = "EEEEEEEEEEEEEEEEEEEEEEEESSSEEEEESSEEEEEEEEEEEEEEESSEEEEESSEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEESSSEEEEESSEEEEEEEEEEEEEEESSEEEEESSEEEEEEEEEE";
	
	// Use this for initialization
	void Start () {
		blockIndex = 0;
		
		obstaclePool = new Queue<Transform>(MAX_TILE_POOL_SIZE);
		floorPool = new Queue<Transform>(MAX_TILE_POOL_SIZE);
		cielingPool = new Queue<Transform>(MAX_TILE_POOL_SIZE);
		
		floorPosition = transform.localPosition;
		cielingPosition = transform.localPosition;
		cielingPosition.y = cielingPosition.y + 9;
		
		FLOOR_Y = floorPosition.y;
		
		for(int i = 0; i < MAX_TILE_POOL_SIZE; i++){
			Transform o = (Transform)Instantiate(tile);
			o.localPosition = floorPosition;
			floorPosition.x += o.localScale.x;
			floorPool.Enqueue(o);
			
			Transform o1 = (Transform) Instantiate(tile);
			o1.localPosition = cielingPosition;
			cielingPosition.x += o1.localScale.x;
			cielingPool.Enqueue(o1);
			blockIndex++;
			
			obstaclePool.Enqueue((Transform) Instantiate(tile));
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(floorPool.Peek().localPosition.x + recycleOffset < PlayerScript.distanceTraveled){
			Transform o = (Transform)floorPool.Dequeue();
			o.localPosition = floorPosition; 
			floorPosition.x += o.localScale.x;
			floorPool.Enqueue(o);
			blockIndex++;
			
			
		}
		if(cielingPool.Peek().localPosition.x + recycleOffset < PlayerScript.distanceTraveled){
			Transform c = (Transform) cielingPool.Dequeue();
			c.localPosition = cielingPosition;
			cielingPosition.x += c.localScale.x;
			cielingPool.Enqueue(c);
		}
		if(DEBUG == true){
			//print(blockIndex);
			Debug.Log(floorPosition.y);
		}
		GenerateObstacle(blockIndex);
	}
	/**
	 * 
	 * 
	 */
	void GenerateObstacle(int index){
		char[] tiles = LEVEL_TILES.ToCharArray();
		switch(tiles[index]){
		case 'S':
			Transform t = (Transform) obstaclePool.Dequeue();
			
			Vector3 newPosition = floorPosition;
			newPosition.y = floorPosition.y + 1; 
			t.localPosition = newPosition;
			
			obstaclePool.Enqueue(t);
			break;
		default:
			break;
		}
		
	}
}
