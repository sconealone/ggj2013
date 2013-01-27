using UnityEngine;
using System.Collections;

public class gameoverScript : MonoBehaviour {
	
	private string START_GAME_SCENE = "HCLevel1";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI(){
		GUI.Box (new Rect (10,10,100,90), "Loader Menu");
		if(GUI.Button (new Rect (20,40,80,20), "Try Again!")){
			Application.LoadLevel(START_GAME_SCENE);
		}
	}
}
