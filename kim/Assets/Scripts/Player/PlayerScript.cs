using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public static float defaultSpeed = 5.0f;
	public static float slowDownSpeed = 1.0f;
	
	private float speed = 5.0f;
	
	public float gravity = 9.80f;
	public float jumpSpeed = 8.0f;
	
	private Vector3 moveDirection =  Vector3.zero;
	
	public static float distanceTraveled;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(5f*Time.deltaTime, 0f, 0f);
		Move();
		distanceTraveled += 5f*Time.deltaTime;
		
		//print(speed);
		
		
	}
	
	
	void Move(){
		print("speed changed :");
		if (Input.GetKeyDown(KeyCode.A)){
			speed = slowDownSpeed;
		}
		else{
			speed = defaultSpeed;
		}
		Debug.Log(speed);
		transform.position += transform.right * speed * Time.deltaTime;
		
		
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
	
	
}
