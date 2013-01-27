using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour {
	
    // Speeds
	public static float defaultSpeed = 7.0f;
	public static float slowDownSpeed = 4.0f;
    public static float destructableObjectPenalty = -3.0f;
	
	private float speed = 5.0f;
	
	public float gravity = 9.80f;
	public float jumpSpeed = 8.0f;
	private float percentageFinished ;
	
	private Vector3 moveDirection =  Vector3.zero;
	
	
	public static float distanceTraveled;
	
    private bool hitRecovery = false;
    private float hitRecoveryTimer = 0;
    public static float hitRecoveryTimePenalty = 0.6f;

    private String state;
    private HeartRateManager heartRateManager;
    private float BPM;

	
	// Use this for initialization
	void Start () {
	
        state = "running";
        heartRateManager = new HeartRateManager(this);
        BPM = heartRateManager.GetCurrentHeartRate();
	}
	// Update is called once per frame
	void FixedUpdate () {
		//transform.Translate(5f*Time.deltaTime, 0f, 0f);

        CheckHitRecovery();
		UpdateBPM();
		Move();
		percentageFinished = transform.localPosition.x / FloorManager.LEVEL_TILES.Length;
		TrackPlayer();
	}
	
	private void TrackPlayer()
	{
		GameObject camera = GameObject.Find("Main Camera");
		camera.transform.Translate(speed*Time.deltaTime, 0, 0);
	}
	
    private void CheckHitRecovery()
    {
        if (hitRecovery)
        {
            hitRecoveryTimer -= Time.deltaTime;
            if (hitRecoveryTimer <= 0.0f) 
            {
                hitRecovery = false;
                state = "running";
            }
        }
    }

	void UpdateBPM(){
        if (heartRateManager.Update(Time.deltaTime))
        {
            state = "recoverStamina";
        }
        BPM = heartRateManager.GetCurrentHeartRate();
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
        else if (state.Equals("recoverStamina"))
        {
            speed = 0.0f;
        }
		else if (Input.GetKey(KeyCode.A)){
			speed = slowDownSpeed;
            state = "jogging";
		}
		else{
			speed = defaultSpeed;
            state = "running";
			
		}

		
		transform.position += transform.right * speed * Time.deltaTime;
		distanceTraveled += speed*Time.deltaTime;
		
    
        if (controller.isGrounded && !hitRecovery && !state.Equals("recoverStamina")) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump")){
                moveDirection.y = jumpSpeed;
                state = "jumping";
            }
        }
        
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
	}
	
	void OnCollisionEnter(Collision collisionInfo){
		
		if (collisionInfo.gameObject.name == "Swag"){
            heartRateManager.AssignPenalty(collisionInfo.gameObject.name);
		}
        else if (collisionInfo.gameObject.name.Equals("Table") ||
				collisionInfo.gameObject.name.Equals("Chair") )
        {
            hitRecovery = true;
            hitRecoveryTimer = hitRecoveryTimePenalty;
            speed = destructableObjectPenalty;
            state = "hitRecovery";
        }
        else if (collisionInfo.gameObject.name.Equals("Computer") ||
				collisionInfo.gameObject.name.Equals("Fridge")
			)
        {
            hitRecovery = true;
            hitRecoveryTimer = hitRecoveryTimePenalty;
            speed = destructableObjectPenalty;
            state = "hitRecovery";
            heartRateManager.AssignPenalty(collisionInfo.gameObject.name);
        }
        else if (collisionInfo.gameObject.name.Equals("Light"))
        {
            hitRecovery = true;
            hitRecoveryTimer = 2*hitRecoveryTimePenalty;
            state = "hitRecovery";
            heartRateManager.AssignPenalty(collisionInfo.gameObject.name);
        }
	}
	
	public float GetSpeed()
    {
        return speed;
    }

    public String GetState()
    {
        return state;
    }
	
    public void SetState(String aState)
    {
        state = aState;
    }
	
}
