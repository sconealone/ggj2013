using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FloorManager : MonoBehaviour {
	
	//debug variables
	private bool DEBUG = false;
	private int obstacle_count;
	//debug var end
	
	
	private int blockIndex;
	
	public Transform floor;
	public Transform obstacles;
	
	public int recycleOffset;
	
	private Queue<Transform> obstaclePool;
	private Queue<Transform> floorPool;
	private Queue<Transform> cielingPool;
	
	Vector3 cielingPosition;
	Vector3 floorPosition;
	public int MAX_TILE_POOL_SIZE;
	private float FLOOR_Y;
	
	
	private bool hasStarted = false;
	/*
	 * 
	 * tiles for obstacles
	 * 
	 * E = empty (no obstacle)
	 * S = shelf (1 block)
	 * 
	 */
	private string LEVEL_TILES = "EEEEEEEEESSSEEEEESSEEESSEEEEESSEEEEEEEEE" +
		"EEEEESSSEEEEESSEEEEEESSEEEEESSEEEEEEESSSEEEEESSE" +
		"EEEEEEEEEEEEEESSEEEEESSEEEEEEEESSSEEEEESSEEEEEEEEEEEEEEESSEEEEESSEEEEEEE" +
		"EEEEEEEEEEESSSEEEEESSEEEEEEEEEEEEEEESSEEEEESSEEEEEEEEEEEEEEESSSEEEEESSEEEEEEEEEEEEEEESSEEEEESSEEEEEEEEEEE" +
		"EEESSSEEEEESSEEEEEEEEEEEEEEESSEEEEESSEEEEESSSEEEEESSEEEEEEEEEEEEEEESSEEEEESSEEEEEEEEEEE" +
		"EEESSSEEEEESSEEEEEEEEEEEEEEESSEEEEESSEEEESSSEEEEESSEEEEEESSEEEEESSEEE";
	
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
			Transform o = (Transform)Instantiate(floor);
			o.localPosition = floorPosition;
			floorPosition.x += o.localScale.x;
			floorPool.Enqueue(o);
			
			Transform o1 = (Transform) Instantiate(floor);
			o1.localPosition = cielingPosition;
			cielingPosition.x += o1.localScale.x;
			cielingPool.Enqueue(o1);
			blockIndex++;
			
			obstaclePool.Enqueue((Transform)Instantiate(obstacles));
			
			GenerateObstacle(blockIndex);
			
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
				
				
				//generate obstacles on new tile creation
				
			}
			if(cielingPool.Peek().localPosition.x + recycleOffset < PlayerScript.distanceTraveled){
				Transform c = (Transform) cielingPool.Dequeue();
				c.localPosition = cielingPosition;
				cielingPosition.x += c.localScale.x;
				cielingPool.Enqueue(c);
				GenerateObstacle(blockIndex);
			}
			if(DEBUG == true){
				//print(blockIndex);
				Debug.Log(floorPosition.y);
			}
		
	}
	/**
	 * 
	 * 
	 */
	void GenerateObstacle(int index){
		char[] tiles = LEVEL_TILES.ToCharArray();
		index = index % LEVEL_TILES.Length;
		switch(tiles[index]){
		case 'S':
			Transform t = (Transform) obstaclePool.Dequeue();
			Vector3 newPosition = floorPosition;
			newPosition.y = floorPosition.y + 1; 
			t.localPosition = newPosition;
			t.localScale = Vector3.one * 3;
			//Debug.Log(obstacle_count++ + "Location:"+t.localPosition.x+":"+t.localPosition.y);
			Debug.Log(obstacle_count++ + "Floor Location:"+floorPosition.x+":"+floorPosition.y);

			obstaclePool.Enqueue(t);
			break;
		default:
			break;
		}
		
	}
}
