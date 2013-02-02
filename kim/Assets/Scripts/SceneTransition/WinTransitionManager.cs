using UnityEngine;

public class WinTransitionManager
{
    private float winCountdown = 0.0f;
    private bool didWin = false;
    private PlayerScript player;
    private Rect goalHitbox;

    public WinTransitionManager(PlayerScript player, Rect goalHitbox)
    {
        this.player = player;
        this.goalHitbox = goalHitbox;
    }

    public void Update()
    {
        bool reachedGoal = goalHitbox.Contains(player.transform.position);
        if (reachedGoal)
        {
            triggerCountdown();
        }
        if (didWin)
        {
            winCountdown -= Time.deltaTime;
            if (winCountdown <= 0.0f)
            {
                Application.LoadLevel("WinScreen");
                didWin = false;

            }

        }
    }

    private void triggerCountdown()
    {
        didWin = true;
        winCountdown = 2.0f;
        player.BecomeInvincible();
    }
}


