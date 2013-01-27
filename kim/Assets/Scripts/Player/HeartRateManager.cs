using System;

public class HeartRateManager
{
    // Rates
    public static float RUNNING_HEART_RATE = 6.0f;
    public static float JOGGING_HEART_RATE = -2.0f;
    public static float RECOVER_STAMINA_HEART_RATE = -20.0f;

    // Caps
    public static float MIN_HEART_RATE = 110.0f;
    public static float MODERATE_HEART_RATE_BREAKPOINT = 140.0f;
    public static float HIGH_HEART_RATE_BREAKPOINT = 170.0f;
    public static float MAX_HEART_RATE = 190.0f;

    public static float STAMINA_RECOVERY_TIME = 3.0f;

    // Penalties
    public static float DURABLE_OBJECT_PENALTY = 5.0f;
    public static float DESTRUCTABLE_OBJECT_PENALTY = 10.0f;
    public static float COLLECTIBLE_ITEM_REWARD = -20.0f;
    public static float JUMP_PENALTY = 2.5f;

    private float currentHeartRate;
    private PlayerScript player;

    public HeartRateManager(PlayerScript player)
    {
        this.player = player;
        currentHeartRate = MIN_HEART_RATE;
    }

    // Pass in the time delta
    // Return if player should be in a tired state
    public bool Update(float dt)
    {
        String currentState = player.GetState();
        if (currentState.Equals("running"))
        {
            currentHeartRate += RUNNING_HEART_RATE*dt;

        }
        else if (currentState.Equals("jogging"))
        {
            currentHeartRate += JOGGING_HEART_RATE*dt;
        }
        else if (currentState.Equals("jumping"))
        {
            currentHeartRate += RUNNING_HEART_RATE*dt;
        }
        else if (currentState.Equals("recoverStamina"))
        {
            currentHeartRate += RECOVER_STAMINA_HEART_RATE*dt;
            if (currentHeartRate < MODERATE_HEART_RATE_BREAKPOINT)
            {
                player.SetState("running");
            }
        }

        return currentHeartRate > MAX_HEART_RATE;
    }

    public float GetCurrentHeartRate()
    {
        return currentHeartRate; 
    }

    public void AssignPenalty(String reasonForPenalty)
    {
        if (reasonForPenalty.Equals("Collectable"))
        {
            currentHeartRate += COLLECTIBLE_ITEM_REWARD;
        }
        else if (reasonForPenalty.Equals("Destructable Object") || reasonForPenalty.Equals("Light"))
        {
            currentHeartRate += DESTRUCTABLE_OBJECT_PENALTY;
        }
        else if (reasonForPenalty.Equals("jumping"))
        {
            currentHeartRate += JUMP_PENALTY;
        }
        else if (reasonForPenalty.Equals("Obstacles"))
        {
            currentHeartRate += DURABLE_OBJECT_PENALTY;
        }
        
    }
}
