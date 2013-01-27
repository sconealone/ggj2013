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
	
	public int CEILING_HEIGHT = 12;
	
	
	private bool hasStarted = false;
	/*
	 * 
	 * tiles for obstacles
	 * 
	 * E = empty (no obstacle)
	 * S = shelf (1 block)
	 * 
	 */
	public static string LEVEL_TILES = "EEEEEEEEESSSEEEEESSEEESSEEEEESSEEEEEEEEE" +
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
		cielingPosition.y = cielingPosition.y + CEILING_HEIGHT;
		
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

		if(index < LEVEL_TILES.Length){
			char[] tiles = LEVEL_TILES.ToCharArray();
			switch(tiles[index]){
			case 'S':
				Transform t = (Transform) obstaclePool.Dequeue();
				Debug.Log(obstacle_count++);
				Vector3 newPosition = floorPosition;
				
				t.localScale = RandomVector(1,3,1,3,1,1);
				newPosition.y = floorPosition.y + t.localScale.y; 
				t.localPosition = newPosition;
				
				
				obstaclePool.Enqueue(t);
				break;
			default:
				break;
			}

		}
		
	}
	
	private Vector3 RandomVector(float min_x, float max_x, float min_y, float max_y,float min_z, float max_z){
		
		return new Vector3(Random.Range(min_x, max_x), Random.Range(min_y,max_y), Random.Range(min_z, max_z));
	}
}
