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
	
	private AudioSource heartBeat;
	private AudioSource jump;
    private HudManager hudManager;
    private WinTransitionManager winManager;

	
	// Use this for initialization
	void Start () {
		AudioSource[] auSource = GetComponents<AudioSource>();
		
		heartBeat = auSource[0];
		//jump = auSource[1];
		
        state = "running";
        heartRateManager = new HeartRateManager(this);
        BPM = heartRateManager.GetCurrentHeartRate();
        hudManager = new HudManager(heartRateManager);
        GameObject goal = GameObject.Find("Level Goal");
        winManager = new WinTransitionManager(this, new Rect(goal.transform.position.x, goal.transform.position.y, 5.0f, 10.0f));
	}
	void FixedUpdate () {
		//transform.Translate(5f*Time.deltaTime, 0f, 0f);

        CheckHitRecovery();
		UpdateBPM();
		Move();
		percentageFinished = transform.localPosition.x / FloorManager.LEVEL_TILES.Length;
		TrackPlayer();
		
		if (BPM >= 170.0f && heartBeat.isPlaying ==false){
			heartBeat.Play();
		}
		
        hudManager.Update();
        winManager.Update();
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
        // Update the BPM counter
        GameObject camera = GameObject.Find("Main Camera");
        GameObject hudSprite = GameObject.Find("hudOkSprite");
        Vector3 hudWorldPos = new Vector3(hudSprite.transform.position.x - 0.75f, 9.3f); // I don't get the y position
        Vector3 hudScreenPos = camera.camera.WorldToScreenPoint(hudWorldPos);
        float width = 100.0f;
        float height = 20.0f;
        GUIStyle style = new GUIStyle();
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;
		GUI.Label(new Rect (hudScreenPos.x,hudScreenPos.y,width,height), BPM.ToString("0"), style);
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
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump")){
				//if (jump.isPlaying == false){
				//	jump.Play();
				//}
                moveDirection.y = jumpSpeed;
                state = "jumping";
				//jump.Stop();
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
			//GameObject.Find("eletricalshock").audio.Play();
			//Debug.Log("shocked");
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
    public void BecomeInvincible()
    {
        // Ignore the wave
        GameObject wave = GameObject.Find("Wave");
        Destroy(wave.rigidbody);
    }

}
