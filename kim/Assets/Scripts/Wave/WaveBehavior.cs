using UnityEngine;
using System.Collections;

public class WaveBehavior : MonoBehaviour {
	
	public static float waveSpeed = 4.5f;
	private bool isGameOver = false;
    private float gameOverTimer = 0.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position +=  transform.right * waveSpeed * Time.deltaTime;	
        if (isGameOver)
        {
            gameOverTimer -= Time.deltaTime;
            if (gameOverTimer <= 0.0f)
            {
			    Application.LoadLevel("gameover");
                isGameOver = false;
            }
        }
	}

	
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name == "Player"){
                gameOverTimer = 5.0f;
                isGameOver = true;
                Destroy(GameObject.Find("Player"));
		}
        else
        {
            Destroy(collision.gameObject);
        }
	}
}
