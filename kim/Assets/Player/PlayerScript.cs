using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	
	public static float distanceTraveled;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(5f*Time.deltaTime, 0f, 0f);
		distanceTraveled += 5f*Time.deltaTime;
	}
}
