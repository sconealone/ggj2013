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
	
	
	void OnGUI(){
		if (isGameOver) {
      	GUI.Box(new Rect( (Screen.width * 0.5f) - 60f, Screen.height - 35f, 120f, 25f ), "Game Over");
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		//Debug.Log(collision.gameObject.tag.ToString());
		if (collision.gameObject.tag == "Player"){
			isGameOver = true;
		}
	}
}
