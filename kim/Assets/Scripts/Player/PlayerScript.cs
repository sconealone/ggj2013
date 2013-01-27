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
	
	// bpm rates
	private float BPM;
	private float BPM_REG_RATE = 2;
	private float BPM_JUMP_RATE = 4;
	// Use this for initialization
	void Start () {
	
		BPM = 70;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(5f*Time.deltaTime, 0f, 0f);
		Move();
		UpdateBPM();
		
	}
	void UpdateBPM(){
		BPM += BPM_REG_RATE*Time.deltaTime;
		
	}
	void OnGUI(){
		GUI.Label(new Rect (0,0,100,50), "BPM"+ BPM.ToString("0..00"));
	}
	
	
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
            	if (Input.GetButton("Jump")){
				 	moveDirection.y = jumpSpeed;
					BPM += BPM_JUMP_RATE;
				}
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
