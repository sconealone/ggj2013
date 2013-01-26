using UnityEngine;
using System.Collections;

public class WaveBehavior : MonoBehaviour {
	
	public static float waveSpeed = 4.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position +=  transform.right * waveSpeed * Time.deltaTime;
		
	}
}
