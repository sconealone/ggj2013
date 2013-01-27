using UnityEngine;
using System.Collections;

public class WaveBehavior : MonoBehaviour {
	
	public static float waveSpeed = 4.5f;
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
		if (collision.gameObject.name == "Player"){
			isGameOver = true;
			Destroy(GameObject.Find("Player"));
		}
        else
        {
            Destroy(collision.gameObject);
        }
	}
}
