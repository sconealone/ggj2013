using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    private bool killCountdown = false;
    private float timer = 1.0f;

	void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("collided with " + collisionInfo.gameObject.name);
		if (collisionInfo.gameObject.name.Equals("Player"))
        {
            StartKillCountdown(0.5f);
		}
    }

    private void StartKillCountdown(float countdownTime)
    {
        killCountdown = true;
        timer = countdownTime;
    }

    void Update()
    {
        if (killCountdown)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                Destroy(gameObject);
                killCountdown = false;
            }
        }
    }
}
