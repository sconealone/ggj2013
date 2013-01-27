using UnityEngine;
using System.Collections;

public class CollectableBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collisionInfo){
		if (collisionInfo.gameObject.name == "Player"){
			GameObject.Destroy(gameObject);
			
		}
	}
}
