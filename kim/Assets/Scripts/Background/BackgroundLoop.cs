using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BackgroundLoop : MonoBehaviour {
	
	public Transform background;
	public Transform mainCamera;
	public float backgroundSpeed = 1.0f;
	private Camera camera;
	
	
	public int BACKGROUND_POOL = 3;
	public int SCREEN_OFFSET = 60;
	
	public int BACKGROUND_X = 28;
	
	private Queue<Transform> backgrounds;
	
	public float Z_POS = 2;
	public float Y_POS = 3;
	public float furthest_X = 0;
	
	// Use this for initialization
	void Start () {
		
		furthest_X = PlayerScript.distanceTraveled;
		
		backgrounds = new Queue<Transform>(BACKGROUND_POOL);
		Vector3 tempPos = new Vector3(-15,0,0);
		
		for(int i = 0; i < BACKGROUND_POOL; i ++){
			Transform t = (Transform) Instantiate(background);
			t.localPosition = tempPos;
			backgrounds.Enqueue(t);
		}
		
			
	}
	
	// Update is called once per frame
	void Update () {
		if(furthest_X < PlayerScript.distanceTraveled + SCREEN_OFFSET){
			Vector3 newPos = new Vector3();
			newPos.x = furthest_X;
			newPos.y = Y_POS;
			newPos.z = Z_POS;
			
			Transform b = backgrounds.Dequeue();
			b.localPosition = newPos;
			
			furthest_X += BACKGROUND_X;
			Debug.Log("background"+ BACKGROUND_X + "furthestX"+furthest_X+ "dist Traveled"+ PlayerScript.distanceTraveled);
			
			backgrounds.Enqueue(b);
		}
		
	}
}
