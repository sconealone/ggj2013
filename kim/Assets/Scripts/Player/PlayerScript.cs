using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
    // Speeds
	public static float defaultSpeed = 7.0f;
	public static float slowDownSpeed = 5.0f;
    public static float destructableObjectPenalty = -4.0f;
	
	private float speed = 5.0f;
	
	public float gravity = 9.80f;
	public float jumpSpeed = 8.0f;
	private float percentageFinished ;
	
	private Vector3 moveDirection =  Vector3.zero;
	
	
	public static float distanceTraveled;
	
	// bpm rates
	private float BPM;
	private float BPM_REG_RATE = 5;
	private float BPM_JUMP_RATE = 3;
    private const float BPM_OBSTACLE_PENALTY = 5;

	
    private bool hitRecovery = false;
    private float hitRecoveryTimer = 0;
    public static float hitRecoveryTimePenalty = 1.0f;
	
	// Use this for initialization
	void Start () {
	
		BPM = 120;
	}
	// Update is called once per frame
	void Update () {
		//transform.Translate(5f*Time.deltaTime, 0f, 0f);
        CheckHitRecovery();
		Move();
		UpdateBPM();
		percentageFinished = transform.localPosition.x / FloorManager.LEVEL_TILES.Length;
		
	}

    private void CheckHitRecovery()
    {
        if (hitRecovery)
        {
            hitRecoveryTimer += Time.deltaTime;
            if (hitRecoveryTimer > hitRecoveryTimePenalty) 
            {
                hitRecovery = false;
            }
        }
    }

	void UpdateBPM(){
		BPM += BPM_REG_RATE*Time.deltaTime;
	}
	void OnGUI(){
		GUI.Label(new Rect (0,0,100,20), "BPM: "+ BPM.ToString("0.00"));
		GUI.Label(new Rect ( 0 , 20, 100 , 50), "% left: "+ (percentageFinished*100).ToString("0.00"));
	}
	
	
	void Move(){
		
        CharacterController controller = GetComponent<CharacterController>();
        if (hitRecovery || !controller.isGrounded)
        {
            // don't change speed
        }
		else if (Input.GetKey(KeyCode.A)){
			speed = slowDownSpeed;
		}
		else{
			speed = defaultSpeed;
			
		}

		
		transform.position += transform.right * speed * Time.deltaTime;
		distanceTraveled += speed*Time.deltaTime;
		
    
        if (controller.isGrounded) {
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
	
	void OnCollisionEnter(Collision collisionInfo){
		Debug.Log("ori BPM: " + BPM);
		if (collisionInfo.gameObject.name == "Collectable"){
			BPM -= 20;	
		}
        else if (collisionInfo.gameObject.name.Equals("Obstacles"))
        {
            hitRecovery = true;
            hitRecoveryTimer = 0;
            speed = destructableObjectPenalty;
        }
        else if (collisionInfo.gameObject.name.Equals("Destructable Object"))
        {
            hitRecovery = true;
            hitRecoveryTimer = 0;
            speed = destructableObjectPenalty;
            BPM += BPM_OBSTACLE_PENALTY;
        }
		
		Debug.Log("new BPM: " + BPM);
	}
	
	
	
	
}
