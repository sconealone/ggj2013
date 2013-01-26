using UnityEngine;
using System.Collections;

public class WaveBehavior : MonoBehaviour {
	
	public static float waveSpeed = 4.0f;
	private bool isGameOver = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position +=  transform.right * waveSpeed * Time.deltaTime;
		
	}
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log(collision.gameObject.tag.ToString());
	}
}
