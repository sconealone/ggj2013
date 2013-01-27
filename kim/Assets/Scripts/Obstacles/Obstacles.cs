using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public int hitPoints = 1;
    private bool isScheduledForDestroy = false;
    private float destroyTimer;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (hitPoints <= 0 && !isScheduledForDestroy)
        {
            ScheduleForDestroy();
        }

        if (collisionInfo.gameObject.name.Equals("Wave"))
        {
            //Destroy(gameObject);

        }
        else if (collisionInfo.gameObject.name.Equals("Player"))
        {
            hitPoints--;
        }
    }

    void ScheduleForDestroy()
    {
        isScheduledForDestroy = true;
        destroyTimer = 0.0f;
    }   

    void Update()
    {
        if (isScheduledForDestroy)
        {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer<= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

}
