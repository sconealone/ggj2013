using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour {
	
    // Speeds
	public static float defaultSpeed = 7.0f;
	public static float slowDownSpeed = 5.0f;
    public static float destructableObjectPenalty = -3.0f;
	
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

    private String state;
    private HeartRateManager heartRateManager;

	
	// Use this for initialization
	void Start () {
	
		BPM = 120;
        state = "running";
        heartRateManager = new HeartRateManager(this);
	}
	// Update is called once per frame
	void Update () {
		//transform.Translate(5f*Time.deltaTime, 0f, 0f);

        CheckHitRecovery();
		UpdateBPM();
		Move();
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
		
    
        if (controller.isGrounded && !hitRecovery) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump")){
                moveDirection.y = jumpSpeed;
                BPM += BPM_JUMP_RATE;
                state = "jumping";
            }
        }
        
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
	}
	
	void OnCollisionEnter(Collision collisionInfo){
		Debug.Log("ori BPM: " + BPM);
		if (collisionInfo.gameObject.name == "Collectable"){
            heartRateManager.Collide(collisionInfo.gameObject.name);
		}
        else if (collisionInfo.gameObject.name.Equals("Obstacles"))
        {
            hitRecovery = true;
            hitRecoveryTimer = 0;
            speed = destructableObjectPenalty;
            state = "hitRecovery";
        }
        else if (collisionInfo.gameObject.name.Equals("Destructable Object"))
        {
            hitRecovery = true;
            hitRecoveryTimer = 0;
            speed = destructableObjectPenalty;
            state = "hitRecovery";
            heartRateManager.Collide(collisionInfo.gameObject.name);
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
