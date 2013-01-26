using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public static float defaultSpeed = 5.0f;
	public static float slowDownSpeed = 3.0f;
	
	private float speed = 5.0f;
	
	public float gravity = 9.80f;
	public float jumpSpeed = 8.0f;
	
	private Vector3 moveDirection =  Vector3.zero;
	
	private bool isGameOver = false;
	
	public static float distanceTraveled;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(5f*Time.deltaTime, 0f, 0f);
		Move();

		
	}
	
//	void OnGUI(){
//		if (isGameOver) {
///      	GUI.Label(Rect(0,0, 100, 20), "Game Over!");
//		}
//	}
	
	
	void Move(){
		
		if (Input.GetKey(KeyCode.A)){
			speed = slowDownSpeed;
		}
		else{
			speed = defaultSpeed;
			
		}

		
		transform.position += transform.right * speed * Time.deltaTime;
		distanceTraveled += speed*Time.deltaTime;
		
			CharacterController controller = GetComponent<CharacterController>();
		
			if (controller.isGrounded){
				moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            	moveDirection = transform.TransformDirection(moveDirection);
            	moveDirection *= speed;
            	if (Input.GetButton("Jump"))
                	moveDirection.y = jumpSpeed;
			}
			
			moveDirection.y -= gravity * Time.deltaTime;
        	controller.Move(moveDirection * Time.deltaTime);
		
			
			
		
		
	}
	
	/*void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.gameObject.tag == "Wave"){
			Debug.Log("you loss");
		}
	}*/
	
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name == "Wave"){
			isGameOver = true;
		}
	}
	
	
}
